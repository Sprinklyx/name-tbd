using Godot;
using System;
using static Godot.GD;

public partial class Dungeon : Node
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

        //adding battle ui to scene
        var playerButtons = GetNode<BattleScene>("BattleScene");
        //adding visible ui location
        var ButtonStart = GetNode<Marker2D>("UIMarker");
        playerButtons.BattleUI(ButtonStart.Position);
    }


    public void OnPlayerHit()
    {
        var player = GetNode<Player>("Player");
        var mob = GetNode<Mob>("Mob");
        PushError(player.Health);
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

        if (mob.OverlapsArea(player) == true)
        {
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
    }
}
