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

    public CardData(int id)
    {
        Id = id;
    }

    public void OnPlayEffect()
    {
        GD.Print($"Effect of card with id={Id} was activated");
    }

    public void OnClickEffect() { }

    public void OnDiscardEffect() { }

    public void OnDamageTakenEffect() { }
}
