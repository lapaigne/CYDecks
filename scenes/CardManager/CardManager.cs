using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

// better off instantiate all cards instead (actually probably not)

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
        // make all slots nodes in the scene tree

        Slots = new Slot[]
        {
            new Slot
            {
                Position = new Vector2(164, 306),
                Type = SlotType.Hand,
                OwnerId = 0
            },
            new Slot
            {
                Position = new Vector2(184, 306),
                Type = SlotType.Hand,
                OwnerId = 0
            },
            new Slot
            {
                Position = new Vector2(204, 306),
                Type = SlotType.Hand,
                OwnerId = 0
            },
            new Slot
            {
                Position = new Vector2(224, 306),
                Type = SlotType.Hand,
                OwnerId = 0
            },
            new Slot
            {
                Position = new Vector2(244, 306),
                Type = SlotType.Hand,
                OwnerId = 0
            },
            new Slot
            {
                Position = new Vector2(264, 306),
                Type = SlotType.Hand,
                OwnerId = 0
            },
            new Slot
            {
                Position = new Vector2(284, 306),
                Type = SlotType.Hand,
                OwnerId = 0
            },
            //
            new Slot
            {
                Position = new Vector2(164, 186),
                Type = SlotType.Play,
                OwnerId = 0
            },
            new Slot
            {
                Position = new Vector2(204, 186),
                Type = SlotType.Play,
                OwnerId = 0
            },
            new Slot
            {
                Position = new Vector2(244, 186),
                Type = SlotType.Play,
                OwnerId = 0
            },
            new Slot
            {
                Position = new Vector2(284, 186),
                Type = SlotType.Play,
                OwnerId = 0
            },
            //
            new Slot
            {
                Position = new Vector2(86, 186),
                Type = SlotType.Discard,
                OwnerId = 0
            },
            //
            new Slot
            {
                Position = new Vector2(6, 246),
                Type = SlotType.Draw,
                OwnerId = 0
            },
            //
            new Slot
            {
                Position = new Vector2(164, 6),
                Type = SlotType.Hand,
                OwnerId = 1
            },
            new Slot
            {
                Position = new Vector2(184, 6),
                Type = SlotType.Hand,
                OwnerId = 1
            },
            new Slot
            {
                Position = new Vector2(204, 6),
                Type = SlotType.Hand,
                OwnerId = 1
            },
            new Slot
            {
                Position = new Vector2(224, 6),
                Type = SlotType.Hand,
                OwnerId = 1
            },
            new Slot
            {
                Position = new Vector2(244, 6),
                Type = SlotType.Hand,
                OwnerId = 1
            },
            new Slot
            {
                Position = new Vector2(264, 6),
                Type = SlotType.Hand,
                OwnerId = 1
            },
            new Slot
            {
                Position = new Vector2(284, 6),
                Type = SlotType.Hand,
                OwnerId = 1
            },
            //
            new Slot
            {
                Position = new Vector2(164, 126),
                Type = SlotType.Play,
                OwnerId = 1
            },
            new Slot
            {
                Position = new Vector2(204, 126),
                Type = SlotType.Play,
                OwnerId = 1
            },
            new Slot
            {
                Position = new Vector2(244, 126),
                Type = SlotType.Play,
                OwnerId = 1
            },
            new Slot
            {
                Position = new Vector2(284, 126),
                Type = SlotType.Play,
                OwnerId = 1
            },
            //
            new Slot
            {
                Position = new Vector2(86, 126),
                Type = SlotType.Discard,
                OwnerId = 1
            },
            //
            new Slot
            {
                Position = new Vector2(6, 66),
                Type = SlotType.Draw,
                OwnerId = 1
            },
            //
            new Slot
            {
                Position = new Vector2(344, 156),
                Type = SlotType.Shop,
                OwnerId = -1
            },
        };

        // var scene = GD.Load<PackedScene>("res://scenes/Card/Card.tscn");

        PlayerDraw = new Queue<Card>();
        var rnd = new RandomNumberGenerator();

        for (int i = 0; i < 100; i++)
        {
            PlayerDraw.Enqueue(
                new Card { Data = new CardData(rnd.RandiRange(0, 20)), State = SlotType.Draw }
            );
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(double delta)
    {
        var children = GetChildren().Where(child => child is Card);
        foreach (Card child in children)
        {
            if (child.isMoving && child.Slot != null)
            {
                child.timeEnRoute += delta;
                // GD.Print(targetSelected);
                var distance = child.Slot.Position.DistanceTo(child.GlobalPosition);
                // var velocity = (targetPosition - GlobalPosition).Normalized() * distance * (float)delta;
                if (distance > 2 && child.timeEnRoute <= 0.2)
                {
                    Translate(child.Velocity * (float)delta / 0.2f);
                }
                else
                {
                    foreach (Card s in children)
                    {
                        s.CanClick = true;
                    }
                    child.GlobalPosition = child.Slot.Position;
                    child.targetSelected = false;
                    child.isMoving = false;
                    child.timeEnRoute = 0;
                }
            }
            else if (child.isHovered)
            {
                if (Input.IsActionJustPressed("click"))
                {
                    if (child.CanClick)
                    {
                        foreach (Card s in children)
                        {
                            s.CanClick = false;
                        }
                        GD.Print("beep boop boop");
                        var rnd = new RandomNumberGenerator();
                        var sprite = child.GetNode<AnimatedSprite2D>("AnimatedSprite2D");
                        sprite.Frame = rnd.RandiRange(0, 20);
                        child.TrySelectingNewPosition();
                    }
                }
            }
        }
    }

    public void AddCard(Card card, Slot slot) { }

    public void MoveCard(Card card, Slot slot) { }
}
