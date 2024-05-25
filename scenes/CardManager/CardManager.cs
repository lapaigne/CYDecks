using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

// better off instantiate all cards instead (actually probably not)

public partial class CardManager : Area2D
{
    public Queue<Card> PlayerDeck;
    public Queue<Card> OpponentDeck;
    public PlayerData Player;
    public PlayerData Opponent;
    private PackedScene cardScene;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        cardScene = GD.Load<PackedScene>("res://scenes/Card/Card.tscn");
        foreach (Card card in PlayerDeck)
        {
            AddCard(card);
        }
    }

    public override void _PhysicsProcess(double delta) { }

    public void AddCard(Card card)
    {
        var instance = (Card)cardScene.Instantiate();
        instance.CurrentState = card.CurrentState;
        instance.Data = card.Data;
        instance.Position = GetNode<Slot>("SlotArray/PDraw").Position;

        instance.OnCardClick += OnCardClick;

        AddChild(instance);
    }

    private void OnCardClick(Card card)
    {
        GD.Print($"Clicked: {card.Data.Id}");
        GD.Print($"Current: {card.CurrentState}");
        GD.Print($"Next: {card.NextState}");
        GD.Print($"Locked: {card.Data.Locked}");
        GD.Print($"Destroy: {card.Data.Destroy}\n");

        TrySelectingNewSlot(card, Player, Opponent);
        
        switch (card.CurrentState)
        {
            case SlotType.Play:
                card.Data.OnClickEffect();
                break;
            default:
                return;
        }
    }

    public bool TrySelectingNewSlot(Card card, PlayerData player = null, PlayerData opponent = null)
    {
        if (card.NextState != SlotType.None)
        {
            return false;
        }

        var slots = new List<Slot>();

        switch (card.CurrentState)
        {
            case SlotType.Play:
                if (card.Data.Locked)
                {
                    card.NextState = SlotType.None;
                }
                else if (card.Data.Destroy)
                {
                    card.Destroy();
                }
                else
                {
                    slots = GetFreeSlots(card, SlotType.Discard);
                    card.Slot.isOccupied = false;
                    card.Slot = slots[0];
                    card.NextState = SlotType.Discard;
                    card.Data.OnDiscardEffect();
                }
                break;
            case SlotType.Discard:
                var leftInDrawPile = GetNode<Node2D>("SlotArray")
                    .GetChildren()
                    .OfType<Card>()
                    .Where(_card => _card.OwnerId == card.OwnerId || card.OwnerId == -1)
                    .Where(_card => _card.CurrentState == SlotType.Draw)
                    .Count();

                if (leftInDrawPile == 0)
                {
                    slots = GetFreeSlots(card, SlotType.Draw);
                    card.Slot = slots[0];
                    card.NextState = SlotType.Draw;
                }
                else
                {
                    return false;
                }
                break;
            case SlotType.Draw:
                slots = GetFreeSlots(card, SlotType.Play);
                if (slots.Count > 0)
                {
                    card.Slot = slots[0];
                    slots[0].isOccupied = true;
                    card.NextState = SlotType.Play;
                }
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
            .Where(slot => slot.OwnerId == card.OwnerId)
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
