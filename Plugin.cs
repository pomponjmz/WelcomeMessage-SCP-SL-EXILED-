using System;
using Exiled.API.Features;
using Exiled.API.Interfaces;

namespace WelcomeBroadcast
{
    /// <summary>
    /// Main plugin class for WelcomeBroadcast.
    /// Sends a personalized welcome broadcast to each player upon joining.
    /// </summary>
    public class Plugin : Plugin<Config>
    {
        // Singleton instance
        public static Plugin? Instance { get; private set; }

        private EventHandlers? _eventHandlers;

        /// <inheritdoc />
        public override string Name => "WelcomeBroadcast";

        /// <inheritdoc />
        public override string Author => "Pustkownia";

        /// <inheritdoc />
        public override string Prefix => "welcome_broadcast";

        /// <inheritdoc />
        public override Version Version => new Version(1, 0, 0);

        /// <inheritdoc />
        public override Version RequiredExiledVersion => new Version(8, 0, 0);

        /// <inheritdoc />
        public override void OnEnabled()
        {
            Instance = this;
            _eventHandlers = new EventHandlers(Config);

            Exiled.Events.Handlers.Player.Verified += _eventHandlers.OnPlayerVerified;

            base.OnEnabled();
            Log.Info($"{Name} v{Version} has been enabled!");
        }

        /// <inheritdoc />
        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Player.Verified -= _eventHandlers.OnPlayerVerified;

            _eventHandlers = null;
            Instance = null;

            base.OnDisabled();
            Log.Info($"{Name} has been disabled.");
        }
    }
}
