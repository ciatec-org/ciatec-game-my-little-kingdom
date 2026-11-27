# AGENTS.md — Meu Pequeno Reino

Codex instructions for the CIATec/FAPESP project at EACH-USP. Reply in Brazilian Portuguese. Be concise without sacrificing correctness, scientific constraints, or verification.

## Project context

- Unity **6000.3.25f1**, URP **17.3.0**, new Input System **1.20.0** only. `Kingdom.unity` boots; `Village.unity` loads additively. Preserve the current `GameApplication`, `GameSession`, gameplay, UI, village and telemetry architecture.
- Consult only relevant sections: `Documentation/SGDD-1-Gabriel.md` (scientific/design requirements), `Documentation/ABD-1-Gabriel.md` (stories), `Documentation/EditingGuide.md` (Unity), `Documentation/TrialTelemetry.md` (JSON contract), `Documentation/Pendencias.md` (known risks).
- Use [the test guide](Documentation/agentes/testes.md) for checks and [the milestone guide](Documentation/agentes/marco-jogavel.md) for milestone acceptance.
- Always distinguish *planned*, *implemented*, *tested*, and *scientifically validated*. Open Issues and ABD descriptions are not proof that a feature is missing.

## Before implementing a GitHub Issue

1. Read its acceptance criteria, dependencies and relevant SGDD/ABD sections. Search the actual code, assets, serialized Unity references and tests as needed.
2. Classify each requirement: **implemented+validated / implemented-unvalidated / partial / absent / blocked / obsolete-conflicting**, citing evidence.
3. **Already done:** validate, don't rebuild. **Partial:** implement only the gap. **Absent:** implement within existing architecture. **Blocked/conflicting:** preserve existing behavior, report the decision required and continue independent authorized work.
4. Close with a brief comparison of requirements, existing behavior, changes, checks and remaining gaps. Do not update GitHub Issues or Projects without permission.

## Scientific and telemetry safeguards

- Preserve Mental Rotation: reference/candidate comparison, proper rotation versus mirroring, geometry, angle/axis, answer keys, trial order, retry behavior, feedback and accessibility. Do not add answer cues. Changes to experimental conditions, RT semantics, progression or adaptation require explicit authorization and scientific review when applicable.
- Do not infer diagnoses from performance or mix gameplay progress with research measures. Do not change SGDD/ABD to justify divergent code.
- Local **JSON telemetry v1.0** (PR #431) is the current contract: preserve IDs, stimulus metadata, **all raw attempts**, responses, correctness, active response times, pauses and trial status. JSON Lines is only a possible future export. No silent schema migration, personal data collection or network transmission; use synthetic test data.
- `TrialController` uses `Time.realtimeSinceStartupAsDouble`. `Agentes.Relogio` is not an automatic substitute; `Agentes.Entrada` uses legacy input and is not used by gameplay. RT in milliseconds is not experimentally validated; first-frame, focus-loss and per-attempt validity remain open.

## Unity, implementation and Git

- Before edits, check the branch, working tree, relevant remote state, applicable branch protections and Git rulesets. Follow those rules and report any verification gaps. Normally branch from current `develop` and preserve collaborators' work. No destructive reset/clean, force push, or commits/pushes/merges/PRs/Project changes without authorization.
- Keep diffs minimal; reuse existing systems and conventions. No unrequested refactors, duplicate services, engine/package updates or gameplay changes.
- Preserve `.meta`/GUIDs and serialized scene, prefab, ScriptableObject, Tilemap and material references. Editor/content changes require scope authorization. Report Inspector values affected by serialized default changes.
- Production scripts: `Assets/Scripts/`; existing validators: `Assets/Scripts/Editor/`. New test asmdefs are planned, not assumed. Keep MonoBehaviour/ScriptableObject class names matched to filenames. Avoid unnecessary component searches and allocations per frame. Use the **new Input System**.
- Routine technical decisions within scope are autonomous. Escalate scientific/design ambiguity or consequential behavior changes; continue independent work where safe.

## Token discipline (quality first)

- **Locate, then read:** use `rg`, symbol search, `git status -s`, `git diff --stat` and targeted ranges before broad reads. Expand when needed to prove behavior; don't reread unchanged material.
- Filter verbose test/log output to relevant errors and summaries; retain full logs as artifacts when needed. Ignore Unity-generated `Library/`, `Temp/`, `Logs/`, `Obj/` and build folders during routine searches unless relevant.
- Avoid redundant planning, repeated command output, speculative documentation and unnecessary subagents. Make coherent edits and verify them. For complex/scientific/security/data-loss work, deeper inspection overrides token savings.
- Run affected checks (when relevant: `KingdomValidation`, `PlayModeFlowChecks`, `VillageInteractionChecks`, `TrialTelemetryChecks`); broaden for integration risk. Use isolated Unity copies if a check may modify files/data. Never claim an unrun test passed.
- If installed, use `$token-saver-codex` for large-context/log tasks or explicit efficiency audits, **not** on every trivial edit. External skills never override scientific rules or authorization.

## Completion

Report succinctly: **what already existed; what changed; files; checks actually run; unmet criteria/risks**. Identify human Editor/hardware/research validation separately. Do not create progress files or expand scope unless requested or needed for the authorized task.
