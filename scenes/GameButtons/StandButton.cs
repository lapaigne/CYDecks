using System;
using Godot;

public partial class StandButton : Button
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        // Pressed += GetNode<CardManager>("CardManager").ButtonPressed;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta) { }

    
}
