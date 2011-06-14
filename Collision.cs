// Type: Terraria.Collision
// Assembly: TerrariaServer, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// Assembly location: C:\Temp\New Folder\TerrariaServer.exe

using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria
{
  public class Collision
  {
    public static bool CanHit(Vector2 Position1, int Width1, int Height1, Vector2 Position2, int Width2, int Height2)
    {
      int index1 = (int) (((double) Position1.X + (double) (Width1 / 2)) / 16.0);
      int index2 = (int) (((double) Position1.Y + (double) (Height1 / 2)) / 16.0);
      int num1 = (int) (((double) Position2.X + (double) (Width2 / 2)) / 16.0);
      int num2 = (int) (((double) Position2.Y + (double) (Height2 / 2)) / 16.0);
      do
      {
        int num3 = Math.Abs(index1 - num1);
        int num4 = Math.Abs(index2 - num2);
        if (index1 != num1 || index2 != num2)
        {
          if (num3 > num4)
          {
            if (index1 < num1)
              ++index1;
            else
              --index1;
            if (Main.tile[index1, index2 - 1] == null)
              Main.tile[index1, index2 - 1] = new Tile();
            if (Main.tile[index1, index2 + 1] == null)
              Main.tile[index1, index2 + 1] = new Tile();
            if (Main.tile[index1, index2 - 1].active && Main.tileSolid[(int) Main.tile[index1, index2 - 1].type] && (!Main.tileSolidTop[(int) Main.tile[index1, index2 - 1].type] && Main.tile[index1, index2 + 1].active) && Main.tileSolid[(int) Main.tile[index1, index2 + 1].type] && !Main.tileSolidTop[(int) Main.tile[index1, index2 + 1].type])
              goto label_11;
          }
          else
          {
            if (index2 < num2)
              ++index2;
            else
              --index2;
            if (Main.tile[index1 - 1, index2] == null)
              Main.tile[index1 - 1, index2] = new Tile();
            if (Main.tile[index1 + 1, index2] == null)
              Main.tile[index1 + 1, index2] = new Tile();
            if (Main.tile[index1 - 1, index2].active && Main.tileSolid[(int) Main.tile[index1 - 1, index2].type] && (!Main.tileSolidTop[(int) Main.tile[index1 - 1, index2].type] && Main.tile[index1 + 1, index2].active) && Main.tileSolid[(int) Main.tile[index1 + 1, index2].type] && !Main.tileSolidTop[(int) Main.tile[index1 + 1, index2].type])
              goto label_20;
          }
          if (Main.tile[index1, index2] == null)
            Main.tile[index1, index2] = new Tile();
        }
        else
          goto label_1;
      }
      while (!Main.tile[index1, index2].active || !Main.tileSolid[(int) Main.tile[index1, index2].type] || Main.tileSolidTop[(int) Main.tile[index1, index2].type]);
      goto label_24;
label_1:
      return true;
label_11:
      return false;
label_20:
      return false;
label_24:
      return false;
    }

    public static bool EmptyTile(int i, int j, bool ignoreTiles = false)
    {
      Rectangle rectangle = new Rectangle(i * 16, j * 16, 16, 16);
      if (Main.tile[i, j].active && !ignoreTiles)
      {
        return false;
      }
      else
      {
        for (int index = 0; index < (int) byte.MaxValue; ++index)
        {
          if (Main.player[index].active && rectangle.Intersects(new Rectangle((int) Main.player[index].position.X, (int) Main.player[index].position.Y, Main.player[index].width, Main.player[index].height)))
            return false;
        }
        for (int index = 0; index < 200; ++index)
        {
          if (Main.item[index].active && rectangle.Intersects(new Rectangle((int) Main.item[index].position.X, (int) Main.item[index].position.Y, Main.item[index].width, Main.item[index].height)))
            return false;
        }
        for (int index = 0; index < 1000; ++index)
        {
          if (Main.npc[index].active && rectangle.Intersects(new Rectangle((int) Main.npc[index].position.X, (int) Main.npc[index].position.Y, Main.npc[index].width, Main.npc[index].height)))
            return false;
        }
        return true;
      }
    }

    public static bool DrownCollision(Vector2 Position, int Width, int Height)
    {
      Vector2 vector2_1 = new Vector2(Position.X + (float) (Width / 2), Position.Y + (float) (Height / 2));
      int num1 = 10;
      int num2 = 12;
      if (num1 > Width)
        num1 = Width;
      if (num2 > Height)
        num2 = Height;
      vector2_1 = new Vector2(vector2_1.X - (float) (num1 / 2), Position.Y + 2f);
      int num3 = (int) ((double) Position.X / 16.0) - 1;
      int num4 = (int) (((double) Position.X + (double) Width) / 16.0) + 2;
      int num5 = (int) ((double) Position.Y / 16.0) - 1;
      int num6 = (int) (((double) Position.Y + (double) Height) / 16.0) + 2;
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
          if (Main.tile[index1, index2] != null && (int) Main.tile[index1, index2].liquid > 0)
          {
            Vector2 vector2_2;
            vector2_2.X = (float) (index1 * 16);
            vector2_2.Y = (float) (index2 * 16);
            int num7 = 16;
            float num8 = (float) (256 - (int) Main.tile[index1, index2].liquid) / 32f;
            vector2_2.Y += num8 * 2f;
            int num9 = num7 - (int) ((double) num8 * 2.0);
            if ((double) vector2_1.X + (double) num1 > (double) vector2_2.X && (double) vector2_1.X < (double) vector2_2.X + 16.0 && (double) vector2_1.Y + (double) num2 > (double) vector2_2.Y && (double) vector2_1.Y < (double) vector2_2.Y + (double) num9)
              return true;
          }
        }
      }
      return false;
    }

    public static bool WetCollision(Vector2 Position, int Width, int Height)
    {
      Vector2 vector2_1 = new Vector2(Position.X + (float) (Width / 2), Position.Y + (float) (Height / 2));
      int num1 = 10;
      int num2 = Height / 2;
      if (num1 > Width)
        num1 = Width;
      if (num2 > Height)
        num2 = Height;
      vector2_1 = new Vector2(vector2_1.X - (float) (num1 / 2), vector2_1.Y - (float) (num2 / 2));
      int num3 = (int) ((double) Position.X / 16.0) - 1;
      int num4 = (int) (((double) Position.X + (double) Width) / 16.0) + 2;
      int num5 = (int) ((double) Position.Y / 16.0) - 1;
      int num6 = (int) (((double) Position.Y + (double) Height) / 16.0) + 2;
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
          if (Main.tile[index1, index2] != null && (int) Main.tile[index1, index2].liquid > 0)
          {
            Vector2 vector2_2;
            vector2_2.X = (float) (index1 * 16);
            vector2_2.Y = (float) (index2 * 16);
            int num7 = 16;
            float num8 = (float) (256 - (int) Main.tile[index1, index2].liquid) / 32f;
            vector2_2.Y += num8 * 2f;
            int num9 = num7 - (int) ((double) num8 * 2.0);
            if ((double) vector2_1.X + (double) num1 > (double) vector2_2.X && (double) vector2_1.X < (double) vector2_2.X + 16.0 && (double) vector2_1.Y + (double) num2 > (double) vector2_2.Y && (double) vector2_1.Y < (double) vector2_2.Y + (double) num9)
              return true;
          }
        }
      }
      return false;
    }

    public static bool LavaCollision(Vector2 Position, int Width, int Height)
    {
      int num1 = (int) ((double) Position.X / 16.0) - 1;
      int num2 = (int) (((double) Position.X + (double) Width) / 16.0) + 2;
      int num3 = (int) ((double) Position.Y / 16.0) - 1;
      int num4 = (int) (((double) Position.Y + (double) Height) / 16.0) + 2;
      if (num1 < 0)
        num1 = 0;
      if (num2 > Main.maxTilesX)
        num2 = Main.maxTilesX;
      if (num3 < 0)
        num3 = 0;
      if (num4 > Main.maxTilesY)
        num4 = Main.maxTilesY;
      for (int index1 = num1; index1 < num2; ++index1)
      {
        for (int index2 = num3; index2 < num4; ++index2)
        {
          if (Main.tile[index1, index2] != null && (int) Main.tile[index1, index2].liquid > 0 && Main.tile[index1, index2].lava)
          {
            Vector2 vector2;
            vector2.X = (float) (index1 * 16);
            vector2.Y = (float) (index2 * 16);
            int num5 = 16;
            float num6 = (float) (256 - (int) Main.tile[index1, index2].liquid) / 32f;
            vector2.Y += num6 * 2f;
            int num7 = num5 - (int) ((double) num6 * 2.0);
            if ((double) Position.X + (double) Width > (double) vector2.X && (double) Position.X < (double) vector2.X + 16.0 && (double) Position.Y + (double) Height > (double) vector2.Y && (double) Position.Y < (double) vector2.Y + (double) num7)
              return true;
          }
        }
      }
      return false;
    }

    public static Vector2 TileCollision(Vector2 Position, Vector2 Velocity, int Width, int Height, bool fallThrough = false, bool fall2 = false)
    {
      Vector2 vector2_1 = Velocity;
      Vector2 vector2_2 = Velocity;
      Vector2 vector2_3 = Position + Velocity;
      Vector2 vector2_4 = Position;
      int num1 = (int) ((double) Position.X / 16.0) - 1;
      int num2 = (int) (((double) Position.X + (double) Width) / 16.0) + 2;
      int num3 = (int) ((double) Position.Y / 16.0) - 1;
      int num4 = (int) (((double) Position.Y + (double) Height) / 16.0) + 2;
      int num5 = -1;
      int num6 = -1;
      int num7 = -1;
      int num8 = -1;
      if (num1 < 0)
        num1 = 0;
      if (num2 > Main.maxTilesX)
        num2 = Main.maxTilesX;
      if (num3 < 0)
        num3 = 0;
      if (num4 > Main.maxTilesY)
        num4 = Main.maxTilesY;
      for (int index1 = num1; index1 < num2; ++index1)
      {
        for (int index2 = num3; index2 < num4; ++index2)
        {
          if (Main.tile[index1, index2] != null && Main.tile[index1, index2].active && (Main.tileSolid[(int) Main.tile[index1, index2].type] || Main.tileSolidTop[(int) Main.tile[index1, index2].type] && (int) Main.tile[index1, index2].frameY == 0))
          {
            Vector2 vector2_5;
            vector2_5.X = (float) (index1 * 16);
            vector2_5.Y = (float) (index2 * 16);
            if ((double) vector2_3.X + (double) Width > (double) vector2_5.X && (double) vector2_3.X < (double) vector2_5.X + 16.0 && (double) vector2_3.Y + (double) Height > (double) vector2_5.Y && (double) vector2_3.Y < (double) vector2_5.Y + 16.0)
            {
              if ((double) vector2_4.Y + (double) Height <= (double) vector2_5.Y)
              {
                if (!Main.tileSolidTop[(int) Main.tile[index1, index2].type] || !fallThrough || (double) Velocity.Y > 1.0 && !fall2)
                {
                  num7 = index1;
                  num8 = index2;
                  if (num7 != num5)
                    vector2_1.Y = vector2_5.Y - (vector2_4.Y + (float) Height);
                }
              }
              else if ((double) vector2_4.X + (double) Width <= (double) vector2_5.X && !Main.tileSolidTop[(int) Main.tile[index1, index2].type])
              {
                num5 = index1;
                num6 = index2;
                if (num6 != num8)
                  vector2_1.X = vector2_5.X - (vector2_4.X + (float) Width);
                if (num7 == num5)
                  vector2_1.Y = vector2_2.Y;
              }
              else if ((double) vector2_4.X >= (double) vector2_5.X + 16.0 && !Main.tileSolidTop[(int) Main.tile[index1, index2].type])
              {
                num5 = index1;
                num6 = index2;
                if (num6 != num8)
                  vector2_1.X = vector2_5.X + 16f - vector2_4.X;
                if (num7 == num5)
                  vector2_1.Y = vector2_2.Y;
              }
              else if ((double) vector2_4.Y >= (double) vector2_5.Y + 16.0 && !Main.tileSolidTop[(int) Main.tile[index1, index2].type])
              {
                num7 = index1;
                num8 = index2;
                vector2_1.Y = (float) ((double) vector2_5.Y + 16.0 - (double) vector2_4.Y + 0.00999999977648258);
                if (num8 == num6)
                  vector2_1.X = vector2_2.X + 0.01f;
              }
            }
          }
        }
      }
      return vector2_1;
    }

    public static Vector2 AnyCollision(Vector2 Position, Vector2 Velocity, int Width, int Height)
    {
      Vector2 vector2_1 = Velocity;
      Vector2 vector2_2 = Velocity;
      Vector2 vector2_3 = Position + Velocity;
      Vector2 vector2_4 = Position;
      int num1 = (int) ((double) Position.X / 16.0) - 1;
      int num2 = (int) (((double) Position.X + (double) Width) / 16.0) + 2;
      int num3 = (int) ((double) Position.Y / 16.0) - 1;
      int num4 = (int) (((double) Position.Y + (double) Height) / 16.0) + 2;
      int num5 = -1;
      int num6 = -1;
      int num7 = -1;
      int num8 = -1;
      if (num1 < 0)
        num1 = 0;
      if (num2 > Main.maxTilesX)
        num2 = Main.maxTilesX;
      if (num3 < 0)
        num3 = 0;
      if (num4 > Main.maxTilesY)
        num4 = Main.maxTilesY;
      for (int index1 = num1; index1 < num2; ++index1)
      {
        for (int index2 = num3; index2 < num4; ++index2)
        {
          if (Main.tile[index1, index2] != null && Main.tile[index1, index2].active)
          {
            Vector2 vector2_5;
            vector2_5.X = (float) (index1 * 16);
            vector2_5.Y = (float) (index2 * 16);
            if ((double) vector2_3.X + (double) Width > (double) vector2_5.X && (double) vector2_3.X < (double) vector2_5.X + 16.0 && (double) vector2_3.Y + (double) Height > (double) vector2_5.Y && (double) vector2_3.Y < (double) vector2_5.Y + 16.0)
            {
              if ((double) vector2_4.Y + (double) Height <= (double) vector2_5.Y)
              {
                num7 = index1;
                num8 = index2;
                if (num7 != num5)
                  vector2_1.Y = vector2_5.Y - (vector2_4.Y + (float) Height);
              }
              else if ((double) vector2_4.X + (double) Width <= (double) vector2_5.X && !Main.tileSolidTop[(int) Main.tile[index1, index2].type])
              {
                num5 = index1;
                num6 = index2;
                if (num6 != num8)
                  vector2_1.X = vector2_5.X - (vector2_4.X + (float) Width);
                if (num7 == num5)
                  vector2_1.Y = vector2_2.Y;
              }
              else if ((double) vector2_4.X >= (double) vector2_5.X + 16.0 && !Main.tileSolidTop[(int) Main.tile[index1, index2].type])
              {
                num5 = index1;
                num6 = index2;
                if (num6 != num8)
                  vector2_1.X = vector2_5.X + 16f - vector2_4.X;
                if (num7 == num5)
                  vector2_1.Y = vector2_2.Y;
              }
              else if ((double) vector2_4.Y >= (double) vector2_5.Y + 16.0 && !Main.tileSolidTop[(int) Main.tile[index1, index2].type])
              {
                num7 = index1;
                num8 = index2;
                vector2_1.Y = (float) ((double) vector2_5.Y + 16.0 - (double) vector2_4.Y + 0.00999999977648258);
                if (num8 == num6)
                  vector2_1.X = vector2_2.X + 0.01f;
              }
            }
          }
        }
      }
      return vector2_1;
    }

    public static void HitTiles(Vector2 Position, Vector2 Velocity, int Width, int Height)
    {
      Vector2 vector2_1 = Position + Velocity;
      int num1 = (int) ((double) Position.X / 16.0) - 1;
      int num2 = (int) (((double) Position.X + (double) Width) / 16.0) + 2;
      int num3 = (int) ((double) Position.Y / 16.0) - 1;
      int num4 = (int) (((double) Position.Y + (double) Height) / 16.0) + 2;
      if (num1 < 0)
        num1 = 0;
      if (num2 > Main.maxTilesX)
        num2 = Main.maxTilesX;
      if (num3 < 0)
        num3 = 0;
      if (num4 > Main.maxTilesY)
        num4 = Main.maxTilesY;
      for (int i = num1; i < num2; ++i)
      {
        for (int j = num3; j < num4; ++j)
        {
          if (Main.tile[i, j] != null && Main.tile[i, j].active && (Main.tileSolid[(int) Main.tile[i, j].type] || Main.tileSolidTop[(int) Main.tile[i, j].type] && (int) Main.tile[i, j].frameY == 0))
          {
            Vector2 vector2_2;
            vector2_2.X = (float) (i * 16);
            vector2_2.Y = (float) (j * 16);
            if ((double) vector2_1.X + (double) Width >= (double) vector2_2.X && (double) vector2_1.X <= (double) vector2_2.X + 16.0 && (double) vector2_1.Y + (double) Height >= (double) vector2_2.Y && (double) vector2_1.Y <= (double) vector2_2.Y + 16.0)
              WorldGen.KillTile(i, j, true, true, false);
          }
        }
      }
    }

    public static Vector2 HurtTiles(Vector2 Position, Vector2 Velocity, int Width, int Height, bool fireImmune = false)
    {
      Vector2 vector2_1 = Position;
      int num1 = (int) ((double) Position.X / 16.0) - 1;
      int num2 = (int) (((double) Position.X + (double) Width) / 16.0) + 2;
      int num3 = (int) ((double) Position.Y / 16.0) - 1;
      int num4 = (int) (((double) Position.Y + (double) Height) / 16.0) + 2;
      if (num1 < 0)
        num1 = 0;
      if (num2 > Main.maxTilesX)
        num2 = Main.maxTilesX;
      if (num3 < 0)
        num3 = 0;
      if (num4 > Main.maxTilesY)
        num4 = Main.maxTilesY;
      for (int index1 = num1; index1 < num2; ++index1)
      {
        for (int index2 = num3; index2 < num4; ++index2)
        {
          if (Main.tile[index1, index2] != null && Main.tile[index1, index2].active && ((int) Main.tile[index1, index2].type == 32 || (int) Main.tile[index1, index2].type == 37 || ((int) Main.tile[index1, index2].type == 48 || (int) Main.tile[index1, index2].type == 53) || ((int) Main.tile[index1, index2].type == 58 || (int) Main.tile[index1, index2].type == 69) || (int) Main.tile[index1, index2].type == 76))
          {
            Vector2 vector2_2;
            vector2_2.X = (float) (index1 * 16);
            vector2_2.Y = (float) (index2 * 16);
            int num5 = 0;
            int num6 = (int) Main.tile[index1, index2].type;
            if (num6 == 32 || num6 == 69)
            {
              if ((double) vector2_1.X + (double) Width > (double) vector2_2.X && (double) vector2_1.X < (double) vector2_2.X + 16.0 && (double) vector2_1.Y + (double) Height > (double) vector2_2.Y && (double) vector2_1.Y < (double) vector2_2.Y + 16.01)
              {
                int num7 = 1;
                if ((double) vector2_1.X + (double) (Width / 2) < (double) vector2_2.X + 8.0)
                  num7 = -1;
                int num8 = 10;
                if (num6 == 69)
                  num8 = 25;
                return new Vector2((float) num7, (float) num8);
              }
            }
            else if (num6 == 53)
            {
              if ((double) vector2_1.X + (double) Width - 2.0 >= (double) vector2_2.X && (double) vector2_1.X + 2.0 <= (double) vector2_2.X + 16.0 && (double) vector2_1.Y + (double) Height - 2.0 >= (double) vector2_2.Y && (double) vector2_1.Y + 2.0 <= (double) vector2_2.Y + 16.0)
              {
                int num7 = 1;
                if ((double) vector2_1.X + (double) (Width / 2) < (double) vector2_2.X + 8.0)
                  num7 = -1;
                if (num6 == 53)
                  num5 = 20;
                return new Vector2((float) num7, (float) num5);
              }
            }
            else if ((double) vector2_1.X + (double) Width >= (double) vector2_2.X && (double) vector2_1.X <= (double) vector2_2.X + 16.0 && (double) vector2_1.Y + (double) Height >= (double) vector2_2.Y && (double) vector2_1.Y <= (double) vector2_2.Y + 16.01)
            {
              int num7 = 1;
              if ((double) vector2_1.X + (double) (Width / 2) < (double) vector2_2.X + 8.0)
                num7 = -1;
              if (!fireImmune && (num6 == 37 || num6 == 58 || num6 == 76))
                num5 = 20;
              if (num6 == 48)
                num5 = 40;
              return new Vector2((float) num7, (float) num5);
            }
          }
        }
      }
      return new Vector2();
    }

    public static bool StickyTiles(Vector2 Position, Vector2 Velocity, int Width, int Height)
    {
      Vector2 vector2_1 = Position;
      bool flag = false;
      int num1 = (int) ((double) Position.X / 16.0) - 1;
      int num2 = (int) (((double) Position.X + (double) Width) / 16.0) + 2;
      int num3 = (int) ((double) Position.Y / 16.0) - 1;
      int num4 = (int) (((double) Position.Y + (double) Height) / 16.0) + 2;
      if (num1 < 0)
        num1 = 0;
      if (num2 > Main.maxTilesX)
        num2 = Main.maxTilesX;
      if (num3 < 0)
        num3 = 0;
      if (num4 > Main.maxTilesY)
        num4 = Main.maxTilesY;
      for (int index1 = num1; index1 < num2; ++index1)
      {
        for (int index2 = num3; index2 < num4; ++index2)
        {
          if (Main.tile[index1, index2] != null && Main.tile[index1, index2].active && (int) Main.tile[index1, index2].type == 51)
          {
            Vector2 vector2_2;
            vector2_2.X = (float) (index1 * 16);
            vector2_2.Y = (float) (index2 * 16);
            if ((double) vector2_1.X + (double) Width > (double) vector2_2.X && (double) vector2_1.X < (double) vector2_2.X + 16.0 && (double) vector2_1.Y + (double) Height > (double) vector2_2.Y && (double) vector2_1.Y < (double) vector2_2.Y + 16.01)
            {
              if ((double) Math.Abs(Velocity.X) + (double) Math.Abs(Velocity.Y) > 0.7 && Main.rand.Next(30) == 0)
                Dust.NewDust(new Vector2((float) (index1 * 16), (float) (index2 * 16)), 16, 16, 30, 0.0f, 0.0f, 0, new Color(), 1f);
              flag = true;
            }
          }
        }
      }
      return flag;
    }

    public static bool SolidTiles(int startX, int endX, int startY, int endY)
    {
      if (startX < 0 || endX >= Main.maxTilesX || (startY < 0 || endY >= Main.maxTilesY))
      {
        return true;
      }
      else
      {
        for (int index1 = startX; index1 < endX + 1; ++index1)
        {
          for (int index2 = startY; index2 < endY + 1; ++index2)
          {
            if (Main.tile[index1, index2] == null)
              return false;
            else if (Main.tile[index1, index2].active && (Main.tileSolid[(int) Main.tile[index1, index2].type] && !Main.tileSolidTop[(int) Main.tile[index1, index2].type]))
              return true;
          }
        }
        return false;
      }
    }
  }
}
