using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratedRPGGame
{
    class Program
    {
        static void Main(string[] args)
        {
            String[] elementSet = new String[7] { "Fire", "Water", "Wind", "Earth", "Thunder", "Light", "Dark" };
            String[] physicalSet = new String[7] { "Piercing", "Smashing", "Slicing", "Slamming", "Crushing", "Penetrating", "Deadly" };
            String[] arcaneSet = new String[9] { "Bolt", "Ball", "Arrow", "Spray", "Missile", "Blast", "Storm", "Explosion", "Burst" };
            String[] empty = new String[2] {"", ""};
            
            List <Quest> questList = new List<Quest>();
            List <Npc> npcList = new List <Npc>();
            List <Obj> objList = new List <Obj>();
            List <Loc> locList = new List <Loc>();
            List <Eqp> eqpList = new List <Eqp>();
            List <Monsters> monList = new List<Monsters>();

            List <CharClasses> classList = new List<CharClasses>();
            List <CharSkills> skillsList = new List<CharSkills>();
            List <CharSpells> spellsList = new List<CharSpells>();
            List <CharPerks> perksList = new List<CharPerks>();

            Generators gen = new Generators();
            var ran = new Random((int) DateTime.Now.Ticks);

            classList.Add(new CharClasses("Warrior"));
            classList.Add(new CharClasses("Theif"));
            classList.Add(new CharClasses("Mage"));
            classList.Add(new CharClasses("Priest"));

            skillsList.Add(new CharSkills(gen.makeAbilityName(physicalSet, elementSet, arcaneSet)));
            spellsList.Add(new CharSpells(gen.makeAbilityName(elementSet, arcaneSet, empty)));
            perksList.Add(new CharPerks(gen.makeAbilityName(physicalSet, arcaneSet, empty)));

            npcList.Add(new Npc());
            objList.Add(new Obj());
            locList.Add(new Loc());
            eqpList.Add(new Eqp());
            monList.Add(new Monsters());
            
            Player np = new Player("Eric", "Gamemaster");
                       
            for (int i=0;i<10;i++)
            {
                questList.Add(gen.makeQuest(npcList.ElementAt(ran.Next(0, npcList.Count())),
                                            objList.ElementAt(ran.Next(0, objList.Count())),
                                            locList.ElementAt(ran.Next(0, locList.Count())),
                                            eqpList.ElementAt(ran.Next(0, eqpList.Count())),
                                            monList.ElementAt(ran.Next(0, monList.Count()))));
                
                npcList.Add(gen.makeNpc(np.playerLvl, np.location, np.playerStats, classList, skillsList, spellsList, perksList));
                
                locList.Add(gen.makeLoc(np.playerLvl, np.playerClass, monList, npcList, eqpList));

                eqpList.Add(gen.makeEqp(np.playerLvl, np.playerStats, np.playerClass, skillsList, spellsList, perksList));

                skillsList.Add(new CharSkills(gen.makeAbilityName(physicalSet, elementSet, arcaneSet)));

                spellsList.Add(new CharSpells(gen.makeAbilityName(elementSet, arcaneSet, empty)));

                perksList.Add(new CharPerks(gen.makeAbilityName(physicalSet, arcaneSet, empty)));

                System.Threading.Thread.Sleep(i * 10);
             }
            confirmRng(questList, npcList, monList, locList);

          }

        public static void confirmRng(List<Quest> questList, List<Npc> npcList, List<Monsters> monList, List<Loc> locList)
        {
            Console.WriteLine("All quests");
            for (int i = 0; i < questList.Count(); i++)
            {
                questList.ElementAt(i).dispQuestInfo();
            }
            Console.ReadLine();

            Console.WriteLine("All NPCS");
            for (int i = 1; i < npcList.Count(); i++)
            {
                npcList.ElementAt(i).dispNPCInfo();
            }
            Console.ReadLine();

            Console.WriteLine("All Monsters");
            for (int i = 1; i < monList.Count(); i++)
            {
                monList.ElementAt(i).dispMonInfo();
            }
            Console.ReadLine();

            Console.WriteLine("All Locations");
            for (int i = 1; i < locList.Count(); i++)
            {
                locList.ElementAt(i).dispLocInfo();
            }
            Console.ReadLine();
        }
    }

    class Generators
    {
        public String name { get; set; }
        public String makeName(int x)
        {
            var vowels = "aeiouy";
            var constant = "bcdfghjklmnpqrstvwxyz";
            var generated = new char[x];
            var random = new Random((int) DateTime.Now.Ticks);

            for (int i = 0; i < generated.Length; i++)
            {
                if (i / 2 == (int)i / 2 && i > 0 && random.Next(0, 100) > 50)
                    generated[i] = vowels[random.Next(vowels.Length)];
                else
                    generated[i] = constant[random.Next(constant.Length)];
            }

            var genName = new String(generated);
            this.name = genName;
            return this.name;
        }

        public String makeAbilityName(String[] set1, String[] set2, String[] set3)
        {
            var rand = new Random((int) DateTime.Now.Ticks);
            String ability = set1.ElementAt(rand.Next(0, set1.Count())) +
                             set2.ElementAt(rand.Next(0, set2.Count())) +
                             set3.ElementAt(rand.Next(0, set3.Count()));
            return ability;
         }

        public String selClass(List<CharClasses> cls)
        {
            var randomn = new Random((int) DateTime.Now.Ticks);
            return cls.ElementAt(randomn.Next(0, cls.Count)).className;
        }

        public String[] selSkills(List<CharSkills> skills, int amount)
        {
            String[] allSkills= new string[amount];
            var randomn = new Random((int) DateTime.Now.Ticks);
            
            for (int i=0;i<allSkills.Count();i++)
            {
                allSkills[i] = skills.ElementAt(randomn.Next(0, skills.Count())).skillName;
            }
            return allSkills;
        }

        public String[] selSpells(List<CharSpells> spells, int amount)
        {
            String[] allSpells = new string[amount];
            var randomn = new Random((int) DateTime.Now.Ticks);

            for (int i = 0; i < allSpells.Count(); i++)
            {
                allSpells[i] = spells.ElementAt(randomn.Next(0, spells.Count())).spellName;
            }
            return allSpells;
        }

        public String[] selPerks(List<CharPerks> perks, int amount)
        {
            String[] allPerks = new string[amount];
            var randomn = new Random((int) DateTime.Now.Ticks);

            for (int i = 0; i < allPerks.Count(); i++)
            {
                allPerks[i] = perks.ElementAt(randomn.Next(0, perks.Count())).perkName;
            }
            return allPerks;
        }

        public String[] selMons(List <Monsters> mon, int amount)
        {
            String[] allMons = new string[amount];
            var randomn = new Random((int) DateTime.Now.Ticks);

            for (int i = 0; i < allMons.Count(); i++)
            {
                allMons[i] = mon.ElementAt(randomn.Next(0, mon.Count())).monName;
            }
            return allMons;
        }

        public String[] selNpcs(List<Npc> npc, int amount)
        {
            String[] allNpcs = new string[amount];
            var randomn = new Random((int) DateTime.Now.Ticks);

            for (int i = 0; i < allNpcs.Count(); i++)
            {
                allNpcs[i] = npc.ElementAt(randomn.Next(0, npc.Count())).npcName;
            }
            return allNpcs;
        }

        public String[] selEqps(List<Eqp> eqp, int amount)
        {
            String[] allEqps = new string[amount];
            var randomn = new Random((int) DateTime.Now.Ticks);

            for (int i = 0; i < allEqps.Count(); i++)
            {
                allEqps[i] = eqp.ElementAt(randomn.Next(0, eqp.Count())).eqpName;
            }
            return allEqps;
        }


        /* Quest is generated based on NPC, Object, Location, Equipment, and Monsters
         */
        public Quest makeQuest(Npc n, Obj o, Loc l, Eqp e, Monsters m)
        {
            Quest newQuest = new Quest(n, o, l, e, m);
            return newQuest;
        }

        /* NPC is generated based on user level, user current location, 
         * user unlocked classes list, user stats, user skills, user spells, and user perks
         */
        public Npc makeNpc(int usnLevel, int[] usnLoc, int[] usnStats, List <CharClasses> usnUnlocked,  List <CharSkills> usnSkills, List<CharSpells> usnSpells, List<CharPerks> usnPerks)
        {
            Npc newNpc = new Npc(makeName(5), "Simple Farmer", selClass(usnUnlocked), usnStats, selSkills(usnSkills, 1), selSpells(usnSpells, 1), selPerks(usnPerks,1));
            return newNpc;
        }

        /* Location is based on user level, userClass, unlocked monsters, unlocked npcs, and unlocked equipment user stats
         */
        public Loc makeLoc(int usnLevel, String usnClass, List <Monsters> mon, List <Npc> npc, List<Eqp> eqp)
        {
            int[] origin= new int[3];
            int[] locSize = new int[2] {usnLevel,usnLevel};
            var randomn= new Random((int) DateTime.Now.Ticks);

            for (int i=0;i<origin.Count();i++)
            {
                origin[i] = randomn.Next(0,usnLevel+1);
            }

            Loc newLoc = new Loc(makeName(7), "Simple Farm", origin, locSize, selMons(mon, usnLevel*5), selNpcs(npc, usnLevel), selEqps(eqp, usnLevel));
            return newLoc;
        }

        public Monsters makeMon(int usnLevel, int[] usnStats, List<CharSkills> csk, List<CharClasses> cls, List<CharSpells> csp, List<CharPerks> prk)
        {
            int[] stats = new int[4];
            var randomN = new Random((int) DateTime.Now.Ticks);
            for (int i=0;i<stats.Count();i++)
            {
                stats[i] = randomN.Next(usnLevel, usnLevel+5);
            }

            Monsters newMon = new Monsters(makeName(6), "Simple Monster", "Neutral", stats, selSkills(csk, 2), selSpells(csp, 2), selPerks(prk, 1));
                                            
            return newMon;
        }

        public Eqp makeEqp(int usnLevel, int[] usnStats, String usnClass, List<CharSkills> csk, List<CharSpells> csp, List<CharPerks> prk)
        {
            int[] sBonus = new int[4];
            var randomN = new Random((int) DateTime.Now.Ticks);

            for (int i=0; i<sBonus.Count();i++)
            {
                sBonus[i] = randomN.Next(0, (int)usnLevel / 2);
            }

            Eqp newEqp = new Eqp(makeName(8), "Simple Sword", "Lmited to" + usnLevel, sBonus, selSkills(csk, 1), selSpells(csp, 1), selPerks(prk, 1));
            return newEqp;
        }



    }

    class Player
    {
        int playerID { get; set; }
        public int playerLvl { get; set; }
        public String playerName { get; set; }
        public String playerClass { get; set; }
        public int[] playerStats { get; set; }
        public int[] location = new int[3] { 0, 0, 0 };
        public String[] playerSkills { get; set; }
        public String[] playerSpells { get; set; }
        public String[] playerPerks { get; set; }

        public Player(String name, String pClass)
        {
            int[] stats = new int[4];
            var randomN = new Random((int) DateTime.Now.Ticks);
            playerName = name;
            playerClass = pClass;

            for (int i=0; i<stats.Count();i++)
            {
                stats[i] = randomN.Next(5, 10);
            }

            playerStats = stats;
        }

    }

    class Quest
    {
        int questID = 0;
        
        String subject { get; set; }
        String content { get; set; }
        String reward  { get; set; }
        String req { get; set; }

        public Quest (Npc n, Obj o, Loc l, Eqp e, Monsters m)
        {
            questID = questID + 1;
            subject = "Get " + o.objName + " from " + m.monName + " for " + n.npcName + " at " + l.locName;
            content = "Retrieve the following above from " + m.monName;
            reward = e.eqpName + " and 1000 exp";
            req = "Requirement: Character Level >5 ";
        }

        public void dispQuestInfo()
        {
            Console.WriteLine("QuestID: " + questID);
            Console.WriteLine("Subject: " + subject);
            Console.WriteLine("Content: " + content);
            Console.WriteLine("Reward: " + reward);
            Console.WriteLine("Requirements: " + req);
        }
    }

    class Npc
    {
        int npcID = 0;
        public String npcName {get; set;}
        String npcDesc { get; set; }
        String npcClass { get; set; }
        int[] npcStats { get; set; }
        String[] npcSkills { get; set; }
        String[] npcSpells { get; set; }
        String[] npcPerks { get; set; }

        public Npc()
        {
            npcName = "One";
        }
        
        public Npc(String name, String desc, String npClass, int[] stats, String[] npSkills, String[] npSpells, String[] npPerks)
        {
            npcID = npcID + 1;
            npcName = name; npcDesc = desc; npcClass = npClass; npcStats = stats;
            npcSkills = npSkills; npcSpells = npSpells; npcPerks = npPerks;
        }

        public void dispNPCInfo()
        {
            String dispString = String.Format("NpcID: {0} \nNpcName: {1} \nNpcDesc: {2} \nNpcClass: {3} \nNpcStats: {4}, {5}, {6}, {7}", 
                                        npcID, npcName, npcDesc, npcClass, npcStats[0].ToString(), npcStats[1].ToString(), npcStats[2].ToString(), npcStats[3].ToString());
            Console.WriteLine(dispString);
            dispList("Skills", npcSkills);
            dispList("Spells", npcSpells);
            dispList("Perks", npcPerks);
        }

        private void dispList(String listTitle, String[] list)
        {
            Console.WriteLine("List of " + listTitle);
            for (int i=0;i<list.Count();i++)
            {
                Console.WriteLine(list[i]);
            }
        }
    }

    class Obj
    {
        int objID { get; set; }
        public String objName { get; set; }
        String objDesc { get; set; }
        String objRestrictions { get; set; }
        int[] objStats { get; set; }
        String[] objEffects { get; set; }
        String[] objPerks { get; set; }
    }

    class Loc
    {
        int locID { get; set; }
        public String locName { get; set; }
        String locDesc { get; set; }
        int[] locOrigin { get; set; }
        int[] locSize {get; set;}
        String[] monList {get; set;}
        String[] npcList {get; set;}
        String[] treasureList {get; set;}

        public Loc()
        {
            locName = "Home";
        }

        public Loc (String name, String desc, int[] xyz, int[] locaSize, String[] mList, String[] nList, String[] eList)
        {
            locID = locID + 1;
            locName = name; locDesc = desc; locOrigin = xyz; locSize = locaSize; monList = mList; npcList = nList; treasureList = eList;
        }

        public void dispLocInfo()
        {
            Console.WriteLine("LocID:" + locID);
            Console.WriteLine("LocName:" + locName);
            Console.WriteLine("LocDesc:" + locDesc);
            Console.WriteLine(String.Format("LocOrigin: {0}, {1}, {2}", locOrigin[0].ToString(), locOrigin[1].ToString(), locOrigin[2].ToString()));
            Console.WriteLine("LocSize:" + locSize[0] + " by " + locSize[1]);

            dispList("Monsters", monList);
            dispList("NPCs", npcList);
            dispList("Treasures", treasureList);

        }

        private void dispList(String listTitle, String[] list)
        {
            Console.WriteLine("List of " + listTitle);
            for (int i=0;i<list.Count();i++)
            {
                Console.WriteLine(list[i]);
            }
        }
    }

    class Eqp
    {
        int eqpID { get; set; }
        public String eqpName { get; set; }
        String eqpDesc { get; set; }
        String eqpLimit { get; set; }
        int[] eqpStats { get; set; }
        String[] eqpSkill { get; set; }
        String[] eqpSpell { get; set; }
        String[] eqpPerk { get; set; }

        public Eqp()
        {
            eqpName = "Weapon";
        }

        public Eqp (String name, String desc, String limit, int[] reqStat, String[] skills, String[] spells, String[] perks)
        {
            eqpName = name; eqpDesc = desc; eqpLimit = limit; eqpStats = reqStat; eqpSkill = skills; eqpSpell = spells; eqpPerk = perks;
        }

        public void dispEqpInfo()
        {
            String toDisp = String.Format("EqpID: {0} \nName: {1} \nEqpDesc: {2} \nEqpLimit: {3} \nEqpStats: {4} {5} {6} {7}",
                                            eqpID, eqpName, eqpDesc, eqpLimit, eqpStats[0].ToString(), eqpStats[1].ToString(), eqpStats[2].ToString(), eqpStats[3].ToString());
            dispList("Eqp Skills", eqpSkill);
            dispList("Eqp Spells", eqpSpell);
            dispList("Eqp Perks", eqpPerk);
        }

        private void dispList(String listTitle, String[] list)
        {
            Console.WriteLine("List of " + listTitle);
            for (int i = 0; i < list.Count(); i++)
            {
                Console.WriteLine(list[i]);
            }
        }
    }

    class Monsters
    {
        int monID { get; set; }
        public String monName { get; set; }
        String monDesc { get; set; }
        String monType { get; set; }
        int[] monStats { get; set; }
        String[] monSkill { get; set; }
        String[] monSpell { get; set; }
        String[] monPerk { get; set; }

        public Monsters()
        {
            monName = "Sin";
        }

        public Monsters(String name, String desc, String type, int[] stat, String[] csk, String[] csp, String[] cprk)
        {
            monID = monID + 1;
            monName = name; monDesc = desc; monType = type; monStats = stat;
            monSkill=csk; monSpell=csp; monPerk=cprk;
        }

        public void dispMonInfo()
        {
            String toDisp = String.Format("MonID: {0} \nName: {1} \nMonDesc: {2} \nMonLimit: {3} \nMonStats: {4} {5} {6} {7}",
                                            monID, monName, monDesc, monType, monStats[0], monStats[1], monStats[2], monStats[3]);
            dispList("Monster Skills", monSkill);
            dispList("Monster Spells", monSpell);
            dispList("Monster Perks", monPerk);
        }

        private void dispList(String listTitle, String[] list)
        {
            Console.WriteLine("List of " + listTitle);
            for (int i = 0; i < list.Count(); i++)
            {
                Console.WriteLine(list[i]);
            }
        }
    }

    class CharClasses
    {
        String classID { get; set; }
        public String className { get; set; }
        int[] classBoosts { get; set; }

        public CharClasses (String cName)
       {
            className = cName;
        }
    }

    class CharSkills
    {
        String skillID { get; set; }
        public String skillName { get; set; }
        String skillDesc { get; set; }
        String skillDmg { get; set; }
        String skillEffect { get; set; }
        String skillType { get; set; }
        String skillCircle { get; set; }

        public CharSkills (String sName)
        {
            skillName = sName;
        }
    }

    class CharSpells
    {
        String spellID { get; set; }
        public String spellName { get; set; }
        String spellDesc { get; set; }
        String spellDmg { get; set; }
        String spellEffect { get; set; }
        String spellType { get; set; }
        String spellCircle { get; set; }

        public CharSpells (String sName)
        {
            spellName = sName;
        }
    }

    class CharPerks
    {
        String perkID { get; set; }
        public String perkName { get; set; }
        String perkDesc { get; set; }
        String perkDmg { get; set; }
        String perkEffect { get; set; }
        String perkType { get; set; }
        String perkState { get; set; }

        public CharPerks (String pName)
        {
            perkName = pName;
        }
    }
}
