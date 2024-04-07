using Godot;
using System;

public partial class Debug : Control
{
    private Label _label;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        _label = GetNode<Label>("Label");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        _label.Text = $"{Engine.GetFramesPerSecond()} fps";
	}
}
