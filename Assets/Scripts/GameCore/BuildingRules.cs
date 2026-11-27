using System;

namespace MeuPequenoReino.Core
{
    public sealed class BuildingRules
    {
        readonly bool[] structuralMatches;

        internal int RequiredPieces { get; }
        internal int? PrerequisiteIndex { get; }
        internal int TrialCount => structuralMatches.Length;

        public BuildingRules(int requiredPieces, int? prerequisiteIndex, bool[] structuralMatches)
        {
            if (structuralMatches == null)
                throw new ArgumentNullException(nameof(structuralMatches));
            RequiredPieces = requiredPieces;
            PrerequisiteIndex = prerequisiteIndex;
            this.structuralMatches = (bool[])structuralMatches.Clone();
        }

        internal bool Matches(int candidateIndex) => structuralMatches[candidateIndex];
    }
}
