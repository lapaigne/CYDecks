using System;
using System.Collections.Generic;
using Godot;

public partial class GameManager : Node2D
{
    public enum GameState { }

    public PlayerData Player;
    public PlayerData Opponent;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        // get initial data from server
        Player = new PlayerData { Health = 15 };
        Opponent = new PlayerData();

        var displayManager = GetNode<DisplayManager>("/root/Board/DisplayManager");
        displayManager.Player = Player;
        displayManager.Opponent = Opponent;

        var cardManager = GetNode<CardManager>("/root/Board/CardManager");
        cardManager.Player = Player;
        cardManager.Opponent = Opponent;

        cardManager.PlayerDeck = new Queue<Card>();
        cardManager.OpponentDeck = new Queue<Card>();
        var rnd = new RandomNumberGenerator();

        for (int i = 0; i < 1; i++)
        {
            var number = rnd.RandiRange(0, 19);
            switch (number)
            {
                case 3:
                    cardManager.PlayerDeck.Enqueue(
                        new Card
                        {
                            Data = new StandardCardData { Id = number, Health = -1 },
                            CurrentState = SlotType.Draw
                        }
                    );
                    break;
                case 18:
                    cardManager.PlayerDeck.Enqueue(
                        new Card { Data = new BankCardData(number), CurrentState = SlotType.Draw }
                    );
                    break;
                default:
                    cardManager.PlayerDeck.Enqueue(
                        new Card { Data = new CardData(number), CurrentState = SlotType.Draw }
                    );
                    break;
            }
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta) { }
}
