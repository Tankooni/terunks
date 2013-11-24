/*
 * Created by SharpDevelop.
 * User: nhti
 * Date: 11/23/2013
 * Time: 12:12 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using NHTI.Entities.Logics;
using NHTI.Hats;
using Punk;
using Punk.Graphics;
using Punk.Utils;
using SFML.Graphics;
using SFML.Window;
using GameObjects;

namespace NHTI.Entities
{
		public struct AnimationData
		{
			public AnimationData(int fps, int[] frames, bool loop, int[] yFacePoints)
			{
				this.fps = fps;
				this.frames = frames;
				this.loop = loop;
				this.yFacePoints = yFacePoints;
			}
			public int fps;
			public int[] frames;
			public bool loop;
			
			public int[] yFacePoints;
		}
	
	/// <summary>
	/// Description of Player.
	/// </summary>
	public class Player : Entity
	{
		public const string CollisionType = "player";
		public uint id;
		public Controller controller;
		
		public CameraFollow cameraFollow;
		//Animation shtuffs
		private Spritemap bodySprites;
		private Spritemap faceSprites;
		
		public static Dictionary<string, AnimationData> BodyAnimDict = new Dictionary<string, AnimationData>()
		{
			{ "Idle", new AnimationData (10, FP.Frames(0), true, new int[]{160})},
			{ "Run", new AnimationData (8, FP.MakeFrames(2,20), true, 
			                            new int[]{150,150,160,160,170,170,180,180,165,165,150,150,160,160,170,170,180,170,165,165})},
			{ "Duck", new AnimationData (6, FP.MakeFrames(21,24), false, new int[]{150,200,240,260})},
			{ "DuckIdle", new AnimationData (10, FP.Frames(25), true, new int[]{260})},
			{ "DuckStand", new AnimationData (6, FP.MakeFrames(21,24).Reverse().ToArray(), false, new int[]{260,240,200,150})},
			{ "DuckShuffle", new AnimationData (8, FP.MakeFrames(26,35), true, new int[]{260,260,260,260,260,260,260,260,260,260})},
			{ "Jump", new AnimationData (6, FP.MakeFrames(36,41), false, new int[]{150,150,75,75,65,65})},
			{ "JumpIdle", new AnimationData (4, FP.Frames(40,41), true, new int[]{65,65})},
			{ "Fall", new AnimationData (6, FP.MakeFrames(42,47), false, new int[]{65,65,115,115,150,150})},
			{ "FallIdle", new AnimationData (4, FP.Frames(46,47), true, new int[]{150,150})},
			{ "Dash", new AnimationData (10, FP.Frames(48,49), true, null)},
			{ "Stagger1", new AnimationData (8, FP.MakeFrames(50,54), false, null)},
			{ "Stagger2", new AnimationData (8, FP.MakeFrames(55,59), false, null)},
			{ "Death", new AnimationData (4, FP.MakeFrames(60,91), false, null)},
			{ "Dance", new AnimationData (6, FP.MakeFrames(92, 106), false, null)}
		};
		
		public static Dictionary<string, AnimationData> FaceAnimDict = new Dictionary<string, AnimationData>()
		{
			{ "None", new AnimationData (10, FP.Frames(0), true, null)},
			{ "Idle", new AnimationData (10, FP.Frames(1), true, null)},
			{ "Run", new AnimationData (8, FP.MakeFrames(2,21), true, null)},
			{ "Jump", new AnimationData (8, FP.MakeFrames(22, 25), false, null)},
			{ "JumpIdle", new AnimationData (4, FP.MakeFrames(26, 27), true, null)},
			{ "Fall", new AnimationData (4, FP.MakeFrames(28, 31, 1), false, null)},
			{ "FallIdle", new AnimationData (4, FP.MakeFrames(32, 33), true, null)},
			{ "ChargeAttack", new AnimationData (3, FP.MakeFrames(34, 41), false, null)},
			{ "ChargeAttackLoop", new AnimationData (1, FP.MakeFrames(42,45), true, null)},
			{ "Release", new AnimationData (6, FP.MakeFrames(46, 57), false, null)}
		};
		
		//Stats
		public int health = 6;
		public int maxHealth = 6;
		
		public Hat hat;
		
		public float jumpForce = 20.0f;
		public float speed = 1f;
		
		//Other stuff
		PhysicsBody physics;
		bool isDucking = false;
		bool isAttacking = false;
		bool isHurt = false;
		
		public Player(float x, float y, uint id) : base(x,y)
		{
			Type = CollisionType;
			this.id = id;
			this.controller = new Controller(id);
			
			cameraFollow = new CameraFollow();
			AddLogic(cameraFollow);
			
			physics = new PhysicsBody();
			physics.Colliders.Add("platform");
			physics.Colliders.Add("wall");
			physics.maxXVelocity = 8f;
			AddLogic(physics);
			
			//Add sprites
			bodySprites = new Spritemap(Library.GetTexture("assets/Timunkslowres.png"), 88, 100, OnAnimationEndBody);
			bodySprites.OriginX = 44;
			bodySprites.OriginY = 100;
			
			faceSprites = new Spritemap(Library.GetTexture("assets/TimunksFacetestlowres2.png"), 42, 34, OnAnimationEndFace);
			faceSprites.CenterOrigin();
			faceSprites.OriginX = 21;
			
			//add all the things
			foreach(KeyValuePair<string, AnimationData> entry in BodyAnimDict)
				bodySprites.Add(entry.Key, entry.Value.frames, entry.Value.fps, entry.Value.loop);
			foreach(KeyValuePair<string, AnimationData> entry in FaceAnimDict)
				faceSprites.Add(entry.Key, entry.Value.frames, entry.Value.fps, entry.Value.loop);
			
			bodySprites.Play("Idle");
			faceSprites.Play("Idle");
			
			AddGraphic(bodySprites);
			AddGraphic(faceSprites);
			
			this.SetOrigin(44, 100);
			this.SetHitbox(48, 100, 24, 100);
			
			//Add a hat
			hat = new NoHat(this);
		}
		
		public override void Added()
		{
			base.Added();
			World.Add(hat);
		}
		
		public override void Update()
		{			
			//Movement
			if(!isHurt)
			{
				if(Input.Pressed(Keyboard.Key.Space) && physics.velocity.Y == 0)
				{
					physics.velocity.Y += jumpForce;
					bodySprites.Play("Jump");
					if(!isAttacking) faceSprites.Play("Jump");
				}
				
				if(Input.Check(Keyboard.Key.A))
					physics.velocity.X -= this.speed;
				if(Input.Check(Keyboard.Key.D))
					physics.velocity.X += this.speed;
				
				if(Input.Check(Keyboard.Key.S))
				{
					if(!isDucking)
						bodySprites.Play("Duck");
					isDucking = true;
				}
				else
				{
					if(isDucking)
						bodySprites.Play("Stand");
					isDucking = false;
				}
			
				//Attack
				if(Input.Pressed(Mouse.Button.Left))
				{
					string nextAnim = hat.attackStart();
					if(nextAnim != "")
						faceSprites.Play(nextAnim);
					isAttacking = true;
				}
				else if(Input.Released(Mouse.Button.Left))
				{
					faceSprites.Play("Release");
				}
				
			}
			
			base.Update();
			
			//Animation stuff
			if(!isHurt)
			{
				//Flippin' stuffs
				if(physics.velocity.X > 0)
				{
					bodySprites.FlippedX = false;
					faceSprites.FlippedX = false;
					Hat.hatmap.FlippedX = false;
				}
				else if (physics.velocity.X < 0)
				{
					bodySprites.FlippedX = true;
					faceSprites.FlippedX = true;
					Hat.hatmap.FlippedX = true;
				}
				
				//Ground
				if(physics.isGrounded)
				{
					if(isDucking)
					{
						//Idle
						if(physics.velocity.X == 0)
						{
							bodySprites.Play("DuckIdle");
						}
						//Run
						else
							bodySprites.Play("DuckShuffle", false);
					}
					else
					{
						//Idle
						if(physics.velocity.X == 0 && bodySprites.CurrentAnim != "Stand")
						{
							bodySprites.Play("Idle");
							if(!isAttacking) faceSprites.Play("Idle");
						}
						//Run
						else if(physics.velocity.X != 0)
						{
							bodySprites.Play("Run");
							if(!isAttacking) faceSprites.Play("Run");
						}
					}
				}
				else //In air
				{
					if(physics.velocity.Y > 0)
					{
						bodySprites.Play("JumpIdle");
						//faceSprites.Play("JumpIdle");
					}
					else if (bodySprites.CurrentAnim != "FallIdle")
					{
						bodySprites.Play("Fall");
						if(!isAttacking) faceSprites.Play("Fall");
					}
				}
			}
			
			bodySprites.Update();
			
			//get y offset of the face
			if(bodySprites.CurrentAnim != "")
			{
				AnimationData animData = BodyAnimDict[bodySprites.CurrentAnim];
				int[] yoffset = animData.yFacePoints;
				if(yoffset != null)
				{
					faceSprites.OriginY = bodySprites.Height - yoffset[bodySprites.Index]/4
											+ faceSprites.Height - 10;
				}
			}
			
			faceSprites.Update();
		}
		
		public void OnAnimationEndBody()
		{		
			if(bodySprites.CurrentAnim == "Duck")
				bodySprites.Play("DuckIdle");
			else if(bodySprites.CurrentAnim == "Stand")
				bodySprites.Play("Idle");
			else if(bodySprites.CurrentAnim == "Jump")
				bodySprites.Play("JumpIdle");
			else if(bodySprites.CurrentAnim == "Fall")
				bodySprites.Play("FallIdle");
			else if(bodySprites.CurrentAnim == "Stagger1"
			       || bodySprites.CurrentAnim == "Stagger2")
			{
				bodySprites.Play("Idle");
				isHurt = false;
			}
			else if(bodySprites.CurrentAnim == "Death")
				throw new Exception("You have died!");
		}
		public void OnAnimationEndFace()
		{
			if(faceSprites.CurrentAnim == "Run")
				faceSprites.Play("Idle");
			else if(faceSprites.CurrentAnim == "Jump")
				faceSprites.Play("JumpIdle");
			else if(faceSprites.CurrentAnim == "ChargeAttack")
			    faceSprites.Play("ChargeAttackLoop");
			else if(faceSprites.CurrentAnim == "Fall")
				faceSprites.Play("FallIdle");
			else if(faceSprites.CurrentAnim == "Release")
			{
				faceSprites.Play(hat.attackEnd());
				isAttacking = false;
			}
		}
		
		public int getFaceOffset()
		{
			if(bodySprites.CurrentAnim != "")
			{
				AnimationData animData = BodyAnimDict[bodySprites.CurrentAnim];
				int[] yoffset = animData.yFacePoints;
				if(yoffset != null)
				{
					return bodySprites.Height - yoffset[bodySprites.Index]/4
											+ faceSprites.Height - 10;
				}
			}
			return -30;
		}
		
		public void onDamage(int amount, float force, Vector2f direction)
		{
			if(isHurt)
				return;
			
			isHurt = true;
			
			health -= amount;
			if(health <= 0)
			{
				onDeath();
				return;
			}
			
			//make the animation make sense and good
			physics.velocity.X += force * direction.X;
			physics.velocity.Y += force * direction.Y;
			
			if(FP.Rand(2) == 1)
				bodySprites.Play("Stagger1");
			else
				bodySprites.Play("Stagger2");
			faceSprites.Play("None");
		}
		public void onDeath()
		{
			//do death stuff
			isHurt = true;
			faceSprites.Play("None");
			bodySprites.Play("Death");
		}
	}
}