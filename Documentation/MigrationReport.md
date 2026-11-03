# Relatório de implementação — Meu Pequeno Reino

Data: 3 de novembro de 2026. Unity **6000.3.25f1**, revisão **e1dba0a9aba4**. URP **17.3.0**.

A implementação foi migrada para cenas, prefabs, ScriptableObjects, malhas e Tilemaps nativos. Os 50 modelos e as cinco sequências foram preservados. Não foram adicionadas mecânicas, persistência entre sessões ou instrumentos de pesquisa.

## A. Arquitetura final

A cena **Kingdom** contém a composição da aplicação, UI e EventSystem. **Village** contém o mundo editável e é carregada adicionalmente uma única vez. As transições alternam telas e visibilidade da vila; não recriam toda a interface e o mapa a cada retorno.

| Área | Componentes principais |
|---|---|
| Composição | GameApplication, GameUI |
| Estado e progressão | GameSession, KingdomDefinition, BuildingDefinition |
| Desafios | TrialController, TrialView, TrialModel, ModelPresentation |
| Tutorial | TutorialController, TutorialView |
| Vila | VillageWorld, VillageBuilding, VillageNavigation, VillageCameraDrag, DepthSorter |
| Personagens | CharacterPortrait, PortraitLibrary, VillageWalker, VillageAnimal, AnimalAnimation |
| Observatório | ObservatoryView, CelestialTarget |
| Ferramentas de editor | Validação estrutural, verificações de Play Mode, Inspector da sessão e geração explícita de malhas |

GameSession contém apenas o estado mutável da sessão; as definições compartilhadas não são modificadas pelo gameplay. As dependências das construções são referências entre assets. O controle da vila aponta para navegação, construções e personagens; construções e aldeões não dependem de VillageWorld para consultar seu estado e comportamento.

Diretórios finais:

- Assets/Art/Original: recursos originais preservados e reorganizados.
- Assets/Art/Converted: PNGs, recortes e arte permanente convertida da implementação anterior.
- Assets/Data: reino, cinco construções, 50 modelos, biblioteca de retratos e três animações de animais.
- Assets/Materials: material dos modelos, material de sprites e shader de apresentação dos policubos.
- Assets/Prefabs/UI e Assets/Prefabs/Village: componentes e telas preparadas.
- Assets/Scenes: Kingdom e Village.
- Assets/Scripts: Core, Gameplay, Trials, Village, Characters, UI, Tutorial, Observatory e Editor.
- Assets/Tiles: 57 assets Tile e a paleta Kingdom.
- Assets/Settings e Assets/TextMesh Pro: configurações e recursos anteriores mantidos.

## B. Scripts

Foram criados **30 arquivos C#** na estrutura final, sendo 25 de runtime e cinco de editor.

| Pasta | Arquivos |
|---|---|
| Core | GameApplication.cs; GameSession.cs; KingdomDefinition.cs |
| Gameplay | BuildingDefinition.cs |
| Trials | TrialController.cs; TrialModel.cs; ModelPresentation.cs |
| Tutorial | TutorialController.cs |
| UI | GameUI.cs; MenuView.cs; TutorialView.cs; TrialView.cs; VillageHUD.cs |
| Observatory | ObservatoryView.cs; CelestialTarget.cs |
| Village | VillageWorld.cs; VillageBuilding.cs; VillageNavigation.cs; VillageCameraDrag.cs; DepthSorter.cs |
| Characters | PortraitLibrary.cs; CharacterPortrait.cs; VillageWalker.cs; AnimalAnimation.cs; VillageAnimal.cs |
| Editor | KingdomValidation.cs; PlayModeFlowChecks.cs; VillageInteractionChecks.cs; TrialMeshBaker.cs; GameApplicationInspector.cs |

**Responsabilidades:** GameApplication compõe e navega; GameSession aplica a progressão; os controllers de tutorial e desafios manipulam componentes prontos; VillageNavigation lê Tilemaps; VillageWalker e VillageAnimal executam comportamentos; DepthSorter trata profundidade; ModelPresentation exibe malhas já gravadas.

**Modificados durante a migração:** o bootstrap anterior recebeu isolamento temporário para testar a nova cena. Foi aposentado após a integração. Os scripts novos foram revisados, compilados e formatados com Roslyn, conferindo igualdade dos tokens na etapa de formatação.

**Renomeações:** a arte original foi movida com seus metadados para Art/Original. As classes anteriores foram substituídas por componentes com novas responsabilidades, em vez de uma renomeação indiscriminada.

**Scripts anteriores retirados de Assets:** GameManager.cs; ReinoVillageView.cs; MeuPequenoReinoDemoBootstrap.cs; MeuPequenoReinoDemoController.cs; DemoModels.cs; DemoDataFactory.cs; DemoUIRoot.cs; DemoUIFactory.cs; DemoTutorialController.cs; DemoTrialController.cs; DemoVillageController.cs; DemoObservatoryController.cs; DemoBuildingRenderer.cs; ReinoTextureImporter.cs. Seus arquivos e metadados estão preservados no backup e no arquivo de aposentados.

As ferramentas pontuais usadas para converter layouts e rasterizar a arte também foram retiradas de Assets após gerar e conferir os substitutos. Permanecem arquivadas para auditoria. A geração usada em runtime foi eliminada.

## C. Prefabs

Foram criados **156 prefabs** em Assets/Prefabs:

| Grupo | Quantidade | Função |
|---|---:|---|
| UI | 11 | Menu, TutorialRotation, TutorialReflection, Trial, Observatory, GameUI, Button, Panel, Feedback, CharacterPortrait e ModelPresentation |
| Construções | 6 | Castle, Observatorio, Museu, Ferraria, Ponte e Moinho |
| Aldeões | 4 | Variações das cores existentes |
| Animais | 3 | Texugo, cervo e javali |
| Árvores e troncos | 8 | Recortes do atlas original |
| Decorações | 16 | Pedras e madeira existentes |
| Cerca | 108 | Segmentos rasterizados com as orientações do contorno original |

As seis construções estão posicionadas como instâncias de prefabs na cena. A vila possui 14 aldeões e 60 animais configurados. Os objetos visuais dos prefabs já existem; não dependem de uma fábrica de UI ou de rasterização para serem construídos em runtime.

A paleta Kingdom é outro prefab, em Assets/Tiles/Palettes, configurado com o Grid isométrico para uso pela Tile Palette.

## D. Assets convertidos

Foram produzidos **616 PNGs** em Art/Converted, incluindo:

- Dois fundos do salão, preservando os desenhos anteriores.
- Castelo e cinco construções, rasterizados uma vez durante a conversão.
- Atlas de partes de retrato e 343 recortes de animação (49 linhas x sete quadros).
- Atlas de árvores e oito recortes.
- 108 segmentos da cerca.
- Tiles ajustados à mesma superfície/projeção da implementação anterior.
- 87 quadros de animais e quatro sprites de aldeões.

Os 50 TrialModel possuem **100 malhas persistentes**, referência e reflexão, armazenadas como subassets. Foram preservadas as coordenadas e a geometria das faces externas. Os ângulos, eixos, condições e ordem permanecem nos assets das construções, incluindo os valores originais de 45°, 90°, 135°, 180° e 225°, sem recalibração.

Os **132 PNGs originais** foram conferidos por SHA-256, sem divergências. Os GUIDs de **136 metadados** da arte original também foram comparados com o backup, sem divergências. A atribuição de personagens a Mana Seed/Seliel the Shaper foi mantida na tela inicial.

## E. Tilemap

O layout Isometric foi escolhido porque o terreno é coplanar; a altura aparente vem dos sprites, e a oclusão é controlada pelos SortingGroups.

A cena Village contém um **Grid nativo Isometric**, com célula (1, 0.5, 1), e Tilemaps Terrain, Paths, Water, Ground Vegetation, Flowers e Navigation Obstacles. O terreno está gravado na cena e pode ser pintado/apagado pela Tile Palette. Não é preenchido integralmente por scripts ao iniciar o jogo.

A distribuição usa a mesma base do cenário anterior: posições das construções, malha de caminhos, rio, lagoa, cerca circular, abertura da ponte, agrupamentos de árvores e clareiras. O mapa tem limites fixos equivalentes a 4200 x 2600 unidades da UI anterior.

Sorting Layers: **Ground**, **World** e **Labels**. As construções, personagens e decorações usam SpriteRenderers e SortingGroups; DepthSorter ordena pela base do objeto. Pivots foram preparados para manter o apoio dos sprites no chão. O layer **TrialModels**, número 31, isola as câmeras dos modelos cognitivos.

VillageNavigation consulta os Tilemaps de caminhos, água e obstáculos, além dos prédios e da condição da ponte. A busca de rotas usa vizinhança ortogonal e suavização de curvas. O moinho só é acessível depois da ponte. Entradas e portas são filhos configuráveis dos prédios; mover uma construção move esses pontos.

DepthSorter também atualiza a ordenação no editor ao mover objetos.

A câmera é ortográfica e usa arraste pelo Input System, com limites e bloqueio durante a apresentação da construção. Não utiliza ScrollRect nem adiciona zoom.

Veja EditingGuide.md para pintura, edição de caminhos, movimentação de construções e troca de sprites.

## F. Interfaces

As telas são objetos de prefabs com uGUI, imagens, textos, botões, retratos e apresentadores de modelos configurados. Os controladores atualizam referências serializadas, visibilidade, conteúdo e estados dos controles.

A referência 1600 x 900, a paleta medieval, os textos, a disposição principal e os personagens foram conservados. O HUD da vila fica ancorado às bordas. O observatório preserva os objetos e a interação por hover/clique, agora com CelestialTarget em vez da reconstrução de EventTriggers.

Os modelos são MeshRenderers reais exibidos em câmeras ortográficas e RenderTextures reutilizadas. O material mantém a apresentação azul acinzentada e o sombreamento direcional; a renderização nativa usa profundidade, substituindo a ordenação de faces em um gráfico de Canvas.

Capturas reais estão em Documentation/Validation: menu.png, tutorial.png, trial.png, observatory.png, model-reference.png, model-candidate.png, village-initial.png e village-complete.png.

## G. Problemas herdados corrigidos

- Referência sem script do antigo DemoBootstrap eliminada junto da cena e do bootstrap anteriores.
- Inicialização substituída por composição serializada da cena, sem criação automática global de toda a aplicação.
- Interfaces e vila deixam de ser destruídas e reconstruídas a cada transição.
- Arte estática e atlas deixam de estar incorporados em Base64 ou rasterizados durante o gameplay.
- mapPosition e parâmetros sem efeito deixam de integrar o estado novo.
- Renderizadores duplicados e painéis anteriores foram retirados.
- Expansão parcial do cenário foi substituída por mapa finito editável.
- Regras de desbloqueio deixam de depender de índices espalhados pelos controladores.
- A navegação da câmera deixa de depender de ScrollRect.
- A geometria é persistente, com câmera, material e profundidade nativos.
- O importador que agendava reparos automaticamente ao abrir o editor foi aposentado.

## H. Código removido

O código anterior foi retirado somente depois de os substitutos passarem pela validação estrutural e pelo ciclo de Play Mode. Isso inclui o rasterizador de runtime, o gráfico personalizado do chão, a fábrica de UI, o renderer anterior de prédios, o gerenciador de quatro painéis e o bootstrap global.

Os recursos originais de uso incerto foram preservados em Art/Original. Assets de terceiros e configurações existentes não foram removidos apenas por estarem sem uso no fluxo atual.

## I. Validações

Os executores ativam temporariamente a execução em segundo plano para os testes automatizados e restauram a configuração original ao sair do Play Mode. A configuração entregue permanece igual à anterior nesse ponto.

A validação ocorreu com a **mesma Unity 6000.3.25f1**, em uma cópia isolada do projeto, preservando a sessão gráfica aberta pelo usuário. A comparação final confirmou igualdade byte a byte dos **2.246 arquivos de Assets** e das configurações relevantes com a cópia testada. Não houve atualização de Unity, manifest.json ou packages-lock.json.

### Executadas e aprovadas

- Compilação dos scripts finais pela Unity.
- Validação estrutural: **790 verificações semânticas e 59.906 verificações de referências/scripts serializados**.
- Comparação das 50 coordenadas de modelos, condições, ângulos, eixos, ordem e metas com o contrato original.
- Comparação da geometria das 100 malhas e das faces externas.
- Verificação de ausência de Missing Scripts e referências pendentes nos prefabs e cenas do jogo.
- Grid isométrico, Tilemaps preenchidos, instâncias de construções e conectividade antes/depois da ponte.
- Ciclo real em Play Mode: menu, tutorial com rotação, reflexão e retorno, erro/repetição, 50 estímulos, recompensas, construções, pausa, retomada, observatório durante pausa, ponte, moinho, conclusão e reinício.
- Renderização real dos modelos, incluindo transparência do fundo.
- Mouse virtual do Input System: arraste, bloqueio da câmera, clique em placa de construção e clique no HUD para iniciar o desafio.
- Remoção/restauração de tile mudando a navegação, deslocamento da entrada junto do prédio, caminhada dos aldeões, movimentação e quadros de animação dos animais.
- Inspeção das capturas reais das interfaces e da vila.
- Integridade dos PNGs originais, GUIDs da arte e versões das dependências.

### Falhas encontradas e corrigidas durante a execução

A primeira compilação da ferramenta temporária usava uma sobrecarga de File.Move indisponível na API da Unity. A conversão inicial de construções tentou ler uma textura cujo buffer de CPU havia sido descartado. A montagem em batch também precisou tratar a cena inicial não salva. Esses pontos foram corrigidos nas ferramentas de preparação; os artefatos foram gerados novamente e validados.

Uma repetição do teste de mouse encontrou um alvo de clique na borda do botão: o executor usava o pivô em vez do centro visual do RectTransform. O executor foi corrigido para clicar no centro, preservando o botão e o gameplay.

A primeira execução automatizada de Play Mode não avançou o loop do editor em background. O driver foi ajustado para reinstalar seus callbacks, solicitar o player loop e registrar seu avanço. As execuções seguintes completaram o ciclo.

### Limitações e ocorrência do ambiente

A automação da janela gráfica retornou timeout/indisponibilidade de geometria. Assim, os testes não foram feitos operando manualmente a janela que estava aberta. Foram executados pela Unity em batch, incluindo uma execução com dispositivo gráfico e eventos do Input System.

A Unity registrou uma exceção de indexação em **UnityEditor.Search.SearchDatabase** no início das execuções gráficas em batch. Ela pertence ao editor; os testes e as capturas continuaram até o fim. Não houve erro de compilação ou exceção dos novos controladores nos ciclos finais verificados. Não se declara o Console da sessão gráfica do usuário revisado ou limpo.

Não foram executados build de distribuição, benchmark em hardware-alvo, avaliação científica ou validação de todas as proporções de tela/dispositivos de entrada físicos. A formatação final dos fontes foi conferida como mudança apenas de whitespace na etapa correspondente.

Evidências: static-validation.txt, play-validation.txt, interaction-validation.txt, integrity-audit.json, original-guids.json e tested-project-equivalence.json, em Documentation/Validation. Os logs completos das execuções ficam em Migration.

## J. Pendências e cuidados

**Concluído:** migração estrutural, arte permanente, malhas, prefabs, Tilemap serializado, integração da progressão, remoção do legado, verificações funcionais e documentação de edição.

**Sem implementação parcial substituindo o fluxo:** o jogo entregue utiliza os componentes novos; o protótipo não é necessário para inicializá-lo.

**Revisão manual recomendada:** abrir Kingdom na sessão gráfica principal, experimentar o jogo no ambiente de uso e conferir legibilidade/escalonamento nas resoluções pretendidas. O comportamento por mouse foi verificado com dispositivo virtual do Input System; não se afirma teste de hardware físico ou gamepad.

**Fora do escopo e mantido assim:** salvamento entre sessões, exportação de dados de pesquisa, adaptação de dificuldade, conteúdo adicional e calibração cognitiva. Os logs de resposta continuam com a semântica anterior, incluindo nova contagem de tempo após erro.

A cópia temporária de validação e seus caches foram removidos após a coleta das evidências.

**Recuperação:** after-migration.zip preserva a entrega, incluindo documentação e metadados. before-migration.zip contém os 437 arquivos originais, conferidos por SHA-256, sem caches. A pasta retired preserva os arquivos aposentados e as ferramentas pontuais. Não houve criação de repositório, commit, push, merge ou alteração de branch.
