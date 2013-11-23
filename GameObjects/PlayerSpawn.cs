using Punk;
using System;

namespace GameObjects
{
	/// <summary>
	/// Description of PlayerSpawn.
	/// </summary>
	public class PlayerSpawn : Entity
	{
		public PlayerSpawn()
		{
		}
		
		public override void Load(System.Xml.XmlNode node)
		{
			base.Load(node);
		}
		
		public override void Added()
		{
			base.Added();
			World.Camera.X = X + HalfHeight;
			World.Camera.Y = Y + HalfWidth;
		}
	}
}
