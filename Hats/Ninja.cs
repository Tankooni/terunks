/*
 * Created by SharpDevelop.
 * User: nhti
 * Date: 11/24/2013
 * Time: 6:50 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using NHTI.Entities;
using NHTI.Entities.Logics;
using Punk;

namespace NHTI.Hats
{
	/// <summary>
	/// Description of Ninja.
	/// </summary>
	public class Ninja : Hat
	{
		int maxNumShots = 3;
		int numShots = 3;
		float shotChargeTime = .75f;
		float timabyby = 0;
		float speed = 10f;
		
		public Ninja(Player p)
		{
			parent = p;
		}
		
		public override void update()
		{
			timabyby += FP.Elapsed;
			if(timabyby >= shotChargeTime)
			{
				timabyby -= shotChargeTime;
				numShots += 1;
				if(numShots > maxNumShots)
					numShots = maxNumShots;
			}
		}
		
		public override string attackStart()
		{
			//throw in only x weally fast or the other way of the x weally fast
			if(numShots > 0)
			{
				//find out the direction to shoot
				int x = parent.World.MouseX - parent.X > 0 ? 1 : -1;
				
				var shurukinian = new Shuriken(x * speed, 0);
				shurukinian.X = parent.X - 20;
				shurukinian.Y = parent.Y - 80;
				parent.World.Add(shurukinian);
				
				numShots -= 1;
				
				return "Release";
			}
			
			return "";
		}
		
		public override string attackEnd()
		{
			return "Idle";
		}
	}
}
