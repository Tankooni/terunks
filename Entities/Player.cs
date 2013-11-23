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
		public Spritemap bodySprites;
		public Spritemap faceSprites;
		
		public enum BodyAnimations
		{
			//Put animations here
			Idle,
			Run,
			Duck,
			DuckIdle,
			DuckShuffle,
			Jump,
			Fall,
			Dash,
			Stagger1,
			Stagger2,
			Death,
			Dance
		}
		
		public enum FaceAnimations
		{
			None = 100,
			Idle,
			RunIdle,
			Jump,
			Fall,
			ChargeAttack,
			Release
		}
		
		public struct AnimationData
		{
			public AnimationData(int fps, int[] frames, Vector2i origin)
			{
				this.fps = fps;
				this.frames = frames;
				this.origin = origin;
			}
			int fps;
			int[] frames;
			Vector2i origin;
		}
		
		public Dictionary<BodyAnimations, AnimationData> BodyAnimDict = new Dictionary<BodyAnimations, AnimationData>()
		{
			{ BodyAnimations.Idle, new AnimationData (10, FP.Frames(), new Vector2i(0,0))},
			{ BodyAnimations.Run, new AnimationData (10, FP.Frames(), new Vector2i(0,0))},
			{ BodyAnimations.Duck, new AnimationData (10, FP.Frames(), new Vector2i(0,0))},
			{ BodyAnimations.DuckIdle, new AnimationData (10, FP.Frames(), new Vector2i(0,0))},
			{ BodyAnimations.DuckShuffle, new AnimationData (10, FP.Frames(), new Vector2i(0,0))},
			{ BodyAnimations.Jump, new AnimationData (10, FP.Frames(), new Vector2i(0,0))},
			{ BodyAnimations.Fall, new AnimationData (10, FP.Frames(), new Vector2i(0,0))},
			{ BodyAnimations.Dash, new AnimationData (10, FP.Frames(), new Vector2i(0,0))},
			{ BodyAnimations.Stagger1, new AnimationData (10, FP.Frames(), new Vector2i(0,0))},
			{ BodyAnimations.Stagger2, new AnimationData (10, FP.Frames(), new Vector2i(0,0))},
			{ BodyAnimations.Death, new AnimationData (10, FP.Frames(), new Vector2i(0,0))},
			{ BodyAnimations.Dance, new AnimationData (10, FP.Frames(), new Vector2i(0,0))}
		};
		
		public Dictionary<FaceAnimations, AnimationData> FaceAnimDict = new Dictionary<FaceAnimations, AnimationData>()
		{
			{ FaceAnimations.None, new AnimationData (10, FP.Frames(), new Vector2i(0,0))},
			{ FaceAnimations.Idle, new AnimationData (10, FP.Frames(), new Vector2i(0,0))},
			{ FaceAnimations.RunIdle, new AnimationData (10, FP.Frames(), new Vector2i(0,0))},
			{ FaceAnimations.Jump, new AnimationData (10, FP.Frames(), new Vector2i(0,0))},
			{ FaceAnimations.Fall, new AnimationData (10, FP.Frames(), new Vector2i(0,0))},
			{ FaceAnimations.ChargeAttack, new AnimationData (10, FP.Frames(), new Vector2i(0,0))},
			{ FaceAnimations.Release, new AnimationData (10, FP.Frames(), new Vector2i(0,0))}
		};
		
		
		
		//Stats
		public int health;
		
		public Hat hat;
		
		public float jumpForce = 3.0f;
		public float jumpDuration = .3f;
		
		//Other stuff
		PhysicsBody physics;
		
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
			Image image = new Image(Library.GetTexture("assets/Terunks.png"));
			image.OriginX = 32; image.OriginY = 126;
			AddGraphic(image);
			
			this.SetOrigin(32, 128);
			this.SetHitbox(67, 128, 32, 128);
			spritemap.
		}
		
		public override void Update()
		{			
			//Movement
			if(Input.Pressed(Keyboard.Key.Space) && physics.velocity.Y == 0)
				OnMessage(PhysicsBody.CHANGE_VEL, 0, 20);
			
			if(Input.Check(Keyboard.Key.A))
				OnMessage(PhysicsBody.CHANGE_VEL, -1f);
			if(Input.Check(Keyboard.Key.S))
				OnMessage(PhysicsBody.CHANGE_VEL, 0, -1f);
			if(Input.Check(Keyboard.Key.D))
				OnMessage(PhysicsBody.CHANGE_VEL, 1f);
			if(Input.Check(Keyboard.Key.W))
				OnMessage(PhysicsBody.CHANGE_VEL, 0, 1f);
			
			
			base.Update();
			
			//Animation stuff
			bodySprites.Update();
			faceSprites.Update();
		}
	}
}
