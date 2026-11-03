# Plano inicial da migração

Registro do planejamento anterior à implementação. O resultado final está em MigrationReport.md.

## Recuperação

Antes das alterações foram arquivados Assets, Packages e ProjectSettings, incluindo os .meta, em `.migration-backups/20261103-103330/before-migration.zip`. Os 437 arquivos foram conferidos contra o SHA-256 de cada entrada. Library, Temp, Logs e UserSettings não fazem parte do backup. Não há repositório Git nesta cópia.

## Dependências e arquitetura-alvo

- A inicialização atual cria todos os controladores e a UI. Será substituída por uma raiz de cena com referências serializadas.
- Dados dos 50 policubos e das cinco sequências serão preservados em assets. Estado da sessão será separado das definições e da apresentação.
- Tutorial e desafios continuarão com as mesmas respostas, tempos de apresentação, pausas e critérios de construção.
- Recursos estáticos do salão, personagens, edifícios, árvores e cercas serão convertidos uma vez, no editor. Os originais permanecem no backup e assets cujo uso é incerto não serão excluídos.
- Interfaces serão prefabs com componentes configurados. O código de criação será restrito às ferramentas de conversão do editor.
- A vila passará a Grid isométrico e Tilemaps serializados, com construções e habitantes em prefabs, navegação lógica e câmera de arraste independentes da UI.
- Menu, tutorial, oficina e observatório podem compartilhar uma cena de aplicação. A vila terá cena própria carregada adicionalmente, preservando a sessão na aplicação.

## Sequência e critérios

1. Backup verificado e ferramentas de conversão; manter o protótipo utilizável.
2. Definições, estado de sessão e recursos visuais permanentes.
3. Prefabs de interface e apresentação dos modelos; preservar o ciclo de desafios.
4. Cena da vila, Tilemaps, prefabs, câmera e navegação.
5. Integração e verificação do fluxo completo.
6. Somente após verificar os substitutos, remover implementações obsoletas e referências antigas.

Não atualizar Unity/pacotes, modificar gabaritos, introduzir persistência ou alterar o protocolo cognitivo. Registrar separadamente inspeções estáticas, compilação e verificações em Play Mode.
