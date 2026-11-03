# Execuções da auditoria e da integração

Data: 03/11/2026. Unity executada: **6000.3.25f1 (e1dba0a9aba4)**. Não se alterou a sessão gráfica original.

## Resultados desta tarefa

| Verificação | Resultado | Evidência nesta pasta |
|---|---|---|
| Integridade do snapshot na inspeção inicial | Nenhum arquivo do manifesto anterior divergia; SampleScene e seu meta eram acréscimos posteriores. README/documentação foram atualizados depois, nesta tarefa | inventory.json |
| GUIDs duplicados / meta faltante de arquivo | Nenhum nos Assets locais | inventory.json |
| Resolução de scripts por Assets e pacotes | Três GUIDs ausentes, exclusivamente na SampleScene adicional | script-guid-resolution.json |
| Região exata dos retratos PNG | 343/343 recortes iguais ao atlas, sem divergência | portrait-crops.json |
| Validação estrutural Unity, executada novamente | PASS: 790 verificações semânticas e 59.906 de referências/scripts serializados das cenas/prefabs do jogo | static-validation.txt |
| Inspeção adicional de SampleScene em preview | Três componentes com Missing Script: GameManager, DemoBootstrap e Art conversion | audit-structure.txt |
| Sequências e composição | Cinco sequências de dez modelos fixos; uma Tilemap na paleta; três camadas por retrato e 49 linhas | audit-structure.txt |
| Ciclo completo em Play Mode na integração | PASS: tutorial/retorno, erro, 50 estímulos, pausa/retomada, observatório, ponte/moinho, cinco construções, reinício e segunda sessão | play-validation.txt |
| Mouse virtual e interação na integração | PASS: arraste/bloqueio, pintura lógica de caminho, entrada movida, caminhada/animação, placa e HUD | interaction-validation.txt |
| Capturas de chegada | Produzidas durante Play Mode real e inspecionadas | audit-arrival-start.png, audit-arrival-middle.png, audit-arrival-finished.png, audit-observations.txt |
| Amostras de layout | Renderizadas/inspecionadas em 1280×960 e 800×1280 | audit-trial-4x3.png, audit-trial-portrait.png |
| Equivalência da integração final com a fonte | PASS: 2.274 arquivos comparados, sendo 2.246 em Assets; nenhuma diferença ou arquivo ativo extra | integration-equivalence.json |
| Campos do relatório | 24 itens P01–P24; todos os campos requeridos presentes | findings-index.json |

### Conferência Git após o primeiro commit local

O commit de integração `3f3d6396c2ac79ee970fc251ab4bfd1e9cf368ff` tem como pai direto o commit original de develop `18a0b6077d126ebd9a1e963e914320b01387b9ce`. O teste de ancestralidade passou; o checkout estava limpo. `git lfs fsck --objects --pointers` retornou **Git LFS fsck OK**. O inventário JSON do LFS relacionou **764 arquivos**, incluindo todos os **763 PNGs** e a fonte; os PNGs incluem 132 originais e 616 convertidos. O commit contém 2.360 arquivos rastreados e nenhum cache, ZIP de backup ou asset de observação temporária.

O blob de `.gitattributes` continua `5eb96286670e75fea831ee29bb21a48473f7fc6e`, igual ao remoto. Não há arquivo selecionado de 50 MB ou mais nem correspondência aos padrões de credenciais pesquisados. Os fontes C# e a documentação nova passaram na verificação de whitespace reconhecendo CRLF; o YAML gerado pela Unity conserva os espaços dos campos vazios, sem reformatar assets nesta tarefa. `git-preflight.json` registra o ponto imediatamente anterior à inclusão do próprio arquivo de preflight.

Push, upload LFS e criação do PR **não foram executados**: a autorização/proveniência da arte está pendente de esclarecimento. Autenticação de escrita e quota do servidor não foram testadas nem apresentadas como impedimentos comprovados. A minuta do PR está pronta localmente. Um commit documental posterior registra este resultado; seu hash final é informado no recibo local e na entrega ao responsável.

As comparações de arquivo usam bytes/SHA-256. A cena local adicional é preservada na documentação da integração, fora de Assets; não é incluída na alegação de ausência de Missing Scripts das cenas do jogo.

## Como os testes foram feitos

Os executores permanentes `KingdomValidation`, `PlayModeFlowChecks` e `VillageInteractionChecks` foram usados no clone. Um observador temporário, somente de editor, coletou a cena adicional, as sequências e as capturas de chegada/layout. Seus arquivos foram removidos dos Assets antes da preparação do commit. A fonte desse observador permanece somente no material local de trabalho `.integration/tools/AuditChecks.cs`; ela não é uma dependência do jogo.

Os testes habilitam execução em segundo plano e restauram a configuração ao sair. A configuração final continua `runInBackground: 0`. Não foram alterados modelos, ordem, condições, timers ou feedback do runtime para executar a auditoria.

As capturas de UI usam câmera offscreen e reposicionamento temporário/restaurado do Canvas. As imagens 4:3 e retrato demonstram a escala uniforme e as margens livres, mas não validam safe areas, DPI, toque físico, Game View em todas as configurações ou compreensão infantil. A captura `arrival-finished` ocorreu durante o feedback do primeiro erro automático: seus botões desabilitados e texto de correção são esperados nesse instante.

## Ocorrências e limites

1. A primeira tentativa de Unity dentro do sandbox não alcançou o serviço de licença; foi interrompida. A execução com acesso ao serviço local prosseguiu.
2. O caminho inicial do clone em `.integration` provocou aviso de nome de diretório. Um caminho longo posterior produziu `DirectoryNotFoundException` no importador de um UXML do pacote 2D Tooling. Os testes funcionais ainda completaram, mas isso não foi ignorado como importação limpa.
3. O clone foi renomeado para o caminho curto `Integration/Kingdom`. A nova execução de estrutura e mouse passou sem repetir a falha de importação do UXML.
4. A Unity voltou a registrar `ArgumentOutOfRangeException` no indexador `UnityEditor.Search.SearchDatabase`. Isso também havia ocorrido na migração; não pertence aos novos controladores e não impediu os testes, mas impede afirmar “Console sem erros”. Os logs brutos ficam em `.integration/audit-unity-final.log` e `.integration/audit-input-final.log`, somente localmente.
5. Nenhum build de distribuição ou benchmark foi executado. Não houve revisão do Console da janela original, operação manual com criança, dispositivos físicos, revisão de literatura ou validação científica.
6. P20 e P21 foram identificados por caminhos de código, não por um ensaio visual dedicado. P01 continua sendo avaliação de apresentação; ausência de falha de renderização não comprova qualidade perceptual.

## Evidência histórica

`Documentation/Validation` conserva os resultados e imagens da migração anterior. Eles não foram rebatizados como testes novos. `MigrationReport.md` descreve essa etapa. A equivalência de bytes permite relacionar aquela base às novas execuções sem alegar cobertura de arquivos acrescentados posteriormente.
