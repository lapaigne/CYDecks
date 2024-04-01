using System;
using Godot;

public enum SlotType
{
    Hand,
    Play,
    Discard,
    Draw,
    Shop,
    Destroyed
}

public partial class Slot
{
    public Vector2 Position;
    public bool isOccupied;
    public SlotType Type;
    public int OwnerId;

    // public Slot(Vector2 position, SlotType type, int ownerId = -1)
    // {
    //     Position = position;
    //     Type = type;
    //     OwnerId = ownerId;
    // }
}
