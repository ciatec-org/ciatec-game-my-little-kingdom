# Auditoria de pendências — Meu Pequeno Reino

Data da inspeção: 03/11/2026. Projeto local: Unity 6000.3.25f1, URP 17.3.0, uGUI. Esta tarefa documenta o estado existente; não corrige gameplay nem define um protocolo científico.

## A. Resumo executivo

O jogo possui um ciclo técnico de 50 estímulos em cinco construções, tutorial, vila, observatório, conclusão e reinício. A migração fornece cenas, prefabs, dados e geometria persistentes. Isso não equivale a uma experiência final de jogo nem a um instrumento de pesquisa validado.

As limitações centrais são a apresentação artística dos estímulos e personagens, a ausência de uma fila visual de visitantes, o fluxo narrativo pouco especificado e a falta de requisitos para tentativas, medidas, aleatorização e adaptação. Não há salvamento, exportação de dados de pesquisa ou sistema de áudio próprios.

A integração exige ainda resolver a autorização para publicar a arte no repositório público. A divergência entre o template remoto (Unity 6000.5.7f1/URP 17.5.0) e a implementação entregue foi tratada preservando o conjunto local validado, sem executar conversão de versão.

Dos nove relatos iniciais: **2 confirmados, 3 parcialmente confirmados, 1 não confirmado, 1 subjetivo e 2 não implementados**. São categorias de verificação, não uma contagem de bugs. Há **15 pendências adicionais, P10–P24**, incluindo limitações técnicas e funcionalidades futuras.

### Evidências e alcance

- Código: os 30 scripts em `Assets/Scripts`, material/shader dos policubos e definições em `Assets/Data`.
- Arte: PNGs, importadores `.meta`, 343 quadros de retratos, prefabs e capturas reais em `Documentation/Validation`.
- Mundo: `Assets/Scenes/Village.unity`, `Assets/Tiles`, paleta Kingdom, construções e navegação.
- Integridade atual: `Audit/inventory.json`, `Audit/script-guid-resolution.json` e `Audit/portrait-crops.json`, relativos a esta pasta Documentation.
- Evidências anteriores de Play Mode: `Validation/play-validation.txt`, `Validation/interaction-validation.txt` e `Validation/static-validation.txt`. Foram produzidas na migração e são identificadas como tal; a seção G distingue as novas execuções.
- Uma `Assets/Scenes/SampleScene.unity` local foi salva depois da entrega migrada. Todos os arquivos do snapshot final da migração continuam idênticos; a cena e seu `.meta` são acréscimos. Sua existência não invalida retroativamente os testes anteriores, mas limita sua abrangência.

## B. Verificação das nove observações

| ID | Observação | Resultado da verificação | Prioridade | Evidência principal |
|---|---|---|---|---|
| P01 | Aparência dos objetos 3D | Subjetivo | P1 | Shader unlit, material único azul, câmeras ortográficas e capturas reais dos modelos |
| P02 | Tratamento dos erros | Confirmado | P1 | `TrialController.Answer/Resolve`: repete o estímulo e reinicia o tempo sem contar tentativas |
| P03 | Organização dos Tilemaps | Parcialmente confirmado | P2 | Seis camadas no mundo; uma única camada genérica na paleta; nomes numéricos |
| P04 | Posição dos sprites de caminhada | Parcialmente confirmado | P1 | Retrato com translação/escala fixas; 343 recortes idênticos ao atlas; aldeão do mundo usa sprite estático |
| P05 | Fila de aldeões incompleta | Não implementado | P2 | Existe um retrato por estímulo, sem coleção/fila visual de visitantes |
| P06 | Tela ainda depende de rasterização em runtime | Não confirmado | P3 | Fundos e retratos são PNGs; uGUI simples e RenderTexture são usados conforme sua função |
| P07 | Fluxo geral incompleto | Parcialmente confirmado | P2 | Ciclo técnico completo; interações/narrativa além dele não especificadas |
| P08 | Ausência de aleatoriedade nos trials | Confirmado | P2 | Cinco arrays fixos; acesso por `CandidateIndex`; sem embaralhamento |
| P09 | Adaptabilidade ausente | Não implementado | P3 | Nenhum mecanismo ajusta estímulos por erro, tempo ou desempenho |

### P01 — Apresentação dos policubos precisa de avaliação artística e perceptual

- **Categoria:** apresentação visual e direção artística.
- **Status de verificação:** Subjetivo. As características técnicas abaixo são confirmadas; a insuficiência para o público infantil requer avaliação.
- **Prioridade:** P1.
- **Descrição:** os estímulos têm o mesmo material azul acinzentado, iluminação simulada no shader e arestas discretas. Há variação de luminância entre faces, mas não há iluminação dinâmica nem sombra projetada dos modelos.
- **Comportamento observado:** cada face externa de cada cubo possui UV próprio e uma linha de borda calculada por derivadas. A apresentação usa RT de 512×512, `antiAliasing = 1`, base visual `Quaternion.Euler(22,-34,0)`, rotação do candidato e câmera ortográfica. O enquadramento depende do raio do modelo. O prefab de viewport tem referência de 354×354 unidades de UI; as telas podem sobrescrever dimensões. Não há rotação interativa pelo participante.
- **Comportamento esperado ou hipótese:** as duas peças devem permitir discriminar forma e profundidade sem introduzir pistas de resposta ou mudar a tarefa.
- **Evidências:** `Validation/model-reference.png`, `Validation/model-candidate.png`, `Validation/trial.png`; geometria e transparência verificadas nos testes de migração.
- **Arquivos envolvidos:** `Assets/Materials/Polycube.shader`, `Polycube.mat`; `Assets/Scripts/Trials/ModelPresentation.cs`, `TrialModel.cs`; `Assets/Prefabs/UI/ModelPresentation.prefab`, `Trial.prefab`; 50 assets de modelos.
- **Impacto:** faces coplanares e cubos adjacentes podem parecer pouco separados; tamanho aparente muda entre formas com o enquadramento por raio. A adequação lúdica não é determinada por ausência de erro de renderização.
- **Possíveis causas:** material único, contraste de aresta moderado, resolução fixa, projeção e escala. Não foi demonstrado defeito de malha, espelhamento ou referência trocada nas cenas entregues.
- **Recomendação:** comparar propostas de contraste de faces/arestas, iluminação e enquadramento uniforme em um conjunto controlado. Não colorir referência e candidato com pistas diferentes nem alterar coordenadas, ângulos ou gabaritos sem decisão do estudo.
- **Critério de conclusão:** revisão visual nas resoluções-alvo e avaliação aprovada pela equipe; geometria e condições originais continuam equivalentes; diferenças de apresentação documentadas.
- **Dependências:** público, dispositivos-alvo, P18 e aprovação de apresentação dos estímulos.
- **Necessita decisão científica ou de design?:** Sim, ambas.

### P02 — Repetição ilimitada e registro insuficiente de tentativas

- **Categoria:** gameplay, usabilidade e definição metodológica.
- **Status de verificação:** Confirmado quanto ao comportamento; não é uma conclusão de inadequação científica.
- **Prioridade:** P1, para decisão antes de coleta científica.
- **Descrição:** uma resposta errada não avança o índice, não retira peças e não limita novas respostas. O feedback textual dura 1,2 s; os botões ficam bloqueados durante a resolução.
- **Comportamento observado:** `Answer` registra toda resposta em `Debug.Log`. `Resolve(false,...)` apresenta “Vamos observar mais uma vez.”, mantém os modelos visíveis, zera `activeElapsed` e reinicia `attemptStarted`. A criança pode repetir a mesma resposta; após perceber o erro, pode escolher a alternativa oposta. O gabarito não muda.
- **Comportamento esperado ou hipótese:** definir com a orientação se o jogo é treinamento, avaliação ou combinação, e qual unidade de medida e política de correção deve valer.
- **Evidências:** ramos de `Answer`, `Resolve`, `Pause`, `Resume`; testes anteriores incluem erro e repetição do estímulo.
- **Arquivos envolvidos:** `Assets/Scripts/Trials/TrialController.cs`, `Assets/Scripts/Core/GameSession.cs`, `Assets/Scripts/UI/TrialView.cs`, `Assets/Prefabs/UI/Trial.prefab`.
- **Impacto:** o tempo da última tentativa não representa o tempo total do estímulo. É possível inferir tentativas pela ordem de logs de uma execução íntegra, mas não há campo `attempt_index`, indicador de primeira resposta ou agregação por estímulo.
- **Possíveis causas:** manutenção deliberada do comportamento do protótipo e ausência de protocolo de registro estruturado.
- **Recomendação:** separar três decisões: prevenção técnica de entradas duplicadas (já há trava `busy`), clareza do feedback e regra experimental de tentativa/tempo. Não escolher aqui limite de tentativas nem excluir respostas arbitrariamente.
- **Critério de conclusão:** especificação aprovada de feedback, avanço, tentativas, tempo acumulado e registro; casos de erro repetido, pausa e retomada testados contra essa especificação.
- **Dependências:** orientação científica, P13 e P14.
- **Necessita decisão científica ou de design?:** Sim. Não há evidência nesta auditoria para declarar uma regra de feedback cientificamente correta.

### P03 — Paleta plana e categorias pouco explícitas

- **Categoria:** ferramentas de edição e organização de assets.
- **Status de verificação:** Parcialmente confirmado.
- **Prioridade:** P2.
- **Descrição:** a vila já possui separação funcional de terreno, caminhos, água, vegetação baixa, flores e obstáculos. A principal deficiência comprovável de organização está na seleção dos tiles pela paleta e nos nomes dos recursos.
- **Comportamento observado:** Grid Isometric, célula `(1,0.5,1)`, seis Tilemaps serializados. `Assets/Tiles` reúne 57 assets, majoritariamente `Ground_NNN`; uma paleta Kingdom reúne os tiles em `Layer1`, sem categorias ou rótulos de terreno/água/caminho/decoração. Árvores e construções são prefabs separados, não tiles dessa paleta.
- **Comportamento esperado ou hipótese:** o editor deve deixar claro qual recurso pintar e em qual Tilemap. Não há requisito para transformar todos os objetos em tiles.
- **Evidências:** `Assets/Tiles/Palettes/Kingdom.prefab`; nomes e propriedades dos Tilemaps de `Village.unity`; `Documentation/EditingGuide.md`.
- **Arquivos envolvidos:** `Assets/Tiles/**`, `Assets/Art/Converted/Tiles/**`, `Assets/Scenes/Village.unity`, `Assets/Scripts/Village/VillageNavigation.cs`.
- **Impacto:** escolha por tentativa visual, manutenção dependente do guia e risco de pintar água/caminho em camada sem efeito na navegação.
- **Possíveis causas:** a conversão preservou índices e layout do conjunto anterior, em vez de criar um catálogo semântico de edição.
- **Recomendação:** planejar nomes, pastas/paletas por função, amostras e documentação de sobreposição, preservando GUIDs. Conferir pivots de tiles altos antes de reorganizar. O marcador de obstáculos deve ser reconhecível no editor.
- **Critério de conclusão:** um desenvolvedor identifica e pinta as categorias sem consultar números de origem; caminhos, água e obstáculos continuam coerentes; nenhum GUID ou layout muda acidentalmente.
- **Dependências:** taxonomia de arte e P19.
- **Necessita decisão científica ou de design?:** Design/ferramentas; não científica, se o conteúdo dos desafios não mudar.

### P04 — Retratos e aldeões usam apresentações de movimento diferentes

- **Categoria:** personagens, animação e composição visual.
- **Status de verificação:** Parcialmente confirmado. Não foi comprovado recorte corrompido pela migração.
- **Prioridade:** P1.
- **Descrição:** a aproximação do visitante é uma animação de UI de cintura para cima, com escala e posição fixas. Os aldeões do mapa usam outro conjunto visual, sem relação de identidade com aquele retrato.
- **Comportamento observado:** o retrato passa de escala 0,78 para 1 e de Y=-112 para Y=-170 em 1,08 s; troca seis quadros a cada 0,135 s e depois retorna ao quadro parado 0. Há máscara retangular de 360×340 e figura de 448×416 no prefab. Os quatro `Villager_N` usam um PNG cada; `VillageWalker` simula passada por balanço e rotação do `body`, não por uma sequência de sprites direcionais.
- **Comportamento esperado ou hipótese:** definir enquadramento, apoio visual, distância de aproximação e correspondência entre personagem da vila, fila e balcão.
- **Evidências:** `Audit/portrait-crops.json`: todos os **343 recortes de 28×26 são cópias exatas** das regiões do atlas de 196×1274. Na pele da linha 0, o limite inferior não transparente permanece Y=25; variações laterais e de altura dos desenhos já existem no atlas. Capturas de menu/tutorial/trial mostram o enquadramento atual.
- **Arquivos envolvidos:** `Assets/Scripts/Characters/CharacterPortrait.cs`, `VillageWalker.cs`, `PortraitLibrary.cs`; `Assets/Prefabs/UI/CharacterPortrait.prefab`; `Assets/Prefabs/Village/Villager_*.prefab`; `Assets/Art/Converted/Characters/**`.
- **Impacto:** movimento pode parecer deslizar/mudar de tamanho; não há apoio pelos pés no retrato recortado nem uma transição de corpo inteiro para o balcão. Não se deve atribuir automaticamente isso a pivots errados dos PNGs.
- **Possíveis causas:** composição herdada de retrato ampliado, coordenadas fixas de UI, máscara e animação simplificada do mundo. A causa de qualquer salto específico precisa ser reproduzida no quadro e personagem correspondentes.
- **Recomendação:** especificar apresentação e capturar aproximação/parada para diferentes combinações. Revisar alinhamento das três camadas antes de editar a arte. Separar animação de retrato e caminhada real da vila.
- **Critério de conclusão:** amostras de chegada/parada sem deslocamento indesejado segundo referência aprovada; enquadramento consistente nas resoluções-alvo; identidade visual definida entre sistemas.
- **Dependências:** P05, P11, P18 e referência de design.
- **Necessita decisão científica ou de design?:** Design; científica se mudar duração/atenção antes da medida de tempo.

### P05 — Não existe uma fila visual de visitantes

- **Categoria:** funcionalidade de apresentação ausente.
- **Status de verificação:** Não implementado.
- **Prioridade:** P2.
- **Descrição:** há sequência lógica de estímulos, não uma fila de personagens posicionados no cenário de atendimento.
- **Comportamento observado:** `TrialView` tem um `CharacterPortrait`. `Present` escolhe aparência por `100 + modelId`; ao acertar, apresenta o próximo. Não existem posições de fila, lista de visitantes, retirada do primeiro, reposicionamento ou animação de saída. Os 14 aldeões do mapa circulam independentemente dos trials.
- **Comportamento esperado ou hipótese:** o comportamento final da fila não está especificado nos documentos examinados. Não presumir quantidade, distância, ordem ou obrigatoriedade de mostrar todos os dez visitantes simultaneamente.
- **Evidências:** campos de TrialView e CharacterPortrait, fluxo de TrialController e hierarquia de Trial.prefab.
- **Arquivos envolvidos:** `Assets/Scripts/UI/TrialView.cs`, `Assets/Scripts/Trials/TrialController.cs`, `Assets/Scripts/Characters/CharacterPortrait.cs`, `Assets/Prefabs/UI/Trial.prefab`.
- **Impacto:** ausência de continuidade visual de entrada/saída e de informação sobre visitantes seguintes.
- **Possíveis causas:** o protótipo implementou apenas um atendimento por vez; não há controlador de fila a completar.
- **Recomendação:** criar especificação de estados/quantidade/identidade/transições antes de implementar. Preservar a ordem experimental aprovada e não associar aparência ao gabarito.
- **Critério de conclusão:** especificação aprovada e fila implementada com transições verificadas, sem adiantar estímulos ou registrar respostas indevidamente.
- **Dependências:** P04, P07, P11 e decisão sobre sequência em P08.
- **Necessita decisão científica ou de design?:** Sim, principalmente design; apresentação antecipada de estímulos requer avaliação científica.

### P06 — A observação de rasterização procedural não corresponde ao runtime atual

- **Categoria:** verificação de arquitetura visual.
- **Status de verificação:** Não confirmado.
- **Prioridade:** P3, apenas se for desejada uma revisão artística futura.
- **Descrição:** não foi encontrada dependência do rasterizador antigo para construir telas ou personagens durante a execução das cenas Kingdom/Village.
- **Comportamento observado:** salão e personagens são sprites importados; textos, fundos de painel, bordas, barras e botões usam uGUI serializado. Polícubos são Mesh assets exibidos por câmeras em RT; criar a RenderTexture não é rasterizar proceduralmente a arte da vila/fila. As criações de Texture2D para capturas estão em scripts de editor.
- **Comportamento esperado ou hipótese:** arte deve ser editável por assets; componentes de UI e conteúdo dinâmico podem continuar em uGUI.
- **Evidências:** busca por `new Texture2D`, `Sprite.Create`, `new GameObject` e `FromBase64String` nos scripts de runtime; referências dos prefabs aos PNGs de Art/Converted. P10 é uma cena adicional quebrada, não uma fábrica de arte ativa.
- **Arquivos envolvidos:** `Assets/Art/Converted/UI/**`, `Assets/Art/Converted/Characters/**`, `Assets/Prefabs/UI/**`, `Assets/Scripts/Trials/ModelPresentation.cs`.
- **Impacto:** não há defeito comprovado a corrigir por esse relato. A fila ausente é P05, e aparência/posicionamento são P04.
- **Possíveis causas:** impressão baseada no protótipo anterior ou expectativa de que painéis simples também fossem PNGs.
- **Recomendação:** não substituir indiscriminadamente texto, layouts e painéis por imagens. Tratar somente necessidades artísticas específicas aprovadas.
- **Critério de conclusão:** nenhuma correção técnica exigida; manter a ausência de dependência do legado nos testes.
- **Dependências:** nenhuma para a constatação; P04/P05 se houver redesign.
- **Necessita decisão científica ou de design?:** Design somente para revisão visual opcional.

### P07 — Ciclo jogável completo, experiência final ainda não especificada

- **Categoria:** fluxo, narrativa e usabilidade.
- **Status de verificação:** Parcialmente confirmado.
- **Prioridade:** P2.
- **Descrição:** as transições essenciais estão implementadas, mas a experiência além do ciclo de desafios/construções é limitada.
- **Comportamento observado:** menu → dois passos de tutorial → sequência do observatório → revelação da construção → vila. Escolher placa e depois INICIAR abre outra sequência; VILA pausa e RETOMAR retorna ao estímulo. Observatório pronto abre uma tela de objetos celestes com nome/tipo por apontar/clicar e botão de retorno. Ponte desbloqueia moinho. Ao concluir cinco prédios, o HUD oferece REINICIAR.
- **Comportamento esperado ou hipótese:** especificar o papel narrativo do jogador, motivo das peças, orientação na escolha das construções e experiência de conclusão. Não há roteiro final aprovado nos documentos.
- **Evidências:** GameApplication, TutorialController, VillageBuilding, ObservatoryView, CelestialTarget e testes de fluxo anteriores. Museu/ferraria/moinho prontos não têm atividade própria; castelo não abre uma tela. Isso é escopo ausente, não estado inacessível comprovado.
- **Arquivos envolvidos:** `Assets/Scripts/Core/GameApplication.cs`, `GameSession.cs`; `Assets/Scripts/Village/VillageBuilding.cs`; `Assets/Scripts/Observatory/**`; prefabs UI e construções.
- **Impacto:** conclusão comunica apenas estado do HUD; a vila pode parecer um seletor de sequências, com poucos motivos para explorar prédios já prontos. Ação em dois passos (placa + INICIAR) depende de clareza da UI.
- **Possíveis causas:** foco anterior em preservar a mecânica técnica, sem roteiro/narrativa adicionais.
- **Recomendação:** aprovar um mapa de experiência e critérios de cada tela. Não adicionar minijogos, narrativa ou recompensas nesta auditoria.
- **Critério de conclusão:** fluxo desejado documentado; cada transição e interação tem objetivo e feedback; testes de observação com público-alvo cobrem descoberta, pausa, retorno e conclusão.
- **Dependências:** P04/P05, design de experiência e P17/P18.
- **Necessita decisão científica ou de design?:** Sim; qualquer mudança que altere treino, duração ou exposição aos estímulos precisa passar pela orientação.

### P08 — Sequências fixas e reproduzíveis, sem embaralhamento

- **Categoria:** configuração experimental e gameplay.
- **Status de verificação:** Confirmado.
- **Prioridade:** P2.
- **Descrição:** os 50 estímulos continuam divididos em cinco sequências de dez, na ordem do contrato anterior.
- **Comportamento observado:** modelo, ângulo, eixo e `structuralMatch` são campos dos arrays `BuildingDefinition.trials`. `CurrentTrial` usa diretamente `CandidateIndex`, iniciado em zero e incrementado após acerto. Não existe seed de trials nem randomização. A escolha livre da ordem de construções após a primeira e a dependência ponte→moinho não embaralham o conteúdo de cada uma. Aparência dos retratos e passeios usam seeds próprias e não mudam o gabarito.
- **Comportamento esperado ou hipótese:** definir se a ordem fixa atende ao estudo ou se será necessário contrabalanceamento/randomização reproduzível.
- **Evidências:** cinco assets `Assets/Data/Buildings/*.asset`, `GameSession.CurrentTrial/StartBuilding/ApplyCorrectAnswer`, `Documentation/OriginalTrialContract.json`.
- **Arquivos envolvidos:** os anteriores e `Assets/Scripts/Trials/TrialController.cs`.
- **Impacto:** repetição permite familiaridade e memorização da sequência; o efeito disso não foi medido. As sessões atuais são reproduzíveis sob a mesma ordem de construções.
- **Possíveis causas:** preservação explícita dos dados fixos originais.
- **Recomendação:** discutir alternativas: manter ordem fixa versionada; embaralhar apenas estímulos dentro de blocos; usar listas contrabalanceadas. Todas precisam preservar quotas/metas, registrar ordem realizada e, quando aplicável, seed/versão. Não fixar aqui uma alternativa científica.
- **Critério de conclusão:** estratégia aprovada e documentada; reprodução da sessão e regressão da progressão verificadas. Até lá, manter os arrays atuais.
- **Dependências:** orientação, P02/P13 e regras de progressão por peças aceitas.
- **Necessita decisão científica ou de design?:** Sim.

### P09 — Não há adaptação ao desempenho

- **Categoria:** funcionalidade futura.
- **Status de verificação:** Não implementado.
- **Prioridade:** P3, até existirem requisitos científicos e de produto.
- **Descrição:** diversidade fixa de forma/ângulo/eixo não constitui adaptação.
- **Comportamento observado:** erros só repetem o mesmo estímulo, acertos avançam o índice, peças desbloqueiam prédios. Tempo e erro não alimentam escolha de dificuldade, perfil, conteúdo ou recomendação. Seeds de retratos/animais são cosméticas.
- **Comportamento esperado ou hipótese:** adaptação é uma intenção futura; não há algoritmo ou regra aprovada nos documentos.
- **Evidências:** GameSession, TrialController, BuildingDefinition e buscas nos scripts de runtime por mecanismos de desempenho/seleção.
- **Arquivos envolvidos:** `Assets/Scripts/Core/GameSession.cs`, `Assets/Scripts/Trials/TrialController.cs`, `Assets/Scripts/Gameplay/BuildingDefinition.cs`, `Assets/Data/Buildings/**`.
- **Impacto:** todos recebem o mesmo conjunto por construção; não existe personalização automática. Não impede o funcionamento do ciclo atual.
- **Possíveis causas:** requisito ainda fora da implementação e ausência de medidas/modelo científico definidos.
- **Recomendação:** documentar objetivo, sinais permitidos, limites e avaliação antes de propor qualquer algoritmo. Não implementar IA nem confundir mais conteúdo com adaptação.
- **Critério de conclusão:** requisitos e protocolo de avaliação aprovados antes de implementação, com rastreabilidade das decisões e preservação da reprodutibilidade.
- **Dependências:** P02, P08, P13, dados apropriados e orientação científica.
- **Necessita decisão científica ou de design?:** Sim, ambas.

## C. Pendências adicionais

Os campos abaixo têm o mesmo significado dos itens iniciais. As prioridades expressam impacto no uso indicado, não complexidade de implementação.

### P10 — Cena local adicional contém scripts aposentados

- **Categoria:** integridade/editor; **Status de verificação:** Confirmado; **Prioridade:** P1.
- **Descrição:** SampleScene foi acrescentada após o snapshot da migração e contém objetos DemoBootstrap, Art conversion e componentes do protótipo.
- **Comportamento observado:** três GUIDs de scripts não existem em Assets nem nos 14.355 GUIDs de metadados de pacotes inspecionados. Dois correspondem a ReinoVillageView e GameManager aposentados. A cena não está nos Build Settings.
- **Comportamento esperado ou hipótese:** entrada oficial continua Kingdom; uma cena de trabalho não deve parecer uma alternativa funcional sem identificação.
- **Evidências:** `Audit/inventory.json`, `Audit/script-guid-resolution.json`, `.meta` aposentados locais e configuração de build.
- **Arquivos envolvidos:** `Assets/Scenes/SampleScene.unity` e `.meta`; `ProjectSettings/EditorBuildSettings.asset`.
- **Impacto:** confusão ao abrir/testar a cena errada; Missing Scripts fora das cenas entregues anteriormente. Não prova falha do fluxo Kingdom.
- **Possíveis causas:** salvamento posterior de uma cena aberta com objetos antigos; origem exata dessa ação não foi determinada.
- **Recomendação:** decidir destino da cena com o autor. Na cópia de integração ela foi preservada em `Documentation/Integration/LocalSceneReview`, fora da importação ativa. O original local foi mantido intacto.
- **Critério de conclusão:** destino documentado e nenhuma cena apresentada como executável contém componentes ausentes.
- **Dependências:** revisão do trabalho local; **Necessita decisão científica ou de design?:** não, decisão de manutenção/autoria.

### P11 — Autorização de redistribuição da arte precisa estar rastreável

- **Categoria:** publicação e proveniência; **Status de verificação:** Necessita validação; **Prioridade:** P0 exclusivamente para o push público dos arquivos afetados.
- **Descrição:** crédito não comprova autorização de redistribuir fontes. Mana Seed foi identificado no código aposentado e no menu; a origem dos tiles, animais e atlas de árvores não está documentada com licença local.
- **Comportamento observado:** os termos atuais da autora restringem redistribuição e também mencionam uso junto de conteúdo produzido por IA. O responsável informou possuir autorização específica; falta registrar seu escopo/referência antes da publicação dos arquivos afetados.
- **Comportamento esperado ou hipótese:** autorização aplicável aos PNGs originais, derivados e disponibilidade pública, incluindo eventuais condições específicas deste projeto.
- **Evidências:** [licença oficial Mana Seed](https://selieltheshaper.weebly.com/user-license.html), [Character Base](https://seliel-the-shaper.itch.io/character-base), consultados em 03/11/2026; `Documentation/ThirdPartyAssets.md` registra o estado da verificação sem presumir autorização.
- **Arquivos envolvidos:** `Assets/Art/Original/**`, `Assets/Art/Converted/Characters/**`, `Nature/**`, `Animals/**`, `Tiles/**` e capturas que mostram essa arte.
- **Impacto:** impede afirmar que a entrega completa está liberada para repositório público. Não impede preparação local ou auditoria.
- **Possíveis causas:** perda de arquivos de licença/proveniência no protótipo e incorporação antiga de PNG em código.
- **Recomendação:** obter referência de autorização e inventário por conjunto; preservar créditos/licenças. Não atribuir uma licença aberta global ao projeto inteiro sem distinguir terceiros. Não contatar autores em nome do usuário nesta tarefa.
- **Critério de conclusão:** cada conjunto tem autor, fonte, licença e permissão de redistribuição pública compatível; exceções específicas registradas, sem expor correspondência privada.
- **Dependências:** responsável pelo projeto/titulares; **Necessita decisão científica ou de design?:** não científica; autorização de publicação.

### P12 — Divergência de versões exige manter uma base técnica explícita

- **Categoria:** integração e dependências; **Status de verificação:** Confirmado; **Prioridade:** P2.
- **Descrição:** o remoto é um template 3D URP em Unity 6000.5.7f1; a entrega é 2D/3D em Unity 6000.3.25f1, URP 17.3.0.
- **Comportamento observado:** manifestos também divergem em uGUI, Test Framework, Timeline e ferramentas de editor; há pacote AI Navigation apenas no remoto e pacotes 2D apenas no local. Não foi encontrada implementação remota de gameplay que dependa do NavMesh.
- **Comportamento esperado ou hipótese:** um conjunto único de versão, lockfile, render pipeline e configurações, sem combinar serializações de versões distintas.
- **Evidências:** `Integration/remote-comparison.json`, `Documentation/GitHubIntegration.md` e histórico-base `18a0b6077d126ebd9a1e963e914320b01387b9ce`.
- **Arquivos envolvidos:** Packages, ProjectSettings e Assets/Settings de ambos os conjuntos.
- **Impacto:** abrir com versão diferente pode provocar reimportação/conversão e dificultar reprodução.
- **Possíveis causas:** bases locais e remotas criadas em editores diferentes, sem integração anterior.
- **Recomendação:** a preparação utiliza integralmente o conjunto local já validado; o template remoto foi preservado para consulta fora de Assets. Não houve execução de upgrade/downgrade. Uma futura troca de versão deve ser tarefa própria.
- **Critério de conclusão:** revisão aceita a versão de referência e importação/build na versão declarada são reproduzíveis. A decisão de composição da branch já está documentada; compatibilidade com 6000.5.7f1 não foi afirmada.
- **Dependências:** revisão técnica; **Necessita decisão científica ou de design?:** não.

### P13 — Logs de Console não são um registro de pesquisa estruturado

- **Categoria:** infraestrutura futura de dados; **Status de verificação:** Não implementado; **Prioridade:** P1 antes de qualquer coleta científica.
- **Descrição:** respostas são enviadas a Debug.Log, sem arquivo/exportador de sessão dedicado.
- **Comportamento observado:** há building, candidate_index, model_id, rotação, eixo, condição, resposta, acerto, peças e tempo. Não há esquema persistente de sessão/tentativa, versão do protocolo, ordem efetiva completa, exportação CSV/JSON, confirmação de escrita ou rotina de recuperação. `accepted_pieces` é registrado antes de aplicar a recompensa daquela resposta.
- **Comportamento esperado ou hipótese:** definir requisitos de pesquisa e tratamento de dados antes de desenvolver o registro.
- **Evidências:** chamada de Debug.Log em TrialController.Answer e ausência de persistência/exportação nos scripts de runtime.
- **Arquivos envolvidos:** `Assets/Scripts/Trials/TrialController.cs`, `Assets/Scripts/Core/GameSession.cs`.
- **Impacto:** Console não assegura conjunto de dados completo nem identidade inequívoca de tentativas/sessões. Os logs sintéticos de testes não são dados de participantes.
- **Possíveis causas:** escopo de protótipo funcional, sem instrumentação científica.
- **Recomendação:** aprovar esquema, pseudonimização, retenção/acesso, sem inventar campos pessoais necessários. Separar logs técnicos de eventos de pesquisa.
- **Critério de conclusão:** exportação verificável/reprodutível com amostra sintética, identificação de primeira resposta/tentativas e regras de tempo aprovadas.
- **Dependências:** P02/P08/P14 e requisitos institucionais; **Necessita decisão científica ou de design?:** científica e de governança de dados.

### P14 — Foco da aplicação e instante de apresentação não têm protocolo de tempo

- **Categoria:** temporização e metodologia; **Status de verificação:** Confirmado; **Prioridade:** P2.
- **Descrição:** usa-se relógio monotônico de tempo real; a pausa explícita é tratada, mas perda de foco/interrupção externa não possui tratamento próprio.
- **Comportamento observado:** tempo começa ao terminar a chegada, quando os objetos são ativados, sem sincronizar o instante com o primeiro frame exibido. VILA acumula tempo ativo e retomar exclui o intervalo pausado. Não existem OnApplicationFocus/OnApplicationPause no runtime. `runInBackground` da entrega é 0; o relógio continua representando tempo real ao retomar foco.
- **Comportamento esperado ou hipótese:** definir o que contar em interrupção, suspensão e latência de apresentação, e qual precisão é necessária.
- **Evidências:** Reveal/Answer/Pause/Resume; configuração PlayerSettings; busca de callbacks de foco.
- **Arquivos envolvidos:** `Assets/Scripts/Trials/TrialController.cs`, `ProjectSettings/ProjectSettings.asset`.
- **Impacto:** tempos podem incluir afastamento do aplicativo; três casas decimais no log não demonstram precisão experimental de milissegundos.
- **Possíveis causas:** cronômetro de gameplay sem protocolo de medição/calibração.
- **Recomendação:** definir e testar interrupções e eventos de apresentação antes de interpretar tempos cientificamente. Não alterar o cronômetro nesta tarefa.
- **Critério de conclusão:** política documentada e casos foco/suspensão/pausa/erro testados no hardware-alvo.
- **Dependências:** P02/P13 e plataforma-alvo; **Necessita decisão científica ou de design?:** científica.

### P15 — Progresso não persiste ao encerrar a aplicação

- **Categoria:** funcionalidade futura; **Status de verificação:** Não implementado; **Prioridade:** P2.
- **Descrição:** GameSession mantém estado somente em memória.
- **Comportamento observado:** reiniciar cria uma sessão limpa; fechar e abrir não carrega construções, peças ou trial em andamento. Não há botão de continuar nem confirmação própria para abandonar a sessão.
- **Comportamento esperado ou hipótese:** decidir se sessões devem ser contínuas ou retomáveis; isso não é um bug contra o escopo anterior.
- **Evidências:** construtor de GameSession, GameApplication.Start/Restart e ausência de PlayerPrefs/arquivos de sessão no runtime.
- **Arquivos envolvidos:** `Assets/Scripts/Core/GameSession.cs`, `GameApplication.cs`, menu.
- **Impacto:** perda de progresso entre execuções; eventual retomada científica necessita regras distintas de simples save de jogo.
- **Possíveis causas:** persistência estava fora do escopo da migração.
- **Recomendação:** especificar política de retomada/reinício antes de implementar, distinguindo progresso lúdico e dados de pesquisa.
- **Critério de conclusão:** política aprovada, save versionado e recuperação testada se aplicável.
- **Dependências:** P13 e design de sessão; **Necessita decisão científica ou de design?:** ambas se usada em estudo.

### P16 — Não há sistema de áudio do jogo

- **Categoria:** funcionalidade futura; **Status de verificação:** Não implementado; **Prioridade:** P3.
- **Descrição:** não foram encontrados clips de áudio próprios, AudioSources de gameplay ou controle de volume nas telas.
- **Comportamento observado:** feedback e tutorial dependem da apresentação visual/textual. AudioListener/câmera ou módulos da engine não equivalem a áudio implementado.
- **Comportamento esperado ou hipótese:** especificar necessidade de efeitos, música e eventual narração, respeitando atenção e protocolo.
- **Evidências:** inventário de extensões de áudio e componentes/ações do runtime.
- **Arquivos envolvidos:** Assets/Prefabs/UI, cenas e configurações de áudio padrão.
- **Impacto:** não há reforço sonoro nem alternativa auditiva ao texto; não impede o ciclo técnico atual.
- **Possíveis causas:** conteúdo fora do escopo anterior.
- **Recomendação:** definir função de cada som e controles antes de produzir/importar conteúdo, com licença apropriada.
- **Critério de conclusão:** áudio aprovado, controles testados e ausência de pistas indevidas nos estímulos.
- **Dependências:** P11/P17 e design; **Necessita decisão científica ou de design?:** ambas conforme uso.

### P17 — Acessibilidade de navegação e compreensão precisa de validação

- **Categoria:** interface/acessibilidade; **Status de verificação:** Parcialmente confirmado; **Prioridade:** P2.
- **Descrição:** existem botões com textos e símbolos, mas não há solução específica de leitura assistida, tamanho de texto, redução de movimento ou alternativa completa ao arraste/apontamento.
- **Comportamento observado:** GameUI seleciona automaticamente botão no menu/tutorial/observatório, mas limpa seleção em trials/vila. A câmera depende de mouse ou toque. Objetos celestes respondem a pointer enter/click, sem interface Selectable própria. Isso não prova que todo teclado falhe; o percurso completo não foi validado por teclado/gamepad.
- **Comportamento esperado ou hipótese:** definir necessidades etárias/motoras/visuais e entradas suportadas.
- **Evidências:** GameUI.Show, VillageCameraDrag.Update, CelestialTarget e propriedades de botões/prefabs.
- **Arquivos envolvidos:** `Assets/Scripts/UI/GameUI.cs`, `Assets/Scripts/Village/VillageCameraDrag.cs`, `Assets/Scripts/Observatory/CelestialTarget.cs`, prefabs UI.
- **Impacto:** parte do público pode depender de um adulto ou dispositivo específico para navegar e compreender o jogo.
- **Possíveis causas:** interação inicialmente orientada a pointer e layout fixo de referência.
- **Recomendação:** especificar suporte de entradas e testar legibilidade/contraste/tamanho de alvos no público-alvo. Não presumir conformidade por uso de uGUI.
- **Critério de conclusão:** percurso completo executável pelas entradas declaradas e avaliação de acessibilidade/entendimento com critérios definidos.
- **Dependências:** P07/P18 e público-alvo; **Necessita decisão científica ou de design?:** design e protocolo de aplicação.

### P18 — Layout mantém proporção fixa e pode reduzir legibilidade

- **Categoria:** interface e resoluções; **Status de verificação:** Parcialmente confirmado; **Prioridade:** P2.
- **Descrição:** CanvasScaler e GameUI preservam uma composição de referência 1600×900; não há layout refluindo os painéis para formato vertical.
- **Comportamento observado:** `GameUI.Update` aplica o menor fator entre largura/1600 e altura/900. Isso favorece caber a composição inteira, mas reduz textos/alvos e deixa espaço livre em outras proporções. Ausência de corte num exemplo não comprova legibilidade ou precisão de toque.
- **Comportamento esperado ou hipótese:** declarar orientações, resolução mínima e distância de uso; escolher entre desktop/paisagem e suporte móvel real.
- **Evidências:** GameUI.Update e CanvasScaler em GameUI.prefab; capturas anteriores de 1600×900. A seção G registra as amostras extras que efetivamente tenham sido produzidas.
- **Arquivos envolvidos:** `Assets/Scripts/UI/GameUI.cs`, `Assets/Prefabs/UI/GameUI.prefab`, telas, `ProjectSettings/ProjectSettings.asset`.
- **Impacto:** textos e retratos podem ficar pequenos em retrato ou janelas estreitas. Uma captura offscreen não testa safe area, DPI ou toque físico.
- **Possíveis causas:** preservação do layout original por escala uniforme.
- **Recomendação:** definir matriz de telas-alvo e critérios mensuráveis de texto/alvos antes de redesenhar. Avaliar também enquadramento do mapa/câmera após redimensionar.
- **Critério de conclusão:** navegação, leitura e estímulos aprovados em todas as configurações declaradas, sem sobreposição ou perda de acesso.
- **Dependências:** plataforma e P01/P17; **Necessita decisão científica ou de design?:** design; científica para tamanho dos estímulos.

### P19 — Obstáculos e rotas exigem manutenção manual coerente

- **Categoria:** vila e ferramentas; **Status de verificação:** Confirmado como limitação; **Prioridade:** P2.
- **Descrição:** a navegação consulta dados de Tilemaps e pontos das construções, não colisores completos da arte visual.
- **Comportamento observado:** Walkable usa Paths, Water e Navigation Obstacles, além de proximidade do centro das construções e células específicas de travessia da ponte. Árvores/cercas/decorações não atualizam automaticamente o Tilemap de obstáculos quando movidas. Um trajeto é calculado e guardado; mudanças após seu cálculo não provocam replanning automático do ator. A suavização de curvas não reconsulta colisão para cada ponto interpolado.
- **Comportamento esperado ou hipótese:** o cenário entregue pode ser estático, mas o fluxo de edição precisa explicitar essas dependências. Não foi comprovada colisão visual no percurso atual por esses fatos isolados.
- **Evidências:** VillageNavigation.Walkable/Route/Smooth/AnimalCell; VillageWalker e VillageAnimal; guia de edição.
- **Arquivos envolvidos:** `Assets/Scripts/Village/VillageNavigation.cs`, `Assets/Scripts/Characters/VillageWalker.cs`, `VillageAnimal.cs`, `Assets/Scenes/Village.unity`.
- **Impacto:** edições podem criar rotas através de arte ou bloquear entradas; ponte/moinho dependem de configuração coerente além de arrastar o sprite.
- **Possíveis causas:** grade lógica separada da arte e rotas estáticas adequadas ao mapa preservado.
- **Recomendação:** validação de autoria de mapa e convenção de footprints; decidir se há necessidade real de mudança dinâmica antes de implementar replanning.
- **Critério de conclusão:** mover/pintar exemplos documentados preserva conectividade/oclusão; nenhum caminho atravessa área declarada bloqueada.
- **Dependências:** P03 e definição de edição suportada; **Necessita decisão científica ou de design?:** design técnico.

### P20 — Destaque de construção pode sobreviver à limpeza da seleção lógica

- **Categoria:** consistência visual; **Status de verificação:** Confirmado por caminho de código; **Prioridade:** P3.
- **Descrição:** selecionar placa chama SetSelected; iniciar e voltar à vila limpam o índice `selected`, mas não restauram a cor das placas.
- **Comportamento observado:** após selecionar → INICIAR → VILA/concluir, o HUD pode pedir nova escolha enquanto a placa anterior continua com cor de seleção. Os pontos de limpeza explícita de cor são nova seleção e Restart.
- **Comportamento esperado ou hipótese:** cor e seleção ativa devem comunicar o mesmo estado.
- **Evidências:** SelectBuilding, StartBuilding, ShowVillage, ShowConstruction e Restart; VillageBuilding.SetSelected/Refresh.
- **Arquivos envolvidos:** `Assets/Scripts/Core/GameApplication.cs`, `Assets/Scripts/Village/VillageBuilding.cs`.
- **Impacto:** pequena ambiguidade de feedback, sem bloqueio de progressão demonstrado.
- **Possíveis causas:** limpeza parcial entre estado lógico e representação.
- **Recomendação:** futura correção pontual após reproduzir visualmente a sequência acima; não foi aplicada nesta tarefa.
- **Critério de conclusão:** cor, HUD e ação disponível concordam em iniciar, pausar, concluir e reiniciar.
- **Dependências:** nenhuma funcionalidade nova; **Necessita decisão científica ou de design?:** apenas confirmação de convenção visual.

### P21 — Reinício não limpa informações do observatório explicitamente

- **Categoria:** estado de apresentação; **Status de verificação:** Confirmado por código; **Prioridade:** P3.
- **Descrição:** progresso é reiniciado, mas os textos de objeto selecionado pertencem a uma UI reutilizada.
- **Comportamento observado:** Restart cria nova GameSession sem repor objectName/objectType do ObservatoryView. CelestialTarget.OnDisable restaura escala, não limpa informações. Em nova visita, o último texto pode continuar até apontar/clicar outro objeto.
- **Comportamento esperado ou hipótese:** definir se nova sessão deve começar sem seleção celeste.
- **Evidências:** GameApplication.Restart, ObservatoryView.ShowInformation e CelestialTarget.OnDisable.
- **Arquivos envolvidos:** `Assets/Scripts/Core/GameApplication.cs`, `Assets/Scripts/Observatory/ObservatoryView.cs`, `CelestialTarget.cs`.
- **Impacto:** apresentação residual entre sessões; não é persistência de dados de participantes nem falha de reset das construções.
- **Possíveis causas:** ausência de reset explícito da tela informativa.
- **Recomendação:** definir estado inicial da tela e cobrir o segundo ciclo em teste de UI.
- **Critério de conclusão:** segunda sessão apresenta somente o estado inicial aprovado até interação do jogador.
- **Dependências:** definição visual; **Necessita decisão científica ou de design?:** design simples.

### P22 — Desempenho e consumo gráfico ainda não têm orçamento medido

- **Categoria:** validação técnica; **Status de verificação:** Necessita validação; **Prioridade:** P2.
- **Descrição:** a vila tem 14 aldeões, 60 animais e muitos renderizadores; cada ModelPresentation prepara uma câmera/RT de 512×512.
- **Comportamento observado:** recursos são reutilizados e desativados com as telas. BFS aloca listas/dicionários para rotas; raios de busca são limitados pelo mapa/habitat. Isso não comprova lentidão, vazamento ou necessidade de otimização.
- **Comportamento esperado ou hipótese:** definir hardware-alvo e metas de frame time/memória/carregamento.
- **Evidências:** VillageNavigation, ModelPresentation, atores serializados; validações funcionais não são benchmarks.
- **Arquivos envolvidos:** `Assets/Scripts/Village/VillageNavigation.cs`, `Assets/Scripts/Trials/ModelPresentation.cs`, personagens e Village.unity.
- **Impacto:** desempenho de editor/batch não representa tablets ou computadores de aplicação do estudo.
- **Possíveis causas:** ausência de hardware e orçamento de desempenho definidos.
- **Recomendação:** medir primeiro com Profiler e build-alvo; otimizar só gargalos demonstrados, preservando estímulos e ritmo.
- **Critério de conclusão:** relatório de desempenho em hardware-alvo, incluindo várias entradas/saídas e reinícios, com limites aprovados.
- **Dependências:** plataforma e P23; **Necessita decisão científica ou de design?:** técnica, com impacto metodológico se afetar apresentação/tempo.

### P23 — Build de distribuição, CI e identidade de aplicação não foram preparados

- **Categoria:** infraestrutura; **Status de verificação:** Não implementado para CI/distribuição; **Prioridade:** P2.
- **Descrição:** os Build Settings apontam corretamente para Kingdom e Village, mas não existe pipeline de build/CI próprio nem evidência de pacote instalável validado.
- **Comportamento observado:** ainda há DefaultCompany e identificadores de template nas PlayerSettings; o remoto tem recomendações de editor e solução gerada, sem workflows de CI. Testes atuais são drivers de editor, não uma suíte integrada a uma pipeline de build.
- **Comportamento esperado ou hipótese:** definir plataforma, identificadores, forma de distribuição e estratégia de execução dos testes sem publicar credenciais/licenças.
- **Evidências:** ProjectSettings.asset, EditorBuildSettings.asset, estrutura remota e scripts Editor.
- **Arquivos envolvidos:** ProjectSettings, Packages e `Assets/Scripts/Editor/*Checks.cs`.
- **Impacto:** Play Mode aprovado não garante build distribuível ou reprodução automatizada em outra máquina.
- **Possíveis causas:** fase atual de desenvolvimento e falta de requisitos de entrega.
- **Recomendação:** primeira tarefa de infraestrutura após decisão de publicação: build limpo na versão fixada, verificação de shaders/LFS/entrada e instruções de abertura. CI exige escolha segura da licença Unity e secrets.
- **Critério de conclusão:** clone com LFS → importação → testes → build → execução, reproduzidos e documentados para plataforma declarada.
- **Dependências:** P11/P12 e plataforma; **Necessita decisão científica ou de design?:** decisão técnica/de distribuição.

### P24 — Validação de dados é específica ao contrato atual, não uma ferramenta geral de autoria

- **Categoria:** manutenção e configuração; **Status de verificação:** Confirmado como limitação; **Prioridade:** P2.
- **Descrição:** os ScriptableObjects são editáveis, mas o contrato e as ferramentas assumem as cinco sequências originais de dez estímulos e metas correspondentes.
- **Comportamento observado:** KingdomValidation compara diretamente com OriginalTrialContract e índices fixos; TrialView tem cinco indicadores no prefab. GameSession lança exceção se a sequência acaba antes da meta. Não há OnValidate próprio impedindo uma referência de modelo vazia, pré-requisito circular ou sequência incoerente enquanto o autor edita.
- **Comportamento esperado ou hipótese:** preservar o contrato atual é correto; expansão futura precisa de validação separada dos testes de regressão histórica.
- **Evidências:** BuildingDefinition, GameSession.ApplyCorrectAnswer, TrialView.ShowProgress e KingdomValidation.
- **Arquivos envolvidos:** `Assets/Scripts/Gameplay/BuildingDefinition.cs`, `Assets/Scripts/Core/GameSession.cs`, `Assets/Scripts/UI/TrialView.cs`, `Assets/Scripts/Editor/KingdomValidation.cs`.
- **Impacto:** adicionar conteúdo pode exigir ajuste de UI/testes e gerar erro de configuração em runtime; não foi constatada configuração inválida no conjunto atual.
- **Possíveis causas:** requisitos originais fixos e proteção estrita de regressão da migração.
- **Recomendação:** manter o contrato de referência versionado; antes de expandir, definir validador de autoria para nulos/metas/ciclos e indicação de mudanças intencionais.
- **Critério de conclusão:** configurações inválidas são identificadas antes do Play; novos conteúdos têm testes próprios sem apagar a evidência da base original.
- **Dependências:** requisitos de expansão, P08/P09; **Necessita decisão científica ou de design?:** científica para novos estímulos, técnica para validação.

## D. Funcionalidades futuras, separadas de defeitos

| Funcionalidade | Estado atual | Referência/condição para começar |
|---|---|---|
| Fila visual e transições completas de visitantes | Ausente | P05; especificação de design e exposição de estímulos |
| Narrativa/interações próprias das demais construções | Não especificadas nem implementadas | P07; aprovar fluxo antes de produzir conteúdo |
| Adaptação ao desempenho | Ausente | P09; requisitos e avaliação científicos |
| Exportação e recuperação de dados de pesquisa | Ausentes | P13; esquema e governança aprovados |
| Salvamento/continuar entre sessões | Ausente | P15; definir relação com protocolo |
| Áudio/narração e controles | Ausentes | P16; função, licenças e acessibilidade |
| Recursos específicos de acessibilidade | Parcialmente cobertos pelo uGUI padrão | P17; público e entradas suportadas |
| Randomização/contrabalanceamento | Ausentes; ordem fixa preservada | P08; não é correção automática |
| Build distribuível e CI | Não preparados/verificados | P23; plataforma e publicação |

## E. Questões metodológicas para a orientação

1. **Finalidade:** treinamento, avaliação ou fases separadas? Isso muda a interpretação de feedback/repetição, não determina uma resposta por si só.
2. **Unidade de análise:** primeira resposta, cada tentativa, resposta final ou agregado por estímulo? Definir denominadores e tratamento de erros antes de analisar dados.
3. **Tempo:** início na exibição efetiva, chegada ou outra referência? Contar pausas, erros, perda de foco e interrupções de que maneira? Precisão necessária e hardware ainda precisam ser definidos.
4. **Ordem:** manter blocos fixos, contrabalancear ou embaralhar sob seed? Separar ordem de construções da ordem de estímulos; progressão por peças correspondentes pode antecipar conclusão se a ordem for alterada sem revisar a regra.
5. **Apresentação:** que alterações de cor, arestas, tamanho, projeção e iluminação são aceitáveis sem fornecer pistas diferenciais? Validar saliência sem mudar as coordenadas/gabaritos.
6. **Personagens e antecipação:** a futura fila mostra apenas personagens ou também peças? Evitar pressupor que exposição antecipada não afeta a tarefa.
7. **Adaptação:** qual objetivo, sinais e limites? Não há algoritmo definido ou validado nesta auditoria.
8. **Dados/sessões:** identificar apenas o necessário, definir acesso/retenção/exportação e regras de retomada. Não foram encontrados dados pessoais de participantes no conjunto selecionado para integração, mas isso não substitui um plano institucional.

Não foi realizada revisão de literatura ou validação científica do paradigma. Nenhuma destas hipóteses foi convertida em regra experimental ou alteração de gameplay.

## F. Recomendações priorizadas

| Ordem | Trabalho recomendado | Dependência/impacto | Esforço indicativo |
|---|---|---|---|
| 1 | Resolver proveniência/autorização de publicação e revisar integração | P11 bloqueia push público; P12 fixa a base técnica | Administrativo variável; revisão técnica curta |
| 2 | Decidir destino da SampleScene adicional | P10; evita entrada errada no editor | Pequeno, após revisão do autor |
| 3 | Aprovar tentativas, feedback, tempo e esquema de eventos | P02/P13/P14; necessário antes de dados científicos | Discussão variável + implementação média posterior |
| 4 | Revisar apresentação de estímulos e retratos em amostras controladas | P01/P04/P18; sem mudar geometria/gabaritos | Médio; exige referência aprovada |
| 5 | Validar build, entradas, telas e desempenho na plataforma-alvo | P17/P18/P22/P23; estabelece limites práticos | Médio, depende do hardware |
| 6 | Organizar paletas e validações de edição | P03/P19/P24; reduz regressões ao editar | Pequeno a médio |
| 7 | Especificar fluxo de experiência e fila | P05/P07; narrativa/quantidade/transições ainda abertas | Design médio; produção dependente do escopo |
| 8 | Corrigir estados visuais residuais com testes focados | P20/P21; baixo risco quando isolados | Pequeno |
| 9 | Planejar save, áudio, eventual aleatorização e adaptação | P08/P09/P15/P16; não iniciar sem decisões anteriores | Variável; adaptação requer etapa científica própria |

As estimativas são relativas e não compromissos de prazo. Nenhuma implementação dessa sequência foi iniciada nesta tarefa.

## G. Limitações da auditoria e pontos já adequados

### Adequadamente implementado na base atual

- Separação entre dados imutáveis e GameSession; scripts especializados e composição serializada.
- Arte permanente, 100 meshes persistentes e terreno gravado na cena, sem fábricas antigas de UI/mundo em runtime.
- Cinquenta modelos/condições e cinco metas preservados no contrato; nenhum pedido de randomização/adaptação foi aplicado sem aprovação científica.
- Trava de respostas durante chegada/feedback; pausa explícita mantém índice e peças; reinício limpa a progressão.
- Construções com pré-requisito por referência; ponte libera conectividade para moinho.
- SortingGroups/DepthSorter e limites da câmera; a navegação consulta os Tilemaps, sem duplicar um terreno gerado em runtime.
- Referências dos prefabs e das duas cenas principais aprovadas nos testes estruturais anteriores; inventário atual sem GUID duplicado ou `.meta` de arquivo faltante.
- Todos os 343 recortes de retrato são exatos em relação ao atlas; uma aparência insatisfatória não deve ser atribuída a perda de pixels sem evidência.

### O que não se pode concluir

Uma execução automática não substitui teste de usabilidade infantil, hardware físico, revisão de todas as resoluções ou avaliação científica. Não se declara ausência de todo bug, acessibilidade completa, precisão experimental ou prontidão de distribuição.

Os logs de migração são evidência histórica válida para o conjunto cujos hashes foram preservados. Os resultados adicionais desta tarefa são registrados em `Audit/Execution.md`, com datas, arquivos e eventuais falhas. Uma tentativa de execução sem acesso ao serviço de licença da Unity foi interrompida; não conta como aprovação. Não foi operado o Console da sessão gráfica do usuário.

A busca estática por credenciais/dados pessoais e a revisão de arquivos selecionados reduzem o risco, mas não equivalem a uma certificação de segurança. Autorização de arte depende dos titulares e dos registros do responsável; esta auditoria não constitui parecer jurídico.
