
using System;
using Punk;
using Punk.Graphics;

namespace GameObjects
{
	/// <summary>
	/// Description of BackDrop.
	/// </summary>
	public class BackDrop
	{
		public Image image;
		public BackDrop(string path) : Entity
		{
			Graphic = image = Library.GetTexture(path);
			image.ScrollX = image.ScrollY = 0;
		}
	}
}
