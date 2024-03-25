using System;
using Godot;

public partial class SDD : AnimatedSprite2D
{
    bool isHidden;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        isHidden = true; // get data from server

        if (isHidden)
        {
            Animation = "idle";
            Play();
        }
        else
        {
            Animation = "display";
            Frame = 8;
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta) { }
}
