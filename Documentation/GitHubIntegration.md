# Integração com o repositório oficial

Inspeção e preparação em 03/11/2026.

## Estado inicial confirmado

- Repositório público: https://github.com/ciatec-org/ciatec-game-my-little-kingdom.
- Branch padrão `develop`, protegida, em `18a0b6077d126ebd9a1e963e914320b01387b9ce`.
- `main`, protegida, em `6c3b40b03b379d8b3b4cb217cf5dae59bf5fd9b6`.
- Histórico remoto: `c394dd4` (Initial check-in) → `18a0b60` (include urp). Main contém o merge desses commits pelo PR #1; não é uma história independente a substituir.
- Sessão/pasta original sem `.git`, sem branch ou remote locais. O conjunto migrado está preservado em backups locais antes/depois da migração.
- Inspeção completa dos 68 arquivos rastreados do remoto: 34 ausentes no local, 21 diferentes e 13 iguais byte a byte no checkout inicial. A lista está em `Integration/remote-comparison.json`.
- Git 2.50.1.windows.1 e Git LFS 3.7.0 instalados. O clone já recebeu o objeto LFS do ícone URP remoto. `.gitattributes` original foi conservado, com PNGs/fontes/binários em LFS e YAML de Unity em texto.

## Estratégia aplicada

Foi clonado o remoto com histórico completo, a partir de develop, em uma pasta controlada dentro do projeto. O checkout final está em:

`D:/MeuPequenoReino/MeuPequenoReino/Integration/Kingdom`

A pasta original continua disponível, com seus caches, cena de trabalho e backups. Ela não foi convertida em repositório por cópia de um `.git` desconectado. Para trabalhar na branch, abrir o checkout indicado.

A branch `feature/kingdom-unity-migration` não existia no remoto e foi criada a partir de `origin/develop`. Não houve reset destrutivo, force push, alteração de branches protegidas ou mudança de regras.

### Preservação do conteúdo remoto

- `.gitattributes`, `.vscode/*` e `my-little-kingdom.slnx` permanecem. A solução é a existente no remoto; Unity/IDE podem regenerá-la localmente.
- O `.gitignore` remoto foi ampliado somente com exclusões das pastas locais de recuperação, integração e saídas brutas de testes.
- Os assets do template URP remoto foram examinados e movidos para `Documentation/Integration/RemoteTemplate/Assets`, com os metadados originais. Incluem SampleScene 3D, perfis/pipelines de PC/Mobile, ações de input e Readme/TutorialInfo. Não havia gameplay próprio nesses arquivos.
- `PhysicsCoreProjectSettings2D.asset` e `ProjectAuditorSettings.asset`, exclusivos do conjunto remoto mais novo, foram preservados na mesma pasta de documentação. Não ficam ativos como configuração da Unity 6000.3.
- Arquivos de configuração comuns foram substituídos pelo conjunto local coerente. Suas versões anteriores continuam no commit-base do histórico, com diferenças revisáveis no commit de integração. Não foi feito merge campo a campo de serializações de versões diferentes.
- A cena local adicional pós-migração `SampleScene.unity` contém três Missing Scripts. Na integração ela e seu `.meta` ficam em `Documentation/Integration/LocalSceneReview`, identificados como trabalho a revisar, sem importação ativa. O arquivo original fora do checkout não foi modificado.

Essa separação evita colisões de GUIDs/configurações entre os templates, preservando os arquivos para revisão. Não se apresentou o template 6000.5 como validado no editor 6000.3.

## Versão da implementação

| Componente | Remoto original | Conjunto migrado usado na branch |
|---|---|---|
| Unity | 6000.5.7f1 | 6000.3.25f1, revisão e1dba0a9aba4 |
| URP | 17.5.0 | 17.3.0 |
| uGUI | 2.5.0 | 2.0.0 |
| Input System | 1.20.0 | 1.20.0 |
| Test Framework | 1.7.0 | 1.6.0 |
| Timeline | 1.8.12 | 1.8.13 |
| AI Navigation | 2.0.14 | Ausente; o jogo usa sua grade/caminhos, sem NavMesh |
| Pacotes 2D | Não estavam no manifesto | Conjunto local de Tilemap, sprites e animação já validado |

O ProjectVersion, manifest e packages-lock locais foram mantidos byte a byte. Não se executou upgrade/downgrade do jogo. O diff do PR registra uma substituição de base de template por uma implementação validada em outra versão; isso deve ser revisado conscientemente.

## Conteúdo selecionado e segurança

Incluídos: Assets com `.meta`, Packages, ProjectSettings, README, `.editorconfig`, documentação da migração, contrato dos estímulos, auditoria e evidências selecionadas. O gameplay e a arte da implementação migrada não foram reformulados nesta tarefa.

Excluídos: Library, Temp, Logs, Obj, UserSettings, Builds, `.migration-backups`, `.integration`, Integration e saídas brutas Migration. Backups ZIP permanecem somente localmente. Ferramentas temporárias de observação não integram os scripts entregues.

A revisão contempla inventário, integridade dos metadados, referências das duas cenas principais, amostras de arquivos selecionados e busca por padrões de tokens/chaves privadas conhecidos. Não foram selecionados arquivos de participantes ou correspondência privada. Os logs e capturas de teste usam sessões sintéticas.

**Bloqueio de publicação pendente:** verificar o escopo de autorização e a origem/licença dos conjuntos artísticos conforme `ThirdPartyAssets.md` e P11 de `Pendencias.md`. Um commit local que contenha os assets não deve ser enviado até resolver essa condição. Não basta excluí-los de um commit posterior, pois permaneceriam no histórico enviado.

Não foram testadas quotas de upload do Git LFS sem necessidade de publicação. LFS local instalado/configurado não comprova autorização ou quota de escrita no servidor. O resultado efetivo de commit/push/PR é registrado no recibo local `Documentation/Integration/ResultadoLocal.md` da pasta original, após as operações.

## Validação e revisão

Consultar `Audit/Execution.md` para as novas execuções desta tarefa e `MigrationReport.md` para a evidência histórica da migração. Os testes não alteram os dados fixos dos 50 estímulos.

Uma tentativa inicial de batch não alcançou a licença dentro do sandbox. Outra tentativa revelou um limite de caminho do importador de um arquivo do pacote 2D Tooling no diretório de clone longo. O checkout foi renomeado para `Integration/Kingdom` e a execução foi repetida nesse caminho curto. O problema é registrado como ocorrência do ambiente, sem ser ocultado como gameplay corrigido.

Para inspecionar a preparação local, no checkout:

```powershell
git status --short --branch
git log --oneline --decorate -5
git diff origin/develop...HEAD --stat
git lfs ls-files
git lfs fsck
```

Depois de cumprir P11, revisar a lista completa de arquivos e executar push normal da branch. O PR deve ter `develop` como base, com o resumo de arquitetura, a decisão de versão, os testes e as limitações. O texto preparado está em `.integration/pull-request.md` da pasta original. Não fazer merge como parte desta tarefa.
