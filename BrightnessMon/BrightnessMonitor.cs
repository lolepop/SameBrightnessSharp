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
		public event BrightnessChangedCallback BrightnessChanged;

		public BrightnessMonitor(int checkDelay = 1000)
		{
			currentInstance = WMITool.GetDisplayInstance();

			prevBrightness = WMITool.GetBrightness();
			prevState = WMITool.GetBatteryState();

			Delay = checkDelay;

		}

		public void Tick(bool changed)
		{
			if (changed)
			{
				WMITool.SetBrightness(currentInstance, prevBrightness);
				BrightnessChanged?.Invoke(prevBrightness);
			}

			prevBrightness = WMITool.GetBrightness();

		}

		public void StartMonitor() // probably will change in the future
		{
			isRunning = true;

			while (isRunning)
			{
				Tick(WMITool.GetBatteryState() != prevState);
				prevState = WMITool.GetBatteryState();

				Thread.Sleep(Delay);
			}

		}

		public void StopMonitor() => isRunning = false;

		public void StartSvcTicker() // only keeps track of previous brightness (for service)
		{
			while (true)
			{
				Tick(false);

				Thread.Sleep(Delay);
			}	
		}


	}
}
