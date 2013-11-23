
using System;
using Punk;

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
		public DoorFace doorFace;
		
		public Door()
		{
		}
		
		public override void Load(System.Xml.XmlNode node)
		{
			
		}
	}
}
