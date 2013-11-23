/*
 * Created by SharpDevelop.
 * User: Chris
 * Date: 7/17/2013
 * Time: 10:06 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Punk;
using Punk.Utils;
using SFML.Window;
using Punk.Tweens.Misc;

namespace NHTI
{
	/// <summary>
	/// Description of CameraShaker.
	/// </summary>
	public class CameraFollow : Logic
	{
		/// <summary>
		/// Broadcast when the camera should shake
		/// Arguments: (float strength = 10.0f, float duration = 1.0f)
		/// </summary>
		public const string SHAKE = "cameraShake";
		
		private float offsetX, offsetY;
		private MultiVarTween prevShaker;
		
		public CameraFollow()
		{
			AddResponse(SHAKE, OnCameraShake);
			offsetX = offsetY = 0;
		}
		
		public override void Update()
		{
			base.Update();
			
			FP.Camera.X = Parent.X + offsetX;
			FP.Camera.Y = Parent.Y + offsetY;
		}
		
		private void OnCameraShake(params object[] args)
		{
			float str = args.Length > 0 ? (float)args[0] : 10.0f;
			float dur = args.Length > 1 ? (float)args[1] : 1.0f;
			
			// Get a random number [-1..1]
			float randX = ((float)FP.Rand(200) - 100.0f) / 100.0f;
			float randY = ((float)FP.Rand(200) - 100.0f) / 100.0f;
			
			// Scale it by the strength
			offsetX = str * (randX);
			offsetY = str * (randY);
			
			if (prevShaker != null)
			{
				prevShaker.Cancel();
			}
			
			var shaker = new MultiVarTween(OnShakeDone, Tween.ONESHOT);
			Parent.AddTween(shaker);
			shaker.Tween(this, new { offsetX = 0.0f, offsetY = 0.0f }, dur, Ease.ElasticOut);
			shaker.Start();
			
			prevShaker = shaker;
		}
		
		private void OnShakeDone()
		{
		}
	}
}