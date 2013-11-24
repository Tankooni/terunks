/*
 * Created by SharpDevelop.
 * User: nhti
 * Date: 11/23/2013
 * Time: 12:14 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using NHTI.Entities;
using Punk;
using Punk.Graphics;

namespace NHTI.Hats
{
	/// <summary>
	/// Description of Hat.
	/// </summary>
	public abstract class Hat
	{
		public static Spritemap hatmap = new Spritemap(Library.GetTexture("assets/Hats2.png"), 66, 66, null);
		//public static Spritemap projectiles = new Spritemap(Library.GetTexture("assets/Projectiles.png"), 41, 41, null);
		
		public static Dictionary<string, AnimationData> hatDict = new Dictionary<string, AnimationData>()
		{
			{ "NoHat", new AnimationData (1, FP.Frames(0), true, null)},
			{ "TopHat", new AnimationData (1, FP.Frames(1), true, null)},
			{ "WWI", new AnimationData (1, FP.Frames(2), true, null)},
			{ "Trophy", new AnimationData (1, FP.Frames(3), true, null)},
			{ "CatEars", new AnimationData (1, FP.Frames(4), true, null)},
			{ "Ninja", new AnimationData (4, FP.Frames(5, 6), true, null)},
			{ "Tiara", new AnimationData (1, FP.Frames(7), true, null)},
			{ "Raiden", new AnimationData (1, FP.Frames(8), true, null)},
			{ "Banana", new AnimationData (1, FP.Frames(9), true, null)},
			{ "KeyHat", new AnimationData (1, FP.Frames(10), true, null)},
			{ "TheDevice", new AnimationData (1, FP.Frames(11), true, null)},
			{ "Fez", new AnimationData (1, FP.Frames(12), true, null)},
			{ "Dunce", new AnimationData (1, FP.Frames(13), true, null)},
			{ "Bow", new AnimationData (1, FP.Frames(14), true, null)}
		};
		/*public static Dictionary<string, AnimationData> projDict = new Dictionary<string, AnimationData>()
		{
			{ "Bubble", new AnimationData (2, FP.Frames(1,2,3), false, null)},
			{ "BubbleLoop", new AnimationData (2, FP.Frames(4,5,6), true, null)},
			{ "BubblePop", new AnimationData (1, FP.Frames(7), false, null)},
			{ "Bananarang", new AnimationData (2, FP.Frames(8,9,10), true, null)},
			{ "Card", new AnimationData (2, FP.Frames(11,12), true, null)},
			{ "Fireball", new AnimationData (2, FP.Frames(13, 14), true, null)},
			{ "Shell", new AnimationData (3, FP.Frames(15,16), true, null)},
			{ "Yarn", new AnimationData (1, FP.Frames(17), true, null)},
			{ "Shuriken", new AnimationData (1, FP.Frames(18,19), true, null)},
			{ "ElecBall", new AnimationData (2, FP.Frames(20), true, null)},
			{ "Key", new AnimationData (1, FP.Frames(21), true, null)},
			{ "Lightning", new AnimationData (1, FP.Frames(22,23), true, null)},
			{ "Egg", new AnimationData (1, FP.Frames(24), true, null)},
			{ "Arrow", new AnimationData (1, FP.Frames(25), true, null)}
		};*/
		
		static Hat()
		{
			foreach(var entry in hatDict)
				hatmap.Add(entry.Key, entry.Value.frames, entry.Value.fps, entry.Value.loop);
		}
		
		public Player parent;
		
		public abstract void update();
		public abstract string attackStart();
		public abstract string attackEnd();
	}
}
