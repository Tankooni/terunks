using Punk;
using System;
using System.Threading;
using System.Collections.Generic;
using GameObjects;
using NHTI.Entities;
using GameObjects;
using Punk.Utils;
using SFML.Window;
using System.IO;

namespace NHTI
{
	/// <summary>
	/// Description of Room.
	/// </summary>
	public class Room : World
	{
		public Entity[] currentEnts;
		public List<Entity> Doors = new List<Entity>();
		Thread roomLoader;
		public Room()
		{
			foreach (string file in Directory.EnumerateFiles("assets/Levels/", "*.oel"))
			{
             	Library.GetXml(file);
			}
			
			roomLoader = new Thread(LoadRooms);
			RegisterClass<wallColTile>("wallCollision");
			RegisterClass<Platform>("platform");
			RegisterClass<GfxTile>("wallGfx");
			RegisterClass<Door>("door");
			RegisterClass<PlayerSpawn>("playerSpawn");
			
			AddList(currentEnts = BuildWorldAsArray("assets/Levels/test.oel"));
			//world.BuildWorld("assets/Levels/test.oel");
			GetType("Door", Doors);
			
			
			this.Add(new Player(300, 300, 1));
		}
		
		public override void Update()
		{
			if(Input.Down(Keyboard.Key.Escape))
			   FP.Screen.Close();
			
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
			if(Input.Down(Keyboard.Key.PageDown))
				Camera.Zoom -= 1*FP.Elapsed;
			else if(Input.Down(Keyboard.Key.PageUp))
				Camera.Zoom += 1*FP.Elapsed;
			FP.Engine.ClearColor = FP.Color(0x123410);
		}
		
		public void LoadRooms()
		{
			
		}
	}
}
