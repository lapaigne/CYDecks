using System;
using System.Collections.Generic;
using Godot;

public partial class Hand : Node2D
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        PackedScene card = GD.Load<PackedScene>("res://scenes/ClickableArea/ClickableArea.tscn");

        // get data from db

        for (int i = 0; i < 10; i++)
        {
            var instance = (ClickableArea)card.Instantiate();
            // AddChild(instance);
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta) { }
}
