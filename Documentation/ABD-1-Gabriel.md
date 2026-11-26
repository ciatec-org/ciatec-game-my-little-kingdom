---
faixa: CIATec
doc: Plano de trabalho
doc_extenso: Agile Backlog Document
titulo: Meu Pequeno Reino
subtitulo: Plano de trabalho do Gabriel, com o agente de IA dele, do Core à Expansão
versao: v13
mudanca: decisões do Gabriel no PR #432, a pedido do orientador
data: 26/11/2026
projeto_vinculado: Meu Pequeno Reino (ciatec-org/ciatec-game-my-little-kingdom)
organizacao: CIATec
triple_diamond:
ppi:
pesquisador: Gabriel Milagres
tipo: backlog
status: rascunho
data_criacao: 25/11/2026
data_atualizacao: 26/11/2026
---

# 1 COMO USAR ESTE PLANO

## 1.1 Para quem é

**Este plano é do Gabriel, que constrói o Meu Pequeno Reino com um agente de IA.** Ele diz o que construir, em que ordem, e em pedaços pequenos o bastante para caber numa sessão de trabalho. O que o jogo é, e por quê, está no SGDD (`SGDD-1-Gabriel.md`, v1.5): este plano não decide nada sobre o jogo, só organiza o trabalho de construí-lo. **Toda dúvida sobre como algo deve acontecer se responde no SGDD**; se ele não responde, a dúvida vai ao orientador, e a resposta volta ao SGDD.

**O jogo já existe em boa parte.** O Gabriel migrou o protótipo para a Unity e, em 26/11/2026, juntou a telemetria local (PR #431, `develop` @ 7417541): a Vila, as obras, as 50 peças, o retrato do habitante, as 7 telas e o arquivo JSON de cada sessão, com todas as respostas de cada tentativa. O que o código já faz está em cada história como **task fechada**, com `arquivo:linha` e 0 h; o plano só pede o que falta. **Onde o código já decide uma regra, o SGDD e este plano seguem o código** (regra do orientador, 26/11/2026).

**A tecnologia é a do projeto:** Unity 6000.3.25f1, URP 17.3, uGUI e só o Input System 1.20. A build é só para o PC (Windows, mouse e teclado). As regras do jogo ficam no assembly `Game.Core`, sem a engine, com os testes em `Tests.EditMode` e `Tests.PlayMode`. A telemetria tem o formato do Gabriel (`Documentation/TrialTelemetry.md`) e se confere pelos verificadores de Editor dele (`TrialTelemetryChecks`, `KingdomValidation`), que o plano estende em vez de refazer.

## 1.2 O ritmo

**O plano não tem datas.** A carga horária do Gabriel e a vazão do agente não são conhecidas (LA-02, decidida pelo orientador em 26/11/2026): o Gabriel estima a task na hora de puxá-la, e o roteiro da seção 5 diz só a ordem. Os dois milestones são **Core** (o jogo jogável de ponta a ponta, com a telemetria) e **Expansão** (o resto do SGDD); os dois juntos são o jogo inteiro.

## 1.3 O quadro e o caminho de uma task

**O quadro tem quatro colunas: A fazer, Fazendo, Em revisão e Feito.** Uma task por vez; o agente escreve o código e os testes, e o Gabriel monta na cena, roda os testes no Test Runner e joga. As tasks fechadas ("Já existe") entram no quadro já em Feito.

## 1.4 Pronta para começar, e pronta de verdade

**Uma história está pronta para entrar** quando as seções do SGDD que ela cita foram lidas, as histórias de que ela depende estão feitas, e não há dúvida aberta sobre o que ela pede.

**Uma história está feita** quando:

- cada requisito da lista dela foi visto acontecer, jogando no Play do Editor ou na build de PC;
- os testes que as tasks dela escrevem passam no Test Runner;
- o Console não mostra exceção durante uma tentativa jogada.

**Teste só onde há regra.** A task que escreve uma regra (a tentativa, a streak, os pontos, o save, o grafo das obras) escreve também o teste dela, EditMode ou PlayMode; não há task só de teste. O resto se confere jogando.

## 1.5 As camadas

Cada task tem uma camada: **gameplay** (regra em C#), **presentation** (tela e cena), **dados** (save, definições), **conteúdo** (falas, textos, cadastro de obras), **arte** (sprites, sons, materiais), **telemetria** (o arquivo da sessão) e **build**. Montar na cena entra na mesma task do código ("…e ligar na cena").

## 1.6 Como ler uma história

Cada história traz o perfil, as horas, a prioridade, o milestone e de que histórias depende; a história no formato *Como… quero… para que…*; **a lista dos requisitos do SGDD que ela realiza, um por linha** — o texto vem do SGDD (§12 numerado de 01 a 35, na ordem da tabela; LG-Gab da §13B; e as seções citadas), sem resumo; os cenários de aceite; e as tasks, de 1 ou 2 h cada, com a camada. A dependência entre tasks está no CSV (`blocked_by`).

> **As histórias de marco** (uma por milestone) só fecham com a build de PC jogada do menu ao fim, e com o arquivo de telemetria da sessão conferido contra a jogada. São a única prova de que as peças foram juntadas.

# 2 CAPACIDADE E PREVISÃO

**O plano tem 9 épicos, 25 histórias e 106 tasks (88 abertas e 18 já feitas), somando 167 horas.** Sem a carga horária do Gabriel (LA-02), não há previsão em semanas: depois de algumas tasks, ele soma as horas que fechou por semana e refaz a conta.

| Épico | Milestone | Histórias | Tasks | Horas |
| --- | --- | --- | --- | --- |
| EP-01 · Fundação do projeto | Core | 1 | 4 | 6 h |
| EP-02 · A tentativa | Core | 4 | 17 | 14 h |
| EP-03 · O reino | Core | 4 | 13 | 18 h |
| EP-04 · A sessão no PC e o marco do Core | Core | 4 | 11 | 19 h |
| EP-05 · A história da peça e a comparação calma | Expansão | 2 | 10 | 20 h |
| EP-06 · Moedas e decoração | Expansão | 2 | 8 | 15 h |
| EP-07 · O reino inteiro | Expansão | 3 | 20 | 34 h |
| EP-08 · Tutorial, som e acessibilidade | Expansão | 2 | 12 | 22 h |
| EP-09 · Pesquisa e marco da Expansão | Expansão | 3 | 11 | 19 h |

| Milestone | Histórias | Tasks (abertas + já feitas) | Horas | O que se vê |
| --- | --- | --- | --- | --- |
| Core | 13 | 45 (30 + 15) | 57 h | A build de PC, do menu às 5 obras do Core: o código da criança, o habitante, a tentativa com a primeira resposta e o RT certos, o retorno com a chama e os pontos, a Vila, a pausa, o fim de sessão pelo mediador, e o arquivo da sessão |
| Expansão | 12 | 61 (58 + 3) | 110 h | O jogo inteiro: o Grande Livro e as quatro explicações, a comparação calma, as moedas e a decoração, as dez obras, os interiores, o tutorial, o som e a acessibilidade, a dificuldade de cada peça recebida do provedor, e a tarefa de referência |

**A meta do molde (de 50 a 70 tasks) não cabe neste jogo.** São 167 h de trabalho, e com tasks de no máximo 2 h isso pede pelo menos 84 tasks abertas; o peso está na Expansão, que tem cinco obras novas com arte e cadastro, três interiores, a decoração, o tutorial de cinco passos, o som e a baixa visão. Chegar a 70 pediria tasks de 3 a 4 h ou um corte de escopo, e as duas coisas são do orientador.

# 3 ÉPICOS

Cada épico está inteiro na seção 4: a descrição, por que importa, quando está pronto, e as histórias e tasks dele.

| Épico | Domínio e milestone | Depende de |
| --- | --- | --- |
| EP-01 · Fundação do projeto | Setup | — |
| EP-02 · A tentativa | Gameplay · Core | EP-01 |
| EP-03 · O reino | Gameplay · Core | EP-02 |
| EP-04 · A sessão no PC e o marco do Core | Interface · Core | EP-02, EP-03 |
| EP-05 · A história da peça e a comparação calma | Narrativa · Expansão | EP-03, EP-04 |
| EP-06 · Moedas e decoração | Recompensa · Expansão | EP-02, EP-03, EP-04 |
| EP-07 · O reino inteiro | Mundo · Expansão | EP-03, EP-06 |
| EP-08 · Tutorial, som e acessibilidade | Interface · Expansão | EP-04, EP-05, EP-06, EP-07 |
| EP-09 · Pesquisa e marco da Expansão | Dados · Expansão | EP-05, EP-06, EP-07, EP-08 |

# 4 ÉPICOS, HISTÓRIAS E TASKS

As histórias estão na ordem em que o time as faz. As tasks de cada uma também: as de cima vêm primeiro. Task fechada é o que o código do Gabriel já faz.

## 4.1 EP-01 — Fundação do projeto

Os assemblies, as regras do jogo num assembly sem a engine e os testes que rodam no Editor, sobre o projeto que o Gabriel já migrou do protótipo.

**Por que importa:** Cada regra do jogo (a tentativa, a streak, os pontos, o save) vira classe C# pura com o teste dela; sem esse assembly, nenhuma regra fecha com teste rodado.

**Pronto quando:**

- O projeto compila com os cinco asmdefs, e os scripts de Editor do aluno continuam abrindo pelo menu;
- O Test Runner roda um teste EditMode e um PlayMode de fumaça, verdes.

## 4.1.1 US-01.1 — O projeto organizado e testável

**Perfil:** Time (IC) · **Horas:** 6 · **Prioridade:** P1 · **Milestone:** Core · **Depende de:** —

*Como o time, quero separar as regras do jogo num assembly sem a engine e ter testes que rodam no Editor, para que cada regra nova feche com teste rodado, e não só escrito.*

**Requisitos do SGDD que ela realiza:**

- `§12-34` — Manter regras centrais independentes da engine sempre que possível

**Cenários de aceite:**

- Dado o assembly das regras, quando alguém roda os testes EditMode no Test Runner, então o teste do `GameSession` passa sem carregar cena.
- Dado o projeto aberto, quando alguém roda o teste PlayMode de fumaça, então o mínimo montado por código chega ao primeiro desafio sem exceção no Console.
- Dado uma classe de regra que usa `UnityEngine`, quando o assembly das regras compila, então a compilação falha e aponta a dependência.

| Task | O que fazer | Horas e camada |
| --- | --- | --- |
| T-01.1.1 | **Já existe:** o projeto Unity 6000.3.25f1, com URP 17.3, uGUI e só o Input System 1.20 (`activeInputHandler: 1`); as cenas `Kingdom` e `Village` na build (`ProjectSettings/EditorBuildSettings.asset:9,12`); e as 7 telas do jogo (`GameScreen`, `GameSession.cs:3-12`), que ficam no lugar dos 16 estados do §10.3 | já feita · build |
| T-01.1.2 | Criar os asmdefs `Game`, `Game.Core` (sem referência à engine), `Game.Editor` (os scripts de Editor do aluno, que ficam e continuam abrindo pelo menu), `Tests.EditMode` e `Tests.PlayMode`, com o `testables` do Input System no `Packages/manifest.json`, e um teste PlayMode de fumaça montado por código que chega ao primeiro desafio sem exceção | 2 h · build |
| T-01.1.3 | Passar o `GameSession`, o `AnswerResult` e as regras puras para `Game.Core` (namespace `MeuPequenoReino.Core`), recebendo os dados das obras em tipos C# puros montados fora dele (hoje ele lê ScriptableObjects, `GameSession.cs:24,34,51,88`) | 2 h · gameplay |
| T-01.1.4 | Isolar o `Vector3` do `TrialDefinition`, dar o namespace `MeuPequenoReino` aos outros scripts (e o `using` que faltar aos de Editor do aluno), e escrever o teste EditMode do `GameSession`: desbloqueio, pausa e conclusão | 2 h · gameplay |

## 4.2 EP-02 — A tentativa

O coração do jogo: as duas peças, SERVE ou NÃO SERVE, a primeira resposta e o tempo dela, os estados de validade, o arquivo da sessão e o retorno, com a streak e os pontos.

**Por que importa:** É a tarefa de rotação mental que a pesquisa mede. O resto do jogo enfeita, organiza ou guarda esta tentativa.

**Pronto quando:**

- Uma tentativa grava a primeira resposta e o RT certos, mesmo com erro, pausa ou perda de foco;
- O retorno não pune, e a placa do acerto mostra a chama e os pontos;
- O arquivo da sessão (o JSON do Gabriel, um por sessão) tem um registro por tentativa, com os campos do §11.4 que o Core já tem.

## 4.2.1 US-02.1 — Comparar as duas peças e responder

**Perfil:** Criança · **Horas:** 4 · **Prioridade:** P1 · **Milestone:** Core · **Depende de:** US-01.1

*Como criança, quero ver a peça do projeto e a peça que o habitante trouxe e dizer se serve ou não serve, para que eu decida olhando a forma, sem pressa e sem girar nada.*

**Requisitos do SGDD que ela realiza:**

- `§12-01` — Apresentar exatamente uma referência ativa e um candidato por tentativa
- `§12-03` — Registrar RT somente após estímulos e controles estarem completamente disponíveis
- `§12-06` — Impedir rotação manual dos estímulos durante tentativa válida
- `§12-07` — Manter tentativas de treinamento separadas das tentativas válidas
- `§12-24` — Não utilizar cronômetro visível ou pressão temporal explícita
- `§12-29` — Ângulo, complexidade e similaridade do distrator devem ser parametrizáveis *(os campos existem no Core; o cálculo da complexidade e da similaridade é da US-09.1)*
- `LG-Gab-01` — Rotação em profundidade, em torno do eixo x, y ou z, como no conjunto original (v1.4, conforme a implementação do aluno)
- `§4.1` — Metadados mínimos do estímulo: identificador da referência; identificador do candidato; condição `MATCH` ou `NON_MATCH`; orientação de referência; orientação do candidato; disparidade angular; sentido da rotação; `complexity_score`; medida de similaridade do distrator

**Cenários de aceite:**

- Dado o habitante terminou de chegar, quando as duas peças e os botões aparecem, então a tentativa está em TRIAL_ACTIVE e o RT começa nesse quadro.
- Dado a tentativa em TRIAL_SETUP, quando a criança clica em SERVE, então nada é registrado e os botões seguem desligados.
- Dado uma tentativa `VALID`, quando a criança tenta arrastar a peça, então nada gira, e nenhum cronômetro aparece.
- Dado os 50 estímulos, quando roda o `KingdomValidation`, então cada um bate com o `OriginalTrialContract.json` na forma, no gabarito, no ângulo e no eixo; uma peça de 225° grava `rotation_angle_degrees` 225°, e a visão derivada (T-09.2.3) dá a disparidade 135°.

| Task | O que fazer | Horas e camada |
| --- | --- | --- |
| T-02.1.1 | **Já existe:** as 50 peças (`TrialModel`, policubos com células, malha da referência e malha espelhada, `TrialModel.cs:6-10`) e a tentativa que mostra referência e candidato juntos, com SERVE e NÃO SERVE, sem giro pela criança, medindo o RT com relógio monotônico (`TrialController.cs:59-75,96`) | já feita · gameplay |
| T-02.1.2 | Escrever em C# puro (`Game.Core`) a máquina da tentativa — TRIAL_SETUP → TRIAL_ACTIVE → RESPONSE_RECEIVED → FEEDBACK → TRIAL_END — e o tipo `TRAINING`/`VALID`, com teste EditMode das transições | 2 h · gameplay |
| T-02.1.3 | Ligar o `TrialController` à máquina e começar o RT no primeiro quadro desenhado com as peças e os botões ativos (hoje começa no callback que os ativa, `TrialController.cs:72-74`, e o `TrialTelemetry.md:50` deixa o primeiro quadro visível para validar), com teste PlayMode montado por código | 2 h · presentation |
| T-02.1.4 | **Já existe:** os metadados do §4.1 que o jogo usa, lidos do `TrialDefinition` e gravados em cada tentativa — o estímulo (`model_id`), a condição (`condition`, `rotated` ou `mirrored_rotated`, e o gabarito em `expected_answer`), o ângulo nominal (`rotation_angle_degrees`) e o eixo (`rotation_axis`) (`TrialController.cs:65-71,156-166`, `TrialTelemetrySession.cs:28-49`); a referência é sempre a peça sem giro e o candidato a mesma peça girada no eixo, espelhada ou não (`TrialController.cs:50-51`); e a conferência dos 50 contra o `OriginalTrialContract.json` na forma, no gabarito, no ângulo e no eixo (`KingdomValidation.cs:40-55`). A disparidade de 0° a 180° e o `rotation_direction` saem da visão derivada (T-09.2.3), sem campo novo no arquivo; a complexidade e a similaridade, na US-09.1 | já feita · dados |

## 4.2.2 US-02.2 — O dado certo mesmo com erro, pausa ou ausência

**Perfil:** Pesquisador · **Horas:** 2 · **Prioridade:** P1 · **Milestone:** Core · **Depende de:** US-02.1

*Como pesquisador, quero que a tentativa guarde a primeira resposta e o tempo dela, deixe o tempo da pausa fora do RT e feche `interrupted` quando a criança sai da janela ou para de responder, para que só o tempo da comparação entre na medida.*

**Requisitos do SGDD que ela realiza:**

- `§12-04` — Registrar apenas a primeira resposta válida *(no arquivo, a primeira resposta e o RT dela ficam no `attempts[0]`; a escolha da primeira resposta válida é da visão derivada (T-09.2.3), decisão do orientador de 26/11/2026: vale o que o Gabriel decidiu no código e no PR #432)*
- `LG-Gab-03` — Depois de um erro a mesma peça volta; registra-se a primeira resposta (v1.4, conforme a implementação do aluno)
- `§11.2.1` — Cada tentativa terá um dos seguintes estados: `COMPLETED`, `PAUSED`, `INTERRUPTED`, `ABANDONED` *(no arquivo valem os estados do modelo do Gabriel: `completed`, `interrupted` e `in_progress`; a pausa fica no `paused_duration_ms`, fora do RT, e o abandono fecha `interrupted`; decisão do orientador de 26/11/2026: vale o que o Gabriel decidiu no código e no PR #432)*
- `§11.2` — Interrupção técnica: perda de foco do aplicativo ou evento que comprometa a medição invalida o RT da tentativa
- `LG-Gab-21` — Pergunta neutra aos 45 s, abandono aos 75 s; “parar” fecha a sessão e mostra a Vila *(no Core, aos 75 s sem resposta a tentativa fecha `interrupted`, sem a pergunta; a pergunta é da US-05.2)*

**Cenários de aceite:**

- Dado a criança respondeu errado, quando a mesma peça volta e ela acerta, então o arquivo guarda as duas respostas em `attempts`, a primeira com o RT dela, numa tentativa só (o mesmo `trial_id`), e o progresso não muda.
- Dado SERVE clicado, quando ela clica de novo antes do retorno, então o segundo clique é ignorado.
- Dado uma tentativa ativa, quando a janela perde o foco e volta, então a tentativa fecha `interrupted` e a peça volta numa tentativa nova.
- Dado uma tentativa ativa, quando ela clica VILA, depois RETOMAR, e acerta, então a tentativa fecha `completed`, com o tempo parado no `paused_duration_ms` e fora do RT.
- Dado a configuração padrão, quando ela fica 75 s sem responder, então a tentativa fecha `interrupted`.

| Task | O que fazer | Horas e camada |
| --- | --- | --- |
| T-02.2.1 | **Já existe** (`TrialController.cs:104-113`, `GameSession.cs:84-104`): depois do erro a mesma peça volta, sem tentativa nova (LG-Gab-03) | já feita · gameplay |
| T-02.2.2 | **Já existe** (PR #431): a tentativa guarda cada resposta aceita em `attempts`, em ordem, com o RT dela (`attempt_index`, `player_answer`, `is_correct`, `response_time_ms`), e só fecha no acerto, então a primeira resposta e o RT dela ficam no `attempts[0]` e o número de respostas é o tamanho de `attempts` (`TrialTelemetrySession.cs:51-68`, `TrialTelemetryRecords.cs:33-39`); a reapresentação depois do erro e a volta da pausa não abrem tentativa nova (`TrialController.cs:65-71`); o clique duplo é ignorado (`TrialController.cs:89-91`); conferido pelo `TrialTelemetryChecks.RunPlay` (`TrialTelemetryChecks.cs:193-201,228-236`) | já feita · gameplay |
| T-02.2.3 | **Já existe** (PR #431): os estados da tentativa no modelo do Gabriel — `in_progress` ao revelar, `completed` no acerto e `interrupted` quando a tentativa ativa acaba sem acerto (`TrialTelemetrySession.cs:45,64,95-102`); a pausa não muda o estado: o tempo parado fica no `paused_duration_ms`, fora do RT, que soma só os trechos ativos (`TrialTelemetrySession.cs:70-82,104-111`, `TrialController.cs:137-138`). A validade do RT é da visão derivada (T-09.2.3) | já feita · gameplay |
| T-02.2.4 | Fechar a tentativa `interrupted` — o estado do modelo do Gabriel para a tentativa ativa que acaba sem acerto (`TrialTelemetrySession.cs:95-102`) — na perda de foco, num quadro acima de 100 ms ou na mudança de resolução em TRIAL_ACTIVE (L-ABD-16), e aos 75 s sem resposta, com o `TrialTimeoutConfig` (45 s e 75 s, LG-Gab-21) como asset ligado ao `TrialController` na cena `Kingdom`; a peça volta numa tentativa nova; estender o `TrialTelemetryChecks.RunPlay` com a perda de foco e os 75 s | 2 h · presentation |

## 4.2.3 US-02.3 — O arquivo da sessão

**Perfil:** Pesquisador · **Horas:** 1 · **Prioridade:** P1 · **Milestone:** Core · **Depende de:** US-02.2

*Como pesquisador, quero um arquivo por sessão com um registro por tentativa e todas as respostas, de onde saem os campos do §11.4, para que eu confira depois o que foi apresentado e o que a criança respondeu.*

**Requisitos do SGDD que ela realiza:**

- `§12-08` — Registrar parâmetros efetivamente apresentados em cada tentativa
- `§11.4` — Cada tentativa deverá registrar, no mínimo: `session_id`, `trial_id`, `trial_type`, `stimulus_reference_id`, `stimulus_candidate_id`, `match_condition`, `rotation_angle`, `rotation_direction`, `complexity_score`, `distractor_similarity`, `difficulty_state`, `response`, `accuracy`, `reaction_time_ms`, `trial_status`, `streak_before`, `streak_after`, `base_points`, `streak_multiplier`, `agility_bonus`, `points_earned`, `timestamp_start`, `timestamp_end` *(no arquivo, o que o jogo grava; o que se deriva — `trial_type`, `rotation_angle` de 0° a 180°, `rotation_direction`, a primeira resposta válida, `attempt_count` e a validade do RT — sai da visão derivada da T-09.2.3, sem campo novo; decisão do orientador de 26/11/2026: vale o que o Gabriel decidiu no código e no PR #432)*
- `§11.4` — Configurações relevantes de acessibilidade, plataforma, dispositivo e versão do jogo deverão estar associadas à sessão quando puderem influenciar a interpretação dos dados *(a versão já está no arquivo, `game_version`; plataforma, dispositivo e acessibilidade o Gabriel adiou ao plano de dados aprovado, PR #432, e saem do Core)*
- `LG-Gab-18` — Arquivo local por sessão, exportação pelo painel do pesquisador, criança identificada pelo código *(a exportação saiu na v1.5 do SGDD: o arquivo fica local, pronto para o backend; a criança não é identificada no arquivo (`TrialTelemetry.md:3`): o código dela fica só no save e no menu (LG-Gab-04), decisão do orientador de 26/11/2026: vale o que o Gabriel decidiu no código e no PR #432)*

**Cenários de aceite:**

- Dado uma obra jogada inteira, quando o pesquisador abre `Telemetry/session_<session_id>.json` no `persistentDataPath`, então há o cabeçalho da sessão e um item em `trials` por peça apresentada, com os campos do §11.4 que o Core tem, nos nomes do `TrialTelemetry.md`.
- Dado a mesma peça respondida duas vezes, quando ele abre o arquivo, então a peça tem um item só em `trials`, com as duas respostas em `attempts`.
- Dado o jogo fechado no meio de uma tentativa, quando ele abre o arquivo, então as tentativas concluídas estão lá e o arquivo é um JSON válido.
- Dado um sistema ainda não ligado (o bônus, por exemplo), quando ele lê o arquivo, então o campo desse sistema ainda não está lá; ele entra com o sistema, e nenhum valor é inventado.

| Task | O que fazer | Horas e camada |
| --- | --- | --- |
| T-02.3.1 | Acrescentar ao `TelemetryTrialRecord` (`TrialTelemetryRecords.cs:17-30`) o que o formato do Gabriel ainda não tem, não se deriva e ele não adiou: a posição da peça na sequência fixa da obra (`GameSession.CandidateIndex`; a obra já está em `building_id`) e os instantes de início e de fim da tentativa, no estilo do `started_at_utc` dele; documentar no `TrialTelemetry.md` e estender o `TrialTelemetryChecks` | 1 h · telemetria |
| T-02.3.2 | **Já existe** (PR #431): o arquivo da sessão, um JSON por sessão em `persistentDataPath/Telemetry/session_<session_id>.json`, com `schema_version`, `session_id`, `game_version` e `started_at_utc` e a lista `trials` (`TrialTelemetryRecords.cs:7-39`), gravado de forma atômica ao abrir a sessão, a cada tentativa revelada, a cada resposta, na volta da pausa e no fim (`TrialTelemetryJsonStore.cs:21-47`, `TrialTelemetrySession.cs:16-93`), com a tentativa ativa marcada `interrupted` ao fechar ou reiniciar (`TrialTelemetrySession.cs:95-102`, `GameApplication.cs:174-200`); conferido pelo `TrialTelemetryChecks.RunStorage` (`TrialTelemetryChecks.cs:28-111`) | já feita · telemetria |
| T-02.3.3 | **Já existe** (PR #431): o registro é a fonte do dado, e o `Debug.Log` do `TrialController` (`TrialController.cs:97`) fica só como diagnóstico; cada tentativa grava a obra (`building_id`) e sai uma vez só, com o mesmo `trial_id`, mesmo com o erro e a pausa, e a próxima peça recebe outro (`TrialTelemetryChecks.cs:217-243`) | já feita · telemetria |
| T-02.3.4 | **Já existe** (PR #431): o formato do arquivo documentado campo a campo, com o local, a gravação, o tempo e os limites, em `Documentation/TrialTelemetry.md` (`TrialTelemetry.md:11-50`); cada task que acrescenta campo o documenta ali | já feita · telemetria |

## 4.2.4 US-02.4 — O retorno da resposta: o ícone, a chama e os pontos

**Perfil:** Criança · **Horas:** 7 · **Prioridade:** P1 · **Milestone:** Core · **Depende de:** US-02.3

*Como criança, quero ver a peça encaixar ou voltar, sem bronca, e ver a chama da sequência e os pontos quando acerto, para que errar não me faça parar e acertar seguido valha a pena.*

**Requisitos do SGDD que ela realiza:**

- `§4.2` — O feedback deverá enfatizar a consequência dentro do mundo, evitando mensagens punitivas ou avaliações pessoais como “errado”, repreensão dos personagens ou perda de progresso
- `§12-13` — Implementar streak baseada somente em acertos consecutivos
- `§12-14` — Erro reinicia streak sem retirar progresso acumulado
- `§12-15` — Implementar multiplicadores de streak configuráveis
- `§4.2.2` — Cada resposta correta concederá inicialmente 100 pontos-base, aos quais será aplicado um multiplicador provisório de streak
- `LG-Gab-09` — Placa só no acerto (streak, pontos, bônus); nada no erro; moedas na Vila *(o bônus é da US-06.1 e as moedas na Vila também)*
- `LG-Gab-08` — Os quatro resultados com consequência própria; quatro explicações em ordem fixa; falas de até 8 palavras *(no Core vale o texto de retorno do código; as quatro explicações são da US-05.1)*

**Cenários de aceite:**

- Dado streak 2, quando a criança acerta, então a streak vai a 3 e a placa mostra a chama e +125 pontos.
- Dado streak 5, quando ela erra, então a streak vai a 0, nenhuma placa aparece e as peças da obra seguem iguais.
- Dado streak 4, quando ela pausa, retoma e erra, então a pausa fica no `paused_duration_ms` e a streak vai a 0; dado streak 4 e a tentativa fechada `interrupted` sem resposta, então a streak continua 4.
- Dado qualquer retorno, quando o time confere, então o texto tem até 8 palavras, nenhuma da lista proibida, e há um ícone de encaixe ou de volta.
- Dado uma resposta, quando o arquivo é lido, então `streak_before`, `streak_after`, `base_points`, `streak_multiplier`, `points_earned` e `feedback_shown` vêm preenchidos no item dela em `attempts`.

| Task | O que fazer | Horas e camada |
| --- | --- | --- |
| T-02.4.1 | **Já existe:** o texto do retorno, que o código mostra no acerto e no erro (`TrialController.cs:106,116-117`) | já feita · presentation |
| T-02.4.2 | `FeedbackIconView`: o ícone de encaixe ou de volta em cada retorno, com os ícones importados e ligados na tela do retorno, e o `feedback_shown` gravado em cada resposta (`TelemetryAttemptRecord`, `TrialTelemetryRecords.cs:33-39`) e documentado no `TrialTelemetry.md`; teste EditMode de que os textos do código não usam palavra da lista proibida e têm até 8 palavras | 2 h · presentation |
| T-02.4.3 | Streak em C# puro: o acerto soma, o erro zera, o RT não conta; a tentativa com pausa (`paused_duration_ms` maior que 0) que recebe resposta conta como as outras, e a fechada `interrupted` sem resposta não mexe (GP-06); gravar `streak_before` e `streak_after` em cada resposta (`TelemetryAttemptRecord`) e documentá-los no `TrialTelemetry.md`; teste EditMode | 2 h · gameplay |
| T-02.4.4 | `ScoringConfig` (base 100 e as faixas do §4.2.2, marcadas provisórias) como asset ligado ao `GameApplication`, e o cálculo em C# puro com a streak já contando o acerto; gravar `base_points`, `streak_multiplier` e `points_earned` em cada resposta (`TelemetryAttemptRecord`) e documentá-los no `TrialTelemetry.md`; teste EditMode das quatro faixas e da troca da base | 2 h · gameplay |
| T-02.4.5 | `RewardPlaqueView`: uma placa só no acerto, fora do TRIAL_ACTIVE, com a chama da streak e os +pontos, montada na cena `Kingdom` | 1 h · presentation |

## 4.3 EP-03 — O reino

O habitante que traz a peça, o código da criança com o reino salvo, a obra que cresce e a Vila onde ela escolhe a próxima obra.

**Por que importa:** É o que faz a tarefa ser cuidar do reino, e não um teste (§6); sem o save, a criança perde o que construiu.

**Pronto quando:**

- O reino de um código fecha e reabre igual;
- A obra fecha com 5 peças, começando pelo Observatório, e a Vila mostra o estado do save.

## 4.3.1 US-03.1 — O habitante chega com o pedido

**Perfil:** Criança · **Horas:** 4 · **Prioridade:** P1 · **Milestone:** Core · **Depende de:** US-02.1

*Como criança, quero ver o habitante chegar e mostrar para qual construção é a peça, para que eu entenda por que estou olhando a peça, mesmo sem ler.*

**Requisitos do SGDD que ela realiza:**

- `§12-32` — Implementar interação principal de NPC em composição 2D frontal
- `LG-Gab-07` — Cada tela segue sozinha (4 / 2,5 / 2,2 / 1,4 s); um clique adianta *(aqui, a chegada: 4 s)*
- `LG-Gab-22` — Habitantes de aparência neutra e constante; um novo na Vila por construção concluída, até 5; o habitante de cada peça sai do modelo dela (v1.4, conforme a implementação do aluno)
- `LG-Gab-20` — Duas músicas, sete efeitos, silêncio na comparação, narração por voz sintética provisória *(aqui, a narração das falas)*

**Cenários de aceite:**

- Dado a Ponte escolhida, quando o habitante chega, então ele mostra o ícone da Ponte e diz uma fala curta, narrada.
- Dado uma criança que não lê, quando o habitante chega, então o ícone, a imagem e a narração bastam para seguir.
- Dado a chegada, quando passam 4 s ou a criança clica, então o jogo segue para a peça.
- Dado a mesma peça, quando ela volta depois do erro, então o habitante é o mesmo (sai do `modelId` dela).

| Task | O que fazer | Horas e camada |
| --- | --- | --- |
| T-03.1.1 | **Já existe:** o retrato do visitante, sorteado pelo `modelId` da peça (`TrialController.cs:55`), com a animação de chegada (`CharacterPortrait.cs:15-46`) | já feita · presentation |
| T-03.1.2 | `NpcDialogView` num painel sobre a tela do desafio (as 7 telas ficam), com o retrato que já existe, o ícone da construção, a fala e a voz, montado em 2D frontal na cena `Kingdom`; a chegada dura 4 s e um clique adianta; teste PlayMode de que mostra o ícone da construção escolhida | 2 h · presentation |
| T-03.1.3 | `InhabitantLines` (falas de até 8 palavras e o áudio de cada uma) como asset preenchido, com a narração gerada por voz sintética provisória, importada e ligada, e o texto na tela | 2 h · conteúdo |

## 4.3.2 US-03.2 — Entrar com meu código e achar meu reino

**Perfil:** Criança · **Horas:** 10 · **Prioridade:** P1 · **Milestone:** Core · **Depende de:** US-01.1

*Como criança, quero escolher o meu código, continuar o meu reino ou começar um, e encontrá-lo igual quando volto, para que o que construí continue sendo meu.*

**Requisitos do SGDD que ela realiza:**

- `§12-10` — Permitir construção e persistência do reino
- `LG-Gab-04` — Um reino por código de criança, escolhido no menu; o mesmo código serve à escola e à casa *(o save fica no aparelho: escola e casa são reinos separados até a API)*
- `§5.2` — O reino deverá persistir entre sessões, permitindo que a criança reconheça construções desenvolvidas anteriormente e observe o impacto acumulado de sua participação

**Cenários de aceite:**

- Dado um código novo, quando a criança o escolhe e clica JOGAR, então vê o carregamento e o desafio da primeira obra, sem tutorial.
- Dado dois códigos com reinos diferentes, quando ela escolhe o segundo e clica CONTINUAR, então carrega o reino do segundo.
- Dado um código com reino, quando ela clica NOVO REINO, então o jogo pede confirmação, e só depois do sim o reino é apagado.
- Dado o jogo fechado com duas obras prontas, quando ela reabre, escolhe o mesmo código e clica CONTINUAR, então as duas estão prontas.
- Dado um save ilegível, quando o jogo abre, então avisa e oferece começar de novo, sem travar; dado SAIR no menu, então o jogo fecha.

| Task | O que fazer | Horas e camada |
| --- | --- | --- |
| T-03.2.1 | `SaveData` versionado (obras, progresso, moedas, decoração, tutoriais vistos) em JSON no `persistentDataPath`, um save por código, com as configurações num arquivo do aparelho à parte; teste EditMode de ida e volta e de dois códigos com reinos separados | 2 h · dados |
| T-03.2.2 | Gravar o save de forma atômica ao fim de cada tentativa e ao sair, e fazer o REINICIAR da Vila (`GameApplication.cs:174-187`) gravar um reino novo do código; teste de fechar no meio da gravação | 2 h · dados |
| T-03.2.3 | Ler o save com migração de versão, e recusar o ilegível com aviso e a opção de começar de novo; teste de versão antiga e de arquivo corrompido | 2 h · dados |
| T-03.2.4 | Menu pictográfico na cena `Kingdom`: a escolha do código no topo (`ChildCodeSelector`, com os códigos deste aparelho e o “+ código”, normalizado em `[A-Z0-9-]` até 32; o código fica no save e no menu, fora do arquivo de telemetria) e, no `MenuView`, CONTINUAR (só com reino), JOGAR ou NOVO REINO (com confirmação), CONFIGURAÇÕES e SAIR | 2 h · presentation |
| T-03.2.5 | LOAD_GAME com o indicador de carregamento: lê o save do código e segue direto ao desafio da obra atual, sem tutorial; teste PlayMode montado por código: menu → código → JOGAR → desafio, CONTINUAR, e NOVO REINO recusado e aceito | 2 h · presentation |

## 4.3.3 US-03.3 — A obra cresce peça por peça

**Perfil:** Criança · **Horas:** 2 · **Prioridade:** P1 · **Milestone:** Core · **Depende de:** US-02.2, US-03.2

*Como criança, quero ver a construção avançar conforme as peças entram, começando pelo Observatório, para que eu saiba quanto falta e ganhe a Vila ao terminar a primeira.*

**Requisitos do SGDD que ela realiza:**

- `§12-02` — Balancear condições `MATCH` e `NON_MATCH` no conjunto de tentativas
- `LG-Gab-02` — 5 peças por construção; só o `MATCH` aceito conta, e a obra fecha no 10º estímulo; o erro não soma nem tira (v1.4, conforme a implementação do aluno)
- `LG-Gab-12` — A primeira construção é o Observatório; antes dela nenhuma outra (v1.4, conforme a implementação do aluno)
- `§4.1` — Cada obra tem uma sequência fixa de 10 peças, com 5 `MATCH` e 5 `NON_MATCH` e cada ângulo uma vez em cada condição, apresentadas sempre na mesma ordem (conforme a implementação do aluno)

**Cenários de aceite:**

- Dado uma peça incorporada, quando a criança olha a obra, então o progresso aumentou; dado a quinta, então a obra está pronta.
- Dado um jogador que acerta metade, quando joga a obra inteira, então a conclui, porque a mesma peça volta depois do erro.
- Dado uma obra, quando o time conta as condições das 10 peças, então são 5 `MATCH` e 5 `NON_MATCH`, sempre na mesma ordem.
- Dado um reino novo, quando a criança clica JOGAR, então a primeira obra é o Observatório; concluído, a Vila abre.

| Task | O que fazer | Horas e camada |
| --- | --- | --- |
| T-03.3.1 | **Já existe:** as 5 obras do Core (Ponte, Moinho, Museu, Observatório e Ferraria) e o castelo, cada obra com um pré-requisito (`BuildingDefinition.cs:19`) e a sequência fixa de 10 peças (`BuildingDefinition.cs:21`, avanço em `GameSession.cs:101`), e o Observatório como primeira obra (`KingdomDefinition.cs:7`, `GameApplication.cs:31`, `Assets/Data/Kingdom.asset:21`) | já feita · dados |
| T-03.3.2 | **Já existe:** a obra pede 5 peças, só o `MATCH` aceito conta e ela fecha no 10º estímulo (`GameSession.cs:84-104`); o progresso aparece na lista `progress` do `TrialView` (`TrialView.cs:17-22`), montada no prefab (`Assets/Prefabs/UI/Trial.prefab:3387-3392`) | já feita · gameplay |
| T-03.3.3 | Concluir a primeira obra libera o acesso regular à Vila (regra em `Game.Core`, lida do save; a conclusão já leva à Vila, `GameApplication.cs:66-79`), com teste EditMode: o reino novo começa pelo Observatório e concluí-lo abre a Vila; cada obra tem 5 `MATCH` e 5 `NON_MATCH` na mesma ordem; um jogador a 50% de acerto conclui a obra | 2 h · gameplay |

## 4.3.4 US-03.4 — Passear pela Vila e escolher a próxima obra

**Perfil:** Criança · **Horas:** 2 · **Prioridade:** P2 · **Milestone:** Core · **Depende de:** US-03.2, US-03.1

*Como criança, quero ver na Vila as obras que terminei, andar por ela e escolher ali o próximo projeto, para que eu veja o resultado do que fiz.*

**Requisitos do SGDD que ela realiza:**

- `§12-31` — Implementar Vila pseudo-isométrica com elementos 2D
- `LG-Gab-16` — Vila com deslocamento, nos limites do mapa da cena (v1.4); Observatório, Museu e Parque com conteúdo provisório; o conteúdo é do pesquisador *(aqui, o deslocamento; os interiores são da US-07.3)*
- `§5.2` — Quando mais de um projeto estiver disponível, o jogador poderá escolher qual deseja desenvolver

**Cenários de aceite:**

- Dado um save com obras prontas, disponíveis e bloqueadas, quando a Vila abre, então cada uma aparece no seu estado; as que ainda não têm cadastro ficam ocultas.
- Dado uma obra com pré-requisito não concluído, quando a criança clica nela, então a Vila mostra que ainda não dá.
- Dado duas obras disponíveis, quando ela clica numa e em INICIAR, então o projeto dela começa.
- Dado a Vila aberta, quando ela arrasta ou usa as setas, então a vista anda, sem passar dos limites do mapa.

| Task | O que fazer | Horas e camada |
| --- | --- | --- |
| T-03.4.1 | **Já existe:** a Vila (`Village.unity`, com Grid isométrico e 6 Tilemaps, o arraste preso aos limites do mapa, `VillageCameraDrag.cs:9,76-82`, e as placas com o estado da obra, `VillageBuilding.cs:34-44`), com as 5 obras do Core e o castelo na lista `buildings` do `VillageWorld`; a criança escolhe a obra pela placa e INICIAR (`GameApplication.cs:91-113,120-139`) | já feita · presentation |
| T-03.4.2 | Ajustar o `VillageBuilding.Refresh` (`VillageBuilding.cs:34-44`) para ler o estado do save e acrescentar ao `VillageCameraDrag` as quatro setas, presas aos mesmos limites e postas na cena `Village`; teste PlayMode com uma Vila mínima: cada obra concluída, disponível ou bloqueada conforme o save | 2 h · presentation |

## 4.4 EP-04 — A sessão no PC e o marco do Core

A pausa e o som, o layout do PC, o fim da sessão com o painel do mediador, e o marco: a build de PC jogada do menu ao fim.

**Por que importa:** O mediador precisa de uma sessão de tamanho certo, que ele pode encerrar, e a criança precisa poder parar sem perder nada.

**Pronto quando:**

- A pausa, o som, o fim de sessão e o painel do mediador funcionam na build de PC;
- A história de marco do Core foi jogada na build, do menu ao fim, e o arquivo de telemetria bateu com a jogada.

## 4.4.1 US-04.1 — Pausar, ajustar o som e sair

**Perfil:** Criança · **Horas:** 6 · **Prioridade:** P1 · **Milestone:** Core · **Depende de:** US-03.2

*Como criança, quero pausar quando quiser, baixar o som e sair sem perder nada, para que eu não seja obrigada a continuar nem a aguentar um som que me incomoda.*

**Requisitos do SGDD que ela realiza:**

- `§12-30` — Jogador deve poder pausar ou interromper sem perda de progresso
- `§12-22` — Permitir controle de áudio
- `LG-Gab-05` — Pausa pelo mesmo botão e ícone em todas as telas, menos no menu e nas configurações; RETOMAR, CONFIGURAÇÕES, SALVAR E SAIR; no desafio, a pausa é o botão VILA, que leva à Vila (v1.4, conforme a implementação do aluno)
- `LG-Gab-17` — Configurações do aparelho, com os padrões da §10.3

**Cenários de aceite:**

- Dado o diálogo do habitante, quando a criança pausa e escolhe SALVAR E SAIR, então ao voltar o reino está igual.
- Dado a Vila, quando ela pausa e escolhe RETOMAR, então o jogo segue do mesmo ponto.
- Dado os efeitos ligados, quando ela liga o mudo dos efeitos, então não ouve efeitos; dado a música baixada, quando reabre o jogo, então a música continua baixa.
- Dado a comparação (TRIAL_ACTIVE), quando ela escuta, então nenhum som toca.

| Task | O que fazer | Horas e camada |
| --- | --- | --- |
| T-04.1.1 | **Já existe:** no desafio, a pausa é o botão VILA, que leva à Vila com RETOMAR e acumula só o tempo ativo (`TrialController.cs:133-154`, `GameSession.cs:65-81`); desde o PR #431, a pausa ainda soma o tempo parado no `paused_duration_ms` da tentativa (`TrialTelemetrySession.cs:70-82,104-111`) | já feita · presentation |
| T-04.1.2 | `PauseMenu` com RETOMAR, CONFIGURAÇÕES e SALVAR E SAIR, aberto pelo mesmo botão e ícone, no mesmo canto, em todas as telas menos o MENU, as configurações e o desafio, montado na cena; teste PlayMode: sair no diálogo, na Vila e na tentativa e reabrir igual | 2 h · presentation |
| T-04.1.3 | `AudioManager` com os canais de música, efeitos e voz, sem nenhum som durante a comparação (TRIAL_ACTIVE) | 2 h · gameplay |
| T-04.1.4 | `SettingsView` com volume e mudo de música, efeitos e voz (padrões 0,6, 0,8 e 1), montada na tela de configurações e aberta pelo menu e pela pausa; as escolhas ficam no aparelho (no arquivo da sessão só quando o plano de dados que o Gabriel adiou pedir); teste EditMode de mudo, persistência e padrões, e de que nada disso muda a demanda | 2 h · presentation |

## 4.4.2 US-04.2 — Jogar no PC, em qualquer tamanho de janela

**Perfil:** Criança · **Horas:** 4 · **Prioridade:** P2 · **Milestone:** Core · **Depende de:** —

*Como criança, quero botões grandes, que acerto sem precisão, e a tela inteira em qualquer janela do PC, para que eu responda sem esforço, com o mouse ou com o teclado.*

**Requisitos do SGDD que ela realiza:**

- `§12-25` — Resposta deve exigir interação simples e de baixa precisão
- `LG-Gab-14` — SERVE e NÃO SERVE com ícone e rótulo, ≥ 2,3 cm; peças com ~37% da altura
- `§13` — Plataforma final — Decidida: Unity, com build para PC (Windows, mouse e teclado)

**Cenários de aceite:**

- Dado o PC, quando a criança clica perto da borda de SERVE, então a resposta é aceita.
- Dado uma janela de 1366×768, quando a tentativa aparece, então o texto e as peças estão legíveis e inteiros; em 1920×1080, nada sai da tela.
- Dado o menu e a tentativa, quando ela usa só o teclado, então escolhe as opções e responde.

| Task | O que fazer | Horas e camada |
| --- | --- | --- |
| T-04.2.1 | Tornar desligável a escala fixa de 1600×900 do `GameUI` (`GameUI.cs:32-37`) e refazer o layout das telas com âncoras e `CanvasScaler`, de 1366×768 a 1920×1080 | 2 h · presentation |
| T-04.2.2 | SERVE e NÃO SERVE com ícone e rótulo, de pelo menos 380×150 px (até 40% da largura e 16% da altura), as peças com 37% da altura da tela, e a navegação por teclado no menu e nos botões de decisão | 2 h · presentation |

## 4.4.3 US-04.3 — A sessão acaba sozinha ou pelo mediador

**Perfil:** Mediador · **Horas:** 4 · **Prioridade:** P1 · **Milestone:** Core · **Depende de:** US-03.2, US-02.3

*Como mediador, quero que a sessão termine sozinha no limite e que eu possa encerrá-la a qualquer momento, com o motivo gravado, para que a sessão tenha o tamanho certo e o desconforto da criança seja respeitado.*

**Requisitos do SGDD que ela realiza:**

- `LG-Gab-15` — Painel do mediador (Ctrl+Shift+M ou botão “adulto” do menu; os três toques no canto saíram na v1.5), encerramento com motivo gravado
- `§11.2` — Interrupção por desconforto: em uso supervisionado, sinais relevantes de desconforto ou desengajamento justificam encerramento da atividade
- `§13` — Quantidade de tentativas por sessão — provisório: 10, ajustável no painel do mediador (§5.1)

**Cenários de aceite:**

- Dado o limite em 10, quando a criança termina a 10ª tentativa `VALID` `completed`, então a sessão fecha sem mensagem de falha, o reino é salvo e a Vila aparece.
- Dado o mediador trocou o limite para 20 no painel, quando a 10ª termina, então a sessão continua até a 20ª.
- Dado a criança com desconforto, quando o mediador aperta Ctrl+Shift+M e escolhe “desconforto”, então a sessão fecha, a tentativa em curso fica `interrupted` e o motivo está no arquivo da sessão.

| Task | O que fazer | Horas e camada |
| --- | --- | --- |
| T-04.3.1 | `SessionConfig` (15 min e 10 tentativas `VALID` `completed`, provisórios) como asset ligado ao `GameApplication`, e o fim de sessão neutro no limite: o reino é salvo, a telemetria fecha pelo `End` que já existe (`TrialTelemetrySession.cs:84-93`) e a criança volta à Vila; teste EditMode dos limites de tempo e de tentativas | 2 h · gameplay |
| T-04.3.2 | `MediatorPanel`, aberto por Ctrl+Shift+M ou pelo botão “adulto” do menu, sem senha: mostra a sessão, encerra por desconforto, protocolo ou outro (a tentativa em curso fica `interrupted`, como o `End` já faz, e o motivo vai para o `TelemetrySessionRecord`) e troca o limite (5, 10, 20, 30 ou sem limite); montado na cena `Kingdom`, com teste do motivo gravado e do limite trocado | 2 h · presentation |

## 4.4.4 US-04.4 — Marco do Core: a build de PC jogada do menu ao fim

**Perfil:** Pesquisador · **Horas:** 5 · **Prioridade:** P1 · **Milestone:** Core · **Depende de:** US-01.1, US-02.1, US-02.2, US-02.3, US-02.4, US-03.1, US-03.2, US-03.3, US-03.4, US-04.1, US-04.2, US-04.3 · **história de marco do Core**

*Como pesquisador, quero jogar a build de PC do Core do menu às obras do Core, voltar outro dia e conferir o arquivo de telemetria, para que eu veja o jogo do Core montado, e não só peças.*

**Cenários de aceite:**

- Dado a build do Core e um código novo, quando a pessoa joga do menu às obras do Core, com um erro, uma pausa e uma perda de foco, então o Observatório vem primeiro, cada obra libera na hora certa, e não há exceção.
- Dado o jogo fechado com uma obra pela metade, quando ela reabre e escolhe o mesmo código, então a obra está no mesmo ponto.
- Dado o arquivo da sessão, quando ela conta os itens de `trials`, os erros (o item com mais de uma resposta em `attempts`, com a primeira resposta e o RT dela no `attempts[0]`) e os `status`, então os números batem com o que fez; streak, pontos e `feedback_shown` vêm preenchidos, e os campos da Expansão (`complexity_score`, `distractor_similarity`, `difficulty_state`, `agility_bonus`) ainda não estão no arquivo.

| Task | O que fazer | Horas e camada |
| --- | --- | --- |
| T-04.4.1 | Ajustar o `PlayModeFlowChecks` e o `VillageInteractionChecks` do aluno só onde o fluxo novo os faz falhar (o menu pelo código e a pausa; hoje clicam o `startButton`, `PlayModeFlowChecks.cs:102,202`), e o `TrialTelemetryChecks` do PR #431, que também entra pelo `startButton` (`TrialTelemetryChecks.cs:177,258`), e gerar a build limpa do Core para o PC (Windows) | 2 h · build |
| T-04.4.2 | Jogar a build do menu às obras do Core, com um erro, uma pausa e uma perda de foco; fechar, reabrir e continuar; corrigir o que aparecer | 2 h · build |
| T-04.4.3 | Conferir o arquivo de telemetria da sessão (`Telemetry/session_<session_id>.json`) contra a jogada: um item por `trial_id`, as respostas em `attempts`, os `status`, a ordem de grandeza do RT e do `paused_duration_ms`, a streak, os pontos e o `feedback_shown` | 1 h · telemetria |

## 4.5 EP-05 — A história da peça e a comparação calma

O Grande Livro que destaca a peça, a obra que mostra o encaixe ou a recusa com as quatro explicações, o retrato que chega sem deslizar, e a comparação sem distração, sem pista e com a pergunta neutra quando a criança para.

**Por que importa:** A narrativa dá sentido à tarefa sem tocar nos parâmetros científicos, e a comparação calma protege a medida.

**Pronto quando:**

- Várias tentativas seguidas acontecem pelo Livro, com uma peça destacada só;
- Em TRIAL_ACTIVE o habitante sai do foco, o ambiente se acalma e nada dá pista do gabarito.

## 4.5.1 US-05.1 — O Livro mostra a peça e a obra mostra o resultado

**Perfil:** Criança · **Horas:** 12 · **Prioridade:** P2 · **Milestone:** Expansão · **Depende de:** US-03.1, US-02.4, US-03.3, US-04.2

*Como criança, quero abrir o Grande Livro e ver a peça que a obra precisa, e depois ver a peça encaixar ou ser recusada com uma explicação, para que a minha decisão tenha consequência no reino.*

**Requisitos do SGDD que ela realiza:**

- `§12-09` — Implementar Grande Livro de Projetos como referência narrativa e funcional
- `§4.1` — O jogador consulta o Grande Livro de Projetos, no qual uma única peça necessária é destacada
- `§4.2` — Quando a criança classifica corretamente uma peça, ela funciona de acordo com o projeto e pode ser incorporada ao processo de construção
- `LG-Gab-08` — Os quatro resultados com consequência própria; quatro explicações em ordem fixa; falas de até 8 palavras
- `LG-Gab-07` — Cada tela segue sozinha (4 / 2,5 / 2,2 / 1,4 s); um clique adianta *(aqui, o Livro 2,5 s, o retorno 2,2 s e o progresso 1,4 s)*

**Cenários de aceite:**

- Dado o habitante trouxe uma peça, quando o Livro abre, então a silhueta da obra mostra as peças obtidas cheias e as que faltam em contorno, e uma peça só está destacada.
- Dado a peça destacada, quando a tentativa começa, então ela é a referência, e nenhuma animação roda depois do início do RT.
- Dado um acerto em `MATCH`, quando o retorno aparece, então a peça entra na obra; dado a recusa correta de um `NON_MATCH`, então a peça não entra e a obra segue para a seguinte.
- Dado um erro, quando o retorno aparece, então a fala dá uma das quatro explicações, na ordem, sem “errado”, e a mesma peça volta.
- Dado três habitantes diferentes, numa janela de 1366×768 a 1920×1080, quando chegam, então nenhum desliza, pula ou fica cortado.

| Task | O que fazer | Horas e camada |
| --- | --- | --- |
| T-05.1.1 | Arte pixel do Grande Livro e das molduras | 2 h · arte |
| T-05.1.2 | `ProjectBookView`: a silhueta da obra com um slot por peça (cheio ou em contorno, pela regra do `TrialView.ShowProgress`) e a peça necessária destacada, montado como Livro aberto em tela cheia (PROJECT_VIEW) na cena `Kingdom` (L-ABD-11) | 2 h · presentation |
| T-05.1.3 | Transição do Livro para a tentativa: a peça destacada vira a referência, o Livro fica 2,5 s e um clique adianta, sem animação depois do início do RT; teste EditMode de que só uma peça é destacada e ela é a referência seguinte | 2 h · gameplay |
| T-05.1.4 | `ConstructionFeedbackView` com os triggers `PieceEnters`, `PieceRejected` e `SamePiece` e as três animações no `Animator` da obra, e a tela CONSTRUCTION_PROGRESS montada e ligada ao `TrialView` | 2 h · presentation |
| T-05.1.5 | `FeedbackLineSet` com as falas das quatro explicações (confusão, fabricação, transporte, semelhança), servidas em rodízio, escritas no asset; teste EditMode de que nenhuma fala usa palavra da lista proibida e todas têm até 8 palavras | 2 h · conteúdo |
| T-05.1.6 | Tirar do código a escala e o deslocamento da chegada do retrato (`CharacterPortrait.cs:37-39`) para campos do Inspector, e acertar o pivô, a escala e a âncora do retrato nas resoluções do PC | 2 h · presentation |

## 4.5.2 US-05.2 — Comparar com calma, sem distração nem pista

**Perfil:** Criança · **Horas:** 8 · **Prioridade:** P2 · **Milestone:** Expansão · **Depende de:** US-02.1, US-02.2, US-03.1

*Como criança, quero comparar peças nítidas, iguais na cor, com nada se mexendo em volta, e ser perguntada sem pressa se quero continuar quando paro, para que eu compare a forma e possa parar sem me sentir cobrada.*

**Requisitos do SGDD que ela realiza:**

- `§12-21` — Cor não pode determinar a resposta correta
- `§12-23` — Reduzir estímulos visuais simultâneos durante a tarefa
- `§12-26` — NPC não pode fornecer pistas sobre a resposta pela aparência ou comportamento
- `§11.2` — Ausência prolongada de resposta: após limite ainda a ser calibrado, o jogo poderá perguntar de forma neutra se a criança deseja continuar
- `LG-Gab-21` — Pergunta neutra aos 45 s, abandono aos 75 s; “parar” fecha a sessão e mostra a Vila
- `LG-Gab-22` — Habitantes de aparência neutra e constante; um novo na Vila por construção concluída, até 5; o habitante de cada peça sai do modelo dela (v1.4, conforme a implementação do aluno) *(aqui, a aparência neutra e constante)*

**Cenários de aceite:**

- Dado um par referência e candidato, quando a tentativa aparece, então os dois têm o mesmo material e a mesma cor, com as faces distinguidas pela luz e pelo contorno.
- Dado a tentativa começou, quando a criança olha a tela, então o habitante está fora do foco e a decoração animada se acalmou.
- Dado duas tentativas com o mesmo `modelId` e gabaritos diferentes, quando o habitante aparece, então a aparência e a animação são as mesmas.
- Dado 45 s sem resposta, quando a pergunta aparece, então ela é pictográfica e sem cronômetro; continuar fecha a tentativa `interrupted` e abre uma nova, e parar fecha a sessão e mostra a Vila.

| Task | O que fazer | Horas e camada |
| --- | --- | --- |
| T-05.2.1 | Material único dos estímulos (URP), com contorno de arestas e luz que distingue as faces | 2 h · arte |
| T-05.2.2 | `ModelPresentation` com o mesmo `stimulusMaterial` na referência e no candidato, e a moldura do Livro e a mão do habitante como âncoras, sem texto, na cena `Kingdom`; teste de mesmo material e mesma cor nas duas | 2 h · presentation |
| T-05.2.3 | `TrialFocusController`: em TRIAL_ACTIVE o habitante sai do foco e os animadores do ambiente se acalmam, ligados na cena; teste de que a aparência e a animação do habitante só dependem do `modelId` (o risco de pista fica na LA-13) | 2 h · presentation |
| T-05.2.4 | `AbsencePrompt`: aos 45 s a pergunta pictográfica (continuar ou parar), montada na cena; responder ou chegar aos 75 s fecha a tentativa `interrupted` (o abandono no modelo do Gabriel), continuar abre tentativa nova e parar fecha a sessão; teste EditMode de continuar, parar e silêncio | 2 h · gameplay |

## 4.6 EP-06 — Moedas e decoração

O Bônus de Agilidade (desligado até o piloto), o dia que fecha e converte pontos em moedas, o saldo, e a decoração da Vila comprada com as moedas.

**Por que importa:** As moedas dão à criança um uso para o que ganhou, sem mexer na demanda nem no progresso das obras.

**Pronto quando:**

- O dia fecha e os pontos viram moedas, que ficam salvas;
- A criança compra, põe e tira enfeites, e as moedas não compram obra.

## 4.6.1 US-06.1 — O bônus e as moedas do fim do dia

**Perfil:** Criança · **Horas:** 9 · **Prioridade:** P2 · **Milestone:** Expansão · **Depende de:** US-02.4, US-03.2, US-03.4, US-04.3

*Como criança, quero ver os meus pontos virarem moedas ao anoitecer e encontrá-las quando volto, e às vezes ganhar um extra por responder logo, para que eu tenha com que enfeitar a Vila.*

**Requisitos do SGDD que ela realiza:**

- `§12-16` — Implementar Bônus de Agilidade independente da streak
- `§12-17` — Converter pontos do dia em moedas com taxa configurável
- `LG-Gab-10` — Bônus de Agilidade desligado até o piloto, com chave no painel do mediador
- `LG-Gab-11` — O dia fecha em 10 tentativas válidas ou no fim da sessão; o número é pendência do piloto
- `§4.2` — A criança não perde construções, moedas já obtidas ou pontos conquistados anteriormente

**Cenários de aceite:**

- Dado 450 pontos no dia, quando o dia termina, então a tela de anoitecer mostra 4 moedas, a sobra não vira moeda, e os pontos do dia zeram.
- Dado o jogo fechado com 7 moedas, quando a criança reabre, então tem 7 moedas, que aparecem na Vila; dado um erro, as moedas não mudam.
- Dado um save novo, sem mexer no painel do mediador, quando ela acerta rápido, então o bônus é 0.
- Dado o mediador ligou o bônus, quando ela acerta rápido num ângulo com 5 acertos ou mais, então a placa mostra o +extra; quando erra rápido, não há bônus.

| Task | O que fazer | Horas e camada |
| --- | --- | --- |
| T-06.1.1 | Bônus de Agilidade em C# puro: só no acerto, sem mexer na streak, com limiares pela mediana do RT do jogador no mesmo ângulo, chave desligada por padrão, e `agility_bonus` gravado em cada resposta (`TelemetryAttemptRecord`) e documentado no `TrialTelemetry.md`; teste EditMode | 2 h · gameplay |
| T-06.1.2 | O +extra na placa do acerto, sem cronômetro nem urgência, e a chave do bônus no painel do mediador, montados na cena e gravando a troca no registro da sessão | 1 h · presentation |
| T-06.1.3 | Ciclo do dia em C# puro: fecha em 10 tentativas `VALID` `completed` ou no fim da sessão; `day_points` vira moedas a 100:1 configurável, a sobra não vira moeda, e zera; `day_points` e `coins` no `TelemetrySessionRecord`; teste EditMode | 2 h · gameplay |
| T-06.1.4 | `NightfallView`: a tela de anoitecer com os pontos do dia virando moedas, montada na cena `Kingdom` | 2 h · presentation |
| T-06.1.5 | O saldo de moedas no save e no `VillageHUD` da cena `Village`; teste de fechar e reabrir, e de que o erro não tira moeda | 2 h · dados |

## 4.6.2 US-06.2 — Decorar a Vila

**Perfil:** Criança · **Horas:** 6 · **Prioridade:** P2 · **Milestone:** Expansão · **Depende de:** US-06.1, US-03.4

*Como criança, quero comprar e pôr árvores, bancos e caminhos na Vila, e tirá-los quando quiser, para que o reino fique do meu jeito.*

**Requisitos do SGDD que ela realiza:**

- `§12-18` — Utilizar moedas somente para decoração na primeira versão
- `LG-Gab-19` — Decoração de 1 a 5 moedas; tirar devolve o preço inteiro
- `§4.2.4` — As moedas serão utilizadas para decoração e personalização da vila, incluindo árvores, bancos, lagos, estradas, vegetação e outros elementos de cenário

**Cenários de aceite:**

- Dado 5 moedas e uma árvore de 3, quando a criança põe a árvore, então fica com 2 moedas e a árvore fica, também depois de reabrir o jogo.
- Dado 2 moedas e um banco de 3, quando ela tenta comprar, então o banco aparece indisponível.
- Dado uma árvore posta, quando ela a tira, então recebe as 3 moedas de volta.
- Dado o catálogo, quando ela o abre, então vê árvores, bancos, lagos, estradas e vegetação, e nenhuma obra à venda.

| Task | O que fazer | Horas e camada |
| --- | --- | --- |
| T-06.2.1 | Escolher e importar árvores, bancos, lagos, estradas e vegetação de pacotes cuja licença permite o uso, e um `DecorationItem` (categoria e preço de 1 a 5 moedas) por item, na lista do catálogo | 2 h · arte |
| T-06.2.2 | `DecorationMode` (DECORATION_MODE): o catálogo, os lugares permitidos, pôr e tirar, com a interface montada na cena `Village` | 2 h · presentation |
| T-06.2.3 | A compra com moedas em C# puro (a moeda não compra obra; tirar devolve o preço inteiro) e a decoração no save; teste EditMode: as moedas não mudam a demanda nem o progresso, lugar proibido recusado, saldo insuficiente, devolução, e sair pela pausa mantém o que foi posto | 2 h · gameplay |

## 4.7 EP-07 — O reino inteiro

As dez construções com o grafo de dependências, a Vila que cresce com regiões, usos e habitantes novos, a apresentação da Vila, e os interiores do Observatório, do Museu e do Parque.

**Por que importa:** As construções prontas mudam o mundo (§12-12, DP-05): é o que a criança vê crescer.

**Pronto quando:**

- As dez obras existem como dados, com o grafo do §5.2;
- A Vila muda a cada obra concluída, e os três interiores abrem sem registrar tentativa.

## 4.7.1 US-07.1 — As dez construções, cada uma no seu tempo

**Perfil:** Criança · **Horas:** 16 · **Prioridade:** P2 · **Milestone:** Expansão · **Depende de:** US-01.1, US-03.3, US-03.4

*Como criança, quero ter as dez construções do reino, cada uma liberada quando as que ela pede ficam prontas, para que o reino cresça na ordem que faz sentido.*

**Requisitos do SGDD que ela realiza:**

- `§12-11` — Representar dependências entre construções por dados configuráveis
- `§12-12` — Construções devem desbloquear experiências ou mudanças perceptíveis do mundo
- `§5.2` — A primeira versão considera dez construções funcionais principais: Ponte; Fazenda; Moinho; Observatório; Museu; Parque de Diversões; Casas; Laboratório; Ferraria; Mercado

**Cenários de aceite:**

- Dado a Fazenda concluída e o Moinho não, quando a criança abre a Vila, então o Mercado segue bloqueado; com os dois, ele libera.
- Dado um grafo com ciclo, quando o grafo é montado, então o ciclo é recusado.
- Dado o Observatório concluído e nenhuma outra obra, quando ela abre a Vila, então o Laboratório e as Casas estão disponíveis, porque não têm pré-requisito.
- Dado o cadastro, quando o time conta as obras, então são dez, cada uma com a sua sequência de 10 peças (5 `MATCH` e 5 `NON_MATCH`); concluir as Casas põe casas e aldeões novos na Vila.

| Task | O que fazer | Horas e camada |
| --- | --- | --- |
| T-07.1.1 | **Já existe** (`VillageBuilding.cs:37-38`): ao concluir a obra, a `VillageBuilding` liga o `completedArt` e desliga o `plotArt` | já feita · presentation |
| T-07.1.2 | Lista de pré-requisitos no `BuildingDefinition` (lendo o `prerequisite` único antigo, `BuildingDefinition.cs:19`) e o grafo em C# puro, com a disponibilidade (hoje `GameSession.IsUnlocked`, `GameSession.cs:47-53`) e a recusa de ciclo; teste EditMode com o grafo do §5.2 | 2 h · gameplay |
| T-07.1.3 | Preencher os pré-requisitos de cada obra com o grafo do §5.2 (L-ABD-14) e ajustar os scripts de Editor do aluno só onde quebram (`KingdomValidation.cs:41,59,80,109`, `PlayModeFlowChecks.cs:142,194`) | 2 h · conteúdo |
| T-07.1.4 | Arte do Laboratório em pixel art | 2 h · arte |
| T-07.1.5 | Cadastrar o Laboratório: sem pré-requisito, com a sequência de 10 peças (modelos e malhas pelo `TrialMeshBaker` do aluno) e o lugar na Vila, na lista `buildings` do `VillageWorld`; teste de que ele não libera outra obra (LA-14) | 2 h · conteúdo |
| T-07.1.6 | Arte da Fazenda e do Mercado | 2 h · arte |
| T-07.1.7 | Cadastrar a Fazenda (depende da Ponte) e o Mercado (da Fazenda e do Moinho), com os campos e as barracas dentro do `completedArt` de cada um; teste das dependências | 2 h · conteúdo |
| T-07.1.8 | Arte das Casas e do Parque de Diversões | 2 h · arte |
| T-07.1.9 | Cadastrar as Casas (sem pré-requisito) e o Parque (depende da Ferraria), com as casas e os aldeões novos dentro do `completedArt` das Casas, somando a população; teste das dez obras | 2 h · conteúdo |

## 4.7.2 US-07.2 — A Vila ganha vida

**Perfil:** Criança · **Horas:** 9 · **Prioridade:** P2 · **Milestone:** Expansão · **Depende de:** US-03.1, US-03.2, US-03.4

*Como criança, quero ver o meu reino na primeira vez que entro, e ver a Vila mudar quando termino uma obra, com regiões novas, obras em uso e habitantes novos, para que eu veja o reino crescer.*

**Requisitos do SGDD que ela realiza:**

- `§12-12` — Construções devem desbloquear experiências ou mudanças perceptíveis do mundo
- `LG-Gab-22` — Habitantes de aparência neutra e constante; um novo na Vila por construção concluída, até 5; o habitante de cada peça sai do modelo dela (v1.4, conforme a implementação do aluno) *(aqui, um habitante novo na Vila por obra concluída, até 5)*

**Cenários de aceite:**

- Dado um código sem reino, quando a criança escolhe JOGAR, então vê o carregamento e a apresentação da Vila, que um clique pula.
- Dado um save com a Ponte pronta, quando a Vila abre, então a região nova está aberta.
- Dado a Fazenda acabou de ficar pronta, quando ela volta à Vila, então um habitante que não tinha visto anda por lá, e cada obra pronta mostra o seu uso (o moinho gira, a ferraria solta fumaça).
- Dado o Observatório concluído, quando ela volta à Vila, então nenhuma placa fica destacada; dado o Moinho escolhido, só ele fica destacado.

| Task | O que fazer | Horas e camada |
| --- | --- | --- |
| T-07.2.1 | Revelar a região da Ponte quando ela fica pronta, e pôr um habitante novo por obra concluída (até 5, com o retrato do `CharacterPortrait`) nos pontos de aparição, montados na cena `Village`; teste PlayMode com uma Vila mínima | 2 h · presentation |
| T-07.2.2 | A animação de uso de cada obra concluída (o moinho gira, a fumaça da ferraria, a luz do observatório), dentro do `completedArt` dela | 2 h · arte |
| T-07.2.3 | A apresentação da Vila na primeira entrada do reino (`IntroController` e a animação curta, pulável com um clique), e o teste PlayMode do fluxo estendido: menu → código → JOGAR → LOAD_GAME → apresentação | 2 h · presentation |
| T-07.2.4 | Apagar o destaque das placas onde a seleção zera (`GameApplication.cs:68,81-89`; o destaque é o `VillageBuilding.SetSelected`), para a cor concordar com a seleção ao iniciar, pausar, concluir e reiniciar; teste dos quatro casos | 1 h · presentation |
| T-07.2.5 | Organizar a paleta de Tilemaps da Vila por função, com nomes legíveis, sem mudar GUID nem layout, e deixar explícito no fluxo de edição que as rotas e os obstáculos dependem dos Tilemaps de navegação (Pendências P03 e P19) | 2 h · conteúdo |

## 4.7.3 US-07.3 — Entrar no Observatório, no Museu e no Parque

**Perfil:** Criança · **Horas:** 9 · **Prioridade:** P3 · **Milestone:** Expansão · **Depende de:** US-03.4, US-06.1, US-07.1

*Como criança, quero entrar nas obras prontas e descobrir o céu, os dinossauros e os brinquedos, para que a obra pronta mude o que eu posso fazer no reino.*

**Requisitos do SGDD que ela realiza:**

- `§12-12` — Construções devem desbloquear experiências ou mudanças perceptíveis do mundo
- `LG-Gab-16` — Vila com deslocamento, nos limites do mapa da cena (v1.4); Observatório, Museu e Parque com conteúdo provisório; o conteúdo é do pesquisador *(aqui, o conteúdo do Observatório, do Museu e do Parque)*

**Cenários de aceite:**

- Dado o Observatório aberto, quando a criança clica na Lua, então vê o nome e a frase; num outro dia de jogo, o céu mostra outros corpos; numa sessão nova, nada está selecionado.
- Dado o Museu pronto, quando ela entra e clica no tiranossauro, então vê o nome e a frase.
- Dado o Parque pronto, quando ela clica na roda-gigante, então ela gira.
- Dado que ela saiu do Museu ou do Parque, quando volta às tentativas, então nenhuma tentativa nem ponto foi registrado lá dentro.

| Task | O que fazer | Horas e camada |
| --- | --- | --- |
| T-07.3.1 | **Já existe:** a tela do Observatório, com corpos celestes de nome e tipo (`ObservatoryView`, `CelestialTarget.cs:7`) | já feita · presentation |
| T-07.3.2 | O Observatório com a descrição de cada corpo (os 9 textos são do Gabriel), 3 dos 9 por dia de jogo e a seleção limpa entre sessões (P21); teste da descrição, da variação por dia e da seleção limpa | 2 h · presentation |
| T-07.3.3 | Arte do interior do Museu, com 5 dinossauros e 2 fósseis | 2 h · arte |
| T-07.3.4 | `ExhibitView`: o interior do Museu montado num painel sobre a tela da Vila, com o nome e a frase de cada peça (textos do Gabriel); teste de que nenhuma tentativa é registrada lá | 2 h · presentation |
| T-07.3.5 | Arte do interior do Parque, com 2 brinquedos animados | 2 h · arte |
| T-07.3.6 | `RideToggle`: o interior do Parque montado, cada brinquedo liga e desliga ao clique, sem pontos nem tentativa; teste | 1 h · presentation |

## 4.8 EP-08 — Tutorial, som e acessibilidade

O tutorial de cinco passos com a tentativa assistida, os microtutoriais de desbloqueio, os sons com o equivalente visual, e os ajustes de acessibilidade e de baixa visão.

**Por que importa:** O Core começa direto, sem tutorial; aqui o jogo ganha o ensino e os ajustes que o SGDD pede para quem precisa deles.

**Pronto quando:**

- Quem entra pela primeira vez passa pelo tutorial, e quem já viu entra direto;
- Todo som essencial tem equivalente visual, e o alto contraste, a ampliação e a leitura por voz funcionam.

## 4.8.1 US-08.1 — Aprender no tutorial e nas dicas

**Perfil:** Criança · **Horas:** 12 · **Prioridade:** P2 · **Milestone:** Expansão · **Depende de:** US-02.1, US-03.2, US-06.2, US-07.3

*Como criança, quero aprender passo a passo, girando a peça com a mão no treino, o que é a mesma peça e o que é a espelhada, e ver uma dica curta quando algo novo abre, para que eu entenda o jogo sem ter de ler.*

**Requisitos do SGDD que ela realiza:**

- `§12-19` — Onboarding não pode depender exclusivamente de leitura
- `§12-07` — Manter tentativas de treinamento separadas das tentativas válidas
- `LG-Gab-13` — Tutorial de 5 passos, giro à mão nos passos 1–4, peças fixas, “pular” à mostra desde o começo, rever no menu

**Cenários de aceite:**

- Dado um código novo, quando a criança escolhe JOGAR, então passa pelo tutorial de cinco passos, que tem o “pular” à mostra desde o começo.
- Dado o passo 3, quando ela arrasta a peça, então a peça gira; no passo 5 e em toda tentativa `VALID`, nada gira.
- Dado o tutorial e uma tentativa do jogo, quando o arquivo da sessão é lido, então a do jogo está em `trials`, com o `trial_type` de `VALID`, e a do passo 5 (`TRAINING`) fica fora dela, separada, como o tutorial de hoje (`TrialTelemetry.md:9`).
- Dado o código já viu o tutorial, quando ela clica CONTINUAR ou NOVO REINO, então vai direto à obra; REVER TUTORIAL fica no menu.
- Dado a decoração abriu pela primeira vez, quando ela entra, então vê o microtutorial dela, que não volta depois de visto.

| Task | O que fazer | Horas e camada |
| --- | --- | --- |
| T-08.1.1 | **Já existe:** o tutorial de 2 passos, a mesma peça girando de 135° a 0° e depois a peça espelhada (`TutorialController.cs:37-58`) | já feita · gameplay |
| T-08.1.2 | Estender o `TutorialController` aos passos 1 a 5 do §4.4, em ordem, com peças fixas e o “pular” à mostra, e o passo 5 como tentativa assistida `TRAINING`, sem girar, com a dica visual montada, fora da lista `trials` do arquivo como o tutorial de hoje; teste PlayMode de que o tutorial termina com uma tentativa `TRAINING` que não entra em `trials` | 2 h · gameplay |
| T-08.1.3 | Montar os passos 1 a 4 (animação, símbolos e texto curto) na cena do tutorial, com a narração de cada passo por voz sintética provisória e legenda | 2 h · conteúdo |
| T-08.1.4 | `TrainingRotator`: girar a peça nos passos 1 a 4 por arraste ou pelos botões de girar, ligados na cena, e desligado no passo 5 e em toda `VALID`; teste | 2 h · gameplay |
| T-08.1.5 | A marca de tutorial visto, guardada por código: CONTINUAR e NOVO REINO pulam o tutorial quando o código já o viu, e REVER TUTORIAL entra no menu; teste de primeira vez com tutorial e segunda direto à obra | 2 h · gameplay |
| T-08.1.6 | O microtutorial como camada sobre a tela, com o “já visto” no save; teste de que aparece no desbloqueio e não volta depois de visto | 2 h · gameplay |
| T-08.1.7 | Montar os microtutoriais pictográficos da Vila, da escolha de projeto, da decoração e dos interiores, ligados à camada | 2 h · conteúdo |

## 4.8.2 US-08.2 — Os sons do reino e os ajustes de acessibilidade

**Perfil:** Criança · **Horas:** 10 · **Prioridade:** P2 · **Milestone:** Expansão · **Depende de:** US-04.1, US-04.2, US-05.2, US-06.1

*Como criança, quero ouvir o reino sem perder nada se o som estiver desligado, e poder diminuir o movimento, aumentar o contraste, ampliar as peças e ouvir o que está escrito, para que eu jogue do jeito que consigo.*

**Requisitos do SGDD que ela realiza:**

- `§12-20` — Informações essenciais não podem depender exclusivamente de áudio
- `§12-21` — Cor não pode determinar a resposta correta *(aqui, no alto contraste)*
- `LG-Gab-20` — Duas músicas, sete efeitos, silêncio na comparação, narração por voz sintética provisória
- `LG-Gab-17` — Configurações do aparelho, com os padrões da §10.3 *(aqui, a acessibilidade, com a ampliação até 1,5×)*
- `§13` — Acessibilidade para deficiência visual severa — Delimitar escopo e estudar alternativas, se aplicável

**Cenários de aceite:**

- Dado um acerto, quando o retorno toca, então a criança ouve o som e vê o ícone equivalente; com o mudo, não perde informação.
- Dado a comparação (TRIAL_ACTIVE), quando ela escuta, então nenhum som toca, e nenhum som depende do gabarito.
- Dado o alto contraste ligado, quando a tentativa aparece, então as duas peças têm a mesma cor clara e o mesmo contorno forte, num fundo escuro; com a ampliação, as peças crescem até 1,5× sem mudar o ângulo.
- Dado a leitura por voz ligada, quando ela abre a pausa, então ouve as opções; a voz nunca descreve a peça.
- Dado reduzir animações ligado, quando ela reabre o jogo, então continua reduzido.

| Task | O que fazer | Horas e camada |
| --- | --- | --- |
| T-08.2.1 | Os sons da lista (duas músicas e sete efeitos), de pacotes cuja licença permite o uso, importados e ligados no `AudioManager` | 2 h · arte |
| T-08.2.2 | `AudioVisualCue`: o ícone equivalente de cada som essencial, ligado na cena; teste de que nenhum som toca em TRIAL_ACTIVE nem depende do gabarito | 2 h · presentation |
| T-08.2.3 | Os controles de acessibilidade na tela de configurações (reduzir animações, reduzir estímulos do ambiente, alto contraste, ampliação até 1,5× e leitura por voz), guardados no aparelho (no arquivo da sessão só quando o plano de dados que o Gabriel adiou pedir); a chave do bônus não fica aqui | 2 h · presentation |
| T-08.2.4 | `HighContrastMode` com o material de alto contraste (peça clara, contorno forte, fundo escuro) e a ampliação das peças até 1,5× sem mudar a disparidade; teste de que as duas peças ficam iguais e o ângulo não muda | 2 h · presentation |
| T-08.2.5 | `ScreenReader`: o áudio de cada tela que não é estímulo (menu, configurações, pausa, diálogos, anoitecer), gerado por voz sintética provisória e ligado; teste de que a voz nunca descreve a peça | 2 h · conteúdo |

## 4.9 EP-09 — Pesquisa e marco da Expansão

O `complexity_score` e a similaridade do distrator, os parâmetros de dificuldade recebidos por um provedor (a regra é do backend), o modo tarefa de referência e o marco: o jogo inteiro jogado na build de PC.

**Por que importa:** É o que o pesquisador leva da Expansão: a dificuldade de cada peça registrada, a tarefa de referência para comparar, e a prova de que o jogo inteiro roda montado.

**Pronto quando:**

- Toda tentativa grava o `complexity_score`, a similaridade e o `difficulty_state` recebido;
- A história de marco da Expansão foi jogada na build, do menu à décima obra, e o arquivo de telemetria bateu com a jogada.

## 4.9.1 US-09.1 — A dificuldade de cada peça, medida e recebida do provedor

**Perfil:** Pesquisador · **Horas:** 10 · **Prioridade:** P2 · **Milestone:** Expansão · **Depende de:** US-02.1, US-02.3

*Como pesquisador, quero que cada peça tenha a complexidade e a similaridade calculadas e que o jogo receba de um provedor os parâmetros da próxima tentativa e os grave, para que a adaptação rode no backend (padrão CIATEC) e eu reconstitua o que foi decidido e aplicado.*

**Requisitos do SGDD que ela realiza:**

- `§12-27` — Adaptação de dificuldade não pode ser determinada apenas por diagnóstico
- `§12-28` — Adaptação deverá considerar inicialmente acurácia e RT *(o jogo manda acurácia e RT; a regra é do backend, em `ABD-1-Gabriel-api.md`)*
- `§12-29` — Ângulo, complexidade e similaridade do distrator devem ser parametrizáveis
- `LG-Gab-06` — Começa no fácil (`a45-cLOW-s1`); sobe em rodízio ângulo → complexidade → similaridade; desce desfazendo o último aumento *(regra executada pelo backend, via API (SGDD v1.5))*
- `§11.4` — O `difficulty_state` grava o estado da adaptação recebido do backend (§4.3; o provedor local, até a API) no formato `a<ângulo máximo>-c<LOW|MID|HIGH>-s<1|2|3>`, como `a45-cLOW-s1`, o estado inicial

**Cenários de aceite:**

- Dado um policubo de 10 cubos, 3 mudanças de direção, sem ramificação e não planar, quando o cálculo roda, então o `complexity_score` é 6,6 e a faixa é média; acima de 10, fica 10.
- Dado um `NON_MATCH` espelhado, quando a escala é aplicada, então a similaridade é 3; num `MATCH`, o campo não se aplica.
- Dado o provedor local, quando a criança joga uma obra inteira, então toda tentativa `VALID` recebe `a45-cLOW-s1`, as peças são as da sequência e não há evento de ajuste.
- Dado um provedor de teste que muda o estado depois da 3ª tentativa, quando a 4ª começa, então ela recebe o estado novo, e o registro grava o ajuste com o antes e o depois.
- Dado um provedor que não responde, quando a tentativa começa, então ela usa os últimos parâmetros, e o RT começa na hora.

| Task | O que fazer | Horas e camada |
| --- | --- | --- |
| T-09.1.1 | Extrair do policubo o número de cubos, as mudanças de direção, as ramificações e a não planaridade, e calcular o `complexity_score` pela fórmula provisória, com os pesos no `ComplexityConfig` e a faixa baixa, média ou alta; teste EditMode em 3 policubos conhecidos e nos limites das faixas | 2 h · gameplay |
| T-09.1.2 | Calcular o score de toda peça no começo da sessão (o `ComplexityConfig` criado e ligado) e classificar a similaridade de todo `NON_MATCH` pela escala provisória (3 espelho, 2 uma célula, 1 duas ou mais), gravando os dois (`complexity_score`, `distractor_similarity`) no `TelemetryTrialRecord` e documentando-os no `TrialTelemetry.md`; teste de que a similaridade não depende do score | 2 h · dados |
| T-09.1.3 | `IDifficultyProvider` em `Game.Core` (pede os parâmetros da próxima tentativa e recebe o `TelemetryTrialRecord` de cada uma, sem entrada de diagnóstico) e o `FixedDifficultyProvider`, que devolve sempre `a45-cLOW-s1`, padrão até a API | 2 h · gameplay |
| T-09.1.4 | Ligar o provedor ao fluxo: pedir os parâmetros no começo de cada `VALID`, entregar o registro ao fim, e usar os últimos recebidos se ele não responder, sem atrasar o RT; a peça é sempre a da sequência fixa (LA-10); teste EditMode com o provedor fixo, um que muda e um que não responde | 2 h · gameplay |
| T-09.1.5 | Gravar em cada tentativa o `difficulty_state` recebido e, quando ele muda, o evento de ajuste com o antes e o depois, e acrescentá-los ao `TelemetryTrialRecord` e ao `TrialTelemetry.md`; teste | 2 h · telemetria |

## 4.9.2 US-09.2 — A tarefa de referência e a visão das tentativas

**Perfil:** Pesquisador · **Horas:** 5 · **Prioridade:** P2 · **Milestone:** Expansão · **Depende de:** US-02.1, US-02.3, US-04.3

*Como pesquisador, quero rodar as mesmas tentativas sem reino, habitante nem economia, e ler cada sessão numa tabela com os campos do §11.4, tirada do arquivo do jogo, para que eu compare o jogo com a tarefa de referência.*

**Requisitos do SGDD que ela realiza:**

- `§13` — Validade da gamificação — Comparar com tarefa de referência em etapa posterior
- `§12-05` — Invalidar RT de tentativas pausadas, interrompidas ou abandonadas *(na visão derivada: o RT da tentativa com pausa (`paused_duration_ms` maior que 0) ou fechada `interrupted` sai inválido; decisão do orientador de 26/11/2026: vale o que o Gabriel decidiu no código e no PR #432)*
- `§12-04` — Registrar apenas a primeira resposta válida *(na visão derivada, pela primeira resposta de `attempts`)*

**Cenários de aceite:**

- Dado o mediador escolhe o modo referência no painel, quando a sessão começa, então só há as tentativas, sem reino.
- Dado uma sessão em cada modo, quando o pesquisador compara os arquivos, então os campos são os mesmos, com os mesmos estímulos, e o `session_mode` diz qual é qual.
- Dado um arquivo de sessão com uma tentativa pausada e uma interrompida, quando o pesquisador gera a visão derivada, então há uma linha por `trial_id`, com a primeira resposta e o RT dela, o RT das duas marcado inválido, e o JSON continua igual.

| Task | O que fazer | Horas e camada |
| --- | --- | --- |
| T-09.2.1 | `ReferenceModeController`: tentativas sem reino, habitante nem economia, aberto só pelo botão do painel do mediador e montado na cena | 2 h · gameplay |
| T-09.2.2 | O `session_mode` (reino ou referência) no `TelemetrySessionRecord`; teste de que os dois modos gravam os mesmos campos com os mesmos estímulos | 1 h · telemetria |
| T-09.2.3 | Visão de pesquisa derivada do arquivo do Gabriel, sem campo novo no JSON (PR #432): um leitor em C# puro que transforma cada item de `trials` numa linha com os campos do §11.4 — `trial_type`, `rotation_angle` de 0° a 180° com `rotation_direction` e o ângulo bruto, `response` e `reaction_time_ms` da primeira resposta válida (§12-04), `attempt_count` (o tamanho de `attempts`), `trial_status` e a validade do RT (§12-05: inválido com pausa ou `interrupted`) —, sem mudar o JSON 1.0 nem perder resposta; teste EditMode sobre um arquivo sintético; documentar no `TrialTelemetry.md` | 2 h · telemetria |

## 4.9.3 US-09.3 — Marco da Expansão: o jogo inteiro jogado do menu à décima obra

**Perfil:** Pesquisador · **Horas:** 4 · **Prioridade:** P2 · **Milestone:** Expansão · **Depende de:** US-04.4, US-05.1, US-05.2, US-06.1, US-06.2, US-07.1, US-07.2, US-07.3, US-08.1, US-08.2, US-09.1, US-09.2 · **história de marco do Expansão**

*Como pesquisador, quero jogar a build de PC do jogo inteiro do menu à décima obra, com tutorial, decoração e interiores, voltar outro dia e conferir o arquivo de telemetria, para que eu veja o jogo inteiro montado.*

**Cenários de aceite:**

- Dado a build e um código novo, quando a pessoa joga do tutorial à décima obra, então a Vila mostra as dez prontas e cada dependente liberou na hora certa, sem exceção.
- Dado o jogo fechado com uma obra pela metade, quando ela reabre, então a obra está no mesmo ponto, com as mesmas moedas e a mesma decoração.
- Dado o arquivo da sessão, quando ela o confere contra a jogada, então os números batem, e a visão derivada lê o arquivo, agora com o tutorial fora de `trials`, o `complexity_score`, a `distractor_similarity`, o `difficulty_state` recebido e o `agility_bonus`.

| Task | O que fazer | Horas e camada |
| --- | --- | --- |
| T-09.3.1 | Empresa, identificador e ícone do Meu Pequeno Reino nas Player Settings (o produto já é `MeuPequenoReino`, `ProjectSettings/ProjectSettings.asset:16`), copiando os saves e as sessões do Core para a pasta nova, que muda com a empresa | 1 h · build |
| T-09.3.2 | Gerar a build limpa do jogo inteiro para o PC (Windows) e jogá-la do menu à décima obra, com tutorial, decoração e interiores; fechar, reabrir e continuar; corrigir o que aparecer | 2 h · build |
| T-09.3.3 | Conferir o arquivo de telemetria contra a jogada, com os campos da Expansão (`complexity_score`, `distractor_similarity`, `difficulty_state`, `agility_bonus`) e o tutorial fora de `trials`, e gerar dele a visão derivada (T-09.2.3) | 1 h · telemetria |

# 5 ROTEIRO

Sem datas (LA-02): só a ordem. Cada linha começa quando as histórias de que ela depende estão feitas; histórias da mesma linha podem andar juntas.

| Passo | Milestone | Histórias e horas |
| --- | --- | --- |
| 1 | Core | US-01.1, US-04.2 · 10 h |
| 2 | Core | US-02.1 · 4 h |
| 3 | Core | US-02.2, US-03.1 · 6 h |
| 4 | Core | US-02.3 · 1 h |
| 5 | Core | US-02.4, US-03.2 · 17 h |
| 6 | Core | US-03.3, US-03.4, US-04.1, US-04.3 · 14 h |
| 7 | Core | US-04.4 · 5 h |
| 8 | Expansão | US-05.1, US-05.2, US-09.1, US-09.2 · 35 h |
| 9 | Expansão | US-06.1, US-07.1, US-07.2 · 34 h |
| 10 | Expansão | US-06.2, US-07.3 · 15 h |
| 11 | Expansão | US-08.1, US-08.2 · 22 h |
| 12 | Expansão | US-09.3 · 4 h |

# 6 COMO OS MARCOS FECHAM

**O milestone termina com o jogo na mão, e não com as tasks fechadas.** Na US-04.4 (Core) e na US-09.3 (Expansão), uma pessoa joga a build de PC do menu ao fim, fecha, reabre e continua, e depois abre o arquivo de telemetria da sessão (`persistentDataPath/Telemetry/session_<session_id>.json`, um JSON por sessão, no formato de `Documentation/TrialTelemetry.md`) e o confere contra o que jogou:

| O que conferir | Como |
| --- | --- |
| As tentativas | Um item em `trials` por `trial_id`; o número de itens bate com as peças jogadas, e o tutorial fica fora |
| O erro | A peça errada tem um item só, com mais de uma resposta em `attempts`; a primeira resposta e o RT dela estão no `attempts[0]` |
| A pausa e a perda de foco | A tentativa pausada fecha `completed`, com o tempo parado no `paused_duration_ms`, fora do RT; a perda de foco e os 75 s sem resposta fecham `interrupted` |
| O retorno | `streak_before`, `streak_after`, os pontos e o `feedback_shown` preenchidos em cada resposta |
| O que ainda não existe | No Core, `complexity_score`, `distractor_similarity`, `difficulty_state` e `agility_bonus` ainda não estão no arquivo; na Expansão, vêm preenchidos |

O que não bater vira task na história que deveria ter feito aquilo.

# 7 RASTREIO DOS REQUISITOS

Todo requisito do §12 do SGDD e toda decisão LG-Gab da §13B estão em ao menos uma história, ou a linha diz por que não. Esta tabela é gerada pelo script do plano, que para se algum faltar.

| Requisito | Texto | Histórias |
| --- | --- | --- |
| `§12-01` | Apresentar exatamente uma referência ativa e um candidato por tentativa | US-02.1 |
| `§12-02` | Balancear condições `MATCH` e `NON_MATCH` no conjunto de tentativas | US-03.3 |
| `§12-03` | Registrar RT somente após estímulos e controles estarem completamente disponíveis | US-02.1 |
| `§12-04` | Registrar apenas a primeira resposta válida | US-02.2, US-09.2 |
| `§12-05` | Invalidar RT de tentativas pausadas, interrompidas ou abandonadas | US-09.2 |
| `§12-06` | Impedir rotação manual dos estímulos durante tentativa válida | US-02.1 |
| `§12-07` | Manter tentativas de treinamento separadas das tentativas válidas | US-02.1, US-08.1 |
| `§12-08` | Registrar parâmetros efetivamente apresentados em cada tentativa | US-02.3 |
| `§12-09` | Implementar Grande Livro de Projetos como referência narrativa e funcional | US-05.1 |
| `§12-10` | Permitir construção e persistência do reino | US-03.2 |
| `§12-11` | Representar dependências entre construções por dados configuráveis | US-07.1 |
| `§12-12` | Construções devem desbloquear experiências ou mudanças perceptíveis do mundo | US-07.1, US-07.2, US-07.3 |
| `§12-13` | Implementar streak baseada somente em acertos consecutivos | US-02.4 |
| `§12-14` | Erro reinicia streak sem retirar progresso acumulado | US-02.4 |
| `§12-15` | Implementar multiplicadores de streak configuráveis | US-02.4 |
| `§12-16` | Implementar Bônus de Agilidade independente da streak | US-06.1 |
| `§12-17` | Converter pontos do dia em moedas com taxa configurável | US-06.1 |
| `§12-18` | Utilizar moedas somente para decoração na primeira versão | US-06.2 |
| `§12-19` | Onboarding não pode depender exclusivamente de leitura | US-08.1 |
| `§12-20` | Informações essenciais não podem depender exclusivamente de áudio | US-08.2 |
| `§12-21` | Cor não pode determinar a resposta correta | US-05.2, US-08.2 |
| `§12-22` | Permitir controle de áudio | US-04.1 |
| `§12-23` | Reduzir estímulos visuais simultâneos durante a tarefa | US-05.2 |
| `§12-24` | Não utilizar cronômetro visível ou pressão temporal explícita | US-02.1 |
| `§12-25` | Resposta deve exigir interação simples e de baixa precisão | US-04.2 |
| `§12-26` | NPC não pode fornecer pistas sobre a resposta pela aparência ou comportamento | US-05.2 |
| `§12-27` | Adaptação de dificuldade não pode ser determinada apenas por diagnóstico | US-09.1 |
| `§12-28` | Adaptação deverá considerar inicialmente acurácia e RT | US-09.1 |
| `§12-29` | Ângulo, complexidade e similaridade do distrator devem ser parametrizáveis | US-02.1, US-09.1 |
| `§12-30` | Jogador deve poder pausar ou interromper sem perda de progresso | US-04.1 |
| `§12-31` | Implementar Vila pseudo-isométrica com elementos 2D | US-03.4 |
| `§12-32` | Implementar interação principal de NPC em composição 2D frontal | US-03.1 |
| `§12-33` | Registrar origem, autor e licença de todo asset externo | — (fora: o inventário (US-03) saiu na versão 8 e o registro da licença no corte de protótipo (versão 9); importar só de pacote cuja licença permite o uso continua na task que importa) |
| `§12-34` | Manter regras centrais independentes da engine sempre que possível | US-01.1 |
| `§12-35` | Caracterizar precisão temporal da plataforma antes de uso científico do RT | — (fora: a caracterização (US-52) foi cortada pelo orientador na versão 8; o SGDD a pede antes do uso científico do RT, fora deste backlog) |
| `LG-Gab-01` | Rotação em profundidade, em torno do eixo x, y ou z, como no conjunto original (v1.4, conforme a implementação do aluno) | US-02.1 |
| `LG-Gab-02` | 5 peças por construção; só o `MATCH` aceito conta, e a obra fecha no 10º estímulo; o erro não soma nem tira (v1.4, conforme a implementação do aluno) | US-03.3 |
| `LG-Gab-03` | Depois de um erro a mesma peça volta; registra-se a primeira resposta (v1.4, conforme a implementação do aluno) | US-02.2 |
| `LG-Gab-04` | Um reino por código de criança, escolhido no menu; o mesmo código serve à escola e à casa | US-03.2 |
| `LG-Gab-05` | Pausa pelo mesmo botão e ícone em todas as telas, menos no menu e nas configurações; RETOMAR, CONFIGURAÇÕES, SALVAR E SAIR; no desafio, a pausa é o botão VILA, que leva à Vila (v1.4, conforme a implementação do aluno) | US-04.1 |
| `LG-Gab-06` | Começa no fácil (`a45-cLOW-s1`); sobe em rodízio ângulo → complexidade → similaridade; desce desfazendo o último aumento | US-09.1 |
| `LG-Gab-07` | Cada tela segue sozinha (4 / 2,5 / 2,2 / 1,4 s); um clique adianta | US-03.1, US-05.1 |
| `LG-Gab-08` | Os quatro resultados com consequência própria; quatro explicações em ordem fixa; falas de até 8 palavras | US-02.4, US-05.1 |
| `LG-Gab-09` | Placa só no acerto (streak, pontos, bônus); nada no erro; moedas na Vila | US-02.4 |
| `LG-Gab-10` | Bônus de Agilidade desligado até o piloto, com chave no painel do mediador | US-06.1 |
| `LG-Gab-11` | O dia fecha em 10 tentativas válidas ou no fim da sessão; o número é pendência do piloto | US-06.1 |
| `LG-Gab-12` | A primeira construção é o Observatório; antes dela nenhuma outra (v1.4, conforme a implementação do aluno) | US-03.3 |
| `LG-Gab-13` | Tutorial de 5 passos, giro à mão nos passos 1–4, peças fixas, “pular” à mostra desde o começo, rever no menu | US-08.1 |
| `LG-Gab-14` | SERVE e NÃO SERVE com ícone e rótulo, ≥ 2,3 cm; peças com ~37% da altura | US-04.2 |
| `LG-Gab-15` | Painel do mediador (Ctrl+Shift+M ou botão “adulto” do menu; os três toques no canto saíram na v1.5), encerramento com motivo gravado | US-04.3 |
| `LG-Gab-16` | Vila com deslocamento, nos limites do mapa da cena (v1.4); Observatório, Museu e Parque com conteúdo provisório; o conteúdo é do pesquisador | US-03.4, US-07.3 |
| `LG-Gab-17` | Configurações do aparelho, com os padrões da §10.3 | US-04.1, US-08.2 |
| `LG-Gab-18` | Arquivo local por sessão, exportação pelo painel do pesquisador, criança identificada pelo código | US-02.3 |
| `LG-Gab-19` | Decoração de 1 a 5 moedas; tirar devolve o preço inteiro | US-06.2 |
| `LG-Gab-20` | Duas músicas, sete efeitos, silêncio na comparação, narração por voz sintética provisória | US-03.1, US-08.2 |
| `LG-Gab-21` | Pergunta neutra aos 45 s, abandono aos 75 s; “parar” fecha a sessão e mostra a Vila | US-02.2, US-05.2 |
| `LG-Gab-22` | Habitantes de aparência neutra e constante; um novo na Vila por construção concluída, até 5; o habitante de cada peça sai do modelo dela (v1.4, conforme a implementação do aluno) | US-03.1, US-05.2, US-07.2 |

# 8 DECISÕES DESTE PLANO

Escolhas de organização e as decisões do orientador que o plano aplica. As de design estão no SGDD.

- **Reescrito no molde do Caça às Cores (26/11/2026, decisão do orientador):** uma história por coisa que a criança, o mediador ou o pesquisador faz ou vê; uma task por pedaço de trabalho, de 1 a 2 h, sem o par `[agente]`/`[aluno]`; teste dentro da task que escreve a regra. O escopo é o da versão 10, sem mudança: o mapa de cada história e task antiga para a nova está em `I:\Ciatec\tmp-claude\abd-final\Gabriel\mapa-molde-caca.md`.
- **Core e Expansão.** O Core é o jogo jogável de ponta a ponta, com a mecânica inteira (a tentativa, a streak, os pontos, o layout do PC e o `feedback_shown`) e a telemetria local; vai para a Expansão, por decisão do orientador, o tutorial, a narrativa do Livro e das quatro explicações (as antigas US-21 e US-22), a comparação calma (US-18 e US-23), as dez obras (US-31), a complexidade e a similaridade (US-16 e US-17) e a dificuldade recebida do provedor (US-42 e US-44). Nenhuma história do Core depende da Expansão.
- **O código do Gabriel decide** (regra do orientador, 26/11/2026): a sequência fixa de 10 peças por obra, com 5 `MATCH` e 5 `NON_MATCH` (LA-10, `BuildingDefinition.cs:21`); as rotações do código, 45° a 225° em x, y ou z, sem 0° e sem convenção nova de sentido (LA-11); a mesma peça volta depois do erro (LG-Gab-03); o Observatório é a primeira obra (LG-Gab-12); as 7 telas ficam no lugar dos 16 estados do §10.3. Não voltam a US-13, a US-66, a US-72 nem a US-19.
- **A adaptação é do backend, por API** (SGDD v1.5): o jogo recebe os parâmetros por um provedor, aplica e grava o `difficulty_state`; até a API, o provedor local devolve sempre `a45-cLOW-s1`. O contrato está em `ABD-1-Gabriel-api.md`.
- **Telemetria local, no formato do Gabriel** (LG-Gab-18, L-ABD-06; PR #431 e PR #432; decisão do orientador de 26/11/2026: vale o que o Gabriel decidiu no código e no PR #432): um JSON por sessão em `persistentDataPath/Telemetry/session_<session_id>.json`, regravado de forma atômica a cada evento, com `schema_version`, `session_id` (GUID), a lista `trials` e, em cada tentativa, a lista `attempts` com todas as respostas e o RT de cada uma (`Documentation/TrialTelemetry.md`). O arquivo guarda o bruto; os campos do §11.4 que se derivam (`trial_type`, `rotation_angle` de 0° a 180°, `rotation_direction`, a primeira resposta válida, `attempt_count` e a validade do RT) saem de uma **visão de pesquisa derivada** (T-09.2.3, Expansão), sem campo novo no arquivo. A pausa fecha a tentativa `completed`, com o tempo parado no `paused_duration_ms`, fora do RT; o abandono e a perda de foco fecham `interrupted`; não há `paused` nem `abandoned`. A criança não é identificada no arquivo: o código dela fica no save e no menu (LG-Gab-04). O JSON Lines, a exportação e a plataforma, o dispositivo e a acessibilidade na sessão o Gabriel adiou (ao plano de dados aprovado) e saem do backlog; a conexão com a API fica para o backend.
- **Realinhado com a develop de 26/11/2026 (v12 e v13):** o PR #431 já faz o arquivo da sessão, a primeira resposta com o RT dela, os estados da tentativa e a conferência dos 50 estímulos, e essas tasks entraram fechadas (T-02.1.4, T-02.2.2, T-02.2.3, T-02.3.2, T-02.3.3 e T-02.3.4); a T-02.3.1 ficou só com a posição da peça e os instantes de início e de fim. Os campos que o jogo grava por resposta (a streak, os pontos, o `feedback_shown` e o `agility_bonus`) vão para o item da resposta em `attempts`; o tutorial continua fora de `trials` (`TrialTelemetry.md:9`).
- **Pendência do Gabriel com o orientador (sem task):** o PR #432 registrou no ABD do repositório uma "pendência de rastreabilidade": as decisões de design atribuídas à orientação no SGDD v1.4 e no histórico do ABD, e a avaliação do protótipo web, pedem um registro documental independente (a origem e o alcance), e a justificativa do JSON Lines da L-ABD-06 também. O Gabriel e o orientador resolvem isso entre eles; o plano não tem task para isso.
- **Valores provisórios, a confirmar no piloto:** a pontuação (base 100 e as faixas do §4.2.2), as 10 tentativas por dia e por sessão e os 15 min, os 45 s e 75 s da ausência, a fórmula do `complexity_score` e a escala de similaridade. Ficam em assets de configuração, para trocar sem mexer no código.
- **Decisões provisórias que as tasks aplicam:** o Livro em tela cheia com a silhueta da obra e a peça destacada (L-ABD-11); o grafo inteiro do §5.2 só na Expansão, com o pré-requisito único do código no Core (L-ABD-14); quadro acima de 100 ms ou mudança de resolução em TRIAL_ACTIVE marcam `interrupted` (L-ABD-16); rotação manual só nos passos 1 a 4 do tutorial (L-ABD-17); a tentativa com pausa (`paused_duration_ms` maior que 0) respondida conta para a streak, e a fechada `interrupted` sem resposta não (GP-06).
- **Pendências do orientador:** **LA-13** — o habitante sai do `modelId` da peça, e cada modelo aparece numa condição só, então o mesmo habitante volta sempre com a mesma resposta e pode virar pista (DP-03, §12-26); a T-05.2.3 testa que a aparência só depende do `modelId`, e o risco fica para o piloto. **LA-14** — sem pré-requisito em Museu e Observatório, o Laboratório não libera nada; a T-07.1.5 o cadastra sem dependentes até a decisão.
- **Fora do plano** (decisões do orientador e regra 4 do molde): a burocracia (anotar na issue, licença, medições, validação com o SGDD na mão, privacidade, capturas), o teste-robô, os validadores e o JSON Schema, o tutorial no Core, e as plataformas além do PC. Saíram nesta versão a T-40 (um teste que só conferia as Player Settings, um validador) e a caracterização da precisão temporal e o inventário de assets, que já tinham saído antes (§12-35 e §12-33, seção 7).

# 9 IMPORTAÇÃO

O CSV `ABD-1-Gabriel-issues.csv`, ao lado, tem as mesmas colunas de antes (`title, type, epic_id, domain, priority, story_points, sprint, milestone, assignee, parent, labels, estimate_h, blocked_by`): uma linha por épico, história e task, com o milestone, as horas, o pai (`parent`: a história é filha do épico, e a task, da história) e as dependências (`blocked_by`, separadas por `;`). A task já feita leva 0 h e "Já existe" no título, e entra fechada. **Nada foi criado no GitHub.**

A camada de cada task vira a label `layer:` do pacote de labels, sem label nova:

| Camada | Label |
| --- | --- |
| gameplay | `layer:gameplay` |
| presentation | `layer:presentation` |
| dados | `layer:database` |
| conteúdo | `layer:content` |
| arte | `layer:assets` |
| telemetria | `layer:research` |
| build | `layer:platform` |

# 10 HISTÓRICO

| Versão | Data | Mudança |
|---|---|---|
| 1 | 25/11/2026 | Backlog do SGDD inteiro num milestone com data, revisado em contexto limpo. |
| 2 | 26/11/2026 | Dono em cada task (`[agente]` só código e testes, `[aluno]` tudo no Editor), com 60 tasks da divisão (T-264 a T-323); decisões LG-Gab-01 a 22 nos critérios; milestones Core e Expansão, sem data, com 4 stories da divisão (US-73 a US-76); a API fica fora do backlog. |
| 3 | 26/11/2026 | Revisão: a Vila do Core usa só as construções do Core (o saldo de moedas foi para a US-30), cenários do Core sem a Expansão, teste do fluxo do Core (T-326), teste de fumaça sem a cena do aluno, ordem suíte → build nos marcos, animação de uso ligada à T-142; T-73 e T-102 sem captura, por decisão desta revisão (LA-08). |
| 4 | 26/11/2026 | O SGDD foi para a v1.2, com as decisões LG-Gab-01 a 22 escritas nas seções e indexadas na §13B, depois de o orientador validá-las jogando o protótipo web. O ABD já as tinha desde a versão 2: muda só a fonte (v1.2), e o rascunho que a US-58/T-251 entrega passa a ser a v1.3, sem as LG-Gab. Nenhuma story, task ou hora mudou. |
| 5 | 26/11/2026 | Perguntas abertas aprovadas pelo orientador (as lacunas LA-01 a LA-08 e as stories marcadas “(Core?)”), sem mudar o que ele validou no protótipo web: LA-01, 02, 04 a 08 decididas, LA-03 pendência da equipe científica; SAIR no menu do build Windows (T-327, T-328, US-71); US-48 e US-51 foram para a Expansão, e o `AudioManager` (T-210) passou para a US-47, para o volume do Core não depender da Expansão; US-03, US-11, US-52 e US-55 ficam no Core sem a marca. |
| 6 | 26/11/2026 | Revisão final contra o protótipo validado e o pacote de agentes novo (SGDD v1.3): o aluno roda os testes no Test Runner (sai a T-36; entram T-329 e T-330); menu pelo código com CONTINUAR e NOVO REINO; `participant_code` é o código do menu; painel do pesquisador como aba do mediador, com o botão “adulto”; chave do Bônus no painel do mediador (T-331, T-332); apagar dados mantém o reino (T-333); sessão de 10 tentativas; blocos de 4; rodízio da adaptação; `PAUSED` respondida; mapa 2400×1350; botões 380×150; assentimento como pendência (LA-09). |
| 7 | 26/11/2026 | Backlog montado sobre o código do aluno (`develop` @ 7429a32) e o SGDD v1.4: onde o código já decide, o SGDD e o ABD o seguem (regra do orientador), com nomes reais, tasks feitas como “Já existe” (0 h) ou encolhidas, asmdefs, namespace e Input System na T-01, T-334 a T-339 novas e as LA-10 a 14 abertas; LA-03 resolvida. |
| 8 | 26/11/2026 | Decisões do orientador: **a adaptação de dificuldade vai para o backend, por API (padrão CIATEC)**: o motor e a regra saem do jogo (US-43 e as T-188, T-189, T-191, T-301 cortadas), a US-42 vira o provedor que recebe e aplica os parâmetros, com um provedor local fixo até a API (T-340 a T-343), a US-44 grava o `difficulty_state` recebido, e a regra passa a ser especificação do backend em `ABD-1-Gabriel-api.md` (proposta) e no SGDD v1.5. **Cortes no Core**, por não serem necessários ao Core jogável e pronto para a API: US-02, US-03, US-04, US-15, US-52 (com o SP-01) e US-55, com todas as tasks; LA-09 encerrada; dependências consertadas. **Foco no jogo funcional com a telemetria salva local** (acréscimos do mesmo dia): saem a US-11 (exportação e pseudonimização; a T-44 vai para a US-09), a US-56 (plano de dados, LGPD), as US-57, US-58 e US-59 (piloto, SGDD e análise), o estudo da T-226, os registros no CHANGELOG (T-265, T-43, T-56, T-151, T-160) e as propostas de linha para o SGDD (T-64, T-68, T-74, T-78, T-123); o painel do mediador fica com uma aba só. **Limítrofes decididos pelo orientador** (mesmo dia): saem a US-53 (checklist do Bloco 13), a US-54 (desempenho), a T-86 (wireframes), a T-82 (proposta de CHANGELOG e SGDD), a T-227 (README, tela Sobre e SGDD), a T-263 (playtest com alguém de fora) e a T-73 (pares mostrados à equipe); ficam a US-68, a T-26 e a T-198. **Dispositivo-alvo: o PC** (Windows, mouse e teclado, decisão do mesmo dia, “pra simplificar”): L-ABD-01 decidida; saem os três toques no canto do painel do mediador (T-105, T-280, US-70) e a visibilidade do SAIR por plataforma (T-327, US-71); “dispositivo-alvo” vira o PC e “toque” vira “clique”. Os IDs cortados não voltam. |
| 9 | 26/11/2026 | **Corte de protótipo** (decisão do orientador): o Core passa a ser o protótipo jogável de ponta a ponta (US-01, 06, 07, 08, 09, 10, 12, 14, 20, 28, 33, 34, 35, 36, 47, 49, 70, 71 e o marco US-60), com a telemetria local, o save, o menu, a pausa e o fim de sessão e a build de PC; as US-16, 17, 18, 21, 22, 23, 25, 26, 31, 42, 44, 45, 50 e 69 vão para a Expansão, com as tasks (a EP-08 inteira, e o Core usa os parâmetros fixos do código); a T-124 vai da US-28 para a US-22. **LA-10 e LA-11 decididas pelo código do Gabriel** (sequência fixa de 10 peças por obra, sem 0°): saem a US-13, a US-66, a US-72 e as propostas presas (T-17, T-268, T-66, T-190); sai a US-19 (as 7 telas do código ficam); LA-12 fora do backlog. **Sai a burocracia:** o teste-robô e a suíte listada e rodada nos marcos (T-259, T-260, T-319, T-320, T-329, T-330), as conferências no Play anotadas na issue (T-102, T-219, T-261, T-321), a medição do contraste (T-70), o teste e o registro de licença (T-174; as T-137, T-172, T-173, T-211 e T-298 ficam sem o registro) e a volta com o ABD na mão (T-262, seção 12). Os marcos passam a ser jogar o build de PC do menu ao fim e conferir o arquivo da sessão. Os IDs cortados não voltam. |
| 10 | 26/11/2026 | **Corte de protótipo, correção do orientador:** a mecânica fica no Core, e o que sai do Core são os tutoriais (“pode ficar só o jogo direto sem precisar de tutorial”). Voltam para o Core, com as tasks, a US-25 e a US-26 (streak e pontos) e a US-50 (alvos e layout no PC); a T-97 vai da US-22 para a US-28, no Core, para a telemetria do Core gravar o `feedback_shown`. O tutorial fica na Expansão (US-45, US-69, US-65), e o jogo começa direto: a T-108 leva o LOAD_GAME à obra atual, e o campo `reviewTutorialButton` sai da T-107 para a T-224. A T-124, que repetia a T-276, saiu (a T-276 ficou com a revisão dela). As US-18, US-23 e US-31 continuam na Expansão até o orientador decidir se são mecânica. |
| 11 | 26/11/2026 | Reescrito no molde do Caça às Cores (26/11/2026), por decisão do orientador: 25 histórias (eram 57) e 105 tasks (eram 240, mais 2 chores), uma história por coisa que se faz ou vê, uma task por pedaço de trabalho, sem o par `[agente]`/`[aluno]` e com o teste dentro da task da regra; IDs novos `US-EE.N` e `T-EE.N.M`; o escopo da versão 10 não muda, e o mapa do antigo para o novo está em `mapa-molde-caca.md`. |
| 12 | 26/11/2026 | Realinhado com a develop de 26/11/2026 (PR #431, telemetria local em JSON, e PR #432, documentação do repositório): o ABD segue o formato e os nomes do arquivo do Gabriel; 14 tasks do Core já feitas (eram 9) e 59 h de Core (eram 69 h); entram fechadas a T-02.1.4, a T-02.2.2, a T-02.3.2, a T-02.3.3 e a T-02.3.4, a T-02.3.1 fica só com os campos do §11.4 que faltam e a T-02.2.3 só com os estados que faltam; as linhas citadas do `TrialController` e do `GameApplication` foram refeitas no código de hoje. O mapa da v11 para a v12 está na seção 6 de `mapa-molde-caca.md`. |
| 13 | 26/11/2026 | Decisões do Gabriel no PR #432, a pedido do orientador ("siga as decisoes dele"): a pausa fica como no código (a tentativa fecha `completed`, com o tempo parado no `paused_duration_ms`, fora do RT), sem os estados `paused` e `abandoned` (o abandono e a perda de foco fecham `interrupted`), e a T-02.2.3 entra fechada; o `participant_code` sai do arquivo, e o código da criança fica no save e no menu (LG-Gab-04); a T-02.3.1 encolhe à posição da peça e aos instantes de início e de fim; os campos do §11.4 que se derivam vão para uma visão de pesquisa derivada, sem campo novo no arquivo, numa task nova da Expansão (T-09.2.3); a plataforma, o dispositivo e a acessibilidade na sessão saem com o plano de dados que o Gabriel adiou; a pendência de rastreabilidade dele fica na seção 8, sem task. 15 tasks do Core já feitas e 57 h de Core (eram 14 e 59 h na v12). O mapa da v12 para a v13 está na seção 8 de `mapa-molde-caca.md`. |
