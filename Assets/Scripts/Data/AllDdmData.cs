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
    [DataContract]
    public class AllDdmData
    {
        [DataMember]
        public DdmData[] ddmData { get; set; }

#if UNITY_EDITOR
        public static string msg { get; set; }
#endif

        public Int16 Lot(int sheetNum, int tableRow, int tableCol, int offsetNum)
        {

            if (sheetNum >= ddmData.Length)
            {
                Console.WriteLine("存在しないシートです。");
                return 0;
            }

            if (tableRow >= ddmData[sheetNum].ddmTables.GetLength(0))
            {
                Console.WriteLine("指定行：" + tableRow);
                Console.WriteLine("最大行：" + ddmData[sheetNum].ddmTables.GetLength(0));
                Console.WriteLine("存在しないテーブル行です。");
                return 0;
            }

            if (tableCol >= ddmData[sheetNum].ddmTables.GetLength(1))
            {
                Console.WriteLine("存在しないテーブル列です。");
                return 0;
            }

            if (offsetNum >= ddmData[sheetNum].ddmTables[tableRow,tableCol].recodes.Length)
            {
                Console.WriteLine("存在しないオフセットです。");
                return 0;
            }

#if !UNITY_EDITOR
            Console.Write(ddmData[sheetNum].lotName + ':');
            Console.Write(ddmData[sheetNum].lotLabel + ':');
            Console.Write(ddmData[sheetNum].ddmTables[tableRow, tableCol].xTableIndexName + ':');
            Console.Write(ddmData[sheetNum].ddmTables[tableRow, tableCol].yTableIndexName + ':');
            Console.Write(ddmData[sheetNum].ddmTables[tableRow, tableCol].recodes[offsetNum].lotIndexName + ':');
#else
            msg = ddmData[sheetNum].lotName + ':';
            msg += ddmData[sheetNum].lotLabel + ':';
            msg += ddmData[sheetNum].ddmTables[tableRow, tableCol].xTableIndexName + ':';
            msg += ddmData[sheetNum].ddmTables[tableRow, tableCol].yTableIndexName + ':';
            msg += ddmData[sheetNum].ddmTables[tableRow, tableCol].recodes[offsetNum].lotIndexName + ':';
#endif

            Int16 result = ddmData[sheetNum].ddmTables[tableRow, tableCol].recodes[offsetNum].Lot();
       
            return result;
        }
    }

    /// <summary>
    /// 抽せんデータ保持用クラス
    /// </summary>
    [DataContract]
    public class DdmData
    {
        [DataMember]
        public string lotName { get; set; }

        [DataMember]
        public string lotLabel { get; set; }

        [DataMember]
        public DdmTable[,] ddmTables { get; set; }
    }

    /// <summary>
    /// 一つの抽せんテーブルデータを保持するデータクラス
    /// </summary>
    [DataContract]
    public class DdmTable
    {
        [DataMember]
        public DdmRecodes[] recodes { get; set; }

        [DataMember]
        public string xTableIndexName { get; set; }

        [DataMember]
        public string yTableIndexName { get; set; }
    }

    /// <summary>
    /// 一つの抽せんデータを保持するデータクラス
    /// </summary>
    [DataContract]
    public class DdmRecodes
    {
        static System.Random random = new System.Random();

        [DataMember]
        public DdmRecode[] data { get; set; }

        [DataMember]
        public string lotIndexName { get; set; }

        public Int16 Lot()
        {
            Int16 SumCheck = 0;
            Int16 Ret = 0;
            foreach (var item in data)
            {
                SumCheck += item.tisu;
            }

            if (SumCheck != 256)
            {
                Console.WriteLine("置数の合計が256になっていません。");
                return -1;
            }

            int Rand = random.Next(256);
            foreach (var item in data)
            {
                Rand -= item.tisu;
                if (Rand < 0)
                {
                    Ret = item.result;

#if !UNITY_EDITOR
                    Console.WriteLine(item.resultName);
#else
                    AllDdmData.msg += item.resultName;
                    Debug.Log(AllDdmData.msg);
#endif
                    break;
                }
            }

            return Ret;
        }
    }

    /// <summary>
    /// 結果と置数の1件データ
    /// </summary>
    [DataContract]
    public class DdmRecode
    {
        [DataMember]
        public Int16 result { get; set; }

        [DataMember]
        public Int16 tisu { get; set; }

        [DataMember]
        public string resultName { get; set; }
    }
}
