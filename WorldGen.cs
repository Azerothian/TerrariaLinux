// Type: Terraria.WorldGen
// Assembly: TerrariaServer, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// Assembly location: C:\Temp\New Folder\TerrariaServer.exe

using Microsoft.Xna.Framework;
using System;
using System.IO;
using System.Threading;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria
{
    internal class WorldGen
    {
        public static bool spawnEye = false;
        public static bool gen = false;
        public static bool shadowOrbSmashed = false;
        public static int shadowOrbCount = 0;
        public static bool spawnMeteor = false;
        public static bool loadFailed = false;
        public static bool loadSuccess = false;
        public static bool worldCleared = false;
        public static bool worldBackup = false;
        public static bool loadBackup = false;
        private static int lastMaxTilesX = 0;
        private static int lastMaxTilesY = 0;
        public static bool saveLock = false;
        private static bool mergeUp = false;
        private static bool mergeDown = false;
        private static bool mergeLeft = false;
        private static bool mergeRight = false;
        private static int tempMoonPhase = Main.moonPhase;
        private static bool tempDayTime = Main.dayTime;
        private static bool tempBloodMoon = Main.bloodMoon;
        private static double tempTime = Main.time;
        private static bool stopDrops = false;
        public static bool noLiquidCheck = false;
        [ThreadStatic]
        public static Random genRand = new Random();
        public static string statusText = "";
        private static bool destroyObject = false;
        public static int spawnDelay = 0;
        public static int spawnNPC = 0;
        public static int maxRoomTiles = 1900;
        public static int[] roomX = new int[WorldGen.maxRoomTiles];
        public static int[] roomY = new int[WorldGen.maxRoomTiles];
        public static bool[] houseTile = new bool[80];
        public static int bestX = 0;
        public static int bestY = 0;
        public static int hiScore = 0;
        public static Vector2 lastDungeonHall = new Vector2();
        public static int maxDRooms = 100;
        public static int numDRooms = 0;
        public static int[] dRoomX = new int[WorldGen.maxDRooms];
        public static int[] dRoomY = new int[WorldGen.maxDRooms];
        public static int[] dRoomSize = new int[WorldGen.maxDRooms];
        private static bool[] dRoomTreasure = new bool[WorldGen.maxDRooms];
        private static int[] dRoomL = new int[WorldGen.maxDRooms];
        private static int[] dRoomR = new int[WorldGen.maxDRooms];
        private static int[] dRoomT = new int[WorldGen.maxDRooms];
        private static int[] dRoomB = new int[WorldGen.maxDRooms];
        private static int[] DDoorX = new int[300];
        private static int[] DDoorY = new int[300];
        private static int[] DDoorPos = new int[300];
        private static int[] DPlatX = new int[300];
        private static int[] DPlatY = new int[300];
        private static int[] JChestX = new int[100];
        private static int[] JChestY = new int[100];
        private static int numJChests = 0;
        public static int dEnteranceX = 0;
        public static bool dSurface = false;
        private static int numIslandHouses = 0;
        private static int houseCount = 0;
        private static int[] fihX = new int[300];
        private static int[] fihY = new int[300];
        private static int numMCaves = 0;
        private static int[] mCaveX = new int[300];
        private static int[] mCaveY = new int[300];
        public static int lavaLine;
        public static int waterLine;
        public static int numRoomTiles;
        public static int roomX1;
        public static int roomX2;
        public static int roomY1;
        public static int roomY2;
        public static bool canSpawn;
        public static int dungeonX;
        public static int dungeonY;
        private static int numDDoors;
        private static int numDPlats;
        private static double dxStrength1;
        private static double dyStrength1;
        private static double dxStrength2;
        private static double dyStrength2;
        private static int dMinX;
        private static int dMaxX;
        private static int dMinY;
        private static int dMaxY;

        static WorldGen()
        {
        }

        public static void SpawnNPC(int x, int y)
        {
            if (Main.wallHouse[(int)Main.tile[x, y].wall])
                WorldGen.canSpawn = true;
            if (WorldGen.canSpawn && WorldGen.StartRoomCheck(x, y) && WorldGen.RoomNeeds(WorldGen.spawnNPC))
            {
                WorldGen.ScoreRoom(-1);
                if (WorldGen.hiScore > 0)
                {
                    int index1 = -1;
                    for (int index2 = 0; index2 < 1000; ++index2)
                    {
                        if (Main.npc[index2].active && Main.npc[index2].homeless && Main.npc[index2].type == WorldGen.spawnNPC)
                        {
                            index1 = index2;
                            break;
                        }
                    }
                    if (index1 == -1)
                    {
                        int index2 = WorldGen.bestX;
                        int index3 = WorldGen.bestY;
                        bool flag = false;
                        Rectangle rectangle1;
                        Rectangle rectangle2;
                        if (!flag)
                        {
                            flag = true;
                            rectangle1 = new Rectangle(index2 * 16 + 8 - NPC.sWidth / 2 - NPC.safeRangeX, index3 * 16 + 8 - NPC.sHeight / 2 - NPC.safeRangeY, NPC.sWidth + NPC.safeRangeX * 2, NPC.sHeight + NPC.safeRangeY * 2);
                            for (int index4 = 0; index4 < (int)byte.MaxValue; ++index4)
                            {
                                if (Main.player[index4].active)
                                {
                                    rectangle2 = new Rectangle((int)Main.player[index4].position.X, (int)Main.player[index4].position.Y, Main.player[index4].width, Main.player[index4].height);
                                    if (rectangle2.Intersects(rectangle1))
                                    {
                                        flag = false;
                                        break;
                                    }
                                }
                            }
                        }
                        if (!flag)
                        {
                            for (int index4 = 1; index4 < 500; ++index4)
                            {
                                for (int index5 = 0; index5 < 2; ++index5)
                                {
                                    index2 = index5 != 0 ? WorldGen.bestX - index4 : WorldGen.bestX + index4;
                                    if (index2 > 10 && index2 < Main.maxTilesX - 10)
                                    {
                                        int num1 = WorldGen.bestY - index4;
                                        double num2 = (double)(WorldGen.bestY + index4);
                                        if (num1 < 10)
                                            num1 = 10;
                                        if (num2 > Main.worldSurface)
                                            num2 = Main.worldSurface;
                                        for (int index6 = num1; (double)index6 < num2; ++index6)
                                        {
                                            index3 = index6;
                                            if (Main.tile[index2, index3].active && Main.tileSolid[(int)Main.tile[index2, index3].type])
                                            {
                                                if (!Collision.SolidTiles(index2 - 1, index2 + 1, index3 - 3, index3 - 1))
                                                {
                                                    flag = true;
                                                    rectangle1 = new Rectangle(index2 * 16 + 8 - NPC.sWidth / 2 - NPC.safeRangeX, index3 * 16 + 8 - NPC.sHeight / 2 - NPC.safeRangeY, NPC.sWidth + NPC.safeRangeX * 2, NPC.sHeight + NPC.safeRangeY * 2);
                                                    for (int index7 = 0; index7 < (int)byte.MaxValue; ++index7)
                                                    {
                                                        if (Main.player[index7].active)
                                                        {
                                                            rectangle2 = new Rectangle((int)Main.player[index7].position.X, (int)Main.player[index7].position.Y, Main.player[index7].width, Main.player[index7].height);
                                                            if (rectangle2.Intersects(rectangle1))
                                                            {
                                                                flag = false;
                                                                break;
                                                            }
                                                        }
                                                    }
                                                    break;
                                                }
                                                else
                                                    break;
                                            }
                                        }
                                    }
                                    if (flag)
                                        break;
                                }
                                if (flag)
                                    break;
                            }
                        }
                        int index8 = NPC.NewNPC(index2 * 16, index3 * 16, WorldGen.spawnNPC, 1);
                        Main.npc[index8].homeTileX = WorldGen.bestX;
                        Main.npc[index8].homeTileY = WorldGen.bestY;
                        if (index2 < WorldGen.bestX)
                            Main.npc[index8].direction = 1;
                        else if (index2 > WorldGen.bestX)
                            Main.npc[index8].direction = -1;
                        Main.npc[index8].netUpdate = true;
                        if (Main.netMode == 0)
                            Main.NewText(Main.npc[index8].name + " has arrived!", (byte)50, (byte)125, byte.MaxValue);
                        else if (Main.netMode == 2)
                            NetMessage.SendData(25, -1, -1, Main.npc[index8].name + " has arrived!", (int)byte.MaxValue, 50f, 125f, (float)byte.MaxValue);
                    }
                    else
                    {
                        WorldGen.spawnNPC = 0;
                        Main.npc[index1].homeTileX = WorldGen.bestX;
                        Main.npc[index1].homeTileY = WorldGen.bestY;
                        Main.npc[index1].homeless = false;
                    }
                    WorldGen.spawnNPC = 0;
                }
            }
        }

        public static bool RoomNeeds(int npcType)
        {
            WorldGen.canSpawn = WorldGen.houseTile[15] && (WorldGen.houseTile[14] || WorldGen.houseTile[18]) && (WorldGen.houseTile[4] || WorldGen.houseTile[33] || (WorldGen.houseTile[34] || WorldGen.houseTile[35]) || (WorldGen.houseTile[36] || WorldGen.houseTile[42] || WorldGen.houseTile[49])) && (WorldGen.houseTile[10] || WorldGen.houseTile[11] || WorldGen.houseTile[19]) || false;
            return WorldGen.canSpawn;
        }

        public static void QuickFindHome(int npc)
        {
            if (Main.npc[npc].homeTileX > 10 && Main.npc[npc].homeTileY > 10 && Main.npc[npc].homeTileX < Main.maxTilesX - 10 && Main.npc[npc].homeTileY < Main.maxTilesY)
            {
                WorldGen.canSpawn = false;
                WorldGen.StartRoomCheck(Main.npc[npc].homeTileX, Main.npc[npc].homeTileY - 1);
                if (!WorldGen.canSpawn)
                {
                    for (int x = Main.npc[npc].homeTileX - 1; x < Main.npc[npc].homeTileX + 2; ++x)
                    {
                        int y = Main.npc[npc].homeTileY - 1;
                        while (y < Main.npc[npc].homeTileY + 2 && !WorldGen.StartRoomCheck(x, y))
                            ++y;
                    }
                }
                if (!WorldGen.canSpawn)
                {
                    int num = 10;
                    int x = Main.npc[npc].homeTileX - num;
                    while (x <= Main.npc[npc].homeTileX + num)
                    {
                        int y = Main.npc[npc].homeTileY - num;
                        while (y <= Main.npc[npc].homeTileY + num && !WorldGen.StartRoomCheck(x, y))
                            y += 2;
                        x += 2;
                    }
                }
                if (WorldGen.canSpawn)
                {
                    WorldGen.RoomNeeds(Main.npc[npc].type);
                    if (WorldGen.canSpawn)
                        WorldGen.ScoreRoom(npc);
                    if (WorldGen.canSpawn && WorldGen.hiScore > 0)
                    {
                        Main.npc[npc].homeTileX = WorldGen.bestX;
                        Main.npc[npc].homeTileY = WorldGen.bestY;
                        Main.npc[npc].homeless = false;
                        WorldGen.canSpawn = false;
                    }
                    else
                        Main.npc[npc].homeless = true;
                }
                else
                    Main.npc[npc].homeless = true;
            }
        }

        public static void ScoreRoom(int ignoreNPC = -1)
        {
            for (int index1 = 0; index1 < 1000; ++index1)
            {
                if (Main.npc[index1].active && Main.npc[index1].townNPC && ignoreNPC != index1 && !Main.npc[index1].homeless)
                {
                    for (int index2 = 0; index2 < WorldGen.numRoomTiles; ++index2)
                    {
                        if (Main.npc[index1].homeTileX == WorldGen.roomX[index2] && Main.npc[index1].homeTileY == WorldGen.roomY[index2])
                        {
                            bool flag = false;
                            for (int index3 = 0; index3 < WorldGen.numRoomTiles; ++index3)
                            {
                                if (Main.npc[index1].homeTileX == WorldGen.roomX[index3] && Main.npc[index1].homeTileY - 1 == WorldGen.roomY[index3])
                                {
                                    flag = true;
                                    break;
                                }
                            }
                            if (flag)
                            {
                                WorldGen.hiScore = -1;
                                return;
                            }
                        }
                    }
                }
            }
            WorldGen.hiScore = 0;
            int num1 = 0;
            int num2 = WorldGen.roomX1 - NPC.sWidth / 2 / 16 - 1 - 21;
            int num3 = WorldGen.roomX2 + NPC.sWidth / 2 / 16 + 1 + 21;
            int num4 = WorldGen.roomY1 - NPC.sHeight / 2 / 16 - 1 - 21;
            int num5 = WorldGen.roomY2 + NPC.sHeight / 2 / 16 + 1 + 21;
            if (num2 < 0)
                num2 = 0;
            if (num3 >= Main.maxTilesX)
                num3 = Main.maxTilesX - 1;
            if (num4 < 0)
                num4 = 0;
            if (num5 > Main.maxTilesX)
                num5 = Main.maxTilesX;
            for (int index1 = num2 + 1; index1 < num3; ++index1)
            {
                for (int index2 = num4 + 2; index2 < num5 + 2; ++index2)
                {
                    if (Main.tile[index1, index2].active)
                    {
                        if ((int)Main.tile[index1, index2].type == 23 || (int)Main.tile[index1, index2].type == 24 || (int)Main.tile[index1, index2].type == 25 || (int)Main.tile[index1, index2].type == 32)
                            ++Main.evilTiles;
                        else if ((int)Main.tile[index1, index2].type == 27)
                            Main.evilTiles -= 5;
                    }
                }
            }
            if (num1 < 50)
                num1 = 0;
            int num6 = -num1;
            if (num6 <= -250)
            {
                WorldGen.hiScore = num6;
            }
            else
            {
                int num7 = WorldGen.roomX1;
                int num8 = WorldGen.roomX2;
                int num9 = WorldGen.roomY1;
                int num10 = WorldGen.roomY2;
                for (int index1 = num7 + 1; index1 < num8; ++index1)
                {
                    for (int index2 = num9 + 2; index2 < num10 + 2; ++index2)
                    {
                        if (Main.tile[index1, index2].active)
                        {
                            int num11 = num6;
                            if (Main.tileSolid[(int)Main.tile[index1, index2].type] && !Main.tileSolidTop[(int)Main.tile[index1, index2].type] && !Collision.SolidTiles(index1 - 1, index1 + 1, index2 - 3, index2 - 1) && (Main.tile[index1 - 1, index2].active && Main.tileSolid[(int)Main.tile[index1 - 1, index2].type] && Main.tile[index1 + 1, index2].active && Main.tileSolid[(int)Main.tile[index1 + 1, index2].type]))
                            {
                                for (int index3 = index1 - 2; index3 < index1 + 3; ++index3)
                                {
                                    for (int index4 = index2 - 4; index4 < index2; ++index4)
                                    {
                                        if (Main.tile[index3, index4].active)
                                        {
                                            if (index3 == index1)
                                                num11 -= 15;
                                            else if ((int)Main.tile[index3, index4].type == 10 || (int)Main.tile[index3, index4].type == 11)
                                                num11 -= 20;
                                            else if (Main.tileSolid[(int)Main.tile[index3, index4].type])
                                                num11 -= 5;
                                            else
                                                num11 += 5;
                                        }
                                    }
                                }
                                if (num11 > WorldGen.hiScore)
                                {
                                    bool flag = false;
                                    for (int index3 = 0; index3 < WorldGen.numRoomTiles; ++index3)
                                    {
                                        if (WorldGen.roomX[index3] == index1 && WorldGen.roomY[index3] == index2)
                                        {
                                            flag = true;
                                            break;
                                        }
                                    }
                                    if (flag)
                                    {
                                        WorldGen.hiScore = num11;
                                        WorldGen.bestX = index1;
                                        WorldGen.bestY = index2;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public static bool StartRoomCheck(int x, int y)
        {
            WorldGen.roomX1 = x;
            WorldGen.roomX2 = x;
            WorldGen.roomY1 = y;
            WorldGen.roomY2 = y;
            WorldGen.numRoomTiles = 0;
            for (int index = 0; index < 80; ++index)
                WorldGen.houseTile[index] = false;
            WorldGen.canSpawn = true;
            if (Main.tile[x, y].active && Main.tileSolid[(int)Main.tile[x, y].type])
                WorldGen.canSpawn = false;
            WorldGen.CheckRoom(x, y);
            if (WorldGen.numRoomTiles < 60)
                WorldGen.canSpawn = false;
            if (WorldGen.canSpawn)
                return true;
            else
                return false;
        }

        public static void CheckRoom(int x, int y)
        {
            if (WorldGen.canSpawn)
            {
                if (x < 10 || y < 10 || x >= Main.maxTilesX - 10 || y >= WorldGen.lastMaxTilesY - 10)
                {
                    WorldGen.canSpawn = false;
                }
                else
                {
                    for (int index = 0; index < WorldGen.numRoomTiles; ++index)
                    {
                        if (WorldGen.roomX[index] == x && WorldGen.roomY[index] == y)
                            return;
                    }
                    WorldGen.roomX[WorldGen.numRoomTiles] = x;
                    WorldGen.roomY[WorldGen.numRoomTiles] = y;
                    ++WorldGen.numRoomTiles;
                    if (WorldGen.numRoomTiles >= WorldGen.maxRoomTiles)
                    {
                        WorldGen.canSpawn = false;
                    }
                    else
                    {
                        if (Main.tile[x, y].active)
                        {
                            WorldGen.houseTile[(int)Main.tile[x, y].type] = true;
                            if (Main.tileSolid[(int)Main.tile[x, y].type] || (int)Main.tile[x, y].type == 11)
                                return;
                        }
                        if (x < WorldGen.roomX1)
                            WorldGen.roomX1 = x;
                        if (x > WorldGen.roomX2)
                            WorldGen.roomX2 = x;
                        if (y < WorldGen.roomY1)
                            WorldGen.roomY1 = y;
                        if (y > WorldGen.roomY2)
                            WorldGen.roomY2 = y;
                        bool flag1 = false;
                        bool flag2 = false;
                        for (int index = -2; index < 3; ++index)
                        {
                            if (Main.wallHouse[(int)Main.tile[x + index, y].wall])
                                flag1 = true;
                            if (Main.tile[x + index, y].active && (Main.tileSolid[(int)Main.tile[x + index, y].type] || (int)Main.tile[x + index, y].type == 11))
                                flag1 = true;
                            if (Main.wallHouse[(int)Main.tile[x, y + index].wall])
                                flag2 = true;
                            if (Main.tile[x, y + index].active && (Main.tileSolid[(int)Main.tile[x, y + index].type] || (int)Main.tile[x, y + index].type == 11))
                                flag2 = true;
                        }
                        if (!flag1 || !flag2)
                        {
                            WorldGen.canSpawn = false;
                        }
                        else
                        {
                            for (int x1 = x - 1; x1 < x + 2; ++x1)
                            {
                                for (int y1 = y - 1; y1 < y + 2; ++y1)
                                {
                                    if ((x1 != x || y1 != y) && WorldGen.canSpawn)
                                        WorldGen.CheckRoom(x1, y1);
                                }
                            }
                        }
                    }
                }
            }
        }

        public static void dropMeteor()
        {
            bool flag = true;
            int num1 = 0;
            if (Main.netMode != 1)
            {
                for (int index = 0; index < (int)byte.MaxValue; ++index)
                {
                    if (Main.player[index].active)
                    {
                        flag = false;
                        break;
                    }
                }
                int num2 = 0;
                int num3 = (int)(400.0 * (double)(Main.maxTilesX / 4200));
                for (int index1 = 5; index1 < Main.maxTilesX - 5; ++index1)
                {
                    for (int index2 = 5; (double)index2 < Main.worldSurface; ++index2)
                    {
                        if (Main.tile[index1, index2].active && (int)Main.tile[index1, index2].type == 37)
                        {
                            ++num2;
                            if (num2 > num3)
                                return;
                        }
                    }
                }
                while (!flag)
                {
                    float num4 = (float)Main.maxTilesX * 0.08f;
                    int i = Main.rand.Next(50, Main.maxTilesX - 50);
                    while ((double)i > (double)Main.spawnTileX - (double)num4 && (double)i < (double)Main.spawnTileX + (double)num4)
                        i = Main.rand.Next(50, Main.maxTilesX - 50);
                    for (int j = Main.rand.Next(100); j < Main.maxTilesY; ++j)
                    {
                        if (Main.tile[i, j].active && Main.tileSolid[(int)Main.tile[i, j].type])
                        {
                            flag = WorldGen.meteor(i, j);
                            break;
                        }
                    }
                    ++num1;
                    if (num1 >= 100)
                        break;
                }
            }
        }

        public static bool meteor(int i, int j)
        {
            if (i < 50 || i > Main.maxTilesX - 50 || (j < 50 || j > Main.maxTilesY - 50))
            {
                return false;
            }
            else
            {
                int num1 = 25;
                Rectangle rectangle1 = new Rectangle((i - num1) * 16, (j - num1) * 16, num1 * 2 * 16, num1 * 2 * 16);
                for (int index = 0; index < (int)byte.MaxValue; ++index)
                {
                    if (Main.player[index].active)
                    {
                        Rectangle rectangle2 = new Rectangle((int)((double)Main.player[index].position.X + (double)(Main.player[index].width / 2) - (double)(NPC.sWidth / 2) - (double)NPC.safeRangeX), (int)((double)Main.player[index].position.Y + (double)(Main.player[index].height / 2) - (double)(NPC.sHeight / 2) - (double)NPC.safeRangeY), NPC.sWidth + NPC.safeRangeX * 2, NPC.sHeight + NPC.safeRangeY * 2);
                        if (rectangle1.Intersects(rectangle2))
                            return false;
                    }
                }
                for (int index = 0; index < 1000; ++index)
                {
                    if (Main.npc[index].active)
                    {
                        Rectangle rectangle2 = new Rectangle((int)Main.npc[index].position.X, (int)Main.npc[index].position.Y, Main.npc[index].width, Main.npc[index].height);
                        if (rectangle1.Intersects(rectangle2))
                            return false;
                    }
                }
                for (int index1 = i - num1; index1 < i + num1; ++index1)
                {
                    for (int index2 = j - num1; index2 < j + num1; ++index2)
                    {
                        if (Main.tile[index1, index2].active && (int)Main.tile[index1, index2].type == 21)
                            return false;
                    }
                }
                WorldGen.stopDrops = true;
                int num2 = 15;
                for (int index1 = i - num2; index1 < i + num2; ++index1)
                {
                    for (int index2 = j - num2; index2 < j + num2; ++index2)
                    {
                        if (index2 > j + Main.rand.Next(-2, 3) - 5 && (double)(Math.Abs(i - index1) + Math.Abs(j - index2)) < (double)num2 * 1.5 + (double)Main.rand.Next(-5, 5))
                        {
                            if (!Main.tileSolid[(int)Main.tile[index1, index2].type])
                                Main.tile[index1, index2].active = false;
                            Main.tile[index1, index2].type = (byte)37;
                        }
                    }
                }
                int num3 = 10;
                for (int index1 = i - num3; index1 < i + num3; ++index1)
                {
                    for (int index2 = j - num3; index2 < j + num3; ++index2)
                    {
                        if (index2 > j + Main.rand.Next(-2, 3) - 5 && Math.Abs(i - index1) + Math.Abs(j - index2) < num3 + Main.rand.Next(-3, 4))
                            Main.tile[index1, index2].active = false;
                    }
                }
                int num4 = 16;
                for (int i1 = i - num4; i1 < i + num4; ++i1)
                {
                    for (int j1 = j - num4; j1 < j + num4; ++j1)
                    {
                        if ((int)Main.tile[i1, j1].type == 5 || (int)Main.tile[i1, j1].type == 32)
                            WorldGen.KillTile(i1, j1, false, false, false);
                        WorldGen.SquareTileFrame(i1, j1, true);
                        WorldGen.SquareWallFrame(i1, j1, true);
                    }
                }
                int num5 = 23;
                for (int i1 = i - num5; i1 < i + num5; ++i1)
                {
                    for (int j1 = j - num5; j1 < j + num5; ++j1)
                    {
                        if (Main.tile[i1, j1].active && Main.rand.Next(10) == 0 && (double)(Math.Abs(i - i1) + Math.Abs(j - j1)) < (double)num5 * 1.3)
                        {
                            if ((int)Main.tile[i1, j1].type == 5 || (int)Main.tile[i1, j1].type == 32)
                                WorldGen.KillTile(i1, j1, false, false, false);
                            Main.tile[i1, j1].type = (byte)37;
                            WorldGen.SquareTileFrame(i1, j1, true);
                        }
                    }
                }
                WorldGen.stopDrops = false;
                if (Main.netMode == 0)
                    Main.NewText("A meteorite has landed!", (byte)50, byte.MaxValue, (byte)130);
                else if (Main.netMode == 2)
                    NetMessage.SendData(25, -1, -1, "A meteorite has landed!", (int)byte.MaxValue, 50f, (float)byte.MaxValue, 130f);
                if (Main.netMode != 1)
                    NetMessage.SendTileSquare(-1, i, j, 30);
                return true;
            }
        }

        public static void setWorldSize()
        {
            Main.bottomWorld = (float)(Main.maxTilesY * 16);
            Main.rightWorld = (float)(Main.maxTilesX * 16);
            Main.maxSectionsX = Main.maxTilesX / 200;
            Main.maxSectionsY = Main.maxTilesY / 150;
        }

        public static void worldGenCallBack(object threadContext)
        {
            Main.PlaySound(10, -1, -1, 1);
            WorldGen.clearWorld();
            WorldGen.generateWorld(-1);
            WorldGen.saveWorld(true);
            Main.LoadWorlds();
            if (Main.menuMode == 10)
                Main.menuMode = 6;
            Main.PlaySound(10, -1, -1, 1);
        }

        public static void CreateNewWorld()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(WorldGen.worldGenCallBack), (object)1);
        }

        public static void SaveAndQuitCallBack(object threadContext)
        {
            Main.menuMode = 10;
            Main.gameMenu = true;
            Player.SavePlayer(Main.player[Main.myPlayer], Main.playerPathName);
            if (Main.netMode == 0)
            {
                WorldGen.saveWorld(false);
                Main.PlaySound(10, -1, -1, 1);
            }
            else
            {
                Netplay.disconnect = true;
                Main.netMode = 0;
            }
            Main.menuMode = 0;
        }

        public static void SaveAndQuit()
        {
            Main.PlaySound(11, -1, -1, 1);
            ThreadPool.QueueUserWorkItem(new WaitCallback(WorldGen.SaveAndQuitCallBack), (object)1);
        }

        public static void playWorldCallBack(object threadContext)
        {
            if (Main.rand == null)
                Main.rand = new Random((int)DateTime.Now.Ticks);
            for (int index = 0; index < (int)byte.MaxValue; ++index)
            {
                if (index != Main.myPlayer)
                    Main.player[index].active = false;
            }
            WorldGen.loadWorld();
            if (WorldGen.loadFailed || !WorldGen.loadSuccess)
            {
                WorldGen.loadWorld();
                if (WorldGen.loadFailed || !WorldGen.loadSuccess)
                {
                    WorldGen.worldBackup = File.Exists(Main.worldPathName + ".bak") || false;
                    Console.WriteLine("Running Backup..");
                    if (!Main.dedServ)
                    {
                        if (WorldGen.worldBackup)
                        {
                            Main.menuMode = 200;
                            return;
                        }
                        else
                        {
                            Main.menuMode = 201;
                            return;
                        }
                    }
                    else if (WorldGen.worldBackup)
                    {
                        Console.WriteLine("Running Backup..");
                        File.Copy(Main.worldPathName + ".bak", Main.worldPathName, true);
                        File.Delete(Main.worldPathName + ".bak");
                        WorldGen.loadWorld();
                        if (WorldGen.loadFailed || !WorldGen.loadSuccess)
                        {
                            WorldGen.loadWorld();
                            if (WorldGen.loadFailed || !WorldGen.loadSuccess)
                            {
                                Console.WriteLine("Load failed!");
                                return;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Load failed!  No backup found.");
                        return;
                    }
                }
            }
            WorldGen.EveryTileFrame();
            if (Main.gameMenu)
                Main.gameMenu = false;
            Main.player[Main.myPlayer].Spawn();
            Main.player[Main.myPlayer].UpdatePlayer(Main.myPlayer);
            Main.dayTime = WorldGen.tempDayTime;
            Main.time = WorldGen.tempTime;
            Main.moonPhase = WorldGen.tempMoonPhase;
            Main.bloodMoon = WorldGen.tempBloodMoon;
            Main.PlaySound(11, -1, -1, 1);
            Main.resetClouds = true;
        }

        public static void playWorld()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(WorldGen.playWorldCallBack), (object)1);
        }

        public static void saveAndPlayCallBack(object threadContext)
        {
            WorldGen.saveWorld(false);
        }

        public static void saveAndPlay()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(WorldGen.saveAndPlayCallBack), (object)1);
        }

        public static void saveToonWhilePlayingCallBack(object threadContext)
        {
            Player.SavePlayer(Main.player[Main.myPlayer], Main.playerPathName);
        }

        public static void saveToonWhilePlaying()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(WorldGen.saveToonWhilePlayingCallBack), (object)1);
        }

        public static void serverLoadWorldCallBack(object threadContext)
        {
            WorldGen.loadWorld();
            if (WorldGen.loadFailed || !WorldGen.loadSuccess)
            {
                WorldGen.loadWorld();
                if (WorldGen.loadFailed || !WorldGen.loadSuccess)
                {
                    WorldGen.worldBackup = File.Exists(Main.worldPathName + ".bak") || false;
                    if (!Main.dedServ)
                    {
                        if (WorldGen.worldBackup)
                        {
                            Main.menuMode = 200;
                            return;
                        }
                        else
                        {
                            Main.menuMode = 201;
                            return;
                        }
                    }
                    else if (WorldGen.worldBackup)
                    {
                        File.Copy(Main.worldPathName + ".bak", Main.worldPathName, true);
                        File.Delete(Main.worldPathName + ".bak");
                        WorldGen.loadWorld();
                        if (WorldGen.loadFailed || !WorldGen.loadSuccess)
                        {
                            WorldGen.loadWorld();
                            if (WorldGen.loadFailed || !WorldGen.loadSuccess)
                            {
                                Console.WriteLine("Load failed!");
                                return;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Load failed!  No backup found.");
                        return;
                    }
                }
            }
            Main.PlaySound(10, -1, -1, 1);
            Netplay.StartServer();
            Main.dayTime = WorldGen.tempDayTime;
            Main.time = WorldGen.tempTime;
            Main.moonPhase = WorldGen.tempMoonPhase;
            Main.bloodMoon = WorldGen.tempBloodMoon;
        }

        public static void serverLoadWorld()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(WorldGen.serverLoadWorldCallBack), (object)1);
        }

        public static void clearWorld()
        {
            WorldGen.spawnEye = false;
            WorldGen.spawnNPC = 0;
            WorldGen.shadowOrbCount = 0;
            Main.helpText = 0;
            Main.dungeonX = 0;
            Main.dungeonY = 0;
            NPC.downedBoss1 = false;
            NPC.downedBoss2 = false;
            NPC.downedBoss3 = false;
            WorldGen.shadowOrbSmashed = false;
            WorldGen.spawnMeteor = false;
            WorldGen.stopDrops = false;
            Main.invasionDelay = 0;
            Main.invasionType = 0;
            Main.invasionSize = 0;
            Main.invasionWarn = 0;
            Main.invasionX = 0.0;
            WorldGen.noLiquidCheck = false;
            Liquid.numLiquid = 0;
            LiquidBuffer.numLiquidBuffer = 0;
            if (Main.netMode == 1 || WorldGen.lastMaxTilesX > Main.maxTilesX || WorldGen.lastMaxTilesY > Main.maxTilesY)
            {
                for (int index1 = 0; index1 < WorldGen.lastMaxTilesX; ++index1)
                {
                    Main.statusText = "Freeing unused resources: " + (object)(int)((double)((float)index1 / (float)WorldGen.lastMaxTilesX) * 100.0 + 1.0) + "%";
                    for (int index2 = 0; index2 < WorldGen.lastMaxTilesY; ++index2)
                        Main.tile[index1, index2] = (Tile)null;
                }
            }
            WorldGen.lastMaxTilesX = Main.maxTilesX;
            WorldGen.lastMaxTilesY = Main.maxTilesY;
            if (Main.netMode != 1)
            {
                for (int index1 = 0; index1 < Main.maxTilesX; ++index1)
                {
                    Main.statusText = "Resetting game objects: " + (object)(int)((double)((float)index1 / (float)Main.maxTilesX) * 100.0 + 1.0) + "%";
                    for (int index2 = 0; index2 < Main.maxTilesY; ++index2)
                        Main.tile[index1, index2] = new Tile();
                }
            }
            for (int index = 0; index < 2000; ++index)
                Main.dust[index] = new Dust();
            for (int index = 0; index < 200; ++index)
                Main.gore[index] = new Gore();
            for (int index = 0; index < 200; ++index)
                Main.item[index] = new Item();
            for (int index = 0; index < 1000; ++index)
                Main.npc[index] = new NPC();
            for (int index = 0; index < 1000; ++index)
                Main.projectile[index] = new Projectile();
            for (int index = 0; index < 1000; ++index)
                Main.chest[index] = (Chest)null;
            for (int index = 0; index < 1000; ++index)
                Main.sign[index] = (Sign)null;
            for (int index = 0; index < Liquid.resLiquid; ++index)
                Main.liquid[index] = new Liquid();
            for (int index = 0; index < 10000; ++index)
                Main.liquidBuffer[index] = new LiquidBuffer();
            WorldGen.setWorldSize();
            WorldGen.worldCleared = true;
        }

        public static void saveWorld(bool resetTime = false)
        {
            if (Main.worldName == "")
                Main.worldName = "World";
            if (!WorldGen.saveLock)
            {
                WorldGen.saveLock = true;
                try
                {
                    Directory.CreateDirectory(Main.WorldPath);
                }
                catch
                {
                }
                if (!Main.skipMenu)
                {
                    bool flag = Main.dayTime;
                    WorldGen.tempTime = Main.time;
                    WorldGen.tempMoonPhase = Main.moonPhase;
                    WorldGen.tempBloodMoon = Main.bloodMoon;
                    if (resetTime)
                    {
                        flag = true;
                        WorldGen.tempTime = 13500.0;
                        WorldGen.tempMoonPhase = 0;
                        WorldGen.tempBloodMoon = false;
                    }
                    if (Main.worldPathName != null)
                    {
                        string str = Main.worldPathName + ".sav";
                        using (FileStream fileStream = new FileStream(str, FileMode.Create))
                        {
                            using (BinaryWriter binaryWriter = new BinaryWriter((Stream)fileStream))
                            {
                                binaryWriter.Write(Main.curRelease);
                                binaryWriter.Write(Main.worldName);
                                binaryWriter.Write(Main.worldID);
                                binaryWriter.Write((int)Main.leftWorld);
                                binaryWriter.Write((int)Main.rightWorld);
                                binaryWriter.Write((int)Main.topWorld);
                                binaryWriter.Write((int)Main.bottomWorld);
                                binaryWriter.Write(Main.maxTilesY);
                                binaryWriter.Write(Main.maxTilesX);
                                binaryWriter.Write(Main.spawnTileX);
                                binaryWriter.Write(Main.spawnTileY);
                                binaryWriter.Write(Main.worldSurface);
                                binaryWriter.Write(Main.rockLayer);
                                binaryWriter.Write(WorldGen.tempTime);
                                binaryWriter.Write(flag);
                                binaryWriter.Write(WorldGen.tempMoonPhase);
                                binaryWriter.Write(WorldGen.tempBloodMoon);
                                binaryWriter.Write(Main.dungeonX);
                                binaryWriter.Write(Main.dungeonY);
                                binaryWriter.Write(NPC.downedBoss1);
                                binaryWriter.Write(NPC.downedBoss2);
                                binaryWriter.Write(NPC.downedBoss3);
                                binaryWriter.Write(WorldGen.shadowOrbSmashed);
                                binaryWriter.Write(WorldGen.spawnMeteor);
                                binaryWriter.Write((byte)WorldGen.shadowOrbCount);
                                binaryWriter.Write(Main.invasionDelay);
                                binaryWriter.Write(Main.invasionSize);
                                binaryWriter.Write(Main.invasionType);
                                binaryWriter.Write(Main.invasionX);
                                for (int index1 = 0; index1 < Main.maxTilesX; ++index1)
                                {
                                    Main.statusText = "Saving world data: " + (object)(int)((double)((float)index1 / (float)Main.maxTilesX) * 100.0 + 1.0) + "%";
                                    for (int index2 = 0; index2 < Main.maxTilesY; ++index2)
                                    {
                                        lock (Main.tile[index1, index2])
                                        {
                                            binaryWriter.Write(Main.tile[index1, index2].active);
                                            if (Main.tile[index1, index2].active)
                                            {
                                                binaryWriter.Write(Main.tile[index1, index2].type);
                                                if (Main.tileFrameImportant[(int)Main.tile[index1, index2].type])
                                                {
                                                    binaryWriter.Write(Main.tile[index1, index2].frameX);
                                                    binaryWriter.Write(Main.tile[index1, index2].frameY);
                                                }
                                            }
                                            binaryWriter.Write(Main.tile[index1, index2].lighted);
                                            if ((int)Main.tile[index1, index2].wall > 0)
                                            {
                                                binaryWriter.Write(true);
                                                binaryWriter.Write(Main.tile[index1, index2].wall);
                                            }
                                            else
                                                binaryWriter.Write(false);
                                            if ((int)Main.tile[index1, index2].liquid > 0)
                                            {
                                                binaryWriter.Write(true);
                                                binaryWriter.Write(Main.tile[index1, index2].liquid);
                                                binaryWriter.Write(Main.tile[index1, index2].lava);
                                            }
                                            else
                                                binaryWriter.Write(false);
                                        }
                                    }
                                }
                                for (int index = 0; index < 1000; ++index)
                                {
                                    if (Main.chest[index] == null)
                                    {
                                        binaryWriter.Write(false);
                                    }
                                    else
                                    {
                                        lock (Main.chest[index])
                                        {
                                            binaryWriter.Write(true);
                                            binaryWriter.Write(Main.chest[index].x);
                                            binaryWriter.Write(Main.chest[index].y);
                                            for (int local_6_1 = 0; local_6_1 < Chest.maxItems; ++local_6_1)
                                            {
                                                binaryWriter.Write((byte)Main.chest[index].item[local_6_1].stack);
                                                if (Main.chest[index].item[local_6_1].stack > 0)
                                                    binaryWriter.Write(Main.chest[index].item[local_6_1].name);
                                            }
                                        }
                                    }
                                }
                                for (int index = 0; index < 1000; ++index)
                                {
                                    if (Main.sign[index] == null || Main.sign[index].text == null)
                                    {
                                        binaryWriter.Write(false);
                                    }
                                    else
                                    {
                                        lock (Main.sign[index])
                                        {
                                            binaryWriter.Write(true);
                                            binaryWriter.Write(Main.sign[index].text);
                                            binaryWriter.Write(Main.sign[index].x);
                                            binaryWriter.Write(Main.sign[index].y);
                                        }
                                    }
                                }
                                for (int index = 0; index < 1000; ++index)
                                {
                                    lock (Main.npc[index])
                                    {
                                        if (Main.npc[index].active && Main.npc[index].townNPC)
                                        {
                                            binaryWriter.Write(true);
                                            binaryWriter.Write(Main.npc[index].name);
                                            binaryWriter.Write(Main.npc[index].position.X);
                                            binaryWriter.Write(Main.npc[index].position.Y);
                                            binaryWriter.Write(Main.npc[index].homeless);
                                            binaryWriter.Write(Main.npc[index].homeTileX);
                                            binaryWriter.Write(Main.npc[index].homeTileY);
                                        }
                                    }
                                }
                                binaryWriter.Write(false);
                                binaryWriter.Write(true);
                                binaryWriter.Write(Main.worldName);
                                binaryWriter.Write(Main.worldID);
                                binaryWriter.Close();
                                fileStream.Close();
                                if (File.Exists(Main.worldPathName))
                                {
                                    Main.statusText = "Backing up world file...";
                                    string destFileName = Main.worldPathName + ".bak";
                                    File.Copy(Main.worldPathName, destFileName, true);
                                }
                                File.Copy(str, Main.worldPathName, true);
                                File.Delete(str);
                            }
                        }
                        WorldGen.saveLock = false;
                    }
                }
            }
        }

        public static void loadWorld()
        {
            if (!File.Exists(Main.worldPathName) && Main.autoGen)
            {
                for (int index = Main.worldPathName.Length - 1; index >= 0; --index)
                {
                    
                    if (Main.worldPathName.Substring(index, 1) == "/")
                    {
                        Directory.CreateDirectory(Main.worldPathName.Substring(0, index));
                        break;
                    }
                }
                Console.WriteLine("Clear World");
                WorldGen.clearWorld();
                WorldGen.generateWorld(-1);
                WorldGen.saveWorld(false);
            }
            if (WorldGen.genRand == null)
                WorldGen.genRand = new Random((int)DateTime.Now.Ticks);
            Console.WriteLine("World Path Name:  {0}", Main.worldPathName);
            using (FileStream fileStream = new FileStream(Main.worldPathName, FileMode.Open))
            {
                using (BinaryReader binaryReader = new BinaryReader((Stream)fileStream))
                {
                    try
                    {
                        WorldGen.loadFailed = false;
                        WorldGen.loadSuccess = false;
                        int release = binaryReader.ReadInt32();
                        if (release > Main.curRelease)
                        {
                            WorldGen.loadFailed = true;
                            WorldGen.loadSuccess = false;
                            try
                            {
                                binaryReader.Close();
                                fileStream.Close();
                            }
                            catch
                            {
                            }
                        }
                        else
                        {
                            Main.worldName = binaryReader.ReadString();
                            Main.worldID = binaryReader.ReadInt32();
                            Main.leftWorld = (float)binaryReader.ReadInt32();
                            Main.rightWorld = (float)binaryReader.ReadInt32();
                            Main.topWorld = (float)binaryReader.ReadInt32();
                            Main.bottomWorld = (float)binaryReader.ReadInt32();
                            Main.maxTilesY = binaryReader.ReadInt32();
                            Main.maxTilesX = binaryReader.ReadInt32();
                            WorldGen.clearWorld();
                            Main.spawnTileX = binaryReader.ReadInt32();
                            Main.spawnTileY = binaryReader.ReadInt32();
                            Main.worldSurface = binaryReader.ReadDouble();
                            Main.rockLayer = binaryReader.ReadDouble();
                            WorldGen.tempTime = binaryReader.ReadDouble();
                            WorldGen.tempDayTime = binaryReader.ReadBoolean();
                            WorldGen.tempMoonPhase = binaryReader.ReadInt32();
                            WorldGen.tempBloodMoon = binaryReader.ReadBoolean();
                            Main.dungeonX = binaryReader.ReadInt32();
                            Main.dungeonY = binaryReader.ReadInt32();
                            NPC.downedBoss1 = binaryReader.ReadBoolean();
                            NPC.downedBoss2 = binaryReader.ReadBoolean();
                            NPC.downedBoss3 = binaryReader.ReadBoolean();
                            WorldGen.shadowOrbSmashed = binaryReader.ReadBoolean();
                            WorldGen.spawnMeteor = binaryReader.ReadBoolean();
                            WorldGen.shadowOrbCount = (int)binaryReader.ReadByte();
                            Main.invasionDelay = binaryReader.ReadInt32();
                            Main.invasionSize = binaryReader.ReadInt32();
                            Main.invasionType = binaryReader.ReadInt32();
                            Main.invasionX = binaryReader.ReadDouble();
                            for (int index1 = 0; index1 < Main.maxTilesX; ++index1)
                            {
                                Main.statusText = "Loading world data: " + (object)(int)((double)((float)index1 / (float)Main.maxTilesX) * 100.0 + 1.0) + "%";
                                for (int index2 = 0; index2 < Main.maxTilesY; ++index2)
                                {
                                    Main.tile[index1, index2].active = binaryReader.ReadBoolean();
                                    if (Main.tile[index1, index2].active)
                                    {
                                        Main.tile[index1, index2].type = binaryReader.ReadByte();
                                        if (Main.tileFrameImportant[(int)Main.tile[index1, index2].type])
                                        {
                                            Main.tile[index1, index2].frameX = binaryReader.ReadInt16();
                                            Main.tile[index1, index2].frameY = binaryReader.ReadInt16();
                                        }
                                        else
                                        {
                                            Main.tile[index1, index2].frameX = (short)-1;
                                            Main.tile[index1, index2].frameY = (short)-1;
                                        }
                                    }
                                    Main.tile[index1, index2].lighted = binaryReader.ReadBoolean();
                                    if (binaryReader.ReadBoolean())
                                        Main.tile[index1, index2].wall = binaryReader.ReadByte();
                                    if (binaryReader.ReadBoolean())
                                    {
                                        Main.tile[index1, index2].liquid = binaryReader.ReadByte();
                                        Main.tile[index1, index2].lava = binaryReader.ReadBoolean();
                                    }
                                }
                            }
                            for (int index1 = 0; index1 < 1000; ++index1)
                            {
                                if (binaryReader.ReadBoolean())
                                {
                                    Main.chest[index1] = new Chest();
                                    Main.chest[index1].x = binaryReader.ReadInt32();
                                    Main.chest[index1].y = binaryReader.ReadInt32();
                                    for (int index2 = 0; index2 < Chest.maxItems; ++index2)
                                    {
                                        Main.chest[index1].item[index2] = new Item();
                                        byte num = binaryReader.ReadByte();
                                        if ((int)num > 0)
                                        {
                                            string ItemName = Item.VersionName(binaryReader.ReadString(), release);
                                            Main.chest[index1].item[index2].SetDefaults(ItemName);
                                            Main.chest[index1].item[index2].stack = (int)num;
                                        }
                                    }
                                }
                            }
                            for (int index1 = 0; index1 < 1000; ++index1)
                            {
                                if (binaryReader.ReadBoolean())
                                {
                                    string str = binaryReader.ReadString();
                                    int index2 = binaryReader.ReadInt32();
                                    int index3 = binaryReader.ReadInt32();
                                    if (Main.tile[index2, index3].active && (int)Main.tile[index2, index3].type == 55)
                                    {
                                        Main.sign[index1] = new Sign();
                                        Main.sign[index1].x = index2;
                                        Main.sign[index1].y = index3;
                                        Main.sign[index1].text = str;
                                    }
                                }
                            }
                            bool flag1 = binaryReader.ReadBoolean();
                            int index = 0;
                            while (flag1)
                            {
                                Main.npc[index].SetDefaults(binaryReader.ReadString());
                                Main.npc[index].position.X = binaryReader.ReadSingle();
                                Main.npc[index].position.Y = binaryReader.ReadSingle();
                                Main.npc[index].homeless = binaryReader.ReadBoolean();
                                Main.npc[index].homeTileX = binaryReader.ReadInt32();
                                Main.npc[index].homeTileY = binaryReader.ReadInt32();
                                flag1 = binaryReader.ReadBoolean();
                                ++index;
                            }
                            if (release >= 7)
                            {
                                bool flag2 = binaryReader.ReadBoolean();
                                string str = binaryReader.ReadString();
                                int num = binaryReader.ReadInt32();
                                if (flag2 && str == Main.worldName && num == Main.worldID)
                                {
                                    WorldGen.loadSuccess = true;
                                }
                                else
                                {
                                    Console.WriteLine("load failed: flag2: {0} str: {1} num: {2}", flag2, str, num);
                                    WorldGen.loadSuccess = false;
                                    WorldGen.loadFailed = true;
                                    binaryReader.Close();
                                    fileStream.Close();
                                    return;
                                }
                            }
                            else
                                WorldGen.loadSuccess = true;
                            binaryReader.Close();
                            fileStream.Close();
                            if (!WorldGen.loadFailed && WorldGen.loadSuccess)
                            {
                                WorldGen.gen = true;
                                WorldGen.waterLine = Main.maxTilesY;
                                Liquid.QuickWater(2, -1, -1);
                                WorldGen.WaterCheck();
                                int num1 = 0;
                                Liquid.quickSettle = true;
                                int num2 = Liquid.numLiquid + LiquidBuffer.numLiquidBuffer;
                                float num3 = 0.0f;
                                while (Liquid.numLiquid > 0 && num1 < 100000)
                                {
                                    ++num1;
                                    float num4 = (float)(num2 - (Liquid.numLiquid + LiquidBuffer.numLiquidBuffer)) / (float)num2;
                                    if (Liquid.numLiquid + LiquidBuffer.numLiquidBuffer > num2)
                                        num2 = Liquid.numLiquid + LiquidBuffer.numLiquidBuffer;
                                    if ((double)num4 > (double)num3)
                                        num3 = num4;
                                    else
                                        num4 = num3;
                                    Main.statusText = "Settling liquids: " + (object)(int)((double)num4 * 100.0 / 2.0 + 50.0) + "%";
                                    Liquid.UpdateLiquid();
                                }
                                Liquid.quickSettle = false;
                                WorldGen.WaterCheck();
                                WorldGen.gen = false;
                            }
                        }
                    }
                    catch
                    {
                        WorldGen.loadFailed = true;
                        WorldGen.loadSuccess = false;
                        try
                        {
                            binaryReader.Close();
                            fileStream.Close();
                        }
                        catch
                        {
                        }
                    }
                }
            }
        }

        private static void resetGen()
        {
            WorldGen.numMCaves = 0;
            WorldGen.numIslandHouses = 0;
            WorldGen.houseCount = 0;
            WorldGen.dEnteranceX = 0;
            WorldGen.numDRooms = 0;
            WorldGen.numDDoors = 0;
            WorldGen.numDPlats = 0;
            WorldGen.numJChests = 0;
        }

        public static void generateWorld(int seed = -1)
    {
      WorldGen.gen = true;
      WorldGen.resetGen();
      WorldGen.genRand = seed <= 0 ? new Random((int) DateTime.Now.Ticks) : new Random(seed);
      Main.worldID = WorldGen.genRand.Next(int.MaxValue);
      int num1 = 0;
      int num2 = 0;
      double num3 = (double) Main.maxTilesY * 0.3 * ((double) WorldGen.genRand.Next(90, 110) * 0.005);
      double num4 = (num3 + (double) Main.maxTilesY * 0.2) * ((double) WorldGen.genRand.Next(90, 110) * 0.01);
      double num5 = num3;
      double num6 = num3;
      double num7 = num4;
      double num8 = num4;
      int num9 = 0;
      int num10 = WorldGen.genRand.Next(2) != 0 ? 1 : -1;
      for (int index1 = 0; index1 < Main.maxTilesX; ++index1)
      {
        Main.statusText = "Generating world terrain: " + (object) (int) ((double) ((float) index1 / (float) Main.maxTilesX) * 100.0 + 1.0) + "%";
        if (num3 < num5)
          num5 = num3;
        if (num3 > num6)
          num6 = num3;
        if (num4 < num7)
          num7 = num4;
        if (num4 > num8)
          num8 = num4;
        if (num2 <= 0)
        {
          num1 = WorldGen.genRand.Next(0, 5);
          num2 = WorldGen.genRand.Next(5, 40);
          if (num1 == 0)
            num2 *= (int) ((double) WorldGen.genRand.Next(5, 30) * 0.2);
        }
        --num2;
        if (num1 == 0)
        {
          while (WorldGen.genRand.Next(0, 7) == 0)
            num3 += (double) WorldGen.genRand.Next(-1, 2);
        }
        else if (num1 == 1)
        {
          while (WorldGen.genRand.Next(0, 4) == 0)
            --num3;
          while (WorldGen.genRand.Next(0, 10) == 0)
            ++num3;
        }
        else if (num1 == 2)
        {
          while (WorldGen.genRand.Next(0, 4) == 0)
            ++num3;
          while (WorldGen.genRand.Next(0, 10) == 0)
            --num3;
        }
        else if (num1 == 3)
        {
          while (WorldGen.genRand.Next(0, 2) == 0)
            --num3;
          while (WorldGen.genRand.Next(0, 6) == 0)
            ++num3;
        }
        else if (num1 == 4)
        {
          while (WorldGen.genRand.Next(0, 2) == 0)
            ++num3;
          while (WorldGen.genRand.Next(0, 5) == 0)
            --num3;
        }
        if (num3 < (double) Main.maxTilesY * 0.15)
        {
          num3 = (double) Main.maxTilesY * 0.15;
          num2 = 0;
        }
        else if (num3 > (double) Main.maxTilesY * 0.3)
        {
          num3 = (double) Main.maxTilesY * 0.3;
          num2 = 0;
        }
        while (WorldGen.genRand.Next(0, 3) == 0)
          num4 += (double) WorldGen.genRand.Next(-2, 3);
        if (num4 < num3 + (double) Main.maxTilesY * 0.05)
          ++num4;
        if (num4 > num3 + (double) Main.maxTilesY * 0.35)
          --num4;
        for (int index2 = 0; (double) index2 < num3; ++index2)
        {
          Main.tile[index1, index2].active = false;
          Main.tile[index1, index2].lighted = true;
          Main.tile[index1, index2].frameX = (short) -1;
          Main.tile[index1, index2].frameY = (short) -1;
        }
        for (int index2 = (int) num3; index2 < Main.maxTilesY; ++index2)
        {
          if ((double) index2 < num4)
          {
            Main.tile[index1, index2].active = true;
            Main.tile[index1, index2].type = (byte) 0;
            Main.tile[index1, index2].frameX = (short) -1;
            Main.tile[index1, index2].frameY = (short) -1;
          }
          else
          {
            Main.tile[index1, index2].active = true;
            Main.tile[index1, index2].type = (byte) 1;
            Main.tile[index1, index2].frameX = (short) -1;
            Main.tile[index1, index2].frameY = (short) -1;
          }
        }
      }
      Main.worldSurface = num6 + 5.0;
      Main.rockLayer = num8;
      double num11 = (double) ((int) ((Main.rockLayer - Main.worldSurface) / 6.0) * 6);
      Main.rockLayer = Main.worldSurface + num11;
      WorldGen.waterLine = (int) (Main.rockLayer + (double) Main.maxTilesY) / 2;
      WorldGen.waterLine += WorldGen.genRand.Next(-100, 20);
      WorldGen.lavaLine = WorldGen.waterLine + WorldGen.genRand.Next(50, 80);
      int num12 = 0;
      Main.statusText = "Adding sand...";
      int num13 = WorldGen.genRand.Next((int) ((double) Main.maxTilesX * 0.0007), (int) ((double) Main.maxTilesX * 0.002)) + 2;
      for (int index1 = 0; index1 < num13; ++index1)
      {
        int num14 = WorldGen.genRand.Next(Main.maxTilesX);
        while ((double) num14 > (double) Main.maxTilesX * 0.449999988079071 && (double) num14 < (double) Main.maxTilesX * 0.550000011920929)
          num14 = WorldGen.genRand.Next(Main.maxTilesX);
        int num15 = WorldGen.genRand.Next(15, 90);
        if (WorldGen.genRand.Next(3) == 0)
          num15 *= 2;
        int num16 = num14 - num15;
        int num17 = WorldGen.genRand.Next(15, 90);
        if (WorldGen.genRand.Next(3) == 0)
          num17 *= 2;
        int num18 = num14 + num17;
        if (num16 < 0)
          num16 = 0;
        if (num18 > Main.maxTilesX)
          num18 = Main.maxTilesX;
        if (index1 == 0)
        {
          num16 = 0;
          num18 = WorldGen.genRand.Next(250, 300);
        }
        else if (index1 == 2)
        {
          num16 = Main.maxTilesX - WorldGen.genRand.Next(250, 300);
          num18 = Main.maxTilesX;
        }
        int num19 = WorldGen.genRand.Next(50, 100);
        for (int index2 = num16; index2 < num18; ++index2)
        {
          if (WorldGen.genRand.Next(2) == 0)
          {
            num19 += WorldGen.genRand.Next(-1, 2);
            if (num19 < 50)
              num19 = 50;
            if (num19 > 100)
              num19 = 100;
          }
          for (int index3 = 0; (double) index3 < Main.worldSurface; ++index3)
          {
            if (Main.tile[index2, index3].active)
            {
              int num20 = num19;
              if (index2 - num16 < num20)
                num20 = index2 - num16;
              if (num18 - index2 < num20)
                num20 = num18 - index2;
              int num21 = num20 + WorldGen.genRand.Next(5);
              for (int index4 = index3; index4 < index3 + num21; ++index4)
              {
                if (index2 > num16 + WorldGen.genRand.Next(5) && index2 < num18 - WorldGen.genRand.Next(5))
                  Main.tile[index2, index4].type = (byte) 53;
              }
              break;
            }
          }
        }
      }
      for (int index = 0; index < (int) ((double) (Main.maxTilesX * Main.maxTilesY) * 8E-06); ++index)
        WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int) Main.worldSurface, (int) Main.rockLayer), (double) WorldGen.genRand.Next(15, 70), WorldGen.genRand.Next(20, 130), 53, false, 0.0f, 0.0f, false, true);
      WorldGen.numMCaves = 0;
      Main.statusText = "Generating hills...";
      for (int index1 = 0; index1 < (int) ((double) Main.maxTilesX * 0.0008); ++index1)
      {
        int num14 = 0;
        bool flag1 = false;
        bool flag2 = false;
        int i = WorldGen.genRand.Next((int) ((double) Main.maxTilesX * 0.25), (int) ((double) Main.maxTilesX * 0.75));
        while (!flag2)
        {
          flag2 = true;
          while (i > Main.maxTilesX / 2 - 100 && i < Main.maxTilesX / 2 + 100)
            i = WorldGen.genRand.Next((int) ((double) Main.maxTilesX * 0.25), (int) ((double) Main.maxTilesX * 0.75));
          for (int index2 = 0; index2 < WorldGen.numMCaves; ++index2)
          {
            if (i > WorldGen.mCaveX[index2] - 50 && i < WorldGen.mCaveX[index2] + 50)
            {
              ++num14;
              flag2 = false;
              break;
            }
          }
          if (num14 >= 200)
          {
            flag1 = true;
            break;
          }
        }
        if (!flag1)
        {
          for (int j = 0; (double) j < Main.worldSurface; ++j)
          {
            if (Main.tile[i, j].active)
            {
              WorldGen.Mountinater(i, j);
              WorldGen.mCaveX[WorldGen.numMCaves] = i;
              WorldGen.mCaveY[WorldGen.numMCaves] = j;
              ++WorldGen.numMCaves;
              break;
            }
          }
        }
      }
      for (int index1 = 1; index1 < Main.maxTilesX - 1; ++index1)
      {
        Main.statusText = "Puttin dirt behind dirt: " + (object) (int) ((double) ((float) index1 / (float) Main.maxTilesX) * 100.0 + 1.0) + "%";
        bool flag = false;
        num12 += WorldGen.genRand.Next(-1, 2);
        if (num12 < 0)
          num12 = 0;
        if (num12 > 10)
          num12 = 10;
        for (int index2 = 0; (double) index2 < Main.worldSurface + 10.0 && (double) index2 <= Main.worldSurface + (double) num12; ++index2)
        {
          if (flag)
            Main.tile[index1, index2].wall = (byte) 2;
          if (Main.tile[index1, index2].active && Main.tile[index1 - 1, index2].active && (Main.tile[index1 + 1, index2].active && Main.tile[index1, index2 + 1].active) && Main.tile[index1 - 1, index2 + 1].active && Main.tile[index1 + 1, index2 + 1].active)
            flag = true;
        }
      }
      Main.statusText = "Placing rocks in the dirt...";
      for (int index = 0; index < (int) ((double) (Main.maxTilesX * Main.maxTilesY) * 0.0002); ++index)
        WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next(0, (int) num5 + 1), (double) WorldGen.genRand.Next(4, 15), WorldGen.genRand.Next(5, 40), 1, false, 0.0f, 0.0f, false, true);
      for (int index = 0; index < (int) ((double) (Main.maxTilesX * Main.maxTilesY) * 0.0002); ++index)
        WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int) num5, (int) num6 + 1), (double) WorldGen.genRand.Next(4, 10), WorldGen.genRand.Next(5, 30), 1, false, 0.0f, 0.0f, false, true);
      for (int index = 0; index < (int) ((double) (Main.maxTilesX * Main.maxTilesY) * 0.0045); ++index)
        WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int) num6, (int) num8 + 1), (double) WorldGen.genRand.Next(2, 7), WorldGen.genRand.Next(2, 23), 1, false, 0.0f, 0.0f, false, true);
      Main.statusText = "Placing dirt in the rocks...";
      for (int index = 0; index < (int) ((double) (Main.maxTilesX * Main.maxTilesY) * 0.005); ++index)
        WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int) num7, Main.maxTilesY), (double) WorldGen.genRand.Next(2, 6), WorldGen.genRand.Next(2, 40), 0, false, 0.0f, 0.0f, false, true);
      Main.statusText = "Adding clay...";
      for (int index = 0; index < (int) ((double) (Main.maxTilesX * Main.maxTilesY) * 2E-05); ++index)
        WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next(0, (int) num5), (double) WorldGen.genRand.Next(4, 14), WorldGen.genRand.Next(10, 50), 40, false, 0.0f, 0.0f, false, true);
      for (int index = 0; index < (int) ((double) (Main.maxTilesX * Main.maxTilesY) * 5E-05); ++index)
        WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int) num5, (int) num6 + 1), (double) WorldGen.genRand.Next(8, 14), WorldGen.genRand.Next(15, 45), 40, false, 0.0f, 0.0f, false, true);
      for (int index = 0; index < (int) ((double) (Main.maxTilesX * Main.maxTilesY) * 2E-05); ++index)
        WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int) num6, (int) num8 + 1), (double) WorldGen.genRand.Next(8, 15), WorldGen.genRand.Next(5, 50), 40, false, 0.0f, 0.0f, false, true);
      for (int index1 = 5; index1 < Main.maxTilesX - 5; ++index1)
      {
        for (int index2 = 1; (double) index2 < Main.worldSurface - 1.0; ++index2)
        {
          if (Main.tile[index1, index2].active)
          {
            for (int index3 = index2; index3 < index2 + 5; ++index3)
            {
              if ((int) Main.tile[index1, index3].type == 40)
                Main.tile[index1, index3].type = (byte) 0;
            }
            break;
          }
        }
      }
      for (int index = 0; index < (int) ((double) (Main.maxTilesX * Main.maxTilesY) * 0.0015); ++index)
      {
        Main.statusText = "Making random holes: " + (object) (int) ((double) ((float) index / ((float) (Main.maxTilesX * Main.maxTilesY) * 0.0015f)) * 100.0 + 1.0) + "%";
        int type = -1;
        if (WorldGen.genRand.Next(5) == 0)
          type = -2;
        WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int) num6, Main.maxTilesY), (double) WorldGen.genRand.Next(2, 5), WorldGen.genRand.Next(2, 20), type, false, 0.0f, 0.0f, false, true);
        WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int) num6, Main.maxTilesY), (double) WorldGen.genRand.Next(8, 15), WorldGen.genRand.Next(7, 30), type, false, 0.0f, 0.0f, false, true);
      }
      for (int index = 0; index < (int) ((double) (Main.maxTilesX * Main.maxTilesY) * 3E-05); ++index)
      {
        Main.statusText = "Generating small caves: " + (object) (int) ((double) ((float) index / ((float) (Main.maxTilesX * Main.maxTilesY) * 3E-05f)) * 100.0 + 1.0) + "%";
        if (num8 <= (double) Main.maxTilesY)
        {
          int type = -1;
          if (WorldGen.genRand.Next(6) == 0)
            type = -2;
          WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int) num5, (int) num8 + 1), (double) WorldGen.genRand.Next(5, 15), WorldGen.genRand.Next(30, 200), type, false, 0.0f, 0.0f, false, true);
        }
      }
      for (int index = 0; index < (int) ((double) (Main.maxTilesX * Main.maxTilesY) * 0.00015); ++index)
      {
        Main.statusText = "Generating large caves: " + (object) (int) ((double) ((float) index / ((float) (Main.maxTilesX * Main.maxTilesY) * 0.00015f)) * 100.0 + 1.0) + "%";
        if (num8 <= (double) Main.maxTilesY)
        {
          int type = -1;
          if (WorldGen.genRand.Next(10) == 0)
            type = -2;
          WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int) num8, Main.maxTilesY), (double) WorldGen.genRand.Next(6, 20), WorldGen.genRand.Next(50, 300), type, false, 0.0f, 0.0f, false, true);
        }
      }
      Main.statusText = "Generating surface caves...";
      for (int index = 0; index < (int) ((double) Main.maxTilesX * 0.0025); ++index)
      {
        int i = WorldGen.genRand.Next(0, Main.maxTilesX);
        for (int j = 0; (double) j < num6; ++j)
        {
          if (Main.tile[i, j].active)
          {
            WorldGen.TileRunner(i, j, (double) WorldGen.genRand.Next(3, 6), WorldGen.genRand.Next(5, 50), -1, false, (float) WorldGen.genRand.Next(-10, 11) * 0.1f, 1f, false, true);
            break;
          }
        }
      }
      for (int index = 0; index < (int) ((double) Main.maxTilesX * 0.0007); ++index)
      {
        int i = WorldGen.genRand.Next(0, Main.maxTilesX);
        for (int j = 0; (double) j < num6; ++j)
        {
          if (Main.tile[i, j].active)
          {
            WorldGen.TileRunner(i, j, (double) WorldGen.genRand.Next(10, 15), WorldGen.genRand.Next(50, 130), -1, false, (float) WorldGen.genRand.Next(-10, 11) * 0.1f, 2f, false, true);
            break;
          }
        }
      }
      for (int index = 0; index < (int) ((double) Main.maxTilesX * 0.0003); ++index)
      {
        int i = WorldGen.genRand.Next(0, Main.maxTilesX);
        for (int j = 0; (double) j < num6; ++j)
        {
          if (Main.tile[i, j].active)
          {
            WorldGen.TileRunner(i, j, (double) WorldGen.genRand.Next(12, 25), WorldGen.genRand.Next(150, 500), -1, false, (float) WorldGen.genRand.Next(-10, 11) * 0.1f, 4f, false, true);
            WorldGen.TileRunner(i, j, (double) WorldGen.genRand.Next(8, 17), WorldGen.genRand.Next(60, 200), -1, false, (float) WorldGen.genRand.Next(-10, 11) * 0.1f, 2f, false, true);
            WorldGen.TileRunner(i, j, (double) WorldGen.genRand.Next(5, 13), WorldGen.genRand.Next(40, 170), -1, false, (float) WorldGen.genRand.Next(-10, 11) * 0.1f, 2f, false, true);
            break;
          }
        }
      }
      for (int index = 0; index < (int) ((double) Main.maxTilesX * 0.0004); ++index)
      {
        int i = WorldGen.genRand.Next(0, Main.maxTilesX);
        for (int j = 0; (double) j < num6; ++j)
        {
          if (Main.tile[i, j].active)
          {
            WorldGen.TileRunner(i, j, (double) WorldGen.genRand.Next(7, 12), WorldGen.genRand.Next(150, 250), -1, false, 0.0f, 1f, true, true);
            break;
          }
        }
      }
      for (int index1 = 0; index1 < (int) ((double) (Main.maxTilesX * Main.maxTilesY) * 0.002); ++index1)
      {
        int index2 = WorldGen.genRand.Next(1, Main.maxTilesX - 1);
        int index3 = WorldGen.genRand.Next((int) num5, (int) num6);
        if (index3 >= Main.maxTilesY)
          index3 = Main.maxTilesY - 2;
        if (Main.tile[index2 - 1, index3].active && (int) Main.tile[index2 - 1, index3].type == 0 && (Main.tile[index2 + 1, index3].active && (int) Main.tile[index2 + 1, index3].type == 0) && (Main.tile[index2, index3 - 1].active && (int) Main.tile[index2, index3 - 1].type == 0 && Main.tile[index2, index3 + 1].active) && (int) Main.tile[index2, index3 + 1].type == 0)
        {
          Main.tile[index2, index3].active = true;
          Main.tile[index2, index3].type = (byte) 2;
        }
        int index4 = WorldGen.genRand.Next(1, Main.maxTilesX - 1);
        int index5 = WorldGen.genRand.Next(0, (int) num5);
        if (index5 >= Main.maxTilesY)
          index5 = Main.maxTilesY - 2;
        if (Main.tile[index4 - 1, index5].active && (int) Main.tile[index4 - 1, index5].type == 0 && (Main.tile[index4 + 1, index5].active && (int) Main.tile[index4 + 1, index5].type == 0) && (Main.tile[index4, index5 - 1].active && (int) Main.tile[index4, index5 - 1].type == 0 && Main.tile[index4, index5 + 1].active) && (int) Main.tile[index4, index5 + 1].type == 0)
        {
          Main.tile[index4, index5].active = true;
          Main.tile[index4, index5].type = (byte) 2;
        }
      }
      Main.statusText = "Generating jungle: 0%";
      float num22 = (float) (Main.maxTilesX / 4200) * 1.5f;
      int num23 = 0;
      float num24 = (float) WorldGen.genRand.Next(15, 30) * 0.01f;
      int num25;
      if (num10 == -1)
      {
        float num14 = 1f - num24;
        num25 = (int) ((double) Main.maxTilesX * (double) num14);
      }
      else
        num25 = (int) ((double) Main.maxTilesX * (double) num24);
      int num26 = (int) ((double) Main.maxTilesY + Main.rockLayer) / 2;
      int i1 = num25 + WorldGen.genRand.Next((int) (-100.0 * (double) num22), (int) (101.0 * (double) num22));
      int j1 = num26 + WorldGen.genRand.Next((int) (-100.0 * (double) num22), (int) (101.0 * (double) num22));
      int num27 = i1;
      int num28 = j1;
      WorldGen.TileRunner(i1, j1, (double) WorldGen.genRand.Next((int) (250.0 * (double) num22), (int) (500.0 * (double) num22)), WorldGen.genRand.Next(50, 150), 59, false, (float) (num10 * 3), 0.0f, false, true);
      Main.statusText = "Generating jungle: 15%";
      int i2 = i1 + WorldGen.genRand.Next((int) (-250.0 * (double) num22), (int) (251.0 * (double) num22));
      int j2 = j1 + WorldGen.genRand.Next((int) (-150.0 * (double) num22), (int) (151.0 * (double) num22));
      int num29 = i2;
      int num30 = j2;
      int num31 = i2;
      int num32 = j2;
      WorldGen.TileRunner(i2, j2, (double) WorldGen.genRand.Next((int) (250.0 * (double) num22), (int) (500.0 * (double) num22)), WorldGen.genRand.Next(50, 150), 59, false, 0.0f, 0.0f, false, true);
      Main.statusText = "Generating jungle: 30%";
      int i3 = i2 + WorldGen.genRand.Next((int) (-400.0 * (double) num22), (int) (401.0 * (double) num22));
      int j3 = j2 + WorldGen.genRand.Next((int) (-150.0 * (double) num22), (int) (151.0 * (double) num22));
      int num33 = i3;
      int num34 = j3;
      WorldGen.TileRunner(i3, j3, (double) WorldGen.genRand.Next((int) (250.0 * (double) num22), (int) (500.0 * (double) num22)), WorldGen.genRand.Next(50, 150), 59, false, (float) (num10 * -3), 0.0f, false, true);
      Main.statusText = "Generating jungle: 45%";
      WorldGen.TileRunner((num27 + num29 + num33) / 3, (num28 + num30 + num34) / 3, (double) WorldGen.genRand.Next((int) (250.0 * (double) num22), (int) (500.0 * (double) num22)), 10000, 59, false, 0.0f, -20f, true, true);
      Main.statusText = "Generating jungle: 60%";
      int i4 = num31;
      int j4 = num32;
      for (int index = 0; (double) index <= 20.0 * (double) num22; ++index)
      {
        Main.statusText = "Generating jungle: " + (object) (int) (60.0 + (double) index / (double) num22) + "%";
        i4 += WorldGen.genRand.Next((int) (-5.0 * (double) num22), (int) (6.0 * (double) num22));
        j4 += WorldGen.genRand.Next((int) (-5.0 * (double) num22), (int) (6.0 * (double) num22));
        WorldGen.TileRunner(i4, j4, (double) WorldGen.genRand.Next(40, 100), WorldGen.genRand.Next(300, 500), 59, false, 0.0f, 0.0f, false, true);
      }
      for (int index1 = 0; (double) index1 <= 10.0 * (double) num22; ++index1)
      {
        Main.statusText = "Generating jungle: " + (object) (int) (80.0 + (double) index1 / (double) num22 * 2.0) + "%";
        int i5 = num31 + WorldGen.genRand.Next((int) (-600.0 * (double) num22), (int) (600.0 * (double) num22));
        int j5;
        for (j5 = num32 + WorldGen.genRand.Next((int) (-200.0 * (double) num22), (int) (200.0 * (double) num22)); i5 < 1 || i5 >= Main.maxTilesX - 1 || (j5 < 1 || j5 >= Main.maxTilesY - 1) || (int) Main.tile[i5, j5].type != 59; j5 = num32 + WorldGen.genRand.Next((int) (-200.0 * (double) num22), (int) (200.0 * (double) num22)))
          i5 = num31 + WorldGen.genRand.Next((int) (-600.0 * (double) num22), (int) (600.0 * (double) num22));
        for (int index2 = 0; (double) index2 < 8.0 * (double) num22; ++index2)
        {
          i5 += WorldGen.genRand.Next(-30, 31);
          j5 += WorldGen.genRand.Next(-30, 31);
          int type = -1;
          if (WorldGen.genRand.Next(7) == 0)
            type = -2;
          WorldGen.TileRunner(i5, j5, (double) WorldGen.genRand.Next(10, 20), WorldGen.genRand.Next(30, 70), type, false, 0.0f, 0.0f, false, true);
        }
      }
      for (int index = 0; (double) index <= 300.0 * (double) num22; ++index)
      {
        int i5 = num31 + WorldGen.genRand.Next((int) (-600.0 * (double) num22), (int) (600.0 * (double) num22));
        int j5;
        for (j5 = num32 + WorldGen.genRand.Next((int) (-200.0 * (double) num22), (int) (200.0 * (double) num22)); i5 < 1 || i5 >= Main.maxTilesX - 1 || (j5 < 1 || j5 >= Main.maxTilesY - 1) || (int) Main.tile[i5, j5].type != 59; j5 = num32 + WorldGen.genRand.Next((int) (-200.0 * (double) num22), (int) (200.0 * (double) num22)))
          i5 = num31 + WorldGen.genRand.Next((int) (-600.0 * (double) num22), (int) (600.0 * (double) num22));
        WorldGen.TileRunner(i5, j5, (double) WorldGen.genRand.Next(4, 10), WorldGen.genRand.Next(5, 30), 1, false, 0.0f, 0.0f, false, true);
        if (WorldGen.genRand.Next(4) == 0)
        {
          int type = WorldGen.genRand.Next(63, 69);
          WorldGen.TileRunner(i5 + WorldGen.genRand.Next(-1, 2), j5 + WorldGen.genRand.Next(-1, 2), (double) WorldGen.genRand.Next(3, 7), WorldGen.genRand.Next(4, 8), type, false, 0.0f, 0.0f, false, true);
        }
      }
      num23 = num31;
      float num35 = (float) WorldGen.genRand.Next(6, 10) * (float) (Main.maxTilesX / 4200);
      for (int index1 = 0; (double) index1 < (double) num35; ++index1)
      {
        bool flag1 = true;
        while (flag1)
        {
          int index2 = WorldGen.genRand.Next(20, Main.maxTilesX - 20);
          int index3 = WorldGen.genRand.Next((int) (Main.worldSurface + Main.rockLayer) / 2, Main.maxTilesY - 300);
          if ((int) Main.tile[index2, index3].type == 59)
          {
            flag1 = false;
            int num14 = WorldGen.genRand.Next(2, 4);
            int num15 = WorldGen.genRand.Next(2, 4);
            for (int index4 = index2 - num14 - 1; index4 <= index2 + num14 + 1; ++index4)
            {
              for (int index5 = index3 - num15 - 1; index5 <= index3 + num15 + 1; ++index5)
              {
                Main.tile[index4, index5].active = true;
                Main.tile[index4, index5].type = (byte) 45;
                Main.tile[index4, index5].liquid = (byte) 0;
                Main.tile[index4, index5].lava = false;
              }
            }
            for (int index4 = index2 - num14; index4 <= index2 + num14; ++index4)
            {
              for (int index5 = index3 - num15; index5 <= index3 + num15; ++index5)
              {
                Main.tile[index4, index5].active = false;
                Main.tile[index4, index5].wall = (byte) 10;
              }
            }
            bool flag2 = false;
            int num16 = 0;
            while (!flag2 && num16 < 100)
            {
              ++num16;
              int i5 = WorldGen.genRand.Next(index2 - num14, index2 + num14 + 1);
              int j5 = WorldGen.genRand.Next(index3 - num15, index3 + num15 - 2);
              WorldGen.PlaceTile(i5, j5, 4, true, false, -1);
              if ((int) Main.tile[i5, j5].type == 4)
                flag2 = true;
            }
            for (int index4 = index2 - num14 - 1; index4 <= index2 + num14 + 1; ++index4)
            {
              for (int index5 = index3 + num15 - 2; index5 <= index3 + num15; ++index5)
                Main.tile[index4, index5].active = false;
            }
            for (int index4 = index2 - num14 - 1; index4 <= index2 + num14 + 1; ++index4)
            {
              for (int index5 = index3 + num15 - 2; index5 <= index3 + num15 - 1; ++index5)
                Main.tile[index4, index5].active = false;
            }
            for (int index4 = index2 - num14 - 1; index4 <= index2 + num14 + 1; ++index4)
            {
              int num17 = 4;
              for (int index5 = index3 + num15 + 2; !Main.tile[index4, index5].active && index5 < Main.maxTilesY && num17 > 0; --num17)
              {
                Main.tile[index4, index5].active = true;
                Main.tile[index4, index5].type = (byte) 59;
                ++index5;
              }
            }
            int num18 = num14 - WorldGen.genRand.Next(1, 3);
            int index6 = index3 - num15 - 2;
            while (num18 > -1)
            {
              for (int index4 = index2 - num18 - 1; index4 <= index2 + num18 + 1; ++index4)
              {
                Main.tile[index4, index6].active = true;
                Main.tile[index4, index6].type = (byte) 45;
              }
              num18 -= WorldGen.genRand.Next(1, 3);
              --index6;
            }
            WorldGen.JChestX[WorldGen.numJChests] = index2;
            WorldGen.JChestY[WorldGen.numJChests] = index3;
            ++WorldGen.numJChests;
          }
        }
      }
      for (int i5 = 0; i5 < Main.maxTilesX; ++i5)
      {
        for (int j5 = 0; j5 < Main.maxTilesY; ++j5)
        {
          if (Main.tile[i5, j5].active)
            WorldGen.SpreadGrass(i5, j5, 59, 60, true);
        }
      }
      WorldGen.numIslandHouses = 0;
      WorldGen.houseCount = 0;
      Main.statusText = "Generating floating islands...";
      for (int index1 = 0; index1 < (int) ((double) Main.maxTilesX * 0.0008); ++index1)
      {
        int num14 = 0;
        bool flag1 = false;
        int index2 = WorldGen.genRand.Next((int) ((double) Main.maxTilesX * 0.1), (int) ((double) Main.maxTilesX * 0.9));
        bool flag2 = false;
        while (!flag2)
        {
          flag2 = true;
          while (index2 > Main.maxTilesX / 2 - 80 && index2 < Main.maxTilesX / 2 + 80)
            index2 = WorldGen.genRand.Next((int) ((double) Main.maxTilesX * 0.1), (int) ((double) Main.maxTilesX * 0.9));
          for (int index3 = 0; index3 < WorldGen.numIslandHouses; ++index3)
          {
            if (index2 > WorldGen.fihX[index3] - 80 && index2 < WorldGen.fihX[index3] + 80)
            {
              ++num14;
              flag2 = false;
              break;
            }
          }
          if (num14 >= 200)
          {
            flag1 = true;
            break;
          }
        }
        if (!flag1)
        {
          for (int index3 = 200; (double) index3 < Main.worldSurface; ++index3)
          {
            if (Main.tile[index2, index3].active)
            {
              int i5 = index2;
              int j5 = WorldGen.genRand.Next(90, index3 - 100);
              while ((double) j5 > num5 - 50.0)
                --j5;
              WorldGen.FloatingIsland(i5, j5);
              WorldGen.fihX[WorldGen.numIslandHouses] = i5;
              WorldGen.fihY[WorldGen.numIslandHouses] = j5;
              ++WorldGen.numIslandHouses;
              break;
            }
          }
        }
      }
      Main.statusText = "Adding mushroom patches...";
      for (int index = 0; index < Main.maxTilesX / 300; ++index)
        WorldGen.ShroomPatch(WorldGen.genRand.Next((int) ((double) Main.maxTilesX * 0.3), (int) ((double) Main.maxTilesX * 0.7)), WorldGen.genRand.Next((int) Main.rockLayer, Main.maxTilesY - 300));
      for (int i5 = 0; i5 < Main.maxTilesX; ++i5)
      {
        for (int j5 = (int) Main.worldSurface; j5 < Main.maxTilesY; ++j5)
        {
          if (Main.tile[i5, j5].active)
            WorldGen.SpreadGrass(i5, j5, 59, 70, false);
        }
      }
      Main.statusText = "Placing mud in the dirt...";
      for (int index = 0; index < (int) ((double) (Main.maxTilesX * Main.maxTilesY) * 0.001); ++index)
        WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int) num7, Main.maxTilesY), (double) WorldGen.genRand.Next(2, 6), WorldGen.genRand.Next(2, 40), 59, false, 0.0f, 0.0f, false, true);
      Main.statusText = "Adding shinies...";
      for (int index = 0; index < (int) ((double) (Main.maxTilesX * Main.maxTilesY) * 6E-05); ++index)
        WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int) num5, (int) num6), (double) WorldGen.genRand.Next(3, 6), WorldGen.genRand.Next(2, 6), 7, false, 0.0f, 0.0f, false, true);
      for (int index = 0; index < (int) ((double) (Main.maxTilesX * Main.maxTilesY) * 8E-05); ++index)
        WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int) num6, (int) num8), (double) WorldGen.genRand.Next(3, 7), WorldGen.genRand.Next(3, 7), 7, false, 0.0f, 0.0f, false, true);
      for (int index = 0; index < (int) ((double) (Main.maxTilesX * Main.maxTilesY) * 0.0002); ++index)
        WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int) num7, Main.maxTilesY), (double) WorldGen.genRand.Next(4, 9), WorldGen.genRand.Next(4, 8), 7, false, 0.0f, 0.0f, false, true);
      for (int index = 0; index < (int) ((double) (Main.maxTilesX * Main.maxTilesY) * 3E-05); ++index)
        WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int) num5, (int) num6), (double) WorldGen.genRand.Next(3, 7), WorldGen.genRand.Next(2, 5), 6, false, 0.0f, 0.0f, false, true);
      for (int index = 0; index < (int) ((double) (Main.maxTilesX * Main.maxTilesY) * 8E-05); ++index)
        WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int) num6, (int) num8), (double) WorldGen.genRand.Next(3, 6), WorldGen.genRand.Next(3, 6), 6, false, 0.0f, 0.0f, false, true);
      for (int index = 0; index < (int) ((double) (Main.maxTilesX * Main.maxTilesY) * 0.0002); ++index)
        WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int) num7, Main.maxTilesY), (double) WorldGen.genRand.Next(4, 9), WorldGen.genRand.Next(4, 8), 6, false, 0.0f, 0.0f, false, true);
      for (int index = 0; index < (int) ((double) (Main.maxTilesX * Main.maxTilesY) * 3E-05); ++index)
        WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int) num6, (int) num8), (double) WorldGen.genRand.Next(3, 6), WorldGen.genRand.Next(3, 6), 9, false, 0.0f, 0.0f, false, true);
      for (int index = 0; index < (int) ((double) (Main.maxTilesX * Main.maxTilesY) * 0.00017); ++index)
        WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int) num7, Main.maxTilesY), (double) WorldGen.genRand.Next(4, 9), WorldGen.genRand.Next(4, 8), 9, false, 0.0f, 0.0f, false, true);
      for (int index = 0; index < (int) ((double) (Main.maxTilesX * Main.maxTilesY) * 0.00017); ++index)
        WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next(0, (int) num5), (double) WorldGen.genRand.Next(4, 9), WorldGen.genRand.Next(4, 8), 9, false, 0.0f, 0.0f, false, true);
      for (int index = 0; index < (int) ((double) (Main.maxTilesX * Main.maxTilesY) * 0.00012); ++index)
        WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int) num7, Main.maxTilesY), (double) WorldGen.genRand.Next(4, 8), WorldGen.genRand.Next(4, 8), 8, false, 0.0f, 0.0f, false, true);
      for (int index = 0; index < (int) ((double) (Main.maxTilesX * Main.maxTilesY) * 0.00012); ++index)
        WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next(0, (int) num5 - 20), (double) WorldGen.genRand.Next(4, 8), WorldGen.genRand.Next(4, 8), 8, false, 0.0f, 0.0f, false, true);
      Main.statusText = "Adding webs...";
      for (int index1 = 0; index1 < (int) ((double) (Main.maxTilesX * Main.maxTilesY) * 0.001); ++index1)
      {
        int index2 = WorldGen.genRand.Next(20, Main.maxTilesX - 20);
        int index3 = WorldGen.genRand.Next((int) num5, Main.maxTilesY - 20);
        if (index1 < WorldGen.numMCaves)
        {
          index2 = WorldGen.mCaveX[index1];
          index3 = WorldGen.mCaveY[index1];
        }
        if (!Main.tile[index2, index3].active && ((double) index3 > Main.worldSurface || (int) Main.tile[index2, index3].wall > 0))
        {
          while (!Main.tile[index2, index3].active && index3 > (int) num5)
            --index3;
          int j5 = index3 + 1;
          int num14 = 1;
          if (WorldGen.genRand.Next(2) == 0)
            num14 = -1;
          while (!Main.tile[index2, j5].active && index2 > 10 && index2 < Main.maxTilesX - 10)
            index2 += num14;
          int i5 = index2 - num14;
          if ((double) j5 > Main.worldSurface || (int) Main.tile[i5, j5].wall > 0)
            WorldGen.TileRunner(i5, j5, (double) WorldGen.genRand.Next(4, 13), WorldGen.genRand.Next(2, 5), 51, true, (float) num14, -1f, false, false);
        }
      }
      Main.statusText = "Creating underworld: 0%";
      int num36 = Main.maxTilesY - WorldGen.genRand.Next(150, 190);
      for (int index1 = 0; index1 < Main.maxTilesX; ++index1)
      {
        num36 += WorldGen.genRand.Next(-3, 4);
        if (num36 < Main.maxTilesY - 190)
          num36 = Main.maxTilesY - 190;
        if (num36 > Main.maxTilesY - 160)
          num36 = Main.maxTilesY - 160;
        for (int index2 = num36 - 20 - WorldGen.genRand.Next(3); index2 < Main.maxTilesY; ++index2)
        {
          if (index2 >= num36)
          {
            Main.tile[index1, index2].active = false;
            Main.tile[index1, index2].lava = false;
            Main.tile[index1, index2].liquid = (byte) 0;
          }
          else
            Main.tile[index1, index2].type = (byte) 57;
        }
      }
      int num37 = Main.maxTilesY - WorldGen.genRand.Next(40, 70);
      for (int index1 = 10; index1 < Main.maxTilesX - 10; ++index1)
      {
        num37 += WorldGen.genRand.Next(-10, 11);
        if (num37 > Main.maxTilesY - 60)
          num37 = Main.maxTilesY - 60;
        if (num37 < Main.maxTilesY - 100)
          num37 = Main.maxTilesY - 120;
        for (int index2 = num37; index2 < Main.maxTilesY - 10; ++index2)
        {
          if (!Main.tile[index1, index2].active)
          {
            Main.tile[index1, index2].lava = true;
            Main.tile[index1, index2].liquid = byte.MaxValue;
          }
        }
      }
      for (int index1 = 0; index1 < Main.maxTilesX; ++index1)
      {
        if (WorldGen.genRand.Next(50) == 0)
        {
          int index2 = Main.maxTilesY - 65;
          while (!Main.tile[index1, index2].active && index2 > Main.maxTilesY - 135)
            --index2;
          WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), index2 + WorldGen.genRand.Next(20, 50), (double) WorldGen.genRand.Next(15, 20), 1000, 57, true, 0.0f, (float) WorldGen.genRand.Next(1, 3), true, true);
        }
      }
      Liquid.QuickWater(-2, -1, -1);
      for (int i5 = 0; i5 < Main.maxTilesX; ++i5)
      {
        Main.statusText = "Creating underworld: " + (object) (int) ((double) ((float) i5 / (float) (Main.maxTilesX - 1)) * 100.0 / 2.0 + 50.0) + "%";
        if (WorldGen.genRand.Next(13) == 0)
        {
          int index = Main.maxTilesY - 65;
          while (((int) Main.tile[i5, index].liquid > 0 || Main.tile[i5, index].active) && index > Main.maxTilesY - 140)
            --index;
          WorldGen.TileRunner(i5, index - WorldGen.genRand.Next(2, 5), (double) WorldGen.genRand.Next(5, 30), 1000, 57, true, 0.0f, (float) WorldGen.genRand.Next(1, 3), true, true);
          float num14 = (float) WorldGen.genRand.Next(1, 3);
          if (WorldGen.genRand.Next(3) == 0)
            num14 *= 0.5f;
          if (WorldGen.genRand.Next(2) == 0)
            WorldGen.TileRunner(i5, index - WorldGen.genRand.Next(2, 5), (double) (int) ((double) WorldGen.genRand.Next(5, 15) * (double) num14), (int) ((double) WorldGen.genRand.Next(10, 15) * (double) num14), 57, true, 1f, 0.3f, false, true);
          if (WorldGen.genRand.Next(2) == 0)
          {
            float num15 = (float) WorldGen.genRand.Next(1, 3);
            WorldGen.TileRunner(i5, index - WorldGen.genRand.Next(2, 5), (double) (int) ((double) WorldGen.genRand.Next(5, 15) * (double) num15), (int) ((double) WorldGen.genRand.Next(10, 15) * (double) num15), 57, true, -1f, 0.3f, false, true);
          }
          WorldGen.TileRunner(i5 + WorldGen.genRand.Next(-10, 10), index + WorldGen.genRand.Next(-10, 10), (double) WorldGen.genRand.Next(5, 15), WorldGen.genRand.Next(5, 10), -2, false, (float) WorldGen.genRand.Next(-1, 3), (float) WorldGen.genRand.Next(-1, 3), false, true);
          if (WorldGen.genRand.Next(3) == 0)
            WorldGen.TileRunner(i5 + WorldGen.genRand.Next(-10, 10), index + WorldGen.genRand.Next(-10, 10), (double) WorldGen.genRand.Next(10, 30), WorldGen.genRand.Next(10, 20), -2, false, (float) WorldGen.genRand.Next(-1, 3), (float) WorldGen.genRand.Next(-1, 3), false, true);
          if (WorldGen.genRand.Next(5) == 0)
            WorldGen.TileRunner(i5 + WorldGen.genRand.Next(-15, 15), index + WorldGen.genRand.Next(-15, 10), (double) WorldGen.genRand.Next(15, 30), WorldGen.genRand.Next(5, 20), -2, false, (float) WorldGen.genRand.Next(-1, 3), (float) WorldGen.genRand.Next(-1, 3), false, true);
        }
      }
      for (int index = 0; index < Main.maxTilesX; ++index)
      {
        if (!Main.tile[index, Main.maxTilesY - 145].active)
        {
          Main.tile[index, Main.maxTilesY - 145].liquid = byte.MaxValue;
          Main.tile[index, Main.maxTilesY - 145].lava = true;
        }
        if (!Main.tile[index, Main.maxTilesY - 144].active)
        {
          Main.tile[index, Main.maxTilesY - 144].liquid = byte.MaxValue;
          Main.tile[index, Main.maxTilesY - 144].lava = true;
        }
      }
      for (int index = 0; index < (int) ((double) (Main.maxTilesX * Main.maxTilesY) * 0.002); ++index)
        WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next(Main.maxTilesY - 140, Main.maxTilesY), (double) WorldGen.genRand.Next(3, 8), WorldGen.genRand.Next(3, 8), 58, false, 0.0f, 0.0f, false, true);
      WorldGen.AddHellHouses();
      int num38 = WorldGen.genRand.Next(2, (int) ((double) Main.maxTilesX * 0.005));
      for (int index = 0; index < num38; ++index)
      {
        Main.statusText = "Adding water bodies: " + (object) (int) ((double) ((float) index / (float) num38) * 100.0) + "%";
        int i5 = WorldGen.genRand.Next(300, Main.maxTilesX - 300);
        while (i5 > Main.maxTilesX / 2 - 50 && i5 < Main.maxTilesX / 2 + 50)
          i5 = WorldGen.genRand.Next(300, Main.maxTilesX - 300);
        int j5 = (int) num5 - 20;
        while (!Main.tile[i5, j5].active)
          ++j5;
        WorldGen.Lakinater(i5, j5);
      }
      num23 = 0;
      int x1;
      if (num10 == -1)
      {
        x1 = WorldGen.genRand.Next((int) ((double) Main.maxTilesX * 0.05), (int) ((double) Main.maxTilesX * 0.2));
        num9 = -1;
      }
      else
      {
        x1 = WorldGen.genRand.Next((int) ((double) Main.maxTilesX * 0.8), (int) ((double) Main.maxTilesX * 0.95));
        num9 = 1;
      }
      int y1 = (int) ((Main.rockLayer + (double) Main.maxTilesY) / 2.0) + WorldGen.genRand.Next(-200, 200);
      WorldGen.MakeDungeon(x1, y1, 41, 7);
      for (int index1 = 0; (double) index1 < (double) Main.maxTilesX * 0.00045; ++index1)
      {
        Main.statusText = "Making the world evil: " + (object) (int) ((double) ((float) index1 / ((float) Main.maxTilesX * 0.00045f)) * 100.0) + "%";
        bool flag1 = false;
        int num14 = 0;
        int num15 = 0;
        int num16 = 0;
        while (!flag1)
        {
          flag1 = true;
          int num17 = Main.maxTilesX / 2;
          int num18 = 200;
          num14 = WorldGen.genRand.Next(Main.maxTilesX);
          num15 = num14 - WorldGen.genRand.Next(150) - 175;
          num16 = num14 + WorldGen.genRand.Next(150) + 175;
          if (num15 < 0)
            num15 = 0;
          if (num16 > Main.maxTilesX)
            num16 = Main.maxTilesX;
          if (num14 > num17 - num18 && num14 < num17 + num18)
            flag1 = false;
          if (num15 > num17 - num18 && num15 < num17 + num18)
            flag1 = false;
          if (num16 > num17 - num18 && num16 < num17 + num18)
            flag1 = false;
          for (int index2 = num15; index2 < num16; ++index2)
          {
            int index3 = 0;
            while (index3 < (int) Main.worldSurface)
            {
              if (Main.tile[index2, index3].active && Main.tileDungeon[(int) Main.tile[index2, index3].type])
              {
                flag1 = false;
                break;
              }
              else if (flag1)
                index3 += 5;
              else
                break;
            }
          }
        }
        int num19 = 0;
        for (int i5 = num15; i5 < num16; ++i5)
        {
          if (num19 > 0)
            --num19;
          if (i5 == num14 || num19 == 0)
          {
            for (int j5 = (int) num5; (double) j5 < Main.worldSurface - 1.0; ++j5)
            {
              if (Main.tile[i5, j5].active || (int) Main.tile[i5, j5].wall > 0)
              {
                if (i5 == num14)
                {
                  num19 = 20;
                  WorldGen.ChasmRunner(i5, j5, WorldGen.genRand.Next(150) + 150, true);
                  break;
                }
                else if (WorldGen.genRand.Next(35) == 0 && num19 == 0)
                {
                  num19 = 30;
                  bool makeOrb = true;
                  WorldGen.ChasmRunner(i5, j5, WorldGen.genRand.Next(50) + 50, makeOrb);
                  break;
                }
                else
                  break;
              }
            }
          }
          for (int index2 = (int) num5; (double) index2 < Main.worldSurface - 1.0; ++index2)
          {
            if (Main.tile[i5, index2].active)
            {
              int num17 = index2 + WorldGen.genRand.Next(10, 14);
              for (int index3 = index2; index3 < num17; ++index3)
              {
                if (((int) Main.tile[i5, index3].type == 59 || (int) Main.tile[i5, index3].type == 60) && (i5 >= num15 + WorldGen.genRand.Next(5) && i5 < num16 - WorldGen.genRand.Next(5)))
                  Main.tile[i5, index3].type = (byte) 0;
              }
              break;
            }
          }
        }
        double num20 = Main.worldSurface + 40.0;
        for (int index2 = num15; index2 < num16; ++index2)
        {
          num20 += (double) WorldGen.genRand.Next(-2, 3);
          if (num20 < Main.worldSurface + 30.0)
            num20 = Main.worldSurface + 30.0;
          if (num20 > Main.worldSurface + 50.0)
            num20 = Main.worldSurface + 50.0;
          int i5 = index2;
          bool flag2 = false;
          for (int j5 = (int) num5; (double) j5 < num20; ++j5)
          {
            if (Main.tile[i5, j5].active)
            {
              if ((int) Main.tile[i5, j5].type == 0 && (double) j5 < Main.worldSurface - 1.0 && !flag2)
                WorldGen.SpreadGrass(i5, j5, 0, 23, true);
              flag2 = true;
              if ((int) Main.tile[i5, j5].type == 1 && (i5 >= num15 + WorldGen.genRand.Next(5) && i5 <= num16 - WorldGen.genRand.Next(5)))
                Main.tile[i5, j5].type = (byte) 25;
              if ((int) Main.tile[i5, j5].type == 2)
                Main.tile[i5, j5].type = (byte) 23;
            }
          }
        }
        for (int index2 = num15; index2 < num16; ++index2)
        {
          for (int index3 = 0; index3 < Main.maxTilesY - 50; ++index3)
          {
            if (Main.tile[index2, index3].active && (int) Main.tile[index2, index3].type == 31)
            {
              int num17 = index2 - 13;
              int num18 = index2 + 13;
              int num21 = index3 - 13;
              int num39 = index3 + 13;
              for (int index4 = num17; index4 < num18; ++index4)
              {
                if (index4 > 10 && index4 < Main.maxTilesX - 10)
                {
                  for (int index5 = num21; index5 < num39; ++index5)
                  {
                    if (Math.Abs(index4 - index2) + Math.Abs(index5 - index3) < 9 + WorldGen.genRand.Next(11) && WorldGen.genRand.Next(3) != 0 && (int) Main.tile[index4, index5].type != 31)
                    {
                      Main.tile[index4, index5].active = true;
                      Main.tile[index4, index5].type = (byte) 25;
                      if (Math.Abs(index4 - index2) <= 1 && Math.Abs(index5 - index3) <= 1)
                        Main.tile[index4, index5].active = false;
                    }
                    if ((int) Main.tile[index4, index5].type != 31 && (Math.Abs(index4 - index2) <= 2 + WorldGen.genRand.Next(3) && Math.Abs(index5 - index3) <= 2 + WorldGen.genRand.Next(3)))
                      Main.tile[index4, index5].active = false;
                  }
                }
              }
            }
          }
        }
      }
      Main.statusText = "Generating mountain caves...";
      for (int index = 0; index < WorldGen.numMCaves; ++index)
      {
        int i5 = WorldGen.mCaveX[index];
        int j5 = WorldGen.mCaveY[index];
        WorldGen.CaveOpenater(i5, j5);
        WorldGen.Cavinator(i5, j5, WorldGen.genRand.Next(40, 50));
      }
      Main.statusText = "Creating beaches...";
      for (int index1 = 0; index1 < 2; ++index1)
      {
        if (index1 == 0)
        {
          int num14 = 0;
          int num15 = WorldGen.genRand.Next(125, 200);
          float num16 = 1f;
          int index2 = 0;
          while (!Main.tile[num15 - 1, index2].active)
            ++index2;
          for (int index3 = num15 - 1; index3 >= num14; --index3)
          {
            num16 += (float) WorldGen.genRand.Next(10, 20) * 0.05f;
            for (int index4 = 0; (double) index4 < (double) index2 + (double) num16; ++index4)
            {
              if ((double) index4 < (double) index2 + (double) num16 * 0.75 - 3.0)
              {
                Main.tile[index3, index4].active = false;
                if (index4 > index2)
                  Main.tile[index3, index4].liquid = byte.MaxValue;
                else if (index4 == index2)
                  Main.tile[index3, index4].liquid = (byte) 127;
              }
              else if (index4 > index2)
              {
                Main.tile[index3, index4].type = (byte) 53;
                Main.tile[index3, index4].active = true;
              }
              Main.tile[index3, index4].wall = (byte) 0;
            }
          }
        }
        else
        {
          int index2 = Main.maxTilesX - WorldGen.genRand.Next(125, 200);
          int num14 = Main.maxTilesX;
          float num15 = 1f;
          int index3 = 0;
          while (!Main.tile[index2, index3].active)
            ++index3;
          for (int index4 = index2; index4 < num14; ++index4)
          {
            num15 += (float) WorldGen.genRand.Next(10, 20) * 0.05f;
            for (int index5 = 0; (double) index5 < (double) index3 + (double) num15; ++index5)
            {
              if ((double) index5 < (double) index3 + (double) num15 * 0.75 - 3.0)
              {
                Main.tile[index4, index5].active = false;
                if (index5 > index3)
                  Main.tile[index4, index5].liquid = byte.MaxValue;
                else if (index5 == index3)
                  Main.tile[index4, index5].liquid = (byte) 127;
              }
              else if (index5 > index3)
              {
                Main.tile[index4, index5].type = (byte) 53;
                Main.tile[index4, index5].active = true;
              }
              Main.tile[index4, index5].wall = (byte) 0;
            }
          }
        }
      }
      Main.statusText = "Adding gems...";
      for (int type = 63; type <= 68; ++type)
      {
        float num14 = 0.0f;
        if (type == 67)
          num14 = (float) Main.maxTilesX * 0.5f;
        else if (type == 66)
          num14 = (float) Main.maxTilesX * 0.45f;
        else if (type == 63)
          num14 = (float) Main.maxTilesX * 0.3f;
        else if (type == 65)
          num14 = (float) Main.maxTilesX * 0.25f;
        else if (type == 64)
          num14 = (float) Main.maxTilesX * 0.1f;
        else if (type == 68)
          num14 = (float) Main.maxTilesX * 0.05f;
        float num15 = num14 * 0.2f;
        for (int index = 0; (double) index < (double) num15; ++index)
        {
          int i5 = WorldGen.genRand.Next(0, Main.maxTilesX);
          int j5;
          for (j5 = WorldGen.genRand.Next((int) Main.worldSurface, Main.maxTilesY); (int) Main.tile[i5, j5].type != 1; j5 = WorldGen.genRand.Next((int) Main.worldSurface, Main.maxTilesY))
            i5 = WorldGen.genRand.Next(0, Main.maxTilesX);
          WorldGen.TileRunner(i5, j5, (double) WorldGen.genRand.Next(2, 6), WorldGen.genRand.Next(3, 7), type, false, 0.0f, 0.0f, false, true);
        }
      }
      for (int index1 = 0; index1 < Main.maxTilesX; ++index1)
      {
        Main.statusText = "Gravitating sand: " + (object) (int) ((double) ((float) index1 / (float) (Main.maxTilesX - 1)) * 100.0) + "%";
        for (int index2 = Main.maxTilesY - 5; index2 > 0; --index2)
        {
          if (Main.tile[index1, index2].active && (int) Main.tile[index1, index2].type == 53)
          {
            for (int index3 = index2; !Main.tile[index1, index3 + 1].active && index3 < Main.maxTilesY - 5; ++index3)
            {
              Main.tile[index1, index3 + 1].active = true;
              Main.tile[index1, index3 + 1].type = (byte) 53;
            }
          }
        }
      }
      for (int index1 = 3; index1 < Main.maxTilesX - 3; ++index1)
      {
        Main.statusText = "Cleaning up dirt backgrounds: " + (object) (int) ((double) ((float) index1 / (float) Main.maxTilesX) * 100.0 + 1.0) + "%";
        for (int index2 = 0; (double) index2 < Main.worldSurface; ++index2)
        {
          if ((int) Main.tile[index1, index2].wall == 2)
            Main.tile[index1, index2].wall = (byte) 0;
          if ((int) Main.tile[index1, index2].type != 53)
          {
            if ((int) Main.tile[index1 - 1, index2].wall == 2)
              Main.tile[index1 - 1, index2].wall = (byte) 0;
            if ((int) Main.tile[index1 - 2, index2].wall == 2 && WorldGen.genRand.Next(2) == 0)
              Main.tile[index1 - 2, index2].wall = (byte) 0;
            if ((int) Main.tile[index1 - 3, index2].wall == 2 && WorldGen.genRand.Next(2) == 0)
              Main.tile[index1 - 3, index2].wall = (byte) 0;
            if ((int) Main.tile[index1 + 1, index2].wall == 2)
              Main.tile[index1 + 1, index2].wall = (byte) 0;
            if ((int) Main.tile[index1 + 2, index2].wall == 2 && WorldGen.genRand.Next(2) == 0)
              Main.tile[index1 + 2, index2].wall = (byte) 0;
            if ((int) Main.tile[index1 + 3, index2].wall == 2 && WorldGen.genRand.Next(2) == 0)
              Main.tile[index1 + 3, index2].wall = (byte) 0;
            if (Main.tile[index1, index2].active)
              break;
          }
        }
      }
      for (int index = 0; index < (int) ((double) (Main.maxTilesX * Main.maxTilesY) * 2E-05); ++index)
      {
        Main.statusText = "Placing altars: " + (object) (int) ((double) ((float) index / ((float) (Main.maxTilesX * Main.maxTilesY) * 2E-05f)) * 100.0 + 1.0) + "%";
        bool flag = false;
        int num14 = 0;
        while (!flag)
        {
          int x2 = WorldGen.genRand.Next(1, Main.maxTilesX);
          int y2 = (int) (num6 + 20.0);
          WorldGen.Place3x2(x2, y2, 26);
          if ((int) Main.tile[x2, y2].type == 26)
          {
            flag = true;
          }
          else
          {
            ++num14;
            if (num14 >= 10000)
              flag = true;
          }
        }
      }
      for (int index1 = 0; index1 < Main.maxTilesX; ++index1)
      {
        int index2 = index1;
        for (int index3 = (int) num5; (double) index3 < Main.worldSurface - 1.0; ++index3)
        {
          if (Main.tile[index2, index3].active)
          {
            if ((int) Main.tile[index2, index3].type == 60)
            {
              Main.tile[index2, index3 - 1].liquid = byte.MaxValue;
              Main.tile[index2, index3 - 2].liquid = byte.MaxValue;
              break;
            }
            else
              break;
          }
        }
      }
      Liquid.QuickWater(3, -1, -1);
      WorldGen.WaterCheck();
      int num40 = 0;
      Liquid.quickSettle = true;
      int num41;
      while (num40 < 10)
      {
        int num14 = Liquid.numLiquid + LiquidBuffer.numLiquidBuffer;
        ++num40;
        float num15 = 0.0f;
        while (Liquid.numLiquid > 0)
        {
          float num16 = (float) (num14 - (Liquid.numLiquid + LiquidBuffer.numLiquidBuffer)) / (float) num14;
          if (Liquid.numLiquid + LiquidBuffer.numLiquidBuffer > num14)
            num14 = Liquid.numLiquid + LiquidBuffer.numLiquidBuffer;
          if ((double) num16 > (double) num15)
            num15 = num16;
          else
            num16 = num15;
          if (num40 == 1)
            Main.statusText = "Settling liquids: " + (object) (int) ((double) num16 * 100.0 / 3.0 + 33.0) + "%";
          int num17 = 10;
          if (num40 > num17)
            num41 = num40;
          Liquid.UpdateLiquid();
        }
        WorldGen.WaterCheck();
        Main.statusText = "Settling liquids: " + (object) (int) ((double) num40 * 10.0 / 3.0 + 66.0) + "%";
      }
      Liquid.quickSettle = false;
      for (int index = 0; index < (int) ((double) (Main.maxTilesX * Main.maxTilesY) * 2.5E-05); ++index)
      {
        Main.statusText = "Placing life crystals: " + (object) (int) ((double) ((float) index / ((float) (Main.maxTilesX * Main.maxTilesY) * 2.5E-05f)) * 100.0 + 1.0) + "%";
        bool flag = false;
        int num14 = 0;
        while (!flag)
        {
          if (WorldGen.AddLifeCrystal(WorldGen.genRand.Next(1, Main.maxTilesX), WorldGen.genRand.Next((int) (num6 + 20.0), Main.maxTilesY)))
          {
            flag = true;
          }
          else
          {
            ++num14;
            if (num14 >= 10000)
              flag = true;
          }
        }
      }
      for (int index = 0; index < (int) ((double) (Main.maxTilesX * Main.maxTilesY) * 1.8E-05); ++index)
      {
        Main.statusText = "Hiding treasure: " + (object) (int) ((double) ((float) index / ((float) (Main.maxTilesX * Main.maxTilesY) * 1.8E-05f)) * 100.0 + 1.0) + "%";
        bool flag = false;
        int num14 = 0;
        while (!flag)
        {
          if (WorldGen.AddBuriedChest(WorldGen.genRand.Next(1, Main.maxTilesX), WorldGen.genRand.Next((int) (num6 + 20.0), Main.maxTilesY), 0))
          {
            flag = true;
          }
          else
          {
            ++num14;
            if (num14 >= 10000)
              flag = true;
          }
        }
      }
      int num42 = 0;
      for (int index = 0; index < WorldGen.numJChests; ++index)
      {
        ++num42;
        int contain = 211;
        if (num42 == 1)
          contain = 211;
        else if (num42 == 2)
          contain = 212;
        else if (num42 == 3)
          contain = 213;
        if (num42 > 3)
          num42 = 0;
        if (!WorldGen.AddBuriedChest(WorldGen.JChestX[index] + WorldGen.genRand.Next(2), WorldGen.JChestY[index], contain))
        {
          for (int i5 = WorldGen.JChestX[index]; i5 <= WorldGen.JChestX[index] + 1; ++i5)
          {
            for (int j5 = WorldGen.JChestY[index]; j5 <= WorldGen.JChestY[index] + 1; ++j5)
              WorldGen.KillTile(i5, j5, false, false, false);
          }
          WorldGen.AddBuriedChest(WorldGen.JChestX[index], WorldGen.JChestY[index], contain);
        }
      }
      float num43 = (float) (Main.maxTilesX / 4200);
      int num44 = 0;
      for (int index = 0; (double) index < 10.0 * (double) num43; ++index)
      {
        ++num44;
        int contain;
        if (num44 == 1)
        {
          contain = 186;
        }
        else
        {
          contain = 187;
          num44 = 0;
        }

          //TODO: (dotPeek)Decomplier f\&*ed it
        //for (bool flag = false; !flag;) {
        //  int i5;
        //  int j5;
        //  flag = WorldGen.AddBuriedChest(i5, j5, contain);
        //}
        //)
        //{
        //  i5 = WorldGen.genRand.Next(1, Main.maxTilesX);
        //  for (j5 = WorldGen.genRand.Next(1, Main.maxTilesY - 200); (int) Main.tile[i5, j5].liquid < 200 || Main.tile[i5, j5].lava; j5 = WorldGen.genRand.Next(1, Main.maxTilesY - 200))
        //    i5 = WorldGen.genRand.Next(1, Main.maxTilesX);
        //}
      }
      for (int index = 0; index < WorldGen.numIslandHouses; ++index)
        WorldGen.IslandHouse(WorldGen.fihX[index], WorldGen.fihY[index]);
      for (int index = 0; index < (int) ((double) (Main.maxTilesX * Main.maxTilesY) * 0.0008); ++index)
      {
        float num14 = (float) index / ((float) (Main.maxTilesX * Main.maxTilesY) * 0.0008f);
        Main.statusText = "Placing breakables: " + (object) (int) ((double) num14 * 100.0 + 1.0) + "%";
        bool flag1 = false;
        int num15 = 0;
        while (!flag1)
        {
          int num16 = WorldGen.genRand.Next((int) num6, Main.maxTilesY - 10);
          if ((double) num14 > 0.93)
            num16 = Main.maxTilesY - 150;
          else if ((double) num14 > 0.75)
            num16 = (int) num5;
          int x2 = WorldGen.genRand.Next(1, Main.maxTilesX);
          bool flag2 = false;
          for (int y2 = num16; y2 < Main.maxTilesY; ++y2)
          {
            if (!flag2)
            {
              if (Main.tile[x2, y2].active && Main.tileSolid[(int) Main.tile[x2, y2].type] && !Main.tile[x2, y2 - 1].lava)
                flag2 = true;
            }
            else if (WorldGen.PlacePot(x2, y2, 28))
            {
              flag1 = true;
              break;
            }
            else
            {
              ++num15;
              if (num15 >= 10000)
              {
                flag1 = true;
                break;
              }
            }
          }
        }
      }
      for (int index1 = 0; index1 < (int) ((double) (Main.maxTilesX * Main.maxTilesY) * 1E-05); ++index1)
      {
        Main.statusText = "Placing hellforges: " + (object) (int) ((double) ((float) index1 / ((float) (Main.maxTilesX * Main.maxTilesY) * 1E-05f)) * 100.0 + 1.0) + "%";
        bool flag = false;
        int num14 = 0;
        while (!flag)
        {
          int i5 = WorldGen.genRand.Next(1, Main.maxTilesX);
          int index2 = WorldGen.genRand.Next(Main.maxTilesY - 250, Main.maxTilesY - 5);
          try
          {
            if ((int) Main.tile[i5, index2].wall == 13)
            {
              while (!Main.tile[i5, index2].active)
                ++index2;
              int j5 = index2 - 1;
              WorldGen.PlaceTile(i5, j5, 77, false, false, -1);
              if ((int) Main.tile[i5, j5].type == 77)
              {
                flag = true;
              }
              else
              {
                ++num14;
                if (num14 >= 10000)
                  flag = true;
              }
            }
          }
          catch
          {
          }
        }
      }
      Main.statusText = "Spreading grass...";
      for (int index = 0; index < Main.maxTilesX; ++index)
      {
        int i5 = index;
        bool flag = true;
        for (int j5 = 0; (double) j5 < Main.worldSurface - 1.0; ++j5)
        {
          if (Main.tile[i5, j5].active)
          {
            if (flag && (int) Main.tile[i5, j5].type == 0)
              WorldGen.SpreadGrass(i5, j5, 0, 2, true);
            if ((double) j5 <= num6)
              flag = false;
            else
              break;
          }
          else if ((int) Main.tile[i5, j5].wall == 0)
            flag = true;
        }
      }
      int num45 = 5;
      bool flag3 = true;
      while (flag3)
      {
        int index1 = Main.maxTilesX / 2 + WorldGen.genRand.Next(-num45, num45 + 1);
        for (int index2 = 0; index2 < Main.maxTilesY; ++index2)
        {
          if (Main.tile[index1, index2].active)
          {
            Main.spawnTileX = index1;
            Main.spawnTileY = index2;
            Main.tile[index1, index2 - 1].lighted = true;
            break;
          }
        }
        flag3 = false;
        ++num45;
        if ((double) Main.spawnTileY > Main.worldSurface)
          flag3 = true;
        if ((int) Main.tile[Main.spawnTileX, Main.spawnTileY - 1].liquid > 0)
          flag3 = true;
      }
      int num46 = 10;
      while ((double) Main.spawnTileY > Main.worldSurface)
      {
        int index1 = WorldGen.genRand.Next(Main.maxTilesX / 2 - num46, Main.maxTilesX / 2 + num46);
        for (int index2 = 0; index2 < Main.maxTilesY; ++index2)
        {
          if (Main.tile[index1, index2].active)
          {
            Main.spawnTileX = index1;
            Main.spawnTileY = index2;
            Main.tile[index1, index2 - 1].lighted = true;
            break;
          }
        }
        ++num46;
      }
      int index7 = NPC.NewNPC(Main.spawnTileX * 16, Main.spawnTileY * 16, 22, 0);
      Main.npc[index7].homeTileX = Main.spawnTileX;
      Main.npc[index7].homeTileY = Main.spawnTileY;
      Main.npc[index7].direction = 1;
      Main.npc[index7].homeless = true;
      Main.statusText = "Planting sunflowers...";
      for (int index1 = 0; (double) index1 < (double) Main.maxTilesX * 0.002; ++index1)
      {
        num41 = 0;
        int num14 = Main.maxTilesX / 2;
        int num15 = WorldGen.genRand.Next(Main.maxTilesX);
        int num16 = num15 - WorldGen.genRand.Next(10) - 7;
        int num17 = num15 + WorldGen.genRand.Next(10) + 7;
        if (num16 < 0)
          num16 = 0;
        if (num17 > Main.maxTilesX - 1)
          num17 = Main.maxTilesX - 1;
        for (int i5 = num16; i5 < num17; ++i5)
        {
          for (int index2 = 1; (double) index2 < Main.worldSurface - 1.0; ++index2)
          {
            if ((int) Main.tile[i5, index2].type == 2 && Main.tile[i5, index2].active && !Main.tile[i5, index2 - 1].active)
              WorldGen.PlaceTile(i5, index2 - 1, 27, true, false, -1);
            if (Main.tile[i5, index2].active)
              break;
          }
        }
      }
      Main.statusText = "Planting trees...";
      for (int index1 = 0; (double) index1 < (double) Main.maxTilesX * 0.003; ++index1)
      {
        int num14 = WorldGen.genRand.Next(50, Main.maxTilesX - 50);
        int num15 = WorldGen.genRand.Next(25, 50);
        for (int i5 = num14 - num15; i5 < num14 + num15; ++i5)
        {
          for (int y2 = 20; (double) y2 < Main.worldSurface; ++y2)
            WorldGen.GrowEpicTree(i5, y2);
        }
      }
      WorldGen.AddTrees();
      Main.statusText = "Planting weeds...";
      WorldGen.AddPlants();
      for (int i5 = 0; i5 < Main.maxTilesX; ++i5)
      {
        for (int y2 = 0; y2 < Main.maxTilesY; ++y2)
        {
          if (Main.tile[i5, y2].active)
          {
            if (y2 >= (int) Main.worldSurface && (int) Main.tile[i5, y2].type == 70 && !Main.tile[i5, y2 - 1].active)
            {
              WorldGen.GrowShroom(i5, y2);
              if (!Main.tile[i5, y2 - 1].active)
                WorldGen.PlaceTile(i5, y2 - 1, 71, true, false, -1);
            }
            if ((int) Main.tile[i5, y2].type == 60 && !Main.tile[i5, y2 - 1].active)
              WorldGen.PlaceTile(i5, y2 - 1, 61, true, false, -1);
          }
        }
      }
      Main.statusText = "Growing vines...";
      for (int index1 = 0; index1 < Main.maxTilesX; ++index1)
      {
        int num14 = 0;
        for (int index2 = 0; (double) index2 < Main.worldSurface; ++index2)
        {
          if (num14 > 0 && !Main.tile[index1, index2].active)
          {
            Main.tile[index1, index2].active = true;
            Main.tile[index1, index2].type = (byte) 52;
            --num14;
          }
          else
            num14 = 0;
          if (Main.tile[index1, index2].active && (int) Main.tile[index1, index2].type == 2 && WorldGen.genRand.Next(5) < 3)
            num14 = WorldGen.genRand.Next(1, 10);
        }
        int num15 = 0;
        for (int index2 = 0; index2 < Main.maxTilesY; ++index2)
        {
          if (num15 > 0 && !Main.tile[index1, index2].active)
          {
            Main.tile[index1, index2].active = true;
            Main.tile[index1, index2].type = (byte) 62;
            --num15;
          }
          else
            num15 = 0;
          if (Main.tile[index1, index2].active && (int) Main.tile[index1, index2].type == 60 && WorldGen.genRand.Next(5) < 3)
            num15 = WorldGen.genRand.Next(1, 10);
        }
      }
      Main.statusText = "Planting flowers...";
      for (int index1 = 0; (double) index1 < (double) Main.maxTilesX * 0.005; ++index1)
      {
        int index2 = WorldGen.genRand.Next(20, Main.maxTilesX - 20);
        int num14 = WorldGen.genRand.Next(5, 15);
        int num15 = WorldGen.genRand.Next(15, 30);
        for (int index3 = 1; (double) index3 < Main.worldSurface - 1.0; ++index3)
        {
          if (Main.tile[index2, index3].active)
          {
            for (int index4 = index2 - num14; index4 < index2 + num14; ++index4)
            {
              for (int index5 = index3 - num15; index5 < index3 + num15; ++index5)
              {
                if ((int) Main.tile[index4, index5].type == 3 || (int) Main.tile[index4, index5].type == 24)
                  Main.tile[index4, index5].frameX = (short) (WorldGen.genRand.Next(6, 8) * 18);
              }
            }
            break;
          }
        }
      }
      Main.statusText = "Planting mushrooms...";
      for (int index1 = 0; (double) index1 < (double) Main.maxTilesX * 0.002; ++index1)
      {
        int index2 = WorldGen.genRand.Next(20, Main.maxTilesX - 20);
        int num14 = WorldGen.genRand.Next(4, 10);
        int num15 = WorldGen.genRand.Next(15, 30);
        for (int index3 = 1; (double) index3 < Main.worldSurface - 1.0; ++index3)
        {
          if (Main.tile[index2, index3].active)
          {
            for (int index4 = index2 - num14; index4 < index2 + num14; ++index4)
            {
              for (int index5 = index3 - num15; index5 < index3 + num15; ++index5)
              {
                if ((int) Main.tile[index4, index5].type == 3 || (int) Main.tile[index4, index5].type == 24)
                  Main.tile[index4, index5].frameX = (short) 144;
              }
            }
            break;
          }
        }
      }
      WorldGen.gen = false;
    }

        public static bool GrowEpicTree(int i, int y)
        {
            int index1 = y;
            while ((int)Main.tile[i, index1].type == 20)
                ++index1;
            if (Main.tile[i, index1].active && ((int)Main.tile[i, index1].type == 2 && (int)Main.tile[i, index1 - 1].wall == 0 && (int)Main.tile[i, index1 - 1].liquid == 0) && (Main.tile[i - 1, index1].active && (int)Main.tile[i - 1, index1].type == 2 && Main.tile[i + 1, index1].active && (int)Main.tile[i + 1, index1].type == 2))
            {
                int num1 = 2;
                if (WorldGen.EmptyTileCheck(i - num1, i + num1, index1 - 55, index1 - 1, 20))
                {
                    bool flag1 = false;
                    bool flag2 = false;
                    int num2 = WorldGen.genRand.Next(20, 30);
                    for (int index2 = index1 - num2; index2 < index1; ++index2)
                    {
                        Main.tile[i, index2].frameNumber = (byte)WorldGen.genRand.Next(3);
                        Main.tile[i, index2].active = true;
                        Main.tile[i, index2].type = (byte)5;
                        int num3 = WorldGen.genRand.Next(3);
                        int num4 = WorldGen.genRand.Next(10);
                        if (index2 == index1 - 1 || index2 == index1 - num2)
                            num4 = 0;
                        while ((num4 == 5 || num4 == 7) && flag1 || (num4 == 6 || num4 == 7) && flag2)
                            num4 = WorldGen.genRand.Next(10);
                        flag1 = false;
                        flag2 = false;
                        if (num4 == 5 || num4 == 7)
                            flag1 = true;
                        if (num4 == 6 || num4 == 7)
                            flag2 = true;
                        if (num4 == 1)
                        {
                            if (num3 == 0)
                            {
                                Main.tile[i, index2].frameX = (short)0;
                                Main.tile[i, index2].frameY = (short)66;
                            }
                            if (num3 == 1)
                            {
                                Main.tile[i, index2].frameX = (short)0;
                                Main.tile[i, index2].frameY = (short)88;
                            }
                            if (num3 == 2)
                            {
                                Main.tile[i, index2].frameX = (short)0;
                                Main.tile[i, index2].frameY = (short)110;
                            }
                        }
                        else if (num4 == 2)
                        {
                            if (num3 == 0)
                            {
                                Main.tile[i, index2].frameX = (short)22;
                                Main.tile[i, index2].frameY = (short)0;
                            }
                            if (num3 == 1)
                            {
                                Main.tile[i, index2].frameX = (short)22;
                                Main.tile[i, index2].frameY = (short)22;
                            }
                            if (num3 == 2)
                            {
                                Main.tile[i, index2].frameX = (short)22;
                                Main.tile[i, index2].frameY = (short)44;
                            }
                        }
                        else if (num4 == 3)
                        {
                            if (num3 == 0)
                            {
                                Main.tile[i, index2].frameX = (short)44;
                                Main.tile[i, index2].frameY = (short)66;
                            }
                            if (num3 == 1)
                            {
                                Main.tile[i, index2].frameX = (short)44;
                                Main.tile[i, index2].frameY = (short)88;
                            }
                            if (num3 == 2)
                            {
                                Main.tile[i, index2].frameX = (short)44;
                                Main.tile[i, index2].frameY = (short)110;
                            }
                        }
                        else if (num4 == 4)
                        {
                            if (num3 == 0)
                            {
                                Main.tile[i, index2].frameX = (short)22;
                                Main.tile[i, index2].frameY = (short)66;
                            }
                            if (num3 == 1)
                            {
                                Main.tile[i, index2].frameX = (short)22;
                                Main.tile[i, index2].frameY = (short)88;
                            }
                            if (num3 == 2)
                            {
                                Main.tile[i, index2].frameX = (short)22;
                                Main.tile[i, index2].frameY = (short)110;
                            }
                        }
                        else if (num4 == 5)
                        {
                            if (num3 == 0)
                            {
                                Main.tile[i, index2].frameX = (short)88;
                                Main.tile[i, index2].frameY = (short)0;
                            }
                            if (num3 == 1)
                            {
                                Main.tile[i, index2].frameX = (short)88;
                                Main.tile[i, index2].frameY = (short)22;
                            }
                            if (num3 == 2)
                            {
                                Main.tile[i, index2].frameX = (short)88;
                                Main.tile[i, index2].frameY = (short)44;
                            }
                        }
                        else if (num4 == 6)
                        {
                            if (num3 == 0)
                            {
                                Main.tile[i, index2].frameX = (short)66;
                                Main.tile[i, index2].frameY = (short)66;
                            }
                            if (num3 == 1)
                            {
                                Main.tile[i, index2].frameX = (short)66;
                                Main.tile[i, index2].frameY = (short)88;
                            }
                            if (num3 == 2)
                            {
                                Main.tile[i, index2].frameX = (short)66;
                                Main.tile[i, index2].frameY = (short)110;
                            }
                        }
                        else if (num4 == 7)
                        {
                            if (num3 == 0)
                            {
                                Main.tile[i, index2].frameX = (short)110;
                                Main.tile[i, index2].frameY = (short)66;
                            }
                            if (num3 == 1)
                            {
                                Main.tile[i, index2].frameX = (short)110;
                                Main.tile[i, index2].frameY = (short)88;
                            }
                            if (num3 == 2)
                            {
                                Main.tile[i, index2].frameX = (short)110;
                                Main.tile[i, index2].frameY = (short)110;
                            }
                        }
                        else
                        {
                            if (num3 == 0)
                            {
                                Main.tile[i, index2].frameX = (short)0;
                                Main.tile[i, index2].frameY = (short)0;
                            }
                            if (num3 == 1)
                            {
                                Main.tile[i, index2].frameX = (short)0;
                                Main.tile[i, index2].frameY = (short)22;
                            }
                            if (num3 == 2)
                            {
                                Main.tile[i, index2].frameX = (short)0;
                                Main.tile[i, index2].frameY = (short)44;
                            }
                        }
                        if (num4 == 5 || num4 == 7)
                        {
                            Main.tile[i - 1, index2].active = true;
                            Main.tile[i - 1, index2].type = (byte)5;
                            int num5 = WorldGen.genRand.Next(3);
                            if (WorldGen.genRand.Next(3) < 2)
                            {
                                if (num5 == 0)
                                {
                                    Main.tile[i - 1, index2].frameX = (short)44;
                                    Main.tile[i - 1, index2].frameY = (short)198;
                                }
                                if (num5 == 1)
                                {
                                    Main.tile[i - 1, index2].frameX = (short)44;
                                    Main.tile[i - 1, index2].frameY = (short)220;
                                }
                                if (num5 == 2)
                                {
                                    Main.tile[i - 1, index2].frameX = (short)44;
                                    Main.tile[i - 1, index2].frameY = (short)242;
                                }
                            }
                            else
                            {
                                if (num5 == 0)
                                {
                                    Main.tile[i - 1, index2].frameX = (short)66;
                                    Main.tile[i - 1, index2].frameY = (short)0;
                                }
                                if (num5 == 1)
                                {
                                    Main.tile[i - 1, index2].frameX = (short)66;
                                    Main.tile[i - 1, index2].frameY = (short)22;
                                }
                                if (num5 == 2)
                                {
                                    Main.tile[i - 1, index2].frameX = (short)66;
                                    Main.tile[i - 1, index2].frameY = (short)44;
                                }
                            }
                        }
                        if (num4 == 6 || num4 == 7)
                        {
                            Main.tile[i + 1, index2].active = true;
                            Main.tile[i + 1, index2].type = (byte)5;
                            int num5 = WorldGen.genRand.Next(3);
                            if (WorldGen.genRand.Next(3) < 2)
                            {
                                if (num5 == 0)
                                {
                                    Main.tile[i + 1, index2].frameX = (short)66;
                                    Main.tile[i + 1, index2].frameY = (short)198;
                                }
                                if (num5 == 1)
                                {
                                    Main.tile[i + 1, index2].frameX = (short)66;
                                    Main.tile[i + 1, index2].frameY = (short)220;
                                }
                                if (num5 == 2)
                                {
                                    Main.tile[i + 1, index2].frameX = (short)66;
                                    Main.tile[i + 1, index2].frameY = (short)242;
                                }
                            }
                            else
                            {
                                if (num5 == 0)
                                {
                                    Main.tile[i + 1, index2].frameX = (short)88;
                                    Main.tile[i + 1, index2].frameY = (short)66;
                                }
                                if (num5 == 1)
                                {
                                    Main.tile[i + 1, index2].frameX = (short)88;
                                    Main.tile[i + 1, index2].frameY = (short)88;
                                }
                                if (num5 == 2)
                                {
                                    Main.tile[i + 1, index2].frameX = (short)88;
                                    Main.tile[i + 1, index2].frameY = (short)110;
                                }
                            }
                        }
                    }
                    int num6 = WorldGen.genRand.Next(3);
                    if (num6 == 0 || num6 == 1)
                    {
                        Main.tile[i + 1, index1 - 1].active = true;
                        Main.tile[i + 1, index1 - 1].type = (byte)5;
                        int num3 = WorldGen.genRand.Next(3);
                        if (num3 == 0)
                        {
                            Main.tile[i + 1, index1 - 1].frameX = (short)22;
                            Main.tile[i + 1, index1 - 1].frameY = (short)132;
                        }
                        if (num3 == 1)
                        {
                            Main.tile[i + 1, index1 - 1].frameX = (short)22;
                            Main.tile[i + 1, index1 - 1].frameY = (short)154;
                        }
                        if (num3 == 2)
                        {
                            Main.tile[i + 1, index1 - 1].frameX = (short)22;
                            Main.tile[i + 1, index1 - 1].frameY = (short)176;
                        }
                    }
                    if (num6 == 0 || num6 == 2)
                    {
                        Main.tile[i - 1, index1 - 1].active = true;
                        Main.tile[i - 1, index1 - 1].type = (byte)5;
                        int num3 = WorldGen.genRand.Next(3);
                        if (num3 == 0)
                        {
                            Main.tile[i - 1, index1 - 1].frameX = (short)44;
                            Main.tile[i - 1, index1 - 1].frameY = (short)132;
                        }
                        if (num3 == 1)
                        {
                            Main.tile[i - 1, index1 - 1].frameX = (short)44;
                            Main.tile[i - 1, index1 - 1].frameY = (short)154;
                        }
                        if (num3 == 2)
                        {
                            Main.tile[i - 1, index1 - 1].frameX = (short)44;
                            Main.tile[i - 1, index1 - 1].frameY = (short)176;
                        }
                    }
                    int num7 = WorldGen.genRand.Next(3);
                    if (num6 == 0)
                    {
                        if (num7 == 0)
                        {
                            Main.tile[i, index1 - 1].frameX = (short)88;
                            Main.tile[i, index1 - 1].frameY = (short)132;
                        }
                        if (num7 == 1)
                        {
                            Main.tile[i, index1 - 1].frameX = (short)88;
                            Main.tile[i, index1 - 1].frameY = (short)154;
                        }
                        if (num7 == 2)
                        {
                            Main.tile[i, index1 - 1].frameX = (short)88;
                            Main.tile[i, index1 - 1].frameY = (short)176;
                        }
                    }
                    else if (num6 == 1)
                    {
                        if (num7 == 0)
                        {
                            Main.tile[i, index1 - 1].frameX = (short)0;
                            Main.tile[i, index1 - 1].frameY = (short)132;
                        }
                        if (num7 == 1)
                        {
                            Main.tile[i, index1 - 1].frameX = (short)0;
                            Main.tile[i, index1 - 1].frameY = (short)154;
                        }
                        if (num7 == 2)
                        {
                            Main.tile[i, index1 - 1].frameX = (short)0;
                            Main.tile[i, index1 - 1].frameY = (short)176;
                        }
                    }
                    else if (num6 == 2)
                    {
                        if (num7 == 0)
                        {
                            Main.tile[i, index1 - 1].frameX = (short)66;
                            Main.tile[i, index1 - 1].frameY = (short)132;
                        }
                        if (num7 == 1)
                        {
                            Main.tile[i, index1 - 1].frameX = (short)66;
                            Main.tile[i, index1 - 1].frameY = (short)154;
                        }
                        if (num7 == 2)
                        {
                            Main.tile[i, index1 - 1].frameX = (short)66;
                            Main.tile[i, index1 - 1].frameY = (short)176;
                        }
                    }
                    if (WorldGen.genRand.Next(3) < 2)
                    {
                        int num3 = WorldGen.genRand.Next(3);
                        if (num3 == 0)
                        {
                            Main.tile[i, index1 - num2].frameX = (short)22;
                            Main.tile[i, index1 - num2].frameY = (short)198;
                        }
                        if (num3 == 1)
                        {
                            Main.tile[i, index1 - num2].frameX = (short)22;
                            Main.tile[i, index1 - num2].frameY = (short)220;
                        }
                        if (num3 == 2)
                        {
                            Main.tile[i, index1 - num2].frameX = (short)22;
                            Main.tile[i, index1 - num2].frameY = (short)242;
                        }
                    }
                    else
                    {
                        int num3 = WorldGen.genRand.Next(3);
                        if (num3 == 0)
                        {
                            Main.tile[i, index1 - num2].frameX = (short)0;
                            Main.tile[i, index1 - num2].frameY = (short)198;
                        }
                        if (num3 == 1)
                        {
                            Main.tile[i, index1 - num2].frameX = (short)0;
                            Main.tile[i, index1 - num2].frameY = (short)220;
                        }
                        if (num3 == 2)
                        {
                            Main.tile[i, index1 - num2].frameX = (short)0;
                            Main.tile[i, index1 - num2].frameY = (short)242;
                        }
                    }
                    WorldGen.RangeFrame(i - 2, index1 - num2 - 1, i + 2, index1 + 1);
                    if (Main.netMode == 2)
                        NetMessage.SendTileSquare(-1, i, (int)((double)index1 - (double)num2 * 0.5), num2 + 1);
                    return true;
                }
            }
            return false;
        }

        public static void GrowTree(int i, int y)
        {
            int index1 = y;
            while ((int)Main.tile[i, index1].type == 20)
                ++index1;
            if (((int)Main.tile[i - 1, index1 - 1].liquid == 0 && (int)Main.tile[i - 1, index1 - 1].liquid == 0 && (int)Main.tile[i + 1, index1 - 1].liquid == 0 || (int)Main.tile[i, index1].type == 60) && Main.tile[i, index1].active && (((int)Main.tile[i, index1].type == 2 || (int)Main.tile[i, index1].type == 23 || (int)Main.tile[i, index1].type == 60) && (int)Main.tile[i, index1 - 1].wall == 0) && (Main.tile[i - 1, index1].active && ((int)Main.tile[i - 1, index1].type == 2 || (int)Main.tile[i - 1, index1].type == 23 || (int)Main.tile[i - 1, index1].type == 60) && Main.tile[i + 1, index1].active && ((int)Main.tile[i + 1, index1].type == 2 || (int)Main.tile[i + 1, index1].type == 23 || (int)Main.tile[i + 1, index1].type == 60)))
            {
                int num1 = 2;
                if (WorldGen.EmptyTileCheck(i - num1, i + num1, index1 - 14, index1 - 1, 20))
                {
                    bool flag1 = false;
                    bool flag2 = false;
                    int num2 = WorldGen.genRand.Next(5, 15);
                    for (int index2 = index1 - num2; index2 < index1; ++index2)
                    {
                        Main.tile[i, index2].frameNumber = (byte)WorldGen.genRand.Next(3);
                        Main.tile[i, index2].active = true;
                        Main.tile[i, index2].type = (byte)5;
                        int num3 = WorldGen.genRand.Next(3);
                        int num4 = WorldGen.genRand.Next(10);
                        if (index2 == index1 - 1 || index2 == index1 - num2)
                            num4 = 0;
                        while ((num4 == 5 || num4 == 7) && flag1 || (num4 == 6 || num4 == 7) && flag2)
                            num4 = WorldGen.genRand.Next(10);
                        flag1 = false;
                        flag2 = false;
                        if (num4 == 5 || num4 == 7)
                            flag1 = true;
                        if (num4 == 6 || num4 == 7)
                            flag2 = true;
                        if (num4 == 1)
                        {
                            if (num3 == 0)
                            {
                                Main.tile[i, index2].frameX = (short)0;
                                Main.tile[i, index2].frameY = (short)66;
                            }
                            if (num3 == 1)
                            {
                                Main.tile[i, index2].frameX = (short)0;
                                Main.tile[i, index2].frameY = (short)88;
                            }
                            if (num3 == 2)
                            {
                                Main.tile[i, index2].frameX = (short)0;
                                Main.tile[i, index2].frameY = (short)110;
                            }
                        }
                        else if (num4 == 2)
                        {
                            if (num3 == 0)
                            {
                                Main.tile[i, index2].frameX = (short)22;
                                Main.tile[i, index2].frameY = (short)0;
                            }
                            if (num3 == 1)
                            {
                                Main.tile[i, index2].frameX = (short)22;
                                Main.tile[i, index2].frameY = (short)22;
                            }
                            if (num3 == 2)
                            {
                                Main.tile[i, index2].frameX = (short)22;
                                Main.tile[i, index2].frameY = (short)44;
                            }
                        }
                        else if (num4 == 3)
                        {
                            if (num3 == 0)
                            {
                                Main.tile[i, index2].frameX = (short)44;
                                Main.tile[i, index2].frameY = (short)66;
                            }
                            if (num3 == 1)
                            {
                                Main.tile[i, index2].frameX = (short)44;
                                Main.tile[i, index2].frameY = (short)88;
                            }
                            if (num3 == 2)
                            {
                                Main.tile[i, index2].frameX = (short)44;
                                Main.tile[i, index2].frameY = (short)110;
                            }
                        }
                        else if (num4 == 4)
                        {
                            if (num3 == 0)
                            {
                                Main.tile[i, index2].frameX = (short)22;
                                Main.tile[i, index2].frameY = (short)66;
                            }
                            if (num3 == 1)
                            {
                                Main.tile[i, index2].frameX = (short)22;
                                Main.tile[i, index2].frameY = (short)88;
                            }
                            if (num3 == 2)
                            {
                                Main.tile[i, index2].frameX = (short)22;
                                Main.tile[i, index2].frameY = (short)110;
                            }
                        }
                        else if (num4 == 5)
                        {
                            if (num3 == 0)
                            {
                                Main.tile[i, index2].frameX = (short)88;
                                Main.tile[i, index2].frameY = (short)0;
                            }
                            if (num3 == 1)
                            {
                                Main.tile[i, index2].frameX = (short)88;
                                Main.tile[i, index2].frameY = (short)22;
                            }
                            if (num3 == 2)
                            {
                                Main.tile[i, index2].frameX = (short)88;
                                Main.tile[i, index2].frameY = (short)44;
                            }
                        }
                        else if (num4 == 6)
                        {
                            if (num3 == 0)
                            {
                                Main.tile[i, index2].frameX = (short)66;
                                Main.tile[i, index2].frameY = (short)66;
                            }
                            if (num3 == 1)
                            {
                                Main.tile[i, index2].frameX = (short)66;
                                Main.tile[i, index2].frameY = (short)88;
                            }
                            if (num3 == 2)
                            {
                                Main.tile[i, index2].frameX = (short)66;
                                Main.tile[i, index2].frameY = (short)110;
                            }
                        }
                        else if (num4 == 7)
                        {
                            if (num3 == 0)
                            {
                                Main.tile[i, index2].frameX = (short)110;
                                Main.tile[i, index2].frameY = (short)66;
                            }
                            if (num3 == 1)
                            {
                                Main.tile[i, index2].frameX = (short)110;
                                Main.tile[i, index2].frameY = (short)88;
                            }
                            if (num3 == 2)
                            {
                                Main.tile[i, index2].frameX = (short)110;
                                Main.tile[i, index2].frameY = (short)110;
                            }
                        }
                        else
                        {
                            if (num3 == 0)
                            {
                                Main.tile[i, index2].frameX = (short)0;
                                Main.tile[i, index2].frameY = (short)0;
                            }
                            if (num3 == 1)
                            {
                                Main.tile[i, index2].frameX = (short)0;
                                Main.tile[i, index2].frameY = (short)22;
                            }
                            if (num3 == 2)
                            {
                                Main.tile[i, index2].frameX = (short)0;
                                Main.tile[i, index2].frameY = (short)44;
                            }
                        }
                        if (num4 == 5 || num4 == 7)
                        {
                            Main.tile[i - 1, index2].active = true;
                            Main.tile[i - 1, index2].type = (byte)5;
                            int num5 = WorldGen.genRand.Next(3);
                            if (WorldGen.genRand.Next(3) < 2)
                            {
                                if (num5 == 0)
                                {
                                    Main.tile[i - 1, index2].frameX = (short)44;
                                    Main.tile[i - 1, index2].frameY = (short)198;
                                }
                                if (num5 == 1)
                                {
                                    Main.tile[i - 1, index2].frameX = (short)44;
                                    Main.tile[i - 1, index2].frameY = (short)220;
                                }
                                if (num5 == 2)
                                {
                                    Main.tile[i - 1, index2].frameX = (short)44;
                                    Main.tile[i - 1, index2].frameY = (short)242;
                                }
                            }
                            else
                            {
                                if (num5 == 0)
                                {
                                    Main.tile[i - 1, index2].frameX = (short)66;
                                    Main.tile[i - 1, index2].frameY = (short)0;
                                }
                                if (num5 == 1)
                                {
                                    Main.tile[i - 1, index2].frameX = (short)66;
                                    Main.tile[i - 1, index2].frameY = (short)22;
                                }
                                if (num5 == 2)
                                {
                                    Main.tile[i - 1, index2].frameX = (short)66;
                                    Main.tile[i - 1, index2].frameY = (short)44;
                                }
                            }
                        }
                        if (num4 == 6 || num4 == 7)
                        {
                            Main.tile[i + 1, index2].active = true;
                            Main.tile[i + 1, index2].type = (byte)5;
                            int num5 = WorldGen.genRand.Next(3);
                            if (WorldGen.genRand.Next(3) < 2)
                            {
                                if (num5 == 0)
                                {
                                    Main.tile[i + 1, index2].frameX = (short)66;
                                    Main.tile[i + 1, index2].frameY = (short)198;
                                }
                                if (num5 == 1)
                                {
                                    Main.tile[i + 1, index2].frameX = (short)66;
                                    Main.tile[i + 1, index2].frameY = (short)220;
                                }
                                if (num5 == 2)
                                {
                                    Main.tile[i + 1, index2].frameX = (short)66;
                                    Main.tile[i + 1, index2].frameY = (short)242;
                                }
                            }
                            else
                            {
                                if (num5 == 0)
                                {
                                    Main.tile[i + 1, index2].frameX = (short)88;
                                    Main.tile[i + 1, index2].frameY = (short)66;
                                }
                                if (num5 == 1)
                                {
                                    Main.tile[i + 1, index2].frameX = (short)88;
                                    Main.tile[i + 1, index2].frameY = (short)88;
                                }
                                if (num5 == 2)
                                {
                                    Main.tile[i + 1, index2].frameX = (short)88;
                                    Main.tile[i + 1, index2].frameY = (short)110;
                                }
                            }
                        }
                    }
                    int num6 = WorldGen.genRand.Next(3);
                    if (num6 == 0 || num6 == 1)
                    {
                        Main.tile[i + 1, index1 - 1].active = true;
                        Main.tile[i + 1, index1 - 1].type = (byte)5;
                        int num3 = WorldGen.genRand.Next(3);
                        if (num3 == 0)
                        {
                            Main.tile[i + 1, index1 - 1].frameX = (short)22;
                            Main.tile[i + 1, index1 - 1].frameY = (short)132;
                        }
                        if (num3 == 1)
                        {
                            Main.tile[i + 1, index1 - 1].frameX = (short)22;
                            Main.tile[i + 1, index1 - 1].frameY = (short)154;
                        }
                        if (num3 == 2)
                        {
                            Main.tile[i + 1, index1 - 1].frameX = (short)22;
                            Main.tile[i + 1, index1 - 1].frameY = (short)176;
                        }
                    }
                    if (num6 == 0 || num6 == 2)
                    {
                        Main.tile[i - 1, index1 - 1].active = true;
                        Main.tile[i - 1, index1 - 1].type = (byte)5;
                        int num3 = WorldGen.genRand.Next(3);
                        if (num3 == 0)
                        {
                            Main.tile[i - 1, index1 - 1].frameX = (short)44;
                            Main.tile[i - 1, index1 - 1].frameY = (short)132;
                        }
                        if (num3 == 1)
                        {
                            Main.tile[i - 1, index1 - 1].frameX = (short)44;
                            Main.tile[i - 1, index1 - 1].frameY = (short)154;
                        }
                        if (num3 == 2)
                        {
                            Main.tile[i - 1, index1 - 1].frameX = (short)44;
                            Main.tile[i - 1, index1 - 1].frameY = (short)176;
                        }
                    }
                    int num7 = WorldGen.genRand.Next(3);
                    if (num6 == 0)
                    {
                        if (num7 == 0)
                        {
                            Main.tile[i, index1 - 1].frameX = (short)88;
                            Main.tile[i, index1 - 1].frameY = (short)132;
                        }
                        if (num7 == 1)
                        {
                            Main.tile[i, index1 - 1].frameX = (short)88;
                            Main.tile[i, index1 - 1].frameY = (short)154;
                        }
                        if (num7 == 2)
                        {
                            Main.tile[i, index1 - 1].frameX = (short)88;
                            Main.tile[i, index1 - 1].frameY = (short)176;
                        }
                    }
                    else if (num6 == 1)
                    {
                        if (num7 == 0)
                        {
                            Main.tile[i, index1 - 1].frameX = (short)0;
                            Main.tile[i, index1 - 1].frameY = (short)132;
                        }
                        if (num7 == 1)
                        {
                            Main.tile[i, index1 - 1].frameX = (short)0;
                            Main.tile[i, index1 - 1].frameY = (short)154;
                        }
                        if (num7 == 2)
                        {
                            Main.tile[i, index1 - 1].frameX = (short)0;
                            Main.tile[i, index1 - 1].frameY = (short)176;
                        }
                    }
                    else if (num6 == 2)
                    {
                        if (num7 == 0)
                        {
                            Main.tile[i, index1 - 1].frameX = (short)66;
                            Main.tile[i, index1 - 1].frameY = (short)132;
                        }
                        if (num7 == 1)
                        {
                            Main.tile[i, index1 - 1].frameX = (short)66;
                            Main.tile[i, index1 - 1].frameY = (short)154;
                        }
                        if (num7 == 2)
                        {
                            Main.tile[i, index1 - 1].frameX = (short)66;
                            Main.tile[i, index1 - 1].frameY = (short)176;
                        }
                    }
                    if (WorldGen.genRand.Next(3) < 2)
                    {
                        int num3 = WorldGen.genRand.Next(3);
                        if (num3 == 0)
                        {
                            Main.tile[i, index1 - num2].frameX = (short)22;
                            Main.tile[i, index1 - num2].frameY = (short)198;
                        }
                        if (num3 == 1)
                        {
                            Main.tile[i, index1 - num2].frameX = (short)22;
                            Main.tile[i, index1 - num2].frameY = (short)220;
                        }
                        if (num3 == 2)
                        {
                            Main.tile[i, index1 - num2].frameX = (short)22;
                            Main.tile[i, index1 - num2].frameY = (short)242;
                        }
                    }
                    else
                    {
                        int num3 = WorldGen.genRand.Next(3);
                        if (num3 == 0)
                        {
                            Main.tile[i, index1 - num2].frameX = (short)0;
                            Main.tile[i, index1 - num2].frameY = (short)198;
                        }
                        if (num3 == 1)
                        {
                            Main.tile[i, index1 - num2].frameX = (short)0;
                            Main.tile[i, index1 - num2].frameY = (short)220;
                        }
                        if (num3 == 2)
                        {
                            Main.tile[i, index1 - num2].frameX = (short)0;
                            Main.tile[i, index1 - num2].frameY = (short)242;
                        }
                    }
                    WorldGen.RangeFrame(i - 2, index1 - num2 - 1, i + 2, index1 + 1);
                    if (Main.netMode == 2)
                        NetMessage.SendTileSquare(-1, i, (int)((double)index1 - (double)num2 * 0.5), num2 + 1);
                }
            }
        }

        public static void GrowShroom(int i, int y)
        {
            int index1 = y;
            if (!Main.tile[i - 1, index1 - 1].lava && !Main.tile[i - 1, index1 - 1].lava && !Main.tile[i + 1, index1 - 1].lava && Main.tile[i, index1].active && ((int)Main.tile[i, index1].type == 70 && (int)Main.tile[i, index1 - 1].wall == 0) && (Main.tile[i - 1, index1].active && (int)Main.tile[i - 1, index1].type == 70 && Main.tile[i + 1, index1].active && (int)Main.tile[i + 1, index1].type == 70 && WorldGen.EmptyTileCheck(i - 2, i + 2, index1 - 13, index1 - 1, 71)))
            {
                int num1 = WorldGen.genRand.Next(4, 11);
                for (int index2 = index1 - num1; index2 < index1; ++index2)
                {
                    Main.tile[i, index2].frameNumber = (byte)WorldGen.genRand.Next(3);
                    Main.tile[i, index2].active = true;
                    Main.tile[i, index2].type = (byte)72;
                    int num2 = WorldGen.genRand.Next(3);
                    if (num2 == 0)
                    {
                        Main.tile[i, index2].frameX = (short)0;
                        Main.tile[i, index2].frameY = (short)0;
                    }
                    if (num2 == 1)
                    {
                        Main.tile[i, index2].frameX = (short)0;
                        Main.tile[i, index2].frameY = (short)18;
                    }
                    if (num2 == 2)
                    {
                        Main.tile[i, index2].frameX = (short)0;
                        Main.tile[i, index2].frameY = (short)36;
                    }
                }
                int num3 = WorldGen.genRand.Next(3);
                if (num3 == 0)
                {
                    Main.tile[i, index1 - num1].frameX = (short)36;
                    Main.tile[i, index1 - num1].frameY = (short)0;
                }
                if (num3 == 1)
                {
                    Main.tile[i, index1 - num1].frameX = (short)36;
                    Main.tile[i, index1 - num1].frameY = (short)18;
                }
                if (num3 == 2)
                {
                    Main.tile[i, index1 - num1].frameX = (short)36;
                    Main.tile[i, index1 - num1].frameY = (short)36;
                }
                WorldGen.RangeFrame(i - 2, index1 - num1 - 1, i + 2, index1 + 1);
                if (Main.netMode == 2)
                    NetMessage.SendTileSquare(-1, i, (int)((double)index1 - (double)num1 * 0.5), num1 + 1);
            }
        }

        public static void AddTrees()
        {
            for (int i = 1; i < Main.maxTilesX - 1; ++i)
            {
                for (int y = 20; (double)y < Main.worldSurface; ++y)
                    WorldGen.GrowTree(i, y);
            }
        }

        public static bool EmptyTileCheck(int startX, int endX, int startY, int endY, int ignoreStyle = -1)
        {
            if (startX < 0 || endX >= Main.maxTilesX || (startY < 0 || endY >= Main.maxTilesY))
            {
                return false;
            }
            else
            {
                for (int index1 = startX; index1 < endX + 1; ++index1)
                {
                    for (int index2 = startY; index2 < endY + 1; ++index2)
                    {
                        if (Main.tile[index1, index2].active && (ignoreStyle == -1 || ignoreStyle == 11 && (int)Main.tile[index1, index2].type != 11 || (ignoreStyle == 20 && (int)Main.tile[index1, index2].type != 20 && ((int)Main.tile[index1, index2].type != 3 && (int)Main.tile[index1, index2].type != 24) && ((int)Main.tile[index1, index2].type != 61 && (int)Main.tile[index1, index2].type != 32 && ((int)Main.tile[index1, index2].type != 69 && (int)Main.tile[index1, index2].type != 73)) && (int)Main.tile[index1, index2].type != 74 || ignoreStyle == 71 && (int)Main.tile[index1, index2].type != 71)))
                            return false;
                    }
                }
                return true;
            }
        }

        public static bool PlaceDoor(int i, int j, int type)
        {
            try
            {
                if (Main.tile[i, j - 2].active && Main.tileSolid[(int)Main.tile[i, j - 2].type] && Main.tile[i, j + 2].active && Main.tileSolid[(int)Main.tile[i, j + 2].type])
                {
                    Main.tile[i, j - 1].active = true;
                    Main.tile[i, j - 1].type = (byte)10;
                    Main.tile[i, j - 1].frameY = (short)0;
                    Main.tile[i, j - 1].frameX = (short)(WorldGen.genRand.Next(3) * 18);
                    Main.tile[i, j].active = true;
                    Main.tile[i, j].type = (byte)10;
                    Main.tile[i, j].frameY = (short)18;
                    Main.tile[i, j].frameX = (short)(WorldGen.genRand.Next(3) * 18);
                    Main.tile[i, j + 1].active = true;
                    Main.tile[i, j + 1].type = (byte)10;
                    Main.tile[i, j + 1].frameY = (short)36;
                    Main.tile[i, j + 1].frameX = (short)(WorldGen.genRand.Next(3) * 18);
                    return true;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public static bool CloseDoor(int i, int j, bool forced = false)
        {
            int num1 = 0;
            int i1 = i;
            int num2 = j;
            if (Main.tile[i, j] == null)
                Main.tile[i, j] = new Tile();
            int num3 = (int)Main.tile[i, j].frameX;
            int num4 = (int)Main.tile[i, j].frameY;
            if (num3 == 0)
            {
                i1 = i;
                num1 = 1;
            }
            else if (num3 == 18)
            {
                i1 = i - 1;
                num1 = 1;
            }
            else if (num3 == 36)
            {
                i1 = i + 1;
                num1 = -1;
            }
            else if (num3 == 54)
            {
                i1 = i;
                num1 = -1;
            }
            if (num4 == 0)
                num2 = j;
            else if (num4 == 18)
                num2 = j - 1;
            else if (num4 == 36)
                num2 = j - 2;
            int num5 = i1;
            if (num1 == -1)
                num5 = i1 - 1;
            if (!forced)
            {
                for (int j1 = num2; j1 < num2 + 3; ++j1)
                {
                    if (!Collision.EmptyTile(i1, j1, true))
                        return false;
                }
            }
            for (int index1 = num5; index1 < num5 + 2; ++index1)
            {
                for (int index2 = num2; index2 < num2 + 3; ++index2)
                {
                    if (index1 == i1)
                    {
                        if (Main.tile[index1, index2] == null)
                            Main.tile[index1, index2] = new Tile();
                        Main.tile[index1, index2].type = (byte)10;
                        Main.tile[index1, index2].frameX = (short)(WorldGen.genRand.Next(3) * 18);
                    }
                    else
                    {
                        if (Main.tile[index1, index2] == null)
                            Main.tile[index1, index2] = new Tile();
                        Main.tile[index1, index2].active = false;
                    }
                }
            }
            for (int i2 = i1 - 1; i2 <= i1 + 1; ++i2)
            {
                for (int j1 = num2 - 1; j1 <= num2 + 2; ++j1)
                    WorldGen.TileFrame(i2, j1, false, false);
            }
            Main.PlaySound(9, i * 16, j * 16, 1);
            return true;
        }

        public static bool AddLifeCrystal(int i, int j)
        {
            for (int index = j; index < Main.maxTilesY; ++index)
            {
                if (Main.tile[i, index].active && Main.tileSolid[(int)Main.tile[i, index].type])
                {
                    int endX = i;
                    int endY = index - 1;
                    if (Main.tile[endX, endY - 1].lava || Main.tile[endX - 1, endY - 1].lava || !WorldGen.EmptyTileCheck(endX - 1, endX, endY - 1, endY, -1))
                    {
                        return false;
                    }
                    else
                    {
                        Main.tile[endX - 1, endY - 1].active = true;
                        Main.tile[endX - 1, endY - 1].type = (byte)12;
                        Main.tile[endX - 1, endY - 1].frameX = (short)0;
                        Main.tile[endX - 1, endY - 1].frameY = (short)0;
                        Main.tile[endX, endY - 1].active = true;
                        Main.tile[endX, endY - 1].type = (byte)12;
                        Main.tile[endX, endY - 1].frameX = (short)18;
                        Main.tile[endX, endY - 1].frameY = (short)0;
                        Main.tile[endX - 1, endY].active = true;
                        Main.tile[endX - 1, endY].type = (byte)12;
                        Main.tile[endX - 1, endY].frameX = (short)0;
                        Main.tile[endX - 1, endY].frameY = (short)18;
                        Main.tile[endX, endY].active = true;
                        Main.tile[endX, endY].type = (byte)12;
                        Main.tile[endX, endY].frameX = (short)18;
                        Main.tile[endX, endY].frameY = (short)18;
                        return true;
                    }
                }
            }
            return false;
        }

        public static void AddShadowOrb(int x, int y)
        {
            if (x >= 10 && x <= Main.maxTilesX - 10 && (y >= 10 && y <= Main.maxTilesY - 10))
            {
                Main.tile[x - 1, y - 1].active = true;
                Main.tile[x - 1, y - 1].type = (byte)31;
                Main.tile[x - 1, y - 1].frameX = (short)0;
                Main.tile[x - 1, y - 1].frameY = (short)0;
                Main.tile[x, y - 1].active = true;
                Main.tile[x, y - 1].type = (byte)31;
                Main.tile[x, y - 1].frameX = (short)18;
                Main.tile[x, y - 1].frameY = (short)0;
                Main.tile[x - 1, y].active = true;
                Main.tile[x - 1, y].type = (byte)31;
                Main.tile[x - 1, y].frameX = (short)0;
                Main.tile[x - 1, y].frameY = (short)18;
                Main.tile[x, y].active = true;
                Main.tile[x, y].type = (byte)31;
                Main.tile[x, y].frameX = (short)18;
                Main.tile[x, y].frameY = (short)18;
            }
        }

        public static void AddHellHouses()
        {
            int num = (int)((double)Main.maxTilesX * 0.25);
            for (int i = num; i < Main.maxTilesX - num; ++i)
            {
                int j = Main.maxTilesY - 40;
                while (Main.tile[i, j].active || (int)Main.tile[i, j].liquid > 0)
                    --j;
                if (Main.tile[i, j + 1].active)
                {
                    WorldGen.HellHouse(i, j);
                    i += WorldGen.genRand.Next(15, 80);
                }
            }
        }

        public static void HellHouse(int i, int j)
        {
            int width = WorldGen.genRand.Next(8, 20);
            int num1 = WorldGen.genRand.Next(3);
            int num2 = WorldGen.genRand.Next(7);
            int i1 = i;
            int j1 = j;
            for (int index = 0; index < num1; ++index)
            {
                int height = WorldGen.genRand.Next(5, 9);
                WorldGen.HellRoom(i1, j1, width, height);
                j1 -= height;
            }
            int j2 = j;
            for (int index = 0; index < num2; ++index)
            {
                int height = WorldGen.genRand.Next(5, 9);
                j2 += height;
                WorldGen.HellRoom(i1, j2, width, height);
            }
            for (int index1 = i - width / 2; index1 <= i + width / 2; ++index1)
            {
                int index2 = j;
                while (index2 < Main.maxTilesY && (Main.tile[index1, index2].active && (int)Main.tile[index1, index2].type == 76 || (int)Main.tile[index1, index2].wall == 13))
                    ++index2;
                int num3 = 6 + WorldGen.genRand.Next(3);
                while (index2 < Main.maxTilesY && !Main.tile[index1, index2].active)
                {
                    --num3;
                    Main.tile[index1, index2].active = true;
                    Main.tile[index1, index2].type = (byte)57;
                    ++index2;
                    if (num3 <= 0)
                        break;
                }
            }
            int index3 = j;
            while (index3 < Main.maxTilesY && (Main.tile[i, index3].active && (int)Main.tile[i, index3].type == 76 || (int)Main.tile[i, index3].wall == 13))
                ++index3;
            int index4 = index3 - 1;
            int maxValue = index4;
            while (Main.tile[i, index4].active && (int)Main.tile[i, index4].type == 76 || (int)Main.tile[i, index4].wall == 13)
            {
                --index4;
                if (Main.tile[i, index4].active && (int)Main.tile[i, index4].type == 76)
                {
                    int num3 = WorldGen.genRand.Next(i - width / 2 + 1, i + width / 2 - 1);
                    int num4 = WorldGen.genRand.Next(i - width / 2 + 1, i + width / 2 - 1);
                    if (num3 > num4)
                    {
                        int num5 = num3;
                        num3 = num4;
                        num4 = num5;
                    }
                    if (num3 == num4)
                    {
                        if (num3 < i)
                            ++num4;
                        else
                            --num3;
                    }
                    for (int index1 = num3; index1 <= num4; ++index1)
                    {
                        if ((int)Main.tile[index1, index4 - 1].wall == 13)
                            Main.tile[index1, index4].wall = (byte)13;
                        Main.tile[index1, index4].type = (byte)19;
                        Main.tile[index1, index4].active = true;
                    }
                    --index4;
                }
            }
            int minValue = index4;
            float num6 = (float)((maxValue - minValue) * width) * 0.02f;
            for (int index1 = 0; (double)index1 < (double)num6; ++index1)
            {
                int num3 = WorldGen.genRand.Next(i - width / 2, i + width / 2 + 1);
                int num4 = WorldGen.genRand.Next(minValue, maxValue);
                int num5 = WorldGen.genRand.Next(3, 8);
                for (int index2 = num3 - num5; index2 <= num3 + num5; ++index2)
                {
                    for (int index5 = num4 - num5; index5 <= num4 + num5; ++index5)
                    {
                        float num7 = (float)Math.Abs(index2 - num3);
                        float num8 = (float)Math.Abs(index5 - num4);
                        if (Math.Sqrt((double)num7 * (double)num7 + (double)num8 * (double)num8) < (double)num5 * 0.4)
                        {
                            if ((int)Main.tile[index2, index5].type == 76 || (int)Main.tile[index2, index5].type == 19)
                                Main.tile[index2, index5].active = false;
                            Main.tile[index2, index5].wall = (byte)0;
                        }
                    }
                }
            }
        }

        public static void HellRoom(int i, int j, int width, int height)
        {
            for (int index1 = i - width / 2; index1 <= i + width / 2; ++index1)
            {
                for (int index2 = j - height; index2 <= j; ++index2)
                {
                    try
                    {
                        Main.tile[index1, index2].active = true;
                        Main.tile[index1, index2].type = (byte)76;
                        Main.tile[index1, index2].liquid = (byte)0;
                        Main.tile[index1, index2].lava = false;
                    }
                    catch
                    {
                    }
                }
            }
            for (int index1 = i - width / 2 + 1; index1 <= i + width / 2 - 1; ++index1)
            {
                for (int index2 = j - height + 1; index2 <= j - 1; ++index2)
                {
                    try
                    {
                        Main.tile[index1, index2].active = false;
                        Main.tile[index1, index2].wall = (byte)13;
                        Main.tile[index1, index2].liquid = (byte)0;
                        Main.tile[index1, index2].lava = false;
                    }
                    catch
                    {
                    }
                }
            }
        }

        public static void MakeDungeon(int x, int y, int tileType = 41, int wallType = 7)
        {
            int num1 = WorldGen.genRand.Next(3);
            int num2 = WorldGen.genRand.Next(3);
            if (num1 == 1)
                tileType = 43;
            else if (num1 == 2)
                tileType = 44;
            if (num2 == 1)
                wallType = 8;
            else if (num2 == 2)
                wallType = 9;
            WorldGen.numDDoors = 0;
            WorldGen.numDPlats = 0;
            WorldGen.numDRooms = 0;
            WorldGen.dungeonX = x;
            WorldGen.dungeonY = y;
            WorldGen.dMinX = x;
            WorldGen.dMaxX = x;
            WorldGen.dMinY = y;
            WorldGen.dMaxY = y;
            WorldGen.dxStrength1 = (double)WorldGen.genRand.Next(25, 30);
            WorldGen.dyStrength1 = (double)WorldGen.genRand.Next(20, 25);
            WorldGen.dxStrength2 = (double)WorldGen.genRand.Next(35, 50);
            WorldGen.dyStrength2 = (double)WorldGen.genRand.Next(10, 15);
            float num3 = (float)(Main.maxTilesX / 60);
            float num4 = num3 + (float)WorldGen.genRand.Next(0, (int)((double)num3 / 3.0));
            float num5 = num4;
            int num6 = 5;
            WorldGen.DungeonRoom(WorldGen.dungeonX, WorldGen.dungeonY, tileType, wallType);
            while ((double)num4 > 0.0)
            {
                if (WorldGen.dungeonX < WorldGen.dMinX)
                    WorldGen.dMinX = WorldGen.dungeonX;
                if (WorldGen.dungeonX > WorldGen.dMaxX)
                    WorldGen.dMaxX = WorldGen.dungeonX;
                if (WorldGen.dungeonY > WorldGen.dMaxY)
                    WorldGen.dMaxY = WorldGen.dungeonY;
                --num4;
                Main.statusText = "Creating dungeon: " + (object)(int)(((double)num5 - (double)num4) / (double)num5 * 60.0) + "%";
                if (num6 > 0)
                    --num6;
                if (num6 == 0 & WorldGen.genRand.Next(3) == 0)
                {
                    num6 = 5;
                    if (WorldGen.genRand.Next(2) == 0)
                    {
                        int num7 = WorldGen.dungeonX;
                        int num8 = WorldGen.dungeonY;
                        WorldGen.DungeonHalls(WorldGen.dungeonX, WorldGen.dungeonY, tileType, wallType, false);
                        if (WorldGen.genRand.Next(2) == 0)
                            WorldGen.DungeonHalls(WorldGen.dungeonX, WorldGen.dungeonY, tileType, wallType, false);
                        WorldGen.DungeonRoom(WorldGen.dungeonX, WorldGen.dungeonY, tileType, wallType);
                        WorldGen.dungeonX = num7;
                        WorldGen.dungeonY = num8;
                    }
                    else
                        WorldGen.DungeonRoom(WorldGen.dungeonX, WorldGen.dungeonY, tileType, wallType);
                }
                else
                    WorldGen.DungeonHalls(WorldGen.dungeonX, WorldGen.dungeonY, tileType, wallType, false);
            }
            WorldGen.DungeonRoom(WorldGen.dungeonX, WorldGen.dungeonY, tileType, wallType);
            int num9 = WorldGen.dRoomX[0];
            int num10 = WorldGen.dRoomY[0];
            for (int index = 0; index < WorldGen.numDRooms; ++index)
            {
                if (WorldGen.dRoomY[index] < num10)
                {
                    num9 = WorldGen.dRoomX[index];
                    num10 = WorldGen.dRoomY[index];
                }
            }
            WorldGen.dungeonX = num9;
            WorldGen.dungeonY = num10;
            WorldGen.dEnteranceX = num9;
            WorldGen.dSurface = false;
            int num11 = 5;
            while (!WorldGen.dSurface)
            {
                if (num11 > 0)
                    --num11;
                if (num11 == 0 & WorldGen.genRand.Next(5) == 0 && (double)WorldGen.dungeonY > Main.worldSurface + 50.0)
                {
                    num11 = 10;
                    int num7 = WorldGen.dungeonX;
                    int num8 = WorldGen.dungeonY;
                    WorldGen.DungeonHalls(WorldGen.dungeonX, WorldGen.dungeonY, tileType, wallType, true);
                    WorldGen.DungeonRoom(WorldGen.dungeonX, WorldGen.dungeonY, tileType, wallType);
                    WorldGen.dungeonX = num7;
                    WorldGen.dungeonY = num8;
                }
                WorldGen.DungeonStairs(WorldGen.dungeonX, WorldGen.dungeonY, tileType, wallType);
            }
            WorldGen.DungeonEnt(WorldGen.dungeonX, WorldGen.dungeonY, tileType, wallType);
            Main.statusText = "Creating dungeon: 65%";
            for (int index1 = 0; index1 < WorldGen.numDRooms; ++index1)
            {
                for (int index2 = WorldGen.dRoomL[index1]; index2 <= WorldGen.dRoomR[index1]; ++index2)
                {
                    if (!Main.tile[index2, WorldGen.dRoomT[index1] - 1].active)
                    {
                        WorldGen.DPlatX[WorldGen.numDPlats] = index2;
                        WorldGen.DPlatY[WorldGen.numDPlats] = WorldGen.dRoomT[index1] - 1;
                        ++WorldGen.numDPlats;
                        break;
                    }
                }
                for (int index2 = WorldGen.dRoomL[index1]; index2 <= WorldGen.dRoomR[index1]; ++index2)
                {
                    if (!Main.tile[index2, WorldGen.dRoomB[index1] + 1].active)
                    {
                        WorldGen.DPlatX[WorldGen.numDPlats] = index2;
                        WorldGen.DPlatY[WorldGen.numDPlats] = WorldGen.dRoomB[index1] + 1;
                        ++WorldGen.numDPlats;
                        break;
                    }
                }
                for (int index2 = WorldGen.dRoomT[index1]; index2 <= WorldGen.dRoomB[index1]; ++index2)
                {
                    if (!Main.tile[WorldGen.dRoomL[index1] - 1, index2].active)
                    {
                        WorldGen.DDoorX[WorldGen.numDDoors] = WorldGen.dRoomL[index1] - 1;
                        WorldGen.DDoorY[WorldGen.numDDoors] = index2;
                        WorldGen.DDoorPos[WorldGen.numDDoors] = -1;
                        ++WorldGen.numDDoors;
                        break;
                    }
                }
                for (int index2 = WorldGen.dRoomT[index1]; index2 <= WorldGen.dRoomB[index1]; ++index2)
                {
                    if (!Main.tile[WorldGen.dRoomR[index1] + 1, index2].active)
                    {
                        WorldGen.DDoorX[WorldGen.numDDoors] = WorldGen.dRoomR[index1] + 1;
                        WorldGen.DDoorY[WorldGen.numDDoors] = index2;
                        WorldGen.DDoorPos[WorldGen.numDDoors] = 1;
                        ++WorldGen.numDDoors;
                        break;
                    }
                }
            }
            Main.statusText = "Creating dungeon: 70%";
            int num12 = 0;
            int num13 = 1000;
            int num14 = 0;
            while (num14 < Main.maxTilesX / 125)
            {
                ++num12;
                int index1 = WorldGen.genRand.Next(WorldGen.dMinX, WorldGen.dMaxX);
                int index2 = WorldGen.genRand.Next((int)Main.worldSurface + 25, WorldGen.dMaxY);
                int num7 = index1;
                if ((int)Main.tile[index1, index2].wall == wallType && !Main.tile[index1, index2].active)
                {
                    int num8 = 1;
                    if (WorldGen.genRand.Next(2) == 0)
                        num8 = -1;
                    while (!Main.tile[index1, index2].active)
                        index2 += num8;
                    if (Main.tile[index1 - 1, index2].active && Main.tile[index1 + 1, index2].active && !Main.tile[index1 - 1, index2 - num8].active && !Main.tile[index1 + 1, index2 - num8].active)
                    {
                        ++num14;
                        for (int index3 = WorldGen.genRand.Next(5, 10); Main.tile[index1 - 1, index2].active && Main.tile[index1, index2 + num8].active && (Main.tile[index1, index2].active && !Main.tile[index1, index2 - num8].active) && index3 > 0; --index3)
                        {
                            Main.tile[index1, index2].type = (byte)48;
                            if (!Main.tile[index1 - 1, index2 - num8].active && !Main.tile[index1 + 1, index2 - num8].active)
                            {
                                Main.tile[index1, index2 - num8].type = (byte)48;
                                Main.tile[index1, index2 - num8].active = true;
                            }
                            --index1;
                        }
                        int num15 = WorldGen.genRand.Next(5, 10);
                        for (int index3 = num7 + 1; Main.tile[index3 + 1, index2].active && Main.tile[index3, index2 + num8].active && (Main.tile[index3, index2].active && !Main.tile[index3, index2 - num8].active) && num15 > 0; --num15)
                        {
                            Main.tile[index3, index2].type = (byte)48;
                            if (!Main.tile[index3 - 1, index2 - num8].active && !Main.tile[index3 + 1, index2 - num8].active)
                            {
                                Main.tile[index3, index2 - num8].type = (byte)48;
                                Main.tile[index3, index2 - num8].active = true;
                            }
                            ++index3;
                        }
                    }
                }
                if (num12 > num13)
                {
                    num12 = 0;
                    ++num14;
                }
            }
            int num16 = 0;
            int num17 = 1000;
            int num18 = 0;
            Main.statusText = "Creating dungeon: 75%";
            while (num18 < Main.maxTilesX / 125)
            {
                ++num16;
                int index1 = WorldGen.genRand.Next(WorldGen.dMinX, WorldGen.dMaxX);
                int index2 = WorldGen.genRand.Next((int)Main.worldSurface + 25, WorldGen.dMaxY);
                int num7 = index2;
                if ((int)Main.tile[index1, index2].wall == wallType && !Main.tile[index1, index2].active)
                {
                    int num8 = 1;
                    if (WorldGen.genRand.Next(2) == 0)
                        num8 = -1;
                    while (index1 > 5 && index1 < Main.maxTilesX - 5 && !Main.tile[index1, index2].active)
                        index1 += num8;
                    if (Main.tile[index1, index2 - 1].active && Main.tile[index1, index2 + 1].active && !Main.tile[index1 - num8, index2 - 1].active && !Main.tile[index1 - num8, index2 + 1].active)
                    {
                        ++num18;
                        for (int index3 = WorldGen.genRand.Next(5, 10); Main.tile[index1, index2 - 1].active && Main.tile[index1 + num8, index2].active && (Main.tile[index1, index2].active && !Main.tile[index1 - num8, index2].active) && index3 > 0; --index3)
                        {
                            Main.tile[index1, index2].type = (byte)48;
                            if (!Main.tile[index1 - num8, index2 - 1].active && !Main.tile[index1 - num8, index2 + 1].active)
                            {
                                Main.tile[index1 - num8, index2].type = (byte)48;
                                Main.tile[index1 - num8, index2].active = true;
                            }
                            --index2;
                        }
                        int num15 = WorldGen.genRand.Next(5, 10);
                        for (int index3 = num7 + 1; Main.tile[index1, index3 + 1].active && Main.tile[index1 + num8, index3].active && (Main.tile[index1, index3].active && !Main.tile[index1 - num8, index3].active) && num15 > 0; --num15)
                        {
                            Main.tile[index1, index3].type = (byte)48;
                            if (!Main.tile[index1 - num8, index3 - 1].active && !Main.tile[index1 - num8, index3 + 1].active)
                            {
                                Main.tile[index1 - num8, index3].type = (byte)48;
                                Main.tile[index1 - num8, index3].active = true;
                            }
                            ++index3;
                        }
                    }
                }
                if (num16 > num17)
                {
                    num16 = 0;
                    ++num18;
                }
            }
            Main.statusText = "Creating dungeon: 80%";
            for (int index1 = 0; index1 < WorldGen.numDDoors; ++index1)
            {
                int num7 = WorldGen.DDoorX[index1] - 10;
                int num8 = WorldGen.DDoorX[index1] + 10;
                int num15 = 100;
                int num19 = 0;
                for (int index2 = num7; index2 < num8; ++index2)
                {
                    bool flag1 = true;
                    int index3 = WorldGen.DDoorY[index1];
                    while (!Main.tile[index2, index3].active)
                        --index3;
                    if (!Main.tileDungeon[(int)Main.tile[index2, index3].type])
                        flag1 = false;
                    int num20 = index3;
                    int index4 = WorldGen.DDoorY[index1];
                    while (!Main.tile[index2, index4].active)
                        ++index4;
                    if (!Main.tileDungeon[(int)Main.tile[index2, index4].type])
                        flag1 = false;
                    int num21 = index4;
                    if (num21 - num20 >= 3)
                    {
                        int num22 = index2 - 20;
                        int num23 = index2 + 20;
                        int num24 = num21 - 10;
                        int num25 = num21 + 10;
                        for (int index5 = num22; index5 < num23; ++index5)
                        {
                            for (int index6 = num24; index6 < num25; ++index6)
                            {
                                if (Main.tile[index5, index6].active && (int)Main.tile[index5, index6].type == 10)
                                {
                                    flag1 = false;
                                    break;
                                }
                            }
                        }
                        if (flag1)
                        {
                            for (int index5 = num21 - 3; index5 < num21; ++index5)
                            {
                                for (int index6 = index2 - 3; index6 <= index2 + 3; ++index6)
                                {
                                    if (Main.tile[index6, index5].active)
                                    {
                                        flag1 = false;
                                        break;
                                    }
                                }
                            }
                        }
                        if (flag1 && num21 - num20 < 20)
                        {
                            bool flag2 = false;
                            if (WorldGen.DDoorPos[index1] == 0 && num21 - num20 < num15)
                                flag2 = true;
                            if (WorldGen.DDoorPos[index1] == -1 && index2 > num19)
                                flag2 = true;
                            if (WorldGen.DDoorPos[index1] == 1 && (index2 < num19 || num19 == 0))
                                flag2 = true;
                            if (flag2)
                            {
                                num19 = index2;
                                num15 = num21 - num20;
                            }
                        }
                    }
                }
                if (num15 < 20)
                {
                    int i = num19;
                    int index2 = WorldGen.DDoorY[index1];
                    int index3 = index2;
                    for (; !Main.tile[i, index2].active; ++index2)
                        Main.tile[i, index2].active = false;
                    while (!Main.tile[i, index3].active)
                        --index3;
                    int j = index2 - 1;
                    int num20 = index3 + 1;
                    for (int index4 = num20; index4 < j - 2; ++index4)
                    {
                        Main.tile[i, index4].active = true;
                        Main.tile[i, index4].type = (byte)tileType;
                    }
                    WorldGen.PlaceTile(i, j, 10, true, false, -1);
                    int index5 = i - 1;
                    int index6 = j - 3;
                    while (!Main.tile[index5, index6].active)
                        --index6;
                    int num21;
                    if (j - index6 < j - num20 + 5 && Main.tileDungeon[(int)Main.tile[index5, index6].type])
                    {
                        num21 = index6;
                        for (int index4 = j - 4 - WorldGen.genRand.Next(3); index4 > index6; --index4)
                        {
                            Main.tile[index5, index4].active = true;
                            Main.tile[index5, index4].type = (byte)tileType;
                        }
                    }
                    int index7 = index5 + 2;
                    int index8 = j - 3;
                    while (!Main.tile[index7, index8].active)
                        --index8;
                    if (j - index8 < j - num20 + 5 && Main.tileDungeon[(int)Main.tile[index7, index8].type])
                    {
                        num21 = index8;
                        for (int index4 = j - 4 - WorldGen.genRand.Next(3); index4 > index8; --index4)
                        {
                            Main.tile[index7, index4].active = true;
                            Main.tile[index7, index4].type = (byte)tileType;
                        }
                    }
                    int index9 = j + 1;
                    int num22 = index7 - 1;
                    Main.tile[num22 - 1, index9].active = true;
                    Main.tile[num22 - 1, index9].type = (byte)tileType;
                    Main.tile[num22 + 1, index9].active = true;
                    Main.tile[num22 + 1, index9].type = (byte)tileType;
                }
            }
            Main.statusText = "Creating dungeon: 85%";
            for (int index1 = 0; index1 < WorldGen.numDPlats; ++index1)
            {
                int index2 = WorldGen.DPlatX[index1];
                int num7 = WorldGen.DPlatY[index1];
                int num8 = Main.maxTilesX;
                int num15 = 10;
                for (int index3 = num7 - 5; index3 <= num7 + 5; ++index3)
                {
                    int index4 = index2;
                    int index5 = index2;
                    bool flag1 = false;
                    if (Main.tile[index4, index3].active)
                    {
                        flag1 = true;
                    }
                    else
                    {
                        while (!Main.tile[index4, index3].active)
                        {
                            --index4;
                            if (!Main.tileDungeon[(int)Main.tile[index4, index3].type])
                                flag1 = true;
                        }
                        while (!Main.tile[index5, index3].active)
                        {
                            ++index5;
                            if (!Main.tileDungeon[(int)Main.tile[index5, index3].type])
                                flag1 = true;
                        }
                    }
                    if (!flag1 && index5 - index4 <= num15)
                    {
                        bool flag2 = true;
                        int num19 = index2 - num15 / 2 - 2;
                        int num20 = index2 + num15 / 2 + 2;
                        int num21 = index3 - 5;
                        int num22 = index3 + 5;
                        for (int index6 = num19; index6 <= num20; ++index6)
                        {
                            for (int index7 = num21; index7 <= num22; ++index7)
                            {
                                if (Main.tile[index6, index7].active && (int)Main.tile[index6, index7].type == 19)
                                {
                                    flag2 = false;
                                    break;
                                }
                            }
                        }
                        for (int index6 = index3 + 3; index6 >= index3 - 5; --index6)
                        {
                            if (Main.tile[index2, index6].active)
                            {
                                flag2 = false;
                                break;
                            }
                        }
                        if (flag2)
                        {
                            num8 = index3;
                            break;
                        }
                    }
                }
                if (num8 > num7 - 10 && num8 < num7 + 10)
                {
                    int index3 = index2;
                    int index4 = num8;
                    int index5 = index2 + 1;
                    for (; !Main.tile[index3, index4].active; --index3)
                    {
                        Main.tile[index3, index4].active = true;
                        Main.tile[index3, index4].type = (byte)19;
                    }
                    for (; !Main.tile[index5, index4].active; ++index5)
                    {
                        Main.tile[index5, index4].active = true;
                        Main.tile[index5, index4].type = (byte)19;
                    }
                }
            }
            Main.statusText = "Creating dungeon: 90%";
            int num26 = 0;
            int num27 = 1000;
            int num28 = 0;
            while (num28 < Main.maxTilesX / 20)
            {
                ++num26;
                int index1 = WorldGen.genRand.Next(WorldGen.dMinX, WorldGen.dMaxX);
                int index2 = WorldGen.genRand.Next(WorldGen.dMinY, WorldGen.dMaxY);
                bool flag1 = true;
                if ((int)Main.tile[index1, index2].wall == wallType && !Main.tile[index1, index2].active)
                {
                    int num7 = 1;
                    if (WorldGen.genRand.Next(2) == 0)
                        num7 = -1;
                    while (flag1 && !Main.tile[index1, index2].active)
                    {
                        index1 -= num7;
                        if (index1 < 5 || index1 > Main.maxTilesX - 5)
                            flag1 = false;
                        else if (Main.tile[index1, index2].active && !Main.tileDungeon[(int)Main.tile[index1, index2].type])
                            flag1 = false;
                    }
                    if (flag1 && (Main.tile[index1, index2].active && Main.tileDungeon[(int)Main.tile[index1, index2].type] && (Main.tile[index1, index2 - 1].active && Main.tileDungeon[(int)Main.tile[index1, index2 - 1].type]) && Main.tile[index1, index2 + 1].active && Main.tileDungeon[(int)Main.tile[index1, index2 + 1].type]))
                    {
                        int i1 = index1 + num7;
                        for (int index3 = i1 - 3; index3 <= i1 + 3; ++index3)
                        {
                            for (int index4 = index2 - 3; index4 <= index2 + 3; ++index4)
                            {
                                if (Main.tile[index3, index4].active && (int)Main.tile[index3, index4].type == 19)
                                {
                                    flag1 = false;
                                    break;
                                }
                            }
                        }
                        if (flag1 && !Main.tile[i1, index2 - 1].active & !Main.tile[i1, index2 - 2].active & !Main.tile[i1, index2 - 3].active)
                        {
                            int index3 = i1;
                            int num8 = i1;
                            while (index3 > WorldGen.dMinX && index3 < WorldGen.dMaxX && (!Main.tile[index3, index2].active && !Main.tile[index3, index2 - 1].active) && !Main.tile[index3, index2 + 1].active)
                                index3 += num7;
                            int num15 = Math.Abs(i1 - index3);
                            bool flag2 = false;
                            if (WorldGen.genRand.Next(2) == 0)
                                flag2 = true;
                            if (num15 > 5)
                            {
                                for (int index4 = WorldGen.genRand.Next(1, 4); index4 > 0; --index4)
                                {
                                    Main.tile[i1, index2].active = true;
                                    Main.tile[i1, index2].type = (byte)19;
                                    if (flag2)
                                    {
                                        WorldGen.PlaceTile(i1, index2 - 1, 50, true, false, -1);
                                        if (WorldGen.genRand.Next(50) == 0 && (int)Main.tile[i1, index2 - 1].type == 50)
                                            Main.tile[i1, index2 - 1].frameX = (short)90;
                                    }
                                    i1 += num7;
                                }
                                num26 = 0;
                                ++num28;
                                if (!flag2 && WorldGen.genRand.Next(2) == 0)
                                {
                                    int i2 = num8;
                                    int j = index2 - 1;
                                    int type = WorldGen.genRand.Next(2);
                                    switch (type)
                                    {
                                        case 0:
                                            type = 13;
                                            break;
                                        case 1:
                                            type = 49;
                                            break;
                                    }
                                    WorldGen.PlaceTile(i2, j, type, true, false, -1);
                                    if ((int)Main.tile[i2, j].type == 13)
                                        Main.tile[i2, j].frameX = WorldGen.genRand.Next(2) != 0 ? (short)36 : (short)18;
                                }
                            }
                        }
                    }
                }
                if (num26 > num27)
                {
                    num26 = 0;
                    ++num28;
                }
            }
            Main.statusText = "Creating dungeon: 95%";
            for (int index = 0; index < WorldGen.numDRooms; ++index)
            {
                int num7 = 0;
                while (num7 < 1000)
                {
                    int num8 = (int)((double)WorldGen.dRoomSize[index] * 0.4);
                    int i = WorldGen.dRoomX[index] + WorldGen.genRand.Next(-num8, num8 + 1);
                    int j = WorldGen.dRoomY[index] + WorldGen.genRand.Next(-num8, num8 + 1);
                    int contain = 0;
                    switch (index)
                    {
                        case 0:
                            contain = 113;
                            break;
                        case 1:
                            contain = 155;
                            break;
                        case 2:
                            contain = 156;
                            break;
                        case 3:
                            contain = 157;
                            break;
                        case 4:
                            contain = 163;
                            break;
                        case 5:
                            contain = 164;
                            break;
                    }
                    if (contain == 0 && WorldGen.genRand.Next(2) == 0)
                    {
                        num7 = 1000;
                    }
                    else
                    {
                        if (WorldGen.AddBuriedChest(i, j, contain))
                            num7 += 1000;
                        ++num7;
                    }
                }
            }
            WorldGen.dMinX -= 25;
            WorldGen.dMaxX += 25;
            WorldGen.dMinY -= 25;
            WorldGen.dMaxY += 25;
            if (WorldGen.dMinX < 0)
                WorldGen.dMinX = 0;
            if (WorldGen.dMaxX > Main.maxTilesX)
                WorldGen.dMaxX = Main.maxTilesX;
            if (WorldGen.dMinY < 0)
                WorldGen.dMinY = 0;
            if (WorldGen.dMaxY > Main.maxTilesY)
                WorldGen.dMaxY = Main.maxTilesY;
            int num29 = 0;
            int num30 = 1000;
            int num31 = 0;
            while (num31 < Main.maxTilesX / 20)
            {
                ++num29;
                int x1 = WorldGen.genRand.Next(WorldGen.dMinX, WorldGen.dMaxX);
                int index1 = WorldGen.genRand.Next(WorldGen.dMinY, WorldGen.dMaxY);
                if ((int)Main.tile[x1, index1].wall == wallType)
                {
                    for (int y1 = index1; y1 > WorldGen.dMinY; --y1)
                    {
                        if (Main.tile[x1, y1 - 1].active && (int)Main.tile[x1, y1 - 1].type == tileType)
                        {
                            bool flag = false;
                            for (int index2 = x1 - 15; index2 < x1 + 15; ++index2)
                            {
                                for (int index3 = y1 - 15; index3 < y1 + 15; ++index3)
                                {
                                    if (index2 > 0 && index2 < Main.maxTilesX && index3 > 0 && index3 < Main.maxTilesY && (int)Main.tile[index2, index3].type == 42)
                                    {
                                        flag = true;
                                        break;
                                    }
                                }
                            }
                            if (Main.tile[x1 - 1, y1].active || Main.tile[x1 + 1, y1].active || (Main.tile[x1 - 1, y1 + 1].active || Main.tile[x1 + 1, y1 + 1].active) || Main.tile[x1, y1 + 2].active)
                                flag = true;
                            if (!flag)
                            {
                                WorldGen.Place1x2Top(x1, y1, 42);
                                if ((int)Main.tile[x1, y1].type == 42)
                                {
                                    num29 = 0;
                                    ++num31;
                                }
                                break;
                            }
                            else
                                break;
                        }
                    }
                }
                if (num29 > num30)
                {
                    ++num31;
                    num29 = 0;
                }
            }
        }

        public static void DungeonStairs(int i, int j, int tileType, int wallType)
        {
            Vector2 vector2_1 = new Vector2();
            double num1 = (double)WorldGen.genRand.Next(5, 9);
            Vector2 vector2_2;
            vector2_2.X = (float)i;
            vector2_2.Y = (float)j;
            int num2 = WorldGen.genRand.Next(10, 30);
            int num3 = i <= WorldGen.dEnteranceX ? 1 : -1;
            vector2_1.Y = -1f;
            vector2_1.X = (float)num3;
            if (WorldGen.genRand.Next(3) == 0)
                vector2_1.X *= 0.5f;
            else if (WorldGen.genRand.Next(3) == 0)
                vector2_1.Y *= 2f;
            while (num2 > 0)
            {
                --num2;
                int num4 = (int)((double)vector2_2.X - num1 - 4.0 - (double)WorldGen.genRand.Next(6));
                int num5 = (int)((double)vector2_2.X + num1 + 4.0 + (double)WorldGen.genRand.Next(6));
                int num6 = (int)((double)vector2_2.Y - num1 - 4.0);
                int num7 = (int)((double)vector2_2.Y + num1 + 4.0 + (double)WorldGen.genRand.Next(6));
                if (num4 < 0)
                    num4 = 0;
                if (num5 > Main.maxTilesX)
                    num5 = Main.maxTilesX;
                if (num6 < 0)
                    num6 = 0;
                if (num7 > Main.maxTilesY)
                    num7 = Main.maxTilesY;
                int num8 = 1;
                if ((double)vector2_2.X > (double)(Main.maxTilesX / 2))
                    num8 = -1;
                int i1 = (int)((double)vector2_2.X + WorldGen.dxStrength1 * 0.600000023841858 * (double)num8 + WorldGen.dxStrength2 * (double)num8);
                int num9 = (int)(WorldGen.dyStrength2 * 0.5);
                if ((double)vector2_2.Y < Main.worldSurface - 5.0 && (int)Main.tile[i1, (int)((double)vector2_2.Y - num1 - 6.0 + (double)num9)].wall == 0 && (int)Main.tile[i1, (int)((double)vector2_2.Y - num1 - 7.0 + (double)num9)].wall == 0 && (int)Main.tile[i1, (int)((double)vector2_2.Y - num1 - 8.0 + (double)num9)].wall == 0)
                {
                    WorldGen.dSurface = true;
                    WorldGen.TileRunner(i1, (int)((double)vector2_2.Y - num1 - 6.0 + (double)num9), (double)WorldGen.genRand.Next(25, 35), WorldGen.genRand.Next(10, 20), -1, false, 0.0f, -1f, false, true);
                }
                for (int index1 = num4; index1 < num5; ++index1)
                {
                    for (int index2 = num6; index2 < num7; ++index2)
                    {
                        Main.tile[index1, index2].liquid = (byte)0;
                        if ((int)Main.tile[index1, index2].wall != wallType)
                        {
                            Main.tile[index1, index2].wall = (byte)0;
                            Main.tile[index1, index2].active = true;
                            Main.tile[index1, index2].type = (byte)tileType;
                        }
                    }
                }
                for (int i2 = num4 + 1; i2 < num5 - 1; ++i2)
                {
                    for (int j1 = num6 + 1; j1 < num7 - 1; ++j1)
                        WorldGen.PlaceWall(i2, j1, wallType, true);
                }
                int num10 = 0;
                if (WorldGen.genRand.Next((int)num1) == 0)
                    num10 = WorldGen.genRand.Next(1, 3);
                int num11 = (int)((double)vector2_2.X - num1 * 0.5 - (double)num10);
                int num12 = (int)((double)vector2_2.X + num1 * 0.5 + (double)num10);
                int num13 = (int)((double)vector2_2.Y - num1 * 0.5 - (double)num10);
                int num14 = (int)((double)vector2_2.Y + num1 * 0.5 + (double)num10);
                if (num11 < 0)
                    num11 = 0;
                if (num12 > Main.maxTilesX)
                    num12 = Main.maxTilesX;
                if (num13 < 0)
                    num13 = 0;
                if (num14 > Main.maxTilesY)
                    num14 = Main.maxTilesY;
                for (int i2 = num11; i2 < num12; ++i2)
                {
                    for (int j1 = num13; j1 < num14; ++j1)
                    {
                        Main.tile[i2, j1].active = false;
                        WorldGen.PlaceWall(i2, j1, wallType, true);
                    }
                }
                if (WorldGen.dSurface)
                    num2 = 0;
                vector2_2 += vector2_1;
            }
            WorldGen.dungeonX = (int)vector2_2.X;
            WorldGen.dungeonY = (int)vector2_2.Y;
        }

        public static void DungeonHalls(int i, int j, int tileType, int wallType, bool forceX = false)
        {
            Vector2 vector2_1 = new Vector2();
            double num1 = (double)WorldGen.genRand.Next(4, 6);
            Vector2 vector2_2 = new Vector2();
            Vector2 vector2_3 = new Vector2();
            Vector2 vector2_4;
            vector2_4.X = (float)i;
            vector2_4.Y = (float)j;
            int num2 = WorldGen.genRand.Next(35, 80);
            if (forceX)
            {
                num2 += 20;
                WorldGen.lastDungeonHall = new Vector2();
            }
            else if (WorldGen.genRand.Next(5) == 0)
            {
                num1 *= 2.0;
                num2 /= 2;
            }
            bool flag1 = false;
            while (!flag1)
            {
                int num3 = WorldGen.genRand.Next(2) != 0 ? 1 : -1;
                bool flag2 = false;
                if (WorldGen.genRand.Next(2) == 0)
                    flag2 = true;
                if (forceX)
                    flag2 = true;
                if (flag2)
                {
                    vector2_2.Y = 0.0f;
                    vector2_2.X = (float)num3;
                    vector2_3.Y = 0.0f;
                    vector2_3.X = (float)-num3;
                    vector2_1.Y = 0.0f;
                    vector2_1.X = (float)num3;
                    if (WorldGen.genRand.Next(3) == 0)
                        vector2_1.Y = WorldGen.genRand.Next(2) != 0 ? 0.2f : -0.2f;
                }
                else
                {
                    ++num1;
                    vector2_1.Y = (float)num3;
                    vector2_1.X = 0.0f;
                    vector2_2.X = 0.0f;
                    vector2_2.Y = (float)num3;
                    vector2_3.X = 0.0f;
                    vector2_3.Y = (float)-num3;
                    if (WorldGen.genRand.Next(2) == 0)
                        vector2_1.X = WorldGen.genRand.Next(2) != 0 ? -0.3f : 0.3f;
                    else
                        num2 /= 2;
                }
                if (WorldGen.lastDungeonHall != vector2_3)
                    flag1 = true;
            }
            if (!forceX)
            {
                if ((double)vector2_4.X > (double)(WorldGen.lastMaxTilesX - 200))
                {
                    int num3 = -1;
                    vector2_2.Y = 0.0f;
                    vector2_2.X = (float)num3;
                    vector2_1.Y = 0.0f;
                    vector2_1.X = (float)num3;
                    if (WorldGen.genRand.Next(3) == 0)
                        vector2_1.Y = WorldGen.genRand.Next(2) != 0 ? 0.2f : -0.2f;
                }
                else if ((double)vector2_4.X < 200.0)
                {
                    int num3 = 1;
                    vector2_2.Y = 0.0f;
                    vector2_2.X = (float)num3;
                    vector2_1.Y = 0.0f;
                    vector2_1.X = (float)num3;
                    if (WorldGen.genRand.Next(3) == 0)
                        vector2_1.Y = WorldGen.genRand.Next(2) != 0 ? 0.2f : -0.2f;
                }
                else if ((double)vector2_4.Y > (double)(WorldGen.lastMaxTilesY - 300))
                {
                    int num3 = -1;
                    ++num1;
                    vector2_1.Y = (float)num3;
                    vector2_1.X = 0.0f;
                    vector2_2.X = 0.0f;
                    vector2_2.Y = (float)num3;
                    if (WorldGen.genRand.Next(2) == 0)
                        vector2_1.X = WorldGen.genRand.Next(2) != 0 ? -0.3f : 0.3f;
                }
                else if ((double)vector2_4.Y < Main.rockLayer)
                {
                    int num3 = 1;
                    ++num1;
                    vector2_1.Y = (float)num3;
                    vector2_1.X = 0.0f;
                    vector2_2.X = 0.0f;
                    vector2_2.Y = (float)num3;
                    if (WorldGen.genRand.Next(2) == 0)
                        vector2_1.X = WorldGen.genRand.Next(2) != 0 ? -0.3f : 0.3f;
                }
                else if ((double)vector2_4.X < (double)(Main.maxTilesX / 2) && (double)vector2_4.X > (double)Main.maxTilesX * 0.25)
                {
                    int num3 = -1;
                    vector2_2.Y = 0.0f;
                    vector2_2.X = (float)num3;
                    vector2_1.Y = 0.0f;
                    vector2_1.X = (float)num3;
                    if (WorldGen.genRand.Next(3) == 0)
                        vector2_1.Y = WorldGen.genRand.Next(2) != 0 ? 0.2f : -0.2f;
                }
                else if ((double)vector2_4.X > (double)(Main.maxTilesX / 2) && (double)vector2_4.X < (double)Main.maxTilesX * 0.75)
                {
                    int num3 = 1;
                    vector2_2.Y = 0.0f;
                    vector2_2.X = (float)num3;
                    vector2_1.Y = 0.0f;
                    vector2_1.X = (float)num3;
                    if (WorldGen.genRand.Next(3) == 0)
                        vector2_1.Y = WorldGen.genRand.Next(2) != 0 ? 0.2f : -0.2f;
                }
            }
            if ((double)vector2_2.Y == 0.0)
            {
                WorldGen.DDoorX[WorldGen.numDDoors] = (int)vector2_4.X;
                WorldGen.DDoorY[WorldGen.numDDoors] = (int)vector2_4.Y;
                WorldGen.DDoorPos[WorldGen.numDDoors] = 0;
                ++WorldGen.numDDoors;
            }
            else
            {
                WorldGen.DPlatX[WorldGen.numDPlats] = (int)vector2_4.X;
                WorldGen.DPlatY[WorldGen.numDPlats] = (int)vector2_4.Y;
                ++WorldGen.numDPlats;
            }
            WorldGen.lastDungeonHall = vector2_2;
            while (num2 > 0)
            {
                if ((double)vector2_2.X > 0.0 && (double)vector2_4.X > (double)(Main.maxTilesX - 100))
                    num2 = 0;
                else if ((double)vector2_2.X < 0.0 && (double)vector2_4.X < 100.0)
                    num2 = 0;
                else if ((double)vector2_2.Y > 0.0 && (double)vector2_4.Y > (double)(Main.maxTilesY - 100))
                    num2 = 0;
                else if ((double)vector2_2.Y < 0.0 && (double)vector2_4.Y < Main.rockLayer + 50.0)
                    num2 = 0;
                --num2;
                int num3 = (int)((double)vector2_4.X - num1 - 4.0 - (double)WorldGen.genRand.Next(6));
                int num4 = (int)((double)vector2_4.X + num1 + 4.0 + (double)WorldGen.genRand.Next(6));
                int num5 = (int)((double)vector2_4.Y - num1 - 4.0 - (double)WorldGen.genRand.Next(6));
                int num6 = (int)((double)vector2_4.Y + num1 + 4.0 + (double)WorldGen.genRand.Next(6));
                if (num3 < 0)
                    num3 = 0;
                if (num4 > Main.maxTilesX)
                    num4 = Main.maxTilesX;
                if (num5 < 0)
                    num5 = 0;
                if (num6 > Main.maxTilesY)
                    num6 = Main.maxTilesY;
                for (int index1 = num3; index1 < num4; ++index1)
                {
                    for (int index2 = num5; index2 < num6; ++index2)
                    {
                        Main.tile[index1, index2].liquid = (byte)0;
                        if ((int)Main.tile[index1, index2].wall == 0)
                        {
                            Main.tile[index1, index2].active = true;
                            Main.tile[index1, index2].type = (byte)tileType;
                        }
                    }
                }
                for (int i1 = num3 + 1; i1 < num4 - 1; ++i1)
                {
                    for (int j1 = num5 + 1; j1 < num6 - 1; ++j1)
                        WorldGen.PlaceWall(i1, j1, wallType, true);
                }
                int num7 = 0;
                if ((double)vector2_1.Y == 0.0 && WorldGen.genRand.Next((int)num1 + 1) == 0)
                    num7 = WorldGen.genRand.Next(1, 3);
                else if ((double)vector2_1.X == 0.0 && WorldGen.genRand.Next((int)num1 - 1) == 0)
                    num7 = WorldGen.genRand.Next(1, 3);
                else if (WorldGen.genRand.Next((int)num1 * 3) == 0)
                    num7 = WorldGen.genRand.Next(1, 3);
                int num8 = (int)((double)vector2_4.X - num1 * 0.5 - (double)num7);
                int num9 = (int)((double)vector2_4.X + num1 * 0.5 + (double)num7);
                int num10 = (int)((double)vector2_4.Y - num1 * 0.5 - (double)num7);
                int num11 = (int)((double)vector2_4.Y + num1 * 0.5 + (double)num7);
                if (num8 < 0)
                    num8 = 0;
                if (num9 > Main.maxTilesX)
                    num9 = Main.maxTilesX;
                if (num10 < 0)
                    num10 = 0;
                if (num11 > Main.maxTilesY)
                    num11 = Main.maxTilesY;
                for (int index1 = num8; index1 < num9; ++index1)
                {
                    for (int index2 = num10; index2 < num11; ++index2)
                    {
                        Main.tile[index1, index2].active = false;
                        Main.tile[index1, index2].wall = (byte)wallType;
                    }
                }
                vector2_4 += vector2_1;
            }
            WorldGen.dungeonX = (int)vector2_4.X;
            WorldGen.dungeonY = (int)vector2_4.Y;
            if ((double)vector2_2.Y == 0.0)
            {
                WorldGen.DDoorX[WorldGen.numDDoors] = (int)vector2_4.X;
                WorldGen.DDoorY[WorldGen.numDDoors] = (int)vector2_4.Y;
                WorldGen.DDoorPos[WorldGen.numDDoors] = 0;
                ++WorldGen.numDDoors;
            }
            else
            {
                WorldGen.DPlatX[WorldGen.numDPlats] = (int)vector2_4.X;
                WorldGen.DPlatY[WorldGen.numDPlats] = (int)vector2_4.Y;
                ++WorldGen.numDPlats;
            }
        }

        public static void DungeonRoom(int i, int j, int tileType, int wallType)
        {
            double num1 = (double)WorldGen.genRand.Next(15, 30);
            Vector2 vector2_1;
            vector2_1.X = (float)WorldGen.genRand.Next(-10, 11) * 0.1f;
            vector2_1.Y = (float)WorldGen.genRand.Next(-10, 11) * 0.1f;
            Vector2 vector2_2;
            vector2_2.X = (float)i;
            vector2_2.Y = (float)j - (float)num1 / 2f;
            int num2 = WorldGen.genRand.Next(10, 20);
            double num3 = (double)vector2_2.X;
            double num4 = (double)vector2_2.X;
            double num5 = (double)vector2_2.Y;
            double num6 = (double)vector2_2.Y;
            while (num2 > 0)
            {
                --num2;
                int num7 = (int)((double)vector2_2.X - num1 * 0.800000011920929 - 5.0);
                int num8 = (int)((double)vector2_2.X + num1 * 0.800000011920929 + 5.0);
                int num9 = (int)((double)vector2_2.Y - num1 * 0.800000011920929 - 5.0);
                int num10 = (int)((double)vector2_2.Y + num1 * 0.800000011920929 + 5.0);
                if (num7 < 0)
                    num7 = 0;
                if (num8 > Main.maxTilesX)
                    num8 = Main.maxTilesX;
                if (num9 < 0)
                    num9 = 0;
                if (num10 > Main.maxTilesY)
                    num10 = Main.maxTilesY;
                for (int index1 = num7; index1 < num8; ++index1)
                {
                    for (int index2 = num9; index2 < num10; ++index2)
                    {
                        Main.tile[index1, index2].liquid = (byte)0;
                        if ((int)Main.tile[index1, index2].wall == 0)
                        {
                            Main.tile[index1, index2].active = true;
                            Main.tile[index1, index2].type = (byte)tileType;
                        }
                    }
                }
                for (int i1 = num7 + 1; i1 < num8 - 1; ++i1)
                {
                    for (int j1 = num9 + 1; j1 < num10 - 1; ++j1)
                        WorldGen.PlaceWall(i1, j1, wallType, true);
                }
                int num11 = (int)((double)vector2_2.X - num1 * 0.5);
                int num12 = (int)((double)vector2_2.X + num1 * 0.5);
                int num13 = (int)((double)vector2_2.Y - num1 * 0.5);
                int num14 = (int)((double)vector2_2.Y + num1 * 0.5);
                if (num11 < 0)
                    num11 = 0;
                if (num12 > Main.maxTilesX)
                    num12 = Main.maxTilesX;
                if (num13 < 0)
                    num13 = 0;
                if (num14 > Main.maxTilesY)
                    num14 = Main.maxTilesY;
                if ((double)num11 < num3)
                    num3 = (double)num11;
                if ((double)num12 > num4)
                    num4 = (double)num12;
                if ((double)num13 < num5)
                    num5 = (double)num13;
                if ((double)num14 > num6)
                    num6 = (double)num14;
                for (int index1 = num11; index1 < num12; ++index1)
                {
                    for (int index2 = num13; index2 < num14; ++index2)
                    {
                        Main.tile[index1, index2].active = false;
                        Main.tile[index1, index2].wall = (byte)wallType;
                    }
                }
                vector2_2 += vector2_1;
                vector2_1.X += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                vector2_1.Y += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                if ((double)vector2_1.X > 1.0)
                    vector2_1.X = 1f;
                if ((double)vector2_1.X < -1.0)
                    vector2_1.X = -1f;
                if ((double)vector2_1.Y > 1.0)
                    vector2_1.Y = 1f;
                if ((double)vector2_1.Y < -1.0)
                    vector2_1.Y = -1f;
            }
            WorldGen.dRoomX[WorldGen.numDRooms] = (int)vector2_2.X;
            WorldGen.dRoomY[WorldGen.numDRooms] = (int)vector2_2.Y;
            WorldGen.dRoomSize[WorldGen.numDRooms] = (int)num1;
            WorldGen.dRoomL[WorldGen.numDRooms] = (int)num3;
            WorldGen.dRoomR[WorldGen.numDRooms] = (int)num4;
            WorldGen.dRoomT[WorldGen.numDRooms] = (int)num5;
            WorldGen.dRoomB[WorldGen.numDRooms] = (int)num6;
            WorldGen.dRoomTreasure[WorldGen.numDRooms] = false;
            ++WorldGen.numDRooms;
        }

        public static void DungeonEnt(int i, int j, int tileType, int wallType)
        {
            double num1 = WorldGen.dxStrength1;
            double num2 = WorldGen.dyStrength1;
            Vector2 vector2;
            vector2.X = (float)i;
            vector2.Y = (float)j - (float)num2 / 2f;
            WorldGen.dMinY = (int)vector2.Y;
            int num3 = 1;
            if (i > Main.maxTilesX / 2)
                num3 = -1;
            int num4 = (int)((double)vector2.X - num1 * 0.600000023841858 - (double)WorldGen.genRand.Next(2, 5));
            int num5 = (int)((double)vector2.X + num1 * 0.600000023841858 + (double)WorldGen.genRand.Next(2, 5));
            int num6 = (int)((double)vector2.Y - num2 * 0.600000023841858 - (double)WorldGen.genRand.Next(2, 5));
            int num7 = (int)((double)vector2.Y + num2 * 0.600000023841858 + (double)WorldGen.genRand.Next(8, 16));
            if (num4 < 0)
                num4 = 0;
            if (num5 > Main.maxTilesX)
                num5 = Main.maxTilesX;
            if (num6 < 0)
                num6 = 0;
            if (num7 > Main.maxTilesY)
                num7 = Main.maxTilesY;
            for (int i1 = num4; i1 < num5; ++i1)
            {
                for (int j1 = num6; j1 < num7; ++j1)
                {
                    Main.tile[i1, j1].liquid = (byte)0;
                    if ((int)Main.tile[i1, j1].wall != wallType)
                    {
                        Main.tile[i1, j1].wall = (byte)0;
                        if (i1 > num4 + 1 && i1 < num5 - 2 && j1 > num6 + 1 && j1 < num7 - 2)
                            WorldGen.PlaceWall(i1, j1, wallType, true);
                        Main.tile[i1, j1].active = true;
                        Main.tile[i1, j1].type = (byte)tileType;
                    }
                }
            }
            int num8 = num4;
            int num9 = num4 + 5 + WorldGen.genRand.Next(4);
            int num10 = num6 - 3 - WorldGen.genRand.Next(3);
            int num11 = num6;
            for (int index1 = num8; index1 < num9; ++index1)
            {
                for (int index2 = num10; index2 < num11; ++index2)
                {
                    if ((int)Main.tile[index1, index2].wall != wallType)
                    {
                        Main.tile[index1, index2].active = true;
                        Main.tile[index1, index2].type = (byte)tileType;
                    }
                }
            }
            int num12 = num5 - 5 - WorldGen.genRand.Next(4);
            int num13 = num5;
            int num14 = num6 - 3 - WorldGen.genRand.Next(3);
            int num15 = num6;
            for (int index1 = num12; index1 < num13; ++index1)
            {
                for (int index2 = num14; index2 < num15; ++index2)
                {
                    if ((int)Main.tile[index1, index2].wall != wallType)
                    {
                        Main.tile[index1, index2].active = true;
                        Main.tile[index1, index2].type = (byte)tileType;
                    }
                }
            }
            int num16 = 1 + WorldGen.genRand.Next(2);
            int num17 = 2 + WorldGen.genRand.Next(4);
            int num18 = 0;
            for (int index1 = num4; index1 < num5; ++index1)
            {
                for (int index2 = num6 - num16; index2 < num6; ++index2)
                {
                    if ((int)Main.tile[index1, index2].wall != wallType)
                    {
                        Main.tile[index1, index2].active = true;
                        Main.tile[index1, index2].type = (byte)tileType;
                    }
                }
                ++num18;
                if (num18 >= num17)
                {
                    index1 += num17;
                    num18 = 0;
                }
            }
            for (int i1 = num4; i1 < num5; ++i1)
            {
                for (int j1 = num7; j1 < num7 + 100; ++j1)
                    WorldGen.PlaceWall(i1, j1, 2, true);
            }
            int num19 = (int)((double)vector2.X - num1 * 0.600000023841858);
            int num20 = (int)((double)vector2.X + num1 * 0.600000023841858);
            int num21 = (int)((double)vector2.Y - num2 * 0.600000023841858);
            int num22 = (int)((double)vector2.Y + num2 * 0.600000023841858);
            if (num19 < 0)
                num19 = 0;
            if (num20 > Main.maxTilesX)
                num20 = Main.maxTilesX;
            if (num21 < 0)
                num21 = 0;
            if (num22 > Main.maxTilesY)
                num22 = Main.maxTilesY;
            for (int i1 = num19; i1 < num20; ++i1)
            {
                for (int j1 = num21; j1 < num22; ++j1)
                    WorldGen.PlaceWall(i1, j1, wallType, true);
            }
            int num23 = (int)((double)vector2.X - num1 * 0.6 - 1.0);
            int num24 = (int)((double)vector2.X + num1 * 0.6 + 1.0);
            int num25 = (int)((double)vector2.Y - num2 * 0.6 - 1.0);
            int num26 = (int)((double)vector2.Y + num2 * 0.6 + 1.0);
            if (num23 < 0)
                num23 = 0;
            if (num24 > Main.maxTilesX)
                num24 = Main.maxTilesX;
            if (num25 < 0)
                num25 = 0;
            if (num26 > Main.maxTilesY)
                num26 = Main.maxTilesY;
            for (int index1 = num23; index1 < num24; ++index1)
            {
                for (int index2 = num25; index2 < num26; ++index2)
                    Main.tile[index1, index2].wall = (byte)wallType;
            }
            int num27 = (int)((double)vector2.X - num1 * 0.5);
            int num28 = (int)((double)vector2.X + num1 * 0.5);
            int num29 = (int)((double)vector2.Y - num2 * 0.5);
            int num30 = (int)((double)vector2.Y + num2 * 0.5);
            if (num27 < 0)
                num27 = 0;
            if (num28 > Main.maxTilesX)
                num28 = Main.maxTilesX;
            if (num29 < 0)
                num29 = 0;
            if (num30 > Main.maxTilesY)
                num30 = Main.maxTilesY;
            for (int index1 = num27; index1 < num28; ++index1)
            {
                for (int index2 = num29; index2 < num30; ++index2)
                {
                    Main.tile[index1, index2].active = false;
                    Main.tile[index1, index2].wall = (byte)wallType;
                }
            }
            WorldGen.DPlatX[WorldGen.numDPlats] = (int)vector2.X;
            WorldGen.DPlatY[WorldGen.numDPlats] = num30;
            ++WorldGen.numDPlats;
            vector2.X += (float)(num1 * 0.600000023841858) * (float)num3;
            vector2.Y += (float)num2 * 0.5f;
            double num31 = WorldGen.dxStrength2;
            double num32 = WorldGen.dyStrength2;
            vector2.X += (float)(num31 * 0.550000011920929) * (float)num3;
            vector2.Y -= (float)num32 * 0.5f;
            int num33 = (int)((double)vector2.X - num31 * 0.600000023841858 - (double)WorldGen.genRand.Next(1, 3));
            int num34 = (int)((double)vector2.X + num31 * 0.600000023841858 + (double)WorldGen.genRand.Next(1, 3));
            int num35 = (int)((double)vector2.Y - num32 * 0.600000023841858 - (double)WorldGen.genRand.Next(1, 3));
            int num36 = (int)((double)vector2.Y + num32 * 0.600000023841858 + (double)WorldGen.genRand.Next(6, 16));
            if (num33 < 0)
                num33 = 0;
            if (num34 > Main.maxTilesX)
                num34 = Main.maxTilesX;
            if (num35 < 0)
                num35 = 0;
            if (num36 > Main.maxTilesY)
                num36 = Main.maxTilesY;
            for (int index1 = num33; index1 < num34; ++index1)
            {
                for (int index2 = num35; index2 < num36; ++index2)
                {
                    if ((int)Main.tile[index1, index2].wall != wallType)
                    {
                        bool flag = true;
                        if (num3 < 0)
                        {
                            if ((double)index1 < (double)vector2.X - num31 * 0.5)
                                flag = false;
                        }
                        else if ((double)index1 > (double)vector2.X + num31 * 0.5 - 1.0)
                            flag = false;
                        if (flag)
                        {
                            Main.tile[index1, index2].wall = (byte)0;
                            Main.tile[index1, index2].active = true;
                            Main.tile[index1, index2].type = (byte)tileType;
                        }
                    }
                }
            }
            for (int i1 = num33; i1 < num34; ++i1)
            {
                for (int j1 = num36; j1 < num36 + 100; ++j1)
                    WorldGen.PlaceWall(i1, j1, 2, true);
            }
            int num37 = (int)((double)vector2.X - num31 * 0.5);
            int num38 = (int)((double)vector2.X + num31 * 0.5);
            int num39 = num37;
            if (num3 < 0)
                ++num39;
            int num40 = num39 + 5 + WorldGen.genRand.Next(4);
            int num41 = num35 - 3 - WorldGen.genRand.Next(3);
            int num42 = num35;
            for (int index1 = num39; index1 < num40; ++index1)
            {
                for (int index2 = num41; index2 < num42; ++index2)
                {
                    if ((int)Main.tile[index1, index2].wall != wallType)
                    {
                        Main.tile[index1, index2].active = true;
                        Main.tile[index1, index2].type = (byte)tileType;
                    }
                }
            }
            int num43 = num38 - 5 - WorldGen.genRand.Next(4);
            int num44 = num38;
            int num45 = num35 - 3 - WorldGen.genRand.Next(3);
            int num46 = num35;
            for (int index1 = num43; index1 < num44; ++index1)
            {
                for (int index2 = num45; index2 < num46; ++index2)
                {
                    if ((int)Main.tile[index1, index2].wall != wallType)
                    {
                        Main.tile[index1, index2].active = true;
                        Main.tile[index1, index2].type = (byte)tileType;
                    }
                }
            }
            int num47 = 1 + WorldGen.genRand.Next(2);
            int num48 = 2 + WorldGen.genRand.Next(4);
            int num49 = 0;
            if (num3 < 0)
                ++num38;
            for (int index1 = num37 + 1; index1 < num38 - 1; ++index1)
            {
                for (int index2 = num35 - num47; index2 < num35; ++index2)
                {
                    if ((int)Main.tile[index1, index2].wall != wallType)
                    {
                        Main.tile[index1, index2].active = true;
                        Main.tile[index1, index2].type = (byte)tileType;
                    }
                }
                ++num49;
                if (num49 >= num48)
                {
                    index1 += num48;
                    num49 = 0;
                }
            }
            int num50 = (int)((double)vector2.X - num31 * 0.6);
            int num51 = (int)((double)vector2.X + num31 * 0.6);
            int num52 = (int)((double)vector2.Y - num32 * 0.6);
            int num53 = (int)((double)vector2.Y + num32 * 0.6);
            if (num50 < 0)
                num50 = 0;
            if (num51 > Main.maxTilesX)
                num51 = Main.maxTilesX;
            if (num52 < 0)
                num52 = 0;
            if (num53 > Main.maxTilesY)
                num53 = Main.maxTilesY;
            for (int index1 = num50; index1 < num51; ++index1)
            {
                for (int index2 = num52; index2 < num53; ++index2)
                    Main.tile[index1, index2].wall = (byte)0;
            }
            int num54 = (int)((double)vector2.X - num31 * 0.5);
            int num55 = (int)((double)vector2.X + num31 * 0.5);
            int num56 = (int)((double)vector2.Y - num32 * 0.5);
            int index3 = (int)((double)vector2.Y + num32 * 0.5);
            if (num54 < 0)
                num54 = 0;
            if (num55 > Main.maxTilesX)
                num55 = Main.maxTilesX;
            if (num56 < 0)
                num56 = 0;
            if (index3 > Main.maxTilesY)
                index3 = Main.maxTilesY;
            for (int index1 = num54; index1 < num55; ++index1)
            {
                for (int index2 = num56; index2 < index3; ++index2)
                {
                    Main.tile[index1, index2].active = false;
                    Main.tile[index1, index2].wall = (byte)0;
                }
            }
            for (int index1 = num54; index1 < num55; ++index1)
            {
                if (!Main.tile[index1, index3].active)
                {
                    Main.tile[index1, index3].active = true;
                    Main.tile[index1, index3].type = (byte)19;
                }
            }
            Main.dungeonX = (int)vector2.X;
            Main.dungeonY = index3;
            int index4 = NPC.NewNPC(WorldGen.dungeonX * 16 + 8, WorldGen.dungeonY * 16, 37, 0);
            Main.npc[index4].homeless = false;
            Main.npc[index4].homeTileX = Main.dungeonX;
            Main.npc[index4].homeTileY = Main.dungeonY;
            if (num3 == 1)
            {
                int num57 = 0;
                for (int index1 = num55; index1 < num55 + 25; ++index1)
                {
                    ++num57;
                    for (int index2 = index3 + num57; index2 < index3 + 25; ++index2)
                    {
                        Main.tile[index1, index2].active = true;
                        Main.tile[index1, index2].type = (byte)tileType;
                    }
                }
            }
            else
            {
                int num57 = 0;
                for (int index1 = num54; index1 > num54 - 25; --index1)
                {
                    ++num57;
                    for (int index2 = index3 + num57; index2 < index3 + 25; ++index2)
                    {
                        Main.tile[index1, index2].active = true;
                        Main.tile[index1, index2].type = (byte)tileType;
                    }
                }
            }
            int num58 = 1 + WorldGen.genRand.Next(2);
            int num59 = 2 + WorldGen.genRand.Next(4);
            int num60 = 0;
            int num61 = (int)((double)vector2.X - num31 * 0.5);
            int num62 = (int)((double)vector2.X + num31 * 0.5);
            int num63 = num61 + 2;
            int num64 = num62 - 2;
            for (int i1 = num63; i1 < num64; ++i1)
            {
                for (int j1 = num56; j1 < index3; ++j1)
                    WorldGen.PlaceWall(i1, j1, wallType, true);
                ++num60;
                if (num60 >= num59)
                {
                    i1 += num59 * 2;
                    num60 = 0;
                }
            }
            vector2.X -= (float)(num31 * 0.600000023841858) * (float)num3;
            vector2.Y += (float)num32 * 0.5f;
            double num65 = 15.0;
            double num66 = 3.0;
            vector2.Y -= (float)num66 * 0.5f;
            int num67 = (int)((double)vector2.X - num65 * 0.5);
            int num68 = (int)((double)vector2.X + num65 * 0.5);
            int num69 = (int)((double)vector2.Y - num66 * 0.5);
            int num70 = (int)((double)vector2.Y + num66 * 0.5);
            if (num67 < 0)
                num67 = 0;
            if (num68 > Main.maxTilesX)
                num68 = Main.maxTilesX;
            if (num69 < 0)
                num69 = 0;
            if (num70 > Main.maxTilesY)
                num70 = Main.maxTilesY;
            for (int index1 = num67; index1 < num68; ++index1)
            {
                for (int index2 = num69; index2 < num70; ++index2)
                    Main.tile[index1, index2].active = false;
            }
            if (num3 < 0)
                --vector2.X;
            WorldGen.PlaceTile((int)vector2.X, (int)vector2.Y + 1, 10, false, false, -1);
        }

        public static bool AddBuriedChest(int i, int j, int contain = 0)
        {
            if (WorldGen.genRand == null)
                WorldGen.genRand = new Random((int)DateTime.Now.Ticks);
            for (int index1 = j; index1 < Main.maxTilesY; ++index1)
            {
                if (Main.tile[i, index1].active && Main.tileSolid[(int)Main.tile[i, index1].type])
                {
                    int index2 = WorldGen.PlaceChest(i - 1, index1 - 1, 21);
                    if (index2 >= 0)
                    {
                        int index3 = 0;
                        while (index3 == 0)
                        {
                            if (contain > 0)
                            {
                                Main.chest[index2].item[index3].SetDefaults(contain);
                                ++index3;
                            }
                            else
                            {
                                int num = WorldGen.genRand.Next(7);
                                if (num == 2 && WorldGen.genRand.Next(2) == 0)
                                    num = WorldGen.genRand.Next(7);
                                if (num == 0)
                                    Main.chest[index2].item[index3].SetDefaults(49);
                                if (num == 1)
                                    Main.chest[index2].item[index3].SetDefaults(50);
                                if (num == 2)
                                    Main.chest[index2].item[index3].SetDefaults(52);
                                if (num == 3)
                                    Main.chest[index2].item[index3].SetDefaults(53);
                                if (num == 4)
                                    Main.chest[index2].item[index3].SetDefaults(54);
                                if (num == 5)
                                    Main.chest[index2].item[index3].SetDefaults(55);
                                if (num == 6)
                                {
                                    Main.chest[index2].item[index3].SetDefaults(51);
                                    Main.chest[index2].item[index3].stack = WorldGen.genRand.Next(26) + 25;
                                }
                                ++index3;
                            }
                            if (WorldGen.genRand.Next(3) == 0)
                            {
                                Main.chest[index2].item[index3].SetDefaults(167);
                                ++index3;
                            }
                            if (WorldGen.genRand.Next(2) == 0)
                            {
                                int num1 = WorldGen.genRand.Next(4);
                                int num2 = WorldGen.genRand.Next(8) + 3;
                                if (num1 == 0)
                                    Main.chest[index2].item[index3].SetDefaults(19);
                                if (num1 == 1)
                                    Main.chest[index2].item[index3].SetDefaults(20);
                                if (num1 == 2)
                                    Main.chest[index2].item[index3].SetDefaults(21);
                                if (num1 == 3)
                                    Main.chest[index2].item[index3].SetDefaults(22);
                                Main.chest[index2].item[index3].stack = num2;
                                ++index3;
                            }
                            if (WorldGen.genRand.Next(2) == 0)
                            {
                                int num1 = WorldGen.genRand.Next(2);
                                int num2 = WorldGen.genRand.Next(26) + 25;
                                if (num1 == 0)
                                    Main.chest[index2].item[index3].SetDefaults(40);
                                if (num1 == 1)
                                    Main.chest[index2].item[index3].SetDefaults(42);
                                Main.chest[index2].item[index3].stack = num2;
                                ++index3;
                            }
                            if (WorldGen.genRand.Next(2) == 0)
                            {
                                int num1 = WorldGen.genRand.Next(1);
                                int num2 = WorldGen.genRand.Next(3) + 3;
                                if (num1 == 0)
                                    Main.chest[index2].item[index3].SetDefaults(28);
                                Main.chest[index2].item[index3].stack = num2;
                                ++index3;
                            }
                            if (WorldGen.genRand.Next(2) == 0)
                            {
                                int num1 = WorldGen.genRand.Next(2);
                                int num2 = WorldGen.genRand.Next(11) + 10;
                                if (num1 == 0)
                                    Main.chest[index2].item[index3].SetDefaults(8);
                                if (num1 == 1)
                                    Main.chest[index2].item[index3].SetDefaults(31);
                                Main.chest[index2].item[index3].stack = num2;
                                ++index3;
                            }
                            if (WorldGen.genRand.Next(2) == 0)
                            {
                                Main.chest[index2].item[index3].SetDefaults(73);
                                Main.chest[index2].item[index3].stack = WorldGen.genRand.Next(1, 3);
                                ++index3;
                            }
                        }
                        return true;
                    }
                    else
                        return false;
                }
            }
            return false;
        }

        public static bool OpenDoor(int i, int j, int direction)
        {
            if (Main.tile[i, j - 1] == null)
                Main.tile[i, j - 1] = new Tile();
            if (Main.tile[i, j - 2] == null)
                Main.tile[i, j - 2] = new Tile();
            if (Main.tile[i, j + 1] == null)
                Main.tile[i, j + 1] = new Tile();
            if (Main.tile[i, j] == null)
                Main.tile[i, j] = new Tile();
            int index1 = (int)Main.tile[i, j - 1].frameY != 0 || (int)Main.tile[i, j - 1].type != (int)Main.tile[i, j].type ? ((int)Main.tile[i, j - 2].frameY != 0 || (int)Main.tile[i, j - 2].type != (int)Main.tile[i, j].type ? ((int)Main.tile[i, j + 1].frameY != 0 || (int)Main.tile[i, j + 1].type != (int)Main.tile[i, j].type ? j : j + 1) : j - 2) : j - 1;
            short num = (short)0;
            int index2;
            int i1;
            if (direction == -1)
            {
                index2 = i - 1;
                num = (short)36;
                i1 = i - 1;
            }
            else
            {
                index2 = i;
                i1 = i + 1;
            }
            bool flag = true;
            for (int j1 = index1; j1 < index1 + 3; ++j1)
            {
                if (Main.tile[i1, j1] == null)
                    Main.tile[i1, j1] = new Tile();
                if (Main.tile[i1, j1].active)
                {
                    if ((int)Main.tile[i1, j1].type == 3 || (int)Main.tile[i1, j1].type == 24 || ((int)Main.tile[i1, j1].type == 52 || (int)Main.tile[i1, j1].type == 61) || ((int)Main.tile[i1, j1].type == 62 || (int)Main.tile[i1, j1].type == 69 || ((int)Main.tile[i1, j1].type == 71 || (int)Main.tile[i1, j1].type == 73)) || (int)Main.tile[i1, j1].type == 74)
                    {
                        WorldGen.KillTile(i1, j1, false, false, false);
                    }
                    else
                    {
                        flag = false;
                        break;
                    }
                }
            }
            if (flag)
            {
                Main.PlaySound(8, i * 16, j * 16, 1);
                Main.tile[index2, index1].active = true;
                Main.tile[index2, index1].type = (byte)11;
                Main.tile[index2, index1].frameY = (short)0;
                Main.tile[index2, index1].frameX = num;
                if (Main.tile[index2 + 1, index1] == null)
                    Main.tile[index2 + 1, index1] = new Tile();
                Main.tile[index2 + 1, index1].active = true;
                Main.tile[index2 + 1, index1].type = (byte)11;
                Main.tile[index2 + 1, index1].frameY = (short)0;
                Main.tile[index2 + 1, index1].frameX = (short)((int)num + 18);
                if (Main.tile[index2, index1 + 1] == null)
                    Main.tile[index2, index1 + 1] = new Tile();
                Main.tile[index2, index1 + 1].active = true;
                Main.tile[index2, index1 + 1].type = (byte)11;
                Main.tile[index2, index1 + 1].frameY = (short)18;
                Main.tile[index2, index1 + 1].frameX = num;
                if (Main.tile[index2 + 1, index1 + 1] == null)
                    Main.tile[index2 + 1, index1 + 1] = new Tile();
                Main.tile[index2 + 1, index1 + 1].active = true;
                Main.tile[index2 + 1, index1 + 1].type = (byte)11;
                Main.tile[index2 + 1, index1 + 1].frameY = (short)18;
                Main.tile[index2 + 1, index1 + 1].frameX = (short)((int)num + 18);
                if (Main.tile[index2, index1 + 2] == null)
                    Main.tile[index2, index1 + 2] = new Tile();
                Main.tile[index2, index1 + 2].active = true;
                Main.tile[index2, index1 + 2].type = (byte)11;
                Main.tile[index2, index1 + 2].frameY = (short)36;
                Main.tile[index2, index1 + 2].frameX = num;
                if (Main.tile[index2 + 1, index1 + 2] == null)
                    Main.tile[index2 + 1, index1 + 2] = new Tile();
                Main.tile[index2 + 1, index1 + 2].active = true;
                Main.tile[index2 + 1, index1 + 2].type = (byte)11;
                Main.tile[index2 + 1, index1 + 2].frameY = (short)36;
                Main.tile[index2 + 1, index1 + 2].frameX = (short)((int)num + 18);
                for (int i2 = index2 - 1; i2 <= index2 + 2; ++i2)
                {
                    for (int j1 = index1 - 1; j1 <= index1 + 2; ++j1)
                        WorldGen.TileFrame(i2, j1, false, false);
                }
            }
            return flag;
        }

        public static void Check1x2(int x, int j, byte type)
        {
            if (!WorldGen.destroyObject)
            {
                int j1 = j;
                bool flag = true;
                if (Main.tile[x, j1] == null)
                    Main.tile[x, j1] = new Tile();
                if (Main.tile[x, j1 + 1] == null)
                    Main.tile[x, j1 + 1] = new Tile();
                if ((int)Main.tile[x, j1].frameY == 18)
                    --j1;
                if (Main.tile[x, j1] == null)
                    Main.tile[x, j1] = new Tile();
                if ((int)Main.tile[x, j1].frameY == 0 && (int)Main.tile[x, j1 + 1].frameY == 18 && ((int)Main.tile[x, j1].type == (int)type && (int)Main.tile[x, j1 + 1].type == (int)type))
                    flag = false;
                if (Main.tile[x, j1 + 2] == null)
                    Main.tile[x, j1 + 2] = new Tile();
                if (!Main.tile[x, j1 + 2].active || !Main.tileSolid[(int)Main.tile[x, j1 + 2].type])
                    flag = true;
                if ((int)Main.tile[x, j1 + 2].type != 2 && (int)Main.tile[x, j1].type == 20)
                    flag = true;
                if (flag)
                {
                    WorldGen.destroyObject = true;
                    if ((int)Main.tile[x, j1].type == (int)type)
                        WorldGen.KillTile(x, j1, false, false, false);
                    if ((int)Main.tile[x, j1 + 1].type == (int)type)
                        WorldGen.KillTile(x, j1 + 1, false, false, false);
                    if ((int)type == 15)
                        Item.NewItem(x * 16, j1 * 16, 32, 32, 34, 1, false);
                    WorldGen.destroyObject = false;
                }
            }
        }

        public static void CheckOnTable1x1(int x, int y, int type)
        {
            if (Main.tile[x, y + 1] != null && (!Main.tile[x, y + 1].active || !Main.tileTable[(int)Main.tile[x, y + 1].type]))
            {
                if (type == 78)
                {
                    if (!Main.tile[x, y + 1].active || !Main.tileSolid[(int)Main.tile[x, y + 1].type])
                        WorldGen.KillTile(x, y, false, false, false);
                }
                else
                    WorldGen.KillTile(x, y, false, false, false);
            }
        }

        public static void CheckSign(int x, int y, int type)
        {
            if (!WorldGen.destroyObject)
            {
                int num1 = x - 2;
                int num2 = x + 3;
                int num3 = y - 2;
                int num4 = y + 3;
                if (num1 >= 0 && num2 <= Main.maxTilesX && (num3 >= 0 && num4 <= Main.maxTilesY))
                {
                    bool flag = false;
                    for (int index1 = num1; index1 < num2; ++index1)
                    {
                        for (int index2 = num3; index2 < num4; ++index2)
                        {
                            if (Main.tile[index1, index2] == null)
                                Main.tile[index1, index2] = new Tile();
                        }
                    }
                    int num5 = (int)Main.tile[x, y].frameX / 18;
                    int num6 = (int)Main.tile[x, y].frameY / 18;
                    while (num5 > 1)
                        num5 -= 2;
                    int x1 = x - num5;
                    int y1 = y - num6;
                    int num7 = (int)Main.tile[x1, y1].frameX / 18 / 2;
                    int num8 = x1;
                    int num9 = x1 + 2;
                    int num10 = y1;
                    int num11 = y1 + 2;
                    int num12 = 0;
                    for (int index1 = num8; index1 < num9; ++index1)
                    {
                        int num13 = 0;
                        for (int index2 = num10; index2 < num11; ++index2)
                        {
                            if (!Main.tile[index1, index2].active || (int)Main.tile[index1, index2].type != type)
                            {
                                flag = true;
                                break;
                            }
                            else if ((int)Main.tile[index1, index2].frameX / 18 != num12 + num7 * 2 || (int)Main.tile[index1, index2].frameY / 18 != num13)
                            {
                                flag = true;
                                break;
                            }
                            else
                                ++num13;
                        }
                        ++num12;
                    }
                    if (!flag)
                    {
                        if (Main.tile[x1, y1 + 2].active && Main.tileSolid[(int)Main.tile[x1, y1 + 2].type] && Main.tile[x1 + 1, y1 + 2].active && Main.tileSolid[(int)Main.tile[x1 + 1, y1 + 2].type])
                            num7 = 0;
                        else if (Main.tile[x1, y1 - 1].active && Main.tileSolid[(int)Main.tile[x1, y1 - 1].type] && (!Main.tileSolidTop[(int)Main.tile[x1, y1 - 1].type] && Main.tile[x1 + 1, y1 - 1].active) && Main.tileSolid[(int)Main.tile[x1 + 1, y1 - 1].type] && !Main.tileSolidTop[(int)Main.tile[x1 + 1, y1 - 1].type])
                            num7 = 1;
                        else if (Main.tile[x1 - 1, y1].active && Main.tileSolid[(int)Main.tile[x1 - 1, y1].type] && (!Main.tileSolidTop[(int)Main.tile[x1 - 1, y1].type] && Main.tile[x1 - 1, y1 + 1].active) && Main.tileSolid[(int)Main.tile[x1 - 1, y1 + 1].type] && !Main.tileSolidTop[(int)Main.tile[x1 - 1, y1 + 1].type])
                            num7 = 2;
                        else if (Main.tile[x1 + 2, y1].active && Main.tileSolid[(int)Main.tile[x1 + 2, y1].type] && (!Main.tileSolidTop[(int)Main.tile[x1 + 2, y1].type] && Main.tile[x1 + 2, y1 + 1].active) && Main.tileSolid[(int)Main.tile[x1 + 2, y1 + 1].type] && !Main.tileSolidTop[(int)Main.tile[x1 + 2, y1 + 1].type])
                            num7 = 3;
                        else
                            flag = true;
                    }
                    if (flag)
                    {
                        WorldGen.destroyObject = true;
                        for (int i = num8; i < num9; ++i)
                        {
                            for (int j = num10; j < num11; ++j)
                            {
                                if ((int)Main.tile[i, j].type == type)
                                    WorldGen.KillTile(i, j, false, false, false);
                            }
                        }
                        Sign.KillSign(x1, y1);
                        Item.NewItem(x * 16, y * 16, 32, 32, 171, 1, false);
                        WorldGen.destroyObject = false;
                    }
                    else
                    {
                        int num13 = 36 * num7;
                        for (int index1 = 0; index1 < 2; ++index1)
                        {
                            for (int index2 = 0; index2 < 2; ++index2)
                            {
                                Main.tile[x1 + index1, y1 + index2].active = true;
                                Main.tile[x1 + index1, y1 + index2].type = (byte)type;
                                Main.tile[x1 + index1, y1 + index2].frameX = (short)(num13 + 18 * index1);
                                Main.tile[x1 + index1, y1 + index2].frameY = (short)(18 * index2);
                            }
                        }
                    }
                }
            }
        }

        public static bool PlaceSign(int x, int y, int type)
        {
            int num1 = x - 2;
            int num2 = x + 3;
            int num3 = y - 2;
            int num4 = y + 3;
            if (num1 < 0 || num2 > Main.maxTilesX || (num3 < 0 || num4 > Main.maxTilesY))
            {
                return false;
            }
            else
            {
                for (int index1 = num1; index1 < num2; ++index1)
                {
                    for (int index2 = num3; index2 < num4; ++index2)
                    {
                        if (Main.tile[index1, index2] == null)
                            Main.tile[index1, index2] = new Tile();
                    }
                }
                int index3 = x;
                int index4 = y;
                int num5;
                if (Main.tile[x, y + 1].active && Main.tileSolid[(int)Main.tile[x, y + 1].type] && Main.tile[x + 1, y + 1].active && Main.tileSolid[(int)Main.tile[x + 1, y + 1].type])
                {
                    --index4;
                    num5 = 0;
                }
                else if (Main.tile[x, y - 1].active && Main.tileSolid[(int)Main.tile[x, y - 1].type] && (!Main.tileSolidTop[(int)Main.tile[x, y - 1].type] && Main.tile[x + 1, y - 1].active) && Main.tileSolid[(int)Main.tile[x + 1, y - 1].type] && !Main.tileSolidTop[(int)Main.tile[x + 1, y - 1].type])
                    num5 = 1;
                else if (Main.tile[x - 1, y].active && Main.tileSolid[(int)Main.tile[x - 1, y].type] && (!Main.tileSolidTop[(int)Main.tile[x - 1, y].type] && Main.tile[x - 1, y + 1].active) && Main.tileSolid[(int)Main.tile[x - 1, y + 1].type] && !Main.tileSolidTop[(int)Main.tile[x - 1, y + 1].type])
                    num5 = 2;
                else if (Main.tile[x + 1, y].active && Main.tileSolid[(int)Main.tile[x + 1, y].type] && (!Main.tileSolidTop[(int)Main.tile[x + 1, y].type] && Main.tile[x + 1, y + 1].active) && Main.tileSolid[(int)Main.tile[x + 1, y + 1].type] && !Main.tileSolidTop[(int)Main.tile[x + 1, y + 1].type])
                {
                    --index3;
                    num5 = 3;
                }
                else
                    return false;
                if (Main.tile[index3, index4].active || Main.tile[index3 + 1, index4].active || Main.tile[index3, index4 + 1].active || Main.tile[index3 + 1, index4 + 1].active)
                {
                    return false;
                }
                else
                {
                    int num6 = 36 * num5;
                    for (int index1 = 0; index1 < 2; ++index1)
                    {
                        for (int index2 = 0; index2 < 2; ++index2)
                        {
                            Main.tile[index3 + index1, index4 + index2].active = true;
                            Main.tile[index3 + index1, index4 + index2].type = (byte)type;
                            Main.tile[index3 + index1, index4 + index2].frameX = (short)(num6 + 18 * index1);
                            Main.tile[index3 + index1, index4 + index2].frameY = (short)(18 * index2);
                        }
                    }
                    return true;
                }
            }
        }

        public static void PlaceOnTable1x1(int x, int y, int type)
        {
            bool flag = false;
            if (Main.tile[x, y] == null)
                Main.tile[x, y] = new Tile();
            if (Main.tile[x, y + 1] == null)
                Main.tile[x, y + 1] = new Tile();
            if (!Main.tile[x, y].active && Main.tile[x, y + 1].active && Main.tileTable[(int)Main.tile[x, y + 1].type])
                flag = true;
            if (type == 78 && (!Main.tile[x, y].active && Main.tile[x, y + 1].active && Main.tileSolid[(int)Main.tile[x, y + 1].type]))
                flag = true;
            if (flag)
            {
                Main.tile[x, y].active = true;
                Main.tile[x, y].frameX = (short)0;
                Main.tile[x, y].frameY = (short)0;
                Main.tile[x, y].type = (byte)type;
                if (type == 50)
                    Main.tile[x, y].frameX = (short)(18 * WorldGen.genRand.Next(5));
            }
        }

        public static void Place1x2(int x, int y, int type)
        {
            short num = (short)0;
            if (type == 20)
                num = (short)(WorldGen.genRand.Next(3) * 18);
            if (Main.tile[x, y - 1] == null)
                Main.tile[x, y - 1] = new Tile();
            if (Main.tile[x, y + 1] == null)
                Main.tile[x, y + 1] = new Tile();
            if (Main.tile[x, y + 1].active && Main.tileSolid[(int)Main.tile[x, y + 1].type] && !Main.tile[x, y - 1].active)
            {
                Main.tile[x, y - 1].active = true;
                Main.tile[x, y - 1].frameY = (short)0;
                Main.tile[x, y - 1].frameX = num;
                Main.tile[x, y - 1].type = (byte)type;
                Main.tile[x, y].active = true;
                Main.tile[x, y].frameY = (short)18;
                Main.tile[x, y].frameX = num;
                Main.tile[x, y].type = (byte)type;
            }
        }

        public static void Place1x2Top(int x, int y, int type)
        {
            short num = (short)0;
            if (Main.tile[x, y - 1] == null)
                Main.tile[x, y - 1] = new Tile();
            if (Main.tile[x, y + 1] == null)
                Main.tile[x, y + 1] = new Tile();
            if (Main.tile[x, y - 1].active && Main.tileSolid[(int)Main.tile[x, y - 1].type] && !Main.tileSolidTop[(int)Main.tile[x, y - 1].type] && !Main.tile[x, y + 1].active)
            {
                Main.tile[x, y].active = true;
                Main.tile[x, y].frameY = (short)0;
                Main.tile[x, y].frameX = num;
                Main.tile[x, y].type = (byte)type;
                Main.tile[x, y + 1].active = true;
                Main.tile[x, y + 1].frameY = (short)18;
                Main.tile[x, y + 1].frameX = num;
                Main.tile[x, y + 1].type = (byte)type;
            }
        }

        public static void Check1x2Top(int x, int j, byte type)
        {
            if (!WorldGen.destroyObject)
            {
                int j1 = j;
                bool flag = true;
                if (Main.tile[x, j1] == null)
                    Main.tile[x, j1] = new Tile();
                if (Main.tile[x, j1 + 1] == null)
                    Main.tile[x, j1 + 1] = new Tile();
                if ((int)Main.tile[x, j1].frameY == 18)
                    --j1;
                if (Main.tile[x, j1] == null)
                    Main.tile[x, j1] = new Tile();
                if ((int)Main.tile[x, j1].frameY == 0 && (int)Main.tile[x, j1 + 1].frameY == 18 && ((int)Main.tile[x, j1].type == (int)type && (int)Main.tile[x, j1 + 1].type == (int)type))
                    flag = false;
                if (Main.tile[x, j1 - 1] == null)
                    Main.tile[x, j1 - 1] = new Tile();
                if (!Main.tile[x, j1 - 1].active || !Main.tileSolid[(int)Main.tile[x, j1 - 1].type] || Main.tileSolidTop[(int)Main.tile[x, j1 - 1].type])
                    flag = true;
                if (flag)
                {
                    WorldGen.destroyObject = true;
                    if ((int)Main.tile[x, j1].type == (int)type)
                        WorldGen.KillTile(x, j1, false, false, false);
                    if ((int)Main.tile[x, j1 + 1].type == (int)type)
                        WorldGen.KillTile(x, j1 + 1, false, false, false);
                    if ((int)type == 42)
                        Item.NewItem(x * 16, j1 * 16, 32, 32, 136, 1, false);
                    WorldGen.destroyObject = false;
                }
            }
        }

        public static void Check2x1(int i, int y, byte type)
        {
            if (!WorldGen.destroyObject)
            {
                int i1 = i;
                bool flag = true;
                if (Main.tile[i1, y] == null)
                    Main.tile[i1, y] = new Tile();
                if (Main.tile[i1 + 1, y] == null)
                    Main.tile[i1 + 1, y] = new Tile();
                if (Main.tile[i1, y + 1] == null)
                    Main.tile[i1, y + 1] = new Tile();
                if (Main.tile[i1 + 1, y + 1] == null)
                    Main.tile[i1 + 1, y + 1] = new Tile();
                if ((int)Main.tile[i1, y].frameX == 18)
                    --i1;
                if ((int)Main.tile[i1, y].frameX == 0 && (int)Main.tile[i1 + 1, y].frameX == 18 && ((int)Main.tile[i1, y].type == (int)type && (int)Main.tile[i1 + 1, y].type == (int)type))
                    flag = false;
                if ((int)type == 29)
                {
                    if (!Main.tile[i1, y + 1].active || !Main.tileTable[(int)Main.tile[i1, y + 1].type])
                        flag = true;
                    if (!Main.tile[i1 + 1, y + 1].active || !Main.tileTable[(int)Main.tile[i1 + 1, y + 1].type])
                        flag = true;
                }
                else
                {
                    if (!Main.tile[i1, y + 1].active || !Main.tileSolid[(int)Main.tile[i1, y + 1].type])
                        flag = true;
                    if (!Main.tile[i1 + 1, y + 1].active || !Main.tileSolid[(int)Main.tile[i1 + 1, y + 1].type])
                        flag = true;
                }
                if (flag)
                {
                    WorldGen.destroyObject = true;
                    if ((int)Main.tile[i1, y].type == (int)type)
                        WorldGen.KillTile(i1, y, false, false, false);
                    if ((int)Main.tile[i1 + 1, y].type == (int)type)
                        WorldGen.KillTile(i1 + 1, y, false, false, false);
                    if ((int)type == 16)
                        Item.NewItem(i1 * 16, y * 16, 32, 32, 35, 1, false);
                    if ((int)type == 18)
                        Item.NewItem(i1 * 16, y * 16, 32, 32, 36, 1, false);
                    if ((int)type == 29)
                    {
                        Item.NewItem(i1 * 16, y * 16, 32, 32, 87, 1, false);
                        Main.PlaySound(13, i * 16, y * 16, 1);
                    }
                    WorldGen.destroyObject = false;
                }
            }
        }

        public static void Place2x1(int x, int y, int type)
        {
            if (Main.tile[x, y] == null)
                Main.tile[x, y] = new Tile();
            if (Main.tile[x + 1, y] == null)
                Main.tile[x + 1, y] = new Tile();
            if (Main.tile[x, y + 1] == null)
                Main.tile[x, y + 1] = new Tile();
            if (Main.tile[x + 1, y + 1] == null)
                Main.tile[x + 1, y + 1] = new Tile();
            bool flag = false;
            if (type != 29 && Main.tile[x, y + 1].active && (Main.tile[x + 1, y + 1].active && Main.tileSolid[(int)Main.tile[x, y + 1].type]) && (Main.tileSolid[(int)Main.tile[x + 1, y + 1].type] && !Main.tile[x, y].active) && !Main.tile[x + 1, y].active)
                flag = true;
            else if (type == 29 && Main.tile[x, y + 1].active && (Main.tile[x + 1, y + 1].active && Main.tileTable[(int)Main.tile[x, y + 1].type]) && (Main.tileTable[(int)Main.tile[x + 1, y + 1].type] && !Main.tile[x, y].active) && !Main.tile[x + 1, y].active)
                flag = true;
            if (flag)
            {
                Main.tile[x, y].active = true;
                Main.tile[x, y].frameY = (short)0;
                Main.tile[x, y].frameX = (short)0;
                Main.tile[x, y].type = (byte)type;
                Main.tile[x + 1, y].active = true;
                Main.tile[x + 1, y].frameY = (short)0;
                Main.tile[x + 1, y].frameX = (short)18;
                Main.tile[x + 1, y].type = (byte)type;
            }
        }

        public static void Check4x2(int i, int j, int type)
        {
            if (!WorldGen.destroyObject)
            {
                bool flag = false;
                int num1 = i;
                int num2 = j;
                int num3 = num1 + (int)Main.tile[i, j].frameX / 18 * -1;
                if (type == 79 && (int)Main.tile[i, j].frameX >= 72)
                    num3 += 4;
                int num4 = num2 + (int)Main.tile[i, j].frameY / 18 * -1;
                for (int index1 = num3; index1 < num3 + 4; ++index1)
                {
                    for (int index2 = num4; index2 < num4 + 2; ++index2)
                    {
                        int num5 = (index1 - num3) * 18;
                        if (type == 79 && (int)Main.tile[i, j].frameX >= 72)
                            num5 = (index1 - num3 + 4) * 18;
                        if (Main.tile[index1, index2] == null)
                            Main.tile[index1, index2] = new Tile();
                        if (!Main.tile[index1, index2].active || (int)Main.tile[index1, index2].type != type || (int)Main.tile[index1, index2].frameX != num5 || (int)Main.tile[index1, index2].frameY != (index2 - num4) * 18)
                            flag = true;
                    }
                    if (Main.tile[index1, num4 + 2] == null)
                        Main.tile[index1, num4 + 2] = new Tile();
                    if (!Main.tile[index1, num4 + 2].active || !Main.tileSolid[(int)Main.tile[index1, num4 + 2].type])
                        flag = true;
                }
                if (flag)
                {
                    WorldGen.destroyObject = true;
                    for (int i1 = num3; i1 < num3 + 4; ++i1)
                    {
                        for (int j1 = num4; j1 < num4 + 3; ++j1)
                        {
                            if ((int)Main.tile[i1, j1].type == type && Main.tile[i1, j1].active)
                                WorldGen.KillTile(i1, j1, false, false, false);
                        }
                    }
                    if (type == 79)
                        Item.NewItem(i * 16, j * 16, 32, 32, 224, 1, false);
                    WorldGen.destroyObject = false;
                    for (int i1 = num3 - 1; i1 < num3 + 4; ++i1)
                    {
                        for (int j1 = num4 - 1; j1 < num4 + 4; ++j1)
                            WorldGen.TileFrame(i1, j1, false, false);
                    }
                }
            }
        }

        public static void Check3x2(int i, int j, int type)
        {
            if (!WorldGen.destroyObject)
            {
                bool flag = false;
                int num1 = i;
                int num2 = j;
                int num3 = num1 + (int)Main.tile[i, j].frameX / 18 * -1;
                int num4 = num2 + (int)Main.tile[i, j].frameY / 18 * -1;
                for (int index1 = num3; index1 < num3 + 3; ++index1)
                {
                    for (int index2 = num4; index2 < num4 + 2; ++index2)
                    {
                        if (Main.tile[index1, index2] == null)
                            Main.tile[index1, index2] = new Tile();
                        if (!Main.tile[index1, index2].active || (int)Main.tile[index1, index2].type != type || (int)Main.tile[index1, index2].frameX != (index1 - num3) * 18 || (int)Main.tile[index1, index2].frameY != (index2 - num4) * 18)
                            flag = true;
                    }
                    if (Main.tile[index1, num4 + 2] == null)
                        Main.tile[index1, num4 + 2] = new Tile();
                    if (!Main.tile[index1, num4 + 2].active || !Main.tileSolid[(int)Main.tile[index1, num4 + 2].type])
                        flag = true;
                }
                if (flag)
                {
                    WorldGen.destroyObject = true;
                    for (int i1 = num3; i1 < num3 + 3; ++i1)
                    {
                        for (int j1 = num4; j1 < num4 + 3; ++j1)
                        {
                            if ((int)Main.tile[i1, j1].type == type && Main.tile[i1, j1].active)
                                WorldGen.KillTile(i1, j1, false, false, false);
                        }
                    }
                    if (type == 14)
                        Item.NewItem(i * 16, j * 16, 32, 32, 32, 1, false);
                    else if (type == 17)
                        Item.NewItem(i * 16, j * 16, 32, 32, 33, 1, false);
                    else if (type == 77)
                        Item.NewItem(i * 16, j * 16, 32, 32, 221, 1, false);
                    WorldGen.destroyObject = false;
                    for (int i1 = num3 - 1; i1 < num3 + 4; ++i1)
                    {
                        for (int j1 = num4 - 1; j1 < num4 + 4; ++j1)
                            WorldGen.TileFrame(i1, j1, false, false);
                    }
                }
            }
        }

        public static void Place4x2(int x, int y, int type, int direction = -1)
        {
            if (x >= 5 && x <= Main.maxTilesX - 5 && y >= 5 && y <= Main.maxTilesY - 5)
            {
                bool flag = true;
                for (int index1 = x - 1; index1 < x + 3; ++index1)
                {
                    for (int index2 = y - 1; index2 < y + 1; ++index2)
                    {
                        if (Main.tile[index1, index2] == null)
                            Main.tile[index1, index2] = new Tile();
                        if (Main.tile[index1, index2].active)
                            flag = false;
                    }
                    if (Main.tile[index1, y + 1] == null)
                        Main.tile[index1, y + 1] = new Tile();
                    if (!Main.tile[index1, y + 1].active || !Main.tileSolid[(int)Main.tile[index1, y + 1].type])
                        flag = false;
                }
                short num = (short)0;
                if (direction == 1)
                    num = (short)72;
                if (flag)
                {
                    Main.tile[x - 1, y - 1].active = true;
                    Main.tile[x - 1, y - 1].frameY = (short)0;
                    Main.tile[x - 1, y - 1].frameX = num;
                    Main.tile[x - 1, y - 1].type = (byte)type;
                    Main.tile[x, y - 1].active = true;
                    Main.tile[x, y - 1].frameY = (short)0;
                    Main.tile[x, y - 1].frameX = (short)(18 + (int)num);
                    Main.tile[x, y - 1].type = (byte)type;
                    Main.tile[x + 1, y - 1].active = true;
                    Main.tile[x + 1, y - 1].frameY = (short)0;
                    Main.tile[x + 1, y - 1].frameX = (short)(36 + (int)num);
                    Main.tile[x + 1, y - 1].type = (byte)type;
                    Main.tile[x + 2, y - 1].active = true;
                    Main.tile[x + 2, y - 1].frameY = (short)0;
                    Main.tile[x + 2, y - 1].frameX = (short)(54 + (int)num);
                    Main.tile[x + 2, y - 1].type = (byte)type;
                    Main.tile[x - 1, y].active = true;
                    Main.tile[x - 1, y].frameY = (short)18;
                    Main.tile[x - 1, y].frameX = num;
                    Main.tile[x - 1, y].type = (byte)type;
                    Main.tile[x, y].active = true;
                    Main.tile[x, y].frameY = (short)18;
                    Main.tile[x, y].frameX = (short)(18 + (int)num);
                    Main.tile[x, y].type = (byte)type;
                    Main.tile[x + 1, y].active = true;
                    Main.tile[x + 1, y].frameY = (short)18;
                    Main.tile[x + 1, y].frameX = (short)(36 + (int)num);
                    Main.tile[x + 1, y].type = (byte)type;
                    Main.tile[x + 2, y].active = true;
                    Main.tile[x + 2, y].frameY = (short)18;
                    Main.tile[x + 2, y].frameX = (short)(54 + (int)num);
                    Main.tile[x + 2, y].type = (byte)type;
                }
            }
        }

        public static void Place3x2(int x, int y, int type)
        {
            if (x >= 5 && x <= Main.maxTilesX - 5 && y >= 5 && y <= Main.maxTilesY - 5)
            {
                bool flag = true;
                for (int index1 = x - 1; index1 < x + 2; ++index1)
                {
                    for (int index2 = y - 1; index2 < y + 1; ++index2)
                    {
                        if (Main.tile[index1, index2] == null)
                            Main.tile[index1, index2] = new Tile();
                        if (Main.tile[index1, index2].active)
                            flag = false;
                    }
                    if (Main.tile[index1, y + 1] == null)
                        Main.tile[index1, y + 1] = new Tile();
                    if (!Main.tile[index1, y + 1].active || !Main.tileSolid[(int)Main.tile[index1, y + 1].type])
                        flag = false;
                }
                if (flag)
                {
                    Main.tile[x - 1, y - 1].active = true;
                    Main.tile[x - 1, y - 1].frameY = (short)0;
                    Main.tile[x - 1, y - 1].frameX = (short)0;
                    Main.tile[x - 1, y - 1].type = (byte)type;
                    Main.tile[x, y - 1].active = true;
                    Main.tile[x, y - 1].frameY = (short)0;
                    Main.tile[x, y - 1].frameX = (short)18;
                    Main.tile[x, y - 1].type = (byte)type;
                    Main.tile[x + 1, y - 1].active = true;
                    Main.tile[x + 1, y - 1].frameY = (short)0;
                    Main.tile[x + 1, y - 1].frameX = (short)36;
                    Main.tile[x + 1, y - 1].type = (byte)type;
                    Main.tile[x - 1, y].active = true;
                    Main.tile[x - 1, y].frameY = (short)18;
                    Main.tile[x - 1, y].frameX = (short)0;
                    Main.tile[x - 1, y].type = (byte)type;
                    Main.tile[x, y].active = true;
                    Main.tile[x, y].frameY = (short)18;
                    Main.tile[x, y].frameX = (short)18;
                    Main.tile[x, y].type = (byte)type;
                    Main.tile[x + 1, y].active = true;
                    Main.tile[x + 1, y].frameY = (short)18;
                    Main.tile[x + 1, y].frameX = (short)36;
                    Main.tile[x + 1, y].type = (byte)type;
                }
            }
        }

        public static void Check3x3(int i, int j, int type)
        {
            if (!WorldGen.destroyObject)
            {
                bool flag = false;
                int num1 = i;
                int num2 = j;
                int num3 = num1 + (int)Main.tile[i, j].frameX / 18 * -1;
                int num4 = num2 + (int)Main.tile[i, j].frameY / 18 * -1;
                for (int index1 = num3; index1 < num3 + 3; ++index1)
                {
                    for (int index2 = num4; index2 < num4 + 3; ++index2)
                    {
                        if (Main.tile[index1, index2] == null)
                            Main.tile[index1, index2] = new Tile();
                        if (!Main.tile[index1, index2].active || (int)Main.tile[index1, index2].type != type || (int)Main.tile[index1, index2].frameX != (index1 - num3) * 18 || (int)Main.tile[index1, index2].frameY != (index2 - num4) * 18)
                            flag = true;
                    }
                }
                if (Main.tile[num3 + 1, num4 - 1] == null)
                    Main.tile[num3 + 1, num4 - 1] = new Tile();
                if (!Main.tile[num3 + 1, num4 - 1].active || !Main.tileSolid[(int)Main.tile[num3 + 1, num4 - 1].type] || Main.tileSolidTop[(int)Main.tile[num3 + 1, num4 - 1].type])
                    flag = true;
                if (flag)
                {
                    WorldGen.destroyObject = true;
                    for (int i1 = num3; i1 < num3 + 3; ++i1)
                    {
                        for (int j1 = num4; j1 < num4 + 3; ++j1)
                        {
                            if ((int)Main.tile[i1, j1].type == type && Main.tile[i1, j1].active)
                                WorldGen.KillTile(i1, j1, false, false, false);
                        }
                    }
                    if (type == 34)
                        Item.NewItem(i * 16, j * 16, 32, 32, 106, 1, false);
                    else if (type == 35)
                        Item.NewItem(i * 16, j * 16, 32, 32, 107, 1, false);
                    else if (type == 36)
                        Item.NewItem(i * 16, j * 16, 32, 32, 108, 1, false);
                    WorldGen.destroyObject = false;
                    for (int i1 = num3 - 1; i1 < num3 + 4; ++i1)
                    {
                        for (int j1 = num4 - 1; j1 < num4 + 4; ++j1)
                            WorldGen.TileFrame(i1, j1, false, false);
                    }
                }
            }
        }

        public static void Place3x3(int x, int y, int type)
        {
            bool flag = true;
            for (int index1 = x - 1; index1 < x + 2; ++index1)
            {
                for (int index2 = y; index2 < y + 3; ++index2)
                {
                    if (Main.tile[index1, index2] == null)
                        Main.tile[index1, index2] = new Tile();
                    if (Main.tile[index1, index2].active)
                        flag = false;
                }
            }
            if (Main.tile[x, y - 1] == null)
                Main.tile[x, y - 1] = new Tile();
            if (!Main.tile[x, y - 1].active || !Main.tileSolid[(int)Main.tile[x, y - 1].type] || Main.tileSolidTop[(int)Main.tile[x, y - 1].type])
                flag = false;
            if (flag)
            {
                Main.tile[x - 1, y].active = true;
                Main.tile[x - 1, y].frameY = (short)0;
                Main.tile[x - 1, y].frameX = (short)0;
                Main.tile[x - 1, y].type = (byte)type;
                Main.tile[x, y].active = true;
                Main.tile[x, y].frameY = (short)0;
                Main.tile[x, y].frameX = (short)18;
                Main.tile[x, y].type = (byte)type;
                Main.tile[x + 1, y].active = true;
                Main.tile[x + 1, y].frameY = (short)0;
                Main.tile[x + 1, y].frameX = (short)36;
                Main.tile[x + 1, y].type = (byte)type;
                Main.tile[x - 1, y + 1].active = true;
                Main.tile[x - 1, y + 1].frameY = (short)18;
                Main.tile[x - 1, y + 1].frameX = (short)0;
                Main.tile[x - 1, y + 1].type = (byte)type;
                Main.tile[x, y + 1].active = true;
                Main.tile[x, y + 1].frameY = (short)18;
                Main.tile[x, y + 1].frameX = (short)18;
                Main.tile[x, y + 1].type = (byte)type;
                Main.tile[x + 1, y + 1].active = true;
                Main.tile[x + 1, y + 1].frameY = (short)18;
                Main.tile[x + 1, y + 1].frameX = (short)36;
                Main.tile[x + 1, y + 1].type = (byte)type;
                Main.tile[x - 1, y + 2].active = true;
                Main.tile[x - 1, y + 2].frameY = (short)36;
                Main.tile[x - 1, y + 2].frameX = (short)0;
                Main.tile[x - 1, y + 2].type = (byte)type;
                Main.tile[x, y + 2].active = true;
                Main.tile[x, y + 2].frameY = (short)36;
                Main.tile[x, y + 2].frameX = (short)18;
                Main.tile[x, y + 2].type = (byte)type;
                Main.tile[x + 1, y + 2].active = true;
                Main.tile[x + 1, y + 2].frameY = (short)36;
                Main.tile[x + 1, y + 2].frameX = (short)36;
                Main.tile[x + 1, y + 2].type = (byte)type;
            }
        }

        public static void PlaceSunflower(int x, int y, int type = 27)
        {
            if ((double)y <= Main.worldSurface - 1.0)
            {
                bool flag = true;
                for (int index1 = x; index1 < x + 2; ++index1)
                {
                    for (int index2 = y - 3; index2 < y + 1; ++index2)
                    {
                        if (Main.tile[index1, index2] == null)
                            Main.tile[index1, index2] = new Tile();
                        if (Main.tile[index1, index2].active || (int)Main.tile[index1, index2].wall > 0)
                            flag = false;
                    }
                    if (Main.tile[index1, y + 1] == null)
                        Main.tile[index1, y + 1] = new Tile();
                    if (!Main.tile[index1, y + 1].active || (int)Main.tile[index1, y + 1].type != 2)
                        flag = false;
                }
                if (flag)
                {
                    for (int index1 = 0; index1 < 2; ++index1)
                    {
                        for (int index2 = -3; index2 < 1; ++index2)
                        {
                            int num1 = index1 * 18 + WorldGen.genRand.Next(3) * 36;
                            int num2 = (index2 + 3) * 18;
                            Main.tile[x + index1, y + index2].active = true;
                            Main.tile[x + index1, y + index2].frameX = (short)num1;
                            Main.tile[x + index1, y + index2].frameY = (short)num2;
                            Main.tile[x + index1, y + index2].type = (byte)type;
                        }
                    }
                }
            }
        }

        public static void CheckSunflower(int i, int j, int type = 27)
        {
            if (!WorldGen.destroyObject)
            {
                bool flag = false;
                int num1 = 0;
                int num2 = j;
                int num3 = num1 + (int)Main.tile[i, j].frameX / 18;
                int num4 = num2 + (int)Main.tile[i, j].frameY / 18 * -1;
                while (num3 > 1)
                    num3 -= 2;
                int num5 = num3 * -1 + i;
                for (int index1 = num5; index1 < num5 + 2; ++index1)
                {
                    for (int index2 = num4; index2 < num4 + 4; ++index2)
                    {
                        if (Main.tile[index1, index2] == null)
                            Main.tile[index1, index2] = new Tile();
                        int num6 = (int)Main.tile[index1, index2].frameX / 18;
                        while (num6 > 1)
                            num6 -= 2;
                        if (!Main.tile[index1, index2].active || (int)Main.tile[index1, index2].type != type || num6 != index1 - num5 || (int)Main.tile[index1, index2].frameY != (index2 - num4) * 18)
                            flag = true;
                    }
                    if (Main.tile[index1, num4 + 4] == null)
                        Main.tile[index1, num4 + 4] = new Tile();
                    if (!Main.tile[index1, num4 + 4].active || (int)Main.tile[index1, num4 + 4].type != 2)
                        flag = true;
                }
                if (flag)
                {
                    WorldGen.destroyObject = true;
                    for (int i1 = num5; i1 < num5 + 2; ++i1)
                    {
                        for (int j1 = num4; j1 < num4 + 4; ++j1)
                        {
                            if ((int)Main.tile[i1, j1].type == type && Main.tile[i1, j1].active)
                                WorldGen.KillTile(i1, j1, false, false, false);
                        }
                    }
                    Item.NewItem(i * 16, j * 16, 32, 32, 63, 1, false);
                    WorldGen.destroyObject = false;
                }
            }
        }

        public static bool PlacePot(int x, int y, int type = 28)
        {
            bool flag = true;
            for (int index1 = x; index1 < x + 2; ++index1)
            {
                for (int index2 = y - 1; index2 < y + 1; ++index2)
                {
                    if (Main.tile[index1, index2] == null)
                        Main.tile[index1, index2] = new Tile();
                    if (Main.tile[index1, index2].active)
                        flag = false;
                }
                if (Main.tile[index1, y + 1] == null)
                    Main.tile[index1, y + 1] = new Tile();
                if (!Main.tile[index1, y + 1].active || !Main.tileSolid[(int)Main.tile[index1, y + 1].type])
                    flag = false;
            }
            if (flag)
            {
                for (int index1 = 0; index1 < 2; ++index1)
                {
                    for (int index2 = -1; index2 < 1; ++index2)
                    {
                        int num1 = index1 * 18 + WorldGen.genRand.Next(3) * 36;
                        int num2 = (index2 + 1) * 18;
                        Main.tile[x + index1, y + index2].active = true;
                        Main.tile[x + index1, y + index2].frameX = (short)num1;
                        Main.tile[x + index1, y + index2].frameY = (short)num2;
                        Main.tile[x + index1, y + index2].type = (byte)type;
                    }
                }
                return true;
            }
            else
                return false;
        }

        public static void CheckPot(int i, int j, int type = 28)
        {
            if (!WorldGen.destroyObject)
            {
                bool flag = false;
                int num1 = 0;
                int num2 = j;
                int num3 = num1 + (int)Main.tile[i, j].frameX / 18;
                int num4 = num2 + (int)Main.tile[i, j].frameY / 18 * -1;
                while (num3 > 1)
                    num3 -= 2;
                int num5 = num3 * -1 + i;
                for (int index1 = num5; index1 < num5 + 2; ++index1)
                {
                    for (int index2 = num4; index2 < num4 + 2; ++index2)
                    {
                        if (Main.tile[index1, index2] == null)
                            Main.tile[index1, index2] = new Tile();
                        int num6 = (int)Main.tile[index1, index2].frameX / 18;
                        while (num6 > 1)
                            num6 -= 2;
                        if (!Main.tile[index1, index2].active || (int)Main.tile[index1, index2].type != type || num6 != index1 - num5 || (int)Main.tile[index1, index2].frameY != (index2 - num4) * 18)
                            flag = true;
                    }
                    if (Main.tile[index1, num4 + 2] == null)
                        Main.tile[index1, num4 + 2] = new Tile();
                    if (!Main.tile[index1, num4 + 2].active || !Main.tileSolid[(int)Main.tile[index1, num4 + 2].type])
                        flag = true;
                }
                if (flag)
                {
                    WorldGen.destroyObject = true;
                    Main.PlaySound(13, i * 16, j * 16, 1);
                    for (int i1 = num5; i1 < num5 + 2; ++i1)
                    {
                        for (int j1 = num4; j1 < num4 + 2; ++j1)
                        {
                            if ((int)Main.tile[i1, j1].type == type && Main.tile[i1, j1].active)
                                WorldGen.KillTile(i1, j1, false, false, false);
                        }
                    }
                    Gore.NewGore(new Vector2((float)(i * 16), (float)(j * 16)), new Vector2(), 51);
                    Gore.NewGore(new Vector2((float)(i * 16), (float)(j * 16)), new Vector2(), 52);
                    Gore.NewGore(new Vector2((float)(i * 16), (float)(j * 16)), new Vector2(), 53);
                    int num6 = Main.rand.Next(10);
                    if (num6 == 0 && Main.player[(int)Player.FindClosest(new Vector2((float)(i * 16), (float)(j * 16)), 16, 16)].statLife < Main.player[(int)Player.FindClosest(new Vector2((float)(i * 16), (float)(j * 16)), 16, 16)].statLifeMax)
                        Item.NewItem(i * 16, j * 16, 16, 16, 58, 1, false);
                    else if (num6 == 1 && Main.player[(int)Player.FindClosest(new Vector2((float)(i * 16), (float)(j * 16)), 16, 16)].statMana < Main.player[(int)Player.FindClosest(new Vector2((float)(i * 16), (float)(j * 16)), 16, 16)].statManaMax)
                        Item.NewItem(i * 16, j * 16, 16, 16, 184, 1, false);
                    else if (num6 == 2)
                    {
                        int Stack = Main.rand.Next(3) + 1;
                        Item.NewItem(i * 16, j * 16, 16, 16, 8, Stack, false);
                    }
                    else if (num6 == 3)
                    {
                        int Stack = Main.rand.Next(8) + 3;
                        Item.NewItem(i * 16, j * 16, 16, 16, 40, Stack, false);
                    }
                    else if (num6 == 4)
                        Item.NewItem(i * 16, j * 16, 16, 16, 28, 1, false);
                    else if (num6 == 5)
                    {
                        int Stack = Main.rand.Next(4) + 1;
                        Item.NewItem(i * 16, j * 16, 16, 16, 166, Stack, false);
                    }
                    else
                    {
                        float num7 = (float)(200 + WorldGen.genRand.Next(-100, 101)) * (float)(1.0 + (double)Main.rand.Next(-20, 21) * 0.00999999977648258);
                        if (Main.rand.Next(5) == 0)
                            num7 *= (float)(1.0 + (double)Main.rand.Next(5, 11) * 0.00999999977648258);
                        if (Main.rand.Next(10) == 0)
                            num7 *= (float)(1.0 + (double)Main.rand.Next(10, 21) * 0.00999999977648258);
                        if (Main.rand.Next(15) == 0)
                            num7 *= (float)(1.0 + (double)Main.rand.Next(20, 41) * 0.00999999977648258);
                        if (Main.rand.Next(20) == 0)
                            num7 *= (float)(1.0 + (double)Main.rand.Next(40, 81) * 0.00999999977648258);
                        if (Main.rand.Next(25) == 0)
                            num7 *= (float)(1.0 + (double)Main.rand.Next(50, 101) * 0.00999999977648258);
                        while ((int)num7 > 0)
                        {
                            if ((double)num7 > 1000000.0)
                            {
                                int Stack = (int)((double)num7 / 1000000.0);
                                if (Stack > 50 && Main.rand.Next(2) == 0)
                                    Stack /= Main.rand.Next(3) + 1;
                                if (Main.rand.Next(2) == 0)
                                    Stack /= Main.rand.Next(3) + 1;
                                num7 -= (float)(1000000 * Stack);
                                Item.NewItem(i * 16, j * 16, 16, 16, 74, Stack, false);
                            }
                            else if ((double)num7 > 10000.0)
                            {
                                int Stack = (int)((double)num7 / 10000.0);
                                if (Stack > 50 && Main.rand.Next(2) == 0)
                                    Stack /= Main.rand.Next(3) + 1;
                                if (Main.rand.Next(2) == 0)
                                    Stack /= Main.rand.Next(3) + 1;
                                num7 -= (float)(10000 * Stack);
                                Item.NewItem(i * 16, j * 16, 16, 16, 73, Stack, false);
                            }
                            else if ((double)num7 > 100.0)
                            {
                                int Stack = (int)((double)num7 / 100.0);
                                if (Stack > 50 && Main.rand.Next(2) == 0)
                                    Stack /= Main.rand.Next(3) + 1;
                                if (Main.rand.Next(2) == 0)
                                    Stack /= Main.rand.Next(3) + 1;
                                num7 -= (float)(100 * Stack);
                                Item.NewItem(i * 16, j * 16, 16, 16, 72, Stack, false);
                            }
                            else
                            {
                                int Stack = (int)num7;
                                if (Stack > 50 && Main.rand.Next(2) == 0)
                                    Stack /= Main.rand.Next(3) + 1;
                                if (Main.rand.Next(2) == 0)
                                    Stack /= Main.rand.Next(4) + 1;
                                if (Stack < 1)
                                    Stack = 1;
                                num7 -= (float)Stack;
                                Item.NewItem(i * 16, j * 16, 16, 16, 71, Stack, false);
                            }
                        }
                    }
                    WorldGen.destroyObject = false;
                }
            }
        }

        public static int PlaceChest(int x, int y, int type = 21)
        {
            bool flag = true;
            int num = -1;
            for (int index1 = x; index1 < x + 2; ++index1)
            {
                for (int index2 = y - 1; index2 < y + 1; ++index2)
                {
                    if (Main.tile[index1, index2] == null)
                        Main.tile[index1, index2] = new Tile();
                    if (Main.tile[index1, index2].active)
                        flag = false;
                    if (Main.tile[index1, index2].lava)
                        flag = false;
                }
                if (Main.tile[index1, y + 1] == null)
                    Main.tile[index1, y + 1] = new Tile();
                if (!Main.tile[index1, y + 1].active || !Main.tileSolid[(int)Main.tile[index1, y + 1].type])
                    flag = false;
            }
            if (flag)
            {
                num = Chest.CreateChest(x, y - 1);
                if (num == -1)
                    flag = false;
            }
            if (flag)
            {
                Main.tile[x, y - 1].active = true;
                Main.tile[x, y - 1].frameY = (short)0;
                Main.tile[x, y - 1].frameX = (short)0;
                Main.tile[x, y - 1].type = (byte)type;
                Main.tile[x + 1, y - 1].active = true;
                Main.tile[x + 1, y - 1].frameY = (short)0;
                Main.tile[x + 1, y - 1].frameX = (short)18;
                Main.tile[x + 1, y - 1].type = (byte)type;
                Main.tile[x, y].active = true;
                Main.tile[x, y].frameY = (short)18;
                Main.tile[x, y].frameX = (short)0;
                Main.tile[x, y].type = (byte)type;
                Main.tile[x + 1, y].active = true;
                Main.tile[x + 1, y].frameY = (short)18;
                Main.tile[x + 1, y].frameX = (short)18;
                Main.tile[x + 1, y].type = (byte)type;
            }
            return num;
        }

        public static void CheckChest(int i, int j, int type)
        {
            if (!WorldGen.destroyObject)
            {
                bool flag = false;
                int num1 = i;
                int num2 = j;
                int num3 = num1 + (int)Main.tile[i, j].frameX / 18 * -1;
                int num4 = num2 + (int)Main.tile[i, j].frameY / 18 * -1;
                for (int index1 = num3; index1 < num3 + 2; ++index1)
                {
                    for (int index2 = num4; index2 < num4 + 2; ++index2)
                    {
                        if (Main.tile[index1, index2] == null)
                            Main.tile[index1, index2] = new Tile();
                        if (!Main.tile[index1, index2].active || (int)Main.tile[index1, index2].type != type || (int)Main.tile[index1, index2].frameX != (index1 - num3) * 18 || (int)Main.tile[index1, index2].frameY != (index2 - num4) * 18)
                            flag = true;
                    }
                    if (Main.tile[index1, num4 + 2] == null)
                        Main.tile[index1, num4 + 2] = new Tile();
                    if (!Main.tile[index1, num4 + 2].active || !Main.tileSolid[(int)Main.tile[index1, num4 + 2].type])
                        flag = true;
                }
                if (flag)
                {
                    WorldGen.destroyObject = true;
                    for (int i1 = num3; i1 < num3 + 2; ++i1)
                    {
                        for (int j1 = num4; j1 < num4 + 3; ++j1)
                        {
                            if ((int)Main.tile[i1, j1].type == type && Main.tile[i1, j1].active)
                                WorldGen.KillTile(i1, j1, false, false, false);
                        }
                    }
                    Item.NewItem(i * 16, j * 16, 32, 32, 48, 1, false);
                    WorldGen.destroyObject = false;
                }
            }
        }

        public static bool PlaceTile(int i, int j, int type, bool mute = false, bool forced = false, int plr = -1)
        {
            bool flag = false;
            if (i >= 0 && j >= 0 && i < Main.maxTilesX && j < Main.maxTilesY)
            {
                if (Main.tile[i, j] == null)
                    Main.tile[i, j] = new Tile();
                if (forced || Collision.EmptyTile(i, j, false) || !Main.tileSolid[type] || type == 23 && (int)Main.tile[i, j].type == 0 && Main.tile[i, j].active || (type == 2 && (int)Main.tile[i, j].type == 0 && Main.tile[i, j].active || type == 60 && (int)Main.tile[i, j].type == 59 && Main.tile[i, j].active) || type == 70 && (int)Main.tile[i, j].type == 59 && Main.tile[i, j].active)
                {
                    if (type == 23 && ((int)Main.tile[i, j].type != 0 || !Main.tile[i, j].active) || type == 2 && ((int)Main.tile[i, j].type != 0 || !Main.tile[i, j].active) || type == 60 && ((int)Main.tile[i, j].type != 59 || !Main.tile[i, j].active) || (int)Main.tile[i, j].liquid > 0 && (type == 3 || type == 4 || (type == 20 || type == 24) || (type == 27 || type == 32 || (type == 51 || type == 69)) || type == 72))
                    {
                        return false;
                    }
                    else
                    {
                        Main.tile[i, j].frameY = (short)0;
                        Main.tile[i, j].frameX = (short)0;
                        if (type == 3 || type == 24)
                        {
                            if (j + 1 < Main.maxTilesY && Main.tile[i, j + 1].active && ((int)Main.tile[i, j + 1].type == 2 && type == 3 || (int)Main.tile[i, j + 1].type == 23 && type == 24 || (int)Main.tile[i, j + 1].type == 78 && type == 3))
                            {
                                if (type == 24 && WorldGen.genRand.Next(13) == 0)
                                {
                                    Main.tile[i, j].active = true;
                                    Main.tile[i, j].type = (byte)32;
                                    WorldGen.SquareTileFrame(i, j, true);
                                }
                                else if ((int)Main.tile[i, j + 1].type == 78)
                                {
                                    Main.tile[i, j].active = true;
                                    Main.tile[i, j].type = (byte)type;
                                    Main.tile[i, j].frameX = (short)(WorldGen.genRand.Next(2) * 18 + 108);
                                }
                                else if ((int)Main.tile[i, j].wall == 0 && (int)Main.tile[i, j + 1].wall == 0)
                                {
                                    if (WorldGen.genRand.Next(50) == 0)
                                    {
                                        Main.tile[i, j].active = true;
                                        Main.tile[i, j].type = (byte)type;
                                        Main.tile[i, j].frameX = (short)144;
                                    }
                                    else if (WorldGen.genRand.Next(35) == 0)
                                    {
                                        Main.tile[i, j].active = true;
                                        Main.tile[i, j].type = (byte)type;
                                        Main.tile[i, j].frameX = (short)(WorldGen.genRand.Next(2) * 18 + 108);
                                    }
                                    else
                                    {
                                        Main.tile[i, j].active = true;
                                        Main.tile[i, j].type = (byte)type;
                                        Main.tile[i, j].frameX = (short)(WorldGen.genRand.Next(6) * 18);
                                    }
                                }
                            }
                        }
                        else if (type == 61)
                        {
                            if (j + 1 < Main.maxTilesY && Main.tile[i, j + 1].active && (int)Main.tile[i, j + 1].type == 60)
                            {
                                if (WorldGen.genRand.Next(10) == 0 && (double)j > Main.worldSurface)
                                {
                                    Main.tile[i, j].active = true;
                                    Main.tile[i, j].type = (byte)69;
                                    WorldGen.SquareTileFrame(i, j, true);
                                }
                                else if (WorldGen.genRand.Next(15) == 0 && (double)j > Main.worldSurface)
                                {
                                    Main.tile[i, j].active = true;
                                    Main.tile[i, j].type = (byte)type;
                                    Main.tile[i, j].frameX = (short)144;
                                }
                                else if (WorldGen.genRand.Next(1000) == 0 && (double)j > Main.rockLayer)
                                {
                                    Main.tile[i, j].active = true;
                                    Main.tile[i, j].type = (byte)type;
                                    Main.tile[i, j].frameX = (short)162;
                                }
                                else
                                {
                                    Main.tile[i, j].active = true;
                                    Main.tile[i, j].type = (byte)type;
                                    Main.tile[i, j].frameX = (double)j <= Main.rockLayer ? (short)(WorldGen.genRand.Next(6) * 18) : (short)(WorldGen.genRand.Next(8) * 18);
                                }
                            }
                        }
                        else if (type == 71)
                        {
                            if (j + 1 < Main.maxTilesY && Main.tile[i, j + 1].active && (int)Main.tile[i, j + 1].type == 70)
                            {
                                Main.tile[i, j].active = true;
                                Main.tile[i, j].type = (byte)type;
                                Main.tile[i, j].frameX = (short)(WorldGen.genRand.Next(5) * 18);
                            }
                        }
                        else if (type == 4)
                        {
                            if (Main.tile[i - 1, j] == null)
                                Main.tile[i - 1, j] = new Tile();
                            if (Main.tile[i + 1, j] == null)
                                Main.tile[i + 1, j] = new Tile();
                            if (Main.tile[i, j + 1] == null)
                                Main.tile[i, j + 1] = new Tile();
                            if (Main.tile[i - 1, j].active && (Main.tileSolid[(int)Main.tile[i - 1, j].type] || (int)Main.tile[i - 1, j].type == 5 && (int)Main.tile[i - 1, j - 1].type == 5 && (int)Main.tile[i - 1, j + 1].type == 5) || Main.tile[i + 1, j].active && (Main.tileSolid[(int)Main.tile[i + 1, j].type] || (int)Main.tile[i + 1, j].type == 5 && (int)Main.tile[i + 1, j - 1].type == 5 && (int)Main.tile[i + 1, j + 1].type == 5) || Main.tile[i, j + 1].active && Main.tileSolid[(int)Main.tile[i, j + 1].type])
                            {
                                Main.tile[i, j].active = true;
                                Main.tile[i, j].type = (byte)type;
                                WorldGen.SquareTileFrame(i, j, true);
                            }
                        }
                        else if (type == 10)
                        {
                            if (Main.tile[i, j - 1] == null)
                                Main.tile[i, j - 1] = new Tile();
                            if (Main.tile[i, j - 2] == null)
                                Main.tile[i, j - 2] = new Tile();
                            if (Main.tile[i, j - 3] == null)
                                Main.tile[i, j - 3] = new Tile();
                            if (Main.tile[i, j + 1] == null)
                                Main.tile[i, j + 1] = new Tile();
                            if (Main.tile[i, j + 2] == null)
                                Main.tile[i, j + 2] = new Tile();
                            if (Main.tile[i, j + 3] == null)
                                Main.tile[i, j + 3] = new Tile();
                            if (!Main.tile[i, j - 1].active && !Main.tile[i, j - 2].active && Main.tile[i, j - 3].active && Main.tileSolid[(int)Main.tile[i, j - 3].type])
                            {
                                WorldGen.PlaceDoor(i, j - 1, type);
                                WorldGen.SquareTileFrame(i, j, true);
                            }
                            else if (!Main.tile[i, j + 1].active && !Main.tile[i, j + 2].active && Main.tile[i, j + 3].active && Main.tileSolid[(int)Main.tile[i, j + 3].type])
                            {
                                WorldGen.PlaceDoor(i, j + 1, type);
                                WorldGen.SquareTileFrame(i, j, true);
                            }
                            else
                                return false;
                        }
                        else if (type == 34 || type == 35 || type == 36)
                        {
                            WorldGen.Place3x3(i, j, type);
                            WorldGen.SquareTileFrame(i, j, true);
                        }
                        else if (type == 13 || type == 33 || (type == 49 || type == 50) || type == 78)
                        {
                            WorldGen.PlaceOnTable1x1(i, j, type);
                            WorldGen.SquareTileFrame(i, j, true);
                        }
                        else if (type == 14 || type == 26)
                        {
                            WorldGen.Place3x2(i, j, type);
                            WorldGen.SquareTileFrame(i, j, true);
                        }
                        else if (type == 20)
                        {
                            if (Main.tile[i, j + 1] == null)
                                Main.tile[i, j + 1] = new Tile();
                            if (Main.tile[i, j + 1].active && (int)Main.tile[i, j + 1].type == 2)
                            {
                                WorldGen.Place1x2(i, j, type);
                                WorldGen.SquareTileFrame(i, j, true);
                            }
                        }
                        else if (type == 15)
                        {
                            if (Main.tile[i, j - 1] == null)
                                Main.tile[i, j - 1] = new Tile();
                            if (Main.tile[i, j] == null)
                                Main.tile[i, j] = new Tile();
                            WorldGen.Place1x2(i, j, type);
                            WorldGen.SquareTileFrame(i, j, true);
                        }
                        else if (type == 16 || type == 18 || type == 29)
                        {
                            WorldGen.Place2x1(i, j, type);
                            WorldGen.SquareTileFrame(i, j, true);
                        }
                        else if (type == 17 || type == 77)
                        {
                            WorldGen.Place3x2(i, j, type);
                            WorldGen.SquareTileFrame(i, j, true);
                        }
                        else if (type == 21)
                        {
                            WorldGen.PlaceChest(i, j, type);
                            WorldGen.SquareTileFrame(i, j, true);
                        }
                        else if (type == 27)
                        {
                            WorldGen.PlaceSunflower(i, j, 27);
                            WorldGen.SquareTileFrame(i, j, true);
                        }
                        else if (type == 28)
                        {
                            WorldGen.PlacePot(i, j, 28);
                            WorldGen.SquareTileFrame(i, j, true);
                        }
                        else if (type == 42)
                        {
                            WorldGen.Place1x2Top(i, j, type);
                            WorldGen.SquareTileFrame(i, j, true);
                        }
                        else if (type == 55)
                            WorldGen.PlaceSign(i, j, type);
                        else if (type == 79)
                        {
                            int direction = 1;
                            if (plr > -1)
                                direction = Main.player[plr].direction;
                            WorldGen.Place4x2(i, j, type, direction);
                        }
                        else
                        {
                            Main.tile[i, j].active = true;
                            Main.tile[i, j].type = (byte)type;
                        }
                        if (Main.tile[i, j].active && !mute)
                        {
                            WorldGen.SquareTileFrame(i, j, true);
                            flag = true;
                            Main.PlaySound(0, i * 16, j * 16, 1);
                            if (type == 22)
                            {
                                for (int index = 0; index < 3; ++index)
                                    Dust.NewDust(new Vector2((float)(i * 16), (float)(j * 16)), 16, 16, 14, 0.0f, 0.0f, 0, new Color(), 1f);
                            }
                        }
                    }
                }
            }
            return flag;
        }

        public static void KillWall(int i, int j, bool fail = false)
        {
            if (i >= 0 && j >= 0 && i < Main.maxTilesX && j < Main.maxTilesY)
            {
                if (Main.tile[i, j] == null)
                    Main.tile[i, j] = new Tile();
                if ((int)Main.tile[i, j].wall > 0)
                {
                    WorldGen.genRand.Next(3);
                    Main.PlaySound(0, i * 16, j * 16, 1);
                    int num = 10;
                    if (fail)
                        num = 3;
                    for (int index = 0; index < num; ++index)
                    {
                        int Type = 0;
                        if ((int)Main.tile[i, j].wall == 1 || (int)Main.tile[i, j].wall == 5 || ((int)Main.tile[i, j].wall == 6 || (int)Main.tile[i, j].wall == 7) || (int)Main.tile[i, j].wall == 8 || (int)Main.tile[i, j].wall == 9)
                            Type = 1;
                        if ((int)Main.tile[i, j].wall == 3)
                            Type = WorldGen.genRand.Next(2) != 0 ? 1 : 14;
                        if ((int)Main.tile[i, j].wall == 4)
                            Type = 7;
                        if ((int)Main.tile[i, j].wall == 12)
                            Type = 9;
                        if ((int)Main.tile[i, j].wall == 10)
                            Type = 10;
                        if ((int)Main.tile[i, j].wall == 11)
                            Type = 11;
                        Dust.NewDust(new Vector2((float)(i * 16), (float)(j * 16)), 16, 16, Type, 0.0f, 0.0f, 0, new Color(), 1f);
                    }
                    if (fail)
                    {
                        WorldGen.SquareWallFrame(i, j, true);
                    }
                    else
                    {
                        int Type = 0;
                        if ((int)Main.tile[i, j].wall == 1)
                            Type = 26;
                        if ((int)Main.tile[i, j].wall == 4)
                            Type = 93;
                        if ((int)Main.tile[i, j].wall == 5)
                            Type = 130;
                        if ((int)Main.tile[i, j].wall == 6)
                            Type = 132;
                        if ((int)Main.tile[i, j].wall == 7)
                            Type = 135;
                        if ((int)Main.tile[i, j].wall == 8)
                            Type = 138;
                        if ((int)Main.tile[i, j].wall == 9)
                            Type = 140;
                        if ((int)Main.tile[i, j].wall == 10)
                            Type = 142;
                        if ((int)Main.tile[i, j].wall == 11)
                            Type = 144;
                        if ((int)Main.tile[i, j].wall == 12)
                            Type = 146;
                        if (Type > 0)
                            Item.NewItem(i * 16, j * 16, 16, 16, Type, 1, false);
                        Main.tile[i, j].wall = (byte)0;
                        WorldGen.SquareWallFrame(i, j, true);
                    }
                }
            }
        }

        public static void KillTile(int i, int j, bool fail = false, bool effectOnly = false, bool noItem = false)
        {
            if (i >= 0 && j >= 0 && i < Main.maxTilesX && j < Main.maxTilesY)
            {
                if (Main.tile[i, j] == null)
                    Main.tile[i, j] = new Tile();
                if (Main.tile[i, j].active)
                {
                    if (j >= 1 && Main.tile[i, j - 1] == null)
                        Main.tile[i, j - 1] = new Tile();
                    if (j < 1 || !Main.tile[i, j - 1].active || ((int)Main.tile[i, j - 1].type != 5 || (int)Main.tile[i, j].type == 5) && ((int)Main.tile[i, j - 1].type != 21 || (int)Main.tile[i, j].type == 21) && (((int)Main.tile[i, j - 1].type != 26 || (int)Main.tile[i, j].type == 26) && ((int)Main.tile[i, j - 1].type != 72 || (int)Main.tile[i, j].type == 72) && ((int)Main.tile[i, j - 1].type != 12 || (int)Main.tile[i, j].type == 12)) || (int)Main.tile[i, j - 1].type == 5 && ((int)Main.tile[i, j - 1].frameX == 66 && (int)Main.tile[i, j - 1].frameY >= 0 && (int)Main.tile[i, j - 1].frameY <= 44 || (int)Main.tile[i, j - 1].frameX == 88 && (int)Main.tile[i, j - 1].frameY >= 66 && (int)Main.tile[i, j - 1].frameY <= 110 || (int)Main.tile[i, j - 1].frameY >= 198))
                    {
                        if (!effectOnly && !WorldGen.stopDrops)
                        {
                            if ((int)Main.tile[i, j].type == 3)
                            {
                                Main.PlaySound(6, i * 16, j * 16, 1);
                                if ((int)Main.tile[i, j].frameX == 144)
                                    Item.NewItem(i * 16, j * 16, 16, 16, 5, 1, false);
                            }
                            else if ((int)Main.tile[i, j].type == 24)
                            {
                                Main.PlaySound(6, i * 16, j * 16, 1);
                                if ((int)Main.tile[i, j].frameX == 144)
                                    Item.NewItem(i * 16, j * 16, 16, 16, 60, 1, false);
                            }
                            else if ((int)Main.tile[i, j].type == 32 || (int)Main.tile[i, j].type == 51 || ((int)Main.tile[i, j].type == 52 || (int)Main.tile[i, j].type == 61) || ((int)Main.tile[i, j].type == 62 || (int)Main.tile[i, j].type == 69 || ((int)Main.tile[i, j].type == 71 || (int)Main.tile[i, j].type == 73)) || (int)Main.tile[i, j].type == 74)
                                Main.PlaySound(6, i * 16, j * 16, 1);
                            else if ((int)Main.tile[i, j].type == 1 || (int)Main.tile[i, j].type == 6 || ((int)Main.tile[i, j].type == 7 || (int)Main.tile[i, j].type == 8) || ((int)Main.tile[i, j].type == 9 || (int)Main.tile[i, j].type == 22 || ((int)Main.tile[i, j].type == 25 || (int)Main.tile[i, j].type == 37)) || ((int)Main.tile[i, j].type == 38 || (int)Main.tile[i, j].type == 39 || ((int)Main.tile[i, j].type == 41 || (int)Main.tile[i, j].type == 43) || ((int)Main.tile[i, j].type == 44 || (int)Main.tile[i, j].type == 45 || ((int)Main.tile[i, j].type == 46 || (int)Main.tile[i, j].type == 47))) || ((int)Main.tile[i, j].type == 48 || (int)Main.tile[i, j].type == 56 || ((int)Main.tile[i, j].type == 58 || (int)Main.tile[i, j].type == 63) || ((int)Main.tile[i, j].type == 64 || (int)Main.tile[i, j].type == 65 || ((int)Main.tile[i, j].type == 66 || (int)Main.tile[i, j].type == 67)) || ((int)Main.tile[i, j].type == 68 || (int)Main.tile[i, j].type == 75)) || (int)Main.tile[i, j].type == 76)
                                Main.PlaySound(21, i * 16, j * 16, 1);
                            else
                                Main.PlaySound(0, i * 16, j * 16, 1);
                        }
                        int num = 10;
                        if (fail)
                            num = 3;
                        for (int index = 0; index < num; ++index)
                        {
                            int Type = 0;
                            if ((int)Main.tile[i, j].type == 0)
                                Type = 0;
                            if ((int)Main.tile[i, j].type == 1 || (int)Main.tile[i, j].type == 16 || ((int)Main.tile[i, j].type == 17 || (int)Main.tile[i, j].type == 38) || ((int)Main.tile[i, j].type == 39 || (int)Main.tile[i, j].type == 41 || ((int)Main.tile[i, j].type == 43 || (int)Main.tile[i, j].type == 44)) || (int)Main.tile[i, j].type == 48 || Main.tileStone[(int)Main.tile[i, j].type])
                                Type = 1;
                            if ((int)Main.tile[i, j].type == 4 || (int)Main.tile[i, j].type == 33)
                                Type = 6;
                            if ((int)Main.tile[i, j].type == 5 || (int)Main.tile[i, j].type == 10 || ((int)Main.tile[i, j].type == 11 || (int)Main.tile[i, j].type == 14) || ((int)Main.tile[i, j].type == 15 || (int)Main.tile[i, j].type == 19 || (int)Main.tile[i, j].type == 21) || (int)Main.tile[i, j].type == 30)
                                Type = 7;
                            if ((int)Main.tile[i, j].type == 2)
                                Type = WorldGen.genRand.Next(2) != 0 ? 2 : 0;
                            if ((int)Main.tile[i, j].type == 6 || (int)Main.tile[i, j].type == 26)
                                Type = 8;
                            if ((int)Main.tile[i, j].type == 7 || (int)Main.tile[i, j].type == 34 || (int)Main.tile[i, j].type == 47)
                                Type = 9;
                            if ((int)Main.tile[i, j].type == 8 || (int)Main.tile[i, j].type == 36 || (int)Main.tile[i, j].type == 45)
                                Type = 10;
                            if ((int)Main.tile[i, j].type == 9 || (int)Main.tile[i, j].type == 35 || (int)Main.tile[i, j].type == 42 || (int)Main.tile[i, j].type == 46)
                                Type = 11;
                            if ((int)Main.tile[i, j].type == 12)
                                Type = 12;
                            if ((int)Main.tile[i, j].type == 3 || (int)Main.tile[i, j].type == 73)
                                Type = 3;
                            if ((int)Main.tile[i, j].type == 13 || (int)Main.tile[i, j].type == 54)
                                Type = 13;
                            if ((int)Main.tile[i, j].type == 22)
                                Type = 14;
                            if ((int)Main.tile[i, j].type == 28 || (int)Main.tile[i, j].type == 78)
                                Type = 22;
                            if ((int)Main.tile[i, j].type == 29)
                                Type = 23;
                            if ((int)Main.tile[i, j].type == 40)
                                Type = 28;
                            if ((int)Main.tile[i, j].type == 49)
                                Type = 29;
                            if ((int)Main.tile[i, j].type == 50)
                                Type = 22;
                            if ((int)Main.tile[i, j].type == 51)
                                Type = 30;
                            if ((int)Main.tile[i, j].type == 52)
                                Type = 3;
                            if ((int)Main.tile[i, j].type == 53)
                                Type = 32;
                            if ((int)Main.tile[i, j].type == 56 || (int)Main.tile[i, j].type == 75)
                                Type = 37;
                            if ((int)Main.tile[i, j].type == 57)
                                Type = 36;
                            if ((int)Main.tile[i, j].type == 59)
                                Type = 38;
                            if ((int)Main.tile[i, j].type == 61 || (int)Main.tile[i, j].type == 62 || (int)Main.tile[i, j].type == 74)
                                Type = 40;
                            if ((int)Main.tile[i, j].type == 69)
                                Type = 7;
                            if ((int)Main.tile[i, j].type == 71 || (int)Main.tile[i, j].type == 72)
                                Type = 26;
                            if ((int)Main.tile[i, j].type == 70)
                                Type = 17;
                            if ((int)Main.tile[i, j].type == 2)
                                Type = WorldGen.genRand.Next(2) != 0 ? 39 : 38;
                            if ((int)Main.tile[i, j].type == 58 || (int)Main.tile[i, j].type == 76 || (int)Main.tile[i, j].type == 77)
                                Type = WorldGen.genRand.Next(2) != 0 ? 25 : 6;
                            if ((int)Main.tile[i, j].type == 37)
                                Type = WorldGen.genRand.Next(2) != 0 ? 23 : 6;
                            if ((int)Main.tile[i, j].type == 32)
                                Type = WorldGen.genRand.Next(2) != 0 ? 24 : 14;
                            if ((int)Main.tile[i, j].type == 23 || (int)Main.tile[i, j].type == 24)
                                Type = WorldGen.genRand.Next(2) != 0 ? 17 : 14;
                            if ((int)Main.tile[i, j].type == 25 || (int)Main.tile[i, j].type == 31)
                                Type = WorldGen.genRand.Next(2) != 0 ? 1 : 14;
                            if ((int)Main.tile[i, j].type == 20)
                                Type = WorldGen.genRand.Next(2) != 0 ? 2 : 7;
                            if ((int)Main.tile[i, j].type == 27)
                                Type = WorldGen.genRand.Next(2) != 0 ? 19 : 3;
                            if (((int)Main.tile[i, j].type == 34 || (int)Main.tile[i, j].type == 35 || ((int)Main.tile[i, j].type == 36 || (int)Main.tile[i, j].type == 42)) && Main.rand.Next(2) == 0)
                                Type = 6;
                            if (Type >= 0)
                                Dust.NewDust(new Vector2((float)(i * 16), (float)(j * 16)), 16, 16, Type, 0.0f, 0.0f, 0, new Color(), 1f);
                        }
                        if (!effectOnly)
                        {
                            if (fail)
                            {
                                if ((int)Main.tile[i, j].type == 2 || (int)Main.tile[i, j].type == 23)
                                    Main.tile[i, j].type = (byte)0;
                                if ((int)Main.tile[i, j].type == 60)
                                    Main.tile[i, j].type = (byte)59;
                                WorldGen.SquareTileFrame(i, j, true);
                            }
                            else if ((int)Main.tile[i, j].type != 21 || Main.netMode == 1 || Chest.DestroyChest(i - (int)Main.tile[i, j].frameX / 18, j - (int)Main.tile[i, j].frameY / 18))
                            {
                                if (!noItem && !WorldGen.stopDrops)
                                {
                                    int Type = 0;
                                    if ((int)Main.tile[i, j].type == 0 || (int)Main.tile[i, j].type == 2)
                                        Type = 2;
                                    else if ((int)Main.tile[i, j].type == 1)
                                        Type = 3;
                                    else if ((int)Main.tile[i, j].type == 4)
                                        Type = 8;
                                    else if ((int)Main.tile[i, j].type == 5)
                                        Type = (int)Main.tile[i, j].frameX < 22 || (int)Main.tile[i, j].frameY < 198 ? 9 : (WorldGen.genRand.Next(2) != 0 ? 9 : 27);
                                    else if ((int)Main.tile[i, j].type == 6)
                                        Type = 11;
                                    else if ((int)Main.tile[i, j].type == 7)
                                        Type = 12;
                                    else if ((int)Main.tile[i, j].type == 8)
                                        Type = 13;
                                    else if ((int)Main.tile[i, j].type == 9)
                                        Type = 14;
                                    else if ((int)Main.tile[i, j].type == 13)
                                    {
                                        Main.PlaySound(13, i * 16, j * 16, 1);
                                        Type = (int)Main.tile[i, j].frameX != 18 ? ((int)Main.tile[i, j].frameX != 36 ? 31 : 110) : 28;
                                    }
                                    else if ((int)Main.tile[i, j].type == 19)
                                        Type = 94;
                                    else if ((int)Main.tile[i, j].type == 22)
                                        Type = 56;
                                    else if ((int)Main.tile[i, j].type == 23)
                                        Type = 2;
                                    else if ((int)Main.tile[i, j].type == 25)
                                        Type = 61;
                                    else if ((int)Main.tile[i, j].type == 30)
                                        Type = 9;
                                    else if ((int)Main.tile[i, j].type == 33)
                                        Type = 105;
                                    else if ((int)Main.tile[i, j].type == 37)
                                        Type = 116;
                                    else if ((int)Main.tile[i, j].type == 38)
                                        Type = 129;
                                    else if ((int)Main.tile[i, j].type == 39)
                                        Type = 131;
                                    else if ((int)Main.tile[i, j].type == 40)
                                        Type = 133;
                                    else if ((int)Main.tile[i, j].type == 41)
                                        Type = 134;
                                    else if ((int)Main.tile[i, j].type == 43)
                                        Type = 137;
                                    else if ((int)Main.tile[i, j].type == 44)
                                        Type = 139;
                                    else if ((int)Main.tile[i, j].type == 45)
                                        Type = 141;
                                    else if ((int)Main.tile[i, j].type == 46)
                                        Type = 143;
                                    else if ((int)Main.tile[i, j].type == 47)
                                        Type = 145;
                                    else if ((int)Main.tile[i, j].type == 48)
                                        Type = 147;
                                    else if ((int)Main.tile[i, j].type == 49)
                                        Type = 148;
                                    else if ((int)Main.tile[i, j].type == 51)
                                        Type = 150;
                                    else if ((int)Main.tile[i, j].type == 53)
                                        Type = 169;
                                    else if ((int)Main.tile[i, j].type == 54)
                                        Main.PlaySound(13, i * 16, j * 16, 1);
                                    else if ((int)Main.tile[i, j].type == 56)
                                        Type = 173;
                                    else if ((int)Main.tile[i, j].type == 57)
                                        Type = 172;
                                    else if ((int)Main.tile[i, j].type == 58)
                                        Type = 174;
                                    else if ((int)Main.tile[i, j].type == 60)
                                        Type = 176;
                                    else if ((int)Main.tile[i, j].type == 70)
                                        Type = 176;
                                    else if ((int)Main.tile[i, j].type == 75)
                                        Type = 192;
                                    else if ((int)Main.tile[i, j].type == 76)
                                        Type = 214;
                                    else if ((int)Main.tile[i, j].type == 78)
                                        Type = 222;
                                    else if ((int)Main.tile[i, j].type == 61 || (int)Main.tile[i, j].type == 74)
                                    {
                                        if ((int)Main.tile[i, j].frameX == 162)
                                            Type = 223;
                                        else if ((int)Main.tile[i, j].frameX >= 108 && (int)Main.tile[i, j].frameX <= 126 && (double)j > Main.rockLayer)
                                        {
                                            if (WorldGen.genRand.Next(2) == 0)
                                                Type = 208;
                                        }
                                        else if (WorldGen.genRand.Next(100) == 0)
                                            Type = 195;
                                    }
                                    else if ((int)Main.tile[i, j].type == 59 || (int)Main.tile[i, j].type == 60)
                                        Type = 176;
                                    else if ((int)Main.tile[i, j].type == 71 || (int)Main.tile[i, j].type == 72)
                                        Type = WorldGen.genRand.Next(50) != 0 ? 183 : 194;
                                    else if ((int)Main.tile[i, j].type >= 63 && (int)Main.tile[i, j].type <= 68)
                                        Type = (int)Main.tile[i, j].type - 63 + 177;
                                    else if ((int)Main.tile[i, j].type == 50)
                                        Type = (int)Main.tile[i, j].frameX != 90 ? 149 : 165;
                                    if (Type > 0)
                                        Item.NewItem(i * 16, j * 16, 16, 16, Type, 1, false);
                                }
                                Main.tile[i, j].active = false;
                                if (Main.tileSolid[(int)Main.tile[i, j].type])
                                    Main.tile[i, j].lighted = false;
                                Main.tile[i, j].frameX = (short)-1;
                                Main.tile[i, j].frameY = (short)-1;
                                Main.tile[i, j].frameNumber = (byte)0;
                                Main.tile[i, j].type = (byte)0;
                                WorldGen.SquareTileFrame(i, j, true);
                            }
                        }
                    }
                }
            }
        }

        public static bool PlayerLOS(int x, int y)
        {
            Rectangle rectangle1 = new Rectangle(x * 16, y * 16, 16, 16);
            for (int index = 0; index < (int)byte.MaxValue; ++index)
            {
                if (Main.player[index].active)
                {
                    Rectangle rectangle2 = new Rectangle((int)((double)Main.player[index].position.X + (double)Main.player[index].width * 0.5 - (double)NPC.sWidth * 0.6), (int)((double)Main.player[index].position.Y + (double)Main.player[index].height * 0.5 - (double)NPC.sHeight * 0.6), (int)((double)NPC.sWidth * 1.2), (int)((double)NPC.sHeight * 1.2));
                    if (rectangle1.Intersects(rectangle2))
                        return true;
                }
            }
            return false;
        }

        public static void UpdateWorld()
        {
            ++Liquid.skipCount;
            if (Liquid.skipCount > 1)
            {
                Liquid.UpdateLiquid();
                Liquid.skipCount = 0;
            }
            float num1 = 4E-05f;
            float num2 = 2E-05f;
            bool flag1 = false;
            ++WorldGen.spawnDelay;
            if (Main.invasionType > 0)
                WorldGen.spawnDelay = 0;
            if (WorldGen.spawnDelay >= 20)
            {
                flag1 = true;
                WorldGen.spawnDelay = 0;
                for (int index = 0; index < 1000; ++index)
                {
                    if (Main.npc[index].active && Main.npc[index].homeless && Main.npc[index].townNPC)
                    {
                        WorldGen.spawnNPC = Main.npc[index].type;
                        break;
                    }
                }
            }
            for (int index1 = 0; (double)index1 < (double)(Main.maxTilesX * Main.maxTilesY) * (double)num1; ++index1)
            {
                int index2 = WorldGen.genRand.Next(10, Main.maxTilesX - 10);
                int index3 = WorldGen.genRand.Next(10, (int)Main.worldSurface - 1);
                int num3 = index2 - 1;
                int num4 = index2 + 2;
                int index4 = index3 - 1;
                int num5 = index3 + 2;
                if (num3 < 10)
                    num3 = 10;
                if (num4 > Main.maxTilesX - 10)
                    num4 = Main.maxTilesX - 10;
                if (index4 < 10)
                    index4 = 10;
                if (num5 > Main.maxTilesY - 10)
                    num5 = Main.maxTilesY - 10;
                if (Main.tile[index2, index3] != null)
                {
                    if ((int)Main.tile[index2, index3].liquid > 32)
                    {
                        if (Main.tile[index2, index3].active && ((int)Main.tile[index2, index3].type == 3 || (int)Main.tile[index2, index3].type == 20 || ((int)Main.tile[index2, index3].type == 24 || (int)Main.tile[index2, index3].type == 27) || (int)Main.tile[index2, index3].type == 73))
                        {
                            WorldGen.KillTile(index2, index3, false, false, false);
                            if (Main.netMode == 2)
                                NetMessage.SendData(17, -1, -1, "", 0, (float)index2, (float)index3, 0.0f);
                        }
                    }
                    else if (Main.tile[index2, index3].active)
                    {
                        if ((int)Main.tile[index2, index3].type == 78)
                        {
                            if (!Main.tile[index2, index4].active)
                            {
                                WorldGen.PlaceTile(index2, index4, 3, true, false, -1);
                                if (Main.netMode == 2 && Main.tile[index2, index4].active)
                                    NetMessage.SendTileSquare(-1, index2, index4, 1);
                            }
                        }
                        else if ((int)Main.tile[index2, index3].type == 2 || (int)Main.tile[index2, index3].type == 23 || (int)Main.tile[index2, index3].type == 32)
                        {
                            int grass = (int)Main.tile[index2, index3].type;
                            if (!Main.tile[index2, index4].active && WorldGen.genRand.Next(10) == 0 && grass == 2)
                            {
                                WorldGen.PlaceTile(index2, index4, 3, true, false, -1);
                                if (Main.netMode == 2 && Main.tile[index2, index4].active)
                                    NetMessage.SendTileSquare(-1, index2, index4, 1);
                            }
                            if (!Main.tile[index2, index4].active && WorldGen.genRand.Next(10) == 0 && grass == 23)
                            {
                                WorldGen.PlaceTile(index2, index4, 24, true, false, -1);
                                if (Main.netMode == 2 && Main.tile[index2, index4].active)
                                    NetMessage.SendTileSquare(-1, index2, index4, 1);
                            }
                            bool flag2 = false;
                            for (int i = num3; i < num4; ++i)
                            {
                                for (int j = index4; j < num5; ++j)
                                {
                                    if ((index2 != i || index3 != j) && Main.tile[i, j].active)
                                    {
                                        if (grass == 32)
                                            grass = 23;
                                        if ((int)Main.tile[i, j].type == 0 || grass == 23 && (int)Main.tile[i, j].type == 2)
                                        {
                                            WorldGen.SpreadGrass(i, j, 0, grass, false);
                                            if (grass == 23)
                                                WorldGen.SpreadGrass(i, j, 2, grass, false);
                                            if ((int)Main.tile[i, j].type == grass)
                                            {
                                                WorldGen.SquareTileFrame(i, j, true);
                                                flag2 = true;
                                            }
                                        }
                                    }
                                }
                            }
                            if (Main.netMode == 2 && flag2)
                                NetMessage.SendTileSquare(-1, index2, index3, 3);
                        }
                        else if ((int)Main.tile[index2, index3].type == 20 && (!WorldGen.PlayerLOS(index2, index3) && WorldGen.genRand.Next(10) == 0))
                            WorldGen.GrowTree(index2, index3);
                        if ((int)Main.tile[index2, index3].type == 3 && WorldGen.genRand.Next(10) == 0 && (int)Main.tile[index2, index3].frameX < 144)
                        {
                            Main.tile[index2, index3].type = (byte)73;
                            if (Main.netMode == 2)
                                NetMessage.SendTileSquare(-1, index2, index3, 3);
                        }
                        if ((int)Main.tile[index2, index3].type == 32 && WorldGen.genRand.Next(3) == 0)
                        {
                            int index5 = index2;
                            int index6 = index3;
                            int num6 = 0;
                            if (Main.tile[index5 + 1, index6].active && (int)Main.tile[index5 + 1, index6].type == 32)
                                ++num6;
                            if (Main.tile[index5 - 1, index6].active && (int)Main.tile[index5 - 1, index6].type == 32)
                                ++num6;
                            if (Main.tile[index5, index6 + 1].active && (int)Main.tile[index5, index6 + 1].type == 32)
                                ++num6;
                            if (Main.tile[index5, index6 - 1].active && (int)Main.tile[index5, index6 - 1].type == 32)
                                ++num6;
                            if (num6 < 3 || (int)Main.tile[index2, index3].type == 23)
                            {
                                switch (WorldGen.genRand.Next(4))
                                {
                                    case 0:
                                        --index6;
                                        break;
                                    case 1:
                                        ++index6;
                                        break;
                                    case 2:
                                        --index5;
                                        break;
                                    case 3:
                                        ++index5;
                                        break;
                                }
                                if (!Main.tile[index5, index6].active)
                                {
                                    int num7 = 0;
                                    if (Main.tile[index5 + 1, index6].active && (int)Main.tile[index5 + 1, index6].type == 32)
                                        ++num7;
                                    if (Main.tile[index5 - 1, index6].active && (int)Main.tile[index5 - 1, index6].type == 32)
                                        ++num7;
                                    if (Main.tile[index5, index6 + 1].active && (int)Main.tile[index5, index6 + 1].type == 32)
                                        ++num7;
                                    if (Main.tile[index5, index6 - 1].active && (int)Main.tile[index5, index6 - 1].type == 32)
                                        ++num7;
                                    if (num7 < 2)
                                    {
                                        int num8 = 7;
                                        int num9 = index5 - num8;
                                        int num10 = index5 + num8;
                                        int num11 = index6 - num8;
                                        int num12 = index6 + num8;
                                        bool flag2 = false;
                                        for (int index7 = num9; index7 < num10; ++index7)
                                        {
                                            for (int index8 = num11; index8 < num12; ++index8)
                                            {
                                                if (Math.Abs(index7 - index5) * 2 + Math.Abs(index8 - index6) < 9 && (Main.tile[index7, index8].active && (int)Main.tile[index7, index8].type == 23) && (Main.tile[index7, index8 - 1].active && (int)Main.tile[index7, index8 - 1].type == 32 && (int)Main.tile[index7, index8 - 1].liquid == 0))
                                                {
                                                    flag2 = true;
                                                    break;
                                                }
                                            }
                                        }
                                        if (flag2)
                                        {
                                            Main.tile[index5, index6].type = (byte)32;
                                            Main.tile[index5, index6].active = true;
                                            WorldGen.SquareTileFrame(index5, index6, true);
                                            if (Main.netMode == 2)
                                                NetMessage.SendTileSquare(-1, index5, index6, 3);
                                        }
                                    }
                                }
                            }
                        }
                        if (((int)Main.tile[index2, index3].type == 2 || (int)Main.tile[index2, index3].type == 52) && WorldGen.genRand.Next(5) == 0 && (!Main.tile[index2, index3 + 1].active && !Main.tile[index2, index3 + 1].lava))
                        {
                            bool flag2 = false;
                            for (int index5 = index3; index5 > index3 - 10; --index5)
                            {
                                if (Main.tile[index2, index5].active && (int)Main.tile[index2, index5].type == 2)
                                {
                                    flag2 = true;
                                    break;
                                }
                            }
                            if (flag2)
                            {
                                int index5 = index2;
                                int index6 = index3 + 1;
                                Main.tile[index5, index6].type = (byte)52;
                                Main.tile[index5, index6].active = true;
                                WorldGen.SquareTileFrame(index5, index6, true);
                                if (Main.netMode == 2)
                                    NetMessage.SendTileSquare(-1, index5, index6, 3);
                            }
                        }
                    }
                    else if (flag1 && WorldGen.spawnNPC > 0)
                        WorldGen.SpawnNPC(index2, index3);
                    if (Main.tile[index2, index3].active && (int)Main.tile[index2, index3].type == 60)
                    {
                        int grass = (int)Main.tile[index2, index3].type;
                        if (!Main.tile[index2, index4].active && WorldGen.genRand.Next(7) == 0)
                        {
                            WorldGen.PlaceTile(index2, index4, 61, true, false, -1);
                            if (Main.netMode == 2 && Main.tile[index2, index4].active)
                                NetMessage.SendTileSquare(-1, index2, index4, 1);
                        }
                        else if (WorldGen.genRand.Next(50) == 0 && (!Main.tile[index2, index4].active || (int)Main.tile[index2, index4].type == 61 || (int)Main.tile[index2, index4].type == 74 || (int)Main.tile[index2, index4].type == 69) && !WorldGen.PlayerLOS(index2, index3))
                            WorldGen.GrowTree(index2, index3);
                        bool flag2 = false;
                        for (int i = num3; i < num4; ++i)
                        {
                            for (int j = index4; j < num5; ++j)
                            {
                                if ((index2 != i || index3 != j) && Main.tile[i, j].active && (int)Main.tile[i, j].type == 59)
                                {
                                    WorldGen.SpreadGrass(i, j, 59, grass, false);
                                    if ((int)Main.tile[i, j].type == grass)
                                    {
                                        WorldGen.SquareTileFrame(i, j, true);
                                        flag2 = true;
                                    }
                                }
                            }
                        }
                        if (Main.netMode == 2 && flag2)
                            NetMessage.SendTileSquare(-1, index2, index3, 3);
                    }
                    if ((int)Main.tile[index2, index3].type == 61 && WorldGen.genRand.Next(3) == 0 && (int)Main.tile[index2, index3].frameX < 144)
                    {
                        Main.tile[index2, index3].type = (byte)74;
                        if (Main.netMode == 2)
                            NetMessage.SendTileSquare(-1, index2, index3, 3);
                    }
                    if (((int)Main.tile[index2, index3].type == 60 || (int)Main.tile[index2, index3].type == 62) && WorldGen.genRand.Next(5) == 0 && (!Main.tile[index2, index3 + 1].active && !Main.tile[index2, index3 + 1].lava))
                    {
                        bool flag2 = false;
                        for (int index5 = index3; index5 > index3 - 10; --index5)
                        {
                            if (Main.tile[index2, index5].active && (int)Main.tile[index2, index5].type == 60)
                            {
                                flag2 = true;
                                break;
                            }
                        }
                        if (flag2)
                        {
                            int index5 = index2;
                            int index6 = index3 + 1;
                            Main.tile[index5, index6].type = (byte)62;
                            Main.tile[index5, index6].active = true;
                            WorldGen.SquareTileFrame(index5, index6, true);
                            if (Main.netMode == 2)
                                NetMessage.SendTileSquare(-1, index5, index6, 3);
                        }
                    }
                }
            }
            for (int index1 = 0; (double)index1 < (double)(Main.maxTilesX * Main.maxTilesY) * (double)num2; ++index1)
            {
                int index2 = WorldGen.genRand.Next(10, Main.maxTilesX - 10);
                int index3 = WorldGen.genRand.Next((int)Main.worldSurface + 2, Main.maxTilesY - 200);
                int num3 = index2 - 1;
                int num4 = index2 + 2;
                int index4 = index3 - 1;
                int num5 = index3 + 2;
                if (num3 < 10)
                    num3 = 10;
                if (num4 > Main.maxTilesX - 10)
                    num4 = Main.maxTilesX - 10;
                if (index4 < 10)
                    index4 = 10;
                if (num5 > Main.maxTilesY - 10)
                    num5 = Main.maxTilesY - 10;
                if (Main.tile[index2, index3] != null && (int)Main.tile[index2, index3].liquid <= 32)
                {
                    if (Main.tile[index2, index3].active)
                    {
                        if ((int)Main.tile[index2, index3].type == 60)
                        {
                            int grass = (int)Main.tile[index2, index3].type;
                            if (!Main.tile[index2, index4].active && WorldGen.genRand.Next(10) == 0)
                            {
                                WorldGen.PlaceTile(index2, index4, 61, true, false, -1);
                                if (Main.netMode == 2 && Main.tile[index2, index4].active)
                                    NetMessage.SendTileSquare(-1, index2, index4, 1);
                            }
                            bool flag2 = false;
                            for (int i = num3; i < num4; ++i)
                            {
                                for (int j = index4; j < num5; ++j)
                                {
                                    if ((index2 != i || index3 != j) && Main.tile[i, j].active && (int)Main.tile[i, j].type == 59)
                                    {
                                        WorldGen.SpreadGrass(i, j, 59, grass, false);
                                        if ((int)Main.tile[i, j].type == grass)
                                        {
                                            WorldGen.SquareTileFrame(i, j, true);
                                            flag2 = true;
                                        }
                                    }
                                }
                            }
                            if (Main.netMode == 2 && flag2)
                                NetMessage.SendTileSquare(-1, index2, index3, 3);
                        }
                        if ((int)Main.tile[index2, index3].type == 61 && WorldGen.genRand.Next(3) == 0 && (int)Main.tile[index2, index3].frameX < 144)
                        {
                            Main.tile[index2, index3].type = (byte)74;
                            if (Main.netMode == 2)
                                NetMessage.SendTileSquare(-1, index2, index3, 3);
                        }
                        if (((int)Main.tile[index2, index3].type == 60 || (int)Main.tile[index2, index3].type == 62) && WorldGen.genRand.Next(5) == 0 && (!Main.tile[index2, index3 + 1].active && !Main.tile[index2, index3 + 1].lava))
                        {
                            bool flag2 = false;
                            for (int index5 = index3; index5 > index3 - 10; --index5)
                            {
                                if (Main.tile[index2, index5].active && (int)Main.tile[index2, index5].type == 60)
                                {
                                    flag2 = true;
                                    break;
                                }
                            }
                            if (flag2)
                            {
                                int index5 = index2;
                                int index6 = index3 + 1;
                                Main.tile[index5, index6].type = (byte)62;
                                Main.tile[index5, index6].active = true;
                                WorldGen.SquareTileFrame(index5, index6, true);
                                if (Main.netMode == 2)
                                    NetMessage.SendTileSquare(-1, index5, index6, 3);
                            }
                        }
                        if ((int)Main.tile[index2, index3].type == 69 && WorldGen.genRand.Next(3) == 0)
                        {
                            int index5 = index2;
                            int index6 = index3;
                            int num6 = 0;
                            if (Main.tile[index5 + 1, index6].active && (int)Main.tile[index5 + 1, index6].type == 69)
                                ++num6;
                            if (Main.tile[index5 - 1, index6].active && (int)Main.tile[index5 - 1, index6].type == 69)
                                ++num6;
                            if (Main.tile[index5, index6 + 1].active && (int)Main.tile[index5, index6 + 1].type == 69)
                                ++num6;
                            if (Main.tile[index5, index6 - 1].active && (int)Main.tile[index5, index6 - 1].type == 69)
                                ++num6;
                            if (num6 < 3 || (int)Main.tile[index2, index3].type == 60)
                            {
                                switch (WorldGen.genRand.Next(4))
                                {
                                    case 0:
                                        --index6;
                                        break;
                                    case 1:
                                        ++index6;
                                        break;
                                    case 2:
                                        --index5;
                                        break;
                                    case 3:
                                        ++index5;
                                        break;
                                }
                                if (!Main.tile[index5, index6].active)
                                {
                                    int num7 = 0;
                                    if (Main.tile[index5 + 1, index6].active && (int)Main.tile[index5 + 1, index6].type == 69)
                                        ++num7;
                                    if (Main.tile[index5 - 1, index6].active && (int)Main.tile[index5 - 1, index6].type == 69)
                                        ++num7;
                                    if (Main.tile[index5, index6 + 1].active && (int)Main.tile[index5, index6 + 1].type == 69)
                                        ++num7;
                                    if (Main.tile[index5, index6 - 1].active && (int)Main.tile[index5, index6 - 1].type == 69)
                                        ++num7;
                                    if (num7 < 2)
                                    {
                                        int num8 = 7;
                                        int num9 = index5 - num8;
                                        int num10 = index5 + num8;
                                        int num11 = index6 - num8;
                                        int num12 = index6 + num8;
                                        bool flag2 = false;
                                        for (int index7 = num9; index7 < num10; ++index7)
                                        {
                                            for (int index8 = num11; index8 < num12; ++index8)
                                            {
                                                if (Math.Abs(index7 - index5) * 2 + Math.Abs(index8 - index6) < 9 && (Main.tile[index7, index8].active && (int)Main.tile[index7, index8].type == 60) && (Main.tile[index7, index8 - 1].active && (int)Main.tile[index7, index8 - 1].type == 69 && (int)Main.tile[index7, index8 - 1].liquid == 0))
                                                {
                                                    flag2 = true;
                                                    break;
                                                }
                                            }
                                        }
                                        if (flag2)
                                        {
                                            Main.tile[index5, index6].type = (byte)69;
                                            Main.tile[index5, index6].active = true;
                                            WorldGen.SquareTileFrame(index5, index6, true);
                                            if (Main.netMode == 2)
                                                NetMessage.SendTileSquare(-1, index5, index6, 3);
                                        }
                                    }
                                }
                            }
                        }
                        if ((int)Main.tile[index2, index3].type == 70)
                        {
                            int grass = (int)Main.tile[index2, index3].type;
                            if (!Main.tile[index2, index4].active && WorldGen.genRand.Next(10) == 0)
                            {
                                WorldGen.PlaceTile(index2, index4, 71, true, false, -1);
                                if (Main.netMode == 2 && Main.tile[index2, index4].active)
                                    NetMessage.SendTileSquare(-1, index2, index4, 1);
                            }
                            bool flag2 = false;
                            for (int i = num3; i < num4; ++i)
                            {
                                for (int j = index4; j < num5; ++j)
                                {
                                    if ((index2 != i || index3 != j) && Main.tile[i, j].active && (int)Main.tile[i, j].type == 59)
                                    {
                                        WorldGen.SpreadGrass(i, j, 59, grass, false);
                                        if ((int)Main.tile[i, j].type == grass)
                                        {
                                            WorldGen.SquareTileFrame(i, j, true);
                                            flag2 = true;
                                        }
                                    }
                                }
                            }
                            if (Main.netMode == 2 && flag2)
                                NetMessage.SendTileSquare(-1, index2, index3, 3);
                        }
                    }
                    else if (flag1 && WorldGen.spawnNPC > 0)
                        WorldGen.SpawnNPC(index2, index3);
                }
            }
            if (!Main.dayTime)
            {
                float num3 = (float)(Main.maxTilesX / 4200);
                if ((double)Main.rand.Next(8000) < 10.0 * (double)num3)
                {
                    int num4 = 12;
                    Vector2 vector2 = new Vector2((float)((Main.rand.Next(Main.maxTilesX - 50) + 100) * 16), (float)(Main.rand.Next((int)((double)Main.maxTilesY * 0.05)) * 16));
                    float num5 = (float)Main.rand.Next(-100, 101);
                    float num6 = (float)(Main.rand.Next(200) + 100);
                    float num7 = (float)Math.Sqrt((double)num5 * (double)num5 + (double)num6 * (double)num6);
                    float num8 = (float)num4 / num7;
                    float SpeedX = num5 * num8;
                    float SpeedY = num6 * num8;
                    Projectile.NewProjectile(vector2.X, vector2.Y, SpeedX, SpeedY, 12, 1000, 10f, Main.myPlayer);
                }
            }
        }

        public static void PlaceWall(int i, int j, int type, bool mute = false)
        {
            if (Main.tile[i, j] == null)
                Main.tile[i, j] = new Tile();
            if ((int)Main.tile[i, j].wall != type)
            {
                for (int index1 = i - 1; index1 < i + 2; ++index1)
                {
                    for (int index2 = j - 1; index2 < j + 2; ++index2)
                    {
                        if (Main.tile[index1, index2] == null)
                            Main.tile[index1, index2] = new Tile();
                        if ((int)Main.tile[index1, index2].wall > 0 && (int)Main.tile[index1, index2].wall != type)
                            return;
                    }
                }
                Main.tile[i, j].wall = (byte)type;
                WorldGen.SquareWallFrame(i, j, true);
                if (!mute)
                    Main.PlaySound(0, i * 16, j * 16, 1);
            }
        }

        public static void AddPlants()
        {
            for (int i = 0; i < Main.maxTilesX; ++i)
            {
                for (int index = 1; index < Main.maxTilesY; ++index)
                {
                    if ((int)Main.tile[i, index].type == 2 && Main.tile[i, index].active)
                    {
                        if (!Main.tile[i, index - 1].active)
                            WorldGen.PlaceTile(i, index - 1, 3, true, false, -1);
                    }
                    else if ((int)Main.tile[i, index].type == 23 && Main.tile[i, index].active && !Main.tile[i, index - 1].active)
                        WorldGen.PlaceTile(i, index - 1, 24, true, false, -1);
                }
            }
        }

        public static void SpreadGrass(int i, int j, int dirt = 0, int grass = 2, bool repeat = true)
        {
            if ((int)Main.tile[i, j].type == dirt && Main.tile[i, j].active && ((double)j >= Main.worldSurface || grass != 70) && ((double)j < Main.worldSurface || dirt != 0))
            {
                int num1 = i - 1;
                int num2 = i + 2;
                int num3 = j - 1;
                int num4 = j + 2;
                if (num1 < 0)
                    num1 = 0;
                if (num2 > Main.maxTilesX)
                    num2 = Main.maxTilesX;
                if (num3 < 0)
                    num3 = 0;
                if (num4 > Main.maxTilesY)
                    num4 = Main.maxTilesY;
                bool flag = true;
                for (int index1 = num1; index1 < num2; ++index1)
                {
                    for (int index2 = num3; index2 < num4; ++index2)
                    {
                        if (!Main.tile[index1, index2].active || !Main.tileSolid[(int)Main.tile[index1, index2].type])
                        {
                            flag = false;
                            break;
                        }
                    }
                }
                if (!flag && (grass != 23 || (int)Main.tile[i, j - 1].type != 27))
                {
                    Main.tile[i, j].type = (byte)grass;
                    for (int i1 = num1; i1 < num2; ++i1)
                    {
                        for (int j1 = num3; j1 < num4; ++j1)
                        {
                            if (Main.tile[i1, j1].active && (int)Main.tile[i1, j1].type == dirt && repeat)
                                WorldGen.SpreadGrass(i1, j1, dirt, grass, true);
                        }
                    }
                }
            }
        }

        public static void ChasmRunner(int i, int j, int steps, bool makeOrb = false)
        {
            bool flag1 = false;
            bool flag2 = false;
            if (!makeOrb)
                flag1 = true;
            float num1 = (float)steps;
            Vector2 vector2_1;
            vector2_1.X = (float)i;
            vector2_1.Y = (float)j;
            Vector2 vector2_2;
            vector2_2.X = (float)WorldGen.genRand.Next(-10, 11) * 0.1f;
            vector2_2.Y = (float)((double)WorldGen.genRand.Next(11) * 0.200000002980232 + 0.5);
            int num2 = 5;
            double num3 = (double)(WorldGen.genRand.Next(5) + 7);
            while (num3 > 0.0)
            {
                if ((double)num1 > 0.0)
                {
                    num3 = num3 + (double)WorldGen.genRand.Next(3) - (double)WorldGen.genRand.Next(3);
                    if (num3 < 7.0)
                        num3 = 7.0;
                    if (num3 > 20.0)
                        num3 = 20.0;
                    if ((double)num1 == 1.0 && num3 < 10.0)
                        num3 = 10.0;
                }
                else
                    num3 -= (double)WorldGen.genRand.Next(4);
                if ((double)vector2_1.Y > Main.rockLayer && (double)num1 > 0.0)
                    num1 = 0.0f;
                --num1;
                if ((double)num1 > (double)num2)
                {
                    int num4 = (int)((double)vector2_1.X - num3 * 0.5);
                    int num5 = (int)((double)vector2_1.X + num3 * 0.5);
                    int num6 = (int)((double)vector2_1.Y - num3 * 0.5);
                    int num7 = (int)((double)vector2_1.Y + num3 * 0.5);
                    if (num4 < 0)
                        num4 = 0;
                    if (num5 > Main.maxTilesX - 1)
                        num5 = Main.maxTilesX - 1;
                    if (num6 < 0)
                        num6 = 0;
                    if (num7 > Main.maxTilesY)
                        num7 = Main.maxTilesY;
                    for (int index1 = num4; index1 < num5; ++index1)
                    {
                        for (int index2 = num6; index2 < num7; ++index2)
                        {
                            if ((double)Math.Abs((float)index1 - vector2_1.X) + (double)Math.Abs((float)index2 - vector2_1.Y) < num3 * 0.5 * (1.0 + (double)WorldGen.genRand.Next(-10, 11) * 0.015))
                                Main.tile[index1, index2].active = false;
                        }
                    }
                }
                if ((double)num1 <= 0.0)
                {
                    if (!flag1)
                    {
                        flag1 = true;
                        WorldGen.AddShadowOrb((int)vector2_1.X, (int)vector2_1.Y);
                    }
                    else if (!flag2)
                    {
                        flag2 = false;
                        bool flag3 = false;
                        int num4 = 0;
                        while (!flag3)
                        {
                            int x = WorldGen.genRand.Next((int)vector2_1.X - 25, (int)vector2_1.X + 25);
                            int y = WorldGen.genRand.Next((int)vector2_1.Y - 50, (int)vector2_1.Y);
                            if (x < 5)
                                x = 5;
                            if (x > Main.maxTilesX - 5)
                                x = Main.maxTilesX - 5;
                            if (y < 5)
                                y = 5;
                            if (y > Main.maxTilesY - 5)
                                y = Main.maxTilesY - 5;
                            if ((double)y > Main.worldSurface)
                            {
                                WorldGen.Place3x2(x, y, 26);
                                if ((int)Main.tile[x, y].type == 26)
                                {
                                    flag3 = true;
                                }
                                else
                                {
                                    ++num4;
                                    if (num4 >= 10000)
                                        flag3 = true;
                                }
                            }
                            else
                                flag3 = true;
                        }
                    }
                }
                vector2_1 += vector2_2;
                vector2_2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.01f;
                if ((double)vector2_2.X > 0.3)
                    vector2_2.X = 0.3f;
                if ((double)vector2_2.X < -0.3)
                    vector2_2.X = -0.3f;
                int num8 = (int)((double)vector2_1.X - num3 * 1.1);
                int num9 = (int)((double)vector2_1.X + num3 * 1.1);
                int num10 = (int)((double)vector2_1.Y - num3 * 1.1);
                int num11 = (int)((double)vector2_1.Y + num3 * 1.1);
                if (num8 < 1)
                    num8 = 1;
                if (num9 > Main.maxTilesX - 1)
                    num9 = Main.maxTilesX - 1;
                if (num10 < 0)
                    num10 = 0;
                if (num11 > Main.maxTilesY)
                    num11 = Main.maxTilesY;
                for (int index1 = num8; index1 < num9; ++index1)
                {
                    for (int index2 = num10; index2 < num11; ++index2)
                    {
                        if ((double)Math.Abs((float)index1 - vector2_1.X) + (double)Math.Abs((float)index2 - vector2_1.Y) < num3 * 1.1 * (1.0 + (double)WorldGen.genRand.Next(-10, 11) * 0.015))
                        {
                            if ((int)Main.tile[index1, index2].type != 25 && index2 > j + WorldGen.genRand.Next(3, 20))
                                Main.tile[index1, index2].active = true;
                            if (steps <= num2)
                                Main.tile[index1, index2].active = true;
                            if ((int)Main.tile[index1, index2].type != 31)
                                Main.tile[index1, index2].type = (byte)25;
                            if ((int)Main.tile[index1, index2].wall == 2)
                                Main.tile[index1, index2].wall = (byte)0;
                        }
                    }
                }
                for (int i1 = num8; i1 < num9; ++i1)
                {
                    for (int j1 = num10; j1 < num11; ++j1)
                    {
                        if ((double)Math.Abs((float)i1 - vector2_1.X) + (double)Math.Abs((float)j1 - vector2_1.Y) < num3 * 1.1 * (1.0 + (double)WorldGen.genRand.Next(-10, 11) * 0.015))
                        {
                            if ((int)Main.tile[i1, j1].type != 31)
                                Main.tile[i1, j1].type = (byte)25;
                            if (steps <= num2)
                                Main.tile[i1, j1].active = true;
                            if (j1 > j + WorldGen.genRand.Next(3, 20))
                                WorldGen.PlaceWall(i1, j1, 3, true);
                        }
                    }
                }
            }
        }

        public static void TileRunner(int i, int j, double strength, int steps, int type, bool addTile = false, float speedX = 0.0f, float speedY = 0.0f, bool noYChange = false, bool overRide = true)
        {
            double num1 = strength;
            float num2 = (float)steps;
            Vector2 vector2_1;
            vector2_1.X = (float)i;
            vector2_1.Y = (float)j;
            Vector2 vector2_2;
            vector2_2.X = (float)WorldGen.genRand.Next(-10, 11) * 0.1f;
            vector2_2.Y = (float)WorldGen.genRand.Next(-10, 11) * 0.1f;
            if ((double)speedX != 0.0 || (double)speedY != 0.0)
            {
                vector2_2.X = speedX;
                vector2_2.Y = speedY;
            }
            while (num1 > 0.0 && (double)num2 > 0.0)
            {
                if ((double)vector2_1.Y < 0.0 && (double)num2 > 0.0 && type == 59)
                    num2 = 0.0f;
                num1 = strength * ((double)num2 / (double)steps);
                --num2;
                int num3 = (int)((double)vector2_1.X - num1 * 0.5);
                int num4 = (int)((double)vector2_1.X + num1 * 0.5);
                int num5 = (int)((double)vector2_1.Y - num1 * 0.5);
                int num6 = (int)((double)vector2_1.Y + num1 * 0.5);
                if (num3 < 0)
                    num3 = 0;
                if (num4 > Main.maxTilesX)
                    num4 = Main.maxTilesX;
                if (num5 < 0)
                    num5 = 0;
                if (num6 > Main.maxTilesY)
                    num6 = Main.maxTilesY;
                for (int index1 = num3; index1 < num4; ++index1)
                {
                    for (int index2 = num5; index2 < num6; ++index2)
                    {
                        if ((double)Math.Abs((float)index1 - vector2_1.X) + (double)Math.Abs((float)index2 - vector2_1.Y) < strength * 0.5 * (1.0 + (double)WorldGen.genRand.Next(-10, 11) * 0.015))
                        {
                            if (type < 0)
                            {
                                if (type == -2 && Main.tile[index1, index2].active && (index2 < WorldGen.waterLine || index2 > WorldGen.lavaLine))
                                {
                                    Main.tile[index1, index2].liquid = byte.MaxValue;
                                    if (index2 > WorldGen.lavaLine)
                                        Main.tile[index1, index2].lava = true;
                                }
                                Main.tile[index1, index2].active = false;
                            }
                            else
                            {
                                if ((overRide || !Main.tile[index1, index2].active) && (type != 40 || (int)Main.tile[index1, index2].type != 53) && ((!Main.tileStone[type] || (int)Main.tile[index1, index2].type == 1) && (int)Main.tile[index1, index2].type != 45 && ((int)Main.tile[index1, index2].type != 1 || type != 59 || (double)index2 >= Main.worldSurface + (double)WorldGen.genRand.Next(-50, 50))) && ((int)Main.tile[index1, index2].type != 53 || type != 59))
                                    Main.tile[index1, index2].type = (byte)type;
                                if (addTile)
                                {
                                    Main.tile[index1, index2].active = true;
                                    Main.tile[index1, index2].liquid = (byte)0;
                                    Main.tile[index1, index2].lava = false;
                                }
                                if (noYChange && (double)index2 < Main.worldSurface && type != 59)
                                    Main.tile[index1, index2].wall = (byte)2;
                                if (type == 59 && (index2 > WorldGen.waterLine && (int)Main.tile[index1, index2].liquid > 0))
                                {
                                    Main.tile[index1, index2].lava = false;
                                    Main.tile[index1, index2].liquid = (byte)0;
                                }
                            }
                        }
                    }
                }
                vector2_1 += vector2_2;
                if (num1 > 50.0)
                {
                    vector2_1 += vector2_2;
                    --num2;
                    vector2_2.Y += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                    vector2_2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                    if (num1 > 100.0)
                    {
                        vector2_1 += vector2_2;
                        --num2;
                        vector2_2.Y += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                        vector2_2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                        if (num1 > 150.0)
                        {
                            vector2_1 += vector2_2;
                            --num2;
                            vector2_2.Y += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                            vector2_2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                            if (num1 > 200.0)
                            {
                                vector2_1 += vector2_2;
                                --num2;
                                vector2_2.Y += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                                vector2_2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                                if (num1 > 250.0)
                                {
                                    vector2_1 += vector2_2;
                                    --num2;
                                    vector2_2.Y += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                                    vector2_2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                                    if (num1 > 300.0)
                                    {
                                        vector2_1 += vector2_2;
                                        --num2;
                                        vector2_2.Y += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                                        vector2_2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                                        if (num1 > 400.0)
                                        {
                                            vector2_1 += vector2_2;
                                            --num2;
                                            vector2_2.Y += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                                            vector2_2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                                            if (num1 > 500.0)
                                            {
                                                vector2_1 += vector2_2;
                                                --num2;
                                                vector2_2.Y += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                                                vector2_2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                                                if (num1 > 600.0)
                                                {
                                                    vector2_1 += vector2_2;
                                                    --num2;
                                                    vector2_2.Y += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                                                    vector2_2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                                                    if (num1 > 700.0)
                                                    {
                                                        vector2_1 += vector2_2;
                                                        --num2;
                                                        vector2_2.Y += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                                                        vector2_2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                                                        if (num1 > 800.0)
                                                        {
                                                            vector2_1 += vector2_2;
                                                            --num2;
                                                            vector2_2.Y += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                                                            vector2_2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                                                            if (num1 > 900.0)
                                                            {
                                                                vector2_1 += vector2_2;
                                                                --num2;
                                                                vector2_2.Y += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                                                                vector2_2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                vector2_2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                if ((double)vector2_2.X > 1.0)
                    vector2_2.X = 1f;
                if ((double)vector2_2.X < -1.0)
                    vector2_2.X = -1f;
                if (!noYChange)
                {
                    vector2_2.Y += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                    if ((double)vector2_2.Y > 1.0)
                        vector2_2.Y = 1f;
                    if ((double)vector2_2.Y < -1.0)
                        vector2_2.Y = -1f;
                }
                else if (type != 59 && num1 < 3.0)
                {
                    if ((double)vector2_2.Y > 1.0)
                        vector2_2.Y = 1f;
                    if ((double)vector2_2.Y < -1.0)
                        vector2_2.Y = -1f;
                }
                if (type == 59 && !noYChange)
                {
                    if ((double)vector2_2.Y > 0.5)
                        vector2_2.Y = 0.5f;
                    if ((double)vector2_2.Y < -0.5)
                        vector2_2.Y = -0.5f;
                    if ((double)vector2_1.Y < Main.rockLayer + 100.0)
                        vector2_2.Y = 1f;
                    if ((double)vector2_1.Y > (double)(Main.maxTilesY - 300))
                        vector2_2.Y = -1f;
                }
            }
        }

        public static void FloatingIsland(int i, int j)
        {
            double num1 = (double)WorldGen.genRand.Next(80, 120);
            float num2 = (float)WorldGen.genRand.Next(20, 25);
            Vector2 vector2_1;
            vector2_1.X = (float)i;
            vector2_1.Y = (float)j;
            Vector2 vector2_2;
            vector2_2.X = (float)WorldGen.genRand.Next(-20, 21) * 0.2f;
            while ((double)vector2_2.X > -2.0 && (double)vector2_2.X < 2.0)
                vector2_2.X = (float)WorldGen.genRand.Next(-20, 21) * 0.2f;
            vector2_2.Y = (float)WorldGen.genRand.Next(-20, -10) * 0.02f;
            while (num1 > 0.0 && (double)num2 > 0.0)
            {
                num1 -= (double)WorldGen.genRand.Next(4);
                --num2;
                int num3 = (int)((double)vector2_1.X - num1 * 0.5);
                int num4 = (int)((double)vector2_1.X + num1 * 0.5);
                int num5 = (int)((double)vector2_1.Y - num1 * 0.5);
                int num6 = (int)((double)vector2_1.Y + num1 * 0.5);
                if (num3 < 0)
                    num3 = 0;
                if (num4 > Main.maxTilesX)
                    num4 = Main.maxTilesX;
                if (num5 < 0)
                    num5 = 0;
                if (num6 > Main.maxTilesY)
                    num6 = Main.maxTilesY;
                double num7 = num1 * (double)WorldGen.genRand.Next(80, 120) * 0.01;
                float num8 = vector2_1.Y + 1f;
                for (int index1 = num3; index1 < num4; ++index1)
                {
                    if (WorldGen.genRand.Next(2) == 0)
                        num8 += (float)WorldGen.genRand.Next(-1, 2);
                    if ((double)num8 < (double)vector2_1.Y)
                        num8 = vector2_1.Y;
                    if ((double)num8 > (double)vector2_1.Y + 2.0)
                        num8 = vector2_1.Y + 2f;
                    for (int index2 = num5; index2 < num6; ++index2)
                    {
                        if ((double)index2 > (double)num8)
                        {
                            float num9 = Math.Abs((float)index1 - vector2_1.X);
                            float num10 = Math.Abs((float)index2 - vector2_1.Y) * 2f;
                            if (Math.Sqrt((double)num9 * (double)num9 + (double)num10 * (double)num10) < num7 * 0.4)
                            {
                                Main.tile[index1, index2].active = true;
                                if ((int)Main.tile[index1, index2].type == 59)
                                    Main.tile[index1, index2].type = (byte)0;
                            }
                        }
                    }
                }
                WorldGen.TileRunner(WorldGen.genRand.Next(num3 + 10, num4 - 10), (int)((double)vector2_1.Y + num7 * 0.1 + 5.0), (double)WorldGen.genRand.Next(5, 10), WorldGen.genRand.Next(10, 15), 0, true, 0.0f, 2f, true, true);
                int num11 = (int)((double)vector2_1.X - num1 * 0.4);
                int num12 = (int)((double)vector2_1.X + num1 * 0.4);
                int num13 = (int)((double)vector2_1.Y - num1 * 0.4);
                int num14 = (int)((double)vector2_1.Y + num1 * 0.4);
                if (num11 < 0)
                    num11 = 0;
                if (num12 > Main.maxTilesX)
                    num12 = Main.maxTilesX;
                if (num13 < 0)
                    num13 = 0;
                if (num14 > Main.maxTilesY)
                    num14 = Main.maxTilesY;
                double num15 = num1 * (double)WorldGen.genRand.Next(80, 120) * 0.01;
                for (int index1 = num11; index1 < num12; ++index1)
                {
                    for (int index2 = num13; index2 < num14; ++index2)
                    {
                        if ((double)index2 > (double)vector2_1.Y + 2.0)
                        {
                            float num9 = Math.Abs((float)index1 - vector2_1.X);
                            float num10 = Math.Abs((float)index2 - vector2_1.Y) * 2f;
                            if (Math.Sqrt((double)num9 * (double)num9 + (double)num10 * (double)num10) < num15 * 0.4)
                                Main.tile[index1, index2].wall = (byte)2;
                        }
                    }
                }
                vector2_1 += vector2_2;
                vector2_2.Y += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                if ((double)vector2_2.X > 1.0)
                    vector2_2.X = 1f;
                if ((double)vector2_2.X < -1.0)
                    vector2_2.X = -1f;
                if ((double)vector2_2.Y > 0.2)
                    vector2_2.Y = -0.2f;
                if ((double)vector2_2.Y < -0.2)
                    vector2_2.Y = -0.2f;
            }
        }

        public static void IslandHouse(int i, int j)
        {
            byte num1 = (byte)WorldGen.genRand.Next(45, 48);
            byte num2 = (byte)WorldGen.genRand.Next(10, 13);
            Vector2 vector2 = new Vector2((float)i, (float)j);
            int num3 = 1;
            if (WorldGen.genRand.Next(2) == 0)
                num3 = -1;
            int num4 = WorldGen.genRand.Next(7, 12);
            int num5 = WorldGen.genRand.Next(5, 7);
            vector2.X = (float)(i + (num4 + 2) * num3);
            for (int index = j - 15; index < j + 30; ++index)
            {
                if (Main.tile[(int)vector2.X, index].active)
                {
                    vector2.Y = (float)(index - 1);
                    break;
                }
            }
            vector2.X = (float)i;
            int num6 = (int)((double)vector2.X - (double)num4 - 2.0);
            int num7 = (int)((double)vector2.X + (double)num4 + 2.0);
            int num8 = (int)((double)vector2.Y - (double)num5 - 2.0);
            int num9 = (int)((double)vector2.Y + 2.0 + (double)WorldGen.genRand.Next(3, 5));
            if (num6 < 0)
                num6 = 0;
            if (num7 > Main.maxTilesX)
                num7 = Main.maxTilesX;
            if (num8 < 0)
                num8 = 0;
            if (num9 > Main.maxTilesY)
                num9 = Main.maxTilesY;
            for (int index1 = num6; index1 <= num7; ++index1)
            {
                for (int index2 = num8; index2 < num9; ++index2)
                {
                    Main.tile[index1, index2].active = true;
                    Main.tile[index1, index2].type = num1;
                    Main.tile[index1, index2].wall = (byte)0;
                }
            }
            int num10 = (int)((double)vector2.X - (double)num4);
            int num11 = (int)((double)vector2.X + (double)num4);
            int num12 = (int)((double)vector2.Y - (double)num5);
            int num13 = (int)((double)vector2.Y + 1.0);
            if (num10 < 0)
                num10 = 0;
            if (num11 > Main.maxTilesX)
                num11 = Main.maxTilesX;
            if (num12 < 0)
                num12 = 0;
            if (num13 > Main.maxTilesY)
                num13 = Main.maxTilesY;
            for (int index1 = num10; index1 <= num11; ++index1)
            {
                for (int index2 = num12; index2 < num13; ++index2)
                {
                    if ((int)Main.tile[index1, index2].wall == 0)
                    {
                        Main.tile[index1, index2].active = false;
                        Main.tile[index1, index2].wall = num2;
                    }
                }
            }
            int i1 = i + (num4 + 1) * num3;
            int j1 = (int)vector2.Y;
            for (int index = i1 - 2; index <= i1 + 2; ++index)
            {
                Main.tile[index, j1].active = false;
                Main.tile[index, j1 - 1].active = false;
                Main.tile[index, j1 - 2].active = false;
            }
            WorldGen.PlaceTile(i1, j1, 10, true, false, -1);
            int contain = 0;
            int num14 = WorldGen.houseCount;
            if (num14 > 2)
                num14 = WorldGen.genRand.Next(3);
            if (num14 == 0)
                contain = 159;
            else if (num14 == 1)
                contain = 65;
            else if (num14 == 2)
                contain = 158;
            WorldGen.AddBuriedChest(i, j1 - 3, contain);
            ++WorldGen.houseCount;
        }

        public static void Mountinater(int i, int j)
        {
            double num1 = (double)WorldGen.genRand.Next(80, 120);
            float num2 = (float)WorldGen.genRand.Next(40, 55);
            Vector2 vector2_1;
            vector2_1.X = (float)i;
            vector2_1.Y = (float)j + num2 / 2f;
            Vector2 vector2_2;
            vector2_2.X = (float)WorldGen.genRand.Next(-10, 11) * 0.1f;
            vector2_2.Y = (float)WorldGen.genRand.Next(-20, -10) * 0.1f;
            while (num1 > 0.0 && (double)num2 > 0.0)
            {
                num1 -= (double)WorldGen.genRand.Next(4);
                --num2;
                int num3 = (int)((double)vector2_1.X - num1 * 0.5);
                int num4 = (int)((double)vector2_1.X + num1 * 0.5);
                int num5 = (int)((double)vector2_1.Y - num1 * 0.5);
                int num6 = (int)((double)vector2_1.Y + num1 * 0.5);
                if (num3 < 0)
                    num3 = 0;
                if (num4 > Main.maxTilesX)
                    num4 = Main.maxTilesX;
                if (num5 < 0)
                    num5 = 0;
                if (num6 > Main.maxTilesY)
                    num6 = Main.maxTilesY;
                double num7 = num1 * (double)WorldGen.genRand.Next(80, 120) * 0.01;
                for (int index1 = num3; index1 < num4; ++index1)
                {
                    for (int index2 = num5; index2 < num6; ++index2)
                    {
                        float num8 = Math.Abs((float)index1 - vector2_1.X);
                        float num9 = Math.Abs((float)index2 - vector2_1.Y);
                        if (Math.Sqrt((double)num8 * (double)num8 + (double)num9 * (double)num9) < num7 * 0.4 && !Main.tile[index1, index2].active)
                        {
                            Main.tile[index1, index2].active = true;
                            Main.tile[index1, index2].type = (byte)0;
                        }
                    }
                }
                vector2_1 += vector2_2;
                vector2_2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                vector2_2.Y += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                if ((double)vector2_2.X > 0.5)
                    vector2_2.X = 0.5f;
                if ((double)vector2_2.X < -0.5)
                    vector2_2.X = -0.5f;
                if ((double)vector2_2.Y > -0.5)
                    vector2_2.Y = -0.5f;
                if ((double)vector2_2.Y < -1.5)
                    vector2_2.Y = -1.5f;
            }
        }

        public static void Lakinater(int i, int j)
        {
            double num1 = (double)WorldGen.genRand.Next(25, 50);
            double num2 = num1;
            float num3 = (float)WorldGen.genRand.Next(30, 80);
            if (WorldGen.genRand.Next(5) == 0)
            {
                num1 *= 1.5;
                num2 *= 1.5;
                num3 *= 1.2f;
            }
            Vector2 vector2_1;
            vector2_1.X = (float)i;
            vector2_1.Y = (float)j - num3 * 0.3f;
            Vector2 vector2_2;
            vector2_2.X = (float)WorldGen.genRand.Next(-10, 11) * 0.1f;
            vector2_2.Y = (float)WorldGen.genRand.Next(-20, -10) * 0.1f;
            while (num1 > 0.0 && (double)num3 > 0.0)
            {
                if ((double)vector2_1.Y + num2 * 0.5 > Main.worldSurface)
                    num3 = 0.0f;
                num1 -= (double)WorldGen.genRand.Next(3);
                --num3;
                int num4 = (int)((double)vector2_1.X - num1 * 0.5);
                int num5 = (int)((double)vector2_1.X + num1 * 0.5);
                int num6 = (int)((double)vector2_1.Y - num1 * 0.5);
                int num7 = (int)((double)vector2_1.Y + num1 * 0.5);
                if (num4 < 0)
                    num4 = 0;
                if (num5 > Main.maxTilesX)
                    num5 = Main.maxTilesX;
                if (num6 < 0)
                    num6 = 0;
                if (num7 > Main.maxTilesY)
                    num7 = Main.maxTilesY;
                num2 = num1 * (double)WorldGen.genRand.Next(80, 120) * 0.01;
                for (int index1 = num4; index1 < num5; ++index1)
                {
                    for (int index2 = num6; index2 < num7; ++index2)
                    {
                        float num8 = Math.Abs((float)index1 - vector2_1.X);
                        float num9 = Math.Abs((float)index2 - vector2_1.Y);
                        if (Math.Sqrt((double)num8 * (double)num8 + (double)num9 * (double)num9) < num2 * 0.4)
                        {
                            if (Main.tile[index1, index2].active)
                                Main.tile[index1, index2].liquid = byte.MaxValue;
                            Main.tile[index1, index2].active = false;
                        }
                    }
                }
                vector2_1 += vector2_2;
                vector2_2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                vector2_2.Y += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                if ((double)vector2_2.X > 0.5)
                    vector2_2.X = 0.5f;
                if ((double)vector2_2.X < -0.5)
                    vector2_2.X = -0.5f;
                if ((double)vector2_2.Y > 1.5)
                    vector2_2.Y = 1.5f;
                if ((double)vector2_2.Y < 0.5)
                    vector2_2.Y = 0.5f;
            }
        }

        public static void ShroomPatch(int i, int j)
        {
            double num1 = (double)WorldGen.genRand.Next(40, 70);
            double num2 = num1;
            float num3 = (float)WorldGen.genRand.Next(10, 20);
            if (WorldGen.genRand.Next(5) == 0)
            {
                num1 *= 1.5;
                double num4 = num2 * 1.5;
                num3 *= 1.2f;
            }
            Vector2 vector2_1;
            vector2_1.X = (float)i;
            vector2_1.Y = (float)j - num3 * 0.3f;
            Vector2 vector2_2;
            vector2_2.X = (float)WorldGen.genRand.Next(-10, 11) * 0.1f;
            vector2_2.Y = (float)WorldGen.genRand.Next(-20, -10) * 0.1f;
            while (num1 > 0.0 && (double)num3 > 0.0)
            {
                num1 -= (double)WorldGen.genRand.Next(3);
                --num3;
                int num4 = (int)((double)vector2_1.X - num1 * 0.5);
                int num5 = (int)((double)vector2_1.X + num1 * 0.5);
                int num6 = (int)((double)vector2_1.Y - num1 * 0.5);
                int num7 = (int)((double)vector2_1.Y + num1 * 0.5);
                if (num4 < 0)
                    num4 = 0;
                if (num5 > Main.maxTilesX)
                    num5 = Main.maxTilesX;
                if (num6 < 0)
                    num6 = 0;
                if (num7 > Main.maxTilesY)
                    num7 = Main.maxTilesY;
                double num8 = num1 * (double)WorldGen.genRand.Next(80, 120) * 0.01;
                for (int index1 = num4; index1 < num5; ++index1)
                {
                    for (int index2 = num6; index2 < num7; ++index2)
                    {
                        float num9 = Math.Abs((float)index1 - vector2_1.X);
                        float num10 = Math.Abs((float)(((double)index2 - (double)vector2_1.Y) * 2.29999995231628));
                        if (Math.Sqrt((double)num9 * (double)num9 + (double)num10 * (double)num10) < num8 * 0.4)
                        {
                            if ((double)index2 < (double)vector2_1.Y + num8 * 0.05)
                            {
                                if ((int)Main.tile[index1, index2].type != 59)
                                    Main.tile[index1, index2].active = false;
                            }
                            else
                                Main.tile[index1, index2].type = (byte)59;
                            Main.tile[index1, index2].liquid = (byte)0;
                            Main.tile[index1, index2].lava = false;
                        }
                    }
                }
                vector2_1 += vector2_2;
                vector2_2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                vector2_2.Y += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                if ((double)vector2_2.X > 1.0)
                    vector2_2.X = 0.1f;
                if ((double)vector2_2.X < -1.0)
                    vector2_2.X = -1f;
                if ((double)vector2_2.Y > 1.0)
                    vector2_2.Y = 1f;
                if ((double)vector2_2.Y < -1.0)
                    vector2_2.Y = -1f;
            }
        }

        public static void Cavinator(int i, int j, int steps)
        {
            double num1 = (double)WorldGen.genRand.Next(7, 15);
            int num2 = 1;
            if (WorldGen.genRand.Next(2) == 0)
                num2 = -1;
            Vector2 vector2_1;
            vector2_1.X = (float)i;
            vector2_1.Y = (float)j;
            int num3 = WorldGen.genRand.Next(20, 40);
            Vector2 vector2_2;
            vector2_2.Y = (float)WorldGen.genRand.Next(10, 20) * 0.01f;
            vector2_2.X = (float)num2;
            while (num3 > 0)
            {
                --num3;
                int num4 = (int)((double)vector2_1.X - num1 * 0.5);
                int num5 = (int)((double)vector2_1.X + num1 * 0.5);
                int num6 = (int)((double)vector2_1.Y - num1 * 0.5);
                int num7 = (int)((double)vector2_1.Y + num1 * 0.5);
                if (num4 < 0)
                    num4 = 0;
                if (num5 > Main.maxTilesX)
                    num5 = Main.maxTilesX;
                if (num6 < 0)
                    num6 = 0;
                if (num7 > Main.maxTilesY)
                    num7 = Main.maxTilesY;
                double num8 = num1 * (double)WorldGen.genRand.Next(80, 120) * 0.01;
                for (int index1 = num4; index1 < num5; ++index1)
                {
                    for (int index2 = num6; index2 < num7; ++index2)
                    {
                        float num9 = Math.Abs((float)index1 - vector2_1.X);
                        float num10 = Math.Abs((float)index2 - vector2_1.Y);
                        if (Math.Sqrt((double)num9 * (double)num9 + (double)num10 * (double)num10) < num8 * 0.4)
                            Main.tile[index1, index2].active = false;
                    }
                }
                vector2_1 += vector2_2;
                vector2_2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                vector2_2.Y += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                if ((double)vector2_2.X > (double)num2 + 0.5)
                    vector2_2.X = (float)num2 + 0.5f;
                if ((double)vector2_2.X < (double)num2 - 0.5)
                    vector2_2.X = (float)num2 - 0.5f;
                if ((double)vector2_2.Y > 2.0)
                    vector2_2.Y = 2f;
                if ((double)vector2_2.Y < 0.0)
                    vector2_2.Y = 0.0f;
            }
            if (steps > 0 && (double)(int)vector2_1.Y < Main.rockLayer + 50.0)
                WorldGen.Cavinator((int)vector2_1.X, (int)vector2_1.Y, steps - 1);
        }

        public static void CaveOpenater(int i, int j)
        {
            double num1 = (double)WorldGen.genRand.Next(7, 12);
            int num2 = 1;
            if (WorldGen.genRand.Next(2) == 0)
                num2 = -1;
            Vector2 vector2_1;
            vector2_1.X = (float)i;
            vector2_1.Y = (float)j;
            int num3 = 100;
            Vector2 vector2_2;
            vector2_2.Y = 0.0f;
            vector2_2.X = (float)num2;
            while (num3 > 0)
            {
                if ((int)Main.tile[(int)vector2_1.X, (int)vector2_1.Y].wall == 0)
                    num3 = 0;
                --num3;
                int num4 = (int)((double)vector2_1.X - num1 * 0.5);
                int num5 = (int)((double)vector2_1.X + num1 * 0.5);
                int num6 = (int)((double)vector2_1.Y - num1 * 0.5);
                int num7 = (int)((double)vector2_1.Y + num1 * 0.5);
                if (num4 < 0)
                    num4 = 0;
                if (num5 > Main.maxTilesX)
                    num5 = Main.maxTilesX;
                if (num6 < 0)
                    num6 = 0;
                if (num7 > Main.maxTilesY)
                    num7 = Main.maxTilesY;
                double num8 = num1 * (double)WorldGen.genRand.Next(80, 120) * 0.01;
                for (int index1 = num4; index1 < num5; ++index1)
                {
                    for (int index2 = num6; index2 < num7; ++index2)
                    {
                        float num9 = Math.Abs((float)index1 - vector2_1.X);
                        float num10 = Math.Abs((float)index2 - vector2_1.Y);
                        if (Math.Sqrt((double)num9 * (double)num9 + (double)num10 * (double)num10) < num8 * 0.4)
                            Main.tile[index1, index2].active = false;
                    }
                }
                vector2_1 += vector2_2;
                vector2_2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                vector2_2.Y += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
                if ((double)vector2_2.X > (double)num2 + 0.5)
                    vector2_2.X = (float)num2 + 0.5f;
                if ((double)vector2_2.X < (double)num2 - 0.5)
                    vector2_2.X = (float)num2 - 0.5f;
                if ((double)vector2_2.Y > 0.0)
                    vector2_2.Y = 0.0f;
                if ((double)vector2_2.Y < -0.5)
                    vector2_2.Y = -0.5f;
            }
        }

        public static void SquareTileFrame(int i, int j, bool resetFrame = true)
        {
            WorldGen.TileFrame(i - 1, j - 1, false, false);
            WorldGen.TileFrame(i - 1, j, false, false);
            WorldGen.TileFrame(i - 1, j + 1, false, false);
            WorldGen.TileFrame(i, j - 1, false, false);
            WorldGen.TileFrame(i, j, resetFrame, false);
            WorldGen.TileFrame(i, j + 1, false, false);
            WorldGen.TileFrame(i + 1, j - 1, false, false);
            WorldGen.TileFrame(i + 1, j, false, false);
            WorldGen.TileFrame(i + 1, j + 1, false, false);
        }

        public static void SquareWallFrame(int i, int j, bool resetFrame = true)
        {
            WorldGen.WallFrame(i - 1, j - 1, false);
            WorldGen.WallFrame(i - 1, j, false);
            WorldGen.WallFrame(i - 1, j + 1, false);
            WorldGen.WallFrame(i, j - 1, false);
            WorldGen.WallFrame(i, j, resetFrame);
            WorldGen.WallFrame(i, j + 1, false);
            WorldGen.WallFrame(i + 1, j - 1, false);
            WorldGen.WallFrame(i + 1, j, false);
            WorldGen.WallFrame(i + 1, j + 1, false);
        }

        public static void SectionTileFrame(int startX, int startY, int endX, int endY)
        {
            int num1 = startX * 200;
            int num2 = (endX + 1) * 200;
            int num3 = startY * 150;
            int num4 = (endY + 1) * 150;
            if (num1 < 1)
                num1 = 1;
            if (num3 < 1)
                num3 = 1;
            if (num1 > Main.maxTilesX - 2)
                num1 = Main.maxTilesX - 2;
            if (num3 > Main.maxTilesY - 2)
                num3 = Main.maxTilesY - 2;
            for (int i = num1 - 1; i < num2 + 1; ++i)
            {
                for (int j = num3 - 1; j < num4 + 1; ++j)
                {
                    if (Main.tile[i, j] == null)
                        Main.tile[i, j] = new Tile();
                    WorldGen.TileFrame(i, j, true, true);
                    WorldGen.WallFrame(i, j, true);
                }
            }
        }

        public static void RangeFrame(int startX, int startY, int endX, int endY)
        {
            int num1 = startX;
            int num2 = endX + 1;
            int num3 = startY;
            int num4 = endY + 1;
            for (int i = num1 - 1; i < num2 + 1; ++i)
            {
                for (int j = num3 - 1; j < num4 + 1; ++j)
                {
                    WorldGen.TileFrame(i, j, false, false);
                    WorldGen.WallFrame(i, j, false);
                }
            }
        }

        public static void WaterCheck()
        {
            Liquid.numLiquid = 0;
            LiquidBuffer.numLiquidBuffer = 0;
            for (int index1 = 1; index1 < Main.maxTilesX - 1; ++index1)
            {
                for (int index2 = Main.maxTilesY - 2; index2 > 0; --index2)
                {
                    Main.tile[index1, index2].checkingLiquid = false;
                    if ((int)Main.tile[index1, index2].liquid > 0 && Main.tile[index1, index2].active && Main.tileSolid[(int)Main.tile[index1, index2].type] && !Main.tileSolidTop[(int)Main.tile[index1, index2].type])
                        Main.tile[index1, index2].liquid = (byte)0;
                    else if ((int)Main.tile[index1, index2].liquid > 0)
                    {
                        if (Main.tile[index1, index2].active)
                        {
                            if (Main.tileWaterDeath[(int)Main.tile[index1, index2].type])
                                WorldGen.KillTile(index1, index2, false, false, false);
                            if (Main.tile[index1, index2].lava && Main.tileLavaDeath[(int)Main.tile[index1, index2].type])
                                WorldGen.KillTile(index1, index2, false, false, false);
                        }
                        if ((!Main.tile[index1, index2 + 1].active || !Main.tileSolid[(int)Main.tile[index1, index2 + 1].type] || Main.tileSolidTop[(int)Main.tile[index1, index2 + 1].type]) && (int)Main.tile[index1, index2 + 1].liquid < (int)byte.MaxValue)
                        {
                            if ((int)Main.tile[index1, index2 + 1].liquid > 250)
                                Main.tile[index1, index2 + 1].liquid = byte.MaxValue;
                            else
                                Liquid.AddWater(index1, index2);
                        }
                        if ((!Main.tile[index1 - 1, index2].active || !Main.tileSolid[(int)Main.tile[index1 - 1, index2].type] || Main.tileSolidTop[(int)Main.tile[index1 - 1, index2].type]) && (int)Main.tile[index1 - 1, index2].liquid != (int)Main.tile[index1, index2].liquid)
                            Liquid.AddWater(index1, index2);
                        else if ((!Main.tile[index1 + 1, index2].active || !Main.tileSolid[(int)Main.tile[index1 + 1, index2].type] || Main.tileSolidTop[(int)Main.tile[index1 + 1, index2].type]) && (int)Main.tile[index1 + 1, index2].liquid != (int)Main.tile[index1, index2].liquid)
                            Liquid.AddWater(index1, index2);
                        if (Main.tile[index1, index2].lava)
                        {
                            if ((int)Main.tile[index1 - 1, index2].liquid > 0 && !Main.tile[index1 - 1, index2].lava)
                                Liquid.AddWater(index1, index2);
                            else if ((int)Main.tile[index1 + 1, index2].liquid > 0 && !Main.tile[index1 + 1, index2].lava)
                                Liquid.AddWater(index1, index2);
                            else if ((int)Main.tile[index1, index2 - 1].liquid > 0 && !Main.tile[index1, index2 - 1].lava)
                                Liquid.AddWater(index1, index2);
                            else if ((int)Main.tile[index1, index2 + 1].liquid > 0 && !Main.tile[index1, index2 + 1].lava)
                                Liquid.AddWater(index1, index2);
                        }
                    }
                }
            }
        }

        public static void EveryTileFrame()
        {
            WorldGen.noLiquidCheck = true;
            for (int i = 0; i < Main.maxTilesX; ++i)
            {
                Main.statusText = "Finding tile frames: " + (object)(int)((double)((float)i / (float)Main.maxTilesX) * 100.0 + 1.0) + "%";
                for (int j = 0; j < Main.maxTilesY; ++j)
                {
                    WorldGen.TileFrame(i, j, true, false);
                    WorldGen.WallFrame(i, j, true);
                }
            }
            WorldGen.noLiquidCheck = false;
        }

        public static void PlantCheck(int i, int j)
        {
            int num1 = -1;
            int num2 = -1;
            int num3 = -1;
            int num4 = -1;
            int num5 = -1;
            int num6 = -1;
            int num7 = -1;
            int num8 = -1;
            int num9 = (int)Main.tile[i, j].type;
            if (i - 1 < 0)
            {
                num1 = num9;
                num4 = num9;
                num6 = num9;
            }
            if (i + 1 >= Main.maxTilesX)
            {
                num3 = num9;
                num5 = num9;
                num8 = num9;
            }
            if (j - 1 < 0)
            {
                num1 = num9;
                num2 = num9;
                num3 = num9;
            }
            if (j + 1 >= Main.maxTilesY)
            {
                num6 = num9;
                num7 = num9;
                num8 = num9;
            }
            if (i - 1 >= 0 && Main.tile[i - 1, j] != null && Main.tile[i - 1, j].active)
                num4 = (int)Main.tile[i - 1, j].type;
            if (i + 1 < Main.maxTilesX && Main.tile[i + 1, j] != null && Main.tile[i + 1, j].active)
                num5 = (int)Main.tile[i + 1, j].type;
            if (j - 1 >= 0 && Main.tile[i, j - 1] != null && Main.tile[i, j - 1].active)
                num2 = (int)Main.tile[i, j - 1].type;
            if (j + 1 < Main.maxTilesY && Main.tile[i, j + 1] != null && Main.tile[i, j + 1].active)
                num7 = (int)Main.tile[i, j + 1].type;
            if (i - 1 >= 0 && j - 1 >= 0 && Main.tile[i - 1, j - 1] != null && Main.tile[i - 1, j - 1].active)
                num1 = (int)Main.tile[i - 1, j - 1].type;
            if (i + 1 < Main.maxTilesX && j - 1 >= 0 && Main.tile[i + 1, j - 1] != null && Main.tile[i + 1, j - 1].active)
                num3 = (int)Main.tile[i + 1, j - 1].type;
            if (i - 1 >= 0 && j + 1 < Main.maxTilesY && Main.tile[i - 1, j + 1] != null && Main.tile[i - 1, j + 1].active)
                num6 = (int)Main.tile[i - 1, j + 1].type;
            if (i + 1 < Main.maxTilesX && j + 1 < Main.maxTilesY && Main.tile[i + 1, j + 1] != null && Main.tile[i + 1, j + 1].active)
                num8 = (int)Main.tile[i + 1, j + 1].type;
            if (num9 == 3 && num7 != 2 && num7 != 78 || (num9 == 24 && num7 != 23 || num9 == 61 && num7 != 60) || (num9 == 71 && num7 != 70 || num9 == 73 && num7 != 2 && num7 != 78) || num9 == 74 && num7 != 60)
                WorldGen.KillTile(i, j, false, false, false);
        }

        public static void WallFrame(int i, int j, bool resetFrame = false)
        {
            if (i >= 0 && j >= 0 && (i < Main.maxTilesX && j < Main.maxTilesY) && Main.tile[i, j] != null && (int)Main.tile[i, j].wall > 0)
            {
                int num1 = -1;
                int num2 = -1;
                int num3 = -1;
                int num4 = -1;
                int num5 = -1;
                int num6 = -1;
                int num7 = -1;
                int num8 = -1;
                int num9 = (int)Main.tile[i, j].wall;
                if (num9 != 0)
                {
                    int num10 = (int)Main.tile[i, j].wallFrameX;
                    int num11 = (int)Main.tile[i, j].wallFrameY;
                    Rectangle rectangle;
                    rectangle.X = -1;
                    rectangle.Y = -1;
                    if (i - 1 < 0)
                    {
                        num1 = num9;
                        num4 = num9;
                        num6 = num9;
                    }
                    if (i + 1 >= Main.maxTilesX)
                    {
                        num3 = num9;
                        num5 = num9;
                        num8 = num9;
                    }
                    if (j - 1 < 0)
                    {
                        num1 = num9;
                        num2 = num9;
                        num3 = num9;
                    }
                    if (j + 1 >= Main.maxTilesY)
                    {
                        num6 = num9;
                        num7 = num9;
                        num8 = num9;
                    }
                    if (i - 1 >= 0 && Main.tile[i - 1, j] != null)
                        num4 = (int)Main.tile[i - 1, j].wall;
                    if (i + 1 < Main.maxTilesX && Main.tile[i + 1, j] != null)
                        num5 = (int)Main.tile[i + 1, j].wall;
                    if (j - 1 >= 0 && Main.tile[i, j - 1] != null)
                        num2 = (int)Main.tile[i, j - 1].wall;
                    if (j + 1 < Main.maxTilesY && Main.tile[i, j + 1] != null)
                        num7 = (int)Main.tile[i, j + 1].wall;
                    if (i - 1 >= 0 && j - 1 >= 0 && Main.tile[i - 1, j - 1] != null)
                        num1 = (int)Main.tile[i - 1, j - 1].wall;
                    if (i + 1 < Main.maxTilesX && j - 1 >= 0 && Main.tile[i + 1, j - 1] != null)
                        num3 = (int)Main.tile[i + 1, j - 1].wall;
                    if (i - 1 >= 0 && j + 1 < Main.maxTilesY && Main.tile[i - 1, j + 1] != null)
                        num6 = (int)Main.tile[i - 1, j + 1].wall;
                    if (i + 1 < Main.maxTilesX && j + 1 < Main.maxTilesY && Main.tile[i + 1, j + 1] != null)
                        num8 = (int)Main.tile[i + 1, j + 1].wall;
                    if (num9 == 2)
                    {
                        if (j == (int)Main.worldSurface)
                        {
                            num7 = num9;
                            num6 = num9;
                            num8 = num9;
                        }
                        else if (j >= (int)Main.worldSurface)
                        {
                            num7 = num9;
                            num6 = num9;
                            num8 = num9;
                            num2 = num9;
                            num1 = num9;
                            num3 = num9;
                            num4 = num9;
                            num5 = num9;
                        }
                    }
                    int num12;
                    if (resetFrame)
                    {
                        num12 = WorldGen.genRand.Next(0, 3);
                        Main.tile[i, j].wallFrameNumber = (byte)num12;
                    }
                    else
                        num12 = (int)Main.tile[i, j].wallFrameNumber;
                    if (rectangle.X < 0 || rectangle.Y < 0)
                    {
                        if (num2 == num9 && num7 == num9 && num4 == num9 & num5 == num9)
                        {
                            if (num1 != num9 && num3 != num9)
                            {
                                if (num12 == 0)
                                {
                                    rectangle.X = 108;
                                    rectangle.Y = 18;
                                }
                                if (num12 == 1)
                                {
                                    rectangle.X = 126;
                                    rectangle.Y = 18;
                                }
                                if (num12 == 2)
                                {
                                    rectangle.X = 144;
                                    rectangle.Y = 18;
                                }
                            }
                            else if (num6 != num9 && num8 != num9)
                            {
                                if (num12 == 0)
                                {
                                    rectangle.X = 108;
                                    rectangle.Y = 36;
                                }
                                if (num12 == 1)
                                {
                                    rectangle.X = 126;
                                    rectangle.Y = 36;
                                }
                                if (num12 == 2)
                                {
                                    rectangle.X = 144;
                                    rectangle.Y = 36;
                                }
                            }
                            else if (num1 != num9 && num6 != num9)
                            {
                                if (num12 == 0)
                                {
                                    rectangle.X = 180;
                                    rectangle.Y = 0;
                                }
                                if (num12 == 1)
                                {
                                    rectangle.X = 180;
                                    rectangle.Y = 18;
                                }
                                if (num12 == 2)
                                {
                                    rectangle.X = 180;
                                    rectangle.Y = 36;
                                }
                            }
                            else if (num3 != num9 && num8 != num9)
                            {
                                if (num12 == 0)
                                {
                                    rectangle.X = 198;
                                    rectangle.Y = 0;
                                }
                                if (num12 == 1)
                                {
                                    rectangle.X = 198;
                                    rectangle.Y = 18;
                                }
                                if (num12 == 2)
                                {
                                    rectangle.X = 198;
                                    rectangle.Y = 36;
                                }
                            }
                            else
                            {
                                if (num12 == 0)
                                {
                                    rectangle.X = 18;
                                    rectangle.Y = 18;
                                }
                                if (num12 == 1)
                                {
                                    rectangle.X = 36;
                                    rectangle.Y = 18;
                                }
                                if (num12 == 2)
                                {
                                    rectangle.X = 54;
                                    rectangle.Y = 18;
                                }
                            }
                        }
                        else if (num2 != num9 && num7 == num9 && num4 == num9 & num5 == num9)
                        {
                            if (num12 == 0)
                            {
                                rectangle.X = 18;
                                rectangle.Y = 0;
                            }
                            if (num12 == 1)
                            {
                                rectangle.X = 36;
                                rectangle.Y = 0;
                            }
                            if (num12 == 2)
                            {
                                rectangle.X = 54;
                                rectangle.Y = 0;
                            }
                        }
                        else if (num2 == num9 && num7 != num9 && num4 == num9 & num5 == num9)
                        {
                            if (num12 == 0)
                            {
                                rectangle.X = 18;
                                rectangle.Y = 36;
                            }
                            if (num12 == 1)
                            {
                                rectangle.X = 36;
                                rectangle.Y = 36;
                            }
                            if (num12 == 2)
                            {
                                rectangle.X = 54;
                                rectangle.Y = 36;
                            }
                        }
                        else if (num2 == num9 && num7 == num9 && num4 != num9 & num5 == num9)
                        {
                            if (num12 == 0)
                            {
                                rectangle.X = 0;
                                rectangle.Y = 0;
                            }
                            if (num12 == 1)
                            {
                                rectangle.X = 0;
                                rectangle.Y = 18;
                            }
                            if (num12 == 2)
                            {
                                rectangle.X = 0;
                                rectangle.Y = 36;
                            }
                        }
                        else if (num2 == num9 && num7 == num9 && num4 == num9 & num5 != num9)
                        {
                            if (num12 == 0)
                            {
                                rectangle.X = 72;
                                rectangle.Y = 0;
                            }
                            if (num12 == 1)
                            {
                                rectangle.X = 72;
                                rectangle.Y = 18;
                            }
                            if (num12 == 2)
                            {
                                rectangle.X = 72;
                                rectangle.Y = 36;
                            }
                        }
                        else if (num2 != num9 && num7 == num9 && num4 != num9 & num5 == num9)
                        {
                            if (num12 == 0)
                            {
                                rectangle.X = 0;
                                rectangle.Y = 54;
                            }
                            if (num12 == 1)
                            {
                                rectangle.X = 36;
                                rectangle.Y = 54;
                            }
                            if (num12 == 2)
                            {
                                rectangle.X = 72;
                                rectangle.Y = 54;
                            }
                        }
                        else if (num2 != num9 && num7 == num9 && num4 == num9 & num5 != num9)
                        {
                            if (num12 == 0)
                            {
                                rectangle.X = 18;
                                rectangle.Y = 54;
                            }
                            if (num12 == 1)
                            {
                                rectangle.X = 54;
                                rectangle.Y = 54;
                            }
                            if (num12 == 2)
                            {
                                rectangle.X = 90;
                                rectangle.Y = 54;
                            }
                        }
                        else if (num2 == num9 && num7 != num9 && num4 != num9 & num5 == num9)
                        {
                            if (num12 == 0)
                            {
                                rectangle.X = 0;
                                rectangle.Y = 72;
                            }
                            if (num12 == 1)
                            {
                                rectangle.X = 36;
                                rectangle.Y = 72;
                            }
                            if (num12 == 2)
                            {
                                rectangle.X = 72;
                                rectangle.Y = 72;
                            }
                        }
                        else if (num2 == num9 && num7 != num9 && num4 == num9 & num5 != num9)
                        {
                            if (num12 == 0)
                            {
                                rectangle.X = 18;
                                rectangle.Y = 72;
                            }
                            if (num12 == 1)
                            {
                                rectangle.X = 54;
                                rectangle.Y = 72;
                            }
                            if (num12 == 2)
                            {
                                rectangle.X = 90;
                                rectangle.Y = 72;
                            }
                        }
                        else if (num2 == num9 && num7 == num9 && num4 != num9 & num5 != num9)
                        {
                            if (num12 == 0)
                            {
                                rectangle.X = 90;
                                rectangle.Y = 0;
                            }
                            if (num12 == 1)
                            {
                                rectangle.X = 90;
                                rectangle.Y = 18;
                            }
                            if (num12 == 2)
                            {
                                rectangle.X = 90;
                                rectangle.Y = 36;
                            }
                        }
                        else if (num2 != num9 && num7 != num9 && num4 == num9 & num5 == num9)
                        {
                            if (num12 == 0)
                            {
                                rectangle.X = 108;
                                rectangle.Y = 72;
                            }
                            if (num12 == 1)
                            {
                                rectangle.X = 126;
                                rectangle.Y = 72;
                            }
                            if (num12 == 2)
                            {
                                rectangle.X = 144;
                                rectangle.Y = 72;
                            }
                        }
                        else if (num2 != num9 && num7 == num9 && num4 != num9 & num5 != num9)
                        {
                            if (num12 == 0)
                            {
                                rectangle.X = 108;
                                rectangle.Y = 0;
                            }
                            if (num12 == 1)
                            {
                                rectangle.X = 126;
                                rectangle.Y = 0;
                            }
                            if (num12 == 2)
                            {
                                rectangle.X = 144;
                                rectangle.Y = 0;
                            }
                        }
                        else if (num2 == num9 && num7 != num9 && num4 != num9 & num5 != num9)
                        {
                            if (num12 == 0)
                            {
                                rectangle.X = 108;
                                rectangle.Y = 54;
                            }
                            if (num12 == 1)
                            {
                                rectangle.X = 126;
                                rectangle.Y = 54;
                            }
                            if (num12 == 2)
                            {
                                rectangle.X = 144;
                                rectangle.Y = 54;
                            }
                        }
                        else if (num2 != num9 && num7 != num9 && num4 != num9 & num5 == num9)
                        {
                            if (num12 == 0)
                            {
                                rectangle.X = 162;
                                rectangle.Y = 0;
                            }
                            if (num12 == 1)
                            {
                                rectangle.X = 162;
                                rectangle.Y = 18;
                            }
                            if (num12 == 2)
                            {
                                rectangle.X = 162;
                                rectangle.Y = 36;
                            }
                        }
                        else if (num2 != num9 && num7 != num9 && num4 == num9 & num5 != num9)
                        {
                            if (num12 == 0)
                            {
                                rectangle.X = 216;
                                rectangle.Y = 0;
                            }
                            if (num12 == 1)
                            {
                                rectangle.X = 216;
                                rectangle.Y = 18;
                            }
                            if (num12 == 2)
                            {
                                rectangle.X = 216;
                                rectangle.Y = 36;
                            }
                        }
                        else if (num2 != num9 && num7 != num9 && num4 != num9 & num5 != num9)
                        {
                            if (num12 == 0)
                            {
                                rectangle.X = 162;
                                rectangle.Y = 54;
                            }
                            if (num12 == 1)
                            {
                                rectangle.X = 180;
                                rectangle.Y = 54;
                            }
                            if (num12 == 2)
                            {
                                rectangle.X = 198;
                                rectangle.Y = 54;
                            }
                        }
                    }
                    if (rectangle.X <= -1 || rectangle.Y <= -1)
                    {
                        if (num12 <= 0)
                        {
                            rectangle.X = 18;
                            rectangle.Y = 18;
                        }
                        if (num12 == 1)
                        {
                            rectangle.X = 36;
                            rectangle.Y = 18;
                        }
                        if (num12 >= 2)
                        {
                            rectangle.X = 54;
                            rectangle.Y = 18;
                        }
                    }
                    Main.tile[i, j].wallFrameX = (byte)rectangle.X;
                    Main.tile[i, j].wallFrameY = (byte)rectangle.Y;
                }
            }
        }

        public static void TileFrame(int i, int j, bool resetFrame = false, bool noBreak = false)
        {
            if (i >= 0 && j >= 0 && (i < Main.maxTilesX && j < Main.maxTilesY) && Main.tile[i, j] != null)
            {
                if ((int)Main.tile[i, j].liquid > 0 && Main.netMode != 1 && !WorldGen.noLiquidCheck)
                    Liquid.AddWater(i, j);
                if (Main.tile[i, j].active && (!noBreak || !Main.tileFrameImportant[(int)Main.tile[i, j].type]))
                {
                    int index1 = -1;
                    int index2 = -1;
                    int index3 = -1;
                    int index4 = -1;
                    int index5 = -1;
                    int index6 = -1;
                    int index7 = -1;
                    int index8 = -1;
                    int type = (int)Main.tile[i, j].type;
                    if (Main.tileStone[type])
                        type = 1;
                    int num1 = (int)Main.tile[i, j].frameX;
                    int num2 = (int)Main.tile[i, j].frameY;
                    Rectangle rectangle;
                    rectangle.X = -1;
                    rectangle.Y = -1;
                    if (type == 3 || type == 24 || (type == 61 || type == 71) || type == 73 || type == 74)
                    {
                        WorldGen.PlantCheck(i, j);
                    }
                    else
                    {
                        WorldGen.mergeUp = false;
                        WorldGen.mergeDown = false;
                        WorldGen.mergeLeft = false;
                        WorldGen.mergeRight = false;
                        if (i - 1 < 0)
                        {
                            index1 = type;
                            index4 = type;
                            index6 = type;
                        }
                        if (i + 1 >= Main.maxTilesX)
                        {
                            index3 = type;
                            index5 = type;
                            index8 = type;
                        }
                        if (j - 1 < 0)
                        {
                            index1 = type;
                            index2 = type;
                            index3 = type;
                        }
                        if (j + 1 >= Main.maxTilesY)
                        {
                            index6 = type;
                            index7 = type;
                            index8 = type;
                        }
                        if (i - 1 >= 0 && Main.tile[i - 1, j] != null && Main.tile[i - 1, j].active)
                            index4 = (int)Main.tile[i - 1, j].type;
                        if (i + 1 < Main.maxTilesX && Main.tile[i + 1, j] != null && Main.tile[i + 1, j].active)
                            index5 = (int)Main.tile[i + 1, j].type;
                        if (j - 1 >= 0 && Main.tile[i, j - 1] != null && Main.tile[i, j - 1].active)
                            index2 = (int)Main.tile[i, j - 1].type;
                        if (j + 1 < Main.maxTilesY && Main.tile[i, j + 1] != null && Main.tile[i, j + 1].active)
                            index7 = (int)Main.tile[i, j + 1].type;
                        if (i - 1 >= 0 && j - 1 >= 0 && Main.tile[i - 1, j - 1] != null && Main.tile[i - 1, j - 1].active)
                            index1 = (int)Main.tile[i - 1, j - 1].type;
                        if (i + 1 < Main.maxTilesX && j - 1 >= 0 && Main.tile[i + 1, j - 1] != null && Main.tile[i + 1, j - 1].active)
                            index3 = (int)Main.tile[i + 1, j - 1].type;
                        if (i - 1 >= 0 && j + 1 < Main.maxTilesY && Main.tile[i - 1, j + 1] != null && Main.tile[i - 1, j + 1].active)
                            index6 = (int)Main.tile[i - 1, j + 1].type;
                        if (i + 1 < Main.maxTilesX && j + 1 < Main.maxTilesY && Main.tile[i + 1, j + 1] != null && Main.tile[i + 1, j + 1].active)
                            index8 = (int)Main.tile[i + 1, j + 1].type;
                        if (index4 >= 0 && Main.tileStone[index4])
                            index4 = 1;
                        if (index5 >= 0 && Main.tileStone[index5])
                            index5 = 1;
                        if (index2 >= 0 && Main.tileStone[index2])
                            index2 = 1;
                        if (index7 >= 0 && Main.tileStone[index7])
                            index7 = 1;
                        if (index1 >= 0 && Main.tileStone[index1])
                            index1 = 1;
                        if (index3 >= 0 && Main.tileStone[index3])
                            index3 = 1;
                        if (index6 >= 0 && Main.tileStone[index6])
                            index6 = 1;
                        if (index8 >= 0 && Main.tileStone[index8])
                            index8 = 1;
                        if (type == 4)
                        {
                            if (index7 >= 0 && Main.tileSolid[index7] && !Main.tileNoAttach[index7])
                                Main.tile[i, j].frameX = (short)0;
                            else if (index4 >= 0 && Main.tileSolid[index4] && !Main.tileNoAttach[index4] || index4 == 5 && index1 == 5 && index6 == 5)
                                Main.tile[i, j].frameX = (short)22;
                            else if (index5 >= 0 && Main.tileSolid[index5] && !Main.tileNoAttach[index5] || index5 == 5 && index3 == 5 && index8 == 5)
                                Main.tile[i, j].frameX = (short)44;
                            else
                                WorldGen.KillTile(i, j, false, false, false);
                        }
                        else if (type == 12 || type == 31)
                        {
                            if (!WorldGen.destroyObject)
                            {
                                int i1 = (int)Main.tile[i, j].frameX != 0 ? i - 1 : i;
                                int j1 = (int)Main.tile[i, j].frameY != 0 ? j - 1 : j;
                                if (Main.tile[i1, j1] != null && Main.tile[i1 + 1, j1] != null && Main.tile[i1, j1 + 1] != null && Main.tile[i1 + 1, j1 + 1] != null && (!Main.tile[i1, j1].active || (int)Main.tile[i1, j1].type != type || (!Main.tile[i1 + 1, j1].active || (int)Main.tile[i1 + 1, j1].type != type) || (!Main.tile[i1, j1 + 1].active || (int)Main.tile[i1, j1 + 1].type != type || !Main.tile[i1 + 1, j1 + 1].active) || (int)Main.tile[i1 + 1, j1 + 1].type != type))
                                {
                                    WorldGen.destroyObject = true;
                                    if ((int)Main.tile[i1, j1].type == type)
                                        WorldGen.KillTile(i1, j1, false, false, false);
                                    if ((int)Main.tile[i1 + 1, j1].type == type)
                                        WorldGen.KillTile(i1 + 1, j1, false, false, false);
                                    if ((int)Main.tile[i1, j1 + 1].type == type)
                                        WorldGen.KillTile(i1, j1 + 1, false, false, false);
                                    if ((int)Main.tile[i1 + 1, j1 + 1].type == type)
                                        WorldGen.KillTile(i1 + 1, j1 + 1, false, false, false);
                                    if (Main.netMode != 1)
                                    {
                                        if (type == 12)
                                            Item.NewItem(i1 * 16, j1 * 16, 32, 32, 29, 1, false);
                                        else if (type == 31)
                                        {
                                            if (WorldGen.genRand.Next(2) == 0)
                                                WorldGen.spawnMeteor = true;
                                            int num3 = Main.rand.Next(5);
                                            if (!WorldGen.shadowOrbSmashed)
                                                num3 = 0;
                                            if (num3 == 0)
                                            {
                                                Item.NewItem(i1 * 16, j1 * 16, 32, 32, 96, 1, false);
                                                int Stack = WorldGen.genRand.Next(25, 51);
                                                Item.NewItem(i1 * 16, j1 * 16, 32, 32, 97, Stack, false);
                                            }
                                            else if (num3 == 1)
                                                Item.NewItem(i1 * 16, j1 * 16, 32, 32, 64, 1, false);
                                            else if (num3 == 2)
                                                Item.NewItem(i1 * 16, j1 * 16, 32, 32, 162, 1, false);
                                            else if (num3 == 3)
                                                Item.NewItem(i1 * 16, j1 * 16, 32, 32, 115, 1, false);
                                            else if (num3 == 4)
                                                Item.NewItem(i1 * 16, j1 * 16, 32, 32, 111, 1, false);
                                            WorldGen.shadowOrbSmashed = true;
                                            ++WorldGen.shadowOrbCount;
                                            if (WorldGen.shadowOrbCount >= 3)
                                            {
                                                WorldGen.shadowOrbCount = 0;
                                                float num4 = (float)(i1 * 16);
                                                float num5 = (float)(j1 * 16);
                                                float num6 = -1f;
                                                int plr = 0;
                                                for (int index9 = 0; index9 < (int)byte.MaxValue; ++index9)
                                                {
                                                    float num7 = Math.Abs(Main.player[index9].position.X - num4) + Math.Abs(Main.player[index9].position.Y - num5);
                                                    if ((double)num7 < (double)num6 || (double)num6 == -1.0)
                                                    {
                                                        plr = 0;
                                                        num6 = num7;
                                                    }
                                                }
                                                NPC.SpawnOnPlayer(plr, 13);
                                            }
                                            else
                                            {
                                                string str = "A horrible chill goes down your spine...";
                                                if (WorldGen.shadowOrbCount == 2)
                                                    str = "Screams echo around you...";
                                                if (Main.netMode == 0)
                                                    Main.NewText(str, (byte)50, byte.MaxValue, (byte)130);
                                                else if (Main.netMode == 2)
                                                    NetMessage.SendData(25, -1, -1, str, (int)byte.MaxValue, 50f, (float)byte.MaxValue, 130f);
                                            }
                                        }
                                    }
                                    Main.PlaySound(13, i * 16, j * 16, 1);
                                    WorldGen.destroyObject = false;
                                }
                            }
                        }
                        else
                        {
                            if (type == 19)
                            {
                                if (index4 == type && index5 == type)
                                {
                                    if ((int)Main.tile[i, j].frameNumber == 0)
                                    {
                                        rectangle.X = 0;
                                        rectangle.Y = 0;
                                    }
                                    if ((int)Main.tile[i, j].frameNumber == 1)
                                    {
                                        rectangle.X = 0;
                                        rectangle.Y = 18;
                                    }
                                    if ((int)Main.tile[i, j].frameNumber == 2)
                                    {
                                        rectangle.X = 0;
                                        rectangle.Y = 36;
                                    }
                                }
                                else if (index4 == type && index5 == -1)
                                {
                                    if ((int)Main.tile[i, j].frameNumber == 0)
                                    {
                                        rectangle.X = 18;
                                        rectangle.Y = 0;
                                    }
                                    if ((int)Main.tile[i, j].frameNumber == 1)
                                    {
                                        rectangle.X = 18;
                                        rectangle.Y = 18;
                                    }
                                    if ((int)Main.tile[i, j].frameNumber == 2)
                                    {
                                        rectangle.X = 18;
                                        rectangle.Y = 36;
                                    }
                                }
                                else if (index4 == -1 && index5 == type)
                                {
                                    if ((int)Main.tile[i, j].frameNumber == 0)
                                    {
                                        rectangle.X = 36;
                                        rectangle.Y = 0;
                                    }
                                    if ((int)Main.tile[i, j].frameNumber == 1)
                                    {
                                        rectangle.X = 36;
                                        rectangle.Y = 18;
                                    }
                                    if ((int)Main.tile[i, j].frameNumber == 2)
                                    {
                                        rectangle.X = 36;
                                        rectangle.Y = 36;
                                    }
                                }
                                else if (index4 != type && index5 == type)
                                {
                                    if ((int)Main.tile[i, j].frameNumber == 0)
                                    {
                                        rectangle.X = 54;
                                        rectangle.Y = 0;
                                    }
                                    if ((int)Main.tile[i, j].frameNumber == 1)
                                    {
                                        rectangle.X = 54;
                                        rectangle.Y = 18;
                                    }
                                    if ((int)Main.tile[i, j].frameNumber == 2)
                                    {
                                        rectangle.X = 54;
                                        rectangle.Y = 36;
                                    }
                                }
                                else if (index4 == type && index5 != type)
                                {
                                    if ((int)Main.tile[i, j].frameNumber == 0)
                                    {
                                        rectangle.X = 72;
                                        rectangle.Y = 0;
                                    }
                                    if ((int)Main.tile[i, j].frameNumber == 1)
                                    {
                                        rectangle.X = 72;
                                        rectangle.Y = 18;
                                    }
                                    if ((int)Main.tile[i, j].frameNumber == 2)
                                    {
                                        rectangle.X = 72;
                                        rectangle.Y = 36;
                                    }
                                }
                                else if (index4 != type && index4 != -1 && index5 == -1)
                                {
                                    if ((int)Main.tile[i, j].frameNumber == 0)
                                    {
                                        rectangle.X = 108;
                                        rectangle.Y = 0;
                                    }
                                    if ((int)Main.tile[i, j].frameNumber == 1)
                                    {
                                        rectangle.X = 108;
                                        rectangle.Y = 18;
                                    }
                                    if ((int)Main.tile[i, j].frameNumber == 2)
                                    {
                                        rectangle.X = 108;
                                        rectangle.Y = 36;
                                    }
                                }
                                else if (index4 == -1 && index5 != type && index5 != -1)
                                {
                                    if ((int)Main.tile[i, j].frameNumber == 0)
                                    {
                                        rectangle.X = 126;
                                        rectangle.Y = 0;
                                    }
                                    if ((int)Main.tile[i, j].frameNumber == 1)
                                    {
                                        rectangle.X = 126;
                                        rectangle.Y = 18;
                                    }
                                    if ((int)Main.tile[i, j].frameNumber == 2)
                                    {
                                        rectangle.X = 126;
                                        rectangle.Y = 36;
                                    }
                                }
                                else
                                {
                                    if ((int)Main.tile[i, j].frameNumber == 0)
                                    {
                                        rectangle.X = 90;
                                        rectangle.Y = 0;
                                    }
                                    if ((int)Main.tile[i, j].frameNumber == 1)
                                    {
                                        rectangle.X = 90;
                                        rectangle.Y = 18;
                                    }
                                    if ((int)Main.tile[i, j].frameNumber == 2)
                                    {
                                        rectangle.X = 90;
                                        rectangle.Y = 36;
                                    }
                                }
                            }
                            else if (type == 10)
                            {
                                if (WorldGen.destroyObject)
                                {
                                    return;
                                }
                                else
                                {
                                    int num3 = (int)Main.tile[i, j].frameY;
                                    int j1 = j;
                                    bool flag = false;
                                    if (num3 == 0)
                                        j1 = j;
                                    if (num3 == 18)
                                        j1 = j - 1;
                                    if (num3 == 36)
                                        j1 = j - 2;
                                    if (Main.tile[i, j1 - 1] == null)
                                        Main.tile[i, j1 - 1] = new Tile();
                                    if (Main.tile[i, j1 + 3] == null)
                                        Main.tile[i, j1 + 3] = new Tile();
                                    if (Main.tile[i, j1 + 2] == null)
                                        Main.tile[i, j1 + 2] = new Tile();
                                    if (Main.tile[i, j1 + 1] == null)
                                        Main.tile[i, j1 + 1] = new Tile();
                                    if (Main.tile[i, j1] == null)
                                        Main.tile[i, j1] = new Tile();
                                    if (!Main.tile[i, j1 - 1].active || !Main.tileSolid[(int)Main.tile[i, j1 - 1].type])
                                        flag = true;
                                    if (!Main.tile[i, j1 + 3].active || !Main.tileSolid[(int)Main.tile[i, j1 + 3].type])
                                        flag = true;
                                    if (!Main.tile[i, j1].active || (int)Main.tile[i, j1].type != type)
                                        flag = true;
                                    if (!Main.tile[i, j1 + 1].active || (int)Main.tile[i, j1 + 1].type != type)
                                        flag = true;
                                    if (!Main.tile[i, j1 + 2].active || (int)Main.tile[i, j1 + 2].type != type)
                                        flag = true;
                                    if (flag)
                                    {
                                        WorldGen.destroyObject = true;
                                        WorldGen.KillTile(i, j1, false, false, false);
                                        WorldGen.KillTile(i, j1 + 1, false, false, false);
                                        WorldGen.KillTile(i, j1 + 2, false, false, false);
                                        Item.NewItem(i * 16, j * 16, 16, 16, 25, 1, false);
                                    }
                                    WorldGen.destroyObject = false;
                                    return;
                                }
                            }
                            else if (type == 11)
                            {
                                if (WorldGen.destroyObject)
                                {
                                    return;
                                }
                                else
                                {
                                    int num3 = 0;
                                    int index9 = i;
                                    int num4 = j;
                                    int num5 = (int)Main.tile[i, j].frameX;
                                    int num6 = (int)Main.tile[i, j].frameY;
                                    bool flag = false;
                                    if (num5 == 0)
                                    {
                                        index9 = i;
                                        num3 = 1;
                                    }
                                    else if (num5 == 18)
                                    {
                                        index9 = i - 1;
                                        num3 = 1;
                                    }
                                    else if (num5 == 36)
                                    {
                                        index9 = i + 1;
                                        num3 = -1;
                                    }
                                    else if (num5 == 54)
                                    {
                                        index9 = i;
                                        num3 = -1;
                                    }
                                    if (num6 == 0)
                                        num4 = j;
                                    else if (num6 == 18)
                                        num4 = j - 1;
                                    else if (num6 == 36)
                                        num4 = j - 2;
                                    if (Main.tile[index9, num4 + 3] == null)
                                        Main.tile[index9, num4 + 3] = new Tile();
                                    if (Main.tile[index9, num4 - 1] == null)
                                        Main.tile[index9, num4 - 1] = new Tile();
                                    if (!Main.tile[index9, num4 - 1].active || !Main.tileSolid[(int)Main.tile[index9, num4 - 1].type] || !Main.tile[index9, num4 + 3].active || !Main.tileSolid[(int)Main.tile[index9, num4 + 3].type])
                                    {
                                        flag = true;
                                        WorldGen.destroyObject = true;
                                        Item.NewItem(i * 16, j * 16, 16, 16, 25, 1, false);
                                    }
                                    int num7 = index9;
                                    if (num3 == -1)
                                        num7 = index9 - 1;
                                    for (int i1 = num7; i1 < num7 + 2; ++i1)
                                    {
                                        for (int j1 = num4; j1 < num4 + 3; ++j1)
                                        {
                                            if (!flag && ((int)Main.tile[i1, j1].type != 11 || !Main.tile[i1, j1].active))
                                            {
                                                WorldGen.destroyObject = true;
                                                Item.NewItem(i * 16, j * 16, 16, 16, 25, 1, false);
                                                flag = true;
                                                i1 = num7;
                                                j1 = num4;
                                            }
                                            if (flag)
                                                WorldGen.KillTile(i1, j1, false, false, false);
                                        }
                                    }
                                    WorldGen.destroyObject = false;
                                    return;
                                }
                            }
                            else if (type == 34 || type == 35 || type == 36)
                            {
                                WorldGen.Check3x3(i, j, (int)(byte)type);
                                return;
                            }
                            else if (type == 15 || type == 20)
                            {
                                WorldGen.Check1x2(i, j, (byte)type);
                                return;
                            }
                            else if (type == 14 || type == 17 || type == 26 || type == 77)
                            {
                                WorldGen.Check3x2(i, j, (int)(byte)type);
                                return;
                            }
                            else if (type == 16 || type == 18 || type == 29)
                            {
                                WorldGen.Check2x1(i, j, (byte)type);
                                return;
                            }
                            else if (type == 13 || type == 33 || (type == 49 || type == 50) || type == 78)
                            {
                                WorldGen.CheckOnTable1x1(i, j, (int)(byte)type);
                                return;
                            }
                            else if (type == 21)
                            {
                                WorldGen.CheckChest(i, j, (int)(byte)type);
                                return;
                            }
                            else if (type == 27)
                            {
                                WorldGen.CheckSunflower(i, j, 27);
                                return;
                            }
                            else if (type == 28)
                            {
                                WorldGen.CheckPot(i, j, 28);
                                return;
                            }
                            else if (type == 42)
                            {
                                WorldGen.Check1x2Top(i, j, (byte)type);
                                return;
                            }
                            else if (type == 55)
                            {
                                WorldGen.CheckSign(i, j, type);
                                return;
                            }
                            else if (type == 79)
                            {
                                WorldGen.Check4x2(i, j, type);
                                return;
                            }
                            if (type == 72)
                            {
                                if (index7 != type && index7 != 70)
                                    WorldGen.KillTile(i, j, false, false, false);
                                else if (index2 != type && (int)Main.tile[i, j].frameX == 0)
                                {
                                    Main.tile[i, j].frameNumber = (byte)WorldGen.genRand.Next(3);
                                    if ((int)Main.tile[i, j].frameNumber == 0)
                                    {
                                        Main.tile[i, j].frameX = (short)18;
                                        Main.tile[i, j].frameY = (short)0;
                                    }
                                    if ((int)Main.tile[i, j].frameNumber == 1)
                                    {
                                        Main.tile[i, j].frameX = (short)18;
                                        Main.tile[i, j].frameY = (short)18;
                                    }
                                    if ((int)Main.tile[i, j].frameNumber == 2)
                                    {
                                        Main.tile[i, j].frameX = (short)18;
                                        Main.tile[i, j].frameY = (short)36;
                                    }
                                }
                            }
                            if (type == 5)
                            {
                                if (index7 == 23)
                                    index7 = 2;
                                if (index7 == 60)
                                    index7 = 2;
                                if ((int)Main.tile[i, j].frameX >= 22 && (int)Main.tile[i, j].frameX <= 44 && (int)Main.tile[i, j].frameY >= 132 && (int)Main.tile[i, j].frameY <= 176)
                                {
                                    if (index4 != type && index5 != type || index7 != 2)
                                        WorldGen.KillTile(i, j, false, false, false);
                                }
                                else if ((int)Main.tile[i, j].frameX == 88 && (int)Main.tile[i, j].frameY >= 0 && (int)Main.tile[i, j].frameY <= 44 || (int)Main.tile[i, j].frameX == 66 && (int)Main.tile[i, j].frameY >= 66 && (int)Main.tile[i, j].frameY <= 130 || (int)Main.tile[i, j].frameX == 110 && (int)Main.tile[i, j].frameY >= 66 && (int)Main.tile[i, j].frameY <= 110 || (int)Main.tile[i, j].frameX == 132 && (int)Main.tile[i, j].frameY >= 0 && (int)Main.tile[i, j].frameY <= 176)
                                {
                                    if (index4 == type && index5 == type)
                                    {
                                        if ((int)Main.tile[i, j].frameNumber == 0)
                                        {
                                            Main.tile[i, j].frameX = (short)110;
                                            Main.tile[i, j].frameY = (short)66;
                                        }
                                        if ((int)Main.tile[i, j].frameNumber == 1)
                                        {
                                            Main.tile[i, j].frameX = (short)110;
                                            Main.tile[i, j].frameY = (short)88;
                                        }
                                        if ((int)Main.tile[i, j].frameNumber == 2)
                                        {
                                            Main.tile[i, j].frameX = (short)110;
                                            Main.tile[i, j].frameY = (short)110;
                                        }
                                    }
                                    else if (index4 == type)
                                    {
                                        if ((int)Main.tile[i, j].frameNumber == 0)
                                        {
                                            Main.tile[i, j].frameX = (short)88;
                                            Main.tile[i, j].frameY = (short)0;
                                        }
                                        if ((int)Main.tile[i, j].frameNumber == 1)
                                        {
                                            Main.tile[i, j].frameX = (short)88;
                                            Main.tile[i, j].frameY = (short)22;
                                        }
                                        if ((int)Main.tile[i, j].frameNumber == 2)
                                        {
                                            Main.tile[i, j].frameX = (short)88;
                                            Main.tile[i, j].frameY = (short)44;
                                        }
                                    }
                                    else if (index5 == type)
                                    {
                                        if ((int)Main.tile[i, j].frameNumber == 0)
                                        {
                                            Main.tile[i, j].frameX = (short)66;
                                            Main.tile[i, j].frameY = (short)66;
                                        }
                                        if ((int)Main.tile[i, j].frameNumber == 1)
                                        {
                                            Main.tile[i, j].frameX = (short)66;
                                            Main.tile[i, j].frameY = (short)88;
                                        }
                                        if ((int)Main.tile[i, j].frameNumber == 2)
                                        {
                                            Main.tile[i, j].frameX = (short)66;
                                            Main.tile[i, j].frameY = (short)110;
                                        }
                                    }
                                    else
                                    {
                                        if ((int)Main.tile[i, j].frameNumber == 0)
                                        {
                                            Main.tile[i, j].frameX = (short)0;
                                            Main.tile[i, j].frameY = (short)0;
                                        }
                                        if ((int)Main.tile[i, j].frameNumber == 1)
                                        {
                                            Main.tile[i, j].frameX = (short)0;
                                            Main.tile[i, j].frameY = (short)22;
                                        }
                                        if ((int)Main.tile[i, j].frameNumber == 2)
                                        {
                                            Main.tile[i, j].frameX = (short)0;
                                            Main.tile[i, j].frameY = (short)44;
                                        }
                                    }
                                }
                                if ((int)Main.tile[i, j].frameY >= 132 && (int)Main.tile[i, j].frameY <= 176 && ((int)Main.tile[i, j].frameX == 0 || (int)Main.tile[i, j].frameX == 66 || (int)Main.tile[i, j].frameX == 88))
                                {
                                    if (index7 != 2)
                                        WorldGen.KillTile(i, j, false, false, false);
                                    if (index4 != type && index5 != type)
                                    {
                                        if ((int)Main.tile[i, j].frameNumber == 0)
                                        {
                                            Main.tile[i, j].frameX = (short)0;
                                            Main.tile[i, j].frameY = (short)0;
                                        }
                                        if ((int)Main.tile[i, j].frameNumber == 1)
                                        {
                                            Main.tile[i, j].frameX = (short)0;
                                            Main.tile[i, j].frameY = (short)22;
                                        }
                                        if ((int)Main.tile[i, j].frameNumber == 2)
                                        {
                                            Main.tile[i, j].frameX = (short)0;
                                            Main.tile[i, j].frameY = (short)44;
                                        }
                                    }
                                    else if (index4 != type)
                                    {
                                        if ((int)Main.tile[i, j].frameNumber == 0)
                                        {
                                            Main.tile[i, j].frameX = (short)0;
                                            Main.tile[i, j].frameY = (short)132;
                                        }
                                        if ((int)Main.tile[i, j].frameNumber == 1)
                                        {
                                            Main.tile[i, j].frameX = (short)0;
                                            Main.tile[i, j].frameY = (short)154;
                                        }
                                        if ((int)Main.tile[i, j].frameNumber == 2)
                                        {
                                            Main.tile[i, j].frameX = (short)0;
                                            Main.tile[i, j].frameY = (short)176;
                                        }
                                    }
                                    else if (index5 != type)
                                    {
                                        if ((int)Main.tile[i, j].frameNumber == 0)
                                        {
                                            Main.tile[i, j].frameX = (short)66;
                                            Main.tile[i, j].frameY = (short)132;
                                        }
                                        if ((int)Main.tile[i, j].frameNumber == 1)
                                        {
                                            Main.tile[i, j].frameX = (short)66;
                                            Main.tile[i, j].frameY = (short)154;
                                        }
                                        if ((int)Main.tile[i, j].frameNumber == 2)
                                        {
                                            Main.tile[i, j].frameX = (short)66;
                                            Main.tile[i, j].frameY = (short)176;
                                        }
                                    }
                                    else
                                    {
                                        if ((int)Main.tile[i, j].frameNumber == 0)
                                        {
                                            Main.tile[i, j].frameX = (short)88;
                                            Main.tile[i, j].frameY = (short)132;
                                        }
                                        if ((int)Main.tile[i, j].frameNumber == 1)
                                        {
                                            Main.tile[i, j].frameX = (short)88;
                                            Main.tile[i, j].frameY = (short)154;
                                        }
                                        if ((int)Main.tile[i, j].frameNumber == 2)
                                        {
                                            Main.tile[i, j].frameX = (short)88;
                                            Main.tile[i, j].frameY = (short)176;
                                        }
                                    }
                                }
                                if ((int)Main.tile[i, j].frameX == 66 && ((int)Main.tile[i, j].frameY == 0 || (int)Main.tile[i, j].frameY == 22 || (int)Main.tile[i, j].frameY == 44) || (int)Main.tile[i, j].frameX == 88 && ((int)Main.tile[i, j].frameY == 66 || (int)Main.tile[i, j].frameY == 88 || (int)Main.tile[i, j].frameY == 110) || (int)Main.tile[i, j].frameX == 44 && ((int)Main.tile[i, j].frameY == 198 || (int)Main.tile[i, j].frameY == 220 || (int)Main.tile[i, j].frameY == 242) || (int)Main.tile[i, j].frameX == 66 && ((int)Main.tile[i, j].frameY == 198 || (int)Main.tile[i, j].frameY == 220 || (int)Main.tile[i, j].frameY == 242))
                                {
                                    if (index4 != type && index5 != type)
                                        WorldGen.KillTile(i, j, false, false, false);
                                }
                                else if (index7 == -1 || index7 == 23)
                                    WorldGen.KillTile(i, j, false, false, false);
                                else if (index2 != type && (int)Main.tile[i, j].frameY < 198 && ((int)Main.tile[i, j].frameX != 22 && (int)Main.tile[i, j].frameX != 44 || (int)Main.tile[i, j].frameY < 132))
                                {
                                    if (index4 == type || index5 == type)
                                    {
                                        if (index7 == type)
                                        {
                                            if (index4 == type && index5 == type)
                                            {
                                                if ((int)Main.tile[i, j].frameNumber == 0)
                                                {
                                                    Main.tile[i, j].frameX = (short)132;
                                                    Main.tile[i, j].frameY = (short)132;
                                                }
                                                if ((int)Main.tile[i, j].frameNumber == 1)
                                                {
                                                    Main.tile[i, j].frameX = (short)132;
                                                    Main.tile[i, j].frameY = (short)154;
                                                }
                                                if ((int)Main.tile[i, j].frameNumber == 2)
                                                {
                                                    Main.tile[i, j].frameX = (short)132;
                                                    Main.tile[i, j].frameY = (short)176;
                                                }
                                            }
                                            else if (index4 == type)
                                            {
                                                if ((int)Main.tile[i, j].frameNumber == 0)
                                                {
                                                    Main.tile[i, j].frameX = (short)132;
                                                    Main.tile[i, j].frameY = (short)0;
                                                }
                                                if ((int)Main.tile[i, j].frameNumber == 1)
                                                {
                                                    Main.tile[i, j].frameX = (short)132;
                                                    Main.tile[i, j].frameY = (short)22;
                                                }
                                                if ((int)Main.tile[i, j].frameNumber == 2)
                                                {
                                                    Main.tile[i, j].frameX = (short)132;
                                                    Main.tile[i, j].frameY = (short)44;
                                                }
                                            }
                                            else if (index5 == type)
                                            {
                                                if ((int)Main.tile[i, j].frameNumber == 0)
                                                {
                                                    Main.tile[i, j].frameX = (short)132;
                                                    Main.tile[i, j].frameY = (short)66;
                                                }
                                                if ((int)Main.tile[i, j].frameNumber == 1)
                                                {
                                                    Main.tile[i, j].frameX = (short)132;
                                                    Main.tile[i, j].frameY = (short)88;
                                                }
                                                if ((int)Main.tile[i, j].frameNumber == 2)
                                                {
                                                    Main.tile[i, j].frameX = (short)132;
                                                    Main.tile[i, j].frameY = (short)110;
                                                }
                                            }
                                        }
                                        else if (index4 == type && index5 == type)
                                        {
                                            if ((int)Main.tile[i, j].frameNumber == 0)
                                            {
                                                Main.tile[i, j].frameX = (short)154;
                                                Main.tile[i, j].frameY = (short)132;
                                            }
                                            if ((int)Main.tile[i, j].frameNumber == 1)
                                            {
                                                Main.tile[i, j].frameX = (short)154;
                                                Main.tile[i, j].frameY = (short)154;
                                            }
                                            if ((int)Main.tile[i, j].frameNumber == 2)
                                            {
                                                Main.tile[i, j].frameX = (short)154;
                                                Main.tile[i, j].frameY = (short)176;
                                            }
                                        }
                                        else if (index4 == type)
                                        {
                                            if ((int)Main.tile[i, j].frameNumber == 0)
                                            {
                                                Main.tile[i, j].frameX = (short)154;
                                                Main.tile[i, j].frameY = (short)0;
                                            }
                                            if ((int)Main.tile[i, j].frameNumber == 1)
                                            {
                                                Main.tile[i, j].frameX = (short)154;
                                                Main.tile[i, j].frameY = (short)22;
                                            }
                                            if ((int)Main.tile[i, j].frameNumber == 2)
                                            {
                                                Main.tile[i, j].frameX = (short)154;
                                                Main.tile[i, j].frameY = (short)44;
                                            }
                                        }
                                        else if (index5 == type)
                                        {
                                            if ((int)Main.tile[i, j].frameNumber == 0)
                                            {
                                                Main.tile[i, j].frameX = (short)154;
                                                Main.tile[i, j].frameY = (short)66;
                                            }
                                            if ((int)Main.tile[i, j].frameNumber == 1)
                                            {
                                                Main.tile[i, j].frameX = (short)154;
                                                Main.tile[i, j].frameY = (short)88;
                                            }
                                            if ((int)Main.tile[i, j].frameNumber == 2)
                                            {
                                                Main.tile[i, j].frameX = (short)154;
                                                Main.tile[i, j].frameY = (short)110;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if ((int)Main.tile[i, j].frameNumber == 0)
                                        {
                                            Main.tile[i, j].frameX = (short)110;
                                            Main.tile[i, j].frameY = (short)0;
                                        }
                                        if ((int)Main.tile[i, j].frameNumber == 1)
                                        {
                                            Main.tile[i, j].frameX = (short)110;
                                            Main.tile[i, j].frameY = (short)22;
                                        }
                                        if ((int)Main.tile[i, j].frameNumber == 2)
                                        {
                                            Main.tile[i, j].frameX = (short)110;
                                            Main.tile[i, j].frameY = (short)44;
                                        }
                                    }
                                }
                                rectangle.X = (int)Main.tile[i, j].frameX;
                                rectangle.Y = (int)Main.tile[i, j].frameY;
                            }
                            if (!Main.tileFrameImportant[(int)Main.tile[i, j].type])
                            {
                                int num3;
                                if (resetFrame)
                                {
                                    num3 = WorldGen.genRand.Next(0, 3);
                                    Main.tile[i, j].frameNumber = (byte)num3;
                                }
                                else
                                    num3 = (int)Main.tile[i, j].frameNumber;
                                if (type == 0)
                                {
                                    for (int index9 = 0; index9 < 80; ++index9)
                                    {
                                        if (index9 == 1 || index9 == 6 || (index9 == 7 || index9 == 8) || (index9 == 9 || index9 == 22 || (index9 == 25 || index9 == 37)) || (index9 == 40 || index9 == 53 || index9 == 56) || index9 == 59)
                                        {
                                            if (index2 == index9)
                                            {
                                                WorldGen.TileFrame(i, j - 1, false, false);
                                                if (WorldGen.mergeDown)
                                                    index2 = type;
                                            }
                                            if (index7 == index9)
                                            {
                                                WorldGen.TileFrame(i, j + 1, false, false);
                                                if (WorldGen.mergeUp)
                                                    index7 = type;
                                            }
                                            if (index4 == index9)
                                            {
                                                WorldGen.TileFrame(i - 1, j, false, false);
                                                if (WorldGen.mergeRight)
                                                    index4 = type;
                                            }
                                            if (index5 == index9)
                                            {
                                                WorldGen.TileFrame(i + 1, j, false, false);
                                                if (WorldGen.mergeLeft)
                                                    index5 = type;
                                            }
                                            if (index1 == index9)
                                                index1 = type;
                                            if (index3 == index9)
                                                index3 = type;
                                            if (index6 == index9)
                                                index6 = type;
                                            if (index8 == index9)
                                                index8 = type;
                                        }
                                    }
                                    if (index2 == 2)
                                        index2 = type;
                                    if (index7 == 2)
                                        index7 = type;
                                    if (index4 == 2)
                                        index4 = type;
                                    if (index5 == 2)
                                        index5 = type;
                                    if (index1 == 2)
                                        index1 = type;
                                    if (index3 == 2)
                                        index3 = type;
                                    if (index6 == 2)
                                        index6 = type;
                                    if (index8 == 2)
                                        index8 = type;
                                    if (index2 == 23)
                                        index2 = type;
                                    if (index7 == 23)
                                        index7 = type;
                                    if (index4 == 23)
                                        index4 = type;
                                    if (index5 == 23)
                                        index5 = type;
                                    if (index1 == 23)
                                        index1 = type;
                                    if (index3 == 23)
                                        index3 = type;
                                    if (index6 == 23)
                                        index6 = type;
                                    if (index8 == 23)
                                        index8 = type;
                                }
                                else if (type == 57)
                                {
                                    if (index2 == 58)
                                    {
                                        WorldGen.TileFrame(i, j - 1, false, false);
                                        if (WorldGen.mergeDown)
                                            index2 = type;
                                    }
                                    if (index7 == 58)
                                    {
                                        WorldGen.TileFrame(i, j + 1, false, false);
                                        if (WorldGen.mergeUp)
                                            index7 = type;
                                    }
                                    if (index4 == 58)
                                    {
                                        WorldGen.TileFrame(i - 1, j, false, false);
                                        if (WorldGen.mergeRight)
                                            index4 = type;
                                    }
                                    if (index5 == 58)
                                    {
                                        WorldGen.TileFrame(i + 1, j, false, false);
                                        if (WorldGen.mergeLeft)
                                            index5 = type;
                                    }
                                    if (index1 == 58)
                                        index1 = type;
                                    if (index3 == 58)
                                        index3 = type;
                                    if (index6 == 58)
                                        index6 = type;
                                    if (index8 == 58)
                                        index8 = type;
                                }
                                else if (type == 59)
                                {
                                    if (index2 == 60)
                                        index2 = type;
                                    if (index7 == 60)
                                        index7 = type;
                                    if (index4 == 60)
                                        index4 = type;
                                    if (index5 == 60)
                                        index5 = type;
                                    if (index1 == 60)
                                        index1 = type;
                                    if (index3 == 60)
                                        index3 = type;
                                    if (index6 == 60)
                                        index6 = type;
                                    if (index8 == 60)
                                        index8 = type;
                                    if (index2 == 70)
                                        index2 = type;
                                    if (index7 == 70)
                                        index7 = type;
                                    if (index4 == 70)
                                        index4 = type;
                                    if (index5 == 70)
                                        index5 = type;
                                    if (index1 == 70)
                                        index1 = type;
                                    if (index3 == 70)
                                        index3 = type;
                                    if (index6 == 70)
                                        index6 = type;
                                    if (index8 == 70)
                                        index8 = type;
                                }
                                if (type == 1 || type == 6 || (type == 7 || type == 8) || (type == 9 || type == 22 || (type == 25 || type == 37)) || (type == 40 || type == 53 || type == 56) || type == 59)
                                {
                                    for (int index9 = 0; index9 < 80; ++index9)
                                    {
                                        if (index9 == 1 || index9 == 6 || (index9 == 7 || index9 == 8) || (index9 == 9 || index9 == 22 || (index9 == 25 || index9 == 37)) || (index9 == 40 || index9 == 53 || index9 == 56) || index9 == 59)
                                        {
                                            if (index2 == 0)
                                                index2 = -2;
                                            if (index7 == 0)
                                                index7 = -2;
                                            if (index4 == 0)
                                                index4 = -2;
                                            if (index5 == 0)
                                                index5 = -2;
                                            if (index1 == 0)
                                                index1 = -2;
                                            if (index3 == 0)
                                                index3 = -2;
                                            if (index6 == 0)
                                                index6 = -2;
                                            if (index8 == 0)
                                                index8 = -2;
                                        }
                                    }
                                }
                                else if (type == 58)
                                {
                                    if (index2 == 57)
                                        index2 = -2;
                                    if (index7 == 57)
                                        index7 = -2;
                                    if (index4 == 57)
                                        index4 = -2;
                                    if (index5 == 57)
                                        index5 = -2;
                                    if (index1 == 57)
                                        index1 = -2;
                                    if (index3 == 57)
                                        index3 = -2;
                                    if (index6 == 57)
                                        index6 = -2;
                                    if (index8 == 57)
                                        index8 = -2;
                                }
                                else if (type == 59)
                                {
                                    if (index2 == 1)
                                        index2 = -2;
                                    if (index7 == 1)
                                        index7 = -2;
                                    if (index4 == 1)
                                        index4 = -2;
                                    if (index5 == 1)
                                        index5 = -2;
                                    if (index1 == 1)
                                        index1 = -2;
                                    if (index3 == 1)
                                        index3 = -2;
                                    if (index6 == 1)
                                        index6 = -2;
                                    if (index8 == 1)
                                        index8 = -2;
                                }
                                if (type == 32 && index7 == 23)
                                    index7 = type;
                                if (type == 69 && index7 == 60)
                                    index7 = type;
                                if (type == 51)
                                {
                                    if (index2 > -1 && !Main.tileNoAttach[index2])
                                        index2 = type;
                                    if (index7 > -1 && !Main.tileNoAttach[index7])
                                        index7 = type;
                                    if (index4 > -1 && !Main.tileNoAttach[index4])
                                        index4 = type;
                                    if (index5 > -1 && !Main.tileNoAttach[index5])
                                        index5 = type;
                                    if (index1 > -1 && !Main.tileNoAttach[index1])
                                        index1 = type;
                                    if (index3 > -1 && !Main.tileNoAttach[index3])
                                        index3 = type;
                                    if (index6 > -1 && !Main.tileNoAttach[index6])
                                        index6 = type;
                                    if (index8 > -1 && !Main.tileNoAttach[index8])
                                        index8 = type;
                                }
                                if (index2 > -1 && !Main.tileSolid[index2] && index2 != type)
                                    index2 = -1;
                                if (index7 > -1 && !Main.tileSolid[index7] && index7 != type)
                                    index7 = -1;
                                if (index4 > -1 && !Main.tileSolid[index4] && index4 != type)
                                    index4 = -1;
                                if (index5 > -1 && !Main.tileSolid[index5] && index5 != type)
                                    index5 = -1;
                                if (index1 > -1 && !Main.tileSolid[index1] && index1 != type)
                                    index1 = -1;
                                if (index3 > -1 && !Main.tileSolid[index3] && index3 != type)
                                    index3 = -1;
                                if (index6 > -1 && !Main.tileSolid[index6] && index6 != type)
                                    index6 = -1;
                                if (index8 > -1 && !Main.tileSolid[index8] && index8 != type)
                                    index8 = -1;
                                if (type == 2 || type == 23 || type == 60 || type == 70)
                                {
                                    int num4 = 0;
                                    if (type == 60 || type == 70)
                                        num4 = 59;
                                    else if (type == 2)
                                    {
                                        if (index2 == 23)
                                            index2 = num4;
                                        if (index7 == 23)
                                            index7 = num4;
                                        if (index4 == 23)
                                            index4 = num4;
                                        if (index5 == 23)
                                            index5 = num4;
                                        if (index1 == 23)
                                            index1 = num4;
                                        if (index3 == 23)
                                            index3 = num4;
                                        if (index6 == 23)
                                            index6 = num4;
                                        if (index8 == 23)
                                            index8 = num4;
                                    }
                                    else if (type == 23)
                                    {
                                        if (index2 == 2)
                                            index2 = num4;
                                        if (index7 == 2)
                                            index7 = num4;
                                        if (index4 == 2)
                                            index4 = num4;
                                        if (index5 == 2)
                                            index5 = num4;
                                        if (index1 == 2)
                                            index1 = num4;
                                        if (index3 == 2)
                                            index3 = num4;
                                        if (index6 == 2)
                                            index6 = num4;
                                        if (index8 == 2)
                                            index8 = num4;
                                    }
                                    if (index2 != type && index2 != num4 && (index7 == type || index7 == num4))
                                    {
                                        if (index4 == num4 && index5 == type)
                                        {
                                            if (num3 == 0)
                                            {
                                                rectangle.X = 0;
                                                rectangle.Y = 198;
                                            }
                                            if (num3 == 1)
                                            {
                                                rectangle.X = 18;
                                                rectangle.Y = 198;
                                            }
                                            if (num3 == 2)
                                            {
                                                rectangle.X = 36;
                                                rectangle.Y = 198;
                                            }
                                        }
                                        else if (index4 == type && index5 == num4)
                                        {
                                            if (num3 == 0)
                                            {
                                                rectangle.X = 54;
                                                rectangle.Y = 198;
                                            }
                                            if (num3 == 1)
                                            {
                                                rectangle.X = 72;
                                                rectangle.Y = 198;
                                            }
                                            if (num3 == 2)
                                            {
                                                rectangle.X = 90;
                                                rectangle.Y = 198;
                                            }
                                        }
                                    }
                                    else if (index7 != type && index7 != num4 && (index2 == type || index2 == num4))
                                    {
                                        if (index4 == num4 && index5 == type)
                                        {
                                            if (num3 == 0)
                                            {
                                                rectangle.X = 0;
                                                rectangle.Y = 216;
                                            }
                                            if (num3 == 1)
                                            {
                                                rectangle.X = 18;
                                                rectangle.Y = 216;
                                            }
                                            if (num3 == 2)
                                            {
                                                rectangle.X = 36;
                                                rectangle.Y = 216;
                                            }
                                        }
                                        else if (index4 == type && index5 == num4)
                                        {
                                            if (num3 == 0)
                                            {
                                                rectangle.X = 54;
                                                rectangle.Y = 216;
                                            }
                                            if (num3 == 1)
                                            {
                                                rectangle.X = 72;
                                                rectangle.Y = 216;
                                            }
                                            if (num3 == 2)
                                            {
                                                rectangle.X = 90;
                                                rectangle.Y = 216;
                                            }
                                        }
                                    }
                                    else if (index4 != type && index4 != num4 && (index5 == type || index5 == num4))
                                    {
                                        if (index2 == num4 && index7 == type)
                                        {
                                            if (num3 == 0)
                                            {
                                                rectangle.X = 72;
                                                rectangle.Y = 144;
                                            }
                                            if (num3 == 1)
                                            {
                                                rectangle.X = 72;
                                                rectangle.Y = 162;
                                            }
                                            if (num3 == 2)
                                            {
                                                rectangle.X = 72;
                                                rectangle.Y = 180;
                                            }
                                        }
                                        else if (index7 == type && index5 == index2)
                                        {
                                            if (num3 == 0)
                                            {
                                                rectangle.X = 72;
                                                rectangle.Y = 90;
                                            }
                                            if (num3 == 1)
                                            {
                                                rectangle.X = 72;
                                                rectangle.Y = 108;
                                            }
                                            if (num3 == 2)
                                            {
                                                rectangle.X = 72;
                                                rectangle.Y = 126;
                                            }
                                        }
                                    }
                                    else if (index5 != type && index5 != num4 && (index4 == type || index4 == num4))
                                    {
                                        if (index2 == num4 && index7 == type)
                                        {
                                            if (num3 == 0)
                                            {
                                                rectangle.X = 90;
                                                rectangle.Y = 144;
                                            }
                                            if (num3 == 1)
                                            {
                                                rectangle.X = 90;
                                                rectangle.Y = 162;
                                            }
                                            if (num3 == 2)
                                            {
                                                rectangle.X = 90;
                                                rectangle.Y = 180;
                                            }
                                        }
                                        else if (index7 == type && index5 == index2)
                                        {
                                            if (num3 == 0)
                                            {
                                                rectangle.X = 90;
                                                rectangle.Y = 90;
                                            }
                                            if (num3 == 1)
                                            {
                                                rectangle.X = 90;
                                                rectangle.Y = 108;
                                            }
                                            if (num3 == 2)
                                            {
                                                rectangle.X = 90;
                                                rectangle.Y = 126;
                                            }
                                        }
                                    }
                                    else if (index2 == type && index7 == type && index4 == type && index5 == type)
                                    {
                                        if (index1 != type && index3 != type && index6 != type && index8 != type)
                                        {
                                            if (index8 == num4)
                                            {
                                                if (num3 == 0)
                                                {
                                                    rectangle.X = 108;
                                                    rectangle.Y = 324;
                                                }
                                                if (num3 == 1)
                                                {
                                                    rectangle.X = 126;
                                                    rectangle.Y = 324;
                                                }
                                                if (num3 == 2)
                                                {
                                                    rectangle.X = 144;
                                                    rectangle.Y = 324;
                                                }
                                            }
                                            else if (index3 == num4)
                                            {
                                                if (num3 == 0)
                                                {
                                                    rectangle.X = 108;
                                                    rectangle.Y = 342;
                                                }
                                                if (num3 == 1)
                                                {
                                                    rectangle.X = 126;
                                                    rectangle.Y = 342;
                                                }
                                                if (num3 == 2)
                                                {
                                                    rectangle.X = 144;
                                                    rectangle.Y = 342;
                                                }
                                            }
                                            else if (index6 == num4)
                                            {
                                                if (num3 == 0)
                                                {
                                                    rectangle.X = 108;
                                                    rectangle.Y = 360;
                                                }
                                                if (num3 == 1)
                                                {
                                                    rectangle.X = 126;
                                                    rectangle.Y = 360;
                                                }
                                                if (num3 == 2)
                                                {
                                                    rectangle.X = 144;
                                                    rectangle.Y = 360;
                                                }
                                            }
                                            else if (index1 == num4)
                                            {
                                                if (num3 == 0)
                                                {
                                                    rectangle.X = 108;
                                                    rectangle.Y = 378;
                                                }
                                                if (num3 == 1)
                                                {
                                                    rectangle.X = 126;
                                                    rectangle.Y = 378;
                                                }
                                                if (num3 == 2)
                                                {
                                                    rectangle.X = 144;
                                                    rectangle.Y = 378;
                                                }
                                            }
                                            else
                                            {
                                                if (num3 == 0)
                                                {
                                                    rectangle.X = 144;
                                                    rectangle.Y = 234;
                                                }
                                                if (num3 == 1)
                                                {
                                                    rectangle.X = 198;
                                                    rectangle.Y = 234;
                                                }
                                                if (num3 == 2)
                                                {
                                                    rectangle.X = 252;
                                                    rectangle.Y = 234;
                                                }
                                            }
                                        }
                                        else if (index1 != type && index8 != type)
                                        {
                                            if (num3 == 0)
                                            {
                                                rectangle.X = 36;
                                                rectangle.Y = 306;
                                            }
                                            if (num3 == 1)
                                            {
                                                rectangle.X = 54;
                                                rectangle.Y = 306;
                                            }
                                            if (num3 == 2)
                                            {
                                                rectangle.X = 72;
                                                rectangle.Y = 306;
                                            }
                                        }
                                        else if (index3 != type && index6 != type)
                                        {
                                            if (num3 == 0)
                                            {
                                                rectangle.X = 90;
                                                rectangle.Y = 306;
                                            }
                                            if (num3 == 1)
                                            {
                                                rectangle.X = 108;
                                                rectangle.Y = 306;
                                            }
                                            if (num3 == 2)
                                            {
                                                rectangle.X = 126;
                                                rectangle.Y = 306;
                                            }
                                        }
                                        else if (index1 != type && index3 == type && index6 == type && index8 == type)
                                        {
                                            if (num3 == 0)
                                            {
                                                rectangle.X = 54;
                                                rectangle.Y = 108;
                                            }
                                            if (num3 == 1)
                                            {
                                                rectangle.X = 54;
                                                rectangle.Y = 144;
                                            }
                                            if (num3 == 2)
                                            {
                                                rectangle.X = 54;
                                                rectangle.Y = 180;
                                            }
                                        }
                                        else if (index1 == type && index3 != type && index6 == type && index8 == type)
                                        {
                                            if (num3 == 0)
                                            {
                                                rectangle.X = 36;
                                                rectangle.Y = 108;
                                            }
                                            if (num3 == 1)
                                            {
                                                rectangle.X = 36;
                                                rectangle.Y = 144;
                                            }
                                            if (num3 == 2)
                                            {
                                                rectangle.X = 36;
                                                rectangle.Y = 180;
                                            }
                                        }
                                        else if (index1 == type && index3 == type && index6 != type && index8 == type)
                                        {
                                            if (num3 == 0)
                                            {
                                                rectangle.X = 54;
                                                rectangle.Y = 90;
                                            }
                                            if (num3 == 1)
                                            {
                                                rectangle.X = 54;
                                                rectangle.Y = 126;
                                            }
                                            if (num3 == 2)
                                            {
                                                rectangle.X = 54;
                                                rectangle.Y = 162;
                                            }
                                        }
                                        else if (index1 == type && index3 == type && index6 == type && index8 != type)
                                        {
                                            if (num3 == 0)
                                            {
                                                rectangle.X = 36;
                                                rectangle.Y = 90;
                                            }
                                            if (num3 == 1)
                                            {
                                                rectangle.X = 36;
                                                rectangle.Y = 126;
                                            }
                                            if (num3 == 2)
                                            {
                                                rectangle.X = 36;
                                                rectangle.Y = 162;
                                            }
                                        }
                                    }
                                    else if (index2 == type && index7 == num4 && (index4 == type && index5 == type) && index1 == -1 && index3 == -1)
                                    {
                                        if (num3 == 0)
                                        {
                                            rectangle.X = 108;
                                            rectangle.Y = 18;
                                        }
                                        if (num3 == 1)
                                        {
                                            rectangle.X = 126;
                                            rectangle.Y = 18;
                                        }
                                        if (num3 == 2)
                                        {
                                            rectangle.X = 144;
                                            rectangle.Y = 18;
                                        }
                                    }
                                    else if (index2 == num4 && index7 == type && (index4 == type && index5 == type) && index6 == -1 && index8 == -1)
                                    {
                                        if (num3 == 0)
                                        {
                                            rectangle.X = 108;
                                            rectangle.Y = 36;
                                        }
                                        if (num3 == 1)
                                        {
                                            rectangle.X = 126;
                                            rectangle.Y = 36;
                                        }
                                        if (num3 == 2)
                                        {
                                            rectangle.X = 144;
                                            rectangle.Y = 36;
                                        }
                                    }
                                    else if (index2 == type && index7 == type && (index4 == num4 && index5 == type) && index3 == -1 && index8 == -1)
                                    {
                                        if (num3 == 0)
                                        {
                                            rectangle.X = 198;
                                            rectangle.Y = 0;
                                        }
                                        if (num3 == 1)
                                        {
                                            rectangle.X = 198;
                                            rectangle.Y = 18;
                                        }
                                        if (num3 == 2)
                                        {
                                            rectangle.X = 198;
                                            rectangle.Y = 36;
                                        }
                                    }
                                    else if (index2 == type && index7 == type && (index4 == type && index5 == num4) && index1 == -1 && index6 == -1)
                                    {
                                        if (num3 == 0)
                                        {
                                            rectangle.X = 180;
                                            rectangle.Y = 0;
                                        }
                                        if (num3 == 1)
                                        {
                                            rectangle.X = 180;
                                            rectangle.Y = 18;
                                        }
                                        if (num3 == 2)
                                        {
                                            rectangle.X = 180;
                                            rectangle.Y = 36;
                                        }
                                    }
                                    else if (index2 == type && index7 == num4 && index4 == type && index5 == type)
                                    {
                                        if (index3 != -1)
                                        {
                                            if (num3 == 0)
                                            {
                                                rectangle.X = 54;
                                                rectangle.Y = 108;
                                            }
                                            if (num3 == 1)
                                            {
                                                rectangle.X = 54;
                                                rectangle.Y = 144;
                                            }
                                            if (num3 == 2)
                                            {
                                                rectangle.X = 54;
                                                rectangle.Y = 180;
                                            }
                                        }
                                        else if (index1 != -1)
                                        {
                                            if (num3 == 0)
                                            {
                                                rectangle.X = 36;
                                                rectangle.Y = 108;
                                            }
                                            if (num3 == 1)
                                            {
                                                rectangle.X = 36;
                                                rectangle.Y = 144;
                                            }
                                            if (num3 == 2)
                                            {
                                                rectangle.X = 36;
                                                rectangle.Y = 180;
                                            }
                                        }
                                    }
                                    else if (index2 == num4 && index7 == type && index4 == type && index5 == type)
                                    {
                                        if (index8 != -1)
                                        {
                                            if (num3 == 0)
                                            {
                                                rectangle.X = 54;
                                                rectangle.Y = 90;
                                            }
                                            if (num3 == 1)
                                            {
                                                rectangle.X = 54;
                                                rectangle.Y = 126;
                                            }
                                            if (num3 == 2)
                                            {
                                                rectangle.X = 54;
                                                rectangle.Y = 162;
                                            }
                                        }
                                        else if (index6 != -1)
                                        {
                                            if (num3 == 0)
                                            {
                                                rectangle.X = 36;
                                                rectangle.Y = 90;
                                            }
                                            if (num3 == 1)
                                            {
                                                rectangle.X = 36;
                                                rectangle.Y = 126;
                                            }
                                            if (num3 == 2)
                                            {
                                                rectangle.X = 36;
                                                rectangle.Y = 162;
                                            }
                                        }
                                    }
                                    else if (index2 == type && index7 == type && index4 == type && index5 == num4)
                                    {
                                        if (index1 != -1)
                                        {
                                            if (num3 == 0)
                                            {
                                                rectangle.X = 54;
                                                rectangle.Y = 90;
                                            }
                                            if (num3 == 1)
                                            {
                                                rectangle.X = 54;
                                                rectangle.Y = 126;
                                            }
                                            if (num3 == 2)
                                            {
                                                rectangle.X = 54;
                                                rectangle.Y = 162;
                                            }
                                        }
                                        else if (index6 != -1)
                                        {
                                            if (num3 == 0)
                                            {
                                                rectangle.X = 54;
                                                rectangle.Y = 108;
                                            }
                                            if (num3 == 1)
                                            {
                                                rectangle.X = 54;
                                                rectangle.Y = 144;
                                            }
                                            if (num3 == 2)
                                            {
                                                rectangle.X = 54;
                                                rectangle.Y = 180;
                                            }
                                        }
                                    }
                                    else if (index2 == type && index7 == type && index4 == num4 && index5 == type)
                                    {
                                        if (index3 != -1)
                                        {
                                            if (num3 == 0)
                                            {
                                                rectangle.X = 36;
                                                rectangle.Y = 90;
                                            }
                                            if (num3 == 1)
                                            {
                                                rectangle.X = 36;
                                                rectangle.Y = 126;
                                            }
                                            if (num3 == 2)
                                            {
                                                rectangle.X = 36;
                                                rectangle.Y = 162;
                                            }
                                        }
                                        else if (index8 != -1)
                                        {
                                            if (num3 == 0)
                                            {
                                                rectangle.X = 36;
                                                rectangle.Y = 108;
                                            }
                                            if (num3 == 1)
                                            {
                                                rectangle.X = 36;
                                                rectangle.Y = 144;
                                            }
                                            if (num3 == 2)
                                            {
                                                rectangle.X = 36;
                                                rectangle.Y = 180;
                                            }
                                        }
                                    }
                                    else if (index2 == num4 && index7 == type && (index4 == type && index5 == type) || index2 == type && index7 == num4 && (index4 == type && index5 == type) || index2 == type && index7 == type && (index4 == num4 && index5 == type) || index2 == type && index7 == type && index4 == type && index5 == num4)
                                    {
                                        if (num3 == 0)
                                        {
                                            rectangle.X = 18;
                                            rectangle.Y = 18;
                                        }
                                        if (num3 == 1)
                                        {
                                            rectangle.X = 36;
                                            rectangle.Y = 18;
                                        }
                                        if (num3 == 2)
                                        {
                                            rectangle.X = 54;
                                            rectangle.Y = 18;
                                        }
                                    }
                                    if ((index2 == type || index2 == num4) && (index7 == type || index7 == num4) && (index4 == type || index4 == num4) && (index5 == type || index5 == num4))
                                    {
                                        if (index1 != type && index1 != num4 && (index3 == type || index3 == num4) && (index6 == type || index6 == num4) && (index8 == type || index8 == num4))
                                        {
                                            if (num3 == 0)
                                            {
                                                rectangle.X = 54;
                                                rectangle.Y = 108;
                                            }
                                            if (num3 == 1)
                                            {
                                                rectangle.X = 54;
                                                rectangle.Y = 144;
                                            }
                                            if (num3 == 2)
                                            {
                                                rectangle.X = 54;
                                                rectangle.Y = 180;
                                            }
                                        }
                                        else if (index3 != type && index3 != num4 && (index1 == type || index1 == num4) && (index6 == type || index6 == num4) && (index8 == type || index8 == num4))
                                        {
                                            if (num3 == 0)
                                            {
                                                rectangle.X = 36;
                                                rectangle.Y = 108;
                                            }
                                            if (num3 == 1)
                                            {
                                                rectangle.X = 36;
                                                rectangle.Y = 144;
                                            }
                                            if (num3 == 2)
                                            {
                                                rectangle.X = 36;
                                                rectangle.Y = 180;
                                            }
                                        }
                                        else if (index6 != type && index6 != num4 && (index1 == type || index1 == num4) && (index3 == type || index3 == num4) && (index8 == type || index8 == num4))
                                        {
                                            if (num3 == 0)
                                            {
                                                rectangle.X = 54;
                                                rectangle.Y = 90;
                                            }
                                            if (num3 == 1)
                                            {
                                                rectangle.X = 54;
                                                rectangle.Y = 126;
                                            }
                                            if (num3 == 2)
                                            {
                                                rectangle.X = 54;
                                                rectangle.Y = 162;
                                            }
                                        }
                                        else if (index8 != type && index8 != num4 && (index1 == type || index1 == num4) && (index6 == type || index6 == num4) && (index3 == type || index3 == num4))
                                        {
                                            if (num3 == 0)
                                            {
                                                rectangle.X = 36;
                                                rectangle.Y = 90;
                                            }
                                            if (num3 == 1)
                                            {
                                                rectangle.X = 36;
                                                rectangle.Y = 126;
                                            }
                                            if (num3 == 2)
                                            {
                                                rectangle.X = 36;
                                                rectangle.Y = 162;
                                            }
                                        }
                                    }
                                    if (index2 != num4 && index2 != type && (index7 == type && index4 != num4) && (index4 != type && index5 == type && index8 != num4) && index8 != type)
                                    {
                                        if (num3 == 0)
                                        {
                                            rectangle.X = 90;
                                            rectangle.Y = 270;
                                        }
                                        if (num3 == 1)
                                        {
                                            rectangle.X = 108;
                                            rectangle.Y = 270;
                                        }
                                        if (num3 == 2)
                                        {
                                            rectangle.X = 126;
                                            rectangle.Y = 270;
                                        }
                                    }
                                    else if (index2 != num4 && index2 != type && (index7 == type && index4 == type) && (index5 != num4 && index5 != type && index6 != num4) && index6 != type)
                                    {
                                        if (num3 == 0)
                                        {
                                            rectangle.X = 144;
                                            rectangle.Y = 270;
                                        }
                                        if (num3 == 1)
                                        {
                                            rectangle.X = 162;
                                            rectangle.Y = 270;
                                        }
                                        if (num3 == 2)
                                        {
                                            rectangle.X = 180;
                                            rectangle.Y = 270;
                                        }
                                    }
                                    else if (index7 != num4 && index7 != type && (index2 == type && index4 != num4) && (index4 != type && index5 == type && index3 != num4) && index3 != type)
                                    {
                                        if (num3 == 0)
                                        {
                                            rectangle.X = 90;
                                            rectangle.Y = 288;
                                        }
                                        if (num3 == 1)
                                        {
                                            rectangle.X = 108;
                                            rectangle.Y = 288;
                                        }
                                        if (num3 == 2)
                                        {
                                            rectangle.X = 126;
                                            rectangle.Y = 288;
                                        }
                                    }
                                    else if (index7 != num4 && index7 != type && (index2 == type && index4 == type) && (index5 != num4 && index5 != type && index1 != num4) && index1 != type)
                                    {
                                        if (num3 == 0)
                                        {
                                            rectangle.X = 144;
                                            rectangle.Y = 288;
                                        }
                                        if (num3 == 1)
                                        {
                                            rectangle.X = 162;
                                            rectangle.Y = 288;
                                        }
                                        if (num3 == 2)
                                        {
                                            rectangle.X = 180;
                                            rectangle.Y = 288;
                                        }
                                    }
                                    else if (index2 != type && index2 != num4 && (index7 == type && index4 == type) && (index5 == type && index6 != type && (index6 != num4 && index8 != type)) && index8 != num4)
                                    {
                                        if (num3 == 0)
                                        {
                                            rectangle.X = 144;
                                            rectangle.Y = 216;
                                        }
                                        if (num3 == 1)
                                        {
                                            rectangle.X = 198;
                                            rectangle.Y = 216;
                                        }
                                        if (num3 == 2)
                                        {
                                            rectangle.X = 252;
                                            rectangle.Y = 216;
                                        }
                                    }
                                    else if (index7 != type && index7 != num4 && (index2 == type && index4 == type) && (index5 == type && index1 != type && (index1 != num4 && index3 != type)) && index3 != num4)
                                    {
                                        if (num3 == 0)
                                        {
                                            rectangle.X = 144;
                                            rectangle.Y = 252;
                                        }
                                        if (num3 == 1)
                                        {
                                            rectangle.X = 198;
                                            rectangle.Y = 252;
                                        }
                                        if (num3 == 2)
                                        {
                                            rectangle.X = 252;
                                            rectangle.Y = 252;
                                        }
                                    }
                                    else if (index4 != type && index4 != num4 && (index7 == type && index2 == type) && (index5 == type && index3 != type && (index3 != num4 && index8 != type)) && index8 != num4)
                                    {
                                        if (num3 == 0)
                                        {
                                            rectangle.X = 126;
                                            rectangle.Y = 234;
                                        }
                                        if (num3 == 1)
                                        {
                                            rectangle.X = 180;
                                            rectangle.Y = 234;
                                        }
                                        if (num3 == 2)
                                        {
                                            rectangle.X = 234;
                                            rectangle.Y = 234;
                                        }
                                    }
                                    else if (index5 != type && index5 != num4 && (index7 == type && index2 == type) && (index4 == type && index1 != type && (index1 != num4 && index6 != type)) && index6 != num4)
                                    {
                                        if (num3 == 0)
                                        {
                                            rectangle.X = 162;
                                            rectangle.Y = 234;
                                        }
                                        if (num3 == 1)
                                        {
                                            rectangle.X = 216;
                                            rectangle.Y = 234;
                                        }
                                        if (num3 == 2)
                                        {
                                            rectangle.X = 270;
                                            rectangle.Y = 234;
                                        }
                                    }
                                    else if (index2 != num4 && index2 != type && (index7 == num4 || index7 == type) && index4 == num4 && index5 == num4)
                                    {
                                        if (num3 == 0)
                                        {
                                            rectangle.X = 36;
                                            rectangle.Y = 270;
                                        }
                                        if (num3 == 1)
                                        {
                                            rectangle.X = 54;
                                            rectangle.Y = 270;
                                        }
                                        if (num3 == 2)
                                        {
                                            rectangle.X = 72;
                                            rectangle.Y = 270;
                                        }
                                    }
                                    else if (index7 != num4 && index7 != type && (index2 == num4 || index2 == type) && index4 == num4 && index5 == num4)
                                    {
                                        if (num3 == 0)
                                        {
                                            rectangle.X = 36;
                                            rectangle.Y = 288;
                                        }
                                        if (num3 == 1)
                                        {
                                            rectangle.X = 54;
                                            rectangle.Y = 288;
                                        }
                                        if (num3 == 2)
                                        {
                                            rectangle.X = 72;
                                            rectangle.Y = 288;
                                        }
                                    }
                                    else if (index4 != num4 && index4 != type && (index5 == num4 || index5 == type) && index2 == num4 && index7 == num4)
                                    {
                                        if (num3 == 0)
                                        {
                                            rectangle.X = 0;
                                            rectangle.Y = 270;
                                        }
                                        if (num3 == 1)
                                        {
                                            rectangle.X = 0;
                                            rectangle.Y = 288;
                                        }
                                        if (num3 == 2)
                                        {
                                            rectangle.X = 0;
                                            rectangle.Y = 306;
                                        }
                                    }
                                    else if (index5 != num4 && index5 != type && (index4 == num4 || index4 == type) && index2 == num4 && index7 == num4)
                                    {
                                        if (num3 == 0)
                                        {
                                            rectangle.X = 18;
                                            rectangle.Y = 270;
                                        }
                                        if (num3 == 1)
                                        {
                                            rectangle.X = 18;
                                            rectangle.Y = 288;
                                        }
                                        if (num3 == 2)
                                        {
                                            rectangle.X = 18;
                                            rectangle.Y = 306;
                                        }
                                    }
                                    else if (index2 == type && index7 == num4 && index4 == num4 && index5 == num4)
                                    {
                                        if (num3 == 0)
                                        {
                                            rectangle.X = 198;
                                            rectangle.Y = 288;
                                        }
                                        if (num3 == 1)
                                        {
                                            rectangle.X = 216;
                                            rectangle.Y = 288;
                                        }
                                        if (num3 == 2)
                                        {
                                            rectangle.X = 234;
                                            rectangle.Y = 288;
                                        }
                                    }
                                    else if (index2 == num4 && index7 == type && index4 == num4 && index5 == num4)
                                    {
                                        if (num3 == 0)
                                        {
                                            rectangle.X = 198;
                                            rectangle.Y = 270;
                                        }
                                        if (num3 == 1)
                                        {
                                            rectangle.X = 216;
                                            rectangle.Y = 270;
                                        }
                                        if (num3 == 2)
                                        {
                                            rectangle.X = 234;
                                            rectangle.Y = 270;
                                        }
                                    }
                                    else if (index2 == num4 && index7 == num4 && index4 == type && index5 == num4)
                                    {
                                        if (num3 == 0)
                                        {
                                            rectangle.X = 198;
                                            rectangle.Y = 306;
                                        }
                                        if (num3 == 1)
                                        {
                                            rectangle.X = 216;
                                            rectangle.Y = 306;
                                        }
                                        if (num3 == 2)
                                        {
                                            rectangle.X = 234;
                                            rectangle.Y = 306;
                                        }
                                    }
                                    else if (index2 == num4 && index7 == num4 && index4 == num4 && index5 == type)
                                    {
                                        if (num3 == 0)
                                        {
                                            rectangle.X = 144;
                                            rectangle.Y = 306;
                                        }
                                        if (num3 == 1)
                                        {
                                            rectangle.X = 162;
                                            rectangle.Y = 306;
                                        }
                                        if (num3 == 2)
                                        {
                                            rectangle.X = 180;
                                            rectangle.Y = 306;
                                        }
                                    }
                                    if (index2 != type && index2 != num4 && (index7 == type && index4 == type) && index5 == type)
                                    {
                                        if ((index6 == num4 || index6 == type) && index8 != num4 && index8 != type)
                                        {
                                            if (num3 == 0)
                                            {
                                                rectangle.X = 0;
                                                rectangle.Y = 324;
                                            }
                                            if (num3 == 1)
                                            {
                                                rectangle.X = 18;
                                                rectangle.Y = 324;
                                            }
                                            if (num3 == 2)
                                            {
                                                rectangle.X = 36;
                                                rectangle.Y = 324;
                                            }
                                        }
                                        else if ((index8 == num4 || index8 == type) && index6 != num4 && index6 != type)
                                        {
                                            if (num3 == 0)
                                            {
                                                rectangle.X = 54;
                                                rectangle.Y = 324;
                                            }
                                            if (num3 == 1)
                                            {
                                                rectangle.X = 72;
                                                rectangle.Y = 324;
                                            }
                                            if (num3 == 2)
                                            {
                                                rectangle.X = 90;
                                                rectangle.Y = 324;
                                            }
                                        }
                                    }
                                    else if (index7 != type && index7 != num4 && (index2 == type && index4 == type) && index5 == type)
                                    {
                                        if ((index1 == num4 || index1 == type) && index3 != num4 && index3 != type)
                                        {
                                            if (num3 == 0)
                                            {
                                                rectangle.X = 0;
                                                rectangle.Y = 342;
                                            }
                                            if (num3 == 1)
                                            {
                                                rectangle.X = 18;
                                                rectangle.Y = 342;
                                            }
                                            if (num3 == 2)
                                            {
                                                rectangle.X = 36;
                                                rectangle.Y = 342;
                                            }
                                        }
                                        else if ((index3 == num4 || index3 == type) && index1 != num4 && index1 != type)
                                        {
                                            if (num3 == 0)
                                            {
                                                rectangle.X = 54;
                                                rectangle.Y = 342;
                                            }
                                            if (num3 == 1)
                                            {
                                                rectangle.X = 72;
                                                rectangle.Y = 342;
                                            }
                                            if (num3 == 2)
                                            {
                                                rectangle.X = 90;
                                                rectangle.Y = 342;
                                            }
                                        }
                                    }
                                    else if (index4 != type && index4 != num4 && (index2 == type && index7 == type) && index5 == type)
                                    {
                                        if ((index3 == num4 || index3 == type) && index8 != num4 && index8 != type)
                                        {
                                            if (num3 == 0)
                                            {
                                                rectangle.X = 54;
                                                rectangle.Y = 360;
                                            }
                                            if (num3 == 1)
                                            {
                                                rectangle.X = 72;
                                                rectangle.Y = 360;
                                            }
                                            if (num3 == 2)
                                            {
                                                rectangle.X = 90;
                                                rectangle.Y = 360;
                                            }
                                        }
                                        else if ((index8 == num4 || index8 == type) && index3 != num4 && index3 != type)
                                        {
                                            if (num3 == 0)
                                            {
                                                rectangle.X = 0;
                                                rectangle.Y = 360;
                                            }
                                            if (num3 == 1)
                                            {
                                                rectangle.X = 18;
                                                rectangle.Y = 360;
                                            }
                                            if (num3 == 2)
                                            {
                                                rectangle.X = 36;
                                                rectangle.Y = 360;
                                            }
                                        }
                                    }
                                    else if (index5 != type && index5 != num4 && (index2 == type && index7 == type) && index4 == type)
                                    {
                                        if ((index1 == num4 || index1 == type) && index6 != num4 && index6 != type)
                                        {
                                            if (num3 == 0)
                                            {
                                                rectangle.X = 0;
                                                rectangle.Y = 378;
                                            }
                                            if (num3 == 1)
                                            {
                                                rectangle.X = 18;
                                                rectangle.Y = 378;
                                            }
                                            if (num3 == 2)
                                            {
                                                rectangle.X = 36;
                                                rectangle.Y = 378;
                                            }
                                        }
                                        else if ((index6 == num4 || index6 == type) && index1 != num4 && index1 != type)
                                        {
                                            if (num3 == 0)
                                            {
                                                rectangle.X = 54;
                                                rectangle.Y = 378;
                                            }
                                            if (num3 == 1)
                                            {
                                                rectangle.X = 72;
                                                rectangle.Y = 378;
                                            }
                                            if (num3 == 2)
                                            {
                                                rectangle.X = 90;
                                                rectangle.Y = 378;
                                            }
                                        }
                                    }
                                    if ((index2 == type || index2 == num4) && (index7 == type || index7 == num4) && ((index4 == type || index4 == num4) && (index5 == type || index5 == num4)) && (index1 != -1 && index3 != -1 && index6 != -1) && index8 != -1)
                                    {
                                        if (num3 == 0)
                                        {
                                            rectangle.X = 18;
                                            rectangle.Y = 18;
                                        }
                                        if (num3 == 1)
                                        {
                                            rectangle.X = 36;
                                            rectangle.Y = 18;
                                        }
                                        if (num3 == 2)
                                        {
                                            rectangle.X = 54;
                                            rectangle.Y = 18;
                                        }
                                    }
                                    if (index2 == num4)
                                        index2 = -2;
                                    if (index7 == num4)
                                        index7 = -2;
                                    if (index4 == num4)
                                        index4 = -2;
                                    if (index5 == num4)
                                        index5 = -2;
                                    if (index1 == num4)
                                        index1 = -2;
                                    if (index3 == num4)
                                        index3 = -2;
                                    if (index6 == num4)
                                        index6 = -2;
                                    if (index8 == num4)
                                        index8 = -2;
                                }
                                if ((type == 1 || type == 2 || (type == 6 || type == 7) || (type == 8 || type == 9 || (type == 22 || type == 23)) || (type == 25 || type == 37 || (type == 40 || type == 53) || (type == 56 || type == 58 || (type == 59 || type == 60))) || type == 70) && rectangle.X == -1 && rectangle.Y == -1)
                                {
                                    if (index2 >= 0 && index2 != type)
                                        index2 = -1;
                                    if (index7 >= 0 && index7 != type)
                                        index7 = -1;
                                    if (index4 >= 0 && index4 != type)
                                        index4 = -1;
                                    if (index5 >= 0 && index5 != type)
                                        index5 = -1;
                                    if (index2 != -1 && index7 != -1 && index4 != -1 && index5 != -1)
                                    {
                                        if (index2 == -2 && index7 == type && index4 == type && index5 == type)
                                        {
                                            if (num3 == 0)
                                            {
                                                rectangle.X = 144;
                                                rectangle.Y = 108;
                                            }
                                            if (num3 == 1)
                                            {
                                                rectangle.X = 162;
                                                rectangle.Y = 108;
                                            }
                                            if (num3 == 2)
                                            {
                                                rectangle.X = 180;
                                                rectangle.Y = 108;
                                            }
                                            WorldGen.mergeUp = true;
                                        }
                                        else if (index2 == type && index7 == -2 && index4 == type && index5 == type)
                                        {
                                            if (num3 == 0)
                                            {
                                                rectangle.X = 144;
                                                rectangle.Y = 90;
                                            }
                                            if (num3 == 1)
                                            {
                                                rectangle.X = 162;
                                                rectangle.Y = 90;
                                            }
                                            if (num3 == 2)
                                            {
                                                rectangle.X = 180;
                                                rectangle.Y = 90;
                                            }
                                            WorldGen.mergeDown = true;
                                        }
                                        else if (index2 == type && index7 == type && index4 == -2 && index5 == type)
                                        {
                                            if (num3 == 0)
                                            {
                                                rectangle.X = 162;
                                                rectangle.Y = 126;
                                            }
                                            if (num3 == 1)
                                            {
                                                rectangle.X = 162;
                                                rectangle.Y = 144;
                                            }
                                            if (num3 == 2)
                                            {
                                                rectangle.X = 162;
                                                rectangle.Y = 162;
                                            }
                                            WorldGen.mergeLeft = true;
                                        }
                                        else if (index2 == type && index7 == type && index4 == type && index5 == -2)
                                        {
                                            if (num3 == 0)
                                            {
                                                rectangle.X = 144;
                                                rectangle.Y = 126;
                                            }
                                            if (num3 == 1)
                                            {
                                                rectangle.X = 144;
                                                rectangle.Y = 144;
                                            }
                                            if (num3 == 2)
                                            {
                                                rectangle.X = 144;
                                                rectangle.Y = 162;
                                            }
                                            WorldGen.mergeRight = true;
                                        }
                                        else if (index2 == -2 && index7 == type && index4 == -2 && index5 == type)
                                        {
                                            if (num3 == 0)
                                            {
                                                rectangle.X = 36;
                                                rectangle.Y = 90;
                                            }
                                            if (num3 == 1)
                                            {
                                                rectangle.X = 36;
                                                rectangle.Y = 126;
                                            }
                                            if (num3 == 2)
                                            {
                                                rectangle.X = 36;
                                                rectangle.Y = 162;
                                            }
                                            WorldGen.mergeUp = true;
                                            WorldGen.mergeLeft = true;
                                        }
                                        else if (index2 == -2 && index7 == type && index4 == type && index5 == -2)
                                        {
                                            if (num3 == 0)
                                            {
                                                rectangle.X = 54;
                                                rectangle.Y = 90;
                                            }
                                            if (num3 == 1)
                                            {
                                                rectangle.X = 54;
                                                rectangle.Y = 126;
                                            }
                                            if (num3 == 2)
                                            {
                                                rectangle.X = 54;
                                                rectangle.Y = 162;
                                            }
                                            WorldGen.mergeUp = true;
                                            WorldGen.mergeRight = true;
                                        }
                                        else if (index2 == type && index7 == -2 && index4 == -2 && index5 == type)
                                        {
                                            if (num3 == 0)
                                            {
                                                rectangle.X = 36;
                                                rectangle.Y = 108;
                                            }
                                            if (num3 == 1)
                                            {
                                                rectangle.X = 36;
                                                rectangle.Y = 144;
                                            }
                                            if (num3 == 2)
                                            {
                                                rectangle.X = 36;
                                                rectangle.Y = 180;
                                            }
                                            WorldGen.mergeDown = true;
                                            WorldGen.mergeLeft = true;
                                        }
                                        else if (index2 == type && index7 == -2 && index4 == type && index5 == -2)
                                        {
                                            if (num3 == 0)
                                            {
                                                rectangle.X = 54;
                                                rectangle.Y = 108;
                                            }
                                            if (num3 == 1)
                                            {
                                                rectangle.X = 54;
                                                rectangle.Y = 144;
                                            }
                                            if (num3 == 2)
                                            {
                                                rectangle.X = 54;
                                                rectangle.Y = 180;
                                            }
                                            WorldGen.mergeDown = true;
                                            WorldGen.mergeRight = true;
                                        }
                                        else if (index2 == type && index7 == type && index4 == -2 && index5 == -2)
                                        {
                                            if (num3 == 0)
                                            {
                                                rectangle.X = 180;
                                                rectangle.Y = 126;
                                            }
                                            if (num3 == 1)
                                            {
                                                rectangle.X = 180;
                                                rectangle.Y = 144;
                                            }
                                            if (num3 == 2)
                                            {
                                                rectangle.X = 180;
                                                rectangle.Y = 162;
                                            }
                                            WorldGen.mergeLeft = true;
                                            WorldGen.mergeRight = true;
                                        }
                                        else if (index2 == -2 && index7 == -2 && index4 == type && index5 == type)
                                        {
                                            if (num3 == 0)
                                            {
                                                rectangle.X = 144;
                                                rectangle.Y = 180;
                                            }
                                            if (num3 == 1)
                                            {
                                                rectangle.X = 162;
                                                rectangle.Y = 180;
                                            }
                                            if (num3 == 2)
                                            {
                                                rectangle.X = 180;
                                                rectangle.Y = 180;
                                            }
                                            WorldGen.mergeUp = true;
                                            WorldGen.mergeDown = true;
                                        }
                                        else if (index2 == -2 && index7 == type && index4 == -2 && index5 == -2)
                                        {
                                            if (num3 == 0)
                                            {
                                                rectangle.X = 198;
                                                rectangle.Y = 90;
                                            }
                                            if (num3 == 1)
                                            {
                                                rectangle.X = 198;
                                                rectangle.Y = 108;
                                            }
                                            if (num3 == 2)
                                            {
                                                rectangle.X = 198;
                                                rectangle.Y = 126;
                                            }
                                            WorldGen.mergeUp = true;
                                            WorldGen.mergeLeft = true;
                                            WorldGen.mergeRight = true;
                                        }
                                        else if (index2 == type && index7 == -2 && index4 == -2 && index5 == -2)
                                        {
                                            if (num3 == 0)
                                            {
                                                rectangle.X = 198;
                                                rectangle.Y = 144;
                                            }
                                            if (num3 == 1)
                                            {
                                                rectangle.X = 198;
                                                rectangle.Y = 162;
                                            }
                                            if (num3 == 2)
                                            {
                                                rectangle.X = 198;
                                                rectangle.Y = 180;
                                            }
                                            WorldGen.mergeDown = true;
                                            WorldGen.mergeLeft = true;
                                            WorldGen.mergeRight = true;
                                        }
                                        else if (index2 == -2 && index7 == -2 && index4 == type && index5 == -2)
                                        {
                                            if (num3 == 0)
                                            {
                                                rectangle.X = 216;
                                                rectangle.Y = 144;
                                            }
                                            if (num3 == 1)
                                            {
                                                rectangle.X = 216;
                                                rectangle.Y = 162;
                                            }
                                            if (num3 == 2)
                                            {
                                                rectangle.X = 216;
                                                rectangle.Y = 180;
                                            }
                                            WorldGen.mergeUp = true;
                                            WorldGen.mergeDown = true;
                                            WorldGen.mergeRight = true;
                                        }
                                        else if (index2 == -2 && index7 == -2 && index4 == -2 && index5 == type)
                                        {
                                            if (num3 == 0)
                                            {
                                                rectangle.X = 216;
                                                rectangle.Y = 90;
                                            }
                                            if (num3 == 1)
                                            {
                                                rectangle.X = 216;
                                                rectangle.Y = 108;
                                            }
                                            if (num3 == 2)
                                            {
                                                rectangle.X = 216;
                                                rectangle.Y = 126;
                                            }
                                            WorldGen.mergeUp = true;
                                            WorldGen.mergeDown = true;
                                            WorldGen.mergeLeft = true;
                                        }
                                        else if (index2 == -2 && index7 == -2 && index4 == -2 && index5 == -2)
                                        {
                                            if (num3 == 0)
                                            {
                                                rectangle.X = 108;
                                                rectangle.Y = 198;
                                            }
                                            if (num3 == 1)
                                            {
                                                rectangle.X = 126;
                                                rectangle.Y = 198;
                                            }
                                            if (num3 == 2)
                                            {
                                                rectangle.X = 144;
                                                rectangle.Y = 198;
                                            }
                                            WorldGen.mergeUp = true;
                                            WorldGen.mergeDown = true;
                                            WorldGen.mergeLeft = true;
                                            WorldGen.mergeRight = true;
                                        }
                                        else if (index2 == type && index7 == type && index4 == type && index5 == type)
                                        {
                                            if (index1 == -2)
                                            {
                                                if (num3 == 0)
                                                {
                                                    rectangle.X = 18;
                                                    rectangle.Y = 108;
                                                }
                                                if (num3 == 1)
                                                {
                                                    rectangle.X = 18;
                                                    rectangle.Y = 144;
                                                }
                                                if (num3 == 2)
                                                {
                                                    rectangle.X = 18;
                                                    rectangle.Y = 180;
                                                }
                                            }
                                            if (index3 == -2)
                                            {
                                                if (num3 == 0)
                                                {
                                                    rectangle.X = 0;
                                                    rectangle.Y = 108;
                                                }
                                                if (num3 == 1)
                                                {
                                                    rectangle.X = 0;
                                                    rectangle.Y = 144;
                                                }
                                                if (num3 == 2)
                                                {
                                                    rectangle.X = 0;
                                                    rectangle.Y = 180;
                                                }
                                            }
                                            if (index6 == -2)
                                            {
                                                if (num3 == 0)
                                                {
                                                    rectangle.X = 18;
                                                    rectangle.Y = 90;
                                                }
                                                if (num3 == 1)
                                                {
                                                    rectangle.X = 18;
                                                    rectangle.Y = 126;
                                                }
                                                if (num3 == 2)
                                                {
                                                    rectangle.X = 18;
                                                    rectangle.Y = 162;
                                                }
                                            }
                                            if (index8 == -2)
                                            {
                                                if (num3 == 0)
                                                {
                                                    rectangle.X = 0;
                                                    rectangle.Y = 90;
                                                }
                                                if (num3 == 1)
                                                {
                                                    rectangle.X = 0;
                                                    rectangle.Y = 126;
                                                }
                                                if (num3 == 2)
                                                {
                                                    rectangle.X = 0;
                                                    rectangle.Y = 162;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (type != 2 && type != 23 && type != 60 && type != 70)
                                        {
                                            if (index2 == -1 && index7 == -2 && index4 == type && index5 == type)
                                            {
                                                if (num3 == 0)
                                                {
                                                    rectangle.X = 234;
                                                    rectangle.Y = 0;
                                                }
                                                if (num3 == 1)
                                                {
                                                    rectangle.X = 252;
                                                    rectangle.Y = 0;
                                                }
                                                if (num3 == 2)
                                                {
                                                    rectangle.X = 270;
                                                    rectangle.Y = 0;
                                                }
                                                WorldGen.mergeDown = true;
                                            }
                                            else if (index2 == -2 && index7 == -1 && index4 == type && index5 == type)
                                            {
                                                if (num3 == 0)
                                                {
                                                    rectangle.X = 234;
                                                    rectangle.Y = 18;
                                                }
                                                if (num3 == 1)
                                                {
                                                    rectangle.X = 252;
                                                    rectangle.Y = 18;
                                                }
                                                if (num3 == 2)
                                                {
                                                    rectangle.X = 270;
                                                    rectangle.Y = 18;
                                                }
                                                WorldGen.mergeUp = true;
                                            }
                                            else if (index2 == type && index7 == type && index4 == -1 && index5 == -2)
                                            {
                                                if (num3 == 0)
                                                {
                                                    rectangle.X = 234;
                                                    rectangle.Y = 36;
                                                }
                                                if (num3 == 1)
                                                {
                                                    rectangle.X = 252;
                                                    rectangle.Y = 36;
                                                }
                                                if (num3 == 2)
                                                {
                                                    rectangle.X = 270;
                                                    rectangle.Y = 36;
                                                }
                                                WorldGen.mergeRight = true;
                                            }
                                            else if (index2 == type && index7 == type && index4 == -2 && index5 == -1)
                                            {
                                                if (num3 == 0)
                                                {
                                                    rectangle.X = 234;
                                                    rectangle.Y = 54;
                                                }
                                                if (num3 == 1)
                                                {
                                                    rectangle.X = 252;
                                                    rectangle.Y = 54;
                                                }
                                                if (num3 == 2)
                                                {
                                                    rectangle.X = 270;
                                                    rectangle.Y = 54;
                                                }
                                                WorldGen.mergeLeft = true;
                                            }
                                        }
                                        if (index2 != -1 && index7 != -1 && index4 == -1 && index5 == type)
                                        {
                                            if (index2 == -2 && index7 == type)
                                            {
                                                if (num3 == 0)
                                                {
                                                    rectangle.X = 72;
                                                    rectangle.Y = 144;
                                                }
                                                if (num3 == 1)
                                                {
                                                    rectangle.X = 72;
                                                    rectangle.Y = 162;
                                                }
                                                if (num3 == 2)
                                                {
                                                    rectangle.X = 72;
                                                    rectangle.Y = 180;
                                                }
                                                WorldGen.mergeUp = true;
                                            }
                                            else if (index7 == -2 && index2 == type)
                                            {
                                                if (num3 == 0)
                                                {
                                                    rectangle.X = 72;
                                                    rectangle.Y = 90;
                                                }
                                                if (num3 == 1)
                                                {
                                                    rectangle.X = 72;
                                                    rectangle.Y = 108;
                                                }
                                                if (num3 == 2)
                                                {
                                                    rectangle.X = 72;
                                                    rectangle.Y = 126;
                                                }
                                                WorldGen.mergeDown = true;
                                            }
                                        }
                                        else if (index2 != -1 && index7 != -1 && index4 == type && index5 == -1)
                                        {
                                            if (index2 == -2 && index7 == type)
                                            {
                                                if (num3 == 0)
                                                {
                                                    rectangle.X = 90;
                                                    rectangle.Y = 144;
                                                }
                                                if (num3 == 1)
                                                {
                                                    rectangle.X = 90;
                                                    rectangle.Y = 162;
                                                }
                                                if (num3 == 2)
                                                {
                                                    rectangle.X = 90;
                                                    rectangle.Y = 180;
                                                }
                                                WorldGen.mergeUp = true;
                                            }
                                            else if (index7 == -2 && index2 == type)
                                            {
                                                if (num3 == 0)
                                                {
                                                    rectangle.X = 90;
                                                    rectangle.Y = 90;
                                                }
                                                if (num3 == 1)
                                                {
                                                    rectangle.X = 90;
                                                    rectangle.Y = 108;
                                                }
                                                if (num3 == 2)
                                                {
                                                    rectangle.X = 90;
                                                    rectangle.Y = 126;
                                                }
                                                WorldGen.mergeDown = true;
                                            }
                                        }
                                        else if (index2 == -1 && index7 == type && index4 != -1 && index5 != -1)
                                        {
                                            if (index4 == -2 && index5 == type)
                                            {
                                                if (num3 == 0)
                                                {
                                                    rectangle.X = 0;
                                                    rectangle.Y = 198;
                                                }
                                                if (num3 == 1)
                                                {
                                                    rectangle.X = 18;
                                                    rectangle.Y = 198;
                                                }
                                                if (num3 == 2)
                                                {
                                                    rectangle.X = 36;
                                                    rectangle.Y = 198;
                                                }
                                                WorldGen.mergeLeft = true;
                                            }
                                            else if (index5 == -2 && index4 == type)
                                            {
                                                if (num3 == 0)
                                                {
                                                    rectangle.X = 54;
                                                    rectangle.Y = 198;
                                                }
                                                if (num3 == 1)
                                                {
                                                    rectangle.X = 72;
                                                    rectangle.Y = 198;
                                                }
                                                if (num3 == 2)
                                                {
                                                    rectangle.X = 90;
                                                    rectangle.Y = 198;
                                                }
                                                WorldGen.mergeRight = true;
                                            }
                                        }
                                        else if (index2 == type && index7 == -1 && index4 != -1 && index5 != -1)
                                        {
                                            if (index4 == -2 && index5 == type)
                                            {
                                                if (num3 == 0)
                                                {
                                                    rectangle.X = 0;
                                                    rectangle.Y = 216;
                                                }
                                                if (num3 == 1)
                                                {
                                                    rectangle.X = 18;
                                                    rectangle.Y = 216;
                                                }
                                                if (num3 == 2)
                                                {
                                                    rectangle.X = 36;
                                                    rectangle.Y = 216;
                                                }
                                                WorldGen.mergeLeft = true;
                                            }
                                            else if (index5 == -2 && index4 == type)
                                            {
                                                if (num3 == 0)
                                                {
                                                    rectangle.X = 54;
                                                    rectangle.Y = 216;
                                                }
                                                if (num3 == 1)
                                                {
                                                    rectangle.X = 72;
                                                    rectangle.Y = 216;
                                                }
                                                if (num3 == 2)
                                                {
                                                    rectangle.X = 90;
                                                    rectangle.Y = 216;
                                                }
                                                WorldGen.mergeRight = true;
                                            }
                                        }
                                        else if (index2 != -1 && index7 != -1 && index4 == -1 && index5 == -1)
                                        {
                                            if (index2 == -2 && index7 == -2)
                                            {
                                                if (num3 == 0)
                                                {
                                                    rectangle.X = 108;
                                                    rectangle.Y = 216;
                                                }
                                                if (num3 == 1)
                                                {
                                                    rectangle.X = 108;
                                                    rectangle.Y = 234;
                                                }
                                                if (num3 == 2)
                                                {
                                                    rectangle.X = 108;
                                                    rectangle.Y = 252;
                                                }
                                                WorldGen.mergeUp = true;
                                                WorldGen.mergeDown = true;
                                            }
                                            else if (index2 == -2)
                                            {
                                                if (num3 == 0)
                                                {
                                                    rectangle.X = 126;
                                                    rectangle.Y = 144;
                                                }
                                                if (num3 == 1)
                                                {
                                                    rectangle.X = 126;
                                                    rectangle.Y = 162;
                                                }
                                                if (num3 == 2)
                                                {
                                                    rectangle.X = 126;
                                                    rectangle.Y = 180;
                                                }
                                                WorldGen.mergeUp = true;
                                            }
                                            else if (index7 == -2)
                                            {
                                                if (num3 == 0)
                                                {
                                                    rectangle.X = 126;
                                                    rectangle.Y = 90;
                                                }
                                                if (num3 == 1)
                                                {
                                                    rectangle.X = 126;
                                                    rectangle.Y = 108;
                                                }
                                                if (num3 == 2)
                                                {
                                                    rectangle.X = 126;
                                                    rectangle.Y = 126;
                                                }
                                                WorldGen.mergeDown = true;
                                            }
                                        }
                                        else if (index2 == -1 && index7 == -1 && index4 != -1 && index5 != -1)
                                        {
                                            if (index4 == -2 && index5 == -2)
                                            {
                                                if (num3 == 0)
                                                {
                                                    rectangle.X = 162;
                                                    rectangle.Y = 198;
                                                }
                                                if (num3 == 1)
                                                {
                                                    rectangle.X = 180;
                                                    rectangle.Y = 198;
                                                }
                                                if (num3 == 2)
                                                {
                                                    rectangle.X = 198;
                                                    rectangle.Y = 198;
                                                }
                                                WorldGen.mergeLeft = true;
                                                WorldGen.mergeRight = true;
                                            }
                                            else if (index4 == -2)
                                            {
                                                if (num3 == 0)
                                                {
                                                    rectangle.X = 0;
                                                    rectangle.Y = 252;
                                                }
                                                if (num3 == 1)
                                                {
                                                    rectangle.X = 18;
                                                    rectangle.Y = 252;
                                                }
                                                if (num3 == 2)
                                                {
                                                    rectangle.X = 36;
                                                    rectangle.Y = 252;
                                                }
                                                WorldGen.mergeLeft = true;
                                            }
                                            else if (index5 == -2)
                                            {
                                                if (num3 == 0)
                                                {
                                                    rectangle.X = 54;
                                                    rectangle.Y = 252;
                                                }
                                                if (num3 == 1)
                                                {
                                                    rectangle.X = 72;
                                                    rectangle.Y = 252;
                                                }
                                                if (num3 == 2)
                                                {
                                                    rectangle.X = 90;
                                                    rectangle.Y = 252;
                                                }
                                                WorldGen.mergeRight = true;
                                            }
                                        }
                                        else if (index2 == -2 && index7 == -1 && index4 == -1 && index5 == -1)
                                        {
                                            if (num3 == 0)
                                            {
                                                rectangle.X = 108;
                                                rectangle.Y = 144;
                                            }
                                            if (num3 == 1)
                                            {
                                                rectangle.X = 108;
                                                rectangle.Y = 162;
                                            }
                                            if (num3 == 2)
                                            {
                                                rectangle.X = 108;
                                                rectangle.Y = 180;
                                            }
                                            WorldGen.mergeUp = true;
                                        }
                                        else if (index2 == -1 && index7 == -2 && index4 == -1 && index5 == -1)
                                        {
                                            if (num3 == 0)
                                            {
                                                rectangle.X = 108;
                                                rectangle.Y = 90;
                                            }
                                            if (num3 == 1)
                                            {
                                                rectangle.X = 108;
                                                rectangle.Y = 108;
                                            }
                                            if (num3 == 2)
                                            {
                                                rectangle.X = 108;
                                                rectangle.Y = 126;
                                            }
                                            WorldGen.mergeDown = true;
                                        }
                                        else if (index2 == -1 && index7 == -1 && index4 == -2 && index5 == -1)
                                        {
                                            if (num3 == 0)
                                            {
                                                rectangle.X = 0;
                                                rectangle.Y = 234;
                                            }
                                            if (num3 == 1)
                                            {
                                                rectangle.X = 18;
                                                rectangle.Y = 234;
                                            }
                                            if (num3 == 2)
                                            {
                                                rectangle.X = 36;
                                                rectangle.Y = 234;
                                            }
                                            WorldGen.mergeLeft = true;
                                        }
                                        else if (index2 == -1 && index7 == -1 && index4 == -1 && index5 == -2)
                                        {
                                            if (num3 == 0)
                                            {
                                                rectangle.X = 54;
                                                rectangle.Y = 234;
                                            }
                                            if (num3 == 1)
                                            {
                                                rectangle.X = 72;
                                                rectangle.Y = 234;
                                            }
                                            if (num3 == 2)
                                            {
                                                rectangle.X = 90;
                                                rectangle.Y = 234;
                                            }
                                            WorldGen.mergeRight = true;
                                        }
                                    }
                                }
                                if (rectangle.X < 0 || rectangle.Y < 0)
                                {
                                    if (type == 2 || type == 23 || type == 60 || type == 70)
                                    {
                                        if (index2 == -2)
                                            index2 = type;
                                        if (index7 == -2)
                                            index7 = type;
                                        if (index4 == -2)
                                            index4 = type;
                                        if (index5 == -2)
                                            index5 = type;
                                        if (index1 == -2)
                                            index1 = type;
                                        if (index3 == -2)
                                            index3 = type;
                                        if (index6 == -2)
                                            index6 = type;
                                        if (index8 == -2)
                                            index8 = type;
                                    }
                                    if (index2 == type && index7 == type && index4 == type & index5 == type)
                                    {
                                        if (index1 != type && index3 != type)
                                        {
                                            if (num3 == 0)
                                            {
                                                rectangle.X = 108;
                                                rectangle.Y = 18;
                                            }
                                            if (num3 == 1)
                                            {
                                                rectangle.X = 126;
                                                rectangle.Y = 18;
                                            }
                                            if (num3 == 2)
                                            {
                                                rectangle.X = 144;
                                                rectangle.Y = 18;
                                            }
                                        }
                                        else if (index6 != type && index8 != type)
                                        {
                                            if (num3 == 0)
                                            {
                                                rectangle.X = 108;
                                                rectangle.Y = 36;
                                            }
                                            if (num3 == 1)
                                            {
                                                rectangle.X = 126;
                                                rectangle.Y = 36;
                                            }
                                            if (num3 == 2)
                                            {
                                                rectangle.X = 144;
                                                rectangle.Y = 36;
                                            }
                                        }
                                        else if (index1 != type && index6 != type)
                                        {
                                            if (num3 == 0)
                                            {
                                                rectangle.X = 180;
                                                rectangle.Y = 0;
                                            }
                                            if (num3 == 1)
                                            {
                                                rectangle.X = 180;
                                                rectangle.Y = 18;
                                            }
                                            if (num3 == 2)
                                            {
                                                rectangle.X = 180;
                                                rectangle.Y = 36;
                                            }
                                        }
                                        else if (index3 != type && index8 != type)
                                        {
                                            if (num3 == 0)
                                            {
                                                rectangle.X = 198;
                                                rectangle.Y = 0;
                                            }
                                            if (num3 == 1)
                                            {
                                                rectangle.X = 198;
                                                rectangle.Y = 18;
                                            }
                                            if (num3 == 2)
                                            {
                                                rectangle.X = 198;
                                                rectangle.Y = 36;
                                            }
                                        }
                                        else
                                        {
                                            if (num3 == 0)
                                            {
                                                rectangle.X = 18;
                                                rectangle.Y = 18;
                                            }
                                            if (num3 == 1)
                                            {
                                                rectangle.X = 36;
                                                rectangle.Y = 18;
                                            }
                                            if (num3 == 2)
                                            {
                                                rectangle.X = 54;
                                                rectangle.Y = 18;
                                            }
                                        }
                                    }
                                    else if (index2 != type && index7 == type && index4 == type & index5 == type)
                                    {
                                        if (num3 == 0)
                                        {
                                            rectangle.X = 18;
                                            rectangle.Y = 0;
                                        }
                                        if (num3 == 1)
                                        {
                                            rectangle.X = 36;
                                            rectangle.Y = 0;
                                        }
                                        if (num3 == 2)
                                        {
                                            rectangle.X = 54;
                                            rectangle.Y = 0;
                                        }
                                    }
                                    else if (index2 == type && index7 != type && index4 == type & index5 == type)
                                    {
                                        if (num3 == 0)
                                        {
                                            rectangle.X = 18;
                                            rectangle.Y = 36;
                                        }
                                        if (num3 == 1)
                                        {
                                            rectangle.X = 36;
                                            rectangle.Y = 36;
                                        }
                                        if (num3 == 2)
                                        {
                                            rectangle.X = 54;
                                            rectangle.Y = 36;
                                        }
                                    }
                                    else if (index2 == type && index7 == type && index4 != type & index5 == type)
                                    {
                                        if (num3 == 0)
                                        {
                                            rectangle.X = 0;
                                            rectangle.Y = 0;
                                        }
                                        if (num3 == 1)
                                        {
                                            rectangle.X = 0;
                                            rectangle.Y = 18;
                                        }
                                        if (num3 == 2)
                                        {
                                            rectangle.X = 0;
                                            rectangle.Y = 36;
                                        }
                                    }
                                    else if (index2 == type && index7 == type && index4 == type & index5 != type)
                                    {
                                        if (num3 == 0)
                                        {
                                            rectangle.X = 72;
                                            rectangle.Y = 0;
                                        }
                                        if (num3 == 1)
                                        {
                                            rectangle.X = 72;
                                            rectangle.Y = 18;
                                        }
                                        if (num3 == 2)
                                        {
                                            rectangle.X = 72;
                                            rectangle.Y = 36;
                                        }
                                    }
                                    else if (index2 != type && index7 == type && index4 != type & index5 == type)
                                    {
                                        if (num3 == 0)
                                        {
                                            rectangle.X = 0;
                                            rectangle.Y = 54;
                                        }
                                        if (num3 == 1)
                                        {
                                            rectangle.X = 36;
                                            rectangle.Y = 54;
                                        }
                                        if (num3 == 2)
                                        {
                                            rectangle.X = 72;
                                            rectangle.Y = 54;
                                        }
                                    }
                                    else if (index2 != type && index7 == type && index4 == type & index5 != type)
                                    {
                                        if (num3 == 0)
                                        {
                                            rectangle.X = 18;
                                            rectangle.Y = 54;
                                        }
                                        if (num3 == 1)
                                        {
                                            rectangle.X = 54;
                                            rectangle.Y = 54;
                                        }
                                        if (num3 == 2)
                                        {
                                            rectangle.X = 90;
                                            rectangle.Y = 54;
                                        }
                                    }
                                    else if (index2 == type && index7 != type && index4 != type & index5 == type)
                                    {
                                        if (num3 == 0)
                                        {
                                            rectangle.X = 0;
                                            rectangle.Y = 72;
                                        }
                                        if (num3 == 1)
                                        {
                                            rectangle.X = 36;
                                            rectangle.Y = 72;
                                        }
                                        if (num3 == 2)
                                        {
                                            rectangle.X = 72;
                                            rectangle.Y = 72;
                                        }
                                    }
                                    else if (index2 == type && index7 != type && index4 == type & index5 != type)
                                    {
                                        if (num3 == 0)
                                        {
                                            rectangle.X = 18;
                                            rectangle.Y = 72;
                                        }
                                        if (num3 == 1)
                                        {
                                            rectangle.X = 54;
                                            rectangle.Y = 72;
                                        }
                                        if (num3 == 2)
                                        {
                                            rectangle.X = 90;
                                            rectangle.Y = 72;
                                        }
                                    }
                                    else if (index2 == type && index7 == type && index4 != type & index5 != type)
                                    {
                                        if (num3 == 0)
                                        {
                                            rectangle.X = 90;
                                            rectangle.Y = 0;
                                        }
                                        if (num3 == 1)
                                        {
                                            rectangle.X = 90;
                                            rectangle.Y = 18;
                                        }
                                        if (num3 == 2)
                                        {
                                            rectangle.X = 90;
                                            rectangle.Y = 36;
                                        }
                                    }
                                    else if (index2 != type && index7 != type && index4 == type & index5 == type)
                                    {
                                        if (num3 == 0)
                                        {
                                            rectangle.X = 108;
                                            rectangle.Y = 72;
                                        }
                                        if (num3 == 1)
                                        {
                                            rectangle.X = 126;
                                            rectangle.Y = 72;
                                        }
                                        if (num3 == 2)
                                        {
                                            rectangle.X = 144;
                                            rectangle.Y = 72;
                                        }
                                    }
                                    else if (index2 != type && index7 == type && index4 != type & index5 != type)
                                    {
                                        if (num3 == 0)
                                        {
                                            rectangle.X = 108;
                                            rectangle.Y = 0;
                                        }
                                        if (num3 == 1)
                                        {
                                            rectangle.X = 126;
                                            rectangle.Y = 0;
                                        }
                                        if (num3 == 2)
                                        {
                                            rectangle.X = 144;
                                            rectangle.Y = 0;
                                        }
                                    }
                                    else if (index2 == type && index7 != type && index4 != type & index5 != type)
                                    {
                                        if (num3 == 0)
                                        {
                                            rectangle.X = 108;
                                            rectangle.Y = 54;
                                        }
                                        if (num3 == 1)
                                        {
                                            rectangle.X = 126;
                                            rectangle.Y = 54;
                                        }
                                        if (num3 == 2)
                                        {
                                            rectangle.X = 144;
                                            rectangle.Y = 54;
                                        }
                                    }
                                    else if (index2 != type && index7 != type && index4 != type & index5 == type)
                                    {
                                        if (num3 == 0)
                                        {
                                            rectangle.X = 162;
                                            rectangle.Y = 0;
                                        }
                                        if (num3 == 1)
                                        {
                                            rectangle.X = 162;
                                            rectangle.Y = 18;
                                        }
                                        if (num3 == 2)
                                        {
                                            rectangle.X = 162;
                                            rectangle.Y = 36;
                                        }
                                    }
                                    else if (index2 != type && index7 != type && index4 == type & index5 != type)
                                    {
                                        if (num3 == 0)
                                        {
                                            rectangle.X = 216;
                                            rectangle.Y = 0;
                                        }
                                        if (num3 == 1)
                                        {
                                            rectangle.X = 216;
                                            rectangle.Y = 18;
                                        }
                                        if (num3 == 2)
                                        {
                                            rectangle.X = 216;
                                            rectangle.Y = 36;
                                        }
                                    }
                                    else if (index2 != type && index7 != type && index4 != type & index5 != type)
                                    {
                                        if (num3 == 0)
                                        {
                                            rectangle.X = 162;
                                            rectangle.Y = 54;
                                        }
                                        if (num3 == 1)
                                        {
                                            rectangle.X = 180;
                                            rectangle.Y = 54;
                                        }
                                        if (num3 == 2)
                                        {
                                            rectangle.X = 198;
                                            rectangle.Y = 54;
                                        }
                                    }
                                }
                                if (rectangle.X <= -1 || rectangle.Y <= -1)
                                {
                                    if (num3 <= 0)
                                    {
                                        rectangle.X = 18;
                                        rectangle.Y = 18;
                                    }
                                    if (num3 == 1)
                                    {
                                        rectangle.X = 36;
                                        rectangle.Y = 18;
                                    }
                                    if (num3 >= 2)
                                    {
                                        rectangle.X = 54;
                                        rectangle.Y = 18;
                                    }
                                }
                                Main.tile[i, j].frameX = (short)rectangle.X;
                                Main.tile[i, j].frameY = (short)rectangle.Y;
                                if (type == 52 || type == 62)
                                {
                                    int num4 = Main.tile[i, j - 1] == null ? type : (Main.tile[i, j - 1].active ? (int)Main.tile[i, j - 1].type : -1);
                                    if (num4 != type && num4 != 2 && num4 != 60)
                                        WorldGen.KillTile(i, j, false, false, false);
                                }
                                if (type == 53)
                                {
                                    if (Main.netMode == 0)
                                    {
                                        if (Main.tile[i, j + 1] != null && !Main.tile[i, j + 1].active)
                                        {
                                            bool flag = true;
                                            if (Main.tile[i, j - 1].active && (int)Main.tile[i, j - 1].type == 21)
                                                flag = false;
                                            if (flag)
                                            {
                                                Main.tile[i, j].active = false;
                                                Projectile.NewProjectile((float)(i * 16 + 8), (float)(j * 16 + 8), 0.0f, 0.41f, 31, 10, 0.0f, Main.myPlayer);
                                                WorldGen.SquareTileFrame(i, j, true);
                                            }
                                        }
                                    }
                                    else if (Main.netMode == 2 && Main.tile[i, j + 1] != null && !Main.tile[i, j + 1].active)
                                    {
                                        bool flag = true;
                                        if (Main.tile[i, j - 1].active && (int)Main.tile[i, j - 1].type == 21)
                                            flag = false;
                                        if (flag)
                                        {
                                            Main.tile[i, j].active = false;
                                            int index9 = Projectile.NewProjectile((float)(i * 16 + 8), (float)(j * 16 + 8), 0.0f, 0.41f, 31, 10, 0.0f, Main.myPlayer);
                                            Main.projectile[index9].velocity.Y = 0.5f;
                                            Main.projectile[index9].position.Y += 2f;
                                            NetMessage.SendTileSquare(-1, i, j, 1);
                                            WorldGen.SquareTileFrame(i, j, true);
                                        }
                                    }
                                }
                                if (rectangle.X != num1 && rectangle.Y != num2 && num1 >= 0 && num2 >= 0)
                                {
                                    bool flag1 = WorldGen.mergeUp;
                                    bool flag2 = WorldGen.mergeDown;
                                    bool flag3 = WorldGen.mergeLeft;
                                    bool flag4 = WorldGen.mergeRight;
                                    WorldGen.TileFrame(i - 1, j, false, false);
                                    WorldGen.TileFrame(i + 1, j, false, false);
                                    WorldGen.TileFrame(i, j - 1, false, false);
                                    WorldGen.TileFrame(i, j + 1, false, false);
                                    WorldGen.mergeUp = flag1;
                                    WorldGen.mergeDown = flag2;
                                    WorldGen.mergeLeft = flag3;
                                    WorldGen.mergeRight = flag4;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
