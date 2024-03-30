using System;
using System.Collections.Generic;
using Godot;

// better off instantiate all cards instead
// queues and stacks make things overcomplicated


public partial class CardManager : Node2D
{
    public List<Card> Shop;
    public PlayerPiles[] Players;

    public CardSlot[] PlayerPlay;
    public CardSlot[] PlayerHand;
    public CardSlot[] PlayerDraw;
    public CardSlot[] OpponentPlay;
    public CardSlot[] OpponentHand;
    public CardSlot[] OpponentDraw;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        PlayerPlay = new CardSlot[]
        {
            new CardSlot { Position = new Vector2(164, 186) },
            new CardSlot { Position = new Vector2(204, 186) },
            new CardSlot { Position = new Vector2(244, 186) },
            new CardSlot { Position = new Vector2(284, 186) },
        };

        PlayerHand = new CardSlot[]
        {
            new CardSlot { Position = new Vector2(164, 306) },
            new CardSlot { Position = new Vector2(184, 306) },
            new CardSlot { Position = new Vector2(204, 306) },
            new CardSlot { Position = new Vector2(224, 306) },
            new CardSlot { Position = new Vector2(244, 306) },
            new CardSlot { Position = new Vector2(264, 306) },
            new CardSlot { Position = new Vector2(284, 306) },
        };

        var resource = GD.Load<CardData>("res://resources/cards/DefaultCard.tres");
        var scene = GD.Load<PackedScene>("res://scenes/Card/Card.tscn");

        var drawQueue = new Queue<Card>();
        var rnd = new RandomNumberGenerator();

        for (int i = 0; i < 100; i++)
        {
            drawQueue.Enqueue(
                new Card { Data = new CardData(rnd.RandiRange(0, 18)), State = CardState.Draw }
            );
        }

        // drawQueue.Enqueue(new Card { Data = new CardData(2), State = CardState.Draw });
        // drawQueue.Enqueue(new Card { Data = new CardData(18), State = CardState.Draw });

        Players = new PlayerPiles[] { new PlayerPiles { DrawPile = drawQueue } };
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta) { }
}
