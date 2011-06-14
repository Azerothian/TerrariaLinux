namespace Terraria
{
    using Microsoft.Xna.Framework;
    using System;
    using System.Runtime.InteropServices;
    using Microsoft.Xna.Framework.Graphics;

    public class Projectile
    {
        public bool active = false;
        public float[] ai = new float[maxAI];
        public int aiStyle;
        public int alpha;
        public int damage = 0;
        public int direction;
        public bool friendly = false;
        public int height;
        public bool hostile;
        public int identity = 0;
        public bool ignoreWater;
        public float knockBack = 0f;
        public bool lavaWet;
        public float light = 0f;
        public static int maxAI = 2;
        public int maxUpdates = 0;
        public string name = "";
        public bool netUpdate = false;
        public int numUpdates = 0;
        public int owner = 0xff;
        public int penetrate = 1;
        public int[] playerImmune = new int[0xff];
        public Vector2 position;
        public int restrikeDelay = 0;
        public float rotation = 0f;
        public float scale = 1f;
        public int soundDelay = 0;
        public bool tileCollide;
        public int timeLeft = 0;
        public int type = 0;
        public Vector2 velocity;
        public bool wet;
        public byte wetCount = 0;
        public int whoAmI;
        public int width;

        public void AI()
        {
            if (this.aiStyle == 1)
            {
                if (((this.type == 20) || (this.type == 14)) || (this.type == 0x24))
                {
                    if (this.alpha > 0)
                    {
                        this.alpha -= 15;
                    }
                    if (this.alpha < 0)
                    {
                        this.alpha = 0;
                    }
                }
                if ((((this.type != 5) && (this.type != 14)) && ((this.type != 20) && (this.type != 0x24))) && (this.type != 0x26))
                {
                    this.ai[0]++;
                }
                if (this.ai[0] >= 15f)
                {
                    this.ai[0] = 15f;
                    this.velocity.Y += 0.1f;
                }
                this.rotation = ((float) Math.Atan2((double) this.velocity.Y, (double) this.velocity.X)) + 1.57f;
                if (this.velocity.Y > 16f)
                {
                    this.velocity.Y = 16f;
                }
            }
            else if (this.aiStyle == 2)
            {
                this.ai[0]++;
                if (this.ai[0] >= 20f)
                {
                    this.velocity.Y += 0.4f;
                    this.velocity.X *= 0.97f;
                }
                this.rotation += ((Math.Abs(this.velocity.X) + Math.Abs(this.velocity.Y)) * 0.03f) * this.direction;
                if (this.velocity.Y > 16f)
                {
                    this.velocity.Y = 16f;
                }
            }
            else
            {
                int num;
                int num2;
                float num4;
                Vector2 vector;
                float num5;
                float num6;
                float num7;
                Color color;
                if (this.aiStyle == 3)
                {
                    if (this.soundDelay == 0)
                    {
                        this.soundDelay = 8;
                        Main.PlaySound(2, (int) this.position.X, (int) this.position.Y, 7);
                    }
                    if (this.type == 0x13)
                    {
                        for (num = 0; num < 2; num++)
                        {
                            color = new Color();
                            num2 = Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 6, this.velocity.X * 0.2f, this.velocity.Y * 0.2f, 100, color, 2f);
                            Main.dust[num2].noGravity = true;
                            Main.dust[num2].velocity.X *= 0.3f;
                            Main.dust[num2].velocity.Y *= 0.3f;
                        }
                    }
                    else if (this.type == 0x21)
                    {
                        if (Main.rand.Next(1) == 0)
                        {
                            num2 = Dust.NewDust(this.position, this.width, this.height, 40, this.velocity.X * 0.25f, this.velocity.Y * 0.25f, 0, new Color(), 1.4f);
                            Main.dust[num2].noGravity = true;
                        }
                    }
                    else if (Main.rand.Next(5) == 0)
                    {
                        Dust.NewDust(this.position, this.width, this.height, 15, this.velocity.X * 0.5f, this.velocity.Y * 0.5f, 150, new Color(), 0.9f);
                    }
                    if (this.ai[0] == 0f)
                    {
                        this.ai[1]++;
                        if (this.ai[1] >= 30f)
                        {
                            this.ai[0] = 1f;
                            this.ai[1] = 0f;
                            this.netUpdate = true;
                        }
                    }
                    else
                    {
                        this.tileCollide = false;
                        float num3 = 9f;
                        num4 = 0.4f;
                        if (this.type == 0x13)
                        {
                            num3 = 13f;
                            num4 = 0.6f;
                        }
                        else if (this.type == 0x21)
                        {
                            num3 = 15f;
                            num4 = 0.8f;
                        }
                        vector = new Vector2(this.position.X + (this.width * 0.5f), this.position.Y + (this.height * 0.5f));
                        num5 = (Main.player[this.owner].position.X + (Main.player[this.owner].width / 2)) - vector.X;
                        num6 = (Main.player[this.owner].position.Y + (Main.player[this.owner].height / 2)) - vector.Y;
                        num7 = (float) Math.Sqrt((double) ((num5 * num5) + (num6 * num6)));
                        num7 = num3 / num7;
                        num5 *= num7;
                        num6 *= num7;
                        if (this.velocity.X < num5)
                        {
                            this.velocity.X += num4;
                            if ((this.velocity.X < 0f) && (num5 > 0f))
                            {
                                this.velocity.X += num4;
                            }
                        }
                        else if (this.velocity.X > num5)
                        {
                            this.velocity.X -= num4;
                            if ((this.velocity.X > 0f) && (num5 < 0f))
                            {
                                this.velocity.X -= num4;
                            }
                        }
                        if (this.velocity.Y < num6)
                        {
                            this.velocity.Y += num4;
                            if ((this.velocity.Y < 0f) && (num6 > 0f))
                            {
                                this.velocity.Y += num4;
                            }
                        }
                        else if (this.velocity.Y > num6)
                        {
                            this.velocity.Y -= num4;
                            if ((this.velocity.Y > 0f) && (num6 < 0f))
                            {
                                this.velocity.Y -= num4;
                            }
                        }
                        if (Main.myPlayer == this.owner)
                        {
                            Rectangle rectangle = new Rectangle((int) this.position.X, (int) this.position.Y, this.width, this.height);
                            Rectangle rectangle2 = new Rectangle((int) Main.player[this.owner].position.X, (int) Main.player[this.owner].position.Y, Main.player[this.owner].width, Main.player[this.owner].height);
                            if (rectangle.Intersects(rectangle2))
                            {
                                this.Kill();
                            }
                        }
                    }
                    this.rotation += 0.4f * this.direction;
                }
                else
                {
                    int num10;
                    if (this.aiStyle == 4)
                    {
                        this.rotation = ((float) Math.Atan2((double) this.velocity.Y, (double) this.velocity.X)) + 1.57f;
                        if (this.ai[0] == 0f)
                        {
                            this.alpha -= 50;
                            if (this.alpha <= 0)
                            {
                                this.alpha = 0;
                                this.ai[0] = 1f;
                                if (this.ai[1] == 0f)
                                {
                                    this.ai[1]++;
                                    this.position += (Vector2) (this.velocity * 1f);
                                }
                                if ((this.type == 7) && (Main.myPlayer == this.owner))
                                {
                                    int type = this.type;
                                    if (this.ai[1] >= 6f)
                                    {
                                        type++;
                                    }
                                    int index = NewProjectile((this.position.X + this.velocity.X) + (this.width / 2), (this.position.Y + this.velocity.Y) + (this.height / 2), this.velocity.X, this.velocity.Y, type, this.damage, this.knockBack, this.owner);
                                    Main.projectile[index].damage = this.damage;
                                    Main.projectile[index].ai[1] = this.ai[1] + 1f;
                                    NetMessage.SendData(0x1b, -1, -1, "", index, 0f, 0f, 0f);
                                }
                            }
                        }
                        else
                        {
                            if ((this.alpha < 170) && ((this.alpha + 5) >= 170))
                            {
                                num10 = 0;
                                while (num10 < 3)
                                {
                                    color = new Color();
                                    Dust.NewDust(this.position, this.width, this.height, 0x12, this.velocity.X * 0.025f, this.velocity.Y * 0.025f, 170, color, 1.2f);
                                    num10++;
                                }
                                Dust.NewDust(this.position, this.width, this.height, 14, 0f, 0f, 170, new Color(), 1.1f);
                            }
                            this.alpha += 5;
                            if (this.alpha >= 0xff)
                            {
                                this.Kill();
                            }
                        }
                    }
                    else if (this.aiStyle == 5)
                    {
                        if (this.soundDelay == 0)
                        {
                            this.soundDelay = 20 + Main.rand.Next(40);
                            Main.PlaySound(2, (int) this.position.X, (int) this.position.Y, 9);
                        }
                        if (this.ai[0] == 0f)
                        {
                            this.ai[0] = 1f;
                        }
                        this.alpha += (int) (25f * this.ai[0]);
                        if (this.alpha > 200)
                        {
                            this.alpha = 200;
                            this.ai[0] = -1f;
                        }
                        if (this.alpha < 0)
                        {
                            this.alpha = 0;
                            this.ai[0] = 1f;
                        }
                        this.rotation += ((Math.Abs(this.velocity.X) + Math.Abs(this.velocity.Y)) * 0.01f) * this.direction;
                        if (Main.rand.Next(10) == 0)
                        {
                            Dust.NewDust(this.position, this.width, this.height, 15, this.velocity.X * 0.5f, this.velocity.Y * 0.5f, 150, new Color(), 1.2f);
                        }
                        if (Main.rand.Next(20) == 0)
                        {
                            Gore.NewGore(this.position, new Vector2(this.velocity.X * 0.2f, this.velocity.Y * 0.2f), Main.rand.Next(0x10, 0x12));
                        }
                    }
                    else
                    {
                        int num11;
                        int maxTilesX;
                        int num13;
                        int maxTilesY;
                        Vector2 vector2;
                        if (this.aiStyle == 6)
                        {
                            this.velocity = (Vector2) (this.velocity * 0.95f);
                            this.ai[0]++;
                            if (this.ai[0] == 180f)
                            {
                                this.Kill();
                            }
                            if (this.ai[1] == 0f)
                            {
                                this.ai[1] = 1f;
                                for (num = 0; num < 30; num++)
                                {
                                    color = new Color();
                                    Dust.NewDust(this.position, this.width, this.height, 10 + this.type, this.velocity.X, this.velocity.Y, 50, color, 1f);
                                }
                            }
                            if (this.type == 10)
                            {
                                num11 = ((int) (this.position.X / 16f)) - 1;
                                maxTilesX = ((int) ((this.position.X + this.width) / 16f)) + 2;
                                num13 = ((int) (this.position.Y / 16f)) - 1;
                                maxTilesY = ((int) ((this.position.Y + this.height) / 16f)) + 2;
                                if (num11 < 0)
                                {
                                    num11 = 0;
                                }
                                if (maxTilesX > Main.maxTilesX)
                                {
                                    maxTilesX = Main.maxTilesX;
                                }
                                if (num13 < 0)
                                {
                                    num13 = 0;
                                }
                                if (maxTilesY > Main.maxTilesY)
                                {
                                    maxTilesY = Main.maxTilesY;
                                }
                                for (num = num11; num < maxTilesX; num++)
                                {
                                    num10 = num13;
                                    while (num10 < maxTilesY)
                                    {
                                        vector2.X = num * 0x10;
                                        vector2.Y = num10 * 0x10;
                                        if (((((((this.position.X + this.width) > vector2.X) && (this.position.X < (vector2.X + 16f))) && ((this.position.Y + this.height) > vector2.Y)) && (this.position.Y < (vector2.Y + 16f))) && (Main.myPlayer == this.owner)) && Main.tile[num, num10].active)
                                        {
                                            if (Main.tile[num, num10].type == 0x17)
                                            {
                                                Main.tile[num, num10].type = 2;
                                                WorldGen.SquareTileFrame(num, num10, true);
                                                if (Main.netMode == 1)
                                                {
                                                    NetMessage.SendTileSquare(-1, num - 1, num10 - 1, 3);
                                                }
                                            }
                                            if (Main.tile[num, num10].type == 0x19)
                                            {
                                                Main.tile[num, num10].type = 1;
                                                WorldGen.SquareTileFrame(num, num10, true);
                                                if (Main.netMode == 1)
                                                {
                                                    NetMessage.SendTileSquare(-1, num - 1, num10 - 1, 3);
                                                }
                                            }
                                        }
                                        num10++;
                                    }
                                }
                            }
                        }
                        else
                        {
                            float num19;
                            if (this.aiStyle == 7)
                            {
                                if (Main.player[this.owner].dead)
                                {
                                    this.Kill();
                                }
                                else
                                {
                                    vector = new Vector2(this.position.X + (this.width * 0.5f), this.position.Y + (this.height * 0.5f));
                                    num5 = (Main.player[this.owner].position.X + (Main.player[this.owner].width / 2)) - vector.X;
                                    num6 = (Main.player[this.owner].position.Y + (Main.player[this.owner].height / 2)) - vector.Y;
                                    num7 = (float) Math.Sqrt((double) ((num5 * num5) + (num6 * num6)));
                                    this.rotation = ((float) Math.Atan2((double) num6, (double) num5)) - 1.57f;
                                    if (this.ai[0] == 0f)
                                    {
                                        if (((num7 > 300f) && (this.type == 13)) || ((num7 > 400f) && (this.type == 0x20)))
                                        {
                                            this.ai[0] = 1f;
                                        }
                                        num11 = ((int) (this.position.X / 16f)) - 1;
                                        maxTilesX = ((int) ((this.position.X + this.width) / 16f)) + 2;
                                        num13 = ((int) (this.position.Y / 16f)) - 1;
                                        maxTilesY = ((int) ((this.position.Y + this.height) / 16f)) + 2;
                                        if (num11 < 0)
                                        {
                                            num11 = 0;
                                        }
                                        if (maxTilesX > Main.maxTilesX)
                                        {
                                            maxTilesX = Main.maxTilesX;
                                        }
                                        if (num13 < 0)
                                        {
                                            num13 = 0;
                                        }
                                        if (maxTilesY > Main.maxTilesY)
                                        {
                                            maxTilesY = Main.maxTilesY;
                                        }
                                        for (num = num11; num < maxTilesX; num++)
                                        {
                                            num10 = num13;
                                            while (num10 < maxTilesY)
                                            {
                                                if (Main.tile[num, num10] == null)
                                                {
                                                    Main.tile[num, num10] = new Tile();
                                                }
                                                vector2.X = num * 0x10;
                                                vector2.Y = num10 * 0x10;
                                                if ((((((this.position.X + this.width) > vector2.X) && (this.position.X < (vector2.X + 16f))) && ((this.position.Y + this.height) > vector2.Y)) && (this.position.Y < (vector2.Y + 16f))) && (Main.tile[num, num10].active && Main.tileSolid[Main.tile[num, num10].type]))
                                                {
                                                    if (Main.player[this.owner].grapCount < 10)
                                                    {
                                                        Main.player[this.owner].grappling[Main.player[this.owner].grapCount] = this.whoAmI;
                                                        Player player1 = Main.player[this.owner];
                                                        player1.grapCount++;
                                                    }
                                                    if (Main.myPlayer == this.owner)
                                                    {
                                                        int num15 = 0;
                                                        int num16 = -1;
                                                        int timeLeft = 0x186a0;
                                                        for (int i = 0; i < 0x3e8; i++)
                                                        {
                                                            if ((Main.projectile[i].active && (Main.projectile[i].owner == this.owner)) && (Main.projectile[i].aiStyle == 7))
                                                            {
                                                                if (Main.projectile[i].timeLeft < timeLeft)
                                                                {
                                                                    num16 = i;
                                                                    timeLeft = Main.projectile[i].timeLeft;
                                                                }
                                                                num15++;
                                                            }
                                                        }
                                                        if (num15 > 3)
                                                        {
                                                            Main.projectile[num16].Kill();
                                                        }
                                                    }
                                                    WorldGen.KillTile(num, num10, true, true, false);
                                                    Main.PlaySound(0, num * 0x10, num10 * 0x10, 1);
                                                    this.velocity.X = 0f;
                                                    this.velocity.Y = 0f;
                                                    this.ai[0] = 2f;
                                                    this.position.X = ((num * 0x10) + 8) - (this.width / 2);
                                                    this.position.Y = ((num10 * 0x10) + 8) - (this.height / 2);
                                                    this.damage = 0;
                                                    this.netUpdate = true;
                                                    if (Main.myPlayer == this.owner)
                                                    {
                                                        NetMessage.SendData(13, -1, -1, "", this.owner, 0f, 0f, 0f);
                                                    }
                                                    break;
                                                }
                                                num10++;
                                            }
                                            if (this.ai[0] == 2f)
                                            {
                                                break;
                                            }
                                        }
                                    }
                                    else if (this.ai[0] == 1f)
                                    {
                                        num19 = 11f;
                                        if (this.type == 0x20)
                                        {
                                            num19 = 15f;
                                        }
                                        if (num7 < 24f)
                                        {
                                            this.Kill();
                                        }
                                        num7 = num19 / num7;
                                        num5 *= num7;
                                        num6 *= num7;
                                        this.velocity.X = num5;
                                        this.velocity.Y = num6;
                                    }
                                    else if (this.ai[0] == 2f)
                                    {
                                        num11 = ((int) (this.position.X / 16f)) - 1;
                                        maxTilesX = ((int) ((this.position.X + this.width) / 16f)) + 2;
                                        num13 = ((int) (this.position.Y / 16f)) - 1;
                                        maxTilesY = ((int) ((this.position.Y + this.height) / 16f)) + 2;
                                        if (num11 < 0)
                                        {
                                            num11 = 0;
                                        }
                                        if (maxTilesX > Main.maxTilesX)
                                        {
                                            maxTilesX = Main.maxTilesX;
                                        }
                                        if (num13 < 0)
                                        {
                                            num13 = 0;
                                        }
                                        if (maxTilesY > Main.maxTilesY)
                                        {
                                            maxTilesY = Main.maxTilesY;
                                        }
                                        bool flag = true;
                                        for (num = num11; num < maxTilesX; num++)
                                        {
                                            num10 = num13;
                                            while (num10 < maxTilesY)
                                            {
                                                if (Main.tile[num, num10] == null)
                                                {
                                                    Main.tile[num, num10] = new Tile();
                                                }
                                                vector2.X = num * 0x10;
                                                vector2.Y = num10 * 0x10;
                                                if ((((((this.position.X + (this.width / 2)) > vector2.X) && ((this.position.X + (this.width / 2)) < (vector2.X + 16f))) && ((this.position.Y + (this.height / 2)) > vector2.Y)) && ((this.position.Y + (this.height / 2)) < (vector2.Y + 16f))) && (Main.tile[num, num10].active && Main.tileSolid[Main.tile[num, num10].type]))
                                                {
                                                    flag = false;
                                                }
                                                num10++;
                                            }
                                        }
                                        if (flag)
                                        {
                                            this.ai[0] = 1f;
                                        }
                                        else if (Main.player[this.owner].grapCount < 10)
                                        {
                                            Main.player[this.owner].grappling[Main.player[this.owner].grapCount] = this.whoAmI;
                                            Player player2 = Main.player[this.owner];
                                            player2.grapCount++;
                                        }
                                    }
                                }
                            }
                            else if (this.aiStyle == 8)
                            {
                                if (this.type == 0x1b)
                                {
                                    color = new Color();
                                    num2 = Dust.NewDust(new Vector2(this.position.X + this.velocity.X, this.position.Y + this.velocity.Y), this.width, this.height, 0x1d, this.velocity.X, this.velocity.Y, 100, color, 3f);
                                    Main.dust[num2].noGravity = true;
                                    num2 = Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 0x1d, this.velocity.X, this.velocity.Y, 100, new Color(), 1.5f);
                                }
                                else
                                {
                                    for (num = 0; num < 2; num++)
                                    {
                                        color = new Color();
                                        num2 = Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 6, this.velocity.X * 0.2f, this.velocity.Y * 0.2f, 100, color, 2f);
                                        Main.dust[num2].noGravity = true;
                                        Main.dust[num2].velocity.X *= 0.3f;
                                        Main.dust[num2].velocity.Y *= 0.3f;
                                    }
                                }
                                if (this.type != 0x1b)
                                {
                                    this.ai[1]++;
                                }
                                if (this.ai[1] >= 20f)
                                {
                                    this.velocity.Y += 0.2f;
                                }
                                this.rotation += 0.3f * this.direction;
                                if (this.velocity.Y > 16f)
                                {
                                    this.velocity.Y = 16f;
                                }
                            }
                            else if (this.aiStyle == 9)
                            {
                                if (this.type == 0x22)
                                {
                                    color = new Color();
                                    num2 = Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 6, this.velocity.X * 0.2f, this.velocity.Y * 0.2f, 100, color, 3.5f);
                                    Main.dust[num2].noGravity = true;
                                    Dust dust1 = Main.dust[num2];
                                    dust1.velocity = (Vector2) (dust1.velocity * 1.4f);
                                    num2 = Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 6, this.velocity.X * 0.2f, this.velocity.Y * 0.2f, 100, new Color(), 1.5f);
                                }
                                else
                                {
                                    if ((this.soundDelay == 0) && ((Math.Abs(this.velocity.X) + Math.Abs(this.velocity.Y)) > 2f))
                                    {
                                        this.soundDelay = 10;
                                        Main.PlaySound(2, (int) this.position.X, (int) this.position.Y, 9);
                                    }
                                    num2 = Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 15, 0f, 0f, 100, new Color(), 2f);
                                    Dust dust2 = Main.dust[num2];
                                    dust2.velocity = (Vector2) (dust2.velocity * 0.3f);
                                    Main.dust[num2].position.X = ((this.position.X + (this.width / 2)) + 4f) + Main.rand.Next(-4, 5);
                                    Main.dust[num2].position.Y = (this.position.Y + (this.height / 2)) + Main.rand.Next(-4, 5);
                                    Main.dust[num2].noGravity = true;
                                }
                                if ((Main.myPlayer == this.owner) && (this.ai[0] == 0f))
                                {
                                    if (Main.player[this.owner].channel)
                                    {
                                        num4 = 12f;
                                        vector = new Vector2(this.position.X + (this.width * 0.5f), this.position.Y + (this.height * 0.5f));
                                        num5 = (Main.mouseState.X + Main.screenPosition.X) - vector.X;
                                        num6 = (Main.mouseState.Y + Main.screenPosition.Y) - vector.Y;
                                        num7 = (float) Math.Sqrt((double) ((num5 * num5) + (num6 * num6)));
                                        num7 = (float) Math.Sqrt((double) ((num5 * num5) + (num6 * num6)));
                                        if (num7 > num4)
                                        {
                                            num7 = num4 / num7;
                                            num5 *= num7;
                                            num6 *= num7;
                                            if ((num5 != this.velocity.X) || (num6 != this.velocity.Y))
                                            {
                                                this.netUpdate = true;
                                            }
                                            this.velocity.X = num5;
                                            this.velocity.Y = num6;
                                        }
                                        else
                                        {
                                            if ((num5 != this.velocity.X) || (num6 != this.velocity.Y))
                                            {
                                                this.netUpdate = true;
                                            }
                                            this.velocity.X = num5;
                                            this.velocity.Y = num6;
                                        }
                                    }
                                    else
                                    {
                                        this.Kill();
                                    }
                                }
                                if (this.type == 0x22)
                                {
                                    this.rotation += 0.3f * this.direction;
                                }
                                else if ((this.velocity.X != 0f) || (this.velocity.Y != 0f))
                                {
                                    this.rotation = ((float) Math.Atan2((double) this.velocity.Y, (double) this.velocity.X)) - 2.355f;
                                }
                                if (this.velocity.Y > 16f)
                                {
                                    this.velocity.Y = 16f;
                                }
                            }
                            else if (this.aiStyle == 10)
                            {
                                if (this.type == 0x1f)
                                {
                                    if (Main.rand.Next(2) == 0)
                                    {
                                        num2 = Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 0x20, 0f, this.velocity.Y / 2f, 0, new Color(), 1f);
                                        Main.dust[num2].velocity.X *= 0.4f;
                                    }
                                }
                                else if (Main.rand.Next(20) == 0)
                                {
                                    Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 0, 0f, 0f, 0, new Color(), 1f);
                                }
                                if ((Main.myPlayer == this.owner) && (this.ai[0] == 0f))
                                {
                                    if (Main.player[this.owner].channel)
                                    {
                                        num4 = 12f;
                                        vector = new Vector2(this.position.X + (this.width * 0.5f), this.position.Y + (this.height * 0.5f));
                                        num5 = (Main.mouseState.X + Main.screenPosition.X) - vector.X;
                                        num6 = (Main.mouseState.Y + Main.screenPosition.Y) - vector.Y;
                                        num7 = (float) Math.Sqrt((double) ((num5 * num5) + (num6 * num6)));
                                        num7 = (float) Math.Sqrt((double) ((num5 * num5) + (num6 * num6)));
                                        if (num7 > num4)
                                        {
                                            num7 = num4 / num7;
                                            num5 *= num7;
                                            num6 *= num7;
                                            if ((num5 != this.velocity.X) || (num6 != this.velocity.Y))
                                            {
                                                this.netUpdate = true;
                                            }
                                            this.velocity.X = num5;
                                            this.velocity.Y = num6;
                                        }
                                        else
                                        {
                                            if ((num5 != this.velocity.X) || (num6 != this.velocity.Y))
                                            {
                                                this.netUpdate = true;
                                            }
                                            this.velocity.X = num5;
                                            this.velocity.Y = num6;
                                        }
                                    }
                                    else
                                    {
                                        this.ai[0] = 1f;
                                        this.netUpdate = true;
                                    }
                                }
                                if (this.ai[0] == 1f)
                                {
                                    this.velocity.Y += 0.41f;
                                }
                                this.rotation += 0.1f;
                                if (this.velocity.Y > 10f)
                                {
                                    this.velocity.Y = 10f;
                                }
                            }
                            else if (this.aiStyle == 11)
                            {
                                this.rotation += 0.02f;
                                if (Main.myPlayer == this.owner)
                                {
                                    if (!Main.player[this.owner].dead)
                                    {
                                        num4 = 4f;
                                        vector = new Vector2(this.position.X + (this.width * 0.5f), this.position.Y + (this.height * 0.5f));
                                        num5 = (Main.player[this.owner].position.X + (Main.player[this.owner].width / 2)) - vector.X;
                                        num6 = (Main.player[this.owner].position.Y + (Main.player[this.owner].height / 2)) - vector.Y;
                                        num7 = (float) Math.Sqrt((double) ((num5 * num5) + (num6 * num6)));
                                        num7 = (float) Math.Sqrt((double) ((num5 * num5) + (num6 * num6)));
                                        if (num7 > Main.screenWidth)
                                        {
                                            this.position.X = (Main.player[this.owner].position.X + (Main.player[this.owner].width / 2)) - (this.width / 2);
                                            this.position.Y = (Main.player[this.owner].position.Y + (Main.player[this.owner].height / 2)) - (this.height / 2);
                                        }
                                        else if (num7 > 64f)
                                        {
                                            num7 = num4 / num7;
                                            num5 *= num7;
                                            num6 *= num7;
                                            if ((num5 != this.velocity.X) || (num6 != this.velocity.Y))
                                            {
                                                this.netUpdate = true;
                                            }
                                            this.velocity.X = num5;
                                            this.velocity.Y = num6;
                                        }
                                        else
                                        {
                                            if ((this.velocity.X != 0f) || (this.velocity.Y != 0f))
                                            {
                                                this.netUpdate = true;
                                            }
                                            this.velocity.X = 0f;
                                            this.velocity.Y = 0f;
                                        }
                                    }
                                    else
                                    {
                                        this.Kill();
                                    }
                                }
                            }
                            else if (this.aiStyle == 12)
                            {
                                this.scale -= 0.05f;
                                if (this.scale <= 0f)
                                {
                                    this.Kill();
                                }
                                if (this.ai[0] > 4f)
                                {
                                    this.alpha = 150;
                                    this.light = 0.8f;
                                    color = new Color();
                                    num2 = Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 0x1d, this.velocity.X, this.velocity.Y, 100, color, 2.5f);
                                    Main.dust[num2].noGravity = true;
                                    Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 0x1d, this.velocity.X, this.velocity.Y, 100, new Color(), 1.5f);
                                }
                                else
                                {
                                    this.ai[0]++;
                                }
                                this.rotation += 0.3f * this.direction;
                            }
                            else if (this.aiStyle == 13)
                            {
                                if (Main.player[this.owner].dead)
                                {
                                    this.Kill();
                                }
                                else
                                {
                                    Main.player[this.owner].itemAnimation = 5;
                                    Main.player[this.owner].itemTime = 5;
                                    if ((this.position.X + (this.width / 2)) > (Main.player[this.owner].position.X + (Main.player[this.owner].width / 2)))
                                    {
                                        Main.player[this.owner].direction = 1;
                                    }
                                    else
                                    {
                                        Main.player[this.owner].direction = -1;
                                    }
                                    vector = new Vector2(this.position.X + (this.width * 0.5f), this.position.Y + (this.height * 0.5f));
                                    num5 = (Main.player[this.owner].position.X + (Main.player[this.owner].width / 2)) - vector.X;
                                    num6 = (Main.player[this.owner].position.Y + (Main.player[this.owner].height / 2)) - vector.Y;
                                    num7 = (float) Math.Sqrt((double) ((num5 * num5) + (num6 * num6)));
                                    if (this.ai[0] == 0f)
                                    {
                                        if (num7 > 600f)
                                        {
                                            this.ai[0] = 1f;
                                        }
                                        this.rotation = ((float) Math.Atan2((double) this.velocity.Y, (double) this.velocity.X)) + 1.57f;
                                        this.ai[1]++;
                                        if (this.ai[1] > 2f)
                                        {
                                            this.alpha = 0;
                                        }
                                        if (this.ai[1] >= 10f)
                                        {
                                            this.ai[1] = 15f;
                                            this.velocity.Y += 0.3f;
                                        }
                                    }
                                    else if (this.ai[0] == 1f)
                                    {
                                        this.tileCollide = false;
                                        this.rotation = ((float) Math.Atan2((double) num6, (double) num5)) - 1.57f;
                                        num19 = 11f;
                                        if (num7 < 50f)
                                        {
                                            this.Kill();
                                        }
                                        num7 = num19 / num7;
                                        num5 *= num7;
                                        num6 *= num7;
                                        this.velocity.X = num5;
                                        this.velocity.Y = num6;
                                    }
                                }
                            }
                            else if (this.aiStyle == 14)
                            {
                                this.ai[0]++;
                                if (this.ai[0] > 5f)
                                {
                                    this.ai[0] = 5f;
                                    if ((this.velocity.Y == 0f) && (this.velocity.X != 0f))
                                    {
                                        this.velocity.X *= 0.97f;
                                        if ((this.velocity.X > -0.01) && (this.velocity.X < 0.01))
                                        {
                                            this.velocity.X = 0f;
                                            this.netUpdate = true;
                                        }
                                    }
                                    this.velocity.Y += 0.2f;
                                }
                                this.rotation += this.velocity.X * 0.1f;
                            }
                            else if (this.aiStyle == 15)
                            {
                                if (this.type == 0x19)
                                {
                                    if (Main.rand.Next(15) == 0)
                                    {
                                        Dust.NewDust(this.position, this.width, this.height, 14, 0f, 0f, 150, new Color(), 1.3f);
                                    }
                                }
                                else if (this.type == 0x1a)
                                {
                                    num2 = Dust.NewDust(this.position, this.width, this.height, 0x1d, this.velocity.X * 0.4f, this.velocity.Y * 0.4f, 100, new Color(), 2.5f);
                                    Main.dust[num2].noGravity = true;
                                    Main.dust[num2].velocity.X /= 2f;
                                    Main.dust[num2].velocity.Y /= 2f;
                                }
                                else if (this.type == 0x23)
                                {
                                    num2 = Dust.NewDust(this.position, this.width, this.height, 6, this.velocity.X * 0.4f, this.velocity.Y * 0.4f, 100, new Color(), 3f);
                                    Main.dust[num2].noGravity = true;
                                    Main.dust[num2].velocity.X *= 2f;
                                    Main.dust[num2].velocity.Y *= 2f;
                                }
                                if (Main.player[this.owner].dead)
                                {
                                    this.Kill();
                                }
                                else
                                {
                                    Main.player[this.owner].itemAnimation = 5;
                                    Main.player[this.owner].itemTime = 5;
                                    if ((this.position.X + (this.width / 2)) > (Main.player[this.owner].position.X + (Main.player[this.owner].width / 2)))
                                    {
                                        Main.player[this.owner].direction = 1;
                                    }
                                    else
                                    {
                                        Main.player[this.owner].direction = -1;
                                    }
                                    vector = new Vector2(this.position.X + (this.width * 0.5f), this.position.Y + (this.height * 0.5f));
                                    num5 = (Main.player[this.owner].position.X + (Main.player[this.owner].width / 2)) - vector.X;
                                    num6 = (Main.player[this.owner].position.Y + (Main.player[this.owner].height / 2)) - vector.Y;
                                    num7 = (float) Math.Sqrt((double) ((num5 * num5) + (num6 * num6)));
                                    if (this.ai[0] == 0f)
                                    {
                                        this.tileCollide = true;
                                        if (num7 > 300f)
                                        {
                                            this.ai[0] = 1f;
                                        }
                                        else
                                        {
                                            this.ai[1]++;
                                            if (this.ai[1] > 2f)
                                            {
                                                this.alpha = 0;
                                            }
                                            if (this.ai[1] >= 5f)
                                            {
                                                this.ai[1] = 15f;
                                                this.velocity.Y += 0.5f;
                                                this.velocity.X *= 0.95f;
                                            }
                                        }
                                    }
                                    else if (this.ai[0] == 1f)
                                    {
                                        this.tileCollide = false;
                                        num19 = 11f;
                                        if (num7 < 20f)
                                        {
                                            this.Kill();
                                        }
                                        num7 = num19 / num7;
                                        num5 *= num7;
                                        num6 *= num7;
                                        this.velocity.X = num5;
                                        this.velocity.Y = num6;
                                    }
                                    this.rotation += this.velocity.X * 0.03f;
                                }
                            }
                            else if (this.aiStyle == 0x10)
                            {
                                if (((this.owner == Main.myPlayer) && (this.timeLeft <= 3)) && (this.ai[1] == 0f))
                                {
                                    this.ai[1] = 1f;
                                    this.netUpdate = true;
                                }
                                if (this.type == 0x25)
                                {
                                    try
                                    {
                                        num11 = ((int) (this.position.X / 16f)) - 1;
                                        maxTilesX = ((int) ((this.position.X + this.width) / 16f)) + 2;
                                        num13 = ((int) (this.position.Y / 16f)) - 1;
                                        maxTilesY = ((int) ((this.position.Y + this.height) / 16f)) + 2;
                                        if (num11 < 0)
                                        {
                                            num11 = 0;
                                        }
                                        if (maxTilesX > Main.maxTilesX)
                                        {
                                            maxTilesX = Main.maxTilesX;
                                        }
                                        if (num13 < 0)
                                        {
                                            num13 = 0;
                                        }
                                        if (maxTilesY > Main.maxTilesY)
                                        {
                                            maxTilesY = Main.maxTilesY;
                                        }
                                        for (num = num11; num < maxTilesX; num++)
                                        {
                                            for (num10 = num13; num10 < maxTilesY; num10++)
                                            {
                                                if ((Main.tile[num, num10] != null) && (Main.tile[num, num10].active && (Main.tileSolid[Main.tile[num, num10].type] || (Main.tileSolidTop[Main.tile[num, num10].type] && (Main.tile[num, num10].frameY == 0)))))
                                                {
                                                    vector2.X = num * 0x10;
                                                    vector2.Y = num10 * 0x10;
                                                    if ((((((this.position.X + this.width) - 4f) > vector2.X) && ((this.position.X + 4f) < (vector2.X + 16f))) && (((this.position.Y + this.height) - 4f) > vector2.Y)) && ((this.position.Y + 4f) < (vector2.Y + 16f)))
                                                    {
                                                        this.velocity.X = 0f;
                                                        this.velocity.Y = -0.2f;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    catch
                                    {
                                    }
                                }
                                if (this.ai[1] > 0f)
                                {
                                    this.alpha = 0xff;
                                    if ((this.type == 0x1c) || (this.type == 0x25))
                                    {
                                        this.position.X += this.width / 2;
                                        this.position.Y += this.height / 2;
                                        this.width = 0x80;
                                        this.height = 0x80;
                                        this.position.X -= this.width / 2;
                                        this.position.Y -= this.height / 2;
                                        this.damage = 100;
                                        this.knockBack = 8f;
                                    }
                                    else if (this.type == 0x1d)
                                    {
                                        this.position.X += this.width / 2;
                                        this.position.Y += this.height / 2;
                                        this.width = 250;
                                        this.height = 250;
                                        this.position.X -= this.width / 2;
                                        this.position.Y -= this.height / 2;
                                        this.damage = 250;
                                        this.knockBack = 10f;
                                    }
                                    else if (this.type == 30)
                                    {
                                        this.position.X += this.width / 2;
                                        this.position.Y += this.height / 2;
                                        this.width = 0x80;
                                        this.height = 0x80;
                                        this.position.X -= this.width / 2;
                                        this.position.Y -= this.height / 2;
                                        this.knockBack = 8f;
                                    }
                                }
                                else if ((this.type != 30) && (Main.rand.Next(4) == 0))
                                {
                                    if (this.type != 30)
                                    {
                                        this.damage = 0;
                                    }
                                    Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 6, 0f, 0f, 100, new Color(), 1f);
                                }
                                this.ai[0]++;
                                if (((this.type == 30) && (this.ai[0] > 10f)) || ((this.type != 30) && (this.ai[0] > 5f)))
                                {
                                    this.ai[0] = 10f;
                                    if ((this.velocity.Y == 0f) && (this.velocity.X != 0f))
                                    {
                                        this.velocity.X *= 0.97f;
                                        if (this.type == 0x1d)
                                        {
                                            this.velocity.X *= 0.99f;
                                        }
                                        if ((this.velocity.X > -0.01) && (this.velocity.X < 0.01))
                                        {
                                            this.velocity.X = 0f;
                                            this.netUpdate = true;
                                        }
                                    }
                                    this.velocity.Y += 0.2f;
                                }
                                this.rotation += this.velocity.X * 0.1f;
                            }
                        }
                    }
                }
            }
        }

        public Color GetAlpha(Color newColor)
        {
            int r;
            int g;
            int b;
            if (((this.type == 9) || (this.type == 15)) || (this.type == 0x22))
            {
                r = newColor.R - (this.alpha / 3);
                g = newColor.G - (this.alpha / 3);
                b = newColor.B - (this.alpha / 3);
            }
            else if ((this.type == 0x10) || (this.type == 0x12))
            {
                r = newColor.R;
                g = newColor.G;
                b = newColor.B;
            }
            else
            {
                r = newColor.R - this.alpha;
                g = newColor.G - this.alpha;
                b = newColor.B - this.alpha;
            }
            int a = newColor.A - this.alpha;
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

        public void Kill()
        {
            if (this.active)
            {
                int num;
                Color color;
                if (this.type == 1)
                {
                    Main.PlaySound(0, (int) this.position.X, (int) this.position.Y, 1);
                    for (num = 0; num < 10; num++)
                    {
                        color = new Color();
                        Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 7, 0f, 0f, 0, color, 1f);
                    }
                }
                else if (this.type == 2)
                {
                    Main.PlaySound(0, (int) this.position.X, (int) this.position.Y, 1);
                    for (num = 0; num < 20; num++)
                    {
                        color = new Color();
                        Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 6, 0f, 0f, 100, color, 1f);
                    }
                }
                else if (this.type == 3)
                {
                    Main.PlaySound(0, (int) this.position.X, (int) this.position.Y, 1);
                    for (num = 0; num < 10; num++)
                    {
                        color = new Color();
                        Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 1, this.velocity.X * 0.1f, this.velocity.Y * 0.1f, 0, color, 0.75f);
                    }
                }
                else if (this.type == 4)
                {
                    Main.PlaySound(0, (int) this.position.X, (int) this.position.Y, 1);
                    for (num = 0; num < 10; num++)
                    {
                        color = new Color();
                        Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 14, 0f, 0f, 150, color, 1.1f);
                    }
                }
                else if (this.type == 5)
                {
                    Main.PlaySound(2, (int) this.position.X, (int) this.position.Y, 10);
                    for (num = 0; num < 60; num++)
                    {
                        color = new Color();
                        Dust.NewDust(this.position, this.width, this.height, 15, this.velocity.X * 0.5f, this.velocity.Y * 0.5f, 150, color, 1.5f);
                    }
                }
                else if ((this.type == 9) || (this.type == 12))
                {
                    Main.PlaySound(2, (int) this.position.X, (int) this.position.Y, 10);
                    for (num = 0; num < 10; num++)
                    {
                        color = new Color();
                        Dust.NewDust(this.position, this.width, this.height, 15, this.velocity.X * 0.1f, this.velocity.Y * 0.1f, 150, color, 1.2f);
                    }
                    for (num = 0; num < 3; num++)
                    {
                        Gore.NewGore(this.position, new Vector2(this.velocity.X * 0.05f, this.velocity.Y * 0.05f), Main.rand.Next(0x10, 0x12));
                    }
                    if ((this.type == 12) && (this.damage < 100))
                    {
                        for (num = 0; num < 10; num++)
                        {
                            color = new Color();
                            Dust.NewDust(this.position, this.width, this.height, 15, this.velocity.X * 0.1f, this.velocity.Y * 0.1f, 150, color, 1.2f);
                        }
                        for (num = 0; num < 3; num++)
                        {
                            Gore.NewGore(this.position, new Vector2(this.velocity.X * 0.05f, this.velocity.Y * 0.05f), Main.rand.Next(0x10, 0x12));
                        }
                    }
                }
                else if (((this.type == 14) || (this.type == 20)) || (this.type == 0x24))
                {
                    Collision.HitTiles(this.position, this.velocity, this.width, this.height);
                    Main.PlaySound(2, (int) this.position.X, (int) this.position.Y, 10);
                }
                else
                {
                    int num2;
                    if ((this.type == 15) || (this.type == 0x22))
                    {
                        Main.PlaySound(2, (int) this.position.X, (int) this.position.Y, 10);
                        for (num = 0; num < 20; num++)
                        {
                            color = new Color();
                            num2 = Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 6, -this.velocity.X * 0.2f, -this.velocity.Y * 0.2f, 100, color, 2f);
                            Main.dust[num2].noGravity = true;
                            Dust dust1 = Main.dust[num2];
                            dust1.velocity = (Vector2) (dust1.velocity * 2f);
                            color = new Color();
                            num2 = Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 6, -this.velocity.X * 0.2f, -this.velocity.Y * 0.2f, 100, color, 1f);
                            Dust dust2 = Main.dust[num2];
                            dust2.velocity = (Vector2) (dust2.velocity * 2f);
                        }
                    }
                    else if (this.type == 0x10)
                    {
                        Main.PlaySound(2, (int) this.position.X, (int) this.position.Y, 10);
                        for (num = 0; num < 20; num++)
                        {
                            color = new Color();
                            num2 = Dust.NewDust(new Vector2(this.position.X - this.velocity.X, this.position.Y - this.velocity.Y), this.width, this.height, 15, 0f, 0f, 100, color, 2f);
                            Main.dust[num2].noGravity = true;
                            Dust dust3 = Main.dust[num2];
                            dust3.velocity = (Vector2) (dust3.velocity * 2f);
                            color = new Color();
                            num2 = Dust.NewDust(new Vector2(this.position.X - this.velocity.X, this.position.Y - this.velocity.Y), this.width, this.height, 15, 0f, 0f, 100, color, 1f);
                        }
                    }
                    else if (this.type == 0x11)
                    {
                        Main.PlaySound(0, (int) this.position.X, (int) this.position.Y, 1);
                        for (num = 0; num < 5; num++)
                        {
                            color = new Color();
                            Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 0, 0f, 0f, 0, color, 1f);
                        }
                    }
                    else if (this.type == 0x1f)
                    {
                        Main.PlaySound(0, (int) this.position.X, (int) this.position.Y, 1);
                        for (num = 0; num < 5; num++)
                        {
                            color = new Color();
                            num2 = Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 0x20, 0f, 0f, 0, color, 1f);
                            Dust dust4 = Main.dust[num2];
                            dust4.velocity = (Vector2) (dust4.velocity * 0.6f);
                        }
                    }
                    else if (this.type == 0x15)
                    {
                        Main.PlaySound(0, (int) this.position.X, (int) this.position.Y, 1);
                        for (num = 0; num < 10; num++)
                        {
                            color = new Color();
                            Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 0x1a, 0f, 0f, 0, color, 0.8f);
                        }
                    }
                    else if (this.type == 0x18)
                    {
                        for (num = 0; num < 10; num++)
                        {
                            color = new Color();
                            Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 1, this.velocity.X * 0.1f, this.velocity.Y * 0.1f, 0, color, 0.75f);
                        }
                    }
                    else if (this.type == 0x1b)
                    {
                        Main.PlaySound(2, (int) this.position.X, (int) this.position.Y, 10);
                        for (num = 0; num < 30; num++)
                        {
                            color = new Color();
                            num2 = Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 0x1d, this.velocity.X * 0.1f, this.velocity.Y * 0.1f, 100, color, 3f);
                            Main.dust[num2].noGravity = true;
                            color = new Color();
                            Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 0x1d, this.velocity.X * 0.1f, this.velocity.Y * 0.1f, 100, color, 2f);
                        }
                    }
                    else if (this.type == 0x26)
                    {
                        for (num = 0; num < 10; num++)
                        {
                            color = new Color();
                            Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 0x2a, this.velocity.X * 0.1f, this.velocity.Y * 0.1f, 0, color, 1f);
                        }
                    }
                    else
                    {
                        int num3;
                        Vector2 vector;
                        if (((this.type == 0x1c) || (this.type == 30)) || (this.type == 0x25))
                        {
                            Main.PlaySound(2, (int) this.position.X, (int) this.position.Y, 14);
                            this.position.X += this.width / 2;
                            this.position.Y += this.height / 2;
                            this.width = 0x16;
                            this.height = 0x16;
                            this.position.X -= this.width / 2;
                            this.position.Y -= this.height / 2;
                            for (num = 0; num < 20; num++)
                            {
                                color = new Color();
                                num2 = Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 0x1f, 0f, 0f, 100, color, 1.5f);
                                Dust dust5 = Main.dust[num2];
                                dust5.velocity = (Vector2) (dust5.velocity * 1.4f);
                            }
                            for (num = 0; num < 10; num++)
                            {
                                color = new Color();
                                num2 = Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 6, 0f, 0f, 100, color, 2.5f);
                                Main.dust[num2].noGravity = true;
                                Dust dust6 = Main.dust[num2];
                                dust6.velocity = (Vector2) (dust6.velocity * 5f);
                                color = new Color();
                                num2 = Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 6, 0f, 0f, 100, color, 1.5f);
                                Dust dust7 = Main.dust[num2];
                                dust7.velocity = (Vector2) (dust7.velocity * 3f);
                            }
                            vector = new Vector2();
                            num3 = Gore.NewGore(new Vector2(this.position.X, this.position.Y), vector, Main.rand.Next(0x3d, 0x40));
                            Gore gore1 = Main.gore[num3];
                            gore1.velocity = (Vector2) (gore1.velocity * 0.4f);
                            Main.gore[num3].velocity.X++;
                            Main.gore[num3].velocity.Y++;
                            vector = new Vector2();
                            num3 = Gore.NewGore(new Vector2(this.position.X, this.position.Y), vector, Main.rand.Next(0x3d, 0x40));
                            Gore gore2 = Main.gore[num3];
                            gore2.velocity = (Vector2) (gore2.velocity * 0.4f);
                            Main.gore[num3].velocity.X--;
                            Main.gore[num3].velocity.Y++;
                            vector = new Vector2();
                            num3 = Gore.NewGore(new Vector2(this.position.X, this.position.Y), vector, Main.rand.Next(0x3d, 0x40));
                            Gore gore3 = Main.gore[num3];
                            gore3.velocity = (Vector2) (gore3.velocity * 0.4f);
                            Main.gore[num3].velocity.X++;
                            Main.gore[num3].velocity.Y--;
                            num3 = Gore.NewGore(new Vector2(this.position.X, this.position.Y), new Vector2(), Main.rand.Next(0x3d, 0x40));
                            Gore gore4 = Main.gore[num3];
                            gore4.velocity = (Vector2) (gore4.velocity * 0.4f);
                            Main.gore[num3].velocity.X--;
                            Main.gore[num3].velocity.Y--;
                        }
                        else if (this.type == 0x1d)
                        {
                            Main.PlaySound(2, (int) this.position.X, (int) this.position.Y, 14);
                            this.position.X += this.width / 2;
                            this.position.Y += this.height / 2;
                            this.width = 200;
                            this.height = 200;
                            this.position.X -= this.width / 2;
                            this.position.Y -= this.height / 2;
                            for (num = 0; num < 50; num++)
                            {
                                color = new Color();
                                num2 = Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 0x1f, 0f, 0f, 100, color, 2f);
                                Dust dust8 = Main.dust[num2];
                                dust8.velocity = (Vector2) (dust8.velocity * 1.4f);
                            }
                            for (num = 0; num < 80; num++)
                            {
                                color = new Color();
                                num2 = Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 6, 0f, 0f, 100, color, 3f);
                                Main.dust[num2].noGravity = true;
                                Dust dust9 = Main.dust[num2];
                                dust9.velocity = (Vector2) (dust9.velocity * 5f);
                                color = new Color();
                                num2 = Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 6, 0f, 0f, 100, color, 2f);
                                Dust dust10 = Main.dust[num2];
                                dust10.velocity = (Vector2) (dust10.velocity * 3f);
                            }
                            num = 0;
                            while (num < 2)
                            {
                                vector = new Vector2();
                                num3 = Gore.NewGore(new Vector2((this.position.X + (this.width / 2)) - 24f, (this.position.Y + (this.height / 2)) - 24f), vector, Main.rand.Next(0x3d, 0x40));
                                Main.gore[num3].scale = 1.5f;
                                Main.gore[num3].velocity.X += 1.5f;
                                Main.gore[num3].velocity.Y += 1.5f;
                                vector = new Vector2();
                                num3 = Gore.NewGore(new Vector2((this.position.X + (this.width / 2)) - 24f, (this.position.Y + (this.height / 2)) - 24f), vector, Main.rand.Next(0x3d, 0x40));
                                Main.gore[num3].scale = 1.5f;
                                Main.gore[num3].velocity.X -= 1.5f;
                                Main.gore[num3].velocity.Y += 1.5f;
                                vector = new Vector2();
                                num3 = Gore.NewGore(new Vector2((this.position.X + (this.width / 2)) - 24f, (this.position.Y + (this.height / 2)) - 24f), vector, Main.rand.Next(0x3d, 0x40));
                                Main.gore[num3].scale = 1.5f;
                                Main.gore[num3].velocity.X += 1.5f;
                                Main.gore[num3].velocity.Y -= 1.5f;
                                vector = new Vector2();
                                num3 = Gore.NewGore(new Vector2((this.position.X + (this.width / 2)) - 24f, (this.position.Y + (this.height / 2)) - 24f), vector, Main.rand.Next(0x3d, 0x40));
                                Main.gore[num3].scale = 1.5f;
                                Main.gore[num3].velocity.X -= 1.5f;
                                Main.gore[num3].velocity.Y -= 1.5f;
                                num++;
                            }
                            this.position.X += this.width / 2;
                            this.position.Y += this.height / 2;
                            this.width = 10;
                            this.height = 10;
                            this.position.X -= this.width / 2;
                            this.position.Y -= this.height / 2;
                        }
                    }
                }
                if (this.owner == Main.myPlayer)
                {
                    if (((this.type == 0x1c) || (this.type == 0x1d)) || (this.type == 0x25))
                    {
                        int num9;
                        float num10;
                        float num11;
                        int num4 = 3;
                        if (this.type == 0x1d)
                        {
                            num4 = 7;
                        }
                        int num5 = ((int) (this.position.X / 16f)) - num4;
                        int maxTilesX = ((int) (this.position.X / 16f)) + num4;
                        int num7 = ((int) (this.position.Y / 16f)) - num4;
                        int maxTilesY = ((int) (this.position.Y / 16f)) + num4;
                        if (num5 < 0)
                        {
                            num5 = 0;
                        }
                        if (maxTilesX > Main.maxTilesX)
                        {
                            maxTilesX = Main.maxTilesX;
                        }
                        if (num7 < 0)
                        {
                            num7 = 0;
                        }
                        if (maxTilesY > Main.maxTilesY)
                        {
                            maxTilesY = Main.maxTilesY;
                        }
                        bool flag = false;
                        for (num9 = num5; num9 <= maxTilesX; num9++)
                        {
                            num = num7;
                            while (num <= maxTilesY)
                            {
                                num10 = Math.Abs((float) (num9 - (this.position.X / 16f)));
                                num11 = Math.Abs((float) (num - (this.position.Y / 16f)));
                                if (((Math.Sqrt((double) ((num10 * num10) + (num11 * num11))) < num4) && (Main.tile[num9, num] != null)) && (Main.tile[num9, num].wall == 0))
                                {
                                    flag = true;
                                    goto Label_181B;
                                }
                                num++;
                            }
                        Label_181B:;
                        }
                        for (num9 = num5; num9 <= maxTilesX; num9++)
                        {
                            for (num = num7; num <= maxTilesY; num++)
                            {
                                num10 = Math.Abs((float) (num9 - (this.position.X / 16f)));
                                num11 = Math.Abs((float) (num - (this.position.Y / 16f)));
                                if (Math.Sqrt((double) ((num10 * num10) + (num11 * num11))) < num4)
                                {
                                    bool flag2 = true;
                                    if ((Main.tile[num9, num] != null) && Main.tile[num9, num].active)
                                    {
                                        flag2 = false;
                                        if ((this.type == 0x1c) || (this.type == 0x25))
                                        {
                                            if ((((((!Main.tileSolid[Main.tile[num9, num].type] || Main.tileSolidTop[Main.tile[num9, num].type]) || ((Main.tile[num9, num].type == 0) || (Main.tile[num9, num].type == 1))) || (((Main.tile[num9, num].type == 2) || (Main.tile[num9, num].type == 0x17)) || ((Main.tile[num9, num].type == 30) || (Main.tile[num9, num].type == 40)))) || ((((Main.tile[num9, num].type == 6) || (Main.tile[num9, num].type == 7)) || ((Main.tile[num9, num].type == 8) || (Main.tile[num9, num].type == 9))) || (((Main.tile[num9, num].type == 10) || (Main.tile[num9, num].type == 0x35)) || ((Main.tile[num9, num].type == 0x36) || (Main.tile[num9, num].type == 0x39))))) || (((((Main.tile[num9, num].type == 0x3b) || (Main.tile[num9, num].type == 60)) || ((Main.tile[num9, num].type == 0x3f) || (Main.tile[num9, num].type == 0x40))) || (((Main.tile[num9, num].type == 0x41) || (Main.tile[num9, num].type == 0x42)) || ((Main.tile[num9, num].type == 0x43) || (Main.tile[num9, num].type == 0x44)))) || (Main.tile[num9, num].type == 70))) || (Main.tile[num9, num].type == 0x25))
                                            {
                                                flag2 = true;
                                            }
                                        }
                                        else if (this.type == 0x1d)
                                        {
                                            flag2 = true;
                                        }
                                        if (((Main.tileDungeon[Main.tile[num9, num].type] || (Main.tile[num9, num].type == 0x1a)) || (Main.tile[num9, num].type == 0x3a)) || (Main.tile[num9, num].type == 0x15))
                                        {
                                            flag2 = false;
                                        }
                                        if (flag2)
                                        {
                                            WorldGen.KillTile(num9, num, false, false, false);
                                            if (!Main.tile[num9, num].active && (Main.netMode == 1))
                                            {
                                                NetMessage.SendData(0x11, -1, -1, "", 0, (float) num9, (float) num, 0f);
                                            }
                                        }
                                    }
                                    if ((flag2 && (Main.tile[num9, num] != null)) && ((Main.tile[num9, num].wall > 0) && flag))
                                    {
                                        WorldGen.KillWall(num9, num, false);
                                        if ((Main.tile[num9, num].wall == 0) && (Main.netMode == 1))
                                        {
                                            NetMessage.SendData(0x11, -1, -1, "", 2, (float) num9, (float) num, 0f);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (Main.netMode != 0)
                    {
                        NetMessage.SendData(0x1d, -1, -1, "", this.identity, (float) this.owner, 0f, 0f);
                    }
                    int number = -1;
                    if (this.aiStyle == 10)
                    {
                        int i = (((int) this.position.X) + (this.width / 2)) / 0x10;
                        int j = (((int) this.position.Y) + (this.width / 2)) / 0x10;
                        int type = 0;
                        int num17 = 2;
                        if (this.type == 0x1f)
                        {
                            type = 0x35;
                            num17 = 0xa9;
                        }
                        if (!Main.tile[i, j].active)
                        {
                            WorldGen.PlaceTile(i, j, type, false, true, -1);
                            if (Main.tile[i, j].active && (Main.tile[i, j].type == type))
                            {
                                NetMessage.SendData(0x11, -1, -1, "", 1, (float) i, (float) j, (float) type);
                            }
                            else
                            {
                                number = Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, num17, 1, false);
                            }
                        }
                        else
                        {
                            number = Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, num17, 1, false);
                        }
                    }
                    if ((this.type == 1) && (Main.rand.Next(3) < 2))
                    {
                        number = Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 40, 1, false);
                    }
                    if ((this.type == 2) && (Main.rand.Next(5) < 4))
                    {
                        if (Main.rand.Next(4) == 0)
                        {
                            number = Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x29, 1, false);
                        }
                        else
                        {
                            number = Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 40, 1, false);
                        }
                    }
                    if ((this.type == 3) && (Main.rand.Next(5) < 4))
                    {
                        number = Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x2a, 1, false);
                    }
                    if ((this.type == 4) && (Main.rand.Next(5) < 4))
                    {
                        number = Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x2f, 1, false);
                    }
                    if ((this.type == 12) && (this.damage > 100))
                    {
                        number = Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x4b, 1, false);
                    }
                    if ((this.type == 0x15) && (Main.rand.Next(5) < 4))
                    {
                        number = Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, 0x9a, 1, false);
                    }
                    if ((Main.netMode == 1) && (number >= 0))
                    {
                        NetMessage.SendData(0x15, -1, -1, "", number, 0f, 0f, 0f);
                    }
                }
                this.active = false;
            }
        }

        public static int NewProjectile(float X, float Y, float SpeedX, float SpeedY, int Type, int Damage, float KnockBack, int Owner = 0xff)
        {
            int index = 0x3e8;
            for (int i = 0; i < 0x3e8; i++)
            {
                if (!Main.projectile[i].active)
                {
                    index = i;
                    break;
                }
            }
            if (index != 0x3e8)
            {
                Main.projectile[index].SetDefaults(Type);
                Main.projectile[index].position.X = X - (Main.projectile[index].width * 0.5f);
                Main.projectile[index].position.Y = Y - (Main.projectile[index].height * 0.5f);
                Main.projectile[index].owner = Owner;
                Main.projectile[index].velocity.X = SpeedX;
                Main.projectile[index].velocity.Y = SpeedY;
                Main.projectile[index].damage = Damage;
                Main.projectile[index].knockBack = KnockBack;
                Main.projectile[index].identity = index;
                Main.projectile[index].wet = Collision.WetCollision(Main.projectile[index].position, Main.projectile[index].width, Main.projectile[index].height);
                if ((Main.netMode != 0) && (Owner == Main.myPlayer))
                {
                    NetMessage.SendData(0x1b, -1, -1, "", index, 0f, 0f, 0f);
                }
                if (Owner == Main.myPlayer)
                {
                    if (Type == 0x1c)
                    {
                        Main.projectile[index].timeLeft = 180;
                    }
                    if (Type == 0x1d)
                    {
                        Main.projectile[index].timeLeft = 300;
                    }
                    if (Type == 30)
                    {
                        Main.projectile[index].timeLeft = 180;
                    }
                    if (Type == 0x25)
                    {
                        Main.projectile[index].timeLeft = 180;
                    }
                }
            }
            return index;
        }

        public void SetDefaults(int Type)
        {
            int num;
            for (num = 0; num < maxAI; num++)
            {
                this.ai[num] = 0f;
            }
            for (num = 0; num < 0xff; num++)
            {
                this.playerImmune[num] = 0;
            }
            this.lavaWet = false;
            this.wetCount = 0;
            this.wet = false;
            this.ignoreWater = false;
            this.hostile = false;
            this.netUpdate = false;
            this.numUpdates = 0;
            this.maxUpdates = 0;
            this.identity = 0;
            this.restrikeDelay = 0;
            this.light = 0f;
            this.penetrate = 1;
            this.tileCollide = true;
            this.position = new Vector2();
            this.velocity = new Vector2();
            this.aiStyle = 0;
            this.alpha = 0;
            this.type = Type;
            this.active = true;
            this.rotation = 0f;
            this.scale = 1f;
            this.owner = 0xff;
            this.timeLeft = 0xe10;
            this.name = "";
            this.friendly = false;
            this.damage = 0;
            this.knockBack = 0f;
            if (this.type == 1)
            {
                this.name = "Wooden Arrow";
                this.width = 10;
                this.height = 10;
                this.aiStyle = 1;
                this.friendly = true;
            }
            else if (this.type == 2)
            {
                this.name = "Fire Arrow";
                this.width = 10;
                this.height = 10;
                this.aiStyle = 1;
                this.friendly = true;
                this.light = 1f;
            }
            else if (this.type == 3)
            {
                this.name = "Shuriken";
                this.width = 0x16;
                this.height = 0x16;
                this.aiStyle = 2;
                this.friendly = true;
                this.penetrate = 4;
            }
            else if (this.type == 4)
            {
                this.name = "Unholy Arrow";
                this.width = 10;
                this.height = 10;
                this.aiStyle = 1;
                this.friendly = true;
                this.light = 0.2f;
                this.penetrate = 3;
            }
            else if (this.type == 5)
            {
                this.name = "Jester's Arrow";
                this.width = 10;
                this.height = 10;
                this.aiStyle = 1;
                this.friendly = true;
                this.light = 0.4f;
                this.penetrate = -1;
                this.timeLeft = 40;
                this.alpha = 100;
                this.ignoreWater = true;
            }
            else if (this.type == 6)
            {
                this.name = "Enchanted Boomerang";
                this.width = 0x16;
                this.height = 0x16;
                this.aiStyle = 3;
                this.friendly = true;
                this.penetrate = -1;
            }
            else if ((this.type == 7) || (this.type == 8))
            {
                this.name = "Vilethorn";
                this.width = 0x1c;
                this.height = 0x1c;
                this.aiStyle = 4;
                this.friendly = true;
                this.penetrate = -1;
                this.tileCollide = false;
                this.alpha = 0xff;
                this.ignoreWater = true;
            }
            else if (this.type == 9)
            {
                this.name = "Starfury";
                this.width = 0x18;
                this.height = 0x18;
                this.aiStyle = 5;
                this.friendly = true;
                this.penetrate = 2;
                this.alpha = 50;
                this.scale = 0.8f;
                this.light = 1f;
            }
            else if (this.type == 10)
            {
                this.name = "Purification Powder";
                this.width = 0x40;
                this.height = 0x40;
                this.aiStyle = 6;
                this.friendly = true;
                this.tileCollide = false;
                this.penetrate = -1;
                this.alpha = 0xff;
                this.ignoreWater = true;
            }
            else if (this.type == 11)
            {
                this.name = "Vile Powder";
                this.width = 0x30;
                this.height = 0x30;
                this.aiStyle = 6;
                this.friendly = true;
                this.tileCollide = false;
                this.penetrate = -1;
                this.alpha = 0xff;
                this.ignoreWater = true;
            }
            else if (this.type == 12)
            {
                this.name = "Fallen Star";
                this.width = 0x10;
                this.height = 0x10;
                this.aiStyle = 5;
                this.friendly = true;
                this.penetrate = -1;
                this.alpha = 50;
                this.light = 1f;
            }
            else if (this.type == 13)
            {
                this.name = "Hook";
                this.width = 0x12;
                this.height = 0x12;
                this.aiStyle = 7;
                this.friendly = true;
                this.penetrate = -1;
                this.tileCollide = false;
            }
            else if (this.type == 14)
            {
                this.name = "Musket Ball";
                this.width = 4;
                this.height = 4;
                this.aiStyle = 1;
                this.friendly = true;
                this.penetrate = 1;
                this.light = 0.5f;
                this.alpha = 0xff;
                this.maxUpdates = 1;
                this.scale = 1.2f;
                this.timeLeft = 600;
            }
            else if (this.type == 15)
            {
                this.name = "Ball of Fire";
                this.width = 0x10;
                this.height = 0x10;
                this.aiStyle = 8;
                this.friendly = true;
                this.light = 0.8f;
                this.alpha = 100;
            }
            else if (this.type == 0x10)
            {
                this.name = "Magic Missile";
                this.width = 10;
                this.height = 10;
                this.aiStyle = 9;
                this.friendly = true;
                this.light = 0.8f;
                this.alpha = 100;
            }
            else if (this.type == 0x11)
            {
                this.name = "Dirt Ball";
                this.width = 10;
                this.height = 10;
                this.aiStyle = 10;
                this.friendly = true;
            }
            else if (this.type == 0x12)
            {
                this.name = "Orb of Light";
                this.width = 0x20;
                this.height = 0x20;
                this.aiStyle = 11;
                this.friendly = true;
                this.light = 1f;
                this.alpha = 150;
                this.tileCollide = false;
                this.penetrate = -1;
                this.timeLeft *= 5;
                this.ignoreWater = true;
            }
            else if (this.type == 0x13)
            {
                this.name = "Flamarang";
                this.width = 0x16;
                this.height = 0x16;
                this.aiStyle = 3;
                this.friendly = true;
                this.penetrate = -1;
                this.light = 1f;
            }
            else if (this.type == 20)
            {
                this.name = "Green Laser";
                this.width = 4;
                this.height = 4;
                this.aiStyle = 1;
                this.friendly = true;
                this.penetrate = -1;
                this.light = 0.75f;
                this.alpha = 0xff;
                this.maxUpdates = 2;
                this.scale = 1.4f;
                this.timeLeft = 600;
            }
            else if (this.type == 0x15)
            {
                this.name = "Bone";
                this.width = 0x10;
                this.height = 0x10;
                this.aiStyle = 2;
                this.scale = 1.2f;
                this.friendly = true;
            }
            else if (this.type == 0x16)
            {
                this.name = "Water Stream";
                this.width = 12;
                this.height = 12;
                this.aiStyle = 12;
                this.friendly = true;
                this.alpha = 0xff;
                this.penetrate = -1;
                this.maxUpdates = 1;
                this.ignoreWater = true;
            }
            else if (this.type == 0x17)
            {
                this.name = "Harpoon";
                this.width = 4;
                this.height = 4;
                this.aiStyle = 13;
                this.friendly = true;
                this.penetrate = -1;
                this.alpha = 0xff;
            }
            else if (this.type == 0x18)
            {
                this.name = "Spiky Ball";
                this.width = 14;
                this.height = 14;
                this.aiStyle = 14;
                this.friendly = true;
                this.penetrate = 3;
            }
            else if (this.type == 0x19)
            {
                this.name = "Ball 'O Hurt";
                this.width = 0x16;
                this.height = 0x16;
                this.aiStyle = 15;
                this.friendly = true;
                this.penetrate = -1;
            }
            else if (this.type == 0x1a)
            {
                this.name = "Blue Moon";
                this.width = 0x16;
                this.height = 0x16;
                this.aiStyle = 15;
                this.friendly = true;
                this.penetrate = -1;
            }
            else if (this.type == 0x1b)
            {
                this.name = "Water Bolt";
                this.width = 0x10;
                this.height = 0x10;
                this.aiStyle = 8;
                this.friendly = true;
                this.light = 0.8f;
                this.alpha = 200;
                this.timeLeft /= 2;
                this.penetrate = 10;
            }
            else if (this.type == 0x1c)
            {
                this.name = "Bomb";
                this.width = 0x16;
                this.height = 0x16;
                this.aiStyle = 0x10;
                this.friendly = true;
                this.penetrate = -1;
            }
            else if (this.type == 0x1d)
            {
                this.name = "Dynamite";
                this.width = 10;
                this.height = 10;
                this.aiStyle = 0x10;
                this.friendly = true;
                this.penetrate = -1;
            }
            else if (this.type == 30)
            {
                this.name = "Grenade";
                this.width = 14;
                this.height = 14;
                this.aiStyle = 0x10;
                this.friendly = true;
                this.penetrate = -1;
            }
            else if (this.type == 0x1f)
            {
                this.name = "Sand Ball";
                this.knockBack = 6f;
                this.width = 10;
                this.height = 10;
                this.aiStyle = 10;
                this.friendly = true;
                this.hostile = true;
                this.penetrate = -1;
            }
            else if (this.type == 0x20)
            {
                this.name = "Ivy Whip";
                this.width = 0x12;
                this.height = 0x12;
                this.aiStyle = 7;
                this.friendly = true;
                this.penetrate = -1;
                this.tileCollide = false;
            }
            else if (this.type == 0x21)
            {
                this.name = "Thorn Chakrum";
                this.width = 0x1c;
                this.height = 0x1c;
                this.aiStyle = 3;
                this.friendly = true;
                this.scale = 0.9f;
                this.penetrate = -1;
            }
            else if (this.type == 0x22)
            {
                this.name = "Flamelash";
                this.width = 14;
                this.height = 14;
                this.aiStyle = 9;
                this.friendly = true;
                this.light = 0.8f;
                this.alpha = 100;
                this.penetrate = 2;
            }
            else if (this.type == 0x23)
            {
                this.name = "Sunfury";
                this.width = 0x16;
                this.height = 0x16;
                this.aiStyle = 15;
                this.friendly = true;
                this.penetrate = -1;
            }
            else if (this.type == 0x24)
            {
                this.name = "Meteor Shot";
                this.width = 4;
                this.height = 4;
                this.aiStyle = 1;
                this.friendly = true;
                this.penetrate = 2;
                this.light = 0.6f;
                this.alpha = 0xff;
                this.maxUpdates = 1;
                this.scale = 1.4f;
                this.timeLeft = 600;
            }
            else if (this.type == 0x25)
            {
                this.name = "Sticky Bomb";
                this.width = 0x16;
                this.height = 0x16;
                this.aiStyle = 0x10;
                this.friendly = true;
                this.penetrate = -1;
                this.tileCollide = false;
            }
            else if (this.type == 0x26)
            {
                this.name = "Harpy Feather";
                this.width = 14;
                this.height = 14;
                this.aiStyle = 0;
                this.hostile = true;
                this.penetrate = -1;
                this.aiStyle = 1;
                this.tileCollide = true;
            }
            else
            {
                this.active = false;
            }
            this.width = (int) (this.width * this.scale);
            this.height = (int) (this.height * this.scale);
        }

        public void Update(int i)
        {
            if (this.active)
            {
                Vector2 velocity = this.velocity;
                if ((((this.position.X <= Main.leftWorld) || ((this.position.X + this.width) >= Main.rightWorld)) || (this.position.Y <= Main.topWorld)) || ((this.position.Y + this.height) >= Main.bottomWorld))
                {
                    this.active = false;
                }
                else
                {
                    int myPlayer;
                    this.whoAmI = i;
                    if (this.soundDelay > 0)
                    {
                        this.soundDelay--;
                    }
                    this.netUpdate = false;
                    for (myPlayer = 0; myPlayer < 0xff; myPlayer++)
                    {
                        if (this.playerImmune[myPlayer] > 0)
                        {
                            this.playerImmune[myPlayer]--;
                        }
                    }
                    this.AI();
                    if ((this.owner < 0xff) && !Main.player[this.owner].active)
                    {
                        this.Kill();
                    }
                    if (!this.ignoreWater)
                    {
                        bool flag;
                        bool flag2;
                        int num2;
                        int num3;
                        Color color;
                        try
                        {
                            flag = Collision.LavaCollision(this.position, this.width, this.height);
                            flag2 = Collision.WetCollision(this.position, this.width, this.height);
                            if (flag)
                            {
                                this.lavaWet = true;
                            }
                        }
                        catch
                        {
                            this.active = false;
                            return;
                        }
                        if (flag2)
                        {
                            if (this.wetCount == 0)
                            {
                                this.wetCount = 10;
                                if (!this.wet)
                                {
                                    if (!flag)
                                    {
                                        for (num2 = 0; num2 < 10; num2++)
                                        {
                                            color = new Color();
                                            num3 = Dust.NewDust(new Vector2(this.position.X - 6f, (this.position.Y + (this.height / 2)) - 8f), this.width + 12, 0x18, 0x21, 0f, 0f, 0, color, 1f);
                                            Main.dust[num3].velocity.Y -= 4f;
                                            Main.dust[num3].velocity.X *= 2.5f;
                                            Main.dust[num3].scale = 1.3f;
                                            Main.dust[num3].alpha = 100;
                                            Main.dust[num3].noGravity = true;
                                        }
                                        Main.PlaySound(0x13, (int) this.position.X, (int) this.position.Y, 1);
                                    }
                                    else
                                    {
                                        num2 = 0;
                                        while (num2 < 10)
                                        {
                                            color = new Color();
                                            num3 = Dust.NewDust(new Vector2(this.position.X - 6f, (this.position.Y + (this.height / 2)) - 8f), this.width + 12, 0x18, 0x23, 0f, 0f, 0, color, 1f);
                                            Main.dust[num3].velocity.Y -= 1.5f;
                                            Main.dust[num3].velocity.X *= 2.5f;
                                            Main.dust[num3].scale = 1.3f;
                                            Main.dust[num3].alpha = 100;
                                            Main.dust[num3].noGravity = true;
                                            num2++;
                                        }
                                        Main.PlaySound(0x13, (int) this.position.X, (int) this.position.Y, 1);
                                    }
                                }
                                this.wet = true;
                            }
                        }
                        else if (this.wet)
                        {
                            this.wet = false;
                            if (this.wetCount == 0)
                            {
                                this.wetCount = 10;
                                if (!this.lavaWet)
                                {
                                    for (num2 = 0; num2 < 10; num2++)
                                    {
                                        color = new Color();
                                        num3 = Dust.NewDust(new Vector2(this.position.X - 6f, this.position.Y + (this.height / 2)), this.width + 12, 0x18, 0x21, 0f, 0f, 0, color, 1f);
                                        Main.dust[num3].velocity.Y -= 4f;
                                        Main.dust[num3].velocity.X *= 2.5f;
                                        Main.dust[num3].scale = 1.3f;
                                        Main.dust[num3].alpha = 100;
                                        Main.dust[num3].noGravity = true;
                                    }
                                    Main.PlaySound(0x13, (int) this.position.X, (int) this.position.Y, 1);
                                }
                                else
                                {
                                    for (num2 = 0; num2 < 10; num2++)
                                    {
                                        color = new Color();
                                        num3 = Dust.NewDust(new Vector2(this.position.X - 6f, (this.position.Y + (this.height / 2)) - 8f), this.width + 12, 0x18, 0x23, 0f, 0f, 0, color, 1f);
                                        Main.dust[num3].velocity.Y -= 1.5f;
                                        Main.dust[num3].velocity.X *= 2.5f;
                                        Main.dust[num3].scale = 1.3f;
                                        Main.dust[num3].alpha = 100;
                                        Main.dust[num3].noGravity = true;
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
                    }
                    if (this.tileCollide)
                    {
                        Vector2 vector2 = this.velocity;
                        bool fallThrough = true;
                        if ((((this.type == 9) || (this.type == 12)) || ((this.type == 15) || (this.type == 13))) || (this.type == 0x1f))
                        {
                            fallThrough = false;
                        }
                        if (this.aiStyle == 10)
                        {
                            this.velocity = Collision.AnyCollision(this.position, this.velocity, this.width, this.height);
                        }
                        else if (this.wet)
                        {
                            Vector2 vector3 = this.velocity;
                            this.velocity = Collision.TileCollision(this.position, this.velocity, this.width, this.height, fallThrough, fallThrough);
                            velocity = (Vector2) (this.velocity * 0.5f);
                            if (this.velocity.X != vector3.X)
                            {
                                velocity.X = this.velocity.X;
                            }
                            if (this.velocity.Y != vector3.Y)
                            {
                                velocity.Y = this.velocity.Y;
                            }
                        }
                        else
                        {
                            this.velocity = Collision.TileCollision(this.position, this.velocity, this.width, this.height, fallThrough, fallThrough);
                        }
                        if (vector2 != this.velocity)
                        {
                            if (this.type == 0x24)
                            {
                                if (this.penetrate > 1)
                                {
                                    Collision.HitTiles(this.position, this.velocity, this.width, this.height);
                                    Main.PlaySound(2, (int) this.position.X, (int) this.position.Y, 10);
                                    this.penetrate--;
                                    if (this.velocity.X != vector2.X)
                                    {
                                        this.velocity.X = -vector2.X;
                                    }
                                    if (this.velocity.Y != vector2.Y)
                                    {
                                        this.velocity.Y = -vector2.Y;
                                    }
                                }
                                else
                                {
                                    this.Kill();
                                }
                            }
                            else if (((this.aiStyle == 3) || (this.aiStyle == 13)) || (this.aiStyle == 15))
                            {
                                Collision.HitTiles(this.position, this.velocity, this.width, this.height);
                                if (this.type == 0x21)
                                {
                                    if (this.velocity.X != vector2.X)
                                    {
                                        this.velocity.X = -vector2.X;
                                    }
                                    if (this.velocity.Y != vector2.Y)
                                    {
                                        this.velocity.Y = -vector2.Y;
                                    }
                                }
                                else
                                {
                                    this.ai[0] = 1f;
                                    if (this.aiStyle == 3)
                                    {
                                        this.velocity.X = -vector2.X;
                                        this.velocity.Y = -vector2.Y;
                                    }
                                }
                                this.netUpdate = true;
                                Main.PlaySound(0, (int) this.position.X, (int) this.position.Y, 1);
                            }
                            else if (this.aiStyle == 8)
                            {
                                Main.PlaySound(2, (int) this.position.X, (int) this.position.Y, 10);
                                this.ai[0]++;
                                if (this.ai[0] >= 5f)
                                {
                                    this.position += this.velocity;
                                    this.Kill();
                                }
                                else
                                {
                                    if (this.velocity.Y != vector2.Y)
                                    {
                                        this.velocity.Y = -vector2.Y;
                                    }
                                    if (this.velocity.X != vector2.X)
                                    {
                                        this.velocity.X = -vector2.X;
                                    }
                                }
                            }
                            else if (this.aiStyle == 14)
                            {
                                if (this.velocity.X != vector2.X)
                                {
                                    this.velocity.X = vector2.X * -0.5f;
                                }
                                if ((this.velocity.Y != vector2.Y) && (vector2.Y > 1f))
                                {
                                    this.velocity.Y = vector2.Y * -0.5f;
                                }
                            }
                            else if (this.aiStyle == 0x10)
                            {
                                if (this.velocity.X != vector2.X)
                                {
                                    this.velocity.X = vector2.X * -0.4f;
                                    if (this.type == 0x1d)
                                    {
                                        this.velocity.X *= 0.8f;
                                    }
                                }
                                if ((this.velocity.Y != vector2.Y) && (vector2.Y > 0.7))
                                {
                                    this.velocity.Y = vector2.Y * -0.4f;
                                    if (this.type == 0x1d)
                                    {
                                        this.velocity.Y *= 0.8f;
                                    }
                                }
                            }
                            else
                            {
                                this.position += this.velocity;
                                this.Kill();
                            }
                        }
                    }
                    if ((this.type != 7) && (this.type != 8))
                    {
                        if (this.wet)
                        {
                            this.position += velocity;
                        }
                        else
                        {
                            this.position += this.velocity;
                        }
                    }
                    if (((((this.aiStyle != 3) || (this.ai[0] != 1f)) && ((this.aiStyle != 7) || (this.ai[0] != 1f))) && ((this.aiStyle != 13) || (this.ai[0] != 1f))) && ((this.aiStyle != 15) || (this.ai[0] != 1f)))
                    {
                        if (this.velocity.X < 0f)
                        {
                            this.direction = -1;
                        }
                        else
                        {
                            this.direction = 1;
                        }
                    }
                    if (this.active)
                    {
                        Rectangle rectangle2;
                        if (this.light > 0f)
                        {
                            Lighting.addLight((int) ((this.position.X + (this.width / 2)) / 16f), (int) ((this.position.Y + (this.height / 2)) / 16f), this.light);
                        }
                        if (this.type == 2)
                        {
                            Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 6, 0f, 0f, 100, new Color(), 1f);
                        }
                        else if (this.type == 4)
                        {
                            if (Main.rand.Next(5) == 0)
                            {
                                Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 14, 0f, 0f, 150, new Color(), 1.1f);
                            }
                        }
                        else if (this.type == 5)
                        {
                            Dust.NewDust(this.position, this.width, this.height, 15, this.velocity.X * 0.5f, this.velocity.Y * 0.5f, 150, new Color(), 1.2f);
                        }
                        Rectangle rectangle = new Rectangle((int) this.position.X, (int) this.position.Y, this.width, this.height);
                        if ((this.hostile && (Main.myPlayer < 0xff)) && (this.damage > 0))
                        {
                            myPlayer = Main.myPlayer;
                            if ((Main.player[myPlayer].active && !Main.player[myPlayer].dead) && !Main.player[myPlayer].immune)
                            {
                                rectangle2 = new Rectangle((int) Main.player[myPlayer].position.X, (int) Main.player[myPlayer].position.Y, Main.player[myPlayer].width, Main.player[myPlayer].height);
                                if (rectangle.Intersects(rectangle2))
                                {
                                    int direction = this.direction;
                                    if ((Main.player[myPlayer].position.X + (Main.player[myPlayer].width / 2)) < (this.position.X + (this.width / 2)))
                                    {
                                        direction = -1;
                                    }
                                    else
                                    {
                                        direction = 1;
                                    }
                                    Main.player[myPlayer].Hurt(this.damage * 2, direction, false, false);
                                    if (Main.netMode != 0)
                                    {
                                        NetMessage.SendData(0x1a, -1, -1, "", myPlayer, (float) this.direction, (float) (this.damage * 2), 0f);
                                    }
                                }
                            }
                        }
                        if (this.friendly && (this.type != 0x12))
                        {
                            Rectangle rectangle3;
                            if (this.owner == Main.myPlayer)
                            {
                                if ((this.aiStyle == 0x10) && (this.ai[1] > 0f))
                                {
                                    for (myPlayer = 0; myPlayer < 0xff; myPlayer++)
                                    {
                                        if ((Main.player[myPlayer].active && !Main.player[myPlayer].dead) && !Main.player[myPlayer].immune)
                                        {
                                            rectangle2 = new Rectangle((int) Main.player[myPlayer].position.X, (int) Main.player[myPlayer].position.Y, Main.player[myPlayer].width, Main.player[myPlayer].height);
                                            if (rectangle.Intersects(rectangle2))
                                            {
                                                if ((Main.player[myPlayer].position.X + (Main.player[myPlayer].width / 2)) < (this.position.X + (this.width / 2)))
                                                {
                                                    this.direction = -1;
                                                }
                                                else
                                                {
                                                    this.direction = 1;
                                                }
                                                Main.player[myPlayer].Hurt(this.damage, this.direction, true, false);
                                                if (Main.netMode != 0)
                                                {
                                                    NetMessage.SendData(0x1a, -1, -1, "", myPlayer, (float) this.direction, (float) this.damage, 1f);
                                                }
                                            }
                                        }
                                    }
                                }
                                int num5 = (int) (this.position.X / 16f);
                                int maxTilesX = ((int) ((this.position.X + this.width) / 16f)) + 1;
                                int num7 = (int) (this.position.Y / 16f);
                                int maxTilesY = ((int) ((this.position.Y + this.height) / 16f)) + 1;
                                if (num5 < 0)
                                {
                                    num5 = 0;
                                }
                                if (maxTilesX > Main.maxTilesX)
                                {
                                    maxTilesX = Main.maxTilesX;
                                }
                                if (num7 < 0)
                                {
                                    num7 = 0;
                                }
                                if (maxTilesY > Main.maxTilesY)
                                {
                                    maxTilesY = Main.maxTilesY;
                                }
                                for (int j = num5; j < maxTilesX; j++)
                                {
                                    for (int k = num7; k < maxTilesY; k++)
                                    {
                                        if ((Main.tile[j, k] != null) && ((((((Main.tile[j, k].type == 3) || (Main.tile[j, k].type == 0x18)) || ((Main.tile[j, k].type == 0x1c) || (Main.tile[j, k].type == 0x20))) || (((Main.tile[j, k].type == 0x33) || (Main.tile[j, k].type == 0x34)) || ((Main.tile[j, k].type == 0x3d) || (Main.tile[j, k].type == 0x3e)))) || (((Main.tile[j, k].type == 0x45) || (Main.tile[j, k].type == 0x47)) || (Main.tile[j, k].type == 0x49))) || (Main.tile[j, k].type == 0x4a)))
                                        {
                                            WorldGen.KillTile(j, k, false, false, false);
                                            if (Main.netMode == 1)
                                            {
                                                NetMessage.SendData(0x11, -1, -1, "", 0, (float) j, (float) k, 0f);
                                            }
                                        }
                                    }
                                }
                                if (this.damage > 0)
                                {
                                    for (myPlayer = 0; myPlayer < 0x3e8; myPlayer++)
                                    {
                                        if ((Main.npc[myPlayer].active && !Main.npc[myPlayer].friendly) && ((this.owner < 0) || (Main.npc[myPlayer].immune[this.owner] == 0)))
                                        {
                                            bool flag4 = false;
                                            if ((this.type == 11) && ((Main.npc[myPlayer].type == 0x2f) || (Main.npc[myPlayer].type == 0x39)))
                                            {
                                                flag4 = true;
                                            }
                                            if (!flag4)
                                            {
                                                rectangle3 = new Rectangle((int) Main.npc[myPlayer].position.X, (int) Main.npc[myPlayer].position.Y, Main.npc[myPlayer].width, Main.npc[myPlayer].height);
                                                if (rectangle.Intersects(rectangle3))
                                                {
                                                    if (this.aiStyle == 3)
                                                    {
                                                        if (this.ai[0] == 0f)
                                                        {
                                                            this.velocity.X = -this.velocity.X;
                                                            this.velocity.Y = -this.velocity.Y;
                                                            this.netUpdate = true;
                                                        }
                                                        this.ai[0] = 1f;
                                                    }
                                                    else if (this.aiStyle == 0x10)
                                                    {
                                                        if (this.timeLeft > 3)
                                                        {
                                                            this.timeLeft = 3;
                                                        }
                                                        if ((Main.npc[myPlayer].position.X + (Main.npc[myPlayer].width / 2)) < (this.position.X + (this.width / 2)))
                                                        {
                                                            this.direction = -1;
                                                        }
                                                        else
                                                        {
                                                            this.direction = 1;
                                                        }
                                                    }
                                                    Main.npc[myPlayer].StrikeNPC(this.damage, this.knockBack, this.direction);
                                                    if (Main.netMode != 0)
                                                    {
                                                        NetMessage.SendData(0x1c, -1, -1, "", myPlayer, (float) this.damage, this.knockBack, (float) this.direction);
                                                    }
                                                    if (this.penetrate != 1)
                                                    {
                                                        Main.npc[myPlayer].immune[this.owner] = 10;
                                                    }
                                                    if (this.penetrate > 0)
                                                    {
                                                        this.penetrate--;
                                                        if (this.penetrate == 0)
                                                        {
                                                            break;
                                                        }
                                                    }
                                                    if (this.aiStyle == 7)
                                                    {
                                                        this.ai[0] = 1f;
                                                        this.damage = 0;
                                                        this.netUpdate = true;
                                                    }
                                                    else if (this.aiStyle == 13)
                                                    {
                                                        this.ai[0] = 1f;
                                                        this.netUpdate = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if ((this.damage > 0) && Main.player[Main.myPlayer].hostile)
                                {
                                    for (myPlayer = 0; myPlayer < 0xff; myPlayer++)
                                    {
                                        if ((((((myPlayer != this.owner) && Main.player[myPlayer].active) && (!Main.player[myPlayer].dead && !Main.player[myPlayer].immune)) && Main.player[myPlayer].hostile) && (this.playerImmune[myPlayer] <= 0)) && ((Main.player[Main.myPlayer].team == 0) || (Main.player[Main.myPlayer].team != Main.player[myPlayer].team)))
                                        {
                                            rectangle2 = new Rectangle((int) Main.player[myPlayer].position.X, (int) Main.player[myPlayer].position.Y, Main.player[myPlayer].width, Main.player[myPlayer].height);
                                            if (rectangle.Intersects(rectangle2))
                                            {
                                                if (this.aiStyle == 3)
                                                {
                                                    if (this.ai[0] == 0f)
                                                    {
                                                        this.velocity.X = -this.velocity.X;
                                                        this.velocity.Y = -this.velocity.Y;
                                                        this.netUpdate = true;
                                                    }
                                                    this.ai[0] = 1f;
                                                }
                                                else if (this.aiStyle == 0x10)
                                                {
                                                    if (this.timeLeft > 3)
                                                    {
                                                        this.timeLeft = 3;
                                                    }
                                                    if ((Main.player[myPlayer].position.X + (Main.player[myPlayer].width / 2)) < (this.position.X + (this.width / 2)))
                                                    {
                                                        this.direction = -1;
                                                    }
                                                    else
                                                    {
                                                        this.direction = 1;
                                                    }
                                                }
                                                Main.player[myPlayer].Hurt(this.damage, this.direction, true, false);
                                                if (Main.netMode != 0)
                                                {
                                                    NetMessage.SendData(0x1a, -1, -1, "", myPlayer, (float) this.direction, (float) this.damage, 1f);
                                                }
                                                this.playerImmune[myPlayer] = 40;
                                                if (this.penetrate > 0)
                                                {
                                                    this.penetrate--;
                                                    if (this.penetrate == 0)
                                                    {
                                                        break;
                                                    }
                                                }
                                                if (this.aiStyle == 7)
                                                {
                                                    this.ai[0] = 1f;
                                                    this.damage = 0;
                                                    this.netUpdate = true;
                                                }
                                                else if (this.aiStyle == 13)
                                                {
                                                    this.ai[0] = 1f;
                                                    this.netUpdate = true;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if ((this.type == 11) && (Main.netMode != 1))
                            {
                                for (myPlayer = 0; myPlayer < 0x3e8; myPlayer++)
                                {
                                    if (Main.npc[myPlayer].active)
                                    {
                                        if (Main.npc[myPlayer].type == 0x2e)
                                        {
                                            rectangle3 = new Rectangle((int) Main.npc[myPlayer].position.X, (int) Main.npc[myPlayer].position.Y, Main.npc[myPlayer].width, Main.npc[myPlayer].height);
                                            if (rectangle.Intersects(rectangle3))
                                            {
                                                Main.npc[myPlayer].Transform(0x2f);
                                            }
                                        }
                                        else if (Main.npc[myPlayer].type == 0x37)
                                        {
                                            rectangle3 = new Rectangle((int) Main.npc[myPlayer].position.X, (int) Main.npc[myPlayer].position.Y, Main.npc[myPlayer].width, Main.npc[myPlayer].height);
                                            if (rectangle.Intersects(rectangle3))
                                            {
                                                Main.npc[myPlayer].Transform(0x39);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        this.timeLeft--;
                        if (this.timeLeft <= 0)
                        {
                            this.Kill();
                        }
                        if (this.penetrate == 0)
                        {
                            this.Kill();
                        }
                        if ((this.active && this.netUpdate) && (this.owner == Main.myPlayer))
                        {
                            NetMessage.SendData(0x1b, -1, -1, "", i, 0f, 0f, 0f);
                        }
                        if (this.active && (this.maxUpdates > 0))
                        {
                            this.numUpdates--;
                            if (this.numUpdates >= 0)
                            {
                                this.Update(i);
                            }
                            else
                            {
                                this.numUpdates = this.maxUpdates;
                            }
                        }
                        this.netUpdate = false;
                    }
                }
            }
        }
    }
}

