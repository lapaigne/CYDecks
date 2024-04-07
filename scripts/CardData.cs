using System.Data.Common;
using Godot;

[GlobalClass]
public partial class CardData : Resource
{
    [Export]
    public int Id;

    public CardData()
    {
        Id = 0;
    }

    public CardData(int id)
    {
        Id = id;
    }

    public virtual void OnPlayEffect(PlayerData player = null, PlayerData opponent = null)
    {
        GD.Print($"Effect of card with id={Id} was activated at play");
    }

    public virtual void OnClickEffect(PlayerData player = null, PlayerData opponent = null) { }

    public virtual void OnDiscardEffect(PlayerData player = null, PlayerData opponent = null) { }

    public virtual void OnDamageTakenEffect(PlayerData player = null, PlayerData opponent = null) { }
}
