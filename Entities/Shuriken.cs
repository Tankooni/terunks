/*
 * Created by SharpDevelop.
 * User: nhti
 * Date: 11/24/2013
 * Time: 7:20 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using GameObjects;
using Punk;

namespace NHTI.Entities.Logics
{
	/// <summary>
	/// Description of Shuriken.
	/// </summary>
	public class Shuriken : Projectile
	{
		public Shuriken(float velX, float velY)
			:base(velX, velY)
		{
			spritemap = makeSpritemap(null);
			spritemap.Add("Ninja", FP.Frames(18,19), 20, true);
			
			AddGraphic(spritemap);
			
			spritemap.Play("Ninja");
			
			_onCollide = onCollide;
			
			physics.maxXVelocity = 30;
		}
		
		public void onCollide(Entity e)
		{
			World.Remove(this);
			
			if(e is GroundEnemy)
			{
				GroundEnemy enemy = (GroundEnemy)e;
				enemy.Health -= 2;
			}
		}
	}
		
		
}
