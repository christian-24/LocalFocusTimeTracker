using System.Runtime.InteropServices;

namespace LocalFocusTimeTracker.ToolWindows
{
    [Guid("2e89e55d-9333-4e3e-bb3a-1c97b5554444")]
    public class StatisticsToolWindow : ToolWindowPane
    {
        public StatisticsToolWindow() : base(null)
        {
            this.Caption = "Project Time Stats";
            this.Content = new Forms.StatisticsWindow();
        }
    }
}
