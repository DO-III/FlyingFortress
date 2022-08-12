using Godot;
using System;

public class Player : Area2D //Extends Area2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	
	[Export]
	private float SPEED = 200; //In pixels per second.

	[Export]
	private float DELAY = 0.2F; //In seconds.

	[Export]
	private int lifeRemaining = 3; //In individual points.
	
	private Vector2 screenSize;
	private Vector2 velocity = Vector2.Zero;
	
	private AnimatedSprite mySprite;
	private Node2D myFiringPositions;
	
	private Timer myFireRateTimer;

	private bool playerInArea = false; //track whether the player is in an area.

	//Store loaded player bullet.
	/*
	First big problem; C# Godot can't preload!

	How we do this is that we instead retrieve the bullet scene as a PackedScene.
	We use GD.Load typecasted as PackedScene and point to the tscn file of the bullet.
	We name with an underscore at the start by convention as this is a resource, not
	a normal type.
	*/
	private PackedScene _bulletScene = (PackedScene)GD.Load("res://Bullet/Bullet.tscn");

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		screenSize = GetViewportRect().Size;
		mySprite = GetNode("AnimatedSprite") as AnimatedSprite;
		myFiringPositions = GetNode("FiringPositions") as Node2D;
		myFireRateTimer = GetNode("FireDelayTimer") as Timer;
		
	}
	
	//For not-so-time-sensitive general behavior.
	//Handles animation at the moment.
	public override void _Process(float delta) {
		//Change frame according to movement.
		if (velocity.x < 0) {
			mySprite.Play("Left");
		} else if (velocity.x > 0) {
			mySprite.Play("Right");
		} else {
			mySprite.Play("Straight");
		}	


		//Check if we're doing the shooty-shoot.
		if (Input.IsActionPressed("shoot") && myFireRateTimer.IsStopped()) {
			/*
			We can't just "drop the bullet in" either - it's the wrong type!

			Either we typecast to a node or declare it separately.
			For readability, we just declare separately.
			*/
			myFireRateTimer.Start(DELAY);


			foreach(Node2D child in myFiringPositions.GetChildren()) {
				Node bullet = _bulletScene.Instance();
				//We use GlobalPosition to make sure it spawns next to the player.
				((Bullet)bullet).GlobalPosition = child.GlobalPosition;
				GetTree().GetRoot().AddChild(bullet);
			}

			/*
			We do not use this line:
			AddChild(bullet);

			That would cause issues because it would be a child of the Player,
			not the Root. As such, it inherits some Player behavior which causes
			jankiness.
			*/

		}
	}
	
	
	//For time-sensitive physics calculations.
	//We do this stuff here as this function makes sure everything fires ASAP.
	public override void _PhysicsProcess(float delta) {
		
		velocity.x = 0;
		velocity.y = 0;
		
		if (Input.IsActionPressed("move_left")) {
			velocity.x = -SPEED;
		} else if (Input.IsActionPressed("move_right")) {
			velocity.x = SPEED;
		}
		
		if (Input.IsActionPressed("move_up")) {
			velocity.y = -SPEED;
		} else if (Input.IsActionPressed("move_down")) {
			velocity.y = SPEED;
		}
		
		velocity = velocity.Normalized() * SPEED;
		
		//Ensure we don't actually leave the screen.
		Position += velocity * delta;
		Position = new Vector2(
			x: Mathf.Clamp(Position.x, 0, screenSize.x),
			y: Mathf.Clamp(Position.y, 0, screenSize.y)
		);
	}


	/*
	Damage the player.

	This function takes of the amount of health specified. When the player's health
	reaches or drops below zero, the player piece is destroyed and any game-over behavior
	should take place.
	*/
	public void damage(int amount) {
		lifeRemaining -= amount;
		GD.Print(String.Format("Life = {0}", lifeRemaining));
		if (lifeRemaining <= 0) {
			//TODO death behavior.
			GD.Print("Player has died");
			QueueFree();
		}
	}

	/*
	Defines behaviors for player-hits-thing cases.

	The player should be able to handle it's own collision logic. The tutorial suggests
	that the meteor should have this logic, but we know that there are many kinds of
	dangerous (and helpful!) entities in the game. Having the player
	handle the logic prevents any assumptions over what should or should-not count, while
	keeping it all confined to one place.
	*/
	public void _on_Player_area_entered(Area2D area) {
		//Enemies and bullets uniformly do 1 point of damage.
		if (area is Enemy) {
            damage(1);
        }
	}
}
