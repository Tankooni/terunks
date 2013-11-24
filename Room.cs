using System.Linq;
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
		public Player player;
		bool isFirst = true;
		public bool RoomsAreLoading = true;
		public int enterDoor;
		public Cursor cursor;
		
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
			RegisterClass<Enemy>("groundEnemy");
			
			AddList(currentEnts = BuildWorldAsArray("assets/Levels/test.oel"));
			
			Add(cursor = new Cursor());
			//world.BuildWorld("assets/Levels/test.oel");
		}
		
		public override void Update()
		{
			if(isFirst)
			{
				GetType("Door", Doors);
				roomLoader.Start();
				isFirst = false;
				
				List<Entity> l = new List<Entity>();
				
				Add(player = new Player(300, 300, 1));
				GetType("PlayerSpawn",l);
				player.X = l[0].X;
				player.Y = l[0].Y;
				
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
			
			FP.Engine.ClearColor = FP.Color(0x123410);
		}
		
		public void NextRoom(Door d)
		{
			enterDoor = d.DoorLink;
			RemoveAll();
			currentEnts = Rooms[d];
			AddList(currentEnts);
			Add(player);
			Add(cursor);
			Doors.Clear();
			Rooms.Clear();
			Doors = currentEnts.ToList().FindAll(e => e is Door);
			roomLoader = new Thread(LoadRooms);
			roomLoader.IsBackground = true;
			roomLoader.Start();
			
			Doors.Find( e => (e as Door).DoorNum == enterDoor);
		}
		
		public void LoadRooms()
		{
			RoomsAreLoading = true;
			FP.Log("LoadRoom Start: " + Doors.Count);
			foreach(Entity d in Doors)
			{
				Rooms.Add(d, BuildWorldAsArray("assets/Levels/" + (d as Door).RoomLink + ".oel"));
				FP.Log(d);
			}
			FP.Log("LoadRoom End");
			RoomsAreLoading = false;
		}
	}
}
