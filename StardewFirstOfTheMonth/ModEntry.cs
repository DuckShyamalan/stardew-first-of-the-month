using System;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley.Menus; // for dialogue box
using StardewValley;

namespace StardewFirstOfTheMonth {

    public class ModEntry : Mod {
        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper) {
            helper.Events.GameLoop.SaveLoaded += this.OnSaveLoaded;
        }

        /*********
        ** Private methods
        *********/
        /// <summary>Raised after the player loads a save slot.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void OnSaveLoaded(object? sender, SaveLoadedEventArgs e) {
            // ignore if a player hasn't loaded a save yet.
            if (!Context.IsWorldReady) return;

            if (Game1.dayOfMonth == 1 && Game1.timeOfDay == 600) {
                string message = "WAKE UP! IT'S THE FIRST OF THE MONTH";
                Game1.activeClickableMenu = new DialogueBox(message);
                //this.Monitor.Log(message, LogLevel.Debug);
            }
        }
    }

}