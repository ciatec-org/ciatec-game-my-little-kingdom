using System;

namespace MeuPequenoReino.Core
{
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

    /// <summary>Mutable progress over immutable building rules.</summary>
    public sealed class GameSession
    {
        public GameScreen Screen { get; set; } = GameScreen.Menu;
        public int CurrentBuilding { get; private set; } = -1;
        public int CandidateIndex { get; private set; }
        public bool Paused { get; private set; }

        readonly BuildingRules[] buildings;
        readonly int[] pieces;
        readonly bool[] built;
        public int CompletedBuildings { get; private set; }
        public bool Complete => CompletedBuildings == built.Length;

        public GameSession(BuildingRules[] buildings)
        {
            if (buildings == null)
                throw new ArgumentNullException(nameof(buildings));
            if (buildings.Length == 0)
                throw new ArgumentException("Reino sem construções.", nameof(buildings));
            this.buildings = (BuildingRules[])buildings.Clone();
            pieces = new int[buildings.Length];
            built = new bool[pieces.Length];
        }

        public bool IsBuilt(int index) => index >= 0 && index < built.Length && built[index];
        public int Pieces(int index) => pieces[index];
        public bool IsUnlocked(int index)
        {
            if (index < 0 || index >= built.Length)
                return false;
            var prerequisite = buildings[index].PrerequisiteIndex;
            return !prerequisite.HasValue || IsBuilt(prerequisite.Value);
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

        public bool IsCorrect(bool serves) => serves == buildings[CurrentBuilding].Matches(CandidateIndex);
        public AnswerResult ApplyCorrectAnswer()
        {
            if (CurrentBuilding < 0 || Paused)
                throw new InvalidOperationException("Desafio não está ativo.");
            var building = buildings[CurrentBuilding];
            if (building.Matches(CandidateIndex))
                pieces[CurrentBuilding]++;
            if (pieces[CurrentBuilding] >= building.RequiredPieces)
            {
                built[CurrentBuilding] = true;
                CompletedBuildings++;
                CurrentBuilding = -1;
                CandidateIndex = 0;
                Screen = GameScreen.BuildingComplete;
                return AnswerResult.BuildingCompleted;
            }

            if (++CandidateIndex >= building.TrialCount)
                throw new InvalidOperationException("Sequência de desafios incompleta.");
            return AnswerResult.NextTrial;
        }
    }
}
