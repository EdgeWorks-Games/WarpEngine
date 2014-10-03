﻿using System;
using WarpEngine;

namespace Warpcore
{
	public sealed class Client : Game
	{
		public Client()
		{
			Loops.Add(new Loop(Update, TimeSpan.FromSeconds(1.0/100.0)));

			// In practice we'll render at the VSync FPS, but this sets a nice just in case minimum
			Loops.Add(new Loop(Render, TimeSpan.FromSeconds(1.0/1000.0)));
		}

		private void Update(TimeSpan delta)
		{
			Console.WriteLine("Update()");
		}

		private void Render(TimeSpan delta)
		{
			Console.WriteLine("Render()");
		}
	}
}