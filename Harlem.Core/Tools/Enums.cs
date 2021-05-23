using System;
using System.Collections.Generic;
using System.Text;

namespace Harlem.Core.Tools
{
   public class Enums
    {
        public enum StockType:ushort
        {
            None=0,
            Adet=1,
            Kilogram=2,
            Paket
        }
        public enum BLLResultType:ushort
        {
            Success=1,
            Error=2,
            Empty=3

        }
    }
}
