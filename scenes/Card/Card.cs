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

    private PackedScene cardScene;

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
    public override void _Process(double delta)
    {
        if (isHovered)
        {
            if (Input.IsActionJustPressed("click"))
            {
                isHovered = false;
                var parent = GetParent<CardManager>();
                if (State == CardState.Draw)
                {
                    GD.Print("clicked");

                    GlobalPosition = new Vector2(164, 186);
                    State = CardState.Hand;

                    var instance = (Card)cardScene.Instantiate();
                    var card = parent.Players[0].DrawPile.Dequeue();

                    instance.Data = card.Data;
                    instance.State = CardState.Draw;

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
        GD.Print("entered");
        isHovered = true;
    }

    public void OnMouseExited()
    {
        GD.Print("exited");
        isHovered = false;
    }
}
