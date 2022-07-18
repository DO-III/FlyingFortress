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

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}
	
	/*
	Manage the location of the bullet on the screen and process
	how it should behave.
	
	The bullet moves upwards at the speed defined by SPEED.
	*/
	public override void _PhysicsProcess(float delta) {

		Vector2 byThisMuch = new Vector2(0, -SPEED * delta);

		Position += byThisMuch;
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
