# Como abrir e editar Meu Pequeno Reino

Use **Unity 6000.3.25f1**. Abra **Assets/Scenes/Kingdom.unity** e pressione Play. Se o editor estava aberto durante a migração, aguarde a recompilação/atualização dos assets e abra essa cena; a antiga SampleScene foi aposentada e preservada no backup.

## Organização das cenas

- **Kingdom**: Game Application, EventSystem, câmera de fundo e instância de GameUI.
- **Village**: cenário real da vila. A aplicação o carrega adicionalmente uma vez e alterna sua visibilidade durante a sessão.
- A cena-template de URP existente continua em Assets/Settings/Scenes; ela não faz parte da lista de build.

## Pintar o terreno e os caminhos

1. Abra **Assets/Scenes/Village.unity** fora do Play Mode.
2. Expanda **Village World / Isometric Grid**.
3. Abra **Window > 2D > Tile Palette** e escolha a paleta **Kingdom**, em Assets/Tiles/Palettes/Kingdom.prefab.
4. Escolha o Tilemap de destino no campo Active Tilemap e pinte/apague os tiles normalmente.

Camadas:

| Tilemap | Papel |
|---|---|
| Terrain | Superfície de grama |
| Paths | Caminhos visuais e fonte da navegação dos aldeões |
| Water | Rio e lagoa; bloqueia navegação, exceto na passagem da ponte pronta |
| Ground Vegetation | Arbustos e cobertura do solo |
| Flowers | Flores e pequenas plantas |
| Navigation Obstacles | Dados de bloqueio: cerca, vegetação e posições reservadas; renderer desativado |

Os tiles estão em **Assets/Tiles**. NavigationMarker é um tile lógico sem sprite, utilizado para marcações invisíveis e para manter a conectividade dos caminhos sob a água da ponte. Não troque seu sprite por uma imagem visível: ele também é usado na passagem do rio.

A grade usa **Isometric**, célula **(1, 0.5, 1)** e Tile Anchor zero. A conversão manteve a projeção anterior: cada 96 unidades da UI antiga corresponde a uma unidade do mundo; uma célula antiga (x,y) corresponde à célula nativa (-y,-x). O cenário é finito e serializado: não há reconstrução do terreno durante o jogo.

Paths e Water afetam a navegação. Ao mudar um caminho, verifique se ainda existe conexão até Path Entrance das construções. Ao adicionar um obstáculo, mantenha Navigation Obstacles coerente. A movimentação considera também as posições das construções e a condição da ponte.

## Construções

Os prefabs ficam em **Assets/Prefabs/Village**: Castle, Observatorio, Museu, Ferraria, Ponte e Moinho.

Na cena, selecione a construção e mova seu Transform. Os filhos **Path Entrance** e **Doorway** acompanham o prédio; podem ser ajustados separadamente para alinhar o caminho e a porta. Pinte os caminhos até a nova entrada.

Para trocar a arte, abra o prefab e selecione **Completed Art / Sprite**. Altere o SpriteRenderer. Ajuste escala/pivot e confira o DepthSorter, que usa a base do objeto para ordenação. Construction Plot representa o lote antes da conclusão. Building Sign contém o botão e os textos da placa.

VillageBuilding referencia uma **BuildingDefinition**, em Assets/Data/Buildings. A definição contém nome, meta de peças, sequência de desafios, dependência e indicação de abertura do observatório. A dependência do moinho está configurada por referência ao asset da ponte. O castelo é o prédio inicial, com definição vazia.

## Interfaces

Abra os prefabs em **Assets/Prefabs/UI**. Menu, TutorialRotation, TutorialReflection, Trial e Observatory contêm seus objetos visuais já preparados. GameUI compõe essas telas e o HUD da vila.

Edite RectTransforms, imagens, fontes e cores pelo Inspector. Os scripts usam referências serializadas: ao substituir um objeto com componente, atualize o campo correspondente de MenuView, TutorialView, TrialView, ObservatoryView ou GameUI.

Button, Panel, Feedback, CharacterPortrait e ModelPresentation são prefabs reutilizáveis. O layout conserva a referência 1600 x 900. A vila usa a câmera e o HUD ancorado nas bordas.

## Personagens e animais

- CharacterPortrait usa **Assets/Data/PortraitLibrary.asset**, com as mesmas 49 linhas e sete quadros do atlas original, separados em sprites.
- Os quatro prefabs Villager preservam as cores dos aldeões; VillageWalker controla deslocamento, aproximação, entrada, saída e transparência.
- Animal_badger, Animal_stag e Animal_boar usam AnimalAnimation em Assets/Data/Animals. Texugos e cervos têm animações direcionais; javalis permanecem em repouso.
- Em cada instância de animal, home e startPosition definem habitat e início; speed e seed configuram seu comportamento. Esses dados são separados do sprite.

## Modelos e desafios

Há 50 assets em **Assets/Data/Models**. Cada TrialModel possui coordenadas originais, malha de referência, malha espelhada e enquadramento. As duas malhas são subassets reais do arquivo.

Selecione um modelo e expanda suas malhas no Project para inspecioná-las. O Inspector oferece regeneração explícita das malhas após uma edição de coordenadas. Essa ação não altera os gabaritos. As sequências, ângulos, eixos e condições ficam nos assets BuildingDefinition.

**Documentation/OriginalTrialContract.json** guarda o contrato original usado na validação da migração. Alterações de conteúdo precisam ser intencionais; o validador apontará divergências desse contrato.

ModelPresentation usa MeshFilter, MeshRenderer, material e câmera ortográfica preparados no prefab. RenderTextures são criadas para a apresentação em UI e reutilizadas; a geometria não é construída a cada desafio. O layer TrialModels isola essas câmeras do cenário.

## Validações disponíveis

No menu **Meu Pequeno Reino**:

- **Validar estrutura migrada**: compara dados, malhas, referências, prefabs, Grid, caminhos e progressão. Usa cenas de preview para preservar a cena aberta.
- **Verificar fluxo em Play Mode**: percorre tutorial, respostas, construção, pausa, observatório, conclusão e reinício.
- **Verificar interação da vila em Play Mode**: injeta eventos em um mouse virtual do Input System, confere arraste, seleção, caminhos, aldeões e animais.

Salve as cenas antes das verificações que entram em Play Mode. Elas abrem Kingdom, executam interações automaticamente e escrevem resultados em Migration. Execute a validação estrutural fora do Play Mode.

## Recuperação

O backup anterior está em **.migration-backups/20261103-103330/before-migration.zip**, com Assets, Packages, ProjectSettings e respectivos metadados. O manifesto SHA-256 acompanha o arquivo. O código aposentado e as ferramentas pontuais de conversão estão no subdiretório retired, fora de Assets.

Para restaurar integralmente a versão anterior, feche a Unity, preserve uma cópia da entrega atual e restaure as três pastas do ZIP em conjunto. Os metadados fazem parte da recuperação. Os caches da Unity não fazem parte desse backup.

O arquivo **after-migration.zip**, no mesmo diretório de backups, preserva a entrega migrada, incluindo documentação. A cópia temporária usada nos testes não é necessária para abrir ou executar o jogo.
