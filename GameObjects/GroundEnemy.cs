/*
 * Created by SharpDevelop.
 * User: Matte
 * Date: 11/23/2013
 * Time: 11:35 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using NHTI.Entities;
using NHTI.Entities.Logics;
using Punk;
using Punk.Graphics;
using Punk.Tweens.Misc;
using Punk.Utils;
using SFML.Window;
using System.Collections.Generic;

namespace GameObjects
{
	/// <summary>
	/// Description of Enemy.
	/// </summary>
	/// 
	public class GroundEnemy : Entity
	{
		List<Vector2i> positionNodes = new List<Vector2i>();
		Vector2i nextNode = new Vector2i(0,0);
		
		Spritemap spritemap;
		PhysicsBody physics;
		
		bool isAttacking = false;
		
		private int health = 10; 
		public int Health {
			set{
				health = value;
				if(health <= 0)
					kill();
			}
			get{return health;}
		}
		
		public GroundEnemy() {}
		
		public override void Load(System.Xml.XmlNode node)
		{
			base.Load(node);
			//Graphic = Image.CreateRect(64, 64, FP.Color(0x66FF33));
			
			//make the enemy hittable and all that jazz
			Type = "enemy";
			physics = new PhysicsBody();
			physics.Colliders.Add("wall");
			physics.Colliders.Add("platform");
			physics.Colliders.Add("player");
			AddLogic(physics);
			
			//hitboxs and such
			spritemap= new Spritemap(Library.GetTexture("assets/RoadHeadlowres.png"), 167/2, 257/2, onAnimationEnd);
			spritemap.Add("Idle", FP.Frames(0), 5, true);
			spritemap.Add("Move", FP.MakeFrames(0,2), 5, true);
			spritemap.Add("ChargeAttack", FP.MakeFrames(3,12), 8, false);
			spritemap.Add("Attack", FP.MakeFrames(13,22), 8, false);
			spritemap.Add("Recover", FP.MakeFrames(23,27), 5, false);
			spritemap.Add("Injured", FP.MakeFrames(27,23), 5, true);
			spritemap.Play("Move");
			
			spritemap.OriginX = spritemap.Width/2;
			spritemap.OriginY = spritemap.Height;
			AddGraphic(spritemap);
			
			SetOrigin(spritemap.Width/2, 257/2);
			SetHitbox(spritemap.Width, spritemap.Height, spritemap.Width/2, spritemap.Height);
			
			foreach (System.Xml.XmlNode n in node)
			{
				FP.Log(int.Parse(n.Attributes["x"].Value) + " " + float.Parse(n.Attributes["y"].Value));
				positionNodes.Add(new Vector2i(int.Parse(n.Attributes["x"].Value), int.Parse(n.Attributes["y"].Value)));
			}
			
			
			if(positionNodes.Count > 0)
			{
				nextNode = positionNodes[0];
				MoveToNextPos();
			}
		}
		
		public void MoveToNextPos()
		{
			nextNode = FP.Next(nextNode, positionNodes, true);
			
			//set the animation
			if(nextNode.X - this.X > 0)
				spritemap.FlippedX = true;
			else if(nextNode.X - this.X < 0)
				spritemap.FlippedX = false;
			
			if(!isAttacking)
			{
				if(nextNode.X - this.X == 0)
				{
					spritemap.Play("Idle");
				} else
				{
					spritemap.Play("Move", false);
				}
			}
			
			var twoon = new MultiVarTween(MoveToNextPos, ONESHOT);
			twoon.Tween(this, new { X = nextNode.X, Y = nextNode.Y}, 5.0f);
			AddTween(twoon, true);
	}
		
		public override void Update()
		{
			base.Update();
			
			if(physics.velocity.X > 0)
				spritemap.FlippedX = true;
			else if(physics.velocity.X < 0)
				spritemap.FlippedX = false;
			
			if(!isAttacking)
			{
				if(physics.velocity.X == 0)
				{
					spritemap.Play("Idle");
				} else
				{
					spritemap.Play("Move");
				}
			}
		}
		
		public void kill()
		{
			World.Remove(this);
		}
		
		public void onAnimationEnd()
		{
			if(spritemap.CurrentAnim == "ChargeAttack")
			{
				//shoot player
				spritemap.Play("Attack");
			}
			else if(spritemap.CurrentAnim == "Attack")
			{
				isAttacking = false;
			}
		}
		
		public override bool MoveCollideX(Entity e)
		{
			onCollide(e);
			return base.MoveCollideX(e);
		}
		public override bool MoveCollideY(Entity e)
		{
			onCollide(e);
			return base.MoveCollideY(e);
		}
		
		public void onCollide(Entity e)
		{
			if(e is Player)
			{
				Player p = (Player)e;
				p.onDamage(1, 2, new Vector2f((p.X - X)* .1f, (p.Y - Y)* .1f));
			}
		}
	}
}
