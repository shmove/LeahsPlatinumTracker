using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LeahsPlatinumTracker
{
    public class Tracker
    {
        // Checks
        public Checks Checks { get; set; }

        // Map
        public List<VisualMapSector> VisualMapSectors { get; set; }
        [JsonIgnore]
        public List<MapSector> MapSectors
        {
            get
            {
                List<MapSector> _MapSectors = new List<MapSector>();
                foreach(VisualMapSector sector in VisualMapSectors)
                {
                    foreach(MapSector mapSector in sector.MapSectors)
                    {
                        _MapSectors.Add(mapSector);
                    }
                }
                return _MapSectors;

            }
        }

        // Identifiers
        public string Game { get; set; }
        public string CreatedVersion { get; set; }

        public Tracker()
        {

            Checks = new Checks(); // Creates Checks with nothing set

            Game = "PokemonPlatinum";
            CreatedVersion = Program.Version;

            // Entire map
            VisualMapSectors = new List<VisualMapSector>();

            // Routes
            VisualMapSectors.Add(new VisualMapSector(this, new MapSector("203", 1, true)));
            VisualMapSectors.Add(new VisualMapSector(this, "204", new List<MapSector>
            {
                new MapSector("204 S", 1, true),
                new MapSector("204 N", 1, "Floaroma")
            }));
            VisualMapSectors.Add(new VisualMapSector(this, "205", new List<MapSector>
            {
                new MapSector("205 S", 0, new List<Condition>
                {
                    new Condition("Floaroma"),
                    new Condition("ValleyWindworks"),
                    new Condition("205 C", new Checks(Checks.CheckFlags.HasDefeatedWindworks)), // Two Grunts block the path until Mars is defeated in ValleyWindworks
                    new Condition("FuegoIronworks S", new Checks(16)) // river can be surfed across
                }),
                new MapSector("205 C", 2, new List<Condition>
                {
                    new Condition("205 S", new Checks(16)), // river can be surfed across
                    new Condition("205 S", new Checks(Checks.CheckFlags.HasDefeatedWindworks)), // Two Grunts block the path until Mars is defeated in ValleyWindworks
                    new Condition("205 N", new Checks(2)), // from the north, with cut
                    new Condition("FuegoIronworks N"), // via one-way from fuego
                    new Condition("FuegoIronworks S", new Checks(16)) // river can be surfed across
                }),
                new MapSector("205 N", 1, new List<Condition>
                {
                    new Condition("Eterna"),
                    new Condition("205 C", new Checks(2)) // from the south, with cut
                })
            }));
            VisualMapSectors.Add(new VisualMapSector(this, "206", new List<MapSector>
            {
                new MapSector("206 S A", 1, "207 N"),
                new MapSector("206 S B", 2, new Condition("206 S A", new Checks(2)))
                //new MapSector("206 N", 2) // basically a hallway but its on the same route
            }));
            VisualMapSectors.Add(new VisualMapSector(this, "207", new List<MapSector>
            {
                new MapSector("207 S", 0, new List<Condition> {
                    new Condition("207 N"),
                    new Condition("Oreburgh A")
                }),
                new MapSector("207 N", 1, new List<Condition> {
                    new Condition("207 S", new Checks(Checks.CheckFlags.HasBike)),
                    new Condition("206 S A")
                })
            }));
            VisualMapSectors.Add(new VisualMapSector(this, new MapSector("208", 3)));
            VisualMapSectors.Add(new VisualMapSector(this, new MapSector("209", 2, "Solaceon")));
            VisualMapSectors.Add(new VisualMapSector(this, "210", new List<MapSector>
            {
                new MapSector("210 S", 1, new List<Condition> {
                    new Condition("Solaceon"),
                    new Condition("215"),
                    new Condition("210 N", new Checks(Checks.CheckFlags.HasSecretPotion)) // Psyduck block the path until given medicine
                }),
                new MapSector("210 N", 0, new List<Condition> {
                    new Condition("Celestic"),
                    new Condition("210 H"),
                    new Condition("210 S", new Checks(Checks.CheckFlags.HasSecretPotion)) // Psyduck block the path until given medicine
                }),
                new MapSector("210 H", 2, new Condition("210 N", new Checks(64))) // House unreachable without rock climb
            }));
            VisualMapSectors.Add(new VisualMapSector(this, "211", new List<MapSector>
            {
                new MapSector("211 W", 1, "Eterna"),
                new MapSector("211 E", 1, "Celestic")
            }));
            VisualMapSectors.Add(new VisualMapSector(this, new MapSector("212", 3, "Pastoria")));
            VisualMapSectors.Add(new VisualMapSector(this, new MapSector("213", 3, new Condition("ValorLake Ext B", new Checks(80)))));
            VisualMapSectors.Add(new VisualMapSector(this, new MapSector("214", 2, new List<Condition>
            {
                new Condition("ValorLake Ext A"),
                new Condition("SpringPath")
            })));
            VisualMapSectors.Add(new VisualMapSector(this, new MapSector("215", 1, "210 S")));
            VisualMapSectors.Add(new VisualMapSector(this, new MapSector("216", 2, "217")));
            VisualMapSectors.Add(new VisualMapSector(this, new MapSector("217", 2, new List<Condition>
            {
                new Condition("216"),
                new Condition("AcuityLake Ext S") // AcuityLake Ext N is a rock climb check
            })));
            // MapSectors.Add(new MapSector("218", 2)); // self contained // "corridor"
            // MapSectors.Add(new MapSector("219/220", 0, new Condition(new Checks(16)))); // ocean route, requires surf
            VisualMapSectors.Add(new VisualMapSector(this, new MapSector("221", 2, new Condition("Sandgem", new Checks(16), true)))); // via sandgem ocean route, requires surf
            VisualMapSectors.Add(new VisualMapSector(this, new MapSector("222", 3, "ValorLake Ext A")));
            // MapSectors.Add(new MapSector("223", 0, new List<Condition> { // empty route
            //    new Condition("Sunyshore", new Checks(16)), // Sunyshore via Surf
            //    new Condition("PokeLeague S", new Checks(144)) // PokeLeague S via Surf + Waterfall
            // }));
            // MapSectors.Add(new MapSector("224", 1)); // dead end (unless oak's letter?)
            VisualMapSectors.Add(new VisualMapSector(this, new MapSector("225", 2, "SurvivalArea A")));
            VisualMapSectors.Add(new VisualMapSector(this, "226", new List<MapSector>
            {
                new MapSector("226 W", 0, new List<Condition> {
                    new Condition("SurvivalArea A"),
                    new Condition("SurvivalArea B", new Checks(64)), // house above SurvivalArea
                    new Condition("226 H", new Checks(80)) // house island via surf + rock climb
                }),
                new MapSector("226 H", 1, new List<Condition> {
                    new Condition("226 W", new Checks(80)), // 226 W via surf + rock climb
                    new Condition("226 E", new Checks(16)) // 226 E via surf
                }),
                new MapSector("226 E", 1, new List<Condition> {
                    new Condition("226 H", new Checks(16)), // 226 H via surf
                    new Condition("227 S")
                })
            }));
            VisualMapSectors.Add(new VisualMapSector(this, "227", new List<MapSector>
            {
                new MapSector("227 S", 0, new List<Condition> {
                    new Condition("226 E"),
                    new Condition("227 C")
                }),
                new MapSector("227 C", 1, new List<Condition> {
                    new Condition("227 S", new Checks(Checks.CheckFlags.HasBike)),
                    new Condition("227 N", new Checks(Checks.CheckFlags.HasBike))
                }),
                new MapSector("227 N", 0, new List<Condition> {
                    new Condition("StarkMtn"),
                    new Condition("227 C", new Checks(Checks.CheckFlags.HasBike))
                })
            }));
            VisualMapSectors.Add(new VisualMapSector(this, "228", new List<MapSector>
            {
                new MapSector("228", 2, new List<Condition>
                {
                    new Condition("ResortArea"),
                    new Condition("228 SW"), // accessible by sliding down bike ramp
                    new Condition("228 C", new Checks(Checks.CheckFlags.HasBike)) // riding up bike ramp from cave
                }),
                new MapSector("228 SW", 1, new Condition("228", new Checks(Checks.CheckFlags.HasBike))), // southwesterly section
                new MapSector("228 C", 1, new Condition("228")) // cave
            }));
            // 230/229 is surf/corridor, treat resort area <-(surf)-> fight area

            // Named Locations
            VisualMapSectors.Add(new VisualMapSector(this, "AcuityLake", new List<MapSector>
            {
                new MapSector("AcuityLake Ext S", 0, new List<Condition>
                {
                    new Condition("217"),
                    new Condition("Snowpoint"),
                    new Condition("AcuityLake Ext N", new Checks(64))
                }),
                new MapSector("AcuityLake Ext N", 1, new Condition("AcuityLake Ext S", new Checks(64))),
                new MapSector("AcuityLake Int A", 1, new Condition("AcuityLake Int B", new Checks(16))),
                new MapSector("AcuityLake Int B", 1, new Condition("AcuityLake Int A", new Checks(16)))
            }, "Lake Acuity"));
            VisualMapSectors.Add(new VisualMapSector(this, new MapSector("BacklotMansion", 6), "Backlot Mansion"));
            VisualMapSectors.Add(new VisualMapSector(this, "Canalave", new List<MapSector>
            {
                new MapSector("Canalave", 10, new List<Condition>
                {
                    new Condition("IronIsland Ext A"),
                    new Condition("Canalave Pokecentre", new Checks(Checks.CheckFlags.HasTeleport))
                }),
                new MapSector("Canalave Pokecentre", 3)
            }));
            VisualMapSectors.Add(new VisualMapSector(this, "Celestic", new List<MapSector>
            {
                new MapSector("Celestic", 6, new List<Condition>
                {
                    new Condition("210 N"),
                    new Condition("211 E"),
                    new Condition("Celestic Pokecentre", new Checks(Checks.CheckFlags.HasTeleport))
                }),
                new MapSector("Celestic Pokecentre", 3)
            }));
            VisualMapSectors.Add(new VisualMapSector(this, new MapSector("DeptStore", 11), "Dept. Store"));
            VisualMapSectors.Add(new VisualMapSector(this, "Eterna", new List<MapSector> {
                new MapSector("Eterna", 10, new List<Condition>
                {
                    new Condition("TGEterna Ext", new Checks(2)), // Accessible via TGEterna exit with Cut
                    new Condition("205 N"),
                    new Condition("211 W"),
                    new Condition("Eterna Pokecentre", new Checks(Checks.CheckFlags.HasTeleport))
                }),
                new MapSector("TGEterna Ext", 1, new Condition("Eterna", new Checks(2))), // Accessible via Eterna with Cut
                new MapSector("Eterna Pokecentre", 3)
            }));
            VisualMapSectors.Add(new VisualMapSector(this, "EternaForest", new List<MapSector>
            {
                new MapSector("EternaForest", 2, new Condition("EternaForest M", new Checks(2))), // Accessible via Mansion w Cut
                new MapSector("EternaForest M", 1, new Condition("EternaForest", new Checks(2))) // vice versa
            }, "Eterna Forest"));
            VisualMapSectors.Add(new VisualMapSector(this, "TGEterna", new List<MapSector>
            {
                new MapSector("TGEterna 1F", 4),
                new MapSector("TGEterna 2F", 3),
                new MapSector("TGEterna 3F", 3),

            }, "T.G. Eterna"));
            VisualMapSectors.Add(new VisualMapSector(this, "FightArea", new List<MapSector>
            {
                new MapSector("FightArea", 5, new List<Condition>
                {
                    new Condition("ResortArea", new Checks(16)), // Accessible via ResortArea/229 if Surf through 230
                    new Condition("Snowpoint", new Checks(Checks.ProgressFlags.HasCynthia)), // can get here from Snowpoint, but only postgame
                    new Condition("FightArea Pokecentre", new Checks(Checks.CheckFlags.HasTeleport))
                }),
                new MapSector("FightArea Pokecentre", 3)
            }, "Fight Area"));
            VisualMapSectors.Add(new VisualMapSector(this, "FloaromaMeadow", new List<MapSector>
            {
                new MapSector("FloaromaMeadow N", 1), // self contained
                new MapSector("FloaromaMeadow S", 2, new Condition("FloaromaMeadow N")) // accessible from the north one-way
            }, "F. Meadow"));
            VisualMapSectors.Add(new VisualMapSector(this, "Floaroma", new List<MapSector>
            {
                new MapSector("Floaroma", 6, new List<Condition>
                {
                    new Condition("205 S"),
                    new Condition("204 N"),
                    new Condition("Floaroma Pokecentre", new Checks(Checks.CheckFlags.HasTeleport))
                }),
                new MapSector("Floaroma Pokecentre", 3)
            }));
            VisualMapSectors.Add(new VisualMapSector(this, "FuegoIronworks", new List<MapSector>
            {
                new MapSector("FuegoIronworks N", 1, new Condition("FuegoIronworks S", new Checks(16))), // surf from FuegoIronworks S
                new MapSector("FuegoIronworks S", 1, new List<Condition>
                {
                    new Condition("FuegoIronworks N", new Checks(16)), // river can be surfed across
                    new Condition("205 C", new Checks(16)), // river can be surfed across
                    new Condition("205 S", new Checks(16)) // river can be surfed across
                })
            }, "Fuego Ironworks"));
            VisualMapSectors.Add(new VisualMapSector(this, "GalacticHQ", new List<MapSector>
            {
                new MapSector("GalacticHQ 1F A", 2, new Condition("GalacticHQ 1F B", new Checks(Checks.CheckFlags.HasGalacticKey))), // 1st floor, entrance section
                new MapSector("GalacticHQ 1F B", 1, new Condition("GalacticHQ 1F A", new Checks(Checks.CheckFlags.HasGalacticKey))), // 1st floor, entrance section behind galactic door
                new MapSector("GalacticHQ 1F C", 3), // 1F room with stairs and 2 warp pads
                new MapSector("GalacticHQ 2F A", 4), // 2F room with 3 warp pads (spread out) and stairs
                new MapSector("GalacticHQ 2F B", 3), // 2F room with 1 warp pad and two sets of stairs
                new MapSector("GalacticHQ 3F", 4), // 3F room with 3 warp pads (together) and stairs
                new MapSector("GalacticHQ 4F A", 1, new Condition("GalacticHQ 4F B", new Checks(Checks.CheckFlags.HasGalacticKey))), // room below Cyrus' room with stairs
                new MapSector("GalacticHQ 4F B", 3, new Condition("GalacticHQ 4F A", new Checks(Checks.CheckFlags.HasGalacticKey))), // Cyrus' Room
                new MapSector("GalacticHQ Warehouse B2F A", 2, new Condition("GalacticHQ Warehouse B2F B", new Checks(Checks.CheckFlags.HasGalacticKey))), // Southern section with 2 stairs
                new MapSector("GalacticHQ Warehouse B2F B", 1, new Condition("GalacticHQ Warehouse B2F A", new Checks(Checks.CheckFlags.HasGalacticKey))) // Northern section with stairs
            }, "Galactic HQ"));
            VisualMapSectors.Add(new VisualMapSector(this, "Hearthome", new List<MapSector>
            {
                new MapSector("Hearthome A", 13, new List<Condition> // main Hearthome, can access from locked sections by walking through NPCs on entry
                {
                    new Condition("Hearthome B"),
                    new Condition("Hearthome C"),
                    new Condition("Hearthome Pokecentre", new Checks(Checks.CheckFlags.HasTeleport))
                }),
                new MapSector("Hearthome B", 1, new Condition("Hearthome A", new Checks(Checks.ProgressFlags.HasRelicBadge))), // bottom right - can access from main Hearthome w 3rd badge
                new MapSector("Hearthome C", 1, new Condition("Hearthome A", new Checks(Checks.CheckFlags.HasSpokenFantina))), // gym - can access after speaking to fantina in contest hall
                new MapSector("Hearthome Pokecentre", 3)
            }));
            VisualMapSectors.Add(new VisualMapSector(this, "IronIsland", new List<MapSector>
            {
                new MapSector("IronIsland Ext A", 2, new List<Condition>
                {
                    new Condition("IronIsland Ext B"), // accessible via one way from cave
                    new Condition("Canalave") // accessible via boat
                }),
                new MapSector("IronIsland Ext B", 1), // inaccessible cave exit
                new MapSector("IronIsland 1F", 3), // room with cave entrance and two sets of stairs
                new MapSector("IronIsland B1F 2", 3), // room with two sets of stairs and elevator leading up to third set of stairs
                new MapSector("IronIsland B3F 1", 3) // room with 1 cave entrance and an elevator leading up to 2 more cave entrances
            }, "Iron Island"));
            VisualMapSectors.Add(new VisualMapSector(this, "Jubilife", new List<MapSector>
            {
                new MapSector("Jubilife A", 8, true), // main jubilife
                new MapSector("Jubilife B", 2, new Condition("Jubilife A", new Checks(Checks.ProgressFlags.HasCoalBadge), true)), // bottom left, accessible with 1st badge
                new MapSector("Jubilife Pokecentre", 3)
            }));
            VisualMapSectors.Add(new VisualMapSector(this, new MapSector("JubilifeGTS", 3), "Jubilife GTS")); // self contained
            VisualMapSectors.Add(new VisualMapSector(this, new MapSector("JubilifeTV", 10), "Jubilife TV")); // self contained
            VisualMapSectors.Add(new VisualMapSector(this, "MtCoronet", new List<MapSector>
            {
                new MapSector("MtCoronet 1F 1 A", 2, new Condition("MtCoronet 1F 1 B", new Checks(64))), // Cyrus cutscene room, lower half - accessible via just rock climb and one way from upper half
                new MapSector("MtCoronet 1F 1 B", 1, new Condition("MtCoronet 1F 1 A", new Checks(80))), // Upper half, only accessible via surf and rock climb
                new MapSector("MtCoronet Upper 1F 1 A", 1), // very bottom, not accessible but exitable with strength
                new MapSector("MtCoronet Upper 1F 1 B", 1, new Condition("MtCoronet Upper 1F 1 C", new Checks(32))), // leftmost entrance, accessible with only strength, can avoid RS check
                new MapSector("MtCoronet Upper 1F 1 C", 1, new List<Condition> // rightmost entrance
                {
                    new Condition("MtCoronet Upper 1F 1 A", new Checks(32)), // accessible from A with strength
                    new Condition("MtCoronet Upper 1F 1 B", new Checks(33)), // accessible from B with strength + rock smash
                    new Condition("MtCoronet Upper 1F 1 D", new Checks(32)) // accessible from D with strength
                }),
                new MapSector("MtCoronet Upper 1F 1 D", 1, new Condition("MtCoronet Upper 1F 1 C", new Checks(32))),
                new MapSector("MtCoronet Upper 1F 2", 3), // self contained room with two cave exits and a set of stairs
                new MapSector("MtCoronet 2F A", 2, new Condition("MtCoronet 2F B", new Checks(32))), // broken boards cave room, leftmost warps
                new MapSector("MtCoronet 2F B", 2, new Condition("MtCoronet 2F A")), // broken boards cave room rightmost stairs
                new MapSector("MtCoronet 3F", 3), // self contained room with cave entrance and two set of stairs down
                new MapSector("MtCoronet 4F 1 A", 1, new Condition("MtCoronet 4F 1 C", new Checks(64))), // MtCoronet room with a waterfall - leftmost entrance, only connected via rock climb
                new MapSector("MtCoronet 4F 1 B", 1, new Condition("MtCoronet 4F 1 D", new Checks(144))), // bottomright entrance, only connected via waterfall + surf
                new MapSector("MtCoronet 4F 1 C", 1, new Condition("MtCoronet 4F 1 A", new Checks(64))), // topright entrance, only connected to bottomleft via rock climb
                new MapSector("MtCoronet 4F 1 D", 1, new Condition("MtCoronet 4F 1 B", new Checks(144))), // top of waterfall, only connected to bottomright via waterfall+surf
            }, "Mt. Coronet"));
            VisualMapSectors.Add(new VisualMapSector(this, "MtCoronetPeak", new List<MapSector>
            {
                new MapSector("MtCoronetPeak A", 1, new Condition("MtCoronetPeak B", new Checks(64))), // bottommost entrance, connects to middle sect via RC
                new MapSector("MtCoronetPeak B", 2, new Condition("MtCoronetPeak A", new Checks(64))), // middle section, accessed from below via RC
                new MapSector("MtCoronetPeak C", 2, new Condition("MtCoronetPeak D", new Checks(64))), // top right section, accessed from top via RC
                new MapSector("MtCoronetPeak D", 1, new Condition("MtCoronetPeak C", new Checks(64))) // topmost section, access from topright via RC
            }, "Mt. Coronet Peak"));
            VisualMapSectors.Add(new VisualMapSector(this, "OldChateau", new List<MapSector>
            {
                new MapSector("OldChateau 1F", 5), // self contained
                new MapSector("OldChateau 2F", 6) // self contained
            }, "Old Chateau"));
            VisualMapSectors.Add(new VisualMapSector(this, "Oreburgh", new List<MapSector>
            {
                new MapSector("Oreburgh A", 11, new List<Condition> // Main area excluding blocked gym entrance
                {
                    new Condition("207 S"),
                    new Condition("Oreburgh B"), // walk through rival in door on exit
                    new Condition("Oreburgh Pokecentre", new Checks(Checks.CheckFlags.HasTeleport))
                }),
                new MapSector("Oreburgh B", 1, new Condition("Oreburgh A", new Checks(Checks.CheckFlags.HasSpokenRoark))), // Gym is inaccessible until player has spoken to Roark (or done something else?)
                new MapSector("Oreburgh Pokecentre", 3)
            }));
            VisualMapSectors.Add(new VisualMapSector(this, "OreburghGate", new List<MapSector>
            {
                new MapSector("OreburghGate A", 2, new Condition("OreburghGate B", new Checks(1))),
                new MapSector("OreburghGate B", 1, new Condition("OreburghGate A", new Checks(1)))
            }, "Oreburgh Gate"));
            VisualMapSectors.Add(new VisualMapSector(this, "Pastoria", new List<MapSector>
            {
                new MapSector("Pastoria", 8, new List<Condition>
                {
                    new Condition("212"),
                    new Condition("Pastoria Pokecentre", new Checks(Checks.CheckFlags.HasTeleport))
                }),
                new MapSector("Pastoria Pokecentre", 3)
            }));
            VisualMapSectors.Add(new VisualMapSector(this, "PokeLeague", new List<MapSector>
            {
                new MapSector("PokeLeague Ext S", 2, new Condition("Sunyshore A", new Checks(144))), // can surf here from sunyshore and waterfall up
                new MapSector("PokeLeague Ext N A", 1, new Condition("PokeLeague Ext N B", new Checks(144))), // caveside, can surf down from league exit
                new MapSector("PokeLeague Ext N B", 1, new List<Condition> // entrance to league, can surf up from caveside
                {
                    new Condition("PokeLeague Ext N A", new Checks(144)),
                    new Condition("PokeLeague Pokecentre", new Checks(Checks.CheckFlags.HasTeleport))
                }), 
                new MapSector("PokeLeague Int", 4), // self contained
                new MapSector("PokeLeague Pokecentre", 3)
            }, "Pokémon League"));
            VisualMapSectors.Add(new VisualMapSector(this, new MapSector("Poketch", 3), "Pokétch")); // self contained
            VisualMapSectors.Add(new VisualMapSector(this, "ResortArea", new List<MapSector>
            {
                new MapSector("ResortArea", 2, new List<Condition>
                {
                    new Condition("FightArea", new Checks(16)), // surf through 230 to fight area
                    new Condition("228"),
                    new Condition("ResortArea Pokecentre")
                }),
                new MapSector("ResortArea Pokecentre", 3)
            }, "Resort Area"));
            VisualMapSectors.Add(new VisualMapSector(this, "Sandgem", new List<MapSector>
            {
                new MapSector("Sandgem", 4, new List<Condition>
                {
                    new Condition("Jubilife"),
                    new Condition("VerityLake Ext"),
                    new Condition("221", new Checks(16))
                }, true),
                new MapSector("Sandgem Pokecentre", 3)
            }));
            VisualMapSectors.Add(new VisualMapSector(this, "Snowpoint", new List<MapSector>
            {
                new MapSector("Snowpoint", 6, new List<Condition>
                {
                    new Condition("FightArea"), // can take boat from fight area here
                    new Condition("AcuityLake Ext S"),
                    new Condition("Snowpoint Pokecentre", new Checks(Checks.CheckFlags.HasTeleport))
                }),
                new MapSector("Snowpoint Pokecentre", 3)
            }));
            VisualMapSectors.Add(new VisualMapSector(this, "SolaceonRuins", new List<MapSector>
            {
                new MapSector("SolaceonRuins 1F", 4), // room with entrance
                new MapSector("SolaceonRuins B1F", 4), // room with hiker
                new MapSector("SolaceonRuins B2F", 4), // room with other NPC
                new MapSector("SolaceonRuins B3F A", 4), // empty room with 4 sets of stairs (1 going up)
                new MapSector("SolaceonRuins B3F B", 3), // empty room with 3 sets of stairs
                new MapSector("SolaceonRuins B4F", 4) // empty room with 4 sets of stairs (1 going down)
            }, "Solaceon Ruins"));
            VisualMapSectors.Add(new VisualMapSector(this, "Solaceon", new List<MapSector>
            {
                new MapSector("Solaceon", 8, new List<Condition>
                {
                    new Condition("210 S"),
                    new Condition("209"),
                    new Condition("Solaceon Pokecentre", new Checks(Checks.CheckFlags.HasTeleport))
                }),
                new MapSector("Solaceon Pokecentre", 3)
            }));
            VisualMapSectors.Add(new VisualMapSector(this, "SendoffSpring", new List<MapSector>
            {
                new MapSector("SpringPath", 1, new Condition("214", new Checks(Checks.ProgressFlags.HasCynthia))), // how does this one work?
                new MapSector("SendoffSpring S", 1, new Condition("SendoffSpring N", new Checks(64))), // accessible from the north with RC
                new MapSector("SendoffSpring N", 1, new Condition("SendoffSpring S", new Checks(4160))) // vice versa + need completion
            }, "Sendoff Spring"));
            VisualMapSectors.Add(new VisualMapSector(this, new MapSector("StarkMtn", 1, new Condition("227 N")), "Stark Mountain"));
            VisualMapSectors.Add(new VisualMapSector(this, "Sunyshore", new List<MapSector>
            {
                new MapSector("Sunyshore A", 9, new List<Condition> // main sunyshore
                {
                    new Condition("Sunyshore B", new Checks(64)), // rock climb house
                    new Condition("Sunyshore C"), // via gym exit, can walk through flint
                    new Condition("PokeLeague Ext S", new Checks(144)), // surf and waterfall down from PokeLeague
                    new Condition("Sunyshore Pokecentre", new Checks(Checks.CheckFlags.HasTeleport))
                }),
                new MapSector("Sunyshore B", 1, new Condition("Sunyshore A", new Checks(64))), // rock climb house
                new MapSector("Sunyshore C", 1, new Condition("Sunyshore A", new Checks(Checks.CheckFlags.HasSpokenVolkner))), // gym
                new MapSector("Sunyshore Pokecentre", 3)
            }));
            VisualMapSectors.Add(new VisualMapSector(this, "SurvivalArea", new List<MapSector>
            {
                new MapSector("SurvivalArea A", 4, new List<Condition>
                {
                    new Condition("225"),
                    new Condition("226 W"),
                    new Condition("SurvivalArea Pokecentre", new Checks(Checks.CheckFlags.HasTeleport))
                }),
                new MapSector("SurvivalArea B", 1, new Condition("226 W", new Checks(64))), // house above SurvivalArea, only accessible via RC from adjacent route
                new MapSector("SurvivalArea Pokecentre", 3)
            }, "Survival Area"));
            VisualMapSectors.Add(new VisualMapSector(this, new MapSector("ValleyWindworks", 1, new Condition("205 S", new Checks(Checks.CheckFlags.HasWorksKey))), "Valley Windworks"));
            VisualMapSectors.Add(new VisualMapSector(this, "ValorLake", new List<MapSector>
            {
                new MapSector("ValorLake Ext A", 7, new List<Condition> // main valor lakefront
                {
                    new Condition("214"),
                    new Condition("222"),
                    new Condition("ValorLake Ext B", new Checks(64)) // extra house (+ lower route) via RC
                }),
                new MapSector("ValorLake Ext B", 1, new List<Condition> // valor lakefront house
                {
                    new Condition("ValorLake Ext A", new Checks(64)), // via lakefront
                    new Condition("213", new Checks(80)) // beach route via RC + surf
                }),
                new MapSector("ValorLake Int A", 1, new Condition("ValorLake Int B", new Checks(16))),
                new MapSector("ValorLake Int B", 1, new Condition("ValorLake Int A", new Checks(16)))
            }, "Lake Valor"));
            VisualMapSectors.Add(new VisualMapSector(this, "Veilstone", new List<MapSector> // self contained
            {
                new MapSector("Veilstone", 14, new Condition("Veilstone Pokecentre", new Checks(Checks.CheckFlags.HasTeleport))),
                new MapSector("Veilstone Pokecentre", 3)
            })); 
            VisualMapSectors.Add(new VisualMapSector(this, "VerityLake", new List<MapSector>
            {
                new MapSector("VerityLake Ext", 1, true),
                new MapSector("VerityLake Int A", 1, new Condition("VerityLake Int B", new Checks(16))),
                new MapSector("VerityLake Int B", 1, new Condition("VerityLake Int A", new Checks(16)))
            }, "Lake Verity"));
            VisualMapSectors.Add(new VisualMapSector(this, "VictoryRoad", new List<MapSector>
            {
                new MapSector("VictoryRoad B1F A", 1, new Condition("VictoryRoad B1F B", new Checks(16))), // Waterfall room, bottom entrance
                new MapSector("VictoryRoad B1F B", 1, new List<Condition>
                {
                    new Condition("VictoryRoad B1F A", new Checks(16)), // from below with surf
                    new Condition("VictoryRoad B1F C") // from above with one-way
                }),
                new MapSector("VictoryRoad B1F C", 1, new Condition("VictoryRoad B1F B", new Checks(144))),

                new MapSector("VictoryRoad 1F A", 1, new Condition("VictoryRoad 1F B", new Checks(64))), // hell room in victory road - bottom entrance via RC from section B
                new MapSector("VictoryRoad 1F B", 1, new List<Condition> // leftmid entrance + bottom section
                {
                    new Condition("VictoryRoad 1F A", new Checks(64)), // from section A with RC
                    new Condition("VictoryRoad 1F D") // via one-way
                }),
                new MapSector("VictoryRoad 1F C", 1, new Condition("VictoryRoad 1F D", new Checks(64))), // leftbottom entrance via RC from section D
                new MapSector("VictoryRoad 1F D", 1, new List<Condition> // mid section with stairs down
                {
                    new Condition("VictoryRoad 1F C", new Checks(64)), // via RC from section C
                    new Condition("VictoryRoad 1F E") // via one-way
                }),
                new MapSector("VictoryRoad 1F E", 2, new List<Condition> // upper section with cave and stairs down
                {
                    new Condition("VictoryRoad 1F F", new Checks(64)), // via RC from section F
                    new Condition("VictoryRoad 1F G", new Checks(64)) // via RC from section G
                }),
                new MapSector("VictoryRoad 1F F", 1, new Condition("VictoryRoad 1F E", new Checks(64))), // lefttop entrance - via RC from section E
                new MapSector("VictoryRoad 1F G", 1, new Condition("VictoryRoad 1F E", new Checks(64))) // topmost entrance - via RC from section E
            }, "Victory Road"));
        }

        // Functions
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
                        System.Diagnostics.Debug.WriteLine("1: Created link from " + warp1.MapID + " to " + warp2.MapID);
                        UpdateMap(warp1.MapID);
                        linked1 = true;
                    };
                }
                
                if (warp2.MapID == MapSector.MapID)
                {
                    if (MapSector.IsLinked(warp2.WarpID))
                    {
                        UnlinkWarp((warp2.MapID, warp2.WarpID));
                        if (!MapSector.IsMapAccessible()) RevertMap(MapSector.MapID);
                    }

                    if (MapSector.Link(warp2.WarpID, warp1))
                    {
                        System.Diagnostics.Debug.WriteLine("2: Created link from " + warp2.MapID + " to " + warp1.MapID);
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

        public MapSector GetMapSector(string MapID)
        {
            foreach (MapSector sector in MapSectors)
            {
                if (sector.MapID == MapID) return sector;
            }
            return null;
        }

        public VisualMapSector GetVisualMapSector(string MapID)
        {
            foreach(VisualMapSector VisualSector in VisualMapSectors)
            {
                foreach(MapSector sector in VisualSector.MapSectors)
                {
                    if (sector.MapID == MapID) return VisualSector;
                }
                if (VisualSector.VisualMapID == MapID) return VisualSector;
            }
            return null;
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

        // Convert tracker to JSON
        public void ToJSON()
        {
            string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "LeahsPlatinumTracker");
            Directory.CreateDirectory(folder);

            string filename = folder + "\\save.lpt";

            string json = JsonConvert.SerializeObject(this, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore, Formatting = Formatting.Indented });
            File.WriteAllText(filename, json);
        }

    }

    public static class TrackerManager
    {
        public static Tracker FromJSON(string json)
        {
            Tracker tracker = new Tracker();
            dynamic? loadedTracker = JsonConvert.DeserializeObject<dynamic>(json);

            tracker.Game = loadedTracker.Game;
            tracker.CreatedVersion = loadedTracker.CreatedVersion;

            tracker.Checks = new Checks();
            tracker.Checks.ChecksMade = loadedTracker.Checks.ChecksMade;
            tracker.Checks.Progress = loadedTracker.Checks.Progress;
            tracker.Checks.HMs = loadedTracker.Checks.HMs;

            foreach (var JSONVisualMapSector in loadedTracker.VisualMapSectors)
            {
                foreach (var JSONMapSector in JSONVisualMapSector.MapSectors)
                {
                    string thisMapID = JSONMapSector.MapID;
                    MapSector thisMapSector = tracker.GetMapSector(thisMapID);
                    foreach(var JSONWarp in JSONMapSector.Warps)
                    {
                        int thisWarpID = JSONWarp.WarpID;
                        Warp thisWarp = thisMapSector.Warps[thisWarpID];
                        string thisWarpDestinationMapID = JSONWarp.Destination.Item1;
                        int thisWarpDestinationWarpID = JSONWarp.Destination.Item2;
                        thisWarp.Destination = (thisWarpDestinationMapID, thisWarpDestinationWarpID);
                        thisWarp.VisualMarkers = JSONWarp.VisualMarkers;
                    }
                    thisMapSector.IsUnlocked = (bool)JSONMapSector.IsUnlocked;
                }
            }

            return tracker;
        }
    }
}
