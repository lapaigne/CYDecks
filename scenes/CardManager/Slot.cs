using System;
using Godot;
/// <summary>
/// Возможные типы слотов. Используется также как состояние карты <c>CurrentState</c> или <c>NextState</c>
/// </summary>
public enum SlotType
{
    None,
    Play,
    Discard,
    Draw
}

public partial class Slot : Node2D
{
    [Export]
    public bool isOccupied;

    [Export]
    public SlotType Type;

    [Export]
    public int OwnerId;
}
