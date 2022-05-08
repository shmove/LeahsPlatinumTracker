using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeahsPlatinumTracker
{
    public class Checks
    {

        // https://stackoverflow.com/a/3261485/13460028
        private static class FlagsTool
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

        public CheckFlags ChecksMade { get; set; }
        public ProgressFlags Progress { get; set; }
        public HMFlags HMs { get; set; }

        // Constructors
        public Checks() { }

        // int to enum explicit conversion, used for HM requirements
        public Checks(int progressFlags)
        {
            HMs = (HMFlags)progressFlags;
            Progress = (ProgressFlags)progressFlags;
        }

        public Checks(ProgressFlags progressFlags)
        {
            Progress = (ProgressFlags)progressFlags;
        }

        public Checks(CheckFlags checkFlags)
        {
            ChecksMade = checkFlags;
        }

        // Functions

        public void Unlock(CheckFlags flag)
        {
            CheckFlags _checks = ChecksMade;
            FlagsTool.Set(ref _checks, flag);
            ChecksMade = _checks;
        }

        public void Lock(CheckFlags flag)
        {
            CheckFlags _checks = ChecksMade;
            FlagsTool.Unset(ref _checks, flag);
            ChecksMade = _checks;
        }

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

        public void Unlock(ProgressFlags flag)
        {
            ProgressFlags _progress = Progress;
            FlagsTool.Set(ref _progress, flag);
            Progress = _progress;
        }

        public void Lock(ProgressFlags flag)
        {
            ProgressFlags _progress = Progress;
            FlagsTool.Unset(ref _progress, flag);
            Progress = _progress;
        }

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

        public void Unlock(HMFlags flag)
        {
            HMFlags _hmflags = HMs;
            FlagsTool.Set(ref _hmflags, flag);
            HMs = _hmflags;
        }

        public void Lock(HMFlags flag)
        {
            HMFlags _hmflags = HMs;
            FlagsTool.Unset(ref _hmflags, flag);
            HMs = _hmflags;
        }

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

        public string CheckString()
        {
            string output = "";
            if (FlagsTool.IsSet(ChecksMade, CheckFlags.HasWorksKey)) output += "WorksKey ";
            if (FlagsTool.IsSet(ChecksMade, CheckFlags.HasGalacticKey)) output += "GalacticKey ";
            if (FlagsTool.IsSet(ChecksMade, CheckFlags.HasTeleport)) output += "Teleport ";
            if (FlagsTool.IsSet(ChecksMade, CheckFlags.HasBike)) output += "Bike ";
            if (FlagsTool.IsSet(Progress, ProgressFlags.HasCoalBadge)) output += "Badge1 ";
            if (FlagsTool.IsSet(Progress, ProgressFlags.HasForestBadge)) output += "Badge2 ";
            if (FlagsTool.IsSet(Progress, ProgressFlags.HasRelicBadge)) output += "Badge3 ";
            if (FlagsTool.IsSet(Progress, ProgressFlags.HasCobbleBadge)) output += "Badge4 ";
            if (FlagsTool.IsSet(Progress, ProgressFlags.HasFenBadge)) output += "Badge5 ";
            if (FlagsTool.IsSet(Progress, ProgressFlags.HasMineBadge)) output += "Badge6 ";
            if (FlagsTool.IsSet(Progress, ProgressFlags.HasIcicleBadge)) output += "Badge7 ";
            if (FlagsTool.IsSet(Progress, ProgressFlags.HasBeaconBadge)) output += "Badge8 ";
            if (FlagsTool.IsSet(Progress, ProgressFlags.HasAaron)) output += "Aaron ";
            if (FlagsTool.IsSet(Progress, ProgressFlags.HasBertha)) output += "Bertha ";
            if (FlagsTool.IsSet(Progress, ProgressFlags.HasFlint)) output += "Flint ";
            if (FlagsTool.IsSet(Progress, ProgressFlags.HasLucian)) output += "Lucian ";
            if (FlagsTool.IsSet(Progress, ProgressFlags.HasCynthia)) output += "Cynthia ";
            if (FlagsTool.IsSet(HMs, HMFlags.HM01)) output += "Cut ";
            if (FlagsTool.IsSet(HMs, HMFlags.HM02)) output += "Fly ";
            if (FlagsTool.IsSet(HMs, HMFlags.HM03)) output += "Surf ";
            if (FlagsTool.IsSet(HMs, HMFlags.HM04)) output += "Strength ";
            if (FlagsTool.IsSet(HMs, HMFlags.HM05)) output += "Defog ";
            if (FlagsTool.IsSet(HMs, HMFlags.HM06)) output += "Rock Smash ";
            if (FlagsTool.IsSet(HMs, HMFlags.HM07)) output += "Waterfall ";
            if (FlagsTool.IsSet(HMs, HMFlags.HM08)) output += "Rock Climb ";
            if (output == "") output = "None";
            return output;
        }

    }
}
