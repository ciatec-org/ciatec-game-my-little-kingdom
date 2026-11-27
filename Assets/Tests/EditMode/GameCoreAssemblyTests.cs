using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using UnityEditor.Compilation;

public sealed class GameCoreAssemblyTests
{
    [Test]
    public void GameCoreHasNoUnityReferences()
    {
        var core = CompilationPipeline.GetAssemblies().Single(assembly => assembly.name == "Game.Core");
        var unityReferences = core.allReferences.Select(Path.GetFileName).Where(name =>
            name.StartsWith("UnityEngine", StringComparison.Ordinal) ||
            name.StartsWith("UnityEditor", StringComparison.Ordinal));

        Assert.That(core.assemblyReferences, Is.Empty);
        Assert.That(unityReferences, Is.Empty);
    }
}
