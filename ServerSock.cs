namespace Terraria
{
    using System;
    using System.Diagnostics;
    using System.Net.Sockets;

    public class ServerSock
    {
        public bool active = false;
        public bool announced = false;
        public Socket clientSocket;
        public bool kill = false;
        public bool locked = false;
        public string name = "Anonymous";
        public NetworkStream networkStream;
        public string oldName = "";
        public byte[] readBuffer;
        public float spamAddBlock = 0f;
        public float spamAddBlockMax = 60f;
        public bool spamCheck = true;
        public float spamDelBlock = 0f;
        public float spamDelBlockMax = 400f;
        public float spamProjectile = 0f;
        public float spamProjectileMax = 60f;
        public float spamWater = 0f;
        public float spamWaterMax = 20f;
        public int state = 0;
        public int statusCount;
        public int statusMax;
        public string statusText = "";
        public string statusText2;
        public TcpClient tcpClient = new TcpClient();
        public bool[,] tileSection = new bool[Main.maxTilesX / 200, Main.maxTilesY / 150];
        public int timeOut = 0;
        public int whoAmI = 0;
        public byte[] writeBuffer;

        public void Reset()
        {
            for (int i = 0; i < Main.maxSectionsX; i++)
            {
                for (int j = 0; j < Main.maxSectionsY; j++)
                {
                    this.tileSection[i, j] = false;
                }
            }
            if (this.whoAmI < 0xff)
            {
                Main.player[this.whoAmI] = new Player();
            }
            this.timeOut = 0;
            this.statusCount = 0;
            this.statusMax = 0;
            this.statusText2 = "";
            this.statusText = "";
            this.name = "Anonymous";
            this.state = 0;
            this.locked = false;
            this.kill = false;
            this.SpamClear();
            this.active = false;
            NetMessage.buffer[this.whoAmI].Reset();
            if (this.networkStream != null)
            {
                this.networkStream.Close();
            }
            if (this.tcpClient != null)
            {
                this.tcpClient.Close();
            }
        }

        public void ServerReadCallBack(IAsyncResult ar)
        {
            int streamLength = 0;
            if (!Netplay.disconnect)
            {
                try
                {
                    streamLength = this.networkStream.EndRead(ar);
                }
                catch
                {
                }
                if (streamLength == 0)
                {
                    this.kill = true;
                }
                else if (Main.ignoreErrors)
                {
                    try
                    {
                        NetMessage.RecieveBytes(this.readBuffer, streamLength, this.whoAmI);
                    }
                    catch
                    {
                        Debug.WriteLine(string.Concat(new object[] { "Error: NetMessage.RecieveBytes(", this.readBuffer, ", ", streamLength, ")" }));
                    }
                }
                else
                {
                    NetMessage.RecieveBytes(this.readBuffer, streamLength, this.whoAmI);
                }
            }
            this.locked = false;
        }

        public void ServerWriteCallBack(IAsyncResult ar)
        {
            messageBuffer buffer1 = NetMessage.buffer[this.whoAmI];
            buffer1.spamCount--;
            if (this.statusMax > 0)
            {
                this.statusCount++;
            }
        }

        public void SpamClear()
        {
            this.spamProjectile = 0f;
            this.spamAddBlock = 0f;
            this.spamDelBlock = 0f;
        }

        public void SpamUpdate()
        {
            if (!this.spamCheck)
            {
                this.spamProjectile = 0f;
                this.spamDelBlock = 0f;
                this.spamAddBlock = 0f;
                this.spamWater = 0f;
            }
            else
            {
                if (this.spamProjectile > this.spamProjectileMax)
                {
                    NetMessage.BootPlayer(this.whoAmI, "Cheating attempt detected: Projectile spam");
                }
                if (this.spamAddBlock > this.spamAddBlockMax)
                {
                    NetMessage.BootPlayer(this.whoAmI, "Cheating attempt detected: Add tile spam");
                }
                if (this.spamDelBlock > this.spamDelBlockMax)
                {
                    NetMessage.BootPlayer(this.whoAmI, "Cheating attempt detected: Remove tile spam");
                }
                if (this.spamWater > this.spamWaterMax)
                {
                    NetMessage.BootPlayer(this.whoAmI, "Cheating attempt detected: Liquid spam");
                }
                this.spamProjectile -= 0.2f;
                if (this.spamProjectile < 0f)
                {
                    this.spamProjectile = 0f;
                }
                this.spamAddBlock -= 0.1f;
                if (this.spamAddBlock < 0f)
                {
                    this.spamAddBlock = 0f;
                }
                this.spamDelBlock -= 2f;
                if (this.spamDelBlock < 0f)
                {
                    this.spamDelBlock = 0f;
                }
                this.spamWater -= 0.1f;
                if (this.spamWater < 0f)
                {
                    this.spamWater = 0f;
                }
            }
        }
    }
}

