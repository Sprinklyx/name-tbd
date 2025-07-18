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
    internal int DamageDealt;
    
    private NodePath currentScene;
    [Export]
    public int Speed { get; set; } = 50;
    private Vector2 start_position = new(100, 370);

    public Vector2 ScreenSize;

    //set player start position
    internal void BattleStart(Vector2 position)
    {
        Position = position;
        Show();
    }
    
    //player attack
    public void AttackDamage()
    {
        Mob enemy = GetNode<Mob>("../Mob");
        //player attack logic
        Vector2 target = enemy.Position;
        MoveAndCollide(target);
        PushError("Hit!");
        PushError(DealingDamage());

        ResetPosition();

    }
    //damage dealt to enemy is recorded
    public int DealingDamage()
    {
        Mob enemy = GetNode<Mob>("../Mob");
        DamageDealt = Damage - enemy.Armor;
        if (DamageDealt <= 0)
        {
            PushError("Miss");
        }
        else
        {

            enemy.Health -= Damage;
            PushError("Enemy health = " + enemy.Health);
            

        }
        return DamageDealt;
    }
    //return player to pre-attack position
    protected void ResetPosition()
    {
        Position = start_position;
    }

    //player health lost 
    public int OnMobDamagedPlayer()
    {
        Player player = GetNode<Player>("../Player");
        player.Health -= DealingDamage();
        return player.Health;
    }
    
    
    public Player()
    {
        Health = 10;
        Armor = 0;
        Damage = 5;
        Level = 1;
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
    
}
