using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Lever
{
    class AtBns
    {
        public static void Flow()
        {
            DdmVariable.RushStartFlg = false;

            if (Function.Lot.L4() == HIT_NOT.Hit)
            {
                DdmVariable.Stock++;
            }

            DdmVariable.BnsGame--;

            if (DdmVariable.BnsGame == 0)
            {
                DdmVariable.DdmMode = DDMMODE.Rush;
                DdmVariable.RushGame = 7;
            }
        }
    }
}
