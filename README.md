# 🌈 WelcomeBroadcast — Exiled Plugin

A lightweight **SCP: Secret Laboratory** plugin built on the [Exiled](https://github.com/ExMod-Team/EXILED) framework.  
When a player joins your server, they receive a **personalized, rainbow-colored broadcast** visible only to them.

---

## ✨ Features

- 📢 **Per-player broadcast** — the message is shown **only** to the joining player, not everyone on the server.
- 🌈 **Automatic rainbow coloring** — every character in the message gets its own color, smoothly cycling through the color wheel.
- 👤 **Username placeholder** — use `{username}` anywhere in the message and it gets replaced with the player's real in-game nickname.
- ⏱️ **Configurable duration** — control exactly how long the message stays on screen.
- 🎨 **Tunable rainbow** — adjust the starting color and how wide the color spread is, straight from the config.
- 🔤 **Rich-text support** — use Unity rich-text tags like `<b>`, `<i>`, and `<size>` inside your message.
- ⚙️ **Zero hardcoding** — every setting lives in the Exiled config file; no recompile needed to change anything.

---

## 📋 Requirements

| Requirement | Version |
|---|---|
| SCP: Secret Laboratory | Latest stable |
| EXILED | **8.9.11** or newer |

---

## 📥 Installation

1. Download the latest `WelcomeBroadcast.dll` from the [Releases](../../releases) page.
2. Place it in your server's plugin folder:
   ```
   ~/.config/EXILED/Plugins/WelcomeBroadcast.dll       ← Pathway
   ```
3. **Start your server once.** Exiled will auto-generate the config file.
4. Edit the config (see below) to customize the message.
5. Restart or reload (`exiled reload`) — done!

---

## ⚙️ Configuration

The config is found inside your main Exiled config file (usually `config.yml`), under the `welcome_broadcast` section.

```yaml
welcome_broadcast:

  # Set to false to completely disable this plugin.
  is_enabled: true

  # Set to true to print extra info to the server console (useful for troubleshooting).
  debug: false

  # The message shown to a player when they join.
  #   • {username} is replaced with the player's in-game nickname automatically.
  #   • You can use <b>bold</b>, <i>italic</i>, or <size=24>sized</size> rich-text tags.
  #   • Do NOT add <color> tags — rainbow coloring is applied automatically.
  welcome_message: "Welcome to the server {username}, have fun!"

  # How many seconds the message stays on screen. (Recommended: 8–12)
  broadcast_duration: 10

  # The color the rainbow starts from, in degrees on the color wheel.
  #   0   = Red
  #   60  = Yellow
  #   120 = Green
  #   180 = Cyan
  #   240 = Blue
  #   300 = Magenta
  start_hue: 0

  # How wide the rainbow spreads across the full message, in degrees.
  #   360 = full rainbow (red → yellow → green → cyan → blue → magenta)
  #   180 = half rainbow (e.g. red → cyan)
  #    60 = narrow band  (e.g. red → yellow only)
  hue_spread: 300
```

---

## 🎨 How the Rainbow Works

Each **non-whitespace character** in the message gets wrapped in a Unity `<color=#RRGGBB>` rich-text tag.  
The hue is distributed evenly across all visible characters using the **HSV color model**, so the transition is always smooth regardless of message length.

Spaces are intentionally skipped — they don't "waste" any hue steps, so the color flow stays consistent even with gaps in the text.

**Example output** for `"Welcome to the server PlayerName, have fun!"`:

```
W  e  l  c  o  m  e     t  o  …
🔴 🟠 🟡 🟢 🔵 🟣 🔴 __ 🟠 🟡
```

> **Tip:** Set `hue_spread: 360` for a full-circle rainbow, or try `start_hue: 240` to start from blue instead of red.

---

## 💬 Rich-Text Examples

You can use any Unity rich-text formatting inside `welcome_message`. Color tags are added on top automatically.

| Tag | Effect | Example |
|---|---|---|
| `<b>text</b>` | **Bold** | `<b>Welcome</b> to the server` |
| `<i>text</i>` | *Italic* | `Have <i>fun</i>!` |
| `<size=30>text</size>` | Larger text | `<size=30>Welcome</size> {username}` |

> ⚠️ Do **not** add `<color>` tags manually — they will conflict with the automatic rainbow coloring.

---

## 📁 Project Structure

```
WelcomeBroadcast/
├── Plugin.cs           # Entry point — registers and unregisters event handlers
├── Config.cs           # All configurable settings with descriptions
├── EventHandlers.cs    # Listens for player join, builds and sends the broadcast
└── WelcomeBroadcast.csproj
```

---

## 📄 License

This project is open-source. Feel free to use or modify. Don't repost as your own if you don't:
-credit or link back to this repository.
