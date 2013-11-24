using System;
using Punk;
using Punk.Graphics;
using Punk.Tweens.Misc;
using Punk.Utils;
using SFML.Window;
using System.Collections.Generic;

namespace GameObjects
{
	/// <summary>
	/// Description of Enemy.
	/// </summary>
	/// 
	public class FlyingEnemy : Entity
	{
		List<Vector2i> positionNodes = new List<Vector2i>();
		Vector2i currentPosition = new Vector2i(0,0);
		
		public FlyingEnemy() {}
		
		public override void Load(System.Xml.XmlNode node)
		{
			base.Load(node);
			Graphic = Image.CreateRect(64, 64, FP.Color(0x66FF33));
			
			foreach (System.Xml.XmlNode n in node)
			{
				FP.Log(int.Parse(n.Attributes["x"].Value) + " " + float.Parse(n.Attributes["y"].Value));
				positionNodes.Add(new Vector2i(int.Parse(n.Attributes["x"].Value), int.Parse(n.Attributes["y"].Value)));
			}
			if(positionNodes.Count > 0)
			{
				currentPosition = positionNodes[0];
				MoveToNextPos();
			}
		}
		
		public void MoveToNextPos()
		{
			currentPosition = FP.Next(currentPosition, positionNodes, true);
			var twoon = new MultiVarTween(MoveToNextPos, ONESHOT);
			twoon.Tween(this, new { X = currentPosition.X, Y = currentPosition.Y}, 5.0f);
			AddTween(twoon, true);
	}
		
		public override void Update()
		{
			base.Update();
			
		}
	}
}
