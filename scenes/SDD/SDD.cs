using System;
using Godot;

public partial class SDD : Node2D
{
    [Export]
    public bool IsHidden = false;

    [Export]
    public int NumericalValue = 0;

    [Export]
    public Color Color;

    private AnimatedSprite2D _left;
    private AnimatedSprite2D _right;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        ((ShaderMaterial)Material).SetShaderParameter("new", Color);

        _left = GetNode<AnimatedSprite2D>("Left");
        _right = GetNode<AnimatedSprite2D>("Right");
        // get data from server and set value/idle
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        _left.Material = Material;
        _right.Material = Material;

        if (IsHidden && !_left.IsPlaying())
        {
            _left.Animation = "idle";
            _right.Animation = "idle";
            _left.Play();
            _right.Play();
        }
        else if (!IsHidden)
        {
            _left.Animation = "display";
            _left.Frame = NumericalValue / 10 % 10;
            _right.Animation = "display";
            _right.Frame = NumericalValue % 10;
        }
    }
}
