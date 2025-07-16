using Godot;
using System;

public partial class Mob : RigidBody2D
{
    protected int Health { get; set; } = 15;
    protected int Armor { get; set; } = 2;
    protected int Damage { get; set; } = 1;
    public int Level { get; set; } = 2;
    public Mob()
    {
    }

}
