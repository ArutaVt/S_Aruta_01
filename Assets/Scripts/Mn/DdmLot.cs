using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATypeSIM.Models
{
    public class DdmLot
    {
        #region ResultEnum

        public enum N1
        {
            NmlA,
            NmlB,
            NmlC,
            RenA,
            RenB,
            RenC,
            Ret,
        }

        public enum N2
        {
            NmlA,
            NmlB,
            NmlC,
            RenA,
            RenB,
            RenC,
            Ret,
        }
        public enum N3
        {
            NmlA,
            NmlB,
            NmlC,
            RenA,
            RenB,
            RenC,
            Ret,
        }

        public enum N4
        {
            _0G,
            _100G,
            _200G,
            _300G,
            _400G,
            _500G,
            _600G,
            _700G,
            _800G,
            _900G,
        }

        public enum N5
        {
            _0G,
            _10G,
            _20G,
            _30G,
            _40G,
            _50G,
            _60G,
            _70G,
            _80G,
            _90G,
        }

        public enum N6
        {
            _0G,
            _1G,
            _2G,
            _3G,
            _4G,
            _5G,
            _6G,
            _7G,
            _8G,
            _9G,
        }

        public enum N7
        {
            Low,
            Nml,
            High,
            HevenShort,
            Heven,
        }

        public enum N8
        {
            Hazure,
            ABIG,
            SBIG,
            REG,
        }

        public enum N9
        {
            _0,
            _1,
            _2,
            _3,
            _7,
            _10,
            _13,
            _15,
            _128,
        }

        public enum N10
        {
            Hazure,
            ABIG,
            SBIG,
            REG,
        }

        public enum N11
        {
            Hazure,
            ABIG,
            SBIG,
            REG,
        }

        public enum N12
        {
            _1,
            _2,
            _3,
            _4,
            _5,
        }

        #endregion

        #region DataEnum
        public enum GHitMode { NmlA, NmlB, NmlC, RenA, RenB, RenC, Ret };
        public enum NmlMode { Low, Nml, High, HevenShort, Heven };

        public enum HitType { RareHit, LimitHit, StockHit };

        public GHitMode gHitMode = GHitMode.NmlA;       // G数解除モード
        public NmlMode nmlMode = NmlMode.Low;           // 通常モード
        public int limitGame = 0;                       // 天井ゲーム数
        public int highGame = 0;                        // 高確率G数
        public int bnsStock = 0;                        // ボーナスストック
        public int renNum = 0;                          // 連回数
        public AutoMakeCode.Enum.Settei settei = AutoMakeCode.Enum.Settei._1;               // 設定
        public AutoMakeCode.Enum.FrtCode frtCode = AutoMakeCode.Enum.FrtCode.Hazure;        // 条件装置
        public AutoMakeCode.Enum.FrtCode frtCode_old = AutoMakeCode.Enum.FrtCode.Hazure;    // 条件装置
        public AutoMakeCode.Enum.BnsCode bnsCode = AutoMakeCode.Enum.BnsCode.Hazure;        // 特賞
        public int nmlGame = 0;
        public HitType hitType = HitType.RareHit;

        #endregion

        /// <summary>
        /// 抽せんデータ
        /// </summary>
        List<LotData> lotDatas;

        /// <summary>
        /// 乱数データ
        /// </summary>
        Random random = new Random();

        public string lotlog;

        public DdmLot(string path)
        {
            //using (StreamReader sr = new StreamReader(path, System.Text.Encoding.UTF8))
            //{
            //    string json = sr.ReadToEnd();
            //    lotDatas = JsonConvert.DeserializeObject<List<LotData>>(json);
            //}

            lotDatas = JsonConvert.DeserializeObject<List<LotData>>(path);

            LotN1();
            LotN4();
            LotN5();
            LotN6();
        }

        private int lot(int _no, int _table_X_Index, int _table_Y_Index, int _box_X_Index)
        {
            LotData recode = lotDatas.Find((x) => x.no == _no && x.table_X_Index == _table_X_Index && x.table_Y_Index == _table_Y_Index && x.box_X_Index == _box_X_Index);

            if (recode != null)
            {
                lotlog += "[" + recode.lotName + recode.table_X_Name + recode.table_Y_Name + recode.box_X_Name + "]";
                return recode.lot();
            }
            else
            {
                // Debug.LogError("抽せん対象が見つかりませんでした。");
                return 0;
            }
        }

        /// <summary>
        /// 初期G数解除モード抽せん
        /// </summary>
        /// <returns></returns>
        public N1 LotN1()
        {
            int _table_X_Index = 0;
            int _table_Y_Index = 0;
            int _box_X_Index = 0;

            N1 ret = (N1)lot(1, _table_X_Index, _table_Y_Index, _box_X_Index);

            switch (ret)
            {
                case N1.NmlA:
                    gHitMode = GHitMode.NmlA;
                    break;
                case N1.NmlB:
                    gHitMode = GHitMode.NmlB;
                    break;
                case N1.NmlC:
                    gHitMode = GHitMode.NmlC;
                    break;
                case N1.RenA:
                    gHitMode = GHitMode.RenA;
                    break;
                case N1.RenB:
                    gHitMode = GHitMode.RenB;
                    break;
                case N1.RenC:
                    gHitMode = GHitMode.RenC;
                    break;
                case N1.Ret:
                    gHitMode = GHitMode.Ret;
                    break;
                default:
                    break;
            }

            return ret;
        }

        /// <summary>
        /// G数解除モード抽せん_G数解除時
        /// </summary>
        /// <returns></returns>
        public N2 LotN2()
        {
            int _table_X_Index = 0;
            int _table_Y_Index = 0;
            int _box_X_Index = (int)gHitMode;

            N2 ret = (N2)lot(2, _table_X_Index, _table_Y_Index, _box_X_Index);

            switch (ret)
            {
                case N2.NmlA:
                    gHitMode = GHitMode.NmlA;
                    break;
                case N2.NmlB:
                    gHitMode = GHitMode.NmlB;
                    break;
                case N2.NmlC:
                    gHitMode = GHitMode.NmlC;
                    break;
                case N2.RenA:
                    gHitMode = GHitMode.RenA;
                    break;
                case N2.RenB:
                    gHitMode = GHitMode.RenB;
                    break;
                case N2.RenC:
                    gHitMode = GHitMode.RenC;
                    break;
                case N2.Ret:
                    gHitMode = GHitMode.Ret;
                    break;
                default:
                    break;
            }

            return ret;
        }

        /// <summary>
        /// G数解除モード抽せん_小役解除時
        /// </summary>
        /// <returns></returns>
        public N3 LotN3()
        {
            int _table_X_Index = 0;
            int _table_Y_Index = (int)gHitMode;
            int _box_X_Index = 8 - (int)frtCode;

            N3 ret = (N3)lot(3, _table_X_Index, _table_Y_Index, _box_X_Index);

            switch (ret)
            {
                case N3.NmlA:
                    gHitMode = GHitMode.NmlA;
                    break;
                case N3.NmlB:
                    gHitMode = GHitMode.NmlB;
                    break;
                case N3.NmlC:
                    gHitMode = GHitMode.NmlC;
                    break;
                case N3.RenA:
                    gHitMode = GHitMode.RenA;
                    break;
                case N3.RenB:
                    gHitMode = GHitMode.RenB;
                    break;
                case N3.RenC:
                    gHitMode = GHitMode.RenC;
                    break;
                case N3.Ret:
                    gHitMode = GHitMode.Ret;
                    break;
                default:
                    break;
            }

            return ret;
        }

        /// <summary>
        /// 天井G数抽せん3桁目
        /// </summary>
        /// <returns></returns>
        public N4 LotN4()
        {
            int _table_X_Index = 0;
            int _table_Y_Index = 0;
            int _box_X_Index = (int)gHitMode;
            N4 ret = (N4)lot(4, _table_X_Index, _table_Y_Index, _box_X_Index);
            limitGame = (int)ret * 100;
            return ret;
        }

        /// <summary>
        /// 天井G数抽せん2桁目
        /// </summary>
        /// <returns></returns>
        public N5 LotN5()
        {
            int _table_X_Index = 0;
            int _table_Y_Index = 0;
            int _box_X_Index = (int)gHitMode;
            N5 ret = (N5)lot(5, _table_X_Index, _table_Y_Index, _box_X_Index);
            limitGame += (int)ret * 10;
            return ret;
        }

        /// <summary>
        /// 天井G数抽せん1桁目
        /// </summary>
        /// <returns></returns>
        public N6 LotN6()
        {
            int _table_X_Index = 0;
            int _table_Y_Index = 0;
            int _box_X_Index = (int)gHitMode;
            N6 ret = (N6)lot(6, _table_X_Index, _table_Y_Index, _box_X_Index);
            limitGame += (int)ret;
            return ret;
        }

        /// <summary>
        /// 通常モード移行抽せん
        /// </summary>
        /// <returns></returns>
        public N7 LotN7()
        {
            int _table_X_Index = 0;
            int _table_Y_Index = (int)nmlMode;
            int _box_X_Index = 8 - (int)frtCode;
            N7 ret = (N7)lot(7, _table_X_Index, _table_Y_Index, _box_X_Index);
            switch (ret)
            {
                case N7.Low:
                    nmlMode = NmlMode.Low;
                    break;
                case N7.Nml:
                    nmlMode = NmlMode.Nml;
                    break;
                case N7.High:
                    nmlMode = NmlMode.High;
                    break;
                case N7.HevenShort:
                    nmlMode = NmlMode.HevenShort;
                    break;
                case N7.Heven:
                    nmlMode = NmlMode.Heven;
                    break;
                default:
                    break;
            }
            return ret;
        }

        /// <summary>
        /// ボーナス抽せん
        /// </summary>
        /// <returns></returns>
        public N8 LotN8()
        {
            int _table_X_Index = (int)nmlMode;
            int _table_Y_Index = renNum;
            int _box_X_Index = 8 - (int)frtCode;
            N8 ret = (N8)lot(8, _table_X_Index, _table_Y_Index, _box_X_Index);
            switch (ret)
            {
                case N8.Hazure:
                    bnsCode = AutoMakeCode.Enum.BnsCode.Hazure;
                    break;
                case N8.ABIG:
                    bnsCode = AutoMakeCode.Enum.BnsCode.ABIG;
                    break;
                case N8.SBIG:
                    bnsCode = AutoMakeCode.Enum.BnsCode.SBIG;
                    break;
                case N8.REG:
                    bnsCode = AutoMakeCode.Enum.BnsCode.RB;
                    break;
                default:
                    break;
            }
            if (ret != N8.Hazure) hitType = HitType.RareHit;
            return ret;
        }

        /// <summary>
        /// ストック抽せん
        /// </summary>
        /// <returns></returns>
        public N9 LotN9()
        {
            int _table_X_Index = 0;
            int _table_Y_Index = 0;
            int _box_X_Index = 8 - (int)frtCode;
            N9 ret = (N9)lot(9, _table_X_Index, _table_Y_Index, _box_X_Index);
            switch (ret)
            {
                case N9._0:
                    break;
                case N9._1:
                    bnsStock += 1;
                    break;
                case N9._2:
                    bnsStock += 2;
                    break;
                case N9._3:
                    bnsStock += 3;
                    break;
                case N9._7:
                    bnsStock += 7;
                    break;
                case N9._10:
                    bnsStock += 10;
                    break;
                case N9._13:
                    bnsStock += 13;
                    break;
                case N9._15:
                    bnsStock += 15;
                    break;
                case N9._128:
                    bnsStock += 128;
                    break;
                default:
                    break;
            }
            return ret;
        }

        /// <summary>
        /// G数解除時ボーナス振り分け
        /// </summary>
        /// <returns></returns>
        public N10 LotN10()
        {
            int _table_X_Index = 0;
            int _table_Y_Index = (int)nmlMode;
            int _box_X_Index = (int)gHitMode;
            N10 ret = (N10)lot(10, _table_X_Index, _table_Y_Index, _box_X_Index);
            switch (ret)
            {
                case N10.Hazure:
                    bnsCode = AutoMakeCode.Enum.BnsCode.Hazure;
                    break;
                case N10.ABIG:
                    bnsCode = AutoMakeCode.Enum.BnsCode.ABIG;
                    break;
                case N10.SBIG:
                    bnsCode = AutoMakeCode.Enum.BnsCode.SBIG;
                    break;
                case N10.REG:
                    bnsCode = AutoMakeCode.Enum.BnsCode.RB;
                    break;
                default:
                    break;
            }
            hitType = HitType.LimitHit;
            return ret;
        }

        /// <summary>
        /// ストック放出時ボーナス振り分け
        /// </summary>
        /// <returns></returns>
        public N11 LotN11()
        {
            int _table_X_Index = 0;
            int _table_Y_Index = 0;
            int _box_X_Index = 0;
            N11 ret = (N11)lot(11, _table_X_Index, _table_Y_Index, _box_X_Index);
            switch (ret)
            {
                case N11.Hazure:
                    bnsCode = AutoMakeCode.Enum.BnsCode.Hazure;
                    break;
                case N11.ABIG:
                    bnsCode = AutoMakeCode.Enum.BnsCode.ABIG;
                    break;
                case N11.SBIG:
                    bnsCode = AutoMakeCode.Enum.BnsCode.SBIG;
                    break;
                case N11.REG:
                    bnsCode = AutoMakeCode.Enum.BnsCode.RB;
                    break;
                default:
                    break;
            }
            hitType = HitType.StockHit;
            bnsStock--;
            return ret;
        }

        /// <summary>
        /// 設定移行抽せん
        /// </summary>
        /// <returns></returns>
        public N12 LotN12()
        {
            int _table_X_Index = 0;
            int _table_Y_Index = (int)settei;
            int _box_X_Index = 8 - (int)frtCode;
            N12 ret = ((N12)lot(12, _table_X_Index, _table_Y_Index, _box_X_Index));
            switch (ret)
            {
                case N12._1:
                    settei = AutoMakeCode.Enum.Settei._1;
                    break;
                case N12._2:
                    settei = AutoMakeCode.Enum.Settei._2;
                    break;
                case N12._3:
                    settei = AutoMakeCode.Enum.Settei._3;
                    break;
                case N12._4:
                    settei = AutoMakeCode.Enum.Settei._4;
                    break;
                case N12._5:
                    settei = AutoMakeCode.Enum.Settei._5;
                    break;
                default:
                    break;
            }
            return ret;
        }

        /// <summary>
        /// レバー時出玉抽せん
        /// </summary>
        public void Lever(AutoMakeCode.Enum.BnsCode _bnsCode, AutoMakeCode.Enum.FrtCode _frtCode, AutoMakeCode.Enum.Status status)
        {
            lotlog = "";

            frtCode_old = frtCode;
            bnsCode = _bnsCode;
            frtCode = _frtCode;
            
            if(status == AutoMakeCode.Enum.Status.Nml) nmlGame++;

            if (frtCode == frtCode_old)
            {
                if (renNum < 5) renNum++;
            }
            else
            {
                renNum = 0;
            }

            switch (gHitMode)
            {
                case GHitMode.NmlA:
                case GHitMode.NmlB:
                case GHitMode.NmlC:
                    settei = AutoMakeCode.Enum.Settei._1;
                    break;
                case GHitMode.RenA:
                case GHitMode.RenB:
                case GHitMode.RenC:
                    settei = AutoMakeCode.Enum.Settei._3;
                    break;
                case GHitMode.Ret:
                    settei = AutoMakeCode.Enum.Settei._2;
                    break;
            }

            switch (status)
            {
                case AutoMakeCode.Enum.Status.ABIGStandby:
                case AutoMakeCode.Enum.Status.SBIGStandby:
                case AutoMakeCode.Enum.Status.RBStandby:
                    break;
                case AutoMakeCode.Enum.Status.ABIG:
                case AutoMakeCode.Enum.Status.SBIG:
                case AutoMakeCode.Enum.Status.RB:
                    // ボーナス中のハズレはボーナスストック
                    if (frtCode == AutoMakeCode.Enum.FrtCode.Hazure) bnsStock++;
                    break;
                case AutoMakeCode.Enum.Status.Nml:
                    // ボーナス抽せん
                    // if (bnsCode == AutoMakeCode.Enum.BnsCode.Hazure) LotN8();

                    // ストック抽せん
                    LotN9();

                    // ストック放出処理
                    if ((bnsCode == AutoMakeCode.Enum.BnsCode.Hazure) && (bnsStock > 0)) LotN11();

                    // 天井到達時処理
                    if ((nmlGame > limitGame) && (bnsCode == AutoMakeCode.Enum.BnsCode.Hazure) && (bnsStock == 0)) LotN10();

                    // 通常モード移行処理
                    // LotN7();

                    // 設定移行処理
                    // LotN12();

                    break;
                default:
                    break;
            }

        }

        /// <summary>
        /// ボーナス開始時出玉処理
        /// </summary>
        public void BnsStart()
        {
            switch (hitType)
            {
                case HitType.RareHit:
                    LotN3();
                    LotN4();
                    LotN5();
                    LotN6();
                    break;
                case HitType.LimitHit:
                    LotN2();
                    LotN4();
                    LotN5();
                    LotN6();
                    break;
                case HitType.StockHit:
                    // ストック放出時はモード移行無し
                    break;
                default:
                    break;
            }

            // G数解除モードが連荘に移行した場合は通常モードも天国へ
            switch (gHitMode)
            {
                case GHitMode.RenA:
                case GHitMode.RenB:
                case GHitMode.RenC:
                    nmlMode = NmlMode.Heven;
                    break;
                default:
                    nmlMode = NmlMode.Nml;
                    break;
            }
        }

        /// <summary>
        /// ボーナス終了時出玉処理
        /// </summary>
        public void BnsEnd()
        {
            nmlGame = 0;
            bnsCode = AutoMakeCode.Enum.BnsCode.Hazure;
        }
    }
}
