
using System;
using NHTI;
using Punk;
using NHTI.Entities;
using Punk.Graphics;
using Punk.Utils;
using SFML.Window;

namespace GameObjects
{
	public enum DoorFace
	{
		Up,
		Down,
		Right,
		Left
	}
	
	/// <summary>
	/// Description of Door.
	/// </summary>
	public class Door : Entity
	{
		public string RoomLink = "test";
		public int DoorNum = 0, DoorLink = 0;
		public DoorFace face;
		public const string Collision = "Door";
		public bool isReady = false;
		public bool RequiresKey;
		
		public Door()
		{
		}
		
		public override void Update()
		{
			base.Update();
			if((World as Room).RoomsAreLoading)
				(Graphic as Image).Color = FP.Color(0xFF00AA);
			else
				(Graphic as Image).Color = FP.Color(0xFF0000);
			if(Collide(Player.CollisionType,X,Y) != null)
			{
				FP.Log("Collided with player");
				
				if(!(World as Room).RoomsAreLoading && Input.Pressed(Keyboard.Key.W))
					(World as Room).NextRoom(this);
			}
		}
		
		public override void Load(System.Xml.XmlNode node)
		{
			base.Load(node);
			Type = Collision;
			Graphic = Image.CreateRect(Width, Height, FP.Color(0x000000));
			face = (DoorFace) Enum.Parse(typeof(DoorFace), node.Attributes["DoorFace"].Value);
		}
	}
}
