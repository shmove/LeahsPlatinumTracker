using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeahsPlatinumTracker
{
    /// <summary>
    /// <see cref="VisualMapSector"/> class.                                            <br />
    ///                                                                                 <br />
    /// Used to represent a group of <see cref="MapSector"/> instances within the UI.   <br />
    /// </summary>
    public class VisualMapSector
    {
        /// <summary>
        /// The unique identifier for this instance.
        /// </summary>
        public string VisualMapID { get; set; }

        /// <summary>
        /// The <see cref="MapSector"/> instances that this instance consists of.
        /// </summary>
        public List<MapSector> MapSectors { get; set; }

        /// <summary>
        /// A boolean pertaining to whether any of this instance's <see cref="MapSector"/>s are unlocked.
        /// </summary>
        [JsonIgnore]
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

        /// <summary>
        /// A boolean pertaining to whether every accessible <see cref="Warp"/> in this instance has been checked.
        /// </summary>
        [JsonIgnore]
        public bool IsCompleted { get { return CheckCompletion(); } }

        /// <summary>
        /// A boolean pertaining to whether every <see cref="Warp"/> (accessible or not) in this instance has been checked.
        /// </summary>
        [JsonIgnore]
        public bool IsFullyCompleted { get { return CheckCompletion(true); } }

        /// <summary>
        /// Evaluates whether every <see cref="Warp"/> in this instance has been checked. If evaluated with <paramref name="strict"/>, it ignores whether the unchecked areas are accessible or not.
        /// </summary>
        /// <param name="strict">Boolean ascertaining whether inaccessible areas count as unchecked.</param>
        /// <returns>A boolean pertaining to whether every <see cref="Warp"/> in this instance has been checked.</returns>
        private bool CheckCompletion(bool strict = false)
        {
            foreach (MapSector sector in MapSectors)
            {
                // if the area is unlocked and a warp is unchecked, the area is not finished
                if (sector.IsUnlocked || strict)
                {
                    foreach (Warp warp in sector.Warps)
                    {
                        if ( !warp.HasDestination && !warp.VisualMarkerChecked) return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Evaluates whether every accessible <see cref="Warp"/> in this instance has been assigned a destination or marker.   <br />
        ///                                                                                                                     <br />
        /// Can be used to determine if an instance is fully checked and is only incomplete because of player defined markers.  <br />
        /// </summary>
        [JsonIgnore]
        public bool IsFullyChecked
        { 
            get 
            {
                foreach(MapSector sector in MapSectors)
                {
                    if (sector.IsUnlocked)
                    {
                        foreach (Warp warp in sector.Warps)
                        {
                            if (!(warp.VisualMarkers > 0 || warp.HasDestination)) return false;
                        }
                    }
                }
                return true;
            } 
        }

        /// <summary>
        /// The <see cref="LeahsPlatinumTracker.Tracker"/> that this instance belongs to.
        /// </summary>
        [JsonIgnore]
        public Tracker? Tracker { get; }

        /// <summary>
        /// A string used to represent this instance in UI elements.
        /// </summary>
        [JsonIgnore]
        public string DisplayName { get; set; }

        // Constructors

        /// <summary>
        /// <see cref="VisualMapSector"/> constructor for an instance with multiple child <see cref="MapSector"/> instances.
        /// </summary>
        /// <param name="_Tracker">The parent <see cref="LeahsPlatinumTracker.Tracker"/> for this instance.</param>
        /// <param name="_VisualMapID">The unique identifier for this instance.</param>
        /// <param name="_MapSectors">A list of <see cref="MapSector"/> instances to be set as a child of this instance.</param>
        /// <param name="_DisplayName">The string used to represent this instance in UI elements. Defaults to the same as <paramref name="_VisualMapID"/>.</param>
        public VisualMapSector(Tracker _Tracker, string _VisualMapID, List<MapSector>_MapSectors, string _DisplayName = "")
        {
            Tracker = _Tracker;
            VisualMapID = _VisualMapID;
            MapSectors = new List<MapSector>();
            foreach (MapSector sector in _MapSectors)
            {
                sector.ParentVisualMapSector = this;
                MapSectors.Add(sector);
            }
            if (_DisplayName != "") DisplayName = _DisplayName;
            else DisplayName = VisualMapID;
        }

        /// <summary>
        /// <see cref="VisualMapSector"/> constructor for an instance with a single child <see cref="MapSector"/>.
        /// </summary>
        /// <param name="_Tracker">The parent <see cref="LeahsPlatinumTracker.Tracker"/> for this instance.</param>
        /// <param name="_MapSector">The <see cref="MapSector"/> that belongs to this instance.</param>
        /// <param name="_DisplayName">The string used to represent this instance in UI elements. Defaults to the same as <paramref name="_MapSector"/>.MapID.</param>
        public VisualMapSector(Tracker _Tracker, MapSector _MapSector, string _DisplayName = "")
        {
            Tracker = _Tracker;
            VisualMapID = _MapSector.MapID;
            MapSectors = new List<MapSector>();
            _MapSector.ParentVisualMapSector = this;
            MapSectors.Add(_MapSector);
            if (_DisplayName != "") DisplayName = _DisplayName;
            else DisplayName = VisualMapID;
        }

    }

    /// <summary>
    /// <see cref="MapSector"/> class.                                                                                                                                                                                          <br />
    ///                                                                                                                                                                                                                         <br />
    /// Used to represent a group of <see cref="Warp"/>s in an area within the <see cref="LeahsPlatinumTracker.Tracker"/>, keep track of the <see cref="Condition"/>s required to reach this area and know whether it is possible to do so.   <br />
    /// </summary>
    public class MapSector
    {
        /// <summary>
        /// The unique identifier for this instance.
        /// </summary>
        public string MapID { get; set; }

        /// <summary>
        /// The <see cref="Warp"/>s that this instance consists of.
        /// </summary>
        public List<Warp> Warps { get; set; }

        /// <summary>
        /// The checks and/or maps required for this MapSector to become unlocked.
        /// </summary>
        public List<Condition> Conditions { get; set; }

        /// <summary>
        /// The boolean representing if this instance is either accessible and has any condition fully met, or is linked by one of its warps.
        /// </summary>
        public bool IsUnlocked { get; set; }

        /// <summary>
        /// The boolean representing if this instance is unlocked by default.
        /// </summary>
        [JsonIgnore]
        public bool DefaultUnlocked { get; set; }

        /// <summary>
        /// The <see cref="VisualMapSector"/> that this instance belongs to. This <b>needs</b> to be set after creation, as it is not set by any constructor.
        /// </summary>
        [JsonIgnore]
        public VisualMapSector? ParentVisualMapSector { get; set; } // needs to be initialised on creation

        /// <summary>
        /// The <see cref="LeahsPlatinumTracker.Tracker"/> that this instance belongs to.
        /// </summary>
        [JsonIgnore]
        public Tracker Tracker
        {
            get
            {
                return ParentVisualMapSector.Tracker;
            }
        }

        [JsonIgnore]
        public List<MapSector> AccessedMaps
        {
            get
            {
                List<MapSector> maps = new List<MapSector>();
                foreach(MapSector sector in Tracker.MapSectors)
                {
                    foreach(Condition condition in sector.Conditions)
                    {
                        if (condition.AccessMap == MapID) maps.Add(sector);
                    }
                }
                return maps;
            }
        }

        [JsonIgnore]
        public bool IsPseudoCorridor
        {
            get
            {
                int linkedWarps = 0;
                foreach(Warp warp in Warps)
                {
                    if (!warp.HasDestination && warp.VisualMarkers != 1) return false;
                    if (warp.HasDestination || (warp.VisualMarkerChecked && warp.VisualMarkers != 1)) linkedWarps++;
                    if (linkedWarps > 2) break;
                }

                if (linkedWarps == 2 && !CanAccess()) return true;
                else return false;
            }
        }

        [JsonIgnore]
        public (Warp Warp1, Warp Warp2) PseudoCorridorWarps
        {
            get
            {
                Warp warp1 = null;
                Warp warp2 = null;
                foreach(Warp warp in Warps)
                {
                    if (warp.HasDestination)
                    {
                        if (warp1 == null) warp1 = warp;
                        else { warp2 = warp; break; }
                    }
                }
                if (warp1 != null && warp2 != null) return (warp1, warp2);
                else throw new Exception("Tried to get pseudo-corridor warps of a non-pseudo-corridor.");
            }
        }

        // Constructors

        /// <summary>
        /// <see cref="MapSector"/> constructor for an area that only requires a map connection.
        /// </summary>
        /// <param name="_MapID">The unique identifier for this <see cref="MapSector"/>.</param>
        /// <param name="numberOfWarps">The number of <see cref="Warp"/> instances to create.</param>
        /// <param name="AccessMap">The <see cref="MapID"/> of another <see cref="MapSector"/> required for accessing this one.</param>
        public MapSector(string _MapID, int numberOfWarps, string AccessMap)
        {
            MapID = _MapID;
            Conditions = new List<Condition> { new Condition(AccessMap) };
            Warps = new List<Warp>();
            for (int i = 0; i < numberOfWarps; i++)
            {
                Warps.Add(new Warp(MapID, i, this));
            }
        }

        /// <summary>
        /// <see cref="MapSector"/> constructor for an area that requires <see cref="Checks"/> and/or a map connection to be unlocked.
        /// </summary>
        /// <param name="_MapID">The unique identifier for this <see cref="MapSector"/>.</param>
        /// <param name="numberOfWarps">The number of <see cref="Warp"/> instances to create.</param>
        /// <param name="_Condition">A <see cref="Condition"/> instance that represents the required <see cref="Checks"/> and/or map connection.</param>
        /// <param name="defaultUnlocked">A boolean representing if this <see cref="MapSector"/> should be unlocked by default. Defaults to <b>false</b>.</param>
        public MapSector(string _MapID, int numberOfWarps, Condition _Condition, bool defaultUnlocked = false)
        {
            MapID = _MapID;
            Conditions = new List<Condition> { _Condition };
            Warps = new List<Warp>();
            for (int i = 0; i < numberOfWarps; i++)
            {
                Warps.Add(new Warp(MapID, i, this));
            }

            if (defaultUnlocked)
            {
                IsUnlocked = true;
                DefaultUnlocked = true;
            }
        }

        /// <summary>
        /// <see cref="MapSector"/> constructor for an area that can require any of a given list of <see cref="Checks"/> and/or map connections to be unlocked.
        /// </summary>
        /// <param name="_MapID">The unique identifier for this <see cref="MapSector"/>.</param>
        /// <param name="numberOfWarps">The number of <see cref="Warp"/> instances to create.</param>
        /// <param name="_Conditions">A list of <see cref="Condition"/> instances representing the required <see cref="Checks"/> and or/map connections.</param>
        /// <param name="defaultUnlocked">A boolean representing if this <see cref="MapSector"/> should be unlocked by default. Defaults to <b>false</b>.</param>
        public MapSector(string _MapID, int numberOfWarps, List<Condition> _Conditions, bool defaultUnlocked = false)
        {
            MapID = _MapID;
            Conditions = _Conditions;
            Warps = new List<Warp>();
            for (int i = 0; i < numberOfWarps; i++)
            {
                Warps.Add(new Warp(MapID, i, this));
            }

            if (defaultUnlocked)
            {
                IsUnlocked = true;
                DefaultUnlocked = true;
            }
        }

        /// <summary>
        /// <see cref="MapSector"/> constructor for an area with no access conditions.
        /// </summary>
        /// <param name="_MapID">The unique identifier for this <see cref="MapSector"/>.</param>
        /// <param name="numberOfWarps">The number of <see cref="Warp"/> instances to create.</param>
        /// <param name="defaultUnlocked">A boolean representing if this <see cref="MapSector"/> should be unlocked by default. Defaults to <b>false</b>.</param>
        public MapSector(string _MapID, int numberOfWarps, bool defaultUnlocked = false)
        {
            MapID = _MapID;
            Conditions = new List<Condition>();
            Warps = new List<Warp>();
            for (int i = 0; i < numberOfWarps; i++)
            {
                Warps.Add(new Warp(MapID, i, this));
            }

            if (defaultUnlocked)
            {
                IsUnlocked = true;
                DefaultUnlocked = true;
            }
        }

        // Functions

        /// <summary>
        /// Sets the destination of one of this instance's <see cref="Warp"/>s matching a given <paramref name="WarpID"/>.
        /// </summary>
        /// <param name="WarpID">The unique identifier of the <see cref="Warp"/> to set the destination of.</param>
        /// <param name="Destination">A tuple representing the intended destination of this <see cref="Warp"/>.</param>
        /// <param name="preserveVisualMarkers">A boolean used to make it so that set VisualMarkers are preserved on warp destination overwrite.</param>
        /// <returns>A boolean pertaining to whether the destination of the matching <see cref="Warp"/> was successfully set.</returns>
        public bool Link(int WarpID, (string MapID, int WarpID) Destination, bool preserveVisualMarkers = false)
        {
            foreach(Warp warp in Warps)
            {
                if (warp.WarpID == WarpID)
                {
                    warp.Set(Destination.MapID, Destination.WarpID, preserveVisualMarkers);
                    IsUnlocked = true;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Unsets the destination of one of this instance's <see cref="Warp"/>s matching a given <paramref name="WarpID"/>.
        /// </summary>
        /// <param name="WarpID">The <paramref name="WarpID"/> of the <see cref="Warp"/> to reset the destination of.</param>
        /// <param name="preserveVisualMarkers">A boolean used to make it so that set VisualMarkers are preserved on warp destination overwrite.</param>
        /// <returns>A boolean pertaining to whether any of the <see cref="Warp"/>s in this instance has a set destination.</returns>
        public bool Unlink(int WarpID, bool preserveVisualMarkers = false)
        {
            foreach(Warp warp in Warps)
            {
                if (warp.WarpID == WarpID)
                {
                    warp.Clear(preserveVisualMarkers);
                    return IsLinked();
                }
            }
            return true;
        }

        /// <summary>
        /// Evaluates if any of this instance's <see cref="Warp"/>s has a set destination. Also returns true if this instance is unlocked by default.
        /// </summary>
        /// <returns>A boolean pertaining to whether any of this instance's <see cref="Warp"/>s has a set destination, or if this instance is unlocked by default.</returns>
        public bool IsLinked()
        {
            foreach (Warp warp in Warps)
            {
                if (warp.HasDestination) return true;
            }
            return DefaultUnlocked;
        }

        /// <summary>
        /// Evaluates if a <see cref="Warp"/> matching the given <paramref name="WarpID"/> has a set destination.
        /// </summary>
        /// <param name="WarpID">The <paramref name="WarpID"/> of the <see cref="Warp"/> to check if it has a set destination or not.</param>
        /// <returns>A boolean pertaining to if the <see cref="Warp"/> matching the given <paramref name="WarpID"/> has a set destination.</returns>
        public bool IsLinked(int WarpID)
        {
            foreach (Warp warp in Warps)
            {
                if (warp.WarpID == WarpID)
                {
                    return warp.HasDestination;
                }
            }
            return false;
        }

        /// <summary>
        /// Evaluates if this instance is accessible from a physical connection.
        /// </summary>
        /// <param name="currentChecks">The current state of the tracker's unlocked <see cref="Checks"/>.</param>
        /// <returns>A boolean pertaining to whether this instance is accessible from a physical connection, or whether it is unlocked by default.</returns>
        public bool IsAccessible(Checks currentChecks)
        {
            foreach (Condition condition in Conditions)
            {
                if (condition.MapAccessible && condition.RequiredChecks.meetsRequirements(currentChecks)) return true;
            }
            return DefaultUnlocked;
        }

        /// <summary>
        /// Returns a <see cref="Warp"/> from this instance matching a given <see cref="Warp.WarpID"/>.
        /// </summary>
        /// <param name="WarpID">Unique identifier of the <see cref="Warp"/> to return.</param>
        /// <returns><see cref="Warp"/> instance matching the given <see cref="Warp.WarpID"/>, or <b>null</b> if none was found.</returns>
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

        /// <summary>
        /// Attempts to unlock this instance given available <see cref="Checks"/>.
        /// </summary>
        /// <param name="currentChecks">The <see cref="Checks"/> instance to compare this instance's conditions to.</param>
        /// <returns>A boolean pertaining to whether this instance is unlocked.</returns>
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

        /// <summary>
        /// Evaluates if this instance should be re-locked given available <see cref="Checks"/>.
        /// </summary>
        /// <param name="currentChecks">The <see cref="Checks"/> instance to compare this instance's conditions to.</param>
        /// <returns>A boolean pertaining to whether this instance has changed state to become locked.</returns>
        public bool AttemptLock(Checks currentChecks)
        {
            bool wasUnlocked = IsUnlocked; // if map was initially unlocked
            bool stillUnlocked = false; // keeps track of whether ANY condition flags as remaining unlocked
            bool unlockOverride = DefaultUnlocked || IsLinked(); // special cases where the map will remain unlocked despite no conditions matching

            foreach (Condition Condition in Conditions)
            {
                if (!unlockOverride && Condition.RequiredChecks.meetsRequirements(currentChecks) && Condition.MapAccessible) stillUnlocked = true;
            }

            if (wasUnlocked && !stillUnlocked && !unlockOverride)
            {
                IsUnlocked = false;
                return true;
            }
            
            return false;
        }

        /// <summary>
        /// Attempts to unlock this instance given the <see cref="MapID"/> of a newly locked <see cref="MapSector"/> and current available checks.
        /// </summary>
        /// <param name="UnlockedMap">The <see cref="MapID"/> of the <see cref="MapSector"/> that has become unlocked.</param>
        /// <param name="currentChecks">The <see cref="Checks"/> instance to compare this instance's conditions to.</param>
        /// <returns>A boolean pertaining to whether this instance is unlocked.</returns>
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

        /// <summary>
        /// Evaluates if this instance should be re-locked given the <see cref="MapID"/> of a newly locked <see cref="MapSector"/> and current available <see cref="Checks"/>.
        /// </summary>
        /// <param name="LockedMap">The <see cref="MapID"/> of the <see cref="MapSector"/> that has become locked.</param>
        /// <param name="currentChecks">The <see cref="Checks"/> instance to compare this instance's conditions to.</param>
        /// <returns>A boolean pertaining to whether this instance has changed state to become locked.</returns>
        public bool AttemptLock(string LockedMap, Checks currentChecks)
        {
            bool wasUnlocked = IsUnlocked; // if map was initially unlocked
            bool stillUnlocked = false; // keeps track of whether ANY condition flags stay unlocked
            bool unlockOverride = DefaultUnlocked || IsLinked(); // special cases where the map will remain unlocked despite no conditions matching

            foreach (Condition Condition in Conditions)
            {
                if (Condition.AccessMap == LockedMap)
                {
                    Condition.MapAccessible = false;
                }

                // if condition still meets checks and is still map accessible then this map remains unlocked
                if (!unlockOverride && Condition.RequiredChecks.meetsRequirements(currentChecks) && Condition.MapAccessible) stillUnlocked = true;
            }

            if (wasUnlocked && !stillUnlocked && !unlockOverride)
            {
                IsUnlocked = false;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Debug method. Returns a boolean pertaining to whether this instance can be unlocked by a given <see cref="MapID"/>.
        /// </summary>
        /// <param name="MapName">The <see cref="MapID"/> to check this instance's conditions for.</param>
        /// <returns>A boolean pertaining to whether this instance can be unlocked by the given <see cref="MapID"/>.</returns>
        public bool CanAccess(string MapName) 
        {
            foreach (Condition Condition in Conditions)
            {
                if (Condition.AccessMap == MapName) return true;
            }
            return false;
        }

        /// <summary>
        /// Evaluates if this <see cref="MapSector"/> can be used as a physical connection to other MapSectors.
        /// </summary>
        /// <returns>A boolean pertaining to whether this instance can be used as a physical connection to other MapSectors.</returns>
        public bool CanAccess()
        {
            foreach(MapSector sector in Tracker.MapSectors)
            {
                foreach(Condition condition in sector.Conditions)
                {
                    if (condition.AccessMap == MapID) return true;
                }    
            }
            return false;
        }

    }

    /// <summary>
    /// <see cref="Condition"/> class.                                                                                                                          <br />
    ///                                                                                                                                                         <br />
    /// Used to represent a way that a <see cref="MapSector"/> can become unlocked; from a simple map connection, to an area and a set of <see cref="Checks"/>.  <br />
    /// </summary>
    public class Condition
    {
        /// <summary>
        /// The <see cref="MapSector.MapID"/> of the <see cref="MapSector"/> that is required to be unlocked for this instance to become MapAccessible.
        /// </summary>
        [JsonIgnore]
        public string AccessMap { get; set; }

        /// <summary>
        /// The <see cref="Checks"/> that are required for this instance to become unlocked, provided it is MapAccessible.
        /// </summary>
        [JsonIgnore]
        public Checks RequiredChecks { get; set; }

        /// <summary>
        /// A value determining if this instance has its AccessMap requirement met, and can be reached from a physical route.
        /// </summary>
        public bool MapAccessible { get; set; }

        /// <summary>
        /// <see cref="Condition"/> constructor for a condition that becomes unlocked from a simple physical connection.
        /// </summary>
        /// <param name="_AccessMap">The <see cref="MapSector.MapID"/> of the <see cref="MapSector"/> that is required to be unlocked for this condition to become unlocked.</param>
        public Condition(string _AccessMap)
        {
            AccessMap = _AccessMap;
            RequiredChecks = new Checks();
            MapAccessible = false;
        }

        /// <summary>
        /// <see cref="Condition"/> constructor for a condition that becomes unlocked from a physical connection and a set of checks being met.
        /// </summary>
        /// <param name="_AccessMap">The <see cref="MapSector.MapID"/> of the <see cref="MapSector"/> that is required to be unlocked for this instance to become MapAccessible.</param>
        /// <param name="_RequiredChecks">The <see cref="Checks"/> instance for the set of Checks that are required for this instance to become unlocked, if MapAccessible.</param>
        /// <param name="_MapAccessible">Optional boolean determining if this instance is MapAccessible by default. Defaults to <b>false</b>.</param>
        public Condition(string _AccessMap, Checks _RequiredChecks, bool _MapAccessible = false)
        {
            AccessMap = _AccessMap;
            RequiredChecks = _RequiredChecks;
            MapAccessible = _MapAccessible;
        }

    }

    /// <summary>
    /// <see cref="Warp"/> class.                                                                                           <br />
    ///                                                                                                                     <br />
    /// Created for each warp in a <see cref="MapSector"/>, used for keeping track of warp destination and UI markers.      <br />
    /// </summary>
    public class Warp
    {
        /// <summary>
        /// The <see cref="MapSector.MapID"/> of the <see cref="MapSector"/> this warp belongs to.
        /// </summary>
        public string MapID { get; set; }

        /// <summary>
        /// The unique identifier for this <see cref="Warp"/>.
        /// </summary>
        public int WarpID { get; set; }

        /// <summary>
        /// A tuple representing the set destination of this <see cref="Warp"/>.
        /// </summary>
        public (string MapID, int WarpID) Destination { get; set; }

        /// <summary>
        /// A value used to represent this <see cref="Warp"/> with an icon/marker on the UI.        <br />
        ///                                                                                         <br />
        /// Supported values: 0 - 23. <em>(see <see cref="VisualMarkers"/> for full list.)</em>     <br />
        /// </summary>
        public int VisualMarkers { get; set; }

        /*
         * Supported values for VisualMarkers:
         * 
         *  0: None
         *  1: Dead End             /
         *  2: Arrow
         *  3: Bike
         *  4: Trainer
         *  5: Coal Badge           /
         *  6: Forest Badge         /
         *  7: Relic Badge          /
         *  8: Cobble Badge         /
         *  9: Fen Badge            /
         * 10: Mine Badge           /
         * 11: Icicle Badge         /
         * 12: Beacon Badge         /
         * 13: Aaron                /
         * 14: Bertha               /
         * 15: Flint                /
         * 16: Lucian               /
         * 17: Cynthia              /
         * 18: Rock Smash
         * 19: Cut
         * 20: Strength
         * 21: Surf
         * 22: Waterfall
         * 23: Rock Climb
         * 24: Master Ball          /
         * 25: Pokeball (item)  
         * 26: PokeMart             /
         * 27: Exclamation Point    /
         * 28: Galactic Key
         * 
         * Values marked with a (/) are considered to be checked.
        */

        /// <summary>
        /// Evaluates if this Warp is considered checked without being linked.
        /// </summary>
        public bool VisualMarkerChecked
        { 
            get
            {
                if (VisualMarkers == 1)                         return true;
                if (VisualMarkers >=5 && VisualMarkers <= 17)   return true;
                if (VisualMarkers == 24)                        return true;
                if (VisualMarkers >= 26 && VisualMarkers <= 27) return true;
                return false;
            }
        }
       
        /// <summary>
        /// <see cref="Warp"/> constructor.
        /// </summary>
        /// <param name="_MapID">The <see cref="MapSector.MapID"/> of the <see cref="MapSector"/> that this instance belongs to.</param>
        /// <param name="_WarpID">The unique identifier for this instance.</param>
        /// <param name="_Parent">The <see cref="MapSector"/> that this instance belongs to.</param>
        public Warp(string _MapID, int _WarpID, MapSector _Parent)
        {
            MapID = _MapID;
            WarpID = _WarpID;
            ParentMapSector = _Parent;
            Destination = ("Not set", -1);
            VisualMarkers = 0;
        }

        /// <summary>
        /// <see cref="Warp"/> constructor. Takes a given Warp and clones its data into a new instance.
        /// </summary>
        /// <param name="warpClone">The existing <see cref="Warp"/> instance to copy from.</param>
        public Warp(Warp warpClone)
        {
            MapID = warpClone.MapID;
            WarpID = warpClone.WarpID;
            ParentMapSector = warpClone.ParentMapSector;
            Destination = warpClone.Destination;
            VisualMarkers = warpClone.VisualMarkers;
        }

        /// <summary>
        /// Sets the <see cref="Destination"/> of this instance.
        /// </summary>
        /// <param name="_MapID">The <see cref="MapSector.MapID"/> of the destination warp.</param>
        /// <param name="_WarpID">The <see cref="WarpID"/> of the destination warp.</param>
        /// <param name="preserveVisualMarkers">A boolean used to make it so that set VisualMarkers are preserved on warp destination overwrite.</param>
        public void Set(string _MapID, int _WarpID, bool preserveVisualMarkers = false)
        {
            Destination = (_MapID, _WarpID);
            if (!preserveVisualMarkers) VisualMarkers = 0;
        }

        /// <summary>
        /// Clears the <see cref="Destination"/> of this instance.
        /// </summary>
        /// <param name="preserveVisualMarkers">A boolean used to make it so that set VisualMarkers are preserved on warp destination overwrite.</param>
        public void Clear(bool preserveVisualMarkers = false)
        {
            Destination = ("Not set", -1);
            if (!preserveVisualMarkers) VisualMarkers = 0;
        }

        // References

        /// <summary>
        /// The <see cref="LeahsPlatinumTracker.Tracker"/> instance that this <see cref="Warp"/> belongs to.
        /// </summary>
        public Tracker Tracker
        {
            get
            {
                return ParentMapSector.Tracker;
            }
        }

        /// <summary>
        /// The <see cref="MapSector"/> that this <see cref="Warp"/> belongs to.
        /// </summary>
        public MapSector ParentMapSector { get; }

        /// <summary>
        /// The <see cref="MapSector"/> that this <see cref="Warp"/> is linked to.
        /// </summary>
        [JsonIgnore]
        public MapSector DestinationMapSector
        {
            get
            {
                if (Destination.WarpID < 0)
                {
                    return null;
                }
                else
                {
                    return Tracker.GetMapSector(Destination.MapID);
                }
            }
        }

        /// <summary>
        /// The <see cref="VisualMapSector"/> that this <see cref="Warp"/> is linked to.
        /// </summary>
        [JsonIgnore]
        public VisualMapSector DestinationVisualMapSector
        {
            get
            {
                if (Destination.WarpID < 0)
                {
                    return null;
                }
                else
                {
                    return Tracker.GetVisualMapSector(Destination.MapID);
                }
            }
        }

        /// <summary>
        /// Evaluates if this <see cref="Warp"/> has a set <see cref="Warp.Destination"/>.
        /// </summary>
        public bool HasDestination { get { return Destination.WarpID >= 0; } }
    }
}
