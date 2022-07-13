using Godot;
using System;

public class Player : KinematicBody2D //Extends Area2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	
	private const float SPEED = 100;
	private Vector2 velocity = Vector2.Zero;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}
	
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
		velocity = MoveAndSlide(velocity);
		
		
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
