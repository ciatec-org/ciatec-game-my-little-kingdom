# Proveniência, créditos e publicação de recursos

Inspeção em 03/11/2026. Este documento registra evidências disponíveis; não concede direitos sobre material de terceiros nem substitui os termos de cada titular.

## Situação para publicação

O repositório oficial é público. A autorização para usar um asset em um jogo e a autorização para distribuir seu arquivo-fonte publicamente precisam ser verificadas separadamente. Crédito no menu, gratuidade e presença antiga no projeto não são comprovação suficiente.

O responsável informou nesta tarefa possuir autorização específica para Mana Seed. Até que seu escopo/referência seja informado, a publicação dos arquivos correspondentes permanece pendente. Não foi presumido que ela cubra os demais conjuntos. Correspondência privada, comprovantes de compra ou dados pessoais não devem ser anexados a um commit público.

| Conjunto | Arquivos/derivados | Origem conhecida | Evidência local / estado |
|---|---|---|---|
| Mana Seed Character Base Demo 2.0 | `Assets/Art/Converted/Characters/PortraitParts.png`, `Characters/Parts/*.png`, retratos/capturas do jogo | Seliel the Shaper; identificação explícita no código anterior e crédito preservado no menu | Autor identificado; autorização específica declarada pelo responsável, escopo ainda não registrado |
| Animais | `Assets/Art/Original/Animals/*.png`, `Art/Converted/Animals/*.png` | Não identificada com segurança | 15 PNGs originais preservados; falta fonte e licença de redistribuição |
| Terreno e atlas Structures | `Assets/Art/Original/Terrain/*.png`, derivados `Art/Converted/Tiles/**`, decorações correspondentes | Não identificada com segurança | Nomes de arquivos e semelhança visual não demonstram autoria/licença |
| Árvores | `Assets/Art/Converted/Nature/Trees.png`, recortes `Tree_*.png` | Atlas antes incorporado ao código como `TreesPng`, comentário `Tiles2.png` | Cadeia técnica identificada; autor/licença não identificados |
| Salões, prédios, cercas e aldeões simplificados | `Assets/Art/Converted/UI/**`, `Buildings/**`, `Characters/Villager_*.png` e segmentos de cerca | Convertidos dos rasterizadores do protótipo local | A conversão em PNG não determina os direitos do desenho nem dos recursos que o originaram; confirmar autoria e eventuais dependências |
| Liberation Sans | `Assets/TextMesh Pro/Fonts/LiberationSans.ttf`, assets SDF e materiais | Google/Red Hat, conforme aviso local | `LiberationSans - OFL.txt` inclui SIL OFL 1.1; preservar avisos e licença, sem relicenciar a fonte como código do projeto |
| EmojiOne de exemplo | `Assets/TextMesh Pro/Sprites/EmojiOne.png`, JSON e sprite asset | EmojiOne, conforme atribuição local do pacote de exemplos | `EmojiOne Attribution.txt` foi preservado; o aviso remete ao site, sem identificar a versão completa dos termos. Confirmar licença aplicável à versão incluída |
| Shaders/recursos TextMesh Pro | `Assets/TextMesh Pro/Shaders/**`, resources e configurações | Recursos essenciais importados da Unity/TMP | Preservados como parte do conjunto local; verificar termos da distribuição correspondente antes de declarar licença global |
| Template remoto URP | `Documentation/Integration/RemoteTemplate/**` na cópia de integração | Histórico oficial do repositório em `18a0b607...` | Preservado para rastreabilidade, fora da importação ativa. Publicação anterior não é licença geral para todos os demais recursos |
| Modelos de estímulos e código do projeto | `Assets/Data/Models/**`, `Assets/Scripts/**`, materiais próprios | Implementação e dados do projeto local | Não foi adicionada uma licença aberta presumida. Direitos e licença do código devem ser definidos pelo responsável/instituição |

## Fontes consultadas

- [Mana Seed User License — site da autora](https://selieltheshaper.weebly.com/user-license.html).
- [Character Base — página oficial da autora](https://seliel-the-shaper.itch.io/character-base).
- A licença OFL e a atribuição EmojiOne acompanham os arquivos locais nos caminhos citados.

Os termos atuais de Mana Seed restringem redistribuição, inclusive de derivados, e mencionam projetos com conteúdo produzido por IA. Uma eventual autorização particular precisa esclarecer essas condições para este projeto. Esta é uma constatação do texto consultado, não uma conclusão jurídica sobre contratos anteriores ou exceções não apresentadas.

Uma busca por nomes encontrou um [índice RPG Palace/Inquisitor](https://www.rpg-palace.com/inquisitor/index.php) com `Structures.png` e `Structures2.png`. É apenas uma pista de investigação: não foi feita correspondência criptográfica com os arquivos locais nem verificada licença aplicável. Não se atribuiu autoria nem permissão com base nessa coincidência.

## Critério para liberar a publicação completa

Registrar para cada conjunto: titular, URL/fonte, versão/pacote, licença aplicável, possibilidade de redistribuir fontes e derivados, atribuição exigida e referência não confidencial de exceções específicas. A confirmação deve abranger também screenshots que contenham os recursos.

Enquanto houver dúvida material, manter os arquivos localmente e não executar push do commit que os contém. Retirar imagens apenas da última revisão não elimina sua presença no histórico de um commit anterior; a revisão deve ocorrer antes de publicar qualquer um desses commits.
