# SafeVolume 🔊

A lightweight Windows utility that prevents sudden volume spikes when connecting headphones or Bluetooth audio devices.

---

## 🚨 Problem

On Windows, connecting headphones or switching audio devices can sometimes reset the volume to a very high level, causing sudden loud audio and potential hearing damage.

---

## ✅ Solution

SafeVolume automatically detects audio device changes and caps the volume to a safe level instantly.

---

## ✨ Features

- 🔒 Automatic volume limiting on device change  
- 🎧 Works with Bluetooth and wired headphones  
- 🖥️ Runs in system tray (background)  
- ⚡ Start with Windows option  
- 💾 Remembers your settings  
- 🔔 Notification when volume is capped  

---

## 🛠️ How it works

- Monitors default audio device changes  
- Instantly sets system volume to user-defined limit  
- Ensures safe listening experience  

---

## 🚀 Usage

1. Run `SafeVolume.exe`  
2. Enable protection  
3. Set your preferred max volume  
4. App runs in background automatically  

---

## 📦 Installation

Download the latest release and run:
```SafeVolume.exe```


No installation required.

---

## 🧱 Build Instructions

### 🔹 Requirements

- Windows 10 / 11  
- Visual Studio 2022 (with .NET desktop development)  
- .NET 8 SDK  

---

### 🔹 Steps to Build

1. Clone the repository:

```bash
git clone https://github.com/YOUR_USERNAME/SafeVolume.git
cd SafeVolume
```
Open the project:
Open SafeVolume.sln in Visual Studio
Restore dependencies:
Visual Studio will automatically restore NuGet packages (NAudio)
Build the project:
Set configuration to Release
Click Build → Build Solution


🔹 Output

The executable will be located at:

SafeVolume/bin/Release/net8.0-windows/
🔹 Run

Simply run:

SafeVolume.exe
🔹 Optional: Publish (Single Executable)

To create a portable build:

Right-click project → Publish
Select Folder
Set:
Deployment mode → Self-contained
Runtime → win-x64
Produce single file → Enabled
Click Publish

Output will be in:

bin/Release/net8.0-windows/publish/win-x64/
⚠️ Note
Keep all files in the release folder together
Designed for Windows 10/11
💡 Motivation

This tool was built to solve a common issue discussed across forums and Reddit where users experience sudden loud audio when connecting headphones.
