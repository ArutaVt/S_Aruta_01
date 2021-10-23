using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if UNITY_EDITOR
using UnityEngine;
#endif

namespace Sim
{
    public enum RTType
    {
        infinity,   // 無限RT
        finite,     // 有限RT
    }

    public enum HitType
    {
        bonus,      // ボーナス
        replay,     // リプレイ
        frt,        // 小役
    }

    /// <summary>
    /// 条件装置データテーブル
    /// </summary>
    [DataContract]
    public class FrtData
    {
        static System.Random random = new System.Random();

        [DataMember]
        public FrtItem[] FrtItems;

        [DataMember]
        public static FrtItem ResultFrtItem;

        /// <summary>
        /// 条件装置抽せん
        /// </summary>
        public void Lot()
        {
            int Rand = random.Next(65535);

            foreach (var item in FrtItems)
            {
                Rand -= item.tisu;
                if(Rand < 0)
                {
                    ResultFrtItem = item;
#if !UNITY_EDITOR
                    Console.WriteLine("条件装置" + item.lowName);
#else
                    Debug.Log("条件装置:" + item.lowName);
#endif
                    break;
                }
            }
        }

        /// <summary>
        /// 停止抽せん
        /// </summary>
        public void PbLot()
        {
            int Rand = random.Next(7999);

            foreach (var item in ResultFrtItem.pbData)
            {
                Rand -= item.tisu;
                if(Rand < 0)
                {
                    ResultFrtItem.pay = item.pay;
                    ResultFrtItem.StopName = item.name;
                    Console.WriteLine("停止系" + item.name);
                    break;
                }
            }
        }
    }

    /// <summary>
    /// 条件装置データ
    /// </summary>
    [DataContract]
    public class FrtItem
    {
        [DataMember]
        public Int16 tisu;

        [DataMember]
        public Int16 lowResult;

        [DataMember]
        public Int16 highResult;

        [DataMember]
        public string lowName;

        [DataMember]
        public string highName;

        [DataMember]
        public RTData moveRT;

        [DataMember]
        public Int16 pay;

        [DataMember]
        public HitType hitType;

        [DataMember]
        public string StopName;

        [DataMember]
        //public Int16[,] pbData;
        public PBRecode[] pbData;
    }

    /// <summary>
    /// PBデータ
    /// </summary>
    public class PBRecode
    {
        public string name;
        public Int16 tisu;
        public Int16 pay;
    }


    /// <summary>
    /// RTのデータ
    /// </summary>
    [DataContract]
    public class RTData
    {
        public RTType type;         // 無限か有限か
        public Int16  RTGame;       // 有限の場合はG数が入る
        public string RTName;       // RTの名前
        public Int16  RTNum;        // RTの番号
        public RTData EndRT;        // 終了時のRT
    }


    /// <summary>
    /// 1種BB、2種BBのデータ
    /// </summary>
    [DataContract]
    public class BB
    {
        public Int16 BnsEndMedal;   // BB終了条件（越える払い出しで終了）
        public string RTName;       // RTの名前
        public Int16 RTNum;         // RTの番号
        public FrtData  frtData;  // BB中の条件装置テーブル
        public RTData EndRT;        // 終了時のRT
    }

    /// <summary>
    /// RBのデータ
    /// </summary>
    public class RB
    {
        public Int16 EndGames;      // 規定遊技回数
        public Int16 EndHits;       // 規定入賞回数
        public string RTName;       // RTの名前
        public Int16 RTNum;         // RT番号
        public FrtData frtData;   // RB中の条件装置テーブル
        public RTData EndRT;        // 終了時のRT
    }

    /// <summary>
    /// SB・CBのデータ
    /// </summary>
    public class SBCB
    {
        public Int16 RTNum;         // RTの名前
        public string RTName;       // RTの番号
        public FrtData frtData;  // SB中の条件装置テーブル
        public RTData EndRT;        // 終了時のRT
    }
}
