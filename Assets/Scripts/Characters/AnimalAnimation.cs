using System;
using UnityEngine;

[Serializable]
public sealed class AnimalDirection
{
    public Sprite[] frames;
}

[CreateAssetMenu(menuName = "Meu Pequeno Reino/Animação de animal")]
public sealed class AnimalAnimation : ScriptableObject
{
    public AnimalDirection[] directions;
}
