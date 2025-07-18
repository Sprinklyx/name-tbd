using Godot;
using System;

#pragma warning disable CA1050 // Declare types in namespaces

public partial class Mob : Area2D
#pragma warning restore CA1050 // Declare types in namespaces

{
    internal int Health { get; set; } = 15;
    internal int Armor { get; set; } = 2;
    internal int Damage { get; set; } = 1;
    public int Level { get; set; } = 2;
    public Mob()
    {
        Health = 15;
        Armor = 2;
        Damage = 1;
        Level = 2;
    }
    [Signal]
    public delegate void HitEventHandler();
    private void OnAreaEntered(Area2D body)
    {
        
        EmitSignal(SignalName.Hit, "Player");
    }

    internal void Start(Vector2 position)
    {
        Position = position;
        Show();
    }
}
