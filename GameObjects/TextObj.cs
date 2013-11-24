
using System;
using Punk;
using Punk.Graphics;

namespace GameObjects
{
	/// <summary>
	/// Description of TextObj.
	/// </summary>
	public class TextObj : Entity
	{
		public string line1;
		public string line2;
		public string line3;
		public Text text;
		public TextObj() {}
		
		public override void Load(System.Xml.XmlNode node)
		{
			base.Load(node);
			Graphic = text = new Text(line1 +"\n"+line2+"\n"+line3);
			text.OriginX = text.X + text.Width/2.0f;
			text.OriginY = text.Y + text.Height/2.0f;
		}
	}
}
