using Godot;
using System;

public partial class BattleScene : Control
{
    public void _Ready()
    {
        Markers();
    }

    protected void Markers()
    {
        var attackButton = GetNode<Marker2D>("AttackMarker");
        var AbOneButton = GetNode<Marker2D>("AbOneMarker");
        var AbTwoButton = GetNode<Marker2D>("AbTwoMarker");
        var AbThreeButton = GetNode<Marker2D>("AbThreeMarker");
    }

}
