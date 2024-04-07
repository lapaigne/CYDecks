using System;
using Godot;

public partial class DisplayArray : Node2D
{
    public PlayerData data;
    private SDD _health;
    private SDD _money;
    private SDD _shield;
    private SDD _damage;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _health = GetNode<SDD>("Health");
        _money = GetNode<SDD>("Money");
        _shield = GetNode<SDD>("Shield");
        _damage = GetNode<SDD>("Damage");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (data != null)
        {
            _health.NumericalValue = data.Health;
            _money.NumericalValue = data.Money;
            _shield.NumericalValue = data.Shield;
            _damage.NumericalValue = data.Damage;
        }
    }
}
