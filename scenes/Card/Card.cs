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

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        cardScene = GD.Load<PackedScene>("res://scenes/Card/Card.tscn");

        if (Data != null)
        {
            var sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
            sprite.Frame = Data.Id;
            Data.TriggerEffect();
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(double delta)
    {
        if (isMoving)
        {
            timeEnRoute += delta;
            if (!targetSelected)
            {
                switch (State)
                {
                    case CardState.Hand:
                        var draw = GetParent<CardManager>().PlayerHand;
                        for (int i = 0; i < draw.Length; i++)
                        {
                            if (!draw[i].isOccupied)
                            {
                                targetPosition = draw[i].Position;
                                draw[i].isOccupied = true;
                                break;
                            }
                        }
                        break;
                    case CardState.Play:
                        var play = GetParent<CardManager>().PlayerPlay;
                        for (int i = 0; i < play.Length; i++)
                        {
                            if (!play[i].isOccupied)
                            {
                                targetPosition = play[i].Position;
                                play[i].isOccupied = true;
                                break;
                            }
                        }
                        break;
                }
                targetSelected = true;
                velocity = targetPosition - GlobalPosition;
            }

            // GD.Print(targetSelected);
            var distance = targetPosition.DistanceTo(GlobalPosition);
            // var velocity = (targetPosition - GlobalPosition).Normalized() * distance * (float)delta;
            if (distance > 2 && timeEnRoute <= 0.4)
            {
                Translate(velocity * (float)delta / 0.4f);
            }
            else
            {
                GlobalPosition = targetPosition;
                targetSelected = false;
                isMoving = false;
            }
        }
        else if (isHovered)
        {
            if (Input.IsActionJustPressed("click"))
            {
                isHovered = false;
                var parent = GetParent<CardManager>();
                if (State == CardState.Draw)
                {
                    // GD.Print("clicked");

                    isMoving = true;
                    State = CardState.Hand;
                    ZIndex++;

                    var instance = (Card)cardScene.Instantiate();
                    var card = parent.Players[0].DrawPile.Dequeue();

                    instance.Data = card.Data;
                    instance.State = CardState.Draw;
                    instance.GlobalPosition = GlobalPosition;

                    parent.AddChild(instance);
                }
                else if (State == CardState.Hand)
                {
                    Data.TriggerEffect();
                }
            }
        }
    }

    // public void ChangeState(CardState state)
    // {
    //     switch (state)
    //     {
    //         default:
    //             return;
    //     }
    // }

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
