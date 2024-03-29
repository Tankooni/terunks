﻿using System;
using NHTI.Entities;
using Punk;
using GameObjects;
using Punk.Utils;
using SFML.Window;

namespace NHTI
{
	/// <summary>
	/// Description of GameWorld.
	/// </summary>
	public class GameWorld : World
	{
		public GameWorld()
		{
			
		}
		
		public override void Update()
		{
			if(Input.Down(Keyboard.Key.Escape))
			   FP.Screen.Close();
			
			if(Input.Down(Keyboard.Key.Escape))
			base.Update();
			if(Input.Down(Keyboard.Key.Left))
				Camera.X -= 10;
			else if(Input.Down(Keyboard.Key.Right))
				Camera.X += 10;
			if(Input.Down(Keyboard.Key.Up))
				Camera.Y -= 10;
			else if(Input.Down(Keyboard.Key.Down))
				Camera.Y += 10;
			if(Input.Down(Keyboard.Key.PageDown))
				Camera.Zoom -= 1*FP.Elapsed;
			else if(Input.Down(Keyboard.Key.PageUp))
				Camera.Zoom += 1*FP.Elapsed;
			FP.Engine.ClearColor = FP.Color(0x123410);
		}
	}
}
