using System;
using Godot;

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
