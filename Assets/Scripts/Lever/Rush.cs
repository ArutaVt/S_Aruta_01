using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Lever
{
    class Rush
    {
        public static void Flow()
        {
            if(DdmVariable.RushGame == 7)
            {
                if(DdmVariable.Stock > 0)
                {
                    DdmVariable.Stock--;
                    DdmVariable.AtSts = RUSH_STS.ReStart;
                }
                else
                {
                    DdmVariable.AtSts = Function.Lot.L5();
                }

                if(DdmVariable.RushStartFlg == false)
                {
                    DdmVariable.RushStartFlg = true;
                    DdmVariable.FreezeType = FREEZE_TYPE.RushStart;
                }
            }

            DdmVariable.RushGame--;

            if(DdmVariable.AtSts != RUSH_STS.End)
            {
                if(Function.Lot.L7() == HIT_NOT.Hit)
                {
                    DdmVariable.Stock++;
                }
            }
            else
            {
                DdmVariable.AtSts = Function.Lot.L6();
            }

            if (DdmVariable.RushGame == 0)
            {
                switch (DdmVariable.AtSts)
                {
                    case RUSH_STS.End:
                        DdmVariable.DdmMode = DDMMODE.Nml;
                        break;
                    case RUSH_STS.ReStart:
                        DdmVariable.DdmMode = DDMMODE.Rush;
                        DdmVariable.RushGame = 7;
                        break;
                    case RUSH_STS.Bonus:
                        DdmVariable.Stock++;
                        DdmVariable.DdmMode = DDMMODE.BnsWait;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
