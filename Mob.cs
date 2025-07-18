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

    

    [Signal]
    public delegate void DamagedPlayerEventHandler();
    private void OnAreaEntered(Area2D body)
    {
        //signal sent out        
        EmitSignal(SignalName.DamagedPlayer, "Player");

        //attacks player
        DealingDamage();
    }

    //enemy attack damage
    public int DealingDamage()
    {
        Player player = GetNode<Player>("../Player");

        DamageDealt = Damage - player.Armor;
       
        if (DamageDealt <= 0)
        {
            PushError("Miss");
        }
        else
        {
            //player loses health
            player.Health -= Damage;
            PushError("Player health = " + player.OnMobDamagedPlayer());
        }

        return DamageDealt;
    }

    


    //set enemy start position
    internal void Start(Vector2 position)
    {
        Position = position;
        Show();
    }
}
