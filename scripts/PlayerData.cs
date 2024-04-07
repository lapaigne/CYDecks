using System.Data.Common;
using Godot;

[GlobalClass]
public partial class PlayerData : Resource
{
    [Export]
    public int Id;

    [Export]
    public int Health = 0;

    [Export]
    public int Shield = 0;

    [Export]
    public int Damage = 0;

    [Export]
    public int Money = 0;

    public PlayerData() { }
}
