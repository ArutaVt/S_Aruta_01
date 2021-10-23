using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Lever
{
    class BnsWait
    {
        public static void Flow()
        {
            if(Function.Lot.L3() == HIT_NOT.Hit)
            {
                DdmVariable.Stock++;
            }

            if((FrtData.ResultFrtItem.lowName == "REGリプレイ") || (FrtData.ResultFrtItem.lowName == "BIGリプレイ"))
            {
                if (DdmVariable.Stock > 0)
                {
                    DdmVariable.Stock--;
                    DdmVariable.DdmMode = DDMMODE.AtBns;
                }
                else
                {
                    DdmVariable.DdmMode = DDMMODE.Bns;
                }
                Function.Common.BonusGameSetting();
            }
            else if(FrtData.ResultFrtItem.lowName == "SBIGリプレイ")
            {
                DdmVariable.Stock++;
                DdmVariable.DdmMode = DDMMODE.AtBns;
                Function.Common.BonusGameSetting();
            }
        }
    }
}
