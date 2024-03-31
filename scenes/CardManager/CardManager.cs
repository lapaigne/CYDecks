using System;
using System.Collections.Generic;
using Godot;

// better off instantiate all cards instead
// queues and stacks make things overcomplicated

public partial class CardManager : Node2D
{
    public List<Card> Shop;
    public Queue<Card> PlayerDraw;
    public Queue<Card> OpponentDraw;
    public CardSlot[] Slots;
    private double _coolDown;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Slots = new CardSlot[]
        {
            new CardSlot { Position = new Vector2(164, 306), Type = Slot.PlayerHand },
            new CardSlot { Position = new Vector2(184, 306), Type = Slot.PlayerHand },
            new CardSlot { Position = new Vector2(204, 306), Type = Slot.PlayerHand },
            new CardSlot { Position = new Vector2(224, 306), Type = Slot.PlayerHand },
            new CardSlot { Position = new Vector2(244, 306), Type = Slot.PlayerHand },
            new CardSlot { Position = new Vector2(264, 306), Type = Slot.PlayerHand },
            new CardSlot { Position = new Vector2(284, 306), Type = Slot.PlayerHand },
            //
            new CardSlot { Position = new Vector2(164, 186), Type = Slot.PlayerPlay },
            new CardSlot { Position = new Vector2(204, 186), Type = Slot.PlayerPlay },
            new CardSlot { Position = new Vector2(244, 186), Type = Slot.PlayerPlay },
            new CardSlot { Position = new Vector2(284, 186), Type = Slot.PlayerPlay },
            //
            new CardSlot { Position = new Vector2(86, 186), Type = Slot.PlayerDiscard },
            //
            new CardSlot { Position = new Vector2(6, 246), Type = Slot.PlayerDraw },
            //
            new CardSlot { Position = new Vector2(164, 6), Type = Slot.OpponentHand },
            new CardSlot { Position = new Vector2(184, 6), Type = Slot.OpponentHand },
            new CardSlot { Position = new Vector2(204, 6), Type = Slot.OpponentHand },
            new CardSlot { Position = new Vector2(224, 6), Type = Slot.OpponentHand },
            new CardSlot { Position = new Vector2(244, 6), Type = Slot.OpponentHand },
            new CardSlot { Position = new Vector2(264, 6), Type = Slot.OpponentHand },
            new CardSlot { Position = new Vector2(284, 6), Type = Slot.OpponentHand },
            //
            new CardSlot { Position = new Vector2(164, 126), Type = Slot.OpponentPlay },
            new CardSlot { Position = new Vector2(204, 126), Type = Slot.OpponentPlay },
            new CardSlot { Position = new Vector2(244, 126), Type = Slot.OpponentPlay },
            new CardSlot { Position = new Vector2(284, 126), Type = Slot.OpponentPlay },
            //
            new CardSlot { Position = new Vector2(86, 126), Type = Slot.OpponentDiscard },
            //
            new CardSlot { Position = new Vector2(6, 66), Type = Slot.OpponentDraw },
            //
            new CardSlot { Position = new Vector2(344, 156), Type = Slot.Shop },
        };

        // var resource = GD.Load<CardData>("res://resources/cards/DefaultCard.tres");
        // var scene = GD.Load<PackedScene>("res://scenes/Card/Card.tscn");

       PlayerDraw  = new Queue<Card>();
        var rnd = new RandomNumberGenerator();

        for (int i = 0; i < 100; i++)
        {
            PlayerDraw.Enqueue(
                new Card { Data = new CardData(rnd.RandiRange(0, 20)), State = CardState.Draw }
            );
        }

        // drawQueue.Enqueue(new Card { Data = new CardData(2), State = CardState.Draw });
        // drawQueue.Enqueue(new Card { Data = new CardData(18), State = CardState.Draw });
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(double delta) { }
}
