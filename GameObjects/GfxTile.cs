
using System;
using Punk;
using Punk.Graphics;

namespace GameObjects
{
	/// <summary>
	/// Description of GfxTile.
	/// </summary>
	public class GfxTile : Entity
	{
		public Image image;
		public GfxTile()
		{
		}
		
		public override void Load(System.Xml.XmlNode node)
		{
			base.Load(node);
			image = new Image(Library.GetTexture("assets/platform.png"));
			FP.Log(Width);
			image.ScaleX = Width / (float)image.Width;
			image.ScaleY = Height / (float)image.Height;
			Graphic = image;
			Collidable  = false;
		}
	}
}
