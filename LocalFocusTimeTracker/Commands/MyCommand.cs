using LocalFocusTimeTracker.ToolWindows;

namespace LocalFocusTimeTracker.Commands
{
    [Command(PackageIds.MyCommand)]
    [ProvideToolWindow(typeof(StatisticsToolWindow))]
    internal sealed class MyCommand : BaseCommand<MyCommand>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            await this.Package.ShowToolWindowAsync(
                typeof(StatisticsToolWindow),
                0,
                true,
                this.Package.DisposalToken);
        }
    }
}
