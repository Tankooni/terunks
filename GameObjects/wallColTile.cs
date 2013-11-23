
using System;
using Punk;

namespace GameObjects
{
	/// <summary>
	/// Description of wallColTile.
	/// </summary>
	public class wallColTile : Entity
	{
		public wallColTile()
		{
			Type = "Wall";
		}
		
		public override void Load(System.Xml.XmlNode node)
		{
			base.Load(node);
			//SetHitbox(Width, Height);
		}
	}
}
