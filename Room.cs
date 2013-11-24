using System.Linq;
using NHTI.GameObjects;
using Punk;
using System;
using System.Threading;
using System.Collections.Generic;
using GameObjects;
using NHTI.Entities;
using Punk.Graphics;
using Punk.Utils;
using SFML.Window;
using System.IO;

namespace NHTI
{
	public enum HatType
	{
		Random,
		NoHat,
		TopHat,
		WWIHat,
		Trophy,
		CatEars,
		Ninja,
		Tiara,
		Raiden,
		Banana,
		Key,
		Device,
		Fez,
		Dunce,
		Bow
	}
	public enum BackGrounds
	{
		Mountain,
		Arctic,
		Space,
		Boss
	}
	
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
		public Door startDoor;
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
			RegisterClass<GroundEnemy>("groundEnemy");
			RegisterClass<TextObj>("textObj");
			RegisterClass<FlyingEnemy>("flyingEnemy");
			RegisterClass<Chest>("chest");
			RegisterClass<Boss>("bossSpawn");
			
			AddList(currentEnts = BuildWorldAsArray("assets/Levels/Level01.oel"));
			
			Add(cursor = new Cursor());
		}
		
		public override void Update()
		{
			base.Update();
			
			if(isFirst)
			{
				GetType("Door", Doors);
				roomLoader.Start();
				isFirst = false;
				
				List<Entity> l = new List<Entity>();
				
				Add(player = new Player(300, 300, 1));
				GetType("PlayerSpawn",l);
				//FP.Log(l[0].);
				player.X = l[0].X + player.HalfWidth;
				player.Y = l[0].Y + player.HalfHeight;
				
			}
			
			if(Input.Down(Keyboard.Key.Escape))
			   FP.Screen.Close();
			if(Input.Down(Keyboard.Key.PageDown))
				Camera.Zoom -= 1*FP.Elapsed;
			else if(Input.Down(Keyboard.Key.PageUp))
				Camera.Zoom += 1*FP.Elapsed;
			
			//FP.Log(roomLoader.IsAlive);
			
			FP.Engine.ClearColor = FP.Color(0x123410);
		}
		
		public void NextRoom(Door d)
		{
//			
			
			enterDoor = d.DoorLink;
			if(d.RoomLink != "")
			{
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
			}
			
			var door = Doors.Find( e => (e as Door).DoorNum == enterDoor) as Door;
			if(door.face == DoorFace.Left)
			{
				player.X = door.Left - player.HalfWidth;
				player.Y = door.Bottom;
			} 
			else if(door.face == DoorFace.Right)
			{
				player.X = door.Right + player.HalfWidth;
				player.Y = door.Bottom;
			}
			else if(door.face == DoorFace.Up)
			{
				player.X = door.HalfWidth;
				player.Y = door.Top;
			}
			else if(door.face == DoorFace.Down)
			{
				player.X = door.X + door.HalfWidth;
				player.Y = door.Bottom + player.Height;
			}
		}
		
		public void LoadRooms()
		{
			RoomsAreLoading = true;
			foreach(Entity d in Doors)
			{
				if((d as Door).RoomLink != "")
					Rooms.Add(d, BuildWorldAsArray("assets/Levels/" + (d as Door).RoomLink + ".oel"));
			}
			RoomsAreLoading = false;
		}
	}
}
