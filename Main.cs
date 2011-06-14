// Type: Terraria.Main
// Assembly: TerrariaServer, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// Assembly location: C:\Temp\New Folder\TerrariaServer.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace Terraria
{
    public class Main : Game
    {
        public static int curRelease = 9;
        public static string versionNumber = "v1.0.4";
        public static string versionNumber2 = "v1.0.4";
        public static bool grabSun = false;
        public static bool stopSpawns = false;
        public static bool skipMenu = false;
        public static bool lightTiles = false;
        public static bool verboseNetplay = false;
        public static bool stopTimeOuts = false;
        public static bool showSpam = false;
        public static bool showItemOwner = false;
        public static bool showSplash = true;
        public static bool ignoreErrors = true;
        public static int alwaysSpawn = 0;
        public static string defaultIP = "";
        public static int maxScreenW = 1920;
        public static int minScreenW = 800;
        public static int maxScreenH = 1200;
        public static int minScreenH = 600;
        private static Stopwatch saveTime = new Stopwatch();
        public static MouseState mouseState = Mouse.GetState();
        public static MouseState oldMouseState = Mouse.GetState();
        public static KeyboardState keyState = Keyboard.GetState();
        public static bool gamePaused = false;
        public static int updateTime = 0;
        public static int drawTime = 0;
        public static int frameRate = 0;
        public static bool frameRelease = false;
        public static bool showFrameRate = false;
        public static int magmaBGFrame = 0;
        public static int magmaBGFrameCounter = 0;
        public static int saveTimer = 0;
        public static bool autoJoin = false;
        public static bool serverStarting = false;
        public static float leftWorld = 0.0f;
        public static float rightWorld = 134400f;
        public static float topWorld = 0.0f;
        public static float bottomWorld = 38400f;
        public static int maxTilesX = (int)Main.rightWorld / 16 + 1;
        public static int maxTilesY = (int)Main.bottomWorld / 16 + 1;
        public static int maxSectionsX = Main.maxTilesX / 200;
        public static int maxSectionsY = Main.maxTilesY / 150;
        public static int maxNetPlayers = (int)byte.MaxValue;
        public static float caveParrallax = 1f;
        public static Liquid[] liquid = new Liquid[Liquid.resLiquid];
        public static LiquidBuffer[] liquidBuffer = new LiquidBuffer[10000];
        public static bool dedServ = false;
        public static int spamCount = 0;
        public static bool autoSave = true;
        public static string statusText = "";
        public static string worldName = "";
        public static int background = 0;
        public static Color[] teamColor = new Color[5];
        public static bool dayTime = true;
        public static double time = 13500.0;
        public static int moonPhase = 0;
        public static short sunModY = (short)0;
        public static short moonModY = (short)0;
        public static bool grabSky = false;
        public static bool bloodMoon = false;
        public static int checkForSpawns = 0;
        public static int helpText = 0;
        public static bool autoGen = false;
        public static bool autoPause = false;
        public static int cloudLimit = 100;
        public static int numClouds = Main.cloudLimit;
        public static float windSpeed = 0.0f;
        public static float windSpeedSpeed = 0.0f;
        public static Cloud[] cloud = new Cloud[100];
        public static bool resetClouds = true;
        public static int fadeCounter = 0;
        public static Texture2D[] armorHeadTexture = new Texture2D[27];
        public static Texture2D[] armorBodyTexture = new Texture2D[16];
        public static Texture2D[] armorArmTexture = new Texture2D[16];
        public static Texture2D[] armorLegTexture = new Texture2D[15];
        public static Texture2D[] itemTexture = new Texture2D[265];
        public static Texture2D[] npcTexture = new Texture2D[59];
        public static Texture2D[] projectileTexture = new Texture2D[39];
        public static Texture2D[] goreTexture = new Texture2D[86];
        public static Texture2D[] tileTexture = new Texture2D[80];
        public static Texture2D[] wallTexture = new Texture2D[14];
        public static Texture2D[] backgroundTexture = new Texture2D[7];
        public static Texture2D[] cloudTexture = new Texture2D[4];
        public static Texture2D[] starTexture = new Texture2D[5];
        public static Texture2D[] liquidTexture = new Texture2D[2];
        public static Texture2D[] treeTopTexture = new Texture2D[3];
        public static Texture2D[] treeBranchTexture = new Texture2D[3];
        public static Texture2D[] playerHairTexture = new Texture2D[17];
        public static SoundEffect[] soundDig = new SoundEffect[3];
        public static SoundEffectInstance[] soundInstanceDig = new SoundEffectInstance[3];
        public static SoundEffect[] soundTink = new SoundEffect[3];
        public static SoundEffectInstance[] soundInstanceTink = new SoundEffectInstance[3];
        public static SoundEffect[] soundPlayerHit = new SoundEffect[3];
        public static SoundEffectInstance[] soundInstancePlayerHit = new SoundEffectInstance[3];
        public static SoundEffect[] soundFemaleHit = new SoundEffect[3];
        public static SoundEffectInstance[] soundInstanceFemaleHit = new SoundEffectInstance[3];
        public static SoundEffect[] soundItem = new SoundEffect[17];
        public static SoundEffectInstance[] soundInstanceItem = new SoundEffectInstance[17];
        public static SoundEffect[] soundNPCHit = new SoundEffect[4];
        public static SoundEffectInstance[] soundInstanceNPCHit = new SoundEffectInstance[4];
        public static SoundEffect[] soundNPCKilled = new SoundEffect[4];
        public static SoundEffectInstance[] soundInstanceNPCKilled = new SoundEffectInstance[4];
        public static SoundEffect[] soundZombie = new SoundEffect[3];
        public static SoundEffectInstance[] soundInstanceZombie = new SoundEffectInstance[3];
        public static SoundEffect[] soundRoar = new SoundEffect[2];
        public static SoundEffectInstance[] soundInstanceRoar = new SoundEffectInstance[2];
        public static SoundEffect[] soundSplash = new SoundEffect[2];
        public static SoundEffectInstance[] soundInstanceSplash = new SoundEffectInstance[2];
        public static Cue[] music = new Cue[7];
        public static float[] musicFade = new float[7];
        public static float musicVolume = 0.75f;
        public static float soundVolume = 1f;
        public static bool[] wallHouse = new bool[14];
        public static bool[] tileStone = new bool[80];
        public static bool[] tileWaterDeath = new bool[80];
        public static bool[] tileLavaDeath = new bool[80];
        public static bool[] tileTable = new bool[80];
        public static bool[] tileBlockLight = new bool[80];
        public static bool[] tileDungeon = new bool[80];
        public static bool[] tileSolidTop = new bool[80];
        public static bool[] tileSolid = new bool[80];
        public static bool[] tileNoAttach = new bool[80];
        public static bool[] tileNoFail = new bool[80];
        public static bool[] tileFrameImportant = new bool[80];
        public static int[] backgroundWidth = new int[7];
        public static int[] backgroundHeight = new int[7];
        public static bool tilesLoaded = false;
        public static Tile[,] tile = new Tile[Main.maxTilesX, Main.maxTilesY];
        public static Dust[] dust = new Dust[2000];
        public static Star[] star = new Star[130];
        public static Item[] item = new Item[201];
        public static NPC[] npc = new NPC[1001];
        public static Gore[] gore = new Gore[201];
        public static Projectile[] projectile = new Projectile[1001];
        public static CombatText[] combatText = new CombatText[100];
        public static Chest[] chest = new Chest[1000];
        public static Sign[] sign = new Sign[1000];
        public static int screenWidth = 800;
        public static int screenHeight = 600;
        public static int chatLength = 600;
        public static bool chatMode = false;
        public static bool chatRelease = false;
        public static int numChatLines = 7;
        public static string chatText = "";
        public static ChatLine[] chatLine = new ChatLine[Main.numChatLines];
        public static bool inputTextEnter = false;
        public static float[] hotbarScale = new float[10]
    {
      1f,
      0.75f,
      0.75f,
      0.75f,
      0.75f,
      0.75f,
      0.75f,
      0.75f,
      0.75f,
      0.75f
    };
        public static byte mouseTextColor = (byte)0;
        public static int mouseTextColorChange = 1;
        public static bool mouseLeftRelease = false;
        public static bool mouseRightRelease = false;
        public static bool playerInventory = false;
        public static int stackCounter = 0;
        public static int stackDelay = 7;
        public static Item mouseItem = new Item();
        private static float inventoryScale = 0.75f;
        public static bool hasFocus = true;
        public static Recipe[] recipe = new Recipe[Recipe.maxRecipes];
        public static int[] availableRecipe = new int[Recipe.maxRecipes];
        public static float[] availableRecipeY = new float[Recipe.maxRecipes];
        public static int myPlayer = 0;
        public static Player[] player = new Player[256];
        public static bool npcChatRelease = false;
        public static bool editSign = false;
        public static string signText = "";
        public static string npcChatText = "";
        public static bool npcChatFocus1 = false;
        public static bool npcChatFocus2 = false;
        public static int npcShop = 0;
        private static Item toolTip = new Item();
        private static int backSpaceCount = 0;
        public static string motd = "";
        public static bool gameMenu = true;
        public static Player[] loadPlayer = new Player[5];
        public static string[] loadPlayerPath = new string[5];
        private static int numLoadPlayers = 0;
        public static string[] loadWorld = new string[999];
        public static string[] loadWorldPath = new string[999];
        private static int numLoadWorlds = 0;
        public static string SavePath = "data";
        public static string WorldPath = Main.SavePath + "/Worlds";
        public static string PlayerPath = Main.SavePath + "/Players";
        public static int invasionType = 0;
        public static double invasionX = 0.0;
        public static int invasionSize = 0;
        public static int invasionDelay = 0;
        public static int invasionWarn = 0;
        public static int[] npcFrameCount = new int[59]
    {
      1,
      2,
      2,
      3,
      6,
      2,
      2,
      1,
      1,
      1,
      1,
      1,
      1,
      1,
      1,
      1,
      2,
      16,
      14,
      16,
      14,
      15,
      16,
      2,
      10,
      1,
      16,
      16,
      16,
      3,
      1,
      15,
      3,
      1,
      3,
      1,
      1,
      16,
      16,
      1,
      1,
      1,
      3,
      3,
      15,
      3,
      7,
      7,
      4,
      5,
      5,
      5,
      3,
      3,
      16,
      6,
      3,
      6,
      6
    };
        private static bool mouseExit = false;
        private static float exitScale = 0.8f;
        public static Player clientPlayer = new Player();
        public static string getIP = Main.defaultIP;
        public static string getPort = Convert.ToString(Netplay.serverPort);
        public static bool menuMultiplayer = false;
        public static bool menuServer = false;
        public static int netMode = 0;
        public static int timeOut = 120;
        public static int maxNPCUpdates = 15;
        public static int maxItemUpdates = 10;
        public static string cUp = "W";
        public static string cLeft = "A";
        public static string cDown = "S";
        public static string cRight = "D";
        public static string cJump = "Space";
        public static string cThrowItem = "Q";
        public static string cInv = "Escape";
        public static Color mouseColor = new Color((int)byte.MaxValue, 50, 95);
        public static Color cursorColor = Color.White;
        public static int cursorColorDirection = 1;
        public static float cursorAlpha = 0.0f;
        public static float cursorScale = 0.0f;
        public static bool signBubble = false;
        public static int signX = 0;
        public static int signY = 0;
        public static bool hideUI = false;
        public static bool releaseUI = false;
        public static bool fixedTiming = false;
        public static string oldStatusText = "";
        public static bool autoShutdown = false;
        private static int maxMenuItems = 11;
        public static int menuMode = 0;
        public static string newWorldName = "";
        public static bool autoPass = false;
        private Process tServer = new Process();
        public int curMusic = 0;
        public int newMusic = 0;
        public Chest[] shop = new Chest[6];
        private int numDisplayModes = 0;
        private int[] displayWidth = new int[99];
        private int[] displayHeight = new int[99];
        private int splashCounter = 0;
        private float logoRotation = 0.0f;
        private float logoRotationDirection = 1f;
        private float logoRotationSpeed = 1f;
        private float logoScale = 1f;
        private float logoScaleDirection = 1f;
        private float logoScaleSpeed = 1f;
        private float[] menuItemScale = new float[Main.maxMenuItems];
        private int focusMenu = -1;
        private int selectedMenu = -1;
        private int selectedPlayer = 0;
        private int selectedWorld = 0;
        private Color selColor = Color.White;
        private int focusColor = 0;
        private int colorDelay = 0;
        private int setKey = -1;
        private int bgScroll = 0;
        private const int MF_BYPOSITION = 1024;
        public const int sectionWidth = 200;
        public const int sectionHeight = 150;
        public const int maxTileSets = 80;
        public const int maxWallTypes = 14;
        public const int maxBackgrounds = 7;
        public const int maxDust = 2000;
        public const int maxCombatText = 100;
        public const int maxPlayers = 255;
        public const int maxChests = 1000;
        public const int maxItemTypes = 265;
        public const int maxItems = 200;
        public const int maxProjectileTypes = 39;
        public const int maxProjectiles = 1000;
        public const int maxNPCTypes = 59;
        public const int maxNPCs = 1000;
        public const int maxGoreTypes = 86;
        public const int maxGore = 200;
        public const int maxInventory = 44;
        public const int maxItemSounds = 16;
        public const int maxNPCHitSounds = 3;
        public const int maxNPCKilledSounds = 3;
        public const int maxLiquidTypes = 2;
        public const int maxMusic = 7;
        public const int numArmorHead = 27;
        public const int numArmorBody = 16;
        public const int numArmorLegs = 15;
        public const double dayLength = 54000.0;
        public const double nightLength = 32400.0;
        public const int maxStars = 130;
        public const int maxStarTypes = 5;
        public const int maxClouds = 100;
        public const int maxCloudTypes = 4;
        public const int maxHair = 17;
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        public static int dungeonX;
        public static int dungeonY;
        public static int worldID;
        public static Color tileColor;
        public static double worldSurface;
        public static double rockLayer;
        public static int numStars;
        public static int evilTiles;
        public static int meteorTiles;
        public static int jungleTiles;
        public static int dungeonTiles;
        [ThreadStatic]
        public static Random rand;
        public static Texture2D chainTexture;
        public static Texture2D chain2Texture;
        public static Texture2D chain3Texture;
        public static Texture2D chain4Texture;
        public static Texture2D chain5Texture;
        public static Texture2D chain6Texture;
        public static Texture2D cdTexture;
        public static Texture2D boneArmTexture;
        public static Texture2D cursorTexture;
        public static Texture2D dustTexture;
        public static Texture2D sunTexture;
        public static Texture2D sun2Texture;
        public static Texture2D moonTexture;
        public static Texture2D blackTileTexture;
        public static Texture2D heartTexture;
        public static Texture2D manaTexture;
        public static Texture2D bubbleTexture;
        public static Texture2D shroomCapTexture;
        public static Texture2D inventoryBackTexture;
        public static Texture2D logoTexture;
        public static Texture2D textBackTexture;
        public static Texture2D chatTexture;
        public static Texture2D chat2Texture;
        public static Texture2D chatBackTexture;
        public static Texture2D teamTexture;
        public static Texture2D reTexture;
        public static Texture2D raTexture;
        public static Texture2D splashTexture;
        public static Texture2D fadeTexture;
        public static Texture2D ninjaTexture;
        public static Texture2D playerEyeWhitesTexture;
        public static Texture2D playerEyesTexture;
        public static Texture2D playerHandsTexture;
        public static Texture2D playerHands2Texture;
        public static Texture2D playerHeadTexture;
        public static Texture2D playerPantsTexture;
        public static Texture2D playerShirtTexture;
        public static Texture2D playerShoesTexture;
        public static Texture2D playerBeltTexture;
        public static Texture2D playerUnderShirtTexture;
        public static Texture2D playerUnderShirt2Texture;
        public static SoundEffect soundPlayerKilled;
        public static SoundEffectInstance soundInstancePlayerKilled;
        public static SoundEffect soundGrass;
        public static SoundEffectInstance soundInstanceGrass;
        public static SoundEffect soundGrab;
        public static SoundEffectInstance soundInstanceGrab;
        public static SoundEffect soundDoorOpen;
        public static SoundEffectInstance soundInstanceDoorOpen;
        public static SoundEffect soundDoorClosed;
        public static SoundEffectInstance soundInstanceDoorClosed;
        public static SoundEffect soundMenuOpen;
        public static SoundEffectInstance soundInstanceMenuOpen;
        public static SoundEffect soundMenuClose;
        public static SoundEffectInstance soundInstanceMenuClose;
        public static SoundEffect soundMenuTick;
        public static SoundEffectInstance soundInstanceMenuTick;
        public static SoundEffect soundShatter;
        public static SoundEffectInstance soundInstanceShatter;
        public static SoundEffect soundDoubleJump;
        public static SoundEffectInstance soundInstanceDoubleJump;
        public static SoundEffect soundRun;
        public static SoundEffectInstance soundInstanceRun;
        public static SoundEffect soundCoins;
        public static SoundEffectInstance soundInstanceCoins;
        public static AudioEngine engine;
        public static SoundBank soundBank;
        public static WaveBank waveBank;
        public static SpriteFont fontItemStack;
        public static SpriteFont fontMouseText;
        public static SpriteFont fontDeathText;
        public static SpriteFont fontCombatText;
        public static Vector2 screenPosition;
        public static Vector2 screenLastPosition;
        public static int stackSplit;
        public static int numAvailableRecipes;
        public static int focusRecipe;
        public static int spawnTileX;
        public static int spawnTileY;
        public bool toggleFullscreen;
        public static string playerPathName;
        public static string worldPathName;
        private static KeyboardState inputText;
        private static KeyboardState oldInputText;
        public static int netPlayCounter;
        public static int lastNPCUpdate;
        public static int lastItemUpdate;
        private int textBlinkerCount;
        private int textBlinkerState;

        static Main()
        {
        }

        public Main()
        {
            this.graphics = new GraphicsDeviceManager((Game)this);
            this.Content.RootDirectory = "Content";
        }

        //[DllImport("User32")]
        //private static extern int RemoveMenu(IntPtr hMenu, int nPosition, int wFlags);

        //[DllImport("User32")]
        //private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        //[DllImport("User32")]
        //private static extern int GetMenuItemCount(IntPtr hWnd);

        public static void LoadWorlds()
        {
            Directory.CreateDirectory(Main.WorldPath);
            string[] files = Directory.GetFiles(Main.WorldPath, "*.wld");
            int num = files.Length;
            if (!Main.dedServ && num > 5)
                num = 5;
            for (int index = 0; index < num; ++index)
            {
                Main.loadWorldPath[index] = files[index];
                try
                {
                    using (FileStream fileStream = new FileStream(Main.loadWorldPath[index], FileMode.Open))
                    {
                        using (BinaryReader binaryReader = new BinaryReader((Stream)fileStream))
                        {
                            binaryReader.ReadInt32();
                            Main.loadWorld[index] = binaryReader.ReadString();
                            binaryReader.Close();
                        }
                    }
                }
                catch
                {
                    Main.loadWorld[index] = Main.loadWorldPath[index];
                }
            }
            Main.numLoadWorlds = num;
        }

        private static void LoadPlayers()
        {
            Directory.CreateDirectory(Main.PlayerPath);
            string[] files = Directory.GetFiles(Main.PlayerPath, "*.plr");
            int num = files.Length;
            if (num > 5)
                num = 5;
            for (int index = 0; index < 5; ++index)
            {
                Main.loadPlayer[index] = new Player();
                if (index < num)
                {
                    Main.loadPlayerPath[index] = files[index];
                    Main.loadPlayer[index] = Player.LoadPlayer(Main.loadPlayerPath[index]);
                }
            }
            Main.numLoadPlayers = num;
        }

        protected void SaveSettings()
        {
            Directory.CreateDirectory(Main.SavePath);
            try
            {
                File.SetAttributes(Main.SavePath + "/config.dat", FileAttributes.Normal);
            }
            catch
            {
            }
            try
            {
                using (FileStream fileStream = new FileStream(Main.SavePath + "/config.dat", FileMode.Create))
                {
                    using (BinaryWriter binaryWriter = new BinaryWriter((Stream)fileStream))
                    {
                        binaryWriter.Write(Main.curRelease);
                        binaryWriter.Write(this.graphics.IsFullScreen);
                        binaryWriter.Write(Main.mouseColor.R);
                        binaryWriter.Write(Main.mouseColor.G);
                        binaryWriter.Write(Main.mouseColor.B);
                        binaryWriter.Write(Main.soundVolume);
                        binaryWriter.Write(Main.musicVolume);
                        binaryWriter.Write(Main.cUp);
                        binaryWriter.Write(Main.cDown);
                        binaryWriter.Write(Main.cLeft);
                        binaryWriter.Write(Main.cRight);
                        binaryWriter.Write(Main.cJump);
                        binaryWriter.Write(Main.cThrowItem);
                        binaryWriter.Write(Main.cInv);
                        binaryWriter.Write(Main.caveParrallax);
                        binaryWriter.Write(Main.fixedTiming);
                        binaryWriter.Write(this.graphics.PreferredBackBufferWidth);
                        binaryWriter.Write(this.graphics.PreferredBackBufferHeight);
                        binaryWriter.Write(Main.autoSave);
                        binaryWriter.Write(Main.autoPause);
                        binaryWriter.Close();
                    }
                }
            }
            catch
            {
            }
        }

        protected void OpenSettings()
        {
            try
            {
                if (File.Exists(Main.SavePath + "/config.dat"))
                {
                    using (FileStream fileStream = new FileStream(Main.SavePath + "/config.dat", FileMode.Open))
                    {
                        using (BinaryReader binaryReader = new BinaryReader((Stream)fileStream))
                        {
                            int num = binaryReader.ReadInt32();
                            bool flag = binaryReader.ReadBoolean();
                            Main.mouseColor.R = binaryReader.ReadByte();
                            Main.mouseColor.G = binaryReader.ReadByte();
                            Main.mouseColor.B = binaryReader.ReadByte();
                            Main.soundVolume = binaryReader.ReadSingle();
                            Main.musicVolume = binaryReader.ReadSingle();
                            Main.cUp = binaryReader.ReadString();
                            Main.cDown = binaryReader.ReadString();
                            Main.cLeft = binaryReader.ReadString();
                            Main.cRight = binaryReader.ReadString();
                            Main.cJump = binaryReader.ReadString();
                            Main.cThrowItem = binaryReader.ReadString();
                            if (num >= 1)
                                Main.cInv = binaryReader.ReadString();
                            Main.caveParrallax = binaryReader.ReadSingle();
                            if (num >= 2)
                                Main.fixedTiming = binaryReader.ReadBoolean();
                            if (num >= 4)
                            {
                                this.graphics.PreferredBackBufferWidth = binaryReader.ReadInt32();
                                this.graphics.PreferredBackBufferHeight = binaryReader.ReadInt32();
                            }
                            if (num >= 8)
                                Main.autoSave = binaryReader.ReadBoolean();
                            if (num >= 9)
                                Main.autoPause = binaryReader.ReadBoolean();
                            binaryReader.Close();
                            if (flag && !this.graphics.IsFullScreen)
                                this.graphics.ToggleFullScreen();
                        }
                    }
                }
            }
            catch
            {
            }
        }

        private static void ErasePlayer(int i)
        {
            try
            {
                File.Delete(Main.loadPlayerPath[i]);
                File.Delete(Main.loadPlayerPath[i] + ".bak");
                Main.LoadPlayers();
            }
            catch
            {
            }
        }

        private static void EraseWorld(int i)
        {
            try
            {
                File.Delete(Main.loadWorldPath[i]);
                File.Delete(Main.loadWorldPath[i] + ".bak");
                Main.LoadWorlds();
            }
            catch
            {
            }
        }

        private static string nextLoadPlayer()
        {
            int num = 1;
            while (true)
            {
                if (File.Exists(string.Concat(new object[4]
        {
          (object) Main.PlayerPath,
          (object) "/player",
          (object) num,
          (object) ".plr"
        })))
                    ++num;
                else
                    break;
            }
            return string.Concat(new object[4]
      {
        (object) Main.PlayerPath,
        (object) "/player",
        (object) num,
        (object) ".plr"
      });
        }

        private static string nextLoadWorld()
        {
            int num = 1;
            while (true)
            {
                if (File.Exists(string.Concat(new object[4]
        {
          (object) Main.WorldPath,
          (object) "/world",
          (object) num,
          (object) ".wld"
        })))
                    ++num;
                else
                    break;
            }
            return string.Concat(new object[4]
      {
        (object) Main.WorldPath,
        (object) "/world",
        (object) num,
        (object) ".wld"
      });
        }

        public void autoCreate(string newOpt)
        {
            if (newOpt == "0")
                Main.autoGen = false;
            else if (newOpt == "1")
            {
                Main.maxTilesX = 4200;
                Main.maxTilesY = 1200;
                Main.autoGen = true;
            }
            else if (newOpt == "2")
            {
                Main.maxTilesX = 6300;
                Main.maxTilesY = 1800;
                Main.autoGen = true;
            }
            else if (newOpt == "3")
            {
                Main.maxTilesX = 8400;
                Main.maxTilesY = 2400;
                Main.autoGen = true;
            }
        }

        public void NewMOTD(string newMOTD)
        {
            Main.motd = newMOTD;
        }

        public void LoadDedConfig(string configPath)
        {
            if (File.Exists(configPath))
            {
                using (StreamReader streamReader = new StreamReader(configPath))
                {
                    string str1;
                    while ((str1 = streamReader.ReadLine()) != null)
                    {
                        try
                        {
                            if (str1.Length > 6 && str1.Substring(0, 6).ToLower() == "world=")
                                Main.worldPathName = str1.Substring(6);
                            if (str1.Length > 5 && str1.Substring(0, 5).ToLower() == "port=")
                            {
                                string str2 = str1.Substring(5);
                                try
                                {
                                    Netplay.serverPort = Convert.ToInt32(str2);
                                }
                                catch
                                {
                                }
                            }
                            if (str1.Length > 11 && str1.Substring(0, 11).ToLower() == "maxplayers=")
                            {
                                string str2 = str1.Substring(11);
                                try
                                {
                                    Main.maxNetPlayers = Convert.ToInt32(str2);
                                }
                                catch
                                {
                                }
                            }
                            if (str1.Length > 9 && str1.Substring(0, 9).ToLower() == "password=")
                                Netplay.password = str1.Substring(9);
                            if (str1.Length > 5 && str1.Substring(0, 5).ToLower() == "motd=")
                                Main.motd = str1.Substring(5);
                            if (str1.Length >= 10 && str1.Substring(0, 10).ToLower() == "worldpath=")
                                Main.WorldPath = str1.Substring(10);
                            if (str1.Length >= 10 && str1.Substring(0, 10).ToLower() == "worldname=")
                                Main.worldName = str1.Substring(10);
                            if (str1.Length > 8 && str1.Substring(0, 8).ToLower() == "banlist=")
                                Netplay.banFile = str1.Substring(8);
                            if (str1.Length > 11 && str1.Substring(0, 11).ToLower() == "autocreate=")
                            {
                                string str2 = str1.Substring(11);
                                if (str2 == "0")
                                    Main.autoGen = false;
                                else if (str2 == "1")
                                {
                                    Main.maxTilesX = 4200;
                                    Main.maxTilesY = 1200;
                                    Main.autoGen = true;
                                }
                                else if (str2 == "2")
                                {
                                    Main.maxTilesX = 6300;
                                    Main.maxTilesY = 1800;
                                    Main.autoGen = true;
                                }
                                else if (str2 == "3")
                                {
                                    Main.maxTilesX = 8400;
                                    Main.maxTilesY = 2400;
                                    Main.autoGen = true;
                                }
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
        }

        public void SetNetPlayers(int mPlayers)
        {
            Main.maxNetPlayers = mPlayers;
        }

        public void SetWorld(string wrold)
        {
            Main.worldPathName = wrold;
        }

        public void SetWorldName(string wrold)
        {
            Main.worldName = wrold;
        }

        public void autoShut()
        {
            Main.autoShutdown = true;
        }
        //TODO: External Refs --- BAD
        //[DllImport("user32.dll")]
        //public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        //[DllImport("user32.dll")]
        //private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        public void AutoPass()
        {
            Main.autoPass = true;
        }

        public void AutoJoin(string IP)
        {
            Main.defaultIP = IP;
            Main.getIP = IP;
            Netplay.SetIP(Main.defaultIP);
            Main.autoJoin = true;
        }

        public void AutoHost()
        {
            Main.menuMultiplayer = true;
            Main.menuServer = true;
            Main.menuMode = 1;
            Main.LoadPlayers();
        }

        public void DedServ()
        {
            Main.rand = new Random();
            if (Main.autoShutdown)
            {
                //string lpWindowName = "terraria" + (object)Main.rand.Next(int.MaxValue);
                //Console.Title = lpWindowName;
                //IntPtr window = Main.FindWindow((string)null, lpWindowName);
                //if (window != IntPtr.Zero)
                //    Main.ShowWindow(window, 0);
            }
            else
                Console.Title = "Terraria Server " + Main.versionNumber2;
            Main.dedServ = true;
            Main.showSplash = false;
            this.Initialize();
            while (Main.worldPathName == null || Main.worldPathName == "")
            {
                Main.LoadWorlds();
                bool flag1 = true;
                while (flag1)
                {
                    Console.WriteLine("Terraria Server " + Main.versionNumber2);
                    Console.WriteLine("");
                    for (int index = 0; index < Main.numLoadWorlds; ++index)
                        Console.WriteLine(string.Concat(new object[4]
            {
              (object) (index + 1),
              (object) '\t',
              (object) '\t',
              (object) Main.loadWorld[index]
            }));
                    Console.WriteLine(string.Concat(new object[4]
          {
            (object) "n",
            (object) '\t',
            (object) '\t',
            (object) "New World"
          }));
                    Console.WriteLine("d <number>" + (object)'\t' + "Delete World");
                    Console.WriteLine("");
                    Console.Write("Choose World: ");
                    string str1 = Console.ReadLine();
                    try
                    {
                        Console.Clear();
                    }
                    catch
                    {
                    }
                    if (str1.Length >= 2 && str1.Substring(0, 2).ToLower() == "d ")
                    {
                        try
                        {
                            int i = Convert.ToInt32(str1.Substring(2)) - 1;
                            if (i < Main.numLoadWorlds)
                            {
                                Console.WriteLine("Terraria Server " + Main.versionNumber2);
                                Console.WriteLine("");
                                Console.WriteLine("Really delete " + Main.loadWorld[i] + "?");
                                Console.Write("(y/n): ");
                                if (Console.ReadLine().ToLower() == "y")
                                    Main.EraseWorld(i);
                            }
                        }
                        catch
                        {
                        }
                        try
                        {
                            Console.Clear();
                        }
                        catch
                        {
                        }
                    }
                    else if (str1 == "n" || str1 == "N")
                    {
                        bool flag2 = true;
                        while (flag2)
                        {
                            Console.WriteLine("Terraria Server " + Main.versionNumber2);
                            Console.WriteLine("");
                            Console.WriteLine("1" + (object)'\t' + "Small");
                            Console.WriteLine("2" + (object)'\t' + "Medium");
                            Console.WriteLine("3" + (object)'\t' + "Large");
                            Console.WriteLine("");
                            Console.Write("Choose size: ");
                            string str2 = Console.ReadLine();
                            try
                            {
                                switch (Convert.ToInt32(str2))
                                {
                                    case 1:
                                        Main.maxTilesX = 4200;
                                        Main.maxTilesY = 1200;
                                        flag2 = false;
                                        break;
                                    case 2:
                                        Main.maxTilesX = 6300;
                                        Main.maxTilesY = 1800;
                                        flag2 = false;
                                        break;
                                    case 3:
                                        Main.maxTilesX = 8400;
                                        Main.maxTilesY = 2400;
                                        flag2 = false;
                                        break;
                                }
                            }
                            catch
                            {
                            }
                            try
                            {
                                Console.Clear();
                            }
                            catch
                            {
                            }
                        }
                        bool flag3 = true;
                        while (flag3)
                        {
                            Console.WriteLine("Terraria Server " + Main.versionNumber2);
                            Console.WriteLine("");
                            Console.Write("Enter world name: ");
                            Main.newWorldName = Console.ReadLine();
                            if (Main.newWorldName != "" && Main.newWorldName != " " && Main.newWorldName != null)
                                flag3 = false;
                            try
                            {
                                Console.Clear();
                            }
                            catch
                            {
                            }
                        }
                        Main.worldName = Main.newWorldName;
                        Main.worldPathName = Main.nextLoadWorld();
                        Main.menuMode = 10;
                        WorldGen.CreateNewWorld();
                        while (Main.menuMode == 10)
                        {
                            if (Main.oldStatusText != Main.statusText)
                            {
                                Main.oldStatusText = Main.statusText;
                                Console.WriteLine(Main.statusText);
                            }
                        }
                        try
                        {
                            Console.Clear();
                        }
                        catch
                        {
                        }
                    }
                    else
                    {
                        try
                        {
                            int index = Convert.ToInt32(str1) - 1;
                            if (index >= 0 && index < Main.numLoadWorlds)
                            {
                                bool flag2 = true;
                                while (flag2)
                                {
                                    Console.WriteLine("Terraria Server " + Main.versionNumber2);
                                    Console.WriteLine("");
                                    Console.Write("Max players (press enter for 8): ");
                                    string str2 = Console.ReadLine();
                                    try
                                    {
                                        if (str2 == "")
                                            str2 = "8";
                                        int num = Convert.ToInt32(str2);
                                        if (num <= (int)byte.MaxValue && num >= 1)
                                        {
                                            Main.maxNetPlayers = num;
                                            flag2 = false;
                                        }
                                        flag2 = false;
                                    }
                                    catch
                                    {
                                    }
                                    try
                                    {
                                        Console.Clear();
                                    }
                                    catch
                                    {
                                    }
                                }
                                bool flag3 = true;
                                while (flag3)
                                {
                                    Console.WriteLine("Terraria Server " + Main.versionNumber2);
                                    Console.WriteLine("");
                                    Console.Write("Server port (press enter for 7777): ");
                                    string str2 = Console.ReadLine();
                                    try
                                    {
                                        if (str2 == "")
                                            str2 = "7777";
                                        int num = Convert.ToInt32(str2);
                                        if (num <= (int)ushort.MaxValue)
                                        {
                                            Netplay.serverPort = num;
                                            flag3 = false;
                                        }
                                    }
                                    catch
                                    {
                                    }
                                    try
                                    {
                                        Console.Clear();
                                    }
                                    catch
                                    {
                                    }
                                }
                                Console.WriteLine("Terraria Server " + Main.versionNumber2);
                                Console.WriteLine("");
                                Console.Write("Server password (press enter for none): ");
                                Netplay.password = Console.ReadLine();
                                Main.worldPathName = Main.loadWorldPath[index];
                                flag1 = false;
                                try
                                {
                                    Console.Clear();
                                }
                                catch
                                {
                                }
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
            try
            {
                Console.Clear();
            }
            catch
            {
            }
            WorldGen.serverLoadWorld();
            Console.WriteLine("Terraria Server " + Main.versionNumber);
            Console.WriteLine("");
            while (!Netplay.ServerUp)
            {
                if (Main.oldStatusText != Main.statusText)
                {
                    Main.oldStatusText = Main.statusText;
                    Console.WriteLine(Main.statusText);
                }
            }
            try
            {
                Console.Clear();
            }
            catch
            {
            }
            Console.WriteLine("Terraria Server " + Main.versionNumber);
            Console.WriteLine("");
            Console.WriteLine("Listening on port " + (object)Netplay.serverPort);
            Console.WriteLine("Type 'help' for a list of commands.");
            Console.WriteLine("");
            Console.Title = "Terraria Server: " + Main.worldName;
            Stopwatch stopwatch = new Stopwatch();
            if (!Main.autoShutdown)
                Main.startDedInput();
            stopwatch.Start();
            double num1 = 16.6666666666667;
            double num2 = 0.0;
            while (!Netplay.disconnect)
            {
                double num3 = (double)stopwatch.ElapsedMilliseconds;
                if (num3 + num2 >= num1)
                {
                    num2 += num3 - num1;
                    stopwatch.Reset();
                    stopwatch.Start();
                    if (Main.oldStatusText != Main.statusText)
                    {
                        Main.oldStatusText = Main.statusText;
                        Console.WriteLine(Main.statusText);
                    }
                    if (num2 > 1000.0)
                        num2 = 1000.0;
                    this.Update(new GameTime());
                    double num4 = (double)stopwatch.ElapsedMilliseconds + num2;
                    if (num4 < num1)
                    {
                        int millisecondsTimeout = (int)(num1 - num4) - 1;
                        if (millisecondsTimeout > 1)
                            Thread.Sleep(millisecondsTimeout);
                    }
                }
                Thread.Sleep(0);
            }
        }

        public static void startDedInput()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(Main.startDedInputCallBack), (object)1);
        }

        public static void startDedInputCallBack(object threadContext)
        {
            while (!Netplay.disconnect)
            {
                Console.Write(": ");
                //Null Exception on Ctrl-C
                string str1 = (Console.ReadLine() ?? "").ToLower();
                string str2 = str1;
                string str3 = str1.ToLower();
                try
                {
                    if (str3 == "help")
                    {
                        Console.WriteLine("Available commands:");
                        Console.WriteLine("");
                        Console.WriteLine(string.Concat(new object[4]
            {
              (object) "help ",
              (object) '\t',
              (object) '\t',
              (object) " Displays a list of commands."
            }));
                        Console.WriteLine("playing " + (object)'\t' + " Shows the list of players");
                        Console.WriteLine(string.Concat(new object[4]
            {
              (object) "clear ",
              (object) '\t',
              (object) '\t',
              (object) " Clear the console window."
            }));
                        Console.WriteLine(string.Concat(new object[4]
            {
              (object) "exit ",
              (object) '\t',
              (object) '\t',
              (object) " Shutdown the server and save."
            }));
                        Console.WriteLine("exit-nosave " + (object)'\t' + " Shutdown the server without saving.");
                        Console.WriteLine(string.Concat(new object[4]
            {
              (object) "save ",
              (object) '\t',
              (object) '\t',
              (object) " Save the game world."
            }));
                        Console.WriteLine("kick <player> " + (object)'\t' + " Kicks a player from the server.");
                        Console.WriteLine("ban <player> " + (object)'\t' + " Bans a player from the server.");
                        Console.WriteLine("password" + (object)'\t' + " Show password.");
                        Console.WriteLine("password <pass>" + (object)'\t' + " Change password.");
                        Console.WriteLine(string.Concat(new object[4]
            {
              (object) "version",
              (object) '\t',
              (object) '\t',
              (object) " Print version number."
            }));
                        Console.WriteLine(string.Concat(new object[4]
            {
              (object) "time",
              (object) '\t',
              (object) '\t',
              (object) " Display game time."
            }));
                        Console.WriteLine(string.Concat(new object[4]
            {
              (object) "port",
              (object) '\t',
              (object) '\t',
              (object) " Print the listening port."
            }));
                        Console.WriteLine("maxplayers" + (object)'\t' + " Print the max number of players.");
                        Console.WriteLine("say <words>" + (object)'\t' + " Send a message.");
                        Console.WriteLine(string.Concat(new object[4]
            {
              (object) "motd",
              (object) '\t',
              (object) '\t',
              (object) " Print MOTD."
            }));
                        Console.WriteLine("motd <words>" + (object)'\t' + " Change MOTD.");
                        Console.WriteLine(string.Concat(new object[4]
            {
              (object) "dawn",
              (object) '\t',
              (object) '\t',
              (object) " Change time to dawn."
            }));
                        Console.WriteLine(string.Concat(new object[4]
            {
              (object) "noon",
              (object) '\t',
              (object) '\t',
              (object) " Change time to noon."
            }));
                        Console.WriteLine(string.Concat(new object[4]
            {
              (object) "dusk",
              (object) '\t',
              (object) '\t',
              (object) " Change time to dusk."
            }));
                        Console.WriteLine("midnight" + (object)'\t' + " Change time to midnight.");
                        Console.WriteLine(string.Concat(new object[4]
            {
              (object) "settle",
              (object) '\t',
              (object) '\t',
              (object) " Settle all water."
            }));
                    }
                    else if (str3 == "settle")
                    {
                        if (!Liquid.panicMode)
                            Liquid.StartPanic();
                        else
                            Console.WriteLine("Water is already settling");
                    }
                    else if (str3 == "dawn")
                    {
                        Main.dayTime = true;
                        Main.time = 0.0;
                        NetMessage.SendData(7, -1, -1, "", 0, 0.0f, 0.0f, 0.0f);
                    }
                    else if (str3 == "dusk")
                    {
                        Main.dayTime = false;
                        Main.time = 0.0;
                        NetMessage.SendData(7, -1, -1, "", 0, 0.0f, 0.0f, 0.0f);
                    }
                    else if (str3 == "noon")
                    {
                        Main.dayTime = true;
                        Main.time = 27000.0;
                        NetMessage.SendData(7, -1, -1, "", 0, 0.0f, 0.0f, 0.0f);
                    }
                    else if (str3 == "midnight")
                    {
                        Main.dayTime = false;
                        Main.time = 16200.0;
                        NetMessage.SendData(7, -1, -1, "", 0, 0.0f, 0.0f, 0.0f);
                    }
                    else if (str3 == "exit-nosave")
                        Netplay.disconnect = true;
                    else if (str3 == "exit")
                    {
                        WorldGen.saveWorld(false);
                        Netplay.disconnect = true;
                    }
                    else if (str3 == "save")
                        WorldGen.saveWorld(false);
                    else if (str3 == "time")
                    {
                        string str4 = "AM";
                        double num1 = Main.time;
                        if (!Main.dayTime)
                            num1 += 54000.0;
                        double num2 = num1 / 86400.0 * 24.0 - 7.5 - 12.0;
                        if (num2 < 0.0)
                            num2 += 24.0;
                        if (num2 >= 12.0)
                            str4 = "PM";
                        int num3 = (int)num2;
                        double num4 = (double)(int)((num2 - (double)num3) * 60.0);
                        string str5 = string.Concat((object)num4);
                        if (num4 < 10.0)
                            str5 = "0" + str5;
                        if (num3 > 12)
                            num3 -= 12;
                        if (num3 == 0)
                            num3 = 12;
                        Console.WriteLine("Time: " + (object)num3 + ":" + str5 + " " + str4);
                    }
                    else if (str3 == "maxplayers")
                        Console.WriteLine("Player limit: " + (object)Main.maxNetPlayers);
                    else if (str3 == "port")
                        Console.WriteLine("Port: " + (object)Netplay.serverPort);
                    else if (str3 == "version")
                        Console.WriteLine("Terraria Server " + Main.versionNumber);
                    else if (str3 == "clear")
                    {
                        try
                        {
                            Console.Clear();
                        }
                        catch
                        {
                        }
                    }
                    else if (str3 == "playing")
                    {
                        //TODO: Remove
                        Console.WriteLine("playing");
                        int num = 0;
                        for (int index = 0; index < (int)byte.MaxValue; ++index)
                        {
                            if (Main.player[index].active)
                            {
                                ++num;
                                Console.WriteLine(string.Concat(new object[4]
                {
                  (object) Main.player[index].name,
                  (object) " (",
                  (object) Netplay.serverSock[index].tcpClient.Client.RemoteEndPoint,
                  (object) ")"
                }));
                            }
                        }
                        if (num == 0)
                            Console.WriteLine("No players connected.");
                        else if (num == 1)
                            Console.WriteLine("1 player connected.");
                        else
                            Console.WriteLine((string)(object)num + (object)" players connected.");
                    }
                    else if (!(str3 == ""))
                    {
                        if (str3 == "motd")
                        {
                            if (Main.motd == "")
                                Console.WriteLine("Welcome to " + Main.worldName + "!");
                            else
                                Console.WriteLine("MOTD: " + Main.motd);
                        }
                        else if (str3.Length >= 5 && str3.Substring(0, 5) == "motd ")
                            Main.motd = str2.Substring(5);
                        else if (str3.Length == 8 && str3.Substring(0, 8) == "password")
                        {
                            if (Netplay.password == "")
                                Console.WriteLine("No password set.");
                            else
                                Console.WriteLine("Password: " + Netplay.password);
                        }
                        else if (str3.Length >= 9 && str3.Substring(0, 9) == "password ")
                        {
                            string str4 = str2.Substring(9);
                            if (str4 == "")
                            {
                                Netplay.password = "";
                                Console.WriteLine("Password disabled.");
                            }
                            else
                            {
                                Netplay.password = str4;
                                Console.WriteLine("Password: " + Netplay.password);
                            }
                        }
                        else if (str3 == "say")
                            Console.WriteLine("Usage: say <words>");
                        else if (str3.Length >= 4 && str3.Substring(0, 4) == "say ")
                        {
                            string str4 = str2.Substring(4);
                            if (str4 == "")
                            {
                                Console.WriteLine("Usage: say <words>");
                            }
                            else
                            {
                                Console.WriteLine("<Server> " + str4);
                                NetMessage.SendData(25, -1, -1, "<Server> " + str4, (int)byte.MaxValue, (float)byte.MaxValue, 240f, 20f);
                            }
                        }
                        else if (str3.Length == 4 && str3.Substring(0, 4) == "kick")
                            Console.WriteLine("Usage: kick <player>");
                        else if (str3.Length >= 5 && str3.Substring(0, 5) == "kick ")
                        {
                            string str4 = str3.Substring(5).ToLower();
                            if (str4 == "")
                            {
                                Console.WriteLine("Usage: kick <player>");
                            }
                            else
                            {
                                for (int remoteClient = 0; remoteClient < (int)byte.MaxValue; ++remoteClient)
                                {
                                    if (Main.player[remoteClient].active && Main.player[remoteClient].name.ToLower() == str4)
                                        NetMessage.SendData(2, remoteClient, -1, "Kicked from server.", 0, 0.0f, 0.0f, 0.0f);
                                }
                            }
                        }
                        else if (str3.Length == 3 && str3.Substring(0, 3) == "ban")
                            Console.WriteLine("Usage: ban <player>");
                        else if (str3.Length >= 4 && str3.Substring(0, 4) == "ban ")
                        {
                            string str4 = str3.Substring(4).ToLower();
                            if (str4 == "")
                            {
                                Console.WriteLine("Usage: ban <player>");
                            }
                            else
                            {
                                for (int index = 0; index < (int)byte.MaxValue; ++index)
                                {
                                    if (Main.player[index].active && Main.player[index].name.ToLower() == str4)
                                    {
                                        Netplay.AddBan(index);
                                        NetMessage.SendData(2, index, -1, "Banned from server.", 0, 0.0f, 0.0f, 0.0f);
                                    }
                                }
                            }
                        }
                        else
                            Console.WriteLine("Invalid command.");
                    }
                }
                catch
                {
                    Console.WriteLine("Invalid command.");
                }
            }
        }

        protected override void Initialize()
        {
            if (Main.rand == null)
                Main.rand = new Random((int)DateTime.Now.Ticks);
            if (WorldGen.genRand == null)
                WorldGen.genRand = new Random((int)DateTime.Now.Ticks);
            int num = Main.rand.Next(5);
            if (num == 0)
                this.Window.Title = "Terraria: Dig Peon, Dig!";
            if (num == 1)
                this.Window.Title = "Terraria: Epic Dirt";
            if (num == 2)
                this.Window.Title = "Terraria: Hey Guys!";
            if (num == 3)
                this.Window.Title = "Terraria: Sand is Overpowered";
            else
                this.Window.Title = "Terraria: Shut Up and Dig Gaiden!";
            Main.tileSolid[0] = true;
            Main.tileBlockLight[0] = true;
            Main.tileSolid[1] = true;
            Main.tileBlockLight[1] = true;
            Main.tileSolid[2] = true;
            Main.tileBlockLight[2] = true;
            Main.tileSolid[3] = false;
            Main.tileNoAttach[3] = true;
            Main.tileNoFail[3] = true;
            Main.tileSolid[4] = false;
            Main.tileNoAttach[4] = true;
            Main.tileNoFail[4] = true;
            Main.tileNoFail[24] = true;
            Main.tileSolid[5] = false;
            Main.tileSolid[6] = true;
            Main.tileBlockLight[6] = true;
            Main.tileSolid[7] = true;
            Main.tileBlockLight[7] = true;
            Main.tileSolid[8] = true;
            Main.tileBlockLight[8] = true;
            Main.tileSolid[9] = true;
            Main.tileBlockLight[9] = true;
            Main.tileBlockLight[10] = true;
            Main.tileSolid[10] = true;
            Main.tileNoAttach[10] = true;
            Main.tileBlockLight[10] = true;
            Main.tileSolid[11] = false;
            Main.tileSolidTop[19] = true;
            Main.tileSolid[19] = true;
            Main.tileSolid[22] = true;
            Main.tileSolid[23] = true;
            Main.tileSolid[25] = true;
            Main.tileSolid[30] = true;
            Main.tileNoFail[32] = true;
            Main.tileBlockLight[32] = true;
            Main.tileSolid[37] = true;
            Main.tileBlockLight[37] = true;
            Main.tileSolid[38] = true;
            Main.tileBlockLight[38] = true;
            Main.tileSolid[39] = true;
            Main.tileBlockLight[39] = true;
            Main.tileSolid[40] = true;
            Main.tileBlockLight[40] = true;
            Main.tileSolid[41] = true;
            Main.tileBlockLight[41] = true;
            Main.tileSolid[43] = true;
            Main.tileBlockLight[43] = true;
            Main.tileSolid[44] = true;
            Main.tileBlockLight[44] = true;
            Main.tileSolid[45] = true;
            Main.tileBlockLight[45] = true;
            Main.tileSolid[46] = true;
            Main.tileBlockLight[46] = true;
            Main.tileSolid[47] = true;
            Main.tileBlockLight[47] = true;
            Main.tileSolid[48] = true;
            Main.tileBlockLight[48] = true;
            Main.tileSolid[53] = true;
            Main.tileBlockLight[53] = true;
            Main.tileSolid[54] = true;
            Main.tileBlockLight[52] = true;
            Main.tileSolid[56] = true;
            Main.tileBlockLight[56] = true;
            Main.tileSolid[57] = true;
            Main.tileBlockLight[57] = true;
            Main.tileSolid[58] = true;
            Main.tileBlockLight[58] = true;
            Main.tileSolid[59] = true;
            Main.tileBlockLight[59] = true;
            Main.tileSolid[60] = true;
            Main.tileBlockLight[60] = true;
            Main.tileSolid[63] = true;
            Main.tileBlockLight[63] = true;
            Main.tileStone[63] = true;
            Main.tileSolid[64] = true;
            Main.tileBlockLight[64] = true;
            Main.tileStone[64] = true;
            Main.tileSolid[65] = true;
            Main.tileBlockLight[65] = true;
            Main.tileStone[65] = true;
            Main.tileSolid[66] = true;
            Main.tileBlockLight[66] = true;
            Main.tileStone[66] = true;
            Main.tileSolid[67] = true;
            Main.tileBlockLight[67] = true;
            Main.tileStone[67] = true;
            Main.tileSolid[68] = true;
            Main.tileBlockLight[68] = true;
            Main.tileStone[68] = true;
            Main.tileSolid[75] = true;
            Main.tileBlockLight[75] = true;
            Main.tileSolid[76] = true;
            Main.tileBlockLight[76] = true;
            Main.tileSolid[70] = true;
            Main.tileBlockLight[70] = true;
            Main.tileBlockLight[51] = true;
            Main.tileNoFail[50] = true;
            Main.tileNoAttach[50] = true;
            Main.tileDungeon[41] = true;
            Main.tileDungeon[43] = true;
            Main.tileDungeon[44] = true;
            Main.tileBlockLight[30] = true;
            Main.tileBlockLight[25] = true;
            Main.tileBlockLight[23] = true;
            Main.tileBlockLight[22] = true;
            Main.tileBlockLight[62] = true;
            Main.tileSolidTop[18] = true;
            Main.tileSolidTop[14] = true;
            Main.tileSolidTop[16] = true;
            Main.tileNoAttach[20] = true;
            Main.tileNoAttach[19] = true;
            Main.tileNoAttach[13] = true;
            Main.tileNoAttach[14] = true;
            Main.tileNoAttach[15] = true;
            Main.tileNoAttach[16] = true;
            Main.tileNoAttach[17] = true;
            Main.tileNoAttach[18] = true;
            Main.tileNoAttach[19] = true;
            Main.tileNoAttach[21] = true;
            Main.tileNoAttach[27] = true;
            Main.tileFrameImportant[3] = true;
            Main.tileFrameImportant[5] = true;
            Main.tileFrameImportant[10] = true;
            Main.tileFrameImportant[11] = true;
            Main.tileFrameImportant[12] = true;
            Main.tileFrameImportant[13] = true;
            Main.tileFrameImportant[14] = true;
            Main.tileFrameImportant[15] = true;
            Main.tileFrameImportant[16] = true;
            Main.tileFrameImportant[17] = true;
            Main.tileFrameImportant[18] = true;
            Main.tileFrameImportant[20] = true;
            Main.tileFrameImportant[21] = true;
            Main.tileFrameImportant[24] = true;
            Main.tileFrameImportant[26] = true;
            Main.tileFrameImportant[27] = true;
            Main.tileFrameImportant[28] = true;
            Main.tileFrameImportant[29] = true;
            Main.tileFrameImportant[31] = true;
            Main.tileFrameImportant[33] = true;
            Main.tileFrameImportant[34] = true;
            Main.tileFrameImportant[35] = true;
            Main.tileFrameImportant[36] = true;
            Main.tileFrameImportant[42] = true;
            Main.tileFrameImportant[50] = true;
            Main.tileFrameImportant[55] = true;
            Main.tileFrameImportant[61] = true;
            Main.tileFrameImportant[71] = true;
            Main.tileFrameImportant[72] = true;
            Main.tileFrameImportant[73] = true;
            Main.tileFrameImportant[74] = true;
            Main.tileFrameImportant[77] = true;
            Main.tileFrameImportant[78] = true;
            Main.tileFrameImportant[79] = true;
            Main.tileTable[14] = true;
            Main.tileTable[18] = true;
            Main.tileTable[19] = true;
            Main.tileWaterDeath[4] = true;
            Main.tileWaterDeath[51] = true;
            Main.tileLavaDeath[3] = true;
            Main.tileLavaDeath[5] = true;
            Main.tileLavaDeath[10] = true;
            Main.tileLavaDeath[11] = true;
            Main.tileLavaDeath[12] = true;
            Main.tileLavaDeath[13] = true;
            Main.tileLavaDeath[14] = true;
            Main.tileLavaDeath[15] = true;
            Main.tileLavaDeath[16] = true;
            Main.tileLavaDeath[17] = true;
            Main.tileLavaDeath[18] = true;
            Main.tileLavaDeath[19] = true;
            Main.tileLavaDeath[20] = true;
            Main.tileLavaDeath[27] = true;
            Main.tileLavaDeath[28] = true;
            Main.tileLavaDeath[29] = true;
            Main.tileLavaDeath[32] = true;
            Main.tileLavaDeath[33] = true;
            Main.tileLavaDeath[34] = true;
            Main.tileLavaDeath[35] = true;
            Main.tileLavaDeath[36] = true;
            Main.tileLavaDeath[42] = true;
            Main.tileLavaDeath[49] = true;
            Main.tileLavaDeath[50] = true;
            Main.tileLavaDeath[52] = true;
            Main.tileLavaDeath[55] = true;
            Main.tileLavaDeath[61] = true;
            Main.tileLavaDeath[62] = true;
            Main.tileLavaDeath[69] = true;
            Main.tileLavaDeath[71] = true;
            Main.tileLavaDeath[72] = true;
            Main.tileLavaDeath[73] = true;
            Main.tileLavaDeath[74] = true;
            Main.tileLavaDeath[78] = true;
            Main.tileLavaDeath[79] = true;
            Main.wallHouse[1] = true;
            Main.wallHouse[4] = true;
            Main.wallHouse[5] = true;
            Main.wallHouse[6] = true;
            Main.wallHouse[10] = true;
            Main.wallHouse[11] = true;
            Main.wallHouse[12] = true;
            for (int index = 0; index < Main.maxMenuItems; ++index)
                this.menuItemScale[index] = 0.8f;
            for (int index = 0; index < 2000; ++index)
                Main.dust[index] = new Dust();
            for (int index = 0; index < 201; ++index)
                Main.item[index] = new Item();
            for (int index = 0; index < 1001; ++index)
            {
                Main.npc[index] = new NPC();
                Main.npc[index].whoAmI = index;
            }
            for (int index = 0; index < 256; ++index)
                Main.player[index] = new Player();
            for (int index = 0; index < 1001; ++index)
                Main.projectile[index] = new Projectile();
            for (int index = 0; index < 201; ++index)
                Main.gore[index] = new Gore();
            for (int index = 0; index < 100; ++index)
                Main.cloud[index] = new Cloud();
            for (int index = 0; index < 100; ++index)
                Main.combatText[index] = new CombatText();
            for (int index = 0; index < Recipe.maxRecipes; ++index)
            {
                Main.recipe[index] = new Recipe();
                Main.availableRecipeY[index] = (float)(65 * index);
            }
            Recipe.SetupRecipes();
            for (int index = 0; index < Main.numChatLines; ++index)
                Main.chatLine[index] = new ChatLine();
            for (int index = 0; index < Liquid.resLiquid; ++index)
                Main.liquid[index] = new Liquid();
            for (int index = 0; index < 10000; ++index)
                Main.liquidBuffer[index] = new LiquidBuffer();
            this.shop[0] = new Chest();
            this.shop[1] = new Chest();
            this.shop[1].SetupShop(1);
            this.shop[2] = new Chest();
            this.shop[2].SetupShop(2);
            this.shop[3] = new Chest();
            this.shop[3].SetupShop(3);
            this.shop[4] = new Chest();
            this.shop[4].SetupShop(4);
            this.shop[5] = new Chest();
            this.shop[5].SetupShop(5);
            Main.teamColor[0] = Color.White;
            Main.teamColor[1] = new Color(230, 40, 20);
            Main.teamColor[2] = new Color(20, 200, 30);
            Main.teamColor[3] = new Color(75, 90, (int)byte.MaxValue);
            Main.teamColor[4] = new Color(200, 180, 0);
            Netplay.Init();
            if (Main.skipMenu)
            {
                WorldGen.clearWorld();
                Main.gameMenu = false;
                Main.LoadPlayers();
                Main.player[Main.myPlayer] = (Player)Main.loadPlayer[0].Clone();
                Main.PlayerPath = Main.loadPlayerPath[0];
                Main.LoadWorlds();
                WorldGen.generateWorld(-1);
                WorldGen.EveryTileFrame();
                Main.player[Main.myPlayer].Spawn();
            }
            else
            {
                //TODO: Bad extern refs
                //IntPtr systemMenu = Main.GetSystemMenu(this.Window.Handle, false);
                //int menuItemCount = Main.GetMenuItemCount(systemMenu);
                //Main.RemoveMenu(systemMenu, menuItemCount - 1, 1024);
            }
            if (!Main.dedServ)
            {
                this.graphics.PreferredBackBufferWidth = Main.screenWidth;
                this.graphics.PreferredBackBufferHeight = Main.screenHeight;
                this.graphics.ApplyChanges();
                base.Initialize();
                this.Window.AllowUserResizing = true;
                this.OpenSettings();
                Star.SpawnStars();
                foreach (DisplayMode displayMode in GraphicsAdapter.DefaultAdapter.SupportedDisplayModes)
                {
                    if (displayMode.Width >= Main.minScreenW && displayMode.Height >= Main.minScreenH && (displayMode.Width <= Main.maxScreenW && displayMode.Height <= Main.maxScreenH))
                    {
                        bool flag = true;
                        for (int index = 0; index < this.numDisplayModes; ++index)
                        {
                            if (displayMode.Width == this.displayWidth[index] && displayMode.Height == this.displayHeight[index])
                            {
                                flag = false;
                                break;
                            }
                        }
                        if (flag)
                        {
                            this.displayHeight[this.numDisplayModes] = displayMode.Height;
                            this.displayWidth[this.numDisplayModes] = displayMode.Width;
                            ++this.numDisplayModes;
                        }
                    }
                }
                if (Main.autoJoin)
                {
                    Main.LoadPlayers();
                    Main.menuMode = 1;
                    Main.menuMultiplayer = true;
                }
            }
        }

        protected override void LoadContent()
        {
            Main.engine = new AudioEngine("Content/TerrariaMusic.xgs");
            Main.soundBank = new SoundBank(Main.engine, "Content/Sound Bank.xsb");
            Main.waveBank = new WaveBank(Main.engine, "Content/Wave Bank.xwb");
            Main.raTexture = this.Content.Load<Texture2D>("Images/ra-logo");
            Main.reTexture = this.Content.Load<Texture2D>("Images/re-logo");
            Main.splashTexture = this.Content.Load<Texture2D>("Images/splash");
            Main.fadeTexture = this.Content.Load<Texture2D>("Images/fade-out");
            for (int index = 1; index < 7; ++index)
                Main.music[index] = Main.soundBank.GetCue("Music_" + (object)index);
            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);
            for (int index = 0; index < 80; ++index)
                Main.tileTexture[index] = this.Content.Load<Texture2D>("Images/Tiles_" + (object)index);
            for (int index = 1; index < 14; ++index)
                Main.wallTexture[index] = this.Content.Load<Texture2D>("Images/Wall_" + (object)index);
            for (int index = 0; index < 7; ++index)
            {
                Main.backgroundTexture[index] = this.Content.Load<Texture2D>("Images/Background_" + (object)index);
                Main.backgroundWidth[index] = Main.backgroundTexture[index].Width;
                Main.backgroundHeight[index] = Main.backgroundTexture[index].Height;
            }
            for (int index = 0; index < 265; ++index)
                Main.itemTexture[index] = this.Content.Load<Texture2D>("Images/Item_" + (object)index);
            for (int index = 0; index < 59; ++index)
                Main.npcTexture[index] = this.Content.Load<Texture2D>("Images/NPC_" + (object)index);
            for (int index = 0; index < 39; ++index)
                Main.projectileTexture[index] = this.Content.Load<Texture2D>("Images/Projectile_" + (object)index);
            for (int index = 1; index < 86; ++index)
                Main.goreTexture[index] = this.Content.Load<Texture2D>("Images/Gore_" + (object)index);
            for (int index = 0; index < 4; ++index)
                Main.cloudTexture[index] = this.Content.Load<Texture2D>("Images/Cloud_" + (object)index);
            for (int index = 0; index < 5; ++index)
                Main.starTexture[index] = this.Content.Load<Texture2D>("Images/Star_" + (object)index);
            for (int index = 0; index < 2; ++index)
                Main.liquidTexture[index] = this.Content.Load<Texture2D>("Images/Liquid_" + (object)index);
            Main.cdTexture = this.Content.Load<Texture2D>("Images/CoolDown");
            Main.logoTexture = this.Content.Load<Texture2D>("Images/Logo");
            Main.dustTexture = this.Content.Load<Texture2D>("Images/Dust");
            Main.sunTexture = this.Content.Load<Texture2D>("Images/Sun");
            Main.sun2Texture = this.Content.Load<Texture2D>("Images/Sun2");
            Main.moonTexture = this.Content.Load<Texture2D>("Images/Moon");
            Main.blackTileTexture = this.Content.Load<Texture2D>("Images/Black_Tile");
            Main.heartTexture = this.Content.Load<Texture2D>("Images/Heart");
            Main.bubbleTexture = this.Content.Load<Texture2D>("Images/Bubble");
            Main.manaTexture = this.Content.Load<Texture2D>("Images/Mana");
            Main.cursorTexture = this.Content.Load<Texture2D>("Images/Cursor");
            Main.ninjaTexture = this.Content.Load<Texture2D>("Images/Ninja");
            Main.treeTopTexture[0] = this.Content.Load<Texture2D>("Images/Tree_Tops_0");
            Main.treeBranchTexture[0] = this.Content.Load<Texture2D>("Images/Tree_Branches_0");
            Main.treeTopTexture[1] = this.Content.Load<Texture2D>("Images/Tree_Tops_1");
            Main.treeBranchTexture[1] = this.Content.Load<Texture2D>("Images/Tree_Branches_1");
            Main.treeTopTexture[2] = this.Content.Load<Texture2D>("Images/Tree_Tops_2");
            Main.treeBranchTexture[2] = this.Content.Load<Texture2D>("Images/Tree_Branches_2");
            Main.shroomCapTexture = this.Content.Load<Texture2D>("Images/Shroom_Tops");
            Main.inventoryBackTexture = this.Content.Load<Texture2D>("Images/Inventory_Back");
            Main.textBackTexture = this.Content.Load<Texture2D>("Images/Text_Back");
            Main.chatTexture = this.Content.Load<Texture2D>("Images/Chat");
            Main.chat2Texture = this.Content.Load<Texture2D>("Images/Chat2");
            Main.chatBackTexture = this.Content.Load<Texture2D>("Images/Chat_Back");
            Main.teamTexture = this.Content.Load<Texture2D>("Images/Team");
            for (int index = 1; index < 16; ++index)
            {
                Main.armorBodyTexture[index] = this.Content.Load<Texture2D>("Images/Armor_Body_" + (object)index);
                Main.armorArmTexture[index] = this.Content.Load<Texture2D>("Images/Armor_Arm_" + (object)index);
            }
            for (int index = 1; index < 27; ++index)
                Main.armorHeadTexture[index] = this.Content.Load<Texture2D>("Images/Armor_Head_" + (object)index);
            for (int index = 1; index < 15; ++index)
                Main.armorLegTexture[index] = this.Content.Load<Texture2D>("Images/Armor_Legs_" + (object)index);
            for (int index = 0; index < 17; ++index)
                Main.playerHairTexture[index] = this.Content.Load<Texture2D>("Images/Player_Hair_" + (object)(index + 1));
            Main.playerEyeWhitesTexture = this.Content.Load<Texture2D>("Images/Player_Eye_Whites");
            Main.playerEyesTexture = this.Content.Load<Texture2D>("Images/Player_Eyes");
            Main.playerHandsTexture = this.Content.Load<Texture2D>("Images/Player_Hands");
            Main.playerHands2Texture = this.Content.Load<Texture2D>("Images/Player_Hands2");
            Main.playerHeadTexture = this.Content.Load<Texture2D>("Images/Player_Head");
            Main.playerPantsTexture = this.Content.Load<Texture2D>("Images/Player_Pants");
            Main.playerShirtTexture = this.Content.Load<Texture2D>("Images/Player_Shirt");
            Main.playerShoesTexture = this.Content.Load<Texture2D>("Images/Player_Shoes");
            Main.playerUnderShirtTexture = this.Content.Load<Texture2D>("Images/Player_Undershirt");
            Main.playerUnderShirt2Texture = this.Content.Load<Texture2D>("Images/Player_Undershirt2");
            Main.chainTexture = this.Content.Load<Texture2D>("Images/Chain");
            Main.chain2Texture = this.Content.Load<Texture2D>("Images/Chain2");
            Main.chain3Texture = this.Content.Load<Texture2D>("Images/Chain3");
            Main.chain4Texture = this.Content.Load<Texture2D>("Images/Chain4");
            Main.chain5Texture = this.Content.Load<Texture2D>("Images/Chain5");
            Main.chain6Texture = this.Content.Load<Texture2D>("Images/Chain6");
            Main.boneArmTexture = this.Content.Load<Texture2D>("Images/Arm_Bone");
            Main.soundGrab = this.Content.Load<SoundEffect>("Sounds/Grab");
            Main.soundInstanceGrab = Main.soundGrab.CreateInstance();
            Main.soundDig[0] = this.Content.Load<SoundEffect>("Sounds/Dig_0");
            Main.soundInstanceDig[0] = Main.soundDig[0].CreateInstance();
            Main.soundDig[1] = this.Content.Load<SoundEffect>("Sounds/Dig_1");
            Main.soundInstanceDig[1] = Main.soundDig[1].CreateInstance();
            Main.soundDig[2] = this.Content.Load<SoundEffect>("Sounds/Dig_2");
            Main.soundInstanceDig[2] = Main.soundDig[2].CreateInstance();
            Main.soundTink[0] = this.Content.Load<SoundEffect>("Sounds/Tink_0");
            Main.soundInstanceTink[0] = Main.soundTink[0].CreateInstance();
            Main.soundTink[1] = this.Content.Load<SoundEffect>("Sounds/Tink_1");
            Main.soundInstanceTink[1] = Main.soundTink[1].CreateInstance();
            Main.soundTink[2] = this.Content.Load<SoundEffect>("Sounds/Tink_2");
            Main.soundInstanceTink[2] = Main.soundTink[2].CreateInstance();
            Main.soundPlayerHit[0] = this.Content.Load<SoundEffect>("Sounds/Player_Hit_0");
            Main.soundInstancePlayerHit[0] = Main.soundPlayerHit[0].CreateInstance();
            Main.soundPlayerHit[1] = this.Content.Load<SoundEffect>("Sounds/Player_Hit_1");
            Main.soundInstancePlayerHit[1] = Main.soundPlayerHit[1].CreateInstance();
            Main.soundPlayerHit[2] = this.Content.Load<SoundEffect>("Sounds/Player_Hit_2");
            Main.soundInstancePlayerHit[2] = Main.soundPlayerHit[2].CreateInstance();
            Main.soundFemaleHit[0] = this.Content.Load<SoundEffect>("Sounds/Female_Hit_0");
            Main.soundInstanceFemaleHit[0] = Main.soundFemaleHit[0].CreateInstance();
            Main.soundFemaleHit[1] = this.Content.Load<SoundEffect>("Sounds/Female_Hit_1");
            Main.soundInstanceFemaleHit[1] = Main.soundFemaleHit[1].CreateInstance();
            Main.soundFemaleHit[2] = this.Content.Load<SoundEffect>("Sounds/Female_Hit_2");
            Main.soundInstanceFemaleHit[2] = Main.soundFemaleHit[2].CreateInstance();
            Main.soundPlayerKilled = this.Content.Load<SoundEffect>("Sounds/Player_Killed");
            Main.soundInstancePlayerKilled = Main.soundPlayerKilled.CreateInstance();
            Main.soundGrass = this.Content.Load<SoundEffect>("Sounds/Grass");
            Main.soundInstanceGrass = Main.soundGrass.CreateInstance();
            Main.soundDoorOpen = this.Content.Load<SoundEffect>("Sounds/Door_Opened");
            Main.soundInstanceDoorOpen = Main.soundDoorOpen.CreateInstance();
            Main.soundDoorClosed = this.Content.Load<SoundEffect>("Sounds/Door_Closed");
            Main.soundInstanceDoorClosed = Main.soundDoorClosed.CreateInstance();
            Main.soundMenuTick = this.Content.Load<SoundEffect>("Sounds/Menu_Tick");
            Main.soundInstanceMenuTick = Main.soundMenuTick.CreateInstance();
            Main.soundMenuOpen = this.Content.Load<SoundEffect>("Sounds/Menu_Open");
            Main.soundInstanceMenuOpen = Main.soundMenuOpen.CreateInstance();
            Main.soundMenuClose = this.Content.Load<SoundEffect>("Sounds/Menu_Close");
            Main.soundInstanceMenuClose = Main.soundMenuClose.CreateInstance();
            Main.soundShatter = this.Content.Load<SoundEffect>("Sounds/Shatter");
            Main.soundInstanceShatter = Main.soundShatter.CreateInstance();
            Main.soundZombie[0] = this.Content.Load<SoundEffect>("Sounds/Zombie_0");
            Main.soundInstanceZombie[0] = Main.soundZombie[0].CreateInstance();
            Main.soundZombie[1] = this.Content.Load<SoundEffect>("Sounds/Zombie_1");
            Main.soundInstanceZombie[1] = Main.soundZombie[1].CreateInstance();
            Main.soundZombie[2] = this.Content.Load<SoundEffect>("Sounds/Zombie_2");
            Main.soundInstanceZombie[2] = Main.soundZombie[2].CreateInstance();
            Main.soundRoar[0] = this.Content.Load<SoundEffect>("Sounds/Roar_0");
            Main.soundInstanceRoar[0] = Main.soundRoar[0].CreateInstance();
            Main.soundRoar[1] = this.Content.Load<SoundEffect>("Sounds/Roar_1");
            Main.soundInstanceRoar[1] = Main.soundRoar[1].CreateInstance();
            Main.soundSplash[0] = this.Content.Load<SoundEffect>("Sounds/Splash_0");
            Main.soundInstanceSplash[0] = Main.soundRoar[0].CreateInstance();
            Main.soundSplash[1] = this.Content.Load<SoundEffect>("Sounds/Splash_1");
            Main.soundInstanceSplash[1] = Main.soundSplash[1].CreateInstance();
            Main.soundDoubleJump = this.Content.Load<SoundEffect>("Sounds/Double_Jump");
            Main.soundInstanceDoubleJump = Main.soundRoar[0].CreateInstance();
            Main.soundRun = this.Content.Load<SoundEffect>("Sounds/Run");
            Main.soundInstanceRun = Main.soundRun.CreateInstance();
            Main.soundCoins = this.Content.Load<SoundEffect>("Sounds/Coins");
            Main.soundInstanceCoins = Main.soundCoins.CreateInstance();
            for (int index = 1; index < 17; ++index)
            {
                Main.soundItem[index] = this.Content.Load<SoundEffect>("Sounds/Item_" + (object)index);
                Main.soundInstanceItem[index] = Main.soundItem[index].CreateInstance();
            }
            for (int index = 1; index < 4; ++index)
            {
                Main.soundNPCHit[index] = this.Content.Load<SoundEffect>("Sounds/NPC_Hit_" + (object)index);
                Main.soundInstanceNPCHit[index] = Main.soundNPCHit[index].CreateInstance();
            }
            for (int index = 1; index < 4; ++index)
            {
                Main.soundNPCKilled[index] = this.Content.Load<SoundEffect>("Sounds/NPC_Killed_" + (object)index);
                Main.soundInstanceNPCKilled[index] = Main.soundNPCKilled[index].CreateInstance();
            }
            Main.fontItemStack = this.Content.Load<SpriteFont>("Fonts/Item_Stack");
            Main.fontMouseText = this.Content.Load<SpriteFont>("Fonts/Mouse_Text");
            Main.fontDeathText = this.Content.Load<SpriteFont>("Fonts/Death_Text");
            Main.fontCombatText = this.Content.Load<SpriteFont>("Fonts/Combat_Text");
        }

        protected override void UnloadContent()
        {
        }

        protected void UpdateMusic()
        {
            if (!Main.dedServ)
            {
                if (this.curMusic > 0)
                {
                    if (!this.IsActive)
                    {
                        if (Main.music[this.curMusic].IsPaused || !Main.music[this.curMusic].IsPlaying)
                        {
                            return;
                        }
                        else
                        {
                            Main.music[this.curMusic].Pause();
                            return;
                        }
                    }
                    else if (Main.music[this.curMusic].IsPaused)
                        Main.music[this.curMusic].Resume();
                }
                bool flag = false;
                Rectangle rectangle1 = new Rectangle((int)Main.screenPosition.X, (int)Main.screenPosition.Y, Main.screenWidth, Main.screenHeight);
                int num = 5000;
                for (int index = 0; index < 1000; ++index)
                {
                    if (Main.npc[index].active && (Main.npc[index].boss || Main.npc[index].type == 13 || (Main.npc[index].type == 14 || Main.npc[index].type == 15) || (Main.npc[index].type == 26 || Main.npc[index].type == 27 || Main.npc[index].type == 28) || Main.npc[index].type == 29))
                    {
                        Rectangle rectangle2 = new Rectangle((int)((double)Main.npc[index].position.X + (double)(Main.npc[index].width / 2)) - num, (int)((double)Main.npc[index].position.Y + (double)(Main.npc[index].height / 2)) - num, num * 2, num * 2);
                        if (rectangle1.Intersects(rectangle2))
                        {
                            flag = true;
                            break;
                        }
                    }
                }
                if ((double)Main.musicVolume == 0.0)
                    this.newMusic = 0;
                else if (Main.gameMenu)
                    this.newMusic = Main.netMode == 2 ? 0 : 6;
                else if (flag)
                    this.newMusic = 5;
                else if (Main.player[Main.myPlayer].zoneEvil || Main.player[Main.myPlayer].zoneMeteor || Main.player[Main.myPlayer].zoneDungeon)
                    this.newMusic = 2;
                else if ((double)Main.player[Main.myPlayer].position.Y > (double)((Main.maxTilesY - 200) * 16))
                    this.newMusic = 2;
                else if ((double)Main.player[Main.myPlayer].position.Y > Main.worldSurface * 16.0 + (double)Main.screenHeight)
                    this.newMusic = 4;
                else if (Main.dayTime)
                    this.newMusic = 1;
                else if (!Main.dayTime)
                    this.newMusic = !Main.bloodMoon ? 3 : 2;
                this.curMusic = this.newMusic;
                for (int index = 1; index < 7; ++index)
                {
                    if (index == this.curMusic)
                    {
                        if (!Main.music[index].IsPlaying)
                        {
                            Main.music[index] = Main.soundBank.GetCue("Music_" + (object)index);
                            Main.music[index].Play();
                            Main.music[index].SetVariable("Volume", Main.musicFade[index] * Main.musicVolume);
                        }
                        else
                        {
                            Main.musicFade[index] += 0.005f;
                            if ((double)Main.musicFade[index] > 1.0)
                                Main.musicFade[index] = 1f;
                            Main.music[index].SetVariable("Volume", Main.musicFade[index] * Main.musicVolume);
                        }
                    }
                    else if (Main.music[index].IsPlaying)
                    {
                        if ((double)Main.musicFade[this.curMusic] > 0.25)
                            Main.musicFade[index] -= 0.005f;
                        else if (this.curMusic == 0)
                            Main.musicFade[index] = 0.0f;
                        if ((double)Main.musicFade[index] <= 0.0)
                        {
                            Main.musicFade[index] -= 0.0f;
                            Main.music[index].Stop(AudioStopOptions.Immediate);
                        }
                        else
                            Main.music[index].SetVariable("Volume", Main.musicFade[index] * Main.musicVolume);
                    }
                    else
                        Main.musicFade[index] = 0.0f;
                }
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (!Main.dedServ)
            {
                if (Main.fixedTiming)
                {
                    if (this.IsActive)
                    {
                        this.IsFixedTimeStep = false;
                        this.graphics.SynchronizeWithVerticalRetrace = true;
                    }
                    else
                        this.IsFixedTimeStep = true;
                }
                else
                    this.IsFixedTimeStep = true;
                this.UpdateMusic();
                if (Main.showSplash)
                {
                    return;
                }
                else
                {
                    if (!Main.gameMenu && Main.netMode == 1)
                    {
                        if (!Main.saveTime.IsRunning)
                            Main.saveTime.Start();
                        if (Main.saveTime.ElapsedMilliseconds > 300000L)
                        {
                            Main.saveTime.Reset();
                            WorldGen.saveToonWhilePlaying();
                        }
                    }
                    else if (!Main.gameMenu && Main.autoSave)
                    {
                        if (!Main.saveTime.IsRunning)
                            Main.saveTime.Start();
                        if (Main.saveTime.ElapsedMilliseconds > 600000L)
                        {
                            Main.saveTime.Reset();
                            WorldGen.saveToonWhilePlaying();
                            WorldGen.saveAndPlay();
                        }
                    }
                    else if (Main.saveTime.IsRunning)
                        Main.saveTime.Stop();
                    ++Main.updateTime;
                    if (Main.updateTime >= 60)
                    {
                        Main.frameRate = Main.drawTime;
                        Main.updateTime = 0;
                        Main.drawTime = 0;
                        if (Main.frameRate == 60)
                        {
                            Lighting.lightPasses = 2;
                            Lighting.lightSkip = 0;
                            Main.cloudLimit = 100;
                            Gore.goreTime = 1200;
                        }
                        else if (Main.frameRate >= 58)
                        {
                            Lighting.lightPasses = 2;
                            Lighting.lightSkip = 0;
                            Main.cloudLimit = 100;
                            Gore.goreTime = 600;
                        }
                        else if (Main.frameRate >= 43)
                        {
                            Lighting.lightPasses = 2;
                            Lighting.lightSkip = 1;
                            Main.cloudLimit = 75;
                            Gore.goreTime = 300;
                        }
                        else if (Main.frameRate >= 28)
                        {
                            if (!Main.gameMenu)
                            {
                                Liquid.maxLiquid = 3000;
                                Liquid.cycles = 6;
                            }
                            Lighting.lightPasses = 2;
                            Lighting.lightSkip = 2;
                            Main.cloudLimit = 50;
                            Gore.goreTime = 180;
                        }
                        else
                        {
                            Lighting.lightPasses = 2;
                            Lighting.lightSkip = 4;
                            Main.cloudLimit = 0;
                            Gore.goreTime = 0;
                        }
                        if (Liquid.quickSettle)
                        {
                            Liquid.maxLiquid = Liquid.resLiquid;
                            Liquid.cycles = 1;
                        }
                        else if (Main.frameRate == 60)
                        {
                            Liquid.maxLiquid = 5000;
                            Liquid.cycles = 7;
                        }
                        else if (Main.frameRate >= 58)
                        {
                            Liquid.maxLiquid = 5000;
                            Liquid.cycles = 12;
                        }
                        else if (Main.frameRate >= 43)
                        {
                            Liquid.maxLiquid = 4000;
                            Liquid.cycles = 13;
                        }
                        else if (Main.frameRate >= 28)
                        {
                            Liquid.maxLiquid = 3500;
                            Liquid.cycles = 15;
                        }
                        else
                        {
                            Liquid.maxLiquid = 3000;
                            Liquid.cycles = 17;
                        }
                        if (Main.netMode == 2)
                            Main.cloudLimit = 0;
                    }
                    Main.hasFocus = this.IsActive && true;
                    if (!this.IsActive && Main.netMode == 0)
                    {
                        this.IsMouseVisible = true;
                        if (Main.netMode != 2 && Main.myPlayer >= 0)
                            Main.player[Main.myPlayer].delayUseItem = true;
                        Main.mouseLeftRelease = false;
                        Main.mouseRightRelease = false;
                        if (Main.gameMenu)
                            Main.UpdateMenu();
                        Main.gamePaused = true;
                        return;
                    }
                    else
                    {
                        this.IsMouseVisible = false;
                        if (Main.keyState.IsKeyDown(Keys.F10) && !Main.chatMode && !Main.editSign)
                        {
                            if (Main.frameRelease)
                            {
                                Main.PlaySound(12, -1, -1, 1);
                                Main.showFrameRate = !Main.showFrameRate && true;
                            }
                            Main.frameRelease = false;
                        }
                        else
                            Main.frameRelease = true;
                        if (Main.keyState.IsKeyDown(Keys.F11))
                        {
                            if (Main.releaseUI)
                                Main.hideUI = !Main.hideUI && true;
                            Main.releaseUI = false;
                        }
                        else
                            Main.releaseUI = true;
                        if ((Main.keyState.IsKeyDown(Keys.LeftAlt) || Main.keyState.IsKeyDown(Keys.RightAlt)) && Main.keyState.IsKeyDown(Keys.Enter))
                        {
                            if (this.toggleFullscreen)
                            {
                                this.graphics.ToggleFullScreen();
                                Main.chatRelease = false;
                            }
                            this.toggleFullscreen = false;
                        }
                        else
                            this.toggleFullscreen = true;
                        Main.oldMouseState = Main.mouseState;
                        Main.mouseState = Mouse.GetState();
                        Main.keyState = Keyboard.GetState();
                        if (Main.editSign)
                            Main.chatMode = false;
                        if (Main.chatMode)
                        {
                            string str = Main.chatText;
                            Main.chatText = Main.GetInputText(Main.chatText);
                            while ((double)Main.fontMouseText.MeasureString(Main.chatText).X > 470.0)
                                Main.chatText = Main.chatText.Substring(0, Main.chatText.Length - 1);
                            if (str != Main.chatText)
                                Main.PlaySound(12, -1, -1, 1);
                            if (Main.inputTextEnter && Main.chatRelease)
                            {
                                if (Main.chatText != "")
                                    NetMessage.SendData(25, -1, -1, Main.chatText, Main.myPlayer, 0.0f, 0.0f, 0.0f);
                                Main.chatText = "";
                                Main.chatMode = false;
                                Main.chatRelease = false;
                                Main.PlaySound(11, -1, -1, 1);
                            }
                        }
                        if (Main.keyState.IsKeyDown(Keys.Enter) && Main.netMode == 1 && (!Main.keyState.IsKeyDown(Keys.LeftAlt) && !Main.keyState.IsKeyDown(Keys.RightAlt)))
                        {
                            if (Main.chatRelease && !Main.chatMode && !Main.editSign)
                            {
                                Main.PlaySound(10, -1, -1, 1);
                                Main.chatMode = true;
                                Main.chatText = "";
                            }
                            Main.chatRelease = false;
                        }
                        else
                            Main.chatRelease = true;
                        if (Main.gameMenu)
                        {
                            Main.UpdateMenu();
                            if (Main.netMode != 2)
                                return;
                            else
                                Main.gamePaused = false;
                        }
                    }
                }
            }
            if (Main.netMode == 1)
            {
                for (int index = 0; index < 44; ++index)
                {
                    if (Main.player[Main.myPlayer].inventory[index].IsNotTheSameAs(Main.clientPlayer.inventory[index]))
                        NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].inventory[index].name, Main.myPlayer, (float)index, 0.0f, 0.0f);
                }
                if (Main.player[Main.myPlayer].armor[0].IsNotTheSameAs(Main.clientPlayer.armor[0]))
                    NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[0].name, Main.myPlayer, 44f, 0.0f, 0.0f);
                if (Main.player[Main.myPlayer].armor[1].IsNotTheSameAs(Main.clientPlayer.armor[1]))
                    NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[1].name, Main.myPlayer, 45f, 0.0f, 0.0f);
                if (Main.player[Main.myPlayer].armor[2].IsNotTheSameAs(Main.clientPlayer.armor[2]))
                    NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[2].name, Main.myPlayer, 46f, 0.0f, 0.0f);
                if (Main.player[Main.myPlayer].armor[3].IsNotTheSameAs(Main.clientPlayer.armor[3]))
                    NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[3].name, Main.myPlayer, 47f, 0.0f, 0.0f);
                if (Main.player[Main.myPlayer].armor[4].IsNotTheSameAs(Main.clientPlayer.armor[4]))
                    NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[4].name, Main.myPlayer, 48f, 0.0f, 0.0f);
                if (Main.player[Main.myPlayer].armor[5].IsNotTheSameAs(Main.clientPlayer.armor[5]))
                    NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[5].name, Main.myPlayer, 49f, 0.0f, 0.0f);
                if (Main.player[Main.myPlayer].armor[6].IsNotTheSameAs(Main.clientPlayer.armor[6]))
                    NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[6].name, Main.myPlayer, 50f, 0.0f, 0.0f);
                if (Main.player[Main.myPlayer].armor[7].IsNotTheSameAs(Main.clientPlayer.armor[7]))
                    NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[7].name, Main.myPlayer, 51f, 0.0f, 0.0f);
                if (Main.player[Main.myPlayer].armor[8].IsNotTheSameAs(Main.clientPlayer.armor[8]))
                    NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[8].name, Main.myPlayer, 52f, 0.0f, 0.0f);
                if (Main.player[Main.myPlayer].armor[9].IsNotTheSameAs(Main.clientPlayer.armor[9]))
                    NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[9].name, Main.myPlayer, 53f, 0.0f, 0.0f);
                if (Main.player[Main.myPlayer].armor[10].IsNotTheSameAs(Main.clientPlayer.armor[10]))
                    NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[10].name, Main.myPlayer, 54f, 0.0f, 0.0f);
                if (Main.player[Main.myPlayer].chest != Main.clientPlayer.chest)
                    NetMessage.SendData(33, -1, -1, "", Main.player[Main.myPlayer].chest, 0.0f, 0.0f, 0.0f);
                if (Main.player[Main.myPlayer].talkNPC != Main.clientPlayer.talkNPC)
                    NetMessage.SendData(40, -1, -1, "", Main.myPlayer, 0.0f, 0.0f, 0.0f);
                if (Main.player[Main.myPlayer].zoneEvil != Main.clientPlayer.zoneEvil)
                    NetMessage.SendData(36, -1, -1, "", Main.myPlayer, 0.0f, 0.0f, 0.0f);
                if (Main.player[Main.myPlayer].zoneMeteor != Main.clientPlayer.zoneMeteor)
                    NetMessage.SendData(36, -1, -1, "", Main.myPlayer, 0.0f, 0.0f, 0.0f);
                if (Main.player[Main.myPlayer].zoneDungeon != Main.clientPlayer.zoneDungeon)
                    NetMessage.SendData(36, -1, -1, "", Main.myPlayer, 0.0f, 0.0f, 0.0f);
                if (Main.player[Main.myPlayer].zoneJungle != Main.clientPlayer.zoneJungle)
                    NetMessage.SendData(36, -1, -1, "", Main.myPlayer, 0.0f, 0.0f, 0.0f);
            }
            if (Main.netMode == 1)
                Main.clientPlayer = (Player)Main.player[Main.myPlayer].clientClone();
            if (Main.netMode == 0 && (Main.playerInventory || Main.npcChatText != "" || Main.player[Main.myPlayer].sign >= 0) && Main.autoPause)
            {
                Keys[] pressedKeys = Main.keyState.GetPressedKeys();
                Main.player[Main.myPlayer].controlInv = false;
                for (int index = 0; index < pressedKeys.Length; ++index)
                {
                    if (string.Concat((object)pressedKeys[index]) == Main.cInv)
                        Main.player[Main.myPlayer].controlInv = true;
                }
                if (Main.player[Main.myPlayer].controlInv)
                {
                    if (Main.player[Main.myPlayer].releaseInventory)
                        Main.player[Main.myPlayer].toggleInv();
                    Main.player[Main.myPlayer].releaseInventory = false;
                }
                else
                    Main.player[Main.myPlayer].releaseInventory = true;
                if (Main.playerInventory)
                {
                    int num = (Main.mouseState.ScrollWheelValue - Main.oldMouseState.ScrollWheelValue) / 120;
                    Main.focusRecipe += num;
                    if (Main.focusRecipe > Main.numAvailableRecipes - 1)
                        Main.focusRecipe = Main.numAvailableRecipes - 1;
                    if (Main.focusRecipe < 0)
                        Main.focusRecipe = 0;
                    Main.player[Main.myPlayer].dropItemCheck();
                }
                Main.player[Main.myPlayer].head = Main.player[Main.myPlayer].armor[0].headSlot;
                Main.player[Main.myPlayer].body = Main.player[Main.myPlayer].armor[1].bodySlot;
                Main.player[Main.myPlayer].legs = Main.player[Main.myPlayer].armor[2].legSlot;
                if (!Main.player[Main.myPlayer].hostile)
                {
                    if (Main.player[Main.myPlayer].armor[8].headSlot >= 0)
                        Main.player[Main.myPlayer].head = Main.player[Main.myPlayer].armor[8].headSlot;
                    if (Main.player[Main.myPlayer].armor[9].bodySlot >= 0)
                        Main.player[Main.myPlayer].body = Main.player[Main.myPlayer].armor[9].bodySlot;
                    if (Main.player[Main.myPlayer].armor[10].legSlot >= 0)
                        Main.player[Main.myPlayer].legs = Main.player[Main.myPlayer].armor[10].legSlot;
                }
                if (Main.editSign)
                {
                    if (Main.player[Main.myPlayer].sign == -1)
                    {
                        Main.editSign = false;
                    }
                    else
                    {
                        Main.npcChatText = Main.GetInputText(Main.npcChatText);
                        if (Main.inputTextEnter)
                        {
                            byte[] bytes = new byte[1]
              {
                (byte) 10
              };
                            Main.npcChatText = Main.npcChatText + Encoding.ASCII.GetString(bytes);
                        }
                    }
                }
                Main.gamePaused = true;
            }
            else
            {
                Main.gamePaused = false;
                if (!Main.dedServ && ((double)Main.screenPosition.Y < Main.worldSurface * 16.0 + 16.0 && (int)byte.MaxValue - (int)Main.tileColor.R - 100 > 0 && Main.netMode != 2))
                {
                    Star.UpdateStars();
                    Cloud.UpdateClouds();
                }
                for (int i = 0; i < (int)byte.MaxValue; ++i)
                {
                    if (Main.ignoreErrors)
                    {
                        try
                        {
                            Main.player[i].UpdatePlayer(i);
                        }
                        catch
                        {
                            Debug.WriteLine("Error: player[" + (object)i + "].UpdatePlayer(" + (string)(object)i + ")");
                        }
                    }
                    else
                        Main.player[i].UpdatePlayer(i);
                }
                if (Main.netMode != 1)
                    NPC.SpawnNPC();
                for (int index = 0; index < (int)byte.MaxValue; ++index)
                {
                    Main.player[index].activeNPCs = 0;
                    Main.player[index].townNPCs = 0;
                }
                for (int i = 0; i < 1000; ++i)
                {
                    if (Main.ignoreErrors)
                    {
                        try
                        {
                            Main.npc[i].UpdateNPC(i);
                        }
                        catch (Exception ex)
                        {
                            Main.npc[i] = new NPC();
                            Debug.WriteLine("Error: npc[" + (object)i + "].UpdateNPC(" + (string)(object)i + ")");
                            Debug.WriteLine((object)ex);
                        }
                    }
                    else
                        Main.npc[i].UpdateNPC(i);
                }
                for (int index = 0; index < 200; ++index)
                {
                    if (Main.ignoreErrors)
                    {
                        try
                        {
                            Main.gore[index].Update();
                        }
                        catch
                        {
                            Main.gore[index] = new Gore();
                            Debug.WriteLine("Error: gore[" + (object)index + "].Update()");
                        }
                    }
                    else
                        Main.gore[index].Update();
                }
                for (int i = 0; i < 1000; ++i)
                {
                    if (Main.ignoreErrors)
                    {
                        try
                        {
                            Main.projectile[i].Update(i);
                        }
                        catch
                        {
                            Main.projectile[i] = new Projectile();
                            Debug.WriteLine("Error: projectile[" + (object)i + "].Update(" + (string)(object)i + ")");
                        }
                    }
                    else
                        Main.projectile[i].Update(i);
                }
                for (int i = 0; i < 200; ++i)
                {
                    if (Main.ignoreErrors)
                    {
                        try
                        {
                            Main.item[i].UpdateItem(i);
                        }
                        catch
                        {
                            Main.item[i] = new Item();
                            Debug.WriteLine("Error: item[" + (object)i + "].UpdateItem(" + (string)(object)i + ")");
                        }
                    }
                    else
                        Main.item[i].UpdateItem(i);
                }
                if (Main.ignoreErrors)
                {
                    try
                    {
                        Dust.UpdateDust();
                    }
                    catch
                    {
                        for (int index = 0; index < 2000; ++index)
                            Main.dust[index] = new Dust();
                        Debug.WriteLine("Error: Dust.Update()");
                    }
                }
                else
                    Dust.UpdateDust();
                if (Main.netMode != 2)
                    CombatText.UpdateCombatText();
                if (Main.ignoreErrors)
                {
                    try
                    {
                        Main.UpdateTime();
                    }
                    catch
                    {
                        Debug.WriteLine("Error: UpdateTime()");
                        Main.checkForSpawns = 0;
                    }
                }
                else
                    Main.UpdateTime();
                if (Main.netMode != 1)
                {
                    if (Main.ignoreErrors)
                    {
                        try
                        {
                            WorldGen.UpdateWorld();
                            Main.UpdateInvasion();
                        }
                        catch
                        {
                            Debug.WriteLine("Error: WorldGen.UpdateWorld()");
                        }
                    }
                    else
                    {
                        WorldGen.UpdateWorld();
                        Main.UpdateInvasion();
                    }
                }
                if (Main.ignoreErrors)
                {
                    try
                    {
                        if (Main.netMode == 2)
                            Main.UpdateServer();
                        if (Main.netMode == 1)
                            Main.UpdateClient();
                    }
                    catch
                    {
                        if (Main.netMode == 2)
                            Debug.WriteLine("Error: UpdateServer()");
                        else
                            Debug.WriteLine("Error: UpdateClient();");
                    }
                }
                else
                {
                    if (Main.netMode == 2)
                        Main.UpdateServer();
                    if (Main.netMode == 1)
                        Main.UpdateClient();
                }
                if (Main.ignoreErrors)
                {
                    try
                    {
                        for (int index = 0; index < Main.numChatLines; ++index)
                        {
                            if (Main.chatLine[index].showTime > 0)
                                --Main.chatLine[index].showTime;
                        }
                    }
                    catch
                    {
                        for (int index = 0; index < Main.numChatLines; ++index)
                            Main.chatLine[index] = new ChatLine();
                        Debug.WriteLine("Error: chatLine[];");
                    }
                }
                else
                {
                    for (int index = 0; index < Main.numChatLines; ++index)
                    {
                        if (Main.chatLine[index].showTime > 0)
                            --Main.chatLine[index].showTime;
                    }
                }
                base.Update(gameTime);
            }
        }

        private static void UpdateMenu()
        {
            Main.playerInventory = false;
            Main.exitScale = 0.8f;
            if (Main.netMode == 0)
            {
                if (!Main.grabSky)
                {
                    Main.time += 86.4;
                    if (!Main.dayTime)
                    {
                        if (Main.time > 32400.0)
                        {
                            Main.bloodMoon = false;
                            Main.time = 0.0;
                            Main.dayTime = true;
                            ++Main.moonPhase;
                            if (Main.moonPhase >= 8)
                                Main.moonPhase = 0;
                        }
                    }
                    else if (Main.time > 54000.0)
                    {
                        Main.time = 0.0;
                        Main.dayTime = false;
                    }
                }
            }
            else if (Main.netMode == 1)
                Main.UpdateTime();
        }

        //[DllImport("user32.dll", CharSet = CharSet.Auto)]
       // public static extern short GetKeyState(int keyCode);

        public static string GetInputText(string oldString)
        {
            if (!Main.hasFocus)
            {
                return oldString;
            }
            else
            {
                Main.inputTextEnter = false;
                string str1 = oldString ?? "";
                Main.oldInputText = Main.inputText;
                Main.inputText = Keyboard.GetState();
                //TODO: Bad extern refs
                bool flag1 = false;// ((int)(ushort)Main.GetKeyState(20) & (int)ushort.MaxValue) != 0;
                bool flag2 = false;
                if (Main.inputText.IsKeyDown(Keys.LeftShift) || Main.inputText.IsKeyDown(Keys.RightShift))
                    flag2 = true;
                Keys[] pressedKeys1 = Main.inputText.GetPressedKeys();
                Keys[] pressedKeys2 = Main.oldInputText.GetPressedKeys();
                bool flag3 = false;
                if (Main.inputText.IsKeyDown(Keys.Back) && Main.oldInputText.IsKeyDown(Keys.Back))
                {
                    if (Main.backSpaceCount == 0)
                    {
                        Main.backSpaceCount = 7;
                        flag3 = true;
                    }
                    --Main.backSpaceCount;
                }
                else
                    Main.backSpaceCount = 15;
                for (int index1 = 0; index1 < pressedKeys1.Length; ++index1)
                {
                    bool flag4 = true;
                    for (int index2 = 0; index2 < pressedKeys2.Length; ++index2)
                    {
                        if (pressedKeys1[index1] == pressedKeys2[index2])
                            flag4 = false;
                    }
                    string str2 = string.Concat((object)pressedKeys1[index1]);
                    if (str2 == "Back" && (flag4 || flag3))
                    {
                        if (str1.Length > 0)
                            str1 = str1.Substring(0, str1.Length - 1);
                    }
                    else if (flag4)
                    {
                        if (str2 == "Space")
                            str2 = " ";
                        else if (str2.Length == 1)
                        {
                            int num = Convert.ToInt32(Convert.ToChar(str2));
                            if (num >= 65 && num <= 90 && (!flag2 && !flag1 || flag2 && flag1))
                                str2 = string.Concat((object)Convert.ToChar(num + 32));
                        }
                        else if (str2.Length == 2 && str2.Substring(0, 1) == "D")
                        {
                            str2 = str2.Substring(1, 1);
                            if (flag2)
                            {
                                if (str2 == "1")
                                    str2 = "!";
                                if (str2 == "2")
                                    str2 = "@";
                                if (str2 == "3")
                                    str2 = "#";
                                if (str2 == "4")
                                    str2 = "$";
                                if (str2 == "5")
                                    str2 = "%";
                                if (str2 == "6")
                                    str2 = "^";
                                if (str2 == "7")
                                    str2 = "&";
                                if (str2 == "8")
                                    str2 = "*";
                                if (str2 == "9")
                                    str2 = "(";
                                if (str2 == "0")
                                    str2 = ")";
                            }
                        }
                        else if (str2.Length == 7 && str2.Substring(0, 6) == "NumPad")
                            str2 = str2.Substring(6, 1);
                        else if (str2 == "Divide")
                            str2 = "/";
                        else if (str2 == "Multiply")
                            str2 = "*";
                        else if (str2 == "Subtract")
                            str2 = "-";
                        else if (str2 == "Add")
                            str2 = "+";
                        else if (str2 == "Decimal")
                        {
                            str2 = ".";
                        }
                        else
                        {
                            if (str2 == "OemSemicolon")
                                str2 = ";";
                            else if (str2 == "OemPlus")
                                str2 = "=";
                            else if (str2 == "OemComma")
                                str2 = ",";
                            else if (str2 == "OemMinus")
                                str2 = "-";
                            else if (str2 == "OemPeriod")
                                str2 = ".";
                            else if (str2 == "OemQuestion")
                                str2 = "/";
                            else if (str2 == "OemTilde")
                                str2 = "`";
                            else if (str2 == "OemOpenBrackets")
                                str2 = "[";
                            else if (str2 == "OemPipe")
                                str2 = "\\";
                            else if (str2 == "OemCloseBrackets")
                                str2 = "]";
                            else if (str2 == "OemQuotes")
                                str2 = "'";
                            else if (str2 == "OemBackslash")
                                str2 = "\\";
                            if (flag2)
                            {
                                if (str2 == ";")
                                    str2 = ":";
                                else if (str2 == "=")
                                    str2 = "+";
                                else if (str2 == ",")
                                    str2 = "<";
                                else if (str2 == "-")
                                    str2 = "_";
                                else if (str2 == ".")
                                    str2 = ">";
                                else if (str2 == "/")
                                    str2 = "?";
                                else if (str2 == "`")
                                    str2 = "~";
                                else if (str2 == "[")
                                    str2 = "{";
                                else if (str2 == "\\")
                                    str2 = "|";
                                else if (str2 == "]")
                                    str2 = "}";
                                else if (str2 == "'")
                                    str2 = "\"";
                            }
                        }
                        if (str2 == "Enter")
                            Main.inputTextEnter = true;
                        if (str2.Length == 1)
                            str1 = str1 + str2;
                    }
                }
                return str1;
            }
        }

        protected void MouseText(string cursorText, int rare = 0)
        {
            int num1 = Main.mouseState.X + 10;
            int num2 = Main.mouseState.Y + 10;
            Color color1 = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
            if (Main.toolTip.type > 0)
            {
                rare = Main.toolTip.rare;
                int length = 20;
                int index1 = 1;
                string[] strArray1 = new string[length];
                strArray1[0] = Main.toolTip.name;
                if (Main.toolTip.stack > 1)
                {
                    string[] strArray2;
                    (strArray2 = strArray1)[0] = string.Concat(new object[4]
          {
            (object) strArray2[0],
            (object) " (",
            (object) Main.toolTip.stack,
            (object) ")"
          });
                }
                if (Main.toolTip.social)
                {
                    strArray1[index1] = "Equipped in social slot";
                    int index2 = index1 + 1;
                    strArray1[index2] = "No stats will be gained";
                    index1 = index2 + 1;
                }
                else
                {
                    if (Main.toolTip.damage > 0)
                    {
                        int num3 = Main.toolTip.damage;
                        if (Main.toolTip.mana > 0)
                            num3 = (int)Math.Round((double)num3 * (double)Main.player[Main.myPlayer].magicBoost);
                        strArray1[index1] = (string)(object)num3 + (object)" damage";
                        ++index1;
                        //TODO: Figure out if we really need?
                        //if (Main.toolTip.useStyle > 0)
                        //{
                        //    strArray1[index1] = Main.toolTip.useAnimation > 8 ? (Main.toolTip.useAnimation > 15 ? (Main.toolTip.useAnimation > 20 ? (Main.toolTip.useAnimation > 25 ? (Main.toolTip.useAnimation > 30 ? (Main.toolTip.useAnimation > 40 ? (Main.toolTip.useAnimation > 50 ? "Snail" : "Extremely slow") : "Very slow") : "Slow") : "Average") : "Fast") : "Very fast") : "Insanely fast";
                        //    string[] strArray2;
                        //    IntPtr index2;
                        //    (strArray2 = strArray1)[(int)(index2 = (IntPtr)index1)] = strArray2[index2] + " speed";
                        //    ++index1;
                        //}
                    }
                    if (Main.toolTip.headSlot > 0 || Main.toolTip.bodySlot > 0 || Main.toolTip.legSlot > 0 || Main.toolTip.accessory)
                    {
                        strArray1[index1] = "Equipable";
                        ++index1;
                    }
                    if (Main.toolTip.vanity)
                    {
                        strArray1[index1] = "Vanity Item";
                        ++index1;
                    }
                    if (Main.toolTip.defense > 0)
                    {
                        strArray1[index1] = (string)(object)Main.toolTip.defense + (object)" defense";
                        ++index1;
                    }
                    if (Main.toolTip.pick > 0)
                    {
                        strArray1[index1] = (string)(object)Main.toolTip.pick + (object)"% pickaxe power";
                        ++index1;
                    }
                    if (Main.toolTip.axe > 0)
                    {
                        strArray1[index1] = (string)(object)Main.toolTip.axe + (object)"% axe power";
                        ++index1;
                    }
                    if (Main.toolTip.hammer > 0)
                    {
                        strArray1[index1] = (string)(object)Main.toolTip.hammer + (object)"% hammer power";
                        ++index1;
                    }
                    if (Main.toolTip.healLife > 0)
                    {
                        strArray1[index1] = "Restores " + (object)Main.toolTip.healLife + " life";
                        ++index1;
                    }
                    if (Main.toolTip.healMana > 0)
                    {
                        strArray1[index1] = "Restores " + (object)Main.toolTip.healMana + " mana";
                        ++index1;
                    }
                    if (Main.toolTip.mana > 0 && (Main.toolTip.type != (int)sbyte.MaxValue || !Main.player[Main.myPlayer].spaceGun))
                    {
                        strArray1[index1] = "Uses " + (object)(int)((double)Main.toolTip.mana * (double)Main.player[Main.myPlayer].manaCost) + " mana";
                        ++index1;
                    }
                    if (Main.toolTip.createWall > 0 || Main.toolTip.createTile > -1)
                    {
                        if (Main.toolTip.type != 213)
                        {
                            strArray1[index1] = "Can be placed";
                            ++index1;
                        }
                    }
                    else if (Main.toolTip.ammo > 0)
                    {
                        strArray1[index1] = "Ammo";
                        ++index1;
                    }
                    else if (Main.toolTip.consumable)
                    {
                        strArray1[index1] = "Consumable";
                        ++index1;
                    }
                    if (Main.toolTip.material)
                    {
                        strArray1[index1] = "Material";
                        ++index1;
                    }
                    if (Main.toolTip.toolTip != null)
                    {
                        strArray1[index1] = Main.toolTip.toolTip;
                        ++index1;
                    }
                    if (Main.toolTip.wornArmor && Main.player[Main.myPlayer].setBonus != "")
                    {
                        strArray1[index1] = "Set bonus: " + Main.player[Main.myPlayer].setBonus;
                        ++index1;
                    }
                }
                if (Main.npcShop > 0)
                {
                    if (Main.toolTip.value > 0)
                    {
                        string str = "";
                        int num3 = 0;
                        int num4 = 0;
                        int num5 = 0;
                        int num6 = 0;
                        int num7 = Main.toolTip.value * Main.toolTip.stack;
                        if (!Main.toolTip.buy)
                            num7 /= 5;
                        if (num7 < 1)
                            num7 = 1;
                        if (num7 >= 1000000)
                        {
                            num3 = num7 / 1000000;
                            num7 -= num3 * 1000000;
                        }
                        if (num7 >= 10000)
                        {
                            num4 = num7 / 10000;
                            num7 -= num4 * 10000;
                        }
                        if (num7 >= 100)
                        {
                            num5 = num7 / 100;
                            num7 -= num5 * 100;
                        }
                        if (num7 >= 1)
                            num6 = num7;
                        if (num3 > 0)
                            str = str + (object)num3 + " platinum ";
                        if (num4 > 0)
                            str = str + (object)num4 + " gold ";
                        if (num5 > 0)
                            str = str + (object)num5 + " silver ";
                        if (num6 > 0)
                            str = str + (object)num6 + " copper ";
                        strArray1[index1] = Main.toolTip.buy ? "Buy price: " + str : "Sell price: " + str;
                        ++index1;
                        float num8 = (float)Main.mouseTextColor / (float)byte.MaxValue;
                        if (num3 > 0)
                            color1 = new Color((int)(byte)(220.0 * (double)num8), (int)(byte)(220.0 * (double)num8), (int)(byte)(198.0 * (double)num8), (int)Main.mouseTextColor);
                        else if (num4 > 0)
                            color1 = new Color((int)(byte)(224.0 * (double)num8), (int)(byte)(201.0 * (double)num8), (int)(byte)(92.0 * (double)num8), (int)Main.mouseTextColor);
                        else if (num5 > 0)
                            color1 = new Color((int)(byte)(181.0 * (double)num8), (int)(byte)(192.0 * (double)num8), (int)(byte)(193.0 * (double)num8), (int)Main.mouseTextColor);
                        else if (num6 > 0)
                            color1 = new Color((int)(byte)(246.0 * (double)num8), (int)(byte)(138.0 * (double)num8), (int)(byte)(96.0 * (double)num8), (int)Main.mouseTextColor);
                    }
                    else
                    {
                        float num3 = (float)Main.mouseTextColor / (float)byte.MaxValue;
                        strArray1[index1] = "No value";
                        ++index1;
                        color1 = new Color((int)(byte)(120.0 * (double)num3), (int)(byte)(120.0 * (double)num3), (int)(byte)(120.0 * (double)num3), (int)Main.mouseTextColor);
                    }
                }
                Vector2 vector2_1 = new Vector2();
                int num9 = 0;
                for (int index2 = 0; index2 < index1; ++index2)
                {
                    Vector2 vector2_2 = Main.fontMouseText.MeasureString(strArray1[index2]);
                    if ((double)vector2_2.X > (double)vector2_1.X)
                        vector2_1.X = vector2_2.X;
                    vector2_1.Y += vector2_2.Y + (float)num9;
                }
                if ((double)num1 + (double)vector2_1.X + 4.0 > (double)Main.screenWidth)
                    num1 = (int)((double)Main.screenWidth - (double)vector2_1.X - 4.0);
                if ((double)num2 + (double)vector2_1.Y + 4.0 > (double)Main.screenHeight)
                    num2 = (int)((double)Main.screenHeight - (double)vector2_1.Y - 4.0);
                int num10 = 0;
                float num11 = (float)Main.mouseTextColor / (float)byte.MaxValue;
                for (int index2 = 0; index2 < index1; ++index2)
                {
                    for (int index3 = 0; index3 < 5; ++index3)
                    {
                        int num3 = num1;
                        int num4 = num2 + num10;
                        Color color2 = Color.Black;
                        if (index3 == 0)
                            num3 -= 2;
                        else if (index3 == 1)
                            num3 += 2;
                        else if (index3 == 2)
                            num4 -= 2;
                        else if (index3 == 3)
                        {
                            num4 += 2;
                        }
                        else
                        {
                            color2 = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
                            if (index2 == 0)
                            {
                                if (rare == 1)
                                    color2 = new Color((int)(byte)(150.0 * (double)num11), (int)(byte)(150.0 * (double)num11), (int)(byte)((double)byte.MaxValue * (double)num11), (int)Main.mouseTextColor);
                                if (rare == 2)
                                    color2 = new Color((int)(byte)(150.0 * (double)num11), (int)(byte)((double)byte.MaxValue * (double)num11), (int)(byte)(150.0 * (double)num11), (int)Main.mouseTextColor);
                                if (rare == 3)
                                    color2 = new Color((int)(byte)((double)byte.MaxValue * (double)num11), (int)(byte)(200.0 * (double)num11), (int)(byte)(150.0 * (double)num11), (int)Main.mouseTextColor);
                                if (rare == 4)
                                    color2 = new Color((int)(byte)((double)byte.MaxValue * (double)num11), (int)(byte)(150.0 * (double)num11), (int)(byte)(150.0 * (double)num11), (int)Main.mouseTextColor);
                            }
                            else if (index2 == index1 - 1)
                                color2 = color1;
                        }
                        this.spriteBatch.DrawString(Main.fontMouseText, strArray1[index2], new Vector2((float)num3, (float)num4), color2, 0.0f, new Vector2(), 1f, SpriteEffects.None, 0.0f);
                    }
                    num10 += (int)((double)Main.fontMouseText.MeasureString(strArray1[index2]).Y + (double)num9);
                }
            }
            else
            {
                Vector2 vector2 = Main.fontMouseText.MeasureString(cursorText);
                if ((double)num1 + (double)vector2.X + 4.0 > (double)Main.screenWidth)
                    num1 = (int)((double)Main.screenWidth - (double)vector2.X - 4.0);
                if ((double)num2 + (double)vector2.Y + 4.0 > (double)Main.screenHeight)
                    num2 = (int)((double)Main.screenHeight - (double)vector2.Y - 4.0);
                this.spriteBatch.DrawString(Main.fontMouseText, cursorText, new Vector2((float)num1, (float)(num2 - 2)), Color.Black, 0.0f, new Vector2(), 1f, SpriteEffects.None, 0.0f);
                this.spriteBatch.DrawString(Main.fontMouseText, cursorText, new Vector2((float)num1, (float)(num2 + 2)), Color.Black, 0.0f, new Vector2(), 1f, SpriteEffects.None, 0.0f);
                this.spriteBatch.DrawString(Main.fontMouseText, cursorText, new Vector2((float)(num1 - 2), (float)num2), Color.Black, 0.0f, new Vector2(), 1f, SpriteEffects.None, 0.0f);
                this.spriteBatch.DrawString(Main.fontMouseText, cursorText, new Vector2((float)(num1 + 2), (float)num2), Color.Black, 0.0f, new Vector2(), 1f, SpriteEffects.None, 0.0f);
                float num3 = (float)Main.mouseTextColor / (float)byte.MaxValue;
                Color color2 = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
                if (rare == 1)
                    color2 = new Color((int)(byte)(150.0 * (double)num3), (int)(byte)(150.0 * (double)num3), (int)(byte)((double)byte.MaxValue * (double)num3), (int)Main.mouseTextColor);
                if (rare == 2)
                    color2 = new Color((int)(byte)(150.0 * (double)num3), (int)(byte)((double)byte.MaxValue * (double)num3), (int)(byte)(150.0 * (double)num3), (int)Main.mouseTextColor);
                if (rare == 3)
                    color2 = new Color((int)(byte)((double)byte.MaxValue * (double)num3), (int)(byte)(200.0 * (double)num3), (int)(byte)(150.0 * (double)num3), (int)Main.mouseTextColor);
                if (rare == 4)
                    color2 = new Color((int)(byte)((double)byte.MaxValue * (double)num3), (int)(byte)(150.0 * (double)num3), (int)(byte)(150.0 * (double)num3), (int)Main.mouseTextColor);
                this.spriteBatch.DrawString(Main.fontMouseText, cursorText, new Vector2((float)num1, (float)num2), color2, 0.0f, new Vector2(), 1f, SpriteEffects.None, 0.0f);
            }
        }

        protected void DrawFPS()
        {
            if (Main.showFrameRate)
            {
                string text = string.Concat((object)Main.frameRate);
                this.spriteBatch.DrawString(Main.fontMouseText, text, new Vector2(4f, (float)(Main.screenHeight - 24)), new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), 0.0f, new Vector2(), 1f, SpriteEffects.None, 0.0f);
            }
        }

        protected void DrawTiles(bool solidOnly = true)
        {
            int num1 = (int)((double)Main.screenPosition.X / 16.0 - 1.0);
            int num2 = (int)(((double)Main.screenPosition.X + (double)Main.screenWidth) / 16.0) + 2;
            int num3 = (int)((double)Main.screenPosition.Y / 16.0 - 1.0);
            int num4 = (int)(((double)Main.screenPosition.Y + (double)Main.screenHeight) / 16.0) + 2;
            if (num1 < 0)
                num1 = 0;
            if (num2 > Main.maxTilesX)
                num2 = Main.maxTilesX;
            if (num3 < 0)
                num3 = 0;
            if (num4 > Main.maxTilesY)
                num4 = Main.maxTilesY;
            for (int y = num3; y < num4 + 4; ++y)
            {
                int num5 = y - num3 + 21;
                for (int x = num1 - 2; x < num2 + 2; ++x)
                {
                    int num6 = x - num1 + 21;
                    if (Main.tile[x, y].active && Main.tileSolid[(int)Main.tile[x, y].type] == solidOnly)
                    {
                        int num7 = 0;
                        if ((int)Main.tile[x, y].type == 78)
                            num7 = 2;
                        if ((int)Main.tile[x, y].type == 33 || (int)Main.tile[x, y].type == 49)
                            num7 = -4;
                        int height1 = (int)Main.tile[x, y].type != 3 && (int)Main.tile[x, y].type != 4 && ((int)Main.tile[x, y].type != 5 && (int)Main.tile[x, y].type != 24) && ((int)Main.tile[x, y].type != 33 && (int)Main.tile[x, y].type != 49 && (int)Main.tile[x, y].type != 61) && (int)Main.tile[x, y].type != 71 ? ((int)Main.tile[x, y].type != 15 && (int)Main.tile[x, y].type != 14 && ((int)Main.tile[x, y].type != 16 && (int)Main.tile[x, y].type != 17) && ((int)Main.tile[x, y].type != 18 && (int)Main.tile[x, y].type != 20 && ((int)Main.tile[x, y].type != 21 && (int)Main.tile[x, y].type != 26)) && ((int)Main.tile[x, y].type != 27 && (int)Main.tile[x, y].type != 32 && ((int)Main.tile[x, y].type != 69 && (int)Main.tile[x, y].type != 72)) && (int)Main.tile[x, y].type != 77 ? 16 : 18) : 20;
                        int width1 = (int)Main.tile[x, y].type != 4 && (int)Main.tile[x, y].type != 5 ? 16 : 20;
                        if ((int)Main.tile[x, y].type == 73 || (int)Main.tile[x, y].type == 74)
                        {
                            num7 -= 12;
                            height1 = 32;
                        }
                        if (!Main.gamePaused && this.IsActive)
                        {
                            if ((int)Main.tile[x, y].type == 4 && Main.rand.Next(40) == 0)
                            {
                                if ((int)Main.tile[x, y].frameX == 22)
                                    Dust.NewDust(new Vector2((float)(x * 16 + 6), (float)(y * 16)), 4, 4, 6, 0.0f, 0.0f, 100, new Color(), 1f);
                                if ((int)Main.tile[x, y].frameX == 44)
                                    Dust.NewDust(new Vector2((float)(x * 16 + 2), (float)(y * 16)), 4, 4, 6, 0.0f, 0.0f, 100, new Color(), 1f);
                                else
                                    Dust.NewDust(new Vector2((float)(x * 16 + 4), (float)(y * 16)), 4, 4, 6, 0.0f, 0.0f, 100, new Color(), 1f);
                            }
                            if ((int)Main.tile[x, y].type == 33 && Main.rand.Next(40) == 0)
                                Dust.NewDust(new Vector2((float)(x * 16 + 4), (float)(y * 16 - 4)), 4, 4, 6, 0.0f, 0.0f, 100, new Color(), 1f);
                            if ((int)Main.tile[x, y].type == 49 && Main.rand.Next(20) == 0)
                                Dust.NewDust(new Vector2((float)(x * 16 + 4), (float)(y * 16 - 4)), 4, 4, 29, 0.0f, 0.0f, 100, new Color(), 1f);
                            if (((int)Main.tile[x, y].type == 34 || (int)Main.tile[x, y].type == 35 || (int)Main.tile[x, y].type == 36) && Main.rand.Next(40) == 0 && (int)Main.tile[x, y].frameY == 18 && ((int)Main.tile[x, y].frameX == 0 || (int)Main.tile[x, y].frameX == 36))
                                Dust.NewDust(new Vector2((float)(x * 16), (float)(y * 16 + 2)), 14, 6, 6, 0.0f, 0.0f, 100, new Color(), 1f);
                            if ((int)Main.tile[x, y].type == 22 && Main.rand.Next(400) == 0)
                                Dust.NewDust(new Vector2((float)(x * 16), (float)(y * 16)), 16, 16, 14, 0.0f, 0.0f, 0, new Color(), 1f);
                            else if (((int)Main.tile[x, y].type == 23 || (int)Main.tile[x, y].type == 24 || (int)Main.tile[x, y].type == 32) && Main.rand.Next(500) == 0)
                                Dust.NewDust(new Vector2((float)(x * 16), (float)(y * 16)), 16, 16, 14, 0.0f, 0.0f, 0, new Color(), 1f);
                            else if ((int)Main.tile[x, y].type == 25 && Main.rand.Next(700) == 0)
                                Dust.NewDust(new Vector2((float)(x * 16), (float)(y * 16)), 16, 16, 14, 0.0f, 0.0f, 0, new Color(), 1f);
                            else if ((int)Main.tile[x, y].type == 31 && Main.rand.Next(20) == 0)
                                Dust.NewDust(new Vector2((float)(x * 16), (float)(y * 16)), 16, 16, 14, 0.0f, 0.0f, 100, new Color(), 1f);
                            else if ((int)Main.tile[x, y].type == 26 && Main.rand.Next(20) == 0)
                                Dust.NewDust(new Vector2((float)(x * 16), (float)(y * 16)), 16, 16, 14, 0.0f, 0.0f, 100, new Color(), 1f);
                            else if (((int)Main.tile[x, y].type == 71 || (int)Main.tile[x, y].type == 72) && Main.rand.Next(500) == 0)
                                Dust.NewDust(new Vector2((float)(x * 16), (float)(y * 16)), 16, 16, 41, 0.0f, 0.0f, 250, new Color(), 0.8f);
                            else if (((int)Main.tile[x, y].type == 17 || (int)Main.tile[x, y].type == 77) && Main.rand.Next(40) == 0)
                            {
                                if ((int)Main.tile[x, y].frameX == 18 & (int)Main.tile[x, y].frameY == 18)
                                    Dust.NewDust(new Vector2((float)(x * 16 + 2), (float)(y * 16)), 8, 6, 6, 0.0f, 0.0f, 100, new Color(), 1f);
                            }
                            else if (((int)Main.tile[x, y].type == 37 || (int)Main.tile[x, y].type == 58 || (int)Main.tile[x, y].type == 76) && Main.rand.Next(200) == 0)
                            {
                                int index = Dust.NewDust(new Vector2((float)(x * 16), (float)(y * 16)), 16, 16, 6, 0.0f, 0.0f, 0, new Color(), (float)Main.rand.Next(3));
                                if ((double)Main.dust[index].scale > 1.0)
                                    Main.dust[index].noGravity = true;
                            }
                        }
                        if ((int)Main.tile[x, y].type == 5 && (int)Main.tile[x, y].frameY >= 198 && (int)Main.tile[x, y].frameX >= 22)
                        {
                            int num8 = 0;
                            if ((int)Main.tile[x, y].frameX == 22)
                            {
                                if ((int)Main.tile[x, y].frameY == 220)
                                    num8 = 1;
                                else if ((int)Main.tile[x, y].frameY == 242)
                                    num8 = 2;
                                int index1 = 0;
                                int width2 = 80;
                                int height2 = 80;
                                int num9 = 32;
                                for (int index2 = y; index2 < y + 100; ++index2)
                                {
                                    if ((int)Main.tile[x, index2].type == 2)
                                    {
                                        index1 = 0;
                                        break;
                                    }
                                    else if ((int)Main.tile[x, index2].type == 23)
                                    {
                                        index1 = 1;
                                        break;
                                    }
                                    else if ((int)Main.tile[x, index2].type == 60)
                                    {
                                        index1 = 2;
                                        width2 = 114;
                                        height2 = 96;
                                        num9 = 48;
                                        break;
                                    }
                                }
                                this.spriteBatch.Draw(Main.treeTopTexture[index1], new Vector2((float)(x * 16 - (int)Main.screenPosition.X - num9), (float)(y * 16 - (int)Main.screenPosition.Y - height2 + 16)), new Rectangle?(new Rectangle(num8 * (width2 + 2), 0, width2, height2)), Lighting.GetColor(x, y), 0.0f, new Vector2(), 1f, SpriteEffects.None, 0.0f);
                            }
                            else if ((int)Main.tile[x, y].frameX == 44)
                            {
                                if ((int)Main.tile[x, y].frameY == 220)
                                    num8 = 1;
                                else if ((int)Main.tile[x, y].frameY == 242)
                                    num8 = 2;
                                int index1 = 0;
                                for (int index2 = y; index2 < y + 100; ++index2)
                                {
                                    if ((int)Main.tile[x + 1, index2].type == 2)
                                    {
                                        index1 = 0;
                                        break;
                                    }
                                    else if ((int)Main.tile[x + 1, index2].type == 23)
                                    {
                                        index1 = 1;
                                        break;
                                    }
                                    else if ((int)Main.tile[x + 1, index2].type == 60)
                                    {
                                        index1 = 2;
                                        break;
                                    }
                                }
                                this.spriteBatch.Draw(Main.treeBranchTexture[index1], new Vector2((float)(x * 16 - (int)Main.screenPosition.X - 24), (float)(y * 16 - (int)Main.screenPosition.Y - 12)), new Rectangle?(new Rectangle(0, num8 * 42, 40, 40)), Lighting.GetColor(x, y), 0.0f, new Vector2(), 1f, SpriteEffects.None, 0.0f);
                            }
                            else if ((int)Main.tile[x, y].frameX == 66)
                            {
                                if ((int)Main.tile[x, y].frameY == 220)
                                    num8 = 1;
                                else if ((int)Main.tile[x, y].frameY == 242)
                                    num8 = 2;
                                int index1 = 0;
                                for (int index2 = y; index2 < y + 100; ++index2)
                                {
                                    if ((int)Main.tile[x - 1, index2].type == 2)
                                    {
                                        index1 = 0;
                                        break;
                                    }
                                    else if ((int)Main.tile[x - 1, index2].type == 23)
                                    {
                                        index1 = 1;
                                        break;
                                    }
                                    else if ((int)Main.tile[x - 1, index2].type == 60)
                                    {
                                        index1 = 2;
                                        break;
                                    }
                                }
                                this.spriteBatch.Draw(Main.treeBranchTexture[index1], new Vector2((float)(x * 16 - (int)Main.screenPosition.X), (float)(y * 16 - (int)Main.screenPosition.Y - 12)), new Rectangle?(new Rectangle(42, num8 * 42, 40, 40)), Lighting.GetColor(x, y), 0.0f, new Vector2(), 1f, SpriteEffects.None, 0.0f);
                            }
                        }
                        if ((int)Main.tile[x, y].type == 72 && (int)Main.tile[x, y].frameX >= 36)
                        {
                            int num8 = 0;
                            if ((int)Main.tile[x, y].frameY == 18)
                                num8 = 1;
                            else if ((int)Main.tile[x, y].frameY == 36)
                                num8 = 2;
                            this.spriteBatch.Draw(Main.shroomCapTexture, new Vector2((float)(x * 16 - (int)Main.screenPosition.X - 22), (float)(y * 16 - (int)Main.screenPosition.Y - 26)), new Rectangle?(new Rectangle(num8 * 62, 0, 60, 42)), Lighting.GetColor(x, y), 0.0f, new Vector2(), 1f, SpriteEffects.None, 0.0f);
                        }
                        if ((double)Lighting.Brightness(x, y) > 0.0)
                        {
                            Color color;
                            if (solidOnly && ((int)Main.tile[x - 1, y].liquid > 0 || (int)Main.tile[x + 1, y].liquid > 0 || (int)Main.tile[x, y - 1].liquid > 0 || (int)Main.tile[x, y + 1].liquid > 0))
                            {
                                color = Lighting.GetColor(x, y);
                                int num8 = 0;
                                bool flag1 = false;
                                bool flag2 = false;
                                bool flag3 = false;
                                bool flag4 = false;
                                int index = 0;
                                bool flag5 = false;
                                if ((int)Main.tile[x - 1, y].liquid > num8)
                                {
                                    num8 = (int)Main.tile[x - 1, y].liquid;
                                    flag1 = true;
                                }
                                else if ((int)Main.tile[x - 1, y].liquid > 0)
                                    flag1 = true;
                                if ((int)Main.tile[x + 1, y].liquid > num8)
                                {
                                    num8 = (int)Main.tile[x + 1, y].liquid;
                                    flag2 = true;
                                }
                                else if ((int)Main.tile[x + 1, y].liquid > 0)
                                {
                                    num8 = (int)Main.tile[x + 1, y].liquid;
                                    flag2 = true;
                                }
                                if ((int)Main.tile[x, y - 1].liquid > 0)
                                    flag3 = true;
                                if ((int)Main.tile[x, y + 1].liquid > 240)
                                    flag4 = true;
                                if ((int)Main.tile[x - 1, y].liquid > 0)
                                {
                                    if (Main.tile[x - 1, y].lava)
                                        index = 1;
                                    else
                                        flag5 = true;
                                }
                                if ((int)Main.tile[x + 1, y].liquid > 0)
                                {
                                    if (Main.tile[x + 1, y].lava)
                                        index = 1;
                                    else
                                        flag5 = true;
                                }
                                if ((int)Main.tile[x, y - 1].liquid > 0)
                                {
                                    if (Main.tile[x, y - 1].lava)
                                        index = 1;
                                    else
                                        flag5 = true;
                                }
                                if ((int)Main.tile[x, y + 1].liquid > 0)
                                {
                                    if (Main.tile[x, y + 1].lava)
                                        index = 1;
                                    else
                                        flag5 = true;
                                }
                                if (!flag5 || index != 1)
                                {
                                    Vector2 vector2 = new Vector2((float)(x * 16), (float)(y * 16));
                                    Rectangle rectangle = new Rectangle(0, 4, 16, 16);
                                    if (flag4 && (flag1 || flag2))
                                    {
                                        flag1 = true;
                                        flag2 = true;
                                    }
                                    if ((!flag3 || !flag1 && !flag2) && (!flag4 || !flag3))
                                    {
                                        if (flag3)
                                            rectangle = new Rectangle(0, 4, 16, 4);
                                        else if (flag4 && !flag1 && !flag2)
                                        {
                                            vector2 = new Vector2((float)(x * 16), (float)(y * 16 + 12));
                                            rectangle = new Rectangle(0, 4, 16, 4);
                                        }
                                        else
                                        {
                                            float num9 = (float)(256 - num8) / 32f;
                                            if (flag1 && flag2)
                                            {
                                                vector2 = new Vector2((float)(x * 16), (float)(y * 16 + (int)num9 * 2));
                                                rectangle = new Rectangle(0, 4, 16, 16 - (int)num9 * 2);
                                            }
                                            else if (flag1)
                                            {
                                                vector2 = new Vector2((float)(x * 16), (float)(y * 16 + (int)num9 * 2));
                                                rectangle = new Rectangle(0, 4, 4, 16 - (int)num9 * 2);
                                            }
                                            else
                                            {
                                                vector2 = new Vector2((float)(x * 16 + 12), (float)(y * 16 + (int)num9 * 2));
                                                rectangle = new Rectangle(0, 4, 4, 16 - (int)num9 * 2);
                                            }
                                        }
                                    }
                                    float num10 = 0.5f;
                                    if (index == 1)
                                        num10 *= 1.6f;
                                    if ((double)y < Main.worldSurface || (double)num10 > 1.0)
                                        num10 = 1f;
                                    color = new Color((int)(byte)((float)color.R * num10), (int)(byte)((float)color.G * num10), (int)(byte)((float)color.B * num10), (int)(byte)((float)color.A * num10));
                                    this.spriteBatch.Draw(Main.liquidTexture[index], vector2 - Main.screenPosition, new Rectangle?(rectangle), color, 0.0f, new Vector2(), 1f, SpriteEffects.None, 0.0f);
                                }
                            }
                            if ((int)Main.tile[x, y].type == 51)
                            {
                                color = Lighting.GetColor(x, y);
                                float num8 = 0.5f;
                                color = new Color((int)(byte)((float)color.R * num8), (int)(byte)((float)color.G * num8), (int)(byte)((float)color.B * num8), (int)(byte)((float)color.A * num8));
                                this.spriteBatch.Draw(Main.tileTexture[(int)Main.tile[x, y].type], new Vector2((float)(x * 16 - (int)Main.screenPosition.X) - (float)(((double)width1 - 16.0) / 2.0), (float)(y * 16 - (int)Main.screenPosition.Y + num7)), new Rectangle?(new Rectangle((int)Main.tile[x, y].frameX, (int)Main.tile[x, y].frameY, width1, height1)), color, 0.0f, new Vector2(), 1f, SpriteEffects.None, 0.0f);
                            }
                            else
                                this.spriteBatch.Draw(Main.tileTexture[(int)Main.tile[x, y].type], new Vector2((float)(x * 16 - (int)Main.screenPosition.X) - (float)(((double)width1 - 16.0) / 2.0), (float)(y * 16 - (int)Main.screenPosition.Y + num7)), new Rectangle?(new Rectangle((int)Main.tile[x, y].frameX, (int)Main.tile[x, y].frameY, width1, height1)), Lighting.GetColor(x, y), 0.0f, new Vector2(), 1f, SpriteEffects.None, 0.0f);
                        }
                    }
                }
            }
        }

        protected void DrawWater(bool bg = false)
        {
            int num1 = (int)((double)Main.screenPosition.X / 16.0 - 1.0);
            int num2 = (int)(((double)Main.screenPosition.X + (double)Main.screenWidth) / 16.0) + 2;
            int num3 = (int)((double)Main.screenPosition.Y / 16.0 - 1.0);
            int num4 = (int)(((double)Main.screenPosition.Y + (double)Main.screenHeight) / 16.0) + 2;
            if (num1 < 0)
                num1 = 0;
            if (num2 > Main.maxTilesX)
                num2 = Main.maxTilesX;
            if (num3 < 0)
                num3 = 0;
            if (num4 > Main.maxTilesY)
                num4 = Main.maxTilesY;
            for (int y = num3; y < num4 + 4; ++y)
            {
                for (int x = num1 - 2; x < num2 + 2; ++x)
                {
                    if ((int)Main.tile[x, y].liquid > 0 && (double)Lighting.Brightness(x, y) > 0.0)
                    {
                        Color color = Lighting.GetColor(x, y);
                        float num5 = (float)(256 - (int)Main.tile[x, y].liquid) / 32f;
                        int index1 = 0;
                        if (Main.tile[x, y].lava)
                            index1 = 1;
                        float num6 = 0.5f;
                        if (bg)
                            num6 = 1f;
                        Vector2 vector2 = new Vector2((float)(x * 16), (float)(y * 16 + (int)num5 * 2));
                        Rectangle rectangle = new Rectangle(0, 0, 16, 16 - (int)num5 * 2);
                        if ((int)Main.tile[x, y + 1].liquid < 245 && (!Main.tile[x, y + 1].active || !Main.tileSolid[(int)Main.tile[x, y + 1].type] || Main.tileSolidTop[(int)Main.tile[x, y + 1].type]))
                        {
                            float num7 = (float)(256 - (int)Main.tile[x, y + 1].liquid) / 32f;
                            num6 = (float)(0.5 * (8.0 - (double)num5) / 4.0);
                            if ((double)num6 > 0.55)
                                num6 = 0.55f;
                            if ((double)num6 < 0.35)
                                num6 = 0.35f;
                            float num8 = num5 / 2f;
                            if ((int)Main.tile[x, y + 1].liquid < 200)
                            {
                                if (!bg)
                                {
                                    if ((int)Main.tile[x, y - 1].liquid > 0 && (int)Main.tile[x, y - 1].liquid > 0)
                                    {
                                        rectangle = new Rectangle(0, 4, 16, 16);
                                        num6 = 0.5f;
                                    }
                                    else if ((int)Main.tile[x, y - 1].liquid > 0)
                                    {
                                        vector2 = new Vector2((float)(x * 16), (float)(y * 16 + 4));
                                        rectangle = new Rectangle(0, 4, 16, 12);
                                        num6 = 0.5f;
                                    }
                                    else if ((int)Main.tile[x, y + 1].liquid > 0)
                                    {
                                        vector2 = new Vector2((float)(x * 16), (float)(y * 16 + (int)num5 * 2 + (int)num7 * 2));
                                        rectangle = new Rectangle(0, 4, 16, 16 - (int)num5 * 2);
                                    }
                                    else
                                    {
                                        vector2 = new Vector2((float)(x * 16 + (int)num8), (float)(y * 16 + (int)num8 * 2 + (int)num7 * 2));
                                        rectangle = new Rectangle(0, 4, 16 - (int)num8 * 2, 16 - (int)num8 * 2);
                                    }
                                }
                                else
                                    continue;
                            }
                            else
                            {
                                num6 = 0.5f;
                                rectangle = new Rectangle(0, 4, 16, 16 - (int)num5 * 2 + (int)num7 * 2);
                            }
                        }
                        else if ((int)Main.tile[x, y - 1].liquid > 32)
                            rectangle = new Rectangle(0, 4, rectangle.Width, rectangle.Height);
                        else if ((double)num5 < 1.0 && Main.tile[x, y - 1].active && Main.tileSolid[(int)Main.tile[x, y - 1].type] && !Main.tileSolidTop[(int)Main.tile[x, y - 1].type])
                        {
                            vector2 = new Vector2((float)(x * 16), (float)(y * 16));
                            rectangle = new Rectangle(0, 4, 16, 16);
                        }
                        else
                        {
                            bool flag = true;
                            for (int index2 = y + 1; index2 < y + 6 && (!Main.tile[x, index2].active || !Main.tileSolid[(int)Main.tile[x, index2].type] || Main.tileSolidTop[(int)Main.tile[x, index2].type]); ++index2)
                            {
                                if ((int)Main.tile[x, index2].liquid < 200)
                                {
                                    flag = false;
                                    break;
                                }
                            }
                            if (!flag)
                            {
                                num6 = 0.5f;
                                rectangle = new Rectangle(0, 4, 16, 16);
                            }
                            else if ((int)Main.tile[x, y - 1].liquid > 0)
                                rectangle = new Rectangle(0, 2, rectangle.Width, rectangle.Height);
                        }
                        if (Main.tile[x, y].lava)
                        {
                            num6 *= 1.6f;
                            if ((double)num6 > 1.0)
                                num6 = 1f;
                            if (this.IsActive)
                            {
                                if ((int)Main.tile[x, y].liquid > 200 && Main.rand.Next(700) == 0)
                                    Dust.NewDust(new Vector2((float)(x * 16), (float)(y * 16)), 16, 16, 35, 0.0f, 0.0f, 0, new Color(), 1f);
                                if (rectangle.Y == 0 && this.IsActive && Main.rand.Next(300) == 0)
                                {
                                    int index2 = Dust.NewDust(new Vector2((float)(x * 16), (float)((double)(y * 16) + (double)num5 * 2.0 - 8.0)), 16, 8, 35, 0.0f, 0.0f, 50, new Color(), 1.5f);
                                    Main.dust[index2].velocity *= 0.8f;
                                    Main.dust[index2].velocity.X *= 2f;
                                    Main.dust[index2].velocity.Y -= (float)Main.rand.Next(1, 7) * 0.1f;
                                    if (Main.rand.Next(10) == 0)
                                        Main.dust[index2].velocity.Y *= (float)Main.rand.Next(2, 5);
                                    Main.dust[index2].noGravity = true;
                                }
                            }
                        }
                        color = new Color((int)(byte)((float)color.R * num6), (int)(byte)((float)color.G * num6), (int)(byte)((float)color.B * num6), (int)(byte)((float)color.A * num6));
                        this.spriteBatch.Draw(Main.liquidTexture[index1], vector2 - Main.screenPosition, new Rectangle?(rectangle), color, 0.0f, new Vector2(), 1f, SpriteEffects.None, 0.0f);
                    }
                }
            }
        }

        protected void DrawGore()
        {
            for (int index = 0; index < 200; ++index)
            {
                if (Main.gore[index].active && Main.gore[index].type > 0)
                {
                    Color alpha = Main.gore[index].GetAlpha(Lighting.GetColor((int)((double)Main.gore[index].position.X + (double)Main.goreTexture[Main.gore[index].type].Width * 0.5) / 16, (int)(((double)Main.gore[index].position.Y + (double)Main.goreTexture[Main.gore[index].type].Height * 0.5) / 16.0)));
                    this.spriteBatch.Draw(Main.goreTexture[Main.gore[index].type], new Vector2(Main.gore[index].position.X - Main.screenPosition.X + (float)(Main.goreTexture[Main.gore[index].type].Width / 2), Main.gore[index].position.Y - Main.screenPosition.Y + (float)(Main.goreTexture[Main.gore[index].type].Height / 2)), new Rectangle?(new Rectangle(0, 0, Main.goreTexture[Main.gore[index].type].Width, Main.goreTexture[Main.gore[index].type].Height)), alpha, Main.gore[index].rotation, new Vector2((float)(Main.goreTexture[Main.gore[index].type].Width / 2), (float)(Main.goreTexture[Main.gore[index].type].Height / 2)), Main.gore[index].scale, SpriteEffects.None, 0.0f);
                }
            }
        }

        protected void DrawNPCs(bool behindTiles = false)
        {
            Rectangle rectangle = new Rectangle((int)Main.screenPosition.X - 300, (int)Main.screenPosition.Y - 300, Main.screenWidth + 600, Main.screenHeight + 600);
            for (int index1 = 999; index1 >= 0; --index1)
            {
                if (Main.npc[index1].active && Main.npc[index1].type > 0 && Main.npc[index1].behindTiles == behindTiles && rectangle.Intersects(new Rectangle((int)Main.npc[index1].position.X, (int)Main.npc[index1].position.Y, Main.npc[index1].width, Main.npc[index1].height)))
                {
                    Vector2 vector2_1;
                    if (Main.npc[index1].aiStyle == 13)
                    {
                        vector2_1 = new Vector2(Main.npc[index1].position.X + (float)(Main.npc[index1].width / 2), Main.npc[index1].position.Y + (float)(Main.npc[index1].height / 2));
                        float num1 = (float)((double)Main.npc[index1].ai[0] * 16.0 + 8.0) - vector2_1.X;
                        float num2 = (float)((double)Main.npc[index1].ai[1] * 16.0 + 8.0) - vector2_1.Y;
                        float rotation = (float)Math.Atan2((double)num2, (double)num1) - 1.57f;
                        bool flag = true;
                        while (flag)
                        {
                            int height = 28;
                            float num3 = (float)Math.Sqrt((double)num1 * (double)num1 + (double)num2 * (double)num2);
                            if ((double)num3 < 40.0)
                            {
                                height = (int)num3 - 40 + 28;
                                flag = false;
                            }
                            float num4 = 28f / num3;
                            float num5 = num1 * num4;
                            float num6 = num2 * num4;
                            vector2_1.X += num5;
                            vector2_1.Y += num6;
                            num1 = (float)((double)Main.npc[index1].ai[0] * 16.0 + 8.0) - vector2_1.X;
                            num2 = (float)((double)Main.npc[index1].ai[1] * 16.0 + 8.0) - vector2_1.Y;
                            Color color = Lighting.GetColor((int)vector2_1.X / 16, (int)((double)vector2_1.Y / 16.0));
                            if (Main.npc[index1].type == 56)
                                this.spriteBatch.Draw(Main.chain5Texture, new Vector2(vector2_1.X - Main.screenPosition.X, vector2_1.Y - Main.screenPosition.Y), new Rectangle?(new Rectangle(0, 0, Main.chain4Texture.Width, height)), color, rotation, new Vector2((float)Main.chain4Texture.Width * 0.5f, (float)Main.chain4Texture.Height * 0.5f), 1f, SpriteEffects.None, 0.0f);
                            else
                                this.spriteBatch.Draw(Main.chain4Texture, new Vector2(vector2_1.X - Main.screenPosition.X, vector2_1.Y - Main.screenPosition.Y), new Rectangle?(new Rectangle(0, 0, Main.chain4Texture.Width, height)), color, rotation, new Vector2((float)Main.chain4Texture.Width * 0.5f, (float)Main.chain4Texture.Height * 0.5f), 1f, SpriteEffects.None, 0.0f);
                        }
                    }
                    if (Main.npc[index1].type == 36)
                    {
                        vector2_1 = new Vector2((float)((double)Main.npc[index1].position.X + (double)Main.npc[index1].width * 0.5 - 5.0 * (double)Main.npc[index1].ai[0]), Main.npc[index1].position.Y + 20f);
                        for (int index2 = 0; index2 < 2; ++index2)
                        {
                            float num1 = Main.npc[(int)Main.npc[index1].ai[1]].position.X + (float)(Main.npc[(int)Main.npc[index1].ai[1]].width / 2) - vector2_1.X;
                            float num2 = Main.npc[(int)Main.npc[index1].ai[1]].position.Y + (float)(Main.npc[(int)Main.npc[index1].ai[1]].height / 2) - vector2_1.Y;
                            float num3;
                            float num4;
                            float num5;
                            if (index2 == 0)
                            {
                                num3 = num1 - 200f * Main.npc[index1].ai[0];
                                num4 = num2 + 130f;
                                num5 = 92f / (float)Math.Sqrt((double)num3 * (double)num3 + (double)num4 * (double)num4);
                                vector2_1.X += num3 * num5;
                                vector2_1.Y += num4 * num5;
                            }
                            else
                            {
                                num3 = num1 - 50f * Main.npc[index1].ai[0];
                                num4 = num2 + 80f;
                                num5 = 60f / (float)Math.Sqrt((double)num3 * (double)num3 + (double)num4 * (double)num4);
                                vector2_1.X += num3 * num5;
                                vector2_1.Y += num4 * num5;
                            }
                            float rotation = (float)Math.Atan2((double)num4, (double)num3) - 1.57f;
                            Color color = Lighting.GetColor((int)vector2_1.X / 16, (int)((double)vector2_1.Y / 16.0));
                            this.spriteBatch.Draw(Main.boneArmTexture, new Vector2(vector2_1.X - Main.screenPosition.X, vector2_1.Y - Main.screenPosition.Y), new Rectangle?(new Rectangle(0, 0, Main.boneArmTexture.Width, Main.boneArmTexture.Height)), color, rotation, new Vector2((float)Main.boneArmTexture.Width * 0.5f, (float)Main.boneArmTexture.Height * 0.5f), 1f, SpriteEffects.None, 0.0f);
                            if (index2 == 0)
                            {
                                vector2_1.X += (float)((double)num3 * (double)num5 / 2.0);
                                vector2_1.Y += (float)((double)num4 * (double)num5 / 2.0);
                            }
                            else if (this.IsActive)
                            {
                                vector2_1.X += (float)((double)num3 * (double)num5 - 16.0);
                                vector2_1.Y += (float)((double)num4 * (double)num5 - 6.0);
                                int index3 = Dust.NewDust(new Vector2(vector2_1.X, vector2_1.Y), 30, 10, 5, num3 * 0.02f, num4 * 0.02f, 0, new Color(), 2f);
                                Main.dust[index3].noGravity = true;
                            }
                        }
                    }
                    Color color1 = Lighting.GetColor((int)((double)Main.npc[index1].position.X + (double)Main.npc[index1].width * 0.5) / 16, (int)(((double)Main.npc[index1].position.Y + (double)Main.npc[index1].height * 0.5) / 16.0));
                    if (Main.npc[index1].type == 50)
                    {
                        Vector2 vector2_2 = new Vector2();
                        float num = 0.0f;
                        vector2_2.Y -= Main.npc[index1].velocity.Y;
                        vector2_2.X -= Main.npc[index1].velocity.X * 2f;
                        float rotation = num + Main.npc[index1].velocity.X * 0.05f;
                        if (Main.npc[index1].frame.Y == 120)
                            vector2_2.Y += 2f;
                        if (Main.npc[index1].frame.Y == 360)
                            vector2_2.Y -= 2f;
                        if (Main.npc[index1].frame.Y == 480)
                            vector2_2.Y -= 6f;
                        this.spriteBatch.Draw(Main.ninjaTexture, new Vector2(Main.npc[index1].position.X - Main.screenPosition.X + (float)(Main.npc[index1].width / 2) + vector2_2.X, Main.npc[index1].position.Y - Main.screenPosition.Y + (float)(Main.npc[index1].height / 2) + vector2_2.Y), new Rectangle?(new Rectangle(0, 0, Main.ninjaTexture.Width, Main.ninjaTexture.Height)), color1, rotation, new Vector2((float)(Main.ninjaTexture.Width / 2), (float)(Main.ninjaTexture.Height / 2)), 1f, SpriteEffects.None, 0.0f);
                    }
                    float num7 = 0.0f;
                    Vector2 origin = new Vector2((float)(Main.npcTexture[Main.npc[index1].type].Width / 2), (float)(Main.npcTexture[Main.npc[index1].type].Height / Main.npcFrameCount[Main.npc[index1].type] / 2));
                    if (Main.npc[index1].type == 4)
                        origin = new Vector2(55f, 107f);
                    if (Main.npc[index1].type == 6)
                        num7 = 26f;
                    if (Main.npc[index1].type == 7 || Main.npc[index1].type == 8 || Main.npc[index1].type == 9)
                        num7 = 13f;
                    if (Main.npc[index1].type == 10 || Main.npc[index1].type == 11 || Main.npc[index1].type == 12)
                        num7 = 8f;
                    if (Main.npc[index1].type == 13 || Main.npc[index1].type == 14 || Main.npc[index1].type == 15)
                        num7 = 26f;
                    if (Main.npc[index1].type == 48)
                        num7 = 32f;
                    if (Main.npc[index1].type == 49 || Main.npc[index1].type == 51)
                        num7 = 4f;
                    float num8 = num7 * Main.npc[index1].scale;
                    if (Main.npc[index1].aiStyle == 10)
                        color1 = Color.White;
                    SpriteEffects effects = SpriteEffects.None;
                    if (Main.npc[index1].spriteDirection == 1)
                        effects = SpriteEffects.FlipHorizontally;
                    this.spriteBatch.Draw(Main.npcTexture[Main.npc[index1].type], new Vector2((float)((double)Main.npc[index1].position.X - (double)Main.screenPosition.X + (double)(Main.npc[index1].width / 2) - (double)Main.npcTexture[Main.npc[index1].type].Width * (double)Main.npc[index1].scale / 2.0 + (double)origin.X * (double)Main.npc[index1].scale), (float)((double)Main.npc[index1].position.Y - (double)Main.screenPosition.Y + (double)Main.npc[index1].height - (double)Main.npcTexture[Main.npc[index1].type].Height * (double)Main.npc[index1].scale / (double)Main.npcFrameCount[Main.npc[index1].type] + 4.0 + (double)origin.Y * (double)Main.npc[index1].scale) + num8), new Rectangle?(Main.npc[index1].frame), Main.npc[index1].GetAlpha(color1), Main.npc[index1].rotation, origin, Main.npc[index1].scale, effects, 0.0f);
                    if (Main.npc[index1].color != new Color())
                        this.spriteBatch.Draw(Main.npcTexture[Main.npc[index1].type], new Vector2((float)((double)Main.npc[index1].position.X - (double)Main.screenPosition.X + (double)(Main.npc[index1].width / 2) - (double)Main.npcTexture[Main.npc[index1].type].Width * (double)Main.npc[index1].scale / 2.0 + (double)origin.X * (double)Main.npc[index1].scale), (float)((double)Main.npc[index1].position.Y - (double)Main.screenPosition.Y + (double)Main.npc[index1].height - (double)Main.npcTexture[Main.npc[index1].type].Height * (double)Main.npc[index1].scale / (double)Main.npcFrameCount[Main.npc[index1].type] + 4.0 + (double)origin.Y * (double)Main.npc[index1].scale) + num8), new Rectangle?(Main.npc[index1].frame), Main.npc[index1].GetColor(color1), Main.npc[index1].rotation, origin, Main.npc[index1].scale, effects, 0.0f);
                }
            }
        }

        protected void DrawPlayer(Player drawPlayer)
        {
            Color immuneAlpha1 = drawPlayer.GetImmuneAlpha(Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.25) / 16.0), Color.White));
            Color immuneAlpha2 = drawPlayer.GetImmuneAlpha(Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.25) / 16.0), drawPlayer.eyeColor));
            Color immuneAlpha3 = drawPlayer.GetImmuneAlpha(Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.25) / 16.0), drawPlayer.hairColor));
            Color immuneAlpha4 = drawPlayer.GetImmuneAlpha(Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.25) / 16.0), drawPlayer.skinColor));
            Color immuneAlpha5 = drawPlayer.GetImmuneAlpha(Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.5) / 16.0), drawPlayer.skinColor));
            Color immuneAlpha6 = drawPlayer.GetImmuneAlpha(Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.5) / 16.0), drawPlayer.shirtColor));
            Color immuneAlpha7 = drawPlayer.GetImmuneAlpha(Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.5) / 16.0), drawPlayer.underShirtColor));
            Color immuneAlpha8 = drawPlayer.GetImmuneAlpha(Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.75) / 16.0), drawPlayer.pantsColor));
            Color immuneAlpha9 = drawPlayer.GetImmuneAlpha(Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.75) / 16.0), drawPlayer.shoeColor));
            Color immuneAlpha10 = drawPlayer.GetImmuneAlpha(Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.75) / 16, Color.White));
            Color immuneAlpha11 = drawPlayer.GetImmuneAlpha(Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.5) / 16, Color.White));
            Color immuneAlpha12 = drawPlayer.GetImmuneAlpha(Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.25) / 16, Color.White));
            SpriteEffects effects1;
            SpriteEffects effects2;
            if (drawPlayer.direction == 1)
            {
                effects1 = SpriteEffects.None;
                effects2 = SpriteEffects.None;
            }
            else
            {
                effects1 = SpriteEffects.FlipHorizontally;
                effects2 = SpriteEffects.FlipHorizontally;
            }
            Vector2 origin1 = new Vector2((float)drawPlayer.legFrame.Width * 0.5f, (float)drawPlayer.legFrame.Height * 0.75f);
            Vector2 origin2 = new Vector2((float)drawPlayer.legFrame.Width * 0.5f, (float)drawPlayer.legFrame.Height * 0.5f);
            Vector2 origin3 = new Vector2((float)drawPlayer.legFrame.Width * 0.5f, (float)drawPlayer.legFrame.Height * 0.25f);
            if (drawPlayer.legs > 0 && drawPlayer.legs < 15)
            {
                this.spriteBatch.Draw(Main.armorLegTexture[drawPlayer.legs], new Vector2((float)(int)((double)drawPlayer.position.X - (double)Main.screenPosition.X - (double)(drawPlayer.legFrame.Width / 2) + (double)(drawPlayer.width / 2)), (float)(int)((double)drawPlayer.position.Y - (double)Main.screenPosition.Y + (double)drawPlayer.height - (double)drawPlayer.legFrame.Height + 4.0)) + drawPlayer.legPosition + origin1, new Rectangle?(drawPlayer.legFrame), immuneAlpha12, drawPlayer.legRotation, origin1, 1f, effects1, 0.0f);
            }
            else
            {
                this.spriteBatch.Draw(Main.playerPantsTexture, new Vector2((float)(int)((double)drawPlayer.position.X - (double)Main.screenPosition.X - (double)(drawPlayer.legFrame.Width / 2) + (double)(drawPlayer.width / 2)), (float)(int)((double)drawPlayer.position.Y - (double)Main.screenPosition.Y + (double)drawPlayer.height - (double)drawPlayer.legFrame.Height + 4.0)) + drawPlayer.legPosition + origin1, new Rectangle?(drawPlayer.legFrame), immuneAlpha8, drawPlayer.legRotation, origin1, 1f, effects1, 0.0f);
                this.spriteBatch.Draw(Main.playerShoesTexture, new Vector2((float)(int)((double)drawPlayer.position.X - (double)Main.screenPosition.X - (double)(drawPlayer.legFrame.Width / 2) + (double)(drawPlayer.width / 2)), (float)(int)((double)drawPlayer.position.Y - (double)Main.screenPosition.Y + (double)drawPlayer.height - (double)drawPlayer.legFrame.Height + 4.0)) + drawPlayer.legPosition + origin1, new Rectangle?(drawPlayer.legFrame), immuneAlpha9, drawPlayer.legRotation, origin1, 1f, effects1, 0.0f);
            }
            if (drawPlayer.body > 0 && drawPlayer.body < 16)
            {
                this.spriteBatch.Draw(Main.armorBodyTexture[drawPlayer.body], new Vector2((float)(int)((double)drawPlayer.position.X - (double)Main.screenPosition.X - (double)(drawPlayer.bodyFrame.Width / 2) + (double)(drawPlayer.width / 2)), (float)(int)((double)drawPlayer.position.Y - (double)Main.screenPosition.Y + (double)drawPlayer.height - (double)drawPlayer.bodyFrame.Height + 4.0)) + drawPlayer.bodyPosition + new Vector2((float)(drawPlayer.bodyFrame.Width / 2), (float)(drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawPlayer.bodyFrame), immuneAlpha11, drawPlayer.bodyRotation, origin2, 1f, effects1, 0.0f);
                if (drawPlayer.body == 10 || drawPlayer.body == 11 || (drawPlayer.body == 12 || drawPlayer.body == 13) || drawPlayer.body == 14 || drawPlayer.body == 15)
                    this.spriteBatch.Draw(Main.playerHandsTexture, new Vector2((float)(int)((double)drawPlayer.position.X - (double)Main.screenPosition.X - (double)(drawPlayer.bodyFrame.Width / 2) + (double)(drawPlayer.width / 2)), (float)(int)((double)drawPlayer.position.Y - (double)Main.screenPosition.Y + (double)drawPlayer.height - (double)drawPlayer.bodyFrame.Height + 4.0)) + drawPlayer.bodyPosition + new Vector2((float)(drawPlayer.bodyFrame.Width / 2), (float)(drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawPlayer.bodyFrame), immuneAlpha5, drawPlayer.bodyRotation, origin2, 1f, effects1, 0.0f);
            }
            else
            {
                this.spriteBatch.Draw(Main.playerUnderShirtTexture, new Vector2((float)(int)((double)drawPlayer.position.X - (double)Main.screenPosition.X - (double)(drawPlayer.bodyFrame.Width / 2) + (double)(drawPlayer.width / 2)), (float)(int)((double)drawPlayer.position.Y - (double)Main.screenPosition.Y + (double)drawPlayer.height - (double)drawPlayer.bodyFrame.Height + 4.0)) + drawPlayer.bodyPosition + new Vector2((float)(drawPlayer.bodyFrame.Width / 2), (float)(drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawPlayer.bodyFrame), immuneAlpha7, drawPlayer.bodyRotation, origin2, 1f, effects1, 0.0f);
                this.spriteBatch.Draw(Main.playerShirtTexture, new Vector2((float)(int)((double)drawPlayer.position.X - (double)Main.screenPosition.X - (double)(drawPlayer.bodyFrame.Width / 2) + (double)(drawPlayer.width / 2)), (float)(int)((double)drawPlayer.position.Y - (double)Main.screenPosition.Y + (double)drawPlayer.height - (double)drawPlayer.bodyFrame.Height + 4.0)) + drawPlayer.bodyPosition + new Vector2((float)(drawPlayer.bodyFrame.Width / 2), (float)(drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawPlayer.bodyFrame), immuneAlpha6, drawPlayer.bodyRotation, origin2, 1f, effects1, 0.0f);
                this.spriteBatch.Draw(Main.playerHandsTexture, new Vector2((float)(int)((double)drawPlayer.position.X - (double)Main.screenPosition.X - (double)(drawPlayer.bodyFrame.Width / 2) + (double)(drawPlayer.width / 2)), (float)(int)((double)drawPlayer.position.Y - (double)Main.screenPosition.Y + (double)drawPlayer.height - (double)drawPlayer.bodyFrame.Height + 4.0)) + drawPlayer.bodyPosition + new Vector2((float)(drawPlayer.bodyFrame.Width / 2), (float)(drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawPlayer.bodyFrame), immuneAlpha5, drawPlayer.bodyRotation, origin2, 1f, effects1, 0.0f);
            }
            this.spriteBatch.Draw(Main.playerHeadTexture, new Vector2((float)(int)((double)drawPlayer.position.X - (double)Main.screenPosition.X - (double)(drawPlayer.bodyFrame.Width / 2) + (double)(drawPlayer.width / 2)), (float)(int)((double)drawPlayer.position.Y - (double)Main.screenPosition.Y + (double)drawPlayer.height - (double)drawPlayer.bodyFrame.Height + 4.0)) + drawPlayer.headPosition + origin3, new Rectangle?(drawPlayer.bodyFrame), immuneAlpha4, drawPlayer.headRotation, origin3, 1f, effects1, 0.0f);
            this.spriteBatch.Draw(Main.playerEyeWhitesTexture, new Vector2((float)(int)((double)drawPlayer.position.X - (double)Main.screenPosition.X - (double)(drawPlayer.bodyFrame.Width / 2) + (double)(drawPlayer.width / 2)), (float)(int)((double)drawPlayer.position.Y - (double)Main.screenPosition.Y + (double)drawPlayer.height - (double)drawPlayer.bodyFrame.Height + 4.0)) + drawPlayer.headPosition + origin3, new Rectangle?(drawPlayer.bodyFrame), immuneAlpha1, drawPlayer.headRotation, origin3, 1f, effects1, 0.0f);
            this.spriteBatch.Draw(Main.playerEyesTexture, new Vector2((float)(int)((double)drawPlayer.position.X - (double)Main.screenPosition.X - (double)(drawPlayer.bodyFrame.Width / 2) + (double)(drawPlayer.width / 2)), (float)(int)((double)drawPlayer.position.Y - (double)Main.screenPosition.Y + (double)drawPlayer.height - (double)drawPlayer.bodyFrame.Height + 4.0)) + drawPlayer.headPosition + origin3, new Rectangle?(drawPlayer.bodyFrame), immuneAlpha2, drawPlayer.headRotation, origin3, 1f, effects1, 0.0f);
            Rectangle rectangle1;
            if (drawPlayer.head == 10 || drawPlayer.head == 12)
            {
                this.spriteBatch.Draw(Main.armorHeadTexture[drawPlayer.head], new Vector2((float)(int)((double)drawPlayer.position.X - (double)Main.screenPosition.X - (double)(drawPlayer.bodyFrame.Width / 2) + (double)(drawPlayer.width / 2)), (float)(int)((double)drawPlayer.position.Y - (double)Main.screenPosition.Y + (double)drawPlayer.height - (double)drawPlayer.bodyFrame.Height + 4.0)) + drawPlayer.headPosition + origin3, new Rectangle?(drawPlayer.bodyFrame), immuneAlpha10, drawPlayer.headRotation, origin3, 1f, effects1, 0.0f);
                rectangle1 = drawPlayer.bodyFrame;
                rectangle1.Y -= 336;
                if (rectangle1.Y < 0)
                    rectangle1.Y = 0;
                this.spriteBatch.Draw(Main.playerHairTexture[drawPlayer.hair], new Vector2((float)(int)((double)drawPlayer.position.X - (double)Main.screenPosition.X - (double)(drawPlayer.bodyFrame.Width / 2) + (double)(drawPlayer.width / 2)), (float)(int)((double)drawPlayer.position.Y - (double)Main.screenPosition.Y + (double)drawPlayer.height - (double)drawPlayer.bodyFrame.Height + 4.0)) + drawPlayer.headPosition + origin3, new Rectangle?(rectangle1), immuneAlpha3, drawPlayer.headRotation, origin3, 1f, effects1, 0.0f);
            }
            if (drawPlayer.head == 23)
            {
                rectangle1 = drawPlayer.bodyFrame;
                rectangle1.Y -= 336;
                if (rectangle1.Y < 0)
                    rectangle1.Y = 0;
                this.spriteBatch.Draw(Main.playerHairTexture[drawPlayer.hair], new Vector2((float)(int)((double)drawPlayer.position.X - (double)Main.screenPosition.X - (double)(drawPlayer.bodyFrame.Width / 2) + (double)(drawPlayer.width / 2)), (float)(int)((double)drawPlayer.position.Y - (double)Main.screenPosition.Y + (double)drawPlayer.height - (double)drawPlayer.bodyFrame.Height + 4.0)) + drawPlayer.headPosition + origin3, new Rectangle?(rectangle1), immuneAlpha3, drawPlayer.headRotation, origin3, 1f, effects1, 0.0f);
                this.spriteBatch.Draw(Main.armorHeadTexture[drawPlayer.head], new Vector2((float)(int)((double)drawPlayer.position.X - (double)Main.screenPosition.X - (double)(drawPlayer.bodyFrame.Width / 2) + (double)(drawPlayer.width / 2)), (float)(int)((double)drawPlayer.position.Y - (double)Main.screenPosition.Y + (double)drawPlayer.height - (double)drawPlayer.bodyFrame.Height + 4.0)) + drawPlayer.headPosition + origin3, new Rectangle?(drawPlayer.bodyFrame), immuneAlpha10, drawPlayer.headRotation, origin3, 1f, effects1, 0.0f);
            }
            else if (drawPlayer.head == 14)
            {
                Rectangle rectangle2 = drawPlayer.bodyFrame;
                int num = 0;
                if (rectangle2.Y == rectangle2.Height * 6)
                    rectangle2.Height -= 2;
                else if (rectangle2.Y == rectangle2.Height * 7)
                    num = -2;
                else if (rectangle2.Y == rectangle2.Height * 8)
                    num = -2;
                else if (rectangle2.Y == rectangle2.Height * 9)
                    num = -2;
                else if (rectangle2.Y == rectangle2.Height * 10)
                    num = -2;
                else if (rectangle2.Y == rectangle2.Height * 13)
                    rectangle2.Height -= 2;
                else if (rectangle2.Y == rectangle2.Height * 14)
                    num = -2;
                else if (rectangle2.Y == rectangle2.Height * 15)
                    num = -2;
                else if (rectangle2.Y == rectangle2.Height * 16)
                    num = -2;
                rectangle2.Y += num;
                this.spriteBatch.Draw(Main.armorHeadTexture[drawPlayer.head], new Vector2((float)(int)((double)drawPlayer.position.X - (double)Main.screenPosition.X - (double)(drawPlayer.bodyFrame.Width / 2) + (double)(drawPlayer.width / 2)), (float)(int)((double)drawPlayer.position.Y - (double)Main.screenPosition.Y + (double)drawPlayer.height - (double)drawPlayer.bodyFrame.Height + 4.0 + (double)num)) + drawPlayer.headPosition + origin3, new Rectangle?(rectangle2), immuneAlpha10, drawPlayer.headRotation, origin3, 1f, effects1, 0.0f);
            }
            else if (drawPlayer.head > 0 && drawPlayer.head < 27)
            {
                this.spriteBatch.Draw(Main.armorHeadTexture[drawPlayer.head], new Vector2((float)(int)((double)drawPlayer.position.X - (double)Main.screenPosition.X - (double)(drawPlayer.bodyFrame.Width / 2) + (double)(drawPlayer.width / 2)), (float)(int)((double)drawPlayer.position.Y - (double)Main.screenPosition.Y + (double)drawPlayer.height - (double)drawPlayer.bodyFrame.Height + 4.0)) + drawPlayer.headPosition + origin3, new Rectangle?(drawPlayer.bodyFrame), immuneAlpha10, drawPlayer.headRotation, origin3, 1f, effects1, 0.0f);
            }
            else
            {
                rectangle1 = drawPlayer.bodyFrame;
                rectangle1.Y -= 336;
                if (rectangle1.Y < 0)
                    rectangle1.Y = 0;
                this.spriteBatch.Draw(Main.playerHairTexture[drawPlayer.hair], new Vector2((float)(int)((double)drawPlayer.position.X - (double)Main.screenPosition.X - (double)(drawPlayer.bodyFrame.Width / 2) + (double)(drawPlayer.width / 2)), (float)(int)((double)drawPlayer.position.Y - (double)Main.screenPosition.Y + (double)drawPlayer.height - (double)drawPlayer.bodyFrame.Height + 4.0)) + drawPlayer.headPosition + origin3, new Rectangle?(rectangle1), immuneAlpha3, drawPlayer.headRotation, origin3, 1f, effects1, 0.0f);
            }
            Color color = Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.5) / 16.0));
            if ((drawPlayer.itemAnimation > 0 || drawPlayer.inventory[drawPlayer.selectedItem].holdStyle > 0) && (drawPlayer.inventory[drawPlayer.selectedItem].type > 0 && !drawPlayer.dead) && !drawPlayer.inventory[drawPlayer.selectedItem].noUseGraphic)
            {
                if (drawPlayer.inventory[drawPlayer.selectedItem].useStyle == 5)
                {
                    int num = 10;
                    Vector2 vector2 = new Vector2((float)(Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width / 2), (float)(Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Height / 2));
                    if (drawPlayer.inventory[drawPlayer.selectedItem].type == 95)
                    {
                        num = 10;
                        vector2.Y += 2f;
                    }
                    else if (drawPlayer.inventory[drawPlayer.selectedItem].type == 96)
                        num = -5;
                    else if (drawPlayer.inventory[drawPlayer.selectedItem].type == 98)
                    {
                        num = -5;
                        vector2.Y -= 2f;
                    }
                    else if (drawPlayer.inventory[drawPlayer.selectedItem].type == 197)
                    {
                        num = -5;
                        vector2.Y += 4f;
                    }
                    else if (drawPlayer.inventory[drawPlayer.selectedItem].type == 126)
                    {
                        num = 4;
                        vector2.Y += 4f;
                    }
                    else if (drawPlayer.inventory[drawPlayer.selectedItem].type == (int)sbyte.MaxValue)
                    {
                        num = 4;
                        vector2.Y += 2f;
                    }
                    else if (drawPlayer.inventory[drawPlayer.selectedItem].type == 157)
                    {
                        num = 6;
                        vector2.Y += 2f;
                    }
                    else if (drawPlayer.inventory[drawPlayer.selectedItem].type == 160)
                        num = -8;
                    else if (drawPlayer.inventory[drawPlayer.selectedItem].type == 164 || drawPlayer.inventory[drawPlayer.selectedItem].type == 219)
                    {
                        num = 2;
                        vector2.Y += 4f;
                    }
                    else if (drawPlayer.inventory[drawPlayer.selectedItem].type == 165)
                    {
                        num = 12;
                        vector2.Y += 6f;
                    }
                    Vector2 origin4 = new Vector2((float)-num, (float)(Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Height / 2));
                    if (drawPlayer.direction == -1)
                        origin4 = new Vector2((float)(Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width + num), (float)(Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Height / 2));
                    this.spriteBatch.Draw(Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type], new Vector2((float)(int)((double)drawPlayer.itemLocation.X - (double)Main.screenPosition.X + (double)vector2.X), (float)(int)((double)drawPlayer.itemLocation.Y - (double)Main.screenPosition.Y + (double)vector2.Y)), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width, Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Height)), drawPlayer.inventory[drawPlayer.selectedItem].GetAlpha(color), drawPlayer.itemRotation, origin4, drawPlayer.inventory[drawPlayer.selectedItem].scale, effects2, 0.0f);
                    if (drawPlayer.inventory[drawPlayer.selectedItem].color != new Color())
                        this.spriteBatch.Draw(Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type], new Vector2((float)(int)((double)drawPlayer.itemLocation.X - (double)Main.screenPosition.X + (double)vector2.X), (float)(int)((double)drawPlayer.itemLocation.Y - (double)Main.screenPosition.Y + (double)vector2.Y)), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width, Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Height)), drawPlayer.inventory[drawPlayer.selectedItem].GetColor(color), drawPlayer.itemRotation, origin4, drawPlayer.inventory[drawPlayer.selectedItem].scale, effects2, 0.0f);
                }
                else
                {
                    this.spriteBatch.Draw(Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type], new Vector2((float)(int)((double)drawPlayer.itemLocation.X - (double)Main.screenPosition.X), (float)(int)((double)drawPlayer.itemLocation.Y - (double)Main.screenPosition.Y)), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width, Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Height)), drawPlayer.inventory[drawPlayer.selectedItem].GetAlpha(color), drawPlayer.itemRotation, new Vector2((float)((double)Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width * 0.5 - (double)Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width * 0.5 * (double)drawPlayer.direction), (float)Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Height), drawPlayer.inventory[drawPlayer.selectedItem].scale, effects2, 0.0f);
                    if (drawPlayer.inventory[drawPlayer.selectedItem].color != new Color())
                        this.spriteBatch.Draw(Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type], new Vector2((float)(int)((double)drawPlayer.itemLocation.X - (double)Main.screenPosition.X), (float)(int)((double)drawPlayer.itemLocation.Y - (double)Main.screenPosition.Y)), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width, Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Height)), drawPlayer.inventory[drawPlayer.selectedItem].GetColor(color), drawPlayer.itemRotation, new Vector2((float)((double)Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width * 0.5 - (double)Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width * 0.5 * (double)drawPlayer.direction), (float)Main.itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Height), drawPlayer.inventory[drawPlayer.selectedItem].scale, effects2, 0.0f);
                }
            }
            if (drawPlayer.body > 0 && drawPlayer.body < 16)
            {
                this.spriteBatch.Draw(Main.armorArmTexture[drawPlayer.body], new Vector2((float)(int)((double)drawPlayer.position.X - (double)Main.screenPosition.X - (double)(drawPlayer.bodyFrame.Width / 2) + (double)(drawPlayer.width / 2)), (float)(int)((double)drawPlayer.position.Y - (double)Main.screenPosition.Y + (double)drawPlayer.height - (double)drawPlayer.bodyFrame.Height + 4.0)) + drawPlayer.bodyPosition + new Vector2((float)(drawPlayer.bodyFrame.Width / 2), (float)(drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawPlayer.bodyFrame), immuneAlpha11, drawPlayer.bodyRotation, origin2, 1f, effects1, 0.0f);
                if (drawPlayer.body == 10 || drawPlayer.body == 11 || (drawPlayer.body == 12 || drawPlayer.body == 13) || drawPlayer.body == 14 || drawPlayer.body == 15)
                    this.spriteBatch.Draw(Main.playerHands2Texture, new Vector2((float)(int)((double)drawPlayer.position.X - (double)Main.screenPosition.X - (double)(drawPlayer.bodyFrame.Width / 2) + (double)(drawPlayer.width / 2)), (float)(int)((double)drawPlayer.position.Y - (double)Main.screenPosition.Y + (double)drawPlayer.height - (double)drawPlayer.bodyFrame.Height + 4.0)) + drawPlayer.bodyPosition + new Vector2((float)(drawPlayer.bodyFrame.Width / 2), (float)(drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawPlayer.bodyFrame), immuneAlpha5, drawPlayer.bodyRotation, origin2, 1f, effects1, 0.0f);
            }
            else
            {
                this.spriteBatch.Draw(Main.playerUnderShirt2Texture, new Vector2((float)(int)((double)drawPlayer.position.X - (double)Main.screenPosition.X - (double)(drawPlayer.bodyFrame.Width / 2) + (double)(drawPlayer.width / 2)), (float)(int)((double)drawPlayer.position.Y - (double)Main.screenPosition.Y + (double)drawPlayer.height - (double)drawPlayer.bodyFrame.Height + 4.0)) + drawPlayer.bodyPosition + new Vector2((float)(drawPlayer.bodyFrame.Width / 2), (float)(drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawPlayer.bodyFrame), immuneAlpha7, drawPlayer.bodyRotation, origin2, 1f, effects1, 0.0f);
                this.spriteBatch.Draw(Main.playerHands2Texture, new Vector2((float)(int)((double)drawPlayer.position.X - (double)Main.screenPosition.X - (double)(drawPlayer.bodyFrame.Width / 2) + (double)(drawPlayer.width / 2)), (float)(int)((double)drawPlayer.position.Y - (double)Main.screenPosition.Y + (double)drawPlayer.height - (double)drawPlayer.bodyFrame.Height + 4.0)) + drawPlayer.bodyPosition + new Vector2((float)(drawPlayer.bodyFrame.Width / 2), (float)(drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawPlayer.bodyFrame), immuneAlpha5, drawPlayer.bodyRotation, origin2, 1f, effects1, 0.0f);
            }
        }

        private static void HelpText()
        {
            bool flag1 = false;
            if (Main.player[Main.myPlayer].statLifeMax > 100)
                flag1 = true;
            bool flag2 = false;
            if (Main.player[Main.myPlayer].statManaMax > 0)
                flag2 = true;
            bool flag3 = true;
            bool flag4 = false;
            bool flag5 = false;
            bool flag6 = false;
            bool flag7 = false;
            bool flag8 = false;
            bool flag9 = false;
            bool flag10 = false;
            for (int index = 0; index < 44; ++index)
            {
                if (Main.player[Main.myPlayer].inventory[index].pick > 0 && Main.player[Main.myPlayer].inventory[index].name != "Copper Pickaxe")
                    flag3 = false;
                if (Main.player[Main.myPlayer].inventory[index].axe > 0 && Main.player[Main.myPlayer].inventory[index].name != "Copper Axe")
                    flag3 = false;
                if (Main.player[Main.myPlayer].inventory[index].hammer > 0)
                    flag3 = false;
                if (Main.player[Main.myPlayer].inventory[index].type == 11 || Main.player[Main.myPlayer].inventory[index].type == 12 || Main.player[Main.myPlayer].inventory[index].type == 13 || Main.player[Main.myPlayer].inventory[index].type == 14)
                    flag4 = true;
                if (Main.player[Main.myPlayer].inventory[index].type == 19 || Main.player[Main.myPlayer].inventory[index].type == 20 || Main.player[Main.myPlayer].inventory[index].type == 21 || Main.player[Main.myPlayer].inventory[index].type == 22)
                    flag5 = true;
                if (Main.player[Main.myPlayer].inventory[index].type == 75)
                    flag6 = true;
                if (Main.player[Main.myPlayer].inventory[index].type == 75)
                    flag8 = true;
                if (Main.player[Main.myPlayer].inventory[index].type == 68 || Main.player[Main.myPlayer].inventory[index].type == 70)
                    flag9 = true;
                if (Main.player[Main.myPlayer].inventory[index].type == 84)
                    flag10 = true;
                if (Main.player[Main.myPlayer].inventory[index].type == 117)
                    flag7 = true;
            }
            bool flag11 = false;
            bool flag12 = false;
            bool flag13 = false;
            bool flag14 = false;
            for (int index = 0; index < 1000; ++index)
            {
                if (Main.npc[index].active)
                {
                    if (Main.npc[index].type == 17)
                        flag11 = true;
                    if (Main.npc[index].type == 18)
                        flag12 = true;
                    if (Main.npc[index].type == 19)
                        flag14 = true;
                    if (Main.npc[index].type == 20)
                        flag13 = true;
                }
            }
            while (true)
            {
                ++Main.helpText;
                if (flag3)
                {
                    if (Main.helpText != 1)
                    {
                        if (Main.helpText != 2)
                        {
                            if (Main.helpText != 3)
                            {
                                if (Main.helpText != 4)
                                {
                                    if (Main.helpText != 5)
                                    {
                                        if (Main.helpText == 6)
                                            goto label_51;
                                    }
                                    else
                                        goto label_49;
                                }
                                else
                                    goto label_47;
                            }
                            else
                                goto label_45;
                        }
                        else
                            goto label_43;
                    }
                    else
                        break;
                }
                if (!flag3 || flag4 || flag5 || Main.helpText != 11)
                {
                    if (flag3 && flag4 && !flag5)
                    {
                        if (Main.helpText != 21)
                        {
                            if (Main.helpText == 22)
                                goto label_58;
                        }
                        else
                            goto label_56;
                    }
                    if (flag3 && flag5)
                    {
                        if (Main.helpText != 31)
                        {
                            if (Main.helpText == 32)
                                goto label_63;
                        }
                        else
                            goto label_61;
                    }
                    if (flag1 || Main.helpText != 41)
                    {
                        if (flag2 || Main.helpText != 42)
                        {
                            if (flag2 || flag6 || Main.helpText != 43)
                            {
                                if (!flag11 && !flag12)
                                {
                                    if (Main.helpText != 51)
                                    {
                                        if (Main.helpText != 52)
                                        {
                                            if (Main.helpText == 53)
                                                goto label_76;
                                        }
                                        else
                                            goto label_74;
                                    }
                                    else
                                        goto label_72;
                                }
                                if (flag11 || Main.helpText != 61)
                                {
                                    if (flag12 || Main.helpText != 62)
                                    {
                                        if (flag14 || Main.helpText != 63)
                                        {
                                            if (flag13 || Main.helpText != 64)
                                            {
                                                if (!flag8 || Main.helpText != 71)
                                                {
                                                    if (!flag9 || Main.helpText != 72)
                                                    {
                                                        if (!flag8 && !flag9 || Main.helpText != 80)
                                                        {
                                                            if (flag10 || Main.helpText != 201)
                                                            {
                                                                if (!flag7 || Main.helpText != 202)
                                                                {
                                                                    if (Main.helpText != 1000)
                                                                    {
                                                                        if (Main.helpText != 1001)
                                                                        {
                                                                            if (Main.helpText != 1002)
                                                                            {
                                                                                if (Main.helpText > 1100)
                                                                                    Main.helpText = 0;
                                                                            }
                                                                            else
                                                                                goto label_100;
                                                                        }
                                                                        else
                                                                            goto label_98;
                                                                    }
                                                                    else
                                                                        goto label_96;
                                                                }
                                                                else
                                                                    goto label_94;
                                                            }
                                                            else
                                                                goto label_92;
                                                        }
                                                        else
                                                            goto label_90;
                                                    }
                                                    else
                                                        goto label_88;
                                                }
                                                else
                                                    goto label_86;
                                            }
                                            else
                                                goto label_84;
                                        }
                                        else
                                            goto label_82;
                                    }
                                    else
                                        goto label_80;
                                }
                                else
                                    goto label_78;
                            }
                            else
                                goto label_69;
                        }
                        else
                            goto label_67;
                    }
                    else
                        goto label_65;
                }
                else
                    goto label_53;
            }
            Main.npcChatText = "You can use your pickaxe to dig through dirt, and your axe to chop down trees. Just place your cursor over the tile and click!";
            return;
        label_43:
            Main.npcChatText = "If you want to survive, you will need to create weapons and shelter. Start by chopping down trees and gathering wood.";
            return;
        label_45:
            Main.npcChatText = "Press ESC to access your crafting menu. When you have enough wood, create a workbench. This will allow you to create more complicated things, as long as you are standing close to it.";
            return;
        label_47:
            Main.npcChatText = "You can build a shelter by placing wood or other blocks in the world. Don't forget to create and place walls.";
            return;
        label_49:
            Main.npcChatText = "Once you have a wooden sword, you might try to gather some gel from the slimes. Combine wood and gel to make a torch!";
            return;
        label_51:
            Main.npcChatText = "To interact with backgrounds and placed objects, use a hammer!";
            return;
        label_53:
            Main.npcChatText = "You should do some mining to find metal ore. You can craft very useful things with it.";
            return;
        label_56:
            Main.npcChatText = "Now that you have some ore, you will need to turn it into a bar in order to make items with it. This requires a furnace!";
            return;
        label_58:
            Main.npcChatText = "You can create a furnace out of torches, wood, and stone. Make sure you are standing near a work bench.";
            return;
        label_61:
            Main.npcChatText = "You will need an anvil to make most things out of metal bars.";
            return;
        label_63:
            Main.npcChatText = "Anvils can be crafted out of iron, or purchased from a merchant.";
            return;
        label_65:
            Main.npcChatText = "Underground are crystal hearts that can be used to increase your max life. You will need a hammer to obtain them.";
            return;
        label_67:
            Main.npcChatText = "If you gather 10 fallen stars, they can be combined to create an item that will increase your magic capacity.";
            return;
        label_69:
            Main.npcChatText = "Stars fall all over the world at night. They can be used for all sorts of usefull things. If you see one, be sure to grab it because they disappear after sunrise.";
            return;
        label_72:
            Main.npcChatText = "There are many different ways you can attract people to move in to our town. They will of course need a home to live in.";
            return;
        label_74:
            Main.npcChatText = "In order for a room to be considered a home, it needs to have a door, chair, table, and a light source.  Make sure the house has walls as well.";
            return;
        label_76:
            Main.npcChatText = "Two people will not live in the same home. Also, if their home is destroyed, they will look for a new place to live.";
            return;
        label_78:
            Main.npcChatText = "If you want a merchant to move in, you will need to gather plenty of money. 50 silver coins should do the trick!";
            return;
        label_80:
            Main.npcChatText = "For a nurse to move in, you might want to increase your maximum life.";
            return;
        label_82:
            Main.npcChatText = "If you had a gun, I bet an arms dealer might show up to sell you some ammo!";
            return;
        label_84:
            Main.npcChatText = "You should prove yourself by defeating a strong monster. That will get the attention of a dryad.";
            return;
        label_86:
            Main.npcChatText = "If you combine lenses at a demon altar, you might be able to find a way to summon a powerful monster. You will want to wait until night before using it, though.";
            return;
        label_88:
            Main.npcChatText = "You can create worm bait with rotten chunks and vile powder. Make sure you are in a corrupt area before using it.";
            return;
        label_90:
            Main.npcChatText = "Demonic altars can usually be found in the corruption. You will need to be near them to craft some items.";
            return;
        label_92:
            Main.npcChatText = "You can make a grappling hook from a hook and 3 chains. Skeletons found deep underground usually carry hooks, and chains can be made from iron bars.";
            return;
        label_94:
            Main.npcChatText = "You can craft a space gun using a flintlock pistol, 10 fallen stars, and 30 meteorite bars.";
            return;
        label_96:
            Main.npcChatText = "If you see a pot, be sure to smash it open. They contain all sorts of useful supplies.";
            return;
        label_98:
            Main.npcChatText = "There is treasure hidden all over the world. Some amazing things can be found deep underground!";
            return;
        label_100:
            Main.npcChatText = "Smashing a shadow orb will cause a meteor to fall out of the sky. Shadow orbs can usually be found in the chasms around corrupt areas.";
        }

        protected void DrawChat()
        {
            if (Main.player[Main.myPlayer].talkNPC < 0 && Main.player[Main.myPlayer].sign == -1)
            {
                Main.npcChatText = "";
            }
            else
            {
                Color color1 = new Color(200, 200, 200, 200);
                int num1 = ((int)Main.mouseTextColor * 2 + (int)byte.MaxValue) / 3;
                Color color2 = new Color(num1, num1, num1, num1);
                int length = 10;
                int index1 = 0;
                string[] strArray1 = new string[length];
                int startIndex1 = 0;
                int num2 = 0;
                if (Main.npcChatText == null)
                    Main.npcChatText = "";
                for (int startIndex2 = 0; startIndex2 < Main.npcChatText.Length; ++startIndex2)
                {
                    if ((int)Encoding.ASCII.GetBytes(Main.npcChatText.Substring(startIndex2, 1))[0] == 10)
                    {
                        strArray1[index1] = Main.npcChatText.Substring(startIndex1, startIndex2 - startIndex1);
                        ++index1;
                        startIndex1 = startIndex2 + 1;
                        num2 = startIndex2 + 1;
                    }
                    else if (Main.npcChatText.Substring(startIndex2, 1) == " " || startIndex2 == Main.npcChatText.Length - 1)
                    {
                        if ((double)Main.fontMouseText.MeasureString(Main.npcChatText.Substring(startIndex1, startIndex2 - startIndex1)).X > 470.0)
                        {
                            strArray1[index1] = Main.npcChatText.Substring(startIndex1, num2 - startIndex1);
                            ++index1;
                            startIndex1 = num2 + 1;
                        }
                        num2 = startIndex2;
                    }
                    if (index1 == 10)
                    {
                        Main.npcChatText = Main.npcChatText.Substring(0, startIndex2 - 1);
                        startIndex1 = startIndex2 - 1;
                        index1 = 9;
                        break;
                    }
                }
                if (index1 < 10)
                    strArray1[index1] = Main.npcChatText.Substring(startIndex1, Main.npcChatText.Length - startIndex1);
                if (Main.editSign)
                {
                    ++this.textBlinkerCount;
                    if (this.textBlinkerCount >= 20)
                    {
                        this.textBlinkerState = this.textBlinkerState != 0 ? 0 : 1;
                        this.textBlinkerCount = 0;
                    }
                    //TODO: Figure out if we really need?
                    //if (this.textBlinkerState == 1)
                    //{
                    //    string[] strArray2;
                    //    IntPtr index2;
                    //    (strArray2 = strArray1)[(int)(index2 = (IntPtr)index1)] = strArray2[index2] + "|";
                    //}
                }
                int num3 = index1 + 1;
                this.spriteBatch.Draw(Main.chatBackTexture, new Vector2((float)(Main.screenWidth / 2 - Main.chatBackTexture.Width / 2), 100f), new Rectangle?(new Rectangle(0, 0, Main.chatBackTexture.Width, (num3 + 1) * 30)), color1, 0.0f, new Vector2(), 1f, SpriteEffects.None, 0.0f);
                this.spriteBatch.Draw(Main.chatBackTexture, new Vector2((float)(Main.screenWidth / 2 - Main.chatBackTexture.Width / 2), (float)(100 + (num3 + 1) * 30)), new Rectangle?(new Rectangle(0, Main.chatBackTexture.Height - 30, Main.chatBackTexture.Width, 30)), color1, 0.0f, new Vector2(), 1f, SpriteEffects.None, 0.0f);
                for (int index2 = 0; index2 < num3; ++index2)
                {
                    for (int index3 = 0; index3 < 5; ++index3)
                    {
                        Color color3 = Color.Black;
                        int num4 = 170 + (Main.screenWidth - 800) / 2;
                        int num5 = 120 + index2 * 30;
                        if (index3 == 0)
                            num4 -= 2;
                        if (index3 == 1)
                            num4 += 2;
                        if (index3 == 2)
                            num5 -= 2;
                        if (index3 == 3)
                            num5 += 2;
                        if (index3 == 4)
                            color3 = color2;
                        this.spriteBatch.DrawString(Main.fontMouseText, strArray1[index2], new Vector2((float)num4, (float)num5), color3, 0.0f, new Vector2(), 1f, SpriteEffects.None, 0.0f);
                    }
                }
                int num6 = (int)Main.mouseTextColor;
                color2 = new Color(num6, (int)((double)num6 / 1.1), num6 / 2, num6);
                string text = "";
                int price = Main.player[Main.myPlayer].statLifeMax - Main.player[Main.myPlayer].statLife;
                if (Main.player[Main.myPlayer].sign > -1)
                    text = !Main.editSign ? "Edit" : "Save";
                else if (Main.npc[Main.player[Main.myPlayer].talkNPC].type == 17 || Main.npc[Main.player[Main.myPlayer].talkNPC].type == 19 || (Main.npc[Main.player[Main.myPlayer].talkNPC].type == 20 || Main.npc[Main.player[Main.myPlayer].talkNPC].type == 38) || Main.npc[Main.player[Main.myPlayer].talkNPC].type == 54)
                    text = "Shop";
                else if (Main.npc[Main.player[Main.myPlayer].talkNPC].type == 22)
                    text = "Help";
                else if (Main.npc[Main.player[Main.myPlayer].talkNPC].type == 18)
                {
                    string str = "";
                    int num4 = 0;
                    int num5 = 0;
                    int num7 = 0;
                    int num8 = 0;
                    int num9 = price;
                    if (num9 > 0)
                    {
                        num9 = (int)((double)num9 * 0.75);
                        if (num9 < 1)
                            num9 = 1;
                    }
                    if (num9 < 0)
                        num9 = 0;
                    if (num9 >= 1000000)
                    {
                        num4 = num9 / 1000000;
                        num9 -= num4 * 1000000;
                    }
                    if (num9 >= 10000)
                    {
                        num5 = num9 / 10000;
                        num9 -= num5 * 10000;
                    }
                    if (num9 >= 100)
                    {
                        num7 = num9 / 100;
                        num9 -= num7 * 100;
                    }
                    if (num9 >= 1)
                        num8 = num9;
                    if (num4 > 0)
                        str = str + (object)num4 + " platinum ";
                    if (num5 > 0)
                        str = str + (object)num5 + " gold ";
                    if (num7 > 0)
                        str = str + (object)num7 + " silver ";
                    if (num8 > 0)
                        str = str + (object)num8 + " copper ";
                    float num10 = (float)Main.mouseTextColor / (float)byte.MaxValue;
                    if (num4 > 0)
                        color2 = new Color((int)(byte)(220.0 * (double)num10), (int)(byte)(220.0 * (double)num10), (int)(byte)(198.0 * (double)num10), (int)Main.mouseTextColor);
                    else if (num5 > 0)
                        color2 = new Color((int)(byte)(224.0 * (double)num10), (int)(byte)(201.0 * (double)num10), (int)(byte)(92.0 * (double)num10), (int)Main.mouseTextColor);
                    else if (num7 > 0)
                        color2 = new Color((int)(byte)(181.0 * (double)num10), (int)(byte)(192.0 * (double)num10), (int)(byte)(193.0 * (double)num10), (int)Main.mouseTextColor);
                    else if (num8 > 0)
                        color2 = new Color((int)(byte)(246.0 * (double)num10), (int)(byte)(138.0 * (double)num10), (int)(byte)(96.0 * (double)num10), (int)Main.mouseTextColor);
                    text = "Heal (" + str + ")";
                    if (num9 == 0)
                        text = "Heal";
                }
                int num11 = 180 + (Main.screenWidth - 800) / 2;
                int num12 = 130 + num3 * 30;
                float scale1 = 0.9f;
                if (Main.mouseState.X > num11 && (double)Main.mouseState.X < (double)num11 + (double)Main.fontMouseText.MeasureString(text).X && Main.mouseState.Y > num12 && (double)Main.mouseState.Y < (double)num12 + (double)Main.fontMouseText.MeasureString(text).Y)
                {
                    Main.player[Main.myPlayer].mouseInterface = true;
                    scale1 = 1.1f;
                    if (!Main.npcChatFocus2)
                        Main.PlaySound(12, -1, -1, 1);
                    Main.npcChatFocus2 = true;
                    Main.player[Main.myPlayer].releaseUseItem = false;
                }
                else
                {
                    if (Main.npcChatFocus2)
                        Main.PlaySound(12, -1, -1, 1);
                    Main.npcChatFocus2 = false;
                }
                Vector2 origin;
                for (int index2 = 0; index2 < 5; ++index2)
                {
                    int num4 = num11;
                    int num5 = num12;
                    Color color3 = Color.Black;
                    if (index2 == 0)
                        num4 -= 2;
                    if (index2 == 1)
                        num4 += 2;
                    if (index2 == 2)
                        num5 -= 2;
                    if (index2 == 3)
                        num5 += 2;
                    if (index2 == 4)
                        color3 = color2;
                    origin = Main.fontMouseText.MeasureString(text);
                    origin *= 0.5f;
                    this.spriteBatch.DrawString(Main.fontMouseText, text, new Vector2((float)num4 + origin.X, (float)num5 + origin.Y), color3, 0.0f, origin, scale1, SpriteEffects.None, 0.0f);
                }
                color2 = new Color(num6, (int)((double)num6 / 1.1), num6 / 2, num6);
                int num13 = num11 + (int)Main.fontMouseText.MeasureString(text).X + 20;
                int num14 = 130 + num3 * 30;
                float scale2 = 0.9f;
                if (Main.mouseState.X > num13 && (double)Main.mouseState.X < (double)num13 + (double)Main.fontMouseText.MeasureString("Close").X && Main.mouseState.Y > num14 && (double)Main.mouseState.Y < (double)num14 + (double)Main.fontMouseText.MeasureString("Close").Y)
                {
                    scale2 = 1.1f;
                    if (!Main.npcChatFocus1)
                        Main.PlaySound(12, -1, -1, 1);
                    Main.npcChatFocus1 = true;
                    Main.player[Main.myPlayer].releaseUseItem = false;
                }
                else
                {
                    if (Main.npcChatFocus1)
                        Main.PlaySound(12, -1, -1, 1);
                    Main.npcChatFocus1 = false;
                }
                for (int index2 = 0; index2 < 5; ++index2)
                {
                    int num4 = num13;
                    int num5 = num14;
                    Color color3 = Color.Black;
                    if (index2 == 0)
                        num4 -= 2;
                    if (index2 == 1)
                        num4 += 2;
                    if (index2 == 2)
                        num5 -= 2;
                    if (index2 == 3)
                        num5 += 2;
                    if (index2 == 4)
                        color3 = color2;
                    origin = Main.fontMouseText.MeasureString("Close");
                    origin *= 0.5f;
                    this.spriteBatch.DrawString(Main.fontMouseText, "Close", new Vector2((float)num4 + origin.X, (float)num5 + origin.Y), color3, 0.0f, origin, scale2, SpriteEffects.None, 0.0f);
                }
                if (Main.mouseState.LeftButton == ButtonState.Pressed && Main.mouseLeftRelease)
                {
                    Main.mouseLeftRelease = false;
                    Main.player[Main.myPlayer].releaseUseItem = false;
                    Main.player[Main.myPlayer].mouseInterface = true;
                    if (Main.npcChatFocus1)
                    {
                        Main.player[Main.myPlayer].talkNPC = -1;
                        Main.player[Main.myPlayer].sign = -1;
                        Main.editSign = false;
                        Main.npcChatText = "";
                        Main.PlaySound(11, -1, -1, 1);
                    }
                    else if (Main.npcChatFocus2)
                    {
                        if (Main.player[Main.myPlayer].sign != -1)
                        {
                            if (Main.editSign)
                            {
                                Main.PlaySound(12, -1, -1, 1);
                                int num4 = Main.player[Main.myPlayer].sign;
                                Sign.TextSign(num4, Main.npcChatText);
                                Main.editSign = false;
                                if (Main.netMode == 1)
                                    NetMessage.SendData(47, -1, -1, "", num4, 0.0f, 0.0f, 0.0f);
                            }
                            else
                            {
                                Main.PlaySound(12, -1, -1, 1);
                                Main.editSign = true;
                            }
                        }
                        else if (Main.npc[Main.player[Main.myPlayer].talkNPC].type == 17)
                        {
                            Main.playerInventory = true;
                            Main.npcChatText = "";
                            Main.npcShop = 1;
                            Main.PlaySound(12, -1, -1, 1);
                        }
                        else if (Main.npc[Main.player[Main.myPlayer].talkNPC].type == 19)
                        {
                            Main.playerInventory = true;
                            Main.npcChatText = "";
                            Main.npcShop = 2;
                            Main.PlaySound(12, -1, -1, 1);
                        }
                        else if (Main.npc[Main.player[Main.myPlayer].talkNPC].type == 20)
                        {
                            Main.playerInventory = true;
                            Main.npcChatText = "";
                            Main.npcShop = 3;
                            Main.PlaySound(12, -1, -1, 1);
                        }
                        else if (Main.npc[Main.player[Main.myPlayer].talkNPC].type == 38)
                        {
                            Main.playerInventory = true;
                            Main.npcChatText = "";
                            Main.npcShop = 4;
                            Main.PlaySound(12, -1, -1, 1);
                        }
                        else if (Main.npc[Main.player[Main.myPlayer].talkNPC].type == 54)
                        {
                            Main.playerInventory = true;
                            Main.npcChatText = "";
                            Main.npcShop = 5;
                            Main.PlaySound(12, -1, -1, 1);
                        }
                        else if (Main.npc[Main.player[Main.myPlayer].talkNPC].type == 22)
                        {
                            Main.PlaySound(12, -1, -1, 1);
                            Main.HelpText();
                        }
                        else if (Main.npc[Main.player[Main.myPlayer].talkNPC].type == 18)
                        {
                            Main.PlaySound(12, -1, -1, 1);
                            if (price > 0)
                            {
                                if (Main.player[Main.myPlayer].BuyItem(price))
                                {
                                    Main.PlaySound(2, -1, -1, 4);
                                    Main.player[Main.myPlayer].HealEffect(Main.player[Main.myPlayer].statLifeMax - Main.player[Main.myPlayer].statLife);
                                    Main.npcChatText = (double)Main.player[Main.myPlayer].statLife >= (double)Main.player[Main.myPlayer].statLifeMax * 0.25 ? ((double)Main.player[Main.myPlayer].statLife >= (double)Main.player[Main.myPlayer].statLifeMax * 0.5 ? ((double)Main.player[Main.myPlayer].statLife >= (double)Main.player[Main.myPlayer].statLifeMax * 0.75 ? "That didn't hurt too bad, now did it?" : "All better. I don't want to see you jumping off anymore cliffs.") : "That's probably going to leave a scar.") : "I managed to sew your face back on. Be more careful next time.";
                                    Main.player[Main.myPlayer].statLife = Main.player[Main.myPlayer].statLifeMax;
                                }
                                else
                                {
                                    int num4 = Main.rand.Next(3);
                                    if (num4 == 0)
                                        Main.npcChatText = "I'm sorry, but you can't afford me.";
                                    if (num4 == 1)
                                        Main.npcChatText = "I'm gonna need more gold than that.";
                                    if (num4 == 2)
                                        Main.npcChatText = "I don't work for free you know.";
                                }
                            }
                            else
                            {
                                int num4 = Main.rand.Next(3);
                                if (num4 == 0)
                                    Main.npcChatText = "I don't give happy endings.";
                                if (num4 == 1)
                                    Main.npcChatText = "I can't do anymore for you without plastic surgery.";
                                if (num4 == 2)
                                    Main.npcChatText = "Quit wasting my time.";
                            }
                        }
                    }
                }
            }
        }

        protected void DrawInterface()
        {
            if (!Main.hideUI)
            {
                if (Main.signBubble)
                {
                    int num1 = (int)((double)Main.signX - (double)Main.screenPosition.X);
                    int num2 = (int)((double)Main.signY - (double)Main.screenPosition.Y);
                    SpriteEffects effects = SpriteEffects.None;
                    int num3;
                    if ((double)Main.signX > (double)Main.player[Main.myPlayer].position.X + (double)Main.player[Main.myPlayer].width)
                    {
                        effects = SpriteEffects.FlipHorizontally;
                        num3 = num1 + (-8 - Main.chat2Texture.Width);
                    }
                    else
                        num3 = num1 + 8;
                    int num4 = num2 - 22;
                    this.spriteBatch.Draw(Main.chat2Texture, new Vector2((float)num3, (float)num4), new Rectangle?(new Rectangle(0, 0, Main.chat2Texture.Width, Main.chat2Texture.Height)), new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), 0.0f, new Vector2(), 1f, effects, 0.0f);
                    Main.signBubble = false;
                }
                Rectangle rectangle1;
                for (int index = 0; index < (int)byte.MaxValue; ++index)
                {
                    if (Main.player[index].active && Main.myPlayer != index && !Main.player[index].dead)
                    {
                        rectangle1 = new Rectangle((int)((double)Main.player[index].position.X + (double)Main.player[index].width * 0.5 - 16.0), (int)((double)Main.player[index].position.Y + (double)Main.player[index].height - 48.0), 32, 48);
                        if (Main.player[Main.myPlayer].team > 0 && Main.player[Main.myPlayer].team == Main.player[index].team)
                        {
                            Rectangle rectangle2 = new Rectangle((int)Main.screenPosition.X, (int)Main.screenPosition.Y, Main.screenWidth, Main.screenHeight);
                            string text1 = Main.player[index].name;
                            if (Main.player[index].statLife < Main.player[index].statLifeMax)
                                text1 = text1 + (object)": " + (string)(object)Main.player[index].statLife + "/" + (string)(object)Main.player[index].statLifeMax;
                            Vector2 position1 = Main.fontMouseText.MeasureString(text1);
                            float num1 = 0.0f;
                            if (Main.player[index].chatShowTime > 0)
                                num1 = -position1.Y;
                            float num2 = 0.0f;
                            float num3 = (float)Main.mouseTextColor / (float)byte.MaxValue;
                            Color color = new Color((int)(byte)((double)Main.teamColor[Main.player[index].team].R * (double)num3), (int)(byte)((double)Main.teamColor[Main.player[index].team].G * (double)num3), (int)(byte)((double)Main.teamColor[Main.player[index].team].B * (double)num3), (int)Main.mouseTextColor);
                            Vector2 vector2 = new Vector2((float)(Main.screenWidth / 2) + Main.screenPosition.X, (float)(Main.screenHeight / 2) + Main.screenPosition.Y);
                            float num4 = Main.player[index].position.X + (float)(Main.player[index].width / 2) - vector2.X;
                            float num5 = (float)((double)Main.player[index].position.Y - (double)position1.Y - 2.0) + num1 - vector2.Y;
                            float num6 = (float)Math.Sqrt((double)num4 * (double)num4 + (double)num5 * (double)num5);
                            int num7 = Main.screenHeight;
                            if (Main.screenHeight > Main.screenWidth)
                                num7 = Main.screenWidth;
                            int num8 = num7 / 2 - 30;
                            if (num8 < 100)
                                num8 = 100;
                            if ((double)num6 < (double)num8)
                            {
                                position1.X = (float)((double)Main.player[index].position.X + (double)(Main.player[index].width / 2) - (double)position1.X / 2.0) - Main.screenPosition.X;
                                position1.Y = (float)((double)Main.player[index].position.Y - (double)position1.Y - 2.0) + num1 - Main.screenPosition.Y;
                            }
                            else
                            {
                                num2 = num6;
                                float num9 = (float)num8 / num6;
                                position1.X = (float)((double)(Main.screenWidth / 2) + (double)num4 * (double)num9 - (double)position1.X / 2.0);
                                position1.Y = (float)(Main.screenHeight / 2) + num5 * num9;
                            }
                            if ((double)num2 > 0.0)
                            {
                                string text2 = "(" + (object)(int)((double)num2 / 16.0 * 2.0) + " ft)";
                                Vector2 position2 = Main.fontMouseText.MeasureString(text2);
                                position2.X = (float)((double)position1.X + (double)Main.fontMouseText.MeasureString(text1).X / 2.0 - (double)position2.X / 2.0);
                                position2.Y = (float)((double)position1.Y + (double)Main.fontMouseText.MeasureString(text1).Y / 2.0 - (double)position2.Y / 2.0 - 20.0);
                                this.spriteBatch.DrawString(Main.fontMouseText, text2, new Vector2(position2.X - 2f, position2.Y), Color.Black, 0.0f, new Vector2(), 1f, SpriteEffects.None, 0.0f);
                                this.spriteBatch.DrawString(Main.fontMouseText, text2, new Vector2(position2.X + 2f, position2.Y), Color.Black, 0.0f, new Vector2(), 1f, SpriteEffects.None, 0.0f);
                                this.spriteBatch.DrawString(Main.fontMouseText, text2, new Vector2(position2.X, position2.Y - 2f), Color.Black, 0.0f, new Vector2(), 1f, SpriteEffects.None, 0.0f);
                                this.spriteBatch.DrawString(Main.fontMouseText, text2, new Vector2(position2.X, position2.Y + 2f), Color.Black, 0.0f, new Vector2(), 1f, SpriteEffects.None, 0.0f);
                                this.spriteBatch.DrawString(Main.fontMouseText, text2, position2, color, 0.0f, new Vector2(), 1f, SpriteEffects.None, 0.0f);
                            }
                            this.spriteBatch.DrawString(Main.fontMouseText, text1, new Vector2(position1.X - 2f, position1.Y), Color.Black, 0.0f, new Vector2(), 1f, SpriteEffects.None, 0.0f);
                            this.spriteBatch.DrawString(Main.fontMouseText, text1, new Vector2(position1.X + 2f, position1.Y), Color.Black, 0.0f, new Vector2(), 1f, SpriteEffects.None, 0.0f);
                            this.spriteBatch.DrawString(Main.fontMouseText, text1, new Vector2(position1.X, position1.Y - 2f), Color.Black, 0.0f, new Vector2(), 1f, SpriteEffects.None, 0.0f);
                            this.spriteBatch.DrawString(Main.fontMouseText, text1, new Vector2(position1.X, position1.Y + 2f), Color.Black, 0.0f, new Vector2(), 1f, SpriteEffects.None, 0.0f);
                            this.spriteBatch.DrawString(Main.fontMouseText, text1, position1, color, 0.0f, new Vector2(), 1f, SpriteEffects.None, 0.0f);
                        }
                    }
                }
                if (Main.npcChatText != "" || Main.player[Main.myPlayer].sign != -1)
                    this.DrawChat();
                Color color1 = new Color(200, 200, 200, 200);
                bool flag1 = false;
                int rare1 = 0;
                int num10 = Main.screenWidth - 800;
                int num11 = Main.player[Main.myPlayer].statLifeMax / 20;
                if (num11 >= 10)
                    num11 = 10;
                this.spriteBatch.DrawString(Main.fontMouseText, "Life", new Vector2((float)(500 + 13 * num11) - Main.fontMouseText.MeasureString("Life").X * 0.5f + (float)num10, 6f), new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), 0.0f, new Vector2(), 1f, SpriteEffects.None, 0.0f);
                int num12 = 20;
                for (int index = 1; index < Main.player[Main.myPlayer].statLifeMax / num12 + 1; ++index)
                {
                    float scale = 1f;
                    int num1;
                    if (Main.player[Main.myPlayer].statLife >= index * num12)
                    {
                        num1 = (int)byte.MaxValue;
                    }
                    else
                    {
                        float num2 = (float)(Main.player[Main.myPlayer].statLife - (index - 1) * num12) / (float)num12;
                        num1 = (int)(30.0 + 225.0 * (double)num2);
                        if (num1 < 30)
                            num1 = 30;
                        scale = (float)((double)num2 / 4.0 + 0.75);
                        if ((double)scale < 0.75)
                            scale = 0.75f;
                    }
                    int num3 = 0;
                    int num4 = 0;
                    if (index > 10)
                    {
                        num3 -= 260;
                        num4 += 26;
                    }
                    this.spriteBatch.Draw(Main.heartTexture, new Vector2((float)(500 + 26 * (index - 1) + num3 + num10), (float)(32.0 + ((double)Main.heartTexture.Height - (double)Main.heartTexture.Height * (double)scale) / 2.0) + (float)num4), new Rectangle?(new Rectangle(0, 0, Main.heartTexture.Width, Main.heartTexture.Height)), new Color(num1, num1, num1, num1), 0.0f, new Vector2(), scale, SpriteEffects.None, 0.0f);
                }
                int num13 = 20;
                if (Main.player[Main.myPlayer].statManaMax > 0)
                {
                    int num1 = Main.player[Main.myPlayer].statManaMax / 20;
                    this.spriteBatch.DrawString(Main.fontMouseText, "Mana", new Vector2((float)(750 + num10), 6f), new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), 0.0f, new Vector2(), 1f, SpriteEffects.None, 0.0f);
                    for (int index = 1; index < Main.player[Main.myPlayer].statManaMax / num13 + 1; ++index)
                    {
                        float scale = 1f;
                        int num2;
                        if (Main.player[Main.myPlayer].statMana >= index * num13)
                        {
                            num2 = (int)byte.MaxValue;
                        }
                        else
                        {
                            float num3 = (float)(Main.player[Main.myPlayer].statMana - (index - 1) * num13) / (float)num13;
                            num2 = (int)(30.0 + 225.0 * (double)num3);
                            if (num2 < 30)
                                num2 = 30;
                            scale = (float)((double)num3 / 4.0 + 0.75);
                            if ((double)scale < 0.75)
                                scale = 0.75f;
                        }
                        this.spriteBatch.Draw(Main.manaTexture, new Vector2((float)(775 + num10), (float)(30 + Main.manaTexture.Height / 2) + (float)(((double)Main.manaTexture.Height - (double)Main.manaTexture.Height * (double)scale) / 2.0) + (float)(28 * (index - 1))), new Rectangle?(new Rectangle(0, 0, Main.manaTexture.Width, Main.manaTexture.Height)), new Color(num2, num2, num2, num2), 0.0f, new Vector2((float)(Main.manaTexture.Width / 2), (float)(Main.manaTexture.Height / 2)), scale, SpriteEffects.None, 0.0f);
                    }
                }
                if (Main.player[Main.myPlayer].breath < Main.player[Main.myPlayer].breathMax)
                {
                    int num1 = 76;
                    int num2 = Main.player[Main.myPlayer].breathMax / 20;
                    this.spriteBatch.DrawString(Main.fontMouseText, "Breath", new Vector2((float)(500 + 13 * num11) - Main.fontMouseText.MeasureString("Breath").X * 0.5f + (float)num10, (float)(6 + num1)), new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), 0.0f, new Vector2(), 1f, SpriteEffects.None, 0.0f);
                    int num3 = 20;
                    for (int index = 1; index < Main.player[Main.myPlayer].breathMax / num3 + 1; ++index)
                    {
                        float scale = 1f;
                        int num4;
                        if (Main.player[Main.myPlayer].breath >= index * num3)
                        {
                            num4 = (int)byte.MaxValue;
                        }
                        else
                        {
                            float num5 = (float)(Main.player[Main.myPlayer].breath - (index - 1) * num3) / (float)num3;
                            num4 = (int)(30.0 + 225.0 * (double)num5);
                            if (num4 < 30)
                                num4 = 30;
                            scale = (float)((double)num5 / 4.0 + 0.75);
                            if ((double)scale < 0.75)
                                scale = 0.75f;
                        }
                        int num6 = 0;
                        int num7 = 0;
                        if (index > 10)
                        {
                            num6 -= 260;
                            num7 += 26;
                        }
                        this.spriteBatch.Draw(Main.bubbleTexture, new Vector2((float)(500 + 26 * (index - 1) + num6 + num10), (float)(32.0 + ((double)Main.bubbleTexture.Height - (double)Main.bubbleTexture.Height * (double)scale) / 2.0) + (float)num7 + (float)num1), new Rectangle?(new Rectangle(0, 0, Main.bubbleTexture.Width, Main.bubbleTexture.Height)), new Color(num4, num4, num4, num4), 0.0f, new Vector2(), scale, SpriteEffects.None, 0.0f);
                    }
                }
                if (Main.player[Main.myPlayer].dead)
                    Main.playerInventory = false;
                if (!Main.playerInventory)
                    Main.player[Main.myPlayer].chest = -1;
                string cursorText1 = "";
                if (Main.playerInventory)
                {
                    if (Main.netMode == 1)
                    {
                        int num1 = 675 + Main.screenWidth - 800;
                        int y = 114;
                        if (Main.player[Main.myPlayer].hostile)
                        {
                            this.spriteBatch.Draw(Main.itemTexture[4], new Vector2((float)(num1 - 2), (float)y), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[4].Width, Main.itemTexture[4].Height)), Main.teamColor[Main.player[Main.myPlayer].team], 0.0f, new Vector2(), 1f, SpriteEffects.None, 0.0f);
                            this.spriteBatch.Draw(Main.itemTexture[4], new Vector2((float)(num1 + 2), (float)y), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[4].Width, Main.itemTexture[4].Height)), Main.teamColor[Main.player[Main.myPlayer].team], 0.0f, new Vector2(), 1f, SpriteEffects.FlipHorizontally, 0.0f);
                        }
                        else
                        {
                            this.spriteBatch.Draw(Main.itemTexture[4], new Vector2((float)(num1 - 16), (float)(y + 14)), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[4].Width, Main.itemTexture[4].Height)), Main.teamColor[Main.player[Main.myPlayer].team], -0.785f, new Vector2(), 1f, SpriteEffects.None, 0.0f);
                            this.spriteBatch.Draw(Main.itemTexture[4], new Vector2((float)(num1 + 2), (float)(y + 14)), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[4].Width, Main.itemTexture[4].Height)), Main.teamColor[Main.player[Main.myPlayer].team], -0.785f, new Vector2(), 1f, SpriteEffects.None, 0.0f);
                        }
                        if (Main.mouseState.X > num1 && Main.mouseState.X < num1 + 34 && Main.mouseState.Y > y - 2 && Main.mouseState.Y < y + 34)
                        {
                            Main.player[Main.myPlayer].mouseInterface = true;
                            if (Main.mouseState.LeftButton == ButtonState.Pressed && Main.mouseLeftRelease)
                            {
                                Main.PlaySound(12, -1, -1, 1);
                                Main.player[Main.myPlayer].hostile = !Main.player[Main.myPlayer].hostile && true;
                                NetMessage.SendData(30, -1, -1, "", Main.myPlayer, 0.0f, 0.0f, 0.0f);
                            }
                        }
                        int num2 = num1 - 3;
                        Rectangle rectangle2 = new Rectangle(Main.mouseState.X, Main.mouseState.Y, 1, 1);
                        int width = Main.teamTexture.Width;
                        int height = Main.teamTexture.Height;
                        for (int index = 0; index < 5; ++index)
                        {
                            Rectangle rectangle3 = new Rectangle();
                            if (index == 0)
                                rectangle3 = new Rectangle(num2 + 50, y - 20, width, height);
                            if (index == 1)
                                rectangle3 = new Rectangle(num2 + 40, y, width, height);
                            if (index == 2)
                                rectangle3 = new Rectangle(num2 + 60, y, width, height);
                            if (index == 3)
                                rectangle3 = new Rectangle(num2 + 40, y + 20, width, height);
                            if (index == 4)
                                rectangle3 = new Rectangle(num2 + 60, y + 20, width, height);
                            if (rectangle3.Intersects(rectangle2))
                            {
                                Main.player[Main.myPlayer].mouseInterface = true;
                                if (Main.mouseState.LeftButton == ButtonState.Pressed && Main.mouseLeftRelease && Main.player[Main.myPlayer].team != index)
                                {
                                    Main.PlaySound(12, -1, -1, 1);
                                    Main.player[Main.myPlayer].team = index;
                                    NetMessage.SendData(45, -1, -1, "", Main.myPlayer, 0.0f, 0.0f, 0.0f);
                                }
                            }
                        }
                        this.spriteBatch.Draw(Main.teamTexture, new Vector2((float)(num2 + 50), (float)(y - 20)), new Rectangle?(new Rectangle(0, 0, Main.teamTexture.Width, Main.teamTexture.Height)), Main.teamColor[0], 0.0f, new Vector2(), 1f, SpriteEffects.None, 0.0f);
                        this.spriteBatch.Draw(Main.teamTexture, new Vector2((float)(num2 + 40), (float)y), new Rectangle?(new Rectangle(0, 0, Main.teamTexture.Width, Main.teamTexture.Height)), Main.teamColor[1], 0.0f, new Vector2(), 1f, SpriteEffects.None, 0.0f);
                        this.spriteBatch.Draw(Main.teamTexture, new Vector2((float)(num2 + 60), (float)y), new Rectangle?(new Rectangle(0, 0, Main.teamTexture.Width, Main.teamTexture.Height)), Main.teamColor[2], 0.0f, new Vector2(), 1f, SpriteEffects.None, 0.0f);
                        this.spriteBatch.Draw(Main.teamTexture, new Vector2((float)(num2 + 40), (float)(y + 20)), new Rectangle?(new Rectangle(0, 0, Main.teamTexture.Width, Main.teamTexture.Height)), Main.teamColor[3], 0.0f, new Vector2(), 1f, SpriteEffects.None, 0.0f);
                        this.spriteBatch.Draw(Main.teamTexture, new Vector2((float)(num2 + 60), (float)(y + 20)), new Rectangle?(new Rectangle(0, 0, Main.teamTexture.Width, Main.teamTexture.Height)), Main.teamColor[4], 0.0f, new Vector2(), 1f, SpriteEffects.None, 0.0f);
                    }
                    string text1 = "Save & Exit";
                    if (Main.netMode != 0)
                        text1 = "Disconnect";
                    Vector2 vector2_1 = Main.fontDeathText.MeasureString(text1);
                    int num3 = Main.screenWidth - 110;
                    int num4 = Main.screenHeight - 20;
                    if (Main.mouseExit)
                    {
                        if ((double)Main.exitScale < 1.0)
                            Main.exitScale += 0.02f;
                    }
                    else if ((double)Main.exitScale > 0.8)
                        Main.exitScale -= 0.02f;
                    for (int index = 0; index < 5; ++index)
                    {
                        int num1 = 0;
                        int num2 = 0;
                        Color color2 = Color.Black;
                        if (index == 0)
                            num1 = -2;
                        if (index == 1)
                            num1 = 2;
                        if (index == 2)
                            num2 = -2;
                        if (index == 3)
                            num2 = 2;
                        if (index == 4)
                            color2 = Color.White;
                        this.spriteBatch.DrawString(Main.fontDeathText, text1, new Vector2((float)(num3 + num1), (float)(num4 + num2)), color2, 0.0f, new Vector2(vector2_1.X / 2f, vector2_1.Y / 2f), Main.exitScale - 0.2f, SpriteEffects.None, 0.0f);
                    }
                    if ((double)Main.mouseState.X > (double)num3 - (double)vector2_1.X / 2.0 && (double)Main.mouseState.X < (double)num3 + (double)vector2_1.X / 2.0 && (double)Main.mouseState.Y > (double)num4 - (double)vector2_1.Y / 2.0 && (double)Main.mouseState.Y < (double)num4 + (double)vector2_1.Y / 2.0 - 10.0)
                    {
                        if (!Main.mouseExit)
                            Main.PlaySound(12, -1, -1, 1);
                        Main.mouseExit = true;
                        Main.player[Main.myPlayer].mouseInterface = true;
                        if (Main.mouseLeftRelease && Main.mouseState.LeftButton == ButtonState.Pressed)
                        {
                            Main.menuMode = 10;
                            WorldGen.SaveAndQuit();
                        }
                    }
                    else
                        Main.mouseExit = false;
                    this.spriteBatch.DrawString(Main.fontMouseText, "Inventory", new Vector2(40f, 0.0f), new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), 0.0f, new Vector2(), 1f, SpriteEffects.None, 0.0f);
                    Main.inventoryScale = 0.85f;
                    Color color3;
                    for (int index1 = 0; index1 < 10; ++index1)
                    {
                        for (int index2 = 0; index2 < 4; ++index2)
                        {
                            int num1 = (int)(20.0 + (double)(index1 * 56) * (double)Main.inventoryScale);
                            int num2 = (int)(20.0 + (double)(index2 * 56) * (double)Main.inventoryScale);
                            int index3 = index1 + index2 * 10;
                            color3 = new Color(100, 100, 100, 100);
                            if (Main.mouseState.X >= num1 && (double)Main.mouseState.X <= (double)num1 + (double)Main.inventoryBackTexture.Width * (double)Main.inventoryScale && Main.mouseState.Y >= num2 && (double)Main.mouseState.Y <= (double)num2 + (double)Main.inventoryBackTexture.Height * (double)Main.inventoryScale)
                            {
                                Main.player[Main.myPlayer].mouseInterface = true;
                                if (Main.mouseLeftRelease && Main.mouseState.LeftButton == ButtonState.Pressed)
                                {
                                    if (Main.player[Main.myPlayer].selectedItem != index3 || Main.player[Main.myPlayer].itemAnimation <= 0)
                                    {
                                        Item obj = Main.mouseItem;
                                        Main.mouseItem = Main.player[Main.myPlayer].inventory[index3];
                                        Main.player[Main.myPlayer].inventory[index3] = obj;
                                        if (Main.player[Main.myPlayer].inventory[index3].type == 0 || Main.player[Main.myPlayer].inventory[index3].stack < 1)
                                            Main.player[Main.myPlayer].inventory[index3] = new Item();
                                        if (Main.mouseItem.IsTheSameAs(Main.player[Main.myPlayer].inventory[index3]) && (Main.player[Main.myPlayer].inventory[index3].stack != Main.player[Main.myPlayer].inventory[index3].maxStack && Main.mouseItem.stack != Main.mouseItem.maxStack))
                                        {
                                            if (Main.mouseItem.stack + Main.player[Main.myPlayer].inventory[index3].stack <= Main.mouseItem.maxStack)
                                            {
                                                Main.player[Main.myPlayer].inventory[index3].stack += Main.mouseItem.stack;
                                                Main.mouseItem.stack = 0;
                                            }
                                            else
                                            {
                                                int num5 = Main.mouseItem.maxStack - Main.player[Main.myPlayer].inventory[index3].stack;
                                                Main.player[Main.myPlayer].inventory[index3].stack += num5;
                                                Main.mouseItem.stack -= num5;
                                            }
                                        }
                                        if (Main.mouseItem.type == 0 || Main.mouseItem.stack < 1)
                                            Main.mouseItem = new Item();
                                        if (Main.mouseItem.type > 0 || Main.player[Main.myPlayer].inventory[index3].type > 0)
                                        {
                                            Recipe.FindRecipes();
                                            Main.PlaySound(7, -1, -1, 1);
                                        }
                                    }
                                }
                                else if (Main.stackSplit <= 1 && Main.mouseState.RightButton == ButtonState.Pressed && (Main.mouseItem.IsTheSameAs(Main.player[Main.myPlayer].inventory[index3]) || Main.mouseItem.type == 0) && (Main.mouseItem.stack < Main.mouseItem.maxStack || Main.mouseItem.type == 0))
                                {
                                    if (Main.mouseItem.type == 0)
                                    {
                                        Main.mouseItem = (Item)Main.player[Main.myPlayer].inventory[index3].Clone();
                                        Main.mouseItem.stack = 0;
                                    }
                                    ++Main.mouseItem.stack;
                                    --Main.player[Main.myPlayer].inventory[index3].stack;
                                    if (Main.player[Main.myPlayer].inventory[index3].stack <= 0)
                                        Main.player[Main.myPlayer].inventory[index3] = new Item();
                                    Recipe.FindRecipes();
                                    Main.soundInstanceMenuTick.Stop();
                                    Main.soundInstanceMenuTick = Main.soundMenuTick.CreateInstance();
                                    Main.PlaySound(12, -1, -1, 1);
                                    Main.stackSplit = Main.stackSplit != 0 ? Main.stackDelay : 15;
                                }
                                cursorText1 = Main.player[Main.myPlayer].inventory[index3].name;
                                Main.toolTip = (Item)Main.player[Main.myPlayer].inventory[index3].Clone();
                                if (Main.player[Main.myPlayer].inventory[index3].stack > 1)
                                    cursorText1 = string.Concat(new object[4]
                  {
                    (object) cursorText1,
                    (object) " (",
                    (object) Main.player[Main.myPlayer].inventory[index3].stack,
                    (object) ")"
                  });
                            }
                            this.spriteBatch.Draw(Main.inventoryBackTexture, new Vector2((float)num1, (float)num2), new Rectangle?(new Rectangle(0, 0, Main.inventoryBackTexture.Width, Main.inventoryBackTexture.Height)), color1, 0.0f, new Vector2(), Main.inventoryScale, SpriteEffects.None, 0.0f);
                            color3 = Color.White;
                            if (Main.player[Main.myPlayer].inventory[index3].type > 0 && Main.player[Main.myPlayer].inventory[index3].stack > 0)
                            {
                                float num5 = 1f;
                                if (Main.itemTexture[Main.player[Main.myPlayer].inventory[index3].type].Width > 32 || Main.itemTexture[Main.player[Main.myPlayer].inventory[index3].type].Height > 32)
                                    num5 = Main.itemTexture[Main.player[Main.myPlayer].inventory[index3].type].Width <= Main.itemTexture[Main.player[Main.myPlayer].inventory[index3].type].Height ? 32f / (float)Main.itemTexture[Main.player[Main.myPlayer].inventory[index3].type].Height : 32f / (float)Main.itemTexture[Main.player[Main.myPlayer].inventory[index3].type].Width;
                                float scale = num5 * Main.inventoryScale;
                                this.spriteBatch.Draw(Main.itemTexture[Main.player[Main.myPlayer].inventory[index3].type], new Vector2((float)((double)num1 + 26.0 * (double)Main.inventoryScale - (double)Main.itemTexture[Main.player[Main.myPlayer].inventory[index3].type].Width * 0.5 * (double)scale), (float)((double)num2 + 26.0 * (double)Main.inventoryScale - (double)Main.itemTexture[Main.player[Main.myPlayer].inventory[index3].type].Height * 0.5 * (double)scale)), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.player[Main.myPlayer].inventory[index3].type].Width, Main.itemTexture[Main.player[Main.myPlayer].inventory[index3].type].Height)), Main.player[Main.myPlayer].inventory[index3].GetAlpha(color3), 0.0f, new Vector2(), scale, SpriteEffects.None, 0.0f);
                                if (Main.player[Main.myPlayer].inventory[index3].color != new Color())
                                    this.spriteBatch.Draw(Main.itemTexture[Main.player[Main.myPlayer].inventory[index3].type], new Vector2((float)((double)num1 + 26.0 * (double)Main.inventoryScale - (double)Main.itemTexture[Main.player[Main.myPlayer].inventory[index3].type].Width * 0.5 * (double)scale), (float)((double)num2 + 26.0 * (double)Main.inventoryScale - (double)Main.itemTexture[Main.player[Main.myPlayer].inventory[index3].type].Height * 0.5 * (double)scale)), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.player[Main.myPlayer].inventory[index3].type].Width, Main.itemTexture[Main.player[Main.myPlayer].inventory[index3].type].Height)), Main.player[Main.myPlayer].inventory[index3].GetColor(color3), 0.0f, new Vector2(), scale, SpriteEffects.None, 0.0f);
                                if (Main.player[Main.myPlayer].inventory[index3].stack > 1)
                                    this.spriteBatch.DrawString(Main.fontItemStack, string.Concat((object)Main.player[Main.myPlayer].inventory[index3].stack), new Vector2((float)num1 + 10f * Main.inventoryScale, (float)num2 + 26f * Main.inventoryScale), color3, 0.0f, new Vector2(), scale, SpriteEffects.None, 0.0f);
                            }
                        }
                    }
                    this.spriteBatch.DrawString(Main.fontMouseText, "Equip", new Vector2((float)(Main.screenWidth - 64 - 28 + 4), 152f), new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), 0.0f, new Vector2(), 0.8f, SpriteEffects.None, 0.0f);
                    for (int index = 0; index < 8; ++index)
                    {
                        int num1 = Main.screenWidth - 64 - 28;
                        int num2 = (int)(174.0 + (double)(index * 56) * (double)Main.inventoryScale);
                        color3 = new Color(100, 100, 100, 100);
                        string text2 = "";
                        if (index == 3)
                            text2 = "Accessories";
                        Vector2 vector2_2 = Main.fontMouseText.MeasureString(text2);
                        this.spriteBatch.DrawString(Main.fontMouseText, text2, new Vector2((float)((double)num1 - (double)vector2_2.X - 10.0), (float)((double)num2 + (double)Main.inventoryBackTexture.Height * 0.5 - (double)vector2_2.Y * 0.5)), new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), 0.0f, new Vector2(), 1f, SpriteEffects.None, 0.0f);
                        if (Main.mouseState.X >= num1 && (double)Main.mouseState.X <= (double)num1 + (double)Main.inventoryBackTexture.Width * (double)Main.inventoryScale && Main.mouseState.Y >= num2 && (double)Main.mouseState.Y <= (double)num2 + (double)Main.inventoryBackTexture.Height * (double)Main.inventoryScale)
                        {
                            Main.player[Main.myPlayer].mouseInterface = true;
                            if (Main.mouseLeftRelease && Main.mouseState.LeftButton == ButtonState.Pressed && (Main.mouseItem.type == 0 || Main.mouseItem.headSlot > -1 && index == 0 || (Main.mouseItem.bodySlot > -1 && index == 1 || Main.mouseItem.legSlot > -1 && index == 2) || Main.mouseItem.accessory && index > 2))
                            {
                                Item obj = Main.mouseItem;
                                Main.mouseItem = Main.player[Main.myPlayer].armor[index];
                                Main.player[Main.myPlayer].armor[index] = obj;
                                if (Main.player[Main.myPlayer].armor[index].type == 0 || Main.player[Main.myPlayer].armor[index].stack < 1)
                                    Main.player[Main.myPlayer].armor[index] = new Item();
                                if (Main.mouseItem.type == 0 || Main.mouseItem.stack < 1)
                                    Main.mouseItem = new Item();
                                if (Main.mouseItem.type > 0 || Main.player[Main.myPlayer].armor[index].type > 0)
                                {
                                    Recipe.FindRecipes();
                                    Main.PlaySound(7, -1, -1, 1);
                                }
                            }
                            cursorText1 = Main.player[Main.myPlayer].armor[index].name;
                            Main.toolTip = (Item)Main.player[Main.myPlayer].armor[index].Clone();
                            if (index <= 2)
                                Main.toolTip.wornArmor = true;
                            if (Main.player[Main.myPlayer].armor[index].stack > 1)
                                cursorText1 = string.Concat(new object[4]
                {
                  (object) cursorText1,
                  (object) " (",
                  (object) Main.player[Main.myPlayer].armor[index].stack,
                  (object) ")"
                });
                        }
                        this.spriteBatch.Draw(Main.inventoryBackTexture, new Vector2((float)num1, (float)num2), new Rectangle?(new Rectangle(0, 0, Main.inventoryBackTexture.Width, Main.inventoryBackTexture.Height)), color1, 0.0f, new Vector2(), Main.inventoryScale, SpriteEffects.None, 0.0f);
                        color3 = Color.White;
                        if (Main.player[Main.myPlayer].armor[index].type > 0 && Main.player[Main.myPlayer].armor[index].stack > 0)
                        {
                            float num5 = 1f;
                            if (Main.itemTexture[Main.player[Main.myPlayer].armor[index].type].Width > 32 || Main.itemTexture[Main.player[Main.myPlayer].armor[index].type].Height > 32)
                                num5 = Main.itemTexture[Main.player[Main.myPlayer].armor[index].type].Width <= Main.itemTexture[Main.player[Main.myPlayer].armor[index].type].Height ? 32f / (float)Main.itemTexture[Main.player[Main.myPlayer].armor[index].type].Height : 32f / (float)Main.itemTexture[Main.player[Main.myPlayer].armor[index].type].Width;
                            float scale = num5 * Main.inventoryScale;
                            this.spriteBatch.Draw(Main.itemTexture[Main.player[Main.myPlayer].armor[index].type], new Vector2((float)((double)num1 + 26.0 * (double)Main.inventoryScale - (double)Main.itemTexture[Main.player[Main.myPlayer].armor[index].type].Width * 0.5 * (double)scale), (float)((double)num2 + 26.0 * (double)Main.inventoryScale - (double)Main.itemTexture[Main.player[Main.myPlayer].armor[index].type].Height * 0.5 * (double)scale)), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.player[Main.myPlayer].armor[index].type].Width, Main.itemTexture[Main.player[Main.myPlayer].armor[index].type].Height)), Main.player[Main.myPlayer].armor[index].GetAlpha(color3), 0.0f, new Vector2(), scale, SpriteEffects.None, 0.0f);
                            if (Main.player[Main.myPlayer].armor[index].color != new Color())
                                this.spriteBatch.Draw(Main.itemTexture[Main.player[Main.myPlayer].armor[index].type], new Vector2((float)((double)num1 + 26.0 * (double)Main.inventoryScale - (double)Main.itemTexture[Main.player[Main.myPlayer].armor[index].type].Width * 0.5 * (double)scale), (float)((double)num2 + 26.0 * (double)Main.inventoryScale - (double)Main.itemTexture[Main.player[Main.myPlayer].armor[index].type].Height * 0.5 * (double)scale)), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.player[Main.myPlayer].armor[index].type].Width, Main.itemTexture[Main.player[Main.myPlayer].armor[index].type].Height)), Main.player[Main.myPlayer].armor[index].GetColor(color3), 0.0f, new Vector2(), scale, SpriteEffects.None, 0.0f);
                            if (Main.player[Main.myPlayer].armor[index].stack > 1)
                                this.spriteBatch.DrawString(Main.fontItemStack, string.Concat((object)Main.player[Main.myPlayer].armor[index].stack), new Vector2((float)num1 + 10f * Main.inventoryScale, (float)num2 + 26f * Main.inventoryScale), color3, 0.0f, new Vector2(), scale, SpriteEffects.None, 0.0f);
                        }
                    }
                    this.spriteBatch.DrawString(Main.fontMouseText, "Social", new Vector2((float)(Main.screenWidth - 64 - 28 - 44), 152f), new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), 0.0f, new Vector2(), 0.8f, SpriteEffects.None, 0.0f);
                    for (int index = 8; index < 11; ++index)
                    {
                        int num1 = Main.screenWidth - 64 - 28 - 47;
                        int num2 = (int)(174.0 + (double)((index - 8) * 56) * (double)Main.inventoryScale);
                        color3 = new Color(100, 100, 100, 100);
                        string text2 = "";
                        if (index == 8)
                            text2 = "Helmet";
                        else if (index == 9)
                            text2 = "Shirt";
                        else if (index == 10)
                            text2 = "Pants";
                        Vector2 vector2_2 = Main.fontMouseText.MeasureString(text2);
                        this.spriteBatch.DrawString(Main.fontMouseText, text2, new Vector2((float)((double)num1 - (double)vector2_2.X - 10.0), (float)((double)num2 + (double)Main.inventoryBackTexture.Height * 0.5 - (double)vector2_2.Y * 0.5)), new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), 0.0f, new Vector2(), 1f, SpriteEffects.None, 0.0f);
                        if (Main.mouseState.X >= num1 && (double)Main.mouseState.X <= (double)num1 + (double)Main.inventoryBackTexture.Width * (double)Main.inventoryScale && Main.mouseState.Y >= num2 && (double)Main.mouseState.Y <= (double)num2 + (double)Main.inventoryBackTexture.Height * (double)Main.inventoryScale)
                        {
                            Main.player[Main.myPlayer].mouseInterface = true;
                            if (Main.mouseLeftRelease && Main.mouseState.LeftButton == ButtonState.Pressed && (Main.mouseItem.type == 0 || Main.mouseItem.headSlot > -1 && index == 8 || Main.mouseItem.bodySlot > -1 && index == 9 || Main.mouseItem.legSlot > -1 && index == 10))
                            {
                                Item obj = Main.mouseItem;
                                Main.mouseItem = Main.player[Main.myPlayer].armor[index];
                                Main.player[Main.myPlayer].armor[index] = obj;
                                if (Main.player[Main.myPlayer].armor[index].type == 0 || Main.player[Main.myPlayer].armor[index].stack < 1)
                                    Main.player[Main.myPlayer].armor[index] = new Item();
                                if (Main.mouseItem.type == 0 || Main.mouseItem.stack < 1)
                                    Main.mouseItem = new Item();
                                if (Main.mouseItem.type > 0 || Main.player[Main.myPlayer].armor[index].type > 0)
                                {
                                    Recipe.FindRecipes();
                                    Main.PlaySound(7, -1, -1, 1);
                                }
                            }
                            cursorText1 = Main.player[Main.myPlayer].armor[index].name;
                            Main.toolTip = (Item)Main.player[Main.myPlayer].armor[index].Clone();
                            Main.toolTip.social = true;
                            if (index <= 2)
                                Main.toolTip.wornArmor = true;
                            if (Main.player[Main.myPlayer].armor[index].stack > 1)
                                cursorText1 = string.Concat(new object[4]
                {
                  (object) cursorText1,
                  (object) " (",
                  (object) Main.player[Main.myPlayer].armor[index].stack,
                  (object) ")"
                });
                        }
                        this.spriteBatch.Draw(Main.inventoryBackTexture, new Vector2((float)num1, (float)num2), new Rectangle?(new Rectangle(0, 0, Main.inventoryBackTexture.Width, Main.inventoryBackTexture.Height)), color1, 0.0f, new Vector2(), Main.inventoryScale, SpriteEffects.None, 0.0f);
                        color3 = Color.White;
                        if (Main.player[Main.myPlayer].armor[index].type > 0 && Main.player[Main.myPlayer].armor[index].stack > 0)
                        {
                            float num5 = 1f;
                            if (Main.itemTexture[Main.player[Main.myPlayer].armor[index].type].Width > 32 || Main.itemTexture[Main.player[Main.myPlayer].armor[index].type].Height > 32)
                                num5 = Main.itemTexture[Main.player[Main.myPlayer].armor[index].type].Width <= Main.itemTexture[Main.player[Main.myPlayer].armor[index].type].Height ? 32f / (float)Main.itemTexture[Main.player[Main.myPlayer].armor[index].type].Height : 32f / (float)Main.itemTexture[Main.player[Main.myPlayer].armor[index].type].Width;
                            float scale = num5 * Main.inventoryScale;
                            this.spriteBatch.Draw(Main.itemTexture[Main.player[Main.myPlayer].armor[index].type], new Vector2((float)((double)num1 + 26.0 * (double)Main.inventoryScale - (double)Main.itemTexture[Main.player[Main.myPlayer].armor[index].type].Width * 0.5 * (double)scale), (float)((double)num2 + 26.0 * (double)Main.inventoryScale - (double)Main.itemTexture[Main.player[Main.myPlayer].armor[index].type].Height * 0.5 * (double)scale)), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.player[Main.myPlayer].armor[index].type].Width, Main.itemTexture[Main.player[Main.myPlayer].armor[index].type].Height)), Main.player[Main.myPlayer].armor[index].GetAlpha(color3), 0.0f, new Vector2(), scale, SpriteEffects.None, 0.0f);
                            if (Main.player[Main.myPlayer].armor[index].color != new Color())
                                this.spriteBatch.Draw(Main.itemTexture[Main.player[Main.myPlayer].armor[index].type], new Vector2((float)((double)num1 + 26.0 * (double)Main.inventoryScale - (double)Main.itemTexture[Main.player[Main.myPlayer].armor[index].type].Width * 0.5 * (double)scale), (float)((double)num2 + 26.0 * (double)Main.inventoryScale - (double)Main.itemTexture[Main.player[Main.myPlayer].armor[index].type].Height * 0.5 * (double)scale)), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.player[Main.myPlayer].armor[index].type].Width, Main.itemTexture[Main.player[Main.myPlayer].armor[index].type].Height)), Main.player[Main.myPlayer].armor[index].GetColor(color3), 0.0f, new Vector2(), scale, SpriteEffects.None, 0.0f);
                            if (Main.player[Main.myPlayer].armor[index].stack > 1)
                                this.spriteBatch.DrawString(Main.fontItemStack, string.Concat((object)Main.player[Main.myPlayer].armor[index].stack), new Vector2((float)num1 + 10f * Main.inventoryScale, (float)num2 + 26f * Main.inventoryScale), color3, 0.0f, new Vector2(), scale, SpriteEffects.None, 0.0f);
                        }
                    }
                    this.spriteBatch.DrawString(Main.fontMouseText, "Crafting", new Vector2(76f, 414f), new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), 0.0f, new Vector2(), 1f, SpriteEffects.None, 0.0f);
                    for (int index = 0; index < Recipe.maxRecipes; ++index)
                    {
                        Main.inventoryScale = (float)(100.0 / ((double)Math.Abs(Main.availableRecipeY[index]) + 100.0));
                        if ((double)Main.inventoryScale < 0.75)
                            Main.inventoryScale = 0.75f;
                        if ((double)Main.availableRecipeY[index] < (double)((index - Main.focusRecipe) * 65))
                        {
                            if ((double)Main.availableRecipeY[index] == 0.0)
                                Main.PlaySound(12, -1, -1, 1);
                            Main.availableRecipeY[index] += 6.5f;
                        }
                        else if ((double)Main.availableRecipeY[index] > (double)((index - Main.focusRecipe) * 65))
                        {
                            if ((double)Main.availableRecipeY[index] == 0.0)
                                Main.PlaySound(12, -1, -1, 1);
                            Main.availableRecipeY[index] -= 6.5f;
                        }
                        if (index < Main.numAvailableRecipes && (double)Math.Abs(Main.availableRecipeY[index]) <= 250.0)
                        {
                            int num1 = (int)(46.0 - 26.0 * (double)Main.inventoryScale);
                            int num2 = (int)(410.0 + (double)Main.availableRecipeY[index] * (double)Main.inventoryScale - 30.0 * (double)Main.inventoryScale);
                            double num5 = (double)((int)color1.A + 50);
                            double num6 = (double)byte.MaxValue;
                            if ((double)Math.Abs(Main.availableRecipeY[index]) > 150.0)
                            {
                                num5 = 150.0 * (100.0 - ((double)Math.Abs(Main.availableRecipeY[index]) - 150.0)) * 0.01;
                                num6 = (double)byte.MaxValue * (100.0 - ((double)Math.Abs(Main.availableRecipeY[index]) - 150.0)) * 0.01;
                            }
                            color3 = new Color((int)(byte)num5, (int)(byte)num5, (int)(byte)num5, (int)(byte)num5);
                            Color newColor = new Color((int)(byte)num6, (int)(byte)num6, (int)(byte)num6, (int)(byte)num6);
                            if (Main.mouseState.X >= num1 && (double)Main.mouseState.X <= (double)num1 + (double)Main.inventoryBackTexture.Width * (double)Main.inventoryScale && Main.mouseState.Y >= num2 && (double)Main.mouseState.Y <= (double)num2 + (double)Main.inventoryBackTexture.Height * (double)Main.inventoryScale)
                            {
                                Main.player[Main.myPlayer].mouseInterface = true;
                                if (Main.focusRecipe == index)
                                {
                                    if (Main.mouseItem.type == 0 || Main.mouseItem.IsTheSameAs(Main.recipe[Main.availableRecipe[index]].createItem) && Main.mouseItem.stack + Main.recipe[Main.availableRecipe[index]].createItem.stack <= Main.mouseItem.maxStack)
                                    {
                                        if (Main.mouseLeftRelease && Main.mouseState.LeftButton == ButtonState.Pressed)
                                        {
                                            int num7 = Main.mouseItem.stack;
                                            Main.mouseItem = (Item)Main.recipe[Main.availableRecipe[index]].createItem.Clone();
                                            Main.mouseItem.stack += num7;
                                            Main.recipe[Main.availableRecipe[index]].Create();
                                            if (Main.mouseItem.type > 0 || Main.recipe[Main.availableRecipe[index]].createItem.type > 0)
                                                Main.PlaySound(7, -1, -1, 1);
                                        }
                                        else if (Main.stackSplit <= 1 && Main.mouseState.RightButton == ButtonState.Pressed && (Main.mouseItem.stack < Main.mouseItem.maxStack || Main.mouseItem.type == 0))
                                        {
                                            Main.stackSplit = Main.stackSplit != 0 ? Main.stackDelay : 15;
                                            int num7 = Main.mouseItem.stack;
                                            Main.mouseItem = (Item)Main.recipe[Main.availableRecipe[index]].createItem.Clone();
                                            Main.mouseItem.stack += num7;
                                            Main.recipe[Main.availableRecipe[index]].Create();
                                            if (Main.mouseItem.type > 0 || Main.recipe[Main.availableRecipe[index]].createItem.type > 0)
                                                Main.PlaySound(7, -1, -1, 1);
                                        }
                                    }
                                }
                                else if (Main.mouseLeftRelease && Main.mouseState.LeftButton == ButtonState.Pressed)
                                    Main.focusRecipe = index;
                                cursorText1 = Main.recipe[Main.availableRecipe[index]].createItem.name;
                                Main.toolTip = (Item)Main.recipe[Main.availableRecipe[index]].createItem.Clone();
                                if (Main.recipe[Main.availableRecipe[index]].createItem.stack > 1)
                                    cursorText1 = string.Concat(new object[4]
                  {
                    (object) cursorText1,
                    (object) " (",
                    (object) Main.recipe[Main.availableRecipe[index]].createItem.stack,
                    (object) ")"
                  });
                            }
                            if (Main.numAvailableRecipes > 0)
                            {
                                double num7 = num5 - 50.0;
                                if (num7 < 0.0)
                                    num7 = 0.0;
                                this.spriteBatch.Draw(Main.inventoryBackTexture, new Vector2((float)num1, (float)num2), new Rectangle?(new Rectangle(0, 0, Main.inventoryBackTexture.Width, Main.inventoryBackTexture.Height)), new Color((int)(byte)num7, (int)(byte)num7, (int)(byte)num7, (int)(byte)num7), 0.0f, new Vector2(), Main.inventoryScale, SpriteEffects.None, 0.0f);
                                if (Main.recipe[Main.availableRecipe[index]].createItem.type > 0 && Main.recipe[Main.availableRecipe[index]].createItem.stack > 0)
                                {
                                    float num8 = 1f;
                                    if (Main.itemTexture[Main.recipe[Main.availableRecipe[index]].createItem.type].Width > 32 || Main.itemTexture[Main.recipe[Main.availableRecipe[index]].createItem.type].Height > 32)
                                        num8 = Main.itemTexture[Main.recipe[Main.availableRecipe[index]].createItem.type].Width <= Main.itemTexture[Main.recipe[Main.availableRecipe[index]].createItem.type].Height ? 32f / (float)Main.itemTexture[Main.recipe[Main.availableRecipe[index]].createItem.type].Height : 32f / (float)Main.itemTexture[Main.recipe[Main.availableRecipe[index]].createItem.type].Width;
                                    float scale = num8 * Main.inventoryScale;
                                    this.spriteBatch.Draw(Main.itemTexture[Main.recipe[Main.availableRecipe[index]].createItem.type], new Vector2((float)((double)num1 + 26.0 * (double)Main.inventoryScale - (double)Main.itemTexture[Main.recipe[Main.availableRecipe[index]].createItem.type].Width * 0.5 * (double)scale), (float)((double)num2 + 26.0 * (double)Main.inventoryScale - (double)Main.itemTexture[Main.recipe[Main.availableRecipe[index]].createItem.type].Height * 0.5 * (double)scale)), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.recipe[Main.availableRecipe[index]].createItem.type].Width, Main.itemTexture[Main.recipe[Main.availableRecipe[index]].createItem.type].Height)), Main.recipe[Main.availableRecipe[index]].createItem.GetAlpha(newColor), 0.0f, new Vector2(), scale, SpriteEffects.None, 0.0f);
                                    if (Main.recipe[Main.availableRecipe[index]].createItem.color != new Color())
                                        this.spriteBatch.Draw(Main.itemTexture[Main.recipe[Main.availableRecipe[index]].createItem.type], new Vector2((float)((double)num1 + 26.0 * (double)Main.inventoryScale - (double)Main.itemTexture[Main.recipe[Main.availableRecipe[index]].createItem.type].Width * 0.5 * (double)scale), (float)((double)num2 + 26.0 * (double)Main.inventoryScale - (double)Main.itemTexture[Main.recipe[Main.availableRecipe[index]].createItem.type].Height * 0.5 * (double)scale)), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.recipe[Main.availableRecipe[index]].createItem.type].Width, Main.itemTexture[Main.recipe[Main.availableRecipe[index]].createItem.type].Height)), Main.recipe[Main.availableRecipe[index]].createItem.GetColor(newColor), 0.0f, new Vector2(), scale, SpriteEffects.None, 0.0f);
                                    if (Main.recipe[Main.availableRecipe[index]].createItem.stack > 1)
                                        this.spriteBatch.DrawString(Main.fontItemStack, string.Concat((object)Main.recipe[Main.availableRecipe[index]].createItem.stack), new Vector2((float)num1 + 10f * Main.inventoryScale, (float)num2 + 26f * Main.inventoryScale), color3, 0.0f, new Vector2(), scale, SpriteEffects.None, 0.0f);
                                }
                            }
                        }
                    }
                    if (Main.numAvailableRecipes > 0)
                    {
                        for (int index = 0; index < Recipe.maxRequirements && Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[index].type != 0; ++index)
                        {
                            int num1 = 80 + index * 40;
                            int num2 = 380;
                            double num5 = (double)((int)color1.A + 50);
                            color3 = Color.White;
                            Color white = Color.White;
                            double num6 = (double)((int)color1.A + 50) - (double)Math.Abs(Main.availableRecipeY[Main.focusRecipe]) * 2.0;
                            double num7 = (double)byte.MaxValue - (double)Math.Abs(Main.availableRecipeY[Main.focusRecipe]) * 2.0;
                            if (num6 < 0.0)
                                num6 = 0.0;
                            if (num7 < 0.0)
                                num7 = 0.0;
                            color3.R = (byte)num6;
                            color3.G = (byte)num6;
                            color3.B = (byte)num6;
                            color3.A = (byte)num6;
                            white.R = (byte)num7;
                            white.G = (byte)num7;
                            white.B = (byte)num7;
                            white.A = (byte)num7;
                            Main.inventoryScale = 0.6f;
                            if (num6 != 0.0)
                            {
                                if (Main.mouseState.X >= num1 && (double)Main.mouseState.X <= (double)num1 + (double)Main.inventoryBackTexture.Width * (double)Main.inventoryScale && Main.mouseState.Y >= num2 && (double)Main.mouseState.Y <= (double)num2 + (double)Main.inventoryBackTexture.Height * (double)Main.inventoryScale)
                                {
                                    Main.player[Main.myPlayer].mouseInterface = true;
                                    cursorText1 = Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[index].name;
                                    Main.toolTip = (Item)Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[index].Clone();
                                    if (Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[index].stack > 1)
                                        cursorText1 = string.Concat(new object[4]
                    {
                      (object) cursorText1,
                      (object) " (",
                      (object) Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[index].stack,
                      (object) ")"
                    });
                                }
                                double num8 = num6 - 50.0;
                                if (num8 < 0.0)
                                    num8 = 0.0;
                                this.spriteBatch.Draw(Main.inventoryBackTexture, new Vector2((float)num1, (float)num2), new Rectangle?(new Rectangle(0, 0, Main.inventoryBackTexture.Width, Main.inventoryBackTexture.Height)), new Color((int)(byte)num8, (int)(byte)num8, (int)(byte)num8, (int)(byte)num8), 0.0f, new Vector2(), Main.inventoryScale, SpriteEffects.None, 0.0f);
                                if (Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[index].type > 0 && Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[index].stack > 0)
                                {
                                    float num9 = 1f;
                                    if (Main.itemTexture[Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[index].type].Width > 32 || Main.itemTexture[Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[index].type].Height > 32)
                                        num9 = Main.itemTexture[Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[index].type].Width <= Main.itemTexture[Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[index].type].Height ? 32f / (float)Main.itemTexture[Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[index].type].Height : 32f / (float)Main.itemTexture[Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[index].type].Width;
                                    float scale = num9 * Main.inventoryScale;
                                    this.spriteBatch.Draw(Main.itemTexture[Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[index].type], new Vector2((float)((double)num1 + 26.0 * (double)Main.inventoryScale - (double)Main.itemTexture[Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[index].type].Width * 0.5 * (double)scale), (float)((double)num2 + 26.0 * (double)Main.inventoryScale - (double)Main.itemTexture[Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[index].type].Height * 0.5 * (double)scale)), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[index].type].Width, Main.itemTexture[Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[index].type].Height)), Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[index].GetAlpha(white), 0.0f, new Vector2(), scale, SpriteEffects.None, 0.0f);
                                    if (Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[index].color != new Color())
                                        this.spriteBatch.Draw(Main.itemTexture[Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[index].type], new Vector2((float)((double)num1 + 26.0 * (double)Main.inventoryScale - (double)Main.itemTexture[Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[index].type].Width * 0.5 * (double)scale), (float)((double)num2 + 26.0 * (double)Main.inventoryScale - (double)Main.itemTexture[Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[index].type].Height * 0.5 * (double)scale)), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[index].type].Width, Main.itemTexture[Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[index].type].Height)), Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[index].GetColor(white), 0.0f, new Vector2(), scale, SpriteEffects.None, 0.0f);
                                    if (Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[index].stack > 1)
                                        this.spriteBatch.DrawString(Main.fontItemStack, string.Concat((object)Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[index].stack), new Vector2((float)num1 + 10f * Main.inventoryScale, (float)num2 + 26f * Main.inventoryScale), color3, 0.0f, new Vector2(), scale, SpriteEffects.None, 0.0f);
                                }
                            }
                            else
                                break;
                        }
                    }
                    this.spriteBatch.DrawString(Main.fontMouseText, "Coins", new Vector2(528f, 84f), new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), 0.0f, new Vector2(), 0.8f, SpriteEffects.None, 0.0f);
                    Main.inventoryScale = 0.55f;
                    for (int index1 = 0; index1 < 4; ++index1)
                    {
                        int num1 = 497;
                        int num2 = (int)(85.0 + (double)(index1 * 56) * (double)Main.inventoryScale);
                        int index2 = index1 + 40;
                        color3 = new Color(100, 100, 100, 100);
                        if (Main.mouseState.X >= num1 && (double)Main.mouseState.X <= (double)num1 + (double)Main.inventoryBackTexture.Width * (double)Main.inventoryScale && Main.mouseState.Y >= num2 && (double)Main.mouseState.Y <= (double)num2 + (double)Main.inventoryBackTexture.Height * (double)Main.inventoryScale)
                        {
                            Main.player[Main.myPlayer].mouseInterface = true;
                            if (Main.mouseLeftRelease && Main.mouseState.LeftButton == ButtonState.Pressed)
                            {
                                if ((Main.player[Main.myPlayer].selectedItem != index2 || Main.player[Main.myPlayer].itemAnimation <= 0) && (Main.mouseItem.type == 0 || Main.mouseItem.type == 71 || (Main.mouseItem.type == 72 || Main.mouseItem.type == 73) || Main.mouseItem.type == 74))
                                {
                                    Item obj = Main.mouseItem;
                                    Main.mouseItem = Main.player[Main.myPlayer].inventory[index2];
                                    Main.player[Main.myPlayer].inventory[index2] = obj;
                                    if (Main.player[Main.myPlayer].inventory[index2].type == 0 || Main.player[Main.myPlayer].inventory[index2].stack < 1)
                                        Main.player[Main.myPlayer].inventory[index2] = new Item();
                                    if (Main.mouseItem.IsTheSameAs(Main.player[Main.myPlayer].inventory[index2]) && (Main.player[Main.myPlayer].inventory[index2].stack != Main.player[Main.myPlayer].inventory[index2].maxStack && Main.mouseItem.stack != Main.mouseItem.maxStack))
                                    {
                                        if (Main.mouseItem.stack + Main.player[Main.myPlayer].inventory[index2].stack <= Main.mouseItem.maxStack)
                                        {
                                            Main.player[Main.myPlayer].inventory[index2].stack += Main.mouseItem.stack;
                                            Main.mouseItem.stack = 0;
                                        }
                                        else
                                        {
                                            int num5 = Main.mouseItem.maxStack - Main.player[Main.myPlayer].inventory[index2].stack;
                                            Main.player[Main.myPlayer].inventory[index2].stack += num5;
                                            Main.mouseItem.stack -= num5;
                                        }
                                    }
                                    if (Main.mouseItem.type == 0 || Main.mouseItem.stack < 1)
                                        Main.mouseItem = new Item();
                                    if (Main.mouseItem.type > 0 || Main.player[Main.myPlayer].inventory[index2].type > 0)
                                        Main.PlaySound(7, -1, -1, 1);
                                }
                            }
                            else if (Main.stackSplit <= 1 && Main.mouseState.RightButton == ButtonState.Pressed && (Main.mouseItem.IsTheSameAs(Main.player[Main.myPlayer].inventory[index2]) || Main.mouseItem.type == 0) && (Main.mouseItem.stack < Main.mouseItem.maxStack || Main.mouseItem.type == 0))
                            {
                                if (Main.mouseItem.type == 0)
                                {
                                    Main.mouseItem = (Item)Main.player[Main.myPlayer].inventory[index2].Clone();
                                    Main.mouseItem.stack = 0;
                                }
                                ++Main.mouseItem.stack;
                                --Main.player[Main.myPlayer].inventory[index2].stack;
                                if (Main.player[Main.myPlayer].inventory[index2].stack <= 0)
                                    Main.player[Main.myPlayer].inventory[index2] = new Item();
                                Recipe.FindRecipes();
                                Main.soundInstanceMenuTick.Stop();
                                Main.soundInstanceMenuTick = Main.soundMenuTick.CreateInstance();
                                Main.PlaySound(12, -1, -1, 1);
                                Main.stackSplit = Main.stackSplit != 0 ? Main.stackDelay : 15;
                            }
                            cursorText1 = Main.player[Main.myPlayer].inventory[index2].name;
                            Main.toolTip = (Item)Main.player[Main.myPlayer].inventory[index2].Clone();
                            if (Main.player[Main.myPlayer].inventory[index2].stack > 1)
                                cursorText1 = string.Concat(new object[4]
                {
                  (object) cursorText1,
                  (object) " (",
                  (object) Main.player[Main.myPlayer].inventory[index2].stack,
                  (object) ")"
                });
                        }
                        this.spriteBatch.Draw(Main.inventoryBackTexture, new Vector2((float)num1, (float)num2), new Rectangle?(new Rectangle(0, 0, Main.inventoryBackTexture.Width, Main.inventoryBackTexture.Height)), color1, 0.0f, new Vector2(), Main.inventoryScale, SpriteEffects.None, 0.0f);
                        color3 = Color.White;
                        if (Main.player[Main.myPlayer].inventory[index2].type > 0 && Main.player[Main.myPlayer].inventory[index2].stack > 0)
                        {
                            float num5 = 1f;
                            if (Main.itemTexture[Main.player[Main.myPlayer].inventory[index2].type].Width > 32 || Main.itemTexture[Main.player[Main.myPlayer].inventory[index2].type].Height > 32)
                                num5 = Main.itemTexture[Main.player[Main.myPlayer].inventory[index2].type].Width <= Main.itemTexture[Main.player[Main.myPlayer].inventory[index2].type].Height ? 32f / (float)Main.itemTexture[Main.player[Main.myPlayer].inventory[index2].type].Height : 32f / (float)Main.itemTexture[Main.player[Main.myPlayer].inventory[index2].type].Width;
                            float scale = num5 * Main.inventoryScale;
                            this.spriteBatch.Draw(Main.itemTexture[Main.player[Main.myPlayer].inventory[index2].type], new Vector2((float)((double)num1 + 26.0 * (double)Main.inventoryScale - (double)Main.itemTexture[Main.player[Main.myPlayer].inventory[index2].type].Width * 0.5 * (double)scale), (float)((double)num2 + 26.0 * (double)Main.inventoryScale - (double)Main.itemTexture[Main.player[Main.myPlayer].inventory[index2].type].Height * 0.5 * (double)scale)), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.player[Main.myPlayer].inventory[index2].type].Width, Main.itemTexture[Main.player[Main.myPlayer].inventory[index2].type].Height)), Main.player[Main.myPlayer].inventory[index2].GetAlpha(color3), 0.0f, new Vector2(), scale, SpriteEffects.None, 0.0f);
                            if (Main.player[Main.myPlayer].inventory[index2].color != new Color())
                                this.spriteBatch.Draw(Main.itemTexture[Main.player[Main.myPlayer].inventory[index2].type], new Vector2((float)((double)num1 + 26.0 * (double)Main.inventoryScale - (double)Main.itemTexture[Main.player[Main.myPlayer].inventory[index2].type].Width * 0.5 * (double)scale), (float)((double)num2 + 26.0 * (double)Main.inventoryScale - (double)Main.itemTexture[Main.player[Main.myPlayer].inventory[index2].type].Height * 0.5 * (double)scale)), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.player[Main.myPlayer].inventory[index2].type].Width, Main.itemTexture[Main.player[Main.myPlayer].inventory[index2].type].Height)), Main.player[Main.myPlayer].inventory[index2].GetColor(color3), 0.0f, new Vector2(), scale, SpriteEffects.None, 0.0f);
                            if (Main.player[Main.myPlayer].inventory[index2].stack > 1)
                                this.spriteBatch.DrawString(Main.fontItemStack, string.Concat((object)Main.player[Main.myPlayer].inventory[index2].stack), new Vector2((float)num1 + 10f * Main.inventoryScale, (float)num2 + 26f * Main.inventoryScale), color3, 0.0f, new Vector2(), scale, SpriteEffects.None, 0.0f);
                        }
                    }
                    if (Main.npcShop > 0 && (!Main.playerInventory || Main.player[Main.myPlayer].talkNPC == -1))
                        Main.npcShop = 0;
                    if (Main.npcShop > 0)
                    {
                        this.spriteBatch.DrawString(Main.fontMouseText, "Shop", new Vector2(284f, 210f), new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), 0.0f, new Vector2(), 1f, SpriteEffects.None, 0.0f);
                        Main.inventoryScale = 0.75f;
                        for (int index1 = 0; index1 < 5; ++index1)
                        {
                            for (int index2 = 0; index2 < 4; ++index2)
                            {
                                int num1 = (int)(73.0 + (double)(index1 * 56) * (double)Main.inventoryScale);
                                int num2 = (int)(210.0 + (double)(index2 * 56) * (double)Main.inventoryScale);
                                int index3 = index1 + index2 * 5;
                                color3 = new Color(100, 100, 100, 100);
                                if (Main.mouseState.X >= num1 && (double)Main.mouseState.X <= (double)num1 + (double)Main.inventoryBackTexture.Width * (double)Main.inventoryScale && Main.mouseState.Y >= num2 && (double)Main.mouseState.Y <= (double)num2 + (double)Main.inventoryBackTexture.Height * (double)Main.inventoryScale)
                                {
                                    Main.player[Main.myPlayer].mouseInterface = true;
                                    if (Main.mouseLeftRelease && Main.mouseState.LeftButton == ButtonState.Pressed)
                                    {
                                        if (Main.mouseItem.type == 0)
                                        {
                                            if ((Main.player[Main.myPlayer].selectedItem != index3 || Main.player[Main.myPlayer].itemAnimation <= 0) && Main.player[Main.myPlayer].BuyItem(this.shop[Main.npcShop].item[index3].value))
                                            {
                                                Main.mouseItem.SetDefaults(this.shop[Main.npcShop].item[index3].name);
                                                Main.PlaySound(18, -1, -1, 1);
                                            }
                                        }
                                        else if (this.shop[Main.npcShop].item[index3].type == 0)
                                        {
                                            if (Main.player[Main.myPlayer].SellItem(Main.mouseItem.value * Main.mouseItem.stack))
                                            {
                                                Main.mouseItem.stack = 0;
                                                Main.mouseItem.type = 0;
                                                Main.PlaySound(18, -1, -1, 1);
                                            }
                                            else if (Main.mouseItem.value == 0)
                                            {
                                                Main.mouseItem.stack = 0;
                                                Main.mouseItem.type = 0;
                                                Main.PlaySound(7, -1, -1, 1);
                                            }
                                        }
                                    }
                                    else if (Main.stackSplit <= 1 && Main.mouseState.RightButton == ButtonState.Pressed && (Main.mouseItem.IsTheSameAs(this.shop[Main.npcShop].item[index3]) || Main.mouseItem.type == 0) && ((Main.mouseItem.stack < Main.mouseItem.maxStack || Main.mouseItem.type == 0) && Main.player[Main.myPlayer].BuyItem(this.shop[Main.npcShop].item[index3].value)))
                                    {
                                        Main.PlaySound(18, -1, -1, 1);
                                        if (Main.mouseItem.type == 0)
                                        {
                                            Main.mouseItem = (Item)this.shop[Main.npcShop].item[index3].Clone();
                                            Main.mouseItem.stack = 0;
                                        }
                                        ++Main.mouseItem.stack;
                                        Main.stackSplit = Main.stackSplit != 0 ? Main.stackDelay : 15;
                                    }
                                    cursorText1 = this.shop[Main.npcShop].item[index3].name;
                                    Main.toolTip = (Item)this.shop[Main.npcShop].item[index3].Clone();
                                    Main.toolTip.buy = true;
                                    if (this.shop[Main.npcShop].item[index3].stack > 1)
                                        cursorText1 = string.Concat(new object[4]
                    {
                      (object) cursorText1,
                      (object) " (",
                      (object) this.shop[Main.npcShop].item[index3].stack,
                      (object) ")"
                    });
                                }
                                this.spriteBatch.Draw(Main.inventoryBackTexture, new Vector2((float)num1, (float)num2), new Rectangle?(new Rectangle(0, 0, Main.inventoryBackTexture.Width, Main.inventoryBackTexture.Height)), color1, 0.0f, new Vector2(), Main.inventoryScale, SpriteEffects.None, 0.0f);
                                color3 = Color.White;
                                if (this.shop[Main.npcShop].item[index3].type > 0 && this.shop[Main.npcShop].item[index3].stack > 0)
                                {
                                    float num5 = 1f;
                                    if (Main.itemTexture[this.shop[Main.npcShop].item[index3].type].Width > 32 || Main.itemTexture[this.shop[Main.npcShop].item[index3].type].Height > 32)
                                        num5 = Main.itemTexture[this.shop[Main.npcShop].item[index3].type].Width <= Main.itemTexture[this.shop[Main.npcShop].item[index3].type].Height ? 32f / (float)Main.itemTexture[this.shop[Main.npcShop].item[index3].type].Height : 32f / (float)Main.itemTexture[this.shop[Main.npcShop].item[index3].type].Width;
                                    float scale = num5 * Main.inventoryScale;
                                    this.spriteBatch.Draw(Main.itemTexture[this.shop[Main.npcShop].item[index3].type], new Vector2((float)((double)num1 + 26.0 * (double)Main.inventoryScale - (double)Main.itemTexture[this.shop[Main.npcShop].item[index3].type].Width * 0.5 * (double)scale), (float)((double)num2 + 26.0 * (double)Main.inventoryScale - (double)Main.itemTexture[this.shop[Main.npcShop].item[index3].type].Height * 0.5 * (double)scale)), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[this.shop[Main.npcShop].item[index3].type].Width, Main.itemTexture[this.shop[Main.npcShop].item[index3].type].Height)), this.shop[Main.npcShop].item[index3].GetAlpha(color3), 0.0f, new Vector2(), scale, SpriteEffects.None, 0.0f);
                                    if (this.shop[Main.npcShop].item[index3].color != new Color())
                                        this.spriteBatch.Draw(Main.itemTexture[this.shop[Main.npcShop].item[index3].type], new Vector2((float)((double)num1 + 26.0 * (double)Main.inventoryScale - (double)Main.itemTexture[this.shop[Main.npcShop].item[index3].type].Width * 0.5 * (double)scale), (float)((double)num2 + 26.0 * (double)Main.inventoryScale - (double)Main.itemTexture[this.shop[Main.npcShop].item[index3].type].Height * 0.5 * (double)scale)), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[this.shop[Main.npcShop].item[index3].type].Width, Main.itemTexture[this.shop[Main.npcShop].item[index3].type].Height)), this.shop[Main.npcShop].item[index3].GetColor(color3), 0.0f, new Vector2(), scale, SpriteEffects.None, 0.0f);
                                    if (this.shop[Main.npcShop].item[index3].stack > 1)
                                        this.spriteBatch.DrawString(Main.fontItemStack, string.Concat((object)this.shop[Main.npcShop].item[index3].stack), new Vector2((float)num1 + 10f * Main.inventoryScale, (float)num2 + 26f * Main.inventoryScale), color3, 0.0f, new Vector2(), scale, SpriteEffects.None, 0.0f);
                                }
                            }
                        }
                    }
                    if (Main.player[Main.myPlayer].chest > -1 && (int)Main.tile[Main.player[Main.myPlayer].chestX, Main.player[Main.myPlayer].chestY].type != 21)
                        Main.player[Main.myPlayer].chest = -1;
                    if (Main.player[Main.myPlayer].chest > -1)
                    {
                        this.spriteBatch.DrawString(Main.fontMouseText, "Chest", new Vector2(284f, 210f), new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), 0.0f, new Vector2(), 1f, SpriteEffects.None, 0.0f);
                        Main.inventoryScale = 0.75f;
                        for (int index1 = 0; index1 < 5; ++index1)
                        {
                            for (int index2 = 0; index2 < 4; ++index2)
                            {
                                int num1 = (int)(73.0 + (double)(index1 * 56) * (double)Main.inventoryScale);
                                int num2 = (int)(210.0 + (double)(index2 * 56) * (double)Main.inventoryScale);
                                int index3 = index1 + index2 * 5;
                                color3 = new Color(100, 100, 100, 100);
                                if (Main.mouseState.X >= num1 && (double)Main.mouseState.X <= (double)num1 + (double)Main.inventoryBackTexture.Width * (double)Main.inventoryScale && Main.mouseState.Y >= num2 && (double)Main.mouseState.Y <= (double)num2 + (double)Main.inventoryBackTexture.Height * (double)Main.inventoryScale)
                                {
                                    Main.player[Main.myPlayer].mouseInterface = true;
                                    if (Main.mouseLeftRelease && Main.mouseState.LeftButton == ButtonState.Pressed)
                                    {
                                        if (Main.player[Main.myPlayer].selectedItem != index3 || Main.player[Main.myPlayer].itemAnimation <= 0)
                                        {
                                            Item obj = Main.mouseItem;
                                            Main.mouseItem = Main.chest[Main.player[Main.myPlayer].chest].item[index3];
                                            Main.chest[Main.player[Main.myPlayer].chest].item[index3] = obj;
                                            if (Main.chest[Main.player[Main.myPlayer].chest].item[index3].type == 0 || Main.chest[Main.player[Main.myPlayer].chest].item[index3].stack < 1)
                                                Main.chest[Main.player[Main.myPlayer].chest].item[index3] = new Item();
                                            if (Main.mouseItem.IsTheSameAs(Main.chest[Main.player[Main.myPlayer].chest].item[index3]) && (Main.chest[Main.player[Main.myPlayer].chest].item[index3].stack != Main.chest[Main.player[Main.myPlayer].chest].item[index3].maxStack && Main.mouseItem.stack != Main.mouseItem.maxStack))
                                            {
                                                if (Main.mouseItem.stack + Main.chest[Main.player[Main.myPlayer].chest].item[index3].stack <= Main.mouseItem.maxStack)
                                                {
                                                    Main.chest[Main.player[Main.myPlayer].chest].item[index3].stack += Main.mouseItem.stack;
                                                    Main.mouseItem.stack = 0;
                                                }
                                                else
                                                {
                                                    int num5 = Main.mouseItem.maxStack - Main.chest[Main.player[Main.myPlayer].chest].item[index3].stack;
                                                    Main.chest[Main.player[Main.myPlayer].chest].item[index3].stack += num5;
                                                    Main.mouseItem.stack -= num5;
                                                }
                                            }
                                            if (Main.mouseItem.type == 0 || Main.mouseItem.stack < 1)
                                                Main.mouseItem = new Item();
                                            if (Main.mouseItem.type > 0 || Main.chest[Main.player[Main.myPlayer].chest].item[index3].type > 0)
                                            {
                                                Recipe.FindRecipes();
                                                Main.PlaySound(7, -1, -1, 1);
                                            }
                                            if (Main.netMode == 1)
                                                NetMessage.SendData(32, -1, -1, "", Main.player[Main.myPlayer].chest, (float)index3, 0.0f, 0.0f);
                                        }
                                    }
                                    else if (Main.stackSplit <= 1 && Main.mouseState.RightButton == ButtonState.Pressed && (Main.mouseItem.IsTheSameAs(Main.chest[Main.player[Main.myPlayer].chest].item[index3]) || Main.mouseItem.type == 0) && (Main.mouseItem.stack < Main.mouseItem.maxStack || Main.mouseItem.type == 0))
                                    {
                                        if (Main.mouseItem.type == 0)
                                        {
                                            Main.mouseItem = (Item)Main.chest[Main.player[Main.myPlayer].chest].item[index3].Clone();
                                            Main.mouseItem.stack = 0;
                                        }
                                        ++Main.mouseItem.stack;
                                        --Main.chest[Main.player[Main.myPlayer].chest].item[index3].stack;
                                        if (Main.chest[Main.player[Main.myPlayer].chest].item[index3].stack <= 0)
                                            Main.chest[Main.player[Main.myPlayer].chest].item[index3] = new Item();
                                        Recipe.FindRecipes();
                                        Main.soundInstanceMenuTick.Stop();
                                        Main.soundInstanceMenuTick = Main.soundMenuTick.CreateInstance();
                                        Main.PlaySound(12, -1, -1, 1);
                                        Main.stackSplit = Main.stackSplit != 0 ? Main.stackDelay : 15;
                                        if (Main.netMode == 1)
                                            NetMessage.SendData(32, -1, -1, "", Main.player[Main.myPlayer].chest, (float)index3, 0.0f, 0.0f);
                                    }
                                    cursorText1 = Main.chest[Main.player[Main.myPlayer].chest].item[index3].name;
                                    Main.toolTip = (Item)Main.chest[Main.player[Main.myPlayer].chest].item[index3].Clone();
                                    if (Main.chest[Main.player[Main.myPlayer].chest].item[index3].stack > 1)
                                        cursorText1 = string.Concat(new object[4]
                    {
                      (object) cursorText1,
                      (object) " (",
                      (object) Main.chest[Main.player[Main.myPlayer].chest].item[index3].stack,
                      (object) ")"
                    });
                                }
                                this.spriteBatch.Draw(Main.inventoryBackTexture, new Vector2((float)num1, (float)num2), new Rectangle?(new Rectangle(0, 0, Main.inventoryBackTexture.Width, Main.inventoryBackTexture.Height)), color1, 0.0f, new Vector2(), Main.inventoryScale, SpriteEffects.None, 0.0f);
                                color3 = Color.White;
                                if (Main.chest[Main.player[Main.myPlayer].chest].item[index3].type > 0 && Main.chest[Main.player[Main.myPlayer].chest].item[index3].stack > 0)
                                {
                                    float num5 = 1f;
                                    if (Main.itemTexture[Main.chest[Main.player[Main.myPlayer].chest].item[index3].type].Width > 32 || Main.itemTexture[Main.chest[Main.player[Main.myPlayer].chest].item[index3].type].Height > 32)
                                        num5 = Main.itemTexture[Main.chest[Main.player[Main.myPlayer].chest].item[index3].type].Width <= Main.itemTexture[Main.chest[Main.player[Main.myPlayer].chest].item[index3].type].Height ? 32f / (float)Main.itemTexture[Main.chest[Main.player[Main.myPlayer].chest].item[index3].type].Height : 32f / (float)Main.itemTexture[Main.chest[Main.player[Main.myPlayer].chest].item[index3].type].Width;
                                    float scale = num5 * Main.inventoryScale;
                                    this.spriteBatch.Draw(Main.itemTexture[Main.chest[Main.player[Main.myPlayer].chest].item[index3].type], new Vector2((float)((double)num1 + 26.0 * (double)Main.inventoryScale - (double)Main.itemTexture[Main.chest[Main.player[Main.myPlayer].chest].item[index3].type].Width * 0.5 * (double)scale), (float)((double)num2 + 26.0 * (double)Main.inventoryScale - (double)Main.itemTexture[Main.chest[Main.player[Main.myPlayer].chest].item[index3].type].Height * 0.5 * (double)scale)), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.chest[Main.player[Main.myPlayer].chest].item[index3].type].Width, Main.itemTexture[Main.chest[Main.player[Main.myPlayer].chest].item[index3].type].Height)), Main.chest[Main.player[Main.myPlayer].chest].item[index3].GetAlpha(color3), 0.0f, new Vector2(), scale, SpriteEffects.None, 0.0f);
                                    if (Main.chest[Main.player[Main.myPlayer].chest].item[index3].color != new Color())
                                        this.spriteBatch.Draw(Main.itemTexture[Main.chest[Main.player[Main.myPlayer].chest].item[index3].type], new Vector2((float)((double)num1 + 26.0 * (double)Main.inventoryScale - (double)Main.itemTexture[Main.chest[Main.player[Main.myPlayer].chest].item[index3].type].Width * 0.5 * (double)scale), (float)((double)num2 + 26.0 * (double)Main.inventoryScale - (double)Main.itemTexture[Main.chest[Main.player[Main.myPlayer].chest].item[index3].type].Height * 0.5 * (double)scale)), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.chest[Main.player[Main.myPlayer].chest].item[index3].type].Width, Main.itemTexture[Main.chest[Main.player[Main.myPlayer].chest].item[index3].type].Height)), Main.chest[Main.player[Main.myPlayer].chest].item[index3].GetColor(color3), 0.0f, new Vector2(), scale, SpriteEffects.None, 0.0f);
                                    if (Main.chest[Main.player[Main.myPlayer].chest].item[index3].stack > 1)
                                        this.spriteBatch.DrawString(Main.fontItemStack, string.Concat((object)Main.chest[Main.player[Main.myPlayer].chest].item[index3].stack), new Vector2((float)num1 + 10f * Main.inventoryScale, (float)num2 + 26f * Main.inventoryScale), color3, 0.0f, new Vector2(), scale, SpriteEffects.None, 0.0f);
                                }
                            }
                        }
                    }
                    if (Main.player[Main.myPlayer].chest == -2)
                    {
                        this.spriteBatch.DrawString(Main.fontMouseText, "Piggy Bank", new Vector2(284f, 210f), new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), 0.0f, new Vector2(), 1f, SpriteEffects.None, 0.0f);
                        Main.inventoryScale = 0.75f;
                        for (int index1 = 0; index1 < 5; ++index1)
                        {
                            for (int index2 = 0; index2 < 4; ++index2)
                            {
                                int num1 = (int)(73.0 + (double)(index1 * 56) * (double)Main.inventoryScale);
                                int num2 = (int)(210.0 + (double)(index2 * 56) * (double)Main.inventoryScale);
                                int index3 = index1 + index2 * 5;
                                color3 = new Color(100, 100, 100, 100);
                                if (Main.mouseState.X >= num1 && (double)Main.mouseState.X <= (double)num1 + (double)Main.inventoryBackTexture.Width * (double)Main.inventoryScale && Main.mouseState.Y >= num2 && (double)Main.mouseState.Y <= (double)num2 + (double)Main.inventoryBackTexture.Height * (double)Main.inventoryScale)
                                {
                                    Main.player[Main.myPlayer].mouseInterface = true;
                                    if (Main.mouseLeftRelease && Main.mouseState.LeftButton == ButtonState.Pressed)
                                    {
                                        if (Main.player[Main.myPlayer].selectedItem != index3 || Main.player[Main.myPlayer].itemAnimation <= 0)
                                        {
                                            Item obj = Main.mouseItem;
                                            Main.mouseItem = Main.player[Main.myPlayer].bank[index3];
                                            Main.player[Main.myPlayer].bank[index3] = obj;
                                            if (Main.player[Main.myPlayer].bank[index3].type == 0 || Main.player[Main.myPlayer].bank[index3].stack < 1)
                                                Main.player[Main.myPlayer].bank[index3] = new Item();
                                            if (Main.mouseItem.IsTheSameAs(Main.player[Main.myPlayer].bank[index3]) && (Main.player[Main.myPlayer].bank[index3].stack != Main.player[Main.myPlayer].bank[index3].maxStack && Main.mouseItem.stack != Main.mouseItem.maxStack))
                                            {
                                                if (Main.mouseItem.stack + Main.player[Main.myPlayer].bank[index3].stack <= Main.mouseItem.maxStack)
                                                {
                                                    Main.player[Main.myPlayer].bank[index3].stack += Main.mouseItem.stack;
                                                    Main.mouseItem.stack = 0;
                                                }
                                                else
                                                {
                                                    int num5 = Main.mouseItem.maxStack - Main.player[Main.myPlayer].bank[index3].stack;
                                                    Main.player[Main.myPlayer].bank[index3].stack += num5;
                                                    Main.mouseItem.stack -= num5;
                                                }
                                            }
                                            if (Main.mouseItem.type == 0 || Main.mouseItem.stack < 1)
                                                Main.mouseItem = new Item();
                                            if (Main.mouseItem.type > 0 || Main.player[Main.myPlayer].bank[index3].type > 0)
                                            {
                                                Recipe.FindRecipes();
                                                Main.PlaySound(7, -1, -1, 1);
                                            }
                                        }
                                    }
                                    else if (Main.stackSplit <= 1 && Main.mouseState.RightButton == ButtonState.Pressed && (Main.mouseItem.IsTheSameAs(Main.player[Main.myPlayer].bank[index3]) || Main.mouseItem.type == 0) && (Main.mouseItem.stack < Main.mouseItem.maxStack || Main.mouseItem.type == 0))
                                    {
                                        if (Main.mouseItem.type == 0)
                                        {
                                            Main.mouseItem = (Item)Main.player[Main.myPlayer].bank[index3].Clone();
                                            Main.mouseItem.stack = 0;
                                        }
                                        ++Main.mouseItem.stack;
                                        --Main.player[Main.myPlayer].bank[index3].stack;
                                        if (Main.player[Main.myPlayer].bank[index3].stack <= 0)
                                            Main.player[Main.myPlayer].bank[index3] = new Item();
                                        Recipe.FindRecipes();
                                        Main.soundInstanceMenuTick.Stop();
                                        Main.soundInstanceMenuTick = Main.soundMenuTick.CreateInstance();
                                        Main.PlaySound(12, -1, -1, 1);
                                        Main.stackSplit = Main.stackSplit != 0 ? Main.stackDelay : 15;
                                    }
                                    cursorText1 = Main.player[Main.myPlayer].bank[index3].name;
                                    Main.toolTip = (Item)Main.player[Main.myPlayer].bank[index3].Clone();
                                    if (Main.player[Main.myPlayer].bank[index3].stack > 1)
                                        cursorText1 = string.Concat(new object[4]
                    {
                      (object) cursorText1,
                      (object) " (",
                      (object) Main.player[Main.myPlayer].bank[index3].stack,
                      (object) ")"
                    });
                                }
                                this.spriteBatch.Draw(Main.inventoryBackTexture, new Vector2((float)num1, (float)num2), new Rectangle?(new Rectangle(0, 0, Main.inventoryBackTexture.Width, Main.inventoryBackTexture.Height)), color1, 0.0f, new Vector2(), Main.inventoryScale, SpriteEffects.None, 0.0f);
                                color3 = Color.White;
                                if (Main.player[Main.myPlayer].bank[index3].type > 0 && Main.player[Main.myPlayer].bank[index3].stack > 0)
                                {
                                    float num5 = 1f;
                                    if (Main.itemTexture[Main.player[Main.myPlayer].bank[index3].type].Width > 32 || Main.itemTexture[Main.player[Main.myPlayer].bank[index3].type].Height > 32)
                                        num5 = Main.itemTexture[Main.player[Main.myPlayer].bank[index3].type].Width <= Main.itemTexture[Main.player[Main.myPlayer].bank[index3].type].Height ? 32f / (float)Main.itemTexture[Main.player[Main.myPlayer].bank[index3].type].Height : 32f / (float)Main.itemTexture[Main.player[Main.myPlayer].bank[index3].type].Width;
                                    float scale = num5 * Main.inventoryScale;
                                    this.spriteBatch.Draw(Main.itemTexture[Main.player[Main.myPlayer].bank[index3].type], new Vector2((float)((double)num1 + 26.0 * (double)Main.inventoryScale - (double)Main.itemTexture[Main.player[Main.myPlayer].bank[index3].type].Width * 0.5 * (double)scale), (float)((double)num2 + 26.0 * (double)Main.inventoryScale - (double)Main.itemTexture[Main.player[Main.myPlayer].bank[index3].type].Height * 0.5 * (double)scale)), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.player[Main.myPlayer].bank[index3].type].Width, Main.itemTexture[Main.player[Main.myPlayer].bank[index3].type].Height)), Main.player[Main.myPlayer].bank[index3].GetAlpha(color3), 0.0f, new Vector2(), scale, SpriteEffects.None, 0.0f);
                                    if (Main.player[Main.myPlayer].bank[index3].color != new Color())
                                        this.spriteBatch.Draw(Main.itemTexture[Main.player[Main.myPlayer].bank[index3].type], new Vector2((float)((double)num1 + 26.0 * (double)Main.inventoryScale - (double)Main.itemTexture[Main.player[Main.myPlayer].bank[index3].type].Width * 0.5 * (double)scale), (float)((double)num2 + 26.0 * (double)Main.inventoryScale - (double)Main.itemTexture[Main.player[Main.myPlayer].bank[index3].type].Height * 0.5 * (double)scale)), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.player[Main.myPlayer].bank[index3].type].Width, Main.itemTexture[Main.player[Main.myPlayer].bank[index3].type].Height)), Main.player[Main.myPlayer].bank[index3].GetColor(color3), 0.0f, new Vector2(), scale, SpriteEffects.None, 0.0f);
                                    if (Main.player[Main.myPlayer].bank[index3].stack > 1)
                                        this.spriteBatch.DrawString(Main.fontItemStack, string.Concat((object)Main.player[Main.myPlayer].bank[index3].stack), new Vector2((float)num1 + 10f * Main.inventoryScale, (float)num2 + 26f * Main.inventoryScale), color3, 0.0f, new Vector2(), scale, SpriteEffects.None, 0.0f);
                                }
                            }
                        }
                    }
                }
                else
                {
                    bool flag2 = false;
                    bool flag3 = false;
                    for (int index1 = 0; index1 < 3; ++index1)
                    {
                        string text = "";
                        if (Main.player[Main.myPlayer].accDepthMeter > 0 && !flag3)
                        {
                            int num1 = (int)(((double)Main.player[Main.myPlayer].position.Y + (double)Main.player[Main.myPlayer].height) * 2.0 / 16.0 - Main.worldSurface * 2.0);
                            if (num1 > 0)
                            {
                                text = "Depth: " + (object)num1 + " feet below";
                                if (num1 == 1)
                                    text = "Depth: " + (object)num1 + " foot below";
                            }
                            else if (num1 < 0)
                            {
                                int num2 = num1 * -1;
                                text = "Depth: " + (object)num2 + " feet above";
                                if (num2 == 1)
                                    text = "Depth: " + (object)num2 + " foot above";
                            }
                            else
                                text = "Depth: Level";
                            flag3 = true;
                        }
                        else if (Main.player[Main.myPlayer].accWatch > 0 && !flag2)
                        {
                            string str1 = "AM";
                            double num1 = Main.time;
                            if (!Main.dayTime)
                                num1 += 54000.0;
                            double num2 = num1 / 86400.0 * 24.0 - 7.5 - 12.0;
                            if (num2 < 0.0)
                                num2 += 24.0;
                            if (num2 >= 12.0)
                                str1 = "PM";
                            int num3 = (int)num2;
                            double num4 = (double)(int)((num2 - (double)num3) * 60.0);
                            string str2 = string.Concat((object)num4);
                            if (num4 < 10.0)
                                str2 = "0" + str2;
                            if (num3 > 12)
                                num3 -= 12;
                            if (num3 == 0)
                                num3 = 12;
                            if (Main.player[Main.myPlayer].accWatch == 1)
                                str2 = "00";
                            else if (Main.player[Main.myPlayer].accWatch == 2)
                                str2 = num4 >= 30.0 ? "30" : "00";
                            text = "Time: " + (object)num3 + ":" + str2 + " " + str1;
                            flag2 = true;
                        }
                        if (text != "")
                        {
                            for (int index2 = 0; index2 < 5; ++index2)
                            {
                                int num1 = 0;
                                int num2 = 0;
                                Color color2 = Color.Black;
                                if (index2 == 0)
                                    num1 = -2;
                                if (index2 == 1)
                                    num1 = 2;
                                if (index2 == 2)
                                    num2 = -2;
                                if (index2 == 3)
                                    num2 = 2;
                                if (index2 == 4)
                                    color2 = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
                                this.spriteBatch.DrawString(Main.fontMouseText, text, new Vector2((float)(22 + num1), (float)(74 + 22 * index1 + num2)), color2, 0.0f, new Vector2(), 1f, SpriteEffects.None, 0.0f);
                            }
                        }
                    }
                }
                if (!Main.playerInventory)
                {
                    this.spriteBatch.DrawString(Main.fontMouseText, "Items", new Vector2(215f, 0.0f), new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), 0.0f, new Vector2(), 1f, SpriteEffects.None, 0.0f);
                    int num1 = 20;
                    for (int index = 0; index < 10; ++index)
                    {
                        if (index == Main.player[Main.myPlayer].selectedItem)
                        {
                            if ((double)Main.hotbarScale[index] < 1.0)
                                Main.hotbarScale[index] += 0.05f;
                        }
                        else if ((double)Main.hotbarScale[index] > 0.75)
                            Main.hotbarScale[index] -= 0.05f;
                        int num2 = (int)(20.0 + 22.0 * (1.0 - (double)Main.hotbarScale[index]));
                        Color color2 = new Color((int)byte.MaxValue, (int)byte.MaxValue, (int)byte.MaxValue, (int)(75.0 + 150.0 * (double)Main.hotbarScale[index]));
                        this.spriteBatch.Draw(Main.inventoryBackTexture, new Vector2((float)num1, (float)num2), new Rectangle?(new Rectangle(0, 0, Main.inventoryBackTexture.Width, Main.inventoryBackTexture.Height)), new Color(100, 100, 100, 100), 0.0f, new Vector2(), Main.hotbarScale[index], SpriteEffects.None, 0.0f);
                        if (Main.mouseState.X >= num1 && (double)Main.mouseState.X <= (double)num1 + (double)Main.inventoryBackTexture.Width * (double)Main.hotbarScale[index] && (Main.mouseState.Y >= num2 && (double)Main.mouseState.Y <= (double)num2 + (double)Main.inventoryBackTexture.Height * (double)Main.hotbarScale[index]) && !Main.player[Main.myPlayer].channel)
                        {
                            Main.player[Main.myPlayer].mouseInterface = true;
                            if (Main.mouseState.LeftButton == ButtonState.Pressed)
                                Main.player[Main.myPlayer].changeItem = index;
                            Main.player[Main.myPlayer].showItemIcon = false;
                            cursorText1 = Main.player[Main.myPlayer].inventory[index].name;
                            if (Main.player[Main.myPlayer].inventory[index].stack > 1)
                                cursorText1 = string.Concat(new object[4]
                {
                  (object) cursorText1,
                  (object) " (",
                  (object) Main.player[Main.myPlayer].inventory[index].stack,
                  (object) ")"
                });
                            rare1 = Main.player[Main.myPlayer].inventory[index].rare;
                        }
                        if (Main.player[Main.myPlayer].inventory[index].type > 0 && Main.player[Main.myPlayer].inventory[index].stack > 0)
                        {
                            float num3 = 1f;
                            if (Main.itemTexture[Main.player[Main.myPlayer].inventory[index].type].Width > 32 || Main.itemTexture[Main.player[Main.myPlayer].inventory[index].type].Height > 32)
                                num3 = Main.itemTexture[Main.player[Main.myPlayer].inventory[index].type].Width <= Main.itemTexture[Main.player[Main.myPlayer].inventory[index].type].Height ? 32f / (float)Main.itemTexture[Main.player[Main.myPlayer].inventory[index].type].Height : 32f / (float)Main.itemTexture[Main.player[Main.myPlayer].inventory[index].type].Width;
                            float scale = num3 * Main.hotbarScale[index];
                            this.spriteBatch.Draw(Main.itemTexture[Main.player[Main.myPlayer].inventory[index].type], new Vector2((float)((double)num1 + 26.0 * (double)Main.hotbarScale[index] - (double)Main.itemTexture[Main.player[Main.myPlayer].inventory[index].type].Width * 0.5 * (double)scale), (float)((double)num2 + 26.0 * (double)Main.hotbarScale[index] - (double)Main.itemTexture[Main.player[Main.myPlayer].inventory[index].type].Height * 0.5 * (double)scale)), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.player[Main.myPlayer].inventory[index].type].Width, Main.itemTexture[Main.player[Main.myPlayer].inventory[index].type].Height)), Main.player[Main.myPlayer].inventory[index].GetAlpha(color2), 0.0f, new Vector2(), scale, SpriteEffects.None, 0.0f);
                            if (Main.player[Main.myPlayer].inventory[index].color != new Color())
                                this.spriteBatch.Draw(Main.itemTexture[Main.player[Main.myPlayer].inventory[index].type], new Vector2((float)((double)num1 + 26.0 * (double)Main.hotbarScale[index] - (double)Main.itemTexture[Main.player[Main.myPlayer].inventory[index].type].Width * 0.5 * (double)scale), (float)((double)num2 + 26.0 * (double)Main.hotbarScale[index] - (double)Main.itemTexture[Main.player[Main.myPlayer].inventory[index].type].Height * 0.5 * (double)scale)), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.player[Main.myPlayer].inventory[index].type].Width, Main.itemTexture[Main.player[Main.myPlayer].inventory[index].type].Height)), Main.player[Main.myPlayer].inventory[index].GetColor(color2), 0.0f, new Vector2(), scale, SpriteEffects.None, 0.0f);
                            if (Main.player[Main.myPlayer].inventory[index].stack > 1)
                                this.spriteBatch.DrawString(Main.fontItemStack, string.Concat((object)Main.player[Main.myPlayer].inventory[index].stack), new Vector2((float)num1 + 10f * Main.hotbarScale[index], (float)num2 + 26f * Main.hotbarScale[index]), color2, 0.0f, new Vector2(), scale, SpriteEffects.None, 0.0f);
                            if (Main.player[Main.myPlayer].inventory[index].potion)
                            {
                                Color color3 = Main.player[Main.myPlayer].inventory[index].GetAlpha(color2);
                                float num4 = (float)Main.player[Main.myPlayer].potionDelay / (float)Item.potionDelay;
                                color3 = new Color((int)(byte)((float)color3.R * num4), (int)(byte)((float)color3.G * num4), (int)(byte)((float)color3.B * num4), (int)(byte)((float)color3.A * num4));
                                this.spriteBatch.Draw(Main.cdTexture, new Vector2((float)((double)num1 + 26.0 * (double)Main.hotbarScale[index] - (double)Main.cdTexture.Width * 0.5 * (double)scale), (float)((double)num2 + 26.0 * (double)Main.hotbarScale[index] - (double)Main.cdTexture.Height * 0.5 * (double)scale)), new Rectangle?(new Rectangle(0, 0, Main.cdTexture.Width, Main.cdTexture.Height)), color3, 0.0f, new Vector2(), scale, SpriteEffects.None, 0.0f);
                            }
                        }
                        num1 += (int)((double)Main.inventoryBackTexture.Width * (double)Main.hotbarScale[index]) + 4;
                    }
                }
                if (cursorText1 != null && cursorText1 != "" && Main.mouseItem.type == 0)
                {
                    Main.player[Main.myPlayer].showItemIcon = false;
                    this.MouseText(cursorText1, rare1);
                    flag1 = true;
                }
                if (Main.chatMode)
                {
                    ++this.textBlinkerCount;
                    if (this.textBlinkerCount >= 20)
                    {
                        this.textBlinkerState = this.textBlinkerState != 0 ? 0 : 1;
                        this.textBlinkerCount = 0;
                    }
                    string text = Main.chatText;
                    if (this.textBlinkerState == 1)
                        text = text + "|";
                    this.spriteBatch.Draw(Main.textBackTexture, new Vector2(78f, (float)(Main.screenHeight - 36)), new Rectangle?(new Rectangle(0, 0, Main.textBackTexture.Width, Main.textBackTexture.Height)), new Color(100, 100, 100, 100), 0.0f, new Vector2(), 1f, SpriteEffects.None, 0.0f);
                    for (int index = 0; index < 5; ++index)
                    {
                        int num1 = 0;
                        int num2 = 0;
                        Color color2 = Color.Black;
                        if (index == 0)
                            num1 = -2;
                        if (index == 1)
                            num1 = 2;
                        if (index == 2)
                            num2 = -2;
                        if (index == 3)
                            num2 = 2;
                        if (index == 4)
                            color2 = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
                        this.spriteBatch.DrawString(Main.fontMouseText, text, new Vector2((float)(88 + num1), (float)(Main.screenHeight - 30 + num2)), color2, 0.0f, new Vector2(), 1f, SpriteEffects.None, 0.0f);
                    }
                }
                for (int index1 = 0; index1 < Main.numChatLines; ++index1)
                {
                    if (Main.chatMode || Main.chatLine[index1].showTime > 0)
                    {
                        float num1 = (float)Main.mouseTextColor / (float)byte.MaxValue;
                        for (int index2 = 0; index2 < 5; ++index2)
                        {
                            int num2 = 0;
                            int num3 = 0;
                            Color color2 = Color.Black;
                            if (index2 == 0)
                                num2 = -2;
                            if (index2 == 1)
                                num2 = 2;
                            if (index2 == 2)
                                num3 = -2;
                            if (index2 == 3)
                                num3 = 2;
                            if (index2 == 4)
                                color2 = new Color((int)(byte)((double)Main.chatLine[index1].color.R * (double)num1), (int)(byte)((double)Main.chatLine[index1].color.G * (double)num1), (int)(byte)((double)Main.chatLine[index1].color.B * (double)num1), (int)Main.mouseTextColor);
                            this.spriteBatch.DrawString(Main.fontMouseText, Main.chatLine[index1].text, new Vector2((float)(88 + num2), (float)(Main.screenHeight - 30 + num3 - 28 - index1 * 21)), color2, 0.0f, new Vector2(), 1f, SpriteEffects.None, 0.0f);
                        }
                    }
                }
                if (Main.player[Main.myPlayer].dead)
                {
                    string text = "You were slain...";
                    this.spriteBatch.DrawString(Main.fontDeathText, text, new Vector2((float)(Main.screenWidth / 2 - text.Length * 10), (float)(Main.screenHeight / 2 - 20)), Main.player[Main.myPlayer].GetDeathAlpha(new Color(0, 0, 0, 0)), 0.0f, new Vector2(), 1f, SpriteEffects.None, 0.0f);
                }
                this.spriteBatch.Draw(Main.cursorTexture, new Vector2((float)(Main.mouseState.X + 1), (float)(Main.mouseState.Y + 1)), new Rectangle?(new Rectangle(0, 0, Main.cursorTexture.Width, Main.cursorTexture.Height)), new Color((int)((double)Main.cursorColor.R * 0.200000002980232), (int)((double)Main.cursorColor.G * 0.200000002980232), (int)((double)Main.cursorColor.B * 0.200000002980232), (int)((double)Main.cursorColor.A * 0.5)), 0.0f, new Vector2(), Main.cursorScale * 1.1f, SpriteEffects.None, 0.0f);
                this.spriteBatch.Draw(Main.cursorTexture, new Vector2((float)Main.mouseState.X, (float)Main.mouseState.Y), new Rectangle?(new Rectangle(0, 0, Main.cursorTexture.Width, Main.cursorTexture.Height)), Main.cursorColor, 0.0f, new Vector2(), Main.cursorScale, SpriteEffects.None, 0.0f);
                if (Main.mouseItem.type > 0 && Main.mouseItem.stack > 0)
                {
                    Main.player[Main.myPlayer].showItemIcon = false;
                    Main.player[Main.myPlayer].showItemIcon2 = 0;
                    flag1 = true;
                    float num1 = 1f;
                    if (Main.itemTexture[Main.mouseItem.type].Width > 32 || Main.itemTexture[Main.mouseItem.type].Height > 32)
                        num1 = Main.itemTexture[Main.mouseItem.type].Width <= Main.itemTexture[Main.mouseItem.type].Height ? 32f / (float)Main.itemTexture[Main.mouseItem.type].Height : 32f / (float)Main.itemTexture[Main.mouseItem.type].Width;
                    float num2 = 1f;
                    Color white = Color.White;
                    float scale = num1 * num2;
                    this.spriteBatch.Draw(Main.itemTexture[Main.mouseItem.type], new Vector2((float)((double)Main.mouseState.X + 26.0 * (double)num2 - (double)Main.itemTexture[Main.mouseItem.type].Width * 0.5 * (double)scale), (float)((double)Main.mouseState.Y + 26.0 * (double)num2 - (double)Main.itemTexture[Main.mouseItem.type].Height * 0.5 * (double)scale)), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.mouseItem.type].Width, Main.itemTexture[Main.mouseItem.type].Height)), Main.mouseItem.GetAlpha(white), 0.0f, new Vector2(), scale, SpriteEffects.None, 0.0f);
                    if (Main.mouseItem.color != new Color())
                        this.spriteBatch.Draw(Main.itemTexture[Main.mouseItem.type], new Vector2((float)((double)Main.mouseState.X + 26.0 * (double)num2 - (double)Main.itemTexture[Main.mouseItem.type].Width * 0.5 * (double)scale), (float)((double)Main.mouseState.Y + 26.0 * (double)num2 - (double)Main.itemTexture[Main.mouseItem.type].Height * 0.5 * (double)scale)), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.mouseItem.type].Width, Main.itemTexture[Main.mouseItem.type].Height)), Main.mouseItem.GetColor(white), 0.0f, new Vector2(), scale, SpriteEffects.None, 0.0f);
                    if (Main.mouseItem.stack > 1)
                        this.spriteBatch.DrawString(Main.fontItemStack, string.Concat((object)Main.mouseItem.stack), new Vector2((float)Main.mouseState.X + 10f * num2, (float)Main.mouseState.Y + 26f * num2), white, 0.0f, new Vector2(), scale, SpriteEffects.None, 0.0f);
                }
                Rectangle rectangle4 = new Rectangle((int)((double)Main.mouseState.X + (double)Main.screenPosition.X), (int)((double)Main.mouseState.Y + (double)Main.screenPosition.Y), 1, 1);
                if (!flag1)
                {
                    int num1 = 26 * Main.player[Main.myPlayer].statLifeMax / num12;
                    int num2 = 0;
                    if (Main.player[Main.myPlayer].statLifeMax > 200)
                    {
                        num1 = 260;
                        num2 += 26;
                    }
                    if (Main.mouseState.X > 500 + num10 && Main.mouseState.X < 500 + num1 + num10 && Main.mouseState.Y > 32 && Main.mouseState.Y < 32 + Main.heartTexture.Height + num2)
                    {
                        Main.player[Main.myPlayer].showItemIcon = false;
                        this.MouseText((string)(object)Main.player[Main.myPlayer].statLife + (object)"/" + (string)(object)Main.player[Main.myPlayer].statLifeMax, 0);
                        flag1 = true;
                    }
                }
                if (!flag1)
                {
                    int num1 = 24;
                    int num2 = 28 * Main.player[Main.myPlayer].statManaMax / num13;
                    if (Main.mouseState.X > 762 + num10 && Main.mouseState.X < 762 + num1 + num10 && Main.mouseState.Y > 30 && Main.mouseState.Y < 30 + num2)
                    {
                        Main.player[Main.myPlayer].showItemIcon = false;
                        this.MouseText((string)(object)Main.player[Main.myPlayer].statMana + (object)"/" + (string)(object)Main.player[Main.myPlayer].statManaMax, 0);
                        flag1 = true;
                    }
                }
                if (!flag1)
                {
                    for (int index = 0; index < 200; ++index)
                    {
                        if (Main.item[index].active)
                        {
                            Rectangle rectangle2 = new Rectangle((int)((double)Main.item[index].position.X + (double)Main.item[index].width * 0.5 - (double)Main.itemTexture[Main.item[index].type].Width * 0.5), (int)((double)Main.item[index].position.Y + (double)Main.item[index].height - (double)Main.itemTexture[Main.item[index].type].Height), Main.itemTexture[Main.item[index].type].Width, Main.itemTexture[Main.item[index].type].Height);
                            if (rectangle4.Intersects(rectangle2))
                            {
                                Main.player[Main.myPlayer].showItemIcon = false;
                                string cursorText2 = Main.item[index].name;
                                if (Main.item[index].stack > 1)
                                    cursorText2 = string.Concat(new object[4]
                  {
                    (object) cursorText2,
                    (object) " (",
                    (object) Main.item[index].stack,
                    (object) ")"
                  });
                                if (Main.item[index].owner < (int)byte.MaxValue && Main.showItemOwner)
                                    cursorText2 = cursorText2 + " <" + Main.player[Main.item[index].owner].name + ">";
                                int rare2 = Main.item[index].rare;
                                this.MouseText(cursorText2, rare2);
                                flag1 = true;
                                break;
                            }
                        }
                    }
                }
                for (int index = 0; index < (int)byte.MaxValue; ++index)
                {
                    if (Main.player[index].active && Main.myPlayer != index && !Main.player[index].dead)
                    {
                        rectangle1 = new Rectangle((int)((double)Main.player[index].position.X + (double)Main.player[index].width * 0.5 - 16.0), (int)((double)Main.player[index].position.Y + (double)Main.player[index].height - 48.0), 32, 48);
                        if (!flag1 && rectangle4.Intersects(rectangle1))
                        {
                            Main.player[Main.myPlayer].showItemIcon = false;
                            string cursorText2 = Main.player[index].name + (object)": " + (string)(object)Main.player[index].statLife + "/" + (string)(object)Main.player[index].statLifeMax;
                            if (Main.player[index].hostile)
                                cursorText2 = cursorText2 + " (PvP)";
                            this.MouseText(cursorText2, 0);
                        }
                    }
                }
                if (!flag1)
                {
                    for (int index = 0; index < 1000; ++index)
                    {
                        if (Main.npc[index].active)
                        {
                            Rectangle rectangle2 = new Rectangle((int)((double)Main.npc[index].position.X + (double)Main.npc[index].width * 0.5 - (double)Main.npcTexture[Main.npc[index].type].Width * 0.5), (int)((double)Main.npc[index].position.Y + (double)Main.npc[index].height - (double)(Main.npcTexture[Main.npc[index].type].Height / Main.npcFrameCount[Main.npc[index].type])), Main.npcTexture[Main.npc[index].type].Width, Main.npcTexture[Main.npc[index].type].Height / Main.npcFrameCount[Main.npc[index].type]);
                            if (rectangle4.Intersects(rectangle2))
                            {
                                bool flag2 = false;
                                if (Main.npc[index].townNPC && new Rectangle((int)((double)Main.player[Main.myPlayer].position.X + (double)(Main.player[Main.myPlayer].width / 2) - (double)(Player.tileRangeX * 16)), (int)((double)Main.player[Main.myPlayer].position.Y + (double)(Main.player[Main.myPlayer].height / 2) - (double)(Player.tileRangeY * 16)), Player.tileRangeX * 16 * 2, Player.tileRangeY * 16 * 2).Intersects(new Rectangle((int)Main.npc[index].position.X, (int)Main.npc[index].position.Y, Main.npc[index].width, Main.npc[index].height)))
                                    flag2 = true;
                                if (flag2)
                                {
                                    int num1 = -(Main.npc[index].width / 2 + 8);
                                    SpriteEffects effects = SpriteEffects.None;
                                    if (Main.npc[index].spriteDirection == -1)
                                    {
                                        effects = SpriteEffects.FlipHorizontally;
                                        num1 = Main.npc[index].width / 2 + 8;
                                    }
                                    this.spriteBatch.Draw(Main.chatTexture, new Vector2(Main.npc[index].position.X + (float)(Main.npc[index].width / 2) - Main.screenPosition.X - (float)(Main.chatTexture.Width / 2) - (float)num1, Main.npc[index].position.Y - (float)Main.chatTexture.Height - Main.screenPosition.Y), new Rectangle?(new Rectangle(0, 0, Main.chatTexture.Width, Main.chatTexture.Height)), new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), 0.0f, new Vector2(), 1f, effects, 0.0f);
                                    if (Main.mouseState.RightButton == ButtonState.Pressed && Main.npcChatRelease)
                                    {
                                        Main.npcChatRelease = false;
                                        if (Main.player[Main.myPlayer].talkNPC != index)
                                        {
                                            Main.player[Main.myPlayer].sign = -1;
                                            Main.editSign = false;
                                            Main.player[Main.myPlayer].talkNPC = index;
                                            Main.playerInventory = false;
                                            Main.player[Main.myPlayer].chest = -1;
                                            Main.npcChatText = Main.npc[index].GetChat();
                                            Main.PlaySound(10, -1, -1, 1);
                                        }
                                    }
                                }
                                Main.player[Main.myPlayer].showItemIcon = false;
                                this.MouseText(Main.npc[index].name + (object)": " + (string)(object)Main.npc[index].life + "/" + (string)(object)Main.npc[index].lifeMax, 0);
                                break;
                            }
                        }
                    }
                }
                Main.npcChatRelease = Main.mouseState.RightButton != ButtonState.Pressed && true;
                if (Main.player[Main.myPlayer].showItemIcon && (Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].type > 0 || Main.player[Main.myPlayer].showItemIcon2 > 0))
                {
                    int index = Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].type;
                    Color color2 = Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].GetAlpha(Color.White);
                    Color color3 = Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].GetColor(Color.White);
                    if (Main.player[Main.myPlayer].showItemIcon2 > 0)
                    {
                        index = Main.player[Main.myPlayer].showItemIcon2;
                        color2 = Color.White;
                        color3 = new Color();
                    }
                    this.spriteBatch.Draw(Main.itemTexture[index], new Vector2((float)(Main.mouseState.X + 10), (float)(Main.mouseState.Y + 10)), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[index].Width, Main.itemTexture[index].Height)), color2, 0.0f, new Vector2(), 1f, SpriteEffects.None, 0.0f);
                    if (Main.player[Main.myPlayer].showItemIcon2 == 0 && Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].color != new Color())
                        this.spriteBatch.Draw(Main.itemTexture[Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].type], new Vector2((float)(Main.mouseState.X + 10), (float)(Main.mouseState.Y + 10)), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].type].Width, Main.itemTexture[Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].type].Height)), color3, 0.0f, new Vector2(), 1f, SpriteEffects.None, 0.0f);
                }
                Main.player[Main.myPlayer].showItemIcon = false;
                Main.player[Main.myPlayer].showItemIcon2 = 0;
            }
        }

        protected void QuitGame()
        {
            //Steam.Kill();
            this.Exit();
        }

        protected void DrawMenu()
        {
            Star.UpdateStars();
            Cloud.UpdateClouds();
            Main.evilTiles = 0;
            Main.jungleTiles = 0;
            Main.chatMode = false;
            for (int index = 0; index < Main.numChatLines; ++index)
                Main.chatLine[index] = new ChatLine();
            this.DrawFPS();
            Main.screenPosition.Y = (float)(Main.worldSurface * 16.0) - (float)Main.screenHeight;
            Main.background = 0;
            byte num1 = (byte)(((int)byte.MaxValue + (int)Main.tileColor.R * 2) / 3);
            Color color1 = new Color((int)num1, (int)num1, (int)num1, (int)byte.MaxValue);
            this.logoRotation += this.logoRotationSpeed * 3E-05f;
            if ((double)this.logoRotation > 0.1)
                this.logoRotationDirection = -1f;
            else if ((double)this.logoRotation < -0.1)
                this.logoRotationDirection = 1f;
            if ((double)this.logoRotationSpeed < 20.0 & (double)this.logoRotationDirection == 1.0)
                ++this.logoRotationSpeed;
            else if ((double)this.logoRotationSpeed > -20.0 & (double)this.logoRotationDirection == -1.0)
                --this.logoRotationSpeed;
            this.logoScale += this.logoScaleSpeed * 1E-05f;
            if ((double)this.logoScale > 1.1)
                this.logoScaleDirection = -1f;
            else if ((double)this.logoScale < 0.9)
                this.logoScaleDirection = 1f;
            if ((double)this.logoScaleSpeed < 50.0 & (double)this.logoScaleDirection == 1.0)
                ++this.logoScaleSpeed;
            else if ((double)this.logoScaleSpeed > -50.0 & (double)this.logoScaleDirection == -1.0)
                --this.logoScaleSpeed;
            this.spriteBatch.Draw(Main.logoTexture, new Vector2((float)(Main.screenWidth / 2), 100f), new Rectangle?(new Rectangle(0, 0, Main.logoTexture.Width, Main.logoTexture.Height)), color1, this.logoRotation, new Vector2((float)(Main.logoTexture.Width / 2), (float)(Main.logoTexture.Height / 2)), this.logoScale, SpriteEffects.None, 0.0f);
            int num2 = 250;
            int num3 = Main.screenWidth / 2;
            int num4 = 80;
            int num5 = 0;
            int num6 = Main.menuMode;
            int index1 = -1;
            int num7 = 0;
            int num8 = 0;
            bool flag1 = false;
            bool flag2 = false;
            bool flag3 = false;
            int num9 = 0;
            bool[] flagArray1 = new bool[Main.maxMenuItems];
            bool[] flagArray2 = new bool[Main.maxMenuItems];
            int[] numArray1 = new int[Main.maxMenuItems];
            int[] numArray2 = new int[Main.maxMenuItems];
            for (int index2 = 0; index2 < Main.maxMenuItems; ++index2)
            {
                flagArray1[index2] = false;
                flagArray2[index2] = false;
                numArray1[index2] = 0;
                numArray2[index2] = 0;
            }
            string[] strArray1 = new string[Main.maxMenuItems];
            if (Main.menuMode == -1)
                Main.menuMode = 0;
            Vector2 origin;
            if (Main.netMode == 2)
            {
                bool flag4 = true;
                for (int index2 = 0; index2 < 8; ++index2)
                {
                    if (index2 < (int)byte.MaxValue)
                    {
                        try
                        {
                            strArray1[index2] = Netplay.serverSock[index2].statusText;
                            if (Netplay.serverSock[index2].active && Main.showSpam)
                            {
                                string[] strArray2;
                                int index3;
                                string str = string.Concat(new object[4]
                {
                     //TODO: Figure out - changed
                  (object) (strArray2 = strArray1)[(index3 = index2)],
                  (object) " (",
                  (object) NetMessage.buffer[index2].spamCount,
                  (object) ")"
                });
                                strArray2[index3] = str;
                            }
                        }
                        catch
                        {
                            strArray1[index2] = "";
                        }
                        flagArray1[index2] = true;
                        if (strArray1[index2] != "" && strArray1[index2] != null)
                            flag4 = false;
                    }
                }
                if (flag4)
                {
                    strArray1[0] = "Start a new instance of Terraria to join!";
                    strArray1[1] = "Running on port " + (object)Netplay.serverPort + ".";
                }
                num5 = 11;
                strArray1[9] = Main.statusText;
                flagArray1[9] = true;
                num2 = 170;
                num4 = 30;
                numArray1[10] = 20;
                numArray1[10] = 40;
                strArray1[10] = "Disconnect";
                if (this.selectedMenu == 10)
                {
                    Netplay.disconnect = true;
                    Main.PlaySound(11, -1, -1, 1);
                }
            }
            else if (Main.menuMode == 31)
            {
                string str = Netplay.password;
                Netplay.password = Main.GetInputText(Netplay.password);
                if (str != Netplay.password)
                    Main.PlaySound(12, -1, -1, 1);
                strArray1[0] = "Server Requires Password:";
                ++this.textBlinkerCount;
                if (this.textBlinkerCount >= 20)
                {
                    this.textBlinkerState = this.textBlinkerState != 0 ? 0 : 1;
                    this.textBlinkerCount = 0;
                }
                strArray1[1] = Netplay.password;
                if (this.textBlinkerState == 1)
                {
                    string[] strArray2;
                    (strArray2 = strArray1)[1] = strArray2[1] + "|";
                    numArray2[1] = 1;
                }
                else
                {
                    string[] strArray2;
                    (strArray2 = strArray1)[1] = strArray2[1] + " ";
                }
                flagArray1[0] = true;
                flagArray1[1] = true;
                numArray1[1] = -20;
                numArray1[2] = 20;
                strArray1[2] = "Accept";
                strArray1[3] = "Back";
                num5 = 4;
                if (this.selectedMenu == 3)
                {
                    Main.PlaySound(11, -1, -1, 1);
                    Main.menuMode = 0;
                    Netplay.disconnect = true;
                    Netplay.password = "";
                }
                else if (this.selectedMenu == 2 || Main.inputTextEnter)
                {
                    NetMessage.SendData(38, -1, -1, Netplay.password, 0, 0.0f, 0.0f, 0.0f);
                    Main.menuMode = 14;
                }
            }
            else if (Main.netMode == 1 || Main.menuMode == 14)
            {
                num5 = 2;
                strArray1[0] = Main.statusText;
                flagArray1[0] = true;
                num2 = 300;
                strArray1[1] = "Cancel";
                if (this.selectedMenu == 1)
                {
                    Netplay.disconnect = true;
                    Netplay.clientSock.tcpClient.Close();
                    Main.PlaySound(11, -1, -1, 1);
                    Main.menuMode = 0;
                    Main.netMode = 0;
                    try
                    {
                        this.tServer.Kill();
                    }
                    catch
                    {
                    }
                }
            }
            else if (Main.menuMode == 30)
            {
                string str = Netplay.password;
                Netplay.password = Main.GetInputText(Netplay.password);
                if (str != Netplay.password)
                    Main.PlaySound(12, -1, -1, 1);
                strArray1[0] = "Enter Server Password:";
                ++this.textBlinkerCount;
                if (this.textBlinkerCount >= 20)
                {
                    this.textBlinkerState = this.textBlinkerState != 0 ? 0 : 1;
                    this.textBlinkerCount = 0;
                }
                strArray1[1] = Netplay.password;
                if (this.textBlinkerState == 1)
                {
                    string[] strArray2;
                    (strArray2 = strArray1)[1] = strArray2[1] + "|";
                    numArray2[1] = 1;
                }
                else
                {
                    string[] strArray2;
                    (strArray2 = strArray1)[1] = strArray2[1] + " ";
                }
                flagArray1[0] = true;
                flagArray1[1] = true;
                numArray1[1] = -20;
                numArray1[2] = 20;
                strArray1[2] = "Accept";
                strArray1[3] = "Back";
                num5 = 4;
                if (this.selectedMenu == 3)
                {
                    Main.PlaySound(11, -1, -1, 1);
                    Main.menuMode = 6;
                    Netplay.password = "";
                }
                else if (this.selectedMenu == 2 || Main.inputTextEnter)
                {
                    this.tServer.StartInfo.FileName = "TerrariaServer.exe";
                    this.tServer.StartInfo.Arguments = "-autoshutdown -world \"" + Main.worldPathName + "\" -password \"" + Netplay.password + "\"";
                    this.tServer.StartInfo.UseShellExecute = false;
                    this.tServer.StartInfo.CreateNoWindow = true;
                    this.tServer.Start();
                    Netplay.SetIP("127.0.0.1");
                    Main.autoPass = true;
                    Main.statusText = "Starting server...";
                    Netplay.StartClient();
                    Main.menuMode = 10;
                }
            }
            else if (Main.menuMode == 15)
            {
                num5 = 2;
                strArray1[0] = Main.statusText;
                flagArray1[0] = true;
                num2 = 80;
                num4 = 400;
                strArray1[1] = "Back";
                if (this.selectedMenu == 1)
                {
                    Netplay.disconnect = true;
                    Main.PlaySound(11, -1, -1, 1);
                    Main.menuMode = 0;
                    Main.netMode = 0;
                }
            }
            else if (Main.menuMode == 200)
            {
                num5 = 3;
                strArray1[0] = "Load failed!";
                flagArray1[0] = true;
                num2 -= 30;
                numArray1[1] = 70;
                numArray1[2] = 50;
                strArray1[1] = "Load Backup";
                strArray1[2] = "Cancel";
                if (this.selectedMenu == 1)
                {
                    if (File.Exists(Main.worldPathName + ".bak"))
                    {
                        File.Copy(Main.worldPathName + ".bak", Main.worldPathName, true);
                        File.Delete(Main.worldPathName + ".bak");
                        Main.PlaySound(10, -1, -1, 1);
                        WorldGen.playWorld();
                        Main.menuMode = 10;
                    }
                    else
                    {
                        Main.PlaySound(11, -1, -1, 1);
                        Main.menuMode = 0;
                        Main.netMode = 0;
                    }
                }
                if (this.selectedMenu == 2)
                {
                    Main.PlaySound(11, -1, -1, 1);
                    Main.menuMode = 0;
                    Main.netMode = 0;
                }
            }
            else if (Main.menuMode == 201)
            {
                num5 = 3;
                strArray1[0] = "Load failed!";
                flagArray1[0] = true;
                flagArray1[1] = true;
                num2 -= 30;
                numArray1[1] = -30;
                numArray1[2] = 50;
                strArray1[1] = "No backup found";
                strArray1[2] = "Back";
                if (this.selectedMenu == 2)
                {
                    Main.PlaySound(11, -1, -1, 1);
                    Main.menuMode = 0;
                    Main.netMode = 0;
                }
            }
            else if (Main.menuMode == 10)
            {
                num5 = 1;
                strArray1[0] = Main.statusText;
                flagArray1[0] = true;
                num2 = 300;
            }
            else if (Main.menuMode == 100)
            {
                num5 = 1;
                strArray1[0] = Main.statusText;
                flagArray1[0] = true;
                num2 = 300;
            }
            else if (Main.menuMode == 0)
            {
                Main.menuMultiplayer = false;
                Main.menuServer = false;
                Main.netMode = 0;
                strArray1[0] = "Single Player";
                strArray1[1] = "Multiplayer";
                strArray1[2] = "Settings";
                strArray1[3] = "Exit";
                num5 = 4;
                if (this.selectedMenu == 3)
                    this.QuitGame();
                if (this.selectedMenu == 1)
                {
                    Main.PlaySound(10, -1, -1, 1);
                    Main.menuMode = 12;
                }
                if (this.selectedMenu == 2)
                {
                    Main.PlaySound(10, -1, -1, 1);
                    Main.menuMode = 11;
                }
                if (this.selectedMenu == 0)
                {
                    Main.PlaySound(10, -1, -1, 1);
                    Main.menuMode = 1;
                    Main.LoadPlayers();
                }
            }
            else if (Main.menuMode == 1)
            {
                Main.myPlayer = 0;
                num2 = 190;
                num4 = 50;
                strArray1[5] = "Create Character";
                strArray1[6] = "Delete";
                if (Main.numLoadPlayers == 5)
                {
                    flagArray2[5] = true;
                    strArray1[5] = "";
                }
                else if (Main.numLoadPlayers == 0)
                {
                    flagArray2[6] = true;
                    strArray1[6] = "";
                }
                strArray1[7] = "Back";
                for (int index2 = 0; index2 < 5; ++index2)
                    strArray1[index2] = index2 >= Main.numLoadPlayers ? (string)null : Main.loadPlayer[index2].name;
                num5 = 8;
                if (this.focusMenu >= 0 && this.focusMenu < Main.numLoadPlayers)
                {
                    index1 = this.focusMenu;
                    origin = Main.fontDeathText.MeasureString(strArray1[index1]);
                    num7 = (int)((double)(Main.screenWidth / 2) + (double)origin.X * 0.5 + 10.0);
                    num8 = num2 + num4 * this.focusMenu + 4;
                }
                if (this.selectedMenu == 7)
                {
                    Main.autoJoin = false;
                    Main.autoPass = false;
                    Main.PlaySound(11, -1, -1, 1);
                    if (Main.menuMultiplayer)
                    {
                        Main.menuMode = 12;
                        Main.menuMultiplayer = false;
                        Main.menuServer = false;
                    }
                    else
                        Main.menuMode = 0;
                }
                else if (this.selectedMenu == 5)
                {
                    Main.loadPlayer[Main.numLoadPlayers] = new Player();
                    Main.PlaySound(10, -1, -1, 1);
                    Main.menuMode = 2;
                }
                else if (this.selectedMenu == 6)
                {
                    Main.PlaySound(10, -1, -1, 1);
                    Main.menuMode = 4;
                }
                else if (this.selectedMenu >= 0)
                {
                    if (Main.menuMultiplayer)
                    {
                        this.selectedPlayer = this.selectedMenu;
                        Main.player[Main.myPlayer] = (Player)Main.loadPlayer[this.selectedPlayer].Clone();
                        Main.playerPathName = Main.loadPlayerPath[this.selectedPlayer];
                        Main.PlaySound(10, -1, -1, 1);
                        if (Main.autoJoin)
                        {
                            if (Netplay.SetIP(Main.getIP))
                            {
                                Main.menuMode = 10;
                                Netplay.StartClient();
                            }
                            else if (Netplay.SetIP2(Main.getIP))
                            {
                                Main.menuMode = 10;
                                Netplay.StartClient();
                            }
                            Main.autoJoin = false;
                        }
                        else if (Main.menuServer)
                        {
                            Main.LoadWorlds();
                            Main.menuMode = 6;
                        }
                        else
                            Main.menuMode = 13;
                    }
                    else
                    {
                        Main.myPlayer = 0;
                        this.selectedPlayer = this.selectedMenu;
                        Main.player[Main.myPlayer] = (Player)Main.loadPlayer[this.selectedPlayer].Clone();
                        Main.playerPathName = Main.loadPlayerPath[this.selectedPlayer];
                        Main.LoadWorlds();
                        Main.PlaySound(10, -1, -1, 1);
                        Main.menuMode = 6;
                    }
                }
            }
            else if (Main.menuMode == 2)
            {
                if (this.selectedMenu == 0)
                {
                    Main.menuMode = 17;
                    Main.PlaySound(10, -1, -1, 1);
                    this.selColor = Main.loadPlayer[Main.numLoadPlayers].hairColor;
                }
                if (this.selectedMenu == 1)
                {
                    Main.menuMode = 18;
                    Main.PlaySound(10, -1, -1, 1);
                    this.selColor = Main.loadPlayer[Main.numLoadPlayers].eyeColor;
                }
                if (this.selectedMenu == 2)
                {
                    Main.menuMode = 19;
                    Main.PlaySound(10, -1, -1, 1);
                    this.selColor = Main.loadPlayer[Main.numLoadPlayers].skinColor;
                }
                if (this.selectedMenu == 3)
                {
                    Main.menuMode = 20;
                    Main.PlaySound(10, -1, -1, 1);
                }
                strArray1[0] = "Hair";
                strArray1[1] = "Eyes";
                strArray1[2] = "Skin";
                strArray1[3] = "Clothes";
                num2 = 260;
                num4 = 50;
                numArray1[4] = 20;
                numArray1[5] = 20;
                strArray1[4] = "Create";
                strArray1[5] = "Back";
                num5 = 6;
                index1 = Main.numLoadPlayers;
                num7 = Main.screenWidth / 2 - 16;
                num8 = 210;
                if (this.selectedMenu == 5)
                {
                    Main.PlaySound(11, -1, -1, 1);
                    Main.menuMode = 1;
                }
                else if (this.selectedMenu == 4)
                {
                    Main.PlaySound(10, -1, -1, 1);
                    Main.loadPlayer[Main.numLoadPlayers].name = "";
                    Main.menuMode = 3;
                }
            }
            else if (Main.menuMode == 20)
            {
                if (this.selectedMenu == 0)
                {
                    Main.menuMode = 21;
                    Main.PlaySound(10, -1, -1, 1);
                    this.selColor = Main.loadPlayer[Main.numLoadPlayers].shirtColor;
                }
                if (this.selectedMenu == 1)
                {
                    Main.menuMode = 22;
                    Main.PlaySound(10, -1, -1, 1);
                    this.selColor = Main.loadPlayer[Main.numLoadPlayers].underShirtColor;
                }
                if (this.selectedMenu == 2)
                {
                    Main.menuMode = 23;
                    Main.PlaySound(10, -1, -1, 1);
                    this.selColor = Main.loadPlayer[Main.numLoadPlayers].pantsColor;
                }
                if (this.selectedMenu == 3)
                {
                    this.selColor = Main.loadPlayer[Main.numLoadPlayers].shoeColor;
                    Main.menuMode = 24;
                    Main.PlaySound(10, -1, -1, 1);
                }
                strArray1[0] = "Shirt";
                strArray1[1] = "Undershirt";
                strArray1[2] = "Pants";
                strArray1[3] = "Misc";
                num2 = 260;
                num4 = 50;
                numArray1[5] = 20;
                strArray1[5] = "Back";
                num5 = 6;
                index1 = Main.numLoadPlayers;
                num7 = Main.screenWidth / 2 - 16;
                num8 = 210;
                if (this.selectedMenu == 5)
                {
                    Main.PlaySound(11, -1, -1, 1);
                    Main.menuMode = 2;
                }
            }
            else if (Main.menuMode == 17)
            {
                index1 = Main.numLoadPlayers;
                num7 = Main.screenWidth / 2 - 16;
                num8 = 210;
                flag1 = true;
                num9 = 390;
                num2 = 260;
                num4 = 60;
                Main.loadPlayer[index1].hairColor = this.selColor;
                num5 = 3;
                strArray1[0] = "Hair " + (object)(Main.loadPlayer[index1].hair + 1);
                strArray1[1] = "Hair Color";
                flagArray1[1] = true;
                numArray1[2] = 150;
                numArray1[1] = 10;
                strArray1[2] = "Back";
                if (this.selectedMenu == 0)
                {
                    Main.PlaySound(12, -1, -1, 1);
                    ++Main.loadPlayer[index1].hair;
                    if (Main.loadPlayer[index1].hair >= 17)
                        Main.loadPlayer[index1].hair = 0;
                }
                if (this.selectedMenu == 2)
                {
                    Main.menuMode = 2;
                    Main.PlaySound(11, -1, -1, 1);
                }
            }
            else if (Main.menuMode == 18)
            {
                index1 = Main.numLoadPlayers;
                num7 = Main.screenWidth / 2 - 16;
                num8 = 210;
                flag1 = true;
                num9 = 370;
                num2 = 240;
                num4 = 60;
                Main.loadPlayer[index1].eyeColor = this.selColor;
                num5 = 3;
                strArray1[0] = "";
                strArray1[1] = "Eye Color";
                flagArray1[1] = true;
                numArray1[2] = 170;
                numArray1[1] = 10;
                strArray1[2] = "Back";
                if (this.selectedMenu == 2)
                {
                    Main.menuMode = 2;
                    Main.PlaySound(11, -1, -1, 1);
                }
            }
            else if (Main.menuMode == 19)
            {
                index1 = Main.numLoadPlayers;
                num7 = Main.screenWidth / 2 - 16;
                num8 = 210;
                flag1 = true;
                num9 = 370;
                num2 = 240;
                num4 = 60;
                Main.loadPlayer[index1].skinColor = this.selColor;
                num5 = 3;
                strArray1[0] = "";
                strArray1[1] = "Skin Color";
                flagArray1[1] = true;
                numArray1[2] = 170;
                numArray1[1] = 10;
                strArray1[2] = "Back";
                if (this.selectedMenu == 2)
                {
                    Main.menuMode = 2;
                    Main.PlaySound(11, -1, -1, 1);
                }
            }
            else if (Main.menuMode == 21)
            {
                index1 = Main.numLoadPlayers;
                num7 = Main.screenWidth / 2 - 16;
                num8 = 210;
                flag1 = true;
                num9 = 370;
                num2 = 240;
                num4 = 60;
                Main.loadPlayer[index1].shirtColor = this.selColor;
                num5 = 3;
                strArray1[0] = "";
                strArray1[1] = "Shirt Color";
                flagArray1[1] = true;
                numArray1[2] = 170;
                numArray1[1] = 10;
                strArray1[2] = "Back";
                if (this.selectedMenu == 2)
                {
                    Main.menuMode = 20;
                    Main.PlaySound(11, -1, -1, 1);
                }
            }
            else if (Main.menuMode == 22)
            {
                index1 = Main.numLoadPlayers;
                num7 = Main.screenWidth / 2 - 16;
                num8 = 210;
                flag1 = true;
                num9 = 370;
                num2 = 240;
                num4 = 60;
                Main.loadPlayer[index1].underShirtColor = this.selColor;
                num5 = 3;
                strArray1[0] = "";
                strArray1[1] = "Undershirt Color";
                flagArray1[1] = true;
                numArray1[2] = 170;
                numArray1[1] = 10;
                strArray1[2] = "Back";
                if (this.selectedMenu == 2)
                {
                    Main.menuMode = 20;
                    Main.PlaySound(11, -1, -1, 1);
                }
            }
            else if (Main.menuMode == 23)
            {
                index1 = Main.numLoadPlayers;
                num7 = Main.screenWidth / 2 - 16;
                num8 = 210;
                flag1 = true;
                num9 = 370;
                num2 = 240;
                num4 = 60;
                Main.loadPlayer[index1].pantsColor = this.selColor;
                num5 = 3;
                strArray1[0] = "";
                strArray1[1] = "Pants Color";
                flagArray1[1] = true;
                numArray1[2] = 170;
                numArray1[1] = 10;
                strArray1[2] = "Back";
                if (this.selectedMenu == 2)
                {
                    Main.menuMode = 20;
                    Main.PlaySound(11, -1, -1, 1);
                }
            }
            else if (Main.menuMode == 24)
            {
                index1 = Main.numLoadPlayers;
                num7 = Main.screenWidth / 2 - 16;
                num8 = 210;
                flag1 = true;
                num9 = 370;
                num2 = 240;
                num4 = 60;
                Main.loadPlayer[index1].shoeColor = this.selColor;
                num5 = 3;
                strArray1[0] = "";
                strArray1[1] = "Misc Color";
                flagArray1[1] = true;
                numArray1[2] = 170;
                numArray1[1] = 10;
                strArray1[2] = "Back";
                if (this.selectedMenu == 2)
                {
                    Main.menuMode = 20;
                    Main.PlaySound(11, -1, -1, 1);
                }
            }
            else if (Main.menuMode == 3)
            {
                string str = Main.loadPlayer[Main.numLoadPlayers].name;
                Main.loadPlayer[Main.numLoadPlayers].name = Main.GetInputText(Main.loadPlayer[Main.numLoadPlayers].name);
                if (Main.loadPlayer[Main.numLoadPlayers].name.Length > Player.nameLen)
                    Main.loadPlayer[Main.numLoadPlayers].name = Main.loadPlayer[Main.numLoadPlayers].name.Substring(0, Player.nameLen);
                if (str != Main.loadPlayer[Main.numLoadPlayers].name)
                    Main.PlaySound(12, -1, -1, 1);
                strArray1[0] = "Enter Character Name:";
                flagArray2[2] = true;
                if (Main.loadPlayer[Main.numLoadPlayers].name != "")
                {
                    if (Main.loadPlayer[Main.numLoadPlayers].name.Substring(0, 1) == " ")
                        Main.loadPlayer[Main.numLoadPlayers].name = "";
                    for (int startIndex = 0; startIndex < Main.loadPlayer[Main.numLoadPlayers].name.Length; ++startIndex)
                    {
                        if (Main.loadPlayer[Main.numLoadPlayers].name.Substring(startIndex, 1) != " ")
                            flagArray2[2] = false;
                    }
                }
                ++this.textBlinkerCount;
                if (this.textBlinkerCount >= 20)
                {
                    this.textBlinkerState = this.textBlinkerState != 0 ? 0 : 1;
                    this.textBlinkerCount = 0;
                }
                strArray1[1] = Main.loadPlayer[Main.numLoadPlayers].name;
                if (this.textBlinkerState == 1)
                {
                    string[] strArray2;
                    (strArray2 = strArray1)[1] = strArray2[1] + "|";
                    numArray2[1] = 1;
                }
                else
                {
                    string[] strArray2;
                    (strArray2 = strArray1)[1] = strArray2[1] + " ";
                }
                flagArray1[0] = true;
                flagArray1[1] = true;
                numArray1[1] = -20;
                numArray1[2] = 20;
                strArray1[2] = "Accept";
                strArray1[3] = "Back";
                num5 = 4;
                if (this.selectedMenu == 3)
                {
                    Main.PlaySound(11, -1, -1, 1);
                    Main.menuMode = 2;
                }
                if (this.selectedMenu == 2 || !flagArray2[2] && Main.inputTextEnter)
                {
                    Main.loadPlayerPath[Main.numLoadPlayers] = Main.nextLoadPlayer();
                    Player.SavePlayer(Main.loadPlayer[Main.numLoadPlayers], Main.loadPlayerPath[Main.numLoadPlayers]);
                    Main.LoadPlayers();
                    Main.PlaySound(10, -1, -1, 1);
                    Main.menuMode = 1;
                }
            }
            else if (Main.menuMode == 4)
            {
                num2 = 220;
                num4 = 60;
                strArray1[5] = "Back";
                for (int index2 = 0; index2 < 5; ++index2)
                    strArray1[index2] = index2 >= Main.numLoadPlayers ? (string)null : Main.loadPlayer[index2].name;
                num5 = 6;
                if (this.focusMenu >= 0 && this.focusMenu < Main.numLoadPlayers)
                {
                    index1 = this.focusMenu;
                    origin = Main.fontDeathText.MeasureString(strArray1[index1]);
                    num7 = (int)((double)(Main.screenWidth / 2) + (double)origin.X * 0.5 + 10.0);
                    num8 = num2 + num4 * this.focusMenu + 4;
                }
                if (this.selectedMenu == 5)
                {
                    Main.PlaySound(11, -1, -1, 1);
                    Main.menuMode = 1;
                }
                else if (this.selectedMenu >= 0)
                {
                    this.selectedPlayer = this.selectedMenu;
                    Main.PlaySound(10, -1, -1, 1);
                    Main.menuMode = 5;
                }
            }
            else if (Main.menuMode == 5)
            {
                strArray1[0] = "Delete " + Main.loadPlayer[this.selectedPlayer].name + "?";
                flagArray1[0] = true;
                strArray1[1] = "Yes";
                strArray1[2] = "No";
                num5 = 3;
                if (this.selectedMenu == 1)
                {
                    Main.ErasePlayer(this.selectedPlayer);
                    Main.PlaySound(10, -1, -1, 1);
                    Main.menuMode = 1;
                }
                else if (this.selectedMenu == 2)
                {
                    Main.PlaySound(11, -1, -1, 1);
                    Main.menuMode = 1;
                }
            }
            else if (Main.menuMode == 6)
            {
                num2 = 190;
                num4 = 50;
                strArray1[5] = "Create World";
                strArray1[6] = "Delete";
                if (Main.numLoadWorlds == 5)
                {
                    flagArray2[5] = true;
                    strArray1[5] = "";
                }
                else if (Main.numLoadWorlds == 0)
                {
                    flagArray2[6] = true;
                    strArray1[6] = "";
                }
                strArray1[7] = "Back";
                for (int index2 = 0; index2 < 5; ++index2)
                    strArray1[index2] = index2 >= Main.numLoadWorlds ? (string)null : Main.loadWorld[index2];
                num5 = 8;
                if (this.selectedMenu == 7)
                {
                    Main.menuMode = !Main.menuMultiplayer ? 1 : 12;
                    Main.PlaySound(11, -1, -1, 1);
                }
                else if (this.selectedMenu == 5)
                {
                    Main.PlaySound(10, -1, -1, 1);
                    Main.menuMode = 16;
                    Main.newWorldName = "World " + (object)(Main.numLoadWorlds + 1);
                }
                else if (this.selectedMenu == 6)
                {
                    Main.PlaySound(10, -1, -1, 1);
                    Main.menuMode = 8;
                }
                else if (this.selectedMenu >= 0)
                {
                    if (Main.menuMultiplayer)
                    {
                        Main.PlaySound(10, -1, -1, 1);
                        Main.worldPathName = Main.loadWorldPath[this.selectedMenu];
                        Main.menuMode = 30;
                    }
                    else
                    {
                        Main.PlaySound(10, -1, -1, 1);
                        Main.worldPathName = Main.loadWorldPath[this.selectedMenu];
                        WorldGen.playWorld();
                        Main.menuMode = 10;
                    }
                }
            }
            else if (Main.menuMode == 7)
            {
                string str = Main.newWorldName;
                Main.newWorldName = Main.GetInputText(Main.newWorldName);
                if (Main.newWorldName.Length > 20)
                    Main.newWorldName = Main.newWorldName.Substring(0, 20);
                if (str != Main.newWorldName)
                    Main.PlaySound(12, -1, -1, 1);
                strArray1[0] = "Enter World Name:";
                flagArray2[2] = true;
                if (Main.newWorldName != "")
                {
                    if (Main.newWorldName.Substring(0, 1) == " ")
                        Main.newWorldName = "";
                    for (int index2 = 0; index2 < Main.newWorldName.Length; ++index2)
                    {
                        if (Main.newWorldName != " ")
                            flagArray2[2] = false;
                    }
                }
                ++this.textBlinkerCount;
                if (this.textBlinkerCount >= 20)
                {
                    this.textBlinkerState = this.textBlinkerState != 0 ? 0 : 1;
                    this.textBlinkerCount = 0;
                }
                strArray1[1] = Main.newWorldName;
                if (this.textBlinkerState == 1)
                {
                    string[] strArray2;
                    (strArray2 = strArray1)[1] = strArray2[1] + "|";
                    numArray2[1] = 1;
                }
                else
                {
                    string[] strArray2;
                    (strArray2 = strArray1)[1] = strArray2[1] + " ";
                }
                flagArray1[0] = true;
                flagArray1[1] = true;
                numArray1[1] = -20;
                numArray1[2] = 20;
                strArray1[2] = "Accept";
                strArray1[3] = "Back";
                num5 = 4;
                if (this.selectedMenu == 3)
                {
                    Main.PlaySound(11, -1, -1, 1);
                    Main.menuMode = 16;
                }
                if (this.selectedMenu == 2 || !flagArray2[2] && Main.inputTextEnter)
                {
                    Main.menuMode = 10;
                    Main.worldName = Main.newWorldName;
                    Main.worldPathName = Main.nextLoadWorld();
                    WorldGen.CreateNewWorld();
                }
            }
            else if (Main.menuMode == 8)
            {
                num2 = 220;
                num4 = 60;
                strArray1[5] = "Back";
                for (int index2 = 0; index2 < 5; ++index2)
                    strArray1[index2] = index2 >= Main.numLoadWorlds ? (string)null : Main.loadWorld[index2];
                num5 = 6;
                if (this.selectedMenu == 5)
                {
                    Main.PlaySound(11, -1, -1, 1);
                    Main.menuMode = 1;
                }
                else if (this.selectedMenu >= 0)
                {
                    this.selectedWorld = this.selectedMenu;
                    Main.PlaySound(10, -1, -1, 1);
                    Main.menuMode = 9;
                }
            }
            else if (Main.menuMode == 9)
            {
                strArray1[0] = "Delete " + Main.loadWorld[this.selectedWorld] + "?";
                flagArray1[0] = true;
                strArray1[1] = "Yes";
                strArray1[2] = "No";
                num5 = 3;
                if (this.selectedMenu == 1)
                {
                    Main.EraseWorld(this.selectedWorld);
                    Main.PlaySound(10, -1, -1, 1);
                    Main.menuMode = 6;
                }
                else if (this.selectedMenu == 2)
                {
                    Main.PlaySound(11, -1, -1, 1);
                    Main.menuMode = 6;
                }
            }
            else if (Main.menuMode == 1111)
            {
                num4 = 60;
                numArray1[4] = 10;
                num5 = 8;
                strArray1[0] = !this.graphics.IsFullScreen ? "Go Fullscreen" : "Go Windowed";
                this.bgScroll = (int)Math.Round((1.0 - (double)Main.caveParrallax) * 500.0);
                strArray1[1] = "Resolution";
                strArray1[2] = "Parallax";
                strArray1[3] = !Main.fixedTiming ? "Frame Skip On" : "Frame Skip Off";
                strArray1[4] = "Back";
                if (this.selectedMenu == 4)
                {
                    Main.PlaySound(11, -1, -1, 1);
                    this.SaveSettings();
                    Main.menuMode = 11;
                }
                if (this.selectedMenu == 3)
                {
                    Main.PlaySound(12, -1, -1, 1);
                    Main.fixedTiming = !Main.fixedTiming && true;
                }
                if (this.selectedMenu == 2)
                {
                    Main.PlaySound(11, -1, -1, 1);
                    Main.menuMode = 28;
                }
                if (this.selectedMenu == 1)
                {
                    Main.PlaySound(10, -1, -1, 1);
                    Main.menuMode = 111;
                }
                if (this.selectedMenu == 0)
                    this.graphics.ToggleFullScreen();
            }
            else if (Main.menuMode == 11)
            {
                num2 = 190;
                num4 = 50;
                numArray1[6] = 10;
                num5 = 8;
                strArray1[0] = "Video";
                strArray1[1] = "Cursor Color";
                strArray1[2] = "Volume";
                strArray1[3] = "Controls";
                strArray1[4] = !Main.autoSave ? "Autosave Off" : "Autosave On";
                strArray1[5] = !Main.autoPause ? "Autopause Off" : "Autopause On";
                strArray1[6] = "Back";
                if (this.selectedMenu == 6)
                {
                    Main.PlaySound(11, -1, -1, 1);
                    this.SaveSettings();
                    Main.menuMode = 0;
                }
                if (this.selectedMenu == 5)
                {
                    Main.PlaySound(12, -1, -1, 1);
                    Main.autoPause = !Main.autoPause && true;
                }
                if (this.selectedMenu == 4)
                {
                    Main.PlaySound(12, -1, -1, 1);
                    Main.autoSave = !Main.autoSave && true;
                }
                if (this.selectedMenu == 3)
                {
                    Main.PlaySound(11, -1, -1, 1);
                    Main.menuMode = 27;
                }
                if (this.selectedMenu == 2)
                {
                    Main.PlaySound(11, -1, -1, 1);
                    Main.menuMode = 26;
                }
                if (this.selectedMenu == 1)
                {
                    Main.PlaySound(10, -1, -1, 1);
                    this.selColor = Main.mouseColor;
                    Main.menuMode = 25;
                }
                if (this.selectedMenu == 0)
                {
                    Main.PlaySound(10, -1, -1, 1);
                    Main.menuMode = 1111;
                }
            }
            else if (Main.menuMode == 111)
            {
                num2 = 240;
                num4 = 60;
                num5 = 3;
                strArray1[0] = "Fullscreen Resolution";
                strArray1[1] = (string)(object)this.graphics.PreferredBackBufferWidth + (object)"x" + (string)(object)this.graphics.PreferredBackBufferHeight;
                flagArray1[0] = true;
                numArray1[2] = 170;
                numArray1[1] = 10;
                strArray1[2] = "Back";
                if (this.selectedMenu == 1)
                {
                    Main.PlaySound(12, -1, -1, 1);
                    int num10 = 0;
                    for (int index2 = 0; index2 < this.numDisplayModes; ++index2)
                    {
                        if (this.displayWidth[index2] == this.graphics.PreferredBackBufferWidth && this.displayHeight[index2] == this.graphics.PreferredBackBufferHeight)
                        {
                            num10 = index2;
                            break;
                        }
                    }
                    int index3 = num10 + 1;
                    if (index3 >= this.numDisplayModes)
                        index3 = 0;
                    this.graphics.PreferredBackBufferWidth = this.displayWidth[index3];
                    this.graphics.PreferredBackBufferHeight = this.displayHeight[index3];
                }
                if (this.selectedMenu == 2)
                {
                    if (this.graphics.IsFullScreen)
                        this.graphics.ApplyChanges();
                    Main.menuMode = 1111;
                    Main.PlaySound(11, -1, -1, 1);
                }
            }
            else if (Main.menuMode == 25)
            {
                flag1 = true;
                num9 = 370;
                num2 = 240;
                num4 = 60;
                Main.mouseColor = this.selColor;
                num5 = 3;
                strArray1[0] = "";
                strArray1[1] = "Cursor Color";
                flagArray1[1] = true;
                numArray1[2] = 170;
                numArray1[1] = 10;
                strArray1[2] = "Back";
                if (this.selectedMenu == 2)
                {
                    Main.menuMode = 11;
                    Main.PlaySound(11, -1, -1, 1);
                }
            }
            else if (Main.menuMode == 26)
            {
                flag2 = true;
                num2 = 240;
                num4 = 60;
                num5 = 3;
                strArray1[0] = "";
                strArray1[1] = "Volume";
                flagArray1[1] = true;
                numArray1[2] = 170;
                numArray1[1] = 10;
                strArray1[2] = "Back";
                if (this.selectedMenu == 2)
                {
                    Main.menuMode = 11;
                    Main.PlaySound(11, -1, -1, 1);
                }
            }
            else if (Main.menuMode == 28)
            {
                Main.caveParrallax = (float)(1.0 - (double)this.bgScroll / 500.0);
                flag3 = true;
                num2 = 240;
                num4 = 60;
                num5 = 3;
                strArray1[0] = "";
                strArray1[1] = "Parallax";
                flagArray1[1] = true;
                numArray1[2] = 170;
                numArray1[1] = 10;
                strArray1[2] = "Back";
                if (this.selectedMenu == 2)
                {
                    Main.menuMode = 1111;
                    Main.PlaySound(11, -1, -1, 1);
                }
            }
            else if (Main.menuMode == 27)
            {
                num2 = 180;
                num4 = 50;
                num5 = 8;
                string[] strArray2 = new string[7]
        {
          Main.cUp,
          Main.cDown,
          Main.cLeft,
          Main.cRight,
          Main.cJump,
          Main.cThrowItem,
          Main.cInv
        };
                if (this.setKey >= 0)
                    strArray2[this.setKey] = "_";
                strArray1[0] = "Up........." + strArray2[0];
                strArray1[1] = "Down......." + strArray2[1];
                strArray1[2] = "Left......." + strArray2[2];
                strArray1[3] = "Right......" + strArray2[3];
                strArray1[4] = "Jump......." + strArray2[4];
                strArray1[5] = "Throw......" + strArray2[5];
                strArray1[6] = "Inventory.." + strArray2[6];
                numArray1[7] = 10;
                strArray1[7] = "Back";
                if (this.selectedMenu == 7)
                {
                    Main.menuMode = 11;
                    Main.PlaySound(11, -1, -1, 1);
                }
                else if (this.selectedMenu >= 0)
                    this.setKey = this.selectedMenu;
                if (this.setKey >= 0)
                {
                    Keys[] pressedKeys = Main.keyState.GetPressedKeys();
                    if (pressedKeys.Length > 0)
                    {
                        string str = string.Concat((object)pressedKeys[0]);
                        if (this.setKey == 0)
                            Main.cUp = str;
                        if (this.setKey == 1)
                            Main.cDown = str;
                        if (this.setKey == 2)
                            Main.cLeft = str;
                        if (this.setKey == 3)
                            Main.cRight = str;
                        if (this.setKey == 4)
                            Main.cJump = str;
                        if (this.setKey == 5)
                            Main.cThrowItem = str;
                        if (this.setKey == 6)
                            Main.cInv = str;
                        this.setKey = -1;
                    }
                }
            }
            else if (Main.menuMode == 12)
            {
                Main.menuServer = false;
                strArray1[0] = "Join";
                strArray1[1] = "Host & Play";
                strArray1[2] = "Back";
                if (this.selectedMenu == 0)
                {
                    Main.LoadPlayers();
                    Main.menuMultiplayer = true;
                    Main.PlaySound(10, -1, -1, 1);
                    Main.menuMode = 1;
                }
                else if (this.selectedMenu == 1)
                {
                    Main.LoadPlayers();
                    Main.PlaySound(10, -1, -1, 1);
                    Main.menuMode = 1;
                    Main.menuMultiplayer = true;
                    Main.menuServer = true;
                }
                if (this.selectedMenu == 2)
                {
                    Main.PlaySound(11, -1, -1, 1);
                    Main.menuMode = 0;
                }
                num5 = 3;
            }
            else if (Main.menuMode == 13)
            {
                string str = Main.getIP;
                Main.getIP = Main.GetInputText(Main.getIP);
                if (str != Main.getIP)
                    Main.PlaySound(12, -1, -1, 1);
                strArray1[0] = "Enter Server IP Address:";
                flagArray2[2] = true;
                if (Main.getIP != "")
                {
                    if (Main.getIP.Substring(0, 1) == " ")
                        Main.getIP = "";
                    for (int index2 = 0; index2 < Main.getIP.Length; ++index2)
                    {
                        if (Main.getIP != " ")
                            flagArray2[2] = false;
                    }
                }
                ++this.textBlinkerCount;
                if (this.textBlinkerCount >= 20)
                {
                    this.textBlinkerState = this.textBlinkerState != 0 ? 0 : 1;
                    this.textBlinkerCount = 0;
                }
                strArray1[1] = Main.getIP;
                if (this.textBlinkerState == 1)
                {
                    string[] strArray2;
                    (strArray2 = strArray1)[1] = strArray2[1] + "|";
                    numArray2[1] = 1;
                }
                else
                {
                    string[] strArray2;
                    (strArray2 = strArray1)[1] = strArray2[1] + " ";
                }
                flagArray1[0] = true;
                flagArray1[1] = true;
                numArray1[1] = -20;
                numArray1[2] = 20;
                strArray1[2] = "Accept";
                strArray1[3] = "Back";
                num5 = 4;
                if (this.selectedMenu == 3)
                {
                    Main.PlaySound(11, -1, -1, 1);
                    Main.menuMode = 1;
                }
                if (this.selectedMenu == 2 || !flagArray2[2] && Main.inputTextEnter)
                {
                    Main.PlaySound(12, -1, -1, 1);
                    Main.menuMode = 131;
                }
            }
            else if (Main.menuMode == 131)
            {
                int num10 = 7777;
                string str = Main.getPort;
                Main.getPort = Main.GetInputText(Main.getPort);
                if (str != Main.getPort)
                    Main.PlaySound(12, -1, -1, 1);
                strArray1[0] = "Enter Server Port:";
                flagArray2[2] = true;
                if (Main.getPort != "")
                {
                    bool flag4 = false;
                    try
                    {
                        num10 = Convert.ToInt32(Main.getPort);
                        if (num10 > 0 && num10 <= (int)ushort.MaxValue)
                            flag4 = true;
                    }
                    catch
                    {
                    }
                    if (flag4)
                        flagArray2[2] = false;
                }
                ++this.textBlinkerCount;
                if (this.textBlinkerCount >= 20)
                {
                    this.textBlinkerState = this.textBlinkerState != 0 ? 0 : 1;
                    this.textBlinkerCount = 0;
                }
                strArray1[1] = Main.getPort;
                if (this.textBlinkerState == 1)
                {
                    string[] strArray2;
                    (strArray2 = strArray1)[1] = strArray2[1] + "|";
                    numArray2[1] = 1;
                }
                else
                {
                    string[] strArray2;
                    (strArray2 = strArray1)[1] = strArray2[1] + " ";
                }
                flagArray1[0] = true;
                flagArray1[1] = true;
                numArray1[1] = -20;
                numArray1[2] = 20;
                strArray1[2] = "Accept";
                strArray1[3] = "Back";
                num5 = 4;
                if (this.selectedMenu == 3)
                {
                    Main.PlaySound(11, -1, -1, 1);
                    Main.menuMode = 1;
                }
                if (this.selectedMenu == 2 || !flagArray2[2] && Main.inputTextEnter)
                {
                    Netplay.serverPort = num10;
                    Main.autoPass = false;
                    if (Netplay.SetIP(Main.getIP))
                    {
                        Main.menuMode = 10;
                        Netplay.StartClient();
                    }
                    else if (Netplay.SetIP2(Main.getIP))
                    {
                        Main.menuMode = 10;
                        Netplay.StartClient();
                    }
                }
            }
            else if (Main.menuMode == 16)
            {
                num2 = 200;
                num4 = 60;
                numArray1[1] = 30;
                numArray1[2] = 30;
                numArray1[3] = 30;
                numArray1[4] = 70;
                strArray1[0] = "Choose world size:";
                flagArray1[0] = true;
                strArray1[1] = "Small";
                strArray1[2] = "Medium";
                strArray1[3] = "Large";
                strArray1[4] = "Back";
                num5 = 5;
                if (this.selectedMenu == 4)
                {
                    Main.menuMode = 6;
                    Main.PlaySound(11, -1, -1, 1);
                }
                else if (this.selectedMenu > 0)
                {
                    if (this.selectedMenu == 1)
                    {
                        Main.maxTilesX = 4200;
                        Main.maxTilesY = 1200;
                    }
                    else if (this.selectedMenu == 2)
                    {
                        Main.maxTilesX = 6300;
                        Main.maxTilesY = 1800;
                    }
                    else
                    {
                        Main.maxTilesX = 8400;
                        Main.maxTilesY = 2400;
                    }
                    Main.menuMode = 7;
                    Main.PlaySound(10, -1, -1, 1);
                    WorldGen.setWorldSize();
                }
            }
            if (Main.menuMode != num6)
            {
                num5 = 0;
                for (int index2 = 0; index2 < Main.maxMenuItems; ++index2)
                    this.menuItemScale[index2] = 0.8f;
            }
            int num11 = this.focusMenu;
            this.selectedMenu = -1;
            this.focusMenu = -1;
            Color color2;
            for (int index2 = 0; index2 < num5; ++index2)
            {
                if (strArray1[index2] != null)
                {
                    if (flag1)
                    {
                        string text1 = "";
                        for (int index3 = 0; index3 < 6; ++index3)
                        {
                            int num10 = num9;
                            int num12 = 370 + Main.screenWidth / 2 - 400;
                            if (index3 == 0)
                                text1 = "Red:";
                            if (index3 == 1)
                            {
                                text1 = "Green:";
                                num10 += 30;
                            }
                            if (index3 == 2)
                            {
                                text1 = "Blue:";
                                num10 += 60;
                            }
                            if (index3 == 3)
                            {
                                text1 = string.Concat((object)this.selColor.R);
                                num12 += 90;
                            }
                            if (index3 == 4)
                            {
                                text1 = string.Concat((object)this.selColor.G);
                                num12 += 90;
                                num10 += 30;
                            }
                            if (index3 == 5)
                            {
                                text1 = string.Concat((object)this.selColor.B);
                                num12 += 90;
                                num10 += 60;
                            }
                            for (int index4 = 0; index4 < 5; ++index4)
                            {
                                color2 = Color.Black;
                                if (index4 == 4)
                                {
                                    color2 = color1;
                                    color2.R = (byte)(((int)byte.MaxValue + (int)color2.R) / 2);
                                    color2.G = (byte)(((int)byte.MaxValue + (int)color2.R) / 2);
                                    color2.B = (byte)(((int)byte.MaxValue + (int)color2.R) / 2);
                                }
                                int num13 = (int)byte.MaxValue;
                                int num14 = (int)color2.R - ((int)byte.MaxValue - num13);
                                if (num14 < 0)
                                    num14 = 0;
                                color2 = new Color((int)(byte)num14, (int)(byte)num14, (int)(byte)num14, (int)(byte)num13);
                                int num15 = 0;
                                int num16 = 0;
                                if (index4 == 0)
                                    num15 = -2;
                                if (index4 == 1)
                                    num15 = 2;
                                if (index4 == 2)
                                    num16 = -2;
                                if (index4 == 3)
                                    num16 = 2;
                                this.spriteBatch.DrawString(Main.fontDeathText, text1, new Vector2((float)(num12 + num15), (float)(num10 + num16)), color2, 0.0f, new Vector2(), 0.5f, SpriteEffects.None, 0.0f);
                            }
                        }
                        bool flag4 = false;
                        for (int index3 = 0; index3 < 2; ++index3)
                        {
                            for (int index4 = 0; index4 < 3; ++index4)
                            {
                                int num10 = num9 + index4 * 30 - 12;
                                int num12 = 360 + Main.screenWidth / 2 - 400;
                                float scale = 0.9f;
                                int num13;
                                if (index3 == 0)
                                {
                                    num13 = num12 - 70;
                                    num10 += 2;
                                }
                                else
                                    num13 = num12 - 40;
                                string text2 = "-";
                                if (index3 == 1)
                                    text2 = "+";
                                Vector2 vector2 = new Vector2(24f, 24f);
                                int num14 = 142;
                                if (Main.mouseState.X > num13 && (double)Main.mouseState.X < (double)num13 + (double)vector2.X && Main.mouseState.Y > num10 + 13 && (double)Main.mouseState.Y < (double)(num10 + 13) + (double)vector2.Y)
                                {
                                    if (this.focusColor != (index3 + 1) * (index4 + 10))
                                        Main.PlaySound(12, -1, -1, 1);
                                    this.focusColor = (index3 + 1) * (index4 + 10);
                                    flag4 = true;
                                    num14 = (int)byte.MaxValue;
                                    if (Main.mouseState.LeftButton == ButtonState.Pressed)
                                    {
                                        if (this.colorDelay <= 1)
                                        {
                                            this.colorDelay = this.colorDelay != 0 ? 3 : 40;
                                            int num15 = index3;
                                            if (index3 == 0)
                                            {
                                                num15 = -1;
                                                if ((int)this.selColor.R + (int)this.selColor.G + (int)this.selColor.B < (int)byte.MaxValue)
                                                    num15 = 0;
                                            }
                                            if (index4 == 0 && ((int)this.selColor.R + num15 >= 0 && (int)this.selColor.R + num15 <= (int)byte.MaxValue))
                                                this.selColor.R = (byte)((uint)this.selColor.R + (uint)num15);
                                            if (index4 == 1 && ((int)this.selColor.G + num15 >= 0 && (int)this.selColor.G + num15 <= (int)byte.MaxValue))
                                                this.selColor.G = (byte)((uint)this.selColor.G + (uint)num15);
                                            if (index4 == 2 && ((int)this.selColor.B + num15 >= 0 && (int)this.selColor.B + num15 <= (int)byte.MaxValue))
                                                this.selColor.B = (byte)((uint)this.selColor.B + (uint)num15);
                                        }
                                        --this.colorDelay;
                                    }
                                    else
                                        this.colorDelay = 0;
                                }
                                for (int index5 = 0; index5 < 5; ++index5)
                                {
                                    color2 = Color.Black;
                                    if (index5 == 4)
                                    {
                                        color2 = color1;
                                        color2.R = (byte)(((int)byte.MaxValue + (int)color2.R) / 2);
                                        color2.G = (byte)(((int)byte.MaxValue + (int)color2.R) / 2);
                                        color2.B = (byte)(((int)byte.MaxValue + (int)color2.R) / 2);
                                    }
                                    int num15 = (int)color2.R - ((int)byte.MaxValue - num14);
                                    if (num15 < 0)
                                        num15 = 0;
                                    color2 = new Color((int)(byte)num15, (int)(byte)num15, (int)(byte)num15, (int)(byte)num14);
                                    int num16 = 0;
                                    int num17 = 0;
                                    if (index5 == 0)
                                        num16 = -2;
                                    if (index5 == 1)
                                        num16 = 2;
                                    if (index5 == 2)
                                        num17 = -2;
                                    if (index5 == 3)
                                        num17 = 2;
                                    this.spriteBatch.DrawString(Main.fontDeathText, text2, new Vector2((float)(num13 + num16), (float)(num10 + num17)), color2, 0.0f, new Vector2(), scale, SpriteEffects.None, 0.0f);
                                }
                            }
                        }
                        if (!flag4)
                        {
                            this.focusColor = 0;
                            this.colorDelay = 0;
                        }
                    }
                    Vector2 vector2_1;
                    if (flag3)
                    {
                        int num10 = 400;
                        string text1 = "";
                        for (int index3 = 0; index3 < 4; ++index3)
                        {
                            int num12 = num10;
                            int num13 = 370 + Main.screenWidth / 2 - 400;
                            if (index3 == 0)
                                text1 = "Parallax: " + (object)this.bgScroll;
                            for (int index4 = 0; index4 < 5; ++index4)
                            {
                                color2 = Color.Black;
                                if (index4 == 4)
                                {
                                    color2 = color1;
                                    color2.R = (byte)(((int)byte.MaxValue + (int)color2.R) / 2);
                                    color2.G = (byte)(((int)byte.MaxValue + (int)color2.R) / 2);
                                    color2.B = (byte)(((int)byte.MaxValue + (int)color2.R) / 2);
                                }
                                int num14 = (int)byte.MaxValue;
                                int num15 = (int)color2.R - ((int)byte.MaxValue - num14);
                                if (num15 < 0)
                                    num15 = 0;
                                color2 = new Color((int)(byte)num15, (int)(byte)num15, (int)(byte)num15, (int)(byte)num14);
                                int num16 = 0;
                                int num17 = 0;
                                if (index4 == 0)
                                    num16 = -2;
                                if (index4 == 1)
                                    num16 = 2;
                                if (index4 == 2)
                                    num17 = -2;
                                if (index4 == 3)
                                    num17 = 2;
                                this.spriteBatch.DrawString(Main.fontDeathText, text1, new Vector2((float)(num13 + num16), (float)(num12 + num17)), color2, 0.0f, new Vector2(), 0.5f, SpriteEffects.None, 0.0f);
                            }
                        }
                        bool flag4 = false;
                        for (int index3 = 0; index3 < 2; ++index3)
                        {
                            for (int index4 = 0; index4 < 1; ++index4)
                            {
                                int num12 = num10 + index4 * 30 - 12;
                                int num13 = 360 + Main.screenWidth / 2 - 400;
                                float scale = 0.9f;
                                int num14;
                                if (index3 == 0)
                                {
                                    num14 = num13 - 70;
                                    num12 += 2;
                                }
                                else
                                    num14 = num13 - 40;
                                string text2 = "-";
                                if (index3 == 1)
                                    text2 = "+";
                                vector2_1 = new Vector2(24f, 24f);
                                int num15 = 142;
                                if (Main.mouseState.X > num14 && (double)Main.mouseState.X < (double)num14 + (double)vector2_1.X && Main.mouseState.Y > num12 + 13 && (double)Main.mouseState.Y < (double)(num12 + 13) + (double)vector2_1.Y)
                                {
                                    if (this.focusColor != (index3 + 1) * (index4 + 10))
                                        Main.PlaySound(12, -1, -1, 1);
                                    this.focusColor = (index3 + 1) * (index4 + 10);
                                    flag4 = true;
                                    num15 = (int)byte.MaxValue;
                                    if (Main.mouseState.LeftButton == ButtonState.Pressed)
                                    {
                                        if (this.colorDelay <= 1)
                                        {
                                            this.colorDelay = this.colorDelay != 0 ? 3 : 40;
                                            int num16 = index3;
                                            if (index3 == 0)
                                                num16 = -1;
                                            if (index4 == 0)
                                            {
                                                this.bgScroll += num16;
                                                if (this.bgScroll > 100)
                                                    this.bgScroll = 100;
                                                if (this.bgScroll < 0)
                                                    this.bgScroll = 0;
                                            }
                                        }
                                        --this.colorDelay;
                                    }
                                    else
                                        this.colorDelay = 0;
                                }
                                for (int index5 = 0; index5 < 5; ++index5)
                                {
                                    color2 = Color.Black;
                                    if (index5 == 4)
                                    {
                                        color2 = color1;
                                        color2.R = (byte)(((int)byte.MaxValue + (int)color2.R) / 2);
                                        color2.G = (byte)(((int)byte.MaxValue + (int)color2.R) / 2);
                                        color2.B = (byte)(((int)byte.MaxValue + (int)color2.R) / 2);
                                    }
                                    int num16 = (int)color2.R - ((int)byte.MaxValue - num15);
                                    if (num16 < 0)
                                        num16 = 0;
                                    color2 = new Color((int)(byte)num16, (int)(byte)num16, (int)(byte)num16, (int)(byte)num15);
                                    int num17 = 0;
                                    int num18 = 0;
                                    if (index5 == 0)
                                        num17 = -2;
                                    if (index5 == 1)
                                        num17 = 2;
                                    if (index5 == 2)
                                        num18 = -2;
                                    if (index5 == 3)
                                        num18 = 2;
                                    this.spriteBatch.DrawString(Main.fontDeathText, text2, new Vector2((float)(num14 + num17), (float)(num12 + num18)), color2, 0.0f, new Vector2(), scale, SpriteEffects.None, 0.0f);
                                }
                            }
                        }
                        if (!flag4)
                        {
                            this.focusColor = 0;
                            this.colorDelay = 0;
                        }
                    }
                    if (flag2)
                    {
                        int num10 = 400;
                        string text1 = "";
                        for (int index3 = 0; index3 < 4; ++index3)
                        {
                            int num12 = num10;
                            int num13 = 370 + Main.screenWidth / 2 - 400;
                            if (index3 == 0)
                                text1 = "Sound:";
                            if (index3 == 1)
                            {
                                text1 = "Music:";
                                num12 += 30;
                            }
                            if (index3 == 2)
                            {
                                text1 = (string)(object)Math.Round((double)Main.soundVolume * 100.0) + (object)"%";
                                num13 += 90;
                            }
                            if (index3 == 3)
                            {
                                text1 = (string)(object)Math.Round((double)Main.musicVolume * 100.0) + (object)"%";
                                num13 += 90;
                                num12 += 30;
                            }
                            for (int index4 = 0; index4 < 5; ++index4)
                            {
                                color2 = Color.Black;
                                if (index4 == 4)
                                {
                                    color2 = color1;
                                    color2.R = (byte)(((int)byte.MaxValue + (int)color2.R) / 2);
                                    color2.G = (byte)(((int)byte.MaxValue + (int)color2.R) / 2);
                                    color2.B = (byte)(((int)byte.MaxValue + (int)color2.R) / 2);
                                }
                                int num14 = (int)byte.MaxValue;
                                int num15 = (int)color2.R - ((int)byte.MaxValue - num14);
                                if (num15 < 0)
                                    num15 = 0;
                                color2 = new Color((int)(byte)num15, (int)(byte)num15, (int)(byte)num15, (int)(byte)num14);
                                int num16 = 0;
                                int num17 = 0;
                                if (index4 == 0)
                                    num16 = -2;
                                if (index4 == 1)
                                    num16 = 2;
                                if (index4 == 2)
                                    num17 = -2;
                                if (index4 == 3)
                                    num17 = 2;
                                this.spriteBatch.DrawString(Main.fontDeathText, text1, new Vector2((float)(num13 + num16), (float)(num12 + num17)), color2, 0.0f, new Vector2(), 0.5f, SpriteEffects.None, 0.0f);
                            }
                        }
                        bool flag4 = false;
                        for (int index3 = 0; index3 < 2; ++index3)
                        {
                            for (int index4 = 0; index4 < 2; ++index4)
                            {
                                int num12 = num10 + index4 * 30 - 12;
                                int num13 = 360 + Main.screenWidth / 2 - 400;
                                float scale = 0.9f;
                                int num14;
                                if (index3 == 0)
                                {
                                    num14 = num13 - 70;
                                    num12 += 2;
                                }
                                else
                                    num14 = num13 - 40;
                                string text2 = "-";
                                if (index3 == 1)
                                    text2 = "+";
                                vector2_1 = new Vector2(24f, 24f);
                                int num15 = 142;
                                if (Main.mouseState.X > num14 && (double)Main.mouseState.X < (double)num14 + (double)vector2_1.X && Main.mouseState.Y > num12 + 13 && (double)Main.mouseState.Y < (double)(num12 + 13) + (double)vector2_1.Y)
                                {
                                    if (this.focusColor != (index3 + 1) * (index4 + 10))
                                        Main.PlaySound(12, -1, -1, 1);
                                    this.focusColor = (index3 + 1) * (index4 + 10);
                                    flag4 = true;
                                    num15 = (int)byte.MaxValue;
                                    if (Main.mouseState.LeftButton == ButtonState.Pressed)
                                    {
                                        if (this.colorDelay <= 1)
                                        {
                                            this.colorDelay = this.colorDelay != 0 ? 3 : 40;
                                            int num16 = index3;
                                            if (index3 == 0)
                                                num16 = -1;
                                            if (index4 == 0)
                                            {
                                                Main.soundVolume += (float)num16 * 0.01f;
                                                if ((double)Main.soundVolume > 1.0)
                                                    Main.soundVolume = 1f;
                                                if ((double)Main.soundVolume < 0.0)
                                                    Main.soundVolume = 0.0f;
                                            }
                                            if (index4 == 1)
                                            {
                                                Main.musicVolume += (float)num16 * 0.01f;
                                                if ((double)Main.musicVolume > 1.0)
                                                    Main.musicVolume = 1f;
                                                if ((double)Main.musicVolume < 0.0)
                                                    Main.musicVolume = 0.0f;
                                            }
                                        }
                                        --this.colorDelay;
                                    }
                                    else
                                        this.colorDelay = 0;
                                }
                                for (int index5 = 0; index5 < 5; ++index5)
                                {
                                    color2 = Color.Black;
                                    if (index5 == 4)
                                    {
                                        color2 = color1;
                                        color2.R = (byte)(((int)byte.MaxValue + (int)color2.R) / 2);
                                        color2.G = (byte)(((int)byte.MaxValue + (int)color2.R) / 2);
                                        color2.B = (byte)(((int)byte.MaxValue + (int)color2.R) / 2);
                                    }
                                    int num16 = (int)color2.R - ((int)byte.MaxValue - num15);
                                    if (num16 < 0)
                                        num16 = 0;
                                    color2 = new Color((int)(byte)num16, (int)(byte)num16, (int)(byte)num16, (int)(byte)num15);
                                    int num17 = 0;
                                    int num18 = 0;
                                    if (index5 == 0)
                                        num17 = -2;
                                    if (index5 == 1)
                                        num17 = 2;
                                    if (index5 == 2)
                                        num18 = -2;
                                    if (index5 == 3)
                                        num18 = 2;
                                    this.spriteBatch.DrawString(Main.fontDeathText, text2, new Vector2((float)(num14 + num17), (float)(num12 + num18)), color2, 0.0f, new Vector2(), scale, SpriteEffects.None, 0.0f);
                                }
                            }
                        }
                        if (!flag4)
                        {
                            this.focusColor = 0;
                            this.colorDelay = 0;
                        }
                    }
                    for (int index3 = 0; index3 < 5; ++index3)
                    {
                        color2 = Color.Black;
                        if (index3 == 4)
                        {
                            color2 = color1;
                            color2.R = (byte)(((int)byte.MaxValue + (int)color2.R) / 2);
                            color2.G = (byte)(((int)byte.MaxValue + (int)color2.R) / 2);
                            color2.B = (byte)(((int)byte.MaxValue + (int)color2.R) / 2);
                        }
                        int num10 = (int)((double)byte.MaxValue * ((double)this.menuItemScale[index2] * 2.0 - 1.0));
                        if (flagArray1[index2])
                            num10 = (int)byte.MaxValue;
                        int num12 = (int)color2.R - ((int)byte.MaxValue - num10);
                        if (num12 < 0)
                            num12 = 0;
                        color2 = new Color((int)(byte)num12, (int)(byte)num12, (int)(byte)num12, (int)(byte)num10);
                        int num13 = 0;
                        int num14 = 0;
                        if (index3 == 0)
                            num13 = -2;
                        if (index3 == 1)
                            num13 = 2;
                        if (index3 == 2)
                            num14 = -2;
                        if (index3 == 3)
                            num14 = 2;
                        origin = Main.fontDeathText.MeasureString(strArray1[index2]);
                        origin.X *= 0.5f;
                        origin.Y *= 0.5f;
                        float scale = this.menuItemScale[index2];
                        if (Main.menuMode == 15 && index2 == 0)
                            scale *= 0.35f;
                        else if (Main.netMode == 2)
                            scale *= 0.5f;
                        this.spriteBatch.DrawString(Main.fontDeathText, strArray1[index2], new Vector2((float)(num3 + num13 + numArray2[index2]), (float)(num2 + num4 * index2 + num14) + origin.Y + (float)numArray1[index2]), color2, 0.0f, origin, scale, SpriteEffects.None, 0.0f);
                    }
                    if (Main.mouseState.X > num3 - strArray1[index2].Length * 10 + numArray2[index2] && Main.mouseState.X < num3 + strArray1[index2].Length * 10 + numArray2[index2] && (Main.mouseState.Y > num2 + num4 * index2 + numArray1[index2] && Main.mouseState.Y < num2 + num4 * index2 + 50 + numArray1[index2]) && Main.hasFocus)
                    {
                        this.focusMenu = index2;
                        if (flagArray1[index2] || flagArray2[index2])
                        {
                            this.focusMenu = -1;
                        }
                        else
                        {
                            if (num11 != this.focusMenu)
                                Main.PlaySound(12, -1, -1, 1);
                            if (Main.mouseLeftRelease && Main.mouseState.LeftButton == ButtonState.Pressed)
                                this.selectedMenu = index2;
                        }
                    }
                }
            }
            for (int index2 = 0; index2 < Main.maxMenuItems; ++index2)
            {
                if (index2 == this.focusMenu)
                {
                    if ((double)this.menuItemScale[index2] < 1.0)
                        this.menuItemScale[index2] += 0.02f;
                    if ((double)this.menuItemScale[index2] > 1.0)
                        this.menuItemScale[index2] = 1f;
                }
                else if ((double)this.menuItemScale[index2] > 0.8)
                    this.menuItemScale[index2] -= 0.02f;
            }
            if (index1 >= 0)
            {
                Main.loadPlayer[index1].PlayerFrame();
                Main.loadPlayer[index1].position.X = (float)num7 + Main.screenPosition.X;
                Main.loadPlayer[index1].position.Y = (float)num8 + Main.screenPosition.Y;
                this.DrawPlayer(Main.loadPlayer[index1]);
            }
            for (int index2 = 0; index2 < 5; ++index2)
            {
                color2 = Color.Black;
                if (index2 == 4)
                {
                    color2 = color1;
                    color2.R = (byte)(((int)byte.MaxValue + (int)color2.R) / 2);
                    color2.G = (byte)(((int)byte.MaxValue + (int)color2.R) / 2);
                    color2.B = (byte)(((int)byte.MaxValue + (int)color2.R) / 2);
                }
                color2.A = (byte)((double)color2.A * 0.300000011920929);
                int num10 = 0;
                int num12 = 0;
                if (index2 == 0)
                    num10 = -2;
                if (index2 == 1)
                    num10 = 2;
                if (index2 == 2)
                    num12 = -2;
                if (index2 == 3)
                    num12 = 2;
                string text = "Copyright 2011 Re-Logic";
                origin = Main.fontMouseText.MeasureString(text);
                origin.X *= 0.5f;
                origin.Y *= 0.5f;
                this.spriteBatch.DrawString(Main.fontMouseText, text, new Vector2((float)((double)Main.screenWidth - (double)origin.X + (double)num10 - 10.0), (float)((double)Main.screenHeight - (double)origin.Y + (double)num12 - 2.0)), color2, 0.0f, origin, 1f, SpriteEffects.None, 0.0f);
            }
            for (int index2 = 0; index2 < 5; ++index2)
            {
                color2 = Color.Black;
                if (index2 == 4)
                {
                    color2 = color1;
                    color2.R = (byte)(((int)byte.MaxValue + (int)color2.R) / 2);
                    color2.G = (byte)(((int)byte.MaxValue + (int)color2.R) / 2);
                    color2.B = (byte)(((int)byte.MaxValue + (int)color2.R) / 2);
                }
                color2.A = (byte)((double)color2.A * 0.300000011920929);
                int num10 = 0;
                int num12 = 0;
                if (index2 == 0)
                    num10 = -2;
                if (index2 == 1)
                    num10 = 2;
                if (index2 == 2)
                    num12 = -2;
                if (index2 == 3)
                    num12 = 2;
                origin = Main.fontMouseText.MeasureString(Main.versionNumber);
                origin.X *= 0.5f;
                origin.Y *= 0.5f;
                this.spriteBatch.DrawString(Main.fontMouseText, Main.versionNumber, new Vector2((float)((double)origin.X + (double)num10 + 10.0), (float)((double)Main.screenHeight - (double)origin.Y + (double)num12 - 2.0)), color2, 0.0f, origin, 1f, SpriteEffects.None, 0.0f);
            }
            this.spriteBatch.Draw(Main.cursorTexture, new Vector2((float)(Main.mouseState.X + 1), (float)(Main.mouseState.Y + 1)), new Rectangle?(new Rectangle(0, 0, Main.cursorTexture.Width, Main.cursorTexture.Height)), new Color((int)((double)Main.cursorColor.R * 0.200000002980232), (int)((double)Main.cursorColor.G * 0.200000002980232), (int)((double)Main.cursorColor.B * 0.200000002980232), (int)((double)Main.cursorColor.A * 0.5)), 0.0f, new Vector2(), Main.cursorScale * 1.1f, SpriteEffects.None, 0.0f);
            this.spriteBatch.Draw(Main.cursorTexture, new Vector2((float)Main.mouseState.X, (float)Main.mouseState.Y), new Rectangle?(new Rectangle(0, 0, Main.cursorTexture.Width, Main.cursorTexture.Height)), Main.cursorColor, 0.0f, new Vector2(), Main.cursorScale, SpriteEffects.None, 0.0f);
            if (Main.fadeCounter > 0)
            {
                Color color3 = Color.White;
                --Main.fadeCounter;
                byte num10 = (byte)(float)((double)Main.fadeCounter / 75.0 * (double)byte.MaxValue);
                color3 = new Color((int)num10, (int)num10, (int)num10, (int)num10);
                this.spriteBatch.Draw(Main.fadeTexture, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), color3);
            }
            this.spriteBatch.End();
            Main.mouseLeftRelease = Main.mouseState.LeftButton != ButtonState.Pressed && true;
            if (Main.mouseState.RightButton == ButtonState.Pressed)
                Main.mouseRightRelease = false;
            else
                Main.mouseRightRelease = true;
        }

        public static void CursorColor()
        {
            Main.cursorAlpha += (float)Main.cursorColorDirection * 0.015f;
            if ((double)Main.cursorAlpha >= 1.0)
            {
                Main.cursorAlpha = 1f;
                Main.cursorColorDirection = -1;
            }
            if ((double)Main.cursorAlpha <= 0.6)
            {
                Main.cursorAlpha = 0.6f;
                Main.cursorColorDirection = 1;
            }
            float num = (float)((double)Main.cursorAlpha * 0.300000011920929 + 0.699999988079071);
            Main.cursorColor = new Color((int)(byte)((double)Main.mouseColor.R * (double)Main.cursorAlpha), (int)(byte)((double)Main.mouseColor.G * (double)Main.cursorAlpha), (int)(byte)((double)Main.mouseColor.B * (double)Main.cursorAlpha), (int)(byte)((double)byte.MaxValue * (double)num));
            Main.cursorScale = (float)((double)Main.cursorAlpha * 0.300000011920929 + 0.699999988079071 + 0.100000001490116);
        }

        protected void DrawSplash(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.Black);
            base.Draw(gameTime);
            this.spriteBatch.Begin();
            ++this.splashCounter;
            Color color = Color.White;
            byte num = (byte)0;
            if (this.splashCounter <= 75)
                num = (byte)(float)((double)this.splashCounter / 75.0 * (double)byte.MaxValue);
            else if (this.splashCounter <= 200)
                num = byte.MaxValue;
            else if (this.splashCounter <= 275)
            {
                num = (byte)(float)((double)(275 - this.splashCounter) / 75.0 * (double)byte.MaxValue);
            }
            else
            {
                Main.showSplash = false;
                Main.fadeCounter = 75;
            }
            color = new Color((int)num, (int)num, (int)num, (int)num);
            this.spriteBatch.Draw(Main.splashTexture, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), color);
            this.spriteBatch.End();
        }

        protected override void Draw(GameTime gameTime)
        {
            if (!Main.dedServ)
            {
                int num1 = Main.screenWidth;
                Viewport viewport = this.GraphicsDevice.Viewport;
                int width = viewport.Width;
                int num2;
                if (num1 == width)
                {
                    int num3 = Main.screenHeight;
                    viewport = this.GraphicsDevice.Viewport;
                    int height = viewport.Height;
                    num2 = num3 == height ? 1 : 0;
                }
                else
                    num2 = 0;
                if (num2 == 0 && Main.gamePaused)
                    Lighting.resize = true;
                viewport = this.GraphicsDevice.Viewport;
                Main.screenWidth = viewport.Width;
                viewport = this.GraphicsDevice.Viewport;
                Main.screenHeight = viewport.Height;
                bool flag = false;
                if (Main.screenWidth > Main.maxScreenW)
                {
                    Main.screenWidth = Main.maxScreenW;
                    flag = true;
                }
                if (Main.screenHeight > Main.maxScreenH)
                {
                    Main.screenHeight = Main.maxScreenH;
                    flag = true;
                }
                if (Main.screenWidth < Main.minScreenW)
                {
                    Main.screenWidth = Main.minScreenW;
                    flag = true;
                }
                if (Main.screenHeight < Main.minScreenH)
                {
                    Main.screenHeight = Main.minScreenH;
                    flag = true;
                }
                if (flag)
                {
                    this.graphics.PreferredBackBufferWidth = Main.screenWidth;
                    this.graphics.PreferredBackBufferHeight = Main.screenHeight;
                    this.graphics.ApplyChanges();
                }
            }
            Main.CursorColor();
            ++Main.drawTime;
            Main.screenLastPosition = Main.screenPosition;
            if (Main.stackSplit == 0)
            {
                Main.stackCounter = 0;
                Main.stackDelay = 7;
            }
            else
            {
                ++Main.stackCounter;
                if (Main.stackCounter >= 30)
                {
                    --Main.stackDelay;
                    if (Main.stackDelay < 2)
                        Main.stackDelay = 2;
                    Main.stackCounter = 0;
                }
            }
            Main.mouseTextColor += (byte)Main.mouseTextColorChange;
            if ((int)Main.mouseTextColor >= 250)
                Main.mouseTextColorChange = -4;
            if ((int)Main.mouseTextColor <= 175)
                Main.mouseTextColorChange = 4;
            if (Main.myPlayer >= 0)
                Main.player[Main.myPlayer].mouseInterface = false;
            Main.toolTip = new Item();
            if (!Main.gameMenu && Main.netMode != 2)
            {
                int num1 = Main.mouseState.X;
                int num2 = Main.mouseState.Y;
                if (num1 < 0)
                    num1 = 0;
                if (num1 > Main.screenWidth)
                {
                    int num3 = Main.screenWidth;
                }
                if (num2 < 0)
                    num2 = 0;
                if (num2 > Main.screenHeight)
                {
                    int num4 = Main.screenHeight;
                }
                Main.screenPosition.X = (float)((double)Main.player[Main.myPlayer].position.X + (double)Main.player[Main.myPlayer].width * 0.5 - (double)Main.screenWidth * 0.5);
                Main.screenPosition.Y = (float)((double)Main.player[Main.myPlayer].position.Y + (double)Main.player[Main.myPlayer].height * 0.5 - (double)Main.screenHeight * 0.5);
                Main.screenPosition.X = (float)(int)Main.screenPosition.X;
                Main.screenPosition.Y = (float)(int)Main.screenPosition.Y;
            }
            if (!Main.gameMenu && Main.netMode != 2)
            {
                if ((double)Main.screenPosition.X < (double)Main.leftWorld + 336.0 + 16.0)
                    Main.screenPosition.X = (float)((double)Main.leftWorld + 336.0 + 16.0);
                else if ((double)Main.screenPosition.X + (double)Main.screenWidth > (double)Main.rightWorld - 336.0 - 32.0)
                    Main.screenPosition.X = (float)((double)Main.rightWorld - (double)Main.screenWidth - 336.0 - 32.0);
                if ((double)Main.screenPosition.Y < (double)Main.topWorld + 336.0 + 16.0)
                    Main.screenPosition.Y = (float)((double)Main.topWorld + 336.0 + 16.0);
                else if ((double)Main.screenPosition.Y + (double)Main.screenHeight > (double)Main.bottomWorld - 336.0 - 32.0)
                    Main.screenPosition.Y = (float)((double)Main.bottomWorld - (double)Main.screenHeight - 336.0 - 32.0);
            }
            if (Main.showSplash)
            {
                this.DrawSplash(gameTime);
            }
            else
            {
                this.GraphicsDevice.Clear(Color.Black);
                base.Draw(gameTime);
                this.spriteBatch.Begin();
                double num1 = 0.5;
                int num2 = (int)(-Math.IEEERemainder((double)Main.screenPosition.X * num1, (double)Main.backgroundWidth[Main.background]) - (double)(Main.backgroundWidth[Main.background] / 2));
                int num3 = Main.screenWidth / Main.backgroundWidth[Main.background] + 2;
                int y1 = (int)(-(double)Main.screenPosition.Y / (Main.worldSurface * 16.0 - 600.0) * 200.0);
                if (Main.gameMenu || Main.netMode == 2)
                    y1 = -200;
                Color white1 = Color.White;
                int num4 = (int)(Main.time / 54000.0 * (double)(Main.screenWidth + Main.sunTexture.Width * 2)) - Main.sunTexture.Width;
                int num5 = 0;
                Color white2 = Color.White;
                float scale1 = 1f;
                float rotation1 = (float)(Main.time / 54000.0 * 2.0 - 7.30000019073486);
                int num6 = (int)(Main.time / 32400.0 * (double)(Main.screenWidth + Main.moonTexture.Width * 2)) - Main.moonTexture.Width;
                int num7 = 0;
                Color white3 = Color.White;
                float scale2 = 1f;
                float rotation2 = (float)(Main.time / 32400.0 * 2.0 - 7.30000019073486);
                if (Main.dayTime)
                {
                    double num8;
                    if (Main.time < 27000.0)
                    {
                        num8 = Math.Pow(1.0 - Main.time / 54000.0 * 2.0, 2.0);
                        num5 = (int)((double)y1 + num8 * 250.0 + 180.0);
                    }
                    else
                    {
                        num8 = Math.Pow((Main.time / 54000.0 - 0.5) * 2.0, 2.0);
                        num5 = (int)((double)y1 + num8 * 250.0 + 180.0);
                    }
                    scale1 = (float)(1.2 - num8 * 0.4);
                }
                else
                {
                    double num8;
                    if (Main.time < 16200.0)
                    {
                        num8 = Math.Pow(1.0 - Main.time / 32400.0 * 2.0, 2.0);
                        num7 = (int)((double)y1 + num8 * 250.0 + 180.0);
                    }
                    else
                    {
                        num8 = Math.Pow((Main.time / 32400.0 - 0.5) * 2.0, 2.0);
                        num7 = (int)((double)y1 + num8 * 250.0 + 180.0);
                    }
                    scale2 = (float)(1.2 - num8 * 0.4);
                }
                if (Main.dayTime)
                {
                    if (Main.time < 13500.0)
                    {
                        float num8 = (float)(Main.time / 13500.0);
                        white2.R = (byte)((double)num8 * 200.0 + 55.0);
                        white2.G = (byte)((double)num8 * 180.0 + 75.0);
                        white2.B = (byte)((double)num8 * 250.0 + 5.0);
                        white1.R = (byte)((double)num8 * 200.0 + 55.0);
                        white1.G = (byte)((double)num8 * 200.0 + 55.0);
                        white1.B = (byte)((double)num8 * 200.0 + 55.0);
                    }
                    if (Main.time > 45900.0)
                    {
                        float num8 = (float)(1.0 - (Main.time / 54000.0 - 0.85) * 6.66666666666667);
                        white2.R = (byte)((double)num8 * 120.0 + 55.0);
                        white2.G = (byte)((double)num8 * 100.0 + 25.0);
                        white2.B = (byte)((double)num8 * 120.0 + 55.0);
                        white1.R = (byte)((double)num8 * 200.0 + 55.0);
                        white1.G = (byte)((double)num8 * 85.0 + 55.0);
                        white1.B = (byte)((double)num8 * 135.0 + 55.0);
                    }
                    else if (Main.time > 37800.0)
                    {
                        float num8 = (float)(1.0 - (Main.time / 54000.0 - 0.7) * 6.66666666666667);
                        white2.R = (byte)((double)num8 * 80.0 + 175.0);
                        white2.G = (byte)((double)num8 * 130.0 + 125.0);
                        white2.B = (byte)((double)num8 * 100.0 + 155.0);
                        white1.R = (byte)((double)num8 * 0.0 + (double)byte.MaxValue);
                        white1.G = (byte)((double)num8 * 115.0 + 140.0);
                        white1.B = (byte)((double)num8 * 75.0 + 180.0);
                    }
                }
                if (!Main.dayTime)
                {
                    if (Main.bloodMoon)
                    {
                        if (Main.time < 16200.0)
                        {
                            float num8 = (float)(1.0 - Main.time / 16200.0);
                            white3.R = (byte)((double)num8 * 10.0 + 205.0);
                            white3.G = (byte)((double)num8 * 170.0 + 55.0);
                            white3.B = (byte)((double)num8 * 200.0 + 55.0);
                            white1.R = (byte)(60.0 - (double)num8 * 60.0 + 55.0);
                            white1.G = (byte)((double)num8 * 40.0 + 15.0);
                            white1.B = (byte)((double)num8 * 40.0 + 15.0);
                        }
                        else if (Main.time >= 16200.0)
                        {
                            float num8 = (float)((Main.time / 32400.0 - 0.5) * 2.0);
                            white3.R = (byte)((double)num8 * 50.0 + 205.0);
                            white3.G = (byte)((double)num8 * 100.0 + 155.0);
                            white3.B = (byte)((double)num8 * 100.0 + 155.0);
                            white3.R = (byte)((double)num8 * 10.0 + 205.0);
                            white3.G = (byte)((double)num8 * 170.0 + 55.0);
                            white3.B = (byte)((double)num8 * 200.0 + 55.0);
                            white1.R = (byte)(60.0 - (double)num8 * 60.0 + 55.0);
                            white1.G = (byte)((double)num8 * 40.0 + 15.0);
                            white1.B = (byte)((double)num8 * 40.0 + 15.0);
                        }
                    }
                    else if (Main.time < 16200.0)
                    {
                        float num8 = (float)(1.0 - Main.time / 16200.0);
                        white3.R = (byte)((double)num8 * 10.0 + 205.0);
                        white3.G = (byte)((double)num8 * 70.0 + 155.0);
                        white3.B = (byte)((double)num8 * 100.0 + 155.0);
                        white1.R = (byte)((double)num8 * 40.0 + 15.0);
                        white1.G = (byte)((double)num8 * 40.0 + 15.0);
                        white1.B = (byte)((double)num8 * 40.0 + 15.0);
                    }
                    else if (Main.time >= 16200.0)
                    {
                        float num8 = (float)((Main.time / 32400.0 - 0.5) * 2.0);
                        white3.R = (byte)((double)num8 * 50.0 + 205.0);
                        white3.G = (byte)((double)num8 * 100.0 + 155.0);
                        white3.B = (byte)((double)num8 * 100.0 + 155.0);
                        white1.R = (byte)((double)num8 * 40.0 + 15.0);
                        white1.G = (byte)((double)num8 * 40.0 + 15.0);
                        white1.B = (byte)((double)num8 * 40.0 + 15.0);
                    }
                }
                if (Main.gameMenu || Main.netMode == 2)
                {
                    y1 = 0;
                    if (!Main.dayTime)
                    {
                        white1.R = (byte)55;
                        white1.G = (byte)55;
                        white1.B = (byte)55;
                    }
                }
                if (Main.evilTiles > 0)
                {
                    float num8 = (float)Main.evilTiles / 500f;
                    if ((double)num8 > 1.0)
                        num8 = 1f;
                    int num9 = (int)white1.R;
                    int num10 = (int)white1.G;
                    int num11 = (int)white1.B;
                    int num12 = num9 + (int)(10.0 * (double)num8);
                    int num13 = num10 - (int)(90.0 * (double)num8 * ((double)white1.G / (double)byte.MaxValue));
                    int num14 = num11 - (int)(190.0 * (double)num8 * ((double)white1.B / (double)byte.MaxValue));
                    if (num12 > (int)byte.MaxValue)
                        num12 = (int)byte.MaxValue;
                    if (num13 < 15)
                        num13 = 15;
                    if (num14 < 15)
                        num14 = 15;
                    white1.R = (byte)num12;
                    white1.G = (byte)num13;
                    white1.B = (byte)num14;
                    int num15 = (int)white2.R;
                    int num16 = (int)white2.G;
                    int num17 = (int)white2.B;
                    int num18 = num15 - (int)(100.0 * (double)num8 * ((double)white2.R / (double)byte.MaxValue));
                    int num19 = num16 - (int)(160.0 * (double)num8 * ((double)white2.G / (double)byte.MaxValue));
                    int num20 = num17 - (int)(170.0 * (double)num8 * ((double)white2.B / (double)byte.MaxValue));
                    if (num18 < 15)
                        num18 = 15;
                    if (num19 < 15)
                        num19 = 15;
                    if (num20 < 15)
                        num20 = 15;
                    white2.R = (byte)num18;
                    white2.G = (byte)num19;
                    white2.B = (byte)num20;
                    int num21 = (int)white3.R;
                    int num22 = (int)white3.G;
                    int num23 = (int)white3.B;
                    int num24 = num21 - (int)(140.0 * (double)num8 * ((double)white3.R / (double)byte.MaxValue));
                    int num25 = num22 - (int)(170.0 * (double)num8 * ((double)white3.G / (double)byte.MaxValue));
                    int num26 = num23 - (int)(190.0 * (double)num8 * ((double)white3.B / (double)byte.MaxValue));
                    if (num24 < 15)
                        num24 = 15;
                    if (num25 < 15)
                        num25 = 15;
                    if (num26 < 15)
                        num26 = 15;
                    white3.R = (byte)num24;
                    white3.G = (byte)num25;
                    white3.B = (byte)num26;
                }
                if (Main.jungleTiles > 0)
                {
                    float num8 = (float)Main.jungleTiles / 200f;
                    if ((double)num8 > 1.0)
                        num8 = 1f;
                    int num9 = (int)white1.R;
                    int num10 = (int)white1.G;
                    int num11 = (int)white1.B;
                    int num12 = num9 - (int)(20.0 * (double)num8 * ((double)white1.R / (double)byte.MaxValue));
                    int num13 = num11 - (int)(150.0 * (double)num8 * ((double)white1.B / (double)byte.MaxValue));
                    if (num10 > (int)byte.MaxValue)
                        num10 = (int)byte.MaxValue;
                    if (num10 < 15)
                        num10 = 15;
                    if (num12 > (int)byte.MaxValue)
                        num12 = (int)byte.MaxValue;
                    if (num12 < 15)
                        num12 = 15;
                    if (num13 < 15)
                        num13 = 15;
                    white1.R = (byte)num12;
                    white1.G = (byte)num10;
                    white1.B = (byte)num13;
                    int num14 = (int)white2.R;
                    int num15 = (int)white2.G;
                    int num16 = (int)white2.B;
                    int num17 = num15 - (int)(10.0 * (double)num8 * ((double)white2.G / (double)byte.MaxValue));
                    int num18 = num14 - (int)(50.0 * (double)num8 * ((double)white2.R / (double)byte.MaxValue));
                    int num19 = num16 - (int)(10.0 * (double)num8 * ((double)white2.B / (double)byte.MaxValue));
                    if (num18 < 15)
                        num18 = 15;
                    if (num17 < 15)
                        num17 = 15;
                    if (num19 < 15)
                        num19 = 15;
                    white2.R = (byte)num18;
                    white2.G = (byte)num17;
                    white2.B = (byte)num19;
                    int num20 = (int)white3.R;
                    int num21 = (int)white3.G;
                    int num22 = (int)white3.B;
                    int num23 = num21 - (int)(140.0 * (double)num8 * ((double)white3.R / (double)byte.MaxValue));
                    int num24 = num20 - (int)(170.0 * (double)num8 * ((double)white3.G / (double)byte.MaxValue));
                    int num25 = num22 - (int)(190.0 * (double)num8 * ((double)white3.B / (double)byte.MaxValue));
                    if (num24 < 15)
                        num24 = 15;
                    if (num23 < 15)
                        num23 = 15;
                    if (num25 < 15)
                        num25 = 15;
                    white3.R = (byte)num24;
                    white3.G = (byte)num23;
                    white3.B = (byte)num25;
                }
                Main.tileColor.A = byte.MaxValue;
                Main.tileColor.R = (byte)(((int)white1.R + (int)white1.B + (int)white1.G) / 3);
                Main.tileColor.G = (byte)(((int)white1.R + (int)white1.B + (int)white1.G) / 3);
                Main.tileColor.B = (byte)(((int)white1.R + (int)white1.B + (int)white1.G) / 3);
                if ((double)Main.screenPosition.Y < Main.worldSurface * 16.0 + 16.0)
                {
                    for (int index = 0; index < num3; ++index)
                        this.spriteBatch.Draw(Main.backgroundTexture[Main.background], new Rectangle(num2 + Main.backgroundWidth[Main.background] * index, y1, Main.backgroundWidth[Main.background], Main.backgroundHeight[Main.background]), white1);
                }
                if ((double)Main.screenPosition.Y < Main.worldSurface * 16.0 + 16.0 && (int)byte.MaxValue - (int)Main.tileColor.R - 100 > 0 && Main.netMode != 2)
                {
                    for (int index = 0; index < Main.numStars; ++index)
                    {
                        Color color = new Color();
                        float num8 = (float)Main.evilTiles / 500f;
                        if ((double)num8 > 1.0)
                            num8 = 1f;
                        float num9 = (float)(1.0 - (double)num8 * 0.5);
                        if (Main.evilTiles <= 0)
                            num9 = 1f;
                        int num10 = (int)((double)((int)byte.MaxValue - (int)Main.tileColor.R - 100) * (double)Main.star[index].twinkle * (double)num9);
                        int num11 = (int)((double)((int)byte.MaxValue - (int)Main.tileColor.G - 100) * (double)Main.star[index].twinkle * (double)num9);
                        int num12 = (int)((double)((int)byte.MaxValue - (int)Main.tileColor.B - 100) * (double)Main.star[index].twinkle * (double)num9);
                        if (num10 < 0)
                            num10 = 0;
                        if (num11 < 0)
                            num11 = 0;
                        if (num12 < 0)
                            num12 = 0;
                        color.R = (byte)num10;
                        color.G = (byte)((double)num11 * (double)num9);
                        color.B = (byte)((double)num12 * (double)num9);
                        float num13 = Main.star[index].position.X * ((float)Main.screenWidth / 800f);
                        float num14 = Main.star[index].position.Y * ((float)Main.screenHeight / 600f);
                        this.spriteBatch.Draw(Main.starTexture[Main.star[index].type], new Vector2(num13 + (float)Main.starTexture[Main.star[index].type].Width * 0.5f, num14 + (float)Main.starTexture[Main.star[index].type].Height * 0.5f + (float)y1), new Rectangle?(new Rectangle(0, 0, Main.starTexture[Main.star[index].type].Width, Main.starTexture[Main.star[index].type].Height)), color, Main.star[index].rotation, new Vector2((float)Main.starTexture[Main.star[index].type].Width * 0.5f, (float)Main.starTexture[Main.star[index].type].Height * 0.5f), Main.star[index].scale * Main.star[index].twinkle, SpriteEffects.None, 0.0f);
                    }
                }
                if (Main.dayTime)
                {
                    if (!Main.gameMenu && Main.player[Main.myPlayer].head == 12)
                        this.spriteBatch.Draw(Main.sun2Texture, new Vector2((float)num4, (float)(num5 + (int)Main.sunModY)), new Rectangle?(new Rectangle(0, 0, Main.sunTexture.Width, Main.sunTexture.Height)), white2, rotation1, new Vector2((float)(Main.sunTexture.Width / 2), (float)(Main.sunTexture.Height / 2)), scale1, SpriteEffects.None, 0.0f);
                    else
                        this.spriteBatch.Draw(Main.sunTexture, new Vector2((float)num4, (float)(num5 + (int)Main.sunModY)), new Rectangle?(new Rectangle(0, 0, Main.sunTexture.Width, Main.sunTexture.Height)), white2, rotation1, new Vector2((float)(Main.sunTexture.Width / 2), (float)(Main.sunTexture.Height / 2)), scale1, SpriteEffects.None, 0.0f);
                }
                if (!Main.dayTime)
                    this.spriteBatch.Draw(Main.moonTexture, new Vector2((float)num6, (float)(num7 + (int)Main.moonModY)), new Rectangle?(new Rectangle(0, Main.moonTexture.Width * Main.moonPhase, Main.moonTexture.Width, Main.moonTexture.Width)), white3, rotation2, new Vector2((float)(Main.moonTexture.Width / 2), (float)(Main.moonTexture.Width / 2)), scale2, SpriteEffects.None, 0.0f);
                Rectangle rectangle1 = !Main.dayTime ? new Rectangle((int)((double)num6 - (double)Main.moonTexture.Width * 0.5 * (double)scale2), (int)((double)num7 - (double)Main.moonTexture.Width * 0.5 * (double)scale2 + (double)Main.moonModY), (int)((double)Main.moonTexture.Width * (double)scale2), (int)((double)Main.moonTexture.Width * (double)scale2)) : new Rectangle((int)((double)num4 - (double)Main.sunTexture.Width * 0.5 * (double)scale1), (int)((double)num5 - (double)Main.sunTexture.Height * 0.5 * (double)scale1 + (double)Main.sunModY), (int)((double)Main.sunTexture.Width * (double)scale1), (int)((double)Main.sunTexture.Width * (double)scale1));
                Rectangle rectangle2 = new Rectangle(Main.mouseState.X, Main.mouseState.Y, 1, 1);
                Main.sunModY = (short)((double)Main.sunModY * 0.999);
                Main.moonModY = (short)((double)Main.moonModY * 0.999);
                if (Main.gameMenu && Main.netMode != 1 || Main.grabSun)
                {
                    if (Main.mouseState.LeftButton == ButtonState.Pressed && Main.hasFocus)
                    {
                        if (rectangle2.Intersects(rectangle1) || Main.grabSky)
                        {
                            if (Main.dayTime)
                            {
                                Main.time = 54000.0 * ((double)(Main.mouseState.X + Main.sunTexture.Width) / ((double)Main.screenWidth + (double)(Main.sunTexture.Width * 2)));
                                Main.sunModY = (short)(Main.mouseState.Y - num5);
                                if (Main.time > 53990.0)
                                    Main.time = 53990.0;
                            }
                            else
                            {
                                Main.time = 32400.0 * ((double)(Main.mouseState.X + Main.moonTexture.Width) / ((double)Main.screenWidth + (double)(Main.moonTexture.Width * 2)));
                                Main.moonModY = (short)(Main.mouseState.Y - num7);
                                if (Main.time > 32390.0)
                                    Main.time = 32390.0;
                            }
                            if (Main.time < 10.0)
                                Main.time = 10.0;
                            if (Main.netMode != 0)
                                NetMessage.SendData(18, -1, -1, "", 0, 0.0f, 0.0f, 0.0f);
                            Main.grabSky = true;
                        }
                    }
                    else
                        Main.grabSky = false;
                }
                if (Main.resetClouds)
                {
                    Cloud.resetClouds();
                    Main.resetClouds = false;
                }
                if (this.IsActive || Main.netMode != 0)
                {
                    Main.windSpeedSpeed += (float)Main.rand.Next(-10, 11) * 0.0001f;
                    if ((double)Main.windSpeedSpeed < -0.002)
                        Main.windSpeedSpeed = -0.002f;
                    if ((double)Main.windSpeedSpeed > 0.002)
                        Main.windSpeedSpeed = 0.002f;
                    Main.windSpeed += Main.windSpeedSpeed;
                    if ((double)Main.windSpeed < -0.3)
                        Main.windSpeed = -0.3f;
                    if ((double)Main.windSpeed > 0.3)
                        Main.windSpeed = 0.3f;
                    Main.numClouds += Main.rand.Next(-1, 2);
                    if (Main.numClouds < 0)
                        Main.numClouds = 0;
                    if (Main.numClouds > Main.cloudLimit)
                        Main.numClouds = Main.cloudLimit;
                }
                if ((double)Main.screenPosition.Y < Main.worldSurface * 16.0 + 16.0)
                {
                    for (int index = 0; index < 100; ++index)
                    {
                        if (Main.cloud[index].active)
                        {
                            int num8 = (int)(40.0 * (2.0 - (double)Main.cloud[index].scale));
                            Color color = new Color();
                            int num9 = (int)white1.R - num8;
                            if (num9 <= 0)
                                num9 = 0;
                            color.R = (byte)num9;
                            int num10 = (int)white1.G - num8;
                            if (num10 <= 0)
                                num10 = 0;
                            color.G = (byte)num10;
                            int num11 = (int)white1.B - num8;
                            if (num11 <= 0)
                                num11 = 0;
                            color.B = (byte)num11;
                            color.A = (byte)((int)byte.MaxValue - num8);
                            float num12 = Main.cloud[index].position.Y * ((float)Main.screenHeight / 600f);
                            this.spriteBatch.Draw(Main.cloudTexture[Main.cloud[index].type], new Vector2(Main.cloud[index].position.X + (float)Main.cloudTexture[Main.cloud[index].type].Width * 0.5f, num12 + (float)Main.cloudTexture[Main.cloud[index].type].Height * 0.5f + (float)y1), new Rectangle?(new Rectangle(0, 0, Main.cloudTexture[Main.cloud[index].type].Width, Main.cloudTexture[Main.cloud[index].type].Height)), color, Main.cloud[index].rotation, new Vector2((float)Main.cloudTexture[Main.cloud[index].type].Width * 0.5f, (float)Main.cloudTexture[Main.cloud[index].type].Height * 0.5f), Main.cloud[index].scale, SpriteEffects.None, 0.0f);
                        }
                    }
                }
                if (Main.gameMenu || Main.netMode == 2)
                {
                    this.DrawMenu();
                }
                else
                {
                    int firstX = (int)((double)Main.screenPosition.X / 16.0 - 1.0);
                    int lastX = (int)(((double)Main.screenPosition.X + (double)Main.screenWidth) / 16.0) + 2;
                    int firstY = (int)((double)Main.screenPosition.Y / 16.0 - 1.0);
                    int lastY = (int)(((double)Main.screenPosition.Y + (double)Main.screenHeight) / 16.0) + 2;
                    if (firstX < 0)
                        firstX = 0;
                    if (lastX > Main.maxTilesX)
                        lastX = Main.maxTilesX;
                    if (firstY < 0)
                        firstY = 0;
                    if (lastY > Main.maxTilesY)
                        lastY = Main.maxTilesY;
                    Lighting.LightTiles(firstX, lastX, firstY, lastY);
                    Color white4 = Color.White;
                    this.DrawWater(true);
                    double num8 = (double)Main.caveParrallax;
                    int num9 = (int)(-Math.IEEERemainder((double)Main.screenPosition.X * num8, (double)Main.backgroundWidth[1]) - (double)(Main.backgroundWidth[1] / 2));
                    int num10 = Main.screenWidth / Main.backgroundWidth[1] + 2;
                    int num11 = (int)((double)((int)Main.worldSurface * 16 - Main.backgroundHeight[1]) - (double)Main.screenPosition.Y + 16.0);
                    for (int index1 = 0; index1 < num10; ++index1)
                    {
                        for (int index2 = 0; index2 < 6; ++index2)
                        {
                            int num12 = (num9 + Main.backgroundWidth[1] * index1 + index2 * 16) / 16;
                            int num13 = num11 / 16;
                            Color color = Lighting.GetColor(num12 + (int)((double)Main.screenPosition.X / 16.0), num13 + (int)((double)Main.screenPosition.Y / 16.0));
                            this.spriteBatch.Draw(Main.backgroundTexture[1], new Vector2((float)(num9 + Main.backgroundWidth[1] * index1 + 16 * index2), (float)num11), new Rectangle?(new Rectangle(16 * index2, 0, 16, 16)), color);
                        }
                    }
                    double num14 = (double)((int)(((double)(Main.maxTilesY - 230) - Main.worldSurface) / 6.0) * 6);
                    double num15 = Main.worldSurface + num14 - 5.0;
                    bool flag1 = false;
                    bool flag2 = false;
                    int num16 = (int)((double)((int)Main.worldSurface * 16) - (double)Main.screenPosition.Y + 16.0);
                    if (Main.worldSurface * 16.0 <= (double)Main.screenPosition.Y + (double)Main.screenHeight)
                    {
                        num8 = (double)Main.caveParrallax;
                        int num12 = (int)(-Math.IEEERemainder(100.0 + (double)Main.screenPosition.X * num8, (double)Main.backgroundWidth[2]) - (double)(Main.backgroundWidth[2] / 2));
                        int num13 = Main.screenWidth / Main.backgroundWidth[2] + 2;
                        int num17;
                        int num18;
                        if (Main.worldSurface * 16.0 < (double)Main.screenPosition.Y - 16.0)
                        {
                            num17 = (int)(Math.IEEERemainder((double)num16, (double)Main.backgroundHeight[2]) - (double)Main.backgroundHeight[2]);
                            num18 = (Main.screenHeight - num17) / Main.backgroundHeight[2] + 1;
                        }
                        else
                        {
                            num17 = num16;
                            num18 = (Main.screenHeight - num16) / Main.backgroundHeight[2] + 1;
                        }
                        if (Main.rockLayer * 16.0 < (double)Main.screenPosition.Y + 600.0)
                        {
                            num18 = (int)(Main.rockLayer * 16.0 - (double)Main.screenPosition.Y + 600.0 - (double)num17) / Main.backgroundHeight[2];
                            flag2 = true;
                        }
                        for (int index1 = 0; index1 < num13; ++index1)
                        {
                            for (int index2 = 0; index2 < num18; ++index2)
                                this.spriteBatch.Draw(Main.backgroundTexture[2], new Rectangle(num12 + Main.backgroundWidth[2] * index1, num17 + Main.backgroundHeight[2] * index2, Main.backgroundWidth[2], Main.backgroundHeight[2]), Color.White);
                        }
                        if (flag2)
                        {
                            num8 = (double)Main.caveParrallax;
                            int num19 = (int)(-Math.IEEERemainder((double)Main.screenPosition.X * num8, (double)Main.backgroundWidth[1]) - (double)(Main.backgroundWidth[1] / 2));
                            int num20 = Main.screenWidth / Main.backgroundWidth[1] + 2;
                            int num21 = num17 + num18 * Main.backgroundHeight[2];
                            for (int index1 = 0; index1 < num20; ++index1)
                            {
                                for (int index2 = 0; index2 < 6; ++index2)
                                {
                                    int num22 = (num19 + Main.backgroundWidth[4] * index1 + index2 * 16) / 16;
                                    int num23 = num21 / 16;
                                    this.spriteBatch.Draw(Main.backgroundTexture[4], new Vector2((float)(num19 + Main.backgroundWidth[4] * index1 + 16 * index2), (float)num21), new Rectangle?(new Rectangle(16 * index2, 0, 16, 24)), Color.White);
                                }
                            }
                        }
                    }
                    int num24 = (int)((double)((int)Main.rockLayer * 16) - (double)Main.screenPosition.Y + 16.0 + 600.0);
                    if (Main.rockLayer * 16.0 <= (double)Main.screenPosition.Y + 600.0)
                    {
                        num8 = (double)Main.caveParrallax;
                        int num12 = (int)(-Math.IEEERemainder(100.0 + (double)Main.screenPosition.X * num8, (double)Main.backgroundWidth[3]) - (double)(Main.backgroundWidth[3] / 2));
                        int num13 = Main.screenWidth / Main.backgroundWidth[3] + 2;
                        int num17;
                        int num18;
                        if (Main.rockLayer * 16.0 + (double)Main.screenHeight < (double)Main.screenPosition.Y - 16.0)
                        {
                            num17 = (int)(Math.IEEERemainder((double)num24, (double)Main.backgroundHeight[3]) - (double)Main.backgroundHeight[3]);
                            num18 = (Main.screenHeight - num17) / Main.backgroundHeight[3] + 1;
                        }
                        else
                        {
                            num17 = num24;
                            num18 = (Main.screenHeight - num24) / Main.backgroundHeight[3] + 1;
                        }
                        if (num15 * 16.0 < (double)Main.screenPosition.Y + 600.0)
                        {
                            num18 = (int)(num15 * 16.0 - (double)Main.screenPosition.Y + 600.0 - (double)num17) / Main.backgroundHeight[2];
                            flag1 = true;
                        }
                        for (int index1 = 0; index1 < num13; ++index1)
                        {
                            for (int index2 = 0; index2 < num18; ++index2)
                                this.spriteBatch.Draw(Main.backgroundTexture[3], new Rectangle(num12 + Main.backgroundWidth[2] * index1, num17 + Main.backgroundHeight[2] * index2, Main.backgroundWidth[2], Main.backgroundHeight[2]), Color.White);
                        }
                        if (flag1)
                        {
                            num8 = (double)Main.caveParrallax;
                            int num19 = (int)(-Math.IEEERemainder((double)Main.screenPosition.X * num8, (double)Main.backgroundWidth[1]) - (double)(Main.backgroundWidth[1] / 2) - 4.0);
                            int num20 = Main.screenWidth / Main.backgroundWidth[1] + 2;
                            int num21 = num17 + num18 * Main.backgroundHeight[2];
                            for (int index1 = 0; index1 < num20; ++index1)
                            {
                                for (int index2 = 0; index2 < 6; ++index2)
                                {
                                    int num22 = (num19 + Main.backgroundWidth[1] * index1 + index2 * 16) / 16;
                                    int num23 = num21 / 16;
                                    Lighting.GetColor(num22 + (int)((double)Main.screenPosition.X / 16.0), num23 + (int)((double)Main.screenPosition.Y / 16.0));
                                    this.spriteBatch.Draw(Main.backgroundTexture[6], new Vector2((float)(num19 + Main.backgroundWidth[1] * index1 + 16 * index2), (float)num21), new Rectangle?(new Rectangle(16 * index2, Main.magmaBGFrame * 24, 16, 24)), Color.White);
                                }
                            }
                        }
                    }
                    int num25 = (int)((double)((int)num15 * 16) - (double)Main.screenPosition.Y + 16.0 + 600.0) + 8;
                    if (num15 * 16.0 <= (double)Main.screenPosition.Y + 600.0)
                    {
                        int num12 = (int)(-Math.IEEERemainder(100.0 + (double)Main.screenPosition.X * num8, (double)Main.backgroundWidth[3]) - (double)(Main.backgroundWidth[3] / 2));
                        int num13 = Main.screenWidth / Main.backgroundWidth[3] + 2;
                        int num17;
                        int num18;
                        if (num15 * 16.0 + (double)Main.screenHeight < (double)Main.screenPosition.Y - 16.0)
                        {
                            num17 = (int)(Math.IEEERemainder((double)num25, (double)Main.backgroundHeight[3]) - (double)Main.backgroundHeight[3]);
                            num18 = (Main.screenHeight - num17) / Main.backgroundHeight[3] + 1;
                        }
                        else
                        {
                            num17 = num25;
                            num18 = (Main.screenHeight - num25) / Main.backgroundHeight[3] + 1;
                        }
                        for (int index1 = 0; index1 < num13; ++index1)
                        {
                            for (int index2 = 0; index2 < num18; ++index2)
                                this.spriteBatch.Draw(Main.backgroundTexture[5], new Vector2((float)(num12 + Main.backgroundWidth[2] * index1), (float)(num17 + Main.backgroundHeight[2] * index2)), new Rectangle?(new Rectangle(0, Main.backgroundHeight[2] * Main.magmaBGFrame, Main.backgroundWidth[2], Main.backgroundHeight[2])), Color.White, 0.0f, new Vector2(), 1f, SpriteEffects.None, 0.0f);
                        }
                    }
                    ++Main.magmaBGFrameCounter;
                    if (Main.magmaBGFrameCounter >= 8)
                    {
                        Main.magmaBGFrameCounter = 0;
                        ++Main.magmaBGFrame;
                        if (Main.magmaBGFrame >= 3)
                            Main.magmaBGFrame = 0;
                    }
                    try
                    {
                        int num12;
                        int num13;
                        for (int y2 = firstY; y2 < lastY + 4; ++y2)
                        {
                            num12 = y2 - firstY + 21;
                            for (int x = firstX - 2; x < lastX + 2; ++x)
                            {
                                if (Main.tile[x, y2] == null)
                                    Main.tile[x, y2] = new Tile();
                                num13 = x - firstX + 21;
                                if ((double)Lighting.Brightness(x, y2) * (double)byte.MaxValue < (double)((int)Main.tileColor.R - 12) || (double)y2 > Main.worldSurface)
                                    this.spriteBatch.Draw(Main.blackTileTexture, new Vector2((float)(x * 16 - (int)Main.screenPosition.X), (float)(y2 * 16 - (int)Main.screenPosition.Y)), new Rectangle?(new Rectangle((int)Main.tile[x, y2].frameX, (int)Main.tile[x, y2].frameY, 16, 16)), Lighting.GetBlackness(x, y2), 0.0f, new Vector2(), 1f, SpriteEffects.None, 0.0f);
                            }
                        }
                        for (int y2 = firstY; y2 < lastY + 4; ++y2)
                        {
                            num12 = y2 - firstY + 21;
                            for (int x = firstX - 2; x < lastX + 2; ++x)
                            {
                                num13 = x - firstX + 21;
                                if ((int)Main.tile[x, y2].wall > 0 && (double)Lighting.Brightness(x, y2) > 0.0)
                                {
                                    if ((int)Main.tile[x, y2].wallFrameY == 18 && (int)Main.tile[x, y2].wallFrameX >= 18)
                                    {
                                        int num17 = (int)Main.tile[x, y2].wallFrameY;
                                    }
                                    bool flag3 = 0 == 0;
                                    Rectangle rectangle3 = new Rectangle((int)Main.tile[x, y2].wallFrameX * 2, (int)Main.tile[x, y2].wallFrameY * 2, 32, 32);
                                    this.spriteBatch.Draw(Main.wallTexture[(int)Main.tile[x, y2].wall], new Vector2((float)(x * 16 - (int)Main.screenPosition.X - 8), (float)(y2 * 16 - (int)Main.screenPosition.Y - 8)), new Rectangle?(rectangle3), Lighting.GetColor(x, y2), 0.0f, new Vector2(), 1f, SpriteEffects.None, 0.0f);
                                }
                            }
                        }
                        this.DrawTiles(false);
                        this.DrawNPCs(true);
                        this.DrawTiles(true);
                        this.DrawGore();
                        this.DrawNPCs(false);
                    }
                    catch
                    {
                    }
                    for (int index = 0; index < 1000; ++index)
                    {
                        if (Main.projectile[index].active && Main.projectile[index].type > 0)
                        {
                            Vector2 vector2;
                            if (Main.projectile[index].type == 32)
                            {
                                vector2 = new Vector2(Main.projectile[index].position.X + (float)Main.projectile[index].width * 0.5f, Main.projectile[index].position.Y + (float)Main.projectile[index].height * 0.5f);
                                float num12 = Main.player[Main.projectile[index].owner].position.X + (float)(Main.player[Main.projectile[index].owner].width / 2) - vector2.X;
                                float num13 = Main.player[Main.projectile[index].owner].position.Y + (float)(Main.player[Main.projectile[index].owner].height / 2) - vector2.Y;
                                float rotation3 = (float)Math.Atan2((double)num13, (double)num12) - 1.57f;
                                bool flag3 = true;
                                if ((double)num12 == 0.0 && (double)num13 == 0.0)
                                {
                                    flag3 = false;
                                }
                                else
                                {
                                    float num17 = 8f / (float)Math.Sqrt((double)num12 * (double)num12 + (double)num13 * (double)num13);
                                    float num18 = num12 * num17;
                                    float num19 = num13 * num17;
                                    vector2.X -= num18;
                                    vector2.Y -= num19;
                                    num12 = Main.player[Main.projectile[index].owner].position.X + (float)(Main.player[Main.projectile[index].owner].width / 2) - vector2.X;
                                    num13 = Main.player[Main.projectile[index].owner].position.Y + (float)(Main.player[Main.projectile[index].owner].height / 2) - vector2.Y;
                                }
                                while (flag3)
                                {
                                    float num17 = (float)Math.Sqrt((double)num12 * (double)num12 + (double)num13 * (double)num13);
                                    if ((double)num17 < 28.0)
                                    {
                                        flag3 = false;
                                    }
                                    else
                                    {
                                        float num18 = 28f / num17;
                                        float num19 = num12 * num18;
                                        float num20 = num13 * num18;
                                        vector2.X += num19;
                                        vector2.Y += num20;
                                        num12 = Main.player[Main.projectile[index].owner].position.X + (float)(Main.player[Main.projectile[index].owner].width / 2) - vector2.X;
                                        num13 = Main.player[Main.projectile[index].owner].position.Y + (float)(Main.player[Main.projectile[index].owner].height / 2) - vector2.Y;
                                        Color color = Lighting.GetColor((int)vector2.X / 16, (int)((double)vector2.Y / 16.0));
                                        this.spriteBatch.Draw(Main.chain5Texture, new Vector2(vector2.X - Main.screenPosition.X, vector2.Y - Main.screenPosition.Y), new Rectangle?(new Rectangle(0, 0, Main.chain5Texture.Width, Main.chain5Texture.Height)), color, rotation3, new Vector2((float)Main.chain5Texture.Width * 0.5f, (float)Main.chain5Texture.Height * 0.5f), 1f, SpriteEffects.None, 0.0f);
                                    }
                                }
                            }
                            else if (Main.projectile[index].aiStyle == 7)
                            {
                                vector2 = new Vector2(Main.projectile[index].position.X + (float)Main.projectile[index].width * 0.5f, Main.projectile[index].position.Y + (float)Main.projectile[index].height * 0.5f);
                                float num12 = Main.player[Main.projectile[index].owner].position.X + (float)(Main.player[Main.projectile[index].owner].width / 2) - vector2.X;
                                float num13 = Main.player[Main.projectile[index].owner].position.Y + (float)(Main.player[Main.projectile[index].owner].height / 2) - vector2.Y;
                                float rotation3 = (float)Math.Atan2((double)num13, (double)num12) - 1.57f;
                                bool flag3 = true;
                                while (flag3)
                                {
                                    float num17 = (float)Math.Sqrt((double)num12 * (double)num12 + (double)num13 * (double)num13);
                                    if ((double)num17 < 25.0)
                                    {
                                        flag3 = false;
                                    }
                                    else
                                    {
                                        float num18 = 12f / num17;
                                        float num19 = num12 * num18;
                                        float num20 = num13 * num18;
                                        vector2.X += num19;
                                        vector2.Y += num20;
                                        num12 = Main.player[Main.projectile[index].owner].position.X + (float)(Main.player[Main.projectile[index].owner].width / 2) - vector2.X;
                                        num13 = Main.player[Main.projectile[index].owner].position.Y + (float)(Main.player[Main.projectile[index].owner].height / 2) - vector2.Y;
                                        Color color = Lighting.GetColor((int)vector2.X / 16, (int)((double)vector2.Y / 16.0));
                                        this.spriteBatch.Draw(Main.chainTexture, new Vector2(vector2.X - Main.screenPosition.X, vector2.Y - Main.screenPosition.Y), new Rectangle?(new Rectangle(0, 0, Main.chainTexture.Width, Main.chainTexture.Height)), color, rotation3, new Vector2((float)Main.chainTexture.Width * 0.5f, (float)Main.chainTexture.Height * 0.5f), 1f, SpriteEffects.None, 0.0f);
                                    }
                                }
                            }
                            else if (Main.projectile[index].aiStyle == 13)
                            {
                                float num12 = Main.projectile[index].position.X + 8f;
                                float num13 = Main.projectile[index].position.Y + 2f;
                                float num17 = Main.projectile[index].velocity.X;
                                float num18 = Main.projectile[index].velocity.Y;
                                float num19 = 20f / (float)Math.Sqrt((double)num17 * (double)num17 + (double)num18 * (double)num18);
                                float x;
                                float y2;
                                if ((double)Main.projectile[index].ai[0] == 0.0)
                                {
                                    x = num12 - Main.projectile[index].velocity.X * num19;
                                    y2 = num13 - Main.projectile[index].velocity.Y * num19;
                                }
                                else
                                {
                                    x = num12 + Main.projectile[index].velocity.X * num19;
                                    y2 = num13 + Main.projectile[index].velocity.Y * num19;
                                }
                                vector2 = new Vector2(x, y2);
                                float num20 = Main.player[Main.projectile[index].owner].position.X + (float)(Main.player[Main.projectile[index].owner].width / 2) - vector2.X;
                                float num21 = Main.player[Main.projectile[index].owner].position.Y + (float)(Main.player[Main.projectile[index].owner].height / 2) - vector2.Y;
                                float rotation3 = (float)Math.Atan2((double)num21, (double)num20) - 1.57f;
                                if (Main.projectile[index].alpha == 0)
                                    Main.player[Main.projectile[index].owner].itemRotation = Main.player[Main.projectile[index].owner].direction != 1 ? rotation3 + 1.57f : rotation3 - 1.57f;
                                bool flag3 = true;
                                while (flag3)
                                {
                                    float num22 = (float)Math.Sqrt((double)num20 * (double)num20 + (double)num21 * (double)num21);
                                    if ((double)num22 < 25.0)
                                    {
                                        flag3 = false;
                                    }
                                    else
                                    {
                                        float num23 = 12f / num22;
                                        float num26 = num20 * num23;
                                        float num27 = num21 * num23;
                                        vector2.X += num26;
                                        vector2.Y += num27;
                                        num20 = Main.player[Main.projectile[index].owner].position.X + (float)(Main.player[Main.projectile[index].owner].width / 2) - vector2.X;
                                        num21 = Main.player[Main.projectile[index].owner].position.Y + (float)(Main.player[Main.projectile[index].owner].height / 2) - vector2.Y;
                                        Color color = Lighting.GetColor((int)vector2.X / 16, (int)((double)vector2.Y / 16.0));
                                        this.spriteBatch.Draw(Main.chainTexture, new Vector2(vector2.X - Main.screenPosition.X, vector2.Y - Main.screenPosition.Y), new Rectangle?(new Rectangle(0, 0, Main.chainTexture.Width, Main.chainTexture.Height)), color, rotation3, new Vector2((float)Main.chainTexture.Width * 0.5f, (float)Main.chainTexture.Height * 0.5f), 1f, SpriteEffects.None, 0.0f);
                                    }
                                }
                            }
                            else if (Main.projectile[index].aiStyle == 15)
                            {
                                vector2 = new Vector2(Main.projectile[index].position.X + (float)Main.projectile[index].width * 0.5f, Main.projectile[index].position.Y + (float)Main.projectile[index].height * 0.5f);
                                float num12 = Main.player[Main.projectile[index].owner].position.X + (float)(Main.player[Main.projectile[index].owner].width / 2) - vector2.X;
                                float num13 = Main.player[Main.projectile[index].owner].position.Y + (float)(Main.player[Main.projectile[index].owner].height / 2) - vector2.Y;
                                float rotation3 = (float)Math.Atan2((double)num13, (double)num12) - 1.57f;
                                if (Main.projectile[index].alpha == 0)
                                    Main.player[Main.projectile[index].owner].itemRotation = Main.player[Main.projectile[index].owner].direction != 1 ? rotation3 + 1.57f : rotation3 - 1.57f;
                                bool flag3 = true;
                                while (flag3)
                                {
                                    float num17 = (float)Math.Sqrt((double)num12 * (double)num12 + (double)num13 * (double)num13);
                                    if ((double)num17 < 25.0)
                                    {
                                        flag3 = false;
                                    }
                                    else
                                    {
                                        float num18 = 12f / num17;
                                        float num19 = num12 * num18;
                                        float num20 = num13 * num18;
                                        vector2.X += num19;
                                        vector2.Y += num20;
                                        num12 = Main.player[Main.projectile[index].owner].position.X + (float)(Main.player[Main.projectile[index].owner].width / 2) - vector2.X;
                                        num13 = Main.player[Main.projectile[index].owner].position.Y + (float)(Main.player[Main.projectile[index].owner].height / 2) - vector2.Y;
                                        Color color = Lighting.GetColor((int)vector2.X / 16, (int)((double)vector2.Y / 16.0));
                                        if (Main.projectile[index].type == 25)
                                            this.spriteBatch.Draw(Main.chain2Texture, new Vector2(vector2.X - Main.screenPosition.X, vector2.Y - Main.screenPosition.Y), new Rectangle?(new Rectangle(0, 0, Main.chain2Texture.Width, Main.chain2Texture.Height)), color, rotation3, new Vector2((float)Main.chain2Texture.Width * 0.5f, (float)Main.chain2Texture.Height * 0.5f), 1f, SpriteEffects.None, 0.0f);
                                        else if (Main.projectile[index].type == 35)
                                            this.spriteBatch.Draw(Main.chain6Texture, new Vector2(vector2.X - Main.screenPosition.X, vector2.Y - Main.screenPosition.Y), new Rectangle?(new Rectangle(0, 0, Main.chain6Texture.Width, Main.chain6Texture.Height)), color, rotation3, new Vector2((float)Main.chain6Texture.Width * 0.5f, (float)Main.chain6Texture.Height * 0.5f), 1f, SpriteEffects.None, 0.0f);
                                        else
                                            this.spriteBatch.Draw(Main.chain3Texture, new Vector2(vector2.X - Main.screenPosition.X, vector2.Y - Main.screenPosition.Y), new Rectangle?(new Rectangle(0, 0, Main.chain3Texture.Width, Main.chain3Texture.Height)), color, rotation3, new Vector2((float)Main.chain3Texture.Width * 0.5f, (float)Main.chain3Texture.Height * 0.5f), 1f, SpriteEffects.None, 0.0f);
                                    }
                                }
                            }
                            Color newColor = Lighting.GetColor((int)((double)Main.projectile[index].position.X + (double)Main.projectile[index].width * 0.5) / 16, (int)(((double)Main.projectile[index].position.Y + (double)Main.projectile[index].height * 0.5) / 16.0));
                            if (Main.projectile[index].type == 14)
                                newColor = Color.White;
                            int num28 = 0;
                            if (Main.projectile[index].type == 16)
                                num28 = 6;
                            if (Main.projectile[index].type == 17 || Main.projectile[index].type == 31)
                                num28 = 2;
                            if (Main.projectile[index].type == 25 || Main.projectile[index].type == 26 || Main.projectile[index].type == 30)
                                num28 = 6;
                            if (Main.projectile[index].type == 28 || Main.projectile[index].type == 37)
                                num28 = 8;
                            if (Main.projectile[index].type == 29)
                                num28 = 11;
                            float x1 = (float)((double)(Main.projectileTexture[Main.projectile[index].type].Width - Main.projectile[index].width) * 0.5 + (double)Main.projectile[index].width * 0.5);
                            this.spriteBatch.Draw(Main.projectileTexture[Main.projectile[index].type], new Vector2(Main.projectile[index].position.X - Main.screenPosition.X + x1, Main.projectile[index].position.Y - Main.screenPosition.Y + (float)(Main.projectile[index].height / 2)), new Rectangle?(new Rectangle(0, 0, Main.projectileTexture[Main.projectile[index].type].Width, Main.projectileTexture[Main.projectile[index].type].Height)), Main.projectile[index].GetAlpha(newColor), Main.projectile[index].rotation, new Vector2(x1, (float)(Main.projectile[index].height / 2 + num28)), Main.projectile[index].scale, SpriteEffects.None, 0.0f);
                        }
                    }
                    for (int index = 0; index < (int)byte.MaxValue; ++index)
                    {
                        if (Main.player[index].active)
                        {
                            if (Main.player[index].head == 5 && Main.player[index].body == 5 && Main.player[index].legs == 5 || Main.player[index].head == 7 && Main.player[index].body == 7 && Main.player[index].legs == 7 || Main.player[index].head == 22 && Main.player[index].body == 14 && Main.player[index].legs == 14)
                            {
                                Vector2 vector2 = Main.player[index].position;
                                Main.player[index].position = Main.player[index].shadowPos[0];
                                Main.player[index].shadow = 0.5f;
                                this.DrawPlayer(Main.player[index]);
                                Main.player[index].position = Main.player[index].shadowPos[1];
                                Main.player[index].shadow = 0.7f;
                                this.DrawPlayer(Main.player[index]);
                                Main.player[index].position = Main.player[index].shadowPos[2];
                                Main.player[index].shadow = 0.9f;
                                this.DrawPlayer(Main.player[index]);
                                Main.player[index].position = vector2;
                                Main.player[index].shadow = 0.0f;
                            }
                            this.DrawPlayer(Main.player[index]);
                        }
                    }
                    for (int index = 0; index < 200; ++index)
                    {
                        if (Main.item[index].active && Main.item[index].type > 0)
                        {
                            int num12 = (int)((double)Main.item[index].position.X + (double)Main.item[index].width * 0.5) / 16 - firstX + 21;
                            int num13 = (int)((double)Main.item[index].position.Y + (double)Main.item[index].height * 0.5) / 16 - firstY + 21;
                            Color color = Lighting.GetColor((int)((double)Main.item[index].position.X + (double)Main.item[index].width * 0.5) / 16, (int)((double)Main.item[index].position.Y + (double)Main.item[index].height * 0.5) / 16);
                            this.spriteBatch.Draw(Main.itemTexture[Main.item[index].type], new Vector2(Main.item[index].position.X - Main.screenPosition.X + (float)(Main.item[index].width / 2) - (float)(Main.itemTexture[Main.item[index].type].Width / 2), Main.item[index].position.Y - Main.screenPosition.Y + (float)(Main.item[index].height / 2) - (float)(Main.itemTexture[Main.item[index].type].Height / 2)), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.item[index].type].Width, Main.itemTexture[Main.item[index].type].Height)), Main.item[index].GetAlpha(color), 0.0f, new Vector2(), 1f, SpriteEffects.None, 0.0f);
                            if (Main.item[index].color != new Color())
                                this.spriteBatch.Draw(Main.itemTexture[Main.item[index].type], new Vector2(Main.item[index].position.X - Main.screenPosition.X + (float)(Main.item[index].width / 2) - (float)(Main.itemTexture[Main.item[index].type].Width / 2), Main.item[index].position.Y - Main.screenPosition.Y + (float)(Main.item[index].height / 2) - (float)(Main.itemTexture[Main.item[index].type].Height / 2)), new Rectangle?(new Rectangle(0, 0, Main.itemTexture[Main.item[index].type].Width, Main.itemTexture[Main.item[index].type].Height)), Main.item[index].GetColor(color), 0.0f, new Vector2(), 1f, SpriteEffects.None, 0.0f);
                        }
                    }
                    Rectangle rectangle4 = new Rectangle((int)Main.screenPosition.X - 50, (int)Main.screenPosition.Y - 50, Main.screenWidth + 100, Main.screenHeight + 100);
                    for (int index = 0; index < 2000; ++index)
                    {
                        if (Main.dust[index].active)
                        {
                            if (new Rectangle((int)Main.dust[index].position.X, (int)Main.dust[index].position.Y, 4, 4).Intersects(rectangle4))
                            {
                                Color newColor = Lighting.GetColor((int)((double)Main.dust[index].position.X + 4.0) / 16, (int)((double)Main.dust[index].position.Y + 4.0) / 16);
                                if (Main.dust[index].type == 6 || Main.dust[index].type == 15 || Main.dust[index].noLight)
                                    newColor = Color.White;
                                this.spriteBatch.Draw(Main.dustTexture, Main.dust[index].position - Main.screenPosition, new Rectangle?(Main.dust[index].frame), Main.dust[index].GetAlpha(newColor), Main.dust[index].rotation, new Vector2(4f, 4f), Main.dust[index].scale, SpriteEffects.None, 0.0f);
                                if (Main.dust[index].color != new Color())
                                    this.spriteBatch.Draw(Main.dustTexture, Main.dust[index].position - Main.screenPosition, new Rectangle?(Main.dust[index].frame), Main.dust[index].GetColor(newColor), Main.dust[index].rotation, new Vector2(4f, 4f), Main.dust[index].scale, SpriteEffects.None, 0.0f);
                            }
                            else
                                Main.dust[index].active = false;
                        }
                    }
                    this.DrawWater(false);
                    if (!Main.hideUI)
                    {
                        for (int index1 = 0; index1 < (int)byte.MaxValue; ++index1)
                        {
                            if (Main.player[index1].active && Main.player[index1].chatShowTime > 0 && index1 != Main.myPlayer && !Main.player[index1].dead)
                            {
                                Vector2 vector2_1 = Main.fontMouseText.MeasureString(Main.player[index1].chatText);
                                Vector2 vector2_2;
                                vector2_2.X = (float)((double)Main.player[index1].position.X + (double)(Main.player[index1].width / 2) - (double)vector2_1.X / 2.0);
                                vector2_2.Y = (float)((double)Main.player[index1].position.Y - (double)vector2_1.Y - 2.0);
                                for (int index2 = 0; index2 < 5; ++index2)
                                {
                                    int num12 = 0;
                                    int num13 = 0;
                                    Color color = Color.Black;
                                    if (index2 == 0)
                                        num12 = -2;
                                    if (index2 == 1)
                                        num12 = 2;
                                    if (index2 == 2)
                                        num13 = -2;
                                    if (index2 == 3)
                                        num13 = 2;
                                    if (index2 == 4)
                                        color = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
                                    this.spriteBatch.DrawString(Main.fontMouseText, Main.player[index1].chatText, new Vector2(vector2_2.X + (float)num12 - Main.screenPosition.X, vector2_2.Y + (float)num13 - Main.screenPosition.Y), color, 0.0f, new Vector2(), 1f, SpriteEffects.None, 0.0f);
                                }
                            }
                        }
                        for (int index1 = 0; index1 < 100; ++index1)
                        {
                            if (Main.combatText[index1].active)
                            {
                                Vector2 vector2 = Main.fontCombatText.MeasureString(Main.combatText[index1].text);
                                Vector2 origin = new Vector2(vector2.X * 0.5f, vector2.Y * 0.5f);
                                int num12 = (int)((double)byte.MaxValue - (double)byte.MaxValue * (double)Main.combatText[index1].scale);
                                float num13 = (float)Main.combatText[index1].color.R;
                                float num17 = (float)Main.combatText[index1].color.G;
                                float num18 = (float)Main.combatText[index1].color.B;
                                float num19 = (float)Main.combatText[index1].color.A;
                                float num20 = num13 * (float)((double)Main.combatText[index1].scale * (double)Main.combatText[index1].alpha * 0.300000011920929);
                                float num21 = num18 * (float)((double)Main.combatText[index1].scale * (double)Main.combatText[index1].alpha * 0.300000011920929);
                                float num22 = num17 * (float)((double)Main.combatText[index1].scale * (double)Main.combatText[index1].alpha * 0.300000011920929);
                                float num23 = num19 * (Main.combatText[index1].scale * Main.combatText[index1].alpha);
                                Color color = new Color((int)num20, (int)num22, (int)num21, (int)num23);
                                for (int index2 = 0; index2 < 5; ++index2)
                                {
                                    int num26 = 0;
                                    int num27 = 0;
                                    if (index2 == 0)
                                        --num26;
                                    else if (index2 == 1)
                                        ++num26;
                                    else if (index2 == 2)
                                        --num27;
                                    else if (index2 == 3)
                                    {
                                        ++num27;
                                    }
                                    else
                                    {
                                        float num28 = (float)Main.combatText[index1].color.R * Main.combatText[index1].scale * Main.combatText[index1].alpha;
                                        float num29 = (float)Main.combatText[index1].color.B * Main.combatText[index1].scale * Main.combatText[index1].alpha;
                                        float num30 = (float)Main.combatText[index1].color.G * Main.combatText[index1].scale * Main.combatText[index1].alpha;
                                        float num31 = (float)Main.combatText[index1].color.A * Main.combatText[index1].scale * Main.combatText[index1].alpha;
                                        color = new Color((int)num28, (int)num30, (int)num29, (int)num31);
                                    }
                                    this.spriteBatch.DrawString(Main.fontCombatText, Main.combatText[index1].text, new Vector2(Main.combatText[index1].position.X - Main.screenPosition.X + (float)num26 + origin.X, Main.combatText[index1].position.Y - Main.screenPosition.Y + (float)num27 + origin.Y), color, Main.combatText[index1].rotation, origin, Main.combatText[index1].scale, SpriteEffects.None, 0.0f);
                                }
                            }
                        }
                        if (Main.netMode == 1 && Netplay.clientSock.statusText != "" && Netplay.clientSock.statusText != null)
                        {
                            string text = string.Concat(new object[4]
              {
                (object) Netplay.clientSock.statusText,
                (object) ": ",
                (object) (int) ((double) Netplay.clientSock.statusCount / (double) Netplay.clientSock.statusMax * 100.0),
                (object) "%"
              });
                            this.spriteBatch.DrawString(Main.fontMouseText, text, new Vector2((float)(628.0 - (double)Main.fontMouseText.MeasureString(text).X * 0.5) + (float)(Main.screenWidth - 800), 84f), new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), 0.0f, new Vector2(), 1f, SpriteEffects.None, 0.0f);
                        }
                        this.DrawFPS();
                        this.DrawInterface();
                    }
                    this.spriteBatch.End();
                    Main.mouseLeftRelease = Main.mouseState.LeftButton != ButtonState.Pressed && true;
                    Main.mouseRightRelease = Main.mouseState.RightButton != ButtonState.Pressed && true;
                    if (Main.mouseState.RightButton != ButtonState.Pressed)
                        Main.stackSplit = 0;
                    if (Main.stackSplit > 0)
                        --Main.stackSplit;
                }
            }
        }

        private static void UpdateInvasion()
        {
            if (Main.invasionType > 0)
            {
                if (Main.invasionSize <= 0)
                {
                    Main.InvasionWarning();
                    Main.invasionType = 0;
                    Main.invasionDelay = 7;
                }
                if (Main.invasionX != (double)Main.spawnTileX)
                {
                    float num = 0.2f;
                    if (Main.invasionX > (double)Main.spawnTileX)
                    {
                        Main.invasionX -= (double)num;
                        if (Main.invasionX <= (double)Main.spawnTileX)
                        {
                            Main.invasionX = (double)Main.spawnTileX;
                            Main.InvasionWarning();
                        }
                        else
                            --Main.invasionWarn;
                    }
                    else if (Main.invasionX < (double)Main.spawnTileX)
                    {
                        Main.invasionX += (double)num;
                        if (Main.invasionX >= (double)Main.spawnTileX)
                        {
                            Main.invasionX = (double)Main.spawnTileX;
                            Main.InvasionWarning();
                        }
                        else
                            --Main.invasionWarn;
                    }
                    if (Main.invasionWarn <= 0)
                    {
                        Main.invasionWarn = 3600;
                        Main.InvasionWarning();
                    }
                }
            }
        }

        private static void InvasionWarning()
        {
            if (Main.invasionType != 0)
            {
                string str = Main.invasionSize > 0 ? (Main.invasionX >= (double)Main.spawnTileX ? (Main.invasionX <= (double)Main.spawnTileX ? "The goblin army has arrived!" : "A goblin army is approaching from the east!") : "A goblin army is approaching from the west!") : "The goblin army has been defeated!";
                if (Main.netMode == 0)
                    Main.NewText(str, (byte)175, (byte)75, byte.MaxValue);
                else if (Main.netMode == 2)
                    NetMessage.SendData(25, -1, -1, str, (int)byte.MaxValue, 175f, 75f, (float)byte.MaxValue);
            }
        }

        private static void StartInvasion()
        {
            if (WorldGen.shadowOrbSmashed && (Main.invasionType == 0 && Main.invasionDelay == 0))
            {
                int num = 0;
                for (int index = 0; index < (int)byte.MaxValue; ++index)
                {
                    if (Main.player[index].active && Main.player[index].statLife >= 200)
                        ++num;
                }
                if (num > 0)
                {
                    Main.invasionType = 1;
                    Main.invasionSize = 100 + 50 * num;
                    Main.invasionWarn = 0;
                    Main.invasionX = Main.rand.Next(2) != 0 ? (double)Main.maxTilesX : 0.0;
                }
            }
        }

        private static void UpdateClient()
        {
            if (Main.myPlayer == (int)byte.MaxValue)
                Netplay.disconnect = true;
            ++Main.netPlayCounter;
            if (Main.netPlayCounter > 3600)
                Main.netPlayCounter = 0;
            if (Math.IEEERemainder((double)Main.netPlayCounter, 300.0) == 0.0)
            {
                NetMessage.SendData(13, -1, -1, "", Main.myPlayer, 0.0f, 0.0f, 0.0f);
                NetMessage.SendData(36, -1, -1, "", Main.myPlayer, 0.0f, 0.0f, 0.0f);
            }
            if (Math.IEEERemainder((double)Main.netPlayCounter, 600.0) == 0.0)
            {
                NetMessage.SendData(16, -1, -1, "", Main.myPlayer, 0.0f, 0.0f, 0.0f);
                NetMessage.SendData(40, -1, -1, "", Main.myPlayer, 0.0f, 0.0f, 0.0f);
            }
            if (Netplay.clientSock.active)
            {
                ++Netplay.clientSock.timeOut;
                if (!Main.stopTimeOuts && Netplay.clientSock.timeOut > 60 * Main.timeOut)
                {
                    Main.statusText = "Connection timed out";
                    Netplay.disconnect = true;
                }
            }
            for (int whoAmI = 0; whoAmI < 200; ++whoAmI)
            {
                if (Main.item[whoAmI].active && Main.item[whoAmI].owner == Main.myPlayer)
                    Main.item[whoAmI].FindOwner(whoAmI);
            }
        }

        private static void UpdateServer()
        {
            ++Main.netPlayCounter;
            if (Main.netPlayCounter > 3600)
            {
                NetMessage.SendData(7, -1, -1, "", 0, 0.0f, 0.0f, 0.0f);
                NetMessage.syncPlayers();
                Main.netPlayCounter = 0;
            }
            for (int index = 0; index < Main.maxNetPlayers; ++index)
            {
                if (Main.player[index].active && Netplay.serverSock[index].active)
                    Netplay.serverSock[index].SpamUpdate();
            }
            Math.IEEERemainder((double)Main.netPlayCounter, 60.0);
            bool flag1 = 0 == 0;
            if (Math.IEEERemainder((double)Main.netPlayCounter, 360.0) == 0.0)
            {
                bool flag2 = true;
                int number = Main.lastItemUpdate;
                int num = 0;
                while (flag2)
                {
                    ++number;
                    if (number >= 200)
                        number = 0;
                    ++num;
                    if (!Main.item[number].active || Main.item[number].owner == (int)byte.MaxValue)
                        NetMessage.SendData(21, -1, -1, "", number, 0.0f, 0.0f, 0.0f);
                    if (num >= Main.maxItemUpdates || number == Main.lastItemUpdate)
                        flag2 = false;
                }
                Main.lastItemUpdate = number;
            }
            for (int whoAmI = 0; whoAmI < 200; ++whoAmI)
            {
                if (Main.item[whoAmI].active && (Main.item[whoAmI].owner == (int)byte.MaxValue || !Main.player[Main.item[whoAmI].owner].active))
                    Main.item[whoAmI].FindOwner(whoAmI);
            }
            for (int index1 = 0; index1 < (int)byte.MaxValue; ++index1)
            {
                if (Netplay.serverSock[index1].active)
                {
                    ++Netplay.serverSock[index1].timeOut;
                    if (!Main.stopTimeOuts && Netplay.serverSock[index1].timeOut > 60 * Main.timeOut)
                        Netplay.serverSock[index1].kill = true;
                }
                if (Main.player[index1].active)
                {
                    int sectionX = Netplay.GetSectionX((int)((double)Main.player[index1].position.X / 16.0));
                    int sectionY1 = Netplay.GetSectionY((int)((double)Main.player[index1].position.Y / 16.0));
                    int num = 0;
                    for (int index2 = sectionX - 1; index2 < sectionX + 2; ++index2)
                    {
                        for (int index3 = sectionY1 - 1; index3 < sectionY1 + 2; ++index3)
                        {
                            if (index2 >= 0 && index2 < Main.maxSectionsX && index3 >= 0 && index3 < Main.maxSectionsY && !Netplay.serverSock[index1].tileSection[index2, index3])
                                ++num;
                        }
                    }
                    if (num > 0)
                    {
                        int number = num * 150;
                        NetMessage.SendData(9, index1, -1, "Recieving tile data", number, 0.0f, 0.0f, 0.0f);
                        Netplay.serverSock[index1].statusText2 = "is recieving tile data";
                        Netplay.serverSock[index1].statusMax += number;
                        for (int index2 = sectionX - 1; index2 < sectionX + 2; ++index2)
                        {
                            for (int sectionY2 = sectionY1 - 1; sectionY2 < sectionY1 + 2; ++sectionY2)
                            {
                                if (index2 >= 0 && index2 < Main.maxSectionsX && sectionY2 >= 0 && sectionY2 < Main.maxSectionsY && !Netplay.serverSock[index1].tileSection[index2, sectionY2])
                                {
                                    NetMessage.SendSection(index1, index2, sectionY2);
                                    NetMessage.SendData(11, index1, -1, "", index2, (float)sectionY2, (float)index2, (float)sectionY2);
                                }
                            }
                        }
                    }
                }
            }
        }

        public static void NewText(string newText, byte R = (byte) 255, byte G = (byte) 255, byte B = (byte) 255)
        {
            for (int index = Main.numChatLines - 1; index > 0; --index)
            {
                Main.chatLine[index].text = Main.chatLine[index - 1].text;
                Main.chatLine[index].showTime = Main.chatLine[index - 1].showTime;
                Main.chatLine[index].color = Main.chatLine[index - 1].color;
            }
            int num = (int)R != 0 || (int)G != 0 ? 1 : ((int)B != 0 ? 1 : 0);
            Main.chatLine[0].color = num != 0 ? new Color((int)R, (int)G, (int)B) : Color.White;
            Main.chatLine[0].text = newText;
            Main.chatLine[0].showTime = Main.chatLength;
            Main.PlaySound(12, -1, -1, 1);
        }

        private static void UpdateTime()
        {
            ++Main.time;
            if (!Main.dayTime)
            {
                if (WorldGen.spawnEye && Main.netMode != 1 && Main.time > 4860.0)
                {
                    for (int plr = 0; plr < (int)byte.MaxValue; ++plr)
                    {
                        if (Main.player[plr].active && !Main.player[plr].dead && (double)Main.player[plr].position.Y < Main.worldSurface * 16.0)
                        {
                            NPC.SpawnOnPlayer(plr, 4);
                            WorldGen.spawnEye = false;
                            break;
                        }
                    }
                }
                if (Main.time > 32400.0)
                {
                    if (Main.invasionDelay > 0)
                        --Main.invasionDelay;
                    WorldGen.spawnNPC = 0;
                    Main.checkForSpawns = 0;
                    Main.time = 0.0;
                    Main.bloodMoon = false;
                    Main.dayTime = true;
                    ++Main.moonPhase;
                    if (Main.moonPhase >= 8)
                        Main.moonPhase = 0;
                    if (Main.netMode == 2)
                    {
                        NetMessage.SendData(7, -1, -1, "", 0, 0.0f, 0.0f, 0.0f);
                        WorldGen.saveAndPlay();
                    }
                    if (Main.netMode != 1 && Main.rand.Next(15) == 0)
                        Main.StartInvasion();
                }
                if (Main.time > 16200.0 && WorldGen.spawnMeteor)
                {
                    WorldGen.spawnMeteor = false;
                    WorldGen.dropMeteor();
                }
            }
            else
            {
                if (Main.time > 54000.0)
                {
                    WorldGen.spawnNPC = 0;
                    Main.checkForSpawns = 0;
                    if (Main.rand.Next(50) == 0 && Main.netMode != 1 && WorldGen.shadowOrbSmashed)
                        WorldGen.spawnMeteor = true;
                    if (!NPC.downedBoss1 && Main.netMode != 1)
                    {
                        bool flag = false;
                        for (int index = 0; index < (int)byte.MaxValue; ++index)
                        {
                            if (Main.player[index].active && Main.player[index].statLifeMax >= 200)
                            {
                                flag = true;
                                break;
                            }
                        }
                        if (flag && Main.rand.Next(3) == 0)
                        {
                            int num = 0;
                            for (int index = 0; index < 1000; ++index)
                            {
                                if (Main.npc[index].active && Main.npc[index].townNPC)
                                    ++num;
                            }
                            if (num >= 4)
                            {
                                WorldGen.spawnEye = true;
                                if (Main.netMode == 0)
                                    Main.NewText("You feel an evil presence watching you...", (byte)50, byte.MaxValue, (byte)130);
                                else if (Main.netMode == 2)
                                    NetMessage.SendData(25, -1, -1, "You feel an evil presence watching you...", (int)byte.MaxValue, 50f, (float)byte.MaxValue, 130f);
                            }
                        }
                    }
                    if (!WorldGen.spawnEye && Main.moonPhase != 4 && Main.rand.Next(7) == 0 && Main.netMode != 1)
                    {
                        for (int index = 0; index < (int)byte.MaxValue; ++index)
                        {
                            if (Main.player[index].active && Main.player[index].statLifeMax > 100)
                            {
                                Main.bloodMoon = true;
                                break;
                            }
                        }
                        if (Main.bloodMoon)
                        {
                            if (Main.netMode == 0)
                                Main.NewText("The Blood Moon is rising...", (byte)50, byte.MaxValue, (byte)130);
                            else if (Main.netMode == 2)
                                NetMessage.SendData(25, -1, -1, "The Blood Moon is rising...", (int)byte.MaxValue, 50f, (float)byte.MaxValue, 130f);
                        }
                    }
                    Main.time = 0.0;
                    Main.dayTime = false;
                    if (Main.netMode == 2)
                        NetMessage.SendData(7, -1, -1, "", 0, 0.0f, 0.0f, 0.0f);
                }
                if (Main.netMode != 1)
                {
                    ++Main.checkForSpawns;
                    if (Main.checkForSpawns >= 7200)
                    {
                        int num1 = 0;
                        for (int index = 0; index < (int)byte.MaxValue; ++index)
                        {
                            if (Main.player[index].active)
                                ++num1;
                        }
                        Main.checkForSpawns = 0;
                        WorldGen.spawnNPC = 0;
                        int num2 = 0;
                        int num3 = 0;
                        int num4 = 0;
                        int num5 = 0;
                        int num6 = 0;
                        int num7 = 0;
                        int num8 = 0;
                        int num9 = 0;
                        int num10 = 0;
                        for (int npc = 0; npc < 1000; ++npc)
                        {
                            if (Main.npc[npc].active && Main.npc[npc].townNPC)
                            {
                                if (Main.npc[npc].type != 37 && !Main.npc[npc].homeless)
                                    WorldGen.QuickFindHome(npc);
                                else
                                    ++num7;
                                if (Main.npc[npc].type == 17)
                                    ++num2;
                                if (Main.npc[npc].type == 18)
                                    ++num3;
                                if (Main.npc[npc].type == 19)
                                    ++num5;
                                if (Main.npc[npc].type == 20)
                                    ++num4;
                                if (Main.npc[npc].type == 22)
                                    ++num6;
                                if (Main.npc[npc].type == 38)
                                    ++num8;
                                if (Main.npc[npc].type == 54)
                                    ++num9;
                                ++num10;
                            }
                        }
                        if (WorldGen.spawnNPC == 0)
                        {
                            int num11 = 0;
                            bool flag1 = false;
                            int num12 = 0;
                            bool flag2 = false;
                            bool flag3 = false;
                            for (int index1 = 0; index1 < (int)byte.MaxValue; ++index1)
                            {
                                if (Main.player[index1].active)
                                {
                                    for (int index2 = 0; index2 < 44; ++index2)
                                    {
                                        if (Main.player[index1].inventory[index2] != null & Main.player[index1].inventory[index2].stack > 0)
                                        {
                                            if (Main.player[index1].inventory[index2].type == 71)
                                                num11 += Main.player[index1].inventory[index2].stack;
                                            if (Main.player[index1].inventory[index2].type == 72)
                                                num11 += Main.player[index1].inventory[index2].stack * 100;
                                            if (Main.player[index1].inventory[index2].type == 73)
                                                num11 += Main.player[index1].inventory[index2].stack * 10000;
                                            if (Main.player[index1].inventory[index2].type == 74)
                                                num11 += Main.player[index1].inventory[index2].stack * 1000000;
                                            if (Main.player[index1].inventory[index2].type == 95 || Main.player[index1].inventory[index2].type == 96 || (Main.player[index1].inventory[index2].type == 97 || Main.player[index1].inventory[index2].type == 98) || Main.player[index1].inventory[index2].useAmmo == 14)
                                                flag2 = true;
                                            if (Main.player[index1].inventory[index2].type == 166 || Main.player[index1].inventory[index2].type == 167 || Main.player[index1].inventory[index2].type == 168 || Main.player[index1].inventory[index2].type == 235)
                                                flag3 = true;
                                        }
                                    }
                                    int num13 = Main.player[index1].statLifeMax / 20;
                                    if (num13 > 5)
                                        flag1 = true;
                                    num12 += num13;
                                }
                            }
                            if (WorldGen.spawnNPC == 0 && num6 < 1)
                                WorldGen.spawnNPC = 22;
                            if (WorldGen.spawnNPC == 0 && (double)num11 > 5000.0 && num2 < 1)
                                WorldGen.spawnNPC = 17;
                            if (WorldGen.spawnNPC == 0 && flag1 && num3 < 1)
                                WorldGen.spawnNPC = 18;
                            if (WorldGen.spawnNPC == 0 && flag2 && num5 < 1)
                                WorldGen.spawnNPC = 19;
                            if (WorldGen.spawnNPC == 0 && (NPC.downedBoss1 || NPC.downedBoss2 || NPC.downedBoss3) && num4 < 1)
                                WorldGen.spawnNPC = 20;
                            if (WorldGen.spawnNPC == 0 && flag3 && num2 > 0 && num8 < 1)
                                WorldGen.spawnNPC = 38;
                            if (WorldGen.spawnNPC == 0 && NPC.downedBoss3 && num9 < 1)
                                WorldGen.spawnNPC = 54;
                            if (WorldGen.spawnNPC == 0 && num11 > 100000 && num2 < 2 && num1 > 2)
                                WorldGen.spawnNPC = 17;
                            if (WorldGen.spawnNPC == 0 && num12 >= 20 && num3 < 2 && num1 > 2)
                                WorldGen.spawnNPC = 18;
                            if (WorldGen.spawnNPC == 0 && num11 > 5000000 && num2 < 3 && num1 > 4)
                                WorldGen.spawnNPC = 17;
                            if (!NPC.downedBoss3 && num7 == 0)
                            {
                                int index = NPC.NewNPC(Main.dungeonX * 16 + 8, Main.dungeonY * 16, 37, 0);
                                Main.npc[index].homeless = false;
                                Main.npc[index].homeTileX = Main.dungeonX;
                                Main.npc[index].homeTileY = Main.dungeonY;
                            }
                        }
                    }
                }
            }
        }

        public static double CalculateDamage(int Damage, int Defense)
        {
            double num = (double)Damage - (double)Defense * 0.5;
            if (num < 1.0)
                num = 1.0;
            return num;
        }

        public static void PlaySound(int type, int x = -1, int y = -1, int style = 1)
        {
            if (!Main.dedServ && (double)Main.soundVolume != 0.0)
            {
                bool flag = false;
                float num1 = 1f;
                float num2 = 0.0f;
                if (x == -1 || y == -1)
                    flag = true;
                else if (WorldGen.gen || Main.netMode == 2)
                {
                    return;
                }
                else
                {
                    Rectangle rectangle1 = new Rectangle((int)((double)Main.screenPosition.X - (double)(Main.screenWidth * 2)), (int)((double)Main.screenPosition.Y - (double)(Main.screenHeight * 2)), Main.screenWidth * 5, Main.screenHeight * 5);
                    Rectangle rectangle2 = new Rectangle(x, y, 1, 1);
                    Vector2 vector2 = new Vector2(Main.screenPosition.X + (float)Main.screenWidth * 0.5f, Main.screenPosition.Y + (float)Main.screenHeight * 0.5f);
                    if (rectangle2.Intersects(rectangle1))
                        flag = true;
                    if (flag)
                    {
                        num2 = (float)(((double)x - (double)vector2.X) / ((double)Main.screenWidth * 0.5));
                        float num3 = Math.Abs((float)x - vector2.X);
                        float num4 = Math.Abs((float)y - vector2.Y);
                        num1 = (float)(1.0 - Math.Sqrt((double)num3 * (double)num3 + (double)num4 * (double)num4) / ((double)Main.screenWidth * 1.5));
                    }
                }
                if ((double)num2 < -1.0)
                    num2 = -1f;
                if ((double)num2 > 1.0)
                    num2 = 1f;
                if ((double)num1 > 1.0)
                    num1 = 1f;
                if ((double)num1 > 0.0 && flag)
                {
                    float num3 = num1 * Main.soundVolume;
                    if (type == 0)
                    {
                        int index = Main.rand.Next(3);
                        Main.soundInstanceDig[index].Stop();
                        Main.soundInstanceDig[index] = Main.soundDig[index].CreateInstance();
                        Main.soundInstanceDig[index].Volume = num3;
                        Main.soundInstanceDig[index].Pan = num2;
                        Main.soundInstanceDig[index].Play();
                    }
                    else if (type == 1)
                    {
                        int index = Main.rand.Next(3);
                        Main.soundInstancePlayerHit[index].Stop();
                        Main.soundInstancePlayerHit[index] = Main.soundPlayerHit[index].CreateInstance();
                        Main.soundInstancePlayerHit[index].Volume = num3;
                        Main.soundInstancePlayerHit[index].Pan = num2;
                        Main.soundInstancePlayerHit[index].Play();
                    }
                    else if (type == 2)
                    {
                        if (style != 9 && style != 10)
                            Main.soundInstanceItem[style].Stop();
                        Main.soundInstanceItem[style] = Main.soundItem[style].CreateInstance();
                        Main.soundInstanceItem[style].Volume = num3;
                        Main.soundInstanceItem[style].Pan = num2;
                        Main.soundInstanceItem[style].Play();
                    }
                    else if (type == 3)
                    {
                        Main.soundInstanceNPCHit[style].Stop();
                        Main.soundInstanceNPCHit[style] = Main.soundNPCHit[style].CreateInstance();
                        Main.soundInstanceNPCHit[style].Volume = num3;
                        Main.soundInstanceNPCHit[style].Pan = num2;
                        Main.soundInstanceNPCHit[style].Play();
                    }
                    else if (type == 4)
                    {
                        Main.soundInstanceNPCKilled[style] = Main.soundNPCKilled[style].CreateInstance();
                        Main.soundInstanceNPCKilled[style].Volume = num3;
                        Main.soundInstanceNPCKilled[style].Pan = num2;
                        Main.soundInstanceNPCKilled[style].Play();
                    }
                    else if (type == 5)
                    {
                        Main.soundInstancePlayerKilled.Stop();
                        Main.soundInstancePlayerKilled = Main.soundPlayerKilled.CreateInstance();
                        Main.soundInstancePlayerKilled.Volume = num3;
                        Main.soundInstancePlayerKilled.Pan = num2;
                        Main.soundInstancePlayerKilled.Play();
                    }
                    else if (type == 6)
                    {
                        Main.soundInstanceGrass.Stop();
                        Main.soundInstanceGrass = Main.soundGrass.CreateInstance();
                        Main.soundInstanceGrass.Volume = num3;
                        Main.soundInstanceGrass.Pan = num2;
                        Main.soundInstanceGrass.Play();
                    }
                    else if (type == 7)
                    {
                        Main.soundInstanceGrab.Stop();
                        Main.soundInstanceGrab = Main.soundGrab.CreateInstance();
                        Main.soundInstanceGrab.Volume = num3;
                        Main.soundInstanceGrab.Pan = num2;
                        Main.soundInstanceGrab.Play();
                    }
                    else if (type == 8)
                    {
                        Main.soundInstanceDoorOpen.Stop();
                        Main.soundInstanceDoorOpen = Main.soundDoorOpen.CreateInstance();
                        Main.soundInstanceDoorOpen.Volume = num3;
                        Main.soundInstanceDoorOpen.Pan = num2;
                        Main.soundInstanceDoorOpen.Play();
                    }
                    else if (type == 9)
                    {
                        Main.soundInstanceDoorClosed.Stop();
                        Main.soundInstanceDoorClosed = Main.soundDoorClosed.CreateInstance();
                        Main.soundInstanceDoorClosed.Volume = num3;
                        Main.soundInstanceDoorClosed.Pan = num2;
                        Main.soundInstanceDoorClosed.Play();
                    }
                    else if (type == 10)
                    {
                        Main.soundInstanceMenuOpen.Stop();
                        Main.soundInstanceMenuOpen = Main.soundMenuOpen.CreateInstance();
                        Main.soundInstanceMenuOpen.Volume = num3;
                        Main.soundInstanceMenuOpen.Pan = num2;
                        Main.soundInstanceMenuOpen.Play();
                    }
                    else if (type == 11)
                    {
                        Main.soundInstanceMenuClose.Stop();
                        Main.soundInstanceMenuClose = Main.soundMenuClose.CreateInstance();
                        Main.soundInstanceMenuClose.Volume = num3;
                        Main.soundInstanceMenuClose.Pan = num2;
                        Main.soundInstanceMenuClose.Play();
                    }
                    else if (type == 12)
                    {
                        Main.soundInstanceMenuTick.Stop();
                        Main.soundInstanceMenuTick = Main.soundMenuTick.CreateInstance();
                        Main.soundInstanceMenuTick.Volume = num3;
                        Main.soundInstanceMenuTick.Pan = num2;
                        Main.soundInstanceMenuTick.Play();
                    }
                    else if (type == 13)
                    {
                        Main.soundInstanceShatter.Stop();
                        Main.soundInstanceShatter = Main.soundShatter.CreateInstance();
                        Main.soundInstanceShatter.Volume = num3;
                        Main.soundInstanceShatter.Pan = num2;
                        Main.soundInstanceShatter.Play();
                    }
                    else if (type == 14)
                    {
                        int index = Main.rand.Next(3);
                        Main.soundInstanceZombie[index] = Main.soundZombie[index].CreateInstance();
                        Main.soundInstanceZombie[index].Volume = num3 * 0.4f;
                        Main.soundInstanceZombie[index].Pan = num2;
                        Main.soundInstanceZombie[index].Play();
                    }
                    else if (type == 15)
                    {
                        Main.soundInstanceRoar[style] = Main.soundRoar[style].CreateInstance();
                        Main.soundInstanceRoar[style].Volume = num3;
                        Main.soundInstanceRoar[style].Pan = num2;
                        Main.soundInstanceRoar[style].Play();
                    }
                    else if (type == 16)
                    {
                        Main.soundInstanceDoubleJump.Stop();
                        Main.soundInstanceDoubleJump = Main.soundDoubleJump.CreateInstance();
                        Main.soundInstanceDoubleJump.Volume = num3;
                        Main.soundInstanceDoubleJump.Pan = num2;
                        Main.soundInstanceDoubleJump.Play();
                    }
                    else if (type == 17)
                    {
                        Main.soundInstanceRun.Stop();
                        Main.soundInstanceRun = Main.soundRun.CreateInstance();
                        Main.soundInstanceRun.Volume = num3;
                        Main.soundInstanceRun.Pan = num2;
                        Main.soundInstanceRun.Play();
                    }
                    else if (type == 18)
                    {
                        Main.soundInstanceCoins = Main.soundCoins.CreateInstance();
                        Main.soundInstanceCoins.Volume = num3;
                        Main.soundInstanceCoins.Pan = num2;
                        Main.soundInstanceCoins.Play();
                    }
                    else if (type == 19)
                    {
                        Main.soundInstanceSplash[style] = Main.soundSplash[style].CreateInstance();
                        Main.soundInstanceSplash[style].Volume = num3;
                        Main.soundInstanceSplash[style].Pan = num2;
                        Main.soundInstanceSplash[style].Play();
                    }
                    else if (type == 20)
                    {
                        int index = Main.rand.Next(3);
                        Main.soundInstanceFemaleHit[index].Stop();
                        Main.soundInstanceFemaleHit[index] = Main.soundFemaleHit[index].CreateInstance();
                        Main.soundInstanceFemaleHit[index].Volume = num3;
                        Main.soundInstanceFemaleHit[index].Pan = num2;
                        Main.soundInstanceFemaleHit[index].Play();
                    }
                    else if (type == 21)
                    {
                        int index = Main.rand.Next(3);
                        Main.soundInstanceTink[index].Stop();
                        Main.soundInstanceTink[index] = Main.soundTink[index].CreateInstance();
                        Main.soundInstanceTink[index].Volume = num3;
                        Main.soundInstanceTink[index].Pan = num2;
                        Main.soundInstanceTink[index].Play();
                    }
                }
            }
        }
    }
}
