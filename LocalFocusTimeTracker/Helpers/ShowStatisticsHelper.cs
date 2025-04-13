using LocalFocusTimeTracker.ToolWindows;

namespace LocalFocusTimeTracker.Helpers
{
    [Command(PackageIds.ShowStatsCommand)]
    internal class ShowStatisticsHelper : BaseCommand<ShowStatisticsHelper>
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
