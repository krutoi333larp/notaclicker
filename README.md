# notclicker 🖱️

This is my first **.NET 9.0** program after just four days of learning C#! 

It’s a lightweight, high-performance auto-clicker built with **WPF** and native **WinAPI** (`mouse_event`). 

## Developer's Note
- **Every single line of code is commented by me in Russian** to ensure I completely understand the underlying logic (this isn't just "vibe code").
- The UI design was generated with the help of AI because XAML layout is from the 90s, and I wanted it to look human-friendly and clean.

## Features
- **Two Trigger Modes:** 
  - `Hold` — Clicks only while the hotkey is pressed.
  - `Toggle` — Press once to start, press again to stop.
- **OS Spam Protection:** The `Toggle` mode architecture handles logic on `KeyUp` cycles, completely circumventing Windows native key-repeat event spam.
- **Safety Delay:** Built-in properties protection (`get`/`set`) that prevents system crashes by locking the minimum delay to 10ms.
- **Asynchronous Engine:** Uses `async/await` and `Task.Delay` to keep the UI perfectly responsive.

## Tech Stack
- **Language & Runtime:** C# (.NET 9.0)
- **UI Framework:** WPF (Windows Presentation Foundation)
- **APIs:** Native WinAPI (`user32.dll`)
- **Libraries:** KeyboardHook 
