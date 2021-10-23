using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Function
{
    class Common
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Common()
        {
        }

        /// <summary>
        /// リセット処理
        /// </summary>
        public static void Reset()
        {

        }

        /// <summary>
        /// ボーナスG数設定処理
        /// </summary>
        public static void BonusGameSetting()
        {
            if(FrtData.ResultFrtItem.lowName == "REGリプレイ")
            {
                DdmVariable.BnsGame = 10;
            }
            else
            {
                DdmVariable.BnsGame = 30;
            }
        }
    }
}
