using Godot;
using System;

public class Meteor : Enemy
{

    [Export]
    private int MIN_SPEED = 50; //In pixels/second.

    [Export]
    private int MAX_SPEED = 80; //In pixels/second.

    [Export]
    private int MIN_ROTATE = -20;

    [Export]
    private int MAX_ROTATE = 20;

    private int METEOR_LIFE = 20; //Starting health of a Meteor.

    private float moveRate = 0; //Actual speed of meteor defined in Ready.
    private float rotateRate = 0; //In degrees!

    /*
    Prepare the meteor for gameplay.

    This prepares the meteor's move rate and rotation rate,
    determined by the values of the MIN and MAX constants for each.
    */
    public override void _Ready() {

        setLife(METEOR_LIFE);

        Random rand = new Random();

        moveRate = rand.Next(MIN_SPEED, MAX_SPEED);
        rotateRate = rand.Next(MIN_ROTATE, MAX_ROTATE);
    }

    /*
    Update the Meteor's location on the screen by constantly
    sliding it downward by the given speed.
    */
    public override void _PhysicsProcess(float delta) {

        Vector2 moveByThisMuch = new Vector2(0, moveRate * delta);

        Position += moveByThisMuch;
        RotationDegrees += rotateRate * delta; //We measure in degrees, not radians.
    }

    /*
    When the Meteor leaves the screen, queue it for deletion.
    */
    public void _on_VisibilityNotifier2D_screen_exited() {
        QueueFree();
    }

   



}
