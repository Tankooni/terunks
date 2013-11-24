
using System;
using NHTI;
using Punk;
using Punk.Graphics;
using NHTI.Entities;
using GameObjects;

namespace GameObjects
{
	/// <summary>
	/// Description of Cursor.
	/// </summary>
	public class Cursor : Entity
	{
		public Cursor()
		{
			Graphic = Image.CreateCircle(10, FP.Color(0x553366));
			(Graphic as Image).CenterOrigin();
			FP.Screen.SetMouseCursorVisible(false);
			Layer = -10000;
		}
		public override void Update()
		{
			base.Update();
			X = World.MouseX;
			Y = World.MouseY;
		}
	}
}
