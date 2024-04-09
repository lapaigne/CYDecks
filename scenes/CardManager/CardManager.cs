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
    private double _coolDown;

    public PlayerData Player;
    public PlayerData Opponent;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        var cardScene = GD.Load<PackedScene>("res://scenes/Card/Card.tscn");
        foreach (Card card in PlayerDraw)
        {
            var instance = (Card)cardScene.Instantiate();
            instance.State = card.State;
            instance.Data = card.Data;
            instance.Position = GetNode<Slot>("SlotArray/PDraw").Position;

            AddChild(instance);
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(double delta)
    {
        var children = GetChildren().OfType<Card>();

        foreach (Card child in children)
        {
            if (child.isMoving && child.Slot != null)
            {
                child.timeEnRoute += delta;
                var distance = child.Slot.Position.DistanceTo(child.GlobalPosition);
                if (distance > 2 && child.timeEnRoute <= 0.2)
                {
                    child.Translate(child.Velocity * (float)delta / 0.2f);
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
                        // GD.Print("beep boop boop");
                        // var rnd = new RandomNumberGenerator();
                        // var sprite = child.GetNode<AnimatedSprite2D>("AnimatedSprite2D");
                        // sprite.Frame = rnd.RandiRange(0, 20);
                        if (child.TrySelectingNewPosition(Player, Opponent))
                        {
                            foreach (Card s in children)
                            {
                                s.CanClick = false;
                            }
                        }
                    }
                }
            }
        }
    }

    public void AddCard(Card card, Slot slot) { }

    public void MoveCard(Card card, Slot slot) { }
}
