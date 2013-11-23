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
		public Dictionary<Entity, Entity[]> Rooms = new Dictionary<Entity, Entity[]>();
		Thread roomLoader;
		Player player;
		bool isFirst = true;
		public Room()
		{
			foreach (string file in Directory.EnumerateFiles("assets/Levels/", "*.oel"))
			{
				FP.Log("PreloadingXML");
             	Library.GetXml(file);
			}
			
			roomLoader = new Thread(LoadRooms);
			roomLoader.IsBackground = true;
			RegisterClass<wallColTile>("wallCollision");
			RegisterClass<Platform>("platform");
			RegisterClass<GfxTile>("wallGfx");
			RegisterClass<Door>("door");
			RegisterClass<PlayerSpawn>("playerSpawn");
			
			AddList(currentEnts = BuildWorldAsArray("assets/Levels/test.oel"));
			//world.BuildWorld("assets/Levels/test.oel");
			this.Add(player = new Player(300, 300, 1));
			
			
		}
		
		public override void Update()
		{
			if(isFirst)
			{
				GetType("Door", Doors);
				roomLoader.Start();
				isFirst = false;
			}
			if(Input.Down(Keyboard.Key.Escape))
			   FP.Screen.Close();
			
//			if(Input.Down(Keyboard.Key.Escape))
			base.Update();
			if(Input.Down(Keyboard.Key.PageDown))
				Camera.Zoom -= 1*FP.Elapsed;
			else if(Input.Down(Keyboard.Key.PageUp))
				Camera.Zoom += 1*FP.Elapsed;
			
			//FP.Log(roomLoader.IsAlive);
			
			if(Input.Pressed(Keyboard.Key.H)  && !roomLoader.IsAlive)
			{
				RemoveAll();
				currentEnts = Rooms[Doors[1]];
				AddList(currentEnts);
				Add(player);
			}
			
			FP.Engine.ClearColor = FP.Color(0x123410);
		}
		
		public void LoadRooms()
		{
			FP.Log("LoadRoom Start: " + Doors.Count);
			foreach(Entity d in Doors)
			{
				Rooms.Add(d, BuildWorldAsArray("assets/Levels/" + (d as Door).RoomLink + ".oel"));
				FP.Log(d);
			}
			FP.Log("LoadRoom End");
		}
	}
}
