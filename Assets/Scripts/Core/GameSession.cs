using System;

public enum GameScreen
{
    Menu,
    Tutorial,
    Trial,
    Village,
    Observatory,
    BuildingComplete,
    FinalVillage
}

public enum AnswerResult
{
    Incorrect,
    NextTrial,
    BuildingCompleted
}

/// <summary>Mutable session state. Definitions are shared assets and are never mutated.</summary>
public sealed class GameSession
{
    public KingdomDefinition Definition { get; }
    public GameScreen Screen { get; set; } = GameScreen.Menu;
    public int CurrentBuilding { get; private set; } = -1;
    public int CandidateIndex { get; private set; }
    public bool Paused { get; private set; }

    readonly int[] pieces;
    readonly bool[] built;
    public int CompletedBuildings { get; private set; }
    public bool Complete => CompletedBuildings == built.Length;
    public TrialDefinition CurrentTrial => Definition.buildings[CurrentBuilding].trials[CandidateIndex];

    public GameSession(KingdomDefinition definition)
    {
        Definition = definition ?? throw new ArgumentNullException(nameof(definition));
        if (definition.buildings == null || definition.buildings.Length == 0)
            throw new ArgumentException("Reino sem construções.");
        pieces = new int[definition.buildings.Length];
        built = new bool[pieces.Length];
    }

    public bool IsBuilt(int index) => index >= 0 && index < built.Length && built[index];
    public int Pieces(int index) => pieces[index];
    public bool IsUnlocked(int index)
    {
        if (index < 0 || index >= built.Length)
            return false;
        var dependency = Definition.buildings[index].prerequisite;
        return dependency == null || IsBuilt(Definition.IndexOf(dependency));
    }

    public bool StartBuilding(int index)
    {
        if (CurrentBuilding >= 0 || Paused || !IsUnlocked(index) || IsBuilt(index))
            return false;
        CurrentBuilding = index;
        CandidateIndex = 0;
        Screen = GameScreen.Trial;
        return true;
    }

    public bool Pause()
    {
        if (CurrentBuilding < 0 || Paused || Screen != GameScreen.Trial)
            return false;
        Paused = true;
        Screen = GameScreen.Village;
        return true;
    }

    public bool Resume()
    {
        if (!Paused || CurrentBuilding < 0)
            return false;
        Paused = false;
        Screen = GameScreen.Trial;
        return true;
    }

    public bool IsCorrect(bool serves) => serves == CurrentTrial.structuralMatch;
    public AnswerResult ApplyCorrectAnswer()
    {
        if (CurrentBuilding < 0 || Paused)
            throw new InvalidOperationException("Desafio não está ativo.");
        var definition = Definition.buildings[CurrentBuilding];
        if (CurrentTrial.structuralMatch)
            pieces[CurrentBuilding]++;
        if (pieces[CurrentBuilding] >= definition.requiredPieces)
        {
            built[CurrentBuilding] = true;
            CompletedBuildings++;
            CurrentBuilding = -1;
            CandidateIndex = 0;
            Screen = GameScreen.BuildingComplete;
            return AnswerResult.BuildingCompleted;
        }

        if (++CandidateIndex >= definition.trials.Length)
            throw new InvalidOperationException("Sequência de desafios incompleta.");
        return AnswerResult.NextTrial;
    }
}
