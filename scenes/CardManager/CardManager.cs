using System;
using System.Collections.Generic;
using Godot;

// better off instantiate all cards instead
// queues and stacks make things overcomplicated


public partial class CardManager : Node2D
{
    public List<Card> Shop;
    public PlayerPiles[] Players;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        var resource = GD.Load<CardData>("res://resources/cards/DefaultCard.tres");
        var scene = GD.Load<PackedScene>("res://scenes/Card/Card.tscn");

        var drawQueue = new Queue<Card>();

        drawQueue.Enqueue(new Card { Data = new CardData(15), State = CardState.Draw });
        drawQueue.Enqueue(new Card { Data = new CardData(2), State = CardState.Draw });
        drawQueue.Enqueue(new Card { Data = new CardData(18), State = CardState.Draw });

        Players = new PlayerPiles[] { new PlayerPiles { DrawPile = drawQueue } };
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta) { }
}
