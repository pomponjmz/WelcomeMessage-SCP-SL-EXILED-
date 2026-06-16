using Exiled.API.Interfaces;
using System.ComponentModel;

namespace WelcomeBroadcast
{
    /// <summary>
    /// Configuration for the WelcomeBroadcast plugin.
    /// Edit these values in your EXILED/Configs/ folder — no recompile needed.
    /// </summary>
    public class Config : IConfig
    {
        // ── General ──────────────────────────────────────────────────────────

        [Description("Set to false to completely disable this plugin.")]
        public bool IsEnabled { get; set; } = true;

        [Description("Set to true to print extra info to the server console (useful for troubleshooting).")]
        public bool Debug { get; set; } = false;

        // ── Message ───────────────────────────────────────────────────────────

        [Description(
            "The message shown to a player when they join.\n" +
            "  • Use {username} where you want their in-game nickname to appear.\n" +
            "  • You can also use <b>bold</b> or <i>italic</i> rich-text tags.\n" +
            "  • Do NOT add <color> tags yourself — rainbow coloring is applied automatically.")]
        public string WelcomeMessage { get; set; } = "Welcome to the server {username}, have fun!";

        [Description("How many seconds the welcome message stays on screen. (Recommended: 8–12)")]
        public ushort BroadcastDuration { get; set; } = 10;

        // ── Rainbow colors ────────────────────────────────────────────────────

        [Description(
            "The color the rainbow starts from, in degrees on the color wheel.\n" +
            "  0   = Red\n" +
            "  60  = Yellow\n" +
            "  120 = Green\n" +
            "  180 = Cyan\n" +
            "  240 = Blue\n" +
            "  300 = Magenta")]
        public float StartHue { get; set; } = 0f;

        [Description(
            "How wide the rainbow spreads across the full message, in degrees.\n" +
            "  360 = full rainbow (red → orange → yellow → green → cyan → blue → magenta → red)\n" +
            "  180 = half rainbow (e.g. red → cyan)\n" +
            "   60 = narrow band (e.g. red → yellow only)")]
        public float HueSpread { get; set; } = 300f;
    }
}
