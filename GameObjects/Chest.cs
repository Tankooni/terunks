
using System;
using NHTI;
using Punk;
using Punk.Graphics;

namespace GameObjects
{
	/// <summary>
	/// Description of Chest.
	/// </summary>
	public class Chest : Entity
	{
		Image image;
		HatType contents;
		public Chest(){	}
		
		public override void Load(System.Xml.XmlNode node)
		{
			base.Load(node);
			Graphic = image = new Image(Library.GetTexture("assets/SnowHead.png"));
			contents = (HatType) Enum.Parse(typeof(HatType), node.Attributes["chestContents"].Value);
		}
		
		public override void Update()
		{
			base.Update();
		}
	}
}
