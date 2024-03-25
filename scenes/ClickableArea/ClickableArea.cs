using System;
using System.Drawing;
using Godot;

public partial class ClickableArea : Area2D
{
    AnimatedSprite2D mainSprite;
    public bool hasMouse { get; set; }
    bool isOpen;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        isOpen = true;
        hasMouse = false;
        mainSprite = GetNode<AnimatedSprite2D>("MainSprite");

        // get resource data

        var rnd = new RandomNumberGenerator();
        mainSprite.Frame = rnd.RandiRange(0, 17);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta) { }

    public void OnMouseEntered()
    {
        // unreliable

        // var siblings = GetParent().GetChildren();

        // foreach (var s in siblings)
        // {
        //     if (s is ClickableArea && s.GetIndex() != GetIndex()){
        //         ((ClickableArea)s).OnMouseExited();
        //     }
        // }

        if (isOpen)
        {
            if (!hasMouse)
            {
                mainSprite.Position += 10 * Vector2.Up;
                hasMouse = true;
            }
        }
    }

    public void OnMouseExited()
    {
        if (isOpen)
        {
            if (hasMouse)
            {
                mainSprite.Position += 10 * Vector2.Down;
                hasMouse = false;
            }
        }
    }
}
