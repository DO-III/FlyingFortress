using Godot;
using System;

public class Enemy : Area2D
{
    //DO NOT DECLARE ANY INSTANCES OF THIS CLASS!

    //This class exists as a chummy workaround for Godot's C# support.

    //If you were using GDScript to make the game and wanted to implement common enemy damage,
    //you would be highly disappointed. You cannot simply "guess" the types; you need to be specific.
    //As such, this class declares some basic behavior for an Enemy so we can find them,
    //and damage them individually.


    //Represents the amount of life something has.
    private int life;



    /*
    Damage the instance by the amount specified.

    This damages the enemy and decreases its life value by 1 each time it is called.
    For example, if the amount entered is 5, it will deal 5 points of damage to the enemy.

    Once the enemy drops to 0 health or below, it is destroyed.
    */
    public void damage(int amount) {
        life -= amount;
        if (life <= 0) {
            QueueFree();
        }
    }

    public void setLife(int amount) {
        life = amount;
    }
}
