# Telemetria local dos trials

Cada sessão de jogo produz um arquivo JSON UTF-8 com a versão do esquema **1.0**, os estímulos apresentados em ordem e as respostas aceitas pelo jogo. Os IDs são GUIDs aleatórios de uso local. O arquivo contém dados da tarefa, como a condição, a resposta e o tempo, sem identificação do jogador ou do dispositivo.

## Arquitetura

`GameApplication` cria um `TrialTelemetrySession` para cada `GameSession` e o entrega ao `TrialController`. O controlador comunica as apresentações, respostas, pausas e retomadas usando os dados do estímulo exibido e o resultado já calculado por `GameSession.IsCorrect`. O gabarito vem de `TrialDefinition.structuralMatch`, o mesmo campo usado para escolher a malha da candidata e julgar a resposta.

`TrialTelemetrySession` mantém o registro de sessão, as ocorrências e as tentativas em memória. `TrialTelemetryRecords.cs` define objetos de dados serializáveis, com campos simples e listas. `TrialTelemetryJsonStore` serializa esses objetos com `JsonUtility` e publica o arquivo local. O serviço observa o fluxo existente de jogo. Cada execução da aplicação e cada uso de **Reiniciar** começam uma nova sessão no menu. A demonstração do tutorial fica fora da lista `trials`.

## Local e gravação

O arquivo fica em `Application.persistentDataPath/Telemetry/session_<session_id>.json`. Nesta configuração Windows, `Application.persistentDataPath` corresponde a `C:\Users\<usuário>\AppData\LocalLow\DefaultCompany\MeuPequenoReino`. O jogo a cria na primeira gravação. Abra a pasta colando o caminho na barra do Explorador e abra um arquivo JSON com um editor de texto. O caminho base varia conforme a plataforma e as configurações de produto da Unity.

O jogo grava ao criar a sessão, ao revelar um trial novo, a cada resposta aceita, ao retomar uma pausa explícita e ao encerrar a sessão. A resposta correta e o estado `completed` entram juntos na mesma gravação. Um trial ativo recebe `interrupted` se a aplicação for encerrada normalmente, o objeto da aplicação for destruído ou a sessão for reiniciada antes do acerto. A conclusão do reino encerra a coleta ao chegar à vila final.

Cada gravação escreve um temporário exclusivo na mesma pasta, fecha e publica o arquivo por movimentação na primeira gravação e por substituição nas seguintes. A publicação inicial recusa sobrescrever qualquer arquivo existente. Se uma gravação falhar, o Console registra o erro e a próxima gravação tenta publicar o estado em memória, incluindo eventos registrados desde a última gravação bem-sucedida. Uma falha abrupta pode deixar um temporário e perder eventos ainda em memória. O último JSON publicado permanece como estava, inclusive com `in_progress` se o trial estava ativo. A próxima execução cria outro arquivo sem tentar reparar ou reclassificar o anterior.

## Campos do JSON

| Objeto | Campo | Tipo e significado |
| --- | --- | --- |
| Sessão | `schema_version` | Texto. Versão do contrato local, inicialmente `1.0`. |
| Sessão | `session_id` | Texto. GUID aleatório da sessão, também usado no nome do arquivo. |
| Sessão | `game_version` | Texto. `Application.version`, configurado em Player Settings e atualmente `1.0`. |
| Sessão | `started_at_utc` | Texto. Instante de criação em UTC no formato ISO 8601, com sufixo `Z`. |
| Sessão | `trials` | Array de ocorrências, em ordem de apresentação. |
| Trial | `trial_id` | Texto. GUID aleatório da ocorrência, preservado em todas as tentativas e retomadas. |
| Trial | `trial_index` | Inteiro. Posição da ocorrência na sessão a partir de 1, contínua entre construções. |
| Trial | `building_id` | Texto. ID da construção em `BuildingDefinition.id`. |
| Trial | `model_id` | Inteiro. ID do estímulo em `TrialModel.modelId`. |
| Trial | `condition` | Texto. `rotated` para a malha de referência rotacionada ou `mirrored_rotated` para a malha espelhada rotacionada. |
| Trial | `rotation_angle_degrees` | Número. Rotação nominal em graus, lida da definição do estímulo. |
| Trial | `rotation_axis` | Texto. `X`, `Y` ou `Z` para o eixo positivo configurado. Um eixo fora do esquema produz aviso no Console e campo vazio. |
| Trial | `expected_answer` | Texto. `SERVE` ou `NAO_SERVE` conforme o gabarito da definição. |
| Trial | `attempts` | Array de respostas aceitas, em ordem. |
| Trial | `paused_duration_ms` | Inteiro. Tempo de pausa explícita acumulado nessa ocorrência, em milissegundos. |
| Trial | `status` | Texto. `in_progress` na revelação, `completed` no acerto ou `interrupted` no encerramento detectado de um trial ativo. |
| Tentativa | `attempt_index` | Inteiro. Posição da resposta dentro do trial a partir de 1. |
| Tentativa | `player_answer` | Texto. `SERVE` ou `NAO_SERVE`. |
| Tentativa | `is_correct` | Booleano. Resultado da avaliação feita pelo jogo para essa resposta. |
| Tentativa | `response_time_ms` | Inteiro. Tempo ativo medido para essa tentativa, em milissegundos. |

## Temporização e limites

O controlador continua usando `Time.realtimeSinceStartupAsDouble`. Na primeira tentativa o cronômetro começa no callback de revelação, imediatamente antes de liberar as respostas. No erro, os botões permanecem bloqueados durante o feedback de 1,2 s. O cronômetro da próxima tentativa começa quando os botões voltam a ser liberados. A medição termina ao aceitar a resposta em `TrialController.Answer`. O tempo de cada resposta equivale à soma dos trechos ativos da sua tentativa, convertida de segundos para milissegundos e arredondada para o inteiro mais próximo.

Ao usar a opção de visitar a vila, o cronômetro da tentativa preserva o trecho ativo anterior. O trecho seguinte começa na nova revelação, quando as respostas são liberadas. `paused_duration_ms` soma os intervalos entre a pausa explícita aceita e o comando aceito de retomar. Se a sessão for encerrada durante uma pausa, soma o trecho até o encerramento. A preparação entre a retomada e a liberação é excluída do tempo da resposta e fica fora da duração da pausa explícita. Em um fechamento abrupto durante uma pausa, apenas os intervalos já gravados ficam disponíveis.

A temporização mede callbacks de software. O primeiro quadro visível, a latência de exibição e entrada, a perda de foco e a suspensão do sistema operacional ainda exigem validação experimental. Uma suspensão fora dos controles do jogo pode aparecer no relógio de tempo real. A gravação é síncrona e executada fora dos trechos ativos da tentativa. Em armazenamento lento ela pode atrasar um quadro. O teste de substituição de arquivo cobre Windows, Unity 6000.3.25f1 e o sistema de arquivos local dessa estação. Uma plataforma em que a substituição falhar preserva o último arquivo e registra o erro no Console.

## Reproduzir as verificações

Use uma cópia do projeto com um nome de produto de teste, para criar os registros sintéticos em outra pasta. Em Unity 6000.3.25f1, use `-batchmode -projectPath "<cópia>" -executeMethod TrialTelemetryChecks.RunStorage -quit` para os testes da gravação e `-batchmode -projectPath "<cópia>" -executeMethod TrialTelemetryChecks.RunPlay` para o cenário de Play Mode. O próprio cenário encerra a Unity ao terminar. Os resultados ficam em `Library/TelemetryValidation/storage.txt` e `Library/TelemetryValidation/play.txt`. A verificação preexistente `PlayModeFlowChecks.Run` percorre o tutorial, os 50 trials, a vila, o observatório e o reinício.

## Integração futura

O consumidor deve interpretar o arquivo como o retrato mais recente de uma sessão identificada por `session_id`, preservando `trial_id` e `attempt_index` ao deduplicar dados. Uma sessão pode terminar com um trial parcial. Mudanças incompatíveis exigem nova versão do esquema. O destino local está concentrado em `TrialTelemetryJsonStore`, o que permite substituí-lo ou adicionar outro consumidor dos registros sem mover a lógica de coleta para o controlador. O protocolo de envio, a política de sincronização e o mapeamento semântico ficam para a integração posterior.

## Resultados da validação

A execução usou Unity 6000.3.25f1 (e1dba0a9aba4) no Windows. Os testes automatizados de armazenamento passaram em **7 verificações**, incluindo recuperação após falha de I/O. O cenário focado em Play Mode passou em **13 verificações**, com erro, acerto posterior, pausa, retomada, reinício e encerramento. A primeira execução do cenário focado atingiu o limite de tempo. O roteiro foi ajustado para conduzir o tutorial em quadros separados e foi executado novamente.

A regressão completa passou em **81 verificações**. O percurso concluiu os 50 estímulos na ordem configurada, completou cinco construções e percorreu vila, observatório e reinício. A validação estrutural registrou **790 checagens semânticas** e **59.906 verificações de referências serializadas**. Um parser independente leu quatro arquivos, comparou os 50 estímulos com o contrato original e confirmou, em ordem, os valores das **53 respostas** registradas também no Console. Os cenários usaram os eventos dos botões na cena real em Play Mode.

## Exemplo gerado na execução controlada

O primeiro trial teve erro aos **379 ms** e acerto aos **651 ms**. A pausa de **1.366 ms** ficou no campo próprio. O segundo trial estava pausado quando foi reiniciado e foi gravado como interrompido, com **312 ms** de pausa e sem respostas.

```json
{
    "schema_version": "1.0",
    "session_id": "49938327a5c4465482e631b9a7e30fa3",
    "game_version": "1.0",
    "started_at_utc": "2026-11-26T13:31:38.1114152Z",
    "trials": [
        {
            "trial_id": "eac4d83828e54a34b7f61b6a1653465b",
            "trial_index": 1,
            "building_id": "Observatorio",
            "model_id": 0,
            "condition": "mirrored_rotated",
            "rotation_angle_degrees": 45.0,
            "rotation_axis": "Y",
            "expected_answer": "NAO_SERVE",
            "attempts": [
                {
                    "attempt_index": 1,
                    "player_answer": "SERVE",
                    "is_correct": false,
                    "response_time_ms": 379
                },
                {
                    "attempt_index": 2,
                    "player_answer": "NAO_SERVE",
                    "is_correct": true,
                    "response_time_ms": 651
                }
            ],
            "paused_duration_ms": 1366,
            "status": "completed"
        },
        {
            "trial_id": "330d911261984e7e9e2b9d6f6a77d772",
            "trial_index": 2,
            "building_id": "Observatorio",
            "model_id": 1,
            "condition": "rotated",
            "rotation_angle_degrees": 45.0,
            "rotation_axis": "Y",
            "expected_answer": "SERVE",
            "attempts": [],
            "paused_duration_ms": 312,
            "status": "interrupted"
        }
    ]
}
```
