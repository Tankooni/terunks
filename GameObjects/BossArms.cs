/*
 * Created by SharpDevelop.
 * User: nhti
 * Date: 11/24/2013
 * Time: 2:01 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using NHTI.Entities;
using Punk;
using Punk.Graphics;

namespace NHTI.GameObjects
{
	/// <summary>
	/// Description of BossArms.
	/// </summary>
	public class BossArms : Entity
	{
		public Spritemap arms;
		public Player p;
		public float fireballSpeed = 5;
		
		private int health = 100; 
		public int Health {
			set{
				health = value;
				if(health <= 0)
					kill();
			}
			get{return health;}
		}
		
		public BossArms()
		{
			arms = new Spritemap(Library.GetTexture("assets/Arms.png"), 256, 256, null);
			arms.Add("Idle", FP.Frames(0), 1, true);
			arms.Add("Attack", FP.Frames(0,1), 4, false);
			
			arms.OriginX = 128;
			arms.OriginY = 128;
			
			AddGraphic(arms);
			
			SetHitbox(256,256, 128, 128);
			
			arms.Play("Idle");
		}
		
		public override void Update()
		{
			base.Update();
			
			if(FP.Rand(100) < 5)
			{
				arms.Play("Attack");
				
				float vx = p.X + 32 - X;
				float vy = Y + 32 - p.Y;
				float mag = (float) Math.Sqrt(vx*vx + vy*vy);
				vx *= fireballSpeed / mag;
				vy *= fireballSpeed / mag;
				
				Fireball fireball = new Fireball(vx, vy);
				fireball.X = X + 32;//hand
				fireball.Y = Y + 32;
				World.Add(fireball);
			}
		}
		
		public void kill()
		{
			World.Remove(this);
		}
	}
}
