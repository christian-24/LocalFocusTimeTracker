using Microsoft.VisualStudio.Shell;
using System;
using System.Runtime.InteropServices;
using System.Windows.Controls;
using Microsoft.Web.WebView2.Wpf;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using LocalFocusTimeTracker.Dtos;
using System.Windows;
using LocalFocusTimeTracker.Forms;

namespace LocalFocusTimeTracker.ToolWindows
{
    [Guid("2e89e55d-9333-4e3e-bb3a-1c97b5554444")]
    public class StatisticsToolWindow : ToolWindowPane
    {
        public StatisticsToolWindow() : base(null)
        {
            this.Caption = "Project Time Stats";
            this.Content = new LocalFocusTimeTracker.Forms.StatisticsWindow();
        }
    }
}
