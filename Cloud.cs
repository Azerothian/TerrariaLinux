namespace Terraria
{
    using Microsoft.Xna.Framework;
    using System;

    public class Cloud
    {
        public bool active = false;
        public int height = 0;
        public Vector2 position;
        private static Random rand = new Random();
        public float rotation;
        public float rSpeed;
        public float scale;
        public float sSpeed;
        public int type = 0;
        public int width = 0;

        public static void addCloud()
        {
            int num2;
            int index = -1;
            for (num2 = 0; num2 < 100; num2++)
            {
                if (!Main.cloud[num2].active)
                {
                    index = num2;
                    break;
                }
            }
            if (index >= 0)
            {
                Main.cloud[index].rSpeed = 0f;
                Main.cloud[index].sSpeed = 0f;
                Main.cloud[index].type = rand.Next(4);
                Main.cloud[index].scale = rand.Next(8, 13) * 0.1f;
                Main.cloud[index].rotation = rand.Next(-10, 11) * 0.01f;
                Main.cloud[index].width = (int) (Main.cloudTexture[Main.cloud[index].type].Width * Main.cloud[index].scale);
                Main.cloud[index].height = (int) (Main.cloudTexture[Main.cloud[index].type].Height * Main.cloud[index].scale);
                if (Main.windSpeed > 0f)
                {
                    Main.cloud[index].position.X = (-Main.cloud[index].width - Main.cloudTexture[Main.cloud[index].type].Width) - rand.Next(Main.screenWidth * 2);
                }
                else
                {
                    Main.cloud[index].position.X = (Main.screenWidth + Main.cloudTexture[Main.cloud[index].type].Width) + rand.Next(Main.screenWidth * 2);
                }
                Main.cloud[index].position.Y = rand.Next((int) (-Main.screenHeight * 0.25f), (int) (Main.screenHeight * 1.25));
                Main.cloud[index].position.Y -= rand.Next((int) (Main.screenHeight * 0.25f));
                Main.cloud[index].position.Y -= rand.Next((int) (Main.screenHeight * 0.25f));
                Cloud cloud1 = Main.cloud[index];
                cloud1.scale *= 2.2f - ((float) ((((double) (Main.cloud[index].position.Y + (Main.screenHeight * 0.25f))) / (Main.screenHeight * 1.5)) + 0.699999988079071));
                if (Main.cloud[index].scale > 1.4)
                {
                    Main.cloud[index].scale = 1.4f;
                }
                if (Main.cloud[index].scale < 0.6)
                {
                    Main.cloud[index].scale = 0.6f;
                }
                Main.cloud[index].active = true;
                Rectangle rectangle = new Rectangle((int) Main.cloud[index].position.X, (int) Main.cloud[index].position.Y, Main.cloud[index].width, Main.cloud[index].height);
                for (num2 = 0; num2 < 100; num2++)
                {
                    if ((index != num2) && Main.cloud[num2].active)
                    {
                        Rectangle rectangle2 = new Rectangle((int) Main.cloud[num2].position.X, (int) Main.cloud[num2].position.Y, Main.cloud[num2].width, Main.cloud[num2].height);
                        if (rectangle.Intersects(rectangle2))
                        {
                            Main.cloud[index].active = false;
                        }
                    }
                }
            }
        }

        public object Clone()
        {
            return base.MemberwiseClone();
        }

        public static void resetClouds()
        {
            if (Main.cloudLimit >= 10)
            {
                int num;
                Main.numClouds = rand.Next(10, Main.cloudLimit);
                Main.windSpeed = 0f;
                while (Main.windSpeed == 0f)
                {
                    Main.windSpeed = rand.Next(-100, 0x65) * 0.01f;
                }
                for (num = 0; num < 100; num++)
                {
                    Main.cloud[num].active = false;
                }
                for (num = 0; num < Main.numClouds; num++)
                {
                    addCloud();
                }
                for (num = 0; num < Main.numClouds; num++)
                {
                    if (Main.windSpeed < 0f)
                    {
                        Main.cloud[num].position.X -= Main.screenWidth * 2;
                    }
                    else
                    {
                        Main.cloud[num].position.X += Main.screenWidth * 2;
                    }
                }
            }
        }

        public void Update()
        {
            if (Main.gameMenu)
            {
                this.position.X += (Main.windSpeed * this.scale) * 3f;
            }
            else
            {
                this.position.X += (Main.windSpeed - (Main.player[Main.myPlayer].velocity.X * 0.1f)) * this.scale;
            }
            if (Main.windSpeed > 0f)
            {
                if ((this.position.X - Main.cloudTexture[this.type].Width) > Main.screenWidth)
                {
                    this.active = false;
                }
            }
            else if (((this.position.X + this.width) + Main.cloudTexture[this.type].Width) < 0f)
            {
                this.active = false;
            }
            this.rSpeed += rand.Next(-10, 11) * 2E-05f;
            if (this.rSpeed > 0.0007)
            {
                this.rSpeed = 0.0007f;
            }
            if (this.rSpeed < -0.0007)
            {
                this.rSpeed = -0.0007f;
            }
            if (this.rotation > 0.05)
            {
                this.rotation = 0.05f;
            }
            if (this.rotation < -0.05)
            {
                this.rotation = -0.05f;
            }
            this.sSpeed += rand.Next(-10, 11) * 2E-05f;
            if (this.sSpeed > 0.0007)
            {
                this.sSpeed = 0.0007f;
            }
            if (this.sSpeed < -0.0007)
            {
                this.sSpeed = -0.0007f;
            }
            if (this.scale > 1.4)
            {
                this.scale = 1.4f;
            }
            if (this.scale < 0.6)
            {
                this.scale = 0.6f;
            }
            this.rotation += this.rSpeed;
            this.scale += this.sSpeed;
            this.width = (int) (Main.cloudTexture[this.type].Width * this.scale);
            this.height = (int) (Main.cloudTexture[this.type].Height * this.scale);
        }

        public static void UpdateClouds()
        {
            int num2;
            int num = 0;
            for (num2 = 0; num2 < 100; num2++)
            {
                if (Main.cloud[num2].active)
                {
                    Main.cloud[num2].Update();
                    num++;
                }
            }
            for (num2 = 0; num2 < 100; num2++)
            {
                if (Main.cloud[num2].active)
                {
                    Cloud cloud;
                    if ((num2 > 1) && (!Main.cloud[num2 - 1].active || (Main.cloud[num2 - 1].scale > (Main.cloud[num2].scale + 0.02))))
                    {
                        cloud = (Cloud) Main.cloud[num2 - 1].Clone();
                        Main.cloud[num2 - 1] = (Cloud) Main.cloud[num2].Clone();
                        Main.cloud[num2] = cloud;
                    }
                    if ((num2 < 0x63) && (!Main.cloud[num2].active || (Main.cloud[num2 + 1].scale < (Main.cloud[num2].scale - 0.02))))
                    {
                        cloud = (Cloud) Main.cloud[num2 + 1].Clone();
                        Main.cloud[num2 + 1] = (Cloud) Main.cloud[num2].Clone();
                        Main.cloud[num2] = cloud;
                    }
                }
            }
            if (num < Main.numClouds)
            {
                addCloud();
            }
        }
    }
}

