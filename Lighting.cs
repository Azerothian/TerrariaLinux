namespace Terraria
{
    using Microsoft.Xna.Framework;
    using System;
    using Microsoft.Xna.Framework.Graphics;

    public class Lighting
    {
        public static float[,] color = new float[(Main.screenWidth + 0x2a) + 10, (Main.screenHeight + 0x2a) + 10];
        private static int firstTileX;
        private static int firstTileY;
        private static int firstToLightX;
        private static int firstToLightY;
        private static int lastTileX;
        private static int lastTileY;
        private static int lastToLightX;
        private static int lastToLightY;
        private static float lightColor = 0f;
        public static int lightCounter = 0;
        public static int lightPasses = 2;
        public static int lightSkip = 1;
        private static int maxTempLights = 100;
        public const int offScreenTiles = 0x15;
        public static bool resize = false;
        private static float[] tempLight = new float[maxTempLights];
        private static int tempLightCount;
        private static int[] tempLightX = new int[maxTempLights];
        private static int[] tempLightY = new int[maxTempLights];

        public static void addLight(int i, int j, float Lightness)
        {
            if (((Main.netMode != 2) && (tempLightCount != maxTempLights)) && ((((((i - firstTileX) + 0x15) >= 0) && (((i - firstTileX) + 0x15) < (((Main.screenWidth / 0x10) + 0x2a) + 10))) && (((j - firstTileY) + 0x15) >= 0)) && (((j - firstTileY) + 0x15) < (((Main.screenHeight / 0x10) + 0x2a) + 10))))
            {
                for (int k = 0; k < tempLightCount; k++)
                {
                    if (((tempLightX[k] == i) && (tempLightY[k] == j)) && (Lightness <= tempLight[k]))
                    {
                        return;
                    }
                }
                tempLightX[tempLightCount] = i;
                tempLightY[tempLightCount] = j;
                tempLight[tempLightCount] = Lightness;
                tempLightCount++;
            }
        }

        public static float Brightness(int x, int y)
        {
            int num = (x - firstTileX) + 0x15;
            int num2 = (y - firstTileY) + 0x15;
            if ((((num < 0) || (num2 < 0)) || (num >= (((Main.screenWidth / 0x10) + 0x2a) + 10))) || (num2 >= (((Main.screenHeight / 0x10) + 0x2a) + 10)))
            {
                return 0f;
            }
            return color[num, num2];
        }

        public static Color GetBlackness(int x, int y)
        {
            int num = (x - firstTileX) + 0x15;
            int num2 = (y - firstTileY) + 0x15;
            if ((((num < 0) || (num2 < 0)) || (num >= (((Main.screenWidth / 0x10) + 0x2a) + 10))) || (num2 >= (((Main.screenHeight / 0x10) + 0x2a) + 10)))
            {
                return Color.Black;
            }
            return new Color(0, 0, 0, (byte) (255f - (255f * color[num, num2])));
        }

        public static Color GetColor(int x, int y)
        {
            int num = (x - firstTileX) + 0x15;
            int num2 = (y - firstTileY) + 0x15;
            if ((((num < 0) || (num2 < 0)) || (num >= (((Main.screenWidth / 0x10) + 0x2a) + 10))) || (num2 >= (((Main.screenHeight / 0x10) + 0x2a) + 10)))
            {
                return Color.Black;
            }
            return new Color((byte) (255f * color[num, num2]), (byte) (255f * color[num, num2]), (byte) (255f * color[num, num2]), 0xff);
        }

        public static Color GetColor(int x, int y, Color oldColor)
        {
            int num = (x - firstTileX) + 0x15;
            int num2 = (y - firstTileY) + 0x15;
            if (Main.gameMenu)
            {
                return oldColor;
            }
            if ((((num < 0) || (num2 < 0)) || (num >= (((Main.screenWidth / 0x10) + 0x2a) + 10))) || (num2 >= (((Main.screenHeight / 0x10) + 0x2a) + 10)))
            {
                return Color.Black;
            }
            Color white = Color.White;
            white.R = (byte) (oldColor.R * color[num, num2]);
            white.G = (byte) (oldColor.G * color[num, num2]);
            white.B = (byte) (oldColor.B * color[num, num2]);
            return white;
        }

        private static void LightColor(int i, int j)
        {
            int num = i - firstToLightX;
            int num2 = j - firstToLightY;
            try
            {
                if (color[num, num2] > lightColor)
                {
                    lightColor = color[num, num2];
                }
                else
                {
                    if (lightColor == 0f)
                    {
                        return;
                    }
                    color[num, num2] = lightColor;
                }
                float num3 = 0.04f;
                if (Main.tile[i, j].active && Main.tileBlockLight[Main.tile[i, j].type])
                {
                    num3 = 0.16f;
                }
                float num4 = lightColor - num3;
                if (num4 < 0f)
                {
                    lightColor = 0f;
                }
                else
                {
                    lightColor -= num3;
                    if (((lightColor > 0f) && (!Main.tile[i, j].active || !Main.tileSolid[Main.tile[i, j].type])) && (j < Main.worldSurface))
                    {
                        Main.tile[i, j].lighted = true;
                    }
                }
            }
            catch
            {
            }
        }

        public static int LightingX(int lightX)
        {
            if (lightX < 0)
            {
                return 0;
            }
            if (lightX >= (((Main.screenWidth / 0x10) + 0x2a) + 10))
            {
                return ((((Main.screenWidth / 0x10) + 0x2a) + 10) - 1);
            }
            return lightX;
        }

        public static int LightingY(int lightY)
        {
            if (lightY < 0)
            {
                return 0;
            }
            if (lightY >= (((Main.screenHeight / 0x10) + 0x2a) + 10))
            {
                return ((((Main.screenHeight / 0x10) + 0x2a) + 10) - 1);
            }
            return lightY;
        }

        public static void LightTiles(int firstX, int lastX, int firstY, int lastY)
        {
            int firstToLightX;
            int firstToLightY;
            firstTileX = firstX;
            lastTileX = lastX;
            firstTileY = firstY;
            lastTileY = lastY;
            if (!Main.gamePaused)
            {
                lightCounter++;
            }
            if (((lightCounter <= lightSkip) || Main.gamePaused) && !resize)
            {
                tempLightCount = 0;
                int num = ((Main.screenWidth / 0x10) + 0x2a) + 10;
                int num2 = ((Main.screenHeight / 0x10) + 0x2a) + 10;
                if (((int) (Main.screenPosition.X / 16f)) < ((int) (Main.screenLastPosition.X / 16f)))
                {
                    for (firstToLightX = num - 1; firstToLightX > 1; firstToLightX--)
                    {
                        firstToLightY = 0;
                        while (firstToLightY < num2)
                        {
                            color[firstToLightX, firstToLightY] = color[firstToLightX - 1, firstToLightY];
                            firstToLightY++;
                        }
                    }
                }
                else if (((int) (Main.screenPosition.X / 16f)) > ((int) (Main.screenLastPosition.X / 16f)))
                {
                    firstToLightX = 0;
                    while (firstToLightX < (num - 1))
                    {
                        firstToLightY = 0;
                        while (firstToLightY < num2)
                        {
                            color[firstToLightX, firstToLightY] = color[firstToLightX + 1, firstToLightY];
                            firstToLightY++;
                        }
                        firstToLightX++;
                    }
                }
                if (((int) (Main.screenPosition.Y / 16f)) < ((int) (Main.screenLastPosition.Y / 16f)))
                {
                    for (firstToLightY = num2 - 1; firstToLightY > 1; firstToLightY--)
                    {
                        firstToLightX = 0;
                        while (firstToLightX < num)
                        {
                            color[firstToLightX, firstToLightY] = color[firstToLightX, firstToLightY - 1];
                            firstToLightX++;
                        }
                    }
                }
                else if (((int) (Main.screenPosition.Y / 16f)) > ((int) (Main.screenLastPosition.Y / 16f)))
                {
                    firstToLightY = 0;
                    while (firstToLightY < (num2 - 1))
                    {
                        firstToLightX = 0;
                        while (firstToLightX < num)
                        {
                            color[firstToLightX, firstToLightY] = color[firstToLightX, firstToLightY + 1];
                            firstToLightX++;
                        }
                        firstToLightY++;
                    }
                }
            }
            else
            {
                lightCounter = 0;
                resize = false;
                Lighting.firstToLightX = firstTileX - 0x15;
                Lighting.firstToLightY = firstTileY - 0x15;
                lastToLightX = lastTileX + 0x15;
                lastToLightY = lastTileY + 0x15;
                for (firstToLightX = 0; firstToLightX < (((Main.screenWidth / 0x10) + 0x2a) + 10); firstToLightX++)
                {
                    firstToLightY = 0;
                    while (firstToLightY < (((Main.screenHeight / 0x10) + 0x2a) + 10))
                    {
                        color[firstToLightX, firstToLightY] = 0f;
                        firstToLightY++;
                    }
                }
                for (firstToLightX = 0; firstToLightX < tempLightCount; firstToLightX++)
                {
                    if ((((((tempLightX[firstToLightX] - firstTileX) + 0x15) >= 0) && (((tempLightX[firstToLightX] - firstTileX) + 0x15) < (((Main.screenWidth / 0x10) + 0x2a) + 10))) && (((tempLightY[firstToLightX] - firstTileY) + 0x15) >= 0)) && (((tempLightY[firstToLightX] - firstTileY) + 0x15) < (((Main.screenHeight / 0x10) + 0x2a) + 10)))
                    {
                        color[(tempLightX[firstToLightX] - firstTileX) + 0x15, (tempLightY[firstToLightX] - firstTileY) + 0x15] = tempLight[firstToLightX];
                    }
                }
                tempLightCount = 0;
                Main.evilTiles = 0;
                Main.meteorTiles = 0;
                Main.jungleTiles = 0;
                Main.dungeonTiles = 0;
                firstToLightX = Lighting.firstToLightX;
                while (firstToLightX < lastToLightX)
                {
                    firstToLightY = Lighting.firstToLightY;
                    while (firstToLightY < lastToLightY)
                    {
                        if ((((firstToLightX >= 0) && (firstToLightX < Main.maxTilesX)) && (firstToLightY >= 0)) && (firstToLightY < Main.maxTilesY))
                        {
                            if (Main.tile[firstToLightX, firstToLightY] == null)
                            {
                                Main.tile[firstToLightX, firstToLightY] = new Tile();
                            }
                            if (Main.tile[firstToLightX, firstToLightY].active)
                            {
                                if ((((Main.tile[firstToLightX, firstToLightY].type == 0x17) || (Main.tile[firstToLightX, firstToLightY].type == 0x18)) || (Main.tile[firstToLightX, firstToLightY].type == 0x19)) || (Main.tile[firstToLightX, firstToLightY].type == 0x20))
                                {
                                    Main.evilTiles++;
                                }
                                else if (Main.tile[firstToLightX, firstToLightY].type == 0x1b)
                                {
                                    Main.evilTiles -= 5;
                                }
                                else if (Main.tile[firstToLightX, firstToLightY].type == 0x25)
                                {
                                    Main.meteorTiles++;
                                }
                                else if (Main.tileDungeon[Main.tile[firstToLightX, firstToLightY].type])
                                {
                                    Main.dungeonTiles++;
                                }
                                else if ((((Main.tile[firstToLightX, firstToLightY].type == 60) || (Main.tile[firstToLightX, firstToLightY].type == 0x3d)) || (Main.tile[firstToLightX, firstToLightY].type == 0x3e)) || (Main.tile[firstToLightX, firstToLightY].type == 0x4a))
                                {
                                    Main.jungleTiles++;
                                }
                            }
                            if (Main.tile[firstToLightX, firstToLightY] == null)
                            {
                                Main.tile[firstToLightX, firstToLightY] = new Tile();
                            }
                            if (Main.lightTiles)
                            {
                                color[(firstToLightX - firstTileX) + 0x15, (firstToLightY - firstTileY) + 0x15] = 1f;
                            }
                            if (Main.tile[firstToLightX, firstToLightY].lava)
                            {
                                float num5 = ((Main.tile[firstToLightX, firstToLightY].liquid / 0xff) * 0.6f) + 0.1f;
                                if (color[firstToLightX - Lighting.firstToLightX, firstToLightY - Lighting.firstToLightY] < num5)
                                {
                                    color[firstToLightX - Lighting.firstToLightX, firstToLightY - Lighting.firstToLightY] = num5;
                                }
                            }
                            if ((((!Main.tile[firstToLightX, firstToLightY].active || !Main.tileSolid[Main.tile[firstToLightX, firstToLightY].type]) || ((Main.tile[firstToLightX, firstToLightY].type == 0x25) || (Main.tile[firstToLightX, firstToLightY].type == 0x3a))) || ((Main.tile[firstToLightX, firstToLightY].type == 70) || (Main.tile[firstToLightX, firstToLightY].type == 0x4c))) && ((((((Main.tile[firstToLightX, firstToLightY].lighted || (Main.tile[firstToLightX, firstToLightY].type == 4)) || ((Main.tile[firstToLightX, firstToLightY].type == 0x11) || (Main.tile[firstToLightX, firstToLightY].type == 0x1f))) || (((Main.tile[firstToLightX, firstToLightY].type == 0x21) || (Main.tile[firstToLightX, firstToLightY].type == 0x22)) || ((Main.tile[firstToLightX, firstToLightY].type == 0x23) || (Main.tile[firstToLightX, firstToLightY].type == 0x24)))) || ((((Main.tile[firstToLightX, firstToLightY].type == 0x25) || (Main.tile[firstToLightX, firstToLightY].type == 0x2a)) || ((Main.tile[firstToLightX, firstToLightY].type == 0x31) || (Main.tile[firstToLightX, firstToLightY].type == 0x3a))) || (((Main.tile[firstToLightX, firstToLightY].type == 0x3d) || (Main.tile[firstToLightX, firstToLightY].type == 70)) || ((Main.tile[firstToLightX, firstToLightY].type == 0x47) || (Main.tile[firstToLightX, firstToLightY].type == 0x48))))) || (((Main.tile[firstToLightX, firstToLightY].type == 0x4c) || (Main.tile[firstToLightX, firstToLightY].type == 0x4d)) || (Main.tile[firstToLightX, firstToLightY].type == 0x13))) || (Main.tile[firstToLightX, firstToLightY].type == 0x1a)))
                            {
                                if ((((color[firstToLightX - Lighting.firstToLightX, firstToLightY - Lighting.firstToLightY] * 255f) < Main.tileColor.R) && (Main.tileColor.R > (color[firstToLightX - Lighting.firstToLightX, firstToLightY - Lighting.firstToLightY] * 255f))) && (((Main.tile[firstToLightX, firstToLightY].wall == 0) && (firstToLightY < Main.worldSurface)) && (Main.tile[firstToLightX, firstToLightY].liquid < 0xff)))
                                {
                                    color[firstToLightX - Lighting.firstToLightX, firstToLightY - Lighting.firstToLightY] = ((float) Main.tileColor.R) / 255f;
                                }
                                if ((((Main.tile[firstToLightX, firstToLightY].type == 4) || (Main.tile[firstToLightX, firstToLightY].type == 0x21)) || ((Main.tile[firstToLightX, firstToLightY].type == 0x22) || (Main.tile[firstToLightX, firstToLightY].type == 0x23))) || (Main.tile[firstToLightX, firstToLightY].type == 0x24))
                                {
                                    color[firstToLightX - Lighting.firstToLightX, firstToLightY - Lighting.firstToLightY] = 1f;
                                }
                                else if ((Main.tile[firstToLightX, firstToLightY].type == 0x11) && (color[firstToLightX - Lighting.firstToLightX, firstToLightY - Lighting.firstToLightY] < 0.8f))
                                {
                                    color[firstToLightX - Lighting.firstToLightX, firstToLightY - Lighting.firstToLightY] = 0.8f;
                                }
                                else if ((Main.tile[firstToLightX, firstToLightY].type == 0x4d) && (color[firstToLightX - Lighting.firstToLightX, firstToLightY - Lighting.firstToLightY] < 0.8f))
                                {
                                    color[firstToLightX - Lighting.firstToLightX, firstToLightY - Lighting.firstToLightY] = 0.8f;
                                }
                                else if ((Main.tile[firstToLightX, firstToLightY].type == 0x25) && (color[firstToLightX - Lighting.firstToLightX, firstToLightY - Lighting.firstToLightY] < 0.6f))
                                {
                                    color[firstToLightX - Lighting.firstToLightX, firstToLightY - Lighting.firstToLightY] = 0.6f;
                                }
                                else if ((Main.tile[firstToLightX, firstToLightY].type == 0x3a) && (color[firstToLightX - Lighting.firstToLightX, firstToLightY - Lighting.firstToLightY] < 0.6f))
                                {
                                    color[firstToLightX - Lighting.firstToLightX, firstToLightY - Lighting.firstToLightY] = 0.6f;
                                }
                                else if ((Main.tile[firstToLightX, firstToLightY].type == 0x4c) && (color[firstToLightX - Lighting.firstToLightX, firstToLightY - Lighting.firstToLightY] < 0.6f))
                                {
                                    color[firstToLightX - Lighting.firstToLightX, firstToLightY - Lighting.firstToLightY] = 0.6f;
                                }
                                else if ((Main.tile[firstToLightX, firstToLightY].type == 0x2a) && (color[firstToLightX - Lighting.firstToLightX, firstToLightY - Lighting.firstToLightY] < 0.75f))
                                {
                                    color[firstToLightX - Lighting.firstToLightX, firstToLightY - Lighting.firstToLightY] = 0.75f;
                                }
                                else if ((Main.tile[firstToLightX, firstToLightY].type == 0x31) && (color[firstToLightX - Lighting.firstToLightX, firstToLightY - Lighting.firstToLightY] < 0.75f))
                                {
                                    color[firstToLightX - Lighting.firstToLightX, firstToLightY - Lighting.firstToLightY] = 0.75f;
                                }
                                else if (((Main.tile[firstToLightX, firstToLightY].type == 70) || (Main.tile[firstToLightX, firstToLightY].type == 0x47)) || (Main.tile[firstToLightX, firstToLightY].type == 0x48))
                                {
                                    float num6 = Main.rand.Next(0x30, 0x34) * 0.01f;
                                    if (color[firstToLightX - Lighting.firstToLightX, firstToLightY - Lighting.firstToLightY] < num6)
                                    {
                                        color[firstToLightX - Lighting.firstToLightX, firstToLightY - Lighting.firstToLightY] = num6;
                                    }
                                }
                                else if (((Main.tile[firstToLightX, firstToLightY].type == 0x3d) && (Main.tile[firstToLightX, firstToLightY].frameX == 0x90)) && (color[firstToLightX - Lighting.firstToLightX, firstToLightY - Lighting.firstToLightY] < 0.75f))
                                {
                                    color[firstToLightX - Lighting.firstToLightX, firstToLightY - Lighting.firstToLightY] = 0.75f;
                                }
                                else if ((Main.tile[firstToLightX, firstToLightY].type == 0x1f) || (Main.tile[firstToLightX, firstToLightY].type == 0x1a))
                                {
                                    float num7 = Main.rand.Next(-5, 6) * 0.01f;
                                    if (color[firstToLightX - Lighting.firstToLightX, firstToLightY - Lighting.firstToLightY] < (0.4f + num7))
                                    {
                                        color[firstToLightX - Lighting.firstToLightX, firstToLightY - Lighting.firstToLightY] = 0.4f + num7;
                                    }
                                }
                            }
                        }
                        firstToLightY++;
                    }
                    firstToLightX++;
                }
                for (int i = 0; i < lightPasses; i++)
                {
                    firstToLightX = Lighting.firstToLightX;
                    while (firstToLightX < lastToLightX)
                    {
                        lightColor = 0f;
                        firstToLightY = Lighting.firstToLightY;
                        while (firstToLightY < lastToLightY)
                        {
                            LightColor(firstToLightX, firstToLightY);
                            firstToLightY++;
                        }
                        firstToLightX++;
                    }
                    firstToLightX = Lighting.firstToLightX;
                    while (firstToLightX < lastToLightX)
                    {
                        lightColor = 0f;
                        firstToLightY = lastToLightY;
                        while (firstToLightY >= Lighting.firstToLightY)
                        {
                            LightColor(firstToLightX, firstToLightY);
                            firstToLightY--;
                        }
                        firstToLightX++;
                    }
                    firstToLightY = Lighting.firstToLightY;
                    while (firstToLightY < lastToLightY)
                    {
                        lightColor = 0f;
                        firstToLightX = Lighting.firstToLightX;
                        while (firstToLightX < lastToLightX)
                        {
                            LightColor(firstToLightX, firstToLightY);
                            firstToLightX++;
                        }
                        firstToLightY++;
                    }
                    for (firstToLightY = Lighting.firstToLightY; firstToLightY < lastToLightY; firstToLightY++)
                    {
                        lightColor = 0f;
                        for (firstToLightX = lastToLightX; firstToLightX >= Lighting.firstToLightX; firstToLightX--)
                        {
                            LightColor(firstToLightX, firstToLightY);
                        }
                    }
                }
            }
        }
    }
}

