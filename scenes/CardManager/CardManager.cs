using System;
using System.Collections.Generic;
using Godot;

public partial class CardManager : Node2D
{
    public List<Card> Shop;

    public Stack<Card> DestroyedPile;

    public PlayerDeckSlots[] Players;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        var resource = GD.Load<CardData>("res://resources/cards/DefaultCard.tres");
        var scene = GD.Load<PackedScene>("res://scenes/Card/Card.tscn");

        var drawQueue = new Queue<Card>();

        drawQueue.Enqueue(new Card { Data = new CardData(15), State = CardState.Draw });
        drawQueue.Enqueue(new Card { Data = new CardData(2), State = CardState.Draw });
        drawQueue.Enqueue(new Card { Data = new CardData(18), State = CardState.Draw });

        Players = new PlayerDeckSlots[] { new PlayerDeckSlots { DrawPile = drawQueue } };
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta) { }
}
