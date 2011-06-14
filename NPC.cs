namespace Terraria
{
    using Microsoft.Xna.Framework;
    using System;
    using System.Runtime.InteropServices;
    using Microsoft.Xna.Framework.Graphics;

    public class NPC
    {
        public bool active;
        private static int activeRangeX = ((int) (sWidth * 1.7));
        private static int activeRangeY = ((int) (sHeight * 1.7));
        private static int activeTime = 750;
        public float[] ai = new float[maxAI];
        public int aiAction = 0;
        public int aiStyle;
        public int alpha;
        public bool behindTiles;
        public bool boss = false;
        public bool closeDoor = false;
        public bool collideX = false;
        public bool collideY = false;
        public Color color;
        public int damage;
        private static int defaultMaxSpawns = 5;
        private static int defaultSpawnRate = 600;
        public int defense;
        public int direction = 1;
        public int directionY = 1;
        public int doorX = 0;
        public int doorY = 0;
        public static bool downedBoss1 = false;
        public static bool downedBoss2 = false;
        public static bool downedBoss3 = false;
        public Rectangle frame;
        public double frameCounter;
        public bool friendly = false;
        public int friendlyRegen = 0;
        public int height;
        public bool homeless = false;
        public int homeTileX = -1;
        public int homeTileY = -1;
        public int[] immune = new int[0x100];
        public static int immuneTime = 20;
        public float knockBackResist = 1f;
        public bool lavaWet = false;
        public int life;
        public int lifeMax;
        public static int maxAI = 4;
        private static int maxSpawns = defaultMaxSpawns;
        public string name;
        public bool netUpdate = false;
        public bool noGravity = false;
        private static bool noSpawnCycle = false;
        public bool noTileCollide = false;
        public int oldDirection = 0;
        public int oldDirectionY = 0;
        public Vector2 oldPosition;
        public int oldTarget = 0;
        public Vector2 oldVelocity;
        public Vector2 position;
        public float rotation = 0f;
        public static int safeRangeX = ((int) ((sWidth / 0x10) * 0.52));
        public static int safeRangeY = ((int) ((sHeight / 0x10) * 0.52));
        public float scale = 1f;
        public static int sHeight = 0x41a;
        public int soundDelay = 0;
        public int soundHit;
        public int soundKilled;
        private static int spawnRangeX = ((int) ((sWidth / 0x10) * 0.7));
        private static int spawnRangeY = ((int) ((sHeight / 0x10) * 0.7));
        private static int spawnRate = defaultSpawnRate;
        private static int spawnSpaceX = 3;
        private static int spawnSpaceY = 3;
        public int spriteDirection = -1;
        public static int sWidth = 0x690;
        public int target = -1;
        public Rectangle targetRect;
        public int timeLeft;
        public bool townNPC = false;
        private static int townRangeX = sWidth;
        private static int townRangeY = sHeight;
        public int type;
        public float value;
        public Vector2 velocity;
        public bool wet = false;
        public byte wetCount = 0;
        public int whoAmI = 0;
        public int width;

        public void AI()
        {
            int num;
            int num3;
            int num4;
            float num9;
            float num10;
            Vector2 vector;
            float num11;
            float num12;
            float num13;
            int num19;
            int whoAmI;
            int num28;
            int num32;
            int num33;
            bool flag8;
            bool flag9;
            Color color;
            if (this.aiStyle == 0)
            {
                this.velocity.X *= 0.93f;
                if ((this.velocity.X > -0.1) && (this.velocity.X < 0.1))
                {
                    this.velocity.X = 0f;
                }
                return;
            }
            if (this.aiStyle == 1)
            {
                this.aiAction = 0;
                if (this.ai[2] == 0f)
                {
                    this.ai[0] = -100f;
                    this.ai[2] = 1f;
                    this.TargetClosest(true);
                }
                if (this.velocity.Y == 0f)
                {
                    if (this.ai[3] == this.position.X)
                    {
                        this.direction *= -1;
                    }
                    this.ai[3] = 0f;
                    this.velocity.X *= 0.8f;
                    if ((this.velocity.X > -0.1) && (this.velocity.X < 0.1))
                    {
                        this.velocity.X = 0f;
                    }
                    if ((!Main.dayTime || (this.life != this.lifeMax)) || (this.position.Y > (Main.worldSurface * 16.0)))
                    {
                        this.ai[0]++;
                    }
                    this.ai[0]++;
                    if (this.ai[0] >= 0f)
                    {
                        this.netUpdate = true;
                        if ((!Main.dayTime || (this.life != this.lifeMax)) || (this.position.Y > (Main.worldSurface * 16.0)))
                        {
                            this.TargetClosest(true);
                        }
                        if (this.ai[1] == 2f)
                        {
                            this.velocity.Y = -8f;
                            this.velocity.X += 3 * this.direction;
                            this.ai[0] = -200f;
                            this.ai[1] = 0f;
                            this.ai[3] = this.position.X;
                        }
                        else
                        {
                            this.velocity.Y = -6f;
                            this.velocity.X += 2 * this.direction;
                            this.ai[0] = -120f;
                            this.ai[1]++;
                        }
                    }
                    else if (this.ai[0] >= -30f)
                    {
                        this.aiAction = 1;
                    }
                }
                else if ((this.target < 0xff) && (((this.direction == 1) && (this.velocity.X < 3f)) || ((this.direction == -1) && (this.velocity.X > -3f))))
                {
                    if (((this.direction == -1) && (this.velocity.X < 0.1)) || ((this.direction == 1) && (this.velocity.X > -0.1)))
                    {
                        this.velocity.X += 0.2f * this.direction;
                    }
                    else
                    {
                        this.velocity.X *= 0.93f;
                    }
                }
                return;
            }
            if (this.aiStyle == 2)
            {
                this.noGravity = true;
                if (this.collideX)
                {
                    this.velocity.X = this.oldVelocity.X * -0.5f;
                    if (((this.direction == -1) && (this.velocity.X > 0f)) && (this.velocity.X < 2f))
                    {
                        this.velocity.X = 2f;
                    }
                    if (((this.direction == 1) && (this.velocity.X < 0f)) && (this.velocity.X > -2f))
                    {
                        this.velocity.X = -2f;
                    }
                }
                if (this.collideY)
                {
                    this.velocity.Y = this.oldVelocity.Y * -0.5f;
                    if ((this.velocity.Y > 0f) && (this.velocity.Y < 1f))
                    {
                        this.velocity.Y = 1f;
                    }
                    if ((this.velocity.Y < 0f) && (this.velocity.Y > -1f))
                    {
                        this.velocity.Y = -1f;
                    }
                }
                if ((Main.dayTime && (this.position.Y <= (Main.worldSurface * 16.0))) && (this.type == 2))
                {
                    if (this.timeLeft > 10)
                    {
                        this.timeLeft = 10;
                    }
                    this.directionY = -1;
                    if (this.velocity.Y > 0f)
                    {
                        this.direction = 1;
                    }
                    this.direction = -1;
                    if (this.velocity.X > 0f)
                    {
                        this.direction = 1;
                    }
                }
                else
                {
                    this.TargetClosest(true);
                }
                if ((this.direction == -1) && (this.velocity.X > -4f))
                {
                    this.velocity.X -= 0.1f;
                    if (this.velocity.X > 4f)
                    {
                        this.velocity.X -= 0.1f;
                    }
                    else if (this.velocity.X > 0f)
                    {
                        this.velocity.X += 0.05f;
                    }
                    if (this.velocity.X < -4f)
                    {
                        this.velocity.X = -4f;
                    }
                }
                else if ((this.direction == 1) && (this.velocity.X < 4f))
                {
                    this.velocity.X += 0.1f;
                    if (this.velocity.X < -4f)
                    {
                        this.velocity.X += 0.1f;
                    }
                    else if (this.velocity.X < 0f)
                    {
                        this.velocity.X -= 0.05f;
                    }
                    if (this.velocity.X > 4f)
                    {
                        this.velocity.X = 4f;
                    }
                }
                if ((this.directionY == -1) && (this.velocity.Y > -1.5))
                {
                    this.velocity.Y -= 0.04f;
                    if (this.velocity.Y > 1.5)
                    {
                        this.velocity.Y -= 0.05f;
                    }
                    else if (this.velocity.Y > 0f)
                    {
                        this.velocity.Y += 0.03f;
                    }
                    if (this.velocity.Y < -1.5)
                    {
                        this.velocity.Y = -1.5f;
                    }
                }
                else if ((this.directionY == 1) && (this.velocity.Y < 1.5))
                {
                    this.velocity.Y += 0.04f;
                    if (this.velocity.Y < -1.5)
                    {
                        this.velocity.Y += 0.05f;
                    }
                    else if (this.velocity.Y < 0f)
                    {
                        this.velocity.Y -= 0.03f;
                    }
                    if (this.velocity.Y > 1.5)
                    {
                        this.velocity.Y = 1.5f;
                    }
                }
                if ((this.type == 2) && (Main.rand.Next(40) == 0))
                {
                    num = Dust.NewDust(new Vector2(this.position.X, this.position.Y + (this.height * 0.25f)), this.width, (int) (this.height * 0.5f), 5, this.velocity.X, 2f, 0, new Color(), 1f);
                    Main.dust[num].velocity.X *= 0.5f;
                    Main.dust[num].velocity.Y *= 0.1f;
                }
                return;
            }
            if (this.aiStyle == 3)
            {
                int num2 = 60;
                bool flag = false;
                if ((this.velocity.Y == 0f) && (((this.velocity.X > 0f) && (this.direction < 0)) || ((this.velocity.X < 0f) && (this.direction > 0))))
                {
                    flag = true;
                }
                if (((this.position.X == this.oldPosition.X) || (this.ai[3] >= num2)) || flag)
                {
                    this.ai[3]++;
                }
                else if ((Math.Abs(this.velocity.X) > 0.9) && (this.ai[3] > 0f))
                {
                    this.ai[3]--;
                }
                if (this.ai[3] > (num2 * 10))
                {
                    this.ai[3] = 0f;
                }
                if (this.ai[3] == num2)
                {
                    this.netUpdate = true;
                }
                if ((((!Main.dayTime || (this.position.Y > (Main.worldSurface * 16.0))) || ((this.type == 0x1a) || (this.type == 0x1b))) || (((this.type == 0x1c) || (this.type == 0x1f)) || (this.type == 0x2f))) && (this.ai[3] < num2))
                {
                    if ((((this.type == 3) || (this.type == 0x15)) || (this.type == 0x1f)) && (Main.rand.Next(0x3e8) == 0))
                    {
                        Main.PlaySound(14, (int) this.position.X, (int) this.position.Y, 1);
                    }
                    this.TargetClosest(true);
                }
                else
                {
                    if ((Main.dayTime && ((this.position.Y / 16f) < Main.worldSurface)) && (this.timeLeft > 10))
                    {
                        this.timeLeft = 10;
                    }
                    if (this.velocity.X == 0f)
                    {
                        if (this.velocity.Y == 0f)
                        {
                            this.ai[0]++;
                            if (this.ai[0] >= 2f)
                            {
                                this.direction *= -1;
                                this.spriteDirection = this.direction;
                                this.ai[0] = 0f;
                            }
                        }
                    }
                    else
                    {
                        this.ai[0] = 0f;
                    }
                    if (this.direction == 0)
                    {
                        this.direction = 1;
                    }
                }
                if (this.type == 0x1b)
                {
                    if ((this.velocity.X < -2f) || (this.velocity.X > 2f))
                    {
                        if (this.velocity.Y == 0f)
                        {
                            this.velocity = (Vector2) (this.velocity * 0.8f);
                        }
                    }
                    else if ((this.velocity.X < 2f) && (this.direction == 1))
                    {
                        this.velocity.X += 0.07f;
                        if (this.velocity.X > 2f)
                        {
                            this.velocity.X = 2f;
                        }
                    }
                    else if ((this.velocity.X > -2f) && (this.direction == -1))
                    {
                        this.velocity.X -= 0.07f;
                        if (this.velocity.X < -2f)
                        {
                            this.velocity.X = -2f;
                        }
                    }
                }
                else if ((((this.type == 0x15) || (this.type == 0x1a)) || (this.type == 0x1f)) || (this.type == 0x2f))
                {
                    if ((this.velocity.X < -1.5f) || (this.velocity.X > 1.5f))
                    {
                        if (this.velocity.Y == 0f)
                        {
                            this.velocity = (Vector2) (this.velocity * 0.8f);
                        }
                    }
                    else if ((this.velocity.X < 1.5f) && (this.direction == 1))
                    {
                        this.velocity.X += 0.07f;
                        if (this.velocity.X > 1.5f)
                        {
                            this.velocity.X = 1.5f;
                        }
                    }
                    else if ((this.velocity.X > -1.5f) && (this.direction == -1))
                    {
                        this.velocity.X -= 0.07f;
                        if (this.velocity.X < -1.5f)
                        {
                            this.velocity.X = -1.5f;
                        }
                    }
                }
                else if ((this.velocity.X < -1f) || (this.velocity.X > 1f))
                {
                    if (this.velocity.Y == 0f)
                    {
                        this.velocity = (Vector2) (this.velocity * 0.8f);
                    }
                }
                else if ((this.velocity.X < 1f) && (this.direction == 1))
                {
                    this.velocity.X += 0.07f;
                    if (this.velocity.X > 1f)
                    {
                        this.velocity.X = 1f;
                    }
                }
                else if ((this.velocity.X > -1f) && (this.direction == -1))
                {
                    this.velocity.X -= 0.07f;
                    if (this.velocity.X < -1f)
                    {
                        this.velocity.X = -1f;
                    }
                }
                if (this.velocity.Y == 0f)
                {
                    num3 = (int) (((this.position.X + (this.width / 2)) + (15 * this.direction)) / 16f);
                    num4 = (int) (((this.position.Y + this.height) - 16f) / 16f);
                    if (Main.tile[num3, num4] == null)
                    {
                        Main.tile[num3, num4] = new Tile();
                    }
                    if (Main.tile[num3, num4 - 1] == null)
                    {
                        Main.tile[num3, num4 - 1] = new Tile();
                    }
                    if (Main.tile[num3, num4 - 2] == null)
                    {
                        Main.tile[num3, num4 - 2] = new Tile();
                    }
                    if (Main.tile[num3, num4 - 3] == null)
                    {
                        Main.tile[num3, num4 - 3] = new Tile();
                    }
                    if (Main.tile[num3, num4 + 1] == null)
                    {
                        Main.tile[num3, num4 + 1] = new Tile();
                    }
                    if (Main.tile[num3 + this.direction, num4 - 1] == null)
                    {
                        Main.tile[num3 + this.direction, num4 - 1] = new Tile();
                    }
                    if (Main.tile[num3 + this.direction, num4 + 1] == null)
                    {
                        Main.tile[num3 + this.direction, num4 + 1] = new Tile();
                    }
                    bool flag2 = true;
                    if (this.type == 0x2f)
                    {
                        flag2 = false;
                    }
                    if ((Main.tile[num3, num4 - 1].active && (Main.tile[num3, num4 - 1].type == 10)) && flag2)
                    {
                        this.ai[2]++;
                        this.ai[3] = 0f;
                        if (this.ai[2] >= 60f)
                        {
                            if (!(Main.bloodMoon || (this.type != 3)))
                            {
                                this.ai[1] = 0f;
                            }
                            this.velocity.X = 0.5f * -this.direction;
                            this.ai[1]++;
                            if (this.type == 0x1b)
                            {
                                this.ai[1]++;
                            }
                            if (this.type == 0x1f)
                            {
                                this.ai[1] += 6f;
                            }
                            this.ai[2] = 0f;
                            bool flag3 = false;
                            if (this.ai[1] >= 10f)
                            {
                                flag3 = true;
                                this.ai[1] = 10f;
                            }
                            WorldGen.KillTile(num3, num4 - 1, true, false, false);
                            if (((Main.netMode != 1) || !flag3) && (flag3 && (Main.netMode != 1)))
                            {
                                if (this.type == 0x1a)
                                {
                                    WorldGen.KillTile(num3, num4 - 1, false, false, false);
                                    if (Main.netMode == 2)
                                    {
                                        NetMessage.SendData(0x11, -1, -1, "", 0, (float) num3, (float) (num4 - 1), 0f);
                                    }
                                }
                                else
                                {
                                    bool flag4 = WorldGen.OpenDoor(num3, num4, this.direction);
                                    if (!flag4)
                                    {
                                        this.ai[3] = num2;
                                        this.netUpdate = true;
                                    }
                                    if ((Main.netMode == 2) && flag4)
                                    {
                                        NetMessage.SendData(0x13, -1, -1, "", 0, (float) num3, (float) num4, (float) this.direction);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (((this.velocity.X < 0f) && (this.spriteDirection == -1)) || ((this.velocity.X > 0f) && (this.spriteDirection == 1)))
                        {
                            if (Main.tile[num3, num4 - 2].active && Main.tileSolid[Main.tile[num3, num4 - 2].type])
                            {
                                if (Main.tile[num3, num4 - 3].active && Main.tileSolid[Main.tile[num3, num4 - 3].type])
                                {
                                    this.velocity.Y = -8f;
                                    this.netUpdate = true;
                                }
                                else
                                {
                                    this.velocity.Y = -7f;
                                    this.netUpdate = true;
                                }
                            }
                            else if (Main.tile[num3, num4 - 1].active && Main.tileSolid[Main.tile[num3, num4 - 1].type])
                            {
                                this.velocity.Y = -6f;
                                this.netUpdate = true;
                            }
                            else if (Main.tile[num3, num4].active && Main.tileSolid[Main.tile[num3, num4].type])
                            {
                                this.velocity.Y = -5f;
                                this.netUpdate = true;
                            }
                            else if (((this.directionY < 0) && (!Main.tile[num3, num4 + 1].active || !Main.tileSolid[Main.tile[num3, num4 + 1].type])) && (!Main.tile[num3 + this.direction, num4 + 1].active || !Main.tileSolid[Main.tile[num3 + this.direction, num4 + 1].type]))
                            {
                                this.velocity.Y = -8f;
                                this.velocity.X *= 1.5f;
                                this.netUpdate = true;
                            }
                            else
                            {
                                this.ai[1] = 0f;
                                this.ai[2] = 0f;
                            }
                        }
                        if (((((this.type == 0x1f) || (this.type == 0x2f)) && (this.velocity.Y == 0f)) && ((Math.Abs((float) ((this.position.X + (this.width / 2)) - (Main.player[this.target].position.X + (Main.player[this.target].width / 2)))) < 100f) && (Math.Abs((float) ((this.position.Y + (this.height / 2)) - (Main.player[this.target].position.Y + (Main.player[this.target].height / 2)))) < 50f))) && (((this.direction > 0) && (this.velocity.X >= 1f)) || ((this.direction < 0) && (this.velocity.X <= -1f))))
                        {
                            this.velocity.X *= 2f;
                            if (this.velocity.X > 3f)
                            {
                                this.velocity.X = 3f;
                            }
                            if (this.velocity.X < -3f)
                            {
                                this.velocity.X = -3f;
                            }
                            this.velocity.Y = -4f;
                            this.netUpdate = true;
                        }
                    }
                }
                else
                {
                    this.ai[1] = 0f;
                    this.ai[2] = 0f;
                }
                return;
            }
            if (this.aiStyle == 4)
            {
                if (!((((this.target >= 0) && (this.target != 0xff)) && !Main.player[this.target].dead) && Main.player[this.target].active))
                {
                    this.TargetClosest(true);
                }
                bool dead = Main.player[this.target].dead;
                float num5 = ((this.position.X + (this.width / 2)) - Main.player[this.target].position.X) - (Main.player[this.target].width / 2);
                float num6 = (((this.position.Y + this.height) - 59f) - Main.player[this.target].position.Y) - (Main.player[this.target].height / 2);
                float num7 = ((float) Math.Atan2((double) num6, (double) num5)) + 1.57f;
                if (num7 < 0f)
                {
                    num7 += 6.283f;
                }
                else if (num7 > 6.283)
                {
                    num7 -= 6.283f;
                }
                float num8 = 0f;
                if ((this.ai[0] == 0f) && (this.ai[1] == 0f))
                {
                    num8 = 0.02f;
                }
                if (((this.ai[0] == 0f) && (this.ai[1] == 2f)) && (this.ai[2] > 40f))
                {
                    num8 = 0.05f;
                }
                if ((this.ai[0] == 3f) && (this.ai[1] == 0f))
                {
                    num8 = 0.05f;
                }
                if (((this.ai[0] == 3f) && (this.ai[1] == 2f)) && (this.ai[2] > 40f))
                {
                    num8 = 0.08f;
                }
                if (this.rotation < num7)
                {
                    if ((num7 - this.rotation) > 3.1415)
                    {
                        this.rotation -= num8;
                    }
                    else
                    {
                        this.rotation += num8;
                    }
                }
                else if (this.rotation > num7)
                {
                    if ((this.rotation - num7) > 3.1415)
                    {
                        this.rotation += num8;
                    }
                    else
                    {
                        this.rotation -= num8;
                    }
                }
                if ((this.rotation > (num7 - num8)) && (this.rotation < (num7 + num8)))
                {
                    this.rotation = num7;
                }
                if (this.rotation < 0f)
                {
                    this.rotation += 6.283f;
                }
                else if (this.rotation > 6.283)
                {
                    this.rotation -= 6.283f;
                }
                if ((this.rotation > (num7 - num8)) && (this.rotation < (num7 + num8)))
                {
                    this.rotation = num7;
                }
                if (Main.rand.Next(5) == 0)
                {
                    color = new Color();
                    num = Dust.NewDust(new Vector2(this.position.X, this.position.Y + (this.height * 0.25f)), this.width, (int) (this.height * 0.5f), 5, this.velocity.X, 2f, 0, color, 1f);
                    Main.dust[num].velocity.X *= 0.5f;
                    Main.dust[num].velocity.Y *= 0.1f;
                }
                if (Main.dayTime || dead)
                {
                    this.velocity.Y -= 0.04f;
                    if (this.timeLeft > 10)
                    {
                        this.timeLeft = 10;
                    }
                }
                else if (this.ai[0] == 0f)
                {
                    if (this.ai[1] == 0f)
                    {
                        num9 = 5f;
                        num10 = 0.04f;
                        vector = new Vector2(this.position.X + (this.width * 0.5f), this.position.Y + (this.height * 0.5f));
                        num11 = (Main.player[this.target].position.X + (Main.player[this.target].width / 2)) - vector.X;
                        num12 = ((Main.player[this.target].position.Y + (Main.player[this.target].height / 2)) - 200f) - vector.Y;
                        num13 = (float) Math.Sqrt((double) ((num11 * num11) + (num12 * num12)));
                        float num14 = num13;
                        num13 = num9 / num13;
                        num11 *= num13;
                        num12 *= num13;
                        if (this.velocity.X < num11)
                        {
                            this.velocity.X += num10;
                            if ((this.velocity.X < 0f) && (num11 > 0f))
                            {
                                this.velocity.X += num10;
                            }
                        }
                        else if (this.velocity.X > num11)
                        {
                            this.velocity.X -= num10;
                            if ((this.velocity.X > 0f) && (num11 < 0f))
                            {
                                this.velocity.X -= num10;
                            }
                        }
                        if (this.velocity.Y < num12)
                        {
                            this.velocity.Y += num10;
                            if ((this.velocity.Y < 0f) && (num12 > 0f))
                            {
                                this.velocity.Y += num10;
                            }
                        }
                        else if (this.velocity.Y > num12)
                        {
                            this.velocity.Y -= num10;
                            if ((this.velocity.Y > 0f) && (num12 < 0f))
                            {
                                this.velocity.Y -= num10;
                            }
                        }
                        this.ai[2]++;
                        if (this.ai[2] >= 600f)
                        {
                            this.ai[1] = 1f;
                            this.ai[2] = 0f;
                            this.ai[3] = 0f;
                            this.target = 0xff;
                            this.netUpdate = true;
                        }
                        else if (((this.position.Y + this.height) < Main.player[this.target].position.Y) && (num14 < 500f))
                        {
                            if (!Main.player[this.target].dead)
                            {
                                this.ai[3]++;
                            }
                            if (this.ai[3] >= 90f)
                            {
                                Vector2 vector3;
                                this.ai[3] = 0f;
                                this.rotation = num7;
                                float num15 = 5f;
                                float num16 = (Main.player[this.target].position.X + (Main.player[this.target].width / 2)) - vector.X;
                                float num17 = (Main.player[this.target].position.Y + (Main.player[this.target].height / 2)) - vector.Y;
                                float num18 = (float) Math.Sqrt((double) ((num16 * num16) + (num17 * num17)));
                                num18 = num15 / num18;
                                Vector2 position = vector;
                                vector3.X = num16 * num18;
                                vector3.Y = num17 * num18;
                                position.X += vector3.X * 10f;
                                position.Y += vector3.Y * 10f;
                                if (Main.netMode != 1)
                                {
                                    num19 = NewNPC((int) position.X, (int) position.Y, 5, 0);
                                    Main.npc[num19].velocity.X = vector3.X;
                                    Main.npc[num19].velocity.Y = vector3.Y;
                                    if ((Main.netMode == 2) && (num19 < 0x3e8))
                                    {
                                        NetMessage.SendData(0x17, -1, -1, "", num19, 0f, 0f, 0f);
                                    }
                                }
                                Main.PlaySound(3, (int) position.X, (int) position.Y, 1);
                                for (whoAmI = 0; whoAmI < 10; whoAmI++)
                                {
                                    color = new Color();
                                    Dust.NewDust(position, 20, 20, 5, vector3.X * 0.4f, vector3.Y * 0.4f, 0, color, 1f);
                                }
                            }
                        }
                    }
                    else if (this.ai[1] == 1f)
                    {
                        this.rotation = num7;
                        num9 = 7f;
                        vector = new Vector2(this.position.X + (this.width * 0.5f), this.position.Y + (this.height * 0.5f));
                        num11 = (Main.player[this.target].position.X + (Main.player[this.target].width / 2)) - vector.X;
                        num12 = (Main.player[this.target].position.Y + (Main.player[this.target].height / 2)) - vector.Y;
                        num13 = (float) Math.Sqrt((double) ((num11 * num11) + (num12 * num12)));
                        num13 = num9 / num13;
                        this.velocity.X = num11 * num13;
                        this.velocity.Y = num12 * num13;
                        this.ai[1] = 2f;
                    }
                    else if (this.ai[1] == 2f)
                    {
                        this.ai[2]++;
                        if (this.ai[2] >= 40f)
                        {
                            this.velocity.X *= 0.98f;
                            this.velocity.Y *= 0.98f;
                            if ((this.velocity.X > -0.1) && (this.velocity.X < 0.1))
                            {
                                this.velocity.X = 0f;
                            }
                            if ((this.velocity.Y > -0.1) && (this.velocity.Y < 0.1))
                            {
                                this.velocity.Y = 0f;
                            }
                        }
                        else
                        {
                            this.rotation = ((float) Math.Atan2((double) this.velocity.Y, (double) this.velocity.X)) - 1.57f;
                        }
                        if (this.ai[2] >= 120f)
                        {
                            this.ai[3]++;
                            this.ai[2] = 0f;
                            this.target = 0xff;
                            this.rotation = num7;
                            if (this.ai[3] >= 3f)
                            {
                                this.ai[1] = 0f;
                                this.ai[3] = 0f;
                            }
                            else
                            {
                                this.ai[1] = 1f;
                            }
                        }
                    }
                    if (this.life < (this.lifeMax * 0.5))
                    {
                        this.ai[0] = 1f;
                        this.ai[1] = 0f;
                        this.ai[2] = 0f;
                        this.ai[3] = 0f;
                        this.netUpdate = true;
                    }
                }
                else if ((this.ai[0] == 1f) || (this.ai[0] == 2f))
                {
                    if (this.ai[0] == 1f)
                    {
                        this.ai[2] += 0.005f;
                        if (this.ai[2] > 0.5)
                        {
                            this.ai[2] = 0.5f;
                        }
                    }
                    else
                    {
                        this.ai[2] -= 0.005f;
                        if (this.ai[2] < 0f)
                        {
                            this.ai[2] = 0f;
                        }
                    }
                    this.rotation += this.ai[2];
                    this.ai[1]++;
                    if (this.ai[1] == 100f)
                    {
                        this.ai[0]++;
                        this.ai[1] = 0f;
                        if (this.ai[0] == 3f)
                        {
                            this.ai[2] = 0f;
                        }
                        else
                        {
                            Main.PlaySound(3, (int) this.position.X, (int) this.position.Y, 1);
                            for (whoAmI = 0; whoAmI < 2; whoAmI++)
                            {
                                Gore.NewGore(this.position, new Vector2(Main.rand.Next(-30, 0x1f) * 0.2f, Main.rand.Next(-30, 0x1f) * 0.2f), 8);
                                Gore.NewGore(this.position, new Vector2(Main.rand.Next(-30, 0x1f) * 0.2f, Main.rand.Next(-30, 0x1f) * 0.2f), 7);
                                Gore.NewGore(this.position, new Vector2(Main.rand.Next(-30, 0x1f) * 0.2f, Main.rand.Next(-30, 0x1f) * 0.2f), 6);
                            }
                            for (whoAmI = 0; whoAmI < 20; whoAmI++)
                            {
                                color = new Color();
                                Dust.NewDust(this.position, this.width, this.height, 5, Main.rand.Next(-30, 0x1f) * 0.2f, Main.rand.Next(-30, 0x1f) * 0.2f, 0, color, 1f);
                            }
                            Main.PlaySound(15, (int) this.position.X, (int) this.position.Y, 0);
                        }
                    }
                    Dust.NewDust(this.position, this.width, this.height, 5, Main.rand.Next(-30, 0x1f) * 0.2f, Main.rand.Next(-30, 0x1f) * 0.2f, 0, new Color(), 1f);
                    this.velocity.X *= 0.98f;
                    this.velocity.Y *= 0.98f;
                    if ((this.velocity.X > -0.1) && (this.velocity.X < 0.1))
                    {
                        this.velocity.X = 0f;
                    }
                    if ((this.velocity.Y > -0.1) && (this.velocity.Y < 0.1))
                    {
                        this.velocity.Y = 0f;
                    }
                }
                else
                {
                    this.damage = 30;
                    this.defense = 6;
                    if (this.ai[1] == 0f)
                    {
                        num9 = 6f;
                        num10 = 0.07f;
                        vector = new Vector2(this.position.X + (this.width * 0.5f), this.position.Y + (this.height * 0.5f));
                        num11 = (Main.player[this.target].position.X + (Main.player[this.target].width / 2)) - vector.X;
                        num12 = ((Main.player[this.target].position.Y + (Main.player[this.target].height / 2)) - 120f) - vector.Y;
                        num13 = (float) Math.Sqrt((double) ((num11 * num11) + (num12 * num12)));
                        num13 = num9 / num13;
                        num11 *= num13;
                        num12 *= num13;
                        if (this.velocity.X < num11)
                        {
                            this.velocity.X += num10;
                            if ((this.velocity.X < 0f) && (num11 > 0f))
                            {
                                this.velocity.X += num10;
                            }
                        }
                        else if (this.velocity.X > num11)
                        {
                            this.velocity.X -= num10;
                            if ((this.velocity.X > 0f) && (num11 < 0f))
                            {
                                this.velocity.X -= num10;
                            }
                        }
                        if (this.velocity.Y < num12)
                        {
                            this.velocity.Y += num10;
                            if ((this.velocity.Y < 0f) && (num12 > 0f))
                            {
                                this.velocity.Y += num10;
                            }
                        }
                        else if (this.velocity.Y > num12)
                        {
                            this.velocity.Y -= num10;
                            if ((this.velocity.Y > 0f) && (num12 < 0f))
                            {
                                this.velocity.Y -= num10;
                            }
                        }
                        this.ai[2]++;
                        if (this.ai[2] >= 200f)
                        {
                            this.ai[1] = 1f;
                            this.ai[2] = 0f;
                            this.ai[3] = 0f;
                            this.target = 0xff;
                            this.netUpdate = true;
                        }
                    }
                    else if (this.ai[1] == 1f)
                    {
                        Main.PlaySound(15, (int) this.position.X, (int) this.position.Y, 0);
                        this.rotation = num7;
                        num9 = 8f;
                        vector = new Vector2(this.position.X + (this.width * 0.5f), this.position.Y + (this.height * 0.5f));
                        num11 = (Main.player[this.target].position.X + (Main.player[this.target].width / 2)) - vector.X;
                        num12 = (Main.player[this.target].position.Y + (Main.player[this.target].height / 2)) - vector.Y;
                        num13 = (float) Math.Sqrt((double) ((num11 * num11) + (num12 * num12)));
                        num13 = num9 / num13;
                        this.velocity.X = num11 * num13;
                        this.velocity.Y = num12 * num13;
                        this.ai[1] = 2f;
                    }
                    else if (this.ai[1] == 2f)
                    {
                        this.ai[2]++;
                        if (this.ai[2] >= 40f)
                        {
                            this.velocity.X *= 0.97f;
                            this.velocity.Y *= 0.97f;
                            if ((this.velocity.X > -0.1) && (this.velocity.X < 0.1))
                            {
                                this.velocity.X = 0f;
                            }
                            if ((this.velocity.Y > -0.1) && (this.velocity.Y < 0.1))
                            {
                                this.velocity.Y = 0f;
                            }
                        }
                        else
                        {
                            this.rotation = ((float) Math.Atan2((double) this.velocity.Y, (double) this.velocity.X)) - 1.57f;
                        }
                        if (this.ai[2] >= 100f)
                        {
                            this.ai[3]++;
                            this.ai[2] = 0f;
                            this.target = 0xff;
                            this.rotation = num7;
                            if (this.ai[3] >= 3f)
                            {
                                this.ai[1] = 0f;
                                this.ai[3] = 0f;
                            }
                            else
                            {
                                this.ai[1] = 1f;
                            }
                        }
                    }
                }
                return;
            }
            if (this.aiStyle == 5)
            {
                if (((this.target < 0) || (this.target == 0xff)) || Main.player[this.target].dead)
                {
                    this.TargetClosest(true);
                }
                num9 = 6f;
                num10 = 0.05f;
                if (this.type == 6)
                {
                    num9 = 4f;
                    num10 = 0.02f;
                }
                else if (this.type == 0x17)
                {
                    num9 = 2.5f;
                    num10 = 0.02f;
                }
                vector = new Vector2(this.position.X + (this.width * 0.5f), this.position.Y + (this.height * 0.5f));
                num11 = (Main.player[this.target].position.X + (Main.player[this.target].width / 2)) - vector.X;
                num12 = (Main.player[this.target].position.Y + (Main.player[this.target].height / 2)) - vector.Y;
                num13 = (float) Math.Sqrt((double) ((num11 * num11) + (num12 * num12)));
                num13 = num9 / num13;
                num11 *= num13;
                num12 *= num13;
                if (Main.player[this.target].dead)
                {
                    num11 = (this.direction * num9) / 2f;
                    num12 = -num9 / 2f;
                }
                if (this.velocity.X < num11)
                {
                    this.velocity.X += num10;
                    if ((this.velocity.X < 0f) && (num11 > 0f))
                    {
                        this.velocity.X += num10;
                    }
                }
                else if (this.velocity.X > num11)
                {
                    this.velocity.X -= num10;
                    if ((this.velocity.X > 0f) && (num11 < 0f))
                    {
                        this.velocity.X -= num10;
                    }
                }
                if (this.velocity.Y < num12)
                {
                    this.velocity.Y += num10;
                    if ((this.velocity.Y < 0f) && (num12 > 0f))
                    {
                        this.velocity.Y += num10;
                    }
                }
                else if (this.velocity.Y > num12)
                {
                    this.velocity.Y -= num10;
                    if ((this.velocity.Y > 0f) && (num12 < 0f))
                    {
                        this.velocity.Y -= num10;
                    }
                }
                if (this.type == 0x17)
                {
                    this.rotation = (float) Math.Atan2((double) this.velocity.Y, (double) this.velocity.X);
                }
                else
                {
                    this.rotation = ((float) Math.Atan2((double) this.velocity.Y, (double) this.velocity.X)) - 1.57f;
                }
                if ((this.type == 6) || (this.type == 0x17))
                {
                    if (this.collideX)
                    {
                        this.netUpdate = true;
                        this.velocity.X = this.oldVelocity.X * -0.7f;
                        if (((this.direction == -1) && (this.velocity.X > 0f)) && (this.velocity.X < 2f))
                        {
                            this.velocity.X = 2f;
                        }
                        if (((this.direction == 1) && (this.velocity.X < 0f)) && (this.velocity.X > -2f))
                        {
                            this.velocity.X = -2f;
                        }
                    }
                    if (this.collideY)
                    {
                        this.netUpdate = true;
                        this.velocity.Y = this.oldVelocity.Y * -0.7f;
                        if ((this.velocity.Y > 0f) && (this.velocity.Y < 2f))
                        {
                            this.velocity.Y = 2f;
                        }
                        if ((this.velocity.Y < 0f) && (this.velocity.Y > -2f))
                        {
                            this.velocity.Y = -2f;
                        }
                    }
                    if (this.type == 0x17)
                    {
                        num = Dust.NewDust(new Vector2(this.position.X - this.velocity.X, this.position.Y - this.velocity.Y), this.width, this.height, 6, this.velocity.X * 0.2f, this.velocity.Y * 0.2f, 100, new Color(), 2f);
                        Main.dust[num].noGravity = true;
                        Main.dust[num].velocity.X *= 0.3f;
                        Main.dust[num].velocity.Y *= 0.3f;
                    }
                    else if (Main.rand.Next(20) == 0)
                    {
                        num = Dust.NewDust(new Vector2(this.position.X, this.position.Y + (this.height * 0.25f)), this.width, (int) (this.height * 0.5f), 0x12, this.velocity.X, 2f, this.alpha, this.color, this.scale);
                        Main.dust[num].velocity.X *= 0.5f;
                        Main.dust[num].velocity.Y *= 0.1f;
                    }
                }
                else if (Main.rand.Next(40) == 0)
                {
                    num = Dust.NewDust(new Vector2(this.position.X, this.position.Y + (this.height * 0.25f)), this.width, (int) (this.height * 0.5f), 5, this.velocity.X, 2f, 0, new Color(), 1f);
                    Main.dust[num].velocity.X *= 0.5f;
                    Main.dust[num].velocity.Y *= 0.1f;
                }
                if (((Main.dayTime && (this.type != 6)) && (this.type != 0x17)) || Main.player[this.target].dead)
                {
                    this.velocity.Y -= num10 * 2f;
                    if (this.timeLeft > 10)
                    {
                        this.timeLeft = 10;
                    }
                }
                return;
            }
            if (this.aiStyle != 6)
            {
                if (this.aiStyle != 7)
                {
                    if (this.aiStyle == 8)
                    {
                        int num43;
                        this.TargetClosest(true);
                        this.velocity.X *= 0.93f;
                        if ((this.velocity.X > -0.1) && (this.velocity.X < 0.1))
                        {
                            this.velocity.X = 0f;
                        }
                        if (this.ai[0] == 0f)
                        {
                            this.ai[0] = 500f;
                        }
                        if ((this.ai[2] != 0f) && (this.ai[3] != 0f))
                        {
                            Main.PlaySound(2, (int) this.position.X, (int) this.position.Y, 8);
                            for (whoAmI = 0; whoAmI < 50; whoAmI++)
                            {
                                if ((this.type == 0x1d) || (this.type == 0x2d))
                                {
                                    color = new Color();
                                    num = Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 0x1b, 0f, 0f, 100, color, (float) Main.rand.Next(1, 3));
                                    Dust dust1 = Main.dust[num];
                                    dust1.velocity = (Vector2) (dust1.velocity * 3f);
                                    if (Main.dust[num].scale > 1f)
                                    {
                                        Main.dust[num].noGravity = true;
                                    }
                                }
                                else if (this.type == 0x20)
                                {
                                    color = new Color();
                                    num = Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 0x1d, 0f, 0f, 100, color, 2.5f);
                                    Dust dust2 = Main.dust[num];
                                    dust2.velocity = (Vector2) (dust2.velocity * 3f);
                                    Main.dust[num].noGravity = true;
                                }
                                else
                                {
                                    color = new Color();
                                    num = Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 6, 0f, 0f, 100, color, 2.5f);
                                    Dust dust3 = Main.dust[num];
                                    dust3.velocity = (Vector2) (dust3.velocity * 3f);
                                    Main.dust[num].noGravity = true;
                                }
                            }
                            this.position.X = ((this.ai[2] * 16f) - (this.width / 2)) + 8f;
                            this.position.Y = (this.ai[3] * 16f) - this.height;
                            this.velocity.X = 0f;
                            this.velocity.Y = 0f;
                            this.ai[2] = 0f;
                            this.ai[3] = 0f;
                            Main.PlaySound(2, (int) this.position.X, (int) this.position.Y, 8);
                            for (whoAmI = 0; whoAmI < 50; whoAmI++)
                            {
                                if ((this.type == 0x1d) || (this.type == 0x2d))
                                {
                                    color = new Color();
                                    num = Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 0x1b, 0f, 0f, 100, color, (float) Main.rand.Next(1, 3));
                                    Dust dust4 = Main.dust[num];
                                    dust4.velocity = (Vector2) (dust4.velocity * 3f);
                                    if (Main.dust[num].scale > 1f)
                                    {
                                        Main.dust[num].noGravity = true;
                                    }
                                }
                                else if (this.type == 0x20)
                                {
                                    color = new Color();
                                    num = Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 0x1d, 0f, 0f, 100, color, 2.5f);
                                    Dust dust5 = Main.dust[num];
                                    dust5.velocity = (Vector2) (dust5.velocity * 3f);
                                    Main.dust[num].noGravity = true;
                                }
                                else
                                {
                                    color = new Color();
                                    num = Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 6, 0f, 0f, 100, color, 2.5f);
                                    Dust dust6 = Main.dust[num];
                                    dust6.velocity = (Vector2) (dust6.velocity * 3f);
                                    Main.dust[num].noGravity = true;
                                }
                            }
                        }
                        this.ai[0]++;
                        if (((this.ai[0] == 75f) || (this.ai[0] == 150f)) || (this.ai[0] == 225f))
                        {
                            this.ai[1] = 30f;
                            this.netUpdate = true;
                        }
                        else if ((this.ai[0] >= 450f) && (Main.netMode != 1))
                        {
                            this.ai[0] = 1f;
                            int num35 = ((int) Main.player[this.target].position.X) / 0x10;
                            int num36 = ((int) Main.player[this.target].position.Y) / 0x10;
                            int num37 = ((int) this.position.X) / 0x10;
                            int num38 = ((int) this.position.Y) / 0x10;
                            int num39 = 20;
                            int num40 = 0;
                            bool flag12 = false;
                            if ((Math.Abs((float) (this.position.X - Main.player[this.target].position.X)) + Math.Abs((float) (this.position.Y - Main.player[this.target].position.Y))) > 2000f)
                            {
                                num40 = 100;
                                flag12 = true;
                            }
                            while (!flag12 && (num40 < 100))
                            {
                                num40++;
                                int num41 = Main.rand.Next(num35 - num39, num35 + num39);
                                for (int i = Main.rand.Next(num36 - num39, num36 + num39); i < (num36 + num39); i++)
                                {
                                    if (((((i < (num36 - 4)) || (i > (num36 + 4))) || ((num41 < (num35 - 4)) || (num41 > (num35 + 4)))) && (((i < (num38 - 1)) || (i > (num38 + 1))) || ((num41 < (num37 - 1)) || (num41 > (num37 + 1))))) && Main.tile[num41, i].active)
                                    {
                                        bool flag13 = true;
                                        if ((this.type == 0x20) && (Main.tile[num41, i - 1].wall == 0))
                                        {
                                            flag13 = false;
                                        }
                                        else if (Main.tile[num41, i - 1].lava)
                                        {
                                            flag13 = false;
                                        }
                                        if ((flag13 && Main.tileSolid[Main.tile[num41, i].type]) && !Collision.SolidTiles(num41 - 1, num41 + 1, i - 4, i - 1))
                                        {
                                            this.ai[1] = 20f;
                                            this.ai[2] = num41;
                                            this.ai[3] = i;
                                            flag12 = true;
                                            break;
                                        }
                                    }
                                }
                            }
                            this.netUpdate = true;
                        }
                        if (this.ai[1] > 0f)
                        {
                            this.ai[1]--;
                            if (this.ai[1] == 25f)
                            {
                                Main.PlaySound(2, (int) this.position.X, (int) this.position.Y, 8);
                                if (Main.netMode != 1)
                                {
                                    if ((this.type == 0x1d) || (this.type == 0x2d))
                                    {
                                        NewNPC(((int) this.position.X) + (this.width / 2), ((int) this.position.Y) - 8, 30, 0);
                                    }
                                    else if (this.type == 0x20)
                                    {
                                        NewNPC(((int) this.position.X) + (this.width / 2), ((int) this.position.Y) - 8, 0x21, 0);
                                    }
                                    else
                                    {
                                        NewNPC((((int) this.position.X) + (this.width / 2)) + (this.direction * 8), ((int) this.position.Y) + 20, 0x19, 0);
                                    }
                                }
                            }
                        }
                        if ((this.type == 0x1d) || (this.type == 0x2d))
                        {
                            if (Main.rand.Next(5) == 0)
                            {
                                num43 = Dust.NewDust(new Vector2(this.position.X, this.position.Y + 2f), this.width, this.height, 0x1b, this.velocity.X * 0.2f, this.velocity.Y * 0.2f, 100, new Color(), 1.5f);
                                Main.dust[num43].noGravity = true;
                                Main.dust[num43].velocity.X *= 0.5f;
                                Main.dust[num43].velocity.Y = -2f;
                            }
                        }
                        else if (this.type == 0x20)
                        {
                            if (Main.rand.Next(2) == 0)
                            {
                                num43 = Dust.NewDust(new Vector2(this.position.X, this.position.Y + 2f), this.width, this.height, 0x1d, this.velocity.X * 0.2f, this.velocity.Y * 0.2f, 100, new Color(), 2f);
                                Main.dust[num43].noGravity = true;
                                Main.dust[num43].velocity.X *= 1f;
                                Main.dust[num43].velocity.Y *= 1f;
                            }
                        }
                        else if (Main.rand.Next(2) == 0)
                        {
                            num43 = Dust.NewDust(new Vector2(this.position.X, this.position.Y + 2f), this.width, this.height, 6, this.velocity.X * 0.2f, this.velocity.Y * 0.2f, 100, new Color(), 2f);
                            Main.dust[num43].noGravity = true;
                            Main.dust[num43].velocity.X *= 1f;
                            Main.dust[num43].velocity.Y *= 1f;
                        }
                    }
                    else if (this.aiStyle == 9)
                    {
                        if (this.target == 0xff)
                        {
                            this.TargetClosest(true);
                            num9 = 6f;
                            if (this.type == 30)
                            {
                                maxSpawns = 8;
                            }
                            vector = new Vector2(this.position.X + (this.width * 0.5f), this.position.Y + (this.height * 0.5f));
                            num11 = (Main.player[this.target].position.X + (Main.player[this.target].width / 2)) - vector.X;
                            num12 = (Main.player[this.target].position.Y + (Main.player[this.target].height / 2)) - vector.Y;
                            num13 = (float) Math.Sqrt((double) ((num11 * num11) + (num12 * num12)));
                            num13 = num9 / num13;
                            this.velocity.X = num11 * num13;
                            this.velocity.Y = num12 * num13;
                        }
                        if (this.timeLeft > 100)
                        {
                            this.timeLeft = 100;
                        }
                        for (whoAmI = 0; whoAmI < 2; whoAmI++)
                        {
                            if (this.type == 30)
                            {
                                color = new Color();
                                num = Dust.NewDust(new Vector2(this.position.X, this.position.Y + 2f), this.width, this.height, 0x1b, this.velocity.X * 0.2f, this.velocity.Y * 0.2f, 100, color, 2f);
                                Main.dust[num].noGravity = true;
                                Dust dust7 = Main.dust[num];
                                dust7.velocity = (Vector2) (dust7.velocity * 0.3f);
                                Main.dust[num].velocity.X -= this.velocity.X * 0.2f;
                                Main.dust[num].velocity.Y -= this.velocity.Y * 0.2f;
                            }
                            else if (this.type == 0x21)
                            {
                                color = new Color();
                                num = Dust.NewDust(new Vector2(this.position.X, this.position.Y + 2f), this.width, this.height, 0x1d, this.velocity.X * 0.2f, this.velocity.Y * 0.2f, 100, color, 2f);
                                Main.dust[num].noGravity = true;
                                Main.dust[num].velocity.X *= 0.3f;
                                Main.dust[num].velocity.Y *= 0.3f;
                            }
                            else
                            {
                                color = new Color();
                                num = Dust.NewDust(new Vector2(this.position.X, this.position.Y + 2f), this.width, this.height, 6, this.velocity.X * 0.2f, this.velocity.Y * 0.2f, 100, color, 2f);
                                Main.dust[num].noGravity = true;
                                Main.dust[num].velocity.X *= 0.3f;
                                Main.dust[num].velocity.Y *= 0.3f;
                            }
                        }
                        this.rotation += 0.4f * this.direction;
                    }
                    else if (this.aiStyle == 10)
                    {
                        if (this.collideX)
                        {
                            this.velocity.X = this.oldVelocity.X * -0.5f;
                            if (((this.direction == -1) && (this.velocity.X > 0f)) && (this.velocity.X < 2f))
                            {
                                this.velocity.X = 2f;
                            }
                            if (((this.direction == 1) && (this.velocity.X < 0f)) && (this.velocity.X > -2f))
                            {
                                this.velocity.X = -2f;
                            }
                        }
                        if (this.collideY)
                        {
                            this.velocity.Y = this.oldVelocity.Y * -0.5f;
                            if ((this.velocity.Y > 0f) && (this.velocity.Y < 1f))
                            {
                                this.velocity.Y = 1f;
                            }
                            if ((this.velocity.Y < 0f) && (this.velocity.Y > -1f))
                            {
                                this.velocity.Y = -1f;
                            }
                        }
                        this.TargetClosest(true);
                        if ((this.direction == -1) && (this.velocity.X > -4f))
                        {
                            this.velocity.X -= 0.1f;
                            if (this.velocity.X > 4f)
                            {
                                this.velocity.X -= 0.1f;
                            }
                            else if (this.velocity.X > 0f)
                            {
                                this.velocity.X += 0.05f;
                            }
                            if (this.velocity.X < -4f)
                            {
                                this.velocity.X = -4f;
                            }
                        }
                        else if ((this.direction == 1) && (this.velocity.X < 4f))
                        {
                            this.velocity.X += 0.1f;
                            if (this.velocity.X < -4f)
                            {
                                this.velocity.X += 0.1f;
                            }
                            else if (this.velocity.X < 0f)
                            {
                                this.velocity.X -= 0.05f;
                            }
                            if (this.velocity.X > 4f)
                            {
                                this.velocity.X = 4f;
                            }
                        }
                        if ((this.directionY == -1) && (this.velocity.Y > -1.5))
                        {
                            this.velocity.Y -= 0.04f;
                            if (this.velocity.Y > 1.5)
                            {
                                this.velocity.Y -= 0.05f;
                            }
                            else if (this.velocity.Y > 0f)
                            {
                                this.velocity.Y += 0.03f;
                            }
                            if (this.velocity.Y < -1.5)
                            {
                                this.velocity.Y = -1.5f;
                            }
                        }
                        else if ((this.directionY == 1) && (this.velocity.Y < 1.5))
                        {
                            this.velocity.Y += 0.04f;
                            if (this.velocity.Y < -1.5)
                            {
                                this.velocity.Y += 0.05f;
                            }
                            else if (this.velocity.Y < 0f)
                            {
                                this.velocity.Y -= 0.03f;
                            }
                            if (this.velocity.Y > 1.5)
                            {
                                this.velocity.Y = 1.5f;
                            }
                        }
                        this.rotation = ((float) Math.Atan2((double) this.velocity.Y, (double) this.velocity.X)) - 1.57f;
                        num = Dust.NewDust(new Vector2(this.position.X - this.velocity.X, this.position.Y - this.velocity.Y), this.width, this.height, 6, this.velocity.X * 0.2f, this.velocity.Y * 0.2f, 100, new Color(), 2f);
                        Main.dust[num].noGravity = true;
                        Main.dust[num].noLight = true;
                        Main.dust[num].velocity.X *= 0.3f;
                        Main.dust[num].velocity.Y *= 0.3f;
                    }
                    else if (this.aiStyle == 11)
                    {
                        if ((this.ai[0] == 0f) && (Main.netMode != 1))
                        {
                            this.TargetClosest(true);
                            this.ai[0] = 1f;
                            num19 = NewNPC(((int) this.position.X) + (this.width / 2), ((int) this.position.Y) + (this.height / 2), 0x24, this.whoAmI);
                            Main.npc[num19].ai[0] = -1f;
                            Main.npc[num19].ai[1] = this.whoAmI;
                            Main.npc[num19].target = this.target;
                            Main.npc[num19].netUpdate = true;
                            num19 = NewNPC(((int) this.position.X) + (this.width / 2), ((int) this.position.Y) + (this.height / 2), 0x24, this.whoAmI);
                            Main.npc[num19].ai[0] = 1f;
                            Main.npc[num19].ai[1] = this.whoAmI;
                            Main.npc[num19].ai[3] = 150f;
                            Main.npc[num19].target = this.target;
                            Main.npc[num19].netUpdate = true;
                        }
                        if ((Main.player[this.target].dead || (Math.Abs((float) (this.position.X - Main.player[this.target].position.X)) > 2000f)) || (Math.Abs((float) (this.position.Y - Main.player[this.target].position.Y)) > 2000f))
                        {
                            this.TargetClosest(true);
                            if ((Main.player[this.target].dead || (Math.Abs((float) (this.position.X - Main.player[this.target].position.X)) > 2000f)) || (Math.Abs((float) (this.position.Y - Main.player[this.target].position.Y)) > 2000f))
                            {
                                this.ai[1] = 3f;
                            }
                        }
                        if ((Main.dayTime && (this.ai[1] != 3f)) && (this.ai[1] != 2f))
                        {
                            this.ai[1] = 2f;
                            Main.PlaySound(15, (int) this.position.X, (int) this.position.Y, 0);
                        }
                        if (this.ai[1] == 0f)
                        {
                            this.ai[2]++;
                            if (this.ai[2] >= 800f)
                            {
                                this.ai[2] = 0f;
                                this.ai[1] = 1f;
                                this.TargetClosest(true);
                                this.netUpdate = true;
                            }
                            this.rotation = this.velocity.X / 15f;
                            if (this.position.Y > (Main.player[this.target].position.Y - 250f))
                            {
                                if (this.velocity.Y > 0f)
                                {
                                    this.velocity.Y *= 0.98f;
                                }
                                this.velocity.Y -= 0.02f;
                                if (this.velocity.Y > 2f)
                                {
                                    this.velocity.Y = 2f;
                                }
                            }
                            else if (this.position.Y < (Main.player[this.target].position.Y - 250f))
                            {
                                if (this.velocity.Y < 0f)
                                {
                                    this.velocity.Y *= 0.98f;
                                }
                                this.velocity.Y += 0.02f;
                                if (this.velocity.Y < -2f)
                                {
                                    this.velocity.Y = -2f;
                                }
                            }
                            if ((this.position.X + (this.width / 2)) > (Main.player[this.target].position.X + (Main.player[this.target].width / 2)))
                            {
                                if (this.velocity.X > 0f)
                                {
                                    this.velocity.X *= 0.98f;
                                }
                                this.velocity.X -= 0.05f;
                                if (this.velocity.X > 8f)
                                {
                                    this.velocity.X = 8f;
                                }
                            }
                            if ((this.position.X + (this.width / 2)) < (Main.player[this.target].position.X + (Main.player[this.target].width / 2)))
                            {
                                if (this.velocity.X < 0f)
                                {
                                    this.velocity.X *= 0.98f;
                                }
                                this.velocity.X += 0.05f;
                                if (this.velocity.X < -8f)
                                {
                                    this.velocity.X = -8f;
                                }
                            }
                        }
                        else if (this.ai[1] == 1f)
                        {
                            this.ai[2]++;
                            if (this.ai[2] == 2f)
                            {
                                Main.PlaySound(15, (int) this.position.X, (int) this.position.Y, 0);
                            }
                            if (this.ai[2] >= 400f)
                            {
                                this.ai[2] = 0f;
                                this.ai[1] = 0f;
                            }
                            this.rotation += this.direction * 0.3f;
                            vector = new Vector2(this.position.X + (this.width * 0.5f), this.position.Y + (this.height * 0.5f));
                            num11 = (Main.player[this.target].position.X + (Main.player[this.target].width / 2)) - vector.X;
                            num12 = (Main.player[this.target].position.Y + (Main.player[this.target].height / 2)) - vector.Y;
                            num13 = (float) Math.Sqrt((double) ((num11 * num11) + (num12 * num12)));
                            num13 = 2.5f / num13;
                            this.velocity.X = num11 * num13;
                            this.velocity.Y = num12 * num13;
                        }
                        else if (this.ai[1] == 2f)
                        {
                            this.damage = 0x270f;
                            this.defense = 0x270f;
                            this.rotation += this.direction * 0.3f;
                            vector = new Vector2(this.position.X + (this.width * 0.5f), this.position.Y + (this.height * 0.5f));
                            num11 = (Main.player[this.target].position.X + (Main.player[this.target].width / 2)) - vector.X;
                            num12 = (Main.player[this.target].position.Y + (Main.player[this.target].height / 2)) - vector.Y;
                            num13 = (float) Math.Sqrt((double) ((num11 * num11) + (num12 * num12)));
                            num13 = 8f / num13;
                            this.velocity.X = num11 * num13;
                            this.velocity.Y = num12 * num13;
                        }
                        else if (this.ai[1] == 3f)
                        {
                            this.velocity.Y -= 0.1f;
                            if (this.velocity.Y > 0f)
                            {
                                this.velocity.Y *= 0.95f;
                            }
                            this.velocity.X *= 0.95f;
                            if (this.timeLeft > 50)
                            {
                                this.timeLeft = 50;
                            }
                        }
                        if ((this.ai[1] != 2f) && (this.ai[1] != 3f))
                        {
                            color = new Color();
                            num = Dust.NewDust(new Vector2(((this.position.X + (this.width / 2)) - 15f) - (this.velocity.X * 5f), (this.position.Y + this.height) - 2f), 30, 10, 5, -this.velocity.X * 0.2f, 3f, 0, color, 2f);
                            Main.dust[num].noGravity = true;
                            Main.dust[num].velocity.X *= 1.3f;
                            Main.dust[num].velocity.X += this.velocity.X * 0.4f;
                            Main.dust[num].velocity.Y += 2f + this.velocity.Y;
                            for (whoAmI = 0; whoAmI < 2; whoAmI++)
                            {
                                color = new Color();
                                num = Dust.NewDust(new Vector2(this.position.X, this.position.Y + 120f), this.width, 60, 5, this.velocity.X, this.velocity.Y, 0, color, 2f);
                                Main.dust[num].noGravity = true;
                                Dust dust8 = Main.dust[num];
                                dust8.velocity -= this.velocity;
                                Main.dust[num].velocity.Y += 5f;
                            }
                        }
                    }
                    else if (this.aiStyle == 12)
                    {
                        this.spriteDirection = -((int) this.ai[0]);
                        if (!Main.npc[(int) this.ai[1]].active || (Main.npc[(int) this.ai[1]].aiStyle != 11))
                        {
                            this.ai[2] += 10f;
                            if ((this.ai[2] > 50f) || (Main.netMode != 2))
                            {
                                this.life = -1;
                                this.HitEffect(0, 10.0);
                                this.active = false;
                            }
                        }
                        if ((this.ai[2] == 0f) || (this.ai[2] == 3f))
                        {
                            if ((Main.npc[(int) this.ai[1]].ai[1] == 3f) && (this.timeLeft > 10))
                            {
                                this.timeLeft = 10;
                            }
                            if (Main.npc[(int) this.ai[1]].ai[1] != 0f)
                            {
                                if (this.position.Y > (Main.npc[(int) this.ai[1]].position.Y - 100f))
                                {
                                    if (this.velocity.Y > 0f)
                                    {
                                        this.velocity.Y *= 0.96f;
                                    }
                                    this.velocity.Y -= 0.07f;
                                    if (this.velocity.Y > 6f)
                                    {
                                        this.velocity.Y = 6f;
                                    }
                                }
                                else if (this.position.Y < (Main.npc[(int) this.ai[1]].position.Y - 100f))
                                {
                                    if (this.velocity.Y < 0f)
                                    {
                                        this.velocity.Y *= 0.96f;
                                    }
                                    this.velocity.Y += 0.07f;
                                    if (this.velocity.Y < -6f)
                                    {
                                        this.velocity.Y = -6f;
                                    }
                                }
                                if ((this.position.X + (this.width / 2)) > ((Main.npc[(int) this.ai[1]].position.X + (Main.npc[(int) this.ai[1]].width / 2)) - (120f * this.ai[0])))
                                {
                                    if (this.velocity.X > 0f)
                                    {
                                        this.velocity.X *= 0.96f;
                                    }
                                    this.velocity.X -= 0.1f;
                                    if (this.velocity.X > 8f)
                                    {
                                        this.velocity.X = 8f;
                                    }
                                }
                                if ((this.position.X + (this.width / 2)) < ((Main.npc[(int) this.ai[1]].position.X + (Main.npc[(int) this.ai[1]].width / 2)) - (120f * this.ai[0])))
                                {
                                    if (this.velocity.X < 0f)
                                    {
                                        this.velocity.X *= 0.96f;
                                    }
                                    this.velocity.X += 0.1f;
                                    if (this.velocity.X < -8f)
                                    {
                                        this.velocity.X = -8f;
                                    }
                                }
                            }
                            else
                            {
                                this.ai[3]++;
                                if (this.ai[3] >= 300f)
                                {
                                    this.ai[2]++;
                                    this.ai[3] = 0f;
                                    this.netUpdate = true;
                                }
                                if (this.position.Y > (Main.npc[(int) this.ai[1]].position.Y + 230f))
                                {
                                    if (this.velocity.Y > 0f)
                                    {
                                        this.velocity.Y *= 0.96f;
                                    }
                                    this.velocity.Y -= 0.04f;
                                    if (this.velocity.Y > 3f)
                                    {
                                        this.velocity.Y = 3f;
                                    }
                                }
                                else if (this.position.Y < (Main.npc[(int) this.ai[1]].position.Y + 230f))
                                {
                                    if (this.velocity.Y < 0f)
                                    {
                                        this.velocity.Y *= 0.96f;
                                    }
                                    this.velocity.Y += 0.04f;
                                    if (this.velocity.Y < -3f)
                                    {
                                        this.velocity.Y = -3f;
                                    }
                                }
                                if ((this.position.X + (this.width / 2)) > ((Main.npc[(int) this.ai[1]].position.X + (Main.npc[(int) this.ai[1]].width / 2)) - (200f * this.ai[0])))
                                {
                                    if (this.velocity.X > 0f)
                                    {
                                        this.velocity.X *= 0.96f;
                                    }
                                    this.velocity.X -= 0.07f;
                                    if (this.velocity.X > 8f)
                                    {
                                        this.velocity.X = 8f;
                                    }
                                }
                                if ((this.position.X + (this.width / 2)) < ((Main.npc[(int) this.ai[1]].position.X + (Main.npc[(int) this.ai[1]].width / 2)) - (200f * this.ai[0])))
                                {
                                    if (this.velocity.X < 0f)
                                    {
                                        this.velocity.X *= 0.96f;
                                    }
                                    this.velocity.X += 0.07f;
                                    if (this.velocity.X < -8f)
                                    {
                                        this.velocity.X = -8f;
                                    }
                                }
                            }
                            vector = new Vector2(this.position.X + (this.width * 0.5f), this.position.Y + (this.height * 0.5f));
                            num11 = ((Main.npc[(int) this.ai[1]].position.X + (Main.npc[(int) this.ai[1]].width / 2)) - (200f * this.ai[0])) - vector.X;
                            num12 = (Main.npc[(int) this.ai[1]].position.Y + 230f) - vector.Y;
                            num13 = (float) Math.Sqrt((double) ((num11 * num11) + (num12 * num12)));
                            this.rotation = ((float) Math.Atan2((double) num12, (double) num11)) + 1.57f;
                        }
                        else if (this.ai[2] == 1f)
                        {
                            vector = new Vector2(this.position.X + (this.width * 0.5f), this.position.Y + (this.height * 0.5f));
                            num11 = ((Main.npc[(int) this.ai[1]].position.X + (Main.npc[(int) this.ai[1]].width / 2)) - (200f * this.ai[0])) - vector.X;
                            num12 = (Main.npc[(int) this.ai[1]].position.Y + 230f) - vector.Y;
                            num13 = (float) Math.Sqrt((double) ((num11 * num11) + (num12 * num12)));
                            this.rotation = ((float) Math.Atan2((double) num12, (double) num11)) + 1.57f;
                            this.velocity.X *= 0.95f;
                            this.velocity.Y -= 0.1f;
                            if (this.velocity.Y < -8f)
                            {
                                this.velocity.Y = -8f;
                            }
                            if (this.position.Y < (Main.npc[(int) this.ai[1]].position.Y - 200f))
                            {
                                this.TargetClosest(true);
                                this.ai[2] = 2f;
                                vector = new Vector2(this.position.X + (this.width * 0.5f), this.position.Y + (this.height * 0.5f));
                                num11 = (Main.player[this.target].position.X + (Main.player[this.target].width / 2)) - vector.X;
                                num12 = (Main.player[this.target].position.Y + (Main.player[this.target].height / 2)) - vector.Y;
                                num13 = (float) Math.Sqrt((double) ((num11 * num11) + (num12 * num12)));
                                num13 = 20f / num13;
                                this.velocity.X = num11 * num13;
                                this.velocity.Y = num12 * num13;
                                this.netUpdate = true;
                            }
                        }
                        else if (this.ai[2] == 2f)
                        {
                            if ((this.position.Y > Main.player[this.target].position.Y) || (this.velocity.Y < 0f))
                            {
                                this.ai[2] = 3f;
                            }
                        }
                        else if (this.ai[2] == 4f)
                        {
                            vector = new Vector2(this.position.X + (this.width * 0.5f), this.position.Y + (this.height * 0.5f));
                            num11 = ((Main.npc[(int) this.ai[1]].position.X + (Main.npc[(int) this.ai[1]].width / 2)) - (200f * this.ai[0])) - vector.X;
                            num12 = (Main.npc[(int) this.ai[1]].position.Y + 230f) - vector.Y;
                            num13 = (float) Math.Sqrt((double) ((num11 * num11) + (num12 * num12)));
                            this.rotation = ((float) Math.Atan2((double) num12, (double) num11)) + 1.57f;
                            this.velocity.Y *= 0.95f;
                            this.velocity.X += 0.1f * -this.ai[0];
                            if (this.velocity.X < -8f)
                            {
                                this.velocity.X = -8f;
                            }
                            if (this.velocity.X > 8f)
                            {
                                this.velocity.X = 8f;
                            }
                            if (((this.position.X + (this.width / 2)) < ((Main.npc[(int) this.ai[1]].position.X + (Main.npc[(int) this.ai[1]].width / 2)) - 500f)) || ((this.position.X + (this.width / 2)) > ((Main.npc[(int) this.ai[1]].position.X + (Main.npc[(int) this.ai[1]].width / 2)) + 500f)))
                            {
                                this.TargetClosest(true);
                                this.ai[2] = 5f;
                                vector = new Vector2(this.position.X + (this.width * 0.5f), this.position.Y + (this.height * 0.5f));
                                num11 = (Main.player[this.target].position.X + (Main.player[this.target].width / 2)) - vector.X;
                                num12 = (Main.player[this.target].position.Y + (Main.player[this.target].height / 2)) - vector.Y;
                                num13 = (float) Math.Sqrt((double) ((num11 * num11) + (num12 * num12)));
                                num13 = 20f / num13;
                                this.velocity.X = num11 * num13;
                                this.velocity.Y = num12 * num13;
                                this.netUpdate = true;
                            }
                        }
                        else if ((this.ai[2] == 5f) && (((this.velocity.X > 0f) && ((this.position.X + (this.width / 2)) > (Main.player[this.target].position.X + (Main.player[this.target].width / 2)))) || ((this.velocity.X < 0f) && ((this.position.X + (this.width / 2)) < (Main.player[this.target].position.X + (Main.player[this.target].width / 2))))))
                        {
                            this.ai[2] = 0f;
                        }
                    }
                    else if (this.aiStyle == 13)
                    {
                        if (Main.tile[(int) this.ai[0], (int) this.ai[1]] == null)
                        {
                            Main.tile[(int) this.ai[0], (int) this.ai[1]] = new Tile();
                        }
                        if (!Main.tile[(int) this.ai[0], (int) this.ai[1]].active)
                        {
                            this.life = -1;
                            this.HitEffect(0, 10.0);
                            this.active = false;
                        }
                        else
                        {
                            this.TargetClosest(true);
                            num10 = 0.05f;
                            vector = new Vector2((this.ai[0] * 16f) + 8f, (this.ai[1] * 16f) + 8f);
                            num11 = ((Main.player[this.target].position.X + (Main.player[this.target].width / 2)) - (this.width / 2)) - vector.X;
                            num12 = ((Main.player[this.target].position.Y + (Main.player[this.target].height / 2)) - (this.height / 2)) - vector.Y;
                            num13 = (float) Math.Sqrt((double) ((num11 * num11) + (num12 * num12)));
                            if (num13 > 150f)
                            {
                                num13 = 150f / num13;
                                num11 *= num13;
                                num12 *= num13;
                            }
                            if (this.position.X < (((this.ai[0] * 16f) + 8f) + num11))
                            {
                                this.velocity.X += num10;
                                if ((this.velocity.X < 0f) && (num11 > 0f))
                                {
                                    this.velocity.X += num10 * 2f;
                                }
                            }
                            else if (this.position.X > (((this.ai[0] * 16f) + 8f) + num11))
                            {
                                this.velocity.X -= num10;
                                if ((this.velocity.X > 0f) && (num11 < 0f))
                                {
                                    this.velocity.X -= num10 * 2f;
                                }
                            }
                            if (this.position.Y < (((this.ai[1] * 16f) + 8f) + num12))
                            {
                                this.velocity.Y += num10;
                                if ((this.velocity.Y < 0f) && (num12 > 0f))
                                {
                                    this.velocity.Y += num10 * 2f;
                                }
                            }
                            else if (this.position.Y > (((this.ai[1] * 16f) + 8f) + num12))
                            {
                                this.velocity.Y -= num10;
                                if ((this.velocity.Y > 0f) && (num12 < 0f))
                                {
                                    this.velocity.Y -= num10 * 2f;
                                }
                            }
                            if (this.velocity.X > 2f)
                            {
                                this.velocity.X = 2f;
                            }
                            if (this.velocity.X < -2f)
                            {
                                this.velocity.X = -2f;
                            }
                            if (this.velocity.Y > 2f)
                            {
                                this.velocity.Y = 2f;
                            }
                            if (this.velocity.Y < -2f)
                            {
                                this.velocity.Y = -2f;
                            }
                            if (num11 > 0f)
                            {
                                this.spriteDirection = 1;
                                this.rotation = (float) Math.Atan2((double) num12, (double) num11);
                            }
                            if (num11 < 0f)
                            {
                                this.spriteDirection = -1;
                                this.rotation = ((float) Math.Atan2((double) num12, (double) num11)) + 3.14f;
                            }
                            if (this.collideX)
                            {
                                this.netUpdate = true;
                                this.velocity.X = this.oldVelocity.X * -0.7f;
                                if ((this.velocity.X > 0f) && (this.velocity.X < 2f))
                                {
                                    this.velocity.X = 2f;
                                }
                                if ((this.velocity.X < 0f) && (this.velocity.X > -2f))
                                {
                                    this.velocity.X = -2f;
                                }
                            }
                            if (this.collideY)
                            {
                                this.netUpdate = true;
                                this.velocity.Y = this.oldVelocity.Y * -0.7f;
                                if ((this.velocity.Y > 0f) && (this.velocity.Y < 2f))
                                {
                                    this.velocity.Y = 2f;
                                }
                                if ((this.velocity.Y < 0f) && (this.velocity.Y > -2f))
                                {
                                    this.velocity.Y = -2f;
                                }
                            }
                        }
                    }
                    else if (this.aiStyle == 14)
                    {
                        this.noGravity = true;
                        if (this.collideX)
                        {
                            this.velocity.X = this.oldVelocity.X * -0.5f;
                            if (((this.direction == -1) && (this.velocity.X > 0f)) && (this.velocity.X < 2f))
                            {
                                this.velocity.X = 2f;
                            }
                            if (((this.direction == 1) && (this.velocity.X < 0f)) && (this.velocity.X > -2f))
                            {
                                this.velocity.X = -2f;
                            }
                        }
                        if (this.collideY)
                        {
                            this.velocity.Y = this.oldVelocity.Y * -0.5f;
                            if ((this.velocity.Y > 0f) && (this.velocity.Y < 1f))
                            {
                                this.velocity.Y = 1f;
                            }
                            if ((this.velocity.Y < 0f) && (this.velocity.Y > -1f))
                            {
                                this.velocity.Y = -1f;
                            }
                        }
                        this.TargetClosest(true);
                        if ((this.direction == -1) && (this.velocity.X > -4f))
                        {
                            this.velocity.X -= 0.1f;
                            if (this.velocity.X > 4f)
                            {
                                this.velocity.X -= 0.1f;
                            }
                            else if (this.velocity.X > 0f)
                            {
                                this.velocity.X += 0.05f;
                            }
                            if (this.velocity.X < -4f)
                            {
                                this.velocity.X = -4f;
                            }
                        }
                        else if ((this.direction == 1) && (this.velocity.X < 4f))
                        {
                            this.velocity.X += 0.1f;
                            if (this.velocity.X < -4f)
                            {
                                this.velocity.X += 0.1f;
                            }
                            else if (this.velocity.X < 0f)
                            {
                                this.velocity.X -= 0.05f;
                            }
                            if (this.velocity.X > 4f)
                            {
                                this.velocity.X = 4f;
                            }
                        }
                        if ((this.directionY == -1) && (this.velocity.Y > -1.5))
                        {
                            this.velocity.Y -= 0.04f;
                            if (this.velocity.Y > 1.5)
                            {
                                this.velocity.Y -= 0.05f;
                            }
                            else if (this.velocity.Y > 0f)
                            {
                                this.velocity.Y += 0.03f;
                            }
                            if (this.velocity.Y < -1.5)
                            {
                                this.velocity.Y = -1.5f;
                            }
                        }
                        else if ((this.directionY == 1) && (this.velocity.Y < 1.5))
                        {
                            this.velocity.Y += 0.04f;
                            if (this.velocity.Y < -1.5)
                            {
                                this.velocity.Y += 0.05f;
                            }
                            else if (this.velocity.Y < 0f)
                            {
                                this.velocity.Y -= 0.03f;
                            }
                            if (this.velocity.Y > 1.5)
                            {
                                this.velocity.Y = 1.5f;
                            }
                        }
                        if (this.type == 0x31)
                        {
                            if ((this.direction == -1) && (this.velocity.X > -4f))
                            {
                                this.velocity.X -= 0.1f;
                                if (this.velocity.X > 4f)
                                {
                                    this.velocity.X -= 0.1f;
                                }
                                else if (this.velocity.X > 0f)
                                {
                                    this.velocity.X += 0.05f;
                                }
                                if (this.velocity.X < -4f)
                                {
                                    this.velocity.X = -4f;
                                }
                            }
                            else if ((this.direction == 1) && (this.velocity.X < 4f))
                            {
                                this.velocity.X += 0.1f;
                                if (this.velocity.X < -4f)
                                {
                                    this.velocity.X += 0.1f;
                                }
                                else if (this.velocity.X < 0f)
                                {
                                    this.velocity.X -= 0.05f;
                                }
                                if (this.velocity.X > 4f)
                                {
                                    this.velocity.X = 4f;
                                }
                            }
                            if ((this.directionY == -1) && (this.velocity.Y > -1.5))
                            {
                                this.velocity.Y -= 0.04f;
                                if (this.velocity.Y > 1.5)
                                {
                                    this.velocity.Y -= 0.05f;
                                }
                                else if (this.velocity.Y > 0f)
                                {
                                    this.velocity.Y += 0.03f;
                                }
                                if (this.velocity.Y < -1.5)
                                {
                                    this.velocity.Y = -1.5f;
                                }
                            }
                            else if ((this.directionY == 1) && (this.velocity.Y < 1.5))
                            {
                                this.velocity.Y += 0.04f;
                                if (this.velocity.Y < -1.5)
                                {
                                    this.velocity.Y += 0.05f;
                                }
                                else if (this.velocity.Y < 0f)
                                {
                                    this.velocity.Y -= 0.03f;
                                }
                                if (this.velocity.Y > 1.5)
                                {
                                    this.velocity.Y = 1.5f;
                                }
                            }
                        }
                        if ((Main.netMode != 1) && (this.type == 0x30))
                        {
                            this.ai[0]++;
                            if (((this.ai[0] == 30f) || (this.ai[0] == 60f)) || (this.ai[0] == 90f))
                            {
                                float num44 = 6f;
                                vector = new Vector2(this.position.X + (this.width * 0.5f), this.position.Y + (this.height * 0.5f));
                                num11 = ((Main.player[this.target].position.X + (Main.player[this.target].width * 0.5f)) - vector.X) + Main.rand.Next(-100, 0x65);
                                num12 = ((Main.player[this.target].position.Y + (Main.player[this.target].height * 0.5f)) - vector.Y) + Main.rand.Next(-100, 0x65);
                                num13 = (float) Math.Sqrt((double) ((num11 * num11) + (num12 * num12)));
                                num13 = num44 / num13;
                                num11 *= num13;
                                num12 *= num13;
                                int damage = 15;
                                int index = Projectile.NewProjectile(vector.X, vector.Y, num11, num12, 0x26, damage, 0f, Main.myPlayer);
                                Main.projectile[index].timeLeft = 300;
                            }
                            else if (this.ai[0] >= (400 + Main.rand.Next(400)))
                            {
                                this.ai[0] = 0f;
                            }
                        }
                    }
                    else if (this.aiStyle == 15)
                    {
                        this.aiAction = 0;
                        if ((this.ai[3] == 0f) && (this.life > 0))
                        {
                            this.ai[3] = this.lifeMax;
                        }
                        if (this.ai[2] == 0f)
                        {
                            this.ai[0] = -100f;
                            this.ai[2] = 1f;
                            this.TargetClosest(true);
                        }
                        if (this.velocity.Y == 0f)
                        {
                            this.velocity.X *= 0.8f;
                            if ((this.velocity.X > -0.1) && (this.velocity.X < 0.1))
                            {
                                this.velocity.X = 0f;
                            }
                            this.ai[0] += 2f;
                            if (this.life < (this.lifeMax * 0.8))
                            {
                                this.ai[0]++;
                            }
                            if (this.life < (this.lifeMax * 0.6))
                            {
                                this.ai[0]++;
                            }
                            if (this.life < (this.lifeMax * 0.4))
                            {
                                this.ai[0] += 2f;
                            }
                            if (this.life < (this.lifeMax * 0.2))
                            {
                                this.ai[0] += 3f;
                            }
                            if (this.life < (this.lifeMax * 0.1))
                            {
                                this.ai[0] += 4f;
                            }
                            if (this.ai[0] >= 0f)
                            {
                                this.netUpdate = true;
                                this.TargetClosest(true);
                                if (this.ai[1] == 3f)
                                {
                                    this.velocity.Y = -13f;
                                    this.velocity.X += 3.5f * this.direction;
                                    this.ai[0] = -200f;
                                    this.ai[1] = 0f;
                                }
                                else if (this.ai[1] == 2f)
                                {
                                    this.velocity.Y = -6f;
                                    this.velocity.X += 4.5f * this.direction;
                                    this.ai[0] = -120f;
                                    this.ai[1]++;
                                }
                                else
                                {
                                    this.velocity.Y = -8f;
                                    this.velocity.X += 4f * this.direction;
                                    this.ai[0] = -120f;
                                    this.ai[1]++;
                                }
                            }
                            else if (this.ai[0] >= -30f)
                            {
                                this.aiAction = 1;
                            }
                        }
                        else if ((this.target < 0xff) && (((this.direction == 1) && (this.velocity.X < 3f)) || ((this.direction == -1) && (this.velocity.X > -3f))))
                        {
                            if (((this.direction == -1) && (this.velocity.X < 0.1)) || ((this.direction == 1) && (this.velocity.X > -0.1)))
                            {
                                this.velocity.X += 0.2f * this.direction;
                            }
                            else
                            {
                                this.velocity.X *= 0.93f;
                            }
                        }
                        num = Dust.NewDust(this.position, this.width, this.height, 4, this.velocity.X, this.velocity.Y, 0xff, new Color(0, 80, 0xff, 80), this.scale * 1.2f);
                        Main.dust[num].noGravity = true;
                        Dust dust9 = Main.dust[num];
                        dust9.velocity = (Vector2) (dust9.velocity * 0.5f);
                        if (this.life > 0)
                        {
                            float num47 = ((float) this.life) / ((float) this.lifeMax);
                            num47 = (num47 * 0.5f) + 0.75f;
                            if (num47 != this.scale)
                            {
                                this.position.X += this.width / 2;
                                this.position.Y += this.height;
                                this.scale = num47;
                                this.width = (int) (98f * this.scale);
                                this.height = (int) (92f * this.scale);
                                this.position.X -= this.width / 2;
                                this.position.Y -= this.height;
                            }
                            if (Main.netMode != 1)
                            {
                                int num48 = (int) (this.lifeMax * 0.05);
                                if ((this.life + num48) < this.ai[3])
                                {
                                    this.ai[3] = this.life;
                                    int num49 = Main.rand.Next(1, 4);
                                    for (whoAmI = 0; whoAmI < num49; whoAmI++)
                                    {
                                        int x = ((int) this.position.X) + Main.rand.Next(this.width - 0x20);
                                        int y = ((int) this.position.Y) + Main.rand.Next(this.height - 0x20);
                                        num19 = NewNPC(x, y, 1, 0);
                                        Main.npc[num19].SetDefaults(1);
                                        Main.npc[num19].velocity.X = Main.rand.Next(-15, 0x10) * 0.1f;
                                        Main.npc[num19].velocity.Y = Main.rand.Next(-30, 1) * 0.1f;
                                        Main.npc[num19].ai[1] = Main.rand.Next(3);
                                        if ((Main.netMode == 2) && (num19 < 0x3e8))
                                        {
                                            NetMessage.SendData(0x17, -1, -1, "", num19, 0f, 0f, 0f);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (this.aiStyle == 0x10)
                    {
                        if (this.direction == 0)
                        {
                            this.TargetClosest(true);
                        }
                        if (this.wet)
                        {
                            if (this.collideX)
                            {
                                this.velocity.X *= -1f;
                                this.direction *= -1;
                            }
                            if (this.collideY)
                            {
                                if (this.velocity.Y > 0f)
                                {
                                    this.velocity.Y = Math.Abs(this.velocity.Y) * -1f;
                                    this.directionY = -1;
                                    this.ai[0] = -1f;
                                }
                                else if (this.velocity.Y < 0f)
                                {
                                    this.velocity.Y = Math.Abs(this.velocity.Y);
                                    this.directionY = 1;
                                    this.ai[0] = 1f;
                                }
                            }
                            bool flag14 = false;
                            if (!this.friendly)
                            {
                                this.TargetClosest(false);
                                if (!(!Main.player[this.target].wet || Main.player[this.target].dead))
                                {
                                    flag14 = true;
                                }
                            }
                            if (flag14)
                            {
                                this.TargetClosest(true);
                                this.velocity.X += this.direction * 0.1f;
                                this.velocity.Y += this.directionY * 0.1f;
                                if (this.velocity.X > 3f)
                                {
                                    this.velocity.X = 3f;
                                }
                                if (this.velocity.X < -3f)
                                {
                                    this.velocity.X = -3f;
                                }
                                if (this.velocity.Y > 2f)
                                {
                                    this.velocity.Y = 2f;
                                }
                                if (this.velocity.Y < -2f)
                                {
                                    this.velocity.Y = -2f;
                                }
                            }
                            else
                            {
                                this.velocity.X += this.direction * 0.1f;
                                if ((this.velocity.X < -1f) || (this.velocity.X > 1f))
                                {
                                    this.velocity.X *= 0.95f;
                                }
                                if (this.ai[0] == -1f)
                                {
                                    this.velocity.Y -= 0.01f;
                                    if (this.velocity.Y < -0.3)
                                    {
                                        this.ai[0] = 1f;
                                    }
                                }
                                else
                                {
                                    this.velocity.Y += 0.01f;
                                    if (this.velocity.Y > 0.3)
                                    {
                                        this.ai[0] = -1f;
                                    }
                                }
                                whoAmI = (((int) this.position.X) + (this.width / 2)) / 0x10;
                                num28 = (((int) this.position.Y) + (this.height / 2)) / 0x10;
                                if (Main.tile[whoAmI, num28 - 1] == null)
                                {
                                    Main.tile[whoAmI, num28 - 1] = new Tile();
                                }
                                if (Main.tile[whoAmI, num28 + 1] == null)
                                {
                                    Main.tile[whoAmI, num28 + 1] = new Tile();
                                }
                                if (Main.tile[whoAmI, num28 + 2] == null)
                                {
                                    Main.tile[whoAmI, num28 + 2] = new Tile();
                                }
                                if (Main.tile[whoAmI, num28 - 1].liquid > 0x80)
                                {
                                    if (Main.tile[whoAmI, num28 + 1].active)
                                    {
                                        this.ai[0] = -1f;
                                    }
                                    else if (Main.tile[whoAmI, num28 + 2].active)
                                    {
                                        this.ai[0] = -1f;
                                    }
                                }
                                if ((this.velocity.Y > 0.4) || (this.velocity.Y < -0.4))
                                {
                                    this.velocity.Y *= 0.95f;
                                }
                            }
                        }
                        else
                        {
                            if ((this.velocity.Y == 0f) && (Main.netMode != 1))
                            {
                                this.velocity.Y = Main.rand.Next(-50, -20) * 0.1f;
                                this.velocity.X = Main.rand.Next(-20, 20) * 0.1f;
                                this.netUpdate = true;
                            }
                            this.velocity.Y += 0.3f;
                            if (this.velocity.Y > 10f)
                            {
                                this.velocity.Y = 10f;
                            }
                            this.ai[0] = 1f;
                        }
                        this.rotation = (this.velocity.Y * this.direction) * 0.1f;
                        if (this.rotation < -0.2)
                        {
                            this.rotation = -0.2f;
                        }
                        if (this.rotation > 0.2)
                        {
                            this.rotation = 0.2f;
                        }
                    }
                    return;
                }
                num32 = (((int) this.position.X) + (this.width / 2)) / 0x10;
                num33 = ((int) ((this.position.Y + this.height) + 1f)) / 0x10;
                if (!((Main.netMode != 1) && this.townNPC))
                {
                    this.homeTileX = num32;
                    this.homeTileY = num33;
                }
                if ((this.type == 0x2e) && (this.target == 0xff))
                {
                    this.TargetClosest(true);
                }
                flag8 = false;
                this.directionY = -1;
                if (this.direction == 0)
                {
                    this.direction = 1;
                }
                for (num28 = 0; num28 < 0xff; num28++)
                {
                    if (Main.player[num28].active && (Main.player[num28].talkNPC == this.whoAmI))
                    {
                        flag8 = true;
                        if (this.ai[0] != 0f)
                        {
                            this.netUpdate = true;
                        }
                        this.ai[0] = 0f;
                        this.ai[1] = 300f;
                        this.ai[2] = 100f;
                        if ((Main.player[num28].position.X + (Main.player[num28].width / 2)) < (this.position.X + (this.width / 2)))
                        {
                            this.direction = -1;
                        }
                        else
                        {
                            this.direction = 1;
                        }
                    }
                }
                if (this.ai[3] > 0f)
                {
                    this.life = -1;
                    this.HitEffect(0, 10.0);
                    this.active = false;
                    if (this.type == 0x25)
                    {
                        Main.PlaySound(15, (int) this.position.X, (int) this.position.Y, 0);
                    }
                }
                if ((this.type != 0x25) || (Main.netMode == 1))
                {
                    goto Label_524A;
                }
                this.homeless = false;
                this.homeTileX = Main.dungeonX;
                this.homeTileY = Main.dungeonY;
                if (downedBoss3)
                {
                    this.ai[3] = 1f;
                    this.netUpdate = true;
                }
                if ((Main.dayTime || !flag8) || (this.ai[3] != 0f))
                {
                    goto Label_524A;
                }
                flag9 = true;
                whoAmI = 0;
                while (whoAmI < 0x3e8)
                {
                    if (Main.npc[whoAmI].active && (Main.npc[whoAmI].type == 0x23))
                    {
                        flag9 = false;
                        break;
                    }
                    whoAmI++;
                }
            }
            else
            {
                if (((this.target < 0) || (this.target == 0xff)) || Main.player[this.target].dead)
                {
                    this.TargetClosest(true);
                }
                if (Main.player[this.target].dead && (this.timeLeft > 10))
                {
                    this.timeLeft = 10;
                }
                if (Main.netMode != 1)
                {
                    if ((((((this.type == 7) || (this.type == 8)) || ((this.type == 10) || (this.type == 11))) || (((this.type == 13) || (this.type == 14)) || (this.type == 0x27))) || (this.type == 40)) && (this.ai[0] == 0f))
                    {
                        if ((((this.type == 7) || (this.type == 10)) || (this.type == 13)) || (this.type == 0x27))
                        {
                            this.ai[2] = 10f;
                            if (this.type == 10)
                            {
                                this.ai[2] = 5f;
                            }
                            if (this.type == 13)
                            {
                                this.ai[2] = 50f;
                            }
                            if (this.type == 0x27)
                            {
                                this.ai[2] = 15f;
                            }
                            this.ai[0] = NewNPC((int) this.position.X, (int) this.position.Y, this.type + 1, this.whoAmI);
                        }
                        else if ((((this.type == 8) || (this.type == 11)) || ((this.type == 14) || (this.type == 40))) && (this.ai[2] > 0f))
                        {
                            this.ai[0] = NewNPC((int) this.position.X, (int) this.position.Y, this.type, this.whoAmI);
                        }
                        else
                        {
                            this.ai[0] = NewNPC((int) this.position.X, (int) this.position.Y, this.type + 1, this.whoAmI);
                        }
                        Main.npc[(int) this.ai[0]].ai[1] = this.whoAmI;
                        Main.npc[(int) this.ai[0]].ai[2] = this.ai[2] - 1f;
                        this.netUpdate = true;
                    }
                    if ((((((this.type == 8) || (this.type == 9)) || ((this.type == 11) || (this.type == 12))) || (this.type == 40)) || (this.type == 0x29)) && !Main.npc[(int) this.ai[1]].active)
                    {
                        this.life = 0;
                        this.HitEffect(0, 10.0);
                        this.active = false;
                    }
                    if ((((((this.type == 7) || (this.type == 8)) || ((this.type == 10) || (this.type == 11))) || (this.type == 0x27)) || (this.type == 40)) && !Main.npc[(int) this.ai[0]].active)
                    {
                        this.life = 0;
                        this.HitEffect(0, 10.0);
                        this.active = false;
                    }
                    if (((this.type == 13) || (this.type == 14)) || (this.type == 15))
                    {
                        int life;
                        if (!(Main.npc[(int) this.ai[1]].active || Main.npc[(int) this.ai[0]].active))
                        {
                            this.life = 0;
                            this.HitEffect(0, 10.0);
                            this.active = false;
                        }
                        if (!((this.type != 13) || Main.npc[(int) this.ai[0]].active))
                        {
                            this.life = 0;
                            this.HitEffect(0, 10.0);
                            this.active = false;
                        }
                        if (!((this.type != 15) || Main.npc[(int) this.ai[1]].active))
                        {
                            this.life = 0;
                            this.HitEffect(0, 10.0);
                            this.active = false;
                        }
                        if ((this.type == 14) && !Main.npc[(int) this.ai[1]].active)
                        {
                            this.type = 13;
                            whoAmI = this.whoAmI;
                            life = this.life;
                            float num22 = this.ai[0];
                            this.SetDefaults(this.type);
                            this.life = life;
                            if (this.life > this.lifeMax)
                            {
                                this.life = this.lifeMax;
                            }
                            this.ai[0] = num22;
                            this.TargetClosest(true);
                            this.netUpdate = true;
                            this.whoAmI = whoAmI;
                        }
                        if ((this.type == 14) && !Main.npc[(int) this.ai[0]].active)
                        {
                            life = this.life;
                            whoAmI = this.whoAmI;
                            float num23 = this.ai[1];
                            this.SetDefaults(this.type);
                            this.life = life;
                            if (this.life > this.lifeMax)
                            {
                                this.life = this.lifeMax;
                            }
                            this.ai[1] = num23;
                            this.TargetClosest(true);
                            this.netUpdate = true;
                            this.whoAmI = whoAmI;
                        }
                        if (this.life == 0)
                        {
                            bool flag6 = true;
                            for (whoAmI = 0; whoAmI < 0x3e8; whoAmI++)
                            {
                                if (Main.npc[whoAmI].active && (((Main.npc[whoAmI].type == 13) || (Main.npc[whoAmI].type == 14)) || (Main.npc[whoAmI].type == 15)))
                                {
                                    flag6 = false;
                                    break;
                                }
                            }
                            if (flag6)
                            {
                                this.boss = true;
                                this.NPCLoot();
                            }
                        }
                    }
                    if (!(this.active || (Main.netMode != 2)))
                    {
                        NetMessage.SendData(0x1c, -1, -1, "", this.whoAmI, -1f, 0f, 0f);
                    }
                }
                int num24 = ((int) (this.position.X / 16f)) - 1;
                int maxTilesX = ((int) ((this.position.X + this.width) / 16f)) + 2;
                int num26 = ((int) (this.position.Y / 16f)) - 1;
                int maxTilesY = ((int) ((this.position.Y + this.height) / 16f)) + 2;
                if (num24 < 0)
                {
                    num24 = 0;
                }
                if (maxTilesX > Main.maxTilesX)
                {
                    maxTilesX = Main.maxTilesX;
                }
                if (num26 < 0)
                {
                    num26 = 0;
                }
                if (maxTilesY > Main.maxTilesY)
                {
                    maxTilesY = Main.maxTilesY;
                }
                bool flag7 = false;
                for (whoAmI = num24; whoAmI < maxTilesX; whoAmI++)
                {
                    for (num28 = num26; num28 < maxTilesY; num28++)
                    {
                        if ((Main.tile[whoAmI, num28] != null) && ((Main.tile[whoAmI, num28].active && (Main.tileSolid[Main.tile[whoAmI, num28].type] || (Main.tileSolidTop[Main.tile[whoAmI, num28].type] && (Main.tile[whoAmI, num28].frameY == 0)))) || (Main.tile[whoAmI, num28].liquid > 0x40)))
                        {
                            Vector2 vector4;
                            vector4.X = whoAmI * 0x10;
                            vector4.Y = num28 * 0x10;
                            if (((((this.position.X + this.width) > vector4.X) && (this.position.X < (vector4.X + 16f))) && ((this.position.Y + this.height) > vector4.Y)) && (this.position.Y < (vector4.Y + 16f)))
                            {
                                flag7 = true;
                                if ((Main.rand.Next(40) == 0) && Main.tile[whoAmI, num28].active)
                                {
                                    WorldGen.KillTile(whoAmI, num28, true, true, false);
                                }
                                if (((Main.netMode != 1) && (Main.tile[whoAmI, num28].type == 2)) && (Main.tile[whoAmI, num28 - 1].type != 0x1b))
                                {
                                }
                            }
                        }
                    }
                }
                num9 = 8f;
                num10 = 0.07f;
                if (this.type == 10)
                {
                    num9 = 6f;
                    num10 = 0.05f;
                }
                if (this.type == 13)
                {
                    num9 = 11f;
                    num10 = 0.08f;
                }
                vector = new Vector2(this.position.X + (this.width * 0.5f), this.position.Y + (this.height * 0.5f));
                num11 = (Main.player[this.target].position.X + (Main.player[this.target].width / 2)) - vector.X;
                num12 = (Main.player[this.target].position.Y + (Main.player[this.target].height / 2)) - vector.Y;
                num13 = (float) Math.Sqrt((double) ((num11 * num11) + (num12 * num12)));
                if (this.ai[1] > 0f)
                {
                    num11 = (Main.npc[(int) this.ai[1]].position.X + (Main.npc[(int) this.ai[1]].width / 2)) - vector.X;
                    num12 = (Main.npc[(int) this.ai[1]].position.Y + (Main.npc[(int) this.ai[1]].height / 2)) - vector.Y;
                    this.rotation = ((float) Math.Atan2((double) num12, (double) num11)) + 1.57f;
                    num13 = (float) Math.Sqrt((double) ((num11 * num11) + (num12 * num12)));
                    num13 = (num13 - this.width) / num13;
                    num11 *= num13;
                    num12 *= num13;
                    this.velocity = new Vector2();
                    this.position.X += num11;
                    this.position.Y += num12;
                }
                else
                {
                    if (!flag7)
                    {
                        this.TargetClosest(true);
                        this.velocity.Y += 0.11f;
                        if (this.velocity.Y > num9)
                        {
                            this.velocity.Y = num9;
                        }
                        if ((Math.Abs(this.velocity.X) + Math.Abs(this.velocity.Y)) < (num9 * 0.4))
                        {
                            if (this.velocity.X < 0f)
                            {
                                this.velocity.X -= num10 * 1.1f;
                            }
                            else
                            {
                                this.velocity.X += num10 * 1.1f;
                            }
                        }
                        else if (this.velocity.Y == num9)
                        {
                            if (this.velocity.X < num11)
                            {
                                this.velocity.X += num10;
                            }
                            else if (this.velocity.X > num11)
                            {
                                this.velocity.X -= num10;
                            }
                        }
                        else if (this.velocity.Y > 4f)
                        {
                            if (this.velocity.X < 0f)
                            {
                                this.velocity.X += num10 * 0.9f;
                            }
                            else
                            {
                                this.velocity.X -= num10 * 0.9f;
                            }
                        }
                    }
                    else
                    {
                        if (this.soundDelay == 0)
                        {
                            float num29 = num13 / 40f;
                            if (num29 < 10f)
                            {
                                num29 = 10f;
                            }
                            if (num29 > 20f)
                            {
                                num29 = 20f;
                            }
                            this.soundDelay = (int) num29;
                            Main.PlaySound(15, (int) this.position.X, (int) this.position.Y, 1);
                        }
                        num13 = (float) Math.Sqrt((double) ((num11 * num11) + (num12 * num12)));
                        float num30 = Math.Abs(num11);
                        float num31 = Math.Abs(num12);
                        num13 = num9 / num13;
                        num11 *= num13;
                        num12 *= num13;
                        if (((((this.velocity.X > 0f) && (num11 > 0f)) || ((this.velocity.X < 0f) && (num11 < 0f))) || ((this.velocity.Y > 0f) && (num12 > 0f))) || ((this.velocity.Y < 0f) && (num12 < 0f)))
                        {
                            if (this.velocity.X < num11)
                            {
                                this.velocity.X += num10;
                            }
                            else if (this.velocity.X > num11)
                            {
                                this.velocity.X -= num10;
                            }
                            if (this.velocity.Y < num12)
                            {
                                this.velocity.Y += num10;
                            }
                            else if (this.velocity.Y > num12)
                            {
                                this.velocity.Y -= num10;
                            }
                        }
                        else if (num30 > num31)
                        {
                            if (this.velocity.X < num11)
                            {
                                this.velocity.X += num10 * 1.1f;
                            }
                            else if (this.velocity.X > num11)
                            {
                                this.velocity.X -= num10 * 1.1f;
                            }
                            if ((Math.Abs(this.velocity.X) + Math.Abs(this.velocity.Y)) < (num9 * 0.5))
                            {
                                if (this.velocity.Y > 0f)
                                {
                                    this.velocity.Y += num10;
                                }
                                else
                                {
                                    this.velocity.Y -= num10;
                                }
                            }
                        }
                        else
                        {
                            if (this.velocity.Y < num12)
                            {
                                this.velocity.Y += num10 * 1.1f;
                            }
                            else if (this.velocity.Y > num12)
                            {
                                this.velocity.Y -= num10 * 1.1f;
                            }
                            if ((Math.Abs(this.velocity.X) + Math.Abs(this.velocity.Y)) < (num9 * 0.5))
                            {
                                if (this.velocity.X > 0f)
                                {
                                    this.velocity.X += num10;
                                }
                                else
                                {
                                    this.velocity.X -= num10;
                                }
                            }
                        }
                    }
                    this.rotation = ((float) Math.Atan2((double) this.velocity.Y, (double) this.velocity.X)) + 1.57f;
                }
                return;
            }
            if (flag9)
            {
                num19 = NewNPC(((int) this.position.X) + (this.width / 2), ((int) this.position.Y) + (this.height / 2), 0x23, 0);
                Main.npc[num19].netUpdate = true;
                string str = "Skeletron";
                if (Main.netMode == 0)
                {
                    Main.NewText(str + " has awoken!", 0xaf, 0x4b, 0xff);
                }
                else if (Main.netMode == 2)
                {
                    NetMessage.SendData(0x19, -1, -1, str + " has awoken!", 0xff, 175f, 75f, 255f);
                }
            }
            this.ai[3] = 1f;
            this.netUpdate = true;
        Label_524A:
            if (((((Main.netMode != 1) && this.townNPC) && !Main.dayTime) && ((num32 != this.homeTileX) || (num33 != this.homeTileY))) && !this.homeless)
            {
                bool flag10 = true;
                for (int j = 0; j < 2; j++)
                {
                    Rectangle rectangle = new Rectangle(((((int) this.position.X) + (this.width / 2)) - (sWidth / 2)) - safeRangeX, ((((int) this.position.Y) + (this.height / 2)) - (sHeight / 2)) - safeRangeY, sWidth + (safeRangeX * 2), sHeight + (safeRangeY * 2));
                    if (j == 1)
                    {
                        rectangle = new Rectangle((((this.homeTileX * 0x10) + 8) - (sWidth / 2)) - safeRangeX, (((this.homeTileY * 0x10) + 8) - (sHeight / 2)) - safeRangeY, sWidth + (safeRangeX * 2), sHeight + (safeRangeY * 2));
                    }
                    for (whoAmI = 0; whoAmI < 0xff; whoAmI++)
                    {
                        if (Main.player[whoAmI].active)
                        {
                            Rectangle rectangle2 = new Rectangle((int) Main.player[whoAmI].position.X, (int) Main.player[whoAmI].position.Y, Main.player[whoAmI].width, Main.player[whoAmI].height);
                            if (rectangle2.Intersects(rectangle))
                            {
                                flag10 = false;
                                break;
                            }
                        }
                        if (!flag10)
                        {
                            break;
                        }
                    }
                }
                if (flag10)
                {
                    if (!((this.type != 0x25) && Collision.SolidTiles(this.homeTileX - 1, this.homeTileX + 1, this.homeTileY - 3, this.homeTileY - 1)))
                    {
                        this.velocity.X = 0f;
                        this.velocity.Y = 0f;
                        this.position.X = ((this.homeTileX * 0x10) + 8) - (this.width / 2);
                        this.position.Y = ((this.homeTileY * 0x10) - this.height) - 0.1f;
                        this.netUpdate = true;
                    }
                    else
                    {
                        this.homeless = true;
                        WorldGen.QuickFindHome(this.whoAmI);
                    }
                }
            }
            if (this.ai[0] == 0f)
            {
                if (this.ai[2] > 0f)
                {
                    this.ai[2]--;
                }
                if (!Main.dayTime && !flag8)
                {
                    if (Main.netMode != 1)
                    {
                        if ((num32 == this.homeTileX) && (num33 == this.homeTileY))
                        {
                            if (this.velocity.X != 0f)
                            {
                                this.netUpdate = true;
                            }
                            if (this.velocity.X > 0.1)
                            {
                                this.velocity.X -= 0.1f;
                            }
                            else if (this.velocity.X < -0.1)
                            {
                                this.velocity.X += 0.1f;
                            }
                            else
                            {
                                this.velocity.X = 0f;
                            }
                        }
                        else if (!flag8)
                        {
                            if (num32 > this.homeTileX)
                            {
                                this.direction = -1;
                            }
                            else
                            {
                                this.direction = 1;
                            }
                            this.ai[0] = 1f;
                            this.ai[1] = 200 + Main.rand.Next(200);
                            this.ai[2] = 0f;
                            this.netUpdate = true;
                        }
                    }
                }
                else
                {
                    if (this.velocity.X > 0.1)
                    {
                        this.velocity.X -= 0.1f;
                    }
                    else if (this.velocity.X < -0.1)
                    {
                        this.velocity.X += 0.1f;
                    }
                    else
                    {
                        this.velocity.X = 0f;
                    }
                    if (Main.netMode != 1)
                    {
                        if (this.ai[1] > 0f)
                        {
                            this.ai[1]--;
                        }
                        if (this.ai[1] <= 0f)
                        {
                            this.ai[0] = 1f;
                            this.ai[1] = 200 + Main.rand.Next(200);
                            if (this.type == 0x2e)
                            {
                                this.ai[1] += Main.rand.Next(200, 400);
                            }
                            this.ai[2] = 0f;
                            this.netUpdate = true;
                        }
                    }
                }
                if ((Main.netMode != 1) && (Main.dayTime || ((num32 == this.homeTileX) && (num33 == this.homeTileY))))
                {
                    if ((num32 < (this.homeTileX - 0x19)) || (num32 > (this.homeTileX + 0x19)))
                    {
                        if (this.ai[2] == 0f)
                        {
                            if ((num32 < (this.homeTileX - 50)) && (this.direction == -1))
                            {
                                this.direction = 1;
                                this.netUpdate = true;
                            }
                            else if ((num32 > (this.homeTileX + 50)) && (this.direction == 1))
                            {
                                this.direction = -1;
                                this.netUpdate = true;
                            }
                        }
                    }
                    else if ((Main.rand.Next(80) == 0) && (this.ai[2] == 0f))
                    {
                        this.ai[2] = 200f;
                        this.direction *= -1;
                        this.netUpdate = true;
                    }
                }
            }
            else if (this.ai[0] == 1f)
            {
                if ((((Main.netMode != 1) && !Main.dayTime) && (num32 == this.homeTileX)) && (num33 == this.homeTileY))
                {
                    this.ai[0] = 0f;
                    this.ai[1] = 200 + Main.rand.Next(200);
                    this.ai[2] = 60f;
                    this.netUpdate = true;
                }
                else
                {
                    if (((Main.netMode != 1) && !this.homeless) && ((num32 < (this.homeTileX - 0x23)) || (num32 > (this.homeTileX + 0x23))))
                    {
                        if ((this.position.X < (this.homeTileX * 0x10)) && (this.direction == -1))
                        {
                            this.direction = 1;
                            this.velocity.X = 0.1f;
                            this.netUpdate = true;
                        }
                        else if ((this.position.X > (this.homeTileX * 0x10)) && (this.direction == 1))
                        {
                            this.direction = -1;
                            this.velocity.X = -0.1f;
                            this.netUpdate = true;
                        }
                    }
                    this.ai[1]--;
                    if (this.ai[1] <= 0f)
                    {
                        this.ai[0] = 0f;
                        this.ai[1] = 300 + Main.rand.Next(300);
                        if (this.type == 0x2e)
                        {
                            this.ai[1] -= Main.rand.Next(100);
                        }
                        this.ai[2] = 60f;
                        this.netUpdate = true;
                    }
                    if (this.closeDoor && ((((this.position.X + (this.width / 2)) / 16f) > (this.doorX + 2)) || (((this.position.X + (this.width / 2)) / 16f) < (this.doorX - 2))))
                    {
                        if (WorldGen.CloseDoor(this.doorX, this.doorY, false))
                        {
                            this.closeDoor = false;
                            NetMessage.SendData(0x13, -1, -1, "", 1, (float) this.doorX, (float) this.doorY, (float) this.direction);
                        }
                        if ((((((this.position.X + (this.width / 2)) / 16f) > (this.doorX + 4)) || (((this.position.X + (this.width / 2)) / 16f) < (this.doorX - 4))) || (((this.position.Y + (this.height / 2)) / 16f) > (this.doorY + 4))) || (((this.position.Y + (this.height / 2)) / 16f) < (this.doorY - 4)))
                        {
                            this.closeDoor = false;
                        }
                    }
                    if ((this.velocity.X < -1f) || (this.velocity.X > 1f))
                    {
                        if (this.velocity.Y == 0f)
                        {
                            this.velocity = (Vector2) (this.velocity * 0.8f);
                        }
                    }
                    else if ((this.velocity.X < 1.15) && (this.direction == 1))
                    {
                        this.velocity.X += 0.07f;
                        if (this.velocity.X > 1f)
                        {
                            this.velocity.X = 1f;
                        }
                    }
                    else if ((this.velocity.X > -1f) && (this.direction == -1))
                    {
                        this.velocity.X -= 0.07f;
                        if (this.velocity.X > 1f)
                        {
                            this.velocity.X = 1f;
                        }
                    }
                    if (this.velocity.Y == 0f)
                    {
                        if (this.position.X == this.ai[2])
                        {
                            this.direction *= -1;
                        }
                        this.ai[2] = -1f;
                        num3 = (int) (((this.position.X + (this.width / 2)) + (15 * this.direction)) / 16f);
                        num4 = (int) (((this.position.Y + this.height) - 16f) / 16f);
                        if (Main.tile[num3, num4] == null)
                        {
                            Main.tile[num3, num4] = new Tile();
                        }
                        if (Main.tile[num3, num4 - 1] == null)
                        {
                            Main.tile[num3, num4 - 1] = new Tile();
                        }
                        if (Main.tile[num3, num4 - 2] == null)
                        {
                            Main.tile[num3, num4 - 2] = new Tile();
                        }
                        if (Main.tile[num3, num4 - 3] == null)
                        {
                            Main.tile[num3, num4 - 3] = new Tile();
                        }
                        if (Main.tile[num3, num4 + 1] == null)
                        {
                            Main.tile[num3, num4 + 1] = new Tile();
                        }
                        if (Main.tile[num3 + this.direction, num4 - 1] == null)
                        {
                            Main.tile[num3 + this.direction, num4 - 1] = new Tile();
                        }
                        if (Main.tile[num3 + this.direction, num4 + 1] == null)
                        {
                            Main.tile[num3 + this.direction, num4 + 1] = new Tile();
                        }
                        if (((this.townNPC && Main.tile[num3, num4 - 2].active) && (Main.tile[num3, num4 - 2].type == 10)) && ((Main.rand.Next(10) == 0) || !Main.dayTime))
                        {
                            if (Main.netMode != 1)
                            {
                                if (WorldGen.OpenDoor(num3, num4 - 2, this.direction))
                                {
                                    this.closeDoor = true;
                                    this.doorX = num3;
                                    this.doorY = num4 - 2;
                                    NetMessage.SendData(0x13, -1, -1, "", 0, (float) num3, (float) (num4 - 2), (float) this.direction);
                                    this.netUpdate = true;
                                    this.ai[1] += 80f;
                                }
                                else if (WorldGen.OpenDoor(num3, num4 - 2, -this.direction))
                                {
                                    this.closeDoor = true;
                                    this.doorX = num3;
                                    this.doorY = num4 - 2;
                                    NetMessage.SendData(0x13, -1, -1, "", 0, (float) num3, (float) (num4 - 2), (float) -this.direction);
                                    this.netUpdate = true;
                                    this.ai[1] += 80f;
                                }
                                else
                                {
                                    this.direction *= -1;
                                    this.netUpdate = true;
                                }
                            }
                        }
                        else
                        {
                            if (((this.velocity.X < 0f) && (this.spriteDirection == -1)) || ((this.velocity.X > 0f) && (this.spriteDirection == 1)))
                            {
                                if ((Main.tile[num3, num4 - 2].active && Main.tileSolid[Main.tile[num3, num4 - 2].type]) && !Main.tileSolidTop[Main.tile[num3, num4 - 2].type])
                                {
                                    if (((this.direction == 1) && !Collision.SolidTiles(num3 - 2, num3 - 1, num4 - 5, num4 - 1)) || ((this.direction == -1) && !Collision.SolidTiles(num3 + 1, num3 + 2, num4 - 5, num4 - 1)))
                                    {
                                        if (!Collision.SolidTiles(num3, num3, num4 - 5, num4 - 3))
                                        {
                                            this.velocity.Y = -6f;
                                            this.netUpdate = true;
                                        }
                                        else
                                        {
                                            this.direction *= -1;
                                            this.netUpdate = true;
                                        }
                                    }
                                    else
                                    {
                                        this.direction *= -1;
                                        this.netUpdate = true;
                                    }
                                }
                                else if ((Main.tile[num3, num4 - 1].active && Main.tileSolid[Main.tile[num3, num4 - 1].type]) && !Main.tileSolidTop[Main.tile[num3, num4 - 1].type])
                                {
                                    if (((this.direction == 1) && !Collision.SolidTiles(num3 - 2, num3 - 1, num4 - 4, num4 - 1)) || ((this.direction == -1) && !Collision.SolidTiles(num3 + 1, num3 + 2, num4 - 4, num4 - 1)))
                                    {
                                        if (!Collision.SolidTiles(num3, num3, num4 - 4, num4 - 2))
                                        {
                                            this.velocity.Y = -5f;
                                            this.netUpdate = true;
                                        }
                                        else
                                        {
                                            this.direction *= -1;
                                            this.netUpdate = true;
                                        }
                                    }
                                    else
                                    {
                                        this.direction *= -1;
                                        this.netUpdate = true;
                                    }
                                }
                                else if ((Main.tile[num3, num4].active && Main.tileSolid[Main.tile[num3, num4].type]) && !Main.tileSolidTop[Main.tile[num3, num4].type])
                                {
                                    if (((this.direction == 1) && !Collision.SolidTiles(num3 - 2, num3, num4 - 3, num4 - 1)) || ((this.direction == -1) && !Collision.SolidTiles(num3, num3 + 2, num4 - 3, num4 - 1)))
                                    {
                                        this.velocity.Y = -3.6f;
                                        this.netUpdate = true;
                                    }
                                    else
                                    {
                                        this.direction *= -1;
                                        this.netUpdate = true;
                                    }
                                }
                                if (Main.tile[num3, num4 + 1] == null)
                                {
                                    Main.tile[num3, num4 + 1] = new Tile();
                                }
                                if (Main.tile[num3 - this.direction, num4 + 1] == null)
                                {
                                    Main.tile[num3 - this.direction, num4 + 1] = new Tile();
                                }
                                if (Main.tile[num3, num4 + 2] == null)
                                {
                                    Main.tile[num3, num4 + 2] = new Tile();
                                }
                                if (Main.tile[num3 - this.direction, num4 + 2] == null)
                                {
                                    Main.tile[num3 - this.direction, num4 + 2] = new Tile();
                                }
                                if (Main.tile[num3, num4 + 3] == null)
                                {
                                    Main.tile[num3, num4 + 3] = new Tile();
                                }
                                if (Main.tile[num3 - this.direction, num4 + 3] == null)
                                {
                                    Main.tile[num3 - this.direction, num4 + 3] = new Tile();
                                }
                                if (Main.tile[num3, num4 + 4] == null)
                                {
                                    Main.tile[num3, num4 + 4] = new Tile();
                                }
                                if (Main.tile[num3 - this.direction, num4 + 4] == null)
                                {
                                    Main.tile[num3 - this.direction, num4 + 4] = new Tile();
                                }
                                else if (((((((num32 >= (this.homeTileX - 0x23)) && (num32 <= (this.homeTileX + 0x23))) && (!Main.tile[num3, num4 + 1].active || !Main.tileSolid[Main.tile[num3, num4 + 1].type])) && ((!Main.tile[num3 - this.direction, num4 + 1].active || !Main.tileSolid[Main.tile[num3 - this.direction, num4 + 1].type]) && (!Main.tile[num3, num4 + 2].active || !Main.tileSolid[Main.tile[num3, num4 + 2].type]))) && (((!Main.tile[num3 - this.direction, num4 + 2].active || !Main.tileSolid[Main.tile[num3 - this.direction, num4 + 2].type]) && (!Main.tile[num3, num4 + 3].active || !Main.tileSolid[Main.tile[num3, num4 + 3].type])) && ((!Main.tile[num3 - this.direction, num4 + 3].active || !Main.tileSolid[Main.tile[num3 - this.direction, num4 + 3].type]) && (!Main.tile[num3, num4 + 4].active || !Main.tileSolid[Main.tile[num3, num4 + 4].type])))) && (!Main.tile[num3 - this.direction, num4 + 4].active || !Main.tileSolid[Main.tile[num3 - this.direction, num4 + 4].type])) && (this.type != 0x2e))
                                {
                                    this.direction *= -1;
                                    this.velocity.X *= -1f;
                                    this.netUpdate = true;
                                }
                                if (this.velocity.Y < 0f)
                                {
                                    this.ai[2] = this.position.X;
                                }
                            }
                            if ((this.velocity.Y < 0f) && this.wet)
                            {
                                this.velocity.Y *= 1.2f;
                            }
                            if ((this.velocity.Y < 0f) && (this.type == 0x2e))
                            {
                                this.velocity.Y *= 1.2f;
                            }
                        }
                    }
                }
            }
        }

        public void CheckActive()
        {
            if (this.active && (((((this.type != 8) && (this.type != 9)) && ((this.type != 11) && (this.type != 12))) && (((this.type != 14) && (this.type != 15)) && (this.type != 40))) && (this.type != 0x29)))
            {
                int num;
                if (this.townNPC)
                {
                    if (this.position.Y < (Main.worldSurface * 18.0))
                    {
                        Rectangle rectangle = new Rectangle((((int) this.position.X) + (this.width / 2)) - townRangeX, (((int) this.position.Y) + (this.height / 2)) - townRangeY, townRangeX * 2, townRangeY * 2);
                        for (num = 0; num < 0xff; num++)
                        {
                            if (Main.player[num].active && rectangle.Intersects(new Rectangle((int) Main.player[num].position.X, (int) Main.player[num].position.Y, Main.player[num].width, Main.player[num].height)))
                            {
                                Player player1 = Main.player[num];
                                player1.townNPCs++;
                            }
                        }
                    }
                }
                else
                {
                    bool flag = false;
                    Rectangle rectangle2 = new Rectangle((((int) this.position.X) + (this.width / 2)) - activeRangeX, (((int) this.position.Y) + (this.height / 2)) - activeRangeY, activeRangeX * 2, activeRangeY * 2);
                    Rectangle rectangle3 = new Rectangle(((int) ((this.position.X + (this.width / 2)) - (sWidth * 0.5))) - this.width, ((int) ((this.position.Y + (this.height / 2)) - (sHeight * 0.5))) - this.height, sWidth + (this.width * 2), sHeight + (this.height * 2));
                    for (num = 0; num < 0xff; num++)
                    {
                        if (Main.player[num].active)
                        {
                            if (rectangle2.Intersects(new Rectangle((int) Main.player[num].position.X, (int) Main.player[num].position.Y, Main.player[num].width, Main.player[num].height)))
                            {
                                flag = true;
                                if (((this.type != 0x19) && (this.type != 30)) && (this.type != 0x21))
                                {
                                    Player player2 = Main.player[num];
                                    player2.activeNPCs++;
                                }
                            }
                            if (rectangle3.Intersects(new Rectangle((int) Main.player[num].position.X, (int) Main.player[num].position.Y, Main.player[num].width, Main.player[num].height)))
                            {
                                this.timeLeft = activeTime;
                            }
                            if (((this.type == 7) || (this.type == 10)) || (this.type == 13))
                            {
                                flag = true;
                            }
                            if ((this.boss || (this.type == 0x23)) || (this.type == 0x24))
                            {
                                flag = true;
                            }
                        }
                    }
                    this.timeLeft--;
                    if (this.timeLeft <= 0)
                    {
                        flag = false;
                    }
                    if (!flag && (Main.netMode != 1))
                    {
                        noSpawnCycle = true;
                        this.active = false;
                        if (Main.netMode == 2)
                        {
                            this.life = 0;
                            NetMessage.SendData(0x17, -1, -1, "", this.whoAmI, 0f, 0f, 0f);
                        }
                    }
                }
            }
        }

        public void FindFrame()
        {
            int num = 1;
            if (!Main.dedServ)
            {
                num = Main.npcTexture[this.type].Height / Main.npcFrameCount[this.type];
            }
            int num2 = 0;
            if (this.aiAction == 0)
            {
                if (this.velocity.Y < 0f)
                {
                    num2 = 2;
                }
                else if (this.velocity.Y > 0f)
                {
                    num2 = 3;
                }
                else if (this.velocity.X != 0f)
                {
                    num2 = 1;
                }
                else
                {
                    num2 = 0;
                }
            }
            else if (this.aiAction == 1)
            {
                num2 = 4;
            }
            if ((this.type == 1) || (this.type == 0x10))
            {
                this.frameCounter++;
                if (num2 > 0)
                {
                    this.frameCounter++;
                }
                if (num2 == 4)
                {
                    this.frameCounter++;
                }
                if (this.frameCounter >= 8.0)
                {
                    this.frame.Y += num;
                    this.frameCounter = 0.0;
                }
                if (this.frame.Y >= (num * Main.npcFrameCount[this.type]))
                {
                    this.frame.Y = 0;
                }
            }
            if (this.type == 50)
            {
                if (this.velocity.Y != 0f)
                {
                    this.frame.Y = num * 4;
                }
                else
                {
                    this.frameCounter++;
                    if (num2 > 0)
                    {
                        this.frameCounter++;
                    }
                    if (num2 == 4)
                    {
                        this.frameCounter++;
                    }
                    if (this.frameCounter >= 8.0)
                    {
                        this.frame.Y += num;
                        this.frameCounter = 0.0;
                    }
                    if (this.frame.Y >= (num * 4))
                    {
                        this.frame.Y = 0;
                    }
                }
            }
            if ((this.type == 2) || (this.type == 0x17))
            {
                if (this.velocity.X > 0f)
                {
                    this.spriteDirection = 1;
                    this.rotation = (float) Math.Atan2((double) this.velocity.Y, (double) this.velocity.X);
                }
                if (this.velocity.X < 0f)
                {
                    this.spriteDirection = -1;
                    this.rotation = ((float) Math.Atan2((double) this.velocity.Y, (double) this.velocity.X)) + 3.14f;
                }
                this.frameCounter++;
                if (this.frameCounter >= 8.0)
                {
                    this.frame.Y += num;
                    this.frameCounter = 0.0;
                }
                if (this.frame.Y >= (num * Main.npcFrameCount[this.type]))
                {
                    this.frame.Y = 0;
                }
            }
            if (((this.type == 0x37) || (this.type == 0x39)) || (this.type == 0x3a))
            {
                this.spriteDirection = this.direction;
                this.frameCounter++;
                if (this.wet)
                {
                    if (this.frameCounter < 6.0)
                    {
                        this.frame.Y = 0;
                    }
                    else if (this.frameCounter < 12.0)
                    {
                        this.frame.Y = num;
                    }
                    else if (this.frameCounter < 18.0)
                    {
                        this.frame.Y = num * 2;
                    }
                    else if (this.frameCounter < 24.0)
                    {
                        this.frame.Y = num * 3;
                    }
                    else
                    {
                        this.frameCounter = 0.0;
                    }
                }
                else if (this.frameCounter < 6.0)
                {
                    this.frame.Y = num * 4;
                }
                else if (this.frameCounter < 12.0)
                {
                    this.frame.Y = num * 5;
                }
                else
                {
                    this.frameCounter = 0.0;
                }
            }
            if (((this.type == 0x30) || (this.type == 0x31)) || (this.type == 0x33))
            {
                if (this.velocity.X > 0f)
                {
                    this.spriteDirection = 1;
                }
                if (this.velocity.X < 0f)
                {
                    this.spriteDirection = -1;
                }
                this.rotation = this.velocity.X * 0.1f;
                this.frameCounter++;
                if (this.frameCounter >= 6.0)
                {
                    this.frame.Y += num;
                    this.frameCounter = 0.0;
                }
                if (this.frame.Y >= (num * 4))
                {
                    this.frame.Y = 0;
                }
            }
            if (this.type == 0x2a)
            {
                if (this.velocity.X > 0f)
                {
                    this.spriteDirection = 1;
                }
                if (this.velocity.X < 0f)
                {
                    this.spriteDirection = -1;
                }
                this.rotation = this.velocity.X * 0.1f;
                this.frameCounter++;
                if (this.frameCounter < 4.0)
                {
                    this.frame.Y = 0;
                }
                else if (this.frameCounter < 8.0)
                {
                    this.frame.Y = num;
                }
                else if (this.frameCounter < 12.0)
                {
                    this.frame.Y = num * 2;
                }
                else if (this.frameCounter < 16.0)
                {
                    this.frame.Y = num;
                }
                if (this.frameCounter == 15.0)
                {
                    this.frameCounter = 0.0;
                }
            }
            if ((this.type == 0x2b) || (this.type == 0x38))
            {
                this.frameCounter++;
                if (this.frameCounter < 6.0)
                {
                    this.frame.Y = 0;
                }
                else if (this.frameCounter < 12.0)
                {
                    this.frame.Y = num;
                }
                else if (this.frameCounter < 18.0)
                {
                    this.frame.Y = num * 2;
                }
                else if (this.frameCounter < 24.0)
                {
                    this.frame.Y = num;
                }
                if (this.frameCounter == 23.0)
                {
                    this.frameCounter = 0.0;
                }
            }
            if ((((((this.type == 0x11) || (this.type == 0x12)) || ((this.type == 0x13) || (this.type == 20))) || (((this.type == 0x16) || (this.type == 0x26)) || ((this.type == 0x1a) || (this.type == 0x1b)))) || ((((this.type == 0x1c) || (this.type == 0x1f)) || ((this.type == 0x15) || (this.type == 0x2c))) || (this.type == 0x36))) || (this.type == 0x25))
            {
                if (this.velocity.Y == 0f)
                {
                    if (this.direction == 1)
                    {
                        this.spriteDirection = 1;
                    }
                    if (this.direction == -1)
                    {
                        this.spriteDirection = -1;
                    }
                    if (this.velocity.X == 0f)
                    {
                        this.frame.Y = 0;
                        this.frameCounter = 0.0;
                    }
                    else
                    {
                        this.frameCounter += Math.Abs(this.velocity.X) * 2f;
                        this.frameCounter++;
                        if (this.frameCounter > 6.0)
                        {
                            this.frame.Y += num;
                            this.frameCounter = 0.0;
                        }
                        if ((this.frame.Y / num) >= Main.npcFrameCount[this.type])
                        {
                            this.frame.Y = num * 2;
                        }
                    }
                }
                else
                {
                    this.frameCounter = 0.0;
                    this.frame.Y = num;
                    if (((this.type == 0x15) || (this.type == 0x1f)) || (this.type == 0x2c))
                    {
                        this.frame.Y = 0;
                    }
                }
            }
            else if (((this.type == 3) || (this.type == 0x34)) || (this.type == 0x35))
            {
                if (this.velocity.Y == 0f)
                {
                    if (this.direction == 1)
                    {
                        this.spriteDirection = 1;
                    }
                    if (this.direction == -1)
                    {
                        this.spriteDirection = -1;
                    }
                }
                if (((this.velocity.Y != 0f) || ((this.direction == -1) && (this.velocity.X > 0f))) || ((this.direction == 1) && (this.velocity.X < 0f)))
                {
                    this.frameCounter = 0.0;
                    this.frame.Y = num * 2;
                }
                else if (this.velocity.X == 0f)
                {
                    this.frameCounter = 0.0;
                    this.frame.Y = 0;
                }
                else
                {
                    this.frameCounter += Math.Abs(this.velocity.X);
                    if (this.frameCounter < 8.0)
                    {
                        this.frame.Y = 0;
                    }
                    else if (this.frameCounter < 16.0)
                    {
                        this.frame.Y = num;
                    }
                    else if (this.frameCounter < 24.0)
                    {
                        this.frame.Y = num * 2;
                    }
                    else if (this.frameCounter < 32.0)
                    {
                        this.frame.Y = num;
                    }
                    else
                    {
                        this.frameCounter = 0.0;
                    }
                }
            }
            else if ((this.type == 0x2e) || (this.type == 0x2f))
            {
                if (this.velocity.Y == 0f)
                {
                    if (this.direction == 1)
                    {
                        this.spriteDirection = 1;
                    }
                    if (this.direction == -1)
                    {
                        this.spriteDirection = -1;
                    }
                    if (this.velocity.X == 0f)
                    {
                        this.frame.Y = 0;
                        this.frameCounter = 0.0;
                    }
                    else
                    {
                        this.frameCounter += Math.Abs(this.velocity.X) * 1f;
                        this.frameCounter++;
                        if (this.frameCounter > 6.0)
                        {
                            this.frame.Y += num;
                            this.frameCounter = 0.0;
                        }
                        if ((this.frame.Y / num) >= Main.npcFrameCount[this.type])
                        {
                            this.frame.Y = 0;
                        }
                    }
                }
                else if (this.velocity.Y < 0f)
                {
                    this.frameCounter = 0.0;
                    this.frame.Y = num * 4;
                }
                else if (this.velocity.Y > 0f)
                {
                    this.frameCounter = 0.0;
                    this.frame.Y = num * 6;
                }
            }
            else if (this.type == 4)
            {
                this.frameCounter++;
                if (this.frameCounter < 7.0)
                {
                    this.frame.Y = 0;
                }
                else if (this.frameCounter < 14.0)
                {
                    this.frame.Y = num;
                }
                else if (this.frameCounter < 21.0)
                {
                    this.frame.Y = num * 2;
                }
                else
                {
                    this.frameCounter = 0.0;
                    this.frame.Y = 0;
                }
                if (this.ai[0] > 1f)
                {
                    this.frame.Y += num * 3;
                }
            }
            else if (this.type == 5)
            {
                this.frameCounter++;
                if (this.frameCounter >= 8.0)
                {
                    this.frame.Y += num;
                    this.frameCounter = 0.0;
                }
                if (this.frame.Y >= (num * Main.npcFrameCount[this.type]))
                {
                    this.frame.Y = 0;
                }
            }
            else if (this.type == 6)
            {
                this.frameCounter++;
                if (this.frameCounter >= 8.0)
                {
                    this.frame.Y += num;
                    this.frameCounter = 0.0;
                }
                if (this.frame.Y >= (num * Main.npcFrameCount[this.type]))
                {
                    this.frame.Y = 0;
                }
            }
            else if (this.type == 0x18)
            {
                if (this.velocity.Y == 0f)
                {
                    if (this.direction == 1)
                    {
                        this.spriteDirection = 1;
                    }
                    if (this.direction == -1)
                    {
                        this.spriteDirection = -1;
                    }
                }
                if (this.ai[1] > 0f)
                {
                    if (this.frame.Y < 4)
                    {
                        this.frameCounter = 0.0;
                    }
                    this.frameCounter++;
                    if (this.frameCounter <= 4.0)
                    {
                        this.frame.Y = num * 4;
                    }
                    else if (this.frameCounter <= 8.0)
                    {
                        this.frame.Y = num * 5;
                    }
                    else if (this.frameCounter <= 12.0)
                    {
                        this.frame.Y = num * 6;
                    }
                    else if (this.frameCounter <= 16.0)
                    {
                        this.frame.Y = num * 7;
                    }
                    else if (this.frameCounter <= 20.0)
                    {
                        this.frame.Y = num * 8;
                    }
                    else
                    {
                        this.frame.Y = num * 9;
                        this.frameCounter = 100.0;
                    }
                }
                else
                {
                    this.frameCounter++;
                    if (this.frameCounter <= 4.0)
                    {
                        this.frame.Y = 0;
                    }
                    else if (this.frameCounter <= 8.0)
                    {
                        this.frame.Y = num;
                    }
                    else if (this.frameCounter <= 12.0)
                    {
                        this.frame.Y = num * 2;
                    }
                    else
                    {
                        this.frame.Y = num * 3;
                        if (this.frameCounter >= 16.0)
                        {
                            this.frameCounter = 0.0;
                        }
                    }
                }
            }
            else if (((this.type == 0x1d) || (this.type == 0x20)) || (this.type == 0x2d))
            {
                if (this.velocity.Y == 0f)
                {
                    if (this.direction == 1)
                    {
                        this.spriteDirection = 1;
                    }
                    if (this.direction == -1)
                    {
                        this.spriteDirection = -1;
                    }
                }
                this.frame.Y = 0;
                if (this.velocity.Y != 0f)
                {
                    this.frame.Y += num;
                }
                else if (this.ai[1] > 0f)
                {
                    this.frame.Y += num * 2;
                }
            }
            if (this.type == 0x22)
            {
                if (this.velocity.X > 0f)
                {
                    this.spriteDirection = -1;
                    this.rotation = (float) Math.Atan2((double) this.velocity.Y, (double) this.velocity.X);
                }
                if (this.velocity.X < 0f)
                {
                    this.spriteDirection = 1;
                    this.rotation = ((float) Math.Atan2((double) this.velocity.Y, (double) this.velocity.X)) + 3.14f;
                }
                this.frameCounter++;
                if (this.frameCounter >= 4.0)
                {
                    this.frame.Y += num;
                    this.frameCounter = 0.0;
                }
                if (this.frame.Y >= (num * Main.npcFrameCount[this.type]))
                {
                    this.frame.Y = 0;
                }
            }
        }

        public Color GetAlpha(Color newColor)
        {
            int r = newColor.R - this.alpha;
            int g = newColor.G - this.alpha;
            int b = newColor.B - this.alpha;
            int a = newColor.A - this.alpha;
            if (((this.type == 0x19) || (this.type == 30)) || (this.type == 0x21))
            {
                r = newColor.R;
                g = newColor.G;
                b = newColor.B;
            }
            if (a < 0)
            {
                a = 0;
            }
            if (a > 0xff)
            {
                a = 0xff;
            }
            return new Color(r, g, b, a);
        }

        public string GetChat()
        {
            int num2;
            bool flag = false;
            bool flag2 = false;
            bool flag3 = false;
            bool flag4 = false;
            bool flag5 = false;
            bool flag6 = false;
            for (int i = 0; i < 0x3e8; i++)
            {
                if (Main.npc[i].type == 0x11)
                {
                    flag = true;
                }
                else if (Main.npc[i].type == 0x12)
                {
                    flag2 = true;
                }
                else if (Main.npc[i].type == 0x13)
                {
                    flag3 = true;
                }
                else if (Main.npc[i].type == 20)
                {
                    flag4 = true;
                }
                else if (Main.npc[i].type == 0x25)
                {
                    flag5 = true;
                }
                else if (Main.npc[i].type == 0x26)
                {
                    flag6 = true;
                }
            }
            string str = "";
            if (this.type == 0x11)
            {
                if (Main.dayTime)
                {
                    if (Main.time < 16200.0)
                    {
                        if (Main.rand.Next(2) == 0)
                        {
                            return "Sword beats paper, get one today.";
                        }
                        return "Lovely morning, wouldn't you say? Was there something you needed?";
                    }
                    if (Main.time > 37800.0)
                    {
                        if (Main.rand.Next(2) == 0)
                        {
                            return "Night be upon us soon, friend. Make your choices while you can.";
                        }
                        return ("Ah, they will tell tales of " + Main.player[Main.myPlayer].name + " some day... good ones I'm sure.");
                    }
                    switch (Main.rand.Next(3))
                    {
                        case 0:
                            str = "Check out my dirt blocks, they are extra dirty.";
                            break;

                        case 1:
                            return "Boy, that sun is hot! I do have some perfectly ventilated armor.";
                    }
                    return "The sun is high, but my prices are not.";
                }
                if (Main.bloodMoon)
                {
                    if (Main.rand.Next(2) == 0)
                    {
                        return "Have you seen Chith...Shith.. Chat... The big eye?";
                    }
                    return "Keep your eye on the prize, buy a lense!";
                }
                if (Main.time < 9720.0)
                {
                    if (Main.rand.Next(2) == 0)
                    {
                        return "Kosh, kapleck Mog. Oh sorry, thats klingon for 'Buy something or die.'";
                    }
                    return (Main.player[Main.myPlayer].name + " is it? I've heard good things, friend!");
                }
                if (Main.time > 22680.0)
                {
                    if (Main.rand.Next(2) == 0)
                    {
                        return "I hear there's a secret treasure... oh never mind.";
                    }
                    return "Angel Statue you say? I'm sorry, I'm not a junk dealer.";
                }
                num2 = Main.rand.Next(3);
                if (num2 == 0)
                {
                    str = "The last guy who was here left me some junk..er I mean.. treasures!";
                }
                if (num2 == 1)
                {
                    return "I wonder if the moon is made of cheese...huh, what? Oh yes, buy something!";
                }
                return "Did you say gold?  I'll take that off of ya'.";
            }
            if (this.type == 0x12)
            {
                if (flag6 && (Main.rand.Next(4) == 0))
                {
                    return "I wish that bomb maker would be more careful.  I'm getting tired of having to sew his limbs back on every day.";
                }
                if (Main.player[Main.myPlayer].statLife < (Main.player[Main.myPlayer].statLifeMax * 0.33))
                {
                    switch (Main.rand.Next(5))
                    {
                        case 0:
                            return "I think you look better this way.";

                        case 1:
                            return "Eww.. What happened to your face?";

                        case 2:
                            return "MY GOODNESS! I'm good but I'm not THAT good.";

                        case 3:
                            return "Dear friends we are gathered here today to bid farewell... oh, you'll be fine.";
                    }
                    return "You left your arm over there. Let me get that for you..";
                }
                if (Main.player[Main.myPlayer].statLife < (Main.player[Main.myPlayer].statLifeMax * 0.66))
                {
                    switch (Main.rand.Next(4))
                    {
                        case 0:
                            return "Quit being such a baby! I've seen worse.";

                        case 1:
                            return "That's gonna need stitches!";

                        case 2:
                            return "Trouble with those bullies again?";
                    }
                    return "You look half digested. Have you been chasing slimes again?";
                }
                switch (Main.rand.Next(3))
                {
                    case 0:
                        return "Turn your head and cough.";

                    case 1:
                        return "Thats not the biggest I've ever seen... Yes, I've seen bigger wounds for sure.";
                }
                return "Show me where it hurts.";
            }
            if (this.type == 0x13)
            {
                if (flag2 && (Main.rand.Next(4) == 0))
                {
                    return "Make it quick! I've got a date with the nurse in an hour.";
                }
                if (flag4 && (Main.rand.Next(4) == 0))
                {
                    return "That dryad is a looker.  Too bad she's such a prude.";
                }
                if (flag6 && (Main.rand.Next(4) == 0))
                {
                    return "Don't bother with that firework vendor, I've got all you need right here.";
                }
                if (Main.bloodMoon)
                {
                    return "I love nights like tonight.  There is never a shortage of things to kill!";
                }
                switch (Main.rand.Next(2))
                {
                    case 0:
                        return "I see you're eyeballin' the Minishark.. You really don't want to know how it was made.";

                    case 1:
                        str = "Keep your hands off my gun, buddy!";
                        break;
                }
                return str;
            }
            if (this.type == 20)
            {
                if (flag3 && (Main.rand.Next(4) == 0))
                {
                    return "I wish that gun seller would stop talking to me. Doesn't he realize I'm 500 years old?";
                }
                if (flag && (Main.rand.Next(4) == 0))
                {
                    return "That merchant keeps trying to sell me an angel statue. Everyone knows that they don't do anything.";
                }
                if (flag5 && (Main.rand.Next(4) == 0))
                {
                    return "Have you seen the old man walking around the dungeon? He doesn't look well at all...";
                }
                if (Main.bloodMoon)
                {
                    return "It is an evil moon tonight. Be careful.";
                }
                switch (Main.rand.Next(6))
                {
                    case 0:
                        return "You must cleanse the world of this corruption.";

                    case 1:
                        return "Be safe; Terraria needs you!";

                    case 2:
                        return "The sands of time are flowing. And well, you are not aging very gracefully.";

                    case 3:
                        return "Whats this about me having more 'bark' than bite?";

                    case 4:
                        return "So two goblins walk into a bar, and one says to the other, 'Want to get a Gobblet of beer?!'";
                }
                return "Be safe; Terraria needs you!";
            }
            if (this.type == 0x16)
            {
                if (Main.bloodMoon)
                {
                    return "You can tell a Blood Moon is out when the sky turns red. There is something about it that causes monsters to swarm.";
                }
                if (!Main.dayTime)
                {
                    return "You should stay indoors at night. It is very dangerous to be wandering around in the dark.";
                }
                switch (Main.rand.Next(3))
                {
                    case 0:
                        return ("Greetings, " + Main.player[Main.myPlayer].name + ". Is there something I can help you with?");

                    case 1:
                        return "I am here to give you advice on what to do next.  It is recommended that you talk with me anytime you get stuck.";

                    case 2:
                        str = "They say there is a person who will tell you how to survive in this land... oh wait. Thats me.";
                        break;
                }
                return str;
            }
            if (this.type == 0x25)
            {
                if (Main.dayTime)
                {
                    switch (Main.rand.Next(2))
                    {
                        case 0:
                            return "I cannot let you enter until you free me of my curse.";

                        case 1:
                            str = "Come back at night if you wish to enter.";
                            break;
                    }
                }
                return str;
            }
            if (this.type == 0x26)
            {
                if (Main.bloodMoon)
                {
                    return "I've got something for them zombies alright!";
                }
                if (flag3 && (Main.rand.Next(4) == 0))
                {
                    return "Even the gun dealer wants what I'm selling!";
                }
                if (flag2 && (Main.rand.Next(4) == 0))
                {
                    return "I'm sure the nurse will help if you accidentally lose a limb to these.";
                }
                if (flag4 && (Main.rand.Next(4) == 0))
                {
                    return "Why purify the world when you can just blow it up?";
                }
                switch (Main.rand.Next(4))
                {
                    case 0:
                        return "Explosives are da' bomb these days.  Buy some now!";

                    case 1:
                        return "It's a good day to die!";

                    case 2:
                        return "I wonder what happens if I... (BOOM!)... Oh, sorry, did you need that leg?";

                    case 3:
                        return "Dynamite, my own special cure-all for what ails ya.";
                }
                return "Check out my goods; they have explosive prices!";
            }
            if (this.type != 0x36)
            {
                return str;
            }
            if (Main.bloodMoon)
            {
                return (Main.player[Main.myPlayer].name + "... we have a problem! Its a blood moon out there!");
            }
            if (flag2 && (Main.rand.Next(4) == 0))
            {
                return "T'were I younger, I would ask the nurse out. I used to be quite the lady killer.";
            }
            if (Main.player[Main.myPlayer].head == 0x18)
            {
                return "That Red Hat of yours looks familiar...";
            }
            num2 = Main.rand.Next(4);
            if (num2 == 0)
            {
                return "Thanks again for freeing me from my curse. Felt like something jumped up and bit me";
            }
            if (num2 == 1)
            {
                return "Mama always said I would make a great tailor.";
            }
            if (num2 == 2)
            {
                return "Life's like a box of clothes, you never know what you are gonna wear!";
            }
            return "Being cursed was lonely, so I once made a friend out of leather. I named him Wilson.";
        }

        public Color GetColor(Color newColor)
        {
            int r = this.color.R - (0xff - newColor.R);
            int g = this.color.G - (0xff - newColor.G);
            int b = this.color.B - (0xff - newColor.B);
            int a = this.color.A - (0xff - newColor.A);
            if (r < 0)
            {
                r = 0;
            }
            if (r > 0xff)
            {
                r = 0xff;
            }
            if (g < 0)
            {
                g = 0;
            }
            if (g > 0xff)
            {
                g = 0xff;
            }
            if (b < 0)
            {
                b = 0;
            }
            if (b > 0xff)
            {
                b = 0xff;
            }
            if (a < 0)
            {
                a = 0;
            }
            if (a > 0xff)
            {
                a = 0xff;
            }
            return new Color(r, g, b, a);
        }

        public void HitEffect(int hitDirection = 0, double dmg = 10.0)
        {
            int num;
            int num2;
            int num3;
            int num4;
            if ((this.type == 1) || (this.type == 0x10))
            {
                if (this.life > 0)
                {
                    for (num = 0; num < ((dmg / ((double) this.lifeMax)) * 100.0); num++)
                    {
                        Dust.NewDust(this.position, this.width, this.height, 4, (float) hitDirection, -1f, this.alpha, this.color, 1f);
                    }
                }
                else
                {
                    num = 0;
                    while (num < 50)
                    {
                        Dust.NewDust(this.position, this.width, this.height, 4, (float) (2 * hitDirection), -2f, this.alpha, this.color, 1f);
                        num++;
                    }
                    if ((Main.netMode != 1) && (this.type == 0x10))
                    {
                        num2 = Main.rand.Next(2) + 2;
                        for (num3 = 0; num3 < num2; num3++)
                        {
                            num4 = NewNPC(((int) this.position.X) + (this.width / 2), ((int) this.position.Y) + this.height, 1, 0);
                            Main.npc[num4].SetDefaults("Baby Slime");
                            Main.npc[num4].velocity.X = this.velocity.X * 2f;
                            Main.npc[num4].velocity.Y = this.velocity.Y;
                            Main.npc[num4].velocity.X += (Main.rand.Next(-20, 20) * 0.1f) + ((num3 * this.direction) * 0.3f);
                            Main.npc[num4].velocity.Y -= (Main.rand.Next(0, 10) * 0.1f) + num3;
                            Main.npc[num4].ai[1] = num3;
                            if ((Main.netMode == 2) && (num4 < 0x3e8))
                            {
                                NetMessage.SendData(0x17, -1, -1, "", num4, 0f, 0f, 0f);
                            }
                        }
                    }
                }
            }
            else if (this.type == 50)
            {
                if (this.life > 0)
                {
                    for (num = 0; num < ((dmg / ((double) this.lifeMax)) * 300.0); num++)
                    {
                        Dust.NewDust(this.position, this.width, this.height, 4, (float) hitDirection, -1f, 0xaf, new Color(0, 80, 0xff, 100), 1f);
                    }
                }
                else
                {
                    num = 0;
                    while (num < 200)
                    {
                        Dust.NewDust(this.position, this.width, this.height, 4, (float) (2 * hitDirection), -2f, 0xaf, new Color(0, 80, 0xff, 100), 1f);
                        num++;
                    }
                    if (Main.netMode != 1)
                    {
                        num2 = Main.rand.Next(4) + 4;
                        for (num3 = 0; num3 < num2; num3++)
                        {
                            int x = ((int) this.position.X) + Main.rand.Next(this.width - 0x20);
                            int y = ((int) this.position.Y) + Main.rand.Next(this.height - 0x20);
                            num4 = NewNPC(x, y, 1, 0);
                            Main.npc[num4].SetDefaults(1);
                            Main.npc[num4].velocity.X = Main.rand.Next(-15, 0x10) * 0.1f;
                            Main.npc[num4].velocity.Y = Main.rand.Next(-30, 1) * 0.1f;
                            Main.npc[num4].ai[1] = Main.rand.Next(3);
                            if ((Main.netMode == 2) && (num4 < 0x3e8))
                            {
                                NetMessage.SendData(0x17, -1, -1, "", num4, 0f, 0f, 0f);
                            }
                        }
                    }
                }
            }
            else
            {
                Color color;
                if ((this.type == 0x31) || (this.type == 0x33))
                {
                    if (this.life > 0)
                    {
                        for (num = 0; num < ((dmg / ((double) this.lifeMax)) * 30.0); num++)
                        {
                            color = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 5, (float) hitDirection, -1f, 0, color, 1f);
                        }
                    }
                    else
                    {
                        num = 0;
                        while (num < 15)
                        {
                            color = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 5, (float) (2 * hitDirection), -2f, 0, color, 1f);
                            num++;
                        }
                        if (this.type == 0x33)
                        {
                            Gore.NewGore(this.position, this.velocity, 0x53);
                        }
                        else
                        {
                            Gore.NewGore(this.position, this.velocity, 0x52);
                        }
                    }
                }
                else if ((this.type == 0x2e) || (this.type == 0x37))
                {
                    if (this.life > 0)
                    {
                        for (num = 0; num < ((dmg / ((double) this.lifeMax)) * 20.0); num++)
                        {
                            color = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 5, (float) hitDirection, -1f, 0, color, 1f);
                        }
                    }
                    else
                    {
                        num = 0;
                        while (num < 10)
                        {
                            color = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 5, (float) (2 * hitDirection), -2f, 0, color, 1f);
                            num++;
                        }
                        if (this.type == 0x2e)
                        {
                            Gore.NewGore(this.position, this.velocity, 0x4c);
                            Gore.NewGore(new Vector2(this.position.X, this.position.Y), this.velocity, 0x4d);
                        }
                    }
                }
                else if (((this.type == 0x2f) || (this.type == 0x39)) || (this.type == 0x3a))
                {
                    if (this.life > 0)
                    {
                        for (num = 0; num < ((dmg / ((double) this.lifeMax)) * 20.0); num++)
                        {
                            color = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 5, (float) hitDirection, -1f, 0, color, 1f);
                        }
                    }
                    else
                    {
                        num = 0;
                        while (num < 10)
                        {
                            color = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 5, (float) (2 * hitDirection), -2f, 0, color, 1f);
                            num++;
                        }
                        if (this.type == 0x39)
                        {
                            Gore.NewGore(new Vector2(this.position.X, this.position.Y), this.velocity, 0x54);
                        }
                        else if (this.type == 0x3a)
                        {
                            Gore.NewGore(new Vector2(this.position.X, this.position.Y), this.velocity, 0x55);
                        }
                        else
                        {
                            Gore.NewGore(this.position, this.velocity, 0x4e);
                            Gore.NewGore(new Vector2(this.position.X, this.position.Y), this.velocity, 0x4f);
                        }
                    }
                }
                else if (this.type == 2)
                {
                    if (this.life > 0)
                    {
                        for (num = 0; num < ((dmg / ((double) this.lifeMax)) * 100.0); num++)
                        {
                            color = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 5, (float) hitDirection, -1f, 0, color, 1f);
                        }
                    }
                    else
                    {
                        num = 0;
                        while (num < 50)
                        {
                            color = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 5, (float) (2 * hitDirection), -2f, 0, color, 1f);
                            num++;
                        }
                        Gore.NewGore(this.position, this.velocity, 1);
                        Gore.NewGore(new Vector2(this.position.X + 14f, this.position.Y), this.velocity, 2);
                    }
                }
                else if (((this.type == 3) || (this.type == 0x34)) || (this.type == 0x35))
                {
                    if (this.life > 0)
                    {
                        for (num = 0; num < ((dmg / ((double) this.lifeMax)) * 100.0); num++)
                        {
                            color = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 5, (float) hitDirection, -1f, 0, color, 1f);
                        }
                    }
                    else
                    {
                        num = 0;
                        while (num < 50)
                        {
                            color = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 5, 2.5f * hitDirection, -2.5f, 0, color, 1f);
                            num++;
                        }
                        Gore.NewGore(this.position, this.velocity, 3);
                        Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 4);
                        Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 4);
                        Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 5);
                        Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 5);
                    }
                }
                else if (this.type == 4)
                {
                    if (this.life > 0)
                    {
                        for (num = 0; num < ((dmg / ((double) this.lifeMax)) * 100.0); num++)
                        {
                            color = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 5, (float) hitDirection, -1f, 0, color, 1f);
                        }
                    }
                    else
                    {
                        num = 0;
                        while (num < 150)
                        {
                            color = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 5, (float) (2 * hitDirection), -2f, 0, color, 1f);
                            num++;
                        }
                        for (num = 0; num < 2; num++)
                        {
                            Gore.NewGore(this.position, new Vector2(Main.rand.Next(-30, 0x1f) * 0.2f, Main.rand.Next(-30, 0x1f) * 0.2f), 2);
                            Gore.NewGore(this.position, new Vector2(Main.rand.Next(-30, 0x1f) * 0.2f, Main.rand.Next(-30, 0x1f) * 0.2f), 7);
                            Gore.NewGore(this.position, new Vector2(Main.rand.Next(-30, 0x1f) * 0.2f, Main.rand.Next(-30, 0x1f) * 0.2f), 9);
                            Gore.NewGore(this.position, new Vector2(Main.rand.Next(-30, 0x1f) * 0.2f, Main.rand.Next(-30, 0x1f) * 0.2f), 10);
                        }
                        Main.PlaySound(15, (int) this.position.X, (int) this.position.Y, 0);
                    }
                }
                else if (this.type == 5)
                {
                    if (this.life > 0)
                    {
                        for (num = 0; num < ((dmg / ((double) this.lifeMax)) * 50.0); num++)
                        {
                            color = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 5, (float) hitDirection, -1f, 0, color, 1f);
                        }
                    }
                    else
                    {
                        num = 0;
                        while (num < 20)
                        {
                            color = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 5, (float) (2 * hitDirection), -2f, 0, color, 1f);
                            num++;
                        }
                        Gore.NewGore(this.position, this.velocity, 6);
                        Gore.NewGore(this.position, this.velocity, 7);
                    }
                }
                else
                {
                    int num7;
                    if (this.type == 6)
                    {
                        if (this.life > 0)
                        {
                            for (num = 0; num < ((dmg / ((double) this.lifeMax)) * 100.0); num++)
                            {
                                Dust.NewDust(this.position, this.width, this.height, 0x12, (float) hitDirection, -1f, this.alpha, this.color, this.scale);
                            }
                        }
                        else
                        {
                            num = 0;
                            while (num < 50)
                            {
                                Dust.NewDust(this.position, this.width, this.height, 0x12, (float) hitDirection, -2f, this.alpha, this.color, this.scale);
                                num++;
                            }
                            num7 = Gore.NewGore(this.position, this.velocity, 14);
                            Main.gore[num7].alpha = this.alpha;
                            num7 = Gore.NewGore(this.position, this.velocity, 15);
                            Main.gore[num7].alpha = this.alpha;
                        }
                    }
                    else if (((this.type == 7) || (this.type == 8)) || (this.type == 9))
                    {
                        if (this.life > 0)
                        {
                            for (num = 0; num < ((dmg / ((double) this.lifeMax)) * 100.0); num++)
                            {
                                Dust.NewDust(this.position, this.width, this.height, 0x12, (float) hitDirection, -1f, this.alpha, this.color, this.scale);
                            }
                        }
                        else
                        {
                            num = 0;
                            while (num < 50)
                            {
                                Dust.NewDust(this.position, this.width, this.height, 0x12, (float) hitDirection, -2f, this.alpha, this.color, this.scale);
                                num++;
                            }
                            num7 = Gore.NewGore(this.position, this.velocity, (this.type - 7) + 0x12);
                            Main.gore[num7].alpha = this.alpha;
                        }
                    }
                    else if (((this.type == 10) || (this.type == 11)) || (this.type == 12))
                    {
                        if (this.life > 0)
                        {
                            for (num = 0; num < ((dmg / ((double) this.lifeMax)) * 50.0); num++)
                            {
                                color = new Color();
                                Dust.NewDust(this.position, this.width, this.height, 5, (float) hitDirection, -1f, 0, color, 1f);
                            }
                        }
                        else
                        {
                            num = 0;
                            while (num < 10)
                            {
                                color = new Color();
                                Dust.NewDust(this.position, this.width, this.height, 5, 2.5f * hitDirection, -2.5f, 0, color, 1f);
                                num++;
                            }
                            Gore.NewGore(this.position, this.velocity, (this.type - 7) + 0x12);
                        }
                    }
                    else if (((this.type == 13) || (this.type == 14)) || (this.type == 15))
                    {
                        if (this.life > 0)
                        {
                            for (num = 0; num < ((dmg / ((double) this.lifeMax)) * 100.0); num++)
                            {
                                Dust.NewDust(this.position, this.width, this.height, 0x12, (float) hitDirection, -1f, this.alpha, this.color, this.scale);
                            }
                        }
                        else
                        {
                            num = 0;
                            while (num < 50)
                            {
                                Dust.NewDust(this.position, this.width, this.height, 0x12, (float) hitDirection, -2f, this.alpha, this.color, this.scale);
                                num++;
                            }
                            if (this.type == 13)
                            {
                                Gore.NewGore(this.position, this.velocity, 0x18);
                                Gore.NewGore(this.position, this.velocity, 0x19);
                            }
                            else if (this.type == 14)
                            {
                                Gore.NewGore(this.position, this.velocity, 0x1a);
                                Gore.NewGore(this.position, this.velocity, 0x1b);
                            }
                            else
                            {
                                Gore.NewGore(this.position, this.velocity, 0x1c);
                                Gore.NewGore(this.position, this.velocity, 0x1d);
                            }
                        }
                    }
                    else if (this.type == 0x11)
                    {
                        if (this.life > 0)
                        {
                            for (num = 0; num < ((dmg / ((double) this.lifeMax)) * 100.0); num++)
                            {
                                color = new Color();
                                Dust.NewDust(this.position, this.width, this.height, 5, (float) hitDirection, -1f, 0, color, 1f);
                            }
                        }
                        else
                        {
                            num = 0;
                            while (num < 50)
                            {
                                color = new Color();
                                Dust.NewDust(this.position, this.width, this.height, 5, 2.5f * hitDirection, -2.5f, 0, color, 1f);
                                num++;
                            }
                            Gore.NewGore(this.position, this.velocity, 30);
                            Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 0x1f);
                            Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 0x1f);
                            Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 0x20);
                            Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 0x20);
                        }
                    }
                    else if (this.type == 0x16)
                    {
                        if (this.life > 0)
                        {
                            for (num = 0; num < ((dmg / ((double) this.lifeMax)) * 100.0); num++)
                            {
                                color = new Color();
                                Dust.NewDust(this.position, this.width, this.height, 5, (float) hitDirection, -1f, 0, color, 1f);
                            }
                        }
                        else
                        {
                            num = 0;
                            while (num < 50)
                            {
                                color = new Color();
                                Dust.NewDust(this.position, this.width, this.height, 5, 2.5f * hitDirection, -2.5f, 0, color, 1f);
                                num++;
                            }
                            Gore.NewGore(this.position, this.velocity, 0x49);
                            Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 0x4a);
                            Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 0x4a);
                            Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 0x4b);
                            Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 0x4b);
                        }
                    }
                    else if ((this.type == 0x25) || (this.type == 0x36))
                    {
                        if (this.life > 0)
                        {
                            for (num = 0; num < ((dmg / ((double) this.lifeMax)) * 100.0); num++)
                            {
                                color = new Color();
                                Dust.NewDust(this.position, this.width, this.height, 5, (float) hitDirection, -1f, 0, color, 1f);
                            }
                        }
                        else
                        {
                            num = 0;
                            while (num < 50)
                            {
                                color = new Color();
                                Dust.NewDust(this.position, this.width, this.height, 5, 2.5f * hitDirection, -2.5f, 0, color, 1f);
                                num++;
                            }
                            Gore.NewGore(this.position, this.velocity, 0x3a);
                            Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 0x3b);
                            Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 0x3b);
                            Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 60);
                            Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 60);
                        }
                    }
                    else if (this.type == 0x12)
                    {
                        if (this.life > 0)
                        {
                            for (num = 0; num < ((dmg / ((double) this.lifeMax)) * 100.0); num++)
                            {
                                color = new Color();
                                Dust.NewDust(this.position, this.width, this.height, 5, (float) hitDirection, -1f, 0, color, 1f);
                            }
                        }
                        else
                        {
                            num = 0;
                            while (num < 50)
                            {
                                color = new Color();
                                Dust.NewDust(this.position, this.width, this.height, 5, 2.5f * hitDirection, -2.5f, 0, color, 1f);
                                num++;
                            }
                            Gore.NewGore(this.position, this.velocity, 0x21);
                            Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 0x22);
                            Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 0x22);
                            Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 0x23);
                            Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 0x23);
                        }
                    }
                    else if (this.type == 0x13)
                    {
                        if (this.life > 0)
                        {
                            for (num = 0; num < ((dmg / ((double) this.lifeMax)) * 100.0); num++)
                            {
                                color = new Color();
                                Dust.NewDust(this.position, this.width, this.height, 5, (float) hitDirection, -1f, 0, color, 1f);
                            }
                        }
                        else
                        {
                            num = 0;
                            while (num < 50)
                            {
                                color = new Color();
                                Dust.NewDust(this.position, this.width, this.height, 5, 2.5f * hitDirection, -2.5f, 0, color, 1f);
                                num++;
                            }
                            Gore.NewGore(this.position, this.velocity, 0x24);
                            Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 0x25);
                            Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 0x25);
                            Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 0x26);
                            Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 0x26);
                        }
                    }
                    else if (this.type == 0x26)
                    {
                        if (this.life > 0)
                        {
                            for (num = 0; num < ((dmg / ((double) this.lifeMax)) * 100.0); num++)
                            {
                                color = new Color();
                                Dust.NewDust(this.position, this.width, this.height, 5, (float) hitDirection, -1f, 0, color, 1f);
                            }
                        }
                        else
                        {
                            num = 0;
                            while (num < 50)
                            {
                                color = new Color();
                                Dust.NewDust(this.position, this.width, this.height, 5, 2.5f * hitDirection, -2.5f, 0, color, 1f);
                                num++;
                            }
                            Gore.NewGore(this.position, this.velocity, 0x40);
                            Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 0x41);
                            Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 0x41);
                            Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 0x42);
                            Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 0x42);
                        }
                    }
                    else if (this.type == 20)
                    {
                        if (this.life > 0)
                        {
                            for (num = 0; num < ((dmg / ((double) this.lifeMax)) * 100.0); num++)
                            {
                                color = new Color();
                                Dust.NewDust(this.position, this.width, this.height, 5, (float) hitDirection, -1f, 0, color, 1f);
                            }
                        }
                        else
                        {
                            num = 0;
                            while (num < 50)
                            {
                                color = new Color();
                                Dust.NewDust(this.position, this.width, this.height, 5, 2.5f * hitDirection, -2.5f, 0, color, 1f);
                                num++;
                            }
                            Gore.NewGore(this.position, this.velocity, 0x27);
                            Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 40);
                            Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 40);
                            Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 0x29);
                            Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 0x29);
                        }
                    }
                    else if ((((this.type == 0x15) || (this.type == 0x1f)) || ((this.type == 0x20) || (this.type == 0x2c))) || (this.type == 0x2d))
                    {
                        if (this.life > 0)
                        {
                            for (num = 0; num < ((dmg / ((double) this.lifeMax)) * 50.0); num++)
                            {
                                color = new Color();
                                Dust.NewDust(this.position, this.width, this.height, 0x1a, (float) hitDirection, -1f, 0, color, 1f);
                            }
                        }
                        else
                        {
                            num = 0;
                            while (num < 20)
                            {
                                color = new Color();
                                Dust.NewDust(this.position, this.width, this.height, 0x1a, 2.5f * hitDirection, -2.5f, 0, color, 1f);
                                num++;
                            }
                            Gore.NewGore(this.position, this.velocity, 0x2a);
                            Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 0x2b);
                            Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 0x2b);
                            Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 0x2c);
                            Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 0x2c);
                        }
                    }
                    else if (((this.type == 0x27) || (this.type == 40)) || (this.type == 0x29))
                    {
                        if (this.life > 0)
                        {
                            for (num = 0; num < ((dmg / ((double) this.lifeMax)) * 50.0); num++)
                            {
                                color = new Color();
                                Dust.NewDust(this.position, this.width, this.height, 0x1a, (float) hitDirection, -1f, 0, color, 1f);
                            }
                        }
                        else
                        {
                            num = 0;
                            while (num < 20)
                            {
                                color = new Color();
                                Dust.NewDust(this.position, this.width, this.height, 0x1a, 2.5f * hitDirection, -2.5f, 0, color, 1f);
                                num++;
                            }
                            Gore.NewGore(this.position, this.velocity, (this.type - 0x27) + 0x43);
                        }
                    }
                    else
                    {
                        int num8;
                        if (this.type == 0x22)
                        {
                            if (this.life > 0)
                            {
                                for (num = 0; num < ((dmg / ((double) this.lifeMax)) * 50.0); num++)
                                {
                                    color = new Color();
                                    num8 = Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 6, -this.velocity.X * 0.2f, -this.velocity.Y * 0.2f, 100, color, 3f);
                                    Main.dust[num8].noLight = true;
                                    Main.dust[num8].noGravity = true;
                                    Dust dust1 = Main.dust[num8];
                                    dust1.velocity = (Vector2) (dust1.velocity * 2f);
                                    color = new Color();
                                    num8 = Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 6, -this.velocity.X * 0.2f, -this.velocity.Y * 0.2f, 100, color, 2f);
                                    Main.dust[num8].noLight = true;
                                    Dust dust2 = Main.dust[num8];
                                    dust2.velocity = (Vector2) (dust2.velocity * 2f);
                                }
                            }
                            else
                            {
                                num = 0;
                                while (num < 20)
                                {
                                    color = new Color();
                                    num8 = Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 6, -this.velocity.X * 0.2f, -this.velocity.Y * 0.2f, 100, color, 3f);
                                    Main.dust[num8].noLight = true;
                                    Main.dust[num8].noGravity = true;
                                    Dust dust3 = Main.dust[num8];
                                    dust3.velocity = (Vector2) (dust3.velocity * 2f);
                                    color = new Color();
                                    num8 = Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 6, -this.velocity.X * 0.2f, -this.velocity.Y * 0.2f, 100, color, 2f);
                                    Main.dust[num8].noLight = true;
                                    Dust dust4 = Main.dust[num8];
                                    dust4.velocity = (Vector2) (dust4.velocity * 2f);
                                    num++;
                                }
                            }
                        }
                        else if ((this.type == 0x23) || (this.type == 0x24))
                        {
                            if (this.life > 0)
                            {
                                for (num = 0; num < ((dmg / ((double) this.lifeMax)) * 100.0); num++)
                                {
                                    color = new Color();
                                    Dust.NewDust(this.position, this.width, this.height, 0x1a, (float) hitDirection, -1f, 0, color, 1f);
                                }
                            }
                            else
                            {
                                num = 0;
                                while (num < 150)
                                {
                                    color = new Color();
                                    Dust.NewDust(this.position, this.width, this.height, 0x1a, 2.5f * hitDirection, -2.5f, 0, color, 1f);
                                    num++;
                                }
                                if (this.type == 0x23)
                                {
                                    Gore.NewGore(this.position, this.velocity, 0x36);
                                    Gore.NewGore(this.position, this.velocity, 0x37);
                                }
                                else
                                {
                                    Gore.NewGore(this.position, this.velocity, 0x38);
                                    Gore.NewGore(this.position, this.velocity, 0x39);
                                    Gore.NewGore(this.position, this.velocity, 0x39);
                                    Gore.NewGore(this.position, this.velocity, 0x39);
                                }
                            }
                        }
                        else if (this.type == 0x17)
                        {
                            int num9;
                            if (this.life > 0)
                            {
                                for (num = 0; num < ((dmg / ((double) this.lifeMax)) * 100.0); num++)
                                {
                                    num9 = 0x19;
                                    if (Main.rand.Next(2) == 0)
                                    {
                                        num9 = 6;
                                    }
                                    color = new Color();
                                    Dust.NewDust(this.position, this.width, this.height, num9, (float) hitDirection, -1f, 0, color, 1f);
                                    color = new Color();
                                    num8 = Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 6, this.velocity.X * 0.2f, this.velocity.Y * 0.2f, 100, color, 2f);
                                    Main.dust[num8].noGravity = true;
                                }
                            }
                            else
                            {
                                num = 0;
                                while (num < 50)
                                {
                                    num9 = 0x19;
                                    if (Main.rand.Next(2) == 0)
                                    {
                                        num9 = 6;
                                    }
                                    color = new Color();
                                    Dust.NewDust(this.position, this.width, this.height, num9, (float) (2 * hitDirection), -2f, 0, color, 1f);
                                    num++;
                                }
                                for (num = 0; num < 50; num++)
                                {
                                    color = new Color();
                                    num8 = Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 6, this.velocity.X * 0.2f, this.velocity.Y * 0.2f, 100, color, 2.5f);
                                    Dust dust5 = Main.dust[num8];
                                    dust5.velocity = (Vector2) (dust5.velocity * 6f);
                                    Main.dust[num8].noGravity = true;
                                }
                            }
                        }
                        else if (this.type == 0x18)
                        {
                            if (this.life > 0)
                            {
                                for (num = 0; num < ((dmg / ((double) this.lifeMax)) * 100.0); num++)
                                {
                                    color = new Color();
                                    num8 = Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 6, this.velocity.X, this.velocity.Y, 100, color, 2.5f);
                                    Main.dust[num8].noGravity = true;
                                }
                            }
                            else
                            {
                                num = 0;
                                while (num < 50)
                                {
                                    color = new Color();
                                    num8 = Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 6, this.velocity.X, this.velocity.Y, 100, color, 2.5f);
                                    Main.dust[num8].noGravity = true;
                                    Dust dust6 = Main.dust[num8];
                                    dust6.velocity = (Vector2) (dust6.velocity * 2f);
                                    num++;
                                }
                                Gore.NewGore(this.position, this.velocity, 0x2d);
                                Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 0x2e);
                                Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 0x2e);
                                Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 0x2f);
                                Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 0x2f);
                            }
                        }
                        else
                        {
                            int num10;
                            if (this.type == 0x19)
                            {
                                Main.PlaySound(2, (int) this.position.X, (int) this.position.Y, 10);
                                for (num10 = 0; num10 < 20; num10++)
                                {
                                    color = new Color();
                                    num8 = Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 6, -this.velocity.X * 0.2f, -this.velocity.Y * 0.2f, 100, color, 2f);
                                    Main.dust[num8].noGravity = true;
                                    Dust dust7 = Main.dust[num8];
                                    dust7.velocity = (Vector2) (dust7.velocity * 2f);
                                    color = new Color();
                                    num8 = Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 6, -this.velocity.X * 0.2f, -this.velocity.Y * 0.2f, 100, color, 1f);
                                    Dust dust8 = Main.dust[num8];
                                    dust8.velocity = (Vector2) (dust8.velocity * 2f);
                                }
                            }
                            else if (this.type == 0x21)
                            {
                                Main.PlaySound(2, (int) this.position.X, (int) this.position.Y, 10);
                                for (num10 = 0; num10 < 20; num10++)
                                {
                                    color = new Color();
                                    num8 = Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 0x1d, -this.velocity.X * 0.2f, -this.velocity.Y * 0.2f, 100, color, 2f);
                                    Main.dust[num8].noGravity = true;
                                    Dust dust9 = Main.dust[num8];
                                    dust9.velocity = (Vector2) (dust9.velocity * 2f);
                                    color = new Color();
                                    num8 = Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 0x1d, -this.velocity.X * 0.2f, -this.velocity.Y * 0.2f, 100, color, 1f);
                                    Dust dust10 = Main.dust[num8];
                                    dust10.velocity = (Vector2) (dust10.velocity * 2f);
                                }
                            }
                            else if ((((this.type == 0x1a) || (this.type == 0x1b)) || (this.type == 0x1c)) || (this.type == 0x1d))
                            {
                                if (this.life > 0)
                                {
                                    for (num = 0; num < ((dmg / ((double) this.lifeMax)) * 100.0); num++)
                                    {
                                        color = new Color();
                                        Dust.NewDust(this.position, this.width, this.height, 5, (float) hitDirection, -1f, 0, color, 1f);
                                    }
                                }
                                else
                                {
                                    num = 0;
                                    while (num < 50)
                                    {
                                        color = new Color();
                                        Dust.NewDust(this.position, this.width, this.height, 5, 2.5f * hitDirection, -2.5f, 0, color, 1f);
                                        num++;
                                    }
                                    Gore.NewGore(this.position, this.velocity, 0x30);
                                    Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 0x31);
                                    Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 0x31);
                                    Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 50);
                                    Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 50);
                                }
                            }
                            else if (this.type == 30)
                            {
                                Main.PlaySound(2, (int) this.position.X, (int) this.position.Y, 10);
                                for (num10 = 0; num10 < 20; num10++)
                                {
                                    color = new Color();
                                    num8 = Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 0x1b, -this.velocity.X * 0.2f, -this.velocity.Y * 0.2f, 100, color, 2f);
                                    Main.dust[num8].noGravity = true;
                                    Dust dust11 = Main.dust[num8];
                                    dust11.velocity = (Vector2) (dust11.velocity * 2f);
                                    color = new Color();
                                    num8 = Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 0x1b, -this.velocity.X * 0.2f, -this.velocity.Y * 0.2f, 100, color, 1f);
                                    Dust dust12 = Main.dust[num8];
                                    dust12.velocity = (Vector2) (dust12.velocity * 2f);
                                }
                            }
                            else if (this.type == 0x2a)
                            {
                                if (this.life > 0)
                                {
                                    for (num = 0; num < ((dmg / ((double) this.lifeMax)) * 100.0); num++)
                                    {
                                        Dust.NewDust(this.position, this.width, this.height, 0x12, (float) hitDirection, -1f, this.alpha, this.color, this.scale);
                                    }
                                }
                                else
                                {
                                    num = 0;
                                    while (num < 50)
                                    {
                                        Dust.NewDust(this.position, this.width, this.height, 0x12, (float) hitDirection, -2f, this.alpha, this.color, this.scale);
                                        num++;
                                    }
                                    Gore.NewGore(this.position, this.velocity, 70);
                                    Gore.NewGore(this.position, this.velocity, 0x47);
                                }
                            }
                            else if ((this.type == 0x2b) || (this.type == 0x38))
                            {
                                if (this.life > 0)
                                {
                                    for (num = 0; num < ((dmg / ((double) this.lifeMax)) * 100.0); num++)
                                    {
                                        Dust.NewDust(this.position, this.width, this.height, 40, (float) hitDirection, -1f, this.alpha, this.color, 1.2f);
                                    }
                                }
                                else
                                {
                                    num = 0;
                                    while (num < 50)
                                    {
                                        Dust.NewDust(this.position, this.width, this.height, 40, (float) hitDirection, -2f, this.alpha, this.color, 1.2f);
                                        num++;
                                    }
                                    Gore.NewGore(this.position, this.velocity, 0x48);
                                    Gore.NewGore(this.position, this.velocity, 0x48);
                                }
                            }
                            else if (this.type == 0x30)
                            {
                                if (this.life > 0)
                                {
                                    for (num = 0; num < ((dmg / ((double) this.lifeMax)) * 100.0); num++)
                                    {
                                        color = new Color();
                                        Dust.NewDust(this.position, this.width, this.height, 5, (float) hitDirection, -1f, 0, color, 1f);
                                    }
                                }
                                else
                                {
                                    for (num = 0; num < 50; num++)
                                    {
                                        color = new Color();
                                        Dust.NewDust(this.position, this.width, this.height, 5, (float) (2 * hitDirection), -2f, 0, color, 1f);
                                    }
                                    Gore.NewGore(this.position, this.velocity, 80);
                                    Gore.NewGore(this.position, this.velocity, 0x51);
                                }
                            }
                        }
                    }
                }
            }
        }

        public static int NewNPC(int X, int Y, int Type, int Start = 0)
        {
            int index = -1;
            for (int i = Start; i < 0x3e8; i++)
            {
                if (!Main.npc[i].active)
                {
                    index = i;
                    break;
                }
            }
            if (index >= 0)
            {
                Main.npc[index] = new NPC();
                Main.npc[index].SetDefaults(Type);
                Main.npc[index].position.X = X - (Main.npc[index].width / 2);
                Main.npc[index].position.Y = Y - Main.npc[index].height;
                Main.npc[index].active = true;
                Main.npc[index].timeLeft = (int) (activeTime * 1.25);
                Main.npc[index].wet = Collision.WetCollision(Main.npc[index].position, Main.npc[index].width, Main.npc[index].height);
                if (Type == 50)
                {
                    if (Main.netMode == 0)
                    {
                        Main.NewText(Main.npc[index].name + " has awoken!", 0xaf, 0x4b, 0xff);
                        return index;
                    }
                    if (Main.netMode == 2)
                    {
                        NetMessage.SendData(0x19, -1, -1, Main.npc[index].name + " has awoken!", 0xff, 175f, 75f, 255f);
                    }
                }
                return index;
            }
            return 0x3e8;
        }

        public void NPCLoot()
        {
            int num;
            if ((this.type == 1) || (this.type == 0x10))
            {
                Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x17, Main.rand.Next(1, 3), false);
            }
            if (this.type == 2)
            {
                if (Main.rand.Next(3) == 0)
                {
                    Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x26, 1, false);
                }
                else if (Main.rand.Next(100) == 0)
                {
                    Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0xec, 1, false);
                }
            }
            if (this.type == 0x3a)
            {
                if (Main.rand.Next(500) == 0)
                {
                    Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x107, 1, false);
                }
                else if (Main.rand.Next(40) == 0)
                {
                    Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x76, 1, false);
                }
            }
            if ((this.type == 3) && (Main.rand.Next(50) == 0))
            {
                Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0xd8, 1, false);
            }
            if (this.type == 0x34)
            {
                Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0xfb, 1, false);
            }
            if (this.type == 0x35)
            {
                Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0xef, 1, false);
            }
            if (this.type == 0x36)
            {
                Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 260, 1, false);
            }
            if (this.type == 0x37)
            {
                Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x105, 1, false);
            }
            if (this.type == 4)
            {
                num = Main.rand.Next(30) + 20;
                Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x2f, num, false);
                num = Main.rand.Next(20) + 10;
                Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x38, num, false);
                num = Main.rand.Next(20) + 10;
                Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x38, num, false);
                num = Main.rand.Next(20) + 10;
                Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x38, num, false);
                num = Main.rand.Next(3) + 1;
                Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x3b, num, false);
            }
            if ((this.type == 6) && (Main.rand.Next(3) == 0))
            {
                Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x44, 1, false);
            }
            if (((this.type == 7) || (this.type == 8)) || (this.type == 9))
            {
                if (Main.rand.Next(3) == 0)
                {
                    Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x44, Main.rand.Next(1, 3), false);
                }
                Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x45, Main.rand.Next(3, 9), false);
            }
            if ((((this.type == 10) || (this.type == 11)) || (this.type == 12)) && (Main.rand.Next(500) == 0))
            {
                Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0xd7, 1, false);
            }
            if ((this.type == 0x2f) && (Main.rand.Next(0x4b) == 0))
            {
                Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0xf3, 1, false);
            }
            if (((this.type == 0x27) || (this.type == 40)) || (this.type == 0x29))
            {
                if (Main.rand.Next(100) == 0)
                {
                    Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 220, 1, false);
                }
                else if (Main.rand.Next(100) == 0)
                {
                    Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0xda, 1, false);
                }
            }
            if (((this.type == 13) || (this.type == 14)) || (this.type == 15))
            {
                num = Main.rand.Next(1, 4);
                Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x56, num, false);
                if (Main.rand.Next(2) == 0)
                {
                    num = Main.rand.Next(2, 6);
                    Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x38, num, false);
                }
                if (this.boss)
                {
                    num = Main.rand.Next(15, 30);
                    Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x38, num, false);
                    num = Main.rand.Next(15, 0x1f);
                    Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x38, num, false);
                    int type = Main.rand.Next(100, 0x67);
                    Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, type, 1, false);
                }
            }
            if ((this.type == 0x15) || (this.type == 0x2c))
            {
                if (Main.rand.Next(0x19) == 0)
                {
                    Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x76, 1, false);
                }
                else if (this.type == 0x2c)
                {
                    Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0xa6, Main.rand.Next(1, 4), false);
                }
            }
            if (this.type == 0x2d)
            {
                Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0xee, 1, false);
            }
            if (this.type == 50)
            {
                Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, Main.rand.Next(0x100, 0x103), 1, false);
            }
            if ((this.type == 0x17) && (Main.rand.Next(50) == 0))
            {
                Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x74, 1, false);
            }
            if (this.type == 0x18)
            {
                if (Main.rand.Next(50) == 0)
                {
                    Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x70, 1, false);
                }
                else if (Main.rand.Next(500) == 0)
                {
                    Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0xf4, 1, false);
                }
            }
            if ((this.type == 0x1f) || (this.type == 0x20))
            {
                Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x9a, 1, false);
            }
            if ((((this.type == 0x1a) || (this.type == 0x1b)) || (this.type == 0x1c)) || (this.type == 0x1d))
            {
                if (Main.rand.Next(400) == 0)
                {
                    Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x80, 1, false);
                }
                else if (Main.rand.Next(200) == 0)
                {
                    Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 160, 1, false);
                }
                else if (Main.rand.Next(2) == 0)
                {
                    num = Main.rand.Next(1, 6);
                    Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0xa1, num, false);
                }
            }
            if (this.type == 0x2a)
            {
                Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0xd1, 1, false);
            }
            if ((this.type == 0x2b) && (Main.rand.Next(5) == 0))
            {
                Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 210, 1, false);
            }
            if (((this.type == 0x2a) || (this.type == 0x2b)) && (Main.rand.Next(150) == 0))
            {
                Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, Main.rand.Next(0xe4, 0xe7), 1, false);
            }
            if (this.boss)
            {
                if (this.type == 4)
                {
                    downedBoss1 = true;
                }
                if (((this.type == 13) || (this.type == 14)) || (this.type == 15))
                {
                    downedBoss2 = true;
                    this.name = "Eater of Worlds";
                }
                if (this.type == 0x23)
                {
                    downedBoss3 = true;
                    this.name = "Skeletron";
                }
                num = Main.rand.Next(5, 0x10);
                Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x1c, num, false);
                int num3 = Main.rand.Next(5) + 5;
                for (int i = 0; i < num3; i++)
                {
                    Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x3a, 1, false);
                }
                if (Main.netMode == 0)
                {
                    Main.NewText(this.name + " has been defeated!", 0xaf, 0x4b, 0xff);
                }
                else if (Main.netMode == 2)
                {
                    NetMessage.SendData(0x19, -1, -1, this.name + " has been defeated!", 0xff, 175f, 75f, 255f);
                }
            }
            if ((Main.rand.Next(7) == 0) && (this.lifeMax > 1))
            {
                if ((Main.rand.Next(2) == 0) && (Main.player[Player.FindClosest(this.position, this.width, this.height)].statMana < Main.player[Player.FindClosest(this.position, this.width, this.height)].statManaMax))
                {
                    Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0xb8, 1, false);
                }
                else if ((Main.rand.Next(2) == 0) && (Main.player[Player.FindClosest(this.position, this.width, this.height)].statLife < Main.player[Player.FindClosest(this.position, this.width, this.height)].statLifeMax))
                {
                    Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x3a, 1, false);
                }
            }
            float num5 = this.value * (1f + (Main.rand.Next(-20, 0x15) * 0.01f));
            if (Main.rand.Next(5) == 0)
            {
                num5 *= 1f + (Main.rand.Next(5, 11) * 0.01f);
            }
            if (Main.rand.Next(10) == 0)
            {
                num5 *= 1f + (Main.rand.Next(10, 0x15) * 0.01f);
            }
            if (Main.rand.Next(15) == 0)
            {
                num5 *= 1f + (Main.rand.Next(15, 0x1f) * 0.01f);
            }
            if (Main.rand.Next(20) == 0)
            {
                num5 *= 1f + (Main.rand.Next(20, 0x29) * 0.01f);
            }
            while (((int) num5) > 0)
            {
                if (num5 > 1000000f)
                {
                    num = (int) (num5 / 1000000f);
                    if ((num > 50) && (Main.rand.Next(2) == 0))
                    {
                        num /= Main.rand.Next(3) + 1;
                    }
                    if (Main.rand.Next(2) == 0)
                    {
                        num /= Main.rand.Next(3) + 1;
                    }
                    num5 -= 0xf4240 * num;
                    Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x4a, num, false);
                }
                else if (num5 > 10000f)
                {
                    num = (int) (num5 / 10000f);
                    if ((num > 50) && (Main.rand.Next(2) == 0))
                    {
                        num /= Main.rand.Next(3) + 1;
                    }
                    if (Main.rand.Next(2) == 0)
                    {
                        num /= Main.rand.Next(3) + 1;
                    }
                    num5 -= 0x2710 * num;
                    Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x49, num, false);
                }
                else if (num5 > 100f)
                {
                    num = (int) (num5 / 100f);
                    if ((num > 50) && (Main.rand.Next(2) == 0))
                    {
                        num /= Main.rand.Next(3) + 1;
                    }
                    if (Main.rand.Next(2) == 0)
                    {
                        num /= Main.rand.Next(3) + 1;
                    }
                    num5 -= 100 * num;
                    Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x48, num, false);
                }
                else
                {
                    num = (int) num5;
                    if ((num > 50) && (Main.rand.Next(2) == 0))
                    {
                        num /= Main.rand.Next(3) + 1;
                    }
                    if (Main.rand.Next(2) == 0)
                    {
                        num /= Main.rand.Next(4) + 1;
                    }
                    if (num < 1)
                    {
                        num = 1;
                    }
                    num5 -= num;
                    Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x47, num, false);
                }
            }
        }

        public void SetDefaults(int Type)
        {
            this.lavaWet = false;
            this.wetCount = 0;
            this.wet = false;
            this.townNPC = false;
            this.homeless = false;
            this.homeTileX = -1;
            this.homeTileY = -1;
            this.friendly = false;
            this.behindTiles = false;
            this.boss = false;
            this.noTileCollide = false;
            this.rotation = 0f;
            this.active = true;
            this.alpha = 0;
            this.color = new Color();
            this.collideX = false;
            this.collideY = false;
            this.direction = 0;
            this.oldDirection = this.direction;
            this.frameCounter = 0.0;
            this.netUpdate = false;
            this.knockBackResist = 1f;
            this.name = "";
            this.noGravity = false;
            this.scale = 1f;
            this.soundHit = 0;
            this.soundKilled = 0;
            this.spriteDirection = -1;
            this.target = 0xff;
            this.oldTarget = this.target;
            this.targetRect = new Rectangle();
            this.timeLeft = activeTime;
            this.type = Type;
            this.value = 0f;
            for (int i = 0; i < maxAI; i++)
            {
                this.ai[i] = 0f;
            }
            if (this.type == 1)
            {
                this.name = "Blue Slime";
                this.width = 0x18;
                this.height = 0x12;
                this.aiStyle = 1;
                this.damage = 7;
                this.defense = 2;
                this.lifeMax = 0x19;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.alpha = 0xaf;
                this.color = new Color(0, 80, 0xff, 100);
                this.value = 25f;
            }
            if (this.type == 2)
            {
                this.name = "Demon Eye";
                this.width = 30;
                this.height = 0x20;
                this.aiStyle = 2;
                this.damage = 0x12;
                this.defense = 2;
                this.lifeMax = 60;
                this.soundHit = 1;
                this.knockBackResist = 0.8f;
                this.soundKilled = 1;
                this.value = 75f;
            }
            if (this.type == 3)
            {
                this.name = "Zombie";
                this.width = 0x12;
                this.height = 40;
                this.aiStyle = 3;
                this.damage = 14;
                this.defense = 6;
                this.lifeMax = 0x2d;
                this.soundHit = 1;
                this.soundKilled = 2;
                this.knockBackResist = 0.5f;
                this.value = 60f;
            }
            if (this.type == 4)
            {
                this.name = "Eye of Cthulhu";
                this.width = 100;
                this.height = 110;
                this.aiStyle = 4;
                this.damage = 0x12;
                this.defense = 12;
                this.lifeMax = 0xbb8;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.knockBackResist = 0f;
                this.noGravity = true;
                this.noTileCollide = true;
                this.timeLeft = activeTime * 30;
                this.boss = true;
                this.value = 30000f;
            }
            if (this.type == 5)
            {
                this.name = "Servant of Cthulhu";
                this.width = 20;
                this.height = 20;
                this.aiStyle = 5;
                this.damage = 0x17;
                this.defense = 0;
                this.lifeMax = 8;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.noGravity = true;
                this.noTileCollide = true;
            }
            if (this.type == 6)
            {
                this.name = "Eater of Souls";
                this.width = 30;
                this.height = 30;
                this.aiStyle = 5;
                this.damage = 15;
                this.defense = 8;
                this.lifeMax = 40;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.noGravity = true;
                this.knockBackResist = 0.5f;
                this.value = 90f;
            }
            if (this.type == 7)
            {
                this.name = "Devourer Head";
                this.width = 0x16;
                this.height = 0x16;
                this.aiStyle = 6;
                this.damage = 0x1c;
                this.defense = 2;
                this.lifeMax = 40;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.noGravity = true;
                this.noTileCollide = true;
                this.knockBackResist = 0f;
                this.behindTiles = true;
                this.value = 300f;
            }
            if (this.type == 8)
            {
                this.name = "Devourer Body";
                this.width = 0x16;
                this.height = 0x16;
                this.aiStyle = 6;
                this.damage = 0x12;
                this.defense = 6;
                this.lifeMax = 60;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.noGravity = true;
                this.noTileCollide = true;
                this.knockBackResist = 0f;
                this.behindTiles = true;
                this.value = 300f;
            }
            if (this.type == 9)
            {
                this.name = "Devourer Tail";
                this.width = 0x16;
                this.height = 0x16;
                this.aiStyle = 6;
                this.damage = 13;
                this.defense = 10;
                this.lifeMax = 100;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.noGravity = true;
                this.noTileCollide = true;
                this.knockBackResist = 0f;
                this.behindTiles = true;
                this.value = 300f;
            }
            if (this.type == 10)
            {
                this.name = "Giant Worm Head";
                this.width = 14;
                this.height = 14;
                this.aiStyle = 6;
                this.damage = 8;
                this.defense = 0;
                this.lifeMax = 10;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.noGravity = true;
                this.noTileCollide = true;
                this.knockBackResist = 0f;
                this.behindTiles = true;
                this.value = 200f;
            }
            if (this.type == 11)
            {
                this.name = "Giant Worm Body";
                this.width = 14;
                this.height = 14;
                this.aiStyle = 6;
                this.damage = 4;
                this.defense = 4;
                this.lifeMax = 15;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.noGravity = true;
                this.noTileCollide = true;
                this.knockBackResist = 0f;
                this.behindTiles = true;
                this.value = 200f;
            }
            if (this.type == 12)
            {
                this.name = "Giant Worm Tail";
                this.width = 14;
                this.height = 14;
                this.aiStyle = 6;
                this.damage = 4;
                this.defense = 6;
                this.lifeMax = 20;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.noGravity = true;
                this.noTileCollide = true;
                this.knockBackResist = 0f;
                this.behindTiles = true;
                this.value = 200f;
            }
            if (this.type == 13)
            {
                this.name = "Eater of Worlds Head";
                this.width = 0x26;
                this.height = 0x26;
                this.aiStyle = 6;
                this.damage = 50;
                this.defense = 2;
                this.lifeMax = 140;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.noGravity = true;
                this.noTileCollide = true;
                this.knockBackResist = 0f;
                this.behindTiles = true;
                this.value = 300f;
            }
            if (this.type == 14)
            {
                this.name = "Eater of Worlds Body";
                this.width = 0x26;
                this.height = 0x26;
                this.aiStyle = 6;
                this.damage = 0x19;
                this.defense = 7;
                this.lifeMax = 230;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.noGravity = true;
                this.noTileCollide = true;
                this.knockBackResist = 0f;
                this.behindTiles = true;
                this.value = 300f;
            }
            if (this.type == 15)
            {
                this.name = "Eater of Worlds Tail";
                this.width = 0x26;
                this.height = 0x26;
                this.aiStyle = 6;
                this.damage = 15;
                this.defense = 10;
                this.lifeMax = 350;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.noGravity = true;
                this.noTileCollide = true;
                this.knockBackResist = 0f;
                this.behindTiles = true;
                this.value = 300f;
            }
            if (this.type == 0x10)
            {
                this.name = "Mother Slime";
                this.width = 0x24;
                this.height = 0x18;
                this.aiStyle = 1;
                this.damage = 20;
                this.defense = 7;
                this.lifeMax = 90;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.alpha = 120;
                this.color = new Color(0, 0, 0, 50);
                this.value = 75f;
                this.scale = 1.25f;
                this.knockBackResist = 0.6f;
            }
            if (this.type == 0x11)
            {
                this.townNPC = true;
                this.friendly = true;
                this.name = "Merchant";
                this.width = 0x12;
                this.height = 40;
                this.aiStyle = 7;
                this.damage = 10;
                this.defense = 15;
                this.lifeMax = 250;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.knockBackResist = 0.5f;
            }
            if (this.type == 0x12)
            {
                this.townNPC = true;
                this.friendly = true;
                this.name = "Nurse";
                this.width = 0x12;
                this.height = 40;
                this.aiStyle = 7;
                this.damage = 10;
                this.defense = 15;
                this.lifeMax = 250;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.knockBackResist = 0.5f;
            }
            if (this.type == 0x13)
            {
                this.townNPC = true;
                this.friendly = true;
                this.name = "Arms Dealer";
                this.width = 0x12;
                this.height = 40;
                this.aiStyle = 7;
                this.damage = 10;
                this.defense = 15;
                this.lifeMax = 250;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.knockBackResist = 0.5f;
            }
            if (this.type == 20)
            {
                this.townNPC = true;
                this.friendly = true;
                this.name = "Dryad";
                this.width = 0x12;
                this.height = 40;
                this.aiStyle = 7;
                this.damage = 10;
                this.defense = 15;
                this.lifeMax = 250;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.knockBackResist = 0.5f;
            }
            if (this.type == 0x15)
            {
                this.name = "Skeleton";
                this.width = 0x12;
                this.height = 40;
                this.aiStyle = 3;
                this.damage = 20;
                this.defense = 8;
                this.lifeMax = 60;
                this.soundHit = 2;
                this.soundKilled = 2;
                this.knockBackResist = 0.5f;
                this.value = 250f;
            }
            if (this.type == 0x16)
            {
                this.townNPC = true;
                this.friendly = true;
                this.name = "Guide";
                this.width = 0x12;
                this.height = 40;
                this.aiStyle = 7;
                this.damage = 10;
                this.defense = 15;
                this.lifeMax = 250;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.knockBackResist = 0.5f;
            }
            if (this.type == 0x17)
            {
                this.name = "Meteor Head";
                this.width = 0x16;
                this.height = 0x16;
                this.aiStyle = 5;
                this.damage = 0x19;
                this.defense = 10;
                this.lifeMax = 50;
                this.soundHit = 3;
                this.soundKilled = 3;
                this.noGravity = true;
                this.noTileCollide = true;
                this.value = 300f;
                this.knockBackResist = 0.8f;
            }
            else if (this.type == 0x18)
            {
                this.name = "Fire Imp";
                this.width = 0x12;
                this.height = 40;
                this.aiStyle = 8;
                this.damage = 30;
                this.defense = 20;
                this.lifeMax = 80;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.knockBackResist = 0.5f;
                this.value = 800f;
            }
            if (this.type == 0x19)
            {
                this.name = "Burning Sphere";
                this.width = 0x10;
                this.height = 0x10;
                this.aiStyle = 9;
                this.damage = 0x19;
                this.defense = 0;
                this.lifeMax = 1;
                this.soundHit = 3;
                this.soundKilled = 3;
                this.noGravity = true;
                this.noTileCollide = true;
                this.knockBackResist = 0f;
                this.alpha = 100;
            }
            if (this.type == 0x1a)
            {
                this.name = "Goblin Peon";
                this.width = 0x12;
                this.height = 40;
                this.aiStyle = 3;
                this.damage = 12;
                this.defense = 4;
                this.lifeMax = 60;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.knockBackResist = 0.8f;
                this.value = 250f;
            }
            if (this.type == 0x1b)
            {
                this.name = "Goblin Thief";
                this.width = 0x12;
                this.height = 40;
                this.aiStyle = 3;
                this.damage = 20;
                this.defense = 6;
                this.lifeMax = 80;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.knockBackResist = 0.7f;
                this.value = 600f;
            }
            if (this.type == 0x1c)
            {
                this.name = "Goblin Warrior";
                this.width = 0x12;
                this.height = 40;
                this.aiStyle = 3;
                this.damage = 0x19;
                this.defense = 8;
                this.lifeMax = 110;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.knockBackResist = 0.5f;
                this.value = 500f;
            }
            else if (this.type == 0x1d)
            {
                this.name = "Goblin Sorcerer";
                this.width = 0x12;
                this.height = 40;
                this.aiStyle = 8;
                this.damage = 20;
                this.defense = 2;
                this.lifeMax = 40;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.knockBackResist = 0.6f;
                this.value = 800f;
            }
            else if (this.type == 30)
            {
                this.name = "Chaos Ball";
                this.width = 0x10;
                this.height = 0x10;
                this.aiStyle = 9;
                this.damage = 20;
                this.defense = 0;
                this.lifeMax = 1;
                this.soundHit = 3;
                this.soundKilled = 3;
                this.noGravity = true;
                this.noTileCollide = true;
                this.alpha = 100;
                this.knockBackResist = 0f;
            }
            else if (this.type == 0x1f)
            {
                this.name = "Angry Bones";
                this.width = 0x12;
                this.height = 40;
                this.aiStyle = 3;
                this.damage = 30;
                this.defense = 10;
                this.lifeMax = 100;
                this.soundHit = 2;
                this.soundKilled = 2;
                this.knockBackResist = 0.7f;
                this.value = 500f;
            }
            else if (this.type == 0x20)
            {
                this.name = "Dark Caster";
                this.width = 0x12;
                this.height = 40;
                this.aiStyle = 8;
                this.damage = 20;
                this.defense = 4;
                this.lifeMax = 50;
                this.soundHit = 2;
                this.soundKilled = 2;
                this.knockBackResist = 0.6f;
                this.value = 800f;
            }
            else if (this.type == 0x21)
            {
                this.name = "Water Sphere";
                this.width = 0x10;
                this.height = 0x10;
                this.aiStyle = 9;
                this.damage = 20;
                this.defense = 0;
                this.lifeMax = 1;
                this.soundHit = 3;
                this.soundKilled = 3;
                this.noGravity = true;
                this.noTileCollide = true;
                this.alpha = 100;
                this.knockBackResist = 0f;
            }
            if (this.type == 0x22)
            {
                this.name = "Burning Skull";
                this.width = 0x1a;
                this.height = 0x1c;
                this.aiStyle = 10;
                this.damage = 0x19;
                this.defense = 30;
                this.lifeMax = 30;
                this.soundHit = 2;
                this.soundKilled = 2;
                this.noGravity = true;
                this.value = 300f;
                this.knockBackResist = 1.2f;
            }
            if (this.type == 0x23)
            {
                this.name = "Skeletron Head";
                this.width = 80;
                this.height = 0x66;
                this.aiStyle = 11;
                this.damage = 0x23;
                this.defense = 12;
                this.lifeMax = 0x1770;
                this.soundHit = 2;
                this.soundKilled = 2;
                this.noGravity = true;
                this.noTileCollide = true;
                this.value = 50000f;
                this.knockBackResist = 0f;
                this.boss = true;
            }
            if (this.type == 0x24)
            {
                this.name = "Skeletron Hand";
                this.width = 0x34;
                this.height = 0x34;
                this.aiStyle = 12;
                this.damage = 30;
                this.defense = 0x12;
                this.lifeMax = 0x4b0;
                this.soundHit = 2;
                this.soundKilled = 2;
                this.noGravity = true;
                this.noTileCollide = true;
                this.knockBackResist = 0f;
            }
            if (this.type == 0x25)
            {
                this.townNPC = true;
                this.friendly = true;
                this.name = "Old Man";
                this.width = 0x12;
                this.height = 40;
                this.aiStyle = 7;
                this.damage = 10;
                this.defense = 15;
                this.lifeMax = 250;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.knockBackResist = 0.5f;
            }
            if (this.type == 0x26)
            {
                this.townNPC = true;
                this.friendly = true;
                this.name = "Demolitionist";
                this.width = 0x12;
                this.height = 40;
                this.aiStyle = 7;
                this.damage = 10;
                this.defense = 15;
                this.lifeMax = 250;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.knockBackResist = 0.5f;
            }
            if (this.type == 0x27)
            {
                this.name = "Bone Serpent Head";
                this.width = 0x16;
                this.height = 0x16;
                this.aiStyle = 6;
                this.damage = 40;
                this.defense = 10;
                this.lifeMax = 120;
                this.soundHit = 2;
                this.soundKilled = 2;
                this.noGravity = true;
                this.noTileCollide = true;
                this.knockBackResist = 0f;
                this.behindTiles = true;
                this.value = 1000f;
            }
            if (this.type == 40)
            {
                this.name = "Bone Serpent Body";
                this.width = 0x16;
                this.height = 0x16;
                this.aiStyle = 6;
                this.damage = 30;
                this.defense = 12;
                this.lifeMax = 150;
                this.soundHit = 2;
                this.soundKilled = 2;
                this.noGravity = true;
                this.noTileCollide = true;
                this.knockBackResist = 0f;
                this.behindTiles = true;
                this.value = 1000f;
            }
            if (this.type == 0x29)
            {
                this.name = "Bone Serpent Tail";
                this.width = 0x16;
                this.height = 0x16;
                this.aiStyle = 6;
                this.damage = 20;
                this.defense = 0x12;
                this.lifeMax = 200;
                this.soundHit = 2;
                this.soundKilled = 2;
                this.noGravity = true;
                this.noTileCollide = true;
                this.knockBackResist = 0f;
                this.behindTiles = true;
                this.value = 1000f;
            }
            if (this.type == 0x2a)
            {
                this.name = "Hornet";
                this.width = 0x22;
                this.height = 0x20;
                this.aiStyle = 2;
                this.damage = 40;
                this.defense = 14;
                this.lifeMax = 100;
                this.soundHit = 1;
                this.knockBackResist = 0.8f;
                this.soundKilled = 1;
                this.value = 750f;
            }
            if (this.type == 0x2b)
            {
                this.noGravity = true;
                this.name = "Man Eater";
                this.width = 30;
                this.height = 30;
                this.aiStyle = 13;
                this.damage = 60;
                this.defense = 0x12;
                this.lifeMax = 200;
                this.soundHit = 1;
                this.knockBackResist = 0.7f;
                this.soundKilled = 1;
                this.value = 750f;
            }
            else if (this.type == 0x2c)
            {
                this.name = "Undead Miner";
                this.width = 0x12;
                this.height = 40;
                this.aiStyle = 3;
                this.damage = 0x16;
                this.defense = 9;
                this.lifeMax = 70;
                this.soundHit = 2;
                this.soundKilled = 2;
                this.knockBackResist = 0.5f;
                this.value = 250f;
            }
            else if (this.type == 0x2d)
            {
                this.name = "Tim";
                this.width = 0x12;
                this.height = 40;
                this.aiStyle = 8;
                this.damage = 20;
                this.defense = 4;
                this.lifeMax = 200;
                this.soundHit = 2;
                this.soundKilled = 2;
                this.knockBackResist = 0.6f;
                this.value = 5000f;
            }
            else if (this.type == 0x2e)
            {
                this.name = "Bunny";
                this.friendly = true;
                this.width = 0x12;
                this.height = 20;
                this.aiStyle = 7;
                this.damage = 0;
                this.defense = 0;
                this.lifeMax = 5;
                this.soundHit = 1;
                this.soundKilled = 1;
            }
            else if (this.type == 0x2f)
            {
                this.name = "Corrupt Bunny";
                this.width = 0x12;
                this.height = 20;
                this.aiStyle = 3;
                this.damage = 30;
                this.defense = 6;
                this.lifeMax = 100;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.value = 500f;
            }
            else if (this.type == 0x30)
            {
                this.name = "Harpey";
                this.width = 0x18;
                this.height = 0x22;
                this.aiStyle = 14;
                this.damage = 0x19;
                this.defense = 8;
                this.lifeMax = 100;
                this.soundHit = 1;
                this.knockBackResist = 0.6f;
                this.soundKilled = 1;
                this.value = 500f;
            }
            else if (this.type == 0x31)
            {
                this.name = "Cave Bat";
                this.width = 12;
                this.height = 12;
                this.aiStyle = 14;
                this.damage = 0x19;
                this.defense = 2;
                this.lifeMax = 0x2d;
                this.soundHit = 1;
                this.knockBackResist = 0.8f;
                this.soundKilled = 1;
                this.value = 100f;
            }
            else if (this.type == 50)
            {
                this.boss = true;
                this.name = "King Slime";
                this.width = 0x62;
                this.height = 0x5c;
                this.aiStyle = 15;
                this.damage = 40;
                this.defense = 10;
                this.lifeMax = 0x7d0;
                this.knockBackResist = 0f;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.alpha = 30;
                this.value = 10000f;
                this.scale = 1.25f;
            }
            else if (this.type == 0x33)
            {
                this.name = "Jungle Bat";
                this.width = 12;
                this.height = 12;
                this.aiStyle = 14;
                this.damage = 20;
                this.defense = 4;
                this.lifeMax = 70;
                this.soundHit = 1;
                this.knockBackResist = 0.8f;
                this.soundKilled = 1;
                this.value = 100f;
            }
            else if (this.type == 0x34)
            {
                this.name = "Doctor Bones";
                this.width = 0x12;
                this.height = 40;
                this.aiStyle = 3;
                this.damage = 20;
                this.defense = 10;
                this.lifeMax = 500;
                this.soundHit = 1;
                this.soundKilled = 2;
                this.knockBackResist = 0.5f;
                this.value = 60f;
            }
            else if (this.type == 0x35)
            {
                this.name = "The Groom";
                this.width = 0x12;
                this.height = 40;
                this.aiStyle = 3;
                this.damage = 14;
                this.defense = 8;
                this.lifeMax = 200;
                this.soundHit = 1;
                this.soundKilled = 2;
                this.knockBackResist = 0.5f;
                this.value = 60f;
            }
            else if (this.type == 0x36)
            {
                this.townNPC = true;
                this.friendly = true;
                this.name = "Clothier";
                this.width = 0x12;
                this.height = 40;
                this.aiStyle = 7;
                this.damage = 10;
                this.defense = 15;
                this.lifeMax = 250;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.knockBackResist = 0.5f;
            }
            else if (this.type == 0x37)
            {
                this.friendly = true;
                this.noGravity = true;
                this.name = "Goldfish";
                this.width = 20;
                this.height = 0x12;
                this.aiStyle = 0x10;
                this.damage = 0;
                this.defense = 0;
                this.lifeMax = 5;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.knockBackResist = 0.5f;
            }
            else if (this.type == 0x38)
            {
                this.noGravity = true;
                this.name = "Snatcher";
                this.width = 30;
                this.height = 30;
                this.aiStyle = 13;
                this.damage = 0x19;
                this.defense = 10;
                this.lifeMax = 100;
                this.soundHit = 1;
                this.knockBackResist = 0.7f;
                this.soundKilled = 1;
                this.value = 500f;
            }
            else if (this.type == 0x39)
            {
                this.noGravity = true;
                this.name = "Corrupt Goldfish";
                this.width = 0x12;
                this.height = 20;
                this.aiStyle = 0x10;
                this.damage = 30;
                this.defense = 6;
                this.lifeMax = 100;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.value = 500f;
            }
            else if (this.type == 0x3a)
            {
                this.noGravity = true;
                this.name = "Piranha";
                this.width = 0x12;
                this.height = 20;
                this.aiStyle = 0x10;
                this.damage = 0x19;
                this.defense = 2;
                this.lifeMax = 30;
                this.soundHit = 1;
                this.soundKilled = 1;
                this.value = 300f;
            }
            if (Main.dedServ)
            {
                this.frame = new Rectangle();
            }
            else
            {
                this.frame = new Rectangle(0, 0, Main.npcTexture[this.type].Width, Main.npcTexture[this.type].Height / Main.npcFrameCount[this.type]);
            }
            this.width = (int) (this.width * this.scale);
            this.height = (int) (this.height * this.scale);
            this.life = this.lifeMax;
        }

        public void SetDefaults(string Name)
        {
            this.SetDefaults(0);
            if (Name == "Green Slime")
            {
                this.SetDefaults(1);
                this.name = Name;
                this.scale = 0.9f;
                this.damage = 8;
                this.defense = 2;
                this.life = 15;
                this.knockBackResist = 1.1f;
                this.color = new Color(0, 220, 40, 100);
                this.value = 3f;
            }
            else if (Name == "Pinky")
            {
                this.SetDefaults(1);
                this.name = Name;
                this.scale = 0.6f;
                this.damage = 5;
                this.defense = 5;
                this.life = 150;
                this.knockBackResist = 1.4f;
                this.color = new Color(250, 30, 90, 90);
                this.value = 10000f;
            }
            else if (Name == "Baby Slime")
            {
                this.SetDefaults(1);
                this.name = Name;
                this.scale = 0.9f;
                this.damage = 13;
                this.defense = 4;
                this.life = 30;
                this.knockBackResist = 0.95f;
                this.alpha = 120;
                this.color = new Color(0, 0, 0, 50);
                this.value = 10f;
            }
            else if (Name == "Black Slime")
            {
                this.SetDefaults(1);
                this.name = Name;
                this.damage = 15;
                this.defense = 4;
                this.life = 0x2d;
                this.color = new Color(0, 0, 0, 50);
                this.value = 20f;
            }
            else if (Name == "Purple Slime")
            {
                this.SetDefaults(1);
                this.name = Name;
                this.scale = 1.2f;
                this.damage = 12;
                this.defense = 6;
                this.life = 40;
                this.knockBackResist = 0.9f;
                this.color = new Color(200, 0, 0xff, 150);
                this.value = 10f;
            }
            else if (Name == "Red Slime")
            {
                this.SetDefaults(1);
                this.name = Name;
                this.damage = 12;
                this.defense = 4;
                this.life = 0x23;
                this.color = new Color(0xff, 30, 0, 100);
                this.value = 8f;
            }
            else if (Name == "Yellow Slime")
            {
                this.SetDefaults(1);
                this.name = Name;
                this.scale = 1.2f;
                this.damage = 15;
                this.defense = 7;
                this.life = 0x2d;
                this.color = new Color(0xff, 0xff, 0, 100);
                this.value = 10f;
            }
            else if (Name == "Jungle Slime")
            {
                this.SetDefaults(1);
                this.name = Name;
                this.damage = 0x12;
                this.defense = 6;
                this.life = 60;
                this.scale = 1.1f;
                this.color = new Color(0x8f, 0xd7, 0x5d, 100);
                this.value = 500f;
            }
            else if (Name != "")
            {
                for (int i = 1; i < 0x3b; i++)
                {
                    this.SetDefaults(i);
                    if (this.name == Name)
                    {
                        break;
                    }
                    if (i == 0x3a)
                    {
                        this.SetDefaults(0);
                        this.active = false;
                    }
                }
            }
            else
            {
                this.active = false;
            }
            this.lifeMax = this.life;
        }

        public static void SpawnNPC()
        {
            if (!Main.stopSpawns)
            {
                if (noSpawnCycle)
                {
                    noSpawnCycle = false;
                }
                else
                {
                    int num5;
                    bool flag = false;
                    bool flag2 = false;
                    int num = 0;
                    int num2 = 0;
                    int num3 = 0;
                    int num4 = 0;
                    for (num5 = 0; num5 < 0xff; num5++)
                    {
                        if (Main.player[num5].active)
                        {
                            num4++;
                        }
                    }
                    for (num5 = 0; num5 < 0xff; num5++)
                    {
                        int num16;
                        if (!Main.player[num5].active || Main.player[num5].dead)
                        {
                            goto Label_1904;
                        }
                        bool flag3 = false;
                        bool flag4 = false;
                        if ((((Main.player[num5].active && (Main.invasionType > 0)) && (Main.invasionDelay == 0)) && (Main.invasionSize > 0)) && (Main.player[num5].position.Y < ((Main.worldSurface * 16.0) + sHeight)))
                        {
                            int num6 = 0xbb8;
                            if ((Main.player[num5].position.X > ((Main.invasionX * 16.0) - num6)) && (Main.player[num5].position.X < ((Main.invasionX * 16.0) + num6)))
                            {
                                flag3 = true;
                            }
                        }
                        flag = false;
                        spawnRate = defaultSpawnRate;
                        maxSpawns = defaultMaxSpawns;
                        if (Main.player[num5].position.Y > ((Main.maxTilesY - 200) * 0x10))
                        {
                            spawnRate = (int) (spawnRate * 0.9f);
                            maxSpawns = (int) (maxSpawns * 1.1f);
                        }
                        else if (Main.player[num5].position.Y > ((Main.rockLayer * 16.0) + sHeight))
                        {
                            spawnRate = (int) (spawnRate * 0.4);
                            maxSpawns = (int) (maxSpawns * 1.9f);
                        }
                        else if (Main.player[num5].position.Y > ((Main.worldSurface * 16.0) + sHeight))
                        {
                            spawnRate = (int) (spawnRate * 0.5);
                            maxSpawns = (int) (maxSpawns * 1.7f);
                        }
                        else if (!Main.dayTime)
                        {
                            spawnRate = (int) (spawnRate * 0.6);
                            maxSpawns = (int) (maxSpawns * 1.3f);
                            if (Main.bloodMoon)
                            {
                                spawnRate = (int) (spawnRate * 0.3);
                                maxSpawns = (int) (maxSpawns * 1.8f);
                            }
                        }
                        if (Main.player[num5].zoneDungeon)
                        {
                            spawnRate = (int) (defaultSpawnRate * 0.15);
                            maxSpawns = defaultMaxSpawns * 2;
                        }
                        else if (Main.player[num5].zoneEvil)
                        {
                            spawnRate = (int) (spawnRate * 0.4);
                            maxSpawns = (int) (maxSpawns * 1.6f);
                        }
                        else if (Main.player[num5].zoneMeteor)
                        {
                            spawnRate = (int) (spawnRate * 0.5);
                        }
                        else if (Main.player[num5].zoneJungle)
                        {
                            spawnRate = (int) (spawnRate * 0.3);
                            maxSpawns = (int) (maxSpawns * 1.6f);
                        }
                        if (Main.player[num5].activeNPCs < (maxSpawns * 0.2))
                        {
                            spawnRate = (int) (spawnRate * 0.6f);
                        }
                        else if (Main.player[num5].activeNPCs < (maxSpawns * 0.4))
                        {
                            spawnRate = (int) (spawnRate * 0.7f);
                        }
                        else if (Main.player[num5].activeNPCs < (maxSpawns * 0.6))
                        {
                            spawnRate = (int) (spawnRate * 0.8f);
                        }
                        else if (Main.player[num5].activeNPCs < (maxSpawns * 0.8))
                        {
                            spawnRate = (int) (spawnRate * 0.9f);
                        }
                        if (((Main.player[num5].position.Y * 16f) > ((Main.worldSurface + Main.rockLayer) / 2.0)) || Main.player[num5].zoneEvil)
                        {
                            if (Main.player[num5].activeNPCs < (maxSpawns * 0.2))
                            {
                                spawnRate = (int) (spawnRate * 0.7f);
                            }
                            else if (Main.player[num5].activeNPCs < (maxSpawns * 0.4))
                            {
                                spawnRate = (int) (spawnRate * 0.9f);
                            }
                        }
                        if (spawnRate < (defaultSpawnRate * 0.1))
                        {
                            spawnRate = (int) (defaultSpawnRate * 0.1);
                        }
                        if (maxSpawns > (defaultMaxSpawns * 3))
                        {
                            maxSpawns = defaultMaxSpawns * 3;
                        }
                        if (Main.player[num5].inventory[Main.player[num5].selectedItem].type == 0x94)
                        {
                            spawnRate = (int) (spawnRate * 0.75);
                            maxSpawns = (int) (maxSpawns * 1.5f);
                        }
                        if (flag3)
                        {
                            maxSpawns = (int) (defaultMaxSpawns * (1.0 + (0.4 * num4)));
                            spawnRate = 30;
                        }
                        bool flag5 = false;
                        if (((!flag3 && (!Main.bloodMoon || Main.dayTime)) && (!Main.player[num5].zoneDungeon && !Main.player[num5].zoneEvil)) && !Main.player[num5].zoneMeteor)
                        {
                            if (Main.player[num5].townNPCs == 1)
                            {
                                if (Main.rand.Next(3) <= 1)
                                {
                                    flag5 = true;
                                    maxSpawns = (int) (maxSpawns * 0.6);
                                }
                                else
                                {
                                    spawnRate = (int) (spawnRate * 2f);
                                }
                            }
                            else if (Main.player[num5].townNPCs == 2)
                            {
                                if (Main.rand.Next(3) == 0)
                                {
                                    flag5 = true;
                                    maxSpawns = (int) (maxSpawns * 0.6);
                                }
                                else
                                {
                                    spawnRate = (int) (spawnRate * 3f);
                                }
                            }
                            else if (Main.player[num5].townNPCs >= 3)
                            {
                                flag5 = true;
                                maxSpawns = (int) (maxSpawns * 0.6);
                            }
                        }
                        if (Main.alwaysSpawn > 0)
                        {
                            spawnRate = 1;
                        }
                        if (((Main.player[num5].active && !Main.player[num5].dead) && (Main.player[num5].activeNPCs < maxSpawns)) && (Main.rand.Next(spawnRate) == 0))
                        {
                            int minValue = ((int) (Main.player[num5].position.X / 16f)) - spawnRangeX;
                            int maxValue = ((int) (Main.player[num5].position.X / 16f)) + spawnRangeX;
                            int num9 = ((int) (Main.player[num5].position.Y / 16f)) - spawnRangeY;
                            int maxTilesY = ((int) (Main.player[num5].position.Y / 16f)) + spawnRangeY;
                            int num11 = ((int) (Main.player[num5].position.X / 16f)) - safeRangeX;
                            int num12 = ((int) (Main.player[num5].position.X / 16f)) + safeRangeX;
                            int num13 = ((int) (Main.player[num5].position.Y / 16f)) - safeRangeY;
                            int num14 = ((int) (Main.player[num5].position.Y / 16f)) + safeRangeY;
                            if (minValue < 0)
                            {
                                minValue = 0;
                            }
                            if (maxValue > Main.maxTilesX)
                            {
                                maxValue = Main.maxTilesX;
                            }
                            if (num9 < 0)
                            {
                                num9 = 0;
                            }
                            if (maxTilesY > Main.maxTilesY)
                            {
                                maxTilesY = Main.maxTilesY;
                            }
                            for (int i = 0; i < 50; i++)
                            {
                                num16 = Main.rand.Next(minValue, maxValue);
                                int num17 = Main.rand.Next(num9, maxTilesY);
                                if (!Main.tile[num16, num17].active || !Main.tileSolid[Main.tile[num16, num17].type])
                                {
                                    int num18;
                                    if (Main.wallHouse[Main.tile[num16, num17].wall])
                                    {
                                        goto Label_0CE4;
                                    }
                                    if (((!flag3 && (num17 < (Main.worldSurface * 0.30000001192092896))) && !flag5) && ((num16 < (Main.maxTilesX * 0.35)) || (num16 > (Main.maxTilesX * 0.65))))
                                    {
                                        num3 = Main.tile[num16, num17].type;
                                        num = num16;
                                        num2 = num17;
                                        flag = true;
                                        flag2 = true;
                                    }
                                    else
                                    {
                                        num18 = num17;
                                        while (num18 < Main.maxTilesY)
                                        {
                                            if (Main.tile[num16, num18].active && Main.tileSolid[Main.tile[num16, num18].type])
                                            {
                                                if ((((num16 < num11) || (num16 > num12)) || (num18 < num13)) || (num18 > num14))
                                                {
                                                    num3 = Main.tile[num16, num18].type;
                                                    num = num16;
                                                    num2 = num18;
                                                    flag = true;
                                                }
                                                break;
                                            }
                                            num18++;
                                        }
                                    }
                                    if (flag)
                                    {
                                        int num19 = num - (spawnSpaceX / 2);
                                        int num20 = num + (spawnSpaceX / 2);
                                        int num21 = num2 - spawnSpaceY;
                                        int num22 = num2;
                                        if (num19 < 0)
                                        {
                                            flag = false;
                                        }
                                        if (num20 > Main.maxTilesX)
                                        {
                                            flag = false;
                                        }
                                        if (num21 < 0)
                                        {
                                            flag = false;
                                        }
                                        if (num22 > Main.maxTilesY)
                                        {
                                            flag = false;
                                        }
                                        if (flag)
                                        {
                                            for (int j = num19; j < num20; j++)
                                            {
                                                for (num18 = num21; num18 < num22; num18++)
                                                {
                                                    if (Main.tile[j, num18].active && Main.tileSolid[Main.tile[j, num18].type])
                                                    {
                                                        flag = false;
                                                        break;
                                                    }
                                                    if (Main.tile[j, num18].lava && (num18 < (Main.maxTilesY - 200)))
                                                    {
                                                        flag = false;
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (flag || flag)
                                {
                                    break;
                                }
                            Label_0CE4:;
                            }
                        }
                        if (flag)
                        {
                            Rectangle rectangle = new Rectangle(num * 0x10, num2 * 0x10, 0x10, 0x10);
                            for (num16 = 0; num16 < 0xff; num16++)
                            {
                                if (Main.player[num16].active)
                                {
                                    Rectangle rectangle2 = new Rectangle(((((int) Main.player[num16].position.X) + (Main.player[num16].width / 2)) - (sWidth / 2)) - safeRangeX, ((((int) Main.player[num16].position.Y) + (Main.player[num16].height / 2)) - (sHeight / 2)) - safeRangeY, sWidth + (safeRangeX * 2), sHeight + (safeRangeY * 2));
                                    if (rectangle.Intersects(rectangle2))
                                    {
                                        flag = false;
                                    }
                                }
                            }
                        }
                        if (flag)
                        {
                            if (Main.player[num5].zoneDungeon && !(Main.tileDungeon[Main.tile[num, num2].type] && (Main.tile[num, num2 - 1].wall != 0)))
                            {
                                flag = false;
                            }
                            if ((Main.tile[num, num2 - 1].liquid > 0) && (Main.tile[num, num2 - 2].liquid > 0))
                            {
                                flag4 = true;
                            }
                        }
                        if (!flag)
                        {
                            goto Label_1904;
                        }
                        flag = false;
                        int type = Main.tile[num, num2].type;
                        int index = 0x3e8;
                        if (Main.alwaysSpawn > 0)
                        {
                            NewNPC((num * 0x10) + 8, num2 * 0x10, Main.alwaysSpawn, 1);
                            break;
                        }
                        if (flag2)
                        {
                            NewNPC((num * 0x10) + 8, num2 * 0x10, 0x30, 0);
                        }
                        else if (flag3)
                        {
                            if (Main.rand.Next(9) == 0)
                            {
                                NewNPC((num * 0x10) + 8, num2 * 0x10, 0x1d, 0);
                            }
                            else if (Main.rand.Next(5) == 0)
                            {
                                NewNPC((num * 0x10) + 8, num2 * 0x10, 0x1a, 0);
                            }
                            else if (Main.rand.Next(3) == 0)
                            {
                                NewNPC((num * 0x10) + 8, num2 * 0x10, 0x1b, 0);
                            }
                            else
                            {
                                NewNPC((num * 0x10) + 8, num2 * 0x10, 0x1c, 0);
                            }
                        }
                        else if (flag4 && (((num2 > Main.rockLayer) && (Main.rand.Next(2) == 0)) || (type == 60)))
                        {
                            NewNPC((num * 0x10) + 8, num2 * 0x10, 0x3a, 0);
                        }
                        else if (flag4 && (Main.rand.Next(4) == 0))
                        {
                            NewNPC((num * 0x10) + 8, num2 * 0x10, 0x37, 0);
                        }
                        else
                        {
                            if (flag5)
                            {
                                if (flag4)
                                {
                                    NewNPC((num * 0x10) + 8, num2 * 0x10, 0x37, 0);
                                    goto Label_187F;
                                }
                                if (type == 2)
                                {
                                    NewNPC((num * 0x10) + 8, num2 * 0x10, 0x2e, 0);
                                    goto Label_187F;
                                }
                                break;
                            }
                            if (Main.player[num5].zoneDungeon)
                            {
                                if (!downedBoss3)
                                {
                                    index = NewNPC((num * 0x10) + 8, num2 * 0x10, 0x23, 0);
                                    Main.npc[index].ai[0] = 1f;
                                    Main.npc[index].ai[2] = 2f;
                                }
                                else if (Main.rand.Next(4) == 0)
                                {
                                    index = NewNPC((num * 0x10) + 8, num2 * 0x10, 0x22, 0);
                                }
                                else if (Main.rand.Next(5) == 0)
                                {
                                    index = NewNPC((num * 0x10) + 8, num2 * 0x10, 0x20, 0);
                                }
                                else
                                {
                                    index = NewNPC((num * 0x10) + 8, num2 * 0x10, 0x1f, 0);
                                }
                            }
                            else if (Main.player[num5].zoneMeteor)
                            {
                                index = NewNPC((num * 0x10) + 8, num2 * 0x10, 0x17, 0);
                            }
                            else if (Main.player[num5].zoneEvil && (Main.rand.Next(50) == 0))
                            {
                                index = NewNPC((num * 0x10) + 8, num2 * 0x10, 7, 1);
                            }
                            else if (!(((type != 60) || (Main.rand.Next(500) != 0)) || Main.dayTime))
                            {
                                index = NewNPC((num * 0x10) + 8, num2 * 0x10, 0x34, 0);
                            }
                            else if ((type == 60) && (num2 > ((Main.worldSurface + Main.rockLayer) / 2.0)))
                            {
                                if (Main.rand.Next(3) == 0)
                                {
                                    index = NewNPC((num * 0x10) + 8, num2 * 0x10, 0x2b, 0);
                                    Main.npc[index].ai[0] = num;
                                    Main.npc[index].ai[1] = num2;
                                    Main.npc[index].netUpdate = true;
                                }
                                else
                                {
                                    index = NewNPC((num * 0x10) + 8, num2 * 0x10, 0x2a, 0);
                                }
                            }
                            else if ((type == 60) && (Main.rand.Next(4) == 0))
                            {
                                index = NewNPC((num * 0x10) + 8, num2 * 0x10, 0x33, 0);
                            }
                            else if ((type == 60) && (Main.rand.Next(8) == 0))
                            {
                                index = NewNPC((num * 0x10) + 8, num2 * 0x10, 0x38, 0);
                                Main.npc[index].ai[0] = num;
                                Main.npc[index].ai[1] = num2;
                                Main.npc[index].netUpdate = true;
                            }
                            else if (num2 <= Main.worldSurface)
                            {
                                if ((type == 0x17) || (type == 0x19))
                                {
                                    index = NewNPC((num * 0x10) + 8, num2 * 0x10, 6, 0);
                                }
                                else if (Main.dayTime)
                                {
                                    int num26 = Math.Abs((int) (num - Main.spawnTileX));
                                    if (((num26 < (Main.maxTilesX / 3)) && (Main.rand.Next(10) == 0)) && (type == 2))
                                    {
                                        NewNPC((num * 0x10) + 8, num2 * 0x10, 0x2e, 0);
                                    }
                                    else if (((num26 > (Main.maxTilesX / 3)) && (type == 2)) && (Main.rand.Next(300) == 0))
                                    {
                                        index = NewNPC((num * 0x10) + 8, num2 * 0x10, 50, 0);
                                    }
                                    else
                                    {
                                        index = NewNPC((num * 0x10) + 8, num2 * 0x10, 1, 0);
                                        if (type == 60)
                                        {
                                            Main.npc[index].SetDefaults("Jungle Slime");
                                        }
                                        else if ((Main.rand.Next(3) == 0) || (num26 < 200))
                                        {
                                            Main.npc[index].SetDefaults("Green Slime");
                                        }
                                        else if ((Main.rand.Next(10) == 0) && (num26 > 400))
                                        {
                                            Main.npc[index].SetDefaults("Purple Slime");
                                        }
                                    }
                                }
                                else if ((Main.rand.Next(6) == 0) || ((Main.moonPhase == 4) && (Main.rand.Next(2) == 0)))
                                {
                                    index = NewNPC((num * 0x10) + 8, num2 * 0x10, 2, 0);
                                }
                                else if ((Main.rand.Next(250) == 0) && Main.bloodMoon)
                                {
                                    NewNPC((num * 0x10) + 8, num2 * 0x10, 0x34, 0);
                                }
                                else
                                {
                                    NewNPC((num * 0x10) + 8, num2 * 0x10, 3, 0);
                                }
                            }
                            else if (num2 <= Main.rockLayer)
                            {
                                if (Main.rand.Next(30) == 0)
                                {
                                    index = NewNPC((num * 0x10) + 8, num2 * 0x10, 10, 1);
                                }
                                else
                                {
                                    index = NewNPC((num * 0x10) + 8, num2 * 0x10, 1, 0);
                                    if (Main.rand.Next(5) == 0)
                                    {
                                        Main.npc[index].SetDefaults("Yellow Slime");
                                    }
                                    else if (Main.rand.Next(2) == 0)
                                    {
                                        Main.npc[index].SetDefaults("Blue Slime");
                                    }
                                    else
                                    {
                                        Main.npc[index].SetDefaults("Red Slime");
                                    }
                                }
                            }
                            else if (num2 > (Main.maxTilesY - 190))
                            {
                                if (Main.rand.Next(5) == 0)
                                {
                                    index = NewNPC((num * 0x10) + 8, num2 * 0x10, 0x27, 1);
                                }
                                else
                                {
                                    index = NewNPC((num * 0x10) + 8, num2 * 0x10, 0x18, 0);
                                }
                            }
                            else if (Main.rand.Next(0x23) == 0)
                            {
                                index = NewNPC((num * 0x10) + 8, num2 * 0x10, 10, 1);
                            }
                            else if (Main.rand.Next(10) == 0)
                            {
                                index = NewNPC((num * 0x10) + 8, num2 * 0x10, 0x10, 0);
                            }
                            else if (Main.rand.Next(4) == 0)
                            {
                                index = NewNPC((num * 0x10) + 8, num2 * 0x10, 1, 0);
                                Main.npc[index].SetDefaults("Black Slime");
                            }
                            else if (Main.rand.Next(2) == 0)
                            {
                                if ((num2 > ((Main.rockLayer + Main.maxTilesY) / 2.0)) && (Main.rand.Next(700) == 0))
                                {
                                    index = NewNPC((num * 0x10) + 8, num2 * 0x10, 0x2d, 0);
                                }
                                else if (Main.rand.Next(15) == 0)
                                {
                                    index = NewNPC((num * 0x10) + 8, num2 * 0x10, 0x2c, 0);
                                }
                                else
                                {
                                    index = NewNPC((num * 0x10) + 8, num2 * 0x10, 0x15, 0);
                                }
                            }
                            else
                            {
                                index = NewNPC((num * 0x10) + 8, num2 * 0x10, 0x31, 0);
                            }
                        }
                    Label_187F:
                        if ((Main.npc[index].type == 1) && (Main.rand.Next(250) == 0))
                        {
                            Main.npc[index].SetDefaults("Pinky");
                        }
                        if ((Main.netMode == 2) && (index < 0x3e8))
                        {
                            NetMessage.SendData(0x17, -1, -1, "", index, 0f, 0f, 0f);
                        }
                        break;
                    Label_1904:;
                    }
                }
            }
        }

        public static void SpawnOnPlayer(int plr, int Type)
        {
            if (Main.netMode != 1)
            {
                bool flag = false;
                int num = 0;
                int num2 = 0;
                int type = 0;
                int minValue = ((int) (Main.player[plr].position.X / 16f)) - (spawnRangeX * 3);
                int maxValue = ((int) (Main.player[plr].position.X / 16f)) + (spawnRangeX * 3);
                int num6 = ((int) (Main.player[plr].position.Y / 16f)) - (spawnRangeY * 3);
                int maxTilesY = ((int) (Main.player[plr].position.Y / 16f)) + (spawnRangeY * 3);
                int num8 = ((int) (Main.player[plr].position.X / 16f)) - safeRangeX;
                int num9 = ((int) (Main.player[plr].position.X / 16f)) + safeRangeX;
                int num10 = ((int) (Main.player[plr].position.Y / 16f)) - safeRangeY;
                int num11 = ((int) (Main.player[plr].position.Y / 16f)) + safeRangeY;
                if (minValue < 0)
                {
                    minValue = 0;
                }
                if (maxValue > Main.maxTilesX)
                {
                    maxValue = Main.maxTilesX;
                }
                if (num6 < 0)
                {
                    num6 = 0;
                }
                if (maxTilesY > Main.maxTilesY)
                {
                    maxTilesY = Main.maxTilesY;
                }
                for (int i = 0; i < 0x3e8; i++)
                {
                    int num14;
                    for (int j = 0; j < 100; j++)
                    {
                        num14 = Main.rand.Next(minValue, maxValue);
                        int num15 = Main.rand.Next(num6, maxTilesY);
                        if (!Main.tile[num14, num15].active || !Main.tileSolid[Main.tile[num14, num15].type])
                        {
                            if (Main.tile[num14, num15].wall == 1)
                            {
                                continue;
                            }
                            int num16 = num15;
                            while (num16 < Main.maxTilesY)
                            {
                                if (Main.tile[num14, num16].active && Main.tileSolid[Main.tile[num14, num16].type])
                                {
                                    if ((((num14 < num8) || (num14 > num9)) || (num16 < num10)) || (num16 > num11))
                                    {
                                        type = Main.tile[num14, num16].type;
                                        num = num14;
                                        num2 = num16;
                                        flag = true;
                                    }
                                    break;
                                }
                                num16++;
                            }
                            if (flag)
                            {
                                int num17 = num - (spawnSpaceX / 2);
                                int num18 = num + (spawnSpaceX / 2);
                                int num19 = num2 - spawnSpaceY;
                                int num20 = num2;
                                if (num17 < 0)
                                {
                                    flag = false;
                                }
                                if (num18 > Main.maxTilesX)
                                {
                                    flag = false;
                                }
                                if (num19 < 0)
                                {
                                    flag = false;
                                }
                                if (num20 > Main.maxTilesY)
                                {
                                    flag = false;
                                }
                                if (flag)
                                {
                                    for (int k = num17; k < num18; k++)
                                    {
                                        for (num16 = num19; num16 < num20; num16++)
                                        {
                                            if (Main.tile[k, num16].active && Main.tileSolid[Main.tile[k, num16].type])
                                            {
                                                flag = false;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (flag || flag)
                        {
                            break;
                        }
                    }
                    if (flag)
                    {
                        Rectangle rectangle = new Rectangle(num * 0x10, num2 * 0x10, 0x10, 0x10);
                        for (num14 = 0; num14 < 0xff; num14++)
                        {
                            if (Main.player[num14].active)
                            {
                                Rectangle rectangle2 = new Rectangle(((((int) Main.player[num14].position.X) + (Main.player[num14].width / 2)) - (sWidth / 2)) - safeRangeX, ((((int) Main.player[num14].position.Y) + (Main.player[num14].height / 2)) - (sHeight / 2)) - safeRangeY, sWidth + (safeRangeX * 2), sHeight + (safeRangeY * 2));
                                if (rectangle.Intersects(rectangle2))
                                {
                                    flag = false;
                                }
                            }
                        }
                    }
                    if (flag)
                    {
                        break;
                    }
                }
                if (flag)
                {
                    int index = 0x3e8;
                    index = NewNPC((num * 0x10) + 8, num2 * 0x10, Type, 1);
                    Main.npc[index].target = plr;
                    string name = Main.npc[index].name;
                    if (Main.npc[index].type == 13)
                    {
                        name = "Eater of Worlds";
                    }
                    if (Main.npc[index].type == 0x23)
                    {
                        name = "Skeletron";
                    }
                    if ((Main.netMode == 2) && (index < 0x3e8))
                    {
                        NetMessage.SendData(0x17, -1, -1, "", index, 0f, 0f, 0f);
                    }
                    if (Main.netMode == 0)
                    {
                        Main.NewText(name + " has awoken!", 0xaf, 0x4b, 0xff);
                    }
                    else if (Main.netMode == 2)
                    {
                        NetMessage.SendData(0x19, -1, -1, name + " has awoken!", 0xff, 175f, 75f, 255f);
                    }
                }
            }
        }

        public double StrikeNPC(int Damage, float knockBack, int hitDirection)
        {
            if (!(this.active && (this.life > 0)))
            {
                return 0.0;
            }
            double dmg = Main.CalculateDamage(Damage, this.defense);
            if (this.friendly)
            {
                CombatText.NewText(new Rectangle((int)this.position.X, (int)this.position.Y, this.width, this.height), new Color(0xff, 80, 90, 0xff), ((int)dmg).ToString());
            }
            else
            {
                CombatText.NewText(new Rectangle((int) this.position.X, (int) this.position.Y, this.width, this.height), new Color(0xff, 160, 80, 0xff),  ((int)dmg).ToString());
            }
            if (dmg < 1.0)
            {
                return 0.0;
            }
            if (this.townNPC)
            {
                this.ai[0] = 1f;
                this.ai[1] = 300 + Main.rand.Next(300);
                this.ai[2] = 0f;
                this.direction = hitDirection;
                this.netUpdate = true;
            }
            if ((this.aiStyle == 8) && (Main.netMode != 1))
            {
                this.ai[0] = 400f;
                this.TargetClosest(true);
            }
            this.life -= (int) dmg;
            if ((knockBack > 0f) && (this.knockBackResist > 0f))
            {
                if (!this.noGravity)
                {
                    this.velocity.Y = (-knockBack * 0.75f) * this.knockBackResist;
                }
                else
                {
                    this.velocity.Y = (-knockBack * 0.5f) * this.knockBackResist;
                }
                this.velocity.X = (knockBack * hitDirection) * this.knockBackResist;
            }
            this.HitEffect(hitDirection, dmg);
            if (this.soundHit > 0)
            {
                Main.PlaySound(3, (int) this.position.X, (int) this.position.Y, this.soundHit);
            }
            if (this.life <= 0)
            {
                noSpawnCycle = true;
                if (this.townNPC && (this.type != 0x25))
                {
                    if (Main.netMode == 0)
                    {
                        Main.NewText(this.name + " was slain...", 0xff, 0x19, 0x19);
                    }
                    else if (Main.netMode == 2)
                    {
                        NetMessage.SendData(0x19, -1, -1, this.name + " was slain...", 0xff, 255f, 25f, 25f);
                    }
                }
                if (((this.townNPC && (Main.netMode != 1)) && this.homeless) && (WorldGen.spawnNPC == this.type))
                {
                    WorldGen.spawnNPC = 0;
                }
                if (this.soundKilled > 0)
                {
                    Main.PlaySound(4, (int) this.position.X, (int) this.position.Y, this.soundKilled);
                }
                this.NPCLoot();
                this.active = false;
                if ((((this.type == 0x1a) || (this.type == 0x1b)) || (this.type == 0x1c)) || (this.type == 0x1d))
                {
                    Main.invasionSize--;
                }
            }
            return dmg;
        }

        public void TargetClosest(bool faceTarget = true)
        {
            float num = -1f;
            for (int i = 0; i < 0xff; i++)
            {
                if ((Main.player[i].active && !Main.player[i].dead) && ((num == -1f) || ((Math.Abs((float) (((Main.player[i].position.X + (Main.player[i].width / 2)) - this.position.X) + (this.width / 2))) + Math.Abs((float) (((Main.player[i].position.Y + (Main.player[i].height / 2)) - this.position.Y) + (this.height / 2)))) < num)))
                {
                    num = Math.Abs((float) (((Main.player[i].position.X + (Main.player[i].width / 2)) - this.position.X) + (this.width / 2))) + Math.Abs((float) (((Main.player[i].position.Y + (Main.player[i].height / 2)) - this.position.Y) + (this.height / 2)));
                    this.target = i;
                }
            }
            if ((this.target < 0) || (this.target >= 0xff))
            {
                this.target = 0;
            }
            this.targetRect = new Rectangle((int) Main.player[this.target].position.X, (int) Main.player[this.target].position.Y, Main.player[this.target].width, Main.player[this.target].height);
            if (faceTarget)
            {
                this.direction = 1;
                if ((this.targetRect.X + (this.targetRect.Width / 2)) < (this.position.X + (this.width / 2)))
                {
                    this.direction = -1;
                }
                this.directionY = 1;
                if ((this.targetRect.Y + (this.targetRect.Height / 2)) < (this.position.Y + (this.height / 2)))
                {
                    this.directionY = -1;
                }
            }
            if (((this.direction != this.oldDirection) || (this.directionY != this.oldDirectionY)) || (this.target != this.oldTarget))
            {
                this.netUpdate = true;
            }
        }

        public void Transform(int newType)
        {
            if (Main.netMode != 1)
            {
                Vector2 velocity = this.velocity;
                int spriteDirection = this.spriteDirection;
                this.SetDefaults(newType);
                this.spriteDirection = spriteDirection;
                this.TargetClosest(true);
                this.velocity = velocity;
                if (Main.netMode == 2)
                {
                    this.netUpdate = true;
                    NetMessage.SendData(0x17, -1, -1, "", this.whoAmI, 0f, 0f, 0f);
                }
            }
        }

        public void UpdateNPC(int i)
        {
            this.whoAmI = i;
            if (this.active)
            {
                int num3;
                if ((Main.netMode != 1) && Main.bloodMoon)
                {
                    if (this.type == 0x2e)
                    {
                        this.Transform(0x2f);
                    }
                    else if (this.type == 0x37)
                    {
                        this.Transform(0x39);
                    }
                }
                float num = 10f;
                float num2 = 0.3f;
                if (this.wet)
                {
                    num2 = 0.2f;
                    num = 7f;
                }
                if (this.soundDelay > 0)
                {
                    this.soundDelay--;
                }
                if (this.life <= 0)
                {
                    this.active = false;
                }
                this.oldTarget = this.target;
                this.oldDirection = this.direction;
                this.oldDirectionY = this.directionY;
                this.AI();
                if (this.type == 0x2c)
                {
                    Lighting.addLight((((int) this.position.X) + (this.width / 2)) / 0x10, ((int) (this.position.Y + 4f)) / 0x10, 0.6f);
                }
                for (num3 = 0; num3 < 0x100; num3++)
                {
                    if (this.immune[num3] > 0)
                    {
                        this.immune[num3]--;
                    }
                }
                if (!this.noGravity)
                {
                    this.velocity.Y += num2;
                    if (this.velocity.Y > num)
                    {
                        this.velocity.Y = num;
                    }
                }
                if ((this.velocity.X < 0.005) && (this.velocity.X > -0.005))
                {
                    this.velocity.X = 0f;
                }
                if (((Main.netMode != 1) && this.friendly) && (this.type != 0x25))
                {
                    if (this.life < this.lifeMax)
                    {
                        this.friendlyRegen++;
                        if (this.friendlyRegen > 300)
                        {
                            this.friendlyRegen = 0;
                            this.life++;
                            this.netUpdate = true;
                        }
                    }
                    if (this.immune[0xff] == 0)
                    {
                        Rectangle rectangle = new Rectangle((int) this.position.X, (int) this.position.Y, this.width, this.height);
                        for (num3 = 0; num3 < 0x3e8; num3++)
                        {
                            if ((Main.npc[num3].active && !Main.npc[num3].friendly) && (Main.npc[num3].damage > 0))
                            {
                                Rectangle rectangle2 = new Rectangle((int) Main.npc[num3].position.X, (int) Main.npc[num3].position.Y, Main.npc[num3].width, Main.npc[num3].height);
                                if (rectangle.Intersects(rectangle2))
                                {
                                    int damage = Main.npc[num3].damage;
                                    int num5 = 6;
                                    int hitDirection = 1;
                                    if ((Main.npc[num3].position.X + (Main.npc[num3].width / 2)) > (this.position.X + (this.width / 2)))
                                    {
                                        hitDirection = -1;
                                    }
                                    Main.npc[i].StrikeNPC(damage, (float) num5, hitDirection);
                                    if (Main.netMode != 0)
                                    {
                                        NetMessage.SendData(0x1c, -1, -1, "", i, (float) damage, (float) num5, (float) hitDirection);
                                    }
                                    this.netUpdate = true;
                                    this.immune[0xff] = 30;
                                }
                            }
                        }
                    }
                }
                if (!this.noTileCollide)
                {
                    int num7;
                    int num8;
                    Color color;
                    bool flag = Collision.LavaCollision(this.position, this.width, this.height);
                    if (flag)
                    {
                        this.lavaWet = true;
                        if ((Main.netMode != 1) && (this.immune[0xff] == 0))
                        {
                            this.immune[0xff] = 30;
                            this.StrikeNPC(50, 0f, 0);
                            if ((Main.netMode == 2) && (Main.netMode != 0))
                            {
                                NetMessage.SendData(0x1c, -1, -1, "", this.whoAmI, 50f, 0f, 0f);
                            }
                        }
                    }
                    if (Collision.WetCollision(this.position, this.width, this.height))
                    {
                        if (!this.wet && (this.wetCount == 0))
                        {
                            this.wetCount = 10;
                            if (!flag)
                            {
                                for (num7 = 0; num7 < 50; num7++)
                                {
                                    color = new Color();
                                    num8 = Dust.NewDust(new Vector2(this.position.X - 6f, (this.position.Y + (this.height / 2)) - 8f), this.width + 12, 0x18, 0x21, 0f, 0f, 0, color, 1f);
                                    Main.dust[num8].velocity.Y -= 4f;
                                    Main.dust[num8].velocity.X *= 2.5f;
                                    Main.dust[num8].scale = 1.3f;
                                    Main.dust[num8].alpha = 100;
                                    Main.dust[num8].noGravity = true;
                                }
                                Main.PlaySound(0x13, (int) this.position.X, (int) this.position.Y, 0);
                            }
                            else
                            {
                                num7 = 0;
                                while (num7 < 20)
                                {
                                    color = new Color();
                                    num8 = Dust.NewDust(new Vector2(this.position.X - 6f, (this.position.Y + (this.height / 2)) - 8f), this.width + 12, 0x18, 0x23, 0f, 0f, 0, color, 1f);
                                    Main.dust[num8].velocity.Y -= 1.5f;
                                    Main.dust[num8].velocity.X *= 2.5f;
                                    Main.dust[num8].scale = 1.3f;
                                    Main.dust[num8].alpha = 100;
                                    Main.dust[num8].noGravity = true;
                                    num7++;
                                }
                                Main.PlaySound(0x13, (int) this.position.X, (int) this.position.Y, 1);
                            }
                        }
                        this.wet = true;
                    }
                    else if (this.wet)
                    {
                        this.velocity.X *= 0.5f;
                        this.wet = false;
                        if (this.wetCount == 0)
                        {
                            this.wetCount = 10;
                            if (!this.lavaWet)
                            {
                                for (num7 = 0; num7 < 50; num7++)
                                {
                                    color = new Color();
                                    num8 = Dust.NewDust(new Vector2(this.position.X - 6f, this.position.Y + (this.height / 2)), this.width + 12, 0x18, 0x21, 0f, 0f, 0, color, 1f);
                                    Main.dust[num8].velocity.Y -= 4f;
                                    Main.dust[num8].velocity.X *= 2.5f;
                                    Main.dust[num8].scale = 1.3f;
                                    Main.dust[num8].alpha = 100;
                                    Main.dust[num8].noGravity = true;
                                }
                                Main.PlaySound(0x13, (int) this.position.X, (int) this.position.Y, 0);
                            }
                            else
                            {
                                for (num7 = 0; num7 < 20; num7++)
                                {
                                    color = new Color();
                                    num8 = Dust.NewDust(new Vector2(this.position.X - 6f, (this.position.Y + (this.height / 2)) - 8f), this.width + 12, 0x18, 0x23, 0f, 0f, 0, color, 1f);
                                    Main.dust[num8].velocity.Y -= 1.5f;
                                    Main.dust[num8].velocity.X *= 2.5f;
                                    Main.dust[num8].scale = 1.3f;
                                    Main.dust[num8].alpha = 100;
                                    Main.dust[num8].noGravity = true;
                                }
                                Main.PlaySound(0x13, (int) this.position.X, (int) this.position.Y, 1);
                            }
                        }
                    }
                    if (!this.wet)
                    {
                        this.lavaWet = false;
                    }
                    if (this.wetCount > 0)
                    {
                        this.wetCount = (byte) (this.wetCount - 1);
                    }
                    bool fallThrough = false;
                    if (this.aiStyle == 10)
                    {
                        fallThrough = true;
                    }
                    if ((this.aiStyle == 3) && (this.directionY == 1))
                    {
                        fallThrough = true;
                    }
                    this.oldVelocity = this.velocity;
                    this.collideX = false;
                    this.collideY = false;
                    if (this.wet)
                    {
                        Vector2 velocity = this.velocity;
                        this.velocity = Collision.TileCollision(this.position, this.velocity, this.width, this.height, fallThrough, fallThrough);
                        Vector2 vector2 = (Vector2) (this.velocity * 0.5f);
                        if (this.velocity.X != velocity.X)
                        {
                            vector2.X = this.velocity.X;
                            this.collideX = true;
                        }
                        if (this.velocity.Y != velocity.Y)
                        {
                            vector2.Y = this.velocity.Y;
                            this.collideY = true;
                        }
                        this.oldPosition = this.position;
                        this.position += vector2;
                    }
                    else
                    {
                        this.velocity = Collision.TileCollision(this.position, this.velocity, this.width, this.height, fallThrough, fallThrough);
                        if (this.oldVelocity.X != this.velocity.X)
                        {
                            this.collideX = true;
                        }
                        if (this.oldVelocity.Y != this.velocity.Y)
                        {
                            this.collideY = true;
                        }
                        this.oldPosition = this.position;
                        this.position += this.velocity;
                    }
                }
                else
                {
                    this.oldPosition = this.position;
                    this.position += this.velocity;
                }
                if (!this.active)
                {
                    this.netUpdate = true;
                }
                if ((Main.netMode == 2) && this.netUpdate)
                {
                    NetMessage.SendData(0x17, -1, -1, "", i, 0f, 0f, 0f);
                }
                this.FindFrame();
                this.CheckActive();
                this.netUpdate = false;
            }
        }
    }
}

