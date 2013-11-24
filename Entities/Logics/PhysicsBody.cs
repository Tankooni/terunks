/*
 * Created by SharpDevelop.
 * User: nhti
 * Date: 11/23/2013
 * Time: 3:40 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using Punk;
using SFML.Window;

namespace NHTI.Entities.Logics
{
	/// <summary>
	/// Description of PhysicsBody.
	/// </summary>
public class PhysicsBody : Logic
	{
		/// <summary>
		/// Changes the accelleration or velocity of the object.
		/// If absolute is true, set the values of x and y
		/// </summary>
		public const string CHANGE_VEL = "change_vel";
		public const string CHANGE_ACC = "change_acc";
		
		/// <summary>
		/// Set whether or not the x or y directions will be changed
		/// </summary>
		public const string CHANGABLEX = "changablex";
		public const string CHANGABLEY = "changabley";
		
		/// <summary>
		/// <para>Used to apply friction to the body.</para>
		/// <para>Arguments: (float frictionFactor)</para>
		/// <para>1 means no slowdown, 0 means complete slowdown.</para>
		/// </summary>
		public const string FRICTION = "friction";
		
		// Movement/Physics
		/// <summary>
		/// The types to collide against.
		/// </summary>
		public List<string> Colliders;
		
		/// <summary>
		/// The value that will be accumulated as the body accelerates downwards.
		/// </summary>
		public static float Gravity = .075f;

		public Vector2f acceleration;
		public Vector2f velocity;
		
		public float maxXVelocity;
		
		private bool canMoveX = true;
		private bool canMoveY = true;
		public bool isGrounded = true;
		public bool hasGravity = true;
		public bool hasFriction = true;
		
		private float frictionFactor = .90f;
		private const float airFriction = 0.9f;
		
		public PhysicsBody()
		{
			Colliders = new List<string>();
			acceleration = new Vector2f();
			Gravity = 0.75f;
			
			velocity = new Vector2f();
			canMoveX = canMoveY = true;
			
			AddResponse(CHANGE_VEL, OnChangeVelocity);
			AddResponse(CHANGE_ACC, OnChangeAcceleration);
			AddResponse(CHANGABLEX, OnChangableX);
			AddResponse(CHANGABLEY, OnChangableY);
			AddResponse(FRICTION, OnApplyFriction);
		}
		
		public override void Update()
		{
			base.Update();
			
			//update velocity
			velocity.X += acceleration.X;
			velocity.Y += acceleration.Y;
			
			if(hasGravity)
				velocity.Y -= Gravity;
			
			if(!canMoveX)
				velocity.X = 0;
			if(!canMoveY)
				velocity.Y = 0;
			
			if(velocity.X > maxXVelocity)
				velocity.X = maxXVelocity;
			else if(velocity.X < -maxXVelocity)
				velocity.X = -maxXVelocity;
			
			//Insert friction stuff here
			if(hasFriction)
				velocity.X *= frictionFactor;
			
			//Actual Movement
			float x = Parent.X, y = Parent.Y;
			Parent.MoveBy(velocity.X, -velocity.Y, Colliders, true);
			
			if(x == Parent.X)
				velocity.X = 0;
			if(y == Parent.Y)
			{
				velocity.Y = 0;
				isGrounded = true;
			}
			else
				isGrounded = false;
		}
		
		private void OnChangeVelocity(params object[] args)
		{
			float x = args.Length > 0 ? Convert.ToSingle(args[0]) : 0;
			float y = args.Length > 1 ? Convert.ToSingle(args[1]) : 0;
			
			if (args.Length > 2 && (bool) args[2])
			{
				velocity.X = x == 0 ? velocity.X : x;
				velocity.Y = y == 0 ? velocity.Y : y;
			}
			else
			{
				velocity.X += x;
				velocity.Y += y;
			}
		}
		
		private void OnChangeAcceleration(params object[] args)
		{
			float x = args.Length > 0 ? Convert.ToSingle(args[0]) : 0;
			float y = args.Length > 1 ? Convert.ToSingle(args[1]) : 0;
			
			if (args.Length > 2 && (bool) args[2])
			{
				acceleration.X = x == 0 ? acceleration.X : x;
				acceleration.Y = y == 0 ? acceleration.Y : y;
			}
			else
			{
				acceleration.X += x;
				acceleration.Y += y;
			}
		}
		
		private void OnChangableX(params object[] args)
		{
			if (args.Length == 0)
				throw new Exception("Must supply a value!");
			
			canMoveX = !((bool) args[0]);
		}
		
		private void OnChangableY(params object[] args)
		{
			if (args.Length == 0)
				throw new Exception("Must supply a value!");
			
			canMoveY = !((bool) args[0]);
		}
		
		private void OnApplyFriction(params object[] args)
		{
			frictionFactor = Convert.ToSingle(args[0]);
			//applyGroundFriction = true;
		}
	}
}
