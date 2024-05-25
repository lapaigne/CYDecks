using System;
using System.Collections.Generic;
using Godot;

public partial class GameManager : Node2D
{
    public enum GameState { }

    public PlayerData Player;
    public PlayerData Opponent;

    // get rid of this class, 'tis useless
    public override void _Ready()
    {
        Player = new PlayerData { Health = 15 };
        Opponent = new PlayerData();

        var cardManager = GetParent().GetNode<CardManager>("CardManager");
        cardManager.Player = Player;
        cardManager.Opponent = Opponent;

        cardManager.PlayerDeck = new Queue<Card>();
        cardManager.OpponentDeck = new Queue<Card>();
        // var rnd = new RandomNumberGenerator();
        var list = new List<int>();
        // for (int i = 0; i < 1; i++)
        foreach (var number in list)
        {
            // var number = rnd.RandiRange(0, 19);
            switch (number)
            {
                case 0:
                    cardManager.PlayerDeck.Enqueue(
                        new Card { Data = new CardData(number), CurrentState = SlotType.Draw }
                    );
                    break;

                case 18:
                    cardManager.PlayerDeck.Enqueue(
                        new Card { Data = new BankCardData(number), CurrentState = SlotType.Draw }
                    );
                    break;

                default:
                    cardManager.PlayerDeck.Enqueue(
                        new Card
                        {
                            Data = new StandardCardData { Id = number, Health = -1 },
                            CurrentState = SlotType.Draw
                        }
                    );
                    break;
            }
        }
    }
    public override void _Process(double delta) { }
}
