using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeahsPlatinumTracker
{
    internal class VisualMapSector
    {
        public string VisualMapID { get; set; }
        public List<MapSector> MapSectors { get; set; }
        public bool IsUnlocked
        {
            get
            {
                foreach (MapSector sector in MapSectors)
                {
                    if (sector.IsUnlocked) return true;
                }
                return false;
            }
        }
        public bool IsCompleted
        {
            get
            {
                foreach(MapSector sector in MapSectors)
                {
                    // if the area is unlocked and a warp is unchecked, the area is not finished
                    if (sector.IsUnlocked)
                    {
                        foreach(Warp warp in sector.Warps)
                        {
                            if (warp.WarpID < 0) return false; 
                        }
                    }
                }
                return true;
            }
        }

        public VisualMapSector(string _VisualMapID, List<MapSector>_MapSectors)
        {
            VisualMapID = _VisualMapID;
            MapSectors = _MapSectors;
        }

        public VisualMapSector(MapSector _MapSector)
        {
            VisualMapID = _MapSector.MapID;
            MapSectors = new List<MapSector>();
            MapSectors.Add(_MapSector);
        }
    }

    internal class MapSector
    {
        public string MapID { get; set; }
        public List<Warp> Warps { get; set; }
        public List<Condition> Conditions { get; set; }
        public bool IsUnlocked { get; set; }
        public bool DefaultUnlocked { get; set; }

        // Constructors
        public MapSector(string _MapID, int numberOfWarps, string AccessMap)
        {
            MapID = _MapID;
            Conditions = new List<Condition> { new Condition(AccessMap) };
            Warps = new List<Warp>();
            for (int i = 0; i < numberOfWarps; i++)
            {
                Warps.Add(new Warp(MapID, i));
            }
        }
        public MapSector(string _MapID, int numberOfWarps, Condition _Condition)
        {
            MapID = _MapID;
            Conditions = new List<Condition> { _Condition };
            Warps = new List<Warp>();
            for (int i = 0; i < numberOfWarps; i++)
            {
                Warps.Add(new Warp(MapID, i));
            }
        }

        public MapSector(string _MapID, int numberOfWarps, List<Condition> _Conditions)
        {
            MapID = _MapID;
            Conditions = _Conditions;
            Warps = new List<Warp>();
            for (int i = 0; i < numberOfWarps; i++)
            {
                Warps.Add(new Warp(MapID, i));
            }
        }

        public MapSector(string _MapID, int numberOfWarps, bool defaultUnlocked)
        {
            MapID = _MapID;
            Conditions = new List<Condition>();
            Warps = new List<Warp>();
            for (int i = 0; i < numberOfWarps; i++)
            {
                Warps.Add(new Warp(MapID, i));
            }

            if (defaultUnlocked)
            {
                IsUnlocked = true;
                DefaultUnlocked = true;
            }
        }

        public MapSector(string _MapID, int numberOfWarps)
        {
            MapID = _MapID;
            Conditions = new List<Condition>();
            Warps = new List<Warp>();
            for (int i = 0; i < numberOfWarps; i++)
            {
                Warps.Add(new Warp(MapID, i));
            }
        }

        // Functions
        public bool Link(int WarpID, (string MapID, int WarpID) Destination)
        {
            foreach(Warp warp in Warps)
            {
                if (warp.WarpID == WarpID)
                {
                    warp.Set(Destination.MapID, Destination.WarpID);
                    IsUnlocked = true;
                    return true;
                }
            }
            return false;
        }

        // Unlinks a warp of specified ID and returns if this MapSector is still linked by any warp
        public bool Unlink(int WarpID)
        {
            foreach(Warp warp in Warps)
            {
                if (warp.WarpID == WarpID)
                {
                    warp.Clear();
                    return IsLinked();
                }
            }
            return true;
        }

        // checks if this MapSector is linked at all via warps
        public bool IsLinked()
        {
            foreach (Warp warp in Warps)
            {
                if (warp.Destination.WarpID >= 0) return true;
            }
            return DefaultUnlocked;
        }

        // checks if a specific warp is linked
        public bool IsLinked(int WarpID)
        {
            foreach (Warp warp in Warps)
            {
                if (warp.WarpID == WarpID)
                {
                    return warp.Destination.WarpID >= 0;
                }
            }
            return false;
        }

        public bool IsMapAccessible()
        {
            foreach (Condition condition in Conditions)
            {
                if (condition.MapAccessible) return true;
            }
            return DefaultUnlocked;
        }

        public Warp GetWarp(int WarpID)
        {
            foreach (Warp warp in Warps)
            {
                if (warp.WarpID == WarpID)
                {
                    return warp;
                }
            }
            return null;
        }

        public bool AttemptUnlock(Checks currentChecks)
        {
            if (IsUnlocked) return false;
            foreach(Condition Condition in Conditions)
            {
                if (!Condition.MapAccessible) continue;
                if (Condition.RequiredChecks.meetsRequirements(currentChecks)) { IsUnlocked = true; break; };
            }
            
            return IsUnlocked;
        }

        public bool AttemptLock(Checks currentChecks)
        {
            if (!IsUnlocked || IsLinked()) return false; // if the map is linked via warp, assume it should remain unlocked

            bool stillUnlocked = false; // keeps track of whether ANY condition flags as remaining unlocked
            foreach(Condition Condition in Conditions)
            {
                if (Condition.RequiredChecks.meetsRequirements(currentChecks)) { stillUnlocked = true; break; };
            }

            if (!stillUnlocked)
            {
                IsUnlocked = false;
                return true;
            }
            
            return false;
        }

        public bool AttemptUnlock(string UnlockedMap, Checks currentChecks)
        {
            if (IsUnlocked) return false;
            foreach(Condition Condition in Conditions)
            {
                if (Condition.AccessMap == UnlockedMap)
                {
                    Condition.MapAccessible = true;
                }
                else continue;

                if (Condition.RequiredChecks.meetsRequirements(currentChecks)) { IsUnlocked = true; break; };
            }

            return IsUnlocked;
        }

        public bool AttemptLock(string LockedMap, Checks currentChecks)
        {
            if (!IsUnlocked || IsLinked()) return false; // if the map is linked via warp, assume it should remain unlocked

            bool stillUnlocked = false; // keeps track of whether ANY condition flags as remaining unlocked
            foreach (Condition Condition in Conditions)
            {
                if (Condition.AccessMap == LockedMap)
                {
                    Condition.MapAccessible = false;
                }

                // if condition still meets checks and is still map accessible then this map remains unlocked
                if (Condition.RequiredChecks.meetsRequirements(currentChecks) && Condition.MapAccessible) { stillUnlocked = true; break; }
            }

            if (!stillUnlocked)
            {
                IsUnlocked = false;
                return true;
            }

            return false;
        }

        public bool CanAccess(string MapName) 
        {
            foreach (Condition Condition in Conditions)
            {
                if (Condition.AccessMap == MapName) return true;
            }
            return false;
        }

    }

    internal class Condition
    {
        public string AccessMap { get; set; }
        public Checks RequiredChecks { get; set; }
        public bool MapAccessible { get; set; }

        public Condition()
        {
            AccessMap = "";
            RequiredChecks = new Checks();
            MapAccessible = false;
        }
        public Condition(string _AccessMap)
        {
            AccessMap = _AccessMap;
            RequiredChecks = new Checks();
            MapAccessible = false;
        }

        public Condition(Checks _RequiredChecks)
        {
            AccessMap = "";
            RequiredChecks = _RequiredChecks;
            MapAccessible = true;
        }

        public Condition(string _AccessMap, Checks _RequiredChecks)
        {
            AccessMap = _AccessMap;
            RequiredChecks = _RequiredChecks;
            MapAccessible = false;
        }

        public Condition(string _AccessMap, Checks _RequiredChecks, bool _MapAccessible)
        {
            AccessMap = _AccessMap;
            RequiredChecks = _RequiredChecks;
            MapAccessible = _MapAccessible;
        }
    }

    internal class Warp
    {
        public string MapID { get; set; }
        public int WarpID { get; set; }
        public (string MapID, int WarpID) Destination { get; set; }

        public Warp(string _MapID, int _WarpID)
        {
            MapID = _MapID;
            WarpID = _WarpID;
            Destination = ("Not set", -1);
        }

        public void Set(string _MapID, int _WarpID)
        {
            Destination = (_MapID, _WarpID);
        }

        public void Clear()
        {
            Destination = ("Not set", -1);
        }
    }
}
