namespace LeahsPlatinumTracker
{

    /// <summary>
    /// <see cref="Checks"/> subclass interface.
    /// </summary>
    public interface IChecks
    {

        /// <summary>
        /// Sets a check flag in this instance.
        /// </summary>
        /// <param name="flag">The enum value of the check flag to be set in this instance.</param>
        void UnlockCheck(int flag);

        /// <summary>
        /// Sets a progress flag in this instance.
        /// </summary>
        /// <param name="flag">The enum value of the progress flag to be set in this instance.</param>
        void UnlockProgress(int flag);

        /// <summary>
        /// Sets a HM flag in this instance.
        /// </summary>
        /// <param name="flag">The enum value of the HM flag to be set in this instance.</param>
        void UnlockHM(int flag);

        /// <summary>
        /// Unsets a check flag in this instance.
        /// </summary>
        /// <param name="flag">The enum value of the check flag to be unset in this instance.</param>
        void LockCheck(int flag);

        /// <summary>
        /// Unsets a progress flag in this instance.
        /// </summary>
        /// <param name="flag">The enum value of the progress flag to be unset in this instance.</param>
        void LockProgress(int flag);

        /// <summary>
        /// Unsets a HM flag in this instance.
        /// </summary>
        /// <param name="flag">The enum value of the HM flag to be unset in this instance.</param>
        void LockHM(int flag);

        /// <summary>
        /// Toggles a check flag in this instance.
        /// </summary>
        /// <param name="flag">The enum value of the check flag to be toggled in this instance.</param>
        /// <returns>A boolean pertaining to the state of the toggled flag.</returns>
        bool ToggleCheck(int flag);

        /// <summary>
        /// Toggles a progress flag in this instance.
        /// </summary>
        /// <param name="flag">The enum value of the progress flag to be toggled in this instance.</param>
        /// <returns>A boolean pertaining to the state of the toggled flag.</returns>
        bool ToggleProgress(int flag);

        /// <summary>
        /// Toggles a HM flag in this instance.
        /// </summary>
        /// <param name="flag">The enum value of the HM flag to be toggled in this instance.</param>
        /// <returns>A boolean pertaining to the state of the toggled flag.</returns>
        bool ToggleHM(int flag);

        bool MeetsRequirements(Checks ReferenceChecks);

        string ToString();

    }

    /// <summary>
    /// <see cref="Checks"/> abstract class.                                                <br />
    ///                                                                                     <br />
    /// Used for keeping track of various progress and area availability related flags.     <br />
    /// </summary>
    public class Checks
    {

        // Instance Fields

        /// <summary>
        /// This instance's associated nonspecific checks.
        /// </summary>
        public int ChecksMade { get; set; }

        /// <summary>
        /// This instance's associated progress related checks.
        /// </summary>
        public int Progress { get; set; }

        /// <summary>
        /// This instance's associated HM related checks.
        /// </summary>
        public int HMs { get; set; }

        // Constructors

        /// <summary>
        /// Empty constructor. Creates a set of checks with nothing set.
        /// </summary>
        public Checks() { }

        /// <summary>
        /// Integer constructor. Used for HM requirements, allowing multiple HM requirements along with their associated required gyms.             <br />
        /// <em>e.g; in Pokémon Platinum, a given value of 65 requires Rock Climb (64), Icicle Badge (64), Rock Smash (1) and Coal Badge (1).</em>  <br />
        /// </summary>
        /// <param name="flags">The given integer to be converted to enum.</param>
        public Checks(int flags)
        {
            Progress = flags;
            HMs = flags;
        }

        // Methods

        /// <summary>
        /// Evaluates if, for every flag that is set in this instance, the given instance has the flag set too.
        /// </summary>
        /// <param name="ReferenceChecks">The Checks instance to compare to this instance.</param>
        /// <returns>A boolean pertaining to whether the given Checks instance meets the flags set in this instance.</returns>
        public bool MeetsRequirements(Checks ReferenceChecks)
        {
            // if this instance has any flags that the reference checks do not, return false
            if ((~ReferenceChecks.ChecksMade & ChecksMade) > 0) return false;
            if ((~ReferenceChecks.Progress & Progress) > 0) return false;
            if ((~ReferenceChecks.HMs & HMs) > 0) return false;
            return true;
        }

        // String localisation

        /// <summary>
        /// A dictionary of strings and their readable stringified versions, localised to current program language.
        /// </summary>
        public static Dictionary<string, string> LocalisedFlagStrings
        {
            get
            {
                Dictionary<string, Dictionary<string,string>> TranslatedStrings = new Dictionary<string, Dictionary<string, string>>
                {
                    { "HasWorksKey",            new Dictionary < string, string > { { "en", "Obtain the Works Key" } } },
                    { "HasGalacticKey",         new Dictionary < string, string > { { "en", "Obtain the Galactic Key" } } },
                    { "HasTeleport",            new Dictionary < string, string > { { "en", "Obtain a Pokémon with Teleport" } } },
                    { "HasBike",                new Dictionary < string, string > { { "en", "Obtain a Bike" } } },
                    { "HasSpokenRoark",         new Dictionary < string, string > { { "en", "Speak to Roark in Oreburgh Mine" } } },
                    { "HasSpokenFantina",       new Dictionary < string, string > { { "en", "Speak to Fantina in the Super Contest Hall" } } },
                    { "HasSpokenVolkner",       new Dictionary < string, string > { { "en", "Speak to Volkner in the Vista Lighthouse" } } },
                    { "HasDefeatedWindworks",   new Dictionary < string, string > { { "en", "Defeat Mars in Valley Windworks" } } },
                    { "HasSecretPotion",        new Dictionary < string, string > { { "en", "Obtain the SecretPotion from Cynthia" } } },
                    { "HasCoalBadge",           new Dictionary < string, string > { { "en", "Obtain the Coal Badge" } } },
                    { "HasForestBadge",         new Dictionary < string, string > { { "en", "Obtain the Forest Badge" } } },
                    { "HasRelicBadge",          new Dictionary < string, string > { { "en", "Obtain the Relic Badge" } } },
                    { "HasCobbleBadge",         new Dictionary < string, string > { { "en", "Obtain the Cobble Badge" } } },
                    { "HasFenBadge",            new Dictionary < string, string > { { "en", "Obtain the Fen Badge" } } },
                    { "HasMineBadge",           new Dictionary < string, string > { { "en", "Obtain the Mine Badge" } } },
                    { "HasIcicleBadge",         new Dictionary < string, string > { { "en", "Obtain the Icicle Badge" } } },
                    { "HasBeaconBadge",         new Dictionary < string, string > { { "en", "Obtain the Beacon Badge" } } },
                    { "HasAaron",               new Dictionary < string, string > { { "en", "Defeat Elite Four Aaron" } } },
                    { "HasBertha",              new Dictionary < string, string > { { "en", "Defeat Elite Four Bertha" } } },
                    { "HasFlint",               new Dictionary < string, string > { { "en", "Defeat Elite Four Flint" } } },
                    { "HasLucian",              new Dictionary < string, string > { { "en", "Defeat Elite Four Lucian" } } },
                    { "HasCynthia",             new Dictionary < string, string > { { "en", "Become Champion" } } },
                    { "HM01",                   new Dictionary < string, string > { { "en", "Obtain HM01 (Cut)" } } },
                    { "HM02",                   new Dictionary < string, string > { { "en", "Obtain HM02 (Fly)" } } },
                    { "HM03",                   new Dictionary < string, string > { { "en", "Obtain HM03 (Surf)" } } },
                    { "HM04",                   new Dictionary < string, string > { { "en", "Obtain HM04 (Strength)" } } },
                    { "HM05",                   new Dictionary < string, string > { { "en", "Obtain HM05 (Defog)" } } },
                    { "HM06",                   new Dictionary < string, string > { { "en", "Obtain HM06 (Rock Smash)" } } },
                    { "HM07",                   new Dictionary < string, string > { { "en", "Obtain HM07 (Waterfall)" } } },
                    { "HM08",                   new Dictionary < string, string > { { "en", "Obtain HM08 (Rock Climb)" } } }
                };

                Dictionary<string, string> LocalisedStrings = new();
                foreach(KeyValuePair<string, Dictionary<string, string>> entry in TranslatedStrings)
                {
                    LocalisedStrings.Add(entry.Key, entry.Value[Program.Language]);
                }

                return LocalisedStrings;
            }
        }
    }

}
