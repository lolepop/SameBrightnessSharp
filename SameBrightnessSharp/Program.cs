using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace SameBrightnessSharp
{
	class Program
	{
		public static void KillCurrProc()
		{
			var currProc = Process.GetCurrentProcess();

			foreach (var process in Process.GetProcessesByName(currProc.ProcessName))
			{
				if (process.Id != currProc.Id)
					process.Kill();
			}
		}

		static void Init()
		{
			BrightnessMonitor monitor = new BrightnessMonitor();
			monitor.StartMonitor();
			Thread.Sleep(Timeout.Infinite);
		}

		static void Main(string[] args)
		{

			if (args.Length > 0)
			{
				var startupFolder = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
				var newexeLoc = Path.Combine(startupFolder, AppDomain.CurrentDomain.FriendlyName);

				switch (args[0].Trim().ToLower())
				{
					case "install":
					case "i":
						if (!File.Exists(newexeLoc))
						{
							File.Copy(System.Reflection.Assembly.GetExecutingAssembly().Location, newexeLoc);
							Process.Start(newexeLoc);
						}

						break;
					case "uninstall":
					case "u":
						if (File.Exists(newexeLoc))
						{
							KillCurrProc();
							File.Delete(newexeLoc);
						}
						break;
					case "stop":
						KillCurrProc();
						break;
					case "start":
						if (File.Exists(newexeLoc))
							Process.Start(newexeLoc);
						else
							Init();
						break;
					default:
						MessageBox.Show("install - Installs program into startup directory and starts it\n"
										 + "uninstall - Uninstalls programs from startup directory\n"
										 + "stop - Kills process\n"
										 + "start - Starts process\n\n"
										 + "Example usage: SameBrightnessSharp.exe install", "Help");
						break;
				}

				Environment.Exit(0);
			}
			Init();

		}
	}
}
