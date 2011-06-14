namespace Terraria
{
    using System;

    public class Tile
    {
        public bool active = false;
        public bool checkingLiquid = false;
        public byte frameNumber;
        public short frameX;
        public short frameY;
        public bool lava = false;
        public bool lighted = false;
        public byte liquid;
        public bool skipLiquid = false;
        public byte type;
        public byte wall;
        public byte wallFrameNumber;
        public byte wallFrameX;
        public byte wallFrameY;
    }
}

