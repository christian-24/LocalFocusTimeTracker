# 📘 Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),  
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).


## [1.2.1] - 2025-04-16

### 🐛 Fixed
- Fixed WebView2 error when reopening the statistics window (`WebView2 already initialized` or `Access Denied`)
- WebView2 instance is now safely reused and HTML reloaded on refresh
- Avoided file access conflicts by ensuring shared HTML path reuse without regeneration collisions

---

## [1.2.0] - 2025-04-15

### 🛠️ Changed
- Updated project metadata to set `"christian-24"` as the official Author of the extension
- Renamed `Vsix.Author` and adjusted manifest consistency

---

## [1.1.0] - 2025-04-13

### ✨ Added
- New **Statistics ToolWindow** showing today's time spent per solution
- HTML-based stats UI rendered using **WebView2**, with elegant dark table and doughnut chart
- JSON summary (`track.json`) is now visualized as interactive HTML
- Pie chart powered by **Chart.js** embedded directly in HTML
- Added support for tracking multiple solutions and showing per-project time breakdown
- New UI layer: `StatisticsWindow.xaml` used to host `WebView2` for improved modularity

### 🛠️ Changed
- Refactored JSON save logic to preserve unrelated projects in the data file
- Moved `SaveTime` and `ClearTime` logic to per-project-safe updates
- Added daily cleanup for outdated time entries

---

## [1.0.0] - 2025-03-28

### ✨ Added
- Initial version of **LocalFocusTimeTracker**
- Created Visual Studio extension project using Community VSIX template
- Added time tracking service that only counts time when the VS window is focused
- Time is shown in the status bar and resets daily
- Time tracking per solution is stored in a local JSON file under `%APPDATA%\LocalFocusTimeTracker\track.json`
- Added license (MIT) and icon

---

