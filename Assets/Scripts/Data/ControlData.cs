using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Data
{
    abstract class ControlData
    {
        public int getSuberiComa(ReelController.Reel_ID reel, int stopComa)
        {
            int ret = 0;
            switch (Mn.stopSeq)
            {
                case Mn.StopSeq._1xx:
                    ret = L_stop(stopComa);
                    break;
                case Mn.StopSeq._12x:
                    ret = LC_stop(stopComa);
                    break;
                case Mn.StopSeq._123:
                    ret = LCR_stop(stopComa);
                    break;
                case Mn.StopSeq._1x2:
                    ret = LR_stop(stopComa);
                    break;
                case Mn.StopSeq._132:
                    ret = LRC_stop(stopComa);
                    break;
                case Mn.StopSeq._x1x:
                    ret = C_stop(stopComa);
                    break;
                case Mn.StopSeq._21x:
                    ret = CL_stop(stopComa);
                    break;
                case Mn.StopSeq._213:
                    ret = CLR_stop(stopComa);
                    break;
                case Mn.StopSeq._x12:
                    ret = CR_stop(stopComa);
                    break;
                case Mn.StopSeq._312:
                    ret = CRL_stop(stopComa);
                    break;
                case Mn.StopSeq._xx1:
                    ret = R_stop(stopComa);
                    break;
                case Mn.StopSeq._2x1:
                    ret = RL_stop(stopComa);
                    break;
                case Mn.StopSeq._231:
                    ret = RLC_stop(stopComa);
                    break;
                case Mn.StopSeq._x21:
                    ret = RC_stop(stopComa);
                    break;
                case Mn.StopSeq._321:
                    ret = RCL_stop(stopComa);
                    break;
                default:
                    break;
            }
            return ret;
        }

        /// <summary>
        /// 左1st停止時処理
        /// </summary>
        /// <returns></returns>
        abstract public int L_stop(int stopComa);

        /// <summary>
        /// 中1st停止時処理
        /// </summary>
        /// <returns></returns>
        abstract public int C_stop(int stopComa);

        /// <summary>
        /// 右1st停止時処理
        /// </summary>
        /// <returns></returns>
        abstract public int R_stop(int stopComa);

        /// <summary>
        /// 左中停止時処理
        /// </summary>
        /// <returns></returns>
        abstract public int LC_stop(int stopComa);

        /// <summary>
        /// 左右停止時処理
        /// </summary>
        /// <returns></returns>
        abstract public int LR_stop(int stopComa);

        /// <summary>
        /// 中左停止時処理
        /// </summary>
        /// <returns></returns>
        abstract public int CL_stop(int stopComa);

        /// <summary>
        /// 中右停止時処理
        /// </summary>
        /// <returns></returns>
        abstract public int CR_stop(int stopComa);

        /// <summary>
        /// 右左停止時処理
        /// </summary>
        /// <returns></returns>
        abstract public int RL_stop(int stopComa);

        /// <summary>
        /// 右中停止時処理
        /// </summary>
        /// <returns></returns>
        abstract public int RC_stop(int stopComa);

        /// <summary>
        /// 左中右停止時処理
        /// </summary>
        /// <returns></returns>
        abstract public int LCR_stop(int stopComa);

        /// <summary>
        /// 左右中停止時処理
        /// </summary>
        /// <returns></returns>
        abstract public int LRC_stop(int stopComa);

        /// <summary>
        /// 中左右停止時処理
        /// </summary>
        /// <returns></returns>
        abstract public int CLR_stop(int stopComa);

        /// <summary>
        /// 中右左停止時処理
        /// </summary>
        /// <returns></returns>
        abstract public int CRL_stop(int stopComa);

        /// <summary>
        /// 右左中停止時処理
        /// </summary>
        /// <returns></returns>
        abstract public int RLC_stop(int stopComa);

        /// <summary>
        /// 右中左停止時処理
        /// </summary>
        /// <returns></returns>
        abstract public int RCL_stop(int stopComa);

        /// <summary>
        /// 配列内の図柄で引き込める物があればその図柄までの滑りコマを返す
        /// </summary>
        /// <param name="zugaras"></param>
        /// <param name="reel"></param>
        /// <param name="stopComa"></param>
        /// <returns></returns>
        public static int trygetZugaraListSuberi(ReelData.Zugara[] zugaras, ReelController.Reel_ID reel, int stopComa)
        {
            int ret = -1;
            foreach (var item in zugaras)
            {
                if(ReelData.getZugaraComa(item, reel, stopComa) >= 0)
                {
                    ret = ReelData.getZugaraComa(item, reel, stopComa);
                    break;
                }
            }
            return ret;
        }

        /// <summary>
        /// 図柄リスト内に引き込める物が無ければエラー、あれば滑りコマを返す
        /// </summary>
        /// <param name="zugaras"></param>
        /// <param name="stopComa"></param>
        /// <returns></returns>
        public static int getZugaraListSuberi(ReelData.Zugara[] zugaras, ReelController.Reel_ID reel, int stopComa)
        {
            if (trygetZugaraListSuberi(zugaras, reel, stopComa) >= 0)
            {
                return trygetZugaraListSuberi(zugaras, reel, stopComa);
            }
            else
            {
                Debug.LogError("制御エラー");
                return 0;
            }
        }

        public enum Pos
        {
            H,  // 上段
            M,  // 中段
            L,  // 下段
        }

        /// <summary>
        /// 指定の場所の図柄を返す
        /// </summary>
        /// <param name="reel"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public static ReelData.Zugara getZugara(ReelController.Reel_ID reel, Pos pos)
        {
            int coma = 0;
            int maxcoma = Mn.leftReel.MaxComa;

            // コマ数取得
            switch (reel)
            {
                case ReelController.Reel_ID.Left:
                    coma = Mn.leftReel.getComa();
                    break;
                case ReelController.Reel_ID.Center:
                    coma = Mn.centerReel.getComa();
                    break;
                case ReelController.Reel_ID.Right:
                    coma = Mn.rightReel.getComa();
                    break;
            }

            // 切れ目補正
            switch (pos)
            {
                case Pos.H:
                    coma--;
                    if (coma == 0) coma += maxcoma;
                    break;
                case Pos.M:
                    break;
                case Pos.L:
                    coma++;
                    if (coma > 21) coma -= maxcoma;
                    break;
            }

            switch (reel)
            {
                case ReelController.Reel_ID.Left:
                    return ReelData.LeftReelArray[coma];
                case ReelController.Reel_ID.Center:
                    return ReelData.CenterReelArray[coma];
                case ReelController.Reel_ID.Right:
                    return ReelData.RightReelArray[coma];
            }

            Debug.LogError("適切な図柄を返せませんでした。");
            return ReelData.Zugara.Replay;
        }
    }

    class ControlTable
    {
        public enum leftTblName
        {
            h_suika,
            m_replay,
            l1stChance,
            cherry,
            m_bell,
            m_suika,
            m_homura,
        }
        public static int[][] leftTbl = new int[][]
        {
            // 上段スイカ
            //         1  2  3  4  5  6  7  8  9  10 11 12 13 14 15 16 17 18 19 20 21
            new int[]{ 3, 0, 1, 2, 3, 0, 1, 2, 3, 4, 0, 1, 2, 0, 1, 2, 3, 4, 0, 1, 2, },
            // 中段リプレイ
            //         1  2  3  4  5  6  7  8  9  10 11 12 13 14 15 16 17 18 19 20 21
            new int[]{ 1, 2, 3, 1, 2, 3, 0, 1, 2, 3, 4, 0, 1, 2, 3, 4, 0, 1, 2, 3, 0, },
            // 左1stチャンス目
            //         1  2  3  4  5  6  7  8  9  10 11 12 13 14 15 16 17 18 19 20 21
            new int[]{ 1, 2, 3, 1, 2, 3, 0, 1, 3, 4, 4, 0, 1, 2, 3, 4, 0, 1, 2, 3, 0, },
            // チェリー
            //         1  2  3  4  5  6  7  8  9  10 11 12 13 14 15 16 17 18 19 20 21
            new int[]{ 0, 1, 2, 3, 0, 1, 2, 0, 1, 2, 3, 4, 0, 1, 2, 3, 4, 0, 1, 2, 3, },
            // 中段ベル
            //         1  2  3  4  5  6  7  8  9  10 11 12 13 14 15 16 17 18 19 20 21
            new int[]{ 2, 0, 1, 2, 3, 0, 1, 2, 3, 4, 0, 1, 2, 3, 4, 0, 1, 2, 3, 0, 1, },
            // 中段スイカ
            //         1  2  3  4  5  6  7  8  9  10 11 12 13 14 15 16 17 18 19 20 21
            new int[]{ 0, 1, 2, 3, 0, 1, 2, 3, 4, 0, 1, 2, 0, 1, 2, 3, 4, 0, 1, 2, 3, },
            // ほむら揃い
            //         1  2  3  4  5  6  7  8  9  10 11 12 13 14 15 16 17 18 19 20 21
            new int[]{ 0, 1, 2, 3, 0, 1, 2, 3, 4, 0, 1, 2, 3, 0, 1, 2, 3, 4, 4, 2, 3, },
        };

        public enum centerTblName
        {
            m_sp,
            m_suika,
            m_replay,
            cherry,
            l_bell,
            m_bell,
        }
        public static int[][] centerTbl = new int[][]
        {
            // 中段特図
            //         1  2  3  4  5  6  7  8  9  10 11 12 13 14 15 16 17 18 19 20 21
            new int[]{ 0, 1, 2, 0, 1, 0, 1, 2, 0, 1, 2, 3, 4, 0, 1, 2, 3, 4, 0, 1, 2, },
            // 中段スイカ
            //         1  2  3  4  5  6  7  8  9  10 11 12 13 14 15 16 17 18 19 20 21
            new int[]{ 0, 1, 0, 1, 2, 3, 4, 0, 0, 0, 0, 1, 2, 3, 4, 0, 1, 2, 3, 4, 2, },
            // 中段リプレイ
            //         1  2  3  4  5  6  7  8  9  10 11 12 13 14 15 16 17 18 19 20 21
            new int[]{ 2, 0, 1, 2, 3, 4, 0, 1, 2, 3, 4, 0, 1, 2, 3, 4, 0, 1, 2, 0, 1, },
            // チェリー
            //         1  2  3  4  5  6  7  8  9  10 11 12 13 14 15 16 17 18 19 20 21
            new int[]{ 0, 1, 2, 3, 4, 2, 3, 4, 0, 1, 2, 3, 4, 0, 1, 2, 3, 4, 0, 1, 2, },
            // 下段ベル
            //         1  2  3  4  5  6  7  8  9  10 11 12 13 14 15 16 17 18 19 20 21
            new int[]{ 2, 3, 4, 0, 1, 2, 0, 1, 2, 3, 4, 0, 1, 2, 3, 4, 0, 1, 2, 0, 1, },
            // 中段ベル
            //         1  2  3  4  5  6  7  8  9  10 11 12 13 14 15 16 17 18 19 20 21
            new int[]{ 1, 2, 3, 4, 0, 1, 2, 0, 1, 2, 3, 4, 0, 1, 2, 3, 4, 0, 1, 2, 0, },

        };

        public enum rightTblName
        {
            h_sp,
            m_sp,
            l_suika,
            m_replay,
            m_bell,
            kyoche,
            h_bell,
        }
        public static int[][] rightTbl = new int[][]
        {
            // 上段特図
            //         1  2  3  4  5  6  7  8  9  10 11 12 13 14 15 16 17 18 19 20 21
            new int[]{ 0, 1, 2, 3, 0, 1, 2, 3, 4, 0, 1, 2, 3, 0, 1, 2, 3, 4, 0, 1, 2, },
            // 中段特図
            //         1  2  3  4  5  6  7  8  9  10 11 12 13 14 15 16 17 18 19 20 21
            new int[]{ 0, 1, 2, 3, 0, 1, 2, 3, 4, 3, 4, 4, 1, 0, 1, 2, 3, 4, 4, 4, 4, },
            // 下段スイカ
            //         1  2  3  4  5  6  7  8  9  10 11 12 13 14 15 16 17 18 19 20 21
            new int[]{ 1, 2, 3, 4, 1, 2, 3, 4, 1, 2, 3, 4, 0, 1, 2, 3, 4, 1, 2, 3, 0, },
            // 中段リプレイ
            //         1  2  3  4  5  6  7  8  9  10 11 12 13 14 15 16 17 18 19 20 21
            new int[]{ 3, 0, 1, 2, 3, 0, 1, 2, 3, 4, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, },
            // 中段ベルA
            //         1  2  3  4  5  6  7  8  9  10 11 12 13 14 15 16 17 18 19 20 21
            new int[]{ 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 4, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, },
            // 強チェリー用特図
            //         1  2  3  4  5  6  7  8  9  10 11 12 13 14 15 16 17 18 19 20 21
            new int[]{ 1, 2, 0, 0, 1, 2, 3, 4, 0, 1, 2, 3, 4, 1, 2, 3, 0, 1, 2, 3, 4, },
            // 上段ベル
            //         1  2  3  4  5  6  7  8  9  10 11 12 13 14 15 16 17 18 19 20 21
            new int[]{ 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 4, 0, 1, 2, 3, 0, 1, 2, 3, 0, },
        };

        public static int[][] sampleTbl = new int[][]
        {
            // 左      1  2  3  4  5  6  7  8  9  10 11 12 13 14 15 16 17 18 19 20 21
            new int[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
            // 中      1  2  3  4  5  6  7  8  9  10 11 12 13 14 15 16 17 18 19 20 21
            new int[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
            // 右      1  2  3  4  5  6  7  8  9  10 11 12 13 14 15 16 17 18 19 20 21
            new int[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
        };

        public static int getleftTbl(leftTblName name)
        {
            return leftTbl[(int)name][Mn.leftReel.getComa()];
        }

        public static int getcenterTbl(centerTblName name)
        {
            return centerTbl[(int)name][Mn.centerReel.getComa()];
        }

        public static int getrightTbl(rightTblName name)
        {
            return rightTbl[(int)name][Mn.rightReel.getComa()];
        }
    }


    /// <summary>
    /// チャンス目制御データ
    /// </summary>
    class ChanceControl : ControlData
    {
        public override int L_stop(int stopComa)
        {
            return ControlTable.getleftTbl(ControlTable.leftTblName.l1stChance);
        }

        public override int LC_stop(int stopComa)
        {
            if(ReelData.LeftReelArray[Mn.leftReel.getComa()] == ReelData.Zugara.Replay)
            {
                return ControlTable.getcenterTbl(ControlTable.centerTblName.m_sp);
            }
            else
            {
                return ControlTable.getcenterTbl(ControlTable.centerTblName.m_suika);
            }
        }

        public override int LCR_stop(int stopComa)
        {
            if ((getZugara(ReelController.Reel_ID.Left, Pos.H) == ReelData.Zugara.Suika) &&
                (getZugara(ReelController.Reel_ID.Center, Pos.M) == ReelData.Zugara.Suika))
            {
                ReelData.Zugara[] zugaras = { ReelData.Zugara.BellA };
                return getZugaraListSuberi(zugaras, ReelController.Reel_ID.Right, stopComa);
            }
            else
            {
                return ControlTable.getrightTbl(ControlTable.rightTblName.h_sp);
            }
        }

        public override int LR_stop(int stopComa)
        {
            if(ReelData.LeftReelArray[Mn.leftReel.getComa() - 1] == ReelData.Zugara.Suika)
            {
                // 上段スイカ
                return ControlTable.centerTbl[(int)ControlTable.centerTblName.m_suika][Mn.centerReel.getComa()];
            }
            else
            {
                return ControlTable.centerTbl[(int)ControlTable.centerTblName.m_sp][Mn.centerReel.getComa()];
            }
        }

        public override int LRC_stop(int stopComa)
        {
            if((getZugara(ReelController.Reel_ID.Left, Pos.H) == ReelData.Zugara.Suika) &&
                (getZugara(ReelController.Reel_ID.Right, Pos.L) == ReelData.Zugara.Suika))
            {
                // ハサミスイカテンパイ
                ReelData.Zugara[] zugaras = { ReelData.Zugara.Replay };
                return getZugaraListSuberi(zugaras, ReelController.Reel_ID.Center, stopComa);
            }
            else
            {
                return ControlTable.getcenterTbl(ControlTable.centerTblName.m_sp);
            }
        }

        public override int C_stop(int stopComa)
        {
            ReelData.Zugara[] zugaras = { 
                ReelData.Zugara.Suika,
                ReelData.Zugara.Bar,
                ReelData.Zugara.Cherry,
                ReelData.Zugara.Replay,
            };

            return getZugaraListSuberi(zugaras, ReelController.Reel_ID.Center, stopComa);
        }

        public override int CL_stop(int stopComa)
        {
            if(ReelData.CenterReelArray[Mn.centerReel.getComa()] == ReelData.Zugara.Suika)
            {
                // 中1stでスイカを引き込んでいる場合
                return ControlTable.leftTbl[(int)ControlTable.leftTblName.h_suika][Mn.leftReel.getComa()];
            }
            else
            {
                // それ以外は左中段にリプレイ
                ReelData.Zugara[] zugaras = { ReelData.Zugara.Replay };
                return getZugaraListSuberi(zugaras, ReelController.Reel_ID.Center, stopComa);
            }
        }

        public override int CLR_stop(int stopComa)
        {
            if ((ReelData.LeftReelArray[Mn.leftReel.getComa() - 1] == ReelData.Zugara.Suika) &&
                (ReelData.CenterReelArray[Mn.centerReel.getComa()] == ReelData.Zugara.Suika))
            {
                // スイカが右下がりにテンパイ
                return ControlTable.rightTbl[(int)ControlTable.rightTblName.m_sp][Mn.rightReel.getComa()];
            }
            else
            {
                return ControlTable.rightTbl[(int)ControlTable.rightTblName.h_sp][Mn.rightReel.getComa()];
            }

        }

        public override int CR_stop(int stopComa)
        {
            if (ReelData.CenterReelArray[Mn.centerReel.getComa()] == ReelData.Zugara.Suika)
            {
                // 中1stでスイカを引き込んでいる場合
                return ControlTable.rightTbl[(int)ControlTable.rightTblName.l_suika][Mn.rightReel.getComa()];
            }
            else
            {
                // それ以外は左中段にリプレイ
                return ControlTable.rightTbl[(int)ControlTable.rightTblName.h_sp][Mn.rightReel.getComa()];
            }
        }

        public override int CRL_stop(int stopComa)
        {
            if((ReelData.CenterReelArray[Mn.centerReel.getComa()] == ReelData.Zugara.Suika)&&
                (ReelData.RightReelArray[Mn.rightReel.getComa() + 1] == ReelData.Zugara.Suika))
            {
                // 左上テンパイ時
                ReelData.Zugara[] zugaras = { ReelData.Zugara.Suika };
                return getZugaraListSuberi(zugaras, ReelController.Reel_ID.Left, stopComa);
            }
            else
            {
                ReelData.Zugara[] zugaras = { ReelData.Zugara.Replay };
                return getZugaraListSuberi(zugaras, ReelController.Reel_ID.Left, stopComa);
            }
        }

        public override int R_stop(int stopComa)
        {
            ReelData.Zugara[] zugaras = {
                ReelData.Zugara.WhiteSeven,
                ReelData.Zugara.Blank,
                ReelData.Zugara.Bar,
                ReelData.Zugara.Homura,
            };

            return getZugaraListSuberi(zugaras, ReelController.Reel_ID.Right, stopComa);
        }

        public override int RL_stop(int stopComa)
        {
            if(getZugara(ReelController.Reel_ID.Right, Pos.L) == ReelData.Zugara.Suika)
            {
                // 右下段スイカの場合
                return ControlTable.getleftTbl(ControlTable.leftTblName.h_suika);
            }
            else
            {
                return ControlTable.getleftTbl(ControlTable.leftTblName.m_replay);
            }
        }

        public override int RLC_stop(int stopComa)
        {
            if((getZugara(ReelController.Reel_ID.Left, Pos.H) == ReelData.Zugara.Suika) &&
                (getZugara(ReelController.Reel_ID.Right, Pos.L) == ReelData.Zugara.Suika))
            {
                return ControlTable.getcenterTbl(ControlTable.centerTblName.m_replay);
            }
            else
            {
                return ControlTable.getcenterTbl(ControlTable.centerTblName.m_sp);
            }
        }

        public override int RC_stop(int stopComa)
        {
            if(getZugara(ReelController.Reel_ID.Right, Pos.L) == ReelData.Zugara.Suika)
            {
                return ControlTable.getcenterTbl(ControlTable.centerTblName.m_suika);
            }
            else
            {
                return ControlTable.getcenterTbl(ControlTable.centerTblName.m_sp);
            }
        }

        public override int RCL_stop(int stopComa)
        {
            if((getZugara(ReelController.Reel_ID.Center, Pos.M) == ReelData.Zugara.Suika) &&
                (getZugara(ReelController.Reel_ID.Right, Pos.L) == ReelData.Zugara.Suika))
            {
                ReelData.Zugara[] zugaras = { ReelData.Zugara.Suika };
                return getZugaraListSuberi(zugaras, ReelController.Reel_ID.Left, stopComa);
            }
            else
            {
                return ControlTable.getleftTbl(ControlTable.leftTblName.m_replay);
            }
        }
    }

    /// <summary>
    /// 強チェリー制御データ
    /// </summary>
    class KyoChe : ControlData
    {
        public override int L_stop(int stopComa)
        {
            return ControlTable.getleftTbl(ControlTable.leftTblName.cherry);
        }

        public override int LC_stop(int stopComa)
        {
            return ControlTable.getcenterTbl(ControlTable.centerTblName.cherry);
        }

        public override int LCR_stop(int stopComa)
        {
            return ControlTable.getrightTbl(ControlTable.rightTblName.kyoche);
        }

        public override int LR_stop(int stopComa)
        {
            return ControlTable.getrightTbl(ControlTable.rightTblName.kyoche);
        }

        public override int LRC_stop(int stopComa)
        {
            return ControlTable.getcenterTbl(ControlTable.centerTblName.cherry);
        }

        public override int C_stop(int stopComa)
        {
            return ControlTable.getcenterTbl(ControlTable.centerTblName.cherry);
        }

        public override int CL_stop(int stopComa)
        {
            return ControlTable.getleftTbl(ControlTable.leftTblName.cherry);
        }

        public override int CLR_stop(int stopComa)
        {
            return ControlTable.getrightTbl(ControlTable.rightTblName.kyoche);
        }

        public override int CR_stop(int stopComa)
        {
            return ControlTable.getrightTbl(ControlTable.rightTblName.kyoche);
        }

        public override int CRL_stop(int stopComa)
        {
            return ControlTable.getleftTbl(ControlTable.leftTblName.cherry);
        }

        public override int R_stop(int stopComa)
        {
            return ControlTable.getrightTbl(ControlTable.rightTblName.kyoche);
        }

        public override int RL_stop(int stopComa)
        {
            return ControlTable.getleftTbl(ControlTable.leftTblName.cherry);
        }

        public override int RLC_stop(int stopComa)
        {
            return ControlTable.getcenterTbl(ControlTable.centerTblName.cherry);
        }

        public override int RC_stop(int stopComa)
        {
            return ControlTable.getcenterTbl(ControlTable.centerTblName.cherry);
        }

        public override int RCL_stop(int stopComa)
        {
            return ControlTable.getleftTbl(ControlTable.leftTblName.cherry);
        }
    }

    /// <summary>
    /// 弱チェリー制御データ
    /// </summary>
    class JakuChe : ControlData
    {
        public override int L_stop(int stopComa)
        {
            return ControlTable.getleftTbl(ControlTable.leftTblName.cherry);
        }

        public override int LC_stop(int stopComa)
        {
            return ControlTable.getcenterTbl(ControlTable.centerTblName.cherry);
        }

        public override int LCR_stop(int stopComa)
        {
            return ControlTable.getrightTbl(ControlTable.rightTblName.m_bell);
        }

        public override int LR_stop(int stopComa)
        {
            return ControlTable.getrightTbl(ControlTable.rightTblName.m_bell);
        }

        public override int LRC_stop(int stopComa)
        {
            return ControlTable.getcenterTbl(ControlTable.centerTblName.cherry);
        }

        public override int C_stop(int stopComa)
        {
            return ControlTable.getcenterTbl(ControlTable.centerTblName.cherry);
        }

        public override int CL_stop(int stopComa)
        {
            return ControlTable.getleftTbl(ControlTable.leftTblName.cherry);
        }

        public override int CLR_stop(int stopComa)
        {
            return ControlTable.getrightTbl(ControlTable.rightTblName.m_bell);
        }

        public override int CR_stop(int stopComa)
        {
            return ControlTable.getrightTbl(ControlTable.rightTblName.m_bell);
        }

        public override int CRL_stop(int stopComa)
        {
            return ControlTable.getleftTbl(ControlTable.leftTblName.cherry);
        }

        public override int R_stop(int stopComa)
        {
            return ControlTable.getrightTbl(ControlTable.rightTblName.m_bell);
        }

        public override int RL_stop(int stopComa)
        {
            return ControlTable.getleftTbl(ControlTable.leftTblName.cherry);
        }

        public override int RLC_stop(int stopComa)
        {
            return ControlTable.getcenterTbl(ControlTable.centerTblName.cherry);
        }

        public override int RC_stop(int stopComa)
        {
            return ControlTable.getcenterTbl(ControlTable.centerTblName.cherry);
        }

        public override int RCL_stop(int stopComa)
        {
            return ControlTable.getleftTbl(ControlTable.leftTblName.cherry);
        }
    }

    /// <summary>
    /// スイカ制御データ
    /// </summary>
    class Suika : ControlData
    {
        public override int CLR_stop(int stopComa)
        {
            return ControlTable.getrightTbl(ControlTable.rightTblName.l_suika);
        }

        public override int CL_stop(int stopComa)
        {
            return ControlTable.getleftTbl(ControlTable.leftTblName.h_suika);
        }

        public override int CRL_stop(int stopComa)
        {
            return ControlTable.getleftTbl(ControlTable.leftTblName.h_suika);
        }

        public override int CR_stop(int stopComa)
        {
            return ControlTable.getrightTbl(ControlTable.rightTblName.l_suika);
        }

        public override int C_stop(int stopComa)
        {
            return ControlTable.getcenterTbl(ControlTable.centerTblName.m_suika);
        }

        public override int LCR_stop(int stopComa)
        {
            return ControlTable.getrightTbl(ControlTable.rightTblName.l_suika);
        }

        public override int LC_stop(int stopComa)
        {
            return ControlTable.getcenterTbl(ControlTable.centerTblName.m_suika);
        }

        public override int LRC_stop(int stopComa)
        {
            return ControlTable.getcenterTbl(ControlTable.centerTblName.m_suika);
        }

        public override int LR_stop(int stopComa)
        {
            return ControlTable.getrightTbl(ControlTable.rightTblName.l_suika);
        }

        public override int L_stop(int stopComa)
        {
            return ControlTable.getleftTbl(ControlTable.leftTblName.h_suika);
        }

        public override int RCL_stop(int stopComa)
        {
            return ControlTable.getleftTbl(ControlTable.leftTblName.h_suika);
        }

        public override int RC_stop(int stopComa)
        {
            return ControlTable.getcenterTbl(ControlTable.centerTblName.m_suika);
        }

        public override int RLC_stop(int stopComa)
        {
            return ControlTable.getcenterTbl(ControlTable.centerTblName.m_suika);
        }

        public override int RL_stop(int stopComa)
        {
            return ControlTable.getleftTbl(ControlTable.leftTblName.h_suika);
        }

        public override int R_stop(int stopComa)
        {
            return ControlTable.getrightTbl(ControlTable.rightTblName.l_suika);
        }
    }

    /// <summary>
    /// ほむらリプレイ制御データ
    /// </summary>
    class HomuraReplay : ControlData
    {
        private static ReelData.Zugara[] zugaras =
        {
            ReelData.Zugara.Homura,
            ReelData.Zugara.Replay,
        };

        private static int leftStop(int stopComa)
        {
            if (IsHomuraStop() == true)
            {
                return ControlTable.getleftTbl(ControlTable.leftTblName.m_homura);
            }
            else
            {
                return ControlTable.getleftTbl(ControlTable.leftTblName.m_replay);
            }
        }

        private static int centerStop(int stopComa)
        {
            if (IsHomuraStop() == true)
            {
                return getZugaraListSuberi(zugaras, ReelController.Reel_ID.Center, stopComa);
            }
            else
            {
                return ControlTable.getcenterTbl(ControlTable.centerTblName.m_replay);
            }
        }

        private static int rightStop(int stopComa)
        {
            if (IsHomuraStop() == true)
            {
                return getZugaraListSuberi(zugaras, ReelController.Reel_ID.Right, stopComa);
            }
            else
            {
                return ControlTable.getrightTbl(ControlTable.rightTblName.m_replay);
            }
        }

        public override int CLR_stop(int stopComa)
        {
            return rightStop(stopComa);
        }

        public override int CL_stop(int stopComa)
        {
            return leftStop(stopComa);
        }

        public override int CRL_stop(int stopComa)
        {
            return leftStop(stopComa);
        }

        public override int CR_stop(int stopComa)
        {
            return rightStop(stopComa);
        }

        public override int C_stop(int stopComa)
        {
            return centerStop(stopComa);
        }

        public override int LCR_stop(int stopComa)
        {
            return rightStop(stopComa);
        }

        public override int LC_stop(int stopComa)
        {
            return centerStop(stopComa);
        }

        public override int LRC_stop(int stopComa)
        {
            return centerStop(stopComa);
        }

        public override int LR_stop(int stopComa)
        {
            return rightStop(stopComa);
        }

        public override int L_stop(int stopComa)
        {
            return ControlTable.getleftTbl(ControlTable.leftTblName.m_replay);
        }

        public override int RCL_stop(int stopComa)
        {
            return leftStop(stopComa);
        }

        public override int RC_stop(int stopComa)
        {
            return centerStop(stopComa);
        }

        public override int RLC_stop(int stopComa)
        {
            return centerStop(stopComa);
        }

        public override int RL_stop(int stopComa)
        {
            return leftStop(stopComa);
        }

        public override int R_stop(int stopComa)
        {
            return rightStop(stopComa);
        }

        private static bool IsHomuraStop()
        {
            switch (Mn.stopSeq)
            {
                case Mn.StopSeq._12x:
                case Mn.StopSeq._1x2:
                    return (getZugara(ReelController.Reel_ID.Left, Pos.M) == ReelData.Zugara.Homura);
                case Mn.StopSeq._21x:
                case Mn.StopSeq._x12:
                    return (getZugara(ReelController.Reel_ID.Center, Pos.M) == ReelData.Zugara.Homura);
                case Mn.StopSeq._x21:
                case Mn.StopSeq._2x1:
                    return (getZugara(ReelController.Reel_ID.Right, Pos.M) == ReelData.Zugara.Homura);
                case Mn.StopSeq._123:
                case Mn.StopSeq._213:
                    return ((getZugara(ReelController.Reel_ID.Left, Pos.M) == ReelData.Zugara.Homura) && (getZugara(ReelController.Reel_ID.Center, Pos.M) == ReelData.Zugara.Homura));
                case Mn.StopSeq._312:
                case Mn.StopSeq._321:
                    return ((getZugara(ReelController.Reel_ID.Right, Pos.M) == ReelData.Zugara.Homura) && (getZugara(ReelController.Reel_ID.Center, Pos.M) == ReelData.Zugara.Homura));
                case Mn.StopSeq._132:
                case Mn.StopSeq._231:
                    return ((getZugara(ReelController.Reel_ID.Left, Pos.M) == ReelData.Zugara.Homura) && (getZugara(ReelController.Reel_ID.Right, Pos.M) == ReelData.Zugara.Homura));
                default:
                    return true;
            }
        }
    }

    /// <summary>
    /// Regリプレイ制御データ
    /// </summary>
    class RegReplay : ControlData
    {
        private static ReelData.Zugara[] zugaras =
        {
            ReelData.Zugara.Bar,
            ReelData.Zugara.Replay,
        };

        private static int leftStop(int stopComa)
        {
            if (IsBarStop() == true)
            {
                return getZugaraListSuberi(zugaras, ReelController.Reel_ID.Left, stopComa);
            }
            else
            {
                return ControlTable.getleftTbl(ControlTable.leftTblName.m_replay);
            }
        }

        private static int centerStop(int stopComa)
        {
            if (IsBarStop() == true)
            {
                return getZugaraListSuberi(zugaras, ReelController.Reel_ID.Center, stopComa);
            }
            else
            {
                return ControlTable.getcenterTbl(ControlTable.centerTblName.m_replay);
            }
        }

        private static int rightStop(int stopComa)
        {
            if (IsBarStop() == true)
            {
                return getZugaraListSuberi(zugaras, ReelController.Reel_ID.Right, stopComa);
            }
            else
            {
                return ControlTable.getrightTbl(ControlTable.rightTblName.m_replay);
            }
        }

        public override int CLR_stop(int stopComa)
        {
            return rightStop(stopComa);
        }

        public override int CL_stop(int stopComa)
        {
            return leftStop(stopComa);
        }

        public override int CRL_stop(int stopComa)
        {
            return leftStop(stopComa);
        }

        public override int CR_stop(int stopComa)
        {
            return rightStop(stopComa);
        }

        public override int C_stop(int stopComa)
        {
            return centerStop(stopComa);
        }

        public override int LCR_stop(int stopComa)
        {
            return rightStop(stopComa);
        }

        public override int LC_stop(int stopComa)
        {
            return centerStop(stopComa);
        }

        public override int LRC_stop(int stopComa)
        {
            return centerStop(stopComa);
        }

        public override int LR_stop(int stopComa)
        {
            return rightStop(stopComa);
        }

        public override int L_stop(int stopComa)
        {
            return ControlTable.getleftTbl(ControlTable.leftTblName.m_replay);
        }

        public override int RCL_stop(int stopComa)
        {
            return leftStop(stopComa);
        }

        public override int RC_stop(int stopComa)
        {
            return centerStop(stopComa);
        }

        public override int RLC_stop(int stopComa)
        {
            return centerStop(stopComa);
        }

        public override int RL_stop(int stopComa)
        {
            return leftStop(stopComa);
        }

        public override int R_stop(int stopComa)
        {
            return rightStop(stopComa);
        }

        private static bool IsBarStop()
        {
            switch (Mn.stopSeq)
            {
                case Mn.StopSeq._12x:
                case Mn.StopSeq._1x2:
                    return (getZugara(ReelController.Reel_ID.Left, Pos.M) == ReelData.Zugara.Bar);
                case Mn.StopSeq._21x:
                case Mn.StopSeq._x12:
                    return (getZugara(ReelController.Reel_ID.Center, Pos.M) == ReelData.Zugara.Bar);
                case Mn.StopSeq._x21:
                case Mn.StopSeq._2x1:
                    return (getZugara(ReelController.Reel_ID.Right, Pos.M) == ReelData.Zugara.Bar);
                case Mn.StopSeq._123:
                case Mn.StopSeq._213:
                    return ((getZugara(ReelController.Reel_ID.Left, Pos.M) == ReelData.Zugara.Bar) && (getZugara(ReelController.Reel_ID.Center, Pos.M) == ReelData.Zugara.Bar));
                case Mn.StopSeq._312:
                case Mn.StopSeq._321:
                    return ((getZugara(ReelController.Reel_ID.Right, Pos.M) == ReelData.Zugara.Bar) && (getZugara(ReelController.Reel_ID.Center, Pos.M) == ReelData.Zugara.Bar));
                case Mn.StopSeq._132:
                case Mn.StopSeq._231:
                    return ((getZugara(ReelController.Reel_ID.Left, Pos.M) == ReelData.Zugara.Bar) && (getZugara(ReelController.Reel_ID.Right, Pos.M) == ReelData.Zugara.Bar));
                default:
                    return true;
            }
        }
    }

    /// <summary>
    /// Bigリプレイ制御データ
    /// </summary>
    class BigReplay : ControlData
    {
        private static ReelData.Zugara[] zugaras =
        {
            ReelData.Zugara.RedSeven,
            ReelData.Zugara.Replay,
        };

        private static int leftStop(int stopComa)
        {
            if (IsRedStop() == true)
            {
                return getZugaraListSuberi(zugaras, ReelController.Reel_ID.Left, stopComa);
            }
            else
            {
                return ControlTable.getleftTbl(ControlTable.leftTblName.m_replay);
            }
        }

        private static int centerStop(int stopComa)
        {
            if (IsRedStop() == true)
            {
                return getZugaraListSuberi(zugaras, ReelController.Reel_ID.Center, stopComa);
            }
            else
            {
                return ControlTable.getcenterTbl(ControlTable.centerTblName.m_replay);
            }
        }

        private static int rightStop(int stopComa)
        {
            if (IsRedStop() == true)
            {
                return getZugaraListSuberi(zugaras, ReelController.Reel_ID.Right, stopComa);
            }
            else
            {
                return ControlTable.getrightTbl(ControlTable.rightTblName.m_replay);
            }
        }

        public override int CLR_stop(int stopComa)
        {
            return rightStop(stopComa);
        }

        public override int CL_stop(int stopComa)
        {
            return leftStop(stopComa);
        }

        public override int CRL_stop(int stopComa)
        {
            return leftStop(stopComa);
        }

        public override int CR_stop(int stopComa)
        {
            return rightStop(stopComa);
        }

        public override int C_stop(int stopComa)
        {
            return centerStop(stopComa);
        }

        public override int LCR_stop(int stopComa)
        {
            return rightStop(stopComa);
        }

        public override int LC_stop(int stopComa)
        {
            return centerStop(stopComa);
        }

        public override int LRC_stop(int stopComa)
        {
            return centerStop(stopComa);
        }

        public override int LR_stop(int stopComa)
        {
            return rightStop(stopComa);
        }

        public override int L_stop(int stopComa)
        {
            return ControlTable.getleftTbl(ControlTable.leftTblName.m_replay);
        }

        public override int RCL_stop(int stopComa)
        {
            return leftStop(stopComa);
        }

        public override int RC_stop(int stopComa)
        {
            return centerStop(stopComa);
        }

        public override int RLC_stop(int stopComa)
        {
            return centerStop(stopComa);
        }

        public override int RL_stop(int stopComa)
        {
            return leftStop(stopComa);
        }

        public override int R_stop(int stopComa)
        {
            return rightStop(stopComa);
        }

        private static bool IsRedStop()
        {
            switch (Mn.stopSeq)
            {
                case Mn.StopSeq._12x:
                case Mn.StopSeq._1x2:
                    return (getZugara(ReelController.Reel_ID.Left, Pos.M) == ReelData.Zugara.RedSeven);
                case Mn.StopSeq._21x:
                case Mn.StopSeq._x12:
                    return (getZugara(ReelController.Reel_ID.Center, Pos.M) == ReelData.Zugara.RedSeven);
                case Mn.StopSeq._x21:
                case Mn.StopSeq._2x1:
                    return (getZugara(ReelController.Reel_ID.Right, Pos.M) == ReelData.Zugara.RedSeven);
                case Mn.StopSeq._123:
                case Mn.StopSeq._213:
                    return ((getZugara(ReelController.Reel_ID.Left, Pos.M) == ReelData.Zugara.RedSeven) && (getZugara(ReelController.Reel_ID.Center, Pos.M) == ReelData.Zugara.RedSeven));
                case Mn.StopSeq._312:
                case Mn.StopSeq._321:
                    return ((getZugara(ReelController.Reel_ID.Right, Pos.M) == ReelData.Zugara.RedSeven) && (getZugara(ReelController.Reel_ID.Center, Pos.M) == ReelData.Zugara.RedSeven));
                case Mn.StopSeq._132:
                case Mn.StopSeq._231:
                    return ((getZugara(ReelController.Reel_ID.Left, Pos.M) == ReelData.Zugara.RedSeven) && (getZugara(ReelController.Reel_ID.Right, Pos.M) == ReelData.Zugara.RedSeven));
                default:
                    return true;
            }
        }
    }

    /// <summary>
    /// SBigリプレイ制御データ
    /// </summary>
    class SBigReplay : ControlData
    {
        private static ReelData.Zugara[] zugaras =
        {
            ReelData.Zugara.WhiteSeven,
            ReelData.Zugara.Replay,
        };

        private static int leftStop(int stopComa)
        {
            if (IsWhiteStop() == true)
            {
                return getZugaraListSuberi(zugaras, ReelController.Reel_ID.Left, stopComa);
            }
            else
            {
                return ControlTable.getleftTbl(ControlTable.leftTblName.m_replay);
            }
        }

        private static int centerStop(int stopComa)
        {
            if (IsWhiteStop() == true)
            {
                return getZugaraListSuberi(zugaras, ReelController.Reel_ID.Center, stopComa);
            }
            else
            {
                return ControlTable.getcenterTbl(ControlTable.centerTblName.m_replay);
            }
        }

        private static int rightStop(int stopComa)
        {
            if (IsWhiteStop() == true)
            {
                return getZugaraListSuberi(zugaras, ReelController.Reel_ID.Right, stopComa);
            }
            else
            {
                return ControlTable.getrightTbl(ControlTable.rightTblName.m_replay);
            }
        }

        public override int CLR_stop(int stopComa)
        {
            return rightStop(stopComa);
        }

        public override int CL_stop(int stopComa)
        {
            return leftStop(stopComa);
        }

        public override int CRL_stop(int stopComa)
        {
            return leftStop(stopComa);
        }

        public override int CR_stop(int stopComa)
        {
            return rightStop(stopComa);
        }

        public override int C_stop(int stopComa)
        {
            return centerStop(stopComa);
        }

        public override int LCR_stop(int stopComa)
        {
            return rightStop(stopComa);
        }

        public override int LC_stop(int stopComa)
        {
            return centerStop(stopComa);
        }

        public override int LRC_stop(int stopComa)
        {
            return centerStop(stopComa);
        }

        public override int LR_stop(int stopComa)
        {
            return rightStop(stopComa);
        }

        public override int L_stop(int stopComa)
        {
            return ControlTable.getleftTbl(ControlTable.leftTblName.m_replay);
        }

        public override int RCL_stop(int stopComa)
        {
            return leftStop(stopComa);
        }

        public override int RC_stop(int stopComa)
        {
            return centerStop(stopComa);
        }

        public override int RLC_stop(int stopComa)
        {
            return centerStop(stopComa);
        }

        public override int RL_stop(int stopComa)
        {
            return leftStop(stopComa);
        }

        public override int R_stop(int stopComa)
        {
            return rightStop(stopComa);
        }

        private static bool IsWhiteStop()
        {
            switch (Mn.stopSeq)
            {
                case Mn.StopSeq._12x:
                case Mn.StopSeq._1x2:
                    return (getZugara(ReelController.Reel_ID.Left, Pos.M) == ReelData.Zugara.WhiteSeven);
                case Mn.StopSeq._21x:
                case Mn.StopSeq._x12:
                    return (getZugara(ReelController.Reel_ID.Center, Pos.M) == ReelData.Zugara.WhiteSeven);
                case Mn.StopSeq._x21:
                case Mn.StopSeq._2x1:
                    return (getZugara(ReelController.Reel_ID.Right, Pos.M) == ReelData.Zugara.WhiteSeven);
                case Mn.StopSeq._123:
                case Mn.StopSeq._213:
                    return ((getZugara(ReelController.Reel_ID.Left, Pos.M) == ReelData.Zugara.WhiteSeven) && (getZugara(ReelController.Reel_ID.Center, Pos.M) == ReelData.Zugara.WhiteSeven));
                case Mn.StopSeq._312:
                case Mn.StopSeq._321:
                    return ((getZugara(ReelController.Reel_ID.Right, Pos.M) == ReelData.Zugara.WhiteSeven) && (getZugara(ReelController.Reel_ID.Center, Pos.M) == ReelData.Zugara.WhiteSeven));
                case Mn.StopSeq._132:
                case Mn.StopSeq._231:
                    return ((getZugara(ReelController.Reel_ID.Left, Pos.M) == ReelData.Zugara.WhiteSeven) && (getZugara(ReelController.Reel_ID.Right, Pos.M) == ReelData.Zugara.WhiteSeven));
                default:
                    return true;
            }
        }
    }

    /// <summary>
    /// 123ベル
    /// </summary>
    class Bell_123 : ControlData
    {
        public override int CLR_stop(int stopComa)
        {
            return ControlTable.getrightTbl(ControlTable.rightTblName.h_bell);
        }

        public override int CL_stop(int stopComa)
        {
            return ControlTable.getleftTbl(ControlTable.leftTblName.m_replay);
        }

        public override int CRL_stop(int stopComa)
        {
            return ControlTable.getleftTbl(ControlTable.leftTblName.m_replay);
        }

        public override int CR_stop(int stopComa)
        {
            return ControlTable.getrightTbl(ControlTable.rightTblName.h_bell);
        }

        public override int C_stop(int stopComa)
        {
            return ControlTable.getcenterTbl(ControlTable.centerTblName.l_bell);
        }

        public override int LCR_stop(int stopComa)
        {
            return ControlTable.getrightTbl(ControlTable.rightTblName.m_replay);
        }

        public override int LC_stop(int stopComa)
        {
            return ControlTable.getcenterTbl(ControlTable.centerTblName.m_bell);
        }

        public override int LRC_stop(int stopComa)
        {
            return ControlTable.getcenterTbl(ControlTable.centerTblName.l_bell);
        }

        public override int LR_stop(int stopComa)
        {
            return ControlTable.getrightTbl(ControlTable.rightTblName.h_bell);
        }

        public override int L_stop(int stopComa)
        {
            return ControlTable.getleftTbl(ControlTable.leftTblName.m_replay);
        }

        public override int RCL_stop(int stopComa)
        {
            return ControlTable.getleftTbl(ControlTable.leftTblName.m_replay);
        }

        public override int RC_stop(int stopComa)
        {
            return ControlTable.getcenterTbl(ControlTable.centerTblName.l_bell);
        }

        public override int RLC_stop(int stopComa)
        {
            return ControlTable.getcenterTbl(ControlTable.centerTblName.l_bell);
        }

        public override int RL_stop(int stopComa)
        {
            return ControlTable.getleftTbl(ControlTable.leftTblName.m_replay);
        }

        public override int R_stop(int stopComa)
        {
            return ControlTable.getrightTbl(ControlTable.rightTblName.h_bell);
        }
    }

    /// <summary>
    /// 132ベル
    /// </summary>
    class Bell_132 : ControlData
    {
        public override int CLR_stop(int stopComa)
        {
            return ControlTable.getrightTbl(ControlTable.rightTblName.h_bell);
        }

        public override int CL_stop(int stopComa)
        {
            return ControlTable.getleftTbl(ControlTable.leftTblName.m_replay);
        }

        public override int CRL_stop(int stopComa)
        {
            return ControlTable.getleftTbl(ControlTable.leftTblName.m_replay);
        }

        public override int CR_stop(int stopComa)
        {
            return ControlTable.getrightTbl(ControlTable.rightTblName.h_bell);
        }

        public override int C_stop(int stopComa)
        {
            return ControlTable.getcenterTbl(ControlTable.centerTblName.l_bell);
        }

        public override int LCR_stop(int stopComa)
        {
            return ControlTable.getrightTbl(ControlTable.rightTblName.h_bell);
        }

        public override int LC_stop(int stopComa)
        {
            return ControlTable.getcenterTbl(ControlTable.centerTblName.l_bell);
        }

        public override int LRC_stop(int stopComa)
        {
            return ControlTable.getcenterTbl(ControlTable.centerTblName.m_bell);
        }

        public override int LR_stop(int stopComa)
        {
            return ControlTable.getrightTbl(ControlTable.rightTblName.m_replay);
        }

        public override int L_stop(int stopComa)
        {
            return ControlTable.getleftTbl(ControlTable.leftTblName.m_replay);
        }

        public override int RCL_stop(int stopComa)
        {
            return ControlTable.getleftTbl(ControlTable.leftTblName.m_replay);
        }

        public override int RC_stop(int stopComa)
        {
            return ControlTable.getcenterTbl(ControlTable.centerTblName.l_bell);
        }

        public override int RLC_stop(int stopComa)
        {
            return ControlTable.getcenterTbl(ControlTable.centerTblName.l_bell);
        }

        public override int RL_stop(int stopComa)
        {
            return ControlTable.getleftTbl(ControlTable.leftTblName.m_replay);
        }

        public override int R_stop(int stopComa)
        {
            return ControlTable.getrightTbl(ControlTable.rightTblName.h_bell);
        }
    }

    /// <summary>
    /// 213ベル
    /// </summary>
    class Bell_213 : ControlData
    {
        public override int CL_stop(int stopComa)
        {
            return ControlTable.getleftTbl(ControlTable.leftTblName.m_bell);
        }

        public override int CRL_stop(int stopComa)
        {
            return ControlTable.getleftTbl(ControlTable.leftTblName.m_suika);
        }
        public override int L_stop(int stopComa)
        {
            return ControlTable.getleftTbl(ControlTable.leftTblName.m_replay);
        }

        public override int RCL_stop(int stopComa)
        {
            return ControlTable.getleftTbl(ControlTable.leftTblName.m_replay);
        }

        public override int RL_stop(int stopComa)
        {
            return ControlTable.getleftTbl(ControlTable.leftTblName.m_replay);
        }

        public override int C_stop(int stopComa)
        {
            return ControlTable.getcenterTbl(ControlTable.centerTblName.m_bell);
        }

        public override int LC_stop(int stopComa)
        {
            return ControlTable.getcenterTbl(ControlTable.centerTblName.l_bell);
        }

        public override int LRC_stop(int stopComa)
        {
            return ControlTable.getcenterTbl(ControlTable.centerTblName.l_bell);
        }

        public override int RC_stop(int stopComa)
        {
            return ControlTable.getcenterTbl(ControlTable.centerTblName.l_bell);
        }

        public override int RLC_stop(int stopComa)
        {
            return ControlTable.getcenterTbl(ControlTable.centerTblName.l_bell);
        }

        public override int LCR_stop(int stopComa)
        {
            return ControlTable.getrightTbl(ControlTable.rightTblName.h_bell);
        }

        public override int LR_stop(int stopComa)
        {
            return ControlTable.getrightTbl(ControlTable.rightTblName.h_bell);
        }

        public override int R_stop(int stopComa)
        {
            return ControlTable.getrightTbl(ControlTable.rightTblName.h_bell);
        }

        public override int CLR_stop(int stopComa)
        {
            return ControlTable.getrightTbl(ControlTable.rightTblName.m_bell);
        }

        public override int CR_stop(int stopComa)
        {
            return ControlTable.getrightTbl(ControlTable.rightTblName.m_replay);
        }
    }

    /// <summary>
    /// 213ベル
    /// </summary>
    class Bell_312 : ControlData
    {
        public override int CL_stop(int stopComa)
        {
            return ControlTable.getleftTbl(ControlTable.leftTblName.m_suika);
        }

        public override int CRL_stop(int stopComa)
        {
            return ControlTable.getleftTbl(ControlTable.leftTblName.m_bell);
        }
        public override int L_stop(int stopComa)
        {
            return ControlTable.getleftTbl(ControlTable.leftTblName.m_replay);
        }

        public override int RCL_stop(int stopComa)
        {
            return ControlTable.getleftTbl(ControlTable.leftTblName.m_replay);
        }

        public override int RL_stop(int stopComa)
        {
            return ControlTable.getleftTbl(ControlTable.leftTblName.m_replay);
        }

        public override int C_stop(int stopComa)
        {
            return ControlTable.getcenterTbl(ControlTable.centerTblName.m_bell);
        }

        public override int LC_stop(int stopComa)
        {
            return ControlTable.getcenterTbl(ControlTable.centerTblName.l_bell);
        }

        public override int LRC_stop(int stopComa)
        {
            return ControlTable.getcenterTbl(ControlTable.centerTblName.l_bell);
        }

        public override int RC_stop(int stopComa)
        {
            return ControlTable.getcenterTbl(ControlTable.centerTblName.l_bell);
        }

        public override int RLC_stop(int stopComa)
        {
            return ControlTable.getcenterTbl(ControlTable.centerTblName.l_bell);
        }

        public override int LCR_stop(int stopComa)
        {
            return ControlTable.getrightTbl(ControlTable.rightTblName.h_bell);
        }

        public override int LR_stop(int stopComa)
        {
            return ControlTable.getrightTbl(ControlTable.rightTblName.h_bell);
        }

        public override int R_stop(int stopComa)
        {
            return ControlTable.getrightTbl(ControlTable.rightTblName.h_bell);
        }

        public override int CLR_stop(int stopComa)
        {
            return ControlTable.getrightTbl(ControlTable.rightTblName.m_replay);
        }

        public override int CR_stop(int stopComa)
        {
            return ControlTable.getrightTbl(ControlTable.rightTblName.m_bell);
        }
    }

    /// <summary>
    /// 321ベル
    /// </summary>
    class Bell_321 : ControlData
    {
        public override int CL_stop(int stopComa)
        {
            return ControlTable.getleftTbl(ControlTable.leftTblName.m_replay);
        }

        public override int CRL_stop(int stopComa)
        {
            return ControlTable.getleftTbl(ControlTable.leftTblName.m_replay);
        }
        public override int L_stop(int stopComa)
        {
            return ControlTable.getleftTbl(ControlTable.leftTblName.m_replay);
        }

        public override int RCL_stop(int stopComa)
        {
            return ControlTable.getleftTbl(ControlTable.leftTblName.m_bell);
        }

        public override int RL_stop(int stopComa)
        {
            return ControlTable.getleftTbl(ControlTable.leftTblName.m_suika);
        }

        public override int C_stop(int stopComa)
        {
            return ControlTable.getcenterTbl(ControlTable.centerTblName.l_bell);
        }

        public override int LC_stop(int stopComa)
        {
            return ControlTable.getcenterTbl(ControlTable.centerTblName.l_bell);
        }

        public override int LRC_stop(int stopComa)
        {
            return ControlTable.getcenterTbl(ControlTable.centerTblName.l_bell);
        }

        public override int RC_stop(int stopComa)
        {
            return ControlTable.getcenterTbl(ControlTable.centerTblName.m_bell);
        }

        public override int RLC_stop(int stopComa)
        {
            return ControlTable.getcenterTbl(ControlTable.centerTblName.cherry);
        }

        public override int LCR_stop(int stopComa)
        {
            return ControlTable.getrightTbl(ControlTable.rightTblName.h_bell);
        }

        public override int LR_stop(int stopComa)
        {
            return ControlTable.getrightTbl(ControlTable.rightTblName.h_bell);
        }

        public override int R_stop(int stopComa)
        {
            return ControlTable.getrightTbl(ControlTable.rightTblName.m_bell);
        }

        public override int CLR_stop(int stopComa)
        {
            return ControlTable.getrightTbl(ControlTable.rightTblName.h_bell);
        }

        public override int CR_stop(int stopComa)
        {
            return ControlTable.getrightTbl(ControlTable.rightTblName.h_bell);
        }
    }

    /// <summary>
    /// 231ベル
    /// </summary>
    class Bell_231 : ControlData
    {
        public override int CL_stop(int stopComa)
        {
            return ControlTable.getleftTbl(ControlTable.leftTblName.m_replay);
        }

        public override int CRL_stop(int stopComa)
        {
            return ControlTable.getleftTbl(ControlTable.leftTblName.m_replay);
        }
        public override int L_stop(int stopComa)
        {
            return ControlTable.getleftTbl(ControlTable.leftTblName.m_replay);
        }

        public override int RCL_stop(int stopComa)
        {
            return ControlTable.getleftTbl(ControlTable.leftTblName.m_suika);
        }

        public override int RL_stop(int stopComa)
        {
            return ControlTable.getleftTbl(ControlTable.leftTblName.m_bell);
        }

        public override int C_stop(int stopComa)
        {
            return ControlTable.getcenterTbl(ControlTable.centerTblName.l_bell);
        }

        public override int LC_stop(int stopComa)
        {
            return ControlTable.getcenterTbl(ControlTable.centerTblName.l_bell);
        }

        public override int LRC_stop(int stopComa)
        {
            return ControlTable.getcenterTbl(ControlTable.centerTblName.l_bell);
        }

        public override int RC_stop(int stopComa)
        {
            return ControlTable.getcenterTbl(ControlTable.centerTblName.cherry);
        }

        public override int RLC_stop(int stopComa)
        {
            return ControlTable.getcenterTbl(ControlTable.centerTblName.m_bell);
        }

        public override int LCR_stop(int stopComa)
        {
            return ControlTable.getrightTbl(ControlTable.rightTblName.h_bell);
        }

        public override int LR_stop(int stopComa)
        {
            return ControlTable.getrightTbl(ControlTable.rightTblName.h_bell);
        }

        public override int R_stop(int stopComa)
        {
            return ControlTable.getrightTbl(ControlTable.rightTblName.m_bell);
        }

        public override int CLR_stop(int stopComa)
        {
            return ControlTable.getrightTbl(ControlTable.rightTblName.h_bell);
        }

        public override int CR_stop(int stopComa)
        {
            return ControlTable.getrightTbl(ControlTable.rightTblName.h_bell);
        }
    }

    /// <summary>
    /// ハズレ
    /// </summary>
    class Hazure : ControlData
    {
        public override int CL_stop(int stopComa)
        {
            return ControlTable.getleftTbl(ControlTable.leftTblName.m_replay);
        }

        public override int CRL_stop(int stopComa)
        {
            return ControlTable.getleftTbl(ControlTable.leftTblName.m_replay);
        }
        public override int L_stop(int stopComa)
        {
            return ControlTable.getleftTbl(ControlTable.leftTblName.m_replay);
        }

        public override int RCL_stop(int stopComa)
        {
            return ControlTable.getleftTbl(ControlTable.leftTblName.m_replay);
        }

        public override int RL_stop(int stopComa)
        {
            return ControlTable.getleftTbl(ControlTable.leftTblName.m_replay);
        }

        public override int C_stop(int stopComa)
        {
            return ControlTable.getcenterTbl(ControlTable.centerTblName.cherry);
        }

        public override int LC_stop(int stopComa)
        {
            return ControlTable.getcenterTbl(ControlTable.centerTblName.cherry);
        }

        public override int LRC_stop(int stopComa)
        {
            return ControlTable.getcenterTbl(ControlTable.centerTblName.cherry);
        }

        public override int RC_stop(int stopComa)
        {
            return ControlTable.getcenterTbl(ControlTable.centerTblName.cherry);
        }

        public override int RLC_stop(int stopComa)
        {
            return ControlTable.getcenterTbl(ControlTable.centerTblName.cherry);
        }

        public override int LCR_stop(int stopComa)
        {
            return ControlTable.getrightTbl(ControlTable.rightTblName.m_bell);
        }

        public override int LR_stop(int stopComa)
        {
            return ControlTable.getrightTbl(ControlTable.rightTblName.m_bell);
        }

        public override int R_stop(int stopComa)
        {
            return ControlTable.getrightTbl(ControlTable.rightTblName.m_bell);
        }

        public override int CLR_stop(int stopComa)
        {
            return ControlTable.getrightTbl(ControlTable.rightTblName.m_bell);
        }

        public override int CR_stop(int stopComa)
        {
            return ControlTable.getrightTbl(ControlTable.rightTblName.m_bell);
        }
    }

    class Payout
    {
        private static List<List<ReelData.Zugara>> suikaList = new List<List<ReelData.Zugara>>()
        { 
            new List<ReelData.Zugara>()
            {
                ReelData.Zugara.BellA,
                ReelData.Zugara.Homura,
                ReelData.Zugara.RedSeven,
            },
            new List<ReelData.Zugara>()
            {
                ReelData.Zugara.Suika
            },
            new List<ReelData.Zugara>()
            {
                ReelData.Zugara.WhiteSeven,
                ReelData.Zugara.Blank
            },
        };

        private static List<List<ReelData.Zugara>> bellListA = new List<List<ReelData.Zugara>>()
        {
            new List<ReelData.Zugara>()
            {
                ReelData.Zugara.Replay,
            },
            new List<ReelData.Zugara>()
            {
                ReelData.Zugara.BellA,
                ReelData.Zugara.BellB,
            },
            new List<ReelData.Zugara>()
            {
                ReelData.Zugara.Replay,
            },
        };

        private static List<List<ReelData.Zugara>> bellListB = new List<List<ReelData.Zugara>>()
        {
            new List<ReelData.Zugara>()
            {
                ReelData.Zugara.BellA,
                ReelData.Zugara.BellB,
            },
            new List<ReelData.Zugara>()
            {
                ReelData.Zugara.BellA,
                ReelData.Zugara.BellB,
            },
            new List<ReelData.Zugara>()
            {
                ReelData.Zugara.BellA,
                ReelData.Zugara.BellB,
            },
        };

        private static List<List<ReelData.Zugara>> cherryList = new List<List<ReelData.Zugara>>()
        {
            new List<ReelData.Zugara>()
            {
                ReelData.Zugara.Bar
            },
            new List<ReelData.Zugara>()
            {
                ReelData.Zugara.Bar,
                ReelData.Zugara.BellA,
                ReelData.Zugara.BellB,
                ReelData.Zugara.Blank,
                ReelData.Zugara.Cherry,
                ReelData.Zugara.Homura,
                ReelData.Zugara.RedSeven,
                ReelData.Zugara.Replay,
                ReelData.Zugara.Suika,
                ReelData.Zugara.WhiteSeven,
            },
            new List<ReelData.Zugara>()
            {
                ReelData.Zugara.Bar,
                ReelData.Zugara.BellA,
                ReelData.Zugara.BellB,
                ReelData.Zugara.Blank,
                ReelData.Zugara.Cherry,
                ReelData.Zugara.Homura,
                ReelData.Zugara.RedSeven,
                ReelData.Zugara.Replay,
                ReelData.Zugara.Suika,
                ReelData.Zugara.WhiteSeven,
            },
        };

        private static bool IsStopCheck(List<List<ReelData.Zugara>> list)
        {
            int index = 0;
            List<bool> muchList = new List<bool>();
            ReelData.Zugara[] stopZugara = new ReelData.Zugara[]
            {
                ReelData.LeftReelArray[Mn.leftReel.getComa()],
                ReelData.CenterReelArray[Mn.centerReel.getComa()],
                ReelData.RightReelArray[Mn.rightReel.getComa()],
            };

            foreach (var item in list)
            {
                if (item.Find(x => x == stopZugara[index]) == stopZugara[index])
                {
                    muchList.Add(true);
                }
                else
                {
                    muchList.Add(false);
                }
                index++;
            }

            // リストに一つでもfalseがあればfalseを返す（検索結果0件）
            return (muchList.FindAll(x => x == false).Count == 0);
        }


        /// <summary>
        /// 払出枚数を取得する
        /// </summary>
        public static bool IsHitCheck()
        {
            switch (Sim.DdmVariable.FrtCode)
            {
                case Sim.FRT_CODE.Chance:
                    return true;
                case Sim.FRT_CODE.KyoChe:
                case Sim.FRT_CODE.JakuChe:
                    return IsStopCheck(cherryList);
                case Sim.FRT_CODE.Suika:
                    return IsStopCheck(suikaList);
                case Sim.FRT_CODE.HomuraReplay:
                case Sim.FRT_CODE.RegReplay:
                case Sim.FRT_CODE.BigReplay:
                case Sim.FRT_CODE.SBigReplay:
                    return false;
                case Sim.FRT_CODE.Bell_123:
                case Sim.FRT_CODE.Bell_132:
                    return IsStopCheck(bellListA);
                case Sim.FRT_CODE.Bell_213:
                case Sim.FRT_CODE.Bell_231:
                case Sim.FRT_CODE.Bell_321:
                case Sim.FRT_CODE.Bell_312:
                    return IsStopCheck(bellListB);
                case Sim.FRT_CODE.Hazure:
                    return false;
                default:
                    return false;
            }
        }

    }
}
