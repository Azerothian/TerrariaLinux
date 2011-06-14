namespace Terraria
{
    using System;
    using System.Diagnostics;
    using System.Net.Sockets;

    public class ClientSock
    {
        public bool active = false;
        public bool locked = false;
        public NetworkStream networkStream;
        public byte[] readBuffer;
        public int state = 0;
        public int statusCount;
        public int statusMax;
        public string statusText;
        public TcpClient tcpClient = new TcpClient();
        public int timeOut = 0;
        public byte[] writeBuffer;

        public void ClientReadCallBack(IAsyncResult ar)
        {
            int streamLength = 0;
            if (!Netplay.disconnect)
            {
                streamLength = this.networkStream.EndRead(ar);
                if (streamLength == 0)
                {
                    Netplay.disconnect = true;
                    Main.statusText = "Lost connection";
                }
                else if (Main.ignoreErrors)
                {
                    try
                    {
                        NetMessage.RecieveBytes(this.readBuffer, streamLength, 0x100);
                    }
                    catch
                    {
                        Debug.WriteLine(string.Concat(new object[] { "Error: NetMessage.RecieveBytes(", this.readBuffer, ", ", streamLength, ")" }));
                    }
                }
                else
                {
                    NetMessage.RecieveBytes(this.readBuffer, streamLength, 0x100);
                }
            }
            this.locked = false;
        }

        public void ClientWriteCallBack(IAsyncResult ar)
        {
            messageBuffer buffer1 = NetMessage.buffer[0x100];
            buffer1.spamCount--;
        }
    }
}

