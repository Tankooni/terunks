
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
			X *= Utils.TILE_SIZE;
			Y *= Utils.TILE_SIZE;
			SetHitbox(Width * Utils.TILE_SIZE, Height * Utils.TILE_SIZE);
		}
	}
}
