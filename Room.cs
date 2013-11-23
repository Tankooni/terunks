using Punk;
using System;
using System.Collections.Generic;
using GameObjects;

namespace NHTI
{
	/// <summary>
	/// Description of Room.
	/// </summary>
	public class Room : Entity
	{
		public List<Door> Doors = new List<Door>();
		public Room()
		{
		}
	}
}
