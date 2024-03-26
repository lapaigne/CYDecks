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
        OwnPlay = new CardData[]
        {
            new CardData { Position = new Vector2(164, 109 + 77), Id = -1 },
            new CardData { Position = new Vector2(164 + 40, 109 + 77), Id = -1 },
            new CardData { Position = new Vector2(164 + 80, 109 + 77), Id = -1 },
            new CardData { Position = new Vector2(164 + 120, 109 + 77), Id = -1 },
        };

        OpponentPlay = new CardData[]
        {
            new CardData(),
            new CardData(),
            new CardData(),
            new CardData(),
        };

        // OpponentPlay = new CardData[];
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta) { }
}
