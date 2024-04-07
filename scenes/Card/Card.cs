using System.Collections.Generic;
using System.Data;
using System.Linq;
using Godot;

public partial class Card : Node2D
{
    [Export]
    public CardData Data;

    [Export]
    public SlotType State;

    [Export]
    public bool BelongsToPlayer;

    [Export]
    public bool CanClick = true;

    public bool isMoving;
    public bool isHovered;
    public bool targetSelected;
    public double timeEnRoute;
    public Vector2 Velocity;
    public Slot Slot = null;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        // cardScene = GD.Load<PackedScene>("res://scenes/Card/Card.tscn");

        // if (Data != null)
        // {
        //     var sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        //     sprite.Frame = (State == Slots.Draw || State == Slots.Shop) ? 1 : Data.Id;

        // }
    }

    public override void _Process(double delta)
    {
        // move to state switching
        var sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        sprite.Frame = (State == SlotType.Draw || State == SlotType.Shop) ? 1 : Data.Id;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(double delta)
    {
        // var siblings = GetParent<CardManager>().GetChildren().Where(s => s is Card);
        // if (isMoving)
        // {
        //     timeEnRoute += delta;

        //     if (Slot != null)
        //     {
        //         // GD.Print(targetSelected);
        //         var distance = Slot.Position.DistanceTo(GlobalPosition);
        //         // var velocity = (targetPosition - GlobalPosition).Normalized() * distance * (float)delta;
        //         if (distance > 2 && timeEnRoute <= 0.2)
        //         {
        //             Translate(_velocity * (float)delta / 0.2f);
        //         }
        //         else
        //         {
        //             foreach (Card s in siblngs)
        //             {
        //                 s.CanClick = true;
        //             }
        //             GlobalPosition = Slot.Position;
        //             targetSelected = false;
        //             isMoving = false;
        //             timeEnRoute = 0;
        //         }
        //     }
        // }
        // else
        // if (isHovered)
        // {
        //     if (Input.IsActionJustPressed("click"))
        //     {
        //         if (CanClick)
        //         {
        //             foreach (Card s in siblings)
        //             {
        //                 s.CanClick = false;
        //             }
        //             TrySelectingNewPosition();
        //         }
        //     }
        // }
    }

    public void TrySelectingNewPosition()
    {
        if (!targetSelected)
        {
            var parent = GetParent<CardManager>();
            var slots = parent.GetNode<Node2D>("SlotArray").GetChildren().OfType<Slot>();
            // var _slots = slots.Where(child => child is Slot);
            var available = new List<Slot>();
            switch (State)
            {
                case SlotType.Draw:
                    available = new List<Slot>(slots.Where(slot => slot.Type == SlotType.Hand));
                    break;

                case SlotType.Hand:
                    available = new List<Slot>(slots.Where(slot => slot.Type == SlotType.Play));
                    break;

                case SlotType.Play:
                    available = new List<Slot>(
                        slots.Where(slots => slots.Type == SlotType.Discard)
                    );
                    break;

                case SlotType.Discard:
                    available = new List<Slot>(slots.Where(slots => slots.Type == SlotType.Draw));
                    break;

                case SlotType.Shop:
                    available = new List<Slot>(slots.Where(slots => slots.Type == SlotType.Draw));
                    break;
            }

            if (available.Count() > 0)
            {
                for (int i = 0; i < available.Count(); i++)
                {
                    if (!available[i].isOccupied)
                    {
                        if (Slot != null)
                        {
                            Slot.isOccupied = false;
                        }
                        Slot = available[i];
                        ZIndex = i;
                        targetSelected = true;
                        Velocity = Slot.Position - GlobalPosition;
                        isMoving = true;

                        switch (State)
                        {
                            case SlotType.Draw:
                                available[i].isOccupied = true;
                                State = SlotType.Hand;
                                break;

                            case SlotType.Hand:
                                available[i].isOccupied = true;
                                Data.OnPlayEffect();
                                State++;
                                break;

                            case SlotType.Shop:
                                State = SlotType.Draw;
                                break;

                            default:
                                Slot.isOccupied = false;
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
        // GD.Print(State);
    }

    public void OnMouseExited()
    {
        // GD.Print("exited");
        isHovered = false;
    }
}
