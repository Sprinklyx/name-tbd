using Godot;
using System;

public partial class Main : Node
{

    public void OnPlayerHit()
    {
        Player player = new();
        Mob mob = new();

        Console.Write(player._Get("Health"));

        
        
    }
}
