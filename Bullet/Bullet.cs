using Godot;
using System;

public class Bullet : Area2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    //Determine bullet speed in pixels/second.
    [Export]
    private float SPEED = 500;

    private int damagePerBullet = 1;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }

    /*
	Manage the location of the bullet on the screen and process
	how it should behave.
	
	The bullet moves upwards at the speed defined by SPEED.
	*/

    public override void _PhysicsProcess(float delta)
    {

        Vector2 byThisMuch = new Vector2(0, -SPEED * delta);

        Position += byThisMuch;
    }


    public void SetPos(Vector2 pos)
    {
        Position = pos;
    }

    /*
	Delete bullets that leave the screen to keep the asset
	tree from getting overburdened.
	*/
    public void _on_VisibilityNotifier2D_screen_exited()
    {
        QueueFree();
    }


    /*
	<summary>
	Track when enemies enter the bullet's area.
	</summary>

	Bullet collisions are handled primarily through this method.
	When an object enters the bullet's area, the bullet should deal damage
	to the object by a given amount, then destroy itself.

	"area"
	*/
    public void _on_Bullet_area_entered(Area2D area)
    {
        //TODO This will not work!
        if (area.IsInGroup("damageable")) {
            //area.damage(damagePerBullet);

        }



    }
}
