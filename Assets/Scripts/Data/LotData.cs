using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class LotData
{
    static System.Random random = new System.Random();
    public int no { get; set; }
    public string lotName { get; set; }
    public string label { get; set; }
    public string box_X_Name { get; set; }
    public int box_X_Index { get; set; }
    public string table_X_Name { get; set; }
    public int table_X_Index { get; set; }
    public string table_Y_Name { get; set; }
    public int table_Y_Index { get; set; }
    public int maxRand { get; set; }
    public int[] tisu { get; set; }

    /// <summary>
    /// 指定の内容で抽せんを行います。
    /// 抽せんエラーは-1を返します。
    /// </summary>
    /// <param name="_no"></param>
    /// <param name="_table_X_Index"></param>
    /// <param name="_table_Y_Index"></param>
    /// <param name="_box_X_Index"></param>
    /// <returns></returns>
    public int lot()
    {
        // 置数に
        if (chkTisu() == false)
        {
            Console.WriteLine($"置数の合計が{maxRand}ではありません。");
            return 0;
        }

        int rand = random.Next(0, maxRand);

        for (int i = 0; i < tisu.Length; i++)
        {
            rand -= tisu[i];
            if (rand < 0) return i;
        }

        return -1;
    }

    /// <summary>
    /// 置数の合計がmaxRandと等しいが確認する
    /// </summary>
    /// <returns></returns>
    private bool chkTisu()
    {
        int num = 0;
        foreach (var item in tisu) num += item;
        return (num == maxRand);
    }
}