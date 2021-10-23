using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Lever
{
    class Nml
    {
        /// <summary>
        /// 通常
        /// </summary>
        public static void Flow()
        {
            DdmVariable.FreezeFlg = false;
            DdmVariable.RushStartFlg = false;
            DdmVariable.NmlGame++;

            if (DdmVariable.ZenchoGame == 0)
            {
                if(DdmVariable.NmlGame == 999)
                {
                    DdmVariable.DdmMode = DDMMODE.BnsWait;
                }
                else if(Function.Lot.L1() == HIT_NOT.Hit)
                {
                    if(FrtData.ResultFrtItem.lowName == "SBIGリプレイ")
                    {
                        Function.Common.BonusGameSetting();     // ボーナスG数設定処理
                        DdmVariable.AtMode = RUSH_MODE._90;
                        DdmVariable.DdmMode = DDMMODE.AtBns;
                        DdmVariable.FreezeType = FREEZE_TYPE.LongFreeze;
                        DdmVariable.FreezeFlg = true;
                    }
                    else
                    {
                        DdmVariable.ZenchoGame = (Int16)Function.Lot.L2();
                        if(DdmVariable.ZenchoGame == 0)
                        {
                            DdmVariable.DdmMode = DDMMODE.BnsWait;
                        }
                    }
                }
            }
            else
            {
                DdmVariable.ZenchoGame--;
                if(Function.Lot.L3() == HIT_NOT.Hit)
                {
                    DdmVariable.Stock++;
                }

                if (DdmVariable.ZenchoGame == 0)
                {
                    DdmVariable.DdmMode = DDMMODE.BnsWait;
                }
            }
        }
    }
}
