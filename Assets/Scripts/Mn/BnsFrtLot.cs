using System;
using System.Collections.Generic;
using System.Text;
using AutoMakeCode;

public class BnsFrtLot
{
    public Recode Lot(AutoMakeCode.Enum.Settei _settei, AutoMakeCode.Enum.Status _status, AutoMakeCode.Enum.RT _rt)
    {
        Recode result = new Recode();
            
        // 設定値で検索
        Data _data = data.Find((x)=>x.settei == _settei);

        // 状態とRTで検索
        Table _table = _data.tables.Find((x)=> x.stsCode == _status && x.rtCode == _rt);

        int rand = r.Next(0, 65536);

        foreach (var item in _table.recodes)
        {
            rand -= item.tisu;
            if(rand < 0)
            {
                // 当せんしたデータを返す
                return item;
            }
        }

        // エラーデータを返す
        return new Recode() { bnsCode = AutoMakeCode.Enum.BnsCode.Hazure, frtCode = AutoMakeCode.Enum.FrtCode.Hazure, tisu = -1 };
    }

    #region データ構成

    public List<Data> data;

    private Random r = new Random();

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public BnsFrtLot()
    {
        return;

        // SampleData作成
        //data = new List<Data>();
        //data.Add(new Data(AutoMakeCode.Enum.Settei._1));
        //data.Add(new Data(AutoMakeCode.Enum.Settei._2));
        //data.Add(new Data(AutoMakeCode.Enum.Settei._3));
        //data.Add(new Data(AutoMakeCode.Enum.Settei._4));
        //data.Add(new Data(AutoMakeCode.Enum.Settei._5));
        //data.Add(new Data(AutoMakeCode.Enum.Settei._6));
    }

    public class Data
    {
        public AutoMakeCode.Enum.Settei settei;
        public List<Table> tables;

        public Data(AutoMakeCode.Enum.Settei _settei)
        {
            //settei = _settei;
            //tables = new List<Table>();
            //tables.Add(new Table(AutoMakeCode.Enum.Status.BB, AutoMakeCode.Enum.RT._0));
            //tables.Add(new Table(AutoMakeCode.Enum.Status.BBStandby, AutoMakeCode.Enum.RT._0));
            //tables.Add(new Table(AutoMakeCode.Enum.Status.RB, AutoMakeCode.Enum.RT._0));
            //tables.Add(new Table(AutoMakeCode.Enum.Status.RBStandby, AutoMakeCode.Enum.RT._0));
            //tables.Add(new Table(AutoMakeCode.Enum.Status.Nml, AutoMakeCode.Enum.RT._0));
        }
    }

    public class Table
    {
        public AutoMakeCode.Enum.Status stsCode;
        public AutoMakeCode.Enum.RT rtCode;
        public List<Recode> recodes;

        public Table(AutoMakeCode.Enum.Status _stsCode, AutoMakeCode.Enum.RT _rtCode)
        {
            //stsCode = _stsCode;
            //rtCode = _rtCode;
            //recodes = new List<Recode>();
            //recodes.Add(new Recode { bnsCode = AutoMakeCode.Enum.BnsCode.BB, frtCode = AutoMakeCode.Enum.FrtCode.Suika, tisu = 51 });
            //recodes.Add(new Recode { bnsCode = AutoMakeCode.Enum.BnsCode.BB, frtCode = AutoMakeCode.Enum.FrtCode.Cherry, tisu = 51 });
            //recodes.Add(new Recode { bnsCode = AutoMakeCode.Enum.BnsCode.BB, frtCode = AutoMakeCode.Enum.FrtCode.Bell, tisu = 51 });
            //recodes.Add(new Recode { bnsCode = AutoMakeCode.Enum.BnsCode.BB, frtCode = AutoMakeCode.Enum.FrtCode.Replay, tisu = 51 });
            //recodes.Add(new Recode { bnsCode = AutoMakeCode.Enum.BnsCode.BB, frtCode = AutoMakeCode.Enum.FrtCode.Hazure, tisu = 51 });
            //recodes.Add(new Recode { bnsCode = AutoMakeCode.Enum.BnsCode.RB, frtCode = AutoMakeCode.Enum.FrtCode.Suika, tisu = 51 });
            //recodes.Add(new Recode { bnsCode = AutoMakeCode.Enum.BnsCode.RB, frtCode = AutoMakeCode.Enum.FrtCode.Cherry, tisu = 51 });
            //recodes.Add(new Recode { bnsCode = AutoMakeCode.Enum.BnsCode.RB, frtCode = AutoMakeCode.Enum.FrtCode.Bell, tisu = 51 });
            //recodes.Add(new Recode { bnsCode = AutoMakeCode.Enum.BnsCode.RB, frtCode = AutoMakeCode.Enum.FrtCode.Replay, tisu = 52 });
            //recodes.Add(new Recode { bnsCode = AutoMakeCode.Enum.BnsCode.RB, frtCode = AutoMakeCode.Enum.FrtCode.Hazure, tisu = 52 });
            //recodes.Add(new Recode { bnsCode = AutoMakeCode.Enum.BnsCode.Hazure, frtCode = AutoMakeCode.Enum.FrtCode.Suika, tisu = 1024 });
            //recodes.Add(new Recode { bnsCode = AutoMakeCode.Enum.BnsCode.Hazure, frtCode = AutoMakeCode.Enum.FrtCode.Cherry, tisu = 2048 });
            //recodes.Add(new Recode { bnsCode = AutoMakeCode.Enum.BnsCode.Hazure, frtCode = AutoMakeCode.Enum.FrtCode.Bell, tisu = 8978 });
            //recodes.Add(new Recode { bnsCode = AutoMakeCode.Enum.BnsCode.Hazure, frtCode = AutoMakeCode.Enum.FrtCode.Replay, tisu = 8978 });
            //recodes.Add(new Recode { bnsCode = AutoMakeCode.Enum.BnsCode.Hazure, frtCode = AutoMakeCode.Enum.FrtCode.Hazure, tisu = 43996 });
        }
    }

    public class Recode
    {
        public AutoMakeCode.Enum.BnsCode bnsCode;
        public AutoMakeCode.Enum.FrtCode frtCode;
        public int tisu;
    }

    #endregion
}
