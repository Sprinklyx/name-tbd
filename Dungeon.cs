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
    public void OnMobAttackPlayer()
    {
        Mob enemy = GetNode<Mob>("Mob");
        Player player = GetNode<Player>("Player");
        //set mob's attack pattern
        PathFollow2D mobAttackPath = GetNode<PathFollow2D>("../MobPath/MobPathFollow");

        enemy.Position.MoveToward(player.Position, (float)0.0167);
        
    }

    //logic for after player turn
    public void OnPlayerTurnOver()
    {
        Timer mobAttack = GetNode<Timer>("MobAttack");
        mobAttack.Start();
        if (mobAttack.IsStopped())
        {
            OnMobAttackPlayer();
        }
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
}
