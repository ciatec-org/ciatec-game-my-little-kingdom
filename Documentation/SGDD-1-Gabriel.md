# CIATec — GLIDE Framework

# DOC 1 — GAME DESIGN DOCUMENT CIENTÍFICO

*Scientific Game Design Document (SGDD)*

**Versão:** v1.5 — especificação pragmática para prototipagem, alinhada à implementação do aluno, com a adaptação no backend (26/11/2026)
**Framework:** GLIDE — Game Lifecycle for Inclusive Development and Evaluation
**Paradigma científico:** Mental Rotation / Shepard–Metzler

---

## 1. IDENTIFICAÇÃO DO PROJETO

**Nome do jogo:** *Meu Pequeno Reino*
**Versão do documento:** v1.5 — especificação pragmática para prototipagem
**Projeto vinculado:** CIATec — Plataforma Aberta de Jogos Adaptativos para Educação Inclusiva
**Organização:** CIATec
**Posição no Triple Diamond:** *a confirmar junto à coordenação do GLIDE*. O jogo encontra-se atualmente em fase de co-design e definição conceitual, anterior à consolidação do protótipo e de seus parâmetros experimentais.
**Ciclos de PPI incorporados:** Relatório PPI TEA v1.0 — Fase 1 de co-design, abril a maio de 2026, incorporado de forma contextual e parcial
**Pesquisador responsável:** Gabriel Milagres
**Data de criação:** 15/09/2026
**Última atualização:** 26/11/2026

### Histórico de versões

| Versão | Data | Mudança |
| --- | --- | --- |
| v1.1 | 28/09/2026 | Especificação pragmática para prototipagem. |
| v1.2 | 26/11/2026 | Decisões do orientador sobre as 22 lacunas do SGDD (LG-Gab-01 a 22), validadas por ele jogando um protótipo web: eixo da rotação, peças por construção, reino por código de criança, pausa em todas as telas, ritmo do ciclo, os quatro resultados, anoitecer, tutorial, mediador, configurações, dados locais e o conteúdo provisório da Vila. O índice está na §13B; cada seção tocada cita a lacuna. |
| v1.2.1 | 26/11/2026 | Perguntas abertas do ABD decididas pelo orientador, sem mudar o que ele validou no protótipo web: SAIR no menu do build Windows; código da criança como código da telemetria, com o reino só no aparelho; tutorial visto por código; painel do pesquisador numa aba do painel do mediador; eixo vertical dos estímulos antigos como pendência da equipe científica. |
| v1.3 | 26/11/2026 | Alinhado ao protótipo validado: plataforma Unity (Windows, WebGL, Android); subida da adaptação em rodízio e `difficulty_state`; blocos de 4 para MATCH/NON_MATCH; botão “adulto” no menu; pausa fora do menu e das configurações; tentativa `PAUSED` respondida; `INTERRUPTED` pelo mediador; limite da sessão e assentimento nas pendências. |
| v1.4 | 26/11/2026 | Alinhado ao código do repositório do aluno, por decisão do orientador: onde o código já decide, o SGDD o segue, marcado “conforme a implementação do aluno”. A primeira obra é o Observatório; só o `MATCH` aceito soma peça e a obra fecha no 10º estímulo; no erro a mesma peça volta; os estímulos mantêm a rotação original em x, y e z; cada obra tem uma sequência fixa de 10; a pausa do desafio leva à Vila; o REINICIAR continua; o habitante sai do modelo da peça; o mapa tem os limites da câmera da Vila. Quatro pendências novas na §13. |
| v1.5 | 26/11/2026 | A adaptação da demanda é executada pelo backend, via API (padrão CIATEC, decisão do orientador): a regra da §4.3 fica como especificação do backend, e o jogo envia a telemetria de cada tentativa, recebe os parâmetros da próxima, aplica-os e grava o `difficulty_state` recebido; até a API, um provedor local devolve `a45-cLOW-s1`. Notas na §4.3, §11.2, §11.4, §13 e no Bloco 9; a regra não muda. Na mesma decisão, a exportação e a exclusão dos dados saíram do jogo: a telemetria fica em arquivo local, pronta para o backend (§11.4). Também “pra simplificar”, o dispositivo-alvo passa a ser só o PC (Windows, mouse e teclado): §2.2, §7.4, §10.3, §13 e Anexo A Bloco 0; saem os builds WebGL e Android, o toque e os três toques no canto. |

### Nota sobre o PPI utilizado

O PPI disponível foi realizado com seis crianças com TEA, entre 5 e 10 anos, além de profissionais clínicos e cuidador, e buscou orientar decisões de design relacionadas a um jogo terapêutico baseado em captura de movimento.

Como o jogo descrito neste SGDD utiliza outro paradigma e outra forma de interação, os achados não são tratados como validação direta desta proposta. Foram incorporadas apenas evidências consideradas transferíveis ao desenho de experiências digitais para o público em questão, principalmente aquelas relacionadas a onboarding, variabilidade sensorial, tolerância à frustração, quantidade de estímulos simultâneos, necessidade de configurabilidade e heterogeneidade entre usuários.

O próprio relatório reconhece que o PPI informa decisões de design, mas não valida paradigmas cognitivos, e que suas decisões devem permanecer como hipóteses a serem testadas em ciclos posteriores.

## 2. CONCEITO DO JOGO

### 2.1. Premissa

O jogo será desenvolvido para o público infantil e terá como cenário uma pequena vila ou reino em processo de crescimento. O jogador assume uma posição de responsabilidade nesse espaço e participa de seu desenvolvimento ao analisar peças e materiais apresentados pelos habitantes para diferentes construções.

Cada habitante traz uma peça que acredita pertencer a determinado projeto. Consultando o **Grande Livro de Projetos**, o jogador observa a estrutura necessária e decide se a peça apresentada corresponde ao mesmo objeto visto em outra orientação ou se possui uma estrutura diferente.

As decisões fazem parte do cotidiano do reino e contribuem para seu crescimento, permitindo o surgimento de novas construções, habitantes, atividades e regiões exploráveis. A intenção é que a criança perceba sua experiência principalmente como a construção e o cuidado de um mundo próprio, enquanto a tarefa cognitiva permanece incorporada à própria jogabilidade.

---

### 2.2. Gênero e Plataforma

**Gênero:** serious game científico/avaliativo com elementos de construção de mundo, exploração, inspeção e puzzle visuoespacial.

**Plataforma:** Unity, com build para PC (Windows, mouse e teclado) (decidido pelo orientador em 26/11/2026; na v1.5, “pra simplificar”, os builds WebGL e Android saíram).

**Modo de jogo:** single player.

**Contexto de uso:** ainda não definido entre contexto escolar, supervisionado ou outros contextos possíveis. O jogo serve aos dois: cada criança tem o seu reino, escolhido pelo código dela no menu, e usa o mesmo código na escola e em casa (LG-Gab-04). Como o reino fica só no aparelho, escola e casa são reinos separados até a conexão com o servidor (decidido pelo orientador em 26/11/2026).

**Tecnologia de interação:** interação digital simples, baseada principalmente em seleção de respostas. Clique nos botões, com o mouse, ou o teclado.

---

### 2.3. Público-Alvo

O jogo será desenvolvido para estudantes dos anos iniciais do Ensino Fundamental, incluindo crianças com diferentes perfis de desenvolvimento e com atenção especial à inclusão de crianças com Transtorno do Espectro Autista.

A proposta não pressupõe a criação de um jogo separado ou de um “modo TEA”. A intenção é que diferentes crianças compartilhem o mesmo núcleo de gameplay, enquanto recursos de acessibilidade e a demanda cognitiva possam ser ajustados conforme necessidades funcionais e desempenho observado.

A escolha do paradigma também não parte da suposição de que crianças autistas possuam uma vantagem geral em Mental Rotation. Meta-análise sobre habilidades visuoespaciais encontrou pequenas vantagens médias em tarefas como *Block Design* e *Figure Disembedding*, mas não identificou diferença clara entre grupos especificamente em Mental Rotation.

Há, por outro lado, estudos de eye-tracking que identificaram, em um subgrupo de crianças autistas muito pequenas, maior atenção relativa a estímulos geométricos dinâmicos quando apresentados ao lado de estímulos sociais. O achado é relevante como contexto para pesquisas sobre atenção visual no TEA, mas não pode ser generalizado para todas as crianças autistas e não implica melhor habilidade de rotação mental.

---

## 3. OBJETIVO TERAPÊUTICO E EDUCACIONAL

### 3.1. Objetivo Terapêutico Principal

O jogo **não possui objetivo terapêutico definido nesta etapa**.

Seu objetivo principal é científico e avaliativo: desenvolver uma implementação gamificada e adaptável do paradigma de Mental Rotation capaz de provocar e registrar decisões baseadas em transformação visuoespacial dentro de uma experiência adequada ao público infantil.

No paradigma clássico de Shepard e Metzler, o participante deve reconhecer se duas representações correspondem à mesma estrutura tridimensional observada em diferentes orientações. O estudo original encontrou aumento do tempo necessário para reconhecer objetos equivalentes conforme aumentava a diferença angular entre suas orientações.

A proposta deste jogo não consiste em reproduzir diretamente o teste clássico, mas em incorporar sua operação cognitiva central a uma mecânica que possua significado dentro de um contexto lúdico.

---

### 3.2. Objetivos Secundários

O jogo pretende:

* preservar Mental Rotation como operação necessária para solucionar a mecânica principal;
* permitir coleta futura de indicadores comportamentais relacionados ao desempenho na tarefa, sem atribuir significado clínico automaticamente a esses dados;
* reduzir dependência de alfabetização, linguagem verbal, percepção cromática e habilidade motora fina;
* permitir adaptação da demanda cognitiva conforme o comportamento observado;
* preservar uma experiência positiva mesmo quando a criança apresenta dificuldades;
* sustentar motivação por meio da construção e transformação persistente do reino;
* oferecer escolhas lúdicas que permitam autonomia sem modificar necessariamente o núcleo científico;
* permitir que crianças com diferentes perfis utilizem o mesmo universo de jogo.

A gamificação será tratada como parte relevante da própria implementação, e não como camada metodologicamente neutra. Em um estudo com 100 crianças de 6 a 9 anos, uma versão gamificada de uma tarefa de Mental Rotation produziu desempenho diferente da versão básica, reforçando a necessidade de validar a implementação resultante como instrumento próprio.

---

### 3.3. Fora do Escopo

Nesta etapa, o jogo:

* não diagnostica TEA;
* não constitui instrumento clínico validado;
* não substitui avaliação neuropsicológica;
* não pretende medir capacidade intelectual;
* não pressupõe uma habilidade visuoespacial característica de todas as pessoas autistas;
* não possui eficácia terapêutica estabelecida;
* não pretende ensinar formalmente geometria;
* não atribui significado clínico isolado a acertos, erros ou tempos de resposta;
* não considera diagnóstico suficiente para determinar automaticamente o nível de dificuldade de um jogador.

---

## 3A. GUIDING PRINCIPLES

Os Guiding Principles abaixo utilizam apenas evidências que podem ser rastreadas ao PPI já realizado. Como esse PPI foi desenvolvido para outro jogo, sua aplicação ao presente projeto permanece como hipótese a ser verificada em ciclos específicos deste protótipo.

| GP                                                | Evidência sintetizada do PPI                                                                                                                                                    | Objetivo de design                                                                                             | Funcionalidades previstas                                                                     |
| ------------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | -------------------------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------- |
| **GP-01 — Instrução concreta e multimodal**       | Participantes com diferentes níveis de suporte apresentaram necessidades distintas de instrução; para alguns, demonstrações visuais e concretas foram consideradas importantes. | Fazer com que a regra possa ser compreendida sem depender de leitura extensa ou explicação verbal abstrata.    | Tutorial visual, demonstração animada, símbolos, áudio complementar e texto simples.          |
| **GP-02 — Configurabilidade sensorial**           | Houve respostas distintas ao áudio, com recomendação convergente de controle de volume e possibilidade de desativá-lo.                                                          | Permitir que características sensoriais não essenciais sejam ajustadas.                                        | Controle de áudio, possibilidade de reduzir efeitos e animações não essenciais.               |
| **GP-03 — Progressão com baixa punição**          | Participantes demonstraram respostas distintas ao aumento da dificuldade, incluindo irritabilidade e frustração com desempenho ou pontuação.                                    | Evitar que dificuldade cognitiva produza perda significativa de progresso ou sensação persistente de fracasso. | Feedback positivo, ausência de perda de progresso e regressão de dificuldade sem penalização. |
| **GP-04 — Adaptação à heterogeneidade funcional** | O PPI encontrou diferenças relevantes entre participantes e concluiu que um design uniforme não seria adequado para todos os perfis.                                            | Permitir que a experiência responda ao usuário, em vez de presumir um perfil único associado ao diagnóstico.   | Ajustes de acessibilidade e adaptação de dificuldade com base na interação e no desempenho.   |
| **GP-05 — Controle da carga visual**              | O PPI recomendou evitar excesso de elementos simultâneos, pois elementos decorativos adicionais poderiam competir pela atenção.                                                 | Manter o momento experimental visualmente legível sem empobrecer o restante do mundo.                          | Redução de movimento e estímulos não essenciais durante a comparação das peças.               |
| **GP-06 — Possibilidade de interrupção**          | O PPI mostrou que sinais de desconforto e necessidade de interrupção variam entre indivíduos.                                                                                   | Não obrigar a criança a permanecer em uma atividade quando deseja interrompê-la.                               | Pausa e saída acessíveis, sem penalização por interrupção voluntária.                         |

### Princípios de Design específicos deste jogo

Como nem todas as decisões do projeto têm origem no PPI, são registrados separadamente os seguintes **Design Principles (DP)**:

| DP        | Princípio                                                                                                                                           |
| --------- | --------------------------------------------------------------------------------------------------------------------------------------------------- |
| **DP-01** | A rotação mental deve ser necessária para responder; o jogador não poderá rotacionar livremente o objeto durante o trial.                           |
| **DP-02** | A cor poderá compor a estética, mas não deverá determinar a resposta experimental.                                                                  |
| **DP-03** | Personagens podem sustentar a narrativa, mas comportamento social, expressão ou personalidade não poderão fornecer pistas sobre a resposta correta. |
| **DP-04** | Desempenho científico e progresso lúdico não serão equivalentes: maior dificuldade na tarefa não deve impedir a criança de desenvolver o reino.     |
| **DP-05** | Construções devem modificar a experiência e desbloquear atividades, não funcionar apenas como indicadores abstratos de progresso.                   |
| **DP-06** | A adaptação deverá responder ao desempenho e às necessidades funcionais, não ao diagnóstico de forma isolada.                                       |

---

## 4. MECÂNICAS CORE — MDA: MECHANICS

### 4.1. Mecânica Principal — Inspeção de Peças

**Descrição:**

A mecânica principal será composta por sucessivas **tentativas experimentais (trials)** contextualizadas pela construção do reino. Um habitante chega trazendo uma peça que acredita pertencer à construção atualmente selecionada. O jogador consulta o **Grande Livro de Projetos**, no qual uma única peça necessária é destacada, e deve decidir se a peça apresentada corresponde à mesma estrutura observada em outra orientação ou se possui uma estrutura diferente.

Embora uma construção possa exigir diversas peças, cada tentativa terá **uma referência ativa e um único candidato**. O jogador não deverá procurar entre várias referências possíveis durante a tentativa, pois isso acrescentaria busca visual e seleção entre alternativas ao núcleo de Mental Rotation.

O fluxo da mecânica principal será:

**habitante chega**  
→ **apresenta o contexto da construção**  
→ **uma peça necessária é destacada no projeto**  
→ **referência e candidato aparecem simultaneamente**  
→ **jogador realiza a comparação visuoespacial**  
→ **seleciona “serve” ou “não serve”**  
→ **o sistema registra a primeira resposta e o tempo de resposta**  
→ **ocorre a consequência narrativa**  
→ **streak, pontos e progresso da construção são atualizados**

Uma peça incompatível não significa necessariamente que o personagem tentou enganar o jogador. Ela pode ter sido confundida durante o transporte, fabricada de forma incorreta, destinada a outra construção ou simplesmente possuir estrutura semelhante à peça procurada.

**Justificativa científica:**

A apresentação simultânea de uma referência e um candidato preserva uma estrutura de comparação compatível com tarefas de Mental Rotation. A referência permanecerá em orientação padronizada, enquanto o candidato poderá ser apresentado em diferentes disparidades angulares. Os estímulos mantêm a **rotação do conjunto original**: o candidato gira **45°, 90°, 135°, 180° ou 225°** em profundidade, em torno do eixo **x, y ou z** do estímulo, e é visto pela mesma câmera elevada da referência; 225° é 135° no sentido contrário (LG-Gab-01, conforme a implementação do aluno). A v1.3 pedia também 0° e os dois sentidos em 45° e 90°, que o conjunto original não tem; acrescentá-los muda a sequência fixa de cada obra e é pendência (§13). Para análise, serão registrados o ângulo do estímulo, o eixo, o sentido e a magnitude absoluta da disparidade angular (0° a 180°).

A distribuição das tentativas deverá buscar equilíbrio entre **MATCH** e **NON_MATCH**, inicialmente com aproximadamente 50% de cada condição, evitando que a frequência das respostas se torne uma pista para o jogador. Cada obra tem uma **sequência fixa de 10 peças**, com 5 `MATCH` e 5 `NON_MATCH` e cada ângulo uma vez em cada condição, apresentadas sempre na mesma ordem (conforme a implementação do aluno).

Os estímulos terão aparência lúdica e coerente com o universo visual do reino, mas deverão ser produzidos ou adaptados especificamente para a tarefa. Detalhes decorativos assimétricos que revelem diretamente a orientação — como símbolos, cores ou marcas exclusivas em uma face — não deverão ser utilizados. Ao mesmo tempo, a própria estrutura não poderá ser excessivamente simétrica a ponto de tornar diferentes orientações indistinguíveis.

Os candidatos `NON_MATCH` deverão permanecer estruturalmente próximos da referência. Sempre que adequado, poderão ser utilizadas versões espelhadas ou alterações estruturais controladas, reduzindo a possibilidade de solucionar a tarefa apenas pela identificação de uma característica local evidente. No conjunto original, todo `NON_MATCH` é a versão espelhada da peça (conforme a implementação do aluno).

**Metadados mínimos do estímulo:**

- identificador da referência;
- identificador do candidato;
- condição `MATCH` ou `NON_MATCH`;
- orientação de referência;
- orientação do candidato;
- disparidade angular;
- sentido da rotação;
- `complexity_score`;
- medida de similaridade do distrator.

**Guiding Principle:** decisão de design específica — DP-01; interface relacionada também a GP-01 e GP-05.

**Hipótese de design:**

Inserir a comparação espacial em uma tarefa funcional do reino pode fazer com que Mental Rotation seja percebido como parte da própria jogabilidade, reduzindo a sensação de repetição de um teste convencional sem retirar o controle sobre os parâmetros experimentais.

**Origem PPI:** não derivada do PPI. Mecânica fundamentada no paradigma científico e no processo de design deste jogo.

---

### 4.2. Sistema de Feedback, Streak e Pontuação

Quando a criança classifica corretamente uma peça, ela funciona de acordo com o projeto e pode ser incorporada ao processo de construção. O feedback deverá enfatizar a consequência dentro do mundo, evitando mensagens punitivas ou avaliações pessoais como “errado”, repreensão dos personagens ou perda de progresso.

Quando a classificação estiver incorreta, a peça simplesmente não funcionará para aquela finalidade. A criança não perde construções, moedas já obtidas ou pontos conquistados anteriormente.

Os quatro resultados e a consequência de cada um (LG-Gab-02, 03, 08 e 09; a contagem e o que vem depois do erro, conforme a implementação do aluno):

| Peça | Resposta | O que acontece | Conta para a construção |
| --- | --- | --- | --- |
| `MATCH` | serve | a peça encaixa no projeto (ícone de encaixe) | sim |
| `NON_MATCH` | não serve | recusa correta: a peça não é desta obra, e a obra segue para a peça seguinte da sequência | não |
| `MATCH` | não serve | erro: o habitante mostra a mesma peça de novo (ícone de volta) | não |
| `NON_MATCH` | serve | erro: a peça não encaixa; a fala dá uma de quatro explicações em ordem fixa (confusão, fabricação, transporte, semelhança), e a mesma peça volta | não |

Uma construção fecha com **5 peças**, e só o `MATCH` aceito soma; como a sequência da obra tem 5 `MATCH` e só avança com a resposta certa, a obra fecha no 10º estímulo. O erro não soma nem tira, e depois dele **a mesma peça volta**, até ser respondida certo; a tentativa registra a primeira resposta, o tempo dela e quantas vezes a peça foi apresentada (conforme a implementação do aluno). As falas têm até 8 palavras. No acerto, uma placa no alto mostra a streak (chama), os pontos (estrela) e o bônus, quando houver; no erro não aparece nada, nem “+0” nem a streak zerada. O saldo de moedas fica na Vila.

### 4.2.1. Streak

A **streak** representa exclusivamente acertos consecutivos:

- acerto: `streak + 1`;
- erro: `streak = 0`.

O tempo de resposta não reduz nem encerra a streak. Dessa forma, uma resposta correta, ainda que lenta, continua sendo tratada como correta.

### 4.2.2. Pontuação

Cada resposta correta concederá inicialmente **100 pontos-base**, aos quais será aplicado um multiplicador provisório de streak:

| Streak atual | Multiplicador inicial |
| ---: | ---: |
| 1–2 | ×1,00 |
| 3–4 | ×1,25 |
| 5–7 | ×1,50 |
| 8 ou mais | ×2,00 |

Os valores são parâmetros iniciais de balanceamento e deverão permanecer configuráveis.

### 4.2.3. Bônus de Agilidade

O jogo deverá incentivar a criança a responder assim que tiver encontrado a solução, mas sem utilizar cronômetro visível, contagem regressiva, barra de tempo, mensagens de urgência ou punição por demora.

Uma resposta correta poderá receber um **Bônus de Agilidade**, inicialmente limitado a **0–20 pontos adicionais**. O bônus:

- somente poderá ser concedido para respostas corretas;
- não interfere na streak;
- não reduz pontuação quando não obtido;
- não será apresentado como punição;
- não deverá utilizar um único limite absoluto de tempo para todas as tentativas;
- deverá considerar a dificuldade da tentativa e, futuramente, o padrão individual de resposta.

Os limiares temporais do bônus **não serão definidos antes do piloto**, e até lá o bônus fica **desligado**, com uma chave no painel do mediador; a regra provisória usa a mediana do RT dos acertos no mesmo ângulo (LG-Gab-10). A mecânica poderá ser implementada de forma parametrizável, mas a associação entre tempo e recompensa precisará ser calibrada empiricamente para reduzir o risco de incentivar uma estratégia excessivamente rápida em detrimento da acurácia.

### 4.2.4. Pontos e moedas

Os pontos representam o desempenho obtido durante o dia de jogo. Ao anoitecer, a pontuação acumulada será convertida em moedas. O dia fecha depois de **10 tentativas válidas** (o número exato é pendência do piloto) ou no fim da sessão; a tela de anoitecer mostra os pontos virando moedas e o saldo, e a sobra que não completa uma moeda não vira moeda (LG-Gab-11).

A relação inicial de implementação será:

**100 pontos = 1 moeda**

Esse valor permanecerá configurável durante o balanceamento.

As moedas serão utilizadas para **decoração e personalização da vila**, incluindo árvores, bancos, lagos, estradas, vegetação e outros elementos de cenário. Os preços iniciais vão de 1 a 5 moedas por item, e tirar um enfeite devolve o preço inteiro (LG-Gab-19). As construções funcionais principais não serão compradas com moedas; seu progresso ocorrerá por meio da gameplay de seleção de peças.

**Justificativa científica e de acessibilidade:**

O PPI registrou frustração relacionada ao aumento da dificuldade e à percepção de pontuação baixa em alguns participantes, recomendando enquadramento mais positivo do desempenho.

**Guiding Principle:** GP-03; DP-04.

**Hipótese de design:**

Recompensar acertos consecutivos e permitir um bônus secundário por respostas corretas ágeis pode sustentar o ritmo de jogo sem transformar tempo em uma condição de sucesso, desde que a influência da recompensa sobre a estratégia de resposta seja posteriormente avaliada.

**Origem PPI:** GP-03 deriva do PPI; streak, multiplicadores, Bônus de Agilidade e conversão de pontos são decisões específicas do presente jogo.

> **Ponto de validação:** feedback e recompensas podem modificar a estratégia utilizada na tarefa. O efeito da streak, do Bônus de Agilidade e da consequência narrativa deverá ser avaliado em piloto e em etapas posteriores de validação.

---

### 4.3. Sistema de Progressão

A progressão será dividida em três dimensões independentes.

#### Progressão lúdica

O jogador desenvolve o reino, desbloqueia construções, encontra personagens e acessa novas experiências. Essa progressão deverá depender principalmente da participação no jogo, e não de desempenho cognitivo elevado.

As construções principais serão desenvolvidas pela mecânica de seleção de peças. As moedas obtidas a partir dos pontos serão utilizadas para personalização e decoração, evitando que baixo desempenho impeça a progressão estrutural do reino.

#### Adaptação da demanda cognitiva

A adaptação utilizará inicialmente **acurácia** e **tempo de resposta** como indicadores principais. O sistema poderá manipular:

- magnitude da rotação;
- complexidade estrutural;
- similaridade do distrator.

A lógica inicial será:

**desempenho consistentemente confortável** → aumentar gradualmente a demanda;  
**desempenho compatível com desafio adequado** → manter parâmetros;  
**erros recorrentes combinados a tempos de resposta elevados** → reduzir gradualmente a demanda.

A adaptação ocorrerá de forma invisível para a criança. Não serão exibidos rótulos como “fácil”, “médio”, “difícil”, “nível 1” ou “regressão”.

Os limiares quantitativos e a janela de desempenho utilizada pelo algoritmo ainda dependem de piloto e deverão permanecer configuráveis.

**Ponto de partida e ordem (LG-Gab-06, provisório até o piloto):** a criança começa no estado fácil (ângulo de até 45°, complexidade baixa, menor similaridade do distrator), registrado como `a45-cLOW-s1`. Os três eixos têm os degraus ângulo 45 → 90 → 135 → 180°, complexidade `LOW` → `MID` → `HIGH` e similaridade 1 → 2 → 3. Janela de 8 tentativas válidas (`COMPLETED`); o histórico guarda o RT das últimas 40 tentativas válidas. **Sobe um degrau** com acurácia ≥ 85% na janela e RT mediano da janela ≤ a mediana do histórico; o eixo que sobe vem **em rodízio** — ângulo, complexidade, similaridade e de novo o ângulo —, a partir do eixo que subiu por último (o primeiro aumento é o do ângulo), pulando o eixo que já está no topo. **Desce um degrau** com acurácia ≤ 50% e RT mediano ≥ 1,25× a mediana do histórico, desfazendo o último aumento (o estado inicial não desce). Senão, mantém. Depois de subir ou descer, a janela recomeça vazia. O estado de adaptação (degraus, pilha dos aumentos, janela e histórico) é do reino da criança. Regra do protótipo validado, decidida pelo orientador em 26/11/2026. Como cada obra apresenta a sua sequência fixa de 10 (§4.1, conforme a implementação do aluno), até a pendência “Adaptação e sequência fixa” (§13) ser decidida o jogo registra o estado de adaptação recebido (`difficulty_state`), sem escolher a peça seguinte por ele.

**Execução da regra (v1.5, padrão CIATEC, decidido pelo orientador em 26/11/2026):** a regra desta seção é executada pelo **backend, via API**, e não pelo jogo. O jogo envia ao backend o registro de cada tentativa (acurácia, tempo de resposta, status e os metadados do estímulo, §11.4) e recebe os parâmetros da próxima tentativa (ângulo, complexidade, similaridade do distrator e o `difficulty_state`); aplica-os e grava o `difficulty_state` recebido. Enquanto a API não existe, um provedor local devolve sempre o estado inicial `a45-cLOW-s1`. A regra acima continua sendo a especificação: é o que o backend executa.

#### Adaptação de acessibilidade

Características que não fazem parte do construto, como volume, intensidade de animações, quantidade de estímulos ambientais e apoio multimodal, poderão ser ajustadas sem que isso seja tratado como avanço ou regressão cognitiva.

**Guiding Principles:** GP-02, GP-03, GP-04; DP-04 e DP-06.

**Hipótese de design:**

Separar progressão lúdica, dificuldade cognitiva e acessibilidade deverá permitir que o jogo responda às necessidades individuais sem transformar diferenças funcionais em uma hierarquia explícita de capacidade.

---

### 4.4. Onboarding Progressivo

O onboarding não será concentrado em um tutorial longo ou obrigatório. Na primeira entrada, o jogo apresentará uma animação curta demonstrando apenas o princípio necessário para iniciar a mecânica principal.

A sequência inicial deverá:

1. apresentar uma estrutura;
2. mostrar essa estrutura girando;
3. demonstrar visualmente que ela continua sendo a mesma peça apesar da mudança de orientação;
4. apresentar uma estrutura realmente diferente;
5. conduzir a criança por uma tentativa assistida.

Nos passos 1 a 4 a criança gira a peça à mão; no passo 5, não (é uma tentativa de treino, `TRAINING`, que não pontua nem entra na adaptação). As peças do tutorial são fixas. O botão **pular** fica à mostra desde o primeiro passo; o menu oferece **rever tutorial** quando há reino salvo, e um reino novo da mesma criança herda o tutorial já visto, que é guardado por código de criança (LG-Gab-13, G3).

A rotação explícita é apropriada durante o treinamento porque o objetivo, nesse momento, é ensinar a regra. Durante as tentativas válidas, porém, o jogador não poderá rotacionar livremente a referência ou o candidato, pois isso externalizaria parte da transformação que se pretende observar.

Depois que a criança puder jogar, novas funcionalidades serão ensinadas por **microtutoriais contextuais**, apresentados apenas quando forem desbloqueadas. A Vila, decoração, interiores de construções e outras funcionalidades não precisarão ser explicadas na primeira sessão.

O onboarding deverá combinar demonstração visual, símbolos, áudio complementar e texto simples, sem tornar a leitura obrigatória.

**Guiding Principle:** GP-01; DP-01.

**Hipótese de design:**

Ensinar somente a informação necessária no momento em que ela passa a ser utilizada poderá reduzir carga inicial e permitir que a criança comece a jogar rapidamente, mantendo os conteúdos secundários distribuídos ao longo da progressão.

---

## 5. DINÂMICAS — MDA: DYNAMICS

### 5.1. Arco de uma Sessão Típica e Loop Principal

Uma sessão deverá alternar momentos de narrativa, comparação visuoespacial, progressão e exploração. A duração total ainda não está definida e deverá ser determinada em protótipo e piloto. A sessão fecha sozinha em **10 tentativas válidas**, valor provisório que o mediador ajusta no painel (5, 10, 20, 30 ou sem limite); o número definitivo é do protocolo.

O loop principal será:

**NPC chega**  
→ **apresenta o contexto da construção**  
→ **Grande Livro de Projetos destaca uma única peça necessária**  
→ **referência e candidato são preparados**  
→ **tentativa de Mental Rotation**  
→ **registro da resposta**  
→ **consequência narrativa**  
→ **atualização de streak e pontos**  
→ **atualização do progresso da construção**  
→ **próximo NPC ou retorno à Vila**

Cada tentativa válida será composta pelos seguintes estados:

```text
TRIAL_SETUP
    ↓
TRIAL_ACTIVE
    ↓
RESPONSE_RECEIVED
    ↓
FEEDBACK
    ↓
TRIAL_END
```

**TRIAL_SETUP:** o sistema prepara a peça seguinte da sequência da obra, com referência, candidato, condição `MATCH/NON_MATCH`, ângulo, eixo e sentido da rotação (conforme a implementação do aluno), e registra a complexidade e a similaridade do distrator. Nenhum tempo de resposta é registrado nessa etapa.

**TRIAL_ACTIVE:** referência, candidato e controles de resposta aparecem integralmente. O cronômetro interno começa somente quando os dois estímulos estiverem completamente disponíveis e os controles de resposta estiverem habilitados. Não deverá existir animação de entrada dos estímulos após o início da medição.

**RESPONSE_RECEIVED:** a primeira resposta válida encerra a medição do tempo de resposta. Respostas posteriores não modificam o dado registrado.

**FEEDBACK:** o jogo apresenta a consequência narrativa da escolha e atualiza os elementos de recompensa.

**TRIAL_END:** os dados são armazenados e o fluxo segue para o próximo estado de gameplay.

A criança deverá poder pausar ou encerrar a sessão voluntariamente sem perda de progresso.

**Ritmo do ciclo (LG-Gab-07):** cada tela segue sozinha e um clique adianta: habitante 4 s, Livro 2,5 s, retorno 2,2 s, progresso 1,4 s; com a narração ligada, a tela dura no mínimo a fala.

**Pausa e saída (LG-Gab-05):** o mesmo botão, com o mesmo ícone, no mesmo canto, em todas as telas, inclusive no desafio, **menos no menu e nas configurações**, onde não há o que pausar. A pausa tem RETOMAR, CONFIGURAÇÕES e SALVAR E SAIR, que grava o reino e volta ao menu. **No desafio, a pausa é o botão VILA**, que leva à Vila com a obra parada; lá, RETOMAR volta à mesma peça (conforme a implementação do aluno).

**Ausência de resposta (LG-Gab-21):** aos 45 s sem resposta aparece uma pergunta neutra, com ícone nos dois botões e sem número (continuar ou parar); aos 75 s a tentativa fica `ABANDONED`. Continuar traz o próximo habitante com peça nova; parar fecha a sessão e mostra a Vila, sem tom de falha. O tempo em pausa não conta.

---

### 5.2. Progressão Longitudinal e Construções

O reino deverá persistir entre sessões, permitindo que a criança reconheça construções desenvolvidas anteriormente e observe o impacto acumulado de sua participação.

A primeira versão considera dez construções funcionais principais:

1. Ponte;
2. Fazenda;
3. Moinho;
4. Observatório;
5. Museu;
6. Parque de Diversões;
7. Casas;
8. Laboratório;
9. Ferraria;
10. Mercado.

Quando mais de um projeto estiver disponível, o jogador poderá escolher qual deseja desenvolver. A primeira construção é o **Observatório**, e antes dela nenhuma outra começa (LG-Gab-12, conforme a implementação do aluno). Algumas construções terão dependências:

```text
PONTE
 ├── FAZENDA ───┐
 └── MOINHO ────┴── MERCADO

FERRARIA
 └── PARQUE DE DIVERSÕES

OBSERVATÓRIO, MUSEU, LABORATÓRIO, CASAS
 └── sem dependência
```

Observatório e Museu não têm pré-requisito, como nos dados do jogo (conforme a implementação do aluno); com isso o Laboratório deixou de ser requisito de outra construção, e a função dele é pendência (§13).

As dependências deverão ser representadas como dados configuráveis, e não como regras individuais espalhadas pela implementação.

As funções iniciais previstas são:

- **Ponte:** desbloqueia nova região e possibilita Fazenda e Moinho;
- **Fazenda:** modifica a paisagem e participa da cadeia necessária ao Mercado;
- **Moinho:** construção funcional e requisito do Mercado;
- **Mercado:** amplia a atividade do centro da vila;
- **Laboratório:** função no grafo pendente (§13);
- **Ferraria:** requisito do Parque de Diversões;
- **Observatório:** permite experiências relacionadas à observação astronômica; em determinados dias poderão aparecer Lua, planetas, cometas ou outros corpos celestes, acompanhados de nome e breve descrição;
- **Museu:** permitirá experiências expositivas, inicialmente com possibilidade de conteúdo relacionado a dinossauros;
- **Parque de Diversões:** reserva espaço para experiências lúdicas ou minigames futuros;
- **Casas:** contribuem para crescimento visual e populacional da vila.

A adaptação cognitiva ocorrerá paralelamente a essa progressão, sem ser representada como uma sequência explícita de capacidade ou inteligência.

**Vila e interiores na v1 (LG-Gab-16 e 22; conteúdo provisório, o de cada interior é do pesquisador):** a Vila é maior que a tela e se desloca por setas e arraste, dentro dos limites do mapa (43,75 × 27,08 unidades da cena, conforme a implementação do aluno); a outra margem do rio fica sob neblina até a Ponte. Observatório: 3 de 9 corpos celestes por dia de jogo, com nome e descrição curta; Museu: 5 dinossauros e 2 fósseis; Parque: 2 brinquedos que a criança liga e desliga. Cada construção concluída traz um habitante novo à Vila, até 5; os habitantes têm aparência neutra e constante. O habitante que traz cada peça é sorteado pelo modelo da peça, e a mesma peça traz sempre o mesmo habitante (conforme a implementação do aluno). Com todas as construções prontas, a Vila oferece REINICIAR, que começa um reino novo do mesmo código (conforme a implementação do aluno).

---

### 5.3. Calibração da Demanda Cognitiva

Como não há objetivo terapêutico definido, esta subseção descreve a calibração da **demanda cognitiva**.

As variáveis iniciais de dificuldade serão:

- magnitude da rotação;
- complexidade estrutural;
- similaridade do distrator.

A complexidade estrutural será representada internamente por um parâmetro contínuo:

**`complexity_score = 0.0 a 10.0`**

Para facilitar comunicação e organização dos estímulos:

| Intervalo | Classificação operacional |
| --- | --- |
| 0,0–3,9 | baixa complexidade |
| 4,0–6,9 | média complexidade |
| 7,0–10,0 | alta complexidade |

Essa escala é um instrumento interno de autoria e **não uma medida psicométrica validada**. O cálculo definitivo deverá considerar propriedades mensuráveis do estímulo, como quantidade de componentes, ramificações, mudanças de direção e complexidade perceptiva da silhueta. Os pesos ainda serão definidos após a produção dos primeiros estímulos e testes piloto.

A similaridade do distrator permanecerá como dimensão distinta da complexidade. Assim, uma estrutura simples poderá ter um distrator altamente semelhante, enquanto uma estrutura complexa poderá ter um distrator relativamente distinto.

Quando o sistema identificar dificuldade persistente, combinando erros recorrentes e tempos de resposta elevados em relação ao histórico do próprio jogador, poderá reduzir gradualmente a complexidade, utilizar rotações menos exigentes ou diminuir a similaridade do distrator. Quando o desempenho se mostrar consistentemente confortável, essas dimensões poderão ser aumentadas de maneira gradual.

Os limiares matemáticos, a janela de tentativas considerada e a ordem exata em que as variáveis serão modificadas ainda dependem de piloto.

---

## 6. EXPERIÊNCIA PRETENDIDA — PLAYER EXPERIENCE

### MDA: AESTHETICS

O framework MDA diferencia as regras e sistemas implementados das dinâmicas que emergem durante a interação e da experiência percebida pelo jogador.

Para este jogo, as dimensões mais relevantes são:

**Fantasy:** assumir responsabilidade sobre um reino em construção e acompanhar sua transformação.

**Discovery:** encontrar novos espaços, construções, atividades e habitantes ao longo das sessões.

**Expression:** influenciar o desenvolvimento do reino por meio de escolhas de construção e, futuramente, personalização.

**Challenge:** solucionar comparações visuoespaciais que permaneçam adequadas ao nível atual do jogador, sem pressão temporal explícita.

**Narrative:** reencontrar habitantes, acompanhar pequenas histórias e compreender por que determinada construção é necessária.

**Sensation:** observar um mundo progressivamente mais vivo, com mudanças ambientais, animações e uso das estruturas construídas.

A experiência pretendida é que a criança perceba que está ajudando o reino dela a crescer, e não estar realizando uma bateria de testes.

---

## 7. ELEMENTAL TETRAD

### 7.1. Mechanics — Regras e Sistemas

O loop principal é composto por apresentação de uma peça, consulta ao projeto, comparação mental, decisão e consequência no reino.

Mecânicas complementares incluem:

* construção progressiva;
* recursos e moedas;
* streak de acertos;
* exploração;
* escolha entre possíveis construções;
* desbloqueio de novas atividades;
* adaptação gradual de dificuldade.

**Restrições da população e do paradigma:**

* leitura não pode ser requisito para compreender a mecânica;
* cor não pode revelar a resposta experimental;
* interação motora deverá ser simples;
* não haverá cronômetro visível pressionando a criança;
* a peça não poderá ser livremente rotacionada durante o trial;
* a resposta não poderá depender da interpretação social de um personagem.

---

### 7.2. Story — Narrativa e Contexto

O jogador participa do desenvolvimento de uma pequena comunidade. Habitantes chegam com materiais e peças encontradas, fabricadas ou transportadas para as obras do reino.

Uma peça incorreta não significa necessariamente que o personagem está mentindo. A narrativa poderá explicar incompatibilidades por confusão, erro de fabricação, transporte ou semelhança entre estruturas.

Isso permite que personagens mantenham identidade e personalidade sem criar uma mecânica baseada em suspeita social.

**Restrições da população:**

A narrativa não deverá depender de leitura extensa, compreensão de ironia ou interpretação correta de expressões sociais para que a tarefa principal seja executada.

---

### 7.3. Aesthetics — Visual, Som e Sensação

A direção visual utilizará **pixel art** como linguagem predominante. O jogo combinará duas composições principais.

Na **Vila**, o mundo será representado em perspectiva pseudo-isométrica ou 2.5D construída com assets bidimensionais, permitindo visualizar o conjunto do reino de forma semelhante a jogos de construção com câmera elevada. Não será necessário implementar um ambiente tridimensional navegável. A criança poderá deslocar a área visualizada para observar regiões específicas da vila.

Nas **interações com NPCs**, a apresentação será frontal e bidimensional, com composição de interface inspirada em jogos de inspeção: o personagem apresenta o contexto e, no momento da comparação, referência e candidato passam a ocupar o foco principal da tela.

Durante `TRIAL_ACTIVE`, elementos decorativos animados deverão ter sua saliência reduzida, evitando competição visual com os estímulos experimentais.

*Grow Island* pode ser utilizado apenas como **referência visual exploratória** para simplicidade de leitura do cenário e progressão de um espaço que se transforma, sem que isso seja tratado como evidência de adequação ao público infantil.

A cor poderá contribuir amplamente para a estética, mas não poderá identificar a resposta correta. Diferenças cromáticas entre referência e candidato podem alterar a dificuldade da discriminação, portanto a estrutura geométrica deverá permanecer como informação relevante.

O áudio deverá enriquecer ambientação e feedback, mas poderá ser reduzido ou desativado. Na v1: duas músicas, sete efeitos, nenhum som durante a comparação, e narração das falas por voz sintética provisória (LG-Gab-20).

**Restrições da população:**

- contraste visual adequado;
- controle de volume;
- ausência de informação indispensável exclusivamente sonora;
- redução de estímulos simultâneos durante a tarefa;
- efeitos ambientais configuráveis quando possível;
- detalhes decorativos não podem funcionar como pistas para a orientação dos estímulos.

---

### 7.4. Technology — Plataforma, Implementação e Assets

**Plataforma:** Unity (decidido pelo orientador em 26/11/2026).

**Builds:** Windows (v1.5; WebGL e Android saíram, “pra simplificar”).

**Dispositivo-alvo:** PC com Windows 10/11, tela de 1366×768 a 1920×1080, mouse e teclado (decidido pelo orientador em 26/11/2026, v1.5).

**Modo:** single player.

**Interação:** seleção discreta por interface, com mouse e teclado.

A especificação lógica deverá permanecer independente da engine sempre que possível. Definição das tentativas, parâmetros dos estímulos, streak, pontuação, dependências de construções, telemetria, configurações e progressão deverão ser representados como dados e regras configuráveis.

A tecnologia escolhida deverá permitir registrar de forma consistente as condições apresentadas em cada tentativa e caracterizar adequadamente a precisão temporal caso o tempo de resposta seja utilizado em análise científica.

---

## 8. MAPEAMENTO LM-GM

O modelo LM-GM propõe mapear objetivos de aprendizagem ou avaliação às mecânicas que efetivamente os operacionalizam, evitando que o conteúdo científico fique separado da experiência de jogo.

| Objetivo científico / educacional     | Mecânica de jogo                                     | Como operacionaliza                                                                         | Princípio     |
| ------------------------------------- | ---------------------------------------------------- | ------------------------------------------------------------------------------------------- | ------------- |
| Evocar transformação visuoespacial    | Comparação entre projeto e peça                      | O jogador precisa decidir se duas representações correspondem à mesma estrutura sob rotação | DP-01         |
| Evitar dependência de leitura         | Tutorial demonstrativo e interface pictográfica      | A regra pode ser aprendida observando exemplos e ações                                      | GP-01         |
| Reduzir interferência sensorial       | Configurações de áudio e foco visual durante o trial | Estímulos não essenciais podem ser reduzidos sem alterar a regra                            | GP-02 / GP-05 |
| Manter engajamento diante do erro     | Progressão sem perda acumulada                       | Erros encerram a streak, mas não destroem o progresso do reino                              | GP-03 / DP-04 |
| Ajustar demanda ao indivíduo          | Progressão adaptativa                                | Dificuldade pode diminuir diante de erros persistentes e respostas demoradas                | GP-04 / DP-06 |
| Sustentar motivação longitudinal      | Construção persistente                               | Trials contribuem para mudanças permanentes no mundo                                        | DP-05         |
| Favorecer autonomia                   | Escolha de construções e exploração                  | O jogador influencia o desenvolvimento do próprio reino                                     | DP-05         |
| Evitar demanda social não relacionada | Separação entre interação com NPC e comparação       | Personagem contextualiza a tarefa, mas não fornece a resposta                               | DP-03         |
| Incentivar ritmo sem pressão explícita | Bônus de Agilidade secundário | Respostas corretas ágeis podem receber bônus pequeno sem alterar a streak ou impor cronômetro | GP-03 / DP-04 |
| Transformar desempenho em personalização | Conversão de pontos em moedas | Pontos do dia são convertidos em recurso usado apenas para decoração | DP-04 / DP-05 |

---

## 9. PERSONAS E REQUISITOS DERIVADOS

As personas abaixo são perfis funcionais sintetizados a partir do PPI disponível. Como o PPI investigou outro jogo, elas são consideradas **personas contextuais provisórias** e deverão ser revisadas após testes específicos deste protótipo.

### 9.1. Persona 1 — Criança com maior autonomia digital

**Identificação funcional:** criança com TEA com boa autonomia em dispositivos digitais e capacidade de compreender regras simples após pouca ou nenhuma demonstração.

**Ciclo PPI de origem:** PPI TEA v1.0 — Fase 1.

**Contexto de uso:** no PPI original, participantes S1 utilizaram tecnologia com maior autonomia e compreenderam rapidamente a mecânica apresentada.

**Requisitos derivados:**

* não obrigar tutorial longo quando a criança já demonstra compreensão;
* oferecer acesso rápido ao gameplay;
* permitir aumento gradual de desafio sem retirar possibilidade de ajuste;
* manter personalização sensorial disponível mesmo para usuários autônomos.

**Guiding Principles relacionados:** GP-01, GP-02, GP-04.

**Impacto nas decisões:** Seções 4, 5, 10 e 11.

**Status de validação:** válida como perfil contextual do PPI; ainda não validada no jogo de Mental Rotation.

---

### 9.2. Persona 2 — Criança que se beneficia de instrução concreta e maior controle de frustração

**Identificação funcional:** criança com necessidade maior de demonstração visual e que pode apresentar frustração diante de dificuldade ou pontuação percebida como baixa.

**Ciclo PPI de origem:** PPI TEA v1.0 — Fase 1.

**Contexto de uso:** o PPI registrou recomendação de instrução visual mais concreta para o participante S2 e elevada competitividade/frustração com pontuação em uma sessão.
**Requisitos derivados:**

* tutorial demonstrativo disponível;
* feedback positivo e compreensível;
* evitar perda significativa de progresso após erro;
* possibilidade de diminuir a demanda quando a dificuldade estiver persistentemente elevada.

**Guiding Principles relacionados:** GP-01, GP-03, GP-04.

**Impacto nas decisões:** Seções 4, 5, 10 e 11.

**Status de validação:** válida como perfil contextual; requer avaliação específica neste jogo.

---

### 9.3. Persona 3 — Criança com necessidade elevada de suporte e redução de estímulos

**Identificação funcional:** criança que pode necessitar de maior mediação para compreender uma atividade nova e apresentar maior sensibilidade à complexidade da interação ou do ambiente.

**Ciclo PPI de origem:** PPI TEA v1.0 — Fase 1.

**Contexto de uso:** participantes S3 do PPI original necessitaram de mediação intensa. É importante ressaltar que parte dessa dificuldade estava diretamente associada à abstração da captura de movimento e, portanto, não pode ser automaticamente transferida para uma tarefa baseada em seleção simples.

**Requisitos derivados transferíveis:**

* fornecer demonstração clara antes da atividade;
* reduzir estímulos simultâneos durante o núcleo da tarefa;
* permitir interrupção e pausa sem penalização;
* manter configurações sensoriais acessíveis.

**Guiding Principles relacionados:** GP-01, GP-02, GP-05, GP-06.

**Impacto nas decisões:** Seções 4, 10 e 11.

**Status de validação:** válida apenas como perfil de referência contextual; a adequação deste jogo a participantes com necessidade elevada de suporte ainda precisa ser investigada.

---

## 10. INTERFACE E ACESSIBILIDADE

### 10.1. Princípios de Interface

A interface deverá procurar remover barreiras que não fazem parte do construto de Mental Rotation.

A compreensão da tarefa não deverá depender exclusivamente de leitura, áudio, cor ou precisão motora. Durante os trials, a interface deverá favorecer a percepção das estruturas que realmente precisam ser comparadas, enquanto elementos narrativos e ambientais assumem menor saliência.

Essa abordagem se aproxima dos princípios de UDL, que recomendam múltiplos meios de representação, e do design centrado no ser humano descrito pela ISO 9241-210:2019.

---

### 10.2. Requisitos de Acessibilidade por Domínio

**Visual:**

* contraste suficiente entre peças e fundo;
* objetos grandes o bastante para inspeção confortável;
* referência e candidato claramente identificáveis;
* ausência de cor como única informação necessária;
* redução de movimento e elementos competitivos durante a comparação;
* evitar detalhes locais que permitam solucionar a tarefa sem transformação mental.

Mental Rotation permanece, contudo, uma tarefa essencialmente visuoespacial. A versão atual não deverá ser apresentada como plenamente acessível a pessoas cegas; uma versão tátil ou baseada em outra modalidade exigiria investigação própria.

**Auditivo:**

* áudio não será indispensável;
* instruções importantes terão equivalente visual;
* volume poderá ser ajustado ou silenciado;
* efeitos sonoros funcionarão como complemento.

**Motor:**

* interação baseada em controles simples;
* áreas de seleção suficientemente grandes: SERVE e NÃO SERVE com ícone e rótulo e pelo menos 2,3 cm no notebook de 11,6"; cada peça ocupa cerca de 37% da altura da tela (LG-Gab-14);
* ausência de necessidade de movimentos rápidos;
* ausência de arraste preciso, desenho ou manipulação motora fina como requisito para a decisão principal.

**Linguístico:**

* português simples;
* textos curtos;
* instruções apoiadas por demonstração;
* mecânica principal executável por criança ainda não plenamente alfabetizada.

**Social e cognitivo:**

Habitantes serão importantes para a identidade do mundo, mas nenhuma resposta dependerá de expressão facial, contato visual, emoção, direção do olhar, tom de voz ou percepção de mentira.

Meta-análise de estudos de eye-tracking encontrou diferenças médias na distribuição da atenção para estímulos sociais em participantes autistas, especialmente em cenas de maior conteúdo social, embora revisões também ressaltem que essas diferenças são dependentes do contexto e não universais.

Assim, a decisão não é remover pessoas do jogo, mas impedir que processamento social seja necessário para solucionar Mental Rotation.

**Cultural:**

Ainda não há evidência específica do PPI suficiente para definir requisitos culturais deste universo narrativo. Representação visual, linguagem, personagens e referências culturais deverão ser avaliados em ciclos posteriores.

---

### 10.3. Telas, Estados e Fluxo de Navegação

O fluxo inicial será:

```text
MENU (código da criança)
 ├── CONTINUAR          (só com reino salvo)
 ├── JOGAR / NOVO REINO (o novo reino pede confirmação)
 ├── REVER TUTORIAL     (só com reino salvo)
 ├── CONFIGURAÇÕES     (com o volume)
 └── SAIR               (fecha o jogo)
        ↓
CARREGAMENTO
        ↓
APRESENTAÇÃO DA VILA
        ↓
TUTORIAL VISUAL INICIAL
        ↓
CORE GAMEPLAY
```

Após a conclusão da primeira construção, será desbloqueado o acesso regular à **Vila**.

O menu começa pela escolha do **código da criança**: cada código tem o seu reino (LG-Gab-04). Com o código escolhido, o menu mostra CONTINUAR (só com reino salvo), JOGAR ou NOVO REINO (este pede confirmação, porque apaga o reino atual; o tutorial já visto continua visto), REVER TUTORIAL (só com reino salvo) e CONFIGURAÇÕES; sem código, só CONFIGURAÇÕES. O adulto abre o **painel do mediador** por Ctrl+Shift+M ou pelo botão discreto **“adulto”** do menu (os três toques no canto saíram na v1.5, com o PC como único dispositivo); nele vê a sessão, encerra-a por desconforto, protocolo ou outro motivo, com o motivo gravado, liga o Bônus de Agilidade e ajusta o limite de tentativas da sessão (LG-Gab-15). O painel do pesquisador, que era uma aba desse mesmo painel, saiu na v1.5 com a exportação (§11.4). O menu tem SAIR, que fecha o jogo; no WebGL e no Android o jogo se fecha pelo navegador ou pelo sistema, e o SALVAR E SAIR da pausa grava o reino e volta ao menu (decidido pelo orientador em 26/11/2026). As **configurações** são do aparelho e valem para todo reino: música 0,6, efeitos 0,8, voz 1, menos animação, menos ambiente, alto contraste, ampliação das peças até 1,5× e leitura das telas por voz (LG-Gab-17).

Os estados principais previstos são:

```text
MENU
CONFIGURATIONS
LOAD_GAME
INTRO
TUTORIAL
NPC_DIALOG
PROJECT_VIEW
TRIAL_SETUP
TRIAL_ACTIVE
RESPONSE_RECEIVED
FEEDBACK
CONSTRUCTION_PROGRESS
VILLAGE_VIEW
BUILDING_INTERIOR
DECORATION_MODE
PAUSE
```

O fluxo principal da mecânica será:

```text
NPC_DIALOG
    ↓
PROJECT_VIEW
    ↓
TRIAL_SETUP
    ↓
TRIAL_ACTIVE
    ↓
RESPONSE_RECEIVED
    ↓
FEEDBACK
    ↓
CONSTRUCTION_PROGRESS
    ↓
NPC_DIALOG ou VILLAGE_VIEW
```

Na Vila:

```text
VILLAGE_VIEW
 ├── observar áreas
 ├── acessar decoração
 ├── selecionar construção
 └── entrar em construções disponíveis
          ↓
     BUILDING_INTERIOR
```

Os tutoriais posteriores deverão funcionar preferencialmente como camadas contextuais sobre os estados existentes, em vez de interromper o jogo com sequências obrigatórias extensas.

Wireframes e organização espacial definitiva das telas ainda serão produzidos em etapa posterior.

---

## 11. PARÂMETROS DE FASE E PROGRESSÃO

### 11.1. Variáveis de Gameplay e Experimentais

| Variável | Tipo / valores iniciais | Função |
| --- | --- | --- |
| **`match_condition`** | `MATCH` / `NON_MATCH` | Define se referência e candidato correspondem à mesma estrutura |
| **`rotation_angle`** | 45°, 90°, 135°, 180° (o 225° do conjunto original entra como 135° no sentido contrário; o 0° é pendência, §13) | Magnitude angular inicial |
| **`rotation_direction`** | CW / CCW | Sentido da rotação para ângulos aplicáveis |
| **`complexity_score`** | 0,0–10,0 | Complexidade estrutural operacional |
| **`distractor_similarity`** | parametrizável | Similaridade entre referência e candidato `NON_MATCH` |
| **`reaction_time_ms`** | inteiro ≥ 0 | Tempo entre ativação completa do trial e primeira resposta válida |
| **`accuracy`** | booleano | Indica acerto ou erro |
| **`streak`** | inteiro ≥ 0 | Quantidade de acertos consecutivos |
| **`base_points`** | 100 inicialmente | Pontos-base por acerto |
| **`streak_multiplier`** | 1,00–2,00 inicialmente | Multiplicador definido pela streak |
| **`agility_bonus`** | 0–20 inicialmente | Bônus secundário para resposta correta ágil |
| **`day_points`** | inteiro ≥ 0 | Pontuação acumulada no dia |
| **`coins`** | inteiro ≥ 0 | Recurso utilizado para decoração |
| **`trial_status`** | `COMPLETED`, `PAUSED`, `INTERRUPTED`, `ABANDONED` | Define validade da tentativa |
| **`trial_type`** | `TRAINING` / `VALID` | Distingue treino de tentativa utilizada para análise |
| **Duração da sessão** | a definir | Depende de protótipo e piloto |
| **Quantidade de tentativas** | a definir | Depende de protocolo e tolerabilidade |
| **Tentativas por dia de jogo** | 10 tentativas válidas (provisório; LG-Gab-11) | Afeta economia e ritmo do ciclo dia/noite |

Todos os valores de gameplay deverão permanecer configuráveis durante prototipagem e balanceamento.

### 11.2. Critérios de Progressão, Regressão e Interrupção

| Evento | Critério conceitual atual |
| --- | --- |
| **Aumento da demanda** | Desempenho consistentemente confortável, combinando boa acurácia e tempos de resposta compatíveis com o próprio histórico. Limiar ainda a definir. |
| **Manutenção** | Desempenho compatível com desafio adequado, sem evidência suficiente para modificar a demanda. |
| **Redução da demanda** | Erros recorrentes combinados a tempos de resposta elevados poderão reduzir complexidade, magnitude da rotação e/ou similaridade do distrator. |
| **Interrupção voluntária** | A criança poderá pausar ou encerrar a sessão sem perda de progresso. |
| **Interrupção técnica** | Perda de foco do aplicativo ou evento que comprometa a medição invalida o RT da tentativa. |
| **Ausência prolongada de resposta** | Após limite ainda a ser calibrado, o jogo poderá perguntar de forma neutra se a criança deseja continuar. |
| **Interrupção por desconforto** | Em uso supervisionado, sinais relevantes de desconforto ou desengajamento justificam encerramento da atividade. Critérios formais dependem do protocolo posterior. |
| **Interrupção do protocolo** | A definir no protocolo de validação. |

Não haverá contagem regressiva visível. O tempo de resposta será registrado internamente.

Aumento, manutenção e redução da demanda são decididos pelo backend, via API (v1.5, §4.3); o jogo aplica os parâmetros recebidos.

### 11.2.1. Validade da tentativa

Cada tentativa terá um dos seguintes estados:

- **`COMPLETED`** — resposta obtida normalmente; RT pode ser utilizado;
- **`PAUSED`** — jogador abriu voluntariamente a pausa durante a tentativa;
- **`INTERRUPTED`** — aplicativo perdeu foco, ocorreu interferência técnica ou o mediador encerrou a sessão pelo painel durante a tentativa;
- **`ABANDONED`** — ausência prolongada de resposta seguida de encerramento da tentativa.

Tentativas `PAUSED`, `INTERRUPTED` e `ABANDONED` não deverão ter seu tempo interpretado como tempo cognitivo de Mental Rotation.

A tentativa `PAUSED` que recebe resposta depois da pausa conta para o jogo, não para a ciência: o acerto soma streak, pontos (sem Bônus de Agilidade) e peça na obra, e o erro zera a streak; ela **não** entra na adaptação, nem na contagem do dia, nem no limite da sessão. Regra do protótipo validado, decidida pelo orientador em 26/11/2026.

### 11.3. Faixas de Fase por Perfil

Não serão estabelecidas faixas de dificuldade fixas com base apenas em diagnóstico ou nível de suporte.

A futura calibração deverá utilizar o desempenho observado e necessidades funcionais. Informações de PPI poderão orientar configurações iniciais de acessibilidade, mas não deverão ser tratadas como equivalentes à capacidade cognitiva individual.

### 11.4. Telemetria mínima por tentativa

Cada tentativa deverá registrar, no mínimo:

```text
session_id
trial_id
trial_type
stimulus_reference_id
stimulus_candidate_id
match_condition
rotation_angle
rotation_direction
complexity_score
distractor_similarity
difficulty_state
response
accuracy
reaction_time_ms
trial_status
streak_before
streak_after
base_points
streak_multiplier
agility_bonus
points_earned
timestamp_start
timestamp_end
```

O `difficulty_state` grava o estado da adaptação recebido do backend (§4.3; o provedor local, até a API) no formato `a<ângulo máximo>-c<LOW|MID|HIGH>-s<1|2|3>`, como `a45-cLOW-s1`, o estado inicial (decidido pelo orientador em 26/11/2026, como no protótipo validado).

Configurações relevantes de acessibilidade, plataforma, dispositivo e versão do jogo deverão estar associadas à sessão quando puderem influenciar a interpretação dos dados.

Os dados ficam **só em arquivo local**, um por sessão, e saem pela exportação do painel do pesquisador; a criança é identificada pelo código escolhido no menu, que é o próprio código de participante da telemetria (LG-Gab-18, decidido pelo orientador em 26/11/2026). Excluir os dados de um código apaga as sessões gravadas e não apaga o reino dele. O envio a servidor é de outro momento.

**Nota da v1.5 (decidido pelo orientador em 26/11/2026):** a exportação pelo painel do pesquisador e a exclusão dos dados de um código saíram do jogo: os dados vão pela telemetria para o backend, e ética, consentimento e dados são tratados fora do jogo (CEP e backend). O jogo grava a telemetria em arquivo local, pronta para o envio ao backend.

---

## 12. REQUISITOS TÉCNICOS CONSOLIDADOS

| Domínio | Requisito | Prioridade | Origem |
| --- | --- | --- | --- |
| Científico | Apresentar exatamente uma referência ativa e um candidato por tentativa | Crítica | Paradigma / DP-01 |
| Científico | Balancear condições `MATCH` e `NON_MATCH` no conjunto de tentativas | Crítica | Paradigma / decisão metodológica |
| Científico | Registrar RT somente após estímulos e controles estarem completamente disponíveis | Crítica | Integridade científica |
| Científico | Registrar apenas a primeira resposta válida | Crítica | Integridade científica |
| Científico | Invalidar RT de tentativas pausadas, interrompidas ou abandonadas | Crítica | Integridade científica |
| Científico | Impedir rotação manual dos estímulos durante tentativa válida | Crítica | DP-01 |
| Científico | Manter tentativas de treinamento separadas das tentativas válidas | Crítica | Integridade científica |
| Científico | Registrar parâmetros efetivamente apresentados em cada tentativa | Crítica | Integridade científica |
| Funcional | Implementar Grande Livro de Projetos como referência narrativa e funcional | Alta | Decisão de design |
| Funcional | Permitir construção e persistência do reino | Alta | DP-05 |
| Funcional | Representar dependências entre construções por dados configuráveis | Alta | Decisão de design |
| Funcional | Construções devem desbloquear experiências ou mudanças perceptíveis do mundo | Alta | DP-05 |
| Feedback | Implementar streak baseada somente em acertos consecutivos | Alta | GP-03 / decisão de design |
| Feedback | Erro reinicia streak sem retirar progresso acumulado | Alta | GP-03 / DP-04 |
| Feedback | Implementar multiplicadores de streak configuráveis | Média | Decisão de design |
| Feedback | Implementar Bônus de Agilidade independente da streak | Média | Decisão de design / hipótese |
| Economia | Converter pontos do dia em moedas com taxa configurável | Média | Decisão de design |
| Economia | Utilizar moedas somente para decoração na primeira versão | Média | DP-04 / DP-05 |
| Acessibilidade | Onboarding não pode depender exclusivamente de leitura | Alta | GP-01 |
| Acessibilidade | Informações essenciais não podem depender exclusivamente de áudio | Alta | GP-02 |
| Acessibilidade | Cor não pode determinar a resposta correta | Crítica | DP-02 / WCAG |
| Acessibilidade | Permitir controle de áudio | Alta | GP-02 |
| Acessibilidade | Reduzir estímulos visuais simultâneos durante a tarefa | Alta | GP-05 |
| Acessibilidade | Não utilizar cronômetro visível ou pressão temporal explícita | Alta | GP-03 / decisão metodológica |
| Motor | Resposta deve exigir interação simples e de baixa precisão | Alta | Decisão de acessibilidade |
| Social | NPC não pode fornecer pistas sobre a resposta pela aparência ou comportamento | Crítica | DP-03 |
| Progressão | Adaptação de dificuldade não pode ser determinada apenas por diagnóstico | Crítica | GP-04 / DP-06 |
| Progressão | Adaptação deverá considerar inicialmente acurácia e RT | Alta | Decisão de design |
| Progressão | Ângulo, complexidade e similaridade do distrator devem ser parametrizáveis | Alta | DP-06 |
| Autonomia | Jogador deve poder pausar ou interromper sem perda de progresso | Alta | GP-06 |
| Interface | Implementar Vila pseudo-isométrica com elementos 2D | Alta | Direção visual |
| Interface | Implementar interação principal de NPC em composição 2D frontal | Alta | Direção visual |
| Assets | Registrar origem, autor e licença de todo asset externo | Alta | Governança técnica |
| Tecnológico | Manter regras centrais independentes da engine sempre que possível | Alta | Decisão atual |
| Tecnológico | Caracterizar precisão temporal da plataforma antes de uso científico do RT | Crítica | Integridade científica |

---

## 13. LACUNAS E DECISÕES PENDENTES

| Lacuna / Decisão pendente | Impacto | Ação necessária | Responsável |
| --- | --- | --- | --- |
| **Posição formal no Triple Diamond** | Campo obrigatório do GLIDE ainda incerto | Confirmar classificação com coordenação do framework | Pesquisador responsável / coordenação |
| **PPI específico do jogo de Mental Rotation** | PPI atual informa população, mas foi conduzido em outro jogo | Realizar ciclo específico com o protótipo | Equipe de pesquisa |
| **Plataforma final** | Afeta interface, desempenho, precisão temporal e estratégia de distribuição | Decidida: Unity, com build para PC (Windows, mouse e teclado) (decidido pelo orientador em 26/11/2026; WebGL e Android saíram na v1.5) | — |
| **Contexto de uso** | Afeta autonomia, supervisão e protocolo | Definir se uso prioritário será escolar, supervisionado, domiciliar ou híbrido. Já decidido: um reino por código de criança, que serve à escola e à casa (LG-Gab-04) | Coordenação científica |
| **Forma final dos estímulos** | Pode comprometer validade caso permita atalhos perceptivos | Produzir e testar conjunto infantil de estímulos; os estímulos do conjunto original mantêm a rotação em x, y e z (LG-Gab-01, conforme a implementação do aluno) | Pesquisador responsável / equipe científica |
| **Cálculo definitivo do `complexity_score`** | Afeta organização dos estímulos e adaptação | Definir métricas e pesos após primeiros protótipos | Equipe científica |
| **Escala de similaridade dos distratores** | Afeta diretamente a dificuldade | Definir método operacional de classificação | Equipe científica |
| **Algoritmo de adaptação** | Limiares e janela de desempenho ainda não definidos | Calibrar com dados de piloto; provisório na §4.3 (LG-Gab-06), executado pelo backend via API (v1.5) | Equipe científica / dados |
| **Critério do Bônus de Agilidade** | Recompensa temporal pode alterar estratégia de resposta | Definir limiares após observar distribuição de RT e acurácia; até lá, desligado (LG-Gab-10) | Equipe científica |
| **Limite para tentativa abandonada** | Necessário para diferenciar RT cognitivo de ausência/interrupção | Estimar a partir de piloto; provisório: pergunta aos 45 s, abandono aos 75 s (LG-Gab-21) | Equipe científica |
| **Duração das sessões** | Pode afetar fadiga, engajamento e quantidade de dados | Avaliar em piloto com crianças | Equipe científica |
| **Quantidade de tentativas por sessão** | Afeta confiabilidade e tolerabilidade | Definir no protocolo; provisório: 10, ajustável no painel do mediador (§5.1) | Equipe científica |
| **Tela de assentimento da criança** | A regra é comum aos jogos da IC e não foi decidida; o protótipo validado não tem a tela | Decidir se existe, quando aparece e o que grava | Orientador (pendência) |
| **Quantidade de tentativas por dia de jogo** | Afeta ciclo dia/noite e economia | Valor provisório 10 (LG-Gab-11); confirmar no piloto | Game design |
| **Valores definitivos de pontos, multiplicadores e moedas** | Afetam engajamento e economia | Simular e rebalancear após definição do ritmo de jogo | Game design |
| **Efeito do feedback narrativo** | Pode produzir aprendizagem ou alterar estratégia entre tentativas | Avaliar em piloto | Equipe científica |
| **Efeito da streak e do Bônus de Agilidade na frustração** | Pode aumentar engajamento ou competitividade | Avaliar em PPI/teste de protótipo | Equipe de pesquisa |
| **Validade da gamificação** | Gamificação pode modificar desempenho em Mental Rotation | Comparar com tarefa de referência em etapa posterior | Equipe científica |
| **Minigames das construções** | Escopo secundário ainda indefinido | Definir somente após estabilização do core | Game design |
| **Assets específicos** | Direção visual definida, mas pacotes ainda não escolhidos | Levantar opções gratuitas e documentar licenças | Desenvolvimento / design |
| **Acessibilidade para deficiência visual severa** | Paradigma possui dependência visuoespacial intrínseca | Delimitar escopo e estudar alternativas, se aplicável | Coordenação científica |
| **Precisão temporal da plataforma** | Necessária para interpretação científica do RT | Caracterizar no PC (Windows) | Equipe de desenvolvimento |
| **Adaptação e sequência fixa** | A adaptação (§4.3) escolhe a peça por ângulo, complexidade e similaridade, mas cada obra tem uma sequência fixa de 10 (§4.1, conforme a implementação do aluno) | Decidir se a adaptação escolhe a peça (e então as obras tiram peças de um conjunto comum, com peças e malhas geradas por código) ou só registra o estado; até lá, o jogo só registra o estado recebido do backend (v1.5) | Orientador (pendência) |
| **0° e o outro sentido de 45° e 90°** | O conjunto original não tem 0° nem 45° e 90° no sentido contrário (§4.1) | Decidir se entram, e como, sem quebrar a sequência fixa de cada obra | Orientador / equipe científica (pendência) |
| **Habitante e gabarito** | O habitante sai do modelo da peça, e cada modelo do conjunto original aparece numa condição só: o mesmo habitante volta sempre com a mesma resposta, o que pode virar pista (§10.2, §12) | Decidir se o sorteio muda ou se o risco fica registrado para o piloto | Orientador (pendência) |
| **Função do Laboratório** | Com Observatório e Museu sem pré-requisito, o Laboratório não libera nada (§5.2) | Dar ao Laboratório um papel no grafo ou deixá-lo sem dependentes | Orientador (pendência) |

## 13B. DECISÕES DO ORIENTADOR SOBRE AS LACUNAS (26/11/2026)

O orientador decidiu as 22 lacunas levantadas na revisão do SGDD v1.1 (numeradas LG-Gab-01 a LG-Gab-22) e validou o resultado jogando um protótipo web do jogo, em 26/11/2026. As seções citadas trazem a regra; esta tabela é o índice. Na v1.4, as linhas marcadas seguem o código do aluno, por decisão do orientador.

| Lacuna | Decisão | Seção |
| --- | --- | --- |
| LG-Gab-01 | Rotação em profundidade, em torno do eixo x, y ou z, como no conjunto original (v1.4, conforme a implementação do aluno) | §4.1 |
| LG-Gab-02 | 5 peças por construção; só o `MATCH` aceito conta, e a obra fecha no 10º estímulo; o erro não soma nem tira (v1.4, conforme a implementação do aluno) | §4.2 |
| LG-Gab-03 | Depois de um erro a mesma peça volta; registra-se a primeira resposta (v1.4, conforme a implementação do aluno) | §4.2 |
| LG-Gab-04 | Um reino por código de criança, escolhido no menu; o mesmo código serve à escola e à casa | §2.2, §10.3 |
| LG-Gab-05 | Pausa pelo mesmo botão e ícone em todas as telas, menos no menu e nas configurações; RETOMAR, CONFIGURAÇÕES, SALVAR E SAIR; no desafio, a pausa é o botão VILA, que leva à Vila (v1.4, conforme a implementação do aluno) | §5.1 |
| LG-Gab-06 | Começa no fácil (`a45-cLOW-s1`); sobe em rodízio ângulo → complexidade → similaridade; desce desfazendo o último aumento | §4.3 |
| LG-Gab-07 | Cada tela segue sozinha (4 / 2,5 / 2,2 / 1,4 s); um clique adianta | §5.1 |
| LG-Gab-08 | Os quatro resultados com consequência própria; quatro explicações em ordem fixa; falas de até 8 palavras | §4.2 |
| LG-Gab-09 | Placa só no acerto (streak, pontos, bônus); nada no erro; moedas na Vila | §4.2 |
| LG-Gab-10 | Bônus de Agilidade desligado até o piloto, com chave no painel do mediador | §4.2.3 |
| LG-Gab-11 | O dia fecha em 10 tentativas válidas ou no fim da sessão; o número é pendência do piloto | §4.2.4, §11.1 |
| LG-Gab-12 | A primeira construção é o Observatório; antes dela nenhuma outra (v1.4, conforme a implementação do aluno) | §5.2 |
| LG-Gab-13 | Tutorial de 5 passos, giro à mão nos passos 1–4, peças fixas, “pular” à mostra desde o começo, rever no menu | §4.4 |
| LG-Gab-14 | SERVE e NÃO SERVE com ícone e rótulo, ≥ 2,3 cm; peças com ~37% da altura | §10.2 |
| LG-Gab-15 | Painel do mediador (Ctrl+Shift+M ou botão “adulto” do menu; os três toques no canto saíram na v1.5), encerramento com motivo gravado | §10.3 |
| LG-Gab-16 | Vila com deslocamento, nos limites do mapa da cena (v1.4); Observatório, Museu e Parque com conteúdo provisório; o conteúdo é do pesquisador | §5.2 |
| LG-Gab-17 | Configurações do aparelho, com os padrões da §10.3 | §10.3 |
| LG-Gab-18 | Arquivo local por sessão, exportação pelo painel do pesquisador, criança identificada pelo código | §11.4 |
| LG-Gab-19 | Decoração de 1 a 5 moedas; tirar devolve o preço inteiro | §4.2.4 |
| LG-Gab-20 | Duas músicas, sete efeitos, silêncio na comparação, narração por voz sintética provisória | §7.3 |
| LG-Gab-21 | Pergunta neutra aos 45 s, abandono aos 75 s; “parar” fecha a sessão e mostra a Vila | §5.1 |
| LG-Gab-22 | Habitantes de aparência neutra e constante; um novo na Vila por construção concluída, até 5; o habitante de cada peça sai do modelo dela (v1.4, conforme a implementação do aluno) | §5.2 |

---

## 13A. HIPÓTESES PARA VALIDAÇÃO

Para preservar rastreabilidade, hipóteses derivadas do PPI utilizam **GP**, enquanto hipóteses específicas do jogo utilizam **DP** ou são identificadas como decisões de design a validar.

| H-ID | Hipótese | Princípio de origem | Será verificada no Doc 3 |
| --- | --- | --- | --- |
| **H-01** | O onboarding visual permitirá compreender a regra principal sem dependência de leitura extensa | GP-01 | Sim |
| **H-02** | Configurações de som e estímulos sensoriais permitirão acomodar preferências diferentes sem modificar o núcleo da tarefa | GP-02 | Sim |
| **H-03** | Um sistema no qual o erro encerra apenas a streak, sem perda acumulada, reduzirá impacto negativo da falha sobre a experiência | GP-03 | Sim |
| **H-04** | A adaptação baseada em desempenho observado será mais adequada à heterogeneidade dos usuários do que progressão fixa por diagnóstico | GP-04 | Sim |
| **H-05** | Reduzir elementos visuais não essenciais durante a comparação facilitará o foco no estímulo relevante | GP-05 | Sim |
| **H-06** | Permitir pausa e saída voluntárias favorecerá autonomia sem comprometer o retorno futuro ao jogo | GP-06 | Sim |
| **H-07** | Impedir rotação manual durante a tentativa favorecerá a necessidade de transformação mental da peça | DP-01 | Sim |
| **H-08** | Manter cor fora da lógica da resposta reduzirá pistas perceptivas alternativas e barreiras cromáticas | DP-02 | Sim |
| **H-09** | Retirar o personagem do foco durante a comparação reduzirá interferência social sem eliminar seu papel narrativo | DP-03 | Sim |
| **H-10** | Separar desempenho científico de progresso no reino permitirá que crianças com maior dificuldade continuem percebendo evolução significativa | DP-04 | Sim |
| **H-11** | Construções que desbloqueiam atividades e novas regiões sustentarão interesse longitudinal melhor do que recompensas exclusivamente numéricas | DP-05 | Sim |
| **H-12** | O mesmo núcleo de jogo poderá atender crianças com diferentes perfis quando dificuldade, acessibilidade e personalização forem tratadas como dimensões distintas | DP-06 | Sim |
| **H-13** | Apresentar uma única referência ativa e um candidato por tentativa reduzirá demandas de busca visual não necessárias ao paradigma | DP-01 | Sim |
| **H-14** | Um Bônus de Agilidade pequeno, concedido apenas após respostas corretas e sem cronômetro visível, poderá favorecer ritmo de interação sem gerar pressão excessiva | GP-03 / decisão de design | Sim |
| **H-15** | Separar interrupções e pausas do RT válido reduzirá contaminação da medida por eventos não cognitivos | Integridade científica | Sim |
| **H-16** | Microtutoriais apresentados no momento do desbloqueio reduzirão carga inicial sem prejudicar a aprendizagem das funcionalidades secundárias | GP-01 | Sim |
| **H-17** | A combinação de ângulo, complexidade estrutural e similaridade do distrator permitirá adaptar a demanda de forma mais gradual do que uma progressão linear única | DP-06 | Sim |

---

# REFERÊNCIAS TEÓRICAS DE APOIO

ARNAB, S.; LIM, T.; CARVALHO, M. B.; et al. **Mapping learning and game mechanics for serious games analysis.** *British Journal of Educational Technology*, v. 46, n. 2, p. 391–411, 2015. DOI: 10.1111/bjet.12113.

CAST. **CAST Universal Design for Learning Guidelines version 3.0.** Wakefield, MA: CAST, 2024.

CHITA-TEGMARK, M. **Social attention in ASD: A review and meta-analysis of eye-tracking studies.** *Research in Developmental Disabilities*, v. 48, p. 79–93, 2016. DOI: 10.1016/j.ridd.2015.10.011.

HUNICKE, R.; LEBLANC, M.; ZUBEK, R. **MDA: A Formal Approach to Game Design and Game Research.** AAAI Workshop on Challenges in Game AI, 2004.

IACHINI, T.; RUGGIERO, G.; BARTOLO, A.; RAPUANO, M.; RUOTOLO, F. **The Effect of Body-Related Stimuli on Mental Rotation in Children, Young and Elderly Adults.** *Scientific Reports*, 2019. DOI: 10.1038/s41598-018-37729-7.

ISO. **ISO 9241-210:2019 — Ergonomics of human-system interaction — Part 210: Human-centred design for interactive systems.** Geneva: International Organization for Standardization, 2019. Versão confirmada em 2025.

LÜTKE, N.; LANGE-KÜTTNER, C. **Keeping It in Three Dimensions: Measuring the Development of Mental Rotation in Children with the Rotated Colour Cube Test.** *International Journal of Developmental Science*, v. 9, n. 2, p. 95–114, 2015. DOI: 10.3233/DEV-14154.

MUTH, A.; HÖNEKOPP, J.; FALTER, C. M. **Visuo-spatial performance in autism: a meta-analysis.** *Journal of Autism and Developmental Disorders*, v. 44, n. 12, p. 3245–3263, 2014. DOI: 10.1007/s10803-014-2188-5.

PIERCE, K.; CONANT, D.; HAZIN, R.; STONER, R.; DESMOND, J. **Preference for geometric patterns early in life as a risk factor for autism.** *Archives of General Psychiatry*, v. 68, n. 1, p. 101–109, 2011. DOI: 10.1001/archgenpsychiatry.2010.113.

SHEPARD, R. N.; METZLER, J. **Mental Rotation of Three-Dimensional Objects.** *Science*, v. 171, n. 3972, p. 701–703, 1971. DOI: 10.1126/science.171.3972.701.

W3C. **Web Content Accessibility Guidelines — WCAG 2.2.** World Wide Web Consortium.

WEN, T. H.; CHENG, A.; ANDREASON, C.; et al. **Large scale validation of an early-age eye-tracking biomarker of an autism spectrum disorder subtype.** *Scientific Reports*, v. 12, 4253, 2022. DOI: 10.1038/s41598-022-08102-6.

ZAKRZEWSKI, S.; MERRILL, E.; YANG, Y. **Can gamification improve children's performance in mental rotation?** *Journal of Experimental Child Psychology*, v. 252, 106169, 2025. DOI: 10.1016/j.jecp.2024.106169.

KALTNER, S.; JANSEN, P. **Developmental Changes in Mental Rotation: A Dissociation Between Object-Based and Egocentric Transformations.** *Advances in Cognitive Psychology*, v. 12, n. 2, p. 67–78, 2016. DOI: 10.5709/acp-0187-y. Há errata publicada em 2017 (DOI: 10.5709/acp-0218-6).

HERTZOG, C.; VERNON, M. C.; RYPMA, B. **Age differences in mental rotation task performance: the influence of speed/accuracy tradeoffs.** *Journal of Gerontology*, v. 48, n. 3, p. P150–P156, 1993. DOI: 10.1093/geronj/48.3.P150.

LIESEFELD, H. R.; FU, X.; ZIMMER, H. D. **Fast and careless or careful and slow? Apparent holistic processing in mental rotation is explained by speed-accuracy trade-offs.** *Journal of Experimental Psychology: Learning, Memory, and Cognition*, v. 41, n. 4, p. 1140–1151, 2015. DOI: 10.1037/xlm0000081.

---

# ANEXO A — PLANO INICIAL DE IMPLEMENTAÇÃO

Este anexo organiza o desenvolvimento em blocos dependentes. A estrutura não constitui ainda um backlog ágil formal, mas foi construída para permitir conversão posterior em Epics, Features, Tasks e critérios de aceite.

## Bloco 0 — Avaliação tecnológica

**Objetivo:** comparar a viabilidade de Unity e Flutter/Flame antes da implementação definitiva. **Concluído:** Unity, com build para PC (Windows, mouse e teclado) (decidido pelo orientador em 26/11/2026; WebGL e Android saíram na v1.5); os itens abaixo ficam como o que a escolha precisa atender.

**Avaliar:**

- suporte ao estilo pixel art e à Vila pseudo-isométrica;
- suporte mobile/tablet e desktop;
- precisão e consistência da medição temporal;
- persistência local;
- gerenciamento de assets;
- animações e interface 2D;
- facilidade de manutenção;
- integração futura com a infraestrutura do CIATec.

**Saída esperada:** decisão tecnológica ou protótipo comparativo suficiente para justificar a escolha.

---

## Bloco 1 — Protótipo científico isolado

**Objetivo:** implementar Mental Rotation sem reino, NPCs, economia ou arte definitiva.

**Implementar:**

- referência e candidato;
- respostas “Serve” e “Não serve”;
- condições `MATCH` e `NON_MATCH`;
- ângulos configuráveis;
- complexidade configurável;
- similaridade do distrator;
- medição de RT;
- registro da primeira resposta;
- estados de validade da tentativa;
- exportação ou inspeção da telemetria.

**Critério de conclusão:** uma sequência de tentativas pode ser executada e todos os parâmetros apresentados e respostas obtidas podem ser verificados posteriormente.

---

## Bloco 2 — Sistema de estímulos

**Objetivo:** separar o conteúdo científico da lógica do jogo.

Cada estímulo deverá ser carregado a partir de estrutura de dados contendo, no mínimo:

```text
stimulus_id
reference_asset
candidate_asset
match_condition
rotation_angle
rotation_direction
complexity_score
distractor_similarity
```

**Critério de conclusão:** novos estímulos podem ser adicionados sem alterar a lógica central de Mental Rotation.

---

## Bloco 3 — Máquina de estados

**Objetivo:** estabelecer o fluxo estrutural do jogo com interfaces provisórias.

**Estados iniciais:**

```text
MENU
CONFIGURATIONS
LOAD_GAME
INTRO
TUTORIAL
NPC_DIALOG
PROJECT_VIEW
TRIAL_SETUP
TRIAL_ACTIVE
RESPONSE_RECEIVED
FEEDBACK
CONSTRUCTION_PROGRESS
VILLAGE_VIEW
BUILDING_INTERIOR
DECORATION_MODE
PAUSE
```

**Critério de conclusão:** o fluxo principal pode ser percorrido integralmente utilizando placeholders.

---

## Bloco 4 — Loop NPC → Projeto → Tentativa

**Objetivo:** incorporar o protótipo científico à narrativa.

**Implementar:**

- NPC apresenta o contexto;
- Grande Livro de Projetos destaca uma peça;
- transição para a comparação;
- resposta;
- consequência narrativa;
- retorno ao fluxo de construção ou próximo NPC.

**Critério de conclusão:** várias tentativas podem ser realizadas em sequência por meio do contexto narrativo, sem alterar os parâmetros científicos definidos.

---

## Bloco 5 — Feedback, streak, pontos e ritmo

**Implementar:**

- pontos-base;
- streak;
- multiplicadores;
- consequência de peça funcionar/não funcionar;
- Bônus de Agilidade parametrizável;
- reinício da streak após erro;
- ausência de perda acumulada.

**Critério de conclusão:** o sistema recompensa acertos consecutivos e permite bônus positivo por agilidade sem punir resposta correta lenta.

---

## Bloco 6 — Construções e dependências

**Implementar:**

- dez construções iniciais;
- progresso individual de construção;
- conclusão;
- grafo de dependências;
- desbloqueios;
- persistência.

Assets provisórios poderão ser utilizados.

**Critério de conclusão:** concluir uma construção altera corretamente a disponibilidade das construções dependentes e o estado persiste entre sessões.

---

## Bloco 7 — Vila

**Implementar:**

- visão pseudo-isométrica em pixel art;
- deslocamento da área visualizada;
- construções concluídas;
- regiões e construções bloqueadas;
- seleção de construções;
- entrada em construções elegíveis.

**Critério de conclusão:** o jogador consegue visualizar de forma persistente os resultados produzidos pelo loop principal.

---

## Bloco 8 — Economia e decoração

**Implementar:**

- ciclo de fechamento do dia;
- conversão de pontos em moedas;
- saldo persistente;
- catálogo inicial de decoração;
- posicionamento de elementos permitidos;
- salvamento da personalização.

**Critério de conclusão:** moedas obtidas na gameplay podem personalizar a Vila sem alterar a dificuldade ou progresso científico.

---

## Bloco 9 — Sistema adaptativo

**Entradas iniciais:**

```text
accuracy
reaction_time
historical_performance
```

**Parâmetros manipuláveis:**

```text
rotation_angle
complexity_score
distractor_similarity
```

Os limiares deverão ser configuráveis externamente à lógica central.

**Onde roda (v1.5):** no backend, via API (padrão CIATEC). As entradas chegam ao backend pela telemetria de cada tentativa; o jogo recebe os parâmetros manipuláveis e os aplica na tentativa seguinte (§4.3).

**Critério de conclusão:** o sistema consegue aumentar, manter ou reduzir a demanda das próximas tentativas sem comunicar explicitamente uma classificação de dificuldade ao jogador.

---

## Bloco 10 — Acessibilidade e configurações

**Implementar:**

- controle de música e efeitos;
- equivalentes visuais para informações sonoras essenciais;
- controles de tamanho adequado;
- pausa e saída;
- redução de estímulos não essenciais;
- suporte visual às instruções;
- demais parâmetros definidos após testes.

**Critério de conclusão:** a mecânica principal pode ser executada sem depender exclusivamente de leitura, áudio ou cor.

---

## Bloco 11 — Interiores e experiências secundárias

Este bloco deverá começar somente após estabilização do núcleo.

**Primeiros candidatos:**

- **Observatório:** eventos astronômicos, seleção de corpos celestes, nome e breve descrição;
- **Museu:** exposição inicial relacionada a dinossauros;
- **Parque de Diversões:** estrutura preparada para experiências lúdicas futuras.

**Critério de conclusão:** as construções oferecem experiências adicionais sem interferir na coleta principal de Mental Rotation.

---

## Bloco 12 — Assets e direção artística

Substituir progressivamente placeholders por:

- cenário pixel art;
- construções;
- NPCs;
- interface;
- animações;
- decorações;
- áudio.

Todo asset externo deverá ter origem e licença registradas.

Os estímulos científicos seguirão fluxo separado de produção e validação.

---

## Bloco 13 — Validação técnica

Antes do piloto com usuários, verificar:

- precisão e consistência do RT;
- registro da primeira resposta;
- tratamento de pausas e interrupções;
- distribuição `MATCH/NON_MATCH`;
- distribuição angular;
- parâmetros efetivamente apresentados;
- funcionamento da adaptação;
- persistência;
- telemetria;
- salvamento;
- comportamento em diferentes dispositivos.

**Critério de conclusão:** nenhuma condição técnica conhecida deve alterar silenciosamente ou tornar ambíguos os dados registrados.

---

## Bloco 14 — Protótipo com usuários e novo ciclo de PPI

Com o protótipo funcional, avaliar:

- compreensão da tarefa;
- tutorial inicial;
- microtutoriais;
- duração espontânea de uso;
- adequação dos estímulos;
- frustração;
- streak e Bônus de Agilidade;
- clareza das consequências narrativas;
- uso da Vila;
- acessibilidade;
- problemas e estratégias não antecipados.

Os resultados deverão alimentar a versão seguinte do SGDD e a parametrização do sistema.
