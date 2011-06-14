// Type: Terraria.Dust
// Assembly: TerrariaServer, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// Assembly location: C:\Temp\New Folder\TerrariaServer.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria
{
  public class Dust
  {
    public bool noGravity = false;
    public bool noLight = false;
    public bool active = false;
    public int type = 0;
    public Vector2 position;
    public Vector2 velocity;
    public float scale;
    public float rotation;
    public Color color;
    public int alpha;
    public Rectangle frame;

    public static int NewDust(Vector2 Position, int Width, int Height, int Type, float SpeedX = 0.0f, float SpeedY = 0.0f, int Alpha = 0, Color? newColor = null, float Scale = 1f)
    {
      if (WorldGen.gen || Main.netMode == 2)
      {
        return 0;
      }
      else
      {
        int num = 0;
        for (int index = 0; index < 2000; ++index)
        {
          if (!Main.dust[index].active)
          {
            num = index;
            Main.dust[index].active = true;
            Main.dust[index].type = Type;
            Main.dust[index].noGravity = false;
            Main.dust[index].color = newColor.Value;
            Main.dust[index].alpha = Alpha;
            Main.dust[index].position.X = (float) ((double) Position.X + (double) Main.rand.Next(Width - 4) + 4.0);
            Main.dust[index].position.Y = (float) ((double) Position.Y + (double) Main.rand.Next(Height - 4) + 4.0);
            Main.dust[index].velocity.X = (float) Main.rand.Next(-20, 21) * 0.1f + SpeedX;
            Main.dust[index].velocity.Y = (float) Main.rand.Next(-20, 21) * 0.1f + SpeedY;
            Main.dust[index].frame.X = 10 * Type;
            Main.dust[index].frame.Y = 10 * Main.rand.Next(3);
            Main.dust[index].frame.Width = 8;
            Main.dust[index].frame.Height = 8;
            Main.dust[index].rotation = 0.0f;
            Main.dust[index].scale = (float) (1.0 + (double) Main.rand.Next(-20, 21) * 0.00999999977648258);
            Main.dust[index].scale *= Scale;
            Main.dust[index].noLight = false;
            if (Main.dust[index].type == 6 || Main.dust[index].type == 29)
            {
              Main.dust[index].velocity.Y = (float) Main.rand.Next(-10, 6) * 0.1f;
              Main.dust[index].velocity.X *= 0.3f;
              Main.dust[index].scale *= 0.7f;
            }
            if (Main.dust[index].type == 33)
            {
              Main.dust[index].alpha = 170;
              Main.dust[index].velocity *= 0.5f;
              ++Main.dust[index].velocity.Y;
            }
            if (Main.dust[index].type == 41)
              Main.dust[index].velocity *= 0.0f;
            if (Main.dust[index].type == 34 || Main.dust[index].type == 35)
            {
              Main.dust[index].velocity *= 0.1f;
              Main.dust[index].velocity.Y = -0.5f;
              if (Main.dust[index].type == 34 && !Collision.WetCollision(new Vector2(Main.dust[index].position.X, Main.dust[index].position.Y - 8f), 4, 4))
                Main.dust[index].active = false;
              break;
            }
            else
              break;
          }
        }
        return num;
      }
    }

    public static void UpdateDust()
    {
      for (int index = 0; index < 2000; ++index)
      {
        if (Main.dust[index].active)
        {
          Main.dust[index].position += Main.dust[index].velocity;
          if (Main.dust[index].type == 6 || Main.dust[index].type == 29)
          {
            if (!Main.dust[index].noGravity)
              Main.dust[index].velocity.Y += 0.05f;
            if (!Main.dust[index].noLight)
            {
              float Lightness = Main.dust[index].scale * 1.6f;
              if (Main.dust[index].type == 29)
                Lightness *= 0.3f;
              if ((double) Lightness > 1.0)
                Lightness = 1f;
              Lighting.addLight((int) ((double) Main.dust[index].position.X / 16.0), (int) ((double) Main.dust[index].position.Y / 16.0), Lightness);
            }
          }
          else if (Main.dust[index].type == 14 || Main.dust[index].type == 16 || Main.dust[index].type == 31)
          {
            Main.dust[index].velocity.Y *= 0.98f;
            Main.dust[index].velocity.X *= 0.98f;
          }
          else if (Main.dust[index].type == 32)
          {
            Main.dust[index].scale -= 0.01f;
            Main.dust[index].velocity.X *= 0.96f;
            Main.dust[index].velocity.Y += 0.1f;
          }
          else if (Main.dust[index].type == 15)
          {
            Main.dust[index].velocity.Y *= 0.98f;
            Main.dust[index].velocity.X *= 0.98f;
            float Lightness = Main.dust[index].scale;
            if ((double) Lightness > 1.0)
              Lightness = 1f;
            Lighting.addLight((int) ((double) Main.dust[index].position.X / 16.0), (int) ((double) Main.dust[index].position.Y / 16.0), Lightness);
          }
          else if (Main.dust[index].type == 20 || Main.dust[index].type == 21)
          {
            Main.dust[index].scale += 0.005f;
            Main.dust[index].velocity.Y *= 0.94f;
            Main.dust[index].velocity.X *= 0.94f;
            float Lightness = Main.dust[index].scale * 0.8f;
            if (Main.dust[index].type == 21)
              Lightness = Main.dust[index].scale * 0.4f;
            if ((double) Lightness > 1.0)
              Lightness = 1f;
            Lighting.addLight((int) ((double) Main.dust[index].position.X / 16.0), (int) ((double) Main.dust[index].position.Y / 16.0), Lightness);
          }
          else if (Main.dust[index].type == 27)
          {
            Main.dust[index].velocity *= 0.94f;
            Main.dust[index].scale += 0.002f;
            float Lightness = Main.dust[index].scale;
            if ((double) Lightness > 1.0)
              Lightness = 1f;
            Lighting.addLight((int) ((double) Main.dust[index].position.X / 16.0), (int) ((double) Main.dust[index].position.Y / 16.0), Lightness);
          }
          else if (!Main.dust[index].noGravity && Main.dust[index].type != 41)
            Main.dust[index].velocity.Y += 0.1f;
          if (Main.dust[index].type == 5 && Main.dust[index].noGravity)
            Main.dust[index].scale -= 0.04f;
          if (Main.dust[index].type == 33)
          {
            if (Collision.WetCollision(new Vector2(Main.dust[index].position.X, Main.dust[index].position.Y), 4, 4))
            {
              Main.dust[index].alpha += 20;
              Main.dust[index].scale -= 0.1f;
            }
            Main.dust[index].alpha += 2;
            Main.dust[index].scale -= 0.005f;
            if (Main.dust[index].alpha > (int) byte.MaxValue)
              Main.dust[index].scale = 0.0f;
            Main.dust[index].velocity.X *= 0.93f;
            if ((double) Main.dust[index].velocity.Y > 4.0)
              Main.dust[index].velocity.Y = 4f;
            if (Main.dust[index].noGravity)
            {
              if ((double) Main.dust[index].velocity.X < 0.0)
                Main.dust[index].rotation -= 0.2f;
              else
                Main.dust[index].rotation += 0.2f;
              Main.dust[index].scale += 0.03f;
              Main.dust[index].velocity.X *= 1.05f;
              Main.dust[index].velocity.Y += 0.15f;
            }
          }
          if (Main.dust[index].type == 35 && Main.dust[index].noGravity)
          {
            Main.dust[index].scale += 0.02f;
            if ((double) Main.dust[index].scale < 1.0)
              Main.dust[index].velocity.Y += 0.075f;
            Main.dust[index].velocity.X *= 1.08f;
            if ((double) Main.dust[index].velocity.X > 0.0)
              Main.dust[index].rotation += 0.01f;
            else
              Main.dust[index].rotation -= 0.01f;
          }
          else if (Main.dust[index].type == 34 || Main.dust[index].type == 35)
          {
            if (!Collision.WetCollision(new Vector2(Main.dust[index].position.X, Main.dust[index].position.Y - 8f), 4, 4))
            {
              Main.dust[index].scale = 0.0f;
            }
            else
            {
              Main.dust[index].alpha += Main.rand.Next(2);
              if (Main.dust[index].alpha > (int) byte.MaxValue)
                Main.dust[index].scale = 0.0f;
              Main.dust[index].velocity.Y = -0.5f;
              if (Main.dust[index].type == 34)
              {
                Main.dust[index].scale += 0.005f;
              }
              else
              {
                ++Main.dust[index].alpha;
                Main.dust[index].scale -= 0.01f;
                Main.dust[index].velocity.Y = -0.2f;
              }
              Main.dust[index].velocity.X += (float) Main.rand.Next(-10, 10) * 0.002f;
              if ((double) Main.dust[index].velocity.X < -0.25)
                Main.dust[index].velocity.X = -0.25f;
              if ((double) Main.dust[index].velocity.X > 0.25)
                Main.dust[index].velocity.X = 0.25f;
            }
            if (Main.dust[index].type == 35)
            {
              float Lightness = Main.dust[index].scale * 1.6f;
              if ((double) Lightness > 1.0)
                Lightness = 1f;
              Lighting.addLight((int) ((double) Main.dust[index].position.X / 16.0), (int) ((double) Main.dust[index].position.Y / 16.0), Lightness);
            }
          }
          if (Main.dust[index].type == 41)
          {
            Main.dust[index].velocity.X += (float) Main.rand.Next(-10, 11) * 0.01f;
            Main.dust[index].velocity.Y += (float) Main.rand.Next(-10, 11) * 0.01f;
            if ((double) Main.dust[index].velocity.X > 0.75)
              Main.dust[index].velocity.X = 0.75f;
            if ((double) Main.dust[index].velocity.X < -0.75)
              Main.dust[index].velocity.X = -0.75f;
            if ((double) Main.dust[index].velocity.Y > 0.75)
              Main.dust[index].velocity.Y = 0.75f;
            if ((double) Main.dust[index].velocity.Y < -0.75)
              Main.dust[index].velocity.Y = -0.75f;
            Main.dust[index].scale += 0.007f;
            float Lightness = Main.dust[index].scale * 0.7f;
            if ((double) Lightness > 1.0)
              Lightness = 1f;
            Lighting.addLight((int) ((double) Main.dust[index].position.X / 16.0), (int) ((double) Main.dust[index].position.Y / 16.0), Lightness);
          }
          else
            Main.dust[index].velocity.X *= 0.99f;
          Main.dust[index].rotation += Main.dust[index].velocity.X * 0.5f;
          Main.dust[index].scale -= 0.01f;
          if (Main.dust[index].noGravity)
          {
            Main.dust[index].velocity *= 0.92f;
            Main.dust[index].scale -= 0.04f;
          }
          if ((double) Main.dust[index].position.Y > (double) Main.screenPosition.Y + (double) Main.screenHeight)
            Main.dust[index].active = false;
          if ((double) Main.dust[index].scale < 0.1)
            Main.dust[index].active = false;
        }
      }
    }

    public Color GetAlpha(Color newColor)
    {
      int r;
      int g;
      int b;
      if (this.type == 15 || this.type == 20 || (this.type == 21 || this.type == 29) || this.type == 35 || this.type == 41)
      {
        r = (int) newColor.R - this.alpha / 3;
        g = (int) newColor.G - this.alpha / 3;
        b = (int) newColor.B - this.alpha / 3;
      }
      else
      {
        r = (int) newColor.R - this.alpha;
        g = (int) newColor.G - this.alpha;
        b = (int) newColor.B - this.alpha;
      }
      int a = (int) newColor.A - this.alpha;
      if (a < 0)
        a = 0;
      if (a > (int) byte.MaxValue)
        a = (int) byte.MaxValue;
      return new Color(r, g, b, a);
    }

    public Color GetColor(Color newColor)
    {
      int r = (int) this.color.R - ((int) byte.MaxValue - (int) newColor.R);
      int g = (int) this.color.G - ((int) byte.MaxValue - (int) newColor.G);
      int b = (int) this.color.B - ((int) byte.MaxValue - (int) newColor.B);
      int a = (int) this.color.A - ((int) byte.MaxValue - (int) newColor.A);
      if (r < 0)
        r = 0;
      if (r > (int) byte.MaxValue)
        r = (int) byte.MaxValue;
      if (g < 0)
        g = 0;
      if (g > (int) byte.MaxValue)
        g = (int) byte.MaxValue;
      if (b < 0)
        b = 0;
      if (b > (int) byte.MaxValue)
        b = (int) byte.MaxValue;
      if (a < 0)
        a = 0;
      if (a > (int) byte.MaxValue)
        a = (int) byte.MaxValue;
      return new Color(r, g, b, a);
    }
  }
}
