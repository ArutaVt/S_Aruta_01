using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Lever
{
    class Bns
    {
        public static void Flow()
        {
            if(Function.Lot.L4() == HIT_NOT.Hit)
            {
                DdmVariable.AtMode = Function.Lot.L9();
                DdmVariable.DdmMode = DDMMODE.AtBns;
            }

            DdmVariable.BnsGame--;

            if(DdmVariable.BnsGame == 0)
            {
                DdmVariable.DdmMode = DDMMODE.Nml;
            }
        }
    }
}
