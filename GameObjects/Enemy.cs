/*
 * Created by SharpDevelop.
 * User: Matte
 * Date: 11/23/2013
 * Time: 11:35 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Punk;
using Punk.Graphics;


namespace NHTI.GameObjects
{
	/// <summary>
	/// Description of Enemy.
	/// </summary>
	/// 
	public class Enemy : Entity
	{
		
		Graphic = Image.CreateRect(50, 50, FP.Color(0x66FF33);
		
		public override void Load(System.Xml.XmlNode node)
		{
			base.Load(node);
		}
		
		public Enemy()
		{
			
		}
	}
}
