using Godot;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using static Godot.GD;

public partial class Player : CharacterBody2D
{
    
    internal int Health { get; set; } = 10;
    internal int Armor { get; set; } = 0;
    internal int Damage { get; set; } = 5;
    public int Level { get; set; } = 1;
    
    private NodePath currentScene;
    [Export]
    public int Speed { get; set; } = 50;

    public Vector2 ScreenSize;

    [Signal]
    public delegate void HitEventHandler();
    protected void OnAreaEntered(Area2D body)
    {
        EmitSignal(SignalName.Hit);

        //GetNode<CollisionShape2D>("FarmerCollider").SetDeferred(CollisionShape2D.PropertyName.Transform, true);
    }
    
    public Player()
    {
        Health = 10;
        Armor = 0;
        Damage = 5;
        Level = 1;
    }
    [Signal]
    public delegate void AttackEventHandler();
    protected void OnButtonPressed()
    {
        EmitSignal(SignalName.Attack);
        PhysicsProcess(0.0167);
    }

    private void PhysicsProcess(double delta)
    {
        Vector2 target = GetNode<Mob>("Mob").Position;
        MoveAndCollide(target);
    }
    public override void _Ready()
    {
        ScreenSize = GetViewportRect().Size;
        currentScene = GetTree().CurrentScene.SceneFilePath;
        PushError(currentScene);
    }

    public override void _Process(double delta)
    {
        if (currentScene == "res://Town.tscn")
        {
            var velocity = Vector2.Zero;

            if (Input.IsActionPressed("move_right"))
            {
                velocity.X += 1;
            }

            if (Input.IsActionPressed("move_left"))
            {
                velocity.X -= 1;
            }

            if (Input.IsActionPressed("move_down"))
            {
                velocity.Y += 1;
            }

            if (Input.IsActionPressed("move_up"))
            {
                velocity.Y -= 1;
            }

            var sprite2D = GetNode<Sprite2D>("Farmer");

            if (velocity.Length() > 0)
            {
                velocity = velocity.Normalized() * Speed;
            }

            Position += velocity * (float)delta;
            Position = new Vector2(
                x: Mathf.Clamp(Position.X, 0, ScreenSize.X),
                y: Mathf.Clamp(Position.Y, 0, ScreenSize.Y)
            );
        }
        
    }
    
    internal void BattleStart(Vector2 position)
    {
        Position = position;
        Show();
    }
}
