using System.Data.Common;
using Godot;

[GlobalClass]
public partial class CardData : Resource
{
    [Export]
    public int Id;

    [Export]
    public bool noEffect = true;

    [Export]
    public int Health = 0;

    [Export]
    public int Shield = 0;

    [Export]
    public int Damage = 0;

    [Export]
    public int Money = 0;

    public CardData()
    {
        Id = 0;
    }

    public void TriggerEffect() { 
        GD.Print($"Effect of card with {Id} was activated");

    }
}
