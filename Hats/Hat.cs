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

namespace NHTI.Hats
{
	/// <summary>
	/// Description of Hat.
	/// </summary>
	public abstract class Hat
	{
		public abstract string attackStart();
		public abstract string attackEnd();
	}
}
