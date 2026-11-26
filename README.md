# Meu Pequeno Reino

Projeto Unity **6000.3.25f1**, com URP 17.3.0, Input System 1.20.0 e uGUI.

Abra **Assets/Scenes/Kingdom.unity** para executar o jogo. A cena **Village** é carregada adicionalmente pela aplicação.

- [Guia de edição](Documentation/EditingGuide.md)
- [Relatório da migração](Documentation/MigrationReport.md)
- [Evidências de validação](Documentation/Validation)
- [Auditoria de pendências](Documentation/Pendencias.md)
- [Telemetria local dos trials](Documentation/TrialTelemetry.md)
- [Integração com GitHub](Documentation/GitHubIntegration.md)
- [Proveniência e publicação da arte](Documentation/ThirdPartyAssets.md)

A vila usa Grid isométrico e Tilemaps gravados na cena. Interfaces, construções e personagens estão em Assets/Prefabs. Definições de conteúdo e modelos estão em Assets/Data; arte em Assets/Art.

A migração foi integrada à `develop` pelo [PR #2](https://github.com/ciatec-org/ciatec-game-my-little-kingdom/pull/2).
O [PR #431](https://github.com/ciatec-org/ciatec-game-my-little-kingdom/pull/431) incorporou a telemetria
local JSON 1.0, com um arquivo por sessão e todas as respostas aceitas em ordem. A documentação de telemetria
registra o formato, as verificações técnicas e os limites de temporização ainda sujeitos a validação científica.

O relatório de migração registra o backup local, os metadados originais e o manifesto SHA-256.
Consulte a documentação de proveniência para as autorizações relativas aos assets.
