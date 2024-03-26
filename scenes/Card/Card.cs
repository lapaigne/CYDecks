using System;
using Godot;

public partial class Card : Area2D
{
    [Export]
    public bool isOpen = true;

    [Export]
    public bool canSlide = false;

    [Export]
    public bool canClick = true;

    AnimatedSprite2D mainSprite;
    bool hasMouse;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
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
        // slide back every but this card; unreliable
        // card won't slide up if cursor was in the area2d

        var siblings = GetParent().GetChildren();

        foreach (var s in siblings)
        {
            if (s is Card && s.GetIndex() != GetIndex())
            {
                ((Card)s).OnMouseExited();
            }
        }

        //

        if (canClick)
        {
            if (Input.IsActionJustPressed("click")) { }
        }

        if (isOpen)
        {
            if (!hasMouse && canSlide)
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
            if (hasMouse && canSlide)
            {
                mainSprite.Position += 10 * Vector2.Down;
                hasMouse = false;
            }
        }
    }

    public void OnClick()
    {
        // get free positions from table
        
        
    }
}
