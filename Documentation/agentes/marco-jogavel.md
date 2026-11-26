# Fechar a story de marco

O ABD prevê uma **story de marco** para o Core (US-60) e outra para a Expansão (US-76). Cada uma verifica
o percurso montado para a entrega correspondente. O Codex executa as verificações automatizadas autorizadas.
A equipe valida o build no dispositivo-alvo e as etapas que exigem julgamento humano ou científico.

## Parte A — o que o agente fecha

1. **Verificações disponíveis:** use `KingdomValidation`, `PlayModeFlowChecks`, `VillageInteractionChecks`
   e `TrialTelemetryChecks` conforme a cobertura do marco. Quando a suíte do Test Runner estiver disponível,
   rode também seus testes EditMode e PlayMode. Veja [o guia de testes](testes.md).
2. **Percurso automatizado:** `PlayModeFlowChecks` já abre `Kingdom` e percorre tutorial, trials, construções,
   vila, observatório e reinício. O robô do marco (`Assets/Tests/PlayMode/RoboJogaTudoTests.cs`) é um
   exemplo de arquivo futuro. Quando a story correspondente o exigir, cubra os estados e os registros
   previstos, usando o Input System para entrada sintética e preservando a medição de RT.
3. **Requisitos:** percorra o ABD e marque cada requisito como:
   - **verificado pelo agente** (diga o teste);
   - **não acontece, e devia** → corrija se a tarefa autorizar ou registre a evidência;
   - **só uma pessoa verifica** → Parte B;
   - **nenhum caminho do jogo exercita isso** → avise o orientador.
4. **Perguntas abertas:** informe as lacunas da entrega que exigem decisão da equipe. Consulte
   `Documentation/decisoes-provisorias.md` se existir. Trate decisões que afetam os critérios do marco
   antes de considerar a etapa concluída.

## Parte B — o que só uma pessoa fecha

O aluno prepara e executa o build no dispositivo-alvo. A equipe confere as telas e a experiência real
(layout, texto, cor, som e acessibilidade), e registra o resultado separadamente. Playtest, piloto, CEP
e validação científica seguem o protocolo da equipe. Registre a evidência que falta e o que deve ser
observado em cada etapa.

## Modelo de registro do marco

Use no relatório do marco ou em `Documentation/progresso.md` quando esse arquivo existir e a tarefa
previr sua atualização.

```markdown
## Marco <ID> — <título>

### A. Fechado pelo agente
- Suíte inteira: <N> testes, <N> passaram (EditMode <n>, PlayMode <n>).
- Robô: `RoboJogaTudoTests` — <o caminho que ele percorre, em uma linha>; passou.
- Requisitos: <n> verificados, <n> corrigidos no marco, <n> sem caminho no jogo (listados abaixo).

### B. Para uma pessoa fechar
| Requisito | Onde no jogo | O que se espera ver |
|---|---|---|
| ... | ... | ... |
```
