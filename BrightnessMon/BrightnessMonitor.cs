using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SameBrightnessSharp
{
	class BrightnessMonitor
	{
		public int Delay { get; set; }

		private string currentInstance;
		private int prevBrightness, prevState;
		private bool isRunning;

		public delegate void BrightnessChangedCallback(int newBrightness);
		public event BrightnessChangedCallback brightnessChanged;

		public BrightnessMonitor(int checkDelay = 1000)
		{
			currentInstance = WMITool.GetDisplayInstance();

			prevBrightness = WMITool.GetBrightness();
			prevState = WMITool.GetBatteryState();

			Delay = checkDelay;

		}


		public void StartMonitor()
		{
			isRunning = true;

			while (isRunning)
			{
				if (WMITool.GetBatteryState() != prevState)
				{
					WMITool.SetBrightness(currentInstance, prevBrightness);
					brightnessChanged?.Invoke(prevBrightness);
				}

				prevBrightness = WMITool.GetBrightness();
				prevState = WMITool.GetBatteryState();

				Thread.Sleep(Delay);
			}

		}

		public void StopMonitor() => isRunning = false;

	}
}
