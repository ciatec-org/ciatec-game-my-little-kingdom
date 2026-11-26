# AGENTS.md — Meu Pequeno Reino

Instruções para o Codex, agente usado no fluxo de trabalho atual deste projeto.

## O projeto

- **Jogo:** Meu Pequeno Reino, projeto do CIATec/FAPESP na EACH-USP. A criança constrói um reino resolvendo
  desafios de rotação mental (Mental Rotation) de policubos. Cada obra pronta aparece na Vila.
- **Unity:** 6000.3.25f1, URP 2D 17.3.0, Input System 1.20.0 como único backend de entrada.
- **Arquitetura atual:** `Kingdom.unity` é a cena de entrada e carrega `Village.unity` aditivamente.
  `GameApplication` compõe a sessão e os controladores. `GameSession` guarda o estado do jogo. Definições,
  prefabs, tilemaps e malhas persistentes são editáveis no Editor.
- **Design (o quê e o porquê):** `Documentation/SGDD-1-Gabriel.md`. É grande: leia só a seção que a story citar.
- **Backlog (o que fazer):** `Documentation/ABD-1-Gabriel.md`. Cada story tem um ID `US-xx` no título (o nível do título muda:
  **busque pelo ID**), critérios de aceite, cenários Dado/Quando/Então e tasks `[agente]` e `[aluno]`.
- **Implementação e edição:** `Documentation/EditingGuide.md`.
- **Contrato local de dados:** `Documentation/TrialTelemetry.md`.
- Separe requisitos e hipóteses do SGDD e do ABD, comportamento implementado e evidências de validação.
  Consulte os registros de progresso e decisões quando existirem e forem relevantes à tarefa.

## Regras

1. **Preserve os requisitos e decida rotinas técnicas dentro do escopo autorizado.** Mudanças de paradigma
   científico, geometria dos estímulos, condições experimentais, gabaritos, temporização, política de tentativas,
   progressão ou contrato de telemetria exigem autorização específica e, quando aplicável, aprovação da
   orientação científica. Reúna as lacunas relevantes da story numa consulta com opções e recomendação.
   Enquanto aguarda a decisão, continue o trabalho independente e registre as pendências na entrega.
2. **Preserve a montagem no Editor atribuída ao aluno.** Cenas, Build Settings, UI, HUD, Canvas, prefabs,
   ScriptableObjects, tilemaps, materiais, luz, câmera, animações, arte, som, mixer e ligações no Inspector
   só entram numa alteração quando o escopo autoriza. Preserve assets permanentes, arquivos `.meta`, GUIDs e
   referências serializadas. Alterações na versão da engine, nos pacotes ou no pipeline exigem justificativa
   e autorização.
   - Em novos componentes, use campos `[SerializeField]` com nome claro. Preserve a ligação de listeners já
     adotada pelo componente, incluindo a composição por `Initialize` quando ela existir.
   - Para testes isolados, monte por código apenas o necessário. Validadores de integração podem abrir a
     cena de entrada numa cópia do projeto.
3. **Mantenha as pastas e assemblies do projeto.** O código vive em `Assets/Scripts/` e os validadores atuais
   em `Assets/Scripts/Editor/`. Os novos asmdefs e testes em `Assets/Tests/` são trabalho planejado da US-01.
4. **Cada `ScriptableObject` e cada `MonoBehaviour` no próprio arquivo, com o nome da classe** (`ConfigFase` em
   `ConfigFase.cs`); senão o `.asset` ou o componente perde o script.
5. **Use o Input System ativo.** O gameplay não usa `Agentes.Entrada`, que encapsula APIs legadas. A adoção de
   um auxiliar de entrada exige tarefa específica, análise de compatibilidade e testes.
6. **Preserve a temporização dos trials.** `TrialController` mede o tempo ativo com
   `Time.realtimeSinceStartupAsDouble`. `Agentes.Relogio` é opcional. Uma substituição exige equivalência
   semântica, política de pausas definida e requisitos de validação. A unidade em milissegundos não demonstra
   precisão experimental.
7. **Execute verificações proporcionais ao risco.** Priorize `KingdomValidation`, `PlayModeFlowChecks`,
   `VillageInteractionChecks` e `TrialTelemetryChecks` conforme a alteração. Use uma cópia isolada se a execução
   puder gravar arquivos do projeto ou dados locais. Informe o que foi verificado por inspeção estática,
   Play Mode, build distribuível ou validação científica e reporte apenas os testes executados.
8. **Registre efeitos de serialização.** Se a tarefa alterar o valor inicial de um campo serializado, diga
   quais valores persistidos em cena ou prefab precisam de revisão no Inspector.
9. **Auditorias de leitura preservam código, assets e configuração.** Registre evidências fora do checkout.
   Se uma verificação ficar bloqueada, documente a causa e prossiga com o que for independente.

## Telemetria e Git

- A telemetria local JSON `schema_version: 1.0` foi integrada pelo PR #431. Preserve os IDs de sessão e trial,
  as condições apresentadas, todas as tentativas brutas, respostas, acertos, tempos, pausas e estados de
  conclusão. Alterações do esquema exigem tarefa específica. Transmissão remota e coleta de dados pessoais
  exigem requisitos aprovados e autorização. Use dados sintéticos nos testes.
- Use `develop` como base de desenvolvimento. Confira o estado do checkout, a referência remota e as regras
  de proteção aplicáveis antes de trabalhar na branch da tarefa. Preserve o trabalho de terceiros. Se a
  proteção efetiva não puder ser confirmada, registre a limitação.
- Commits, pushes e merges exigem autorização. Force push é proibido.

## Padrões de código

- **O padrão que o repositório já tem vence este:** pastas, asmdef, nomes e guias do projeto (ex.: um
  `EditingGuide.md`). Onde a pasta ou o asmdef dele difere da regra 3, vale o dele.
- C#: classes, métodos e propriedades em `PascalCase`; campos e variáveis em `camelCase`; um namespace do jogo.
- Campo do Inspector é `[SerializeField] private`, não `public`.
- Valor de design (tempo, velocidade, limite, quantidade) fica num ScriptableObject de configuração, não espalhado
  no código.
- Sistemas conversam por evento (`event`, `UnityEvent`) ou referência ligada no Inspector; nada de `Find*` nem de
  singleton para tudo.
- Nada de `Find*`, `GetComponent` ou alocação (`new`, LINQ, concatenação de string) dentro do `Update`.
- Classe pequena, com uma responsabilidade. Antes de criar um sistema, procure (`rg`) se já existe um que faz isso.
- Comentário só onde explica o porquê. Evite código morto e logs temporários sem função.

## Como fazer uma story

1. **Ler só o necessário:** localize a story no ABD pelo ID (`rg -n "US-xx" Documentation/ABD-1-Gabriel.md`),
   consulte a seção citada do SGDD, o código atual e as decisões relevantes disponíveis.
2. **Fazer:** trabalhe uma story por vez nas tasks `[agente]` autorizadas. Use os validadores existentes antes
   de propor infraestrutura nova. Preserve as tasks de montagem e uso em hardware atribuídas ao aluno.
3. **Conferir:** teste o comportamento afetado e seus consumidores. Encaminhe para validação humana os
   critérios que exigem o dispositivo-alvo, playtest ou revisão científica.
4. **Fechar:** informe critérios verificados, evidências, lacunas e, se aplicável, o que o aluno monta e liga
   no Editor (cena, objeto, componente e referência). Atualize registros de progresso ou decisões quando
   fizerem parte do escopo. Preserve as decisões do SGDD e a rastreabilidade do ABD em revisões autorizadas.

## Quando precisar

- `Documentation/agentes/testes.md` — como executar e escrever testes no projeto
- `Documentation/agentes/marco-jogavel.md` — como fechar a story de marco
