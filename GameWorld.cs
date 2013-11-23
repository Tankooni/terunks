using System;
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
			RegisterClass<wallColTile>("wallCollision");
			RegisterClass<Platform>("platform");
			RegisterClass<GfxTile>("wallGfx");
			
			BuildWorld("assets/Levels/test.oel");
			
			this.Add(new Player(300, 300, 1));
		}
		
		public override void Update()
		{
			
//			if(Input.Down(Keyboard.Key.Escape))
			base.Update();
			if(Input.Down(Keyboard.Key.Left))
				Camera.X -= 10;
			else if(Input.Down(Keyboard.Key.Right))
				Camera.X += 10;
			if(Input.Down(Keyboard.Key.Up))
				Camera.Y -= 10;
			else if(Input.Down(Keyboard.Key.Down))
				Camera.Y += 10;
			FP.Engine.ClearColor = FP.Color(0x123410);
		}
	}
}
