/*
 * Created by SharpDevelop.
 * User: nhti
 * Date: 11/24/2013
 * Time: 1:23 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using NHTI.Entities.Logics;
using NHTI.Hats;
using Punk;
using Punk.Graphics;
using SFML.Graphics;
using SFML.Window;

namespace NHTI.Entities
{
	/// <summary>
	/// Description of Projectile.
	/// </summary>
	public class Projectile : Entity
	{
		const string type = "projectile";
		static protected Texture tex = Library.GetTexture("assets/Projectiles.png");
		
		public Spritemap spritemap;
		protected delegate void d_onCollide(Entity e);
		protected d_onCollide _onCollide;
		
		public Projectile(float velX, float velY)
		{
			Type = type;
			CenterOrigin();
			SetHitbox(41,41,20,20);
			
			PhysicsBody physics = new PhysicsBody();
			physics.velocity = new Vector2f(velX, velY);
			physics.Colliders.Add("platform");
			physics.Colliders.Add("wall");
			physics.hasGravity = false;
			physics.hasFriction = false;
			physics.maxXVelocity = 5;
			this.AddLogic(physics);
		}
		
		public override bool MoveCollideX(Entity e)
		{
			bool hindered = base.MoveCollideX(e);
			if(hindered)
				_onCollide(e);
			return hindered;
		}
		public override bool MoveCollideY(Entity e)
		{
			bool hindered = base.MoveCollideY(e);
			if(hindered)
				_onCollide(e);
			return hindered;
		}
		
		protected Spritemap makeSpritemap(Spritemap.OnComplete callback)
		{
			return new Spritemap(tex, 41, 41, callback);
		}
	}
}
