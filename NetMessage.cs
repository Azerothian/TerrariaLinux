// Type: Terraria.NetMessage
// Assembly: TerrariaServer, Version=1.0.4.0, Culture=neutral, PublicKeyToken=null
// Assembly location: C:\Temp\New Folder\TerrariaServer.exe

using System;
using System.Diagnostics;
using System.Text;

namespace Terraria
{
    public class NetMessage
    {
        public static messageBuffer[] buffer = new messageBuffer[257];

        static NetMessage()
        {
        }

        public static void SendData(int msgType, int remoteClient = -1, int ignoreClient = -1, string text = "", int number = 0, float number2 = 0.0f, float number3 = 0.0f, float number4 = 0.0f)
        {
            int whoAmi = 256;
            if (Main.netMode == 2 && remoteClient >= 0)
            {
                whoAmi = remoteClient;
            }
            lock (NetMessage.buffer[whoAmi])
            {
                
                int local_1 = 5;
                int local_2 = local_1;
                int local_2_38;
                if (msgType == 1)
                {
                    byte[] local_3 = BitConverter.GetBytes(msgType);
                    byte[] local_4 = Encoding.ASCII.GetBytes("Terraria" + (object)Main.curRelease);
                    local_1 += local_4.Length;
                    Buffer.BlockCopy((Array)BitConverter.GetBytes(local_1 - 4), 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 0, 4);
                    Buffer.BlockCopy((Array)local_3, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 4, 1);
                    Buffer.BlockCopy((Array)local_4, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 5, local_4.Length);
                }
                else if (msgType == 2)
                {
                    byte[] local_3_1 = BitConverter.GetBytes(msgType);
                    byte[] local_4_1 = Encoding.ASCII.GetBytes(text);
                    local_1 += local_4_1.Length;
                    Buffer.BlockCopy((Array)BitConverter.GetBytes(local_1 - 4), 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 0, 4);
                    Buffer.BlockCopy((Array)local_3_1, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 4, 1);
                    Buffer.BlockCopy((Array)local_4_1, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 5, local_4_1.Length);
                    if (Main.dedServ)
                    {

                        Console.WriteLine(Netplay.serverSock[whoAmi].tcpClient.Client.RemoteEndPoint.ToString() + " was booted: " + text);
                    }
                }
                else if (msgType == 3)
                {
                    byte[] local_3_2 = BitConverter.GetBytes(msgType);
                    byte[] local_4_2 = BitConverter.GetBytes(remoteClient);
                    local_1 += local_4_2.Length;
                    Buffer.BlockCopy((Array)BitConverter.GetBytes(local_1 - 4), 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 0, 4);
                    Buffer.BlockCopy((Array)local_3_2, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 4, 1);
                    Buffer.BlockCopy((Array)local_4_2, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 5, local_4_2.Length);
                }
                else if (msgType == 4)
                {
                    byte[] local_3_3 = BitConverter.GetBytes(msgType);
                    byte local_6 = (byte)number;
                    byte local_7 = (byte)Main.player[(int)local_6].hair;
                    byte[] local_8 = Encoding.ASCII.GetBytes(text);
                    local_1 += 23 + local_8.Length;
                    Buffer.BlockCopy((Array)BitConverter.GetBytes(local_1 - 4), 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 0, 4);
                    Buffer.BlockCopy((Array)local_3_3, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 4, 1);
                    NetMessage.buffer[whoAmi].writeBuffer[5] = local_6;
                    int local_2_1 = local_2 + 1;
                    NetMessage.buffer[whoAmi].writeBuffer[6] = local_7;
                    int local_2_2 = local_2_1 + 1;
                    NetMessage.buffer[whoAmi].writeBuffer[local_2_2] = Main.player[(int)local_6].hairColor.R;
                    int local_2_3 = local_2_2 + 1;
                    NetMessage.buffer[whoAmi].writeBuffer[local_2_3] = Main.player[(int)local_6].hairColor.G;
                    int local_2_4 = local_2_3 + 1;
                    NetMessage.buffer[whoAmi].writeBuffer[local_2_4] = Main.player[(int)local_6].hairColor.B;
                    int local_2_5 = local_2_4 + 1;
                    NetMessage.buffer[whoAmi].writeBuffer[local_2_5] = Main.player[(int)local_6].skinColor.R;
                    int local_2_6 = local_2_5 + 1;
                    NetMessage.buffer[whoAmi].writeBuffer[local_2_6] = Main.player[(int)local_6].skinColor.G;
                    int local_2_7 = local_2_6 + 1;
                    NetMessage.buffer[whoAmi].writeBuffer[local_2_7] = Main.player[(int)local_6].skinColor.B;
                    int local_2_8 = local_2_7 + 1;
                    NetMessage.buffer[whoAmi].writeBuffer[local_2_8] = Main.player[(int)local_6].eyeColor.R;
                    int local_2_9 = local_2_8 + 1;
                    NetMessage.buffer[whoAmi].writeBuffer[local_2_9] = Main.player[(int)local_6].eyeColor.G;
                    int local_2_10 = local_2_9 + 1;
                    NetMessage.buffer[whoAmi].writeBuffer[local_2_10] = Main.player[(int)local_6].eyeColor.B;
                    int local_2_11 = local_2_10 + 1;
                    NetMessage.buffer[whoAmi].writeBuffer[local_2_11] = Main.player[(int)local_6].shirtColor.R;
                    int local_2_12 = local_2_11 + 1;
                    NetMessage.buffer[whoAmi].writeBuffer[local_2_12] = Main.player[(int)local_6].shirtColor.G;
                    int local_2_13 = local_2_12 + 1;
                    NetMessage.buffer[whoAmi].writeBuffer[local_2_13] = Main.player[(int)local_6].shirtColor.B;
                    int local_2_14 = local_2_13 + 1;
                    NetMessage.buffer[whoAmi].writeBuffer[local_2_14] = Main.player[(int)local_6].underShirtColor.R;
                    int local_2_15 = local_2_14 + 1;
                    NetMessage.buffer[whoAmi].writeBuffer[local_2_15] = Main.player[(int)local_6].underShirtColor.G;
                    int local_2_16 = local_2_15 + 1;
                    NetMessage.buffer[whoAmi].writeBuffer[local_2_16] = Main.player[(int)local_6].underShirtColor.B;
                    int local_2_17 = local_2_16 + 1;
                    NetMessage.buffer[whoAmi].writeBuffer[local_2_17] = Main.player[(int)local_6].pantsColor.R;
                    int local_2_18 = local_2_17 + 1;
                    NetMessage.buffer[whoAmi].writeBuffer[local_2_18] = Main.player[(int)local_6].pantsColor.G;
                    int local_2_19 = local_2_18 + 1;
                    NetMessage.buffer[whoAmi].writeBuffer[local_2_19] = Main.player[(int)local_6].pantsColor.B;
                    int local_2_20 = local_2_19 + 1;
                    NetMessage.buffer[whoAmi].writeBuffer[local_2_20] = Main.player[(int)local_6].shoeColor.R;
                    int local_2_21 = local_2_20 + 1;
                    NetMessage.buffer[whoAmi].writeBuffer[local_2_21] = Main.player[(int)local_6].shoeColor.G;
                    int local_2_22 = local_2_21 + 1;
                    NetMessage.buffer[whoAmi].writeBuffer[local_2_22] = Main.player[(int)local_6].shoeColor.B;
                    int local_2_23 = local_2_22 + 1;
                    Buffer.BlockCopy((Array)local_8, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_23, local_8.Length);
                }
                else if (msgType == 5)
                {
                    byte[] local_3_4 = BitConverter.GetBytes(msgType);
                    byte local_6_1 = (byte)number;
                    byte local_7_1 = (byte)number2;
                    byte local_9;
                    if ((double)number2 < 44.0)
                    {
                        local_9 = (byte)Main.player[number].inventory[(int)number2].stack;
                        if (Main.player[number].inventory[(int)number2].stack < 0)
                            local_9 = (byte)0;
                    }
                    else
                    {
                        local_9 = (byte)Main.player[number].armor[(int)number2 - 44].stack;
                        if (Main.player[number].armor[(int)number2 - 44].stack < 0)
                            local_9 = (byte)0;
                    }
                    byte[] local_11 = Encoding.ASCII.GetBytes(text ?? "");
                    local_1 += 3 + local_11.Length;
                    Buffer.BlockCopy((Array)BitConverter.GetBytes(local_1 - 4), 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 0, 4);
                    Buffer.BlockCopy((Array)local_3_4, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 4, 1);
                    NetMessage.buffer[whoAmi].writeBuffer[5] = local_6_1;
                    int local_2_24 = local_2 + 1;
                    NetMessage.buffer[whoAmi].writeBuffer[6] = local_7_1;
                    int local_2_25 = local_2_24 + 1;
                    NetMessage.buffer[whoAmi].writeBuffer[7] = local_9;
                    int local_2_26 = local_2_25 + 1;
                    Buffer.BlockCopy((Array)local_11, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_26, local_11.Length);
                }
                else if (msgType == 6)
                {
                    byte[] local_3_5 = BitConverter.GetBytes(msgType);
                    Buffer.BlockCopy((Array)BitConverter.GetBytes(local_1 - 4), 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 0, 4);
                    Buffer.BlockCopy((Array)local_3_5, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 4, 1);
                }
                else if (msgType == 7)
                {
                    byte[] local_3_6 = BitConverter.GetBytes(msgType);
                    byte[] local_4_3 = BitConverter.GetBytes((int)Main.time);
                    byte local_7_2 = (byte)0;
                    if (Main.dayTime)
                        local_7_2 = (byte)1;
                    byte local_9_1 = (byte)Main.moonPhase;
                    byte local_12 = (byte)0;
                    if (Main.bloodMoon)
                        local_12 = (byte)1;
                    byte[] local_11_1 = BitConverter.GetBytes(Main.maxTilesX);
                    byte[] local_13 = BitConverter.GetBytes(Main.maxTilesY);
                    byte[] local_14 = BitConverter.GetBytes(Main.spawnTileX);
                    byte[] local_15 = BitConverter.GetBytes(Main.spawnTileY);
                    byte[] local_16 = BitConverter.GetBytes((int)Main.worldSurface);
                    byte[] local_17 = BitConverter.GetBytes((int)Main.rockLayer);
                    byte[] local_18 = BitConverter.GetBytes(Main.worldID);
                    byte[] local_19 = Encoding.ASCII.GetBytes(Main.worldName);
                    local_1 += local_4_3.Length + 1 + 1 + 1 + local_11_1.Length + local_13.Length + local_14.Length + local_15.Length + local_16.Length + local_17.Length + local_18.Length + local_19.Length;
                    Buffer.BlockCopy((Array)BitConverter.GetBytes(local_1 - 4), 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 0, 4);
                    Buffer.BlockCopy((Array)local_3_6, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 4, 1);
                    Buffer.BlockCopy((Array)local_4_3, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 5, local_4_3.Length);
                    int local_2_27 = local_2 + local_4_3.Length;
                    NetMessage.buffer[whoAmi].writeBuffer[local_2_27] = local_7_2;
                    int local_2_28 = local_2_27 + 1;
                    NetMessage.buffer[whoAmi].writeBuffer[local_2_28] = local_9_1;
                    int local_2_29 = local_2_28 + 1;
                    NetMessage.buffer[whoAmi].writeBuffer[local_2_29] = local_12;
                    int local_2_30 = local_2_29 + 1;
                    Buffer.BlockCopy((Array)local_11_1, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_30, local_11_1.Length);
                    int local_2_31 = local_2_30 + local_11_1.Length;
                    Buffer.BlockCopy((Array)local_13, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_31, local_13.Length);
                    int local_2_32 = local_2_31 + local_13.Length;
                    Buffer.BlockCopy((Array)local_14, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_32, local_14.Length);
                    int local_2_33 = local_2_32 + local_14.Length;
                    Buffer.BlockCopy((Array)local_15, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_33, local_15.Length);
                    int local_2_34 = local_2_33 + local_15.Length;
                    Buffer.BlockCopy((Array)local_16, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_34, local_16.Length);
                    int local_2_35 = local_2_34 + local_16.Length;
                    Buffer.BlockCopy((Array)local_17, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_35, local_17.Length);
                    int local_2_36 = local_2_35 + local_17.Length;
                    Buffer.BlockCopy((Array)local_18, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_36, local_18.Length);
                    int local_2_37 = local_2_36 + local_18.Length;
                    Buffer.BlockCopy((Array)local_19, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_37, local_19.Length);
                    local_2_38 = local_2_37 + local_19.Length;
                }
                else if (msgType == 8)
                {
                    byte[] local_3_7 = BitConverter.GetBytes(msgType);
                    byte[] local_20 = BitConverter.GetBytes(number);
                    byte[] local_21 = BitConverter.GetBytes((int)number2);
                    local_1 += local_20.Length + local_21.Length;
                    Buffer.BlockCopy((Array)BitConverter.GetBytes(local_1 - 4), 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 0, 4);
                    Buffer.BlockCopy((Array)local_3_7, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 4, 1);
                    Buffer.BlockCopy((Array)local_20, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2, 4);
                    int local_2_39 = local_2 + 4;
                    Buffer.BlockCopy((Array)local_21, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_39, 4);
                }
                else if (msgType == 9)
                {
                    byte[] local_3_8 = BitConverter.GetBytes(msgType);
                    byte[] local_4_4 = BitConverter.GetBytes(number);
                    byte[] local_22 = Encoding.ASCII.GetBytes(text);
                    local_1 += local_4_4.Length + local_22.Length;
                    Buffer.BlockCopy((Array)BitConverter.GetBytes(local_1 - 4), 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 0, 4);
                    Buffer.BlockCopy((Array)local_3_8, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 4, 1);
                    Buffer.BlockCopy((Array)local_4_4, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2, 4);
                    int local_2_40 = local_2 + 4;
                    Buffer.BlockCopy((Array)local_22, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_40, local_22.Length);
                }
                else if (msgType == 10)
                {
                    short local_23 = (short)number;
                    int local_24 = (int)number2;
                    int local_25 = (int)number3;
                    Buffer.BlockCopy((Array)BitConverter.GetBytes(msgType), 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 4, 1);
                    Buffer.BlockCopy((Array)BitConverter.GetBytes(local_23), 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2, 2);
                    int local_2_41 = local_2 + 2;
                    Buffer.BlockCopy((Array)BitConverter.GetBytes(local_24), 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_41, 4);
                    int local_2_42 = local_2_41 + 4;
                    Buffer.BlockCopy((Array)BitConverter.GetBytes(local_25), 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_42, 4);
                    int local_2_43 = local_2_42 + 4;
                    for (int local_29 = local_24; local_29 < local_24 + (int)local_23; ++local_29)
                    {
                        byte local_30 = (byte)0;
                        if (Main.tile[local_29, local_25].active)
                            ++local_30;
                        if (Main.tile[local_29, local_25].lighted)
                            local_30 += (byte)2;
                        if ((int)Main.tile[local_29, local_25].wall > 0)
                            local_30 += (byte)4;
                        if ((int)Main.tile[local_29, local_25].liquid > 0)
                            local_30 += (byte)8;
                        NetMessage.buffer[whoAmi].writeBuffer[local_2_43] = local_30;
                        ++local_2_43;
                        byte[] local_31 = BitConverter.GetBytes(Main.tile[local_29, local_25].frameX);
                        byte[] local_32 = BitConverter.GetBytes(Main.tile[local_29, local_25].frameY);
                        byte local_33 = Main.tile[local_29, local_25].wall;
                        if (Main.tile[local_29, local_25].active)
                        {
                            NetMessage.buffer[whoAmi].writeBuffer[local_2_43] = Main.tile[local_29, local_25].type;
                            ++local_2_43;
                            if (Main.tileFrameImportant[(int)Main.tile[local_29, local_25].type])
                            {
                                Buffer.BlockCopy((Array)local_31, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_43, 2);
                                int local_2_44 = local_2_43 + 2;
                                Buffer.BlockCopy((Array)local_32, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_44, 2);
                                local_2_43 = local_2_44 + 2;
                            }
                        }
                        if ((int)local_33 > 0)
                        {
                            NetMessage.buffer[whoAmi].writeBuffer[local_2_43] = local_33;
                            ++local_2_43;
                        }
                        if ((int)Main.tile[local_29, local_25].liquid > 0)
                        {
                            NetMessage.buffer[whoAmi].writeBuffer[local_2_43] = Main.tile[local_29, local_25].liquid;
                            int local_2_45 = local_2_43 + 1;
                            byte local_34 = (byte)0;
                            if (Main.tile[local_29, local_25].lava)
                                local_34 = (byte)1;
                            NetMessage.buffer[whoAmi].writeBuffer[local_2_45] = local_34;
                            local_2_43 = local_2_45 + 1;
                        }
                    }
                    Buffer.BlockCopy((Array)BitConverter.GetBytes(local_2_43 - 4), 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 0, 4);
                    local_1 = local_2_43;
                }
                else if (msgType == 11)
                {
                    byte[] local_3_10 = BitConverter.GetBytes(msgType);
                    byte[] local_35 = BitConverter.GetBytes(number);
                    byte[] local_36 = BitConverter.GetBytes((int)number2);
                    byte[] local_37 = BitConverter.GetBytes((int)number3);
                    byte[] local_38 = BitConverter.GetBytes((int)number4);
                    local_1 += local_35.Length + local_36.Length + local_37.Length + local_38.Length;
                    Buffer.BlockCopy((Array)BitConverter.GetBytes(local_1 - 4), 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 0, 4);
                    Buffer.BlockCopy((Array)local_3_10, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 4, 1);
                    Buffer.BlockCopy((Array)local_35, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2, 4);
                    int local_2_46 = local_2 + 4;
                    Buffer.BlockCopy((Array)local_36, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_46, 4);
                    int local_2_47 = local_2_46 + 4;
                    Buffer.BlockCopy((Array)local_37, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_47, 4);
                    int local_2_48 = local_2_47 + 4;
                    Buffer.BlockCopy((Array)local_38, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_48, 4);
                    local_2_38 = local_2_48 + 4;
                }
                else if (msgType == 12)
                {
                    byte[] local_3_11 = BitConverter.GetBytes(msgType);
                    byte local_39 = (byte)number;
                    byte[] local_40 = BitConverter.GetBytes(Main.player[(int)local_39].SpawnX);
                    byte[] local_41 = BitConverter.GetBytes(Main.player[(int)local_39].SpawnY);
                    local_1 += 1 + local_40.Length + local_41.Length;
                    Buffer.BlockCopy((Array)BitConverter.GetBytes(local_1 - 4), 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 0, 4);
                    Buffer.BlockCopy((Array)local_3_11, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 4, 1);
                    NetMessage.buffer[whoAmi].writeBuffer[local_2] = local_39;
                    int local_2_49 = local_2 + 1;
                    Buffer.BlockCopy((Array)local_40, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_49, 4);
                    int local_2_50 = local_2_49 + 4;
                    Buffer.BlockCopy((Array)local_41, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_50, 4);
                    local_2_38 = local_2_50 + 4;
                }
                else if (msgType == 13)
                {
                    byte[] local_3_12 = BitConverter.GetBytes(msgType);
                    byte local_39_1 = (byte)number;
                    byte local_42 = (byte)0;
                    if (Main.player[(int)local_39_1].controlUp)
                        ++local_42;
                    if (Main.player[(int)local_39_1].controlDown)
                        local_42 += (byte)2;
                    if (Main.player[(int)local_39_1].controlLeft)
                        local_42 += (byte)4;
                    if (Main.player[(int)local_39_1].controlRight)
                        local_42 += (byte)8;
                    if (Main.player[(int)local_39_1].controlJump)
                        local_42 += (byte)16;
                    if (Main.player[(int)local_39_1].controlUseItem)
                        local_42 += (byte)32;
                    if (Main.player[(int)local_39_1].direction == 1)
                        local_42 += (byte)64;
                    byte local_43 = (byte)Main.player[(int)local_39_1].selectedItem;
                    byte[] local_40_1 = BitConverter.GetBytes(Main.player[number].position.X);
                    byte[] local_41_1 = BitConverter.GetBytes(Main.player[number].position.Y);
                    byte[] local_44 = BitConverter.GetBytes(Main.player[number].velocity.X);
                    byte[] local_45 = BitConverter.GetBytes(Main.player[number].velocity.Y);
                    local_1 += 3 + local_40_1.Length + local_41_1.Length + local_44.Length + local_45.Length;
                    Buffer.BlockCopy((Array)BitConverter.GetBytes(local_1 - 4), 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 0, 4);
                    Buffer.BlockCopy((Array)local_3_12, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 4, 1);
                    NetMessage.buffer[whoAmi].writeBuffer[5] = local_39_1;
                    int local_2_51 = local_2 + 1;
                    NetMessage.buffer[whoAmi].writeBuffer[6] = local_42;
                    int local_2_52 = local_2_51 + 1;
                    NetMessage.buffer[whoAmi].writeBuffer[7] = local_43;
                    int local_2_53 = local_2_52 + 1;
                    Buffer.BlockCopy((Array)local_40_1, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_53, 4);
                    int local_2_54 = local_2_53 + 4;
                    Buffer.BlockCopy((Array)local_41_1, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_54, 4);
                    int local_2_55 = local_2_54 + 4;
                    Buffer.BlockCopy((Array)local_44, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_55, 4);
                    int local_2_56 = local_2_55 + 4;
                    Buffer.BlockCopy((Array)local_45, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_56, 4);
                }
                else if (msgType == 14)
                {
                    byte[] local_3_13 = BitConverter.GetBytes(msgType);
                    byte local_39_2 = (byte)number;
                    byte local_46 = (byte)number2;
                    local_1 += 2;
                    Buffer.BlockCopy((Array)BitConverter.GetBytes(local_1 - 4), 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 0, 4);
                    Buffer.BlockCopy((Array)local_3_13, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 4, 1);
                    NetMessage.buffer[whoAmi].writeBuffer[5] = local_39_2;
                    NetMessage.buffer[whoAmi].writeBuffer[6] = local_46;
                }
                else if (msgType == 15)
                {
                    byte[] local_3_14 = BitConverter.GetBytes(msgType);
                    Buffer.BlockCopy((Array)BitConverter.GetBytes(local_1 - 4), 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 0, 4);
                    Buffer.BlockCopy((Array)local_3_14, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 4, 1);
                }
                else if (msgType == 16)
                {
                    byte[] local_3_15 = BitConverter.GetBytes(msgType);
                    byte local_39_3 = (byte)number;
                    byte[] local_47 = BitConverter.GetBytes((short)Main.player[(int)local_39_3].statLife);
                    byte[] local_48 = BitConverter.GetBytes((short)Main.player[(int)local_39_3].statLifeMax);
                    local_1 += 1 + local_47.Length + local_48.Length;
                    Buffer.BlockCopy((Array)BitConverter.GetBytes(local_1 - 4), 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 0, 4);
                    Buffer.BlockCopy((Array)local_3_15, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 4, 1);
                    NetMessage.buffer[whoAmi].writeBuffer[5] = local_39_3;
                    int local_2_57 = local_2 + 1;
                    Buffer.BlockCopy((Array)local_47, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_57, 2);
                    int local_2_58 = local_2_57 + 2;
                    Buffer.BlockCopy((Array)local_48, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_58, 2);
                }
                else if (msgType == 17)
                {
                    byte[] local_3_16 = BitConverter.GetBytes(msgType);
                    byte local_49 = (byte)number;
                    byte[] local_40_2 = BitConverter.GetBytes((int)number2);
                    byte[] local_41_2 = BitConverter.GetBytes((int)number3);
                    byte local_50 = (byte)number4;
                    local_1 += 1 + local_40_2.Length + local_41_2.Length + 1;
                    Buffer.BlockCopy((Array)BitConverter.GetBytes(local_1 - 4), 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 0, 4);
                    Buffer.BlockCopy((Array)local_3_16, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 4, 1);
                    NetMessage.buffer[whoAmi].writeBuffer[local_2] = local_49;
                    int local_2_59 = local_2 + 1;
                    Buffer.BlockCopy((Array)local_40_2, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_59, 4);
                    int local_2_60 = local_2_59 + 4;
                    Buffer.BlockCopy((Array)local_41_2, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_60, 4);
                    int local_2_61 = local_2_60 + 4;
                    NetMessage.buffer[whoAmi].writeBuffer[local_2_61] = local_50;
                }
                else if (msgType == 18)
                {
                    byte[] local_3_17 = BitConverter.GetBytes(msgType);
                    BitConverter.GetBytes((int)Main.time);
                    byte local_51 = (byte)0;
                    if (Main.dayTime)
                        local_51 = (byte)1;
                    byte[] local_52 = BitConverter.GetBytes((int)Main.time);
                    byte[] local_53 = BitConverter.GetBytes(Main.sunModY);
                    byte[] local_54 = BitConverter.GetBytes(Main.moonModY);
                    local_1 += 1 + local_52.Length + local_53.Length + local_54.Length;
                    Buffer.BlockCopy((Array)BitConverter.GetBytes(local_1 - 4), 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 0, 4);
                    Buffer.BlockCopy((Array)local_3_17, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 4, 1);
                    NetMessage.buffer[whoAmi].writeBuffer[local_2] = local_51;
                    int local_2_62 = local_2 + 1;
                    Buffer.BlockCopy((Array)local_52, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_62, 4);
                    int local_2_63 = local_2_62 + 4;
                    Buffer.BlockCopy((Array)local_53, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_63, 2);
                    int local_2_64 = local_2_63 + 2;
                    Buffer.BlockCopy((Array)local_54, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_64, 2);
                    local_2_38 = local_2_64 + 2;
                }
                else if (msgType == 19)
                {
                    byte[] local_3_18 = BitConverter.GetBytes(msgType);
                    byte local_49_1 = (byte)number;
                    byte[] local_40_3 = BitConverter.GetBytes((int)number2);
                    byte[] local_41_3 = BitConverter.GetBytes((int)number3);
                    byte local_55 = (byte)0;
                    if ((double)number4 == 1.0)
                        local_55 = (byte)1;
                    local_1 += 1 + local_40_3.Length + local_41_3.Length + 1;
                    Buffer.BlockCopy((Array)BitConverter.GetBytes(local_1 - 4), 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 0, 4);
                    Buffer.BlockCopy((Array)local_3_18, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 4, 1);
                    NetMessage.buffer[whoAmi].writeBuffer[local_2] = local_49_1;
                    int local_2_65 = local_2 + 1;
                    Buffer.BlockCopy((Array)local_40_3, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_65, 4);
                    int local_2_66 = local_2_65 + 4;
                    Buffer.BlockCopy((Array)local_41_3, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_66, 4);
                    int local_2_67 = local_2_66 + 4;
                    NetMessage.buffer[whoAmi].writeBuffer[local_2_67] = local_55;
                }
                else if (msgType == 20)
                {
                    short local_23_1 = (short)number;
                    int local_24_1 = (int)number2;
                    int local_56 = (int)number3;
                    Buffer.BlockCopy((Array)BitConverter.GetBytes(msgType), 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 4, 1);
                    Buffer.BlockCopy((Array)BitConverter.GetBytes(local_23_1), 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2, 2);
                    int local_2_68 = local_2 + 2;
                    Buffer.BlockCopy((Array)BitConverter.GetBytes(local_24_1), 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_68, 4);
                    int local_2_69 = local_2_68 + 4;
                    Buffer.BlockCopy((Array)BitConverter.GetBytes(local_56), 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_69, 4);
                    int local_2_70 = local_2_69 + 4;
                    for (int local_29_1 = local_24_1; local_29_1 < local_24_1 + (int)local_23_1; ++local_29_1)
                    {
                        for (int local_25_1 = local_56; local_25_1 < local_56 + (int)local_23_1; ++local_25_1)
                        {
                            byte local_30_1 = (byte)0;
                            if (Main.tile[local_29_1, local_25_1].active)
                                ++local_30_1;
                            if (Main.tile[local_29_1, local_25_1].lighted)
                                local_30_1 += (byte)2;
                            if ((int)Main.tile[local_29_1, local_25_1].wall > 0)
                                local_30_1 += (byte)4;
                            if ((int)Main.tile[local_29_1, local_25_1].liquid > 0 && Main.netMode == 2)
                                local_30_1 += (byte)8;
                            NetMessage.buffer[whoAmi].writeBuffer[local_2_70] = local_30_1;
                            ++local_2_70;
                            byte[] local_31_1 = BitConverter.GetBytes(Main.tile[local_29_1, local_25_1].frameX);
                            byte[] local_32_1 = BitConverter.GetBytes(Main.tile[local_29_1, local_25_1].frameY);
                            byte local_33_1 = Main.tile[local_29_1, local_25_1].wall;
                            if (Main.tile[local_29_1, local_25_1].active)
                            {
                                NetMessage.buffer[whoAmi].writeBuffer[local_2_70] = Main.tile[local_29_1, local_25_1].type;
                                ++local_2_70;
                                if (Main.tileFrameImportant[(int)Main.tile[local_29_1, local_25_1].type])
                                {
                                    Buffer.BlockCopy((Array)local_31_1, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_70, 2);
                                    int local_2_71 = local_2_70 + 2;
                                    Buffer.BlockCopy((Array)local_32_1, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_71, 2);
                                    local_2_70 = local_2_71 + 2;
                                }
                            }
                            if ((int)local_33_1 > 0)
                            {
                                NetMessage.buffer[whoAmi].writeBuffer[local_2_70] = local_33_1;
                                ++local_2_70;
                            }
                            if ((int)Main.tile[local_29_1, local_25_1].liquid > 0 && Main.netMode == 2)
                            {
                                NetMessage.buffer[whoAmi].writeBuffer[local_2_70] = Main.tile[local_29_1, local_25_1].liquid;
                                int local_2_72 = local_2_70 + 1;
                                byte local_34_1 = (byte)0;
                                if (Main.tile[local_29_1, local_25_1].lava)
                                    local_34_1 = (byte)1;
                                NetMessage.buffer[whoAmi].writeBuffer[local_2_72] = local_34_1;
                                local_2_70 = local_2_72 + 1;
                            }
                        }
                    }
                    Buffer.BlockCopy((Array)BitConverter.GetBytes(local_2_70 - 4), 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 0, 4);
                    local_1 = local_2_70;
                }
                else if (msgType == 21)
                {
                    byte[] local_3_20 = BitConverter.GetBytes(msgType);
                    byte[] local_57 = BitConverter.GetBytes((short)number);
                    byte[] local_40_4 = BitConverter.GetBytes(Main.item[number].position.X);
                    byte[] local_41_4 = BitConverter.GetBytes(Main.item[number].position.Y);
                    byte[] local_44_1 = BitConverter.GetBytes(Main.item[number].velocity.X);
                    byte[] local_45_1 = BitConverter.GetBytes(Main.item[number].velocity.Y);
                    byte local_58 = (byte)Main.item[number].stack;
                    string local_59 = "0";
                    if (Main.item[number].active && Main.item[number].stack > 0)
                        local_59 = Main.item[number].name;
                    if (local_59 == null)
                        local_59 = "0";
                    byte[] local_60 = Encoding.ASCII.GetBytes(local_59);
                    local_1 += local_57.Length + local_40_4.Length + local_41_4.Length + local_44_1.Length + local_45_1.Length + 1 + local_60.Length;
                    Buffer.BlockCopy((Array)BitConverter.GetBytes(local_1 - 4), 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 0, 4);
                    Buffer.BlockCopy((Array)local_3_20, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 4, 1);
                    Buffer.BlockCopy((Array)local_57, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2, local_57.Length);
                    int local_2_73 = local_2 + 2;
                    Buffer.BlockCopy((Array)local_40_4, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_73, local_40_4.Length);
                    int local_2_74 = local_2_73 + 4;
                    Buffer.BlockCopy((Array)local_41_4, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_74, local_41_4.Length);
                    int local_2_75 = local_2_74 + 4;
                    Buffer.BlockCopy((Array)local_44_1, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_75, local_44_1.Length);
                    int local_2_76 = local_2_75 + 4;
                    Buffer.BlockCopy((Array)local_45_1, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_76, local_45_1.Length);
                    int local_2_77 = local_2_76 + 4;
                    NetMessage.buffer[whoAmi].writeBuffer[local_2_77] = local_58;
                    int local_2_78 = local_2_77 + 1;
                    Buffer.BlockCopy((Array)local_60, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_78, local_60.Length);
                }
                else if (msgType == 22)
                {
                    byte[] local_3_21 = BitConverter.GetBytes(msgType);
                    byte[] local_57_1 = BitConverter.GetBytes((short)number);
                    byte local_61 = (byte)Main.item[number].owner;
                    local_1 += local_57_1.Length + 1;
                    Buffer.BlockCopy((Array)BitConverter.GetBytes(local_1 - 4), 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 0, 4);
                    Buffer.BlockCopy((Array)local_3_21, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 4, 1);
                    Buffer.BlockCopy((Array)local_57_1, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2, local_57_1.Length);
                    int local_2_79 = local_2 + 2;
                    NetMessage.buffer[whoAmi].writeBuffer[local_2_79] = local_61;
                }
                else if (msgType == 23)
                {
                    byte[] local_3_22 = BitConverter.GetBytes(msgType);
                    byte[] local_57_2 = BitConverter.GetBytes((short)number);
                    byte[] local_40_5 = BitConverter.GetBytes(Main.npc[number].position.X);
                    byte[] local_41_5 = BitConverter.GetBytes(Main.npc[number].position.Y);
                    byte[] local_44_2 = BitConverter.GetBytes(Main.npc[number].velocity.X);
                    byte[] local_45_2 = BitConverter.GetBytes(Main.npc[number].velocity.Y);
                    byte[] local_62 = BitConverter.GetBytes((short)Main.npc[number].target);
                    byte[] local_47_1 = BitConverter.GetBytes((short)Main.npc[number].life);
                    if (!Main.npc[number].active)
                        local_47_1 = BitConverter.GetBytes((short)0);
                    byte[] local_63 = Encoding.ASCII.GetBytes(Main.npc[number].name);
                    local_1 += local_57_2.Length + local_40_5.Length + local_41_5.Length + local_44_2.Length + local_45_2.Length + local_62.Length + local_47_1.Length + NPC.maxAI * 4 + local_63.Length + 1 + 1;
                    Buffer.BlockCopy((Array)BitConverter.GetBytes(local_1 - 4), 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 0, 4);
                    Buffer.BlockCopy((Array)local_3_22, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 4, 1);
                    Buffer.BlockCopy((Array)local_57_2, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2, local_57_2.Length);
                    int local_2_80 = local_2 + 2;
                    Buffer.BlockCopy((Array)local_40_5, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_80, local_40_5.Length);
                    int local_2_81 = local_2_80 + 4;
                    Buffer.BlockCopy((Array)local_41_5, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_81, local_41_5.Length);
                    int local_2_82 = local_2_81 + 4;
                    Buffer.BlockCopy((Array)local_44_2, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_82, local_44_2.Length);
                    int local_2_83 = local_2_82 + 4;
                    Buffer.BlockCopy((Array)local_45_2, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_83, local_45_2.Length);
                    int local_2_84 = local_2_83 + 4;
                    Buffer.BlockCopy((Array)local_62, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_84, local_62.Length);
                    int local_2_85 = local_2_84 + 2;
                    NetMessage.buffer[whoAmi].writeBuffer[local_2_85] = (byte)(Main.npc[number].direction + 1);
                    int local_2_86 = local_2_85 + 1;
                    NetMessage.buffer[whoAmi].writeBuffer[local_2_86] = (byte)(Main.npc[number].directionY + 1);
                    int local_2_87 = local_2_86 + 1;
                    Buffer.BlockCopy((Array)local_47_1, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_87, local_47_1.Length);
                    int local_2_88 = local_2_87 + 2;
                    for (int local_56_1 = 0; local_56_1 < NPC.maxAI; ++local_56_1)
                    {
                        byte[] local_64 = BitConverter.GetBytes(Main.npc[number].ai[local_56_1]);
                        Buffer.BlockCopy((Array)local_64, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_88, local_64.Length);
                        local_2_88 += 4;
                    }
                    Buffer.BlockCopy((Array)local_63, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_88, local_63.Length);
                }
                else if (msgType == 24)
                {
                    byte[] local_3_23 = BitConverter.GetBytes(msgType);
                    byte[] local_57_3 = BitConverter.GetBytes((short)number);
                    byte local_39_4 = (byte)number2;
                    local_1 += local_57_3.Length + 1;
                    Buffer.BlockCopy((Array)BitConverter.GetBytes(local_1 - 4), 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 0, 4);
                    Buffer.BlockCopy((Array)local_3_23, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 4, 1);
                    Buffer.BlockCopy((Array)local_57_3, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2, local_57_3.Length);
                    int local_2_89 = local_2 + 2;
                    NetMessage.buffer[whoAmi].writeBuffer[local_2_89] = local_39_4;
                }
                else if (msgType == 25)
                {
                    byte[] local_3_24 = BitConverter.GetBytes(msgType);
                    byte local_39_5 = (byte)number;
                    byte[] local_65 = Encoding.ASCII.GetBytes(text);
                    byte local_66 = (byte)number2;
                    byte local_67 = (byte)number3;
                    byte local_68 = (byte)number4;
                    local_1 += 1 + local_65.Length + 3;
                    Buffer.BlockCopy((Array)BitConverter.GetBytes(local_1 - 4), 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 0, 4);
                    Buffer.BlockCopy((Array)local_3_24, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 4, 1);
                    NetMessage.buffer[whoAmi].writeBuffer[local_2] = local_39_5;
                    int local_2_90 = local_2 + 1;
                    NetMessage.buffer[whoAmi].writeBuffer[local_2_90] = local_66;
                    int local_2_91 = local_2_90 + 1;
                    NetMessage.buffer[whoAmi].writeBuffer[local_2_91] = local_67;
                    int local_2_92 = local_2_91 + 1;
                    NetMessage.buffer[whoAmi].writeBuffer[local_2_92] = local_68;
                    int local_2_93 = local_2_92 + 1;
                    Buffer.BlockCopy((Array)local_65, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_93, local_65.Length);
                }
                else if (msgType == 26)
                {
                    byte[] local_3_25 = BitConverter.GetBytes(msgType);
                    byte local_39_6 = (byte)number;
                    byte local_55_1 = (byte)((double)number2 + 1.0);
                    byte[] local_69 = BitConverter.GetBytes((short)number3);
                    byte local_70 = (byte)number4;
                    local_1 += 2 + local_69.Length + 1;
                    Buffer.BlockCopy((Array)BitConverter.GetBytes(local_1 - 4), 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 0, 4);
                    Buffer.BlockCopy((Array)local_3_25, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 4, 1);
                    NetMessage.buffer[whoAmi].writeBuffer[local_2] = local_39_6;
                    int local_2_94 = local_2 + 1;
                    NetMessage.buffer[whoAmi].writeBuffer[local_2_94] = local_55_1;
                    int local_2_95 = local_2_94 + 1;
                    Buffer.BlockCopy((Array)local_69, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_95, local_69.Length);
                    int local_2_96 = local_2_95 + 2;
                    NetMessage.buffer[whoAmi].writeBuffer[local_2_96] = local_70;
                }
                else if (msgType == 27)
                {
                    byte[] local_3_26 = BitConverter.GetBytes(msgType);
                    byte[] local_57_4 = BitConverter.GetBytes((short)Main.projectile[number].identity);
                    byte[] local_40_6 = BitConverter.GetBytes(Main.projectile[number].position.X);
                    byte[] local_41_6 = BitConverter.GetBytes(Main.projectile[number].position.Y);
                    byte[] local_44_3 = BitConverter.GetBytes(Main.projectile[number].velocity.X);
                    byte[] local_45_3 = BitConverter.GetBytes(Main.projectile[number].velocity.Y);
                    byte[] local_71 = BitConverter.GetBytes(Main.projectile[number].knockBack);
                    byte[] local_69_1 = BitConverter.GetBytes((short)Main.projectile[number].damage);
                    Buffer.BlockCopy((Array)local_3_26, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 4, 1);
                    Buffer.BlockCopy((Array)local_57_4, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2, local_57_4.Length);
                    int local_2_97 = local_2 + 2;
                    Buffer.BlockCopy((Array)local_40_6, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_97, local_40_6.Length);
                    int local_2_98 = local_2_97 + 4;
                    Buffer.BlockCopy((Array)local_41_6, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_98, local_41_6.Length);
                    int local_2_99 = local_2_98 + 4;
                    Buffer.BlockCopy((Array)local_44_3, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_99, local_44_3.Length);
                    int local_2_100 = local_2_99 + 4;
                    Buffer.BlockCopy((Array)local_45_3, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_100, local_45_3.Length);
                    int local_2_101 = local_2_100 + 4;
                    Buffer.BlockCopy((Array)local_71, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_101, local_71.Length);
                    int local_2_102 = local_2_101 + 4;
                    Buffer.BlockCopy((Array)local_69_1, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_102, local_69_1.Length);
                    int local_2_103 = local_2_102 + 2;
                    NetMessage.buffer[whoAmi].writeBuffer[local_2_103] = (byte)Main.projectile[number].owner;
                    int local_2_104 = local_2_103 + 1;
                    NetMessage.buffer[whoAmi].writeBuffer[local_2_104] = (byte)Main.projectile[number].type;
                    int local_2_105 = local_2_104 + 1;
                    for (int local_56_2 = 0; local_56_2 < Projectile.maxAI; ++local_56_2)
                    {
                        byte[] local_64_1 = BitConverter.GetBytes(Main.projectile[number].ai[local_56_2]);
                        Buffer.BlockCopy((Array)local_64_1, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_105, local_64_1.Length);
                        local_2_105 += 4;
                    }
                    local_1 += local_2_105;
                    Buffer.BlockCopy((Array)BitConverter.GetBytes(local_1 - 4), 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 0, 4);
                }
                else if (msgType == 28)
                {
                    byte[] local_3_27 = BitConverter.GetBytes(msgType);
                    byte[] local_57_5 = BitConverter.GetBytes((short)number);
                    byte[] local_69_2 = BitConverter.GetBytes((short)number2);
                    byte[] local_71_1 = BitConverter.GetBytes(number3);
                    byte local_72 = (byte)((double)number4 + 1.0);
                    local_1 += local_57_5.Length + local_69_2.Length + local_71_1.Length + 1;
                    Buffer.BlockCopy((Array)BitConverter.GetBytes(local_1 - 4), 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 0, 4);
                    Buffer.BlockCopy((Array)local_3_27, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 4, 1);
                    Buffer.BlockCopy((Array)local_57_5, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2, local_57_5.Length);
                    int local_2_106 = local_2 + 2;
                    Buffer.BlockCopy((Array)local_69_2, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_106, local_69_2.Length);
                    int local_2_107 = local_2_106 + 2;
                    Buffer.BlockCopy((Array)local_71_1, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_107, local_71_1.Length);
                    int local_2_108 = local_2_107 + 4;
                    NetMessage.buffer[whoAmi].writeBuffer[local_2_108] = local_72;
                }
                else if (msgType == 29)
                {
                    byte[] local_3_28 = BitConverter.GetBytes(msgType);
                    byte[] local_57_6 = BitConverter.GetBytes((short)number);
                    byte local_61_1 = (byte)number2;
                    local_1 += local_57_6.Length + 1;
                    Buffer.BlockCopy((Array)BitConverter.GetBytes(local_1 - 4), 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 0, 4);
                    Buffer.BlockCopy((Array)local_3_28, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 4, 1);
                    Buffer.BlockCopy((Array)local_57_6, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2, local_57_6.Length);
                    int local_2_109 = local_2 + 2;
                    NetMessage.buffer[whoAmi].writeBuffer[local_2_109] = local_61_1;
                }
                else if (msgType == 30)
                {
                    byte[] local_3_29 = BitConverter.GetBytes(msgType);
                    byte local_39_7 = (byte)number;
                    byte local_73 = (byte)0;
                    if (Main.player[(int)local_39_7].hostile)
                        local_73 = (byte)1;
                    local_1 += 2;
                    Buffer.BlockCopy((Array)BitConverter.GetBytes(local_1 - 4), 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 0, 4);
                    Buffer.BlockCopy((Array)local_3_29, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 4, 1);
                    NetMessage.buffer[whoAmi].writeBuffer[local_2] = local_39_7;
                    int local_2_110 = local_2 + 1;
                    NetMessage.buffer[whoAmi].writeBuffer[local_2_110] = local_73;
                }
                else if (msgType == 31)
                {
                    byte[] local_3_30 = BitConverter.GetBytes(msgType);
                    byte[] local_40_7 = BitConverter.GetBytes(number);
                    byte[] local_41_7 = BitConverter.GetBytes((int)number2);
                    local_1 += local_40_7.Length + local_41_7.Length;
                    Buffer.BlockCopy((Array)BitConverter.GetBytes(local_1 - 4), 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 0, 4);
                    Buffer.BlockCopy((Array)local_3_30, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 4, 1);
                    Buffer.BlockCopy((Array)local_40_7, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2, local_40_7.Length);
                    int local_2_111 = local_2 + 4;
                    Buffer.BlockCopy((Array)local_41_7, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_111, local_41_7.Length);
                }
                else if (msgType == 32)
                {
                    byte[] local_3_31 = BitConverter.GetBytes(msgType);
                    byte[] local_74 = BitConverter.GetBytes((short)number);
                    byte local_75 = (byte)number2;
                    byte local_58_1 = (byte)Main.chest[number].item[(int)number2].stack;
                    byte[] local_76 = Main.chest[number].item[(int)number2].name != null ? Encoding.ASCII.GetBytes(Main.chest[number].item[(int)number2].name) : Encoding.ASCII.GetBytes("");
                    local_1 += local_74.Length + 1 + 1 + local_76.Length;
                    Buffer.BlockCopy((Array)BitConverter.GetBytes(local_1 - 4), 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 0, 4);
                    Buffer.BlockCopy((Array)local_3_31, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 4, 1);
                    Buffer.BlockCopy((Array)local_74, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2, local_74.Length);
                    int local_2_112 = local_2 + 2;
                    NetMessage.buffer[whoAmi].writeBuffer[local_2_112] = local_75;
                    int local_2_113 = local_2_112 + 1;
                    NetMessage.buffer[whoAmi].writeBuffer[local_2_113] = local_58_1;
                    int local_2_114 = local_2_113 + 1;
                    Buffer.BlockCopy((Array)local_76, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_114, local_76.Length);
                }
                else if (msgType == 33)
                {
                    byte[] local_3_32 = BitConverter.GetBytes(msgType);
                    byte[] local_74_1 = BitConverter.GetBytes((short)number);
                    byte[] local_40_8;
                    byte[] local_41_8;
                    if (number > -1)
                    {
                        local_40_8 = BitConverter.GetBytes(Main.chest[number].x);
                        local_41_8 = BitConverter.GetBytes(Main.chest[number].y);
                    }
                    else
                    {
                        local_40_8 = BitConverter.GetBytes(0);
                        local_41_8 = BitConverter.GetBytes(0);
                    }
                    local_1 += local_74_1.Length + local_40_8.Length + local_41_8.Length;
                    Buffer.BlockCopy((Array)BitConverter.GetBytes(local_1 - 4), 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 0, 4);
                    Buffer.BlockCopy((Array)local_3_32, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 4, 1);
                    Buffer.BlockCopy((Array)local_74_1, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2, local_74_1.Length);
                    int local_2_115 = local_2 + 2;
                    Buffer.BlockCopy((Array)local_40_8, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_115, local_40_8.Length);
                    int local_2_116 = local_2_115 + 4;
                    Buffer.BlockCopy((Array)local_41_8, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_116, local_41_8.Length);
                }
                else if (msgType == 34)
                {
                    byte[] local_3_33 = BitConverter.GetBytes(msgType);
                    byte[] local_40_9 = BitConverter.GetBytes(number);
                    byte[] local_41_9 = BitConverter.GetBytes((int)number2);
                    local_1 += local_40_9.Length + local_41_9.Length;
                    Buffer.BlockCopy((Array)BitConverter.GetBytes(local_1 - 4), 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 0, 4);
                    Buffer.BlockCopy((Array)local_3_33, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 4, 1);
                    Buffer.BlockCopy((Array)local_40_9, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2, local_40_9.Length);
                    int local_2_117 = local_2 + 4;
                    Buffer.BlockCopy((Array)local_41_9, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_117, local_41_9.Length);
                }
                else if (msgType == 35)
                {
                    byte[] local_3_34 = BitConverter.GetBytes(msgType);
                    byte local_39_8 = (byte)number;
                    byte[] local_77 = BitConverter.GetBytes((short)number2);
                    local_1 += 1 + local_77.Length;
                    Buffer.BlockCopy((Array)BitConverter.GetBytes(local_1 - 4), 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 0, 4);
                    Buffer.BlockCopy((Array)local_3_34, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 4, 1);
                    NetMessage.buffer[whoAmi].writeBuffer[5] = local_39_8;
                    int local_2_118 = local_2 + 1;
                    Buffer.BlockCopy((Array)local_77, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_118, 2);
                }
                else if (msgType == 36)
                {
                    byte[] local_3_35 = BitConverter.GetBytes(msgType);
                    byte local_39_9 = (byte)number;
                    byte local_78 = (byte)0;
                    if (Main.player[(int)local_39_9].zoneEvil)
                        local_78 = (byte)1;
                    byte local_79 = (byte)0;
                    if (Main.player[(int)local_39_9].zoneMeteor)
                        local_79 = (byte)1;
                    byte local_80 = (byte)0;
                    if (Main.player[(int)local_39_9].zoneDungeon)
                        local_80 = (byte)1;
                    byte local_81 = (byte)0;
                    if (Main.player[(int)local_39_9].zoneJungle)
                        local_81 = (byte)1;
                    local_1 += 4;
                    Buffer.BlockCopy((Array)BitConverter.GetBytes(local_1 - 4), 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 0, 4);
                    Buffer.BlockCopy((Array)local_3_35, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 4, 1);
                    NetMessage.buffer[whoAmi].writeBuffer[local_2] = local_39_9;
                    int local_2_119 = local_2 + 1;
                    NetMessage.buffer[whoAmi].writeBuffer[local_2_119] = local_78;
                    int local_2_120 = local_2_119 + 1;
                    NetMessage.buffer[whoAmi].writeBuffer[local_2_120] = local_79;
                    int local_2_121 = local_2_120 + 1;
                    NetMessage.buffer[whoAmi].writeBuffer[local_2_121] = local_80;
                    int local_2_122 = local_2_121 + 1;
                    NetMessage.buffer[whoAmi].writeBuffer[local_2_122] = local_81;
                    local_2_38 = local_2_122 + 1;
                }
                else if (msgType == 37)
                {
                    byte[] local_3_36 = BitConverter.GetBytes(msgType);
                    Buffer.BlockCopy((Array)BitConverter.GetBytes(local_1 - 4), 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 0, 4);
                    Buffer.BlockCopy((Array)local_3_36, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 4, 1);
                }
                else if (msgType == 38)
                {
                    byte[] local_3_37 = BitConverter.GetBytes(msgType);
                    byte[] local_82 = Encoding.ASCII.GetBytes(text);
                    local_1 += local_82.Length;
                    Buffer.BlockCopy((Array)BitConverter.GetBytes(local_1 - 4), 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 0, 4);
                    Buffer.BlockCopy((Array)local_3_37, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 4, 1);
                    Buffer.BlockCopy((Array)local_82, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2, local_82.Length);
                }
                else if (msgType == 39)
                {
                    byte[] local_3_38 = BitConverter.GetBytes(msgType);
                    byte[] local_57_7 = BitConverter.GetBytes((short)number);
                    local_1 += local_57_7.Length;
                    Buffer.BlockCopy((Array)BitConverter.GetBytes(local_1 - 4), 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 0, 4);
                    Buffer.BlockCopy((Array)local_3_38, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 4, 1);
                    Buffer.BlockCopy((Array)local_57_7, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2, local_57_7.Length);
                }
                else if (msgType == 40)
                {
                    byte[] local_3_39 = BitConverter.GetBytes(msgType);
                    byte local_39_10 = (byte)number;
                    byte[] local_83 = BitConverter.GetBytes((short)Main.player[(int)local_39_10].talkNPC);
                    local_1 += 1 + local_83.Length;
                    Buffer.BlockCopy((Array)BitConverter.GetBytes(local_1 - 4), 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 0, 4);
                    Buffer.BlockCopy((Array)local_3_39, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 4, 1);
                    NetMessage.buffer[whoAmi].writeBuffer[local_2] = local_39_10;
                    int local_2_123 = local_2 + 1;
                    Buffer.BlockCopy((Array)local_83, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_123, local_83.Length);
                    local_2_38 = local_2_123 + 2;
                }
                else if (msgType == 41)
                {
                    byte[] local_3_40 = BitConverter.GetBytes(msgType);
                    byte local_39_11 = (byte)number;
                    byte[] local_84 = BitConverter.GetBytes(Main.player[(int)local_39_11].itemRotation);
                    byte[] local_85 = BitConverter.GetBytes((short)Main.player[(int)local_39_11].itemAnimation);
                    local_1 += 1 + local_84.Length + local_85.Length;
                    Buffer.BlockCopy((Array)BitConverter.GetBytes(local_1 - 4), 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 0, 4);
                    Buffer.BlockCopy((Array)local_3_40, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 4, 1);
                    NetMessage.buffer[whoAmi].writeBuffer[local_2] = local_39_11;
                    int local_2_124 = local_2 + 1;
                    Buffer.BlockCopy((Array)local_84, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_124, local_84.Length);
                    int local_2_125 = local_2_124 + 4;
                    Buffer.BlockCopy((Array)local_85, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_125, local_85.Length);
                }
                else if (msgType == 42)
                {
                    byte[] local_3_41 = BitConverter.GetBytes(msgType);
                    byte local_39_12 = (byte)number;
                    byte[] local_86 = BitConverter.GetBytes((short)Main.player[(int)local_39_12].statMana);
                    byte[] local_87 = BitConverter.GetBytes((short)Main.player[(int)local_39_12].statManaMax);
                    local_1 += 1 + local_86.Length + local_87.Length;
                    Buffer.BlockCopy((Array)BitConverter.GetBytes(local_1 - 4), 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 0, 4);
                    Buffer.BlockCopy((Array)local_3_41, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 4, 1);
                    NetMessage.buffer[whoAmi].writeBuffer[5] = local_39_12;
                    int local_2_126 = local_2 + 1;
                    Buffer.BlockCopy((Array)local_86, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_126, 2);
                    int local_2_127 = local_2_126 + 2;
                    Buffer.BlockCopy((Array)local_87, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_127, 2);
                }
                else if (msgType == 43)
                {
                    byte[] local_3_42 = BitConverter.GetBytes(msgType);
                    byte local_39_13 = (byte)number;
                    byte[] local_88 = BitConverter.GetBytes((short)number2);
                    local_1 += 1 + local_88.Length;
                    Buffer.BlockCopy((Array)BitConverter.GetBytes(local_1 - 4), 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 0, 4);
                    Buffer.BlockCopy((Array)local_3_42, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 4, 1);
                    NetMessage.buffer[whoAmi].writeBuffer[5] = local_39_13;
                    int local_2_128 = local_2 + 1;
                    Buffer.BlockCopy((Array)local_88, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_128, 2);
                }
                else if (msgType == 44)
                {
                    byte[] local_3_43 = BitConverter.GetBytes(msgType);
                    byte local_39_14 = (byte)number;
                    byte local_55_2 = (byte)((double)number2 + 1.0);
                    byte[] local_69_3 = BitConverter.GetBytes((short)number3);
                    byte local_70_1 = (byte)number4;
                    local_1 += 2 + local_69_3.Length + 1;
                    Buffer.BlockCopy((Array)BitConverter.GetBytes(local_1 - 4), 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 0, 4);
                    Buffer.BlockCopy((Array)local_3_43, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 4, 1);
                    NetMessage.buffer[whoAmi].writeBuffer[local_2] = local_39_14;
                    int local_2_129 = local_2 + 1;
                    NetMessage.buffer[whoAmi].writeBuffer[local_2_129] = local_55_2;
                    int local_2_130 = local_2_129 + 1;
                    Buffer.BlockCopy((Array)local_69_3, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_130, local_69_3.Length);
                    int local_2_131 = local_2_130 + 2;
                    NetMessage.buffer[whoAmi].writeBuffer[local_2_131] = local_70_1;
                }
                else if (msgType == 45)
                {
                    byte[] local_3_44 = BitConverter.GetBytes(msgType);
                    byte local_39_15 = (byte)number;
                    byte local_89 = (byte)Main.player[(int)local_39_15].team;
                    local_1 += 2;
                    Buffer.BlockCopy((Array)BitConverter.GetBytes(local_1 - 4), 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 0, 4);
                    Buffer.BlockCopy((Array)local_3_44, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 4, 1);
                    NetMessage.buffer[whoAmi].writeBuffer[5] = local_39_15;
                    int local_2_132 = local_2 + 1;
                    NetMessage.buffer[whoAmi].writeBuffer[local_2_132] = local_89;
                }
                else if (msgType == 46)
                {
                    byte[] local_3_45 = BitConverter.GetBytes(msgType);
                    byte[] local_40_10 = BitConverter.GetBytes(number);
                    byte[] local_41_10 = BitConverter.GetBytes((int)number2);
                    local_1 += local_40_10.Length + local_41_10.Length;
                    Buffer.BlockCopy((Array)BitConverter.GetBytes(local_1 - 4), 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 0, 4);
                    Buffer.BlockCopy((Array)local_3_45, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 4, 1);
                    Buffer.BlockCopy((Array)local_40_10, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2, local_40_10.Length);
                    int local_2_133 = local_2 + 4;
                    Buffer.BlockCopy((Array)local_41_10, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_133, local_41_10.Length);
                }
                else if (msgType == 47)
                {
                    byte[] local_3_46 = BitConverter.GetBytes(msgType);
                    byte[] local_90 = BitConverter.GetBytes((short)number);
                    byte[] local_91 = BitConverter.GetBytes(Main.sign[number].x);
                    byte[] local_92 = BitConverter.GetBytes(Main.sign[number].y);
                    byte[] local_93 = Encoding.ASCII.GetBytes(Main.sign[number].text);
                    local_1 += local_90.Length + local_91.Length + local_92.Length + local_93.Length;
                    Buffer.BlockCopy((Array)BitConverter.GetBytes(local_1 - 4), 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 0, 4);
                    Buffer.BlockCopy((Array)local_3_46, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 4, 1);
                    Buffer.BlockCopy((Array)local_90, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2, local_90.Length);
                    int local_2_134 = local_2 + local_90.Length;
                    Buffer.BlockCopy((Array)local_91, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_134, local_91.Length);
                    int local_2_135 = local_2_134 + local_91.Length;
                    Buffer.BlockCopy((Array)local_92, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_135, local_92.Length);
                    int local_2_136 = local_2_135 + local_92.Length;
                    Buffer.BlockCopy((Array)local_93, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_136, local_93.Length);
                    local_2_38 = local_2_136 + local_93.Length;
                }
                else if (msgType == 48)
                {
                    byte[] local_3_47 = BitConverter.GetBytes(msgType);
                    byte[] local_40_11 = BitConverter.GetBytes(number);
                    byte[] local_41_11 = BitConverter.GetBytes((int)number2);
                    byte local_94 = Main.tile[number, (int)number2].liquid;
                    byte local_34_2 = (byte)0;
                    if (Main.tile[number, (int)number2].lava)
                        local_34_2 = (byte)1;
                    local_1 += local_40_11.Length + local_41_11.Length + 1 + 1;
                    Buffer.BlockCopy((Array)BitConverter.GetBytes(local_1 - 4), 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 0, 4);
                    Buffer.BlockCopy((Array)local_3_47, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 4, 1);
                    Buffer.BlockCopy((Array)local_40_11, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2, 4);
                    int local_2_137 = local_2 + 4;
                    Buffer.BlockCopy((Array)local_41_11, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, local_2_137, 4);
                    int local_2_138 = local_2_137 + 4;
                    NetMessage.buffer[whoAmi].writeBuffer[local_2_138] = local_94;
                    int local_2_139 = local_2_138 + 1;
                    NetMessage.buffer[whoAmi].writeBuffer[local_2_139] = local_34_2;
                    local_2_38 = local_2_139 + 1;
                }
                else if (msgType == 49)
                {
                    byte[] local_3_48 = BitConverter.GetBytes(msgType);
                    Buffer.BlockCopy((Array)BitConverter.GetBytes(local_1 - 4), 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 0, 4);
                    Buffer.BlockCopy((Array)local_3_48, 0, (Array)NetMessage.buffer[whoAmi].writeBuffer, 4, 1);

                }
                if (Main.netMode == 1)
                {
                    if (Netplay.clientSock.tcpClient.Connected)
                    {
                        try
                        {
                            ++NetMessage.buffer[whoAmi].spamCount;
                            Netplay.clientSock.networkStream.Write(NetMessage.buffer[whoAmi].writeBuffer, 0, local_1);
                        }
                        catch
                        {
                            Debug.WriteLine("    Exception normal: Tried to send data to the server after losing connection");
                        }
                    }
                }
                else if (remoteClient == -1)
                {
                    for (int local_24_2 = 0; local_24_2 < 256; ++local_24_2)
                    {
                        if (local_24_2 != ignoreClient && (NetMessage.buffer[local_24_2].broadcast || Netplay.serverSock[local_24_2].state >= 3 && msgType == 10))
                        {
                            if (Netplay.serverSock[local_24_2].tcpClient.Connected)
                            {
                                try
                                {
                                    ++NetMessage.buffer[local_24_2].spamCount;
                                    Netplay.serverSock[local_24_2].networkStream.Write(NetMessage.buffer[whoAmi].writeBuffer, 0, local_1);
                                }
                                catch
                                {
                                    Debug.WriteLine("    Exception normal: Tried to send data to a client after losing connection");
                                }
                            }
                        }
                    }
                }
                else if (Netplay.serverSock[remoteClient].tcpClient.Connected)
                {
                    try
                    {

                        ++NetMessage.buffer[remoteClient].spamCount;
                        Netplay.serverSock[remoteClient].networkStream.Write(
                            NetMessage.buffer[whoAmi].writeBuffer, 0, local_1);

                    }
                    catch
                    {
                        Debug.WriteLine("    Exception normal: Tried to send data to a client after losing connection");
                    }
                }
                if (Main.verboseNetplay)
                {
                    Debug.WriteLine("Sent:");
                    for (int local_24_3 = 0; local_24_3 < local_1; ++local_24_3)
                        Debug.Write((string)(object)NetMessage.buffer[whoAmi].writeBuffer[local_24_3] + (object)" ");
                    Debug.WriteLine("");
                    for (int local_24_4 = 0; local_24_4 < local_1; ++local_24_4)
                        Debug.Write((object)(char)NetMessage.buffer[whoAmi].writeBuffer[local_24_4]);
                    Debug.WriteLine("");
                    Debug.WriteLine("");
                }
                NetMessage.buffer[whoAmi].writeLocked = false;

                if (msgType == 19 && Main.netMode == 1)
                {
                    int local_96 = 5;
                    NetMessage.SendTileSquare(whoAmi, (int)number2, (int)number3, local_96);
                }
                if (msgType == 2 && Main.netMode == 2)
                    Netplay.serverSock[whoAmi].kill = true;
            }
        }

        public static void RecieveBytes(byte[] bytes, int streamLength, int i = 256)
        {
            lock (NetMessage.buffer[i])
            {
                try
                {
                    Buffer.BlockCopy((Array)bytes, 0, (Array)NetMessage.buffer[i].readBuffer, NetMessage.buffer[i].totalData, streamLength);
                    NetMessage.buffer[i].totalData += streamLength;
                    NetMessage.buffer[i].checkBytes = true;
                }
                catch
                {
                    if (Main.netMode == 1)
                    {
                        Debug.WriteLine("    Exception cause: Bad header lead to a read buffer overflow.");
                        Main.menuMode = 15;
                        Main.statusText = "Bad header lead to a read buffer overflow.";
                        Netplay.disconnect = true;
                    }
                    else
                    {
                        Debug.WriteLine("    Exception cause: Bad header lead to a read buffer overflow.");
                        Netplay.serverSock[i].kill = true;
                    }
                }
            }
        }

        public static void CheckBytes(int i = 256)
        {
            lock (NetMessage.buffer[i])
            {
                int local_0 = 0;
                if (NetMessage.buffer[i].totalData >= 4)
                {
                    if (NetMessage.buffer[i].messageLength == 0)
                        NetMessage.buffer[i].messageLength = BitConverter.ToInt32(NetMessage.buffer[i].readBuffer, 0) + 4;
                    for (; NetMessage.buffer[i].totalData >= NetMessage.buffer[i].messageLength + local_0 && NetMessage.buffer[i].messageLength > 0; NetMessage.buffer[i].messageLength = NetMessage.buffer[i].totalData - local_0 < 4 ? 0 : BitConverter.ToInt32(NetMessage.buffer[i].readBuffer, local_0) + 4)
                    {
                        if (!Main.ignoreErrors)
                        {
                            NetMessage.buffer[i].GetData(local_0 + 4, NetMessage.buffer[i].messageLength - 4);
                        }
                        else
                        {
                            try
                            {
                                NetMessage.buffer[i].GetData(local_0 + 4, NetMessage.buffer[i].messageLength - 4);
                            }
                            catch
                            {
                                Debug.WriteLine("Error: buffer[" + (object)i + "].GetData(" + (string)(object)(local_0 + 4) + "," + (string)(object)(NetMessage.buffer[i].messageLength - 4) + ")");
                            }
                        }
                        local_0 += NetMessage.buffer[i].messageLength;
                    }
                    if (local_0 == NetMessage.buffer[i].totalData)
                        NetMessage.buffer[i].totalData = 0;
                    else if (local_0 > 0)
                    {
                        Buffer.BlockCopy((Array)NetMessage.buffer[i].readBuffer, local_0, (Array)NetMessage.buffer[i].readBuffer, 0, NetMessage.buffer[i].totalData - local_0);
                        NetMessage.buffer[i].totalData -= local_0;
                    }
                    NetMessage.buffer[i].checkBytes = false;
                }
            }
        }

        public static void BootPlayer(int plr, string msg)
        {
            NetMessage.SendData(2, plr, -1, msg, 0, 0.0f, 0.0f, 0.0f);
        }

        public static void SendTileSquare(int whoAmi, int tileX, int tileY, int size)
        {
            int num = (size - 1) / 2;
            NetMessage.SendData(20, whoAmi, -1, "", size, (float)(tileX - num), (float)(tileY - num), 0.0f);
        }

        public static void SendSection(int whoAmi, int sectionX, int sectionY)
        {
            try
            {
                if (sectionX >= 0 && sectionY >= 0 && sectionX < Main.maxSectionsX && sectionY < Main.maxSectionsY)
                {
                    Netplay.serverSock[whoAmi].tileSection[sectionX, sectionY] = true;
                    int num1 = sectionX * 200;
                    int num2 = sectionY * 150;
                    for (int index = num2; index < num2 + 150; ++index)
                        NetMessage.SendData(10, whoAmi, -1, "", 200, (float)num1, (float)index, 0.0f);
                }
            }
            catch
            {
            }
        }

        public static void greetPlayer(int plr)
        {
            if (Main.motd == "")
                NetMessage.SendData(25, plr, -1, "Welcome to " + Main.worldName + "!", (int)byte.MaxValue, (float)byte.MaxValue, 240f, 20f);
            else
                NetMessage.SendData(25, plr, -1, Main.motd, (int)byte.MaxValue, (float)byte.MaxValue, 240f, 20f);
            string str = "";
            for (int index = 0; index < (int)byte.MaxValue; ++index)
            {
                if (Main.player[index].active)
                    str = !(str == "") ? str + ", " + Main.player[index].name : str + Main.player[index].name;
            }
            NetMessage.SendData(25, plr, -1, "Current players: " + str + ".", (int)byte.MaxValue, (float)byte.MaxValue, 240f, 20f);
        }

        public static void sendWater(int x, int y)
        {
            if (Main.netMode == 1)
            {
                NetMessage.SendData(48, -1, -1, "", x, (float)y, 0.0f, 0.0f);
            }
            else
            {
                for (int remoteClient = 0; remoteClient < 256; ++remoteClient)
                {
                    if ((NetMessage.buffer[remoteClient].broadcast || Netplay.serverSock[remoteClient].state >= 3) && Netplay.serverSock[remoteClient].tcpClient.Connected)
                    {
                        int index1 = x / 200;
                        int index2 = y / 150;
                        if (Netplay.serverSock[remoteClient].tileSection[index1, index2])
                            NetMessage.SendData(48, remoteClient, -1, "", x, (float)y, 0.0f, 0.0f);
                    }
                }
            }
        }

        public static void syncPlayers()
        {
            bool flag = false;
            for (int index1 = 0; index1 < (int)byte.MaxValue; ++index1)
            {
                int num = 0;
                if (Main.player[index1].active)
                    num = 1;
                if (Netplay.serverSock[index1].state == 10)
                {
                    if (Main.autoShutdown && !flag)
                    {

                        string str1 = Netplay.serverSock[index1].tcpClient.Client.RemoteEndPoint.ToString();
                        string str2 = str1;
                        for (int index2 = 0; index2 < str1.Length; ++index2)
                        {
                            if (str1.Substring(index2, 1) == ":")
                                str2 = str1.Substring(0, index2);
                        }
                        if (str2 == "127.0.0.1")
                            flag = true;
                    }
                    NetMessage.SendData(14, -1, index1, "", index1, (float)num, 0.0f, 0.0f);
                    NetMessage.SendData(13, -1, index1, "", index1, 0.0f, 0.0f, 0.0f);
                    NetMessage.SendData(16, -1, index1, "", index1, 0.0f, 0.0f, 0.0f);
                    NetMessage.SendData(30, -1, index1, "", index1, 0.0f, 0.0f, 0.0f);
                    NetMessage.SendData(45, -1, index1, "", index1, 0.0f, 0.0f, 0.0f);
                    NetMessage.SendData(42, -1, index1, "", index1, 0.0f, 0.0f, 0.0f);
                    NetMessage.SendData(4, -1, index1, Main.player[index1].name, index1, 0.0f, 0.0f, 0.0f);
                    for (int index2 = 0; index2 < 44; ++index2)
                        NetMessage.SendData(5, -1, index1, Main.player[index1].inventory[index2].name, index1, (float)index2, 0.0f, 0.0f);
                    NetMessage.SendData(5, -1, index1, Main.player[index1].armor[0].name, index1, 44f, 0.0f, 0.0f);
                    NetMessage.SendData(5, -1, index1, Main.player[index1].armor[1].name, index1, 45f, 0.0f, 0.0f);
                    NetMessage.SendData(5, -1, index1, Main.player[index1].armor[2].name, index1, 46f, 0.0f, 0.0f);
                    NetMessage.SendData(5, -1, index1, Main.player[index1].armor[3].name, index1, 47f, 0.0f, 0.0f);
                    NetMessage.SendData(5, -1, index1, Main.player[index1].armor[4].name, index1, 48f, 0.0f, 0.0f);
                    NetMessage.SendData(5, -1, index1, Main.player[index1].armor[5].name, index1, 49f, 0.0f, 0.0f);
                    NetMessage.SendData(5, -1, index1, Main.player[index1].armor[6].name, index1, 50f, 0.0f, 0.0f);
                    NetMessage.SendData(5, -1, index1, Main.player[index1].armor[7].name, index1, 51f, 0.0f, 0.0f);
                    NetMessage.SendData(5, -1, index1, Main.player[index1].armor[8].name, index1, 52f, 0.0f, 0.0f);
                    NetMessage.SendData(5, -1, index1, Main.player[index1].armor[9].name, index1, 53f, 0.0f, 0.0f);
                    NetMessage.SendData(5, -1, index1, Main.player[index1].armor[10].name, index1, 54f, 0.0f, 0.0f);
                    if (!Netplay.serverSock[index1].announced)
                    {
                        Netplay.serverSock[index1].announced = true;
                        NetMessage.SendData(25, -1, index1, Main.player[index1].name + " has joined.", (int)byte.MaxValue, (float)byte.MaxValue, 240f, 20f);
                        if (Main.dedServ)
                            Console.WriteLine(Main.player[index1].name + " has joined.");
                    }
                }
                else
                {
                    NetMessage.SendData(14, -1, index1, "", index1, (float)num, 0.0f, 0.0f);
                    if (Netplay.serverSock[index1].announced)
                    {
                        Netplay.serverSock[index1].announced = false;
                        NetMessage.SendData(25, -1, index1, Netplay.serverSock[index1].oldName + " has left.", (int)byte.MaxValue, (float)byte.MaxValue, 240f, 20f);
                        if (Main.dedServ)
                            Console.WriteLine(Netplay.serverSock[index1].oldName + " has left.");
                    }
                }
            }
            if (Main.autoShutdown && !flag)
            {
                WorldGen.saveWorld(false);
                Netplay.disconnect = true;
            }
        }
    }
}
