using Godot;
using System;

public class Player : Area2D //Extends Area2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	
	[Export]
	private float SPEED = 200; //Move speed.
	
	private Vector2 screenSize;
	private Vector2 velocity = Vector2.Zero;
	
	private Sprite mySprite;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		screenSize = GetViewportRect().Size;
		mySprite = GetNode("Sprite") as Sprite;
		
	}
	
	//For not-so-time-sensitive general behavior.
	//Handles animation at the moment.
	public override void _Process(float delta) {
		//Change frame according to movement.
		if (velocity.x < 0) {
			mySprite.SetFrame(0);
		} else if (velocity.x > 0) {
			mySprite.SetFrame(2);
		} else {
			mySprite.SetFrame(1);
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
}
