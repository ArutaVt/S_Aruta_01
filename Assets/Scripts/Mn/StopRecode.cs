using System;
using System.Collections.Generic;
using System.Text;

public class StopRecode
{
    public StopRecode(int _maxComa)
    {
        // 順列、最大個数を求める
        int p = 1;

        for (int i = 0; i < 3; i++) p *= (i + 1);

        firstData = new int[3, _maxComa];
        secondData = new int[p, _maxComa, _maxComa];
        thirdData = new int[p, _maxComa, _maxComa, _maxComa];
    }

    public enum Reel
    {
        L = 0,  // Left
        C = 1,  // Center
        R = 2,  // Right
        MAX,
    };

    public enum PushSeq
    {
        LCR = 0,  // 123
        LRC = 1,  // 132
        CLR = 2,  // 213
        CRL = 3,  // 312
        RLC = 4,  // 231
        RCL = 5,  // 321
        MAX,
    };

    public string stopName { get; set; } = "ハズレ＋ハズレ";
    public int stopCode { get; set; } = 0;
    public string bnsName { get; set; } = "ハズレ";
    public int bnsCode { get; set; } = 0;
    public string frtName { get; set; } = "ハズレ";
    public int frtCode { get; set; } = 0;

    /// <summary>
    /// 1st停止データ
    /// </summary>
    public int[,] firstData { get; set; }
    
    /// <summary>
    /// 2nd停止データ
    /// </summary>
    public int[,,] secondData { get; set; }

    /// <summary>
    /// 3rd停止データ
    /// </summary>
    public int[,,,] thirdData { get; set; }

    /// <summary>
    /// 1st滑りコマ取得
    /// </summary>
    /// <param name="firstReel"></param>
    /// <param name="firstPushPos"></param>
    /// <returns></returns>
    public int getSuberiComa(Reel _reel, int firstPushPos)
    {
        return firstData[(int)_reel, firstPushPos];
    }

    /// <summary>
    /// 2nd滑りコマ取得
    /// </summary>
    /// <param name="firstReel"></param>
    /// <param name="firstPushPos"></param>
    /// <param name="secondReel"></param>
    /// <param name="secondPushPos"></param>
    /// <returns></returns>
    public int getSuberiComa(PushSeq _pushseq, int firstPushPos, int secondPushPos)
    {
        return secondData[(int)_pushseq, firstPushPos, secondPushPos];
    }

    /// <summary>
    /// 3rd滑りコマ取得
    /// </summary>
    /// <param name="_pushSeq"></param>
    /// <param name="firstPushPos"></param>
    /// <param name="secondReel"></param>
    /// <param name="secondPushPos"></param>
    /// <param name="thirdPushPos"></param>
    /// <returns></returns>
    public int getSuberiComa(PushSeq _pushSeq, int firstPushPos, int secondPushPos, int thirdPushPos)
    {
        return thirdData[(int)_pushSeq, firstPushPos, secondPushPos, thirdPushPos];
    }
}
