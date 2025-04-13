using LocalFocusTimeTracker.Dtos;
using Microsoft.Web.WebView2.Core;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace LocalFocusTimeTracker.Forms
{
    public partial class StatisticsWindow : UserControl
    {
        public StatisticsWindow()
        {
            InitializeComponent();
            LoadData();
            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            string userDataFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "WebView2DemoUserData");

            try
            {
                var env = await CoreWebView2Environment.CreateAsync(null, userDataFolder);
                await MyWebView.EnsureCoreWebView2Async(env);

                string htmlPath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "LocalFocusTimeTracker", "index.html");

                if (!File.Exists(htmlPath))
                {
                    File.WriteAllText(htmlPath, "<html><body><h1>Brak danych</h1></body></html>");
                }

                string fixedPath = "file:///" + htmlPath.Replace("\\", "/");
                MyWebView.Source = new Uri(fixedPath);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Błąd WebView2: " + ex.Message);
            }
        }

        private void LoadData()
        {
            string path = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "LocalFocusTimeTracker", "track.json");

            if (!File.Exists(path)) return;

            string json = File.ReadAllText(path);
            var data = JsonSerializer.Deserialize<Dictionary<string, TimeEntryDto>>(json);

            string today = DateTime.Now.ToString("yyyy-MM-dd");

            var todayEntries = data?
                .Where(x => x.Value.Date == today)
                .ToList();

            if (todayEntries == null || todayEntries.Count == 0) return;

            int totalSeconds = todayEntries.Sum(x => x.Value.Seconds);

            var displayList = todayEntries
                .Select(x => new TimeEntryDisplayDto
                {
                    Name = x.Key,
                    Time = TimeSpan.FromSeconds(x.Value.Seconds).ToString(@"hh\:mm\:ss"),
                    Percent = totalSeconds > 0 ? (int)Math.Round(x.Value.Seconds * 100.0 / totalSeconds) : 0
                })
                .OrderByDescending(x => x.Percent) // opcjonalnie: sortuj od największego udziału
                .ToList();

            var html = $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
    <title>Time Tracker</title>
    <script src='https://cdn.jsdelivr.net/npm/chart.js'></script>
    <style>
        body {{
            background: #1e1e1e;
            color: white;
            font-family: 'Segoe UI';
            padding: 20px;
        }}
        table {{
            width: 100%;
            border-collapse: collapse;
            margin-top: 40px;
        }}
        th, td {{
            padding: 10px;
            border-bottom: 1px solid #333;
            text-align: left;
        }}
        th {{
            background: #2d2d30;
        }}
    </style>
</head>
<body>
    <h1>📊 Projects Today</h1>

    <div style='display: flex; justify-content: center; align-items: center; margin-bottom: 40px;'>
        <canvas id='projectChart' width='200' height='200'></canvas>
    </div>

    <table>
        <thead>
            <tr><th>Project</th><th>Time</th><th>%</th></tr>
        </thead>
        <tbody>
            {string.Join("\n", displayList.Select(x => $"<tr><td>{x.Name}</td><td>{x.Time}</td><td>{x.Percent}%</td></tr>"))}
        </tbody>
    </table>

    <script>
        const ctx = document.getElementById('projectChart').getContext('2d');
        const chart = new Chart(ctx, {{
            type: 'doughnut',
            data: {{
                labels: [{string.Join(",", displayList.Select(x => $"'{x.Name}'"))}],
                datasets: [{{
                    label: 'Time Share (%)',
                    data: [{string.Join(",", displayList.Select(x => x.Percent))}],
                    backgroundColor: ['#3498db', '#2ecc71', '#e74c3c', '#9b59b6', '#f1c40f'],
                    borderColor: '#111',
                    borderWidth: 1
                }}]
            }},
            options: {{
                responsive: false,
                plugins: {{
                    legend: {{
                        labels: {{
                            color: 'white'
                        }},
                        position: 'bottom'
                    }}
                }}
            }}
        }});
    </script>
</body>
</html>";


            string indexPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "LocalFocusTimeTracker", "index.html");

            File.WriteAllText(indexPath, html);
        }
    }
}
