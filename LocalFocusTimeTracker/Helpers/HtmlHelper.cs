using LocalFocusTimeTracker.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace LocalFocusTimeTracker.Helpers
{
    internal class HtmlHelper
    {
        internal static string CreateProjectStatsTemplate(List<TimeEntryDisplayDto> displayList)
        {
            return $@"
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
                        .refresh-btn {{
                                background-color: #007acc;
                                color: white;
                                border: none;
                                padding: 10px 20px;
                                font-size: 14px;
                                border-radius: 4px;
                                cursor: pointer;
                                margin-bottom: 20px;
                                transition: background-color 0.2s ease;
                            }}

                        .refresh-btn:hover {{
                            background-color: #005f99;
                        }}
                    </style>
                <script>
                    function refreshStats() {{
                        if (window.chrome && window.chrome.webview) {{
                            window.chrome.webview.postMessage('refresh');
                        }} else {{
                            alert(""WebView2 not available."");
                        }}
                    }}
                </script>
                </head>
                <body>
                    <div style=""display: flex; justify-content: flex-end;"">
                        <button onclick=""refreshStats()"" class=""refresh-btn"">🔄 Refresh data</button>
                    </div>
                    <h1>📊 Projects Today</h1>

                    <div style='display: flex; justify-content: center; align-items: center; margin-bottom: 40px; width: 100%;'>
                        <canvas id='projectChart' style='max-width: 400px; max-height: 400px;'></canvas>
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
                                maintainAspectRatio: false,
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
        }
    }
}
