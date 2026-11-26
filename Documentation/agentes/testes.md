# Testes

## Validadores disponíveis

Use os validadores existentes em `Assets/Scripts/Editor/` conforme o risco da alteração. Para uma mudança
documental, a revisão do diff e a conferência das referências dos documentos costumam bastar. Para mudanças
de `.meta`, compare os GUIDs e confirme a resolução de caminhos numa importação limpa.

| Validador | Modo | Cobertura |
| --- | --- | --- |
| `KingdomValidation.Run` | Editor, fora de Play Mode | Contrato dos 50 estímulos, malhas, cenas, prefabs e referências serializadas |
| `PlayModeFlowChecks.Run` | Play Mode com renderização | Menu, tutorial, respostas, progressão, observatório, vila e reinício |
| `VillageInteractionChecks.Run` | Play Mode | Mouse sintético pelo Input System, câmera, navegação e interação na vila |
| `TrialTelemetryChecks.RunStorage` | Editor, fora de Play Mode | JSON sintético, identidade, persistência e falha de I/O controlada |
| `TrialTelemetryChecks.RunPlay` | Play Mode | Registro de tentativas, erro seguido de acerto, pausa, retomada e encerramento |

Na Unity 6000.3.25f1, o Editor aceita `-batchmode -projectPath "<cópia>" -executeMethod <método>`.
Use `-quit` para os validadores síncronos fora de Play Mode. Os validadores de Play Mode encerram o
Editor em batch após a execução. Mantenha a renderização habilitada quando o teste verificar imagens ou
modelos exibidos. Para escrever JSONs em Play Mode, isole a pasta de dados com um produto de teste na
cópia, conforme [TrialTelemetry.md](../TrialTelemetry.md).

## Entrada e tempo

- O jogo usa o **Input System 1.20.0**, com `activeInputHandler: 1`. Para navegação e interação, siga
  o exemplo executável de `VillageInteractionChecks`, que envia eventos a um mouse sintético. Para
  validar somente o fluxo de regras de um botão já ligado, `PlayModeFlowChecks` aciona `onClick.Invoke()`
  e confere o estado resultante. Registre qual tipo de entrada o teste exercitou.
- `Agentes.Entrada` encapsula o Input Manager legado e está fora do gameplay. Qualquer adoção desse
  auxiliar depende de tarefa própria, compatibilidade com o backend ativo e testes.
- O RT atual vem de `Time.realtimeSinceStartupAsDouble` em `TrialController`. Quando um teste precisar
  aguardar, use condições de estado e espera em tempo real, mantendo a lógica e a política de pausa
  do jogo. Um relógio falso pode ser útil em testes de regras com tempo controlado. Isso é um exemplo
  genérico de teste, sem dependência obrigatória de `Agentes.Relogio`. Trocar o relógio do RT exige
  definir equivalência semântica, política de pausa e validação antes da implementação.
- Remova dispositivos sintéticos e restaure dependências substituídas no teardown. A unidade de
  milissegundos no JSON descreve a gravação. Precisão temporal, latência de entrada e validade
  científica dependem de validação própria.

## Assemblies e Test Runner

A base já inclui `com.unity.test-framework`. Os validadores acima são rotinas de Editor existentes.
`Game`, `Game.Core`, `Game.Editor`, `Tests.EditMode` e `Tests.PlayMode` são os assemblies planejados
na US-01/T-01. A configuração `testables` do Input System e o uso de `InputTestFixture` pertencem a essa
tarefa quando necessária. Evite migrar assemblies ou adicionar pacotes só para executar um validador
já disponível.

Em testes isolados, crie por código o mínimo necessário. Exemplos genéricos incluem `AddComponent`
para um componente e `ScriptableObject.CreateInstance` para dados de configuração. No Test Runner,
quando aplicável, selecione EditMode ou PlayMode conforme a dependência de cenas, renderização e
player loop. Preserve os testes de integração que abrem a cena de entrada na cópia de teste.

## Cobertura observada

Registre o método, a versão da Unity, o ambiente e o resultado real. Uma verificação estática,
um percurso em Play Mode, um build executado em hardware e uma validação científica são evidências
distintas. Encaminhe para validação humana o que exigir dispositivo-alvo, revisão perceptual,
acessibilidade ou protocolo científico. Use registros sintéticos na automação e indique verificações
pendentes separadamente.
