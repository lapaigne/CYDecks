using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
    public CardData Data;

    [Export]
    public CardState State;

    [Export]
    public Node2D Master;

    private bool isHovered;
    private bool isMoving;
    private PackedScene cardScene;
    private Vector2 targetPosition;
    private bool targetSelected;
    private double timeEnRoute;
    private Vector2 velocity;
    private CardSlot slot = null;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        cardScene = GD.Load<PackedScene>("res://scenes/Card/Card.tscn");

        if (Data != null)
        {
            var sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
            sprite.Frame = Data.Id;
            Data.OnPlayEffect();
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(double delta)
    {
        if (isMoving)
        {
            timeEnRoute += delta;
            SelectNewPosition();

            if (slot != null)
            {
                // GD.Print(targetSelected);
                var distance = slot.Position.DistanceTo(GlobalPosition);
                // var velocity = (targetPosition - GlobalPosition).Normalized() * distance * (float)delta;
                if (distance > 2 && timeEnRoute <= 0.2)
                {
                    Translate(velocity * (float)delta / 0.2f);
                }
                else
                {
                    GlobalPosition = slot.Position;
                    targetSelected = false;
                    isMoving = false;
                    timeEnRoute = 0;
                }
            }
        }
        else if (isHovered)
        {
            if (Input.IsActionJustPressed("click"))
            {
                ChangeState();
            }
        }
    }

    public void SelectNewPosition()
    {
        if (!targetSelected)
        {
            var parent = GetParent<CardManager>();
            var slots = parent.Slots;
            var available = new List<CardSlot>();
            switch (State)
            {
                case CardState.Hand:
                    available = new List<CardSlot>(
                        slots.Where(slot => slot.Type == Slot.PlayerHand)
                    );
                    break;
                case CardState.Play:
                    available = new List<CardSlot>(
                        slots.Where(slot => slot.Type == Slot.PlayerPlay)
                    );
                    break;
            }

            if (available.Count() > 0)
            {
                for (int i = 0; i < available.Count(); i++)
                {
                    if (!available[i].isOccupied)
                    {
                        available[i].isOccupied = true;
                        slot = available[i];
                        ZIndex = i;
                        break;
                    }
                }
                velocity = slot.Position - GlobalPosition;
            }
            else
            {
                GD.Print("no free slots were found");
            }
            targetSelected = true;
        }
    }

    public void ChangeState(bool giveOpponent = false)
    {
        isHovered = false;
        var parent = GetParent<CardManager>();
        switch (State)
        {
            case CardState.Draw:
                isMoving = true;
                State = CardState.Hand;
                ZIndex++;

                var instance = (Card)cardScene.Instantiate();
                var card = parent.PlayerDraw.Dequeue();

                instance.Data = card.Data;
                instance.State = CardState.Draw;
                instance.GlobalPosition = GlobalPosition;

                parent.AddChild(instance);
                return;

            case CardState.Hand:
                if (slot != null)
                {
                    slot.isOccupied = false;
                }

                isMoving = true;
                State = CardState.Play;
                // Data.OnPlayEffect();
                return;

            case CardState.Play:
                isMoving = true;
                State = CardState.Discard;
                return;

            case CardState.Discard:

                isMoving = true;
                State = CardState.Draw;
                return;

            case CardState.Shop:
                isMoving = true;
                State = CardState.Draw;
                return;

            default:
                return;
        }
    }

    public void OnMouseEntered()
    {
        // GD.Print("entered");
        isHovered = true;
    }

    public void OnMouseExited()
    {
        // GD.Print("exited");
        isHovered = false;
    }
}
