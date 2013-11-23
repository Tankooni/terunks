
using System;
using Punk;
using NHTI;

namespace GameObjects
{
	/// <summary>
	/// Description of wallColTile.
	/// </summary>
	public class wallColTile : Entity
	{
		public wallColTile()
		{
			Type = "wall";
		}
		
		public override void Load(System.Xml.XmlNode node)
		{
			base.Load(node);
			//SetHitbox(Width * Utils.TILE_SIZE, Height * Utils.TILE_SIZE);
		}
	}
}
