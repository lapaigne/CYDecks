using System;
using System.Data;
using Godot;

public enum CardState
{
    Draw,
    Hand,
    Play,
    Discard,
    Shop,
    Destroyed
}

public partial class Card : Node2D
{
    [Export]
    public Resource CardData;

    [Export]
    public CardState State;

    [Export]
    public Node2D Master;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        if (CardData is CardData){
            ((CardData)CardData).TriggerEffect();
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta) { }

    // public void ChangeState(CardState state)
    // {
    //     switch (state)
    //     {
    //         default:
    //             return;
    //     }
    // }
}
