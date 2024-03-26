using System.Data.Common;
using Godot;

public partial class CardData : Resource
{
    [Export]
    public Vector2 Position;
    [Export]
    public int Id;


    public CardData()
    {
        Position = new Vector2();
        Id = 0;
    }
}