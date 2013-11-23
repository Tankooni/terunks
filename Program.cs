﻿using System;
using Punk;

namespace NHTI
{
	class Program : Engine
	{
		public Program() : base(1000, 1000, 60) {}
		
		public override void Init()
		{
			base.Init();
			
			FP.Console.Enable();
			FP.Screen.SetTitle("Bananas");
			FP.World = new GameWorld();
		}
		
		public static void Main(string[] args)
		{
			new Program();
		}
	}
}