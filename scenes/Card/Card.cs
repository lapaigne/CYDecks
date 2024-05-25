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
    public bool isMoving;
    public bool hasMouse;
    public Slot Slot = null;
    public double timeEnRoute;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GetNode<AnimatedSprite2D>("AnimatedSprite2D").Frame = Data.Id;
    }
    public override void _Process(double delta)
    {
        TryMoving(delta);
    }

    public bool TryMoving(double delta)
    {
        if (Slot == null || NextState == SlotType.None)
        {
            return false;
        }

        timeEnRoute += delta;
        
        Velocity = Slot.Position - GlobalPosition;
        if (Velocity.Length() > 2 && timeEnRoute <= 0.1f)
        {
            Translate(Velocity * (float)delta / 0.05f);
        }
        else
        {
            GlobalPosition = Slot.Position;
            CurrentState = Slot.Type;
            NextState = SlotType.None;
            isMoving = false;
            timeEnRoute = 0;
            return true;
        }

        return false;
    }

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
                GD.Print("clicked");
                EmitSignal("OnCardClick", this);
            }
        }
    }
}
