using System;
using System.Collections.Generic;
using Godot;

public partial class Table : Node2D
{
    public CardData[] OwnPlay;
    public CardData[] OpponentPlay;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        // OpponentPlay = new CardData[];
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta) { }
}
