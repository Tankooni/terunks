﻿
using System;
using NHTI;
using Punk;
using NHTI.Entities;
using Punk.Graphics;

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
		
		public Door()
		{
		}
		
		public override void Update()
		{
			base.Update();
			if(Collide(Player.Collision,X,Y) != null)
			{
				FP.Log("Collided with player");
				
				if(isReady)
					(World as Room).NextRoom(this);
				//else
					//(World as Room).player.
				//else
					
				
			}
		}
		
		public override void Load(System.Xml.XmlNode node)
		{
			base.Load(node);
			Type = Collision;
			Graphic = Image.CreateRect(Width, Height, FP.Color(0xFF00AA));
			face = (DoorFace) Enum.Parse(typeof(DoorFace), node.Attributes["DoorFace"].Value);
		}
	}
}
