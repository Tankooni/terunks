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
		
		public static Dictionary<string, AnimationData> BodyAnimDict = new Dictionary<string, AnimationData>()
		{
			{ "Idle", new AnimationData (10, FP.Frames(0), true, new int[]{160})},
			{ "Run", new AnimationData (10, FP.MakeFrames(1,20,1), false, 
			                            new int[]{150,150,160,160,170,170,180,180,165,165,150,150,160,160,170,170,180,170,165,165})},
			{ "Duck", new AnimationData (10, FP.MakeFrames(21,24,1), false, new int[]{150,200,240,260})},
			{ "DuckIdle", new AnimationData (10, FP.Frames(25), true, new int[]{260})},
			{ "DuckStand", new AnimationData (10, FP.MakeFrames(21,24,1).Reverse().ToArray(), true, new int[]{260,240,200,150})},
			{ "DuckShuffle", new AnimationData (10, FP.MakeFrames(26,35,1), true, new int[]{260,260,260,260,260,260,260,260,260,260})},
			{ "Jump", new AnimationData (10, FP.MakeFrames(36,41,1), false, new int[]{150,150,75,75,65,65})},
			{ "JumpIdle", new AnimationData (10, FP.Frames(40,41), true, new int[]{65,65})},
			{ "Fall", new AnimationData (10, FP.MakeFrames(42,47,1), false, new int[]{65,65,115,115,150,150})},
			{ "FallIdle", new AnimationData (10, FP.Frames(46,47), true, new int[]{150,150})},
			{ "Dash", new AnimationData (10, FP.Frames(48,49), true, null)},
			{ "Stagger1", new AnimationData (10, FP.MakeFrames(50,54,1), false, null)},
			{ "Stagger2", new AnimationData (10, FP.MakeFrames(55,59,1), false, null)},
			{ "Death", new AnimationData (10, FP.MakeFrames(60,91,1), false, null)},
			{ "Dance", new AnimationData (10, FP.MakeFrames(92, 106,1), false, null)}
		};
		
		public static Dictionary<string, AnimationData> FaceAnimDict = new Dictionary<string, AnimationData>()
		{
			{ "None", new AnimationData (10, FP.Frames(0), false, null)},
			{ "Idle", new AnimationData (10, FP.Frames(1), true, null)},
			{ "RunIdle", new AnimationData (10, FP.MakeFrames(2,21,1), false, null)},
			{ "Jump", new AnimationData (10, FP.MakeFrames(22, 27, 1), false, null)},
			{ "JumpIdle", new AnimationData (10, FP.MakeFrames(26, 27, 1), true, null)},
			{ "Fall", new AnimationData (10, FP.MakeFrames(28, 33, 1), false, null)},
			{ "FallIdle", new AnimationData (10, FP.MakeFrames(32, 33, 1), true, null)},
			{ "ChargeAttack", new AnimationData (10, FP.MakeFrames(34, 42, 1), false, null)},
			{ "ChargeAttackLoop", new AnimationData (10, FP.MakeFrames(42,45, 1), true, null)},
			{ "Release", new AnimationData (10, FP.MakeFrames(46, 57, 1), false, null)}
		};
		
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
			
			//Add sprites
			bodySprites = new Spritemap(Library.GetTexture("assets/Timunkslowres.png"), 88, 100, OnAnimationEndBody);
			bodySprites.OriginX = 44;
			bodySprites.OriginY = 100;
			
			faceSprites = new Spritemap(Library.GetTexture("assets/TimunksFacelowres.png"), 42, 34, OnAnimationEndFace);
			faceSprites.CenterOrigin();
			faceSprites.OriginX = 21;
			
			//bodySprites.
			
			//add all the things
			foreach(KeyValuePair<string, AnimationData> entry in BodyAnimDict)
				bodySprites.Add(entry.Key, entry.Value.frames, entry.Value.fps, entry.Value.loop);
			foreach(KeyValuePair<string, AnimationData> entry in FaceAnimDict)
				faceSprites.Add(entry.Key, entry.Value.frames, entry.Value.fps, entry.Value.loop);
			
			bodySprites.Play("Idle");
			faceSprites.Play("Idle");
			
			AddGraphic(bodySprites);
			AddGraphic(faceSprites);
			
			this.SetHitbox(88, 100, 44, 100);
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
					if(physics.velocity.X == 0 && bodySprites.CurrentAnim != "DuckIdle")
						bodySprites.Play("Duck", false);
					//Run
					else
						bodySprites.Play("DuckShuffle");
				}
				else
				{
					//Idle
					if(physics.velocity.X == 0 && bodySprites.CurrentAnim != "Stand")
						bodySprites.Play("Idle");
					//Run
					else if(physics.velocity.X != 0)
					{
						bodySprites.Play("Run", true);
						faceSprites.Play("Run", true);
					}
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
			
			//get y offset of the face
			if(bodySprites.CurrentAnim != "")
			{
				AnimationData animData = BodyAnimDict[bodySprites.CurrentAnim];
				int[] yoffset = animData.yFacePoints;
				if(yoffset != null)
				{
					faceSprites.OriginY = bodySprites.Height - yoffset[bodySprites.Frame - animData.frames[0]]/4
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
			else if(bodySprites.CurrentAnim == "Run")
				bodySprites.Play("Idle");
		}
		public void OnAnimationEndFace()
		{
			if(faceSprites.CurrentAnim == "Run")
				faceSprites.Play("Idle");
		}	
		
		public override void Added()
		{
			base.Added();
			List<Entity> l = new List<Entity>();
			World.GetType("PlayerSpawn",l);
			X = l[0].X;
			Y = l[0].Y;
		}
	}
}
