using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMLot
{
    public enum A1_Result
    {
        None,
        Normal,
        Yokoku_A,
        Yokoku_B,
        Yokoku_C,
        Late,
    }
    public A1_Result a1_Result;

    public enum A2_Result{ None, A, B, C }
    public enum A3_Result{ None, A, B, C }
    public enum A4_Result{ None, Hit }
    public enum A5_Result{ None, S1, S12, S123, S123_On, S123_Off }
    public A5_Result a5_Result;
    public enum A6_Result
    {
        nml,            // 点灯
        blackout,       // 消灯
        flash,          // 点滅
        yellow,         // 黄
        blue,           // 青
        red,            // 赤
        green,          // 緑
        yellow_flash,   // 黄フラッシュ
        blue_flash,     // 青フラッシュ
        red_flash,      // 赤フラッシュ
        green_flash,    // 緑フラッシュ
    }

    public enum A7_Result { None, Hit }

    private List<LotData> lotDatas;

    private string _lotname;

    public SMLot()
    {
        // SM抽せんデータ読み込み
        var inputString = Resources.Load<TextAsset>("Json/lotDatas");
        lotDatas = JsonConvert.DeserializeObject<List<LotData>>(inputString.text);
    }

    private int lot(int _no, int _table_X_Index, int _table_Y_Index, int _box_X_Index)
    {
        LotData recode = lotDatas.Find((x) => x.no == _no && x.table_X_Index == _table_X_Index && x.table_Y_Index == _table_Y_Index && x.box_X_Index == _box_X_Index);

        if(recode != null)
        {
            _lotname = recode.lotName;
            return recode.lot();
        }
        else
        {
            Debug.LogError("抽せん対象が見つかりませんでした。");
            return 0;
        }
    }

    /// <summary>
    /// 予告音抽せん
    /// </summary>
    /// <returns></returns>
    public A1_Result lotA1()
    {
        int table_X_Index = (int)Mn.bnsCode;
        int table_Y_Index = 0;
        int box_X_Index = (int)Mn.frtCode;
        if(Mn.hitBnsGameFlg == true) table_Y_Index = 1;
        else table_Y_Index = 0;
        A1_Result result = (A1_Result)lot(1, table_X_Index, table_Y_Index, box_X_Index);
        Debug.Log(_lotname + ":" + result);
        a1_Result = result;
        return result;
    }

    public A2_Result lotA2()
    {
        int table_X_Index = 0;
        int table_Y_Index = 0;
        int box_X_Index = (int)Mn.bnsCode;
        if (Mn.hitBnsGameFlg == true) table_Y_Index = 1;
        else table_Y_Index = 0;
        A2_Result result = (A2_Result)lot(2, table_X_Index, table_Y_Index, box_X_Index);
        Debug.Log(_lotname + ":" + result);
        return result;
    }

    public A3_Result lotA3()
    {
        int table_X_Index = (int)Mn.bnsCode;
        int table_Y_Index = 0;
        int box_X_Index = (int)Mn.frtCode;
        if (Mn.hitBnsGameFlg == true) table_Y_Index = 1;
        else table_Y_Index = 0;
        A3_Result result = (A3_Result)lot(3, table_X_Index, table_Y_Index, box_X_Index);
        Debug.Log(_lotname + ":" + result);
        return result;
    }

    public A4_Result lotA4()
    {
        int table_X_Index = 0;
        int table_Y_Index = 0;
        int box_X_Index = (int)Mn.bnsCode;
        if (Mn.hitBnsGameFlg == true) table_Y_Index = 1;
        else table_Y_Index = 0;
        A4_Result result = (A4_Result)lot(4, table_X_Index, table_Y_Index, box_X_Index);
        Debug.Log(_lotname + ":" + result);
        return result;
    }

    public A5_Result lotA5()
    {
        int table_X_Index = (int)a1_Result;
        int table_Y_Index = (int)Mn.bnsCode;
        int box_X_Index = (int)Mn.frtCode;
        A5_Result result = (A5_Result)lot(5, table_X_Index, table_Y_Index, box_X_Index);
        Debug.Log(_lotname + ":" + result);
        a5_Result = result;
        return result;
    }

    public A6_Result lotA6()
    {
        int table_X_Index = (int)Mn.bnsCode;
        int table_Y_Index = 0;
        int box_X_Index = (int)Mn.frtCode;
        if (Mn.hitBnsGameFlg == true) table_Y_Index = 1;
        else table_Y_Index = 0;
        A6_Result result = (A6_Result)lot(6, table_X_Index, table_Y_Index, box_X_Index);
        Debug.Log(_lotname + ":" + result);
        return result;
    }

    public A7_Result lotA7()
    {
        int table_X_Index = 0;
        int table_Y_Index = 0;
        int box_X_Index = (int)Mn.bnsCode;
        if (Mn.hitBnsGameFlg == true) table_Y_Index = 1;
        else table_Y_Index = 0;
        A7_Result result = (A7_Result)lot(7, table_X_Index, table_Y_Index, box_X_Index);
        Debug.Log(_lotname + ":" + result);
        return result;
    }
}
