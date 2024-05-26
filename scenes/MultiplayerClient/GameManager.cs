using System.Collections.Generic;
using System.Threading.Tasks;
using Godot;

partial class MultiplayerClient
{
    public void SetGameData()
    {
        Player = new PlayerData { Health = 15 };
        Opponent = new PlayerData();

        TotalList = new List<Card>();

        var list = new List<(int, int, int)> { (1,1,1), (2,3,2), (3,2,3), (4,1,4), (3,2,5) };

        var cardManager = GetNode<CardManager>("Client/CardManager");

        foreach (var (cardId, playerId, unique) in list)
        {
            Card card;

            switch (cardId)
            {
                case 0:
                    card = new Card { Data = new CardData(cardId), CurrentState = SlotType.Draw, OwnerId = playerId };
                    break;

                case 18:
                    card = new Card
                    {
                        Data = new BankCardData(cardId),
                        CurrentState = SlotType.Draw,
                        OwnerId = playerId
                    };
                    break;

                default:
                    card = new Card
                    {
                        Data = new StandardCardData { Id = cardId },
                        CurrentState = SlotType.Draw,
                        OwnerId = playerId
                    };
                    break;
            }
            TotalList.Add(cardManager.AddCard(card));
        }
    }
}
