using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
            CueDefinition myCueDefinition = new CueDefinition();

            // Adding the name for the cue, which will be
            // the name of the audio to play when using sound functions.
            myCueDefinition.name = "wakeUp";
            myCueDefinition.instanceLimit = 1;
            myCueDefinition.limitBehavior = CueDefinition.LimitBehavior.ReplaceOldest;

            // Get the audio file and add it to a SoundEffect.
            SoundEffect audio;
            string filePathCombined = Path.Combine(this.Helper.DirectoryPath, "voicemod-wake-up-highpitch.wav");
            using (var stream = new System.IO.FileStream(filePathCombined, System.IO.FileMode.Open)) {
                audio = SoundEffect.FromStream(stream);
            }

            // Setting the sound effect to the new cue.
            myCueDefinition.SetSound(audio, Game1.audioEngine.GetCategoryIndex("Sound"), false);

            // Adding the cue to the sound bank.
            Game1.soundBank.AddCue(myCueDefinition);

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
                Game1.playSound("wakeUp");

                //this.Monitor.Log(message, LogLevel.Debug);
            }
        }
    }

}