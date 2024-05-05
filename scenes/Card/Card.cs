using System.Collections.Generic;
using System.Data;
using System.Linq;
using Godot;

public partial class Card : CharacterBody2D
{
    [Signal]
    public delegate void OnCardClickEventHandler(Card card);

    [Export]
    public CardData Data;

    [Export]
    public SlotType CurrentState;

    [Export]
    public SlotType NextState;

    [Export]
    public int OwnerId;

    [Export]
    public bool CanClick = true;
    public bool isMoving;
    public bool hasMouse;
    public double timeEnRoute;
    public Slot Slot = null;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready() { }

    public override void _Process(double delta) { }

    public void OnMouseEntered()
    {
        hasMouse = true;
    }

    public void OnMouseExited()
    {
        hasMouse = false;
    }

    public void Destroy()
    {
        GD.Print("Card Destroyed");
        // Visible = false;
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton inputEventMouseButton)
        {
            if (inputEventMouseButton.Pressed && hasMouse)
            {
                EmitSignal("OnCardClick", this);
            }
        }
    }
}
