using System;

namespace MetastockWrapper.MSFL
{
    public class MsflPriceRecord
    {
        public DateTime Date { get; set; }

        public float Open { get; set; }

        public float High { get; set; }

        public float Low { get; set; }

        public float Close { get; set; }

        public float Volume { get; set; }

        public float OpenInt { get; set; }

        public ushort DataAvailable { get; set; }
    }
}
