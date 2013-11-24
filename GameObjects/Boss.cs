/*
 * Created by SharpDevelop.
 * User: nhti
 * Date: 11/24/2013
 * Time: 10:01 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Punk;
using Punk.Graphics;

namespace NHTI.GameObjects
{
	/// <summary>
	/// Description of Boss.
	/// </summary>
	public class Boss : Enemy
	{
		Spritemap cloak, arms;
		
		public Boss()
		{
			cloak = new Spritemap(Library.GetTexture("assets/ArmsBoss.png"), 673, 349, onAnimationEnd);
			cloak.Add("Reveal", FP.MakeFrames(0,11), 2, false);
			
			AddGraphic(cloak);
			cloak.Play("Reveal");
			
		}
		
		public void onAnimationEnd()
		{
			//if(spritemap.CurrentAnim == "
		}
	}
}
