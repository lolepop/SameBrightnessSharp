using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using SameBrightnessSharp;

namespace SameBrightnessService
{
	public partial class BrightnessSvc : ServiceBase
	{
		public BrightnessSvc()
		{
			InitializeComponent();
		}

		protected override void OnStart(string[] args)
		{
			new Thread(() => {
				BrightnessMonitor monitor = new BrightnessMonitor();
				monitor.StartMonitor();
			}).Start();
		}

		protected override void OnStop()
		{
		}
	}
}
