using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StopRecode
{
    public string stopName { get; set; }
    public string stopCode { get; set; }
    public string bnsName { get; set; }
    public string bnsCode { get; set; }
    public string frtName { get; set; }
    public string frtCode { get; set; }
    public int[][] firstData { get; set; }
    public int[][][] secondData { get; set; }
    public int[][][][] thirdData { get; set; }
}