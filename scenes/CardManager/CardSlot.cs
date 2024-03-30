using System;
using Godot;

public enum Slot{
    PlayerHand,
    PlayerPlay,
    PlayerDiscard,
    PlayerDraw,
    OpponentHand,
    OpponentPlay,
    OpponentDiscard,
    OpponentDraw,
    Shop,
    Destroyed

}

public partial class CardSlot { 
    public Vector2 Position;
    public bool isOccupied;
    public Slot Type;
}
