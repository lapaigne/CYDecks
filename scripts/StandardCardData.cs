using System.Data.Common;
using Godot;

[GlobalClass]
public partial class StandardCardData : CardData
{
    public int Health;
    public int Money;
    public int Shield;
    public int Damage;

    public StandardCardData()
    {
        Id = 0;
    }

    public StandardCardData(int id)
    {
        Id = id;
    }

    public override void OnPlayEffect(PlayerData player = null, PlayerData opponent = null)
    {
        player.Health += Health;
        player.Money += Money;
        player.Shield += Shield;
        player.Damage += Damage;
        GD.Print($"Effect of card with id={Id} was activated at play");
    }

    public override void OnClickEffect(PlayerData player = null, PlayerData opponent = null) { }

    public override void OnDiscardEffect(PlayerData player = null, PlayerData opponent = null) { }

    public override void OnDamageTakenEffect(PlayerData player = null, PlayerData opponent = null) { }
}
