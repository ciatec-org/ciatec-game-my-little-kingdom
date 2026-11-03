using System;
using UnityEngine;

[Serializable]
public sealed class PortraitFrames
{
    public Sprite[] frames;
}

[CreateAssetMenu(menuName = "Meu Pequeno Reino/Retratos")]
public sealed class PortraitLibrary : ScriptableObject
{
    public PortraitFrames[] rows;
}
