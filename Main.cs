using Godot;
using System;
using static Godot.GD;

public partial class Main : Node
{
    internal int PlayerDamageTaken;
    internal int MobDamageTaken;

    public override void _Ready()
    {
        EnterBattle();
    }


    public void EnterBattle()
    {
        //adding player to scene
        var player = GetNode<Player>("Player");
        //player start position
        var playerStart = GetNode<Marker2D>("PlayerStart");
        player.BattleStart(playerStart.Position);
        //adding enemy to scene
        var mob = GetNode<Mob>("Mob");
        //mob start position
        var mobStart = GetNode<Marker2D>("MobStart");
        mob.Start(mobStart.Position);

        var playerAttack = GetNode<Button>("Button");

    }


    public void OnPlayerHit()
    {
        var player = GetNode<Player>("Player");
        var mob = GetNode<Mob>("Mob");

        PlayerDamageTaken -= mob.Damage - player.Armor;
        if (PlayerDamageTaken < 0)
        {
            Console.WriteLine("Miss");
        }
        else
        {
            player.Health -= PlayerDamageTaken;
            Print(player.Health);

        }
    }
    public void OnMobHit()
    {
        var player = GetNode<Player>("Player");
        var mob = GetNode<Mob>("Mob");

        MobDamageTaken -= player.Damage - mob.Armor;
        if (MobDamageTaken < 0)
        {
            Console.WriteLine("Miss");
        }
        else
        {
            mob.Health -= MobDamageTaken;
            Print(mob.Health);
        }
    }

    public void OnButtonPressed()
    {
        Print("button pressed");

        var player = GetNode<Player>("Player");
        var mob = GetNode<Mob>("Mob");
        var target = mob.Position;

        player._PhysicsProcess(0.0167);
        player.Velocity = player.Position.DirectionTo(target) * player.Speed;
        for (int i = 0; i < player.Position.DistanceTo(target) + 300; i++)
        {
            player.MoveAndSlide();
            i++;
        }
        
        
        

    }
    

}
    