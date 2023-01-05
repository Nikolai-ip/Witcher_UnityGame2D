using System;
using UnityEngine;

[SerializeField]
public abstract class Attack: IComparable
{
    public float AnimationDuration { get; protected set; }
    public float MovePlayerTime { get; protected set; }
    public float MovePlayerSpeed { get; protected set; } = 3;

    public string AnimationName { get; protected set; } = "Attack";
    public int CompareIndex { get; protected set; }

    public int CompareTo(object other)
    {
        return CompareIndex.CompareTo((other as Attack).CompareIndex);
    }
}