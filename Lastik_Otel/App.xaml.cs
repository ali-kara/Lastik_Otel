using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Windows;

namespace Lastik_Otel
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionHandler;

            // Create a new object, representing the German culture.  
            CultureInfo culture = CultureInfo.CreateSpecificCulture("tr-TR");

            // The following line provides localization for the application's user interface.  
            Thread.CurrentThread.CurrentUICulture = culture;

            // The following line provides localization for data formats.  
            Thread.CurrentThread.CurrentCulture = culture;

            // Set this culture as the default culture for all threads in this application.  
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

        }

        static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs args)
        {
            try
            {
                Exception ex = (Exception)args.ExceptionObject;
                MessageBox.Show(ex.Message + "\n\n" + ex.ToString());
                Loglama.Write(ex.ToString());

                ProcessStartInfo startInfo = new ProcessStartInfo(ResourceAssembly.Location);
                startInfo.UseShellExecute = true;//This should not block your program
                Process.Start(startInfo);

                Environment.Exit(0);
            }
            catch
            {
                Environment.Exit(0);
            }
        }

        private void OnAppStartup_UpdateThemeName(object sender, StartupEventArgs e)
        {
            DevExpress.Xpf.Core.ApplicationThemeHelper.UpdateApplicationThemeName();
        }
    }
}