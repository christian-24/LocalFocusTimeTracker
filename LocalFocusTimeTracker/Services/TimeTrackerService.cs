using EnvDTE;
using LocalFocusTimeTracker.Helpers;
using LocalFocusTimeTracker.Storage;
using System.Windows.Threading;

namespace LocalFocusTimeTracker.Services
{
    public class TimeTrackerService
    {
        private static DispatcherTimer _timer;
        private static int             _secondsSpentToday;

        public static async Task InitializeAsync(AsyncPackage package)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            TimeStorage.ClearOutdated();

            _secondsSpentToday = TimeStorage.LoadTime();

            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };

            _timer.Tick += (s, e) =>
            {
                ThreadHelper.ThrowIfNotOnUIThread();

                var dte = (DTE)Package.GetGlobalService(typeof(DTE));
                if (dte != null && dte.MainWindow != null && dte.MainWindow.Visible && dte.MainWindow.HWnd == NativeMethodsHelper.GetForegroundWindow())
                {
                    _secondsSpentToday++;
                    TimeStorage.SaveTime(_secondsSpentToday);
                    UpdateStatusBar(dte);
                }
            };

            _timer.Start();
        }

        private static void UpdateStatusBar(DTE dte)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            int hours          = _secondsSpentToday / 3600;
            int minutes        = (_secondsSpentToday % 3600) / 60;
            dte.StatusBar.Text = $"⏱️ Project time today: {hours:D2}:{minutes:D2}";
        }
    }
}
