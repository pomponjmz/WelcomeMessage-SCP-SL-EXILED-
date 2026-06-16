using System;
using System.Text;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;

namespace WelcomeBroadcast
{
    /// <summary>
    /// Handles game events for the WelcomeBroadcast plugin.
    /// </summary>
    public class EventHandlers
    {
        private readonly Config _config;

        /// <summary>
        /// Initializes a new instance of <see cref="EventHandlers"/>.
        /// </summary>
        /// <param name="config">The plugin config.</param>
        public EventHandlers(Config config)
        {
            _config = config;
        }

        /// <summary>
        /// Fired when a player's identity has been verified (they have fully joined).
        /// Sends a personalized, rainbow-colored welcome broadcast visible only to that player.
        /// </summary>
        /// <param name="ev">The event arguments containing the joining player.</param>
        public void OnPlayerVerified(VerifiedEventArgs ev)
        {
            Player player = ev.Player;

            // Replace {username} placeholder, then wrap every character in a rainbow color tag
            string rawMessage = _config.WelcomeMessage.Replace("{username}", player.Nickname);
            string coloredMessage = ApplyRainbow(rawMessage, _config.StartHue, _config.HueSpread);

            // Broadcast only to this specific player
            player.Broadcast(_config.BroadcastDuration, coloredMessage, Broadcast.BroadcastFlags.Normal);

            if (Plugin.Instance?.Config.Debug == true)
                Log.Debug($"[WelcomeBroadcast] Sent welcome to: {player.Nickname}");
        }

        /// <summary>
        /// Wraps each non-whitespace character in a Unity rich-text color tag,
        /// cycling hues across the full message to create a smooth rainbow gradient.
        /// Whitespace characters are left untagged so spacing is preserved naturally.
        /// </summary>
        /// <param name="text">The plain text to colorize.</param>
        /// <param name="startHue">Starting hue in degrees (0–360).</param>
        /// <param name="hueSpread">How many degrees of the color wheel to span across the text.</param>
        /// <returns>Rich-text string with per-character color tags.</returns>
        private static string ApplyRainbow(string text, float startHue, float hueSpread)
        {
            // Count only visible (non-whitespace) characters so spaces don't consume hue steps
            int visibleCount = 0;
            foreach (char c in text)
                if (!char.IsWhiteSpace(c)) visibleCount++;

            if (visibleCount == 0)
                return text;

            var sb = new StringBuilder(text.Length * 28); // pre-allocate space for color tags
            int visibleIndex = 0;

            foreach (char c in text)
            {
                if (char.IsWhiteSpace(c))
                {
                    sb.Append(c);
                    continue;
                }

                // Spread the hue evenly across all visible characters
                float hue = (startHue + hueSpread * visibleIndex / Math.Max(visibleCount - 1, 1)) % 360f;
                (byte r, byte g, byte b) = HsvToRgb(hue, saturation: 1f, value: 1f);

                sb.Append($"<color=#{r:X2}{g:X2}{b:X2}>{c}</color>");
                visibleIndex++;
            }

            return sb.ToString();
        }

        /// <summary>
        /// Converts an HSV color to RGB bytes.
        /// </summary>
        /// <param name="h">Hue in degrees (0–360).</param>
        /// <param name="saturation">Saturation (0–1).</param>
        /// <param name="value">Value/brightness (0–1).</param>
        private static (byte r, byte g, byte b) HsvToRgb(float h, float saturation, float value)
        {
            h = ((h % 360f) + 360f) % 360f; // ensure positive

            float c = value * saturation;
            float x = c * (1f - Math.Abs((h / 60f) % 2f - 1f));
            float m = value - c;

            float r1, g1, b1;
            if      (h < 60f)  { r1 = c; g1 = x; b1 = 0; }
            else if (h < 120f) { r1 = x; g1 = c; b1 = 0; }
            else if (h < 180f) { r1 = 0; g1 = c; b1 = x; }
            else if (h < 240f) { r1 = 0; g1 = x; b1 = c; }
            else if (h < 300f) { r1 = x; g1 = 0; b1 = c; }
            else               { r1 = c; g1 = 0; b1 = x; }

            return (
                (byte)Math.Round((r1 + m) * 255),
                (byte)Math.Round((g1 + m) * 255),
                (byte)Math.Round((b1 + m) * 255)
            );
        }
    }
}
