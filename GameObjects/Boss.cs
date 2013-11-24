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
		Spritemap cloak;
		
		public Boss()
		{

			
		}
		
		public override void Load(System.Xml.XmlNode node)
		{
			base.Load(node);
			
			cloak = new Spritemap(Library.GetTexture("assets/ArmsBosslowres.png"), 674/2, 349/2, onAnimationEnd);
			cloak.Add("Idle", FP.Frames(0), 1, true);
			cloak.Add("Reveal", FP.MakeFrames(0,11), 5, false);
			
			AddGraphic(cloak);
			
			cloak.Scale = 4;
			cloak.OriginX = 673/8 - 15;
			cloak.OriginY = 349/4;			
			
			cloak.Play("Idle");
		}
		
		public override void Added()
		{
			base.Added();
			(World as Room).musics.Stop();
		}
		
		public override void Update()
		{
			if(FP.Distance(X,Y,World.Camera.X, World.Camera.Y) < 400)
			   cloak.Play("Reveal");
		}
		
		public void onAnimationEnd()
		{
			if(cloak.CurrentAnim == "Reveal")
			{
				//kill this and spawn arms
				BossArms right = new BossArms();
				BossArms left = new BossArms();
				
				left.X = X+128;
				left.Y = Y;
				left.arms.FlippedX = true;
				
				right.X = X-128;
				right.Y = Y;
				right.arms.FlippedX = false;
				
				left.p = ((Room)World).player;
				right.p = ((Room)World).player;
				
				World.Add(left);
				World.Add(right);
				
				(World as Room).musics = new Sfx(Library.GetBuffer("assets/Bossedd.ogg"));
				(World as Room).musics.Loop();
				World.Remove(this);
			}
		}
	}
}
