---
faixa: CIATec
doc: Doc 2 — Backlog Ágil (parte à parte)
titulo: Meu Pequeno Reino — Contrato da adaptação com o backend
subtitulo: O que o jogo manda, o que recebe e a regra que o backend executa
versao: v0.1
data: 26/11/2026
tipo: spec
status: proposta — para o orientador aprovar; fora do backlog (a conexão com a API é da etapa seguinte)
fonte_principal: SGDD-1-Gabriel.md (v1.5), §4.3, §11.2, §11.4, §13 e Anexo A Bloco 9; ABD-1-Gabriel.md (v13), US-09.1
data_criacao: 26/11/2026
data_atualizacao: 26/11/2026
---

# Meu Pequeno Reino — Contrato da adaptação com o backend

> **Proposta.** Em 26/11/2026 o orientador decidiu que a adaptação da demanda é do **backend, via API** (padrão
> CIATEC): o jogo não calcula a regra. Este arquivo propõe o contrato entre os dois e guarda a regra do SGDD como
> especificação do backend. Nada daqui está no `ABD-1-Gabriel-issues.csv`: no jogo, o que existe é a interface do
> provedor e o provedor local fixo (US-09.1, T-09.1.3 e T-09.1.4) e o registro do `difficulty_state` recebido (US-09.1, T-09.1.5). A
> conexão (cliente, servidor, transporte) é da etapa do backend, no ano que vem.

## 1 Onde isto se liga ao jogo

- **Interface:** `IDifficultyProvider`, em `Game.Core` (T-09.1.3). Pede os parâmetros da próxima tentativa e recebe o
  registro de cada tentativa terminada. Não tem entrada de diagnóstico (SGDD §12-27, DP-06).
- **Até a API:** `FixedDifficultyProvider` (T-09.1.3) devolve sempre `a45-cLOW-s1`, sem regra. O Core joga como hoje.
- **Quando houver API:** um provedor novo, que fala com o backend, entra no lugar do fixo, sem mudar o fluxo (T-09.1.4).
- **Telemetria:** o arquivo JSON local por sessão (`Telemetry/session_<session_id>.json`, no formato do `Documentation/TrialTelemetry.md` do Gabriel, US-02.3) continua sendo a fonte; o envio ao backend é cópia dele, com os nomes do arquivo levados aos desta seção.

## 2 O que o jogo manda

A cada tentativa terminada (`TRAINING` ou `VALID`, qualquer `trial_status`), o registro da tentativa, com os campos
de §11.4 na ordem do SGDD e os 4 extras do ABD, tirado do arquivo local pela visão derivada (T-09.2.3):

| Grupo | Campos |
|---|---|
| Identificação | `session_id` (UUID v4), `participant_code` (fora do arquivo local, que não identifica a criança: decisão do orientador de 26/11/2026; como identificar no envio fica para a API), `trial_id`, `trial_type` |
| Estímulo | `stimulus_reference_id`, `stimulus_candidate_id`, `match_condition`, `rotation_angle` (0° a 180°), `rotation_direction`, `rotation_axis`, `rotation_angle_raw`, `complexity_score` (0–10), `distractor_similarity` (1–3) |
| Estado aplicado | `difficulty_state` (o recebido para esta tentativa) |
| Resposta | `response`, `accuracy`, `reaction_time_ms`, `trial_status`, `attempt_count` |
| Jogo | `streak_before`, `streak_after`, `base_points`, `streak_multiplier`, `agility_bonus`, `points_earned`, `feedback_shown` |
| Tempo | `timestamp_start`, `timestamp_end` |

E, uma vez por sessão, o cabeçalho do arquivo da sessão (US-02.3): versão do jogo, plataforma, dispositivo e
configurações de acessibilidade; a obra e a posição de cada peça na sequência fixa vão em cada tentativa.

## 3 O que o jogo recebe

Antes de cada tentativa `VALID`, os parâmetros da próxima tentativa:

| Campo | Valores | Uso no jogo |
|---|---|---|
| `max_rotation_angle` | 45, 90, 135, 180 | ângulo máximo da peça |
| `complexity_band` | `LOW`, `MID`, `HIGH` | faixa do `complexity_score` (§5.3) |
| `distractor_similarity` | 1, 2, 3 | similaridade do distrator nos `NON_MATCH` |
| `difficulty_state` | `a<ângulo>-c<faixa>-s<similaridade>`, como `a45-cLOW-s1` | gravado na tentativa (US-09.1, T-09.1.5) |

**Comportamento do jogo:**

- Os parâmetros valem para a tentativa seguinte. A peça é sempre a da sequência fixa da obra (LA-10, decidida pelo
  orientador pelo código do aluno no corte de protótipo, 26/11/2026), e o jogo só aplica e grava o estado.
- Sem resposta no tempo limite (em configuração), a tentativa usa os últimos parâmetros recebidos; sem nenhum
  recebido, `a45-cLOW-s1`. O início do RT nunca espera o backend.
- Parâmetro fora dos valores acima é ignorado, e vale o último válido (proposta; ver seção 5).
- Nenhuma tela mostra nível, dificuldade ou regressão (§4.3).

## 4 A regra que o backend executa (SGDD v1.5, §4.3, LG-Gab-06)

- **Entradas:** acurácia e RT das tentativas `VALID` com `trial_status` `COMPLETED`. `TRAINING`, `PAUSED`,
  `INTERRUPTED` e `ABANDONED` **não entram** (§4.3, §11.2.1).
- **Estado inicial:** `a45-cLOW-s1`. Degraus: ângulo 45 → 90 → 135 → 180°; complexidade `LOW` → `MID` → `HIGH`;
  similaridade 1 → 2 → 3.
- **Janela:** 8 tentativas válidas. **Histórico:** o RT das últimas 40 tentativas válidas.
- **Sobe um degrau** com acurácia ≥ 85% na janela e RT mediano da janela ≤ a mediana do histórico. O eixo que sobe
  vem em rodízio (ângulo, complexidade, similaridade, de novo o ângulo), a partir do que subiu por último; o
  primeiro aumento é o do ângulo, e o eixo já no topo é pulado.
- **Desce um degrau** com acurácia ≤ 50% e RT mediano ≥ 1,25× a mediana do histórico, desfazendo o último aumento.
  O estado inicial não desce.
- **Senão, mantém.** Depois de subir ou descer, a janela recomeça vazia.
- **Estado por criança:** degraus, pilha dos aumentos, janela e histórico são do reino da criança (do
  `participant_code`).
- Limiares e janela são provisórios até o piloto e ficam em configuração do backend (SGDD §13, Algoritmo de
  adaptação).

## 5 Pendências para o orientador

| ID | Pendência | Proposta |
|---|---|---|
| API-01 | Transporte e autenticação (REST/JSON, como o RT-10 do Lucas, ou outro). | Decidir na etapa do backend. |
| API-02 | Onde mora o estado de adaptação quando a mesma criança joga em aparelhos diferentes. | No backend, pelo `participant_code`. |
| API-03 | Envio sem rede: fila e reenvio da telemetria. | O arquivo local é a fonte; reenviar o que faltar. |
| API-04 | Parâmetro inválido vindo do backend. | Ignorar e manter o último válido. |
| API-05 | Escolha da peça pelos parâmetros (LA-10 do ABD). | Decidida no corte de protótipo (26/11/2026): a peça é a da sequência fixa da obra, e a adaptação não a escolhe. |

## 6 Como conferir (quando houver API)

- [ ] Com o provedor fixo, toda tentativa `VALID` grava `a45-cLOW-s1` (já coberto pela T-09.1.4).
- [ ] Uma sequência sintética de tentativas faz o backend subir, manter e descer como na seção 4, um caso por regra.
- [ ] `PAUSED`, `TRAINING`, `INTERRUPTED` e `ABANDONED` não mudam a janela.
- [ ] Sem rede, o jogo segue com os últimos parâmetros e o RT não atrasa.
