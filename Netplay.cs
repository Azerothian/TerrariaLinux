namespace Terraria
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading;

    public class Netplay
    {
        public static string banFile = "banlist.txt";
        public const int bufferSize = 0x400;
        public static ClientSock clientSock = new ClientSock();
        public static bool disconnect = false;
        public const int maxConnections = 0x100;
        public static string password = "";
        public static IPAddress serverIP;
        public static IPAddress serverListenIP;
        public static int serverPort = 0x1e61;
        public static ServerSock[] serverSock = new ServerSock[0x100];
        public static bool ServerUp = false;
        public static bool stopListen = false;
        public static TcpListener tcpListener;

        public static void AddBan(int plr)
        {
            string str = serverSock[plr].tcpClient.Client.RemoteEndPoint.ToString();
            string str2 = str;
            for (int i = 0; i < str.Length; i++)
            {
                if (str.Substring(i, 1) == ":")
                {
                    str2 = str.Substring(0, i);
                }
            }
            using (StreamWriter writer = new StreamWriter(banFile, true))
            {
                writer.WriteLine("//" + Main.player[plr].name);
                writer.WriteLine(str2);
            }
        }

        public static bool CheckBan(string ip)
        {
            try
            {
                string str = ip;
                for (int i = 0; i < ip.Length; i++)
                {
                    if (ip.Substring(i, 1) == ":")
                    {
                        str = ip.Substring(0, i);
                    }
                }
                if (System.IO.File.Exists(banFile))
                {
                    using (StreamReader reader = new StreamReader(banFile))
                    {
                        string str2;
                        while ((str2 = reader.ReadLine()) != null)
                        {
                            if (str2 == str)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            catch
            {
            }
            return false;
        }

        public static void ClientLoop(object threadContext)
        {
            if (Main.rand == null)
            {
                Main.rand = new Random((int) DateTime.Now.Ticks);
            }
            if (WorldGen.genRand == null)
            {
                WorldGen.genRand = new Random((int) DateTime.Now.Ticks);
            }
            Main.player[Main.myPlayer].hostile = false;
            Main.clientPlayer = (Player) Main.player[Main.myPlayer].clientClone();
            Main.menuMode = 10;
            Main.menuMode = 14;
            if (!Main.autoPass)
            {
                Main.statusText = "Connecting to " + serverIP;
            }
            Main.netMode = 1;
            disconnect = false;
            clientSock = new ClientSock();
            clientSock.tcpClient.NoDelay = true;
            clientSock.readBuffer = new byte[0x400];
            clientSock.writeBuffer = new byte[0x400];
            bool flag = true;
            while (flag)
            {
                flag = false;
                try
                {
                    clientSock.tcpClient.Connect(serverIP, serverPort);
                    clientSock.networkStream = clientSock.tcpClient.GetStream();
                    flag = false;
                }
                catch
                {
                    if (!(!disconnect && Main.gameMenu))
                    {
                        Debug.WriteLine("   Exception normal: Player hit cancel before initiating a connection.");
                    }
                    else
                    {
                        flag = true;
                    }
                }
            }
            NetMessage.buffer[0x100].Reset();
            for (int i = -1; !disconnect; i = clientSock.state)
            {
                if (clientSock.tcpClient.Connected)
                {
                    if (NetMessage.buffer[0x100].checkBytes)
                    {
                        NetMessage.CheckBytes(0x100);
                    }
                    clientSock.active = true;
                    if (clientSock.state == 0)
                    {
                        Main.statusText = "Found server";
                        clientSock.state = 1;
                        NetMessage.SendData(1, -1, -1, "", 0, 0f, 0f, 0f);
                    }
                    if ((clientSock.state == 2) && (i != clientSock.state))
                    {
                        Main.statusText = "Sending player data...";
                    }
                    if ((clientSock.state == 3) && (i != clientSock.state))
                    {
                        Main.statusText = "Requesting world information";
                    }
                    if (clientSock.state == 4)
                    {
                        WorldGen.worldCleared = false;
                        clientSock.state = 5;
                        WorldGen.clearWorld();
                    }
                    if ((clientSock.state == 5) && WorldGen.worldCleared)
                    {
                        clientSock.state = 6;
                        Main.player[Main.myPlayer].FindSpawn();
                        NetMessage.SendData(8, -1, -1, "", Main.player[Main.myPlayer].SpawnX, (float) Main.player[Main.myPlayer].SpawnY, 0f, 0f);
                    }
                    if ((clientSock.state == 6) && (i != clientSock.state))
                    {
                        Main.statusText = "Requesting tile data";
                    }
                    if (!clientSock.locked && !(disconnect || !clientSock.networkStream.DataAvailable))
                    {
                        clientSock.locked = true;
                        clientSock.networkStream.BeginRead(clientSock.readBuffer, 0, clientSock.readBuffer.Length, new AsyncCallback(clientSock.ClientReadCallBack), clientSock.networkStream);
                    }
                    if ((clientSock.statusMax > 0) && (clientSock.statusText != ""))
                    {
                        if (clientSock.statusCount >= clientSock.statusMax)
                        {
                            Main.statusText = clientSock.statusText + ": Complete!";
                            clientSock.statusText = "";
                            clientSock.statusMax = 0;
                            clientSock.statusCount = 0;
                        }
                        else
                        {
                            Main.statusText = string.Concat(new object[] { clientSock.statusText, ": ", (int) ((((float) clientSock.statusCount) / ((float) clientSock.statusMax)) * 100f), "%" });
                        }
                    }
                    Thread.Sleep(1);
                }
                else if (clientSock.active)
                {
                    Main.statusText = "Lost connection";
                    disconnect = true;
                }
            }
            try
            {
                clientSock.networkStream.Close();
                clientSock.networkStream = clientSock.tcpClient.GetStream();
            }
            catch
            {
                Debug.WriteLine("   Exception normal: Redundant closing of the TCP Client and/or Network Stream.");
            }
            if (!Main.gameMenu)
            {
                Main.netMode = 0;
                Player.SavePlayer(Main.player[Main.myPlayer], Main.playerPathName);
                Main.gameMenu = true;
                Main.menuMode = 14;
            }
            NetMessage.buffer[0x100].Reset();
            if ((Main.menuMode == 15) && (Main.statusText == "Lost connection"))
            {
                Main.menuMode = 14;
            }
            if ((clientSock.statusText != "") && (clientSock.statusText != null))
            {
                Main.statusText = "Lost connection";
            }
            clientSock.statusCount = 0;
            clientSock.statusMax = 0;
            clientSock.statusText = "";
            Main.netMode = 0;
        }

        public static int GetSectionX(int x)
        {
            return (x / 200);
        }

        public static int GetSectionY(int y)
        {
            return (y / 150);
        }

        public static void Init()
        {
            for (int i = 0; i < 0x101; i++)
            {
                if (i < 0x100)
                {
                    serverSock[i] = new ServerSock();
                    serverSock[i].tcpClient.NoDelay = true;
                }
                NetMessage.buffer[i] = new messageBuffer();
                NetMessage.buffer[i].whoAmI = i;
            }
            clientSock.tcpClient.NoDelay = true;
        }

        public static void ListenForClients(object threadContext)
        {
            while (!disconnect && !stopListen)
            {
                int index = -1;
                for (int i = 0; i < Main.maxNetPlayers; i++)
                {
                    if (!serverSock[i].tcpClient.Connected)
                    {
                        index = i;
                        break;
                    }
                }
                if (index >= 0)
                {
                    try
                    {
                        serverSock[index].tcpClient = tcpListener.AcceptTcpClient();
                        serverSock[index].tcpClient.NoDelay = true;
                        Console.WriteLine(serverSock[index].tcpClient.Client.RemoteEndPoint + " is connecting...");
                        Console.WriteLine("passed");
                    }
                    catch (Exception exception)
                    {
                        if (!disconnect)
                        {
                            Main.menuMode = 15;
                            Main.statusText = exception.ToString();
                            disconnect = true;
                        }
                        else
                        {
                            Debug.WriteLine("   Exception normal: Server shut down.");
                        }
                    }
                }
                else
                {
                    stopListen = true;
                    tcpListener.Stop();
                }
            }
        }

        public static void ServerLoop(object threadContext)
        {
            if (Main.rand == null)
            {
                Main.rand = new Random((int) DateTime.Now.Ticks);
            }
            if (WorldGen.genRand == null)
            {
                WorldGen.genRand = new Random((int) DateTime.Now.Ticks);
            }
            Main.myPlayer = 0xff;
            serverIP = IPAddress.Any;
            serverListenIP = serverIP;
            Main.menuMode = 14;
            Main.statusText = "Starting server...";
            Main.netMode = 2;
            disconnect = false;
            int index = 0;
            while (index < 0x100)
            {
                serverSock[index] = new ServerSock();
                serverSock[index].Reset();
                serverSock[index].whoAmI = index;
                serverSock[index].tcpClient = new TcpClient();
                serverSock[index].tcpClient.NoDelay = true;
                serverSock[index].readBuffer = new byte[0x400];
                serverSock[index].writeBuffer = new byte[0x400];
                index++;
            }
            tcpListener = new TcpListener(serverListenIP, serverPort);
            try
            {
                tcpListener.Start();
            }
            catch (Exception exception)
            {
                Main.menuMode = 15;
                Main.statusText = exception.ToString();
                disconnect = true;
                Debug.WriteLine("   Exception normal: Tried to run two servers on the same PC");
            }
            if (!disconnect)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(Netplay.ListenForClients), 1);
                Main.statusText = "Server started";
            }
            int num2 = 0;
            while (!disconnect)
            {
                if (stopListen)
                {
                    int num3 = -1;
                    index = 0;
                    while (index < 0xff)
                    {
                        if (!serverSock[index].tcpClient.Connected)
                        {
                            num3 = index;
                            break;
                        }
                        index++;
                    }
                    if (num3 >= 0)
                    {
                        tcpListener.Start();
                        stopListen = false;
                        ThreadPool.QueueUserWorkItem(new WaitCallback(Netplay.ListenForClients), 1);
                    }
                }
                int num4 = 0;
                index = 0;
                while (index < 0x100)
                {
                    if (NetMessage.buffer[index].checkBytes)
                    {
                        NetMessage.CheckBytes(index);
                    }
                    if (serverSock[index].kill)
                    {
                        serverSock[index].Reset();
                        NetMessage.syncPlayers();
                    }
                    else if (serverSock[index].tcpClient.Connected)
                    {
                        if (!serverSock[index].active)
                        {
                            serverSock[index].state = 0;
                        }
                        serverSock[index].active = true;
                        num4++;
                        if (!serverSock[index].locked)
                        {
                            try
                            {
                                serverSock[index].networkStream = serverSock[index].tcpClient.GetStream();
                                if (serverSock[index].networkStream.DataAvailable)
                                {
                                    serverSock[index].locked = true;
                                    serverSock[index].networkStream.BeginRead(serverSock[index].readBuffer, 0, serverSock[index].readBuffer.Length, new AsyncCallback(serverSock[index].ServerReadCallBack), serverSock[index].networkStream);
                                }
                            }
                            catch
                            {
                                Debug.WriteLine("   Exception normal: Tried to get data from a client after losing connection");
                                serverSock[index].kill = true;
                            }
                        }
                        if ((serverSock[index].statusMax > 0) && (serverSock[index].statusText2 != ""))
                        {
                            if (serverSock[index].statusCount >= serverSock[index].statusMax && serverSock[index].tcpClient.Client.Connected)
                            {

                                string status = string.Concat(new object[] { "(", serverSock[index].tcpClient.Client.RemoteEndPoint, ") ", serverSock[index].name, " ", serverSock[index].statusText2, ": Complete!" });
                               // Trace.WriteLine("Test1: {0}", status); //TODO: Remove the RemoteEndPoint
                                serverSock[index].statusText = status;
                                serverSock[index].statusText2 = "";
                                serverSock[index].statusMax = 0;
                                serverSock[index].statusCount = 0;
                            }
                            else
                            {
                                var sock = serverSock[index];
                                string status = string.Concat(new object[] { "(", sock.tcpClient.Client.RemoteEndPoint, ") ", sock.name, " ", sock.statusText2, ": ", (int)((((float)sock.statusCount) / ((float)sock.statusMax)) * 100f), "%" });
                              //  Trace.WriteLine("Test2: {0}", status); //TODO: Remove the RemoteEndPoint
                                sock.statusText = status;
                            }
                        }
                        else if (serverSock[index].state == 0)
                        {
                            string status = string.Concat(new object[] { "(", serverSock[index].tcpClient.Client.RemoteEndPoint, ") ", serverSock[index].name, " is connecting..." });
                            //Trace.WriteLine("Test3: {0}", status); //TODO: Remove the RemoteEndPoint
                            serverSock[index].statusText = status;
                        }
                        else if (serverSock[index].state == 1)
                        {
                            string status = string.Concat(new object[] { "(", serverSock[index].tcpClient.Client.RemoteEndPoint, ") ", serverSock[index].name, " is sending player data..." });
                            //Trace.WriteLine("Test4: {0}", status); //TODO: Remove the RemoteEndPoint
                            serverSock[index].statusText = status;
                        }
                        else if (serverSock[index].state == 2)
                        {
                            string status = string.Concat(new object[] { "(", serverSock[index].tcpClient.Client.RemoteEndPoint, ") ", serverSock[index].name, " requested world information" });
                            //Trace.WriteLine("Test5: {0}", status); //TODO: Remove the RemoteEndPoint
                            serverSock[index].statusText = status;
                        }
                        else if ((serverSock[index].state != 3) && (serverSock[index].state == 10))
                        {

                            string status = string.Concat(new object[] { "(", serverSock[index].tcpClient.Client.RemoteEndPoint, ") ", serverSock[index].name, " is playing" });
                           // Trace.WriteLine("Test6: {0}", status); //TODO: Remove the RemoteEndPoint
                            serverSock[index].statusText = status;
                        }
                    }
                    else if (serverSock[index].active)
                    {
                        serverSock[index].kill = true;
                    }
                    else
                    {
                        serverSock[index].statusText2 = "";
                        if (index < 0xff)
                        {
                            Main.player[index].active = false;
                        }
                    }
                    index++;
                }
                num2++;
                if (num2 > 10)
                {
                    Thread.Sleep(1);
                    num2 = 0;
                }
                else
                {
                    Thread.Sleep(0);
                }
                if (!WorldGen.saveLock && !Main.dedServ)
                {
                    if (num4 == 0)
                    {
                        Main.statusText = "Waiting for clients...";
                    }
                    else
                    {
                        Main.statusText = num4 + " clients connected";
                    }
                }
                ServerUp = true;
            }
            tcpListener.Stop();
            for (index = 0; index < 0x100; index++)
            {
                serverSock[index].Reset();
            }
            if (Main.menuMode != 15)
            {
                Main.netMode = 0;
                Main.menuMode = 10;
                WorldGen.saveWorld(false);
                while (WorldGen.saveLock)
                {
                }
                Main.menuMode = 0;
            }
            else
            {
                Main.netMode = 0;
            }
            Main.myPlayer = 0;
        }

        public static bool SetIP(string newIP)
        {
            try
            {
                serverIP = IPAddress.Parse(newIP);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static bool SetIP2(string newIP)
        {
            try
            {
                IPAddress[] addressList = Dns.GetHostEntry(newIP).AddressList;
                for (int i = 0; i < addressList.Length; i++)
                {
                    if (addressList[i].AddressFamily == AddressFamily.InterNetwork)
                    {
                        serverIP = addressList[i];
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public static void StartClient()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(Netplay.ClientLoop), 1);
        }

        public static void StartServer()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(Netplay.ServerLoop), 1);
        }
    }
}

