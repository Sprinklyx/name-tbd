using Godot;
using System;

public partial class BattleScene : GridContainer
{
    internal void BattleUI(Vector2 position)
    {
        Position = position;
        Show();
    }

    //add signal handler
    [Signal]
    public delegate void AttackEventHandler();
    protected void PlayerAttack()
    {
        //send signal out
        EmitSignal(SignalName.Attack);
    }

}
