namespace LeahsPlatinumTracker
{
    /// <summary>
    /// <see cref="Checks"/> subclass for Pokémon Platinum. Extends <see cref="IChecks"/>.
    /// </summary>
    public class PlatinumChecks : Checks, IChecks
    {
        // Unique enums

        /// <summary>
        /// Check flags. Contains checks that aren't inherently related to progress, but still important for tracking area availability.
        /// </summary>
        [Flags]
        public enum CheckFlags
        {
            None = 0,
            HasWorksKey = 1,
            HasGalacticKey = 2,
            HasTeleport = 4,
            HasBike = 8,
            HasSpokenRoark = 16,
            HasSpokenFantina = 32,
            HasSpokenVolkner = 64,
            HasDefeatedWindworks = 128,
            HasSecretPotion = 256
        }

        /// <summary>
        /// Progress flags. Contains checks that are relevant to typical game progression, like gyms and elite four members.
        /// </summary>
        [Flags]
        public enum ProgressFlags
        {
            None = 0,
            HasCoalBadge = 1,
            HasForestBadge = 2,
            HasRelicBadge = 4,
            HasCobbleBadge = 8,
            HasFenBadge = 16,
            HasMineBadge = 32,
            HasIcicleBadge = 64,
            HasBeaconBadge = 128,
            HasAaron = 256,
            HasBertha = 512,
            HasFlint = 1024,
            HasLucian = 2048,
            HasCynthia = 4096
        }

        /// <summary>
        /// HM flags. Contains checks for HM unlocks.
        /// </summary>
        [Flags]
        public enum HMFlags
        {
            None = 0,
            HM06 = 1,   // Rock Smash
            HM01 = 2,   // Cut
            HM05 = 4,   // Defog
            HM02 = 8,   // Fly
            HM03 = 16,  // Surf
            HM04 = 32,  // Strength
            HM08 = 64,  // Rock Climb
            HM07 = 128  // Waterfall
        }

        // String Localisation

        /// <summary>
        /// A Dictionary of values used for localising strings of flag names for Pokémon Platinum.
        /// </summary>
        internal Dictionary<string, Dictionary<string, string>> TranslatedStrings = new Dictionary<string, Dictionary<string, string>>
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

        // Common constructors

        /// <summary>
        /// Empty constructor. Creates a set of checks with nothing set.
        /// </summary>
        public PlatinumChecks(): base() {}

        /// <summary>
        /// Integer constructor. Used for HM requirements, allowing multiple HM requirements along with their associated required gyms.             <br />
        /// <em>e.g; in Pokémon Platinum, a given value of 65 requires Rock Climb (64), Icicle Badge (64), Rock Smash (1) and Coal Badge (1).</em>  <br />
        /// </summary>
        /// <param name="flags">The given integer to be converted to enum.</param>
        public PlatinumChecks(int flags): base(flags) {}

        /// <summary>
        /// Complex flag constructor. Can be given any amount of flags in any order. Flags of different categories must be passed separately.
        /// </summary>
        /// <param name="flagObj1">Any flag or combination of flags from this instance.</param>
        /// <param name="flagObj2">Any flag or combination of flags from this instance. Optional parameter.</param>
        /// <param name="flagObj3">Any flag or combination of flags from this instance. Optional parameter.</param>
        public PlatinumChecks(object flagObj1, object? flagObj2 = null, object? flagObj3 = null)
        {
            List<object> list = new() { flagObj1 };
            if (flagObj2 != null) list.Add(flagObj2);
            if (flagObj3 != null) list.Add(flagObj3);

            foreach(object flagObj in list)
            {
                Type type = flagObj.GetType();
                if (type == typeof(CheckFlags))
                {
                    ChecksMade = (int)flagObj;
                    continue;
                }
                else if (type == typeof(ProgressFlags))
                {
                    Progress = (int)flagObj;
                    continue;
                }
                else if (type == typeof(HMFlags))
                {
                    HMs = (int)flagObj;
                    continue;
                }

                throw new Exception(GetType().Name + ": Tried to use an invalid flag in constructor. (" + flagObj.GetType() + ")");
            }
        }

        // Common interface methods

        public void UnlockCheck(int flag) { ChecksMade |= flag; }
        public void UnlockProgress(int flag) { Progress |= flag; }
        public void UnlockHM(int flag) { HMs |= flag; }

        public void LockCheck(int flag) { ChecksMade &= ~flag; }
        public void LockProgress(int flag) { Progress &= ~flag; }
        public void LockHM(int flag) { HMs &= ~flag; }

        public bool ToggleCheck(int flag) { ChecksMade ^= flag; return (ChecksMade & flag) > 0; }
        public bool ToggleProgress(int flag) { Progress ^= flag; return (Progress & flag) > 0; }
        public bool ToggleHM(int flag) { HMs ^= flag; return (HMs & flag) > 0;}

        /// <summary>
        /// Returns a dictionary of strings and their readable stringified versions, localised to current program language.
        /// </summary>
        public Dictionary<string, string> GetLocalisedFlagStrings()
        {
            Dictionary<string, string> LocalisedStrings = new();
            foreach (KeyValuePair<string, Dictionary<string, string>> entry in TranslatedStrings)
            {
                LocalisedStrings.Add(entry.Key, entry.Value[Program.Language]);
            }

            return LocalisedStrings;
        }

        /// <summary>
        /// Returns a string representing the flags set in this instance.
        /// </summary>
        public override string ToString()
        {
            List<string> Flags = new();

            foreach(var Flag in Enum.GetValues(typeof(CheckFlags)).Cast<CheckFlags>())
            {
                if ((int)Flag == 0) continue; // early exit if no flag

                if (((CheckFlags)ChecksMade).HasFlag(Flag))
                {
                    Flags.Add(Flag.ToString());
                }
            }

            foreach (var Flag in Enum.GetValues(typeof(ProgressFlags)).Cast<ProgressFlags>())
            {
                if ((int)Flag == 0) continue; // early exit if no flag

                // don't add progress flag to string if HM flag is also present
                if (((ProgressFlags)Progress).HasFlag(Flag) && !((HMFlags)HMs).HasFlag((HMFlags)Flag))
                {
                    Flags.Add(Flag.ToString());
                }
            }

            foreach (var Flag in Enum.GetValues(typeof(HMFlags)).Cast<HMFlags>())
            {
                if ((int)Flag == 0) continue; // early exit if no flag

                if (((HMFlags)HMs).HasFlag(Flag))
                {
                    Flags.Add(Flag.ToString());
                }
            }

            Dictionary<string, string> LocalisedFlagStrings = GetLocalisedFlagStrings();

            // localisation
            for (int i=0;i<Flags.Count;i++)
            {
                if (LocalisedFlagStrings.ContainsKey(Flags[i])) Flags[i] = LocalisedFlagStrings[Flags[i]];
            }

            return string.Join(", ", Flags);
        }

    }
}
