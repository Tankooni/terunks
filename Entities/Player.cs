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
using SFML.Window;

namespace NHTI.Entities
{
	/// <summary>
	/// Description of Player.
	/// </summary>
	public class Player : Entity
	{
		public uint id;
		public Controller controller;
		
		
		//Animation shtuffs
		private Spritemap bodySprites;
		private Spritemap faceSprites;
		
		public struct AnimationData
		{
			public AnimationData(int fps, int[] frames, bool loop, Vector2i origin)
			{
				this.fps = fps;
				this.frames = frames;
				this.loop = loop;
				this.origin = origin;
			}
			public int fps;
			public int[] frames;
			public bool loop;
			public Vector2i origin;
		}
		
		public static Dictionary<string, AnimationData> BodyAnimDict = new Dictionary<string, AnimationData>()
		{
			{ "Idle", new AnimationData (10, FP.Frames(1), true, new Vector2i(0,0))},
			{ "Run", new AnimationData (10, FP.MakeFrames(24,43,1), true, new Vector2i(0,0))},
			{ "Duck", new AnimationData (10, FP.MakeFrames(44,47,1), false, new Vector2i(0,0))},
			{ "DuckIdle", new AnimationData (10, FP.MakeFrames(48,53,1), true, new Vector2i(0,0))},
			{ "DuckStand", new AnimationData (10, FP.MakeFrames(44,47,1).Reverse().ToArray(), true, new Vector2i(0,0))},
			{ "DuckShuffle", new AnimationData (10, FP.MakeFrames(54,63,1), true, new Vector2i(0,0))},
			{ "Jump", new AnimationData (10, FP.MakeFrames(64,69,1), false, new Vector2i(0,0))},
			{ "JumpIdle", new AnimationData (10, FP.Frames(68,69), true, new Vector2i(0,0))},
			{ "Fall", new AnimationData (10, FP.MakeFrames(70,75,1), false, new Vector2i(0,0))},
			{ "FallIdle", new AnimationData (10, FP.Frames(74,75), true, new Vector2i(0,0))},
			{ "Dash", new AnimationData (10, FP.Frames(76,77), true, new Vector2i(0,0))},
			{ "Stagger1", new AnimationData (10, FP.MakeFrames(78,82,1), false, new Vector2i(0,0))},
			{ "Stagger2", new AnimationData (10, FP.MakeFrames(83,87,1), false, new Vector2i(0,0))},
			{ "Death", new AnimationData (10, FP.MakeFrames(88,119), false, new Vector2i(0,0))},
			{ "Dance", new AnimationData (10, FP.MakeFrames(120, 134), false, new Vector2i(0,0))}
		};
		
		public static Dictionary<string, AnimationData> FaceAnimDict = new Dictionary<string, AnimationData>()
		{
			{ "None", new AnimationData (10, FP.Frames(), false, new Vector2i(0,0))},
			{ "Idle", new AnimationData (10, FP.Frames(), true, new Vector2i(0,0))},
			{ "RunIdle", new AnimationData (10, FP.Frames(), true, new Vector2i(0,0))},
			{ "Jump", new AnimationData (10, FP.Frames(), true, new Vector2i(0,0))},
			{ "Fall", new AnimationData (10, FP.Frames(), true, new Vector2i(0,0))},
			{ "ChargeAttack", new AnimationData (10, FP.Frames(), false, new Vector2i(0,0))},
			{ "ChargeAttackLoop", new AnimationData (10, FP.Frames(), true, new Vector2i(0,0))},
			{ "Release", new AnimationData (10, FP.Frames(), false, new Vector2i(0,0))}
		};
		
		static Player(){

		}
		
		//Stats
		public int health;
		
		public Hat hat;
		
		public float jumpForce = 20.0f;
		
		//Other stuff
		PhysicsBody physics;
		bool isDucking = false;
		
		public Player(float x, float y, uint id) : base(x,y)
		{
			this.id = id;
			this.controller = new Controller(id);
			
			physics = new PhysicsBody();
			physics.Colliders.Add("platform");
			physics.Colliders.Add("wall");
			physics.maxXVelocity = 5f;
			AddLogic(physics);
			
			//placeholder sorta type of image
			//Image image = new Image(Library.GetTexture("assets/Terunks.png"));
			//image.OriginX = 32; image.OriginY = 126;
			//AddGraphic(image);
			
			//Add sprites
			bodySprites = new Spritemap(Library.GetTexture("assets/Timunkslowres.png"), 64, 64, OnAnimationEnd);
			bodySprites.OriginX = 32;
			bodySprites.OriginY = 64;
			
			//add all the things
			foreach(KeyValuePair<string, AnimationData> entry in BodyAnimDict)
				bodySprites.Add(entry.Key, entry.Value.frames, entry.Value.fps, entry.Value.loop);
			
			bodySprites.Play("Run");
			
			AddGraphic(bodySprites);
			
			this.SetHitbox(64, 64, 32, 64);
			//bodySprites.
		}
		
		public override void Update()
		{			
			//Movement
			if(Input.Pressed(Keyboard.Key.Space) && physics.velocity.Y == 0)
			{
				physics.velocity.Y += jumpForce;
				bodySprites.Play("Jump");
			}
			
			if(Input.Check(Keyboard.Key.A))
				physics.velocity.X -= 1f;
			if(Input.Check(Keyboard.Key.D))
				physics.velocity.X += 1f;
			
			if(Input.Check(Keyboard.Key.S))
				isDucking = true;
			else
			{
				if(isDucking)
					bodySprites.Play("Stand");
				isDucking = false;
			}
			
			
			base.Update();
			
			//Animation stuff
			
			//Flippin' stuffs
			if(physics.velocity.X > 0)
				bodySprites.FlippedX = false;
			else if (physics.velocity.X < 0)
				bodySprites.FlippedX = true;
			
			//Ground
			if(physics.isGrounded)
			{
				if(isDucking)
				{
					//Idle
					if(physics.velocity.X == 0)
						bodySprites.Play("Duck", false);
					//Run
					else if(physics.velocity.X > 0)
					{
						bodySprites.Play("DuckShuffle");
						bodySprites.FlippedX = false;
					}
					else if (physics.velocity.X < 0)
					{
						bodySprites.Play("DuckShuffle");
						bodySprites.FlippedX = true;
					}
				}
				else
				{
					//Idle
					if(physics.velocity.X == 0 && bodySprites.CurrentAnim != "Stand")
						bodySprites.Play("Idle");
					//Run
					else if(physics.velocity.X > 0)
						bodySprites.Play("Run");
					else if (physics.velocity.X < 0)
						bodySprites.Play("Run");
				}
			}
			else //In air
			{
				if(physics.velocity.Y > 0)
					bodySprites.Play("JumpIdle", false);
				else if (bodySprites.CurrentAnim != "FallIdle")
					bodySprites.Play("Fall", false);
			}
			
			bodySprites.Update();
			//faceSprites.Update();
		}
		
		public void OnAnimationEnd()
		{
			if(bodySprites.CurrentAnim == "Duck")
				bodySprites.Play("DuckIdle");
			else if(bodySprites.CurrentAnim == "Stand")
				bodySprites.Play("Idle");
			else if(bodySprites.CurrentAnim == "Jump")
				bodySprites.Play("JumpIdle");
			else if(bodySprites.CurrentAnim == "Fall")
				bodySprites.Play("FallIdle");
		}
	}
}
