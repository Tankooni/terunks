using System;
using Punk;
using Punk.Graphics;

namespace GameObjects
{
	/// <summary>
	/// Description of Platform.
	/// </summary>
	public class Platform : Entity
	{
		public Nineslice image;
		public Platform()
		{
			Type = "platform";
		}
		
		public override void Load(System.Xml.XmlNode node)
		{
			base.Load(node);
			image = new Nineslice(Library.GetTexture("assets/platform.png"), 3, 3);
			
			image.ScaleX = Width / image.Width;
			image.ScaleY = Height / image.Height;
			
			Graphic = image;
		}
	}
}
