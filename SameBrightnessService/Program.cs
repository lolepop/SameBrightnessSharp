using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Configuration.Install;
using System.Security.Principal;

namespace SameBrightnessService
{
	static class Program
	{
		private class Installer
		{
			public static void Install() => ManagedInstallerClass.InstallHelper(new string[] { System.Reflection.Assembly.GetExecutingAssembly().Location });
			public static void Uninstall() => ManagedInstallerClass.InstallHelper(new string[] { "-uninstall", System.Reflection.Assembly.GetExecutingAssembly().Location });
		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static void Main(string[] args)
		{


			if (args.Length > 0)
			{

				using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
				{
					WindowsPrincipal principal = new WindowsPrincipal(identity);
					if (!principal.IsInRole(WindowsBuiltInRole.Administrator))
					{
						Console.WriteLine("Must be launched as administrator");
						return;
					}
				}

				switch (args[0].Trim().ToLower())
				{
					case "install":
					case "i":
						try
						{
							Installer.Install();
							Console.WriteLine("Service successfully installed");

							ServiceController sc = new ServiceController(Properties.Settings.Default.ServiceName);
							sc.Start();
						}
						catch (Exception)
						{
							Console.WriteLine("Installation failed, make sure you are running as admin");
						}
						break;
					case "uninstall":
					case "u":
						try
						{
							Installer.Uninstall();
							Console.WriteLine("Service successfully uninstalled");
						}
						catch (Exception)
						{
							Console.WriteLine("Uninstallation failed, make sure you are running as admin");
						}
						break;
					default:
						Console.WriteLine("install - Installs service\n"
										 + "uninstall - Uninstalls service\n\n"
										 + "Example usage: SameBrightnessService.exe install");
						break;
				}
			}
			else
			{
				ServiceBase[] ServicesToRun;
				ServicesToRun = new ServiceBase[]
				{
					new BrightnessSvc()
				};
				ServiceBase.Run(ServicesToRun);
			}

		}
	}
}
