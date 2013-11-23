using System;
using Punk;

namespace NHTI
{
	/// <summary>
	/// Description of GameWorld.
	/// </summary>
	public class GameWorld : World
	{
		public GameWorld()
		{
		}
		
		public override void Update()
		{
			base.Update();
			
			FP.Engine.ClearColor = FP.Color(FP.Rand(uint.MaxValue));
		}
	}
}
