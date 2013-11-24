﻿
using System;
using Punk;
using Punk.Graphics;

namespace GameObjects
{
	/// <summary>
	/// Description of BackDrop.
	/// </summary>
	public class BackDrop : Entity
	{
		public string path;
		public Image image;
		public BackDrop()
		{
		}
		public override void Load(System.Xml.XmlNode node)
		{
			base.Load(node);
			Graphic = image = new Image(Library.GetTexture("assets/" + path + ".png"));
			
			image.ScrollX = 0;
			image.ScrollY = 0;
		}
	}
}
