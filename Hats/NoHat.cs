/*
 * Created by SharpDevelop.
 * User: nhti
 * Date: 11/23/2013
 * Time: 10:56 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Punk;

namespace NHTI.Hats
{
	/// <summary>
	/// Description of NoHat.
	/// </summary>
	public class NoHat : Hat
	{
		bool isCharging = false;
		float maxCharge = 5;
		float timeCharged = 0;
		
		public NoHat(Player p)
		{
			parent = p;
		}
		
		public update()
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
			
			
			return "Idle";
		}
	}
}
