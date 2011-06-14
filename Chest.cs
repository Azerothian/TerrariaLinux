// Type: Terraria.Chest
// Assembly: TerrariaServer, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// Assembly location: C:\Temp\New Folder\TerrariaServer.exe

namespace Terraria
{
    public class Chest
    {
        public static int maxItems = 20;
        public Item[] item = new Item[Chest.maxItems];
        public int x;
        public int y;

        static Chest()
        {
        }

        public static int UsingChest(int i)
        {
            if (Main.chest[i] != null)
            {
                for (int index = 0; index < (int)byte.MaxValue; ++index)
                {
                    if (Main.player[index].active && Main.player[index].chest == i)
                        return index;
                }
            }
            return -1;
        }

        public static int FindChest(int X, int Y)
        {
            for (int index = 0; index < 1000; ++index)
            {
                if (Main.chest[index] != null && Main.chest[index].x == X && Main.chest[index].y == Y)
                    return index;
            }
            return -1;
        }

        public static int CreateChest(int X, int Y)
        {
            for (int index = 0; index < 1000; ++index)
            {
                if (Main.chest[index] != null && Main.chest[index].x == X && Main.chest[index].y == Y)
                    return -1;
            }
            for (int index1 = 0; index1 < 1000; ++index1)
            {
                if (Main.chest[index1] == null)
                {
                    Main.chest[index1] = new Chest();
                    Main.chest[index1].x = X;
                    Main.chest[index1].y = Y;
                    for (int index2 = 0; index2 < Chest.maxItems; ++index2)
                        Main.chest[index1].item[index2] = new Item();
                    return index1;
                }
            }
            return -1;
        }

        public static bool DestroyChest(int X, int Y)
        {
            for (int index1 = 0; index1 < 1000; ++index1)
            {
                if (Main.chest[index1] != null && Main.chest[index1].x == X && Main.chest[index1].y == Y)
                {
                    for (int index2 = 0; index2 < Chest.maxItems; ++index2)
                    {
                        if (Main.chest[index1].item[index2].type > 0 && Main.chest[index1].item[index2].stack > 0)
                            return false;
                    }
                    Main.chest[index1] = (Chest)null;
                    return true;
                }
            }
            return true;
        }

        public void SetupShop(int type)
        {
            for (int index = 0; index < Chest.maxItems; ++index)
                this.item[index] = new Item();
            int num;
            if (type == 1)
            {
                int index1 = 0;
                this.item[index1].SetDefaults("Mining Helmet");
                int index2 = index1 + 1;
                this.item[index2].SetDefaults("Piggy Bank");
                int index3 = index2 + 1;
                this.item[index3].SetDefaults("Iron Anvil");
                int index4 = index3 + 1;
                this.item[index4].SetDefaults("Copper Pickaxe");
                int index5 = index4 + 1;
                this.item[index5].SetDefaults("Copper Axe");
                int index6 = index5 + 1;
                this.item[index6].SetDefaults("Torch");
                int index7 = index6 + 1;
                this.item[index7].SetDefaults("Lesser Healing Potion");
                int index8 = index7 + 1;
                this.item[index8].SetDefaults("Wooden Arrow");
                int index9 = index8 + 1;
                this.item[index9].SetDefaults("Shuriken");
                num = index9 + 1;
            }
            else if (type == 2)
            {
                int index1 = 0;
                this.item[index1].SetDefaults("Musket Ball");
                int index2 = index1 + 1;
                this.item[index2].SetDefaults("Flintlock Pistol");
                int index3 = index2 + 1;
                this.item[index3].SetDefaults("Minishark");
                num = index3 + 1;
            }
            else if (type == 3)
            {
                int index1 = 0;
                this.item[index1].SetDefaults("Purification Powder");
                int index2 = index1 + 1;
                this.item[index2].SetDefaults("Acorn");
                int index3 = index2 + 1;
                this.item[index3].SetDefaults("Grass Seeds");
                int index4 = index3 + 1;
                this.item[index4].SetDefaults("Sunflower");
                int index5 = index4 + 1;
                this.item[index5].SetDefaults(114);
                num = index5 + 1;
            }
            else if (type == 4)
            {
                int index1 = 0;
                this.item[index1].SetDefaults("Grenade");
                int index2 = index1 + 1;
                this.item[index2].SetDefaults("Bomb");
                int index3 = index2 + 1;
                this.item[index3].SetDefaults("Dynamite");
                num = index3 + 1;
            }
            else if (type == 5)
            {
                int index1 = 0;
                this.item[index1].SetDefaults(254);
                int index2 = index1 + 1;
                this.item[index2].SetDefaults(242);
                int index3 = index2 + 1;
                this.item[index3].SetDefaults(245);
                int index4 = index3 + 1;
                this.item[index4].SetDefaults(246);
                num = index4 + 1;
            }
        }
    }
}
