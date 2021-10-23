using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim
{
    class DdmMain
    {
        public static AllDdmData allDdmData = new AllDdmData();        // jsonから読み出す
        public static FrtData frtData = new FrtData();                 // jsonから読みだす
        public SimData simData = new SimData();

        string path = Directory.GetCurrentDirectory() + @"\log.csv";
        string outText;

        // コンストラクタ
        public DdmMain()
        {
            // エクセルから吐き出された抽せんデータjsonファイルをカレントから読み込む
            string json = File.ReadAllText(Directory.GetCurrentDirectory() + @"\ddmdata.json");
            allDdmData = JsonConvert.DeserializeObject<AllDdmData>(json);
            json = File.ReadAllText(Directory.GetCurrentDirectory() + @"\frtData.json");
            frtData = JsonConvert.DeserializeObject<FrtData>(json);
        }

        public void Init()
        {
            // エクセルから吐き出された抽せんデータjsonファイルをカレントから読み込む
            string json = File.ReadAllText(Directory.GetCurrentDirectory() + @"\ddmdata.json");
            allDdmData = JsonConvert.DeserializeObject<AllDdmData>(json);
            json = File.ReadAllText(Directory.GetCurrentDirectory() + @"\frtData.json");
            frtData = JsonConvert.DeserializeObject<FrtData>(json);
            DdmVariable.In = 0;
            DdmVariable.Out = 0;
            FrtData.ResultFrtItem = new FrtItem { hitType = HitType.frt, pay = 0 }; // 初期化
            Data.Totalization.Reset();
        }

        public void OutputData()
        {
            string json = JsonConvert.SerializeObject(frtData, Formatting.Indented);
            string stCurrentDir = Directory.GetCurrentDirectory() + @"\frtData.json";
            File.WriteAllText(stCurrentDir, json);
        }

        public void LeverDdmStart()
        {
            Init();

            if(simData.LogFlg == true)
            {
                using (var sw = new StreamWriter(path, true, Encoding.GetEncoding("shift_jis")))
                {
                    sw.Write(GetLogHeader());

                    for (UInt64 i = 0; i < simData.SimNum; i++)
                    {
                        AllLever.Flow();
                        sw.Write(GetLogData());
                    }
                }
            }
            else
            {
                for (UInt64 i = 0; i < simData.SimNum; i++)
                {
                    AllLever.Flow();
                }
            }
        }

        /// <summary>
        /// ログヘッダー
        /// </summary>
        private string GetLogHeader()
        {
            outText = "";
            outText = outText + "有利区間G数,";
            outText = outText + "MYカウンター,";
            outText = outText + "設定値,";
            outText = outText + "In枚数,";
            outText = outText + "Out枚数,";
            outText = outText + "条件装置,";
            outText = outText + "出玉状態,";
            outText = outText + "開始時出玉状態,";
            outText = outText + "終了時出玉状態,";
            outText = outText + "高確率G数,";
            outText = outText + "ボーナスゲーム数,";
            outText = outText + "状態,";
            outText = outText + "継続ストック,";
            outText = outText + "継続率,";
            outText = outText + "継続ランク,";
            outText = outText + "前兆G数,";
            outText = outText + "RushG数" + "\n";
            return outText;
        }

        /// <summary>
        /// ログデータ
        /// </summary>
        private string GetLogData()
        {
            outText = "";
            outText = outText + DdmVariable.YuuriGame + ",";
            outText = outText + DdmVariable.MYCounter + ",";
            outText = outText + DdmVariable.Settei + ",";
            outText = outText + DdmVariable.In + ",";
            outText = outText + DdmVariable.Out + ",";
            outText = outText + DdmVariable.FrtCode + ",";
            outText = outText + DdmVariable.DdmMode + ",";
            outText = outText + DdmVariable.StartDdmMode + ",";
            outText = outText + DdmVariable.EndDdmMode + ",";
            outText = outText + DdmVariable.HighGame + ",";
            outText = outText + DdmVariable.BnsGame + ",";
            outText = outText + DdmVariable.NmlSts + ",";
            outText = outText + DdmVariable.Stock + ",";
            outText = outText + DdmVariable.AtMode + ",";
            outText = outText + DdmVariable.AtSts + ",";
            outText = outText + DdmVariable.ZenchoGame + ",";
            outText = outText + DdmVariable.RushGame + "\n";
            return outText;
        }

        public class SimData
        {
            public UInt64 SimNum = 0;   // シミュレートG数
            public bool LogFlg = false;     // ログの出力
        }


    }
}
