using Godot;
using System;
using static Godot.GD;


public partial class Mob : Area2D

{
    internal int Health { get; set; } = 15;
    internal int Armor { get; set; } = 2;
    internal int Damage { get; set; } = 1;
    public int Level { get; set; } = 2;
    internal int DamageDealt;
    public Mob()
    {
        Health = 15;
        Armor = 2;
        Damage = 1;
        Level = 2;
    }


    public void Attack()
    {
        Player player = GetNode<Player>("../Player");
        Position.MoveToward(player.Position, (float)0.0167);
        
    }
    
    
    private void OnAreaEntered(Area2D body)
    {
        //attacks player
        DealingDamage();
    }

    //enemy attack handler
    [Signal]
    private delegate void AttackPlayerEventHandler();

     //enemy attack damage
    public int DealingDamage()
    {
        EmitSignal(SignalName.AttackPlayer, "Player");

        Player player = GetNode<Player>("../Player");

        
        DamageDealt = Damage - player.Armor;

        if (DamageDealt <= 0)
        {
            PushError("Miss");
        }
        else
        {
            //player loses health
            player.Health -= DamageDealt;
            PushError("Player health = " + player.Health);
        }

        return DamageDealt;
    }

    //remove enemy sprite
    public void HealthDepleted()
    {
        if (Health == 0)
        {
            QueueFree();
        }
    }


    //set enemy start position
    internal void Start(Vector2 position)
    {
        Position = position;
        Show();
    }
}
