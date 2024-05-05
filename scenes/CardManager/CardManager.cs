using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

// better off instantiate all cards instead (actually probably not)

public partial class CardManager : Area2D
{
    public List<Card> Shop;
    public Queue<Card> PlayerDeck;
    public Queue<Card> OpponentDeck;
    private double _coolDown;

    public PlayerData Player;
    public PlayerData Opponent;
    private PackedScene cardScene;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        cardScene = GD.Load<PackedScene>("res://scenes/Card/Card.tscn");
        foreach (Card card in PlayerDeck)
        {
            // todo: move instantiate into game manager to make PlayerDeck and /-/ useful
            AddCard(card);
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(double delta)
    {
        var list = GetChildren().OfType<Card>().ToList();
        foreach (var card in list)
        {
            TryMovingCard(card);
        }
    }

    public void AddCard(Card card)
    {
        var instance = (Card)cardScene.Instantiate();
        instance.CurrentState = card.CurrentState;
        instance.Data = card.Data;
        instance.Position = GetNode<Slot>("SlotArray/PDraw").Position;

        instance.OnCardClick += onCardClick;

        AddChild(instance);
    }

    private void onCardClick(Card card)
    {
        GD.Print("Clicked");
        switch (card.CurrentState)
        {
            case SlotType.Hand:
            case SlotType.Draw:
                TrySelectingNewSlot(card, Player, Opponent);
                break;
            case SlotType.Play:
                card.Data.OnClickEffect();
                break;
            default:
                return;
        }
    }

    public bool TryMovingCard(Card card)
    {
        if (card.Slot == null)
        {
            return false;
        }
        var delta = GetPhysicsProcessDeltaTime();

        card.timeEnRoute += delta;
        var distance = card.Slot.Position.DistanceTo(card.GlobalPosition);
        if (distance > 2 && card.timeEnRoute <= 0.2)
        {
            card.Translate(card.Velocity * (float)delta / 0.2f);
        }
        else
        {
            card.GlobalPosition = card.Slot.Position;
            card.CurrentState = card.Slot.Type;
            card.NextState = SlotType.None;
            card.isMoving = false;
            card.timeEnRoute = 0;
        }
        return false;
    }

    public bool TrySelectingNewSlot(Card card, PlayerData player, PlayerData opponent)
    {
        if (card.NextState != SlotType.None)
        {
            return false;
        }

        var slots = new List<Slot>();

        switch (card.CurrentState)
        {
            case SlotType.Hand:
                slots = GetFreeSlots(card, SlotType.Play);
                if (slots.Count > 0)
                {
                    card.Slot = slots[0];
                    slots[0].isOccupied = true;
                    card.NextState = SlotType.Play;
                    card.Data.OnPlayEffect(player, opponent);
                }
                break;
            case SlotType.Play:
                if (card.Data.Locked)
                {
                    card.NextState = SlotType.None;
                }
                else if (card.Data.Destroy)
                {
                    // card.QueueFree();
                    card.Destroy();
                }
                else
                {
                    slots = GetFreeSlots(card, SlotType.Discard);
                    card.Slot = slots[0];
                    card.NextState = SlotType.Discard;
                    card.Data.OnDiscardEffect();
                }
                break;
            case SlotType.Discard:
                var leftInDrawPile = GetNode<Node2D>("SlotArray")
                    .GetChildren()
                    .OfType<Slot>()
                    .Where(slot => slot.OwnerId == card.OwnerId || card.OwnerId == -1)
                    .Where(slot => slot.Type == SlotType.Draw)
                    .Count();

                if (leftInDrawPile == 0)
                {
                    slots = GetFreeSlots(card, SlotType.Draw);
                    card.Slot = slots[0];
                    card.NextState = SlotType.Draw;
                    break;
                }

                break;
            case SlotType.Draw:
                slots = GetFreeSlots(card, SlotType.Hand);
                if (slots.Count > 0)
                {
                    card.Slot = slots[0];
                    slots[0].isOccupied = true;
                    card.NextState = SlotType.Hand;
                }
                break;
            case SlotType.Shop:

                // todo

                break;
            case SlotType.None:

                // todo

                break;
            default:
                return false;
        }

        return true;
    }

    private List<Slot> GetFreeSlots(Card card, SlotType nextState)
    {
        return GetNode<Node2D>("SlotArray")
            .GetChildren()
            .OfType<Slot>()
            .Where(slot => slot.OwnerId == card.OwnerId || card.OwnerId == -1)
            .Where(slot => slot.Type == nextState)
            .Where(slot => !slot.isOccupied)
            .ToList();
    }

    public void ButtonPressed()
    {
        var list = GetChildren().OfType<Card>().ToList();
        foreach (var card in list)
        {
            TrySelectingNewSlot(card, Player, Opponent);
        }
    }
}
