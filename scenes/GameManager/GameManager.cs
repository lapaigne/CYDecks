using System;
using Godot;

public partial class GameManager : Node2D
{
    public enum GameState { 
        
    }

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
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta) { }
}
