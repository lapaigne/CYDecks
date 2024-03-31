using System;
using Godot;

public enum Slots
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
    public Slots Type;
    public int OwnerId;
}
