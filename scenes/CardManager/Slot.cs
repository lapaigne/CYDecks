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

    // public Slot(Vector2 position, SlotType type, int ownerId = -1)
    // {
    //     Position = position;
    //     Type = type;
    //     OwnerId = ownerId;
    // }
}
