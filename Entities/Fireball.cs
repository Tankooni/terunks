/*
 * Created by SharpDevelop.
 * User: nhti
 * Date: 11/24/2013
 * Time: 2:24 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Punk;
using SFML.Window;

namespace NHTI.Entities
{
	/// <summary>
	/// Description of Fireball.
	/// </summary>
	public class Fireball : Projectile
	{
		public Fireball(float velX, float velY)
			:base(velX, velY)
		{
			spritemap = makeSpritemap(null);
			spritemap.Add("Fireball", FP.Frames(13,14), 20, true);
			
			AddGraphic(spritemap);
			
			spritemap.Play("Fireball");
			
			_onCollide = onCollide;
			physics.Colliders.Add("player");
			physics.Colliders.Remove("enemy");
			
			physics.maxXVelocity = 30;
		}
		
		public void onCollide(Entity e)
		{
			World.Remove(this);
			
			if(e is Player)
			{
				Player p = (Player)e;
				p.onDamage(1, 2, new Vector2f((p.X - X)* .1f, (p.Y - Y)* .1f));
			}
		}
	}
}
