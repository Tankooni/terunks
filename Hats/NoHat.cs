/*
 * Created by SharpDevelop.
 * User: nhti
 * Date: 11/23/2013
 * Time: 10:56 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using NHTI.Entities;
using Punk;
using Punk.Utils;

namespace NHTI.Hats
{
	/// <summary>
	/// Description of NoHat.
	/// </summary>
	public class NoHat : Hat
	{
		float bubblespeed = 2;
		bool isCharging = false;
		float maxCharge = 5;
		float timeCharged = 0;
		
		public NoHat(Player p)
		{
			parent = p;
		}
		
		public override void Update()
		{
			if(isCharging)
			{
				timeCharged += FP.Elapsed;
				if(timeCharged > maxCharge)
					timeCharged = maxCharge;
			}
		}
		
		public override string attackStart()
		{
			
			return "ChargeAttack";
		}
		
		public override string attackEnd()
		{

			int i = timeCharged >= 3? 3 : 1;
			while( i-- > 0)
			{
				float x = parent.World.MouseX - parent.X;
				float y = parent.Y - parent.World.MouseY;
				//add some variance
				x *= .8f + FP.Rand(4000) / 10000f;
				y *= .8f + FP.Rand(4000) / 10000f;
				
				//normalize them
				float mag = (float)Math.Sqrt(x*x + y*y);
				x *= bubblespeed / mag;
				y *= bubblespeed / mag;
				
				var bubble = new Bubble(x,y);
				bubble.X = parent.X - 20;
				bubble.Y = parent.Y - 80;
				parent.World.Add(bubble);
			}
			
			return "Idle";
		}
	}
}
