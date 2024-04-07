using System;
using Godot;

public partial class DisplayManager : Node2D
{
    public PlayerData Player;
    public PlayerData Opponent;

    private DisplayArray _player;
    private DisplayArray _opponent;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _player = GetNode<DisplayArray>("PDisplay");
        _opponent = GetNode<DisplayArray>("ODisplay");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        _player.data = Player;
        _opponent.data = Opponent;
    }
}
