using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Sim
{
    public class AllLever
    {
        private static System.Random random = new System.Random();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public AllLever()
        {
        }

        public static void Reset()
        {
            FrtData.ResultFrtItem = new FrtItem() { hitType = HitType.frt, pay = 0 }; // ハズレにしておく

            Function.Common.Reset();     // 初期リセット
        }

        public static void Flow()
        {
            MedalInFunc();      // メダル投入処理
            DdmMain.frtData.Lot();      // 条件装置抽せん
            DdmVariable.FrtCode = (FRT_CODE)FrtData.ResultFrtItem.lowResult;
            if ((DdmVariable.StartDdmMode != DDMMODE.Nml) && (DdmVariable.DdmMode == DDMMODE.Nml)) DdmVariable.NmlGame = 0; // 通常1G目は初期化
            DdmVariable.StartDdmMode = DdmVariable.DdmMode;
            Data.Totalization.ddmModeDatas[(int)DdmVariable.DdmMode].Game++;         //集計
            DdmVariable.YuuriGame++;        // 有利区間G数をカウント
            DdmVariable.FreezeType = FREEZE_TYPE.None;

            switch (DdmVariable.DdmMode)
            {
                case DDMMODE.Nml:
                    Lever.Nml.Flow();
                    break;
                case DDMMODE.BnsWait:
                    Lever.BnsWait.Flow();
                    break;
                case DDMMODE.Bns:
                    Lever.Bns.Flow();
                    break;
                case DDMMODE.Rush:
                    Lever.Rush.Flow();
                    break;
                case DDMMODE.AtBns:
                    Lever.AtBns.Flow();
                    break;
                default:
                    break;
            }

#if !UNITY_STANDALONE
            MedalOutFunc();     // メダル払い出し処理
#endif

            // 出玉状態集計
            if(DdmVariable.StartDdmMode != DdmVariable.DdmMode)
            {
                Data.Totalization.SyukeiDdmMove(DdmVariable.StartDdmMode, DdmVariable.DdmMode);
            }

        }

        /// <summary>
        /// メダル投入処理
        /// </summary>
        private static void MedalInFunc()
        {
#if !UNITY_STANDALONE
            // 抽せん前に結果を参照してメダル処理
            if(FrtData.ResultFrtItem.hitType != HitType.replay)
            {
                DdmVariable.In += 3;
                Data.Totalization.ddmModeDatas[(int)DdmVariable.DdmMode].In += 3; 
            }
#endif
        }

        /// <summary>
        /// メダル払い出し処理
        /// </summary>
        private static void MedalOutFunc()
        {
            DdmMain.frtData.PbLot();    // 停止抽せん

            switch (DdmVariable.StartDdmMode)
            {
                case DDMMODE.Bns:
                case DDMMODE.Rush:
                case DDMMODE.AtBns:
                    if(FrtData.ResultFrtItem.lowName.IndexOf("押し順ベル") != -1)
                    {
                        // 押し順ベルの場合
                        FrtData.ResultFrtItem.pay = 11;
                        FrtData.ResultFrtItem.StopName = "ベル";
                    }
                    break;
                default:
                    break;
            }

            Data.Totalization.ddmModeDatas[(int)DdmVariable.StartDdmMode].Out += (UInt64)FrtData.ResultFrtItem.pay;
            DdmVariable.Out += (UInt64)FrtData.ResultFrtItem.pay;
            // DdmVariable.Pay = FrtData.ResultFrtItem.pay;
        }

    }


}
