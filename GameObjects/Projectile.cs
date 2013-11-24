/*
 * Created by SharpDevelop.
 * User: Matte
 * Date: 11/23/2013
 * Time: 5:05 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Punk;
using Punk.Graphics;

namespace NHTI.GameObjects
{
	/// <summary>
	/// Description of Projectile.
	/// </summary>
	public class Projectile : Entity
	{
		public Image image;
		
		int xVelocity = 0;
		int yVelocity = 0;
		
		public Projectile(int sentXVel, int sentYVel)
		{
			Type = "Projectile";
			Graphic = image = new Image(Library.GetTexture("assets/Snowball.png"));
			xVelocity = sentXVel;
			yVelocity = sentYVel;
		}

		public override void Update()
		{
			base.Update();
			image.X += xVelocity;
			image.Y += yVelocity;
		}
	}
}
