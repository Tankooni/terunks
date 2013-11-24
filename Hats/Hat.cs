/*
 * Created by SharpDevelop.
 * User: nhti
 * Date: 11/23/2013
 * Time: 12:14 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
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
		public static Spritemap hatmap = new Spritemap(Library.GetTexture("Hats2.png"), 66, 66, null);
		public static Spritemap projectiles = new Spritemap(Library.GetTexture("Projectiles.png"), 41, 41, null);
		                                             
		public Player parent;
		
		public abstract void update();
		public abstract string attackStart();
		public abstract string attackEnd();
	}
}
