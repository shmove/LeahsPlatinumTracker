using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeahsPlatinumTracker
{
    /// <summary>
    /// <see cref="Checks"/> class.                                                         <br />
    ///                                                                                     <br />
    /// Used for keeping track of various progress and area availability related flags.     <br />
    /// </summary>
    public class Checks
    {
        /// <summary>
        /// <see cref="FlagsTool"/> internal class, used for setting and unsetting flags in enums.      <br />
        ///                                                                                             <br />
        /// <see href="https://stackoverflow.com/a/3261485/13460028"/>                                  <br />
        /// </summary>
        internal static class FlagsTool
        {
            // The casts to object in the below code are an unfortunate necessity due to
            // C#'s restriction against a where T : Enum constraint. (There are ways around
            // this, but they're outside the scope of this simple illustration.)
            public static bool IsSet<T>(T flags, T flag) where T : struct
            {
                int flagsValue = (int)(object)flags;
                int flagValue = (int)(object)flag;

                return (flagsValue & flagValue) != 0;
            }

            public static void Set<T>(ref T flags, T flag) where T : struct
            {
                int flagsValue = (int)(object)flags;
                int flagValue = (int)(object)flag;

                flags = (T)(object)(flagsValue | flagValue);
            }

            public static void Unset<T>(ref T flags, T flag) where T : struct
            {
                int flagsValue = (int)(object)flags;
                int flagValue = (int)(object)flag;

                flags = (T)(object)(flagsValue & (~flagValue));
            }
        }

        // Check Enums

        /// <summary>
        /// Check flags. Contains checks that aren't specific in nature or inherently related to progress, but still important for tracking area availability.
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

        /// <summary>
        /// This instance's associated nonspecific checks.
        /// </summary>
        public CheckFlags ChecksMade { get; set; }

        /// <summary>
        /// This instance's associated progress related checks.
        /// </summary>
        public ProgressFlags Progress { get; set; }

        /// <summary>
        /// This instance's associated HM related checks.
        /// </summary>
        public HMFlags HMs { get; set; }

        // Constructors

        /// <summary>
        /// Empty constructor. Creates a set of checks with nothing set.
        /// </summary>
        public Checks() { }

        // int to enum explicit conversion, used for HM requirements
        /// <summary>
        /// Integer constructor. Used for HM requirements, explicitly converting int to enum and allowing multiple HM requirements along with their associated required gyms.   <br />
        /// <em>e.g; a given value of 65 requires Rock Climb (64), Icicle Badge (64), Rock Smash (1) and Coal Badge (1).</em>                                                   <br />
        /// </summary>
        /// <param name="progressFlags">The given integer to be converted to enum.</param>
        public Checks(int progressFlags)
        {
            HMs = (HMFlags)progressFlags;
            Progress = (ProgressFlags)progressFlags;
        }

        /// <summary>
        /// ProgressFlags constructor. Takes an instance of the ProgressFlags enum and creates a Checks instance with those flags set.
        /// </summary>
        /// <param name="progressFlags">The given instance of the ProgressFlags enum.</param>
        public Checks(ProgressFlags progressFlags)
        {
            Progress = progressFlags;
        }

        /// <summary>
        /// CheckFlags constructor. Takes an instance of the CheckFlags enum and creates a Checks instance with those flags set.
        /// </summary>
        /// <param name="checkFlags">The given instance of the CheckFlags enum.</param>
        public Checks(CheckFlags checkFlags)
        {
            ChecksMade = checkFlags;
        }

        // Functions

        /// <summary>
        /// Sets a flag in this instance's CheckFlags.
        /// </summary>
        /// <param name="flag">The flag to be set.</param>
        public void Unlock(CheckFlags flag)
        {
            CheckFlags _checks = ChecksMade;
            FlagsTool.Set(ref _checks, flag);
            ChecksMade = _checks;
        }

        /// <summary>
        /// Unsets a flag in this instance's CheckFlags.
        /// </summary>
        /// <param name="flag">The flag to be unset.</param>
        public void Lock(CheckFlags flag)
        {
            CheckFlags _checks = ChecksMade;
            FlagsTool.Unset(ref _checks, flag);
            ChecksMade = _checks;
        }

        /// <summary>
        /// Toggles a flag in this instance's CheckFlags.
        /// </summary>
        /// <param name="flag">The flag to be toggled.</param>
        /// <returns>A boolean pertaining to the state of the flag after toggling.</returns>
        public bool Toggle(CheckFlags flag)
        {
            if (!FlagsTool.IsSet(ChecksMade, flag))
            {
                Unlock(flag);
                return true;
            }
            else
            {
                Lock(flag);
                return false;
            }
        }

        /// <summary>
        /// Sets a flag in this instance's ProgressFlags.
        /// </summary>
        /// <param name="flag">The flag to be set.</param>
        public void Unlock(ProgressFlags flag)
        {
            ProgressFlags _progress = Progress;
            FlagsTool.Set(ref _progress, flag);
            Progress = _progress;
        }

        /// <summary>
        /// Unsets a flag in this instance's ProgressFlags.
        /// </summary>
        /// <param name="flag">The flag to be unset.</param>
        public void Lock(ProgressFlags flag)
        {
            ProgressFlags _progress = Progress;
            FlagsTool.Unset(ref _progress, flag);
            Progress = _progress;
        }

        /// <summary>
        /// Toggles a flag in this instance's ProgressFlags.
        /// </summary>
        /// <param name="flag">The flag to be toggled.</param>
        /// <returns>A boolean pertaining to the state of the flag after toggling.</returns>
        public bool Toggle(ProgressFlags flag)
        {
            if (!FlagsTool.IsSet(Progress, flag))
            {
                Unlock(flag);
                return true;
            }
            else
            {
                Lock(flag);
                return false;
            }
        }

        /// <summary>
        /// Sets a flag in this instance's HMFlags.
        /// </summary>
        /// <param name="flag">The flag to be set.</param>
        public void Unlock(HMFlags flag)
        {
            HMFlags _hmflags = HMs;
            FlagsTool.Set(ref _hmflags, flag);
            HMs = _hmflags;
        }

        /// <summary>
        /// Unsets a flag in this instance's HMFlags.
        /// </summary>
        /// <param name="flag">The flag to be unset.</param>
        public void Lock(HMFlags flag)
        {
            HMFlags _hmflags = HMs;
            FlagsTool.Unset(ref _hmflags, flag);
            HMs = _hmflags;
        }

        /// <summary>
        /// Toggles a flag in this instance's HMFlags.
        /// </summary>
        /// <param name="flag">The flag to be toggled.</param>
        /// <returns>A boolean pertaining to the state of the flag after toggling.</returns>
        public bool Toggle(HMFlags flag)
        {
            if (!FlagsTool.IsSet(HMs, flag))
            {
                Unlock(flag);
                return true;
            }
            else
            {
                Lock(flag);
                return false;
            }
        }

        /// <summary>
        /// Evaluates if, for every flag that is set in this instance, the given instance has the flag set too.
        /// </summary>
        /// <param name="currentChecks">The Checks instance to compare to this instance.</param>
        /// <returns>A boolean pertaining to whether the given Checks instance meets the flags set in this instance.</returns>
        public bool meetsRequirements(Checks currentChecks)
        {
            bool MeetsRequiredFlags<T>(T requirementChecks, T currentChecks, T flag) where T : struct
            {
                int reqValue = (int)(object)requirementChecks;
                int curValue = (int)(object)currentChecks;
                int flagValue = (int)(object)flag;

                if (!((reqValue & flagValue) != 0)) return true; // return true if flag is not required
                return (curValue & flagValue) != 0; // return if flag is met
            }

            // Go through all checks and return false if a single one doesn't meet requirements
            // (this really sucks, need to improve it)
            if (!MeetsRequiredFlags(ChecksMade, currentChecks.ChecksMade, CheckFlags.HasWorksKey))              return false;
            if (!MeetsRequiredFlags(ChecksMade, currentChecks.ChecksMade, CheckFlags.HasGalacticKey))           return false;
            if (!MeetsRequiredFlags(ChecksMade, currentChecks.ChecksMade, CheckFlags.HasTeleport))              return false;
            if (!MeetsRequiredFlags(ChecksMade, currentChecks.ChecksMade, CheckFlags.HasBike))                  return false;
            if (!MeetsRequiredFlags(ChecksMade, currentChecks.ChecksMade, CheckFlags.HasSpokenRoark))           return false;
            if (!MeetsRequiredFlags(ChecksMade, currentChecks.ChecksMade, CheckFlags.HasSpokenFantina))         return false;
            if (!MeetsRequiredFlags(ChecksMade, currentChecks.ChecksMade, CheckFlags.HasSpokenVolkner))         return false;
            if (!MeetsRequiredFlags(ChecksMade, currentChecks.ChecksMade, CheckFlags.HasDefeatedWindworks))     return false;
            if (!MeetsRequiredFlags(ChecksMade, currentChecks.ChecksMade, CheckFlags.HasSecretPotion))          return false;
            if (!MeetsRequiredFlags(Progress, currentChecks.Progress, ProgressFlags.HasCoalBadge))              return false;
            if (!MeetsRequiredFlags(Progress, currentChecks.Progress, ProgressFlags.HasForestBadge))            return false;
            if (!MeetsRequiredFlags(Progress, currentChecks.Progress, ProgressFlags.HasRelicBadge))             return false;
            if (!MeetsRequiredFlags(Progress, currentChecks.Progress, ProgressFlags.HasCobbleBadge))            return false;
            if (!MeetsRequiredFlags(Progress, currentChecks.Progress, ProgressFlags.HasFenBadge))               return false;
            if (!MeetsRequiredFlags(Progress, currentChecks.Progress, ProgressFlags.HasMineBadge))              return false;
            if (!MeetsRequiredFlags(Progress, currentChecks.Progress, ProgressFlags.HasIcicleBadge))            return false;
            if (!MeetsRequiredFlags(Progress, currentChecks.Progress, ProgressFlags.HasBeaconBadge))            return false;
            if (!MeetsRequiredFlags(Progress, currentChecks.Progress, ProgressFlags.HasAaron))                  return false;
            if (!MeetsRequiredFlags(Progress, currentChecks.Progress, ProgressFlags.HasBertha))                 return false;
            if (!MeetsRequiredFlags(Progress, currentChecks.Progress, ProgressFlags.HasFlint))                  return false;
            if (!MeetsRequiredFlags(Progress, currentChecks.Progress, ProgressFlags.HasLucian))                 return false;
            if (!MeetsRequiredFlags(Progress, currentChecks.Progress, ProgressFlags.HasCynthia))                return false;
            if (!MeetsRequiredFlags(HMs, currentChecks.HMs, HMFlags.HM01))                                      return false;
            if (!MeetsRequiredFlags(HMs, currentChecks.HMs, HMFlags.HM02))                                      return false;
            if (!MeetsRequiredFlags(HMs, currentChecks.HMs, HMFlags.HM03))                                      return false;
            if (!MeetsRequiredFlags(HMs, currentChecks.HMs, HMFlags.HM04))                                      return false;
            if (!MeetsRequiredFlags(HMs, currentChecks.HMs, HMFlags.HM05))                                      return false;
            if (!MeetsRequiredFlags(HMs, currentChecks.HMs, HMFlags.HM06))                                      return false;
            if (!MeetsRequiredFlags(HMs, currentChecks.HMs, HMFlags.HM07))                                      return false;
            if (!MeetsRequiredFlags(HMs, currentChecks.HMs, HMFlags.HM08))                                      return false;
            return true; // meets all requirements!
        }

        /// <summary>
        /// Returns readable stringified versions of the flags set in this instance.
        /// </summary>
        public List<string> ToList()
        {
            List<string> Flags = new();

            // CheckFlags
            foreach(var Flag in Enum.GetValues(typeof(CheckFlags)).Cast<CheckFlags>())
            {
                if (ChecksMade.HasFlag(Flag))
                {
                    string? flagDesc = null;
                    switch (Flag.ToString())
                    {
                        case "HasWorksKey":
                            flagDesc = "Obtain the Works Key";
                            break;
                        case "HasGalacticKey":
                            flagDesc = "Obtain the Galactic Key";
                            break;
                        case "HasTeleport":
                            flagDesc = "Obtain a Pokémon with Teleport";
                            break;
                        case "HasBike":
                            flagDesc = "Obtain a Bike";
                            break;
                        case "HasSpokenRoark":
                            flagDesc = "Speak to Roark in Oreburgh Mine";
                            break;
                        case "HasSpokenFantina":
                            flagDesc = "Speak to Fantina in the Super Contest Hall";
                            break;
                        case "HasSpokenVolkner":
                            flagDesc = "Speak to Volkner in the Vista Lighthouse";
                            break;
                        case "HasDefeatedWindworks":
                            flagDesc = "Defeat Mars in Valley Windworks";
                            break;
                        case "HasSecretPotion":
                            flagDesc = "Obtain the SecretPotion from Cynthia";
                            break;
                    }
                    if (flagDesc != null) Flags.Add(flagDesc);
                }
            }

            // HMs
            foreach (var Flag in Enum.GetValues(typeof(HMFlags)).Cast<HMFlags>())
            {
                if (HMs.HasFlag(Flag))
                {
                    string? flagDesc = null;
                    switch (Flag.ToString())
                    {
                        case "HM01":
                            flagDesc = "Obtain HM01 (Cut)";
                            break;
                        case "HM02":
                            flagDesc = "Obtain HM02 (Fly)";
                            break;
                        case "HM03":
                            flagDesc = "Obtain HM03 (Surf)";
                            break;
                        case "HM04":
                            flagDesc = "Obtain HM04 (Strength)";
                            break;
                        case "HM05":
                            flagDesc = "Obtain HM05 (Defog)";
                            break;
                        case "HM06":
                            flagDesc = "Obtain HM06 (Rock Smash)";
                            break;
                        case "HM07":
                            flagDesc = "Obtain HM07 (Waterfall)";
                            break;
                        case "HM08":
                            flagDesc = "Obtain HM08 (Rock Climb)";
                            break;
                    }
                    if (flagDesc != null) Flags.Add(flagDesc);
                }
            }

            // Progress
            foreach (var Flag in Enum.GetValues(typeof(ProgressFlags)).Cast<ProgressFlags>())
            {
                if (Progress.HasFlag(Flag))
                {
                    string? flagDesc = null;
                    switch (Flag.ToString())
                    {
                        case "HasCoalBadge":
                            if (!HMs.HasFlag((HMFlags)(int)Flag)) flagDesc = "Obtain the Coal Badge";
                            break;
                        case "HasForestBadge":
                            if (!HMs.HasFlag((HMFlags)(int)Flag)) flagDesc = "Obtain the Forest Badge";
                            break;
                        case "HasRelicBadge":
                            if (!HMs.HasFlag((HMFlags)(int)Flag)) flagDesc = "Obtain the Relic Badge";
                            break;
                        case "HasCobbleBadge":
                            if (!HMs.HasFlag((HMFlags)(int)Flag)) flagDesc = "Obtain the Cobble Badge";
                            break;
                        case "HasFenBadge":
                            if (!HMs.HasFlag((HMFlags)(int)Flag)) flagDesc = "Obtain the Fen Badge";
                            break;
                        case "HasMineBadge":
                            if (!HMs.HasFlag((HMFlags)(int)Flag)) flagDesc = "Obtain the Mine Badge";
                            break;
                        case "HasIcicleBadge":
                            if (!HMs.HasFlag((HMFlags)(int)Flag)) flagDesc = "Obtain the Icicle Badge";
                            break;
                        case "HasBeaconBadge":
                            if (!HMs.HasFlag((HMFlags)(int)Flag)) flagDesc = "Obtain the Beacon Badge";
                            break;
                        case "HasAaron":
                            flagDesc = "Defeat Elite Four Aaron";
                            break;
                        case "HasBertha":
                            flagDesc = "Defeat Elite Four Bertha";
                            break;
                        case "HasFlint":
                            flagDesc = "Defeat Elite Four Flint";
                            break;
                        case "HasLucian":
                            flagDesc = "Defeat Elite Four Lucian";
                            break;
                        case "HasCynthia":
                            flagDesc = "Become Champion";
                            break;
                    }
                    if (flagDesc != null) Flags.Add(flagDesc);
                }
            }

            return Flags;
        }

    }
}
