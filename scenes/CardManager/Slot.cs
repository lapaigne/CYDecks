using System;
using Godot;

public enum SlotType
{
    Hand,
    Play,
    Discard,
    Draw,
    Shop,
    Unassigned
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
