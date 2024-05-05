using System;
using Godot;

public enum SlotType
{
    None,
    Hand,
    Play,
    Discard,
    Draw,
    Shop
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
