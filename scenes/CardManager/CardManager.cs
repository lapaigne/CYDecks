using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

// better off instantiate all cards instead (actually probably not)

public partial class CardManager : Area2D
{
    private PackedScene cardScene;

    public override void _Ready()
    {
        cardScene = GD.Load<PackedScene>("res://scenes/Card/Card.tscn");

        var slots = GetNode<Node2D>("SlotArray").GetChildren().OfType<Slot>();
        foreach (var slot in slots) { }
    }

    public Card AddCard(Card card)
    {
        var instance = (Card)cardScene.Instantiate();
        instance.OwnerId = card.OwnerId == GetNode<MultiplayerClient>("../..").ClientId ? 0 : 1;
        instance.CurrentState = card.CurrentState;
        instance.Data = card.Data;
        instance.Position =
            (instance.OwnerId == 0)
            ? GetNode<Slot>("SlotArray/PDraw").Position
            : GetNode<Slot>("SlotArray/ODraw").Position;

        instance.OnCardClick += OnCardClick;

        AddChild(instance);
        return instance;
    }

    private void OnCardClick(Card card)
    {
        TrySelectingNewSlot(
            card,
            GetNode<MultiplayerClient>("../..").Player,
            GetNode<MultiplayerClient>("../..").Opponent
        );

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
        // GD.Print(card.GlobalPosition);
        // GD.Print($"\n\n---\nClicked: {card.Data.Id}");
        // GD.Print($"Current: {card.CurrentState}");
        // GD.Print($"Next: {card.NextState}");
        // GD.Print($"Locked: {card.Data.Locked}");
        // GD.Print($"Destroy: {card.Data.Destroy}");

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
                    .Where(_card => _card.OwnerId == card.OwnerId)
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
                    GD.Print("suc");
                    GD.Print(card.Slot);
                }
                GD.Print(slots.Count);
                break;
            default:
                return false;
        }
        GD.Print($"{card.NextState}");
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
            TrySelectingNewSlot(
                card,
                GetParent<MultiplayerClient>().Player,
                GetParent<MultiplayerClient>().Opponent
            );
        }
    }
}
