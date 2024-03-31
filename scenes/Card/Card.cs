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

    [Export]
    public bool CanClick = true;
    private bool isHovered;
    private bool isMoving;
    private PackedScene cardScene;
    private bool _targetSelected;
    private double _timeEnRoute;
    private Vector2 _velocity;
    private CardSlot _slot = null;

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
                    ChangeState();
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
                case CardState.Discard:
                    available = new List<CardSlot>(
                        slots.Where(slots => slots.Type == Slot.PlayerDiscard)
                    );
                    break;
            }

            if (available.Count() > 0)
            {
                for (int i = 0; i < available.Count(); i++)
                {
                    if (!available[i].isOccupied)
                    {
                        if (State == CardState.Play || State == CardState.Hand)
                        {
                            available[i].isOccupied = true;
                        }

                        _slot = available[i];
                        ZIndex = i;
                        _targetSelected = true;
                        _velocity = _slot.Position - GlobalPosition;

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


    // change state in selection instead
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
                if (_slot != null)
                {
                    _slot.isOccupied = false;
                }

                isMoving = true;
                State = CardState.Play;
                // Data.OnPlayEffect();
                return;

            case CardState.Play:
                if (_slot != null)
                {
                    GD.Print(_slot.Type);
                    _slot.isOccupied = false;
                }
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
