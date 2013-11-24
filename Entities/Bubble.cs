﻿/*
 * Created by SharpDevelop.
 * User: nhti
 * Date: 11/24/2013
 * Time: 1:56 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using NHTI.Entities.Logics;
using Punk;
using Punk.Graphics;

namespace NHTI.Entities
{
	/// <summary>
	/// Description of Bubble.
	/// </summary>
	public class Bubble : Projectile
	{
		public Bubble(float velX, float velY)
			:base(velX, velY)
		{
			spritemap = makeSpritemap(onAnimationEnd);
			spritemap.Add("Bubble", FP.Frames(1,2,3), 5, false);
			spritemap.Add("BubbleLoop", FP.Frames(4,5,6), 5, true);
			spritemap.Add("BubblePop", FP.Frames(7), 10, false);
			
			AddGraphic(spritemap);
			
			spritemap.Play("Bubble");
			
			_onCollide = onCollide;
		}
		
		void onAnimationEnd()
		{
			if(spritemap.CurrentAnim == "Bubble")
				spritemap.Play("BubbleLoop");
			else if(spritemap.CurrentAnim == "BubblePop")
				this.World.Remove(this);
		}
		
		public void onCollide(Entity e)
		{
			spritemap.Play("BubblePop", false);
		}
	}
}
