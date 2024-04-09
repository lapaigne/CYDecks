using System.Collections;
using System.Data.Common;
using Godot;

[GlobalClass]
public partial class BankCardData : CardData
{
    [Export]
    public int Duration;

    [Export]
    public int Initial;

    [Export]
    public int Regular;
    private int _playCounter;

    public BankCardData()
    {
        _playCounter = 0;
    }

    public BankCardData(int Id)
    {
        this.Id = Id;
        _playCounter = 0;
    }

    public override void OnPlayEffect(PlayerData player = null, PlayerData opponent = null)
    {
        GD.Print(_playCounter);
        if (_playCounter == 0)
        {
            player.Money += 10;
        }
        else if (_playCounter <= 4)
        {
            player.Money -= 3;
            if (player.Money < 0)
            {
                player.Health += player.Money;
                player.Money = 0;
                Destroy = true;
            }
        }
        _playCounter++;
        // GD.Print($"Effect of bank card with id={Id} was activated");
    }

    // card manager always checks whether the effect method would be called or not (this happens when card state changes)
    public override void OnClickEffect(PlayerData player = null, PlayerData opponent = null) { }

    public override void OnDiscardEffect(PlayerData player = null, PlayerData opponent = null) { }

    public override void OnDamageTakenEffect(
        PlayerData player = null,
        PlayerData opponent = null
    ) { }
}
