# 🕒 LocalFocusTimeTracker

**LocalFocusTimeTracker** is a lightweight and privacy-friendly Visual Studio 2022 extension that tracks your active development time — **only when the Visual Studio window is focused**. It resets the timer daily and works locally, with no data sent anywhere.

---

## ✨ Features

- ✅ Shows time spent per solution in the Visual Studio status bar.
- ✅ Tracks time **only when the VS window is focused** (multi-monitor safe).
- ✅ Automatically resets time tracking every new day.
- ✅ Saves data locally in JSON format (`AppData/LocalFocusTimeTracker/track.json`).
- ✅ One file tracks all solutions, keeping history clean.

---

## 🔐 Privacy

This extension:
- Stores all time-tracking data **locally**.
- Does **not** send any data to external servers.
- Does **not** track keystrokes or file content.

---

## ⚙️ Installation

1. Download from Visual Studio Marketplace or install the `.vsix` file manually.
2. Open any solution.
3. Watch the time tracker appear in the Visual Studio status bar.

---

## 💡 Use Cases

- Freelancers or consultants tracking billable hours per solution.
- Developers who want to stay focused.
- Teams needing transparent time tracking without cloud services.

---

## 🧰 File location

Tracked data is saved to:
%APPDATA%\LocalFocusTimeTracker\track.json


You can open this manually or programmatically to analyze your productivity.

---

## 🛠️ Developer

Created by Christian-24
Feel free to contribute or report issues.

---

## 📄 License

This extension is licensed under the MIT License.
