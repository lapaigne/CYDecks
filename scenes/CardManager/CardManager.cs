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
    public Slot[] Slots;
    private double _coolDown;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Slots = new Slot[]
        {
            new Slot { Position = new Vector2(164, 306), Type = global::Slots.Hand, OwnerId = 0 },
            new Slot { Position = new Vector2(184, 306), Type = global::Slots.Hand, OwnerId = 0 },
            new Slot { Position = new Vector2(204, 306), Type = global::Slots.Hand, OwnerId = 0 },
            new Slot { Position = new Vector2(224, 306), Type = global::Slots.Hand, OwnerId = 0 },
            new Slot { Position = new Vector2(244, 306), Type = global::Slots.Hand, OwnerId = 0 },
            new Slot { Position = new Vector2(264, 306), Type = global::Slots.Hand, OwnerId = 0 },
            new Slot { Position = new Vector2(284, 306), Type = global::Slots.Hand, OwnerId = 0 },
            //
            new Slot { Position = new Vector2(164, 186), Type = global::Slots.Play, OwnerId = 0 },
            new Slot { Position = new Vector2(204, 186), Type = global::Slots.Play, OwnerId = 0 },
            new Slot { Position = new Vector2(244, 186), Type = global::Slots.Play, OwnerId = 0 },
            new Slot { Position = new Vector2(284, 186), Type = global::Slots.Play, OwnerId = 0 },
            //
            new Slot { Position = new Vector2(86, 186), Type = global::Slots.Discard, OwnerId = 0 },
            //
            new Slot { Position = new Vector2(6, 246), Type = global::Slots.Draw, OwnerId = 0 },
            //
            new Slot { Position = new Vector2(164, 6), Type = global::Slots.Hand, OwnerId = 1 },
            new Slot { Position = new Vector2(184, 6), Type = global::Slots.Hand, OwnerId = 1 },
            new Slot { Position = new Vector2(204, 6), Type = global::Slots.Hand, OwnerId = 1 },
            new Slot { Position = new Vector2(224, 6), Type = global::Slots.Hand, OwnerId = 1 },
            new Slot { Position = new Vector2(244, 6), Type = global::Slots.Hand, OwnerId = 1 },
            new Slot { Position = new Vector2(264, 6), Type = global::Slots.Hand, OwnerId = 1 },
            new Slot { Position = new Vector2(284, 6), Type = global::Slots.Hand, OwnerId = 1 },
            //
            new Slot { Position = new Vector2(164, 126), Type = global::Slots.Play, OwnerId = 1 },
            new Slot { Position = new Vector2(204, 126), Type = global::Slots.Play, OwnerId = 1 },
            new Slot { Position = new Vector2(244, 126), Type = global::Slots.Play, OwnerId = 1 },
            new Slot { Position = new Vector2(284, 126), Type = global::Slots.Play, OwnerId = 1 },
            //
            new Slot { Position = new Vector2(86, 126), Type = global::Slots.Discard, OwnerId = 1 },
            //
            new Slot { Position = new Vector2(6, 66), Type = global::Slots.Draw, OwnerId = 1 },
            //
            new Slot { Position = new Vector2(344, 156), Type = global::Slots.Shop, OwnerId = -1 },
        };

        // var scene = GD.Load<PackedScene>("res://scenes/Card/Card.tscn");

        PlayerDraw = new Queue<Card>();
        var rnd = new RandomNumberGenerator();

        for (int i = 0; i < 100; i++)
        {
            PlayerDraw.Enqueue(
                new Card { Data = new CardData(rnd.RandiRange(0, 20)), State = global::Slots.Draw }
            );
        }

        // drawQueue.Enqueue(new Card { Data = new CardData(2), State = CardState.Draw });
        // drawQueue.Enqueue(new Card { Data = new CardData(18), State = CardState.Draw });
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(double delta) { }

    public void AddCard(Card card, Slot slot) { }
}
