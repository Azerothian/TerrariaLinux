// Type: Terraria.Player
// Assembly: TerrariaServer, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// Assembly location: C:\Temp\New Folder\TerrariaServer.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria
{
  public class Player
  {
    public static int nameLen = 20;
    public static int tileRangeX = 5;
    public static int tileRangeY = 4;
    private static int jumpHeight = 15;
    private static float jumpSpeed = 5.01f;
    private static int itemGrabRange = 38;
    private static float itemGrabSpeed = 0.45f;
    private static float itemGrabSpeedMax = 4f;
    public bool pvpDeath = false;
    public bool zoneDungeon = false;
    public bool zoneEvil = false;
    public bool zoneMeteor = false;
    public bool zoneJungle = false;
    public bool boneArmor = false;
    public int townNPCs = 0;
    public int team = 0;
    public string chatText = "";
    public int sign = -1;
    public int chatShowTime = 0;
    public int changeItem = -1;
    public int selectedItem = 0;
    public Item[] armor = new Item[11];
    public int breathCD = 0;
    public int breathMax = 200;
    public int breath = 200;
    public string setBonus = "";
    public Item[] inventory = new Item[44];
    public Item[] bank = new Item[Chest.maxItems];
    public bool dead = false;
    public string name = "";
    public int potionDelay = 0;
    public bool wet = false;
    public byte wetCount = (byte) 0;
    public bool lavaWet = false;
    public int head = -1;
    public int body = -1;
    public int legs = -1;
    public int width = 20;
    public int height = 42;
    public int direction = 1;
    public bool showItemIcon = false;
    public int showItemIcon2 = 0;
    public int whoAmi = 0;
    public int runSoundDelay = 0;
    public float shadow = 0.0f;
    public float manaCost = 1f;
    public bool fireWalk = false;
    public Vector2[] shadowPos = new Vector2[3];
    public int shadowCount = 0;
    public bool channel = false;
    public int step = -1;
    public float meleeSpeed = 1f;
    public int statDefense = 0;
    public int statAttack = 0;
    public int statLifeMax = 100;
    public int statLife = 100;
    public int statMana = 0;
    public int statManaMax = 0;
    public int lifeRegen = 0;
    public int lifeRegenCount = 0;
    public int manaRegen = 0;
    public int manaRegenCount = 0;
    public int manaRegenDelay = 0;
    public bool noKnockback = false;
    public bool spaceGun = false;
    public float magicBoost = 1f;
    public int SpawnX = -1;
    public int SpawnY = -1;
    public int[] spX = new int[200];
    public int[] spY = new int[200];
    public string[] spN = new string[200];
    public int[] spI = new int[200];
    public bool[] adjTile = new bool[80];
    public bool[] oldAdjTile = new bool[80];
    public Color hairColor = new Color(215, 90, 55);
    public Color skinColor = new Color((int) byte.MaxValue, 125, 90);
    public Color eyeColor = new Color(105, 90, 75);
    public Color shirtColor = new Color(175, 165, 140);
    public Color underShirtColor = new Color(160, 180, 215);
    public Color pantsColor = new Color((int) byte.MaxValue, 230, 175);
    public Color shoeColor = new Color(160, 105, 60);
    public int hair = 0;
    public bool hostile = false;
    public int accWatch = 0;
    public int accDepthMeter = 0;
    public bool accFlipper = false;
    public bool doubleJump = false;
    public bool jumpAgain = false;
    public bool spawnMax = false;
    public int[] grappling = new int[20];
    public int grapCount = 0;
    public int rocketDelay = 0;
    public int rocketDelay2 = 0;
    public bool rocketRelease = false;
    public bool rocketFrame = false;
    public bool rocketBoots = false;
    public bool canRocket = false;
    public bool jumpBoost = false;
    public bool noFallDmg = false;
    public int swimTime = 0;
    public int chest = -1;
    public int chestX = 0;
    public int chestY = 0;
    public int talkNPC = -1;
    public int fallStart = 0;
    public int slowCount = 0;
    public Vector2 position;
    public Vector2 velocity;
    public Vector2 oldVelocity;
    public double headFrameCounter;
    public double bodyFrameCounter;
    public double legFrameCounter;
    public bool immune;
    public int immuneTime;
    public int immuneAlphaDirection;
    public int immuneAlpha;
    public int activeNPCs;
    public bool mouseInterface;
    public int itemAnimation;
    public int itemAnimationMax;
    public int itemTime;
    public float itemRotation;
    public int itemWidth;
    public int itemHeight;
    public Vector2 itemLocation;
    public float headRotation;
    public float bodyRotation;
    public float legRotation;
    public Vector2 headPosition;
    public Vector2 bodyPosition;
    public Vector2 legPosition;
    public Vector2 headVelocity;
    public Vector2 bodyVelocity;
    public Vector2 legVelocity;
    public int respawnTimer;
    public int attackCD;
    public int hitTile;
    public int hitTileX;
    public int hitTileY;
    public int jump;
    public Rectangle headFrame;
    public Rectangle bodyFrame;
    public Rectangle legFrame;
    public Rectangle hairFrame;
    public bool controlLeft;
    public bool controlRight;
    public bool controlUp;
    public bool controlDown;
    public bool controlJump;
    public bool controlUseItem;
    public bool controlUseTile;
    public bool controlThrow;
    public bool controlInv;
    public bool releaseJump;
    public bool releaseUseItem;
    public bool releaseUseTile;
    public bool releaseInventory;
    public bool delayUseItem;
    public bool active;
    private static int tileTargetX;
    private static int tileTargetY;

    static Player()
    {
    }

    public Player()
    {
      for (int index = 0; index < 44; ++index)
      {
        if (index < 11)
        {
          this.armor[index] = new Item();
          this.armor[index].name = "";
        }
        this.inventory[index] = new Item();
        this.inventory[index].name = "";
      }
      for (int index = 0; index < Chest.maxItems; ++index)
      {
        this.bank[index] = new Item();
        this.bank[index].name = "";
      }
      this.grappling[0] = -1;
      this.inventory[0].SetDefaults("Copper Pickaxe");
      this.inventory[1].SetDefaults("Copper Axe");
      for (int index = 0; index < 80; ++index)
      {
        this.adjTile[index] = false;
        this.oldAdjTile[index] = false;
      }
    }

    public void HealEffect(int healAmount)
    {
      CombatText.NewText(new Rectangle((int) this.position.X, (int) this.position.Y, this.width, this.height), new Color(100, 100, (int) byte.MaxValue, (int) byte.MaxValue), string.Concat((object) healAmount));
      if (Main.netMode == 1 && this.whoAmi == Main.myPlayer)
        NetMessage.SendData(35, -1, -1, "", this.whoAmi, (float) healAmount, 0.0f, 0.0f);
    }

    public void ManaEffect(int manaAmount)
    {
      CombatText.NewText(new Rectangle((int) this.position.X, (int) this.position.Y, this.width, this.height), new Color(180, 50, (int) byte.MaxValue, (int) byte.MaxValue), string.Concat((object) manaAmount));
      if (Main.netMode == 1 && this.whoAmi == Main.myPlayer)
        NetMessage.SendData(43, -1, -1, "", this.whoAmi, (float) manaAmount, 0.0f, 0.0f);
    }

    public static byte FindClosest(Vector2 Position, int Width, int Height)
    {
      byte num1 = (byte) 0;
      for (int index = 0; index < (int) byte.MaxValue; ++index)
      {
        if (Main.player[index].active)
        {
          num1 = (byte) index;
          break;
        }
      }
      float num2 = -1f;
      for (int index = 0; index < (int) byte.MaxValue; ++index)
      {
        if (Main.player[index].active && !Main.player[index].dead && ((double) num2 == -1.0 || (double) Math.Abs(Main.player[index].position.X + (float) (Main.player[index].width / 2) - Position.X + (float) (Width / 2)) + (double) Math.Abs(Main.player[index].position.Y + (float) (Main.player[index].height / 2) - Position.Y + (float) (Height / 2)) < (double) num2))
        {
          num2 = Math.Abs(Main.player[index].position.X + (float) (Main.player[index].width / 2) - Position.X + (float) (Width / 2)) + Math.Abs(Main.player[index].position.Y + (float) (Main.player[index].height / 2) - Position.Y + (float) (Height / 2));
          num1 = (byte) index;
        }
      }
      return num1;
    }

    public void checkArmor()
    {
    }

    public void toggleInv()
    {
      if (this.talkNPC >= 0)
      {
        this.talkNPC = -1;
        Main.npcChatText = "";
        Main.PlaySound(11, -1, -1, 1);
      }
      else if (this.sign >= 0)
      {
        this.sign = -1;
        Main.editSign = false;
        Main.npcChatText = "";
        Main.PlaySound(11, -1, -1, 1);
      }
      else if (!Main.playerInventory)
      {
        Recipe.FindRecipes();
        Main.playerInventory = true;
        Main.PlaySound(10, -1, -1, 1);
      }
      else
      {
        Main.playerInventory = false;
        Main.PlaySound(11, -1, -1, 1);
      }
    }

    public void dropItemCheck()
    {
      if (this.controlThrow && this.inventory[this.selectedItem].type > 0 && !Main.chatMode || (Main.mouseState.LeftButton == ButtonState.Pressed && !this.mouseInterface && Main.mouseLeftRelease || !Main.playerInventory) && Main.mouseItem.type > 0)
      {
        Item obj = new Item();
        bool flag = false;
        if ((Main.mouseState.LeftButton == ButtonState.Pressed && !this.mouseInterface && Main.mouseLeftRelease || !Main.playerInventory) && Main.mouseItem.type > 0)
        {
          obj = this.inventory[this.selectedItem];
          this.inventory[this.selectedItem] = Main.mouseItem;
          this.delayUseItem = true;
          this.controlUseItem = false;
          flag = true;
        }
        int number = Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, this.inventory[this.selectedItem].type, 1, false);
        if (!flag && this.inventory[this.selectedItem].type == 8 && this.inventory[this.selectedItem].stack > 1)
        {
          --this.inventory[this.selectedItem].stack;
        }
        else
        {
          this.inventory[this.selectedItem].position = Main.item[number].position;
          Main.item[number] = this.inventory[this.selectedItem];
          this.inventory[this.selectedItem] = new Item();
        }
        if (Main.netMode == 0)
          Main.item[number].noGrabDelay = 100;
        Main.item[number].velocity.Y = -2f;
        Main.item[number].velocity.X = (float) (4 * this.direction) + this.velocity.X;
        if ((Main.mouseState.LeftButton == ButtonState.Pressed && !this.mouseInterface || !Main.playerInventory) && Main.mouseItem.type > 0)
        {
          this.inventory[this.selectedItem] = obj;
          Main.mouseItem = new Item();
        }
        else
        {
          this.itemAnimation = 10;
          this.itemAnimationMax = 10;
        }
        Recipe.FindRecipes();
        if (Main.netMode == 1)
          NetMessage.SendData(21, -1, -1, "", number, 0.0f, 0.0f, 0.0f);
      }
    }

    public void UpdatePlayer(int i)
    {
      float num1 = 10f;
      float num2 = 0.4f;
      Player.jumpHeight = 15;
      Player.jumpSpeed = 5.01f;
      if (this.wet)
      {
        num2 = 0.2f;
        num1 = 5f;
        Player.jumpHeight = 30;
        Player.jumpSpeed = 6.01f;
      }
      float num3 = 3f;
      float num4 = 0.08f;
      float num5 = 0.2f;
      float num6 = num3;
      if (this.active)
      {
        ++this.shadowCount;
        if (this.shadowCount == 1)
          this.shadowPos[2] = this.shadowPos[1];
        else if (this.shadowCount == 2)
          this.shadowPos[1] = this.shadowPos[0];
        else if (this.shadowCount >= 3)
        {
          this.shadowCount = 0;
          this.shadowPos[0] = this.position;
        }
        this.whoAmi = i;
        if (this.runSoundDelay > 0)
          --this.runSoundDelay;
        if (this.attackCD > 0)
          --this.attackCD;
        if (this.itemAnimation == 0)
          this.attackCD = 0;
        if (this.chatShowTime > 0)
          --this.chatShowTime;
        if (this.potionDelay > 0)
          --this.potionDelay;
        if (this.dead)
        {
          if (i == Main.myPlayer)
          {
            Main.npcChatText = "";
            Main.editSign = false;
          }
          this.sign = -1;
          this.talkNPC = -1;
          this.statLife = 0;
          this.channel = false;
          this.potionDelay = 0;
          this.chest = -1;
          this.changeItem = -1;
          this.itemAnimation = 0;
          this.immuneAlpha += 2;
          if (this.immuneAlpha > (int) byte.MaxValue)
            this.immuneAlpha = (int) byte.MaxValue;
          --this.respawnTimer;
          this.headPosition += this.headVelocity;
          this.bodyPosition += this.bodyVelocity;
          this.legPosition += this.legVelocity;
          this.headRotation += this.headVelocity.X * 0.1f;
          this.bodyRotation += this.bodyVelocity.X * 0.1f;
          this.legRotation += this.legVelocity.X * 0.1f;
          this.headVelocity.Y += 0.1f;
          this.bodyVelocity.Y += 0.1f;
          this.legVelocity.Y += 0.1f;
          this.headVelocity.X *= 0.99f;
          this.bodyVelocity.X *= 0.99f;
          this.legVelocity.X *= 0.99f;
          if (this.respawnTimer <= 0 && Main.myPlayer == this.whoAmi)
            this.Spawn();
        }
        else
        {
          if (i == Main.myPlayer)
          {
            this.zoneEvil = false;
            if (Main.evilTiles >= 500)
              this.zoneEvil = true;
            this.zoneMeteor = false;
            if (Main.meteorTiles >= 50)
              this.zoneMeteor = true;
            this.zoneDungeon = false;
            if (Main.dungeonTiles >= 250 && (double) this.position.Y > Main.worldSurface * 16.0 + (double) Main.screenHeight)
            {
              int index1 = (int) this.position.X / 16;
              int index2 = (int) this.position.Y / 16;
              if ((int) Main.tile[index1, index2].wall > 0 && !Main.wallHouse[(int) Main.tile[index1, index2].wall])
                this.zoneDungeon = true;
            }
            this.zoneJungle = false;
            if (Main.jungleTiles >= 150)
              this.zoneJungle = true;
            this.controlUp = false;
            this.controlLeft = false;
            this.controlDown = false;
            this.controlRight = false;
            this.controlJump = false;
            this.controlUseItem = false;
            this.controlUseTile = false;
            this.controlThrow = false;
            this.controlInv = false;
            if (Main.hasFocus)
            {
              if (!Main.chatMode && !Main.editSign)
              {
                foreach (int num7 in Main.keyState.GetPressedKeys())
                {
                  string str = string.Concat((object) (Keys) num7);
                  if (str == Main.cUp)
                    this.controlUp = true;
                  if (str == Main.cLeft)
                    this.controlLeft = true;
                  if (str == Main.cDown)
                    this.controlDown = true;
                  if (str == Main.cRight)
                    this.controlRight = true;
                  if (str == Main.cJump)
                    this.controlJump = true;
                  if (str == Main.cThrowItem)
                    this.controlThrow = true;
                  if (str == Main.cInv)
                    this.controlInv = true;
                }
                if (this.controlLeft && this.controlRight)
                {
                  this.controlLeft = false;
                  this.controlRight = false;
                }
              }
              if (Main.mouseState.LeftButton == ButtonState.Pressed && !this.mouseInterface)
                this.controlUseItem = true;
              if (Main.mouseState.RightButton == ButtonState.Pressed && !this.mouseInterface)
                this.controlUseTile = true;
              if (this.controlInv)
              {
                if (this.releaseInventory)
                  this.toggleInv();
                this.releaseInventory = false;
              }
              else
                this.releaseInventory = true;
              if (this.delayUseItem)
              {
                if (!this.controlUseItem)
                  this.delayUseItem = false;
                this.controlUseItem = false;
              }
              if (this.itemAnimation == 0 && this.itemTime == 0)
              {
                this.dropItemCheck();
                if (!Main.playerInventory)
                {
                  int num7 = this.selectedItem;
                  if (!Main.chatMode)
                  {
                    if (Main.keyState.IsKeyDown(Keys.D1))
                      this.selectedItem = 0;
                    if (Main.keyState.IsKeyDown(Keys.D2))
                      this.selectedItem = 1;
                    if (Main.keyState.IsKeyDown(Keys.D3))
                      this.selectedItem = 2;
                    if (Main.keyState.IsKeyDown(Keys.D4))
                      this.selectedItem = 3;
                    if (Main.keyState.IsKeyDown(Keys.D5))
                      this.selectedItem = 4;
                    if (Main.keyState.IsKeyDown(Keys.D6))
                      this.selectedItem = 5;
                    if (Main.keyState.IsKeyDown(Keys.D7))
                      this.selectedItem = 6;
                    if (Main.keyState.IsKeyDown(Keys.D8))
                      this.selectedItem = 7;
                    if (Main.keyState.IsKeyDown(Keys.D9))
                      this.selectedItem = 8;
                    if (Main.keyState.IsKeyDown(Keys.D0))
                      this.selectedItem = 9;
                  }
                  if (num7 != this.selectedItem)
                    Main.PlaySound(12, -1, -1, 1);
                  int num8 = (Main.mouseState.ScrollWheelValue - Main.oldMouseState.ScrollWheelValue) / 120;
                  while (num8 > 9)
                    num8 -= 10;
                  while (num8 < 0)
                    num8 += 10;
                  this.selectedItem -= num8;
                  if (num8 != 0)
                    Main.PlaySound(12, -1, -1, 1);
                  if (this.changeItem >= 0)
                  {
                    if (this.selectedItem != this.changeItem)
                      Main.PlaySound(12, -1, -1, 1);
                    this.selectedItem = this.changeItem;
                    this.changeItem = -1;
                  }
                  while (this.selectedItem > 9)
                    this.selectedItem -= 10;
                  while (this.selectedItem < 0)
                    this.selectedItem += 10;
                }
                else
                {
                  int num7 = (Main.mouseState.ScrollWheelValue - Main.oldMouseState.ScrollWheelValue) / 120;
                  Main.focusRecipe += num7;
                  if (Main.focusRecipe > Main.numAvailableRecipes - 1)
                    Main.focusRecipe = Main.numAvailableRecipes - 1;
                  if (Main.focusRecipe < 0)
                    Main.focusRecipe = 0;
                }
              }
            }
            if (Main.netMode == 1)
            {
              bool flag = false;
              if (this.statLife != Main.clientPlayer.statLife || this.statLifeMax != Main.clientPlayer.statLifeMax)
              {
                NetMessage.SendData(16, -1, -1, "", Main.myPlayer, 0.0f, 0.0f, 0.0f);
                flag = true;
              }
              if (this.statMana != Main.clientPlayer.statMana || this.statManaMax != Main.clientPlayer.statManaMax)
              {
                NetMessage.SendData(42, -1, -1, "", Main.myPlayer, 0.0f, 0.0f, 0.0f);
                flag = true;
              }
              if (this.controlUp != Main.clientPlayer.controlUp)
                flag = true;
              if (this.controlDown != Main.clientPlayer.controlDown)
                flag = true;
              if (this.controlLeft != Main.clientPlayer.controlLeft)
                flag = true;
              if (this.controlRight != Main.clientPlayer.controlRight)
                flag = true;
              if (this.controlJump != Main.clientPlayer.controlJump)
                flag = true;
              if (this.controlUseItem != Main.clientPlayer.controlUseItem)
                flag = true;
              if (this.selectedItem != Main.clientPlayer.selectedItem)
                flag = true;
              if (flag)
                NetMessage.SendData(13, -1, -1, "", Main.myPlayer, 0.0f, 0.0f, 0.0f);
            }
            if (Main.playerInventory)
              this.AdjTiles();
            if (this.chest != -1)
            {
              int num7 = (int) (((double) this.position.X + (double) this.width * 0.5) / 16.0);
              int num8 = (int) (((double) this.position.Y + (double) this.height * 0.5) / 16.0);
              if (num7 < this.chestX - 5 || num7 > this.chestX + 6 || num8 < this.chestY - 4 || num8 > this.chestY + 5)
              {
                if (this.chest != -1)
                  Main.PlaySound(11, -1, -1, 1);
                this.chest = -1;
              }
              if (!Main.tile[this.chestX, this.chestY].active)
              {
                Main.PlaySound(11, -1, -1, 1);
                this.chest = -1;
              }
            }
            if ((double) this.velocity.Y == 0.0)
            {
              int num7 = (int) ((double) this.position.Y / 16.0) - this.fallStart;
              if (num7 > 25 && !this.noFallDmg)
              {
                int Damage = (num7 - 25) * 10;
                this.immune = false;
                this.Hurt(Damage, -this.direction, false, false);
              }
              this.fallStart = (int) ((double) this.position.Y / 16.0);
            }
            if ((double) this.velocity.Y < 0.0 || this.rocketDelay > 0 || this.wet)
              this.fallStart = (int) ((double) this.position.Y / 16.0);
          }
          if (this.mouseInterface)
            this.delayUseItem = true;
          Player.tileTargetX = (int) (((double) Main.mouseState.X + (double) Main.screenPosition.X) / 16.0);
          Player.tileTargetY = (int) (((double) Main.mouseState.Y + (double) Main.screenPosition.Y) / 16.0);
          if (this.immune)
          {
            --this.immuneTime;
            if (this.immuneTime <= 0)
              this.immune = false;
            this.immuneAlpha += this.immuneAlphaDirection * 50;
            if (this.immuneAlpha <= 50)
              this.immuneAlphaDirection = 1;
            else if (this.immuneAlpha >= 205)
              this.immuneAlphaDirection = -1;
          }
          else
            this.immuneAlpha = 0;
          if (this.manaRegenDelay > 0)
            --this.manaRegenDelay;
          this.statDefense = 0;
          this.accWatch = 0;
          this.accDepthMeter = 0;
          this.lifeRegen = 0;
          this.manaCost = 1f;
          this.meleeSpeed = 1f;
          this.boneArmor = false;
          this.rocketBoots = false;
          this.fireWalk = false;
          this.noKnockback = false;
          this.jumpBoost = false;
          this.noFallDmg = false;
          this.accFlipper = false;
          this.spawnMax = false;
          this.spaceGun = false;
          this.magicBoost = 1f;
          this.manaRegen = this.manaRegenDelay != 0 ? 0 : this.statManaMax / 30 + 1;
          this.doubleJump = false;
          for (int index = 0; index < 8; ++index)
          {
            this.statDefense += this.armor[index].defense;
            this.lifeRegen += this.armor[index].lifeRegen;
            this.manaRegen += this.armor[index].manaRegen;
            if (this.armor[index].type == 193)
              this.fireWalk = true;
            if (this.armor[index].type == 238)
              this.magicBoost *= 1.15f;
          }
          this.head = this.armor[0].headSlot;
          this.body = this.armor[1].bodySlot;
          this.legs = this.armor[2].legSlot;
          for (int index = 3; index < 8; ++index)
          {
            if (this.armor[index].type == 15 && this.accWatch < 1)
              this.accWatch = 1;
            if (this.armor[index].type == 16 && this.accWatch < 2)
              this.accWatch = 2;
            if (this.armor[index].type == 17 && this.accWatch < 3)
              this.accWatch = 3;
            if (this.armor[index].type == 18 && this.accDepthMeter < 1)
              this.accDepthMeter = 1;
            if (this.armor[index].type == 53)
              this.doubleJump = true;
            if (this.armor[index].type == 54)
              num6 = 6f;
            if (this.armor[index].type == 128)
              this.rocketBoots = true;
            if (this.armor[index].type == 156)
              this.noKnockback = true;
            if (this.armor[index].type == 158)
              this.noFallDmg = true;
            if (this.armor[index].type == 159)
              this.jumpBoost = true;
            if (this.armor[index].type == 187)
              this.accFlipper = true;
            if (this.armor[index].type == 211)
              this.meleeSpeed *= 0.9f;
            if (this.armor[index].type == 223)
              this.spawnMax = true;
            if (this.armor[index].type == 212)
            {
              num4 *= 1.1f;
              num3 *= 1.1f;
            }
          }
          this.lifeRegenCount += this.lifeRegen;
          while (this.lifeRegenCount >= 120)
          {
            this.lifeRegenCount -= 120;
            if (this.statLife < this.statLifeMax)
              ++this.statLife;
            if (this.statLife > this.statLifeMax)
              this.statLife = this.statLifeMax;
          }
          this.manaRegenCount += this.manaRegen;
          while (this.manaRegenCount >= 120)
          {
            this.manaRegenCount -= 120;
            if (this.statMana < this.statManaMax)
              ++this.statMana;
            if (this.statMana > this.statManaMax)
              this.statMana = this.statManaMax;
          }
          if (this.head == 11)
            Lighting.addLight((int) ((double) this.position.X + (double) (this.width / 2) + (double) (8 * this.direction)) / 16, (int) ((double) this.position.Y + 2.0) / 16, 0.8f);
          if (this.jumpBoost)
          {
            Player.jumpHeight = 20;
            Player.jumpSpeed = 6.51f;
          }
          this.setBonus = "";
          if (this.head == 1 && this.body == 1 && this.legs == 1 || this.head == 2 && this.body == 2 && this.legs == 2)
          {
            this.setBonus = "2 defense";
            this.statDefense += 2;
          }
          if (this.head == 3 && this.body == 3 && this.legs == 3 || this.head == 4 && this.body == 4 && this.legs == 4)
          {
            this.setBonus = "3 defense";
            this.statDefense += 3;
          }
          if (this.head == 5 && this.body == 5 && this.legs == 5)
          {
            this.setBonus = "15 % increased melee speed";
            this.meleeSpeed *= 0.85f;
          }
          if (this.head == 6 && this.body == 6 && this.legs == 6)
          {
            this.setBonus = "Space Gun costs 0 mana";
            this.spaceGun = true;
          }
          if (this.head == 7 && this.body == 7 && this.legs == 7)
          {
            num4 *= 1.3f;
            num3 *= 1.3f;
            this.setBonus = "30% increased movement speed";
          }
          if (this.head == 8 && this.body == 8 && this.legs == 8)
          {
            this.setBonus = "25% reduced mana usage";
            this.manaCost *= 0.75f;
            this.meleeSpeed *= 0.9f;
          }
          if (this.head == 9 && this.body == 9 && this.legs == 9)
          {
            this.setBonus = "5 defense";
            this.statDefense += 5;
          }
          if (!this.doubleJump)
            this.jumpAgain = false;
          else if ((double) this.velocity.Y == 0.0)
            this.jumpAgain = true;
          if ((double) this.meleeSpeed < 0.7)
            this.meleeSpeed = 0.7f;
          if (this.grappling[0] == -1)
          {
            if (this.controlLeft && (double) this.velocity.X > -(double) num3)
            {
              if ((double) this.velocity.X > (double) num5)
                this.velocity.X -= num5;
              this.velocity.X -= num4;
              if (this.itemAnimation == 0 || this.inventory[this.selectedItem].useTurn)
                this.direction = -1;
            }
            else if (this.controlRight && (double) this.velocity.X < (double) num3)
            {
              if ((double) this.velocity.X < -(double) num5)
                this.velocity.X += num5;
              this.velocity.X += num4;
              if (this.itemAnimation == 0 || this.inventory[this.selectedItem].useTurn)
                this.direction = 1;
            }
            else if (this.controlLeft && (double) this.velocity.X > -(double) num6)
            {
              if (this.itemAnimation == 0 || this.inventory[this.selectedItem].useTurn)
                this.direction = -1;
              if ((double) this.velocity.Y == 0.0)
              {
                if ((double) this.velocity.X > (double) num5)
                  this.velocity.X -= num5;
                this.velocity.X -= num4 * 0.2f;
              }
              if ((double) this.velocity.X < -((double) num6 + (double) num3) / 2.0 && (double) this.velocity.Y == 0.0)
              {
                if (this.runSoundDelay == 0 && (double) this.velocity.Y == 0.0)
                {
                  Main.PlaySound(17, (int) this.position.X, (int) this.position.Y, 1);
                  this.runSoundDelay = 9;
                }
                int index = Dust.NewDust(new Vector2(this.position.X - 4f, this.position.Y + (float) this.height), this.width + 8, 4, 16, (float) (-(double) this.velocity.X * 0.5), this.velocity.Y * 0.5f, 50, new Color(), 1.5f);
                Main.dust[index].velocity.X = Main.dust[index].velocity.X * 0.2f;
                Main.dust[index].velocity.Y = Main.dust[index].velocity.Y * 0.2f;
              }
            }
            else if (this.controlRight && (double) this.velocity.X < (double) num6)
            {
              if (this.itemAnimation == 0 || this.inventory[this.selectedItem].useTurn)
                this.direction = 1;
              if ((double) this.velocity.Y == 0.0)
              {
                if ((double) this.velocity.X < -(double) num5)
                  this.velocity.X += num5;
                this.velocity.X += num4 * 0.2f;
              }
              if ((double) this.velocity.X > ((double) num6 + (double) num3) / 2.0 && (double) this.velocity.Y == 0.0)
              {
                if (this.runSoundDelay == 0 && (double) this.velocity.Y == 0.0)
                {
                  Main.PlaySound(17, (int) this.position.X, (int) this.position.Y, 1);
                  this.runSoundDelay = 9;
                }
                int index = Dust.NewDust(new Vector2(this.position.X - 4f, this.position.Y + (float) this.height), this.width + 8, 4, 16, (float) (-(double) this.velocity.X * 0.5), this.velocity.Y * 0.5f, 50, new Color(), 1.5f);
                Main.dust[index].velocity.X = Main.dust[index].velocity.X * 0.2f;
                Main.dust[index].velocity.Y = Main.dust[index].velocity.Y * 0.2f;
              }
            }
            else if ((double) this.velocity.Y == 0.0)
            {
              if ((double) this.velocity.X > (double) num5)
                this.velocity.X -= num5;
              else if ((double) this.velocity.X < -(double) num5)
                this.velocity.X += num5;
              else
                this.velocity.X = 0.0f;
            }
            else if ((double) this.velocity.X > (double) num5 * 0.5)
              this.velocity.X -= num5 * 0.5f;
            else if ((double) this.velocity.X < -(double) num5 * 0.5)
              this.velocity.X += num5 * 0.5f;
            else
              this.velocity.X = 0.0f;
            if (this.controlJump)
            {
              if (this.jump > 0)
              {
                if ((double) this.velocity.Y > -(double) Player.jumpSpeed + (double) num2 * 2.0)
                {
                  this.jump = 0;
                }
                else
                {
                  this.velocity.Y = -Player.jumpSpeed;
                  --this.jump;
                }
              }
              else if (((double) this.velocity.Y == 0.0 || this.jumpAgain || this.wet && this.accFlipper) && this.releaseJump)
              {
                bool flag = false;
                if (this.wet && this.accFlipper)
                {
                  if (this.swimTime == 0)
                    this.swimTime = 30;
                  flag = true;
                }
                this.jumpAgain = false;
                this.canRocket = false;
                this.rocketRelease = false;
                if ((double) this.velocity.Y == 0.0 && this.doubleJump)
                  this.jumpAgain = true;
                if ((double) this.velocity.Y == 0.0 || flag)
                {
                  this.velocity.Y = -Player.jumpSpeed;
                  this.jump = Player.jumpHeight;
                }
                else
                {
                  Main.PlaySound(16, (int) this.position.X, (int) this.position.Y, 1);
                  this.velocity.Y = -Player.jumpSpeed;
                  this.jump = Player.jumpHeight / 2;
                  for (int index1 = 0; index1 < 10; ++index1)
                  {
                    int index2 = Dust.NewDust(new Vector2(this.position.X - 34f, (float) ((double) this.position.Y + (double) this.height - 16.0)), 102, 32, 16, (float) (-(double) this.velocity.X * 0.5), this.velocity.Y * 0.5f, 100, new Color(), 1.5f);
                    Main.dust[index2].velocity.X = (float) ((double) Main.dust[index2].velocity.X * 0.5 - (double) this.velocity.X * 0.100000001490116);
                    Main.dust[index2].velocity.Y = (float) ((double) Main.dust[index2].velocity.Y * 0.5 - (double) this.velocity.Y * 0.300000011920929);
                  }
                  int index3 = Gore.NewGore(new Vector2((float) ((double) this.position.X + (double) (this.width / 2) - 16.0), (float) ((double) this.position.Y + (double) this.height - 16.0)), new Vector2(-this.velocity.X, -this.velocity.Y), Main.rand.Next(11, 14));
                  Main.gore[index3].velocity.X = (float) ((double) Main.gore[index3].velocity.X * 0.100000001490116 - (double) this.velocity.X * 0.100000001490116);
                  Main.gore[index3].velocity.Y = (float) ((double) Main.gore[index3].velocity.Y * 0.100000001490116 - (double) this.velocity.Y * 0.0500000007450581);
                  int index4 = Gore.NewGore(new Vector2(this.position.X - 36f, (float) ((double) this.position.Y + (double) this.height - 16.0)), new Vector2(-this.velocity.X, -this.velocity.Y), Main.rand.Next(11, 14));
                  Main.gore[index4].velocity.X = (float) ((double) Main.gore[index4].velocity.X * 0.100000001490116 - (double) this.velocity.X * 0.100000001490116);
                  Main.gore[index4].velocity.Y = (float) ((double) Main.gore[index4].velocity.Y * 0.100000001490116 - (double) this.velocity.Y * 0.0500000007450581);
                  int index5 = Gore.NewGore(new Vector2((float) ((double) this.position.X + (double) this.width + 4.0), (float) ((double) this.position.Y + (double) this.height - 16.0)), new Vector2(-this.velocity.X, -this.velocity.Y), Main.rand.Next(11, 14));
                  Main.gore[index5].velocity.X = (float) ((double) Main.gore[index5].velocity.X * 0.100000001490116 - (double) this.velocity.X * 0.100000001490116);
                  Main.gore[index5].velocity.Y = (float) ((double) Main.gore[index5].velocity.Y * 0.100000001490116 - (double) this.velocity.Y * 0.0500000007450581);
                }
              }
              this.releaseJump = false;
            }
            else
            {
              this.jump = 0;
              this.releaseJump = true;
              this.rocketRelease = true;
            }
            if (this.doubleJump && !this.jumpAgain && ((double) this.velocity.Y < 0.0 && !this.rocketBoots) && !this.accFlipper)
            {
              int index = Dust.NewDust(new Vector2(this.position.X - 4f, this.position.Y + (float) this.height), this.width + 8, 4, 16, (float) (-(double) this.velocity.X * 0.5), this.velocity.Y * 0.5f, 100, new Color(), 1.5f);
              Main.dust[index].velocity.X = (float) ((double) Main.dust[index].velocity.X * 0.5 - (double) this.velocity.X * 0.100000001490116);
              Main.dust[index].velocity.Y = (float) ((double) Main.dust[index].velocity.Y * 0.5 - (double) this.velocity.Y * 0.300000011920929);
            }
            if ((double) this.velocity.Y > -(double) Player.jumpSpeed && (double) this.velocity.Y != 0.0)
              this.canRocket = true;
            if (this.rocketBoots && this.controlJump && (this.rocketDelay == 0 && this.canRocket) && this.rocketRelease && !this.jumpAgain)
            {
              int num7 = 7;
              if (this.statMana >= (int) ((double) num7 * (double) this.manaCost))
              {
                this.manaRegenDelay = 180;
                this.statMana -= (int) ((double) num7 * (double) this.manaCost);
                this.rocketDelay = 10;
                if (this.rocketDelay2 <= 0)
                {
                  Main.PlaySound(2, (int) this.position.X, (int) this.position.Y, 13);
                  this.rocketDelay2 = 30;
                }
              }
              else
                this.canRocket = false;
            }
            if (this.rocketDelay2 > 0)
              --this.rocketDelay2;
            if (this.rocketDelay == 0)
              this.rocketFrame = false;
            if (this.rocketDelay > 0)
            {
              this.rocketFrame = true;
              for (int index1 = 0; index1 < 2; ++index1)
              {
                if (index1 == 0)
                {
                  int index2 = Dust.NewDust(new Vector2(this.position.X - 4f, (float) ((double) this.position.Y + (double) this.height - 10.0)), 8, 8, 6, 0.0f, 0.0f, 100, new Color(), 2.5f);
                  Main.dust[index2].noGravity = true;
                  Main.dust[index2].velocity.X = (float) ((double) Main.dust[index2].velocity.X * 1.0 - 2.0 - (double) this.velocity.X * 0.300000011920929);
                  Main.dust[index2].velocity.Y = (float) ((double) Main.dust[index2].velocity.Y * 1.0 + 2.0 - (double) this.velocity.Y * 0.300000011920929);
                }
                else
                {
                  int index2 = Dust.NewDust(new Vector2((float) ((double) this.position.X + (double) this.width - 4.0), (float) ((double) this.position.Y + (double) this.height - 10.0)), 8, 8, 6, 0.0f, 0.0f, 100, new Color(), 2.5f);
                  Main.dust[index2].noGravity = true;
                  Main.dust[index2].velocity.X = (float) ((double) Main.dust[index2].velocity.X * 1.0 + 2.0 - (double) this.velocity.X * 0.300000011920929);
                  Main.dust[index2].velocity.Y = (float) ((double) Main.dust[index2].velocity.Y * 1.0 + 2.0 - (double) this.velocity.Y * 0.300000011920929);
                }
              }
              if (this.rocketDelay == 0)
                this.releaseJump = true;
              --this.rocketDelay;
              this.velocity.Y -= 0.1f;
              if ((double) this.velocity.Y > 0.0)
                this.velocity.Y -= 0.3f;
              if ((double) this.velocity.Y < -(double) Player.jumpSpeed)
                this.velocity.Y = -Player.jumpSpeed;
            }
            else
              this.velocity.Y += num2;
            if ((double) this.velocity.Y > (double) num1)
              this.velocity.Y = num1;
          }
          for (int number = 0; number < 200; ++number)
          {
            if (Main.item[number].active && Main.item[number].noGrabDelay == 0 && Main.item[number].owner == i)
            {
              Rectangle rectangle = new Rectangle((int) this.position.X, (int) this.position.Y, this.width, this.height);
              if (rectangle.Intersects(new Rectangle((int) Main.item[number].position.X, (int) Main.item[number].position.Y, Main.item[number].width, Main.item[number].height)))
              {
                if (i == Main.myPlayer && (this.inventory[this.selectedItem].type != 0 || this.itemAnimation <= 0))
                {
                  if (Main.item[number].type == 58)
                  {
                    Main.PlaySound(7, (int) this.position.X, (int) this.position.Y, 1);
                    this.statLife += 20;
                    if (Main.myPlayer == this.whoAmi)
                      this.HealEffect(20);
                    if (this.statLife > this.statLifeMax)
                      this.statLife = this.statLifeMax;
                    Main.item[number] = new Item();
                    if (Main.netMode == 1)
                      NetMessage.SendData(21, -1, -1, "", number, 0.0f, 0.0f, 0.0f);
                  }
                  else if (Main.item[number].type == 184)
                  {
                    Main.PlaySound(7, (int) this.position.X, (int) this.position.Y, 1);
                    this.statMana += 20;
                    if (Main.myPlayer == this.whoAmi)
                      this.ManaEffect(20);
                    if (this.statMana > this.statManaMax)
                      this.statMana = this.statManaMax;
                    Main.item[number] = new Item();
                    if (Main.netMode == 1)
                      NetMessage.SendData(21, -1, -1, "", number, 0.0f, 0.0f, 0.0f);
                  }
                  else
                  {
                    Main.item[number] = this.GetItem(i, Main.item[number]);
                    if (Main.netMode == 1)
                      NetMessage.SendData(21, -1, -1, "", number, 0.0f, 0.0f, 0.0f);
                  }
                }
              }
              else
              {
                rectangle = new Rectangle((int) this.position.X - Player.itemGrabRange, (int) this.position.Y - Player.itemGrabRange, this.width + Player.itemGrabRange * 2, this.height + Player.itemGrabRange * 2);
                if (rectangle.Intersects(new Rectangle((int) Main.item[number].position.X, (int) Main.item[number].position.Y, Main.item[number].width, Main.item[number].height)) && this.ItemSpace(Main.item[number]))
                {
                  Main.item[number].beingGrabbed = true;
                  if ((double) this.position.X + (double) this.width * 0.5 > (double) Main.item[number].position.X + (double) Main.item[number].width * 0.5)
                  {
                    if ((double) Main.item[number].velocity.X < (double) Player.itemGrabSpeedMax + (double) this.velocity.X)
                      Main.item[number].velocity.X += Player.itemGrabSpeed;
                    if ((double) Main.item[number].velocity.X < 0.0)
                      Main.item[number].velocity.X += Player.itemGrabSpeed * 0.75f;
                  }
                  else
                  {
                    if ((double) Main.item[number].velocity.X > -(double) Player.itemGrabSpeedMax + (double) this.velocity.X)
                      Main.item[number].velocity.X -= Player.itemGrabSpeed;
                    if ((double) Main.item[number].velocity.X > 0.0)
                      Main.item[number].velocity.X -= Player.itemGrabSpeed * 0.75f;
                  }
                  if ((double) this.position.Y + (double) this.height * 0.5 > (double) Main.item[number].position.Y + (double) Main.item[number].height * 0.5)
                  {
                    if ((double) Main.item[number].velocity.Y < (double) Player.itemGrabSpeedMax)
                      Main.item[number].velocity.Y += Player.itemGrabSpeed;
                    if ((double) Main.item[number].velocity.Y < 0.0)
                      Main.item[number].velocity.Y += Player.itemGrabSpeed * 0.75f;
                  }
                  else
                  {
                    if ((double) Main.item[number].velocity.Y > -(double) Player.itemGrabSpeedMax)
                      Main.item[number].velocity.Y -= Player.itemGrabSpeed;
                    if ((double) Main.item[number].velocity.Y > 0.0)
                      Main.item[number].velocity.Y -= Player.itemGrabSpeed * 0.75f;
                  }
                }
              }
            }
          }
          if ((double) this.position.X / 16.0 - (double) Player.tileRangeX <= (double) Player.tileTargetX && ((double) this.position.X + (double) this.width) / 16.0 + (double) Player.tileRangeX - 1.0 >= (double) Player.tileTargetX && (double) this.position.Y / 16.0 - (double) Player.tileRangeY <= (double) Player.tileTargetY && ((double) this.position.Y + (double) this.height) / 16.0 + (double) Player.tileRangeY - 2.0 >= (double) Player.tileTargetY && Main.tile[Player.tileTargetX, Player.tileTargetY].active)
          {
            if ((int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 79)
            {
              this.showItemIcon = true;
              this.showItemIcon2 = 224;
            }
            if ((int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 21)
            {
              this.showItemIcon = true;
              this.showItemIcon2 = 48;
            }
            if ((int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 4)
            {
              this.showItemIcon = true;
              this.showItemIcon2 = 8;
            }
            if ((int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 13)
            {
              this.showItemIcon = true;
              this.showItemIcon2 = (int) Main.tile[Player.tileTargetX, Player.tileTargetY].frameX != 18 ? ((int) Main.tile[Player.tileTargetX, Player.tileTargetY].frameX != 36 ? 31 : 110) : 28;
            }
            if ((int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 29)
            {
              this.showItemIcon = true;
              this.showItemIcon2 = 87;
            }
            if ((int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 33)
            {
              this.showItemIcon = true;
              this.showItemIcon2 = 105;
            }
            if ((int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 49)
            {
              this.showItemIcon = true;
              this.showItemIcon2 = 148;
            }
            if ((int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 50 && (int) Main.tile[Player.tileTargetX, Player.tileTargetY].frameX == 90)
            {
              this.showItemIcon = true;
              this.showItemIcon2 = 165;
            }
            if ((int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 55)
            {
              int num7 = (int) Main.tile[Player.tileTargetX, Player.tileTargetY].frameX / 18;
              int num8 = (int) Main.tile[Player.tileTargetX, Player.tileTargetY].frameY / 18;
              while (num7 > 1)
                num7 -= 2;
              int num9 = Player.tileTargetX - num7;
              int num10 = Player.tileTargetY - num8;
              Main.signBubble = true;
              Main.signX = num9 * 16 + 16;
              Main.signY = num10 * 16;
            }
            if ((int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 10 || (int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 11)
            {
              this.showItemIcon = true;
              this.showItemIcon2 = 25;
            }
            if (this.controlUseTile)
            {
              if (this.releaseUseTile)
              {
                if ((int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 4 || (int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 13 || ((int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 33 || (int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 49) || (int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 50 && (int) Main.tile[Player.tileTargetX, Player.tileTargetY].frameX == 90)
                {
                  WorldGen.KillTile(Player.tileTargetX, Player.tileTargetY, false, false, false);
                  if (Main.netMode == 1)
                    NetMessage.SendData(17, -1, -1, "", 0, (float) Player.tileTargetX, (float) Player.tileTargetY, 0.0f);
                }
                else if ((int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 79)
                {
                  int num7 = Player.tileTargetX;
                  int num8 = Player.tileTargetY;
                  int num9 = num7 + (int) Main.tile[Player.tileTargetX, Player.tileTargetY].frameX / 18 * -1;
                  int x = (int) Main.tile[Player.tileTargetX, Player.tileTargetY].frameX < 72 ? num9 + 2 : num9 + 4 + 1;
                  int y = num8 + (int) Main.tile[Player.tileTargetX, Player.tileTargetY].frameY / 18 * -1 + 2;
                  if (Player.CheckSpawn(x, y))
                  {
                    this.ChangeSpawn(x, y);
                    Main.NewText("Spawn point set!", byte.MaxValue, (byte) 240, (byte) 20);
                  }
                }
                else if ((int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 55)
                {
                  bool flag = true;
                  if (this.sign >= 0 && Sign.ReadSign(Player.tileTargetX, Player.tileTargetY) == this.sign)
                  {
                    this.sign = -1;
                    Main.npcChatText = "";
                    Main.editSign = false;
                    Main.PlaySound(11, -1, -1, 1);
                    flag = false;
                  }
                  if (flag)
                  {
                    if (Main.netMode == 0)
                    {
                      this.talkNPC = -1;
                      Main.playerInventory = false;
                      Main.editSign = false;
                      Main.PlaySound(10, -1, -1, 1);
                      int index = Sign.ReadSign(Player.tileTargetX, Player.tileTargetY);
                      this.sign = index;
                      Main.npcChatText = Main.sign[index].text;
                    }
                    else
                    {
                      int num7 = (int) Main.tile[Player.tileTargetX, Player.tileTargetY].frameX / 18;
                      int num8 = (int) Main.tile[Player.tileTargetX, Player.tileTargetY].frameY / 18;
                      while (num7 > 1)
                        num7 -= 2;
                      int number = Player.tileTargetX - num7;
                      int index = Player.tileTargetY - num8;
                      if ((int) Main.tile[number, index].type == 55)
                        NetMessage.SendData(46, -1, -1, "", number, (float) index, 0.0f, 0.0f);
                    }
                  }
                }
                else if ((int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 10)
                {
                  WorldGen.OpenDoor(Player.tileTargetX, Player.tileTargetY, this.direction);
                  NetMessage.SendData(19, -1, -1, "", 0, (float) Player.tileTargetX, (float) Player.tileTargetY, (float) this.direction);
                }
                else if ((int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 11)
                {
                  if (WorldGen.CloseDoor(Player.tileTargetX, Player.tileTargetY, false))
                    NetMessage.SendData(19, -1, -1, "", 1, (float) Player.tileTargetX, (float) Player.tileTargetY, (float) this.direction);
                }
                else if (((int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 21 || (int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 29) && this.talkNPC == -1)
                {
                  bool flag = false;
                  int num7 = Player.tileTargetX - (int) Main.tile[Player.tileTargetX, Player.tileTargetY].frameX / 18;
                  int Y = Player.tileTargetY - (int) Main.tile[Player.tileTargetX, Player.tileTargetY].frameY / 18;
                  if ((int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 29)
                    flag = true;
                  if (Main.netMode == 1 && !flag)
                  {
                    if (num7 == this.chestX && Y == this.chestY && this.chest != -1)
                    {
                      this.chest = -1;
                      Main.PlaySound(11, -1, -1, 1);
                    }
                    else
                      NetMessage.SendData(31, -1, -1, "", num7, (float) Y, 0.0f, 0.0f);
                  }
                  else
                  {
                    int num8 = !flag ? Chest.FindChest(num7, Y) : -2;
                    if (num8 != -1)
                    {
                      if (num8 == this.chest)
                      {
                        this.chest = -1;
                        Main.PlaySound(11, -1, -1, 1);
                      }
                      else if (num8 != this.chest && this.chest == -1)
                      {
                        this.chest = num8;
                        Main.playerInventory = true;
                        Main.PlaySound(10, -1, -1, 1);
                        this.chestX = num7;
                        this.chestY = Y;
                      }
                      else
                      {
                        this.chest = num8;
                        Main.playerInventory = true;
                        Main.PlaySound(12, -1, -1, 1);
                        this.chestX = num7;
                        this.chestY = Y;
                      }
                    }
                  }
                }
              }
              this.releaseUseTile = false;
            }
            else
              this.releaseUseTile = true;
          }
          if (Main.myPlayer == this.whoAmi)
          {
            Rectangle rectangle1;
            if (this.talkNPC >= 0)
            {
              rectangle1 = new Rectangle((int) ((double) this.position.X + (double) (this.width / 2) - (double) (Player.tileRangeX * 16)), (int) ((double) this.position.Y + (double) (this.height / 2) - (double) (Player.tileRangeY * 16)), Player.tileRangeX * 16 * 2, Player.tileRangeY * 16 * 2);
              Rectangle rectangle2 = new Rectangle((int) Main.npc[this.talkNPC].position.X, (int) Main.npc[this.talkNPC].position.Y, Main.npc[this.talkNPC].width, Main.npc[this.talkNPC].height);
              if (!rectangle1.Intersects(rectangle2) || this.chest != -1 || !Main.npc[this.talkNPC].active)
              {
                if (this.chest == -1)
                  Main.PlaySound(11, -1, -1, 1);
                this.talkNPC = -1;
                Main.npcChatText = "";
              }
            }
            if (this.sign >= 0)
            {
              rectangle1 = new Rectangle((int) ((double) this.position.X + (double) (this.width / 2) - (double) (Player.tileRangeX * 16)), (int) ((double) this.position.Y + (double) (this.height / 2) - (double) (Player.tileRangeY * 16)), Player.tileRangeX * 16 * 2, Player.tileRangeY * 16 * 2);
              Rectangle rectangle2 = new Rectangle(Main.sign[this.sign].x * 16, Main.sign[this.sign].y * 16, 32, 32);
              if (!rectangle1.Intersects(rectangle2))
              {
                Main.PlaySound(11, -1, -1, 1);
                this.sign = -1;
                Main.editSign = false;
                Main.npcChatText = "";
              }
            }
            if (Main.editSign)
            {
              if (this.sign == -1)
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
            Rectangle rectangle3 = new Rectangle((int) this.position.X, (int) this.position.Y, this.width, this.height);
            for (int index = 0; index < 1000; ++index)
            {
              if (Main.npc[index].active && !Main.npc[index].friendly && Main.npc[index].damage > 0 && rectangle3.Intersects(new Rectangle((int) Main.npc[index].position.X, (int) Main.npc[index].position.Y, Main.npc[index].width, Main.npc[index].height)))
              {
                int hitDirection = -1;
                if ((double) Main.npc[index].position.X + (double) (Main.npc[index].width / 2) < (double) this.position.X + (double) (this.width / 2))
                  hitDirection = 1;
                this.Hurt(Main.npc[index].damage, hitDirection, false, false);
              }
            }
            Vector2 vector2 = Collision.HurtTiles(this.position, this.velocity, this.width, this.height, this.fireWalk);
            if ((double) vector2.Y != 0.0)
              this.Hurt((int) vector2.Y, (int) vector2.X, false, false);
          }
          if (this.grappling[0] >= 0)
          {
            this.rocketDelay = 0;
            this.rocketFrame = false;
            this.canRocket = false;
            this.rocketRelease = false;
            this.fallStart = (int) ((double) this.position.Y / 16.0);
            float num7 = 0.0f;
            float num8 = 0.0f;
            for (int index = 0; index < this.grapCount; ++index)
            {
              num7 += Main.projectile[this.grappling[index]].position.X + (float) (Main.projectile[this.grappling[index]].width / 2);
              num8 += Main.projectile[this.grappling[index]].position.Y + (float) (Main.projectile[this.grappling[index]].height / 2);
            }
            float num9 = num7 / (float) this.grapCount;
            float num10 = num8 / (float) this.grapCount;
            Vector2 vector2 = new Vector2(this.position.X + (float) this.width * 0.5f, this.position.Y + (float) this.height * 0.5f);
            float num11 = num9 - vector2.X;
            float num12 = num10 - vector2.Y;
            float num13 = (float) Math.Sqrt((double) num11 * (double) num11 + (double) num12 * (double) num12);
            float num14 = 11f;
            float num15 = (double) num13 <= (double) num14 ? 1f : num14 / num13;
            float num16 = num11 * num15;
            float num17 = num12 * num15;
            this.velocity.X = num16;
            this.velocity.Y = num17;
            if (this.itemAnimation == 0)
            {
              if ((double) this.velocity.X > 0.0)
                this.direction = 1;
              if ((double) this.velocity.X < 0.0)
                this.direction = -1;
            }
            if (this.controlJump)
            {
              if (this.releaseJump)
              {
                if ((double) this.velocity.Y == 0.0 || this.wet && (double) this.velocity.Y > -0.02 && (double) this.velocity.Y < 0.02)
                {
                  this.velocity.Y = -Player.jumpSpeed;
                  this.jump = Player.jumpHeight / 2;
                  this.releaseJump = false;
                }
                else
                {
                  this.velocity.Y += 0.01f;
                  this.releaseJump = false;
                }
                if (this.doubleJump)
                  this.jumpAgain = true;
                this.grappling[0] = 0;
                this.grapCount = 0;
                for (int index = 0; index < 1000; ++index)
                {
                  if (Main.projectile[index].active && Main.projectile[index].owner == i && Main.projectile[index].aiStyle == 7)
                    Main.projectile[index].Kill();
                }
              }
            }
            else
              this.releaseJump = true;
          }
          if (Collision.StickyTiles(this.position, this.velocity, this.width, this.height))
          {
            this.fallStart = (int) ((double) this.position.Y / 16.0);
            this.jump = 0;
            if ((double) this.velocity.X > 1.0)
              this.velocity.X = 1f;
            if ((double) this.velocity.X < -1.0)
              this.velocity.X = -1f;
            if ((double) this.velocity.Y > 1.0)
              this.velocity.Y = 1f;
            if ((double) this.velocity.Y < -5.0)
              this.velocity.Y = -5f;
            if ((double) this.velocity.X > 0.75 || (double) this.velocity.X < -0.75)
              this.velocity.X *= 0.85f;
            else
              this.velocity.X *= 0.6f;
            if ((double) this.velocity.Y < 0.0)
              this.velocity.Y *= 0.96f;
            else
              this.velocity.Y *= 0.3f;
          }
          bool flag1 = Collision.DrownCollision(this.position, this.width, this.height);
          if (this.armor[0].type == 250)
            flag1 = true;
          if (this.inventory[this.selectedItem].type == 186)
          {
            try
            {
              int index1 = (int) (((double) this.position.X + (double) (this.width / 2) + (double) (6 * this.direction)) / 16.0);
              int index2 = (int) (((double) this.position.Y - 44.0) / 16.0);
              if ((int) Main.tile[index1, index2].liquid < 128)
              {
                if (Main.tile[index1, index2] == null)
                  Main.tile[index1, index2] = new Tile();
                if (!Main.tile[index1, index2].active || !Main.tileSolid[(int) Main.tile[index1, index2].type] || Main.tileSolidTop[(int) Main.tile[index1, index2].type])
                  flag1 = false;
              }
            }
            catch
            {
            }
          }
          if (Main.myPlayer == i)
          {
            if (flag1)
            {
              ++this.breathCD;
              int num7 = 7;
              if (this.inventory[this.selectedItem].type == 186)
                num7 *= 2;
              if (this.breathCD >= num7)
              {
                this.breathCD = 0;
                --this.breath;
                if (this.breath <= 0)
                {
                  this.breath = 0;
                  this.statLife -= 2;
                  if (this.statLife <= 0)
                  {
                    this.statLife = 0;
                    this.KillMe(10.0, 0, false);
                  }
                }
              }
            }
            else
            {
              this.breath += 3;
              if (this.breath > this.breathMax)
                this.breath = this.breathMax;
              this.breathCD = 0;
            }
          }
          if (flag1 && Main.rand.Next(20) == 0)
          {
            if (this.inventory[this.selectedItem].type == 186)
              Dust.NewDust(new Vector2((float) ((double) this.position.X + (double) (10 * this.direction) + 4.0), this.position.Y - 54f), this.width - 8, 8, 34, 0.0f, 0.0f, 0, new Color(), 1.2f);
            else
              Dust.NewDust(new Vector2(this.position.X + (float) (12 * this.direction), this.position.Y + 4f), this.width - 8, 8, 34, 0.0f, 0.0f, 0, new Color(), 1.2f);
          }
          bool flag2 = Collision.LavaCollision(this.position, this.width, this.height);
          if (flag2)
          {
            if (Main.myPlayer == i)
              this.Hurt(100, 0, false, false);
            this.lavaWet = true;
          }
          if (Collision.WetCollision(this.position, this.width, this.height))
          {
            if (!this.wet)
            {
              if ((int) this.wetCount == 0)
              {
                this.wetCount = (byte) 10;
                if (!flag2)
                {
                  for (int index1 = 0; index1 < 50; ++index1)
                  {
                    int index2 = Dust.NewDust(new Vector2(this.position.X - 6f, (float) ((double) this.position.Y + (double) (this.height / 2) - 8.0)), this.width + 12, 24, 33, 0.0f, 0.0f, 0, new Color(), 1f);
                    Main.dust[index2].velocity.Y -= 4f;
                    Main.dust[index2].velocity.X *= 2.5f;
                    Main.dust[index2].scale = 1.3f;
                    Main.dust[index2].alpha = 100;
                    Main.dust[index2].noGravity = true;
                  }
                  Main.PlaySound(19, (int) this.position.X, (int) this.position.Y, 0);
                }
                else
                {
                  for (int index1 = 0; index1 < 20; ++index1)
                  {
                    int index2 = Dust.NewDust(new Vector2(this.position.X - 6f, (float) ((double) this.position.Y + (double) (this.height / 2) - 8.0)), this.width + 12, 24, 35, 0.0f, 0.0f, 0, new Color(), 1f);
                    Main.dust[index2].velocity.Y -= 1.5f;
                    Main.dust[index2].velocity.X *= 2.5f;
                    Main.dust[index2].scale = 1.3f;
                    Main.dust[index2].alpha = 100;
                    Main.dust[index2].noGravity = true;
                  }
                  Main.PlaySound(19, (int) this.position.X, (int) this.position.Y, 1);
                }
              }
              this.wet = true;
            }
          }
          else if (this.wet)
          {
            this.wet = false;
            if (this.jump > Player.jumpHeight / 5)
              this.jump = Player.jumpHeight / 5;
            if ((int) this.wetCount == 0)
            {
              this.wetCount = (byte) 10;
              if (!this.lavaWet)
              {
                for (int index1 = 0; index1 < 50; ++index1)
                {
                  int index2 = Dust.NewDust(new Vector2(this.position.X - 6f, this.position.Y + (float) (this.height / 2)), this.width + 12, 24, 33, 0.0f, 0.0f, 0, new Color(), 1f);
                  Main.dust[index2].velocity.Y -= 4f;
                  Main.dust[index2].velocity.X *= 2.5f;
                  Main.dust[index2].scale = 1.3f;
                  Main.dust[index2].alpha = 100;
                  Main.dust[index2].noGravity = true;
                }
                Main.PlaySound(19, (int) this.position.X, (int) this.position.Y, 0);
              }
              else
              {
                for (int index1 = 0; index1 < 20; ++index1)
                {
                  int index2 = Dust.NewDust(new Vector2(this.position.X - 6f, (float) ((double) this.position.Y + (double) (this.height / 2) - 8.0)), this.width + 12, 24, 35, 0.0f, 0.0f, 0, new Color(), 1f);
                  Main.dust[index2].velocity.Y -= 1.5f;
                  Main.dust[index2].velocity.X *= 2.5f;
                  Main.dust[index2].scale = 1.3f;
                  Main.dust[index2].alpha = 100;
                  Main.dust[index2].noGravity = true;
                }
                Main.PlaySound(19, (int) this.position.X, (int) this.position.Y, 1);
              }
            }
          }
          if (!this.wet)
            this.lavaWet = false;
          if ((int) this.wetCount > 0)
            --this.wetCount;
          if (this.wet)
          {
            if (this.wet)
            {
              Vector2 vector2_1 = this.velocity;
              this.velocity = Collision.TileCollision(this.position, this.velocity, this.width, this.height, this.controlDown, false);
              Vector2 vector2_2 = this.velocity * 0.5f;
              if ((double) this.velocity.X != (double) vector2_1.X)
                vector2_2.X = this.velocity.X;
              if ((double) this.velocity.Y != (double) vector2_1.Y)
                vector2_2.Y = this.velocity.Y;
              this.position += vector2_2;
            }
          }
          else
          {
            this.velocity = Collision.TileCollision(this.position, this.velocity, this.width, this.height, this.controlDown, false);
            this.position += this.velocity;
          }
          if ((double) this.position.X < (double) Main.leftWorld + 336.0 + 16.0)
          {
            this.position.X = (float) ((double) Main.leftWorld + 336.0 + 16.0);
            this.velocity.X = 0.0f;
          }
          if ((double) this.position.X + (double) this.width > (double) Main.rightWorld - 336.0 - 32.0)
          {
            this.position.X = (float) ((double) Main.rightWorld - 336.0 - 32.0) - (float) this.width;
            this.velocity.X = 0.0f;
          }
          if ((double) this.position.Y < (double) Main.topWorld + 336.0 + 16.0)
          {
            this.position.Y = (float) ((double) Main.topWorld + 336.0 + 16.0);
            if ((double) this.velocity.Y < -0.1)
              this.velocity.Y = -0.1f;
          }
          if ((double) this.position.Y > (double) Main.bottomWorld - 336.0 - 32.0 - (double) this.height)
          {
            this.position.Y = (float) ((double) Main.bottomWorld - 336.0 - 32.0) - (float) this.height;
            this.velocity.Y = 0.0f;
          }
          if (Main.ignoreErrors)
          {
            try
            {
              this.ItemCheck(i);
            }
            catch
            {
              Debug.WriteLine("Error: Main.Player[" + (object) i + "].ItemCheck(" + (string) (object) i + ")");
            }
          }
          else
            this.ItemCheck(i);
          this.PlayerFrame();
          if (this.statLife > this.statLifeMax)
            this.statLife = this.statLifeMax;
          this.grappling[0] = -1;
          this.grapCount = 0;
        }
      }
    }

    public bool SellItem(int price)
    {
      if (price <= 0)
      {
        return false;
      }
      else
      {
        Item[] objArray = new Item[44];
        for (int index = 0; index < 44; ++index)
        {
          objArray[index] = new Item();
          objArray[index] = (Item) this.inventory[index].Clone();
        }
        int num = price / 5;
        if (num < 1)
          num = 1;
        bool flag = false;
        while (num >= 1000000 && !flag)
        {
          int index = -1;
          for (int i = 43; i >= 0; --i)
          {
            if (index == -1 && (this.inventory[i].type == 0 || this.inventory[i].stack == 0))
              index = i;
            while (this.inventory[i].type == 74 && this.inventory[i].stack < this.inventory[i].maxStack && num >= 1000000)
            {
              ++this.inventory[i].stack;
              num -= 1000000;
              this.DoCoins(i);
              if (this.inventory[i].stack == 0 && index == -1)
                index = i;
            }
          }
          if (num >= 1000000)
          {
            if (index == -1)
            {
              flag = true;
            }
            else
            {
              this.inventory[index].SetDefaults(74);
              num -= 1000000;
            }
          }
        }
        while (num >= 10000 && !flag)
        {
          int index = -1;
          for (int i = 43; i >= 0; --i)
          {
            if (index == -1 && (this.inventory[i].type == 0 || this.inventory[i].stack == 0))
              index = i;
            while (this.inventory[i].type == 73 && this.inventory[i].stack < this.inventory[i].maxStack && num >= 10000)
            {
              ++this.inventory[i].stack;
              num -= 10000;
              this.DoCoins(i);
              if (this.inventory[i].stack == 0 && index == -1)
                index = i;
            }
          }
          if (num >= 10000)
          {
            if (index == -1)
            {
              flag = true;
            }
            else
            {
              this.inventory[index].SetDefaults(73);
              num -= 10000;
            }
          }
        }
        while (num >= 100 && !flag)
        {
          int index = -1;
          for (int i = 43; i >= 0; --i)
          {
            if (index == -1 && (this.inventory[i].type == 0 || this.inventory[i].stack == 0))
              index = i;
            while (this.inventory[i].type == 72 && this.inventory[i].stack < this.inventory[i].maxStack && num >= 100)
            {
              ++this.inventory[i].stack;
              num -= 100;
              this.DoCoins(i);
              if (this.inventory[i].stack == 0 && index == -1)
                index = i;
            }
          }
          if (num >= 100)
          {
            if (index == -1)
            {
              flag = true;
            }
            else
            {
              this.inventory[index].SetDefaults(72);
              num -= 100;
            }
          }
        }
        while (num >= 1 && !flag)
        {
          int index = -1;
          for (int i = 43; i >= 0; --i)
          {
            if (index == -1 && (this.inventory[i].type == 0 || this.inventory[i].stack == 0))
              index = i;
            while (this.inventory[i].type == 71 && this.inventory[i].stack < this.inventory[i].maxStack && num >= 1)
            {
              ++this.inventory[i].stack;
              --num;
              this.DoCoins(i);
              if (this.inventory[i].stack == 0 && index == -1)
                index = i;
            }
          }
          if (num >= 1)
          {
            if (index == -1)
            {
              flag = true;
            }
            else
            {
              this.inventory[index].SetDefaults(71);
              --num;
            }
          }
        }
        if (flag)
        {
          for (int index = 0; index < 44; ++index)
            this.inventory[index] = (Item) objArray[index].Clone();
          return false;
        }
        else
          return true;
      }
    }

    public bool BuyItem(int price)
    {
      if (price == 0)
      {
        return false;
      }
      else
      {
        int num1 = 0;
        Item[] objArray = new Item[44];
        for (int index = 0; index < 44; ++index)
        {
          objArray[index] = new Item();
          objArray[index] = (Item) this.inventory[index].Clone();
          if (this.inventory[index].type == 71)
            num1 += this.inventory[index].stack;
          if (this.inventory[index].type == 72)
            num1 += this.inventory[index].stack * 100;
          if (this.inventory[index].type == 73)
            num1 += this.inventory[index].stack * 10000;
          if (this.inventory[index].type == 74)
            num1 += this.inventory[index].stack * 1000000;
        }
        if (num1 >= price)
        {
          int num2 = price;
          while (num2 > 0)
          {
            if (num2 >= 1000000)
            {
              for (int index = 0; index < 44; ++index)
              {
                if (this.inventory[index].type == 74)
                {
                  while (this.inventory[index].stack > 0 && num2 >= 1000000)
                  {
                    num2 -= 1000000;
                    --this.inventory[index].stack;
                    if (this.inventory[index].stack == 0)
                      this.inventory[index].type = 0;
                  }
                }
              }
            }
            if (num2 >= 10000)
            {
              for (int index = 0; index < 44; ++index)
              {
                if (this.inventory[index].type == 73)
                {
                  while (this.inventory[index].stack > 0 && num2 >= 10000)
                  {
                    num2 -= 10000;
                    --this.inventory[index].stack;
                    if (this.inventory[index].stack == 0)
                      this.inventory[index].type = 0;
                  }
                }
              }
            }
            if (num2 >= 100)
            {
              for (int index = 0; index < 44; ++index)
              {
                if (this.inventory[index].type == 72)
                {
                  while (this.inventory[index].stack > 0 && num2 >= 100)
                  {
                    num2 -= 100;
                    --this.inventory[index].stack;
                    if (this.inventory[index].stack == 0)
                      this.inventory[index].type = 0;
                  }
                }
              }
            }
            if (num2 >= 1)
            {
              for (int index = 0; index < 44; ++index)
              {
                if (this.inventory[index].type == 71)
                {
                  while (this.inventory[index].stack > 0 && num2 >= 1)
                  {
                    --num2;
                    --this.inventory[index].stack;
                    if (this.inventory[index].stack == 0)
                      this.inventory[index].type = 0;
                  }
                }
              }
            }
            if (num2 > 0)
            {
              int index1 = -1;
              for (int index2 = 43; index2 >= 0; --index2)
              {
                if (this.inventory[index2].type == 0 || this.inventory[index2].stack == 0)
                {
                  index1 = index2;
                  break;
                }
              }
              if (index1 >= 0)
              {
                bool flag = true;
                if (num2 >= 10000)
                {
                  for (int index2 = 0; index2 < 44; ++index2)
                  {
                    if (this.inventory[index2].type == 74 && this.inventory[index2].stack >= 1)
                    {
                      --this.inventory[index2].stack;
                      if (this.inventory[index2].stack == 0)
                        this.inventory[index2].type = 0;
                      this.inventory[index1].SetDefaults(73);
                      this.inventory[index1].stack = 100;
                      flag = false;
                      break;
                    }
                  }
                }
                else if (num2 >= 100)
                {
                  for (int index2 = 0; index2 < 44; ++index2)
                  {
                    if (this.inventory[index2].type == 73 && this.inventory[index2].stack >= 1)
                    {
                      --this.inventory[index2].stack;
                      if (this.inventory[index2].stack == 0)
                        this.inventory[index2].type = 0;
                      this.inventory[index1].SetDefaults(72);
                      this.inventory[index1].stack = 100;
                      flag = false;
                      break;
                    }
                  }
                }
                else if (num2 >= 1)
                {
                  for (int index2 = 0; index2 < 44; ++index2)
                  {
                    if (this.inventory[index2].type == 72 && this.inventory[index2].stack >= 1)
                    {
                      --this.inventory[index2].stack;
                      if (this.inventory[index2].stack == 0)
                        this.inventory[index2].type = 0;
                      this.inventory[index1].SetDefaults(71);
                      this.inventory[index1].stack = 100;
                      flag = false;
                      break;
                    }
                  }
                }
                if (flag)
                {
                  if (num2 < 10000)
                  {
                    for (int index2 = 0; index2 < 44; ++index2)
                    {
                      if (this.inventory[index2].type == 73 && this.inventory[index2].stack >= 1)
                      {
                        --this.inventory[index2].stack;
                        if (this.inventory[index2].stack == 0)
                          this.inventory[index2].type = 0;
                        this.inventory[index1].SetDefaults(72);
                        this.inventory[index1].stack = 100;
                        flag = false;
                        break;
                      }
                    }
                  }
                  if (flag && num2 < 1000000)
                  {
                    for (int index2 = 0; index2 < 44; ++index2)
                    {
                      if (this.inventory[index2].type == 74 && this.inventory[index2].stack >= 1)
                      {
                        --this.inventory[index2].stack;
                        if (this.inventory[index2].stack == 0)
                          this.inventory[index2].type = 0;
                        this.inventory[index1].SetDefaults(73);
                        this.inventory[index1].stack = 100;
                        break;
                      }
                    }
                  }
                }
              }
              else
              {
                for (int index2 = 0; index2 < 44; ++index2)
                  this.inventory[index2] = (Item) objArray[index2].Clone();
                return false;
              }
            }
          }
          return true;
        }
        else
          return false;
      }
    }

    public void AdjTiles()
    {
      int num1 = 4;
      int num2 = 3;
      for (int index = 0; index < 80; ++index)
      {
        this.oldAdjTile[index] = this.adjTile[index];
        this.adjTile[index] = false;
      }
      int num3 = (int) (((double) this.position.X + (double) (this.width / 2)) / 16.0);
      int num4 = (int) (((double) this.position.Y + (double) this.height) / 16.0);
      for (int index1 = num3 - num1; index1 <= num3 + num1; ++index1)
      {
        for (int index2 = num4 - num2; index2 < num4 + num2; ++index2)
        {
          if (Main.tile[index1, index2].active)
          {
            this.adjTile[(int) Main.tile[index1, index2].type] = true;
            if ((int) Main.tile[index1, index2].type == 77)
              this.adjTile[17] = true;
          }
        }
      }
      if (Main.playerInventory)
      {
        bool flag = false;
        for (int index = 0; index < 80; ++index)
        {
          if (this.oldAdjTile[index] != this.adjTile[index])
          {
            flag = true;
            break;
          }
        }
        if (flag)
          Recipe.FindRecipes();
      }
    }

    public void PlayerFrame()
    {
      if (this.swimTime > 0)
      {
        --this.swimTime;
        if (!this.wet)
          this.swimTime = 0;
      }
      this.head = this.armor[0].headSlot;
      this.body = this.armor[1].bodySlot;
      this.legs = this.armor[2].legSlot;
      if (!this.hostile)
      {
        if (this.armor[8].headSlot >= 0)
          this.head = this.armor[8].headSlot;
        if (this.armor[9].bodySlot >= 0)
          this.body = this.armor[9].bodySlot;
        if (this.armor[10].legSlot >= 0)
          this.legs = this.armor[10].legSlot;
      }
      if (this.head == 5 && this.body == 5 && this.legs == 5 && Main.rand.Next(10) == 0)
        Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 14, 0.0f, 0.0f, 200, new Color(), 1.2f);
      if (this.head == 6 && this.body == 6 && this.legs == 6 && ((double) Math.Abs(this.velocity.X) + (double) Math.Abs(this.velocity.Y) > 1.0 && !this.rocketFrame))
      {
        for (int index1 = 0; index1 < 2; ++index1)
        {
          int index2 = Dust.NewDust(new Vector2(this.position.X - this.velocity.X * 2f, (float) ((double) this.position.Y - 2.0 - (double) this.velocity.Y * 2.0)), this.width, this.height, 6, 0.0f, 0.0f, 100, new Color(), 2f);
          Main.dust[index2].noGravity = true;
          Main.dust[index2].velocity.X -= this.velocity.X * 0.5f;
          Main.dust[index2].velocity.Y -= this.velocity.Y * 0.5f;
        }
      }
      if (this.head == 7 && this.body == 7 && this.legs == 7)
        this.boneArmor = true;
      if (this.head == 8 && this.body == 8 && this.legs == 8 && (double) Math.Abs(this.velocity.X) + (double) Math.Abs(this.velocity.Y) > 1.0)
      {
        int index = Dust.NewDust(new Vector2(this.position.X - this.velocity.X * 2f, (float) ((double) this.position.Y - 2.0 - (double) this.velocity.Y * 2.0)), this.width, this.height, 40, 0.0f, 0.0f, 50, new Color(), 1.4f);
        Main.dust[index].noGravity = true;
        Main.dust[index].velocity.X = this.velocity.X * 0.25f;
        Main.dust[index].velocity.Y = this.velocity.Y * 0.25f;
      }
      if (this.head == 9 && this.body == 9 && this.legs == 9 && ((double) Math.Abs(this.velocity.X) + (double) Math.Abs(this.velocity.Y) > 1.0 && !this.rocketFrame))
      {
        for (int index1 = 0; index1 < 2; ++index1)
        {
          int index2 = Dust.NewDust(new Vector2(this.position.X - this.velocity.X * 2f, (float) ((double) this.position.Y - 2.0 - (double) this.velocity.Y * 2.0)), this.width, this.height, 6, 0.0f, 0.0f, 100, new Color(), 2f);
          Main.dust[index2].noGravity = true;
          Main.dust[index2].velocity.X -= this.velocity.X * 0.5f;
          Main.dust[index2].velocity.Y -= this.velocity.Y * 0.5f;
        }
      }
      this.bodyFrame.Width = 40;
      this.bodyFrame.Height = 56;
      this.legFrame.Width = 40;
      this.legFrame.Height = 56;
      this.bodyFrame.X = 0;
      this.legFrame.X = 0;
      if (this.itemAnimation > 0 && this.inventory[this.selectedItem].useStyle != 10)
      {
        if (this.inventory[this.selectedItem].useStyle == 1 || this.inventory[this.selectedItem].type == 0)
          this.bodyFrame.Y = (double) this.itemAnimation >= (double) this.itemAnimationMax * 0.333 ? ((double) this.itemAnimation >= (double) this.itemAnimationMax * 0.666 ? this.bodyFrame.Height : this.bodyFrame.Height * 2) : this.bodyFrame.Height * 3;
        else if (this.inventory[this.selectedItem].useStyle == 2)
          this.bodyFrame.Y = (double) this.itemAnimation <= (double) this.itemAnimationMax * 0.5 ? this.bodyFrame.Height * 2 : this.bodyFrame.Height * 3;
        else if (this.inventory[this.selectedItem].useStyle == 3)
          this.bodyFrame.Y = (double) this.itemAnimation <= (double) this.itemAnimationMax * 0.666 ? this.bodyFrame.Height * 3 : this.bodyFrame.Height * 3;
        else if (this.inventory[this.selectedItem].useStyle == 4)
          this.bodyFrame.Y = this.bodyFrame.Height * 2;
        else if (this.inventory[this.selectedItem].useStyle == 5)
        {
          float num = this.itemRotation * (float) this.direction;
          this.bodyFrame.Y = this.bodyFrame.Height * 3;
          if ((double) num < -0.75)
            this.bodyFrame.Y = this.bodyFrame.Height * 2;
          if ((double) num > 0.6)
            this.bodyFrame.Y = this.bodyFrame.Height * 4;
        }
      }
      else if (this.inventory[this.selectedItem].holdStyle == 1)
        this.bodyFrame.Y = this.bodyFrame.Height * 3;
      else if (this.inventory[this.selectedItem].holdStyle == 2)
        this.bodyFrame.Y = this.bodyFrame.Height * 2;
      else if (this.grappling[0] >= 0)
      {
        Vector2 vector2 = new Vector2(this.position.X + (float) this.width * 0.5f, this.position.Y + (float) this.height * 0.5f);
        float num1 = 0.0f;
        float num2 = 0.0f;
        for (int index = 0; index < this.grapCount; ++index)
        {
          num1 += Main.projectile[this.grappling[index]].position.X + (float) (Main.projectile[this.grappling[index]].width / 2);
          num2 += Main.projectile[this.grappling[index]].position.Y + (float) (Main.projectile[this.grappling[index]].height / 2);
        }
        float num3 = num1 / (float) this.grapCount;
        float num4 = num2 / (float) this.grapCount;
        float num5 = num3 - vector2.X;
        float num6 = num4 - vector2.Y;
        this.bodyFrame.Y = (double) num6 >= 0.0 || (double) Math.Abs(num6) <= (double) Math.Abs(num5) ? ((double) num6 <= 0.0 || (double) Math.Abs(num6) <= (double) Math.Abs(num5) ? this.bodyFrame.Height * 3 : this.bodyFrame.Height * 4) : this.bodyFrame.Height * 2;
      }
      else if (this.swimTime > 0)
        this.bodyFrame.Y = this.swimTime <= 20 ? (this.swimTime <= 10 ? 0 : this.bodyFrame.Height * 5) : 0;
      else if ((double) this.velocity.Y != 0.0)
      {
        this.bodyFrameCounter = 0.0;
        this.bodyFrame.Y = this.bodyFrame.Height * 5;
      }
      else if ((double) this.velocity.X != 0.0)
      {
        this.bodyFrameCounter += (double) Math.Abs(this.velocity.X) * 1.5;
        this.bodyFrame.Y = this.legFrame.Y;
      }
      else
      {
        this.bodyFrameCounter = 0.0;
        this.bodyFrame.Y = 0;
      }
      if (this.swimTime > 0)
      {
        this.legFrameCounter += 2.0;
        while (this.legFrameCounter > 8.0)
        {
          this.legFrameCounter -= 8.0;
          this.legFrame.Y += this.legFrame.Height;
        }
        if (this.legFrame.Y < this.legFrame.Height * 7)
          this.legFrame.Y = this.legFrame.Height * 19;
        else if (this.legFrame.Y > this.legFrame.Height * 19)
          this.legFrame.Y = this.legFrame.Height * 7;
      }
      else if ((double) this.velocity.Y != 0.0 || this.grappling[0] > -1)
      {
        this.legFrameCounter = 0.0;
        this.legFrame.Y = this.legFrame.Height * 5;
      }
      else if ((double) this.velocity.X != 0.0)
      {
        this.legFrameCounter += (double) Math.Abs(this.velocity.X) * 1.3;
        while (this.legFrameCounter > 8.0)
        {
          this.legFrameCounter -= 8.0;
          this.legFrame.Y += this.legFrame.Height;
        }
        if (this.legFrame.Y < this.legFrame.Height * 7)
          this.legFrame.Y = this.legFrame.Height * 19;
        else if (this.legFrame.Y > this.legFrame.Height * 19)
          this.legFrame.Y = this.legFrame.Height * 7;
      }
      else
      {
        this.legFrameCounter = 0.0;
        this.legFrame.Y = 0;
      }
    }

    public void Spawn()
    {
      if (this.whoAmi == Main.myPlayer)
      {
        this.FindSpawn();
        if (!Player.CheckSpawn(this.SpawnX, this.SpawnY))
        {
          this.SpawnX = -1;
          this.SpawnY = -1;
        }
      }
      if (Main.netMode == 1 && this.whoAmi == Main.myPlayer)
      {
        NetMessage.SendData(12, -1, -1, "", Main.myPlayer, 0.0f, 0.0f, 0.0f);
        Main.gameMenu = false;
      }
      this.headPosition = new Vector2();
      this.bodyPosition = new Vector2();
      this.legPosition = new Vector2();
      this.headRotation = 0.0f;
      this.bodyRotation = 0.0f;
      this.legRotation = 0.0f;
      if (this.statLife <= 0)
      {
        this.statLife = 100;
        this.breath = this.breathMax;
        if (this.spawnMax)
        {
          this.statLife = this.statLifeMax;
          this.statMana = this.statManaMax;
        }
      }
      this.immune = true;
      this.dead = false;
      this.immuneTime = 0;
      this.active = true;
      if (this.SpawnX >= 0 && this.SpawnY >= 0)
      {
        this.position.X = (float) (this.SpawnX * 16 + 8 - this.width / 2);
        this.position.Y = (float) (this.SpawnY * 16 - this.height);
      }
      else
      {
        this.position.X = (float) (Main.spawnTileX * 16 + 8 - this.width / 2);
        this.position.Y = (float) (Main.spawnTileY * 16 - this.height);
        for (int i = Main.spawnTileX - 1; i < Main.spawnTileX + 2; ++i)
        {
          for (int j = Main.spawnTileY - 3; j < Main.spawnTileY; ++j)
          {
            if (Main.tileSolid[(int) Main.tile[i, j].type] && !Main.tileSolidTop[(int) Main.tile[i, j].type])
              WorldGen.KillTile(i, j, false, false, false);
            if ((int) Main.tile[i, j].liquid > 0)
            {
              Main.tile[i, j].lava = false;
              Main.tile[i, j].liquid = (byte) 0;
              WorldGen.SquareTileFrame(i, j, true);
            }
          }
        }
      }
      this.wet = false;
      this.wetCount = (byte) 0;
      this.lavaWet = false;
      this.fallStart = (int) ((double) this.position.Y / 16.0);
      this.velocity.X = 0.0f;
      this.velocity.Y = 0.0f;
      this.talkNPC = -1;
      if (this.pvpDeath)
      {
        this.pvpDeath = false;
        this.immuneTime = 300;
        this.statLife = this.statLifeMax;
      }
      else
        this.immuneTime = 60;
      if (this.whoAmi == Main.myPlayer)
      {
        Lighting.lightCounter = Lighting.lightSkip + 1;
        Main.screenPosition.X = this.position.X + (float) (this.width / 2) - (float) (Main.screenWidth / 2);
        Main.screenPosition.Y = this.position.Y + (float) (this.height / 2) - (float) (Main.screenHeight / 2);
      }
    }

    public double Hurt(int Damage, int hitDirection, bool pvp = false, bool quiet = false)
    {
      if (!this.immune)
      {
        int Damage1 = Damage;
        if (pvp)
          Damage1 *= 2;
        double dmg = Main.CalculateDamage(Damage1, this.statDefense);
        if (dmg >= 1.0)
        {
          if (Main.netMode == 1 && this.whoAmi == Main.myPlayer && !quiet)
          {
            int num = 0;
            if (pvp)
              num = 1;
            NetMessage.SendData(13, -1, -1, "", this.whoAmi, 0.0f, 0.0f, 0.0f);
            NetMessage.SendData(16, -1, -1, "", this.whoAmi, 0.0f, 0.0f, 0.0f);
            NetMessage.SendData(26, -1, -1, "", this.whoAmi, (float) hitDirection, (float) Damage, (float) num);
          }
          CombatText.NewText(new Rectangle((int) this.position.X, (int) this.position.Y, this.width, this.height), new Color((int) byte.MaxValue, 80, 90, (int) byte.MaxValue), string.Concat((object) (int) dmg));
          this.statLife -= (int) dmg;
          this.immune = true;
          this.immuneTime = 40;
          if (pvp)
            this.immuneTime = 8;
          if (!this.noKnockback && hitDirection != 0)
          {
            this.velocity.X = 4.5f * (float) hitDirection;
            this.velocity.Y = -3.5f;
          }
          if (this.boneArmor)
            Main.PlaySound(3, (int) this.position.X, (int) this.position.Y, 2);
          else if (this.hair == 5 || this.hair == 6 || this.hair == 9 || this.hair == 11)
            Main.PlaySound(20, (int) this.position.X, (int) this.position.Y, 1);
          else
            Main.PlaySound(1, (int) this.position.X, (int) this.position.Y, 1);
          if (this.statLife > 0)
          {
            for (int index = 0; (double) index < dmg / (double) this.statLifeMax * 100.0; ++index)
            {
              if (this.boneArmor)
                Dust.NewDust(this.position, this.width, this.height, 26, (float) (2 * hitDirection), -2f, 0, new Color(), 1f);
              else
                Dust.NewDust(this.position, this.width, this.height, 5, (float) (2 * hitDirection), -2f, 0, new Color(), 1f);
            }
          }
          else
          {
            this.statLife = 0;
            if (this.whoAmi == Main.myPlayer)
              this.KillMe(dmg, hitDirection, pvp);
          }
        }
        if (pvp)
          dmg = Main.CalculateDamage(Damage1, this.statDefense);
        return dmg;
      }
      else
        return 0.0;
    }

    public void KillMe(double dmg, int hitDirection, bool pvp = false)
    {
      if (!this.dead)
      {
        if (pvp)
          this.pvpDeath = true;
        Main.PlaySound(5, (int) this.position.X, (int) this.position.Y, 1);
        this.headVelocity.Y = (float) Main.rand.Next(-40, -10) * 0.1f;
        this.bodyVelocity.Y = (float) Main.rand.Next(-40, -10) * 0.1f;
        this.legVelocity.Y = (float) Main.rand.Next(-40, -10) * 0.1f;
        this.headVelocity.X = (float) Main.rand.Next(-20, 21) * 0.1f + (float) (2 * hitDirection);
        this.bodyVelocity.X = (float) Main.rand.Next(-20, 21) * 0.1f + (float) (2 * hitDirection);
        this.legVelocity.X = (float) Main.rand.Next(-20, 21) * 0.1f + (float) (2 * hitDirection);
        for (int index = 0; (double) index < 20.0 + dmg / (double) this.statLifeMax * 100.0; ++index)
        {
          if (this.boneArmor)
            Dust.NewDust(this.position, this.width, this.height, 26, (float) (2 * hitDirection), -2f, 0, new Color(), 1f);
          else
            Dust.NewDust(this.position, this.width, this.height, 5, (float) (2 * hitDirection), -2f, 0, new Color(), 1f);
        }
        this.dead = true;
        this.respawnTimer = 600;
        this.immuneAlpha = 0;
        if (Main.netMode == 2)
          NetMessage.SendData(25, -1, -1, this.name + " was slain...", (int) byte.MaxValue, 225f, 25f, 25f);
        if (this.whoAmi == Main.myPlayer)
          WorldGen.saveToonWhilePlaying();
        if (Main.netMode == 1 && this.whoAmi == Main.myPlayer)
        {
          int num = 0;
          if (pvp)
            num = 1;
          NetMessage.SendData(44, -1, -1, "", this.whoAmi, (float) hitDirection, (float) (int) dmg, (float) num);
        }
        if (!pvp && this.whoAmi == Main.myPlayer)
          this.DropItems();
      }
    }

    public bool ItemSpace(Item newItem)
    {
      if (newItem.type == 58 || newItem.type == 184)
      {
        return true;
      }
      else
      {
        int num = 40;
        if (newItem.type == 71 || newItem.type == 72 || newItem.type == 73 || newItem.type == 74)
          num = 44;
        for (int index = 0; index < num; ++index)
        {
          if (this.inventory[index].type == 0)
            return true;
        }
        for (int index = 0; index < num; ++index)
        {
          if (this.inventory[index].type > 0 && this.inventory[index].stack < this.inventory[index].maxStack && newItem.IsTheSameAs(this.inventory[index]))
            return true;
        }
        return false;
      }
    }

    public void DoCoins(int i)
    {
      if (this.inventory[i].stack == 100 && (this.inventory[i].type == 71 || this.inventory[i].type == 72 || this.inventory[i].type == 73))
      {
        this.inventory[i].SetDefaults(this.inventory[i].type + 1);
        for (int i1 = 0; i1 < 44; ++i1)
        {
          if (this.inventory[i1].IsTheSameAs(this.inventory[i]) && i1 != i && this.inventory[i1].stack < this.inventory[i1].maxStack)
          {
            ++this.inventory[i1].stack;
            this.inventory[i].SetDefaults("");
            this.inventory[i].active = false;
            this.inventory[i].name = "";
            this.inventory[i].type = 0;
            this.inventory[i].stack = 0;
            this.DoCoins(i1);
          }
        }
      }
    }

    public Item GetItem(int plr, Item newItem)
    {
      Item obj = newItem;
      if (newItem.noGrabDelay > 0)
      {
        return obj;
      }
      else
      {
        int num = 0;
        if (newItem.type == 71 || newItem.type == 72 || newItem.type == 73 || newItem.type == 74)
          num = -4;
        for (int index = num; index < 40; ++index)
        {
          int i = index;
          if (i < 0)
            i = 44 + index;
          if (this.inventory[i].type > 0 && this.inventory[i].stack < this.inventory[i].maxStack && obj.IsTheSameAs(this.inventory[i]))
          {
            Main.PlaySound(7, (int) this.position.X, (int) this.position.Y, 1);
            if (obj.stack + this.inventory[i].stack <= this.inventory[i].maxStack)
            {
              this.inventory[i].stack += obj.stack;
              this.DoCoins(i);
              if (plr == Main.myPlayer)
                Recipe.FindRecipes();
              return new Item();
            }
            else
            {
              obj.stack -= this.inventory[i].maxStack - this.inventory[i].stack;
              this.inventory[i].stack = this.inventory[i].maxStack;
              this.DoCoins(i);
              if (plr == Main.myPlayer)
                Recipe.FindRecipes();
            }
          }
        }
        for (int index = num; index < 40; ++index)
        {
          int i = index;
          if (i < 0)
            i = 44 + index;
          if (this.inventory[i].type == 0)
          {
            this.inventory[i] = obj;
            this.DoCoins(i);
            Main.PlaySound(7, (int) this.position.X, (int) this.position.Y, 1);
            if (plr == Main.myPlayer)
              Recipe.FindRecipes();
            return new Item();
          }
        }
        return obj;
      }
    }

    public void ItemCheck(int i)
    {
      if (this.inventory[this.selectedItem].autoReuse)
      {
        this.releaseUseItem = true;
        if (this.itemAnimation == 1 && this.inventory[this.selectedItem].stack > 0)
          this.itemAnimation = 0;
      }
      if (this.controlUseItem && this.itemAnimation == 0 && this.releaseUseItem && this.inventory[this.selectedItem].useStyle > 0)
      {
        bool flag = true;
        if (this.inventory[this.selectedItem].shoot == 6 || this.inventory[this.selectedItem].shoot == 19 || this.inventory[this.selectedItem].shoot == 33)
        {
          for (int index = 0; index < 1000; ++index)
          {
            if (Main.projectile[index].active && Main.projectile[index].owner == Main.myPlayer && Main.projectile[index].type == this.inventory[this.selectedItem].shoot)
              flag = false;
          }
        }
        if (this.inventory[this.selectedItem].potion)
        {
          if (this.potionDelay <= 0)
            this.potionDelay = Item.potionDelay;
          else
            flag = false;
        }
        if (this.inventory[this.selectedItem].mana > 0 && (this.inventory[this.selectedItem].type != (int) sbyte.MaxValue || !this.spaceGun))
        {
          if (this.statMana >= (int) ((double) this.inventory[this.selectedItem].mana * (double) this.manaCost))
            this.statMana -= (int) ((double) this.inventory[this.selectedItem].mana * (double) this.manaCost);
          else
            flag = false;
        }
        if (this.inventory[this.selectedItem].type == 43 && Main.dayTime)
          flag = false;
        if (this.inventory[this.selectedItem].type == 70 && !this.zoneEvil)
          flag = false;
        if (this.inventory[this.selectedItem].shoot == 17 && flag && i == Main.myPlayer)
        {
          int i1 = (int) ((double) Main.mouseState.X + (double) Main.screenPosition.X) / 16;
          int j = (int) ((double) Main.mouseState.Y + (double) Main.screenPosition.Y) / 16;
          if (Main.tile[i1, j].active && ((int) Main.tile[i1, j].type == 0 || (int) Main.tile[i1, j].type == 2 || (int) Main.tile[i1, j].type == 23))
          {
            WorldGen.KillTile(i1, j, false, false, true);
            if (!Main.tile[i1, j].active)
            {
              if (Main.netMode == 1)
                NetMessage.SendData(17, -1, -1, "", 4, (float) i1, (float) j, 0.0f);
            }
            else
              flag = false;
          }
          else
            flag = false;
        }
        if (flag && this.inventory[this.selectedItem].useAmmo > 0)
        {
          flag = false;
          for (int index = 0; index < 44; ++index)
          {
            if (this.inventory[index].ammo == this.inventory[this.selectedItem].useAmmo && this.inventory[index].stack > 0)
            {
              flag = true;
              break;
            }
          }
        }
        if (flag)
        {
          if (this.grappling[0] > -1)
          {
            if (this.controlRight)
              this.direction = 1;
            else if (this.controlLeft)
              this.direction = -1;
          }
          this.channel = this.inventory[this.selectedItem].channel;
          this.attackCD = 0;
          if (this.inventory[this.selectedItem].shoot > 0 || this.inventory[this.selectedItem].damage == 0)
            this.meleeSpeed = 1f;
          this.itemAnimation = (int) ((double) this.inventory[this.selectedItem].useAnimation * (double) this.meleeSpeed);
          this.itemAnimationMax = (int) ((double) this.inventory[this.selectedItem].useAnimation * (double) this.meleeSpeed);
          if (this.inventory[this.selectedItem].useSound > 0)
            Main.PlaySound(2, (int) this.position.X, (int) this.position.Y, this.inventory[this.selectedItem].useSound);
        }
        if (flag && this.inventory[this.selectedItem].shoot == 18)
        {
          for (int index = 0; index < 1000; ++index)
          {
            if (Main.projectile[index].active && Main.projectile[index].owner == i && Main.projectile[index].type == this.inventory[this.selectedItem].shoot)
              Main.projectile[index].Kill();
          }
        }
      }
      if (!this.controlUseItem)
        this.channel = false;
      if (this.itemAnimation > 0)
      {
        if (this.inventory[this.selectedItem].mana > 0)
          this.manaRegenDelay = 180;
        if (Main.dedServ)
        {
          this.itemHeight = this.inventory[this.selectedItem].height;
          this.itemWidth = this.inventory[this.selectedItem].width;
        }
        else
        {
          this.itemHeight = Main.itemTexture[this.inventory[this.selectedItem].type].Height;
          this.itemWidth = Main.itemTexture[this.inventory[this.selectedItem].type].Width;
        }
        --this.itemAnimation;
        if (!Main.dedServ)
        {
          if (this.inventory[this.selectedItem].useStyle == 1)
          {
            if ((double) this.itemAnimation < (double) this.itemAnimationMax * 0.333)
            {
              float num = 10f;
              if (Main.itemTexture[this.inventory[this.selectedItem].type].Width > 32)
                num = 14f;
              this.itemLocation.X = (float) ((double) this.position.X + (double) this.width * 0.5 + ((double) Main.itemTexture[this.inventory[this.selectedItem].type].Width * 0.5 - (double) num) * (double) this.direction);
              this.itemLocation.Y = this.position.Y + 24f;
            }
            else if ((double) this.itemAnimation < (double) this.itemAnimationMax * 0.666)
            {
              float num1 = 10f;
              if (Main.itemTexture[this.inventory[this.selectedItem].type].Width > 32)
                num1 = 18f;
              this.itemLocation.X = (float) ((double) this.position.X + (double) this.width * 0.5 + ((double) Main.itemTexture[this.inventory[this.selectedItem].type].Width * 0.5 - (double) num1) * (double) this.direction);
              float num2 = 10f;
              if (Main.itemTexture[this.inventory[this.selectedItem].type].Height > 32)
                num2 = 8f;
              this.itemLocation.Y = this.position.Y + num2;
            }
            else
            {
              float num1 = 6f;
              if (Main.itemTexture[this.inventory[this.selectedItem].type].Width > 32)
                num1 = 14f;
              this.itemLocation.X = (float) ((double) this.position.X + (double) this.width * 0.5 - ((double) Main.itemTexture[this.inventory[this.selectedItem].type].Width * 0.5 - (double) num1) * (double) this.direction);
              float num2 = 10f;
              if (Main.itemTexture[this.inventory[this.selectedItem].type].Height > 32)
                num2 = 10f;
              this.itemLocation.Y = this.position.Y + num2;
            }
            this.itemRotation = (float) (((double) this.itemAnimation / (double) this.itemAnimationMax - 0.5) * (double) -this.direction * 3.5 - (double) this.direction * 0.300000011920929);
          }
          else if (this.inventory[this.selectedItem].useStyle == 2)
          {
            this.itemRotation = (float) ((double) this.itemAnimation / (double) this.itemAnimationMax * (double) this.direction * 2.0 + -1.39999997615814 * (double) this.direction);
            if ((double) this.itemAnimation < (double) this.itemAnimationMax * 0.5)
            {
              this.itemLocation.X = (float) ((double) this.position.X + (double) this.width * 0.5 + ((double) Main.itemTexture[this.inventory[this.selectedItem].type].Width * 0.5 - 9.0 - (double) this.itemRotation * 12.0 * (double) this.direction) * (double) this.direction);
              this.itemLocation.Y = (float) ((double) this.position.Y + 38.0 + (double) this.itemRotation * (double) this.direction * 4.0);
            }
            else
            {
              this.itemLocation.X = (float) ((double) this.position.X + (double) this.width * 0.5 + ((double) Main.itemTexture[this.inventory[this.selectedItem].type].Width * 0.5 - 9.0 - (double) this.itemRotation * 16.0 * (double) this.direction) * (double) this.direction);
              this.itemLocation.Y = (float) ((double) this.position.Y + 38.0 + (double) this.itemRotation * (double) this.direction);
            }
          }
          else if (this.inventory[this.selectedItem].useStyle == 3)
          {
            if ((double) this.itemAnimation > (double) this.itemAnimationMax * 0.666)
            {
              this.itemLocation.X = -1000f;
              this.itemLocation.Y = -1000f;
              this.itemRotation = -1.3f * (float) this.direction;
            }
            else
            {
              this.itemLocation.X = (float) ((double) this.position.X + (double) this.width * 0.5 + ((double) Main.itemTexture[this.inventory[this.selectedItem].type].Width * 0.5 - 4.0) * (double) this.direction);
              this.itemLocation.Y = this.position.Y + 24f;
              float num = (float) ((double) this.itemAnimation / (double) this.itemAnimationMax * (double) Main.itemTexture[this.inventory[this.selectedItem].type].Width * (double) this.direction * (double) this.inventory[this.selectedItem].scale * 1.20000004768372) - (float) (10 * this.direction);
              if ((double) num > -4.0 && this.direction == -1)
                num = -8f;
              if ((double) num < 4.0 && this.direction == 1)
                num = 8f;
              this.itemLocation.X -= num;
              this.itemRotation = 0.8f * (float) this.direction;
            }
          }
          else if (this.inventory[this.selectedItem].useStyle == 4)
          {
            this.itemRotation = 0.0f;
            this.itemLocation.X = (float) ((double) this.position.X + (double) this.width * 0.5 + ((double) Main.itemTexture[this.inventory[this.selectedItem].type].Width * 0.5 - 9.0 - (double) this.itemRotation * 14.0 * (double) this.direction) * (double) this.direction);
            this.itemLocation.Y = this.position.Y + (float) Main.itemTexture[this.inventory[this.selectedItem].type].Height * 0.5f;
          }
          else if (this.inventory[this.selectedItem].useStyle == 5)
          {
            this.itemLocation.X = (float) ((double) this.position.X + (double) this.width * 0.5 - (double) Main.itemTexture[this.inventory[this.selectedItem].type].Width * 0.5) - (float) (this.direction * 2);
            this.itemLocation.Y = (float) ((double) this.position.Y + (double) this.height * 0.5 - (double) Main.itemTexture[this.inventory[this.selectedItem].type].Height * 0.5);
          }
        }
      }
      else if (this.inventory[this.selectedItem].holdStyle == 1)
      {
        this.itemLocation.X = !Main.dedServ ? (float) ((double) this.position.X + (double) this.width * 0.5 + ((double) Main.itemTexture[this.inventory[this.selectedItem].type].Width * 0.5 + 4.0) * (double) this.direction) : (float) ((double) this.position.X + (double) this.width * 0.5 + 20.0 * (double) this.direction);
        this.itemLocation.Y = this.position.Y + 24f;
        this.itemRotation = 0.0f;
      }
      else if (this.inventory[this.selectedItem].holdStyle == 2)
      {
        this.itemLocation.X = this.position.X + (float) this.width * 0.5f + (float) (6 * this.direction);
        this.itemLocation.Y = this.position.Y + 16f;
        this.itemRotation = 0.79f * (float) -this.direction;
      }
      if (this.inventory[this.selectedItem].type == 8)
      {
        int maxValue = 20;
        if (this.itemAnimation > 0)
          maxValue = 7;
        if (this.direction == -1)
        {
          if (Main.rand.Next(maxValue) == 0)
            Dust.NewDust(new Vector2(this.itemLocation.X - 16f, this.itemLocation.Y - 14f), 4, 4, 6, 0.0f, 0.0f, 100, new Color(), 1f);
          Lighting.addLight((int) (((double) this.itemLocation.X - 16.0 + (double) this.velocity.X) / 16.0), (int) (((double) this.itemLocation.Y - 14.0) / 16.0), 1f);
        }
        else
        {
          if (Main.rand.Next(maxValue) == 0)
            Dust.NewDust(new Vector2(this.itemLocation.X + 6f, this.itemLocation.Y - 14f), 4, 4, 6, 0.0f, 0.0f, 100, new Color(), 1f);
          Lighting.addLight((int) (((double) this.itemLocation.X + 6.0 + (double) this.velocity.X) / 16.0), (int) (((double) this.itemLocation.Y - 14.0) / 16.0), 1f);
        }
      }
      else if (this.inventory[this.selectedItem].type == 105)
      {
        int maxValue = 20;
        if (this.itemAnimation > 0)
          maxValue = 7;
        if (this.direction == -1)
        {
          if (Main.rand.Next(maxValue) == 0)
            Dust.NewDust(new Vector2(this.itemLocation.X - 12f, this.itemLocation.Y - 20f), 4, 4, 6, 0.0f, 0.0f, 100, new Color(), 1f);
          Lighting.addLight((int) (((double) this.itemLocation.X - 16.0 + (double) this.velocity.X) / 16.0), (int) (((double) this.itemLocation.Y - 14.0) / 16.0), 1f);
        }
        else
        {
          if (Main.rand.Next(maxValue) == 0)
            Dust.NewDust(new Vector2(this.itemLocation.X + 4f, this.itemLocation.Y - 20f), 4, 4, 6, 0.0f, 0.0f, 100, new Color(), 1f);
          Lighting.addLight((int) (((double) this.itemLocation.X + 6.0 + (double) this.velocity.X) / 16.0), (int) (((double) this.itemLocation.Y - 14.0) / 16.0), 1f);
        }
      }
      else if (this.inventory[this.selectedItem].type == 148)
      {
        int maxValue = 10;
        if (this.itemAnimation > 0)
          maxValue = 7;
        if (this.direction == -1)
        {
          if (Main.rand.Next(maxValue) == 0)
            Dust.NewDust(new Vector2(this.itemLocation.X - 12f, this.itemLocation.Y - 20f), 4, 4, 29, 0.0f, 0.0f, 100, new Color(), 1f);
          Lighting.addLight((int) (((double) this.itemLocation.X - 16.0 + (double) this.velocity.X) / 16.0), (int) (((double) this.itemLocation.Y - 14.0) / 16.0), 1f);
        }
        else
        {
          if (Main.rand.Next(maxValue) == 0)
            Dust.NewDust(new Vector2(this.itemLocation.X + 4f, this.itemLocation.Y - 20f), 4, 4, 29, 0.0f, 0.0f, 100, new Color(), 1f);
          Lighting.addLight((int) (((double) this.itemLocation.X + 6.0 + (double) this.velocity.X) / 16.0), (int) (((double) this.itemLocation.Y - 14.0) / 16.0), 1f);
        }
      }
      this.releaseUseItem = !this.controlUseItem && true;
      if (this.itemTime > 0)
        --this.itemTime;
      if (i == Main.myPlayer)
      {
        if (this.inventory[this.selectedItem].shoot > 0 && this.itemAnimation > 0 && this.itemTime == 0)
        {
          int Type = this.inventory[this.selectedItem].shoot;
          float num1 = this.inventory[this.selectedItem].shootSpeed;
          bool flag = false;
          int Damage = this.inventory[this.selectedItem].damage;
          float KnockBack = this.inventory[this.selectedItem].knockBack;
          if (Type == 13 || Type == 32)
          {
            this.grappling[0] = -1;
            this.grapCount = 0;
            for (int index = 0; index < 1000; ++index)
            {
              if (Main.projectile[index].active && Main.projectile[index].owner == i && Main.projectile[index].type == 13)
                Main.projectile[index].Kill();
            }
          }
          if (this.inventory[this.selectedItem].useAmmo > 0)
          {
            for (int index = 0; index < 44; ++index)
            {
              if (this.inventory[index].ammo == this.inventory[this.selectedItem].useAmmo && this.inventory[index].stack > 0)
              {
                if (this.inventory[index].shoot > 0)
                  Type = this.inventory[index].shoot;
                num1 += this.inventory[index].shootSpeed;
                Damage += this.inventory[index].damage;
                KnockBack += this.inventory[index].knockBack;
                --this.inventory[index].stack;
                if (this.inventory[index].stack <= 0)
                {
                  this.inventory[index].active = false;
                  this.inventory[index].name = "";
                  this.inventory[index].type = 0;
                }
                flag = true;
                break;
              }
            }
          }
          else
            flag = true;
          if (Type == 9 && (double) this.position.Y > Main.worldSurface * 16.0 + (double) (Main.screenHeight / 2))
            flag = false;
          if (flag)
          {
            if (this.inventory[this.selectedItem].mana > 0)
              Damage = (int) Math.Round((double) Damage * (double) this.magicBoost);
            if (Type == 1 && this.inventory[this.selectedItem].type == 120)
              Type = 2;
            this.itemTime = this.inventory[this.selectedItem].useTime;
            this.direction = (double) Main.mouseState.X + (double) Main.screenPosition.X <= (double) this.position.X + (double) this.width * 0.5 ? -1 : 1;
            Vector2 vector2 = new Vector2(this.position.X + (float) this.width * 0.5f, this.position.Y + (float) this.height * 0.5f);
            if (Type == 9)
            {
              vector2 = new Vector2(this.position.X + (float) this.width * 0.5f + (float) (Main.rand.Next(601) * -this.direction), (float) ((double) this.position.Y + (double) this.height * 0.5 - 300.0) - (float) Main.rand.Next(100));
              KnockBack = 0.0f;
            }
            float num2 = (float) Main.mouseState.X + Main.screenPosition.X - vector2.X;
            float num3 = (float) Main.mouseState.Y + Main.screenPosition.Y - vector2.Y;
            float num4 = (float) Math.Sqrt((double) num2 * (double) num2 + (double) num3 * (double) num3);
            float num5 = num1 / num4;
            float SpeedX = num2 * num5;
            float SpeedY = num3 * num5;
            if (Type == 12)
            {
              vector2.X += SpeedX * 3f;
              vector2.Y += SpeedY * 3f;
            }
            if (this.inventory[this.selectedItem].useStyle == 5)
            {
              this.itemRotation = (float) Math.Atan2((double) SpeedY * (double) this.direction, (double) SpeedX * (double) this.direction);
              NetMessage.SendData(13, -1, -1, "", this.whoAmi, 0.0f, 0.0f, 0.0f);
              NetMessage.SendData(41, -1, -1, "", this.whoAmi, 0.0f, 0.0f, 0.0f);
            }
            if (Type == 17)
            {
              vector2.X = (float) Main.mouseState.X + Main.screenPosition.X;
              vector2.Y = (float) Main.mouseState.Y + Main.screenPosition.Y;
            }
            Projectile.NewProjectile(vector2.X, vector2.Y, SpeedX, SpeedY, Type, Damage, KnockBack, i);
          }
          else if (this.inventory[this.selectedItem].useStyle == 5)
          {
            this.itemRotation = 0.0f;
            NetMessage.SendData(41, -1, -1, "", this.whoAmi, 0.0f, 0.0f, 0.0f);
          }
        }
        if (this.inventory[this.selectedItem].type >= 205 && this.inventory[this.selectedItem].type <= 207 && ((double) this.position.X / 16.0 - (double) Player.tileRangeX - (double) this.inventory[this.selectedItem].tileBoost <= (double) Player.tileTargetX && ((double) this.position.X + (double) this.width) / 16.0 + (double) Player.tileRangeX + (double) this.inventory[this.selectedItem].tileBoost - 1.0 >= (double) Player.tileTargetX && (double) this.position.Y / 16.0 - (double) Player.tileRangeY - (double) this.inventory[this.selectedItem].tileBoost <= (double) Player.tileTargetY && ((double) this.position.Y + (double) this.height) / 16.0 + (double) Player.tileRangeY + (double) this.inventory[this.selectedItem].tileBoost - 2.0 >= (double) Player.tileTargetY))
        {
          this.showItemIcon = true;
          if (this.itemTime == 0 && this.itemAnimation > 0 && this.controlUseItem)
          {
            if (this.inventory[this.selectedItem].type == 205)
            {
              bool flag1 = Main.tile[Player.tileTargetX, Player.tileTargetY].lava;
              int num1 = 0;
              for (int index1 = Player.tileTargetX - 1; index1 <= Player.tileTargetX + 1; ++index1)
              {
                for (int index2 = Player.tileTargetY - 1; index2 <= Player.tileTargetY + 1; ++index2)
                {
                  if (Main.tile[index1, index2].lava == flag1)
                    num1 += (int) Main.tile[index1, index2].liquid;
                }
              }
              if ((int) Main.tile[Player.tileTargetX, Player.tileTargetY].liquid > 0 && num1 > 100)
              {
                bool flag2 = Main.tile[Player.tileTargetX, Player.tileTargetY].lava;
                if (!Main.tile[Player.tileTargetX, Player.tileTargetY].lava)
                  this.inventory[this.selectedItem].SetDefaults(206);
                else
                  this.inventory[this.selectedItem].SetDefaults(207);
                Main.PlaySound(19, (int) this.position.X, (int) this.position.Y, 1);
                this.itemTime = this.inventory[this.selectedItem].useTime;
                int num2 = (int) Main.tile[Player.tileTargetX, Player.tileTargetY].liquid;
                Main.tile[Player.tileTargetX, Player.tileTargetY].liquid = (byte) 0;
                Main.tile[Player.tileTargetX, Player.tileTargetY].lava = false;
                WorldGen.SquareTileFrame(Player.tileTargetX, Player.tileTargetY, false);
                if (Main.netMode == 1)
                  NetMessage.sendWater(Player.tileTargetX, Player.tileTargetY);
                else
                  Liquid.AddWater(Player.tileTargetX, Player.tileTargetY);
                for (int index1 = Player.tileTargetX - 1; index1 <= Player.tileTargetX + 1; ++index1)
                {
                  for (int index2 = Player.tileTargetY - 1; index2 <= Player.tileTargetY + 1; ++index2)
                  {
                    if (num2 < 256 && Main.tile[index1, index2].lava == flag1)
                    {
                      int num3 = (int) Main.tile[index1, index2].liquid;
                      if (num3 + num2 > (int) byte.MaxValue)
                        num3 = (int) byte.MaxValue - num2;
                      num2 += num3;
                      Main.tile[index1, index2].liquid -= (byte) num3;
                      Main.tile[index1, index2].lava = flag2;
                      if ((int) Main.tile[index1, index2].liquid == 0)
                        Main.tile[index1, index2].lava = false;
                      WorldGen.SquareTileFrame(index1, index2, false);
                      if (Main.netMode == 1)
                        NetMessage.sendWater(index1, index2);
                      else
                        Liquid.AddWater(index1, index2);
                    }
                  }
                }
              }
            }
            else if ((int) Main.tile[Player.tileTargetX, Player.tileTargetY].liquid < 200 && (!Main.tile[Player.tileTargetX, Player.tileTargetY].active || !Main.tileSolid[(int) Main.tile[Player.tileTargetX, Player.tileTargetY].type] || !Main.tileSolidTop[(int) Main.tile[Player.tileTargetX, Player.tileTargetY].type]))
            {
              if (this.inventory[this.selectedItem].type == 207)
              {
                if ((int) Main.tile[Player.tileTargetX, Player.tileTargetY].liquid == 0 || Main.tile[Player.tileTargetX, Player.tileTargetY].lava)
                {
                  Main.PlaySound(19, (int) this.position.X, (int) this.position.Y, 1);
                  Main.tile[Player.tileTargetX, Player.tileTargetY].lava = true;
                  Main.tile[Player.tileTargetX, Player.tileTargetY].liquid = byte.MaxValue;
                  WorldGen.SquareTileFrame(Player.tileTargetX, Player.tileTargetY, true);
                  this.inventory[this.selectedItem].SetDefaults(205);
                  this.itemTime = this.inventory[this.selectedItem].useTime;
                  if (Main.netMode == 1)
                    NetMessage.sendWater(Player.tileTargetX, Player.tileTargetY);
                }
              }
              else if ((int) Main.tile[Player.tileTargetX, Player.tileTargetY].liquid == 0 || !Main.tile[Player.tileTargetX, Player.tileTargetY].lava)
              {
                Main.PlaySound(19, (int) this.position.X, (int) this.position.Y, 1);
                Main.tile[Player.tileTargetX, Player.tileTargetY].lava = false;
                Main.tile[Player.tileTargetX, Player.tileTargetY].liquid = byte.MaxValue;
                WorldGen.SquareTileFrame(Player.tileTargetX, Player.tileTargetY, true);
                this.inventory[this.selectedItem].SetDefaults(205);
                this.itemTime = this.inventory[this.selectedItem].useTime;
                if (Main.netMode == 1)
                  NetMessage.sendWater(Player.tileTargetX, Player.tileTargetY);
              }
            }
          }
        }
        if ((this.inventory[this.selectedItem].pick > 0 || this.inventory[this.selectedItem].axe > 0 || this.inventory[this.selectedItem].hammer > 0) && ((double) this.position.X / 16.0 - (double) Player.tileRangeX - (double) this.inventory[this.selectedItem].tileBoost <= (double) Player.tileTargetX && ((double) this.position.X + (double) this.width) / 16.0 + (double) Player.tileRangeX + (double) this.inventory[this.selectedItem].tileBoost - 1.0 >= (double) Player.tileTargetX && (double) this.position.Y / 16.0 - (double) Player.tileRangeY - (double) this.inventory[this.selectedItem].tileBoost <= (double) Player.tileTargetY && ((double) this.position.Y + (double) this.height) / 16.0 + (double) Player.tileRangeY + (double) this.inventory[this.selectedItem].tileBoost - 2.0 >= (double) Player.tileTargetY))
        {
          this.showItemIcon = true;
          if (Main.tile[Player.tileTargetX, Player.tileTargetY].active && (this.itemTime == 0 && this.itemAnimation > 0 && this.controlUseItem))
          {
            if (this.hitTileX != Player.tileTargetX || this.hitTileY != Player.tileTargetY)
            {
              this.hitTile = 0;
              this.hitTileX = Player.tileTargetX;
              this.hitTileY = Player.tileTargetY;
            }
            if (Main.tileNoFail[(int) Main.tile[Player.tileTargetX, Player.tileTargetY].type])
              this.hitTile = 100;
            if ((int) Main.tile[Player.tileTargetX, Player.tileTargetY].type != 27)
            {
              if ((int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 4 || (int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 10 || ((int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 11 || (int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 12) || ((int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 13 || (int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 14 || ((int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 15 || (int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 16)) || ((int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 17 || (int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 18 || ((int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 19 || (int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 21) || ((int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 26 || (int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 28 || ((int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 29 || (int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 31))) || ((int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 33 || (int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 34 || ((int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 35 || (int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 36) || ((int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 42 || (int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 48 || ((int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 49 || (int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 50)) || ((int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 54 || (int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 55 || ((int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 77 || (int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 78))) || (int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 79)
              {
                if ((int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 48)
                  this.hitTile += this.inventory[this.selectedItem].hammer / 3;
                else
                  this.hitTile += this.inventory[this.selectedItem].hammer;
                if ((double) Player.tileTargetY > Main.rockLayer && (int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 77 && this.inventory[this.selectedItem].hammer < 60)
                  this.hitTile = 0;
                if (this.inventory[this.selectedItem].hammer > 0)
                {
                  if ((int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 26)
                  {
                    this.Hurt(this.statLife / 2, -this.direction, false, false);
                    WorldGen.KillTile(Player.tileTargetX, Player.tileTargetY, true, false, false);
                    if (Main.netMode == 1)
                      NetMessage.SendData(17, -1, -1, "", 0, (float) Player.tileTargetX, (float) Player.tileTargetY, 1f);
                  }
                  else if (this.hitTile >= 100)
                  {
                    if (Main.netMode == 1 && (int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 21)
                    {
                      WorldGen.KillTile(Player.tileTargetX, Player.tileTargetY, true, false, false);
                      NetMessage.SendData(17, -1, -1, "", 0, (float) Player.tileTargetX, (float) Player.tileTargetY, 1f);
                      NetMessage.SendData(34, -1, -1, "", Player.tileTargetX, (float) Player.tileTargetY, 0.0f, 0.0f);
                    }
                    else
                    {
                      this.hitTile = 0;
                      WorldGen.KillTile(Player.tileTargetX, Player.tileTargetY, false, false, false);
                      if (Main.netMode == 1)
                        NetMessage.SendData(17, -1, -1, "", 0, (float) Player.tileTargetX, (float) Player.tileTargetY, 0.0f);
                    }
                  }
                  else
                  {
                    WorldGen.KillTile(Player.tileTargetX, Player.tileTargetY, true, false, false);
                    if (Main.netMode == 1)
                      NetMessage.SendData(17, -1, -1, "", 0, (float) Player.tileTargetX, (float) Player.tileTargetY, 1f);
                  }
                  this.itemTime = this.inventory[this.selectedItem].useTime;
                }
              }
              else if ((int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 5 || (int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 30 || (int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 72)
              {
                if ((int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 30)
                  this.hitTile += this.inventory[this.selectedItem].axe * 5;
                else
                  this.hitTile += this.inventory[this.selectedItem].axe;
                if (this.inventory[this.selectedItem].axe > 0)
                {
                  if (this.hitTile >= 100)
                  {
                    this.hitTile = 0;
                    WorldGen.KillTile(Player.tileTargetX, Player.tileTargetY, false, false, false);
                    if (Main.netMode == 1)
                      NetMessage.SendData(17, -1, -1, "", 0, (float) Player.tileTargetX, (float) Player.tileTargetY, 0.0f);
                  }
                  else
                  {
                    WorldGen.KillTile(Player.tileTargetX, Player.tileTargetY, true, false, false);
                    if (Main.netMode == 1)
                      NetMessage.SendData(17, -1, -1, "", 0, (float) Player.tileTargetX, (float) Player.tileTargetY, 1f);
                  }
                  this.itemTime = this.inventory[this.selectedItem].useTime;
                }
              }
              else if (this.inventory[this.selectedItem].pick > 0)
              {
                if (Main.tileDungeon[(int) Main.tile[Player.tileTargetX, Player.tileTargetY].type] || (int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 37 || (int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 25 || (int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 58)
                  this.hitTile += this.inventory[this.selectedItem].pick / 2;
                else
                  this.hitTile += this.inventory[this.selectedItem].pick;
                if ((int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 25 && this.inventory[this.selectedItem].pick < 65)
                  this.hitTile = 0;
                else if ((int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 37 && this.inventory[this.selectedItem].pick < 55)
                  this.hitTile = 0;
                else if ((int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 56 && this.inventory[this.selectedItem].pick < 65)
                  this.hitTile = 0;
                else if ((int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 58 && this.inventory[this.selectedItem].pick < 65)
                  this.hitTile = 0;
                else if (Main.tileDungeon[(int) Main.tile[Player.tileTargetX, Player.tileTargetY].type] && this.inventory[this.selectedItem].pick < 65 && ((double) Player.tileTargetX < (double) Main.maxTilesX * 0.25 || (double) Player.tileTargetX > (double) Main.maxTilesX * 0.75))
                  this.hitTile = 0;
                if ((int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 0 || (int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 40 || (int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 53 || (int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 59)
                  this.hitTile += this.inventory[this.selectedItem].pick;
                if (this.hitTile >= 100)
                {
                  this.hitTile = 0;
                  WorldGen.KillTile(Player.tileTargetX, Player.tileTargetY, false, false, false);
                  if (Main.netMode == 1)
                    NetMessage.SendData(17, -1, -1, "", 0, (float) Player.tileTargetX, (float) Player.tileTargetY, 0.0f);
                }
                else
                {
                  WorldGen.KillTile(Player.tileTargetX, Player.tileTargetY, true, false, false);
                  if (Main.netMode == 1)
                    NetMessage.SendData(17, -1, -1, "", 0, (float) Player.tileTargetX, (float) Player.tileTargetY, 1f);
                }
                this.itemTime = this.inventory[this.selectedItem].useTime;
              }
            }
          }
          if ((int) Main.tile[Player.tileTargetX, Player.tileTargetY].wall > 0 && (this.itemTime == 0 && this.itemAnimation > 0 && this.controlUseItem && this.inventory[this.selectedItem].hammer > 0))
          {
            bool flag = true;
            if (!Main.wallHouse[(int) Main.tile[Player.tileTargetX, Player.tileTargetY].wall])
            {
              flag = false;
              for (int index1 = Player.tileTargetX - 1; index1 < Player.tileTargetX + 2; ++index1)
              {
                for (int index2 = Player.tileTargetY - 1; index2 < Player.tileTargetY + 2; ++index2)
                {
                  if ((int) Main.tile[index1, index2].wall != (int) Main.tile[Player.tileTargetX, Player.tileTargetY].wall)
                  {
                    flag = true;
                    break;
                  }
                }
              }
            }
            if (flag)
            {
              if (this.hitTileX != Player.tileTargetX || this.hitTileY != Player.tileTargetY)
              {
                this.hitTile = 0;
                this.hitTileX = Player.tileTargetX;
                this.hitTileY = Player.tileTargetY;
              }
              this.hitTile += this.inventory[this.selectedItem].hammer;
              if (this.hitTile >= 100)
              {
                this.hitTile = 0;
                WorldGen.KillWall(Player.tileTargetX, Player.tileTargetY, false);
                if (Main.netMode == 1)
                  NetMessage.SendData(17, -1, -1, "", 2, (float) Player.tileTargetX, (float) Player.tileTargetY, 0.0f);
              }
              else
              {
                WorldGen.KillWall(Player.tileTargetX, Player.tileTargetY, true);
                if (Main.netMode == 1)
                  NetMessage.SendData(17, -1, -1, "", 2, (float) Player.tileTargetX, (float) Player.tileTargetY, 1f);
              }
              this.itemTime = this.inventory[this.selectedItem].useTime;
            }
          }
        }
        if (this.inventory[this.selectedItem].type == 29 && this.itemAnimation > 0 && this.statLifeMax < 400 && this.itemTime == 0)
        {
          this.itemTime = this.inventory[this.selectedItem].useTime;
          this.statLifeMax += 20;
          this.statLife += 20;
          if (Main.myPlayer == this.whoAmi)
            this.HealEffect(20);
        }
        if (this.inventory[this.selectedItem].type == 109 && this.itemAnimation > 0 && this.statManaMax < 200 && this.itemTime == 0)
        {
          this.itemTime = this.inventory[this.selectedItem].useTime;
          this.statManaMax += 20;
          this.statMana += 20;
          if (Main.myPlayer == this.whoAmi)
            this.ManaEffect(20);
        }
        if (this.inventory[this.selectedItem].createTile >= 0 && ((double) this.position.X / 16.0 - (double) Player.tileRangeX - (double) this.inventory[this.selectedItem].tileBoost <= (double) Player.tileTargetX && ((double) this.position.X + (double) this.width) / 16.0 + (double) Player.tileRangeX + (double) this.inventory[this.selectedItem].tileBoost - 1.0 >= (double) Player.tileTargetX && (double) this.position.Y / 16.0 - (double) Player.tileRangeY - (double) this.inventory[this.selectedItem].tileBoost <= (double) Player.tileTargetY && ((double) this.position.Y + (double) this.height) / 16.0 + (double) Player.tileRangeY + (double) this.inventory[this.selectedItem].tileBoost - 2.0 >= (double) Player.tileTargetY))
        {
          this.showItemIcon = true;
          if ((!Main.tile[Player.tileTargetX, Player.tileTargetY].active || this.inventory[this.selectedItem].createTile == 23 || (this.inventory[this.selectedItem].createTile == 2 || this.inventory[this.selectedItem].createTile == 60) || this.inventory[this.selectedItem].createTile == 70) && (this.itemTime == 0 && this.itemAnimation > 0 && this.controlUseItem))
          {
            bool flag = false;
            if (this.inventory[this.selectedItem].createTile == 23 || this.inventory[this.selectedItem].createTile == 2)
            {
              if (Main.tile[Player.tileTargetX, Player.tileTargetY].active && (int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 0)
                flag = true;
            }
            else if (this.inventory[this.selectedItem].createTile == 60 || this.inventory[this.selectedItem].createTile == 70)
            {
              if (Main.tile[Player.tileTargetX, Player.tileTargetY].active && (int) Main.tile[Player.tileTargetX, Player.tileTargetY].type == 59)
                flag = true;
            }
            else if (this.inventory[this.selectedItem].createTile == 4)
            {
              int index1 = (int) Main.tile[Player.tileTargetX, Player.tileTargetY + 1].type;
              int index2 = (int) Main.tile[Player.tileTargetX - 1, Player.tileTargetY].type;
              int index3 = (int) Main.tile[Player.tileTargetX + 1, Player.tileTargetY].type;
              int num1 = (int) Main.tile[Player.tileTargetX - 1, Player.tileTargetY - 1].type;
              int num2 = (int) Main.tile[Player.tileTargetX + 1, Player.tileTargetY - 1].type;
              int num3 = (int) Main.tile[Player.tileTargetX - 1, Player.tileTargetY - 1].type;
              int num4 = (int) Main.tile[Player.tileTargetX + 1, Player.tileTargetY + 1].type;
              if (!Main.tile[Player.tileTargetX, Player.tileTargetY + 1].active)
                index1 = -1;
              if (!Main.tile[Player.tileTargetX - 1, Player.tileTargetY].active)
                index2 = -1;
              if (!Main.tile[Player.tileTargetX + 1, Player.tileTargetY].active)
                index3 = -1;
              if (!Main.tile[Player.tileTargetX - 1, Player.tileTargetY - 1].active)
                num1 = -1;
              if (!Main.tile[Player.tileTargetX + 1, Player.tileTargetY - 1].active)
                num2 = -1;
              if (!Main.tile[Player.tileTargetX - 1, Player.tileTargetY + 1].active)
                num3 = -1;
              if (!Main.tile[Player.tileTargetX + 1, Player.tileTargetY + 1].active)
                num4 = -1;
              if (index1 >= 0 && Main.tileSolid[index1] && !Main.tileNoAttach[index1])
                flag = true;
              else if (index2 >= 0 && Main.tileSolid[index2] && !Main.tileNoAttach[index2] || index2 == 5 && num1 == 5 && num3 == 5)
                flag = true;
              else if (index3 >= 0 && Main.tileSolid[index3] && !Main.tileNoAttach[index3] || index3 == 5 && num2 == 5 && num4 == 5)
                flag = true;
            }
            else if (this.inventory[this.selectedItem].createTile == 78)
            {
              if (Main.tile[Player.tileTargetX, Player.tileTargetY + 1].active && (Main.tileSolid[(int) Main.tile[Player.tileTargetX, Player.tileTargetY + 1].type] || Main.tileTable[(int) Main.tile[Player.tileTargetX, Player.tileTargetY + 1].type]))
                flag = true;
            }
            else if (this.inventory[this.selectedItem].createTile == 13 || this.inventory[this.selectedItem].createTile == 29 || this.inventory[this.selectedItem].createTile == 33 || this.inventory[this.selectedItem].createTile == 49)
            {
              if (Main.tile[Player.tileTargetX, Player.tileTargetY + 1].active && Main.tileTable[(int) Main.tile[Player.tileTargetX, Player.tileTargetY + 1].type])
                flag = true;
            }
            else if (this.inventory[this.selectedItem].createTile == 51)
            {
              if (Main.tile[Player.tileTargetX + 1, Player.tileTargetY].active || (int) Main.tile[Player.tileTargetX + 1, Player.tileTargetY].wall > 0 || (Main.tile[Player.tileTargetX - 1, Player.tileTargetY].active || (int) Main.tile[Player.tileTargetX - 1, Player.tileTargetY].wall > 0) || (Main.tile[Player.tileTargetX, Player.tileTargetY + 1].active || (int) Main.tile[Player.tileTargetX, Player.tileTargetY + 1].wall > 0 || Main.tile[Player.tileTargetX, Player.tileTargetY - 1].active) || (int) Main.tile[Player.tileTargetX, Player.tileTargetY - 1].wall > 0)
                flag = true;
            }
            else if (Main.tile[Player.tileTargetX + 1, Player.tileTargetY].active && Main.tileSolid[(int) Main.tile[Player.tileTargetX + 1, Player.tileTargetY].type] || (int) Main.tile[Player.tileTargetX + 1, Player.tileTargetY].wall > 0 || (Main.tile[Player.tileTargetX - 1, Player.tileTargetY].active && Main.tileSolid[(int) Main.tile[Player.tileTargetX - 1, Player.tileTargetY].type] || (int) Main.tile[Player.tileTargetX - 1, Player.tileTargetY].wall > 0) || (Main.tile[Player.tileTargetX, Player.tileTargetY + 1].active && Main.tileSolid[(int) Main.tile[Player.tileTargetX, Player.tileTargetY + 1].type] || (int) Main.tile[Player.tileTargetX, Player.tileTargetY + 1].wall > 0 || Main.tile[Player.tileTargetX, Player.tileTargetY - 1].active && Main.tileSolid[(int) Main.tile[Player.tileTargetX, Player.tileTargetY - 1].type]) || (int) Main.tile[Player.tileTargetX, Player.tileTargetY - 1].wall > 0)
              flag = true;
            if (flag && WorldGen.PlaceTile(Player.tileTargetX, Player.tileTargetY, this.inventory[this.selectedItem].createTile, false, false, this.whoAmi))
            {
              this.itemTime = this.inventory[this.selectedItem].useTime;
              if (Main.netMode == 1)
                NetMessage.SendData(17, -1, -1, "", 1, (float) Player.tileTargetX, (float) Player.tileTargetY, (float) this.inventory[this.selectedItem].createTile);
              if (this.inventory[this.selectedItem].createTile == 15)
              {
                if (this.direction == 1)
                {
                  Main.tile[Player.tileTargetX, Player.tileTargetY].frameX += (short) 18;
                  Main.tile[Player.tileTargetX, Player.tileTargetY - 1].frameX += (short) 18;
                }
                if (Main.netMode == 1)
                  NetMessage.SendTileSquare(-1, Player.tileTargetX - 1, Player.tileTargetY - 1, 3);
              }
              else if (this.inventory[this.selectedItem].createTile == 79 && Main.netMode == 1)
                NetMessage.SendTileSquare(-1, Player.tileTargetX, Player.tileTargetY, 5);
            }
          }
        }
        if (this.inventory[this.selectedItem].createWall >= 0)
        {
          Player.tileTargetX = (int) (((double) Main.mouseState.X + (double) Main.screenPosition.X) / 16.0);
          Player.tileTargetY = (int) (((double) Main.mouseState.Y + (double) Main.screenPosition.Y) / 16.0);
          if ((double) this.position.X / 16.0 - (double) Player.tileRangeX - (double) this.inventory[this.selectedItem].tileBoost <= (double) Player.tileTargetX && ((double) this.position.X + (double) this.width) / 16.0 + (double) Player.tileRangeX + (double) this.inventory[this.selectedItem].tileBoost - 1.0 >= (double) Player.tileTargetX && (double) this.position.Y / 16.0 - (double) Player.tileRangeY - (double) this.inventory[this.selectedItem].tileBoost <= (double) Player.tileTargetY && ((double) this.position.Y + (double) this.height) / 16.0 + (double) Player.tileRangeY + (double) this.inventory[this.selectedItem].tileBoost - 2.0 >= (double) Player.tileTargetY)
          {
            this.showItemIcon = true;
            if (this.itemTime == 0 && this.itemAnimation > 0 && this.controlUseItem && ((Main.tile[Player.tileTargetX + 1, Player.tileTargetY].active || (int) Main.tile[Player.tileTargetX + 1, Player.tileTargetY].wall > 0 || (Main.tile[Player.tileTargetX - 1, Player.tileTargetY].active || (int) Main.tile[Player.tileTargetX - 1, Player.tileTargetY].wall > 0) || (Main.tile[Player.tileTargetX, Player.tileTargetY + 1].active || (int) Main.tile[Player.tileTargetX, Player.tileTargetY + 1].wall > 0 || Main.tile[Player.tileTargetX, Player.tileTargetY - 1].active) || (int) Main.tile[Player.tileTargetX, Player.tileTargetY - 1].wall > 0) && (int) Main.tile[Player.tileTargetX, Player.tileTargetY].wall != this.inventory[this.selectedItem].createWall))
            {
              WorldGen.PlaceWall(Player.tileTargetX, Player.tileTargetY, this.inventory[this.selectedItem].createWall, false);
              if ((int) Main.tile[Player.tileTargetX, Player.tileTargetY].wall == this.inventory[this.selectedItem].createWall)
              {
                this.itemTime = this.inventory[this.selectedItem].useTime;
                if (Main.netMode == 1)
                  NetMessage.SendData(17, -1, -1, "", 3, (float) Player.tileTargetX, (float) Player.tileTargetY, (float) this.inventory[this.selectedItem].createWall);
              }
            }
          }
        }
      }
      if (this.inventory[this.selectedItem].damage >= 0 && this.inventory[this.selectedItem].type > 0 && !this.inventory[this.selectedItem].noMelee && this.itemAnimation > 0)
      {
        bool flag = false;
        Rectangle rectangle1 = new Rectangle((int) this.itemLocation.X, (int) this.itemLocation.Y, 32, 32);
        if (!Main.dedServ)
          rectangle1 = new Rectangle((int) this.itemLocation.X, (int) this.itemLocation.Y, Main.itemTexture[this.inventory[this.selectedItem].type].Width, Main.itemTexture[this.inventory[this.selectedItem].type].Height);
        rectangle1.Width = (int) ((double) rectangle1.Width * (double) this.inventory[this.selectedItem].scale);
        rectangle1.Height = (int) ((double) rectangle1.Height * (double) this.inventory[this.selectedItem].scale);
        if (this.direction == -1)
          rectangle1.X -= rectangle1.Width;
        rectangle1.Y -= rectangle1.Height;
        if (this.inventory[this.selectedItem].useStyle == 1)
        {
          if ((double) this.itemAnimation < (double) this.itemAnimationMax * 0.333)
          {
            if (this.direction == -1)
              rectangle1.X -= (int) ((double) rectangle1.Width * 1.4 - (double) rectangle1.Width);
            rectangle1.Width = (int) ((double) rectangle1.Width * 1.4);
            rectangle1.Y += (int) ((double) rectangle1.Height * 0.5);
            rectangle1.Height = (int) ((double) rectangle1.Height * 1.1);
          }
          else if ((double) this.itemAnimation >= (double) this.itemAnimationMax * 0.666)
          {
            if (this.direction == 1)
              rectangle1.X -= (int) ((double) rectangle1.Width * 1.2);
            rectangle1.Width = rectangle1.Width * 2;
            rectangle1.Y -= (int) ((double) rectangle1.Height * 1.4 - (double) rectangle1.Height);
            rectangle1.Height = (int) ((double) rectangle1.Height * 1.4);
          }
        }
        else if (this.inventory[this.selectedItem].useStyle == 3)
        {
          if ((double) this.itemAnimation > (double) this.itemAnimationMax * 0.666)
          {
            flag = true;
          }
          else
          {
            if (this.direction == -1)
              rectangle1.X -= (int) ((double) rectangle1.Width * 1.4 - (double) rectangle1.Width);
            rectangle1.Width = (int) ((double) rectangle1.Width * 1.4);
            rectangle1.Y += (int) ((double) rectangle1.Height * 0.6);
            rectangle1.Height = (int) ((double) rectangle1.Height * 0.6);
          }
        }
        if (!flag)
        {
          if ((this.inventory[this.selectedItem].type == 44 || this.inventory[this.selectedItem].type == 45 || (this.inventory[this.selectedItem].type == 46 || this.inventory[this.selectedItem].type == 103) || this.inventory[this.selectedItem].type == 104) && Main.rand.Next(15) == 0)
            Dust.NewDust(new Vector2((float) rectangle1.X, (float) rectangle1.Y), rectangle1.Width, rectangle1.Height, 14, (float) (this.direction * 2), 0.0f, 150, new Color(), 1.3f);
          if (this.inventory[this.selectedItem].type == 65)
          {
            if (Main.rand.Next(5) == 0)
              Dust.NewDust(new Vector2((float) rectangle1.X, (float) rectangle1.Y), rectangle1.Width, rectangle1.Height, 15, 0.0f, 0.0f, 150, new Color(), 1.2f);
            if (Main.rand.Next(10) == 0)
              Gore.NewGore(new Vector2((float) rectangle1.X, (float) rectangle1.Y), new Vector2(), Main.rand.Next(16, 18));
          }
          if (this.inventory[this.selectedItem].type == 190 || this.inventory[this.selectedItem].type == 213)
          {
            int index = Dust.NewDust(new Vector2((float) rectangle1.X, (float) rectangle1.Y), rectangle1.Width, rectangle1.Height, 40, this.velocity.X * 0.2f + (float) (this.direction * 3), this.velocity.Y * 0.2f, 0, new Color(), 1.2f);
            Main.dust[index].noGravity = true;
          }
          if (this.inventory[this.selectedItem].type == 121)
          {
            for (int index1 = 0; index1 < 2; ++index1)
            {
              int index2 = Dust.NewDust(new Vector2((float) rectangle1.X, (float) rectangle1.Y), rectangle1.Width, rectangle1.Height, 6, this.velocity.X * 0.2f + (float) (this.direction * 3), this.velocity.Y * 0.2f, 100, new Color(), 2.5f);
              Main.dust[index2].noGravity = true;
              Main.dust[index2].velocity.X *= 2f;
              Main.dust[index2].velocity.Y *= 2f;
            }
          }
          if (this.inventory[this.selectedItem].type == 122 || this.inventory[this.selectedItem].type == 217)
          {
            int index = Dust.NewDust(new Vector2((float) rectangle1.X, (float) rectangle1.Y), rectangle1.Width, rectangle1.Height, 6, this.velocity.X * 0.2f + (float) (this.direction * 3), this.velocity.Y * 0.2f, 100, new Color(), 1.9f);
            Main.dust[index].noGravity = true;
          }
          if (this.inventory[this.selectedItem].type == 155)
          {
            int index = Dust.NewDust(new Vector2((float) rectangle1.X, (float) rectangle1.Y), rectangle1.Width, rectangle1.Height, 29, this.velocity.X * 0.2f + (float) (this.direction * 3), this.velocity.Y * 0.2f, 100, new Color(), 2f);
            Main.dust[index].noGravity = true;
            Main.dust[index].velocity.X /= 2f;
            Main.dust[index].velocity.Y /= 2f;
          }
          if (this.inventory[this.selectedItem].type >= 198 && this.inventory[this.selectedItem].type <= 203)
            Lighting.addLight((int) (((double) this.itemLocation.X + 6.0 + (double) this.velocity.X) / 16.0), (int) (((double) this.itemLocation.Y - 14.0) / 16.0), 0.5f);
          if (Main.myPlayer == i)
          {
            int num1 = rectangle1.X / 16;
            int num2 = (rectangle1.X + rectangle1.Width) / 16 + 1;
            int num3 = rectangle1.Y / 16;
            int num4 = (rectangle1.Y + rectangle1.Height) / 16 + 1;
            for (int i1 = num1; i1 < num2; ++i1)
            {
              for (int j = num3; j < num4; ++j)
              {
                if ((int) Main.tile[i1, j].type == 3 || (int) Main.tile[i1, j].type == 24 || ((int) Main.tile[i1, j].type == 28 || (int) Main.tile[i1, j].type == 32) || ((int) Main.tile[i1, j].type == 51 || (int) Main.tile[i1, j].type == 52 || ((int) Main.tile[i1, j].type == 61 || (int) Main.tile[i1, j].type == 62)) || ((int) Main.tile[i1, j].type == 69 || (int) Main.tile[i1, j].type == 71 || (int) Main.tile[i1, j].type == 73) || (int) Main.tile[i1, j].type == 74)
                {
                  WorldGen.KillTile(i1, j, false, false, false);
                  if (Main.netMode == 1)
                    NetMessage.SendData(17, -1, -1, "", 0, (float) i1, (float) j, 0.0f);
                }
              }
            }
            for (int number = 0; number < 1000; ++number)
            {
              if (Main.npc[number].active && Main.npc[number].immune[i] == 0 && this.attackCD == 0 && !Main.npc[number].friendly)
              {
                Rectangle rectangle2 = new Rectangle((int) Main.npc[number].position.X, (int) Main.npc[number].position.Y, Main.npc[number].width, Main.npc[number].height);
                if (rectangle1.Intersects(rectangle2) && (Main.npc[number].noTileCollide || Collision.CanHit(this.position, this.width, this.height, Main.npc[number].position, Main.npc[number].width, Main.npc[number].height)))
                {
                  Main.npc[number].StrikeNPC(this.inventory[this.selectedItem].damage, this.inventory[this.selectedItem].knockBack, this.direction);
                  if (Main.netMode == 1)
                    NetMessage.SendData(24, -1, -1, "", number, (float) i, 0.0f, 0.0f);
                  Main.npc[number].immune[i] = this.itemAnimation;
                  this.attackCD = (int) ((double) this.itemAnimationMax * 0.33);
                }
              }
            }
            if (this.hostile)
            {
              for (int number = 0; number < (int) byte.MaxValue; ++number)
              {
                if (number != i && Main.player[number].active && (Main.player[number].hostile && !Main.player[number].immune) && !Main.player[number].dead && (Main.player[i].team == 0 || Main.player[i].team != Main.player[number].team))
                {
                  Rectangle rectangle2 = new Rectangle((int) Main.player[number].position.X, (int) Main.player[number].position.Y, Main.player[number].width, Main.player[number].height);
                  if (rectangle1.Intersects(rectangle2) && Collision.CanHit(this.position, this.width, this.height, Main.player[number].position, Main.player[number].width, Main.player[number].height))
                  {
                    Main.player[number].Hurt(this.inventory[this.selectedItem].damage, this.direction, true, false);
                    if (Main.netMode != 0)
                      NetMessage.SendData(26, -1, -1, "", number, (float) this.direction, (float) this.inventory[this.selectedItem].damage, 1f);
                    this.attackCD = (int) ((double) this.itemAnimationMax * 0.33);
                  }
                }
              }
            }
          }
        }
      }
      if (this.itemTime == 0 && this.itemAnimation > 0)
      {
        if (this.inventory[this.selectedItem].healLife > 0)
        {
          this.statLife += this.inventory[this.selectedItem].healLife;
          this.itemTime = this.inventory[this.selectedItem].useTime;
          if (Main.myPlayer == this.whoAmi)
            this.HealEffect(this.inventory[this.selectedItem].healLife);
        }
        if (this.inventory[this.selectedItem].healMana > 0)
        {
          this.statMana += this.inventory[this.selectedItem].healMana;
          this.itemTime = this.inventory[this.selectedItem].useTime;
          if (Main.myPlayer == this.whoAmi)
            this.ManaEffect(this.inventory[this.selectedItem].healMana);
        }
      }
      if (this.itemTime == 0 && this.itemAnimation > 0 && (this.inventory[this.selectedItem].type == 43 || this.inventory[this.selectedItem].type == 70))
      {
        this.itemTime = this.inventory[this.selectedItem].useTime;
        bool flag = false;
        int num = 4;
        if (this.inventory[this.selectedItem].type == 43)
          num = 4;
        else if (this.inventory[this.selectedItem].type == 70)
          num = 13;
        for (int index = 0; index < 1000; ++index)
        {
          if (Main.npc[index].active && Main.npc[index].type == num)
          {
            flag = true;
            break;
          }
        }
        if (flag)
        {
          if (Main.myPlayer == this.whoAmi)
            this.Hurt(this.statLife * (this.statDefense + 1), -this.direction, false, false);
        }
        else if (this.inventory[this.selectedItem].type == 43)
        {
          if (!Main.dayTime)
          {
            Main.PlaySound(15, (int) this.position.X, (int) this.position.Y, 0);
            if (Main.netMode != 1)
              NPC.SpawnOnPlayer(i, 4);
          }
        }
        else if (this.inventory[this.selectedItem].type == 70 && this.zoneEvil)
        {
          Main.PlaySound(15, (int) this.position.X, (int) this.position.Y, 0);
          if (Main.netMode != 1)
            NPC.SpawnOnPlayer(i, 13);
        }
      }
      if (this.inventory[this.selectedItem].type == 50 && this.itemAnimation > 0)
      {
        if (Main.rand.Next(2) == 0)
          Dust.NewDust(this.position, this.width, this.height, 15, 0.0f, 0.0f, 150, new Color(), 1.1f);
        if (this.itemTime == 0)
          this.itemTime = this.inventory[this.selectedItem].useTime;
        else if (this.itemTime == this.inventory[this.selectedItem].useTime / 2)
        {
          for (int index = 0; index < 70; ++index)
            Dust.NewDust(this.position, this.width, this.height, 15, this.velocity.X * 0.5f, this.velocity.Y * 0.5f, 150, new Color(), 1.5f);
          this.grappling[0] = -1;
          this.grapCount = 0;
          for (int index = 0; index < 1000; ++index)
          {
            if (Main.projectile[index].active && Main.projectile[index].owner == i && Main.projectile[index].aiStyle == 7)
              Main.projectile[index].Kill();
          }
          this.Spawn();
          for (int index = 0; index < 70; ++index)
            Dust.NewDust(this.position, this.width, this.height, 15, 0.0f, 0.0f, 150, new Color(), 1.5f);
        }
      }
      if (i == Main.myPlayer)
      {
        if (this.itemTime == this.inventory[this.selectedItem].useTime && this.inventory[this.selectedItem].consumable)
        {
          --this.inventory[this.selectedItem].stack;
          if (this.inventory[this.selectedItem].stack <= 0)
            this.itemTime = this.itemAnimation;
        }
        if (this.inventory[this.selectedItem].stack <= 0 && this.itemAnimation == 0)
          this.inventory[this.selectedItem] = new Item();
      }
    }

    public Color GetImmuneAlpha(Color newColor)
    {
      float num = (float) ((int) byte.MaxValue - this.immuneAlpha) / (float) byte.MaxValue;
      if ((double) this.shadow > 0.0)
        num *= 1f - this.shadow;
      int r = (int) ((double) newColor.R * (double) num);
      int g = (int) ((double) newColor.G * (double) num);
      int b = (int) ((double) newColor.B * (double) num);
      int a = (int) ((double) newColor.A * (double) num);
      if (a < 0)
        a = 0;
      if (a > (int) byte.MaxValue)
        a = (int) byte.MaxValue;
      return new Color(r, g, b, a);
    }

    public Color GetDeathAlpha(Color newColor)
    {
      int r = (int) newColor.R + (int) ((double) this.immuneAlpha * 0.9);
      int g = (int) newColor.G + (int) ((double) this.immuneAlpha * 0.5);
      int b = (int) newColor.B + (int) ((double) this.immuneAlpha * 0.5);
      int a = (int) newColor.A + (int) ((double) this.immuneAlpha * 0.4);
      if (a < 0)
        a = 0;
      if (a > (int) byte.MaxValue)
        a = (int) byte.MaxValue;
      return new Color(r, g, b, a);
    }

    public void DropItems()
    {
      for (int index = 0; index < 44; ++index)
      {
        if (this.inventory[index].type >= 71 && this.inventory[index].type <= 74)
        {
          int number = Item.NewItem((int) this.position.X, (int) this.position.Y, this.width, this.height, this.inventory[index].type, 1, false);
          int num1 = this.inventory[index].stack / 2;
          int num2 = this.inventory[index].stack - num1;
          this.inventory[index].stack -= num2;
          if (this.inventory[index].stack <= 0)
            this.inventory[index] = new Item();
          Main.item[number].stack = num2;
          Main.item[number].velocity.Y = (float) Main.rand.Next(-20, 1) * 0.2f;
          Main.item[number].velocity.X = (float) Main.rand.Next(-20, 21) * 0.2f;
          Main.item[number].noGrabDelay = 100;
          if (Main.netMode == 1)
            NetMessage.SendData(21, -1, -1, "", number, 0.0f, 0.0f, 0.0f);
        }
      }
    }

    public object Clone()
    {
      return this.MemberwiseClone();
    }

    public object clientClone()
    {
      Player player = new Player();
      player.zoneEvil = this.zoneEvil;
      player.zoneMeteor = this.zoneMeteor;
      player.zoneDungeon = this.zoneDungeon;
      player.zoneJungle = this.zoneJungle;
      player.direction = this.direction;
      player.selectedItem = this.selectedItem;
      player.controlUp = this.controlUp;
      player.controlDown = this.controlDown;
      player.controlLeft = this.controlLeft;
      player.controlRight = this.controlRight;
      player.controlJump = this.controlJump;
      player.controlUseItem = this.controlUseItem;
      player.statLife = this.statLife;
      player.statLifeMax = this.statLifeMax;
      player.statMana = this.statMana;
      player.statManaMax = this.statManaMax;
      player.position.X = this.position.X;
      player.chest = this.chest;
      player.talkNPC = this.talkNPC;
      for (int index = 0; index < 44; ++index)
      {
        player.inventory[index] = (Item) this.inventory[index].Clone();
        if (index < 11)
          player.armor[index] = (Item) this.armor[index].Clone();
      }
      return (object) player;
    }

    private static void EncryptFile(string inputFile, string outputFile)
    {
      byte[] bytes = new UnicodeEncoding().GetBytes("h3y_gUyZ");
      FileStream fileStream1 = new FileStream(outputFile, FileMode.Create);
      RijndaelManaged rijndaelManaged = new RijndaelManaged();
      CryptoStream cryptoStream = new CryptoStream((Stream) fileStream1, rijndaelManaged.CreateEncryptor(bytes, bytes), CryptoStreamMode.Write);
      FileStream fileStream2 = new FileStream(inputFile, FileMode.Open);
      int num;
      while ((num = fileStream2.ReadByte()) != -1)
        cryptoStream.WriteByte((byte) num);
      fileStream2.Close();
      cryptoStream.Close();
      fileStream1.Close();
    }

    private static bool DecryptFile(string inputFile, string outputFile)
    {
      byte[] bytes = new UnicodeEncoding().GetBytes("h3y_gUyZ");
      FileStream fileStream1 = new FileStream(inputFile, FileMode.Open);
      RijndaelManaged rijndaelManaged = new RijndaelManaged();
      CryptoStream cryptoStream = new CryptoStream((Stream) fileStream1, rijndaelManaged.CreateDecryptor(bytes, bytes), CryptoStreamMode.Read);
      FileStream fileStream2 = new FileStream(outputFile, FileMode.Create);
      try
      {
        int num;
        while ((num = cryptoStream.ReadByte()) != -1)
          fileStream2.WriteByte((byte) num);
        fileStream2.Close();
        cryptoStream.Close();
        fileStream1.Close();
      }
      catch
      {
        fileStream2.Close();
        fileStream1.Close();
        File.Delete(outputFile);
        return true;
      }
      return false;
    }

    public static bool CheckSpawn(int x, int y)
    {
      if (x < 10 || x > Main.maxTilesX - 10 || y < 10 || y > Main.maxTilesX - 10 || Main.tile[x, y - 1] == null || (!Main.tile[x, y - 1].active || (int) Main.tile[x, y - 1].type != 79))
      {
        return false;
      }
      else
      {
        for (int index1 = x - 1; index1 <= x + 1; ++index1)
        {
          for (int index2 = y - 3; index2 < y; ++index2)
          {
            if (Main.tile[index1, index2] == null || Main.tile[index1, index2].active && Main.tileSolid[(int) Main.tile[index1, index2].type] && !Main.tileSolidTop[(int) Main.tile[index1, index2].type])
              return false;
          }
        }
        if (!WorldGen.StartRoomCheck(x, y - 1))
          return false;
        else
          return true;
      }
    }

    public void FindSpawn()
    {
      for (int index = 0; index < 200; ++index)
      {
        if (this.spN[index] == null)
        {
          this.SpawnX = -1;
          this.SpawnY = -1;
          break;
        }
        else if (this.spN[index] == Main.worldName && this.spI[index] == Main.worldID)
        {
          this.SpawnX = this.spX[index];
          this.SpawnY = this.spY[index];
          break;
        }
      }
    }

    public void ChangeSpawn(int x, int y)
    {
      for (int index1 = 0; index1 < 200 && this.spN[index1] != null; ++index1)
      {
        if (this.spN[index1] == Main.worldName && this.spI[index1] == Main.worldID)
        {
          for (int index2 = index1; index2 > 0; --index2)
          {
            this.spN[index2] = this.spN[index2 - 1];
            this.spI[index2] = this.spI[index2 - 1];
            this.spX[index2] = this.spX[index2 - 1];
            this.spY[index2] = this.spY[index2 - 1];
          }
          this.spN[0] = Main.worldName;
          this.spI[0] = Main.worldID;
          this.spX[0] = x;
          this.spY[0] = y;
          return;
        }
      }
      for (int index = 199; index > 0; --index)
      {
        if (this.spN[index - 1] != null)
        {
          this.spN[index] = this.spN[index - 1];
          this.spI[index] = this.spI[index - 1];
          this.spX[index] = this.spX[index - 1];
          this.spY[index] = this.spY[index - 1];
        }
      }
      this.spN[0] = Main.worldName;
      this.spI[0] = Main.worldID;
      this.spX[0] = x;
      this.spY[0] = y;
    }

    public static void SavePlayer(Player newPlayer, string playerPath)
    {
      try
      {
        Directory.CreateDirectory(Main.PlayerPath);
      }
      catch
      {
      }
      if (playerPath != null)
      {
        string destFileName = playerPath + ".bak";
        if (File.Exists(playerPath))
          File.Copy(playerPath, destFileName, true);
        string str = playerPath + ".dat";
        using (FileStream fileStream = new FileStream(str, FileMode.Create))
        {
          using (BinaryWriter binaryWriter = new BinaryWriter((Stream) fileStream))
          {
            binaryWriter.Write(Main.curRelease);
            binaryWriter.Write(newPlayer.name);
            binaryWriter.Write(newPlayer.hair);
            binaryWriter.Write(newPlayer.statLife);
            binaryWriter.Write(newPlayer.statLifeMax);
            binaryWriter.Write(newPlayer.statMana);
            binaryWriter.Write(newPlayer.statManaMax);
            binaryWriter.Write(newPlayer.hairColor.R);
            binaryWriter.Write(newPlayer.hairColor.G);
            binaryWriter.Write(newPlayer.hairColor.B);
            binaryWriter.Write(newPlayer.skinColor.R);
            binaryWriter.Write(newPlayer.skinColor.G);
            binaryWriter.Write(newPlayer.skinColor.B);
            binaryWriter.Write(newPlayer.eyeColor.R);
            binaryWriter.Write(newPlayer.eyeColor.G);
            binaryWriter.Write(newPlayer.eyeColor.B);
            binaryWriter.Write(newPlayer.shirtColor.R);
            binaryWriter.Write(newPlayer.shirtColor.G);
            binaryWriter.Write(newPlayer.shirtColor.B);
            binaryWriter.Write(newPlayer.underShirtColor.R);
            binaryWriter.Write(newPlayer.underShirtColor.G);
            binaryWriter.Write(newPlayer.underShirtColor.B);
            binaryWriter.Write(newPlayer.pantsColor.R);
            binaryWriter.Write(newPlayer.pantsColor.G);
            binaryWriter.Write(newPlayer.pantsColor.B);
            binaryWriter.Write(newPlayer.shoeColor.R);
            binaryWriter.Write(newPlayer.shoeColor.G);
            binaryWriter.Write(newPlayer.shoeColor.B);
            for (int index = 0; index < 11; ++index)
            {
              if (newPlayer.armor[index].name == null)
                newPlayer.armor[index].name = "";
              binaryWriter.Write(newPlayer.armor[index].name);
            }
            for (int index = 0; index < 44; ++index)
            {
              if (newPlayer.inventory[index].name == null)
                newPlayer.inventory[index].name = "";
              binaryWriter.Write(newPlayer.inventory[index].name);
              binaryWriter.Write(newPlayer.inventory[index].stack);
            }
            for (int index = 0; index < Chest.maxItems; ++index)
            {
              if (newPlayer.bank[index].name == null)
                newPlayer.bank[index].name = "";
              binaryWriter.Write(newPlayer.bank[index].name);
              binaryWriter.Write(newPlayer.bank[index].stack);
            }
            for (int index = 0; index < 200; ++index)
            {
              if (newPlayer.spN[index] == null)
              {
                binaryWriter.Write(-1);
                break;
              }
              else
              {
                binaryWriter.Write(newPlayer.spX[index]);
                binaryWriter.Write(newPlayer.spY[index]);
                binaryWriter.Write(newPlayer.spI[index]);
                binaryWriter.Write(newPlayer.spN[index]);
              }
            }
            binaryWriter.Close();
          }
        }
        Player.EncryptFile(str, playerPath);
        File.Delete(str);
      }
    }

    public static Player LoadPlayer(string playerPath)
    {
      if (Main.rand == null)
        Main.rand = new Random((int) DateTime.Now.Ticks);
      Player player = new Player();
      bool flag;
      try
      {
        string str = playerPath + ".dat";
        flag = Player.DecryptFile(playerPath, str);
        if (!flag)
        {
          using (FileStream fileStream = new FileStream(str, FileMode.Open))
          {
            using (BinaryReader binaryReader = new BinaryReader((Stream) fileStream))
            {
              int release = binaryReader.ReadInt32();
              player.name = binaryReader.ReadString();
              player.hair = binaryReader.ReadInt32();
              player.statLife = binaryReader.ReadInt32();
              player.statLifeMax = binaryReader.ReadInt32();
              if (player.statLife > player.statLifeMax)
                player.statLife = player.statLifeMax;
              player.statMana = binaryReader.ReadInt32();
              player.statManaMax = binaryReader.ReadInt32();
              if (player.statMana > player.statManaMax)
                player.statMana = player.statManaMax;
              player.hairColor.R = binaryReader.ReadByte();
              player.hairColor.G = binaryReader.ReadByte();
              player.hairColor.B = binaryReader.ReadByte();
              player.skinColor.R = binaryReader.ReadByte();
              player.skinColor.G = binaryReader.ReadByte();
              player.skinColor.B = binaryReader.ReadByte();
              player.eyeColor.R = binaryReader.ReadByte();
              player.eyeColor.G = binaryReader.ReadByte();
              player.eyeColor.B = binaryReader.ReadByte();
              player.shirtColor.R = binaryReader.ReadByte();
              player.shirtColor.G = binaryReader.ReadByte();
              player.shirtColor.B = binaryReader.ReadByte();
              player.underShirtColor.R = binaryReader.ReadByte();
              player.underShirtColor.G = binaryReader.ReadByte();
              player.underShirtColor.B = binaryReader.ReadByte();
              player.pantsColor.R = binaryReader.ReadByte();
              player.pantsColor.G = binaryReader.ReadByte();
              player.pantsColor.B = binaryReader.ReadByte();
              player.shoeColor.R = binaryReader.ReadByte();
              player.shoeColor.G = binaryReader.ReadByte();
              player.shoeColor.B = binaryReader.ReadByte();
              for (int index = 0; index < 8; ++index)
                player.armor[index].SetDefaults(Item.VersionName(binaryReader.ReadString(), release));
              if (release >= 6)
              {
                for (int index = 8; index < 11; ++index)
                  player.armor[index].SetDefaults(Item.VersionName(binaryReader.ReadString(), release));
              }
              for (int index = 0; index < 44; ++index)
              {
                player.inventory[index].SetDefaults(Item.VersionName(binaryReader.ReadString(), release));
                player.inventory[index].stack = binaryReader.ReadInt32();
              }
              for (int index = 0; index < Chest.maxItems; ++index)
              {
                player.bank[index].SetDefaults(Item.VersionName(binaryReader.ReadString(), release));
                player.bank[index].stack = binaryReader.ReadInt32();
              }
              for (int index = 0; index < 200; ++index)
              {
                int num = binaryReader.ReadInt32();
                if (num != -1)
                {
                  player.spX[index] = num;
                  player.spY[index] = binaryReader.ReadInt32();
                  player.spI[index] = binaryReader.ReadInt32();
                  player.spN[index] = binaryReader.ReadString();
                }
                else
                  break;
              }
              binaryReader.Close();
            }
          }
          player.PlayerFrame();
          File.Delete(str);
          return player;
        }
      }
      catch
      {
        flag = true;
      }
      if (flag)
      {
        string str = playerPath + ".bak";
        if (File.Exists(str))
        {
          File.Delete(playerPath);
          File.Move(str, playerPath);
          return Player.LoadPlayer(playerPath);
        }
        else
          return new Player();
      }
      else
        return new Player();
    }
  }
}
