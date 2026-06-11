namespace Engine;

public static class World
{
    public static readonly List<Item> Items = [];
    public static readonly List<Monster> Monsters = [];
    public static readonly List<Quest> Quests = [];
    public static readonly List<Location> Locations = [];
    public static readonly List<Spell> Spells = [];
    public static readonly List<Buff> Buffs = [];
    public static readonly List<Race> Races = [];

    // ITEMS: INDEX - 18
    public const int ITEM_ID_RUSTY_SWORD = 1;
    public const int ITEM_ID_CLUB = 6;
    public const int ITEM_ID_LEGENDARY_SWORD = 11;
    public const int ITEM_ID_MAGICAL_BOW = 12;
    public const int ITEM_ID_IRON_SWORD = 18;
    public const int ITEM_ID_CLOTH_ARMOUR = 16;
    public const int ITEM_ID_METAL_ARMOUR = 17;
    public const int ITEM_ID_RAT_TAIL = 2;
    public const int ITEM_ID_PIECE_OF_FUR = 3;
    public const int ITEM_ID_SNAKE_FANG = 4;
    public const int ITEM_ID_SNAKESKIN = 5;
    public const int ITEM_ID_SPIDER_FANG = 8;
    public const int ITEM_ID_SPIDER_SILK = 9;
    public const int ITEM_ID_DRAGON_SKULL = 13;
    public const int ITEM_ID_DEATHCLAW_HAND = 14;
    public const int ITEM_ID_HEALING_POTION = 7;
    public const int ITEM_ID_ADVANCED_HEALING_POTION = 15;
    public const int ITEM_ID_ADVENTURER_PASS = 10;

    // Monsters INDEX: 6
    public const int MONSTER_ID_RAT = 1;
    public const int MONSTER_ID_SNAKE = 2;
    public const int MONSTER_ID_GIANT_SPIDER = 3;
    public const int MONSTER_ID_DRAGON = 4;
    public const int MONSTER_ID_MAGICAN = 5;
    public const int MONSTER_ID_DEATHCLAW = 6;

    // QUESTS INDEX: 3
    public const int QUEST_ID_CLEAR_ALCHEMIST_GARDEN = 1;
    public const int QUEST_ID_CLEAR_FARMERS_FIELD = 2;
    public const int QUEST_ID_HIDDEN_TREASURE = 3;

    // Locations INDEX: 9
    public const int LOCATION_ID_HOME = 1;
    public const int LOCATION_ID_TOWN_SQUARE = 2;
    public const int LOCATION_ID_GUARD_POST = 3;
    public const int LOCATION_ID_ALCHEMIST_HUT = 4;
    public const int LOCATION_ID_ALCHEMISTS_GARDEN = 5;
    public const int LOCATION_ID_FARMHOUSE = 6;
    public const int LOCATION_ID_FARM_FIELD = 7;
    public const int LOCATION_ID_BRIDGE = 8;
    public const int LOCATION_ID_SPIDER_FIELD = 9;

    // Spell effects INDEX: 7
    public const int SPELL_EFFECT_ID_DAMAGE = 1;
    public const int SPELL_EFFECT_ID_HEAL = 2;
    public const int SPELL_EFFECT_ID_RAISE_HP = 3;
    public const int SPELL_EFFECT_ID_RAISE_STRENGTH = 4;
    public const int SPELL_EFFECT_ID_RESTORE_MANA = 5;
    public const int SPELL_EFFECT_ID_TELEPORT = 6;
    public const int SPELL_EFFECT_ID_INVISIBLE = 7;

    // SPELLS INDEX: 7
    public const int SPELL_ID_FIREBALL = 1;
    public const int SPELL_ID_FROSTICE = 2;
    public const int SPELL_ID_TELEPORT = 3;
    public const int SPELL_ID_ENDURANCE = 4;
    public const int SPELL_ID_HOLY_NOVA = 5;
    public const int SPELL_ID_RESTORE_HEALTH = 6;
    public const int SPELL_ID_RESTORE_MANA = 7;

    // BUFFS INDEX: 5
    public const int BUFF_ID_INCREASE_MAX_HP = 1;
    public const int BUFF_ID_INCREASE_STR = 2;
    public const int BUFF_ID_INCREASE_DEX = 3;
    public const int BUFF_ID_INCREASE_INT = 4;
    public const int BUFF_ID_INCREASE_MAX_MANA = 5;

    // RACES INDEX
    public const int RACE_ID_HUNMAN = 1;
    public const int RACE_ID_ELF = 2;
    public const int RACE_ID_ORC = 3;
    public const int RACE_ID_GOBLIN = 4;
    public const int RACE_ID_DWARF = 5;
    public const int RACE_ID_MONSTER = 6;
    public const int RACE_ID_ANIMAL = 7;

    static World()
    {
        PopulateRaces();
        PopulateItems();
        PopulateBuffs();
        PopulateSpells();
        PopulateMonsters();
        PopulateQuests();
        PopulateLocations();
    }

    private static void PopulateRaces()
    {
        Races.Add(new Race(RACE_ID_HUNMAN, "Human", "Humans are smart but naive"));
        Races.Add(new Race(RACE_ID_ELF, "Elf", "Elves are skilled with magic and crafting"));
        Races.Add(new Race(RACE_ID_GOBLIN, "Goblin", "Goblins are physically larger and stronger but lack intelligence"));
        Races.Add(new Race(RACE_ID_ORC, "Orc", "Strong and can endure in battle more than anyone else. Their mines are usually the best and the largest of all"));
        Races.Add(new Race(RACE_ID_DWARF, "Dwarf", "A dwarf may be smaller than most but sure is agile, some of them are the best archers in the world"));
        Races.Add(new Race(RACE_ID_MONSTER, "Monster", "Some monster can be really deadly"));
        Races.Add(new Race(RACE_ID_ANIMAL, "Animal", "Not all animls are edible"));
    }

    private static void PopulateBuffs()
    {
        Buffs.Add(new Buff(BUFF_ID_INCREASE_MAX_HP, "Endurance", "Increases your maximum hit points"));
        Buffs.Add(new Buff(BUFF_ID_INCREASE_DEX, "Dexterity Buff", "Increases your dexterity stat"));
        Buffs.Add(new Buff(BUFF_ID_INCREASE_INT, "Intelligent Buff", "Increases your intelligent stat"));
    }

    private static void PopulateSpells()
    {
        Spells.Add(new Spell(SPELL_ID_FIREBALL,       "Fireball",      5,  SPELL_EFFECT_ID_DAMAGE,       15,  true));
        Spells.Add(new Spell(SPELL_ID_FROSTICE,       "Frostice",      5,  SPELL_EFFECT_ID_DAMAGE,       15,  true));
        Spells.Add(new Spell(SPELL_ID_HOLY_NOVA,      "Holy Nova",     50, SPELL_EFFECT_ID_DAMAGE,       100, true));
        Spells.Add(new Spell(SPELL_ID_RESTORE_HEALTH, "Heal",          10, SPELL_EFFECT_ID_HEAL,         10,  true));
        Spells.Add(new Spell(SPELL_ID_RESTORE_MANA,   "Restore Mana",  0,  SPELL_EFFECT_ID_RESTORE_MANA, 20,  true));
        Spells.Add(new Spell(SPELL_ID_ENDURANCE,      "Endurance",     30, SPELL_EFFECT_ID_RAISE_HP,     100, false));
    }

    private static void PopulateItems()
    {
        Items.Add(new Weapon(ITEM_ID_RUSTY_SWORD,     "Rusty sword",    "Rusty swords",    0,  5));
        Items.Add(new Weapon(ITEM_ID_MAGICAL_BOW,     "Magical Bow",    "Magical bows",    5,  10));
        Items.Add(new Weapon(ITEM_ID_CLUB,            "Club",           "Clubs",           3,  10));
        Items.Add(new Weapon(ITEM_ID_IRON_SWORD,      "Iron Sword",     "Iron Swords",     3,  7));
        Items.Add(new Weapon(ITEM_ID_LEGENDARY_SWORD, "Legendary Sword","Legendary Swords",50, 100));
        Items.Add(new Armour(ITEM_ID_CLOTH_ARMOUR,    "Cloth Armour",   "Cloth Armour",    5,  0));
        Items.Add(new Armour(ITEM_ID_METAL_ARMOUR,    "Metal Armour",   "Metal Armours",   20, 10));
        Items.Add(new Item(ITEM_ID_RAT_TAIL,          "Rat tail",       "Rat tails"));
        Items.Add(new Item(ITEM_ID_PIECE_OF_FUR,      "Piece of fur",   "Pieces of fur"));
        Items.Add(new Item(ITEM_ID_SNAKE_FANG,        "Snake fang",     "Snake fangs"));
        Items.Add(new Item(ITEM_ID_SNAKESKIN,         "Snakeskin",      "Snakeskins"));
        Items.Add(new Item(ITEM_ID_SPIDER_FANG,       "Spider fang",    "Spider fangs"));
        Items.Add(new Item(ITEM_ID_SPIDER_SILK,       "Spider silk",    "Spider silks"));
        Items.Add(new Item(ITEM_ID_DRAGON_SKULL,      "Dragon skull",   "Dragon skulls"));
        Items.Add(new Item(ITEM_ID_DEATHCLAW_HAND,    "Deathclaw hand", "Deathclaw hands"));
        Items.Add(new HealingPotion(ITEM_ID_HEALING_POTION,          "Healing potion",          "Healing potions",          5));
        Items.Add(new HealingPotion(ITEM_ID_ADVANCED_HEALING_POTION, "Advanced healing potion", "Advanced healing potions", 25));
        Items.Add(new Item(ITEM_ID_ADVENTURER_PASS,   "Adventurer pass","Adventurer passes"));
    }

    private static void PopulateMonsters()
    {
        Monster rat = new(MONSTER_ID_RAT, "Rat", 5, 3, 10, 3, 3, 1, 2, 5, 1, 0, 0, RaceByID(RACE_ID_ANIMAL)!);
        rat.LootTable.Add(new LootItem(ItemByID(ITEM_ID_RAT_TAIL)!,    75, false));
        rat.LootTable.Add(new LootItem(ItemByID(ITEM_ID_PIECE_OF_FUR)!, 75, true));

        Monster snake = new(MONSTER_ID_SNAKE, "Snake", 5, 3, 10, 5, 5, 1, 1, 5, 1, 0, 0, RaceByID(RACE_ID_ANIMAL)!);
        snake.LootTable.Add(new LootItem(ItemByID(ITEM_ID_SNAKE_FANG)!, 75, false));
        snake.LootTable.Add(new LootItem(ItemByID(ITEM_ID_SNAKESKIN)!,  75, true));

        Monster giantSpider = new(MONSTER_ID_GIANT_SPIDER, "Giant spider", 20, 5, 40, 10, 10, 7, 7, 2, 4, 10, 10,
            RaceByID(RACE_ID_MONSTER)!, (Armour)ItemByID(ITEM_ID_METAL_ARMOUR)!);
        giantSpider.LootTable.Add(new LootItem(ItemByID(ITEM_ID_SPIDER_FANG)!, 75, true));
        giantSpider.LootTable.Add(new LootItem(ItemByID(ITEM_ID_SPIDER_SILK)!, 25, false));

        Monster dragon = new(MONSTER_ID_DRAGON, "Dragon", 30, 100, 100, 75, 75, 10, 10, 10, 8, 50, 50, RaceByID(RACE_ID_MONSTER)!);
        dragon.LootTable.Add(new LootItem(ItemByID(ITEM_ID_DRAGON_SKULL)!, 100, true));

        Monster magican = new(MONSTER_ID_MAGICAN, "Magican", 5, 50, 150, 35, 35, 5, 2, 2, 10, 500, 500,
            RaceByID(RACE_ID_ELF)!, (Armour)ItemByID(ITEM_ID_CLOTH_ARMOUR)!);
        magican.LootTable.Add(new LootItem(ItemByID(ITEM_ID_ADVANCED_HEALING_POTION)!, 75, true));
        magican.Spells.Add(new SpellList(SpellByID(SPELL_ID_FIREBALL)!));
        magican.Spells.Add(new SpellList(SpellByID(SPELL_ID_FROSTICE)!));

        Monster deathclaw = new(MONSTER_ID_DEATHCLAW, "Deathclaw", 250, 500, 1000, 250, 250, 25, 15, 15, 5, 0, 0, RaceByID(RACE_ID_MONSTER)!);
        deathclaw.LootTable.Add(new LootItem(ItemByID(ITEM_ID_DEATHCLAW_HAND)!, 100, true));

        Monsters.Add(rat);
        Monsters.Add(snake);
        Monsters.Add(giantSpider);
        Monsters.Add(magican);
        Monsters.Add(dragon);
        Monsters.Add(deathclaw);
    }

    private static void PopulateQuests()
    {
        Quest clearAlchemistGarden = new(
            QUEST_ID_CLEAR_ALCHEMIST_GARDEN,
            "Clear the alchemist's garden",
            "Kill rats in the alchemist's garden and bring back 3 rat tails. You will receive a healing potion and 10 gold pieces.",
            20, 10);
        clearAlchemistGarden.QuestCompletionItems.Add(new QuestCompletionItem(ItemByID(ITEM_ID_RAT_TAIL)!, 3));
        clearAlchemistGarden.RewardItem = ItemByID(ITEM_ID_HEALING_POTION);

        Quest clearFarmersField = new(
            QUEST_ID_CLEAR_FARMERS_FIELD,
            "Clear the farmer's field",
            "Kill snakes in the farmer's field and bring back 3 snake fangs. You will receive an adventurer's pass and 20 gold pieces.",
            20, 20);
        clearFarmersField.QuestCompletionItems.Add(new QuestCompletionItem(ItemByID(ITEM_ID_SNAKE_FANG)!, 3));
        clearFarmersField.QuestCompletionItems.Add(new QuestCompletionItem(ItemByID(ITEM_ID_SNAKESKIN)!,  2));
        clearFarmersField.RewardItem = ItemByID(ITEM_ID_ADVENTURER_PASS);

        Quest findTheHiddenTreasure = new(
            QUEST_ID_HIDDEN_TREASURE,
            "Find the hidden chest",
            "There are rumors of hidden treasures somewhere, I need to find it",
            30, 100,
            ItemByID(ITEM_ID_LEGENDARY_SWORD));
        findTheHiddenTreasure.QuestCompletionItems.Add(new QuestCompletionItem(ItemByID(ITEM_ID_DEATHCLAW_HAND)!, 1));

        Quests.Add(clearAlchemistGarden);
        Quests.Add(clearFarmersField);
        Quests.Add(findTheHiddenTreasure);
    }

    private static void PopulateLocations()
    {
        Location home        = new(LOCATION_ID_HOME,            "Home",             "Your house. You really need to clean up the place.");
        Location townSquare  = new(LOCATION_ID_TOWN_SQUARE,     "Town square",      "You see a fountain.");
        Location alchemistHut= new(LOCATION_ID_ALCHEMIST_HUT,   "Alchemist's hut",  "There are many strange plants on the shelves.");
        alchemistHut.QuestAvailableHere = QuestByID(QUEST_ID_CLEAR_ALCHEMIST_GARDEN);

        Location alchemistsGarden = new(LOCATION_ID_ALCHEMISTS_GARDEN, "Alchemist's garden", "Many plants are growing here.");
        alchemistsGarden.MonsterLivingHere = MonsterByID(MONSTER_ID_RAT);

        Location farmhouse   = new(LOCATION_ID_FARMHOUSE, "Farmhouse", "There is a small farmhouse, with a farmer in front.");
        farmhouse.QuestAvailableHere = QuestByID(QUEST_ID_CLEAR_FARMERS_FIELD);

        Location farmersField = new(LOCATION_ID_FARM_FIELD, "Farmer's field", "You see rows of vegetables growing here.");
        farmersField.MonsterLivingHere = MonsterByID(MONSTER_ID_SNAKE);

        Location guardPost   = new(LOCATION_ID_GUARD_POST, "Guard post", "There is a large, tough-looking guard here.",
            ItemByID(ITEM_ID_ADVENTURER_PASS));
        guardPost.MonsterLivingHere = MonsterByID(MONSTER_ID_MAGICAN);

        Location bridge      = new(LOCATION_ID_BRIDGE,      "Bridge", "A stone bridge crosses a wide river.");
        bridge.QuestAvailableHere = QuestByID(QUEST_ID_HIDDEN_TREASURE);

        Location spiderField = new(LOCATION_ID_SPIDER_FIELD, "Forest", "You see spider webs covering the trees in this forest.",
            levelRequirement: 5);
        spiderField.MonsterLivingHere = MonsterByID(MONSTER_ID_GIANT_SPIDER);

        home.LocationToNorth = townSquare;

        townSquare.LocationToNorth = alchemistHut;
        townSquare.LocationToSouth = home;
        townSquare.LocationToEast  = guardPost;
        townSquare.LocationToWest  = farmhouse;

        farmhouse.LocationToEast = townSquare;
        farmhouse.LocationToWest = farmersField;

        farmersField.LocationToEast = farmhouse;

        alchemistHut.LocationToSouth = townSquare;
        alchemistHut.LocationToNorth = alchemistsGarden;

        alchemistsGarden.LocationToSouth = alchemistHut;

        guardPost.LocationToEast = bridge;
        guardPost.LocationToWest = townSquare;

        bridge.LocationToWest = guardPost;
        bridge.LocationToEast = spiderField;

        spiderField.LocationToWest = bridge;

        Locations.Add(home);
        Locations.Add(townSquare);
        Locations.Add(guardPost);
        Locations.Add(alchemistHut);
        Locations.Add(alchemistsGarden);
        Locations.Add(farmhouse);
        Locations.Add(farmersField);
        Locations.Add(bridge);
        Locations.Add(spiderField);
    }

    public static Item?     ItemByID(int id)     => Items.FirstOrDefault(i => i.ID == id);
    public static Spell?    SpellByID(int id)    => Spells.FirstOrDefault(s => s.ID == id);
    public static Race?     RaceByID(int id)     => Races.FirstOrDefault(r => r.ID == id);
    public static Monster?  MonsterByID(int id)  => Monsters.FirstOrDefault(m => m.ID == id);
    public static Quest?    QuestByID(int id)    => Quests.FirstOrDefault(q => q.ID == id);
    public static Buff?     BuffByID(int id)     => Buffs.FirstOrDefault(b => b.ID == id);
    public static Location? LocationByID(int id) => Locations.FirstOrDefault(l => l.ID == id);
}
