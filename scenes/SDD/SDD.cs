using System;
using Godot;

public partial class SDD : Node2D
{
    [Export]
    public bool isHidden = false;
    [Export]
    public byte NumericalValue = 0;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        var left = GetNode<AnimatedSprite2D>("Left");
        var right = GetNode<AnimatedSprite2D>("Right");
         // get data from server and set value/idle

        if (isHidden)
        {
            left.Animation = "idle";
            right.Animation = "idle";
            left.Play();
            right.Play();
        }
        else
        {
            left.Animation = "display";
            left.Frame = NumericalValue / 10 % 10;
            right.Animation = "display";
            right.Frame = NumericalValue % 10;
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta) { }
}
