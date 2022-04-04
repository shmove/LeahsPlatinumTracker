using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leah_s_Platinum_Tracker
{
    internal class Tracker
    {
        // Checks
        public Checks Checks { get; set; }

        // Map
        public List<MapSector> MapSectors { get; set; }

        public Tracker()
        {

            Checks = new Checks(); // Creates Checks with nothing set

            // Entire map
            MapSectors = new List<MapSector>();
            //MapSectors.Add(new MapSector());
            // Routes
            MapSectors.Add(new MapSector("201", 0, true));
            MapSectors.Add(new MapSector("202", 0, true));
            MapSectors.Add(new MapSector("203", 1, true));
            MapSectors.Add(new MapSector("204 S", 1, true));
            MapSectors.Add(new MapSector("204 N", 1, "Floaroma"));
            MapSectors.Add(new MapSector("205 S", 0, new List<Condition>
            {
                new Condition("Floaroma"),
                new Condition("ValleyWindworks"),
                new Condition("205 C", new Checks(Checks.CheckFlags.HasDefeatedWindworks)), // Two Grunts block the path until Mars is defeated in ValleyWindworks
                new Condition("FuegoIronworks S", new Checks(16)) // river can be surfed across
            }));
            MapSectors.Add(new MapSector("205 C", 2, new List<Condition>
            {
                new Condition("205 S", new Checks(16)), // river can be surfed across
                new Condition("205 S", new Checks(Checks.CheckFlags.HasDefeatedWindworks)), // Two Grunts block the path until Mars is defeated in ValleyWindworks
                new Condition("205 N", new Checks(2)), // from the north, with cut
                new Condition("FuegoIronworks N"), // via one-way from fuego
                new Condition("FuegoIronworks S", new Checks(16)) // river can be surfed across
            }));
            MapSectors.Add(new MapSector("205 N", 1, new List<Condition>
            {
                new Condition("Eterna"),
                new Condition("205 C", new Checks(2)) // from the south, with cut
            }));
            MapSectors.Add(new MapSector("206 S A", 1, "207 N"));
            MapSectors.Add(new MapSector("206 S B", 2, new Condition("206 S A", new Checks(2))));
            // MapSectors.Add(new MapSector("206 N", 2)); // "corridor"
            MapSectors.Add(new MapSector("207 S", 0, new List<Condition> {
                new Condition("207 N"),
                new Condition("Oreburgh A")
            }));
            MapSectors.Add(new MapSector("207 N", 1, new List<Condition> {
                new Condition("207 S", new Checks(Checks.CheckFlags.HasBike)),
                new Condition("206 S A")
            }));
            MapSectors.Add(new MapSector("208", 3));
            MapSectors.Add(new MapSector("209", 2, "Solaceon"));
            MapSectors.Add(new MapSector("210 S", 1, new List<Condition> {
                new Condition("Solaceon"),
                new Condition("215"),
                new Condition("210 N", new Checks(Checks.CheckFlags.HasSecretPotion)) // Psyduck block the path until given medicine
            }));
            MapSectors.Add(new MapSector("210 N", 0, new List<Condition> {
                new Condition("Celestic"),
                new Condition("210 H"),
                new Condition("210 S", new Checks(Checks.CheckFlags.HasSecretPotion)) // Psyduck block the path until given medicine
            }));
            MapSectors.Add(new MapSector("210 H", 2, new Condition("210 N", new Checks(64)))); // House unreachable without rock climb
            MapSectors.Add(new MapSector("211 W", 1, "Eterna"));
            MapSectors.Add(new MapSector("211 E", 1, "Celestic"));
            MapSectors.Add(new MapSector("212", 3, "Pastoria"));
            MapSectors.Add(new MapSector("213", 3, new Condition("ValorLake Ext B", new Checks(80))));
            MapSectors.Add(new MapSector("214", 2, new List<Condition>
            {
                new Condition("ValorLake Ext A"),
                new Condition("SpringPath")
            }));
            MapSectors.Add(new MapSector("215", 1, "210 S"));
            MapSectors.Add(new MapSector("216", 2, "217"));
            MapSectors.Add(new MapSector("217", 2, new List<Condition>
            {
                new Condition("216"),
                new Condition("AcuityLake Ext S") // AcuityLake Ext N is a rock climb check
            }));
            // MapSectors.Add(new MapSector("218", 2)); // self contained // "corridor"
            // MapSectors.Add(new MapSector("219/220", 0, new Condition(new Checks(16)))); // ocean route, requires surf
            MapSectors.Add(new MapSector("221", 2, new Condition("Sandgem", new Checks(16), true))); // via sandgem ocean route, requires surf
            MapSectors.Add(new MapSector("222", 3, "ValorLake Ext A"));
            //MapSectors.Add(new MapSector("223", 0, new List<Condition> { // empty route
            //    new Condition("Sunyshore", new Checks(16)), // Sunyshore via Surf
            //    new Condition("PokeLeague S", new Checks(144)) // PokeLeague S via Surf + Waterfall
            //}));
            // MapSectors.Add(new MapSector("224", 1)); // dead end (unless oak's letter?)
            MapSectors.Add(new MapSector("225", 2, "SurvivalArea A"));
            MapSectors.Add(new MapSector("226 W", 0, new List<Condition> {
                new Condition("SurvivalArea A"),
                new Condition("SurvivalArea B", new Checks(64)), // house above SurvivalArea
                new Condition("226 H", new Checks(80)) // house island via surf + rock climb
            }));
            MapSectors.Add(new MapSector("226 H", 1, new List<Condition> {
                new Condition("226 W", new Checks(80)), // 226 W via surf + rock climb
                new Condition("226 E", new Checks(16)) // 226 E via surf
            }));
            MapSectors.Add(new MapSector("226 E", 1, new List<Condition> {
                new Condition("226 H", new Checks(16)), // 226 H via surf
                new Condition("227 S")
            }));
            MapSectors.Add(new MapSector("227 S", 0, new List<Condition> {
                new Condition("226 E"),
                new Condition("227 C")
            }));
            MapSectors.Add(new MapSector("227 C", 1, new List<Condition> {
                new Condition("227 S", new Checks(Checks.CheckFlags.HasBike)),
                new Condition("227 N", new Checks(Checks.CheckFlags.HasBike))
            }));
            MapSectors.Add(new MapSector("227 N", 0, new List<Condition> {
                new Condition("StarkMtn"),
                new Condition("227 C", new Checks(Checks.CheckFlags.HasBike))
            }));
            MapSectors.Add(new MapSector("228", 4, "229"));
            MapSectors.Add(new MapSector("229", 0, new List<Condition> {
                new Condition("228"),
                new Condition("ResortArea"),
                new Condition("FightArea", new Checks(16)) // surf through 230 to fight area
            }));
            // 230 is surf corridor, treat 229 <-(surf)-> fight area

            MapSectors.Add(new MapSector("AcuityLake Ext S", 0, new List<Condition>
            {
                new Condition("217"),
                new Condition("Snowpoint"),
                new Condition("AcuityLake Ext N", new Checks(64))
            }));
            MapSectors.Add(new MapSector("AcuityLake Ext N", 1, new Condition("AcuityLake Ext S", new Checks(64))));
            MapSectors.Add(new MapSector("AcuityLake Int A", 1, new Condition("AcuityLake Int B", new Checks(16))));
            MapSectors.Add(new MapSector("AcuityLake Int B", 1, new Condition("AcuityLake Int A", new Checks(16))));
            MapSectors.Add(new MapSector("BacklotMansion", 6));
            MapSectors.Add(new MapSector("Canalave", 10, "IronIsland Ext A"));
            MapSectors.Add(new MapSector("Celestic", 6, new List<Condition>
            {
                new Condition("210 N"),
                new Condition("211 E")
            }));
            MapSectors.Add(new MapSector("DeptStore", 11));
            MapSectors.Add(new MapSector("Eterna", 10, new List<Condition>
            {
                new Condition("TGEterna Ext", new Checks(2)), // Accessible via TGEterna exit with Cut
                new Condition("205 N"),
                new Condition("211 W")
            }));
            MapSectors.Add(new MapSector("EternaForest", 2, new Condition("EternaForest M", new Checks(2)))); // Accessible via Mansion w Cut
            MapSectors.Add(new MapSector("EternaForest M", 1, new Condition("EternaForest", new Checks(2)))); // vice versa
            MapSectors.Add(new MapSector("TGEterna Ext", 1, new Condition("Eterna", new Checks(2)))); // Accessible via Eterna with Cut
            MapSectors.Add(new MapSector("TGEterna 1F", 4));
            MapSectors.Add(new MapSector("TGEterna 2F", 3));
            MapSectors.Add(new MapSector("TGEterna 3F", 3));
            MapSectors.Add(new MapSector("FightArea", 5, new List<Condition>
            {
                new Condition("229", new Checks(16)), // Accessible via 229 if Surf through 230
                new Condition("Snowpoint", new Checks(Checks.ProgressFlags.HasCynthia)) // can get here from Snowpoint, but only postgame
            }));
            MapSectors.Add(new MapSector("FloaromaMeadow N", 1)); // self contained
            MapSectors.Add(new MapSector("FloaromaMeadow S", 2, new Condition("FloaromaMeadow N"))); // accessible from the north one-way
            MapSectors.Add(new MapSector("Floaroma", 6, new List<Condition>
            {
                new Condition("205 S"),
                new Condition("204 N")
            }));
            MapSectors.Add(new MapSector("FuegoIronworks N", 1, new Condition("FuegoIronworks S", new Checks(16)))); // surf from FuegoIronworks S
            MapSectors.Add(new MapSector("FuegoIronworks S", 1, new List<Condition>
            {
                new Condition("FuegoIronworks N", new Checks(16)), // river can be surfed across
                new Condition("205 C", new Checks(16)), // river can be surfed across
                new Condition("205 S", new Checks(16)) // river can be surfed across
            }));
            MapSectors.Add(new MapSector("GalacticHQ 1F A", 2, new Condition("GalacticHQ 1F B", new Checks(Checks.CheckFlags.HasGalacticKey)))); // 1st floor, entrance section
            MapSectors.Add(new MapSector("GalacticHQ 1F B", 1, new Condition("GalacticHQ 1F A", new Checks(Checks.CheckFlags.HasGalacticKey)))); // 1st floor, entrance section behind galactic door
            MapSectors.Add(new MapSector("GalacticHQ 1F C", 3)); // 1F room with stairs and 2 warp pads
            MapSectors.Add(new MapSector("GalacticHQ 2F A", 4)); // 2F room with 3 warp pads (spread out) and stairs
            MapSectors.Add(new MapSector("GalacticHQ 2F B", 3)); // 2F room with 1 warp pad and two sets of stairs
            MapSectors.Add(new MapSector("GalacticHQ 3F", 4)); // 3F room with 3 warp pads (together) and stairs
            MapSectors.Add(new MapSector("GalacticHQ 4F A", 1, new Condition("GalacticHQ 4F B", new Checks(Checks.CheckFlags.HasGalacticKey)))); // room below Cyrus' room with stairs
            MapSectors.Add(new MapSector("GalacticHQ 4F B", 3, new Condition("GalacticHQ 4F A", new Checks(Checks.CheckFlags.HasGalacticKey)))); // Cyrus' Room
            MapSectors.Add(new MapSector("GalacticHQ Warehouse B2F A", 2, new Condition("GalacticHQ Warehouse B2F B", new Checks(Checks.CheckFlags.HasGalacticKey)))); // Southern section with 2 stairs
            MapSectors.Add(new MapSector("GalacticHQ Warehouse B2F B", 1, new Condition("GalacticHQ Warehouse B2F A", new Checks(Checks.CheckFlags.HasGalacticKey)))); // Northern section with stairs
            MapSectors.Add(new MapSector("Hearthome A", 13, new List<Condition> // main Hearthome, can access from locked sections by walking through NPCs on entry
            {
                new Condition("Hearthome B"),
                new Condition("Hearthome C")
            })); 
            MapSectors.Add(new MapSector("Hearthome B", 1, new Condition("Hearthome A", new Checks(Checks.ProgressFlags.HasRelicBadge)))); // bottom right - can access from main Hearthome w 3rd badge
            MapSectors.Add(new MapSector("Hearthome C", 1, new Condition("Hearthome A", new Checks(Checks.CheckFlags.HasSpokenFantina)))); // gym - can access after speaking to fantina in contest hall
            MapSectors.Add(new MapSector("IronIsland Ext A", 2, new List<Condition>
            {
                new Condition("IronIsland Ext B"), // accessible via one way from cave
                new Condition("Canalave") // accessible via boat
            }));
            MapSectors.Add(new MapSector("IronIsland Ext B", 1)); // inaccessible cave exit
            MapSectors.Add(new MapSector("IronIsland 1F", 3)); // room with cave entrance and two sets of stairs
            MapSectors.Add(new MapSector("IronIsland B1F 2", 3)); // room with two sets of stairs and elevator leading up to third set of stairs
            MapSectors.Add(new MapSector("IronIsland B3F 1", 3)); // room with 1 cave entrance and an elevator leading up to 2 more cave entrances
            MapSectors.Add(new MapSector("Jubilife A", 8, true)); // main jubilife
            MapSectors.Add(new MapSector("Jubilife B", 2, new Condition("Jubilife A", new Checks(Checks.ProgressFlags.HasCoalBadge), true))); // bottom left, accessible with 1st badge
            MapSectors.Add(new MapSector("Jubilife GTS", 3)); // self contained
            MapSectors.Add(new MapSector("Jubilife TV", 10)); // self contained
            MapSectors.Add(new MapSector("MtCoronet 1F 1 A", 2, new Condition("MtCoronet 1F 1 B", new Checks(64)))); // Cyrus cutscene room, lower half - accessible via just rock climb and one way from upper half
            MapSectors.Add(new MapSector("MtCoronet 1F 1 B", 1, new Condition("MtCoronet 1F 1 A", new Checks(80)))); // Upper half, only accessible via surf and rock climb
            MapSectors.Add(new MapSector("MtCoronet Upper 1F 1 A", 1)); // very bottom, not accessible but exitable with strength
            MapSectors.Add(new MapSector("MtCoronet Upper 1F 1 B", 1, new Condition("MtCoronet Upper 1F 1 C", new Checks(32))));// leftmost entrance, accessible with only strength, can avoid RS check
            MapSectors.Add(new MapSector("MtCoronet Upper 1F 1 C", 1, new List<Condition> // rightmost entrance
            {
                new Condition("MtCoronet Upper 1F 1 A", new Checks(32)), // accessible from A with strength
                new Condition("MtCoronet Upper 1F 1 B", new Checks(33)), // accessible from B with strength + rock smash
                new Condition("MtCoronet Upper 1F 1 D", new Checks(32)) // accessible from D with strength
            }));
            MapSectors.Add(new MapSector("MtCoronet Upper 1F 1 D", 1, new Condition("MtCoronet Upper 1F 1 C", new Checks(32)))); // topmost entrance, accessible with strength
            MapSectors.Add(new MapSector("MtCoronet Upper 1F 2", 3)); // self contained room with two cave exits and a set of stairs
            MapSectors.Add(new MapSector("MtCoronet 3F", 3)); // self contained room with cave entrance and two set of stairs down
            MapSectors.Add(new MapSector("MtCoronet 4F 1 A", 1, new Condition("MtCoronet 4F 1 C", new Checks(64)))); // MtCoronet room with a waterfall - leftmost entrance, only connected via rock climb
            MapSectors.Add(new MapSector("MtCoronet 4F 1 B", 1, new Condition("MtCoronet 4F 1 D", new Checks(144)))); // bottomright entrance, only connected via waterfall + surf
            MapSectors.Add(new MapSector("MtCoronet 4F 1 C", 1, new Condition("MtCoronet 4F 1 A", new Checks(64)))); // topright entrance, only connected to bottomleft via rock climb
            MapSectors.Add(new MapSector("MtCoronet 4F 1 D", 1, new Condition("MtCoronet 4F 1 B", new Checks(144)))); // top of waterfall, only connected to bottomright via waterfall+surf
            MapSectors.Add(new MapSector("MtCoronet Summit A", 1, new Condition("MtCoronet Summit B", new Checks(64)))); // bottommost entrance, connects to middle sect via RC
            MapSectors.Add(new MapSector("MtCoronet Summit B", 2, new Condition("MtCoronet Summit A", new Checks(64)))); // middle section, accessed from below via RC
            MapSectors.Add(new MapSector("MtCoronet Summit C", 2, new Condition("MtCoronet Summit D", new Checks(64)))); // top right section, accessed from top via RC
            MapSectors.Add(new MapSector("MtCoronet Summit D", 1, new Condition("MtCoronet Summit C", new Checks(64)))); // topmost section, access from topright via RC
            MapSectors.Add(new MapSector("OldChateau 1F", 5)); // self contained
            MapSectors.Add(new MapSector("OldChateau 2F", 6)); // self contained
            MapSectors.Add(new MapSector("Oreburgh A", 11, new List<Condition> // Main area excluding blocked gym entrance
            {
                new Condition("207 S"),
                new Condition("Oreburgh B") // walk through rival in door on exit
            }));
            MapSectors.Add(new MapSector("Oreburgh B", 1, new Condition("Oreburgh A", new Checks(Checks.CheckFlags.HasSpokenRoark)))); // Gym is inaccessible until player has spoken to Roark (or done something else?)
            MapSectors.Add(new MapSector("OreburghGate A", 2, new Condition("OreburghGate B", new Checks(1))));
            MapSectors.Add(new MapSector("OreburghGate B", 1, new Condition("OreburghGate A", new Checks(1))));
            MapSectors.Add(new MapSector("Pastoria", 8, new Condition("212")));
            MapSectors.Add(new MapSector("PokeLeague Ext S", 2, new Condition("Sunyshore A", new Checks(144)))); // can surf here from sunyshore and waterfall up
            MapSectors.Add(new MapSector("PokeLeague Ext N A", 1, new Condition("PokeLeague Ext N B", new Checks(144)))); // caveside, can surf down from league exit
            MapSectors.Add(new MapSector("PokeLeague Ext N B", 1, new Condition("PokeLeague Ext N A", new Checks(144)))); // entrance to league, can surf up from caveside
            MapSectors.Add(new MapSector("PokeLeague Int", 4)); // self contained
            MapSectors.Add(new MapSector("Poketch", 3)); // self contained
            MapSectors.Add(new MapSector("ResortArea", 2, new Condition("229")));
            MapSectors.Add(new MapSector("Sandgem", 4, true));
            MapSectors.Add(new MapSector("Snowpoint", 6, new List<Condition>
            {
                new Condition("FightArea"), // can take boat from fight area here
                new Condition("AcuityLake Ext S")
            }));
            MapSectors.Add(new MapSector("SolaceonRuins 1F", 4)); // room with entrance
            MapSectors.Add(new MapSector("SolaceonRuins B1F", 4)); // room with hiker
            MapSectors.Add(new MapSector("SolaceonRuins B2F", 4)); // room with other NPC
            MapSectors.Add(new MapSector("SolaceonRuins B3F A", 4)); // empty room with 4 sets of stairs (1 going up)
            MapSectors.Add(new MapSector("SolaceonRuins B3F B", 3)); // empty room with 3 sets of stairs
            MapSectors.Add(new MapSector("SolaceonRuins B4F", 4)); // empty room with 4 sets of stairs (1 going down)
            MapSectors.Add(new MapSector("Solaceon", 8, new List<Condition>
            {
                new Condition("210 S"),
                new Condition("209")
            }));
            MapSectors.Add(new MapSector("SpringPath", 1, new Condition("214", new Checks(Checks.ProgressFlags.HasCynthia)))); // how does this one work?
            MapSectors.Add(new MapSector("SendoffSpring", 2)); // self contained
            MapSectors.Add(new MapSector("StarkMtn", 1, new Condition("227 N")));
            MapSectors.Add(new MapSector("Sunyshore A", 9, new List<Condition> // main sunyshore
            {
                new Condition("Sunyshore B", new Checks(64)), // rock climb house
                new Condition("Sunyshore C"), // via gym exit, can walk through flint
                new Condition("PokeLeague Ext S", new Checks(144)) // surf and waterfall down from PokeLeague
            }));
            MapSectors.Add(new MapSector("Sunyshore B", 1, new Condition("Sunyshore A", new Checks(64)))); // rock climb house
            MapSectors.Add(new MapSector("Sunyshore C", 1, new Condition("Sunyshore A", new Checks(Checks.CheckFlags.HasSpokenVolkner)))); // gym
            MapSectors.Add(new MapSector("SurvivalArea A", 4, new List<Condition>
            {
                new Condition("225"),
                new Condition("226 W")
            }));
            MapSectors.Add(new MapSector("SurvivalArea B", 1, new Condition("226 W", new Checks(64)))); // house above SurvivalArea, only accessible via RC from adjacent route
            MapSectors.Add(new MapSector("ValleyWindworks", 1, new Condition("205 S", new Checks(Checks.CheckFlags.HasWorksKey))));
            MapSectors.Add(new MapSector("ValorLake Ext A", 7, new List<Condition> // main valor lakefront
            {
                new Condition("214"),
                new Condition("222"),
                new Condition("ValorLake Ext B", new Checks(64)) // extra house (+ lower route) via RC
            }));
            MapSectors.Add(new MapSector("ValorLake Ext B", 1, new List<Condition> // valor lakefront house
            {
                new Condition("ValorLake Ext A", new Checks(64)), // via lakefront
                new Condition("213", new Checks(80)) // beach route via RC + surf
            }));
            MapSectors.Add(new MapSector("ValorLake Int A", 1, new Condition("ValorLake Int B", new Checks(16))));
            MapSectors.Add(new MapSector("ValorLake Int B", 1, new Condition("ValorLake Int A", new Checks(16))));
            MapSectors.Add(new MapSector("Veilstone", 14)); // self contained
            MapSectors.Add(new MapSector("VerityLake Ext", 1, true));
            MapSectors.Add(new MapSector("VerityLake Int A", 1, new Condition("VerityLake Int B", new Checks(16))));
            MapSectors.Add(new MapSector("VerityLake Int B", 1, new Condition("VerityLake Int A", new Checks(16))));
            MapSectors.Add(new MapSector("VictoryRoad B1F A", 1, new Condition("VictoryRoad B1F B", new Checks(16)))); // Waterfall room, bottom entrance
            MapSectors.Add(new MapSector("VictoryRoad B1F B", 1, new List<Condition>
            {
                new Condition("VictoryRoad B1F A", new Checks(16)), // from below with surf
                new Condition("VictoryRoad B1F C") // from above with one-way
            }));
            MapSectors.Add(new MapSector("VictoryRoad B1F C", 1, new Condition("VictoryRoad B1F B", new Checks(144))));
            MapSectors.Add(new MapSector("VictoryRoad 1F A", 1, new Condition("VictoryRoad 1F B", new Checks(64)))); // hell room in victory road - bottom entrance via RC from section B
            MapSectors.Add(new MapSector("VictoryRoad 1F B", 1, new List<Condition> // leftmid entrance + bottom section
            {
                new Condition("VictoryRoad 1F A", new Checks(64)), // from section A with RC
                new Condition("VictoryRoad 1F D") // via one-way
            }));
            MapSectors.Add(new MapSector("VictoryRoad 1F C", 1, new Condition("VictoryRoad 1F D", new Checks(64)))); // leftbottom entrance via RC from section D
            MapSectors.Add(new MapSector("VictoryRoad 1F D", 1, new List<Condition> // mid section with stairs down
            {
                new Condition("VictoryRoad 1F C", new Checks(64)), // via RC from section C
                new Condition("VictoryRoad 1F E") // via one-way
            }));
            MapSectors.Add(new MapSector("VictoryRoad 1F E", 2, new List<Condition> // upper section with cave and stairs down
            {
                new Condition("VictoryRoad 1F F", new Checks(64)), // via RC from section F
                new Condition("VictoryRoad 1F G", new Checks(64)) // via RC from section G
            }));
            MapSectors.Add(new MapSector("VictoryRoad 1F F", 1, new Condition("VictoryRoad 1F E", new Checks(64)))); // lefttop entrance - via RC from section E
            MapSectors.Add(new MapSector("VictoryRoad 1F G", 1, new Condition("VictoryRoad 1F E", new Checks(64)))); // topmost entrance - via RC from section E
        }

        public bool UpdateMap(string unlockedArea)
        {
            bool successfullyUpdated = false;
            foreach (MapSector sector in MapSectors)
            {
                if (sector.AttemptUnlock(unlockedArea, Checks))
                {
                    System.Diagnostics.Debug.WriteLine("Unlocked map: " + sector.MapID);
                    successfullyUpdated = true;
                    UpdateMap(sector.MapID);
                };
            }
            return successfullyUpdated;
        }

        public void RevertMap(string lockedArea)
        {
            foreach(MapSector sector in MapSectors)
            {
                if (sector.AttemptLock(lockedArea, Checks))
                {
                    System.Diagnostics.Debug.WriteLine("Locked map: " + sector.MapID);
                    RevertMap(sector.MapID);
                }
            }
        }

        public bool UpdateMap()
        {
            bool successfullyUpdated = false;
            foreach (MapSector sector in MapSectors)
            {
                if (sector.AttemptUnlock(Checks))
                {
                    System.Diagnostics.Debug.WriteLine("Unlocked map: " + sector.MapID);
                    successfullyUpdated = true;
                    UpdateMap(sector.MapID);
                }
            }
            return successfullyUpdated;
        }

        public void RevertMap()
        {
            foreach(MapSector sector in MapSectors)
            {
                if(sector.AttemptLock(Checks))
                {
                    System.Diagnostics.Debug.WriteLine("Locked map: " + sector.MapID);
                    RevertMap(sector.MapID);
                };
            }
        }

        public bool LinkWarps((string MapID, int WarpID) warp1, (string MapID, int WarpID) warp2)
        {
            bool linked1 = false;
            bool linked2 = false;
            foreach (MapSector MapSector in MapSectors)
            {
                if (warp1.MapID == MapSector.MapID)
                {
                    if (MapSector.IsLinked(warp1.WarpID))
                    {
                        UnlinkWarp((warp1.MapID, warp1.WarpID));
                        if (!MapSector.IsMapAccessible()) RevertMap(MapSector.MapID);
                    }

                    if (MapSector.Link(warp1.WarpID, warp2))
                    {
                        System.Diagnostics.Debug.WriteLine("Created link from " + warp1.MapID + " to " + warp2.MapID);
                        UpdateMap(warp1.MapID);
                        linked1 = true;
                    };
                }
                else if (warp2.MapID == MapSector.MapID)
                {
                    if (MapSector.IsLinked(warp2.WarpID))
                    {
                        UnlinkWarp((warp2.MapID, warp2.WarpID));
                        if (!MapSector.IsMapAccessible()) RevertMap(MapSector.MapID);
                    }

                    if (MapSector.Link(warp2.WarpID, warp1))
                    {
                        System.Diagnostics.Debug.WriteLine("Created link from " + warp2.MapID + " to " + warp1.MapID);
                        UpdateMap(warp2.MapID);
                        linked2 = false;
                    }
                }

                if (linked1 && linked2) break;
            }

            return linked1 && linked2;
        }

        public void UnlinkWarp((string MapID, int WarpID) warp)
        {
            // loop to find initial warp to unlink
            foreach (MapSector MapSector1 in MapSectors)
            {
                if (warp.MapID == MapSector1.MapID)
                {
                    Warp preWarp = MapSector1.GetWarp(warp.WarpID);
                    string destinationMap = preWarp.Destination.MapID;
                    int destinationID = preWarp.Destination.WarpID;

                    System.Diagnostics.Debug.WriteLine("Removed link between " + warp.MapID + " and " + destinationMap);

                    // Unlink warp, and if this isolates the MapSector then update map
                    if (!MapSector1.Unlink(warp.WarpID))
                    {
                        if (!MapSector1.IsMapAccessible()) RevertMap(MapSector1.MapID);
                    };
                    // loop to find destination and unlink
                    foreach (MapSector MapSector2 in MapSectors)
                    {
                        if (destinationMap == MapSector2.MapID)
                        {   
                            // Unlink warp, and if this isolates the MapSector then update map
                            if (!MapSector2.Unlink(destinationID))
                            {
                                if (!MapSector2.IsMapAccessible()) RevertMap(MapSector2.MapID);
                            };
                            break;
                        }
                    }
                    break;
                }
            }
        }

        public void log()
        {
            System.Diagnostics.Debug.WriteLine("");
            System.Diagnostics.Debug.WriteLine("Logging all tracker data.");
            System.Diagnostics.Debug.WriteLine("CURRENT CHECKS:");
            System.Diagnostics.Debug.WriteLine(Checks.CheckString());
            System.Diagnostics.Debug.WriteLine("");
            System.Diagnostics.Debug.WriteLine("MAP DATA:");
            foreach (var Sector in MapSectors)
            {
                System.Diagnostics.Debug.WriteLine(Sector.MapID);
                System.Diagnostics.Debug.WriteLine("    isUnlocked: " + Sector.IsUnlocked);
                System.Diagnostics.Debug.WriteLine("    Conditions: ");
                foreach(var Condition in Sector.Conditions)
                {
                    System.Diagnostics.Debug.WriteLine("        - Access Map: " + Condition.AccessMap);
                    System.Diagnostics.Debug.WriteLine("          Checks:");
                    System.Diagnostics.Debug.WriteLine("            " + Condition.RequiredChecks.CheckString());

                }
                System.Diagnostics.Debug.WriteLine("    Warps: ");
                foreach (var Warp in Sector.Warps)
                {
                    System.Diagnostics.Debug.WriteLine("        " + Warp.WarpID + ": " + Warp.Destination);
                }
            }
            System.Diagnostics.Debug.WriteLine("");

            foreach (MapSector Sector in MapSectors)
            {
                if (Sector.IsUnlocked && (Sector.Conditions.Count == 0))
                {
                    System.Diagnostics.Debug.WriteLine(Sector.MapID + " with " + Sector.Warps.Count + " warps: Starting area");
                }
                else
                {
                    bool accessible = false;
                    bool accesses = false;
                    foreach (MapSector mapSector in MapSectors)
                    {
                        if (mapSector.CanAccess(Sector.MapID))
                        {
                            accessible = true;
                            bool wayBack = false;
                            foreach (Condition condition in Sector.Conditions)
                            {
                                if (condition.AccessMap == mapSector.MapID) wayBack = true;
                            }
                            if (!wayBack) System.Diagnostics.Debug.WriteLine(Sector.MapID + " has a One-Way route to " + mapSector.MapID);
                        }
                    };
                    foreach (Condition condition in Sector.Conditions)
                    {
                        bool thisAccesses = false;
                        foreach (MapSector mapSector in MapSectors)
                        {
                            if (mapSector.MapID == condition.AccessMap)
                            {
                                accesses = true;
                                thisAccesses = true;
                            }
                        }
                        if (!thisAccesses) System.Diagnostics.Debug.WriteLine(Sector.MapID + " is accessed by " + condition.AccessMap + ", which is unknown");
                    };
                    if (accessible && accesses) System.Diagnostics.Debug.WriteLine(Sector.MapID + " with " + Sector.Warps.Count + " warps: In group of connected maps");
                    else if (accessible) System.Diagnostics.Debug.WriteLine(Sector.MapID + " with " + Sector.Warps.Count + " warps: Exitable, but not accessible");
                    else if (accesses) System.Diagnostics.Debug.WriteLine(Sector.MapID + " with " + Sector.Warps.Count + " warps: Accessible, but not exitable");
                    else System.Diagnostics.Debug.WriteLine(Sector.MapID + " with " + Sector.Warps.Count + " warps: Self confined");
                };
            }

            System.Diagnostics.Debug.WriteLine("");
        }

    }

}
