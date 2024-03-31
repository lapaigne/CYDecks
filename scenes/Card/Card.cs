using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Godot;

public partial class Card : Node2D
{
    [Export]
    public CardData Data;

    [Export]
    public Slots State;

    [Export]
    public Node2D Master;

    [Export]
    public bool CanClick = true;
    private bool isHovered;
    private bool isMoving;
    private PackedScene cardScene;
    private bool _targetSelected;
    private double _timeEnRoute;
    private Vector2 _velocity;
    private Slot _slot = null;

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
        var siblngs = GetParent<CardManager>().GetChildren().Where(s => s is Card);
        if (isMoving)
        {
            _timeEnRoute += delta;

            if (_slot != null)
            {
                // GD.Print(targetSelected);
                var distance = _slot.Position.DistanceTo(GlobalPosition);
                // var velocity = (targetPosition - GlobalPosition).Normalized() * distance * (float)delta;
                if (distance > 2 && _timeEnRoute <= 0.2)
                {
                    Translate(_velocity * (float)delta / 0.2f);
                }
                else
                {
                    foreach (Card s in siblngs)
                    {
                        s.CanClick = true;
                    }
                    GlobalPosition = _slot.Position;
                    _targetSelected = false;
                    isMoving = false;
                    _timeEnRoute = 0;
                }
            }
        }
        else if (isHovered)
        {
            if (Input.IsActionJustPressed("click"))
            {
                if (CanClick)
                {
                    foreach (Card s in siblngs)
                    {
                        s.CanClick = false;
                    }
                    TrySelectingNewPosition();
                }
            }
        }
    }

    public void TrySelectingNewPosition()
    {
        if (!_targetSelected)
        {
            var parent = GetParent<CardManager>();
            var slots = parent.Slots;
            var available = new List<Slot>();
            switch (State)
            {
                case Slots.Draw:
                    available = new List<Slot>(slots.Where(slot => slot.Type == Slots.Hand));
                    break;

                case Slots.Hand:
                    available = new List<Slot>(slots.Where(slot => slot.Type == Slots.Play));
                    break;

                case Slots.Play:
                    available = new List<Slot>(
                        slots.Where(slots => slots.Type == Slots.Discard)
                    );
                    break;

                case Slots.Discard:
                    available = new List<Slot>(slots.Where(slots => slots.Type == Slots.Draw));
                    break;

                case Slots.Shop:
                    available = new List<Slot>(slots.Where(slots => slots.Type == Slots.Draw));
                    break;
            }

            if (available.Count() > 0)
            {
                for (int i = 0; i < available.Count(); i++)
                {
                    if (!available[i].isOccupied)
                    {
                        if (_slot != null)
                        {
                            _slot.isOccupied = false;
                        }
                        _slot = available[i];
                        ZIndex = i;
                        _targetSelected = true;
                        _velocity = _slot.Position - GlobalPosition;
                        isMoving = true;

                        switch (State)
                        {
                            case Slots.Draw:
                                available[i].isOccupied = true;
                                State = Slots.Hand;
                                break;
                            case Slots.Hand:
                                available[i].isOccupied = true;
                                State++;
                                break;
                            case Slots.Shop:
                                State = Slots.Draw;
                                break;

                            default:
                                if (_slot != null)
                                {
                                    _slot.isOccupied = false;
                                }
                                State++;
                                break;
                        }

                        break;
                        // return true;
                    }
                }
                // return false;
            }
            else
            {
                GD.Print("no free slots were found");
                // return false;
            }
        }
    }

    public void OnMouseEntered()
    {
        // GD.Print("entered");
        isHovered = true;
        GD.Print(State);
    }

    public void OnMouseExited()
    {
        // GD.Print("exited");
        isHovered = false;
    }
}
