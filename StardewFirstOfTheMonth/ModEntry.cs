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
            CueDefinition wakeUpCueDefinition = new CueDefinition();
            CueDefinition ballinCueDefinition = new CueDefinition();

            // Adding the name for the cue, which will be
            // the name of the audio to play when using sound functions.
            wakeUpCueDefinition.name = "wakeUp";
            wakeUpCueDefinition.instanceLimit = 1;
            wakeUpCueDefinition.limitBehavior = CueDefinition.LimitBehavior.ReplaceOldest;

            ballinCueDefinition.name = "ballin";
            ballinCueDefinition.instanceLimit = 1;
            ballinCueDefinition.limitBehavior = CueDefinition.LimitBehavior.ReplaceOldest;

            // Get the audio file and add it to a SoundEffect.
            SoundEffect wakeUpAudio;
            string filePathCombined = Path.Combine(this.Helper.DirectoryPath, "voicemod-wake-up-highpitch.wav");
            using (var stream = new System.IO.FileStream(filePathCombined, System.IO.FileMode.Open)) {
                wakeUpAudio = SoundEffect.FromStream(stream);
            }

            SoundEffect ballinAudio;
            filePathCombined = Path.Combine(this.Helper.DirectoryPath, "ballin-voicemod.wav");
            using (var stream = new System.IO.FileStream(filePathCombined, System.IO.FileMode.Open)) {
                ballinAudio = SoundEffect.FromStream(stream);
            }

            // Setting the sound effect to the new cue.
            wakeUpCueDefinition.SetSound(wakeUpAudio, Game1.audioEngine.GetCategoryIndex("Sound"), false);
            ballinCueDefinition.SetSound(ballinAudio, Game1.audioEngine.GetCategoryIndex("Sound"), false);

            // Adding the cue to the sound bank.
            Game1.soundBank.AddCue(wakeUpCueDefinition);

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

            if (Game1.timeOfDay == 600)
                if (Game1.dayOfMonth == 1) 
                {
                    string message = "WAKE UP! IT'S THE FIRST OF THE MONTH";
                    Game1.activeClickableMenu = new DialogueBox(message);
                    Game1.playSound("wakeUp");

                    //this.Monitor.Log(message, LogLevel.Debug);
                }
                else if (Game1.dayOfMonth == 4)
                {
                    string message = "BALL BALL BALL LIN LIN BALL BALL BALL \n My granny called she said Travvy you work too hard, I'm worried you'll forget about me \n BALL BALL \n I'm falling in and out of clouds, don't worry, Imma get it, Granny";
                    Game1.activeClickableMenu = new DialogueBox(message);
                    Game1.playSound("ballin");
                }
        }
    }

}