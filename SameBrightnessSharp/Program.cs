﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SameBrightnessSharp
{
	class Program
	{
		static void Main()
		{
			BrightnessMonitor monitor = new BrightnessMonitor();
			monitor.StartMonitor();
			Thread.Sleep(Timeout.Infinite);
			
		}
	}
}
