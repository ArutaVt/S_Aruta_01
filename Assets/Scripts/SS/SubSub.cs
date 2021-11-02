using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubSub : MonoBehaviour
{
    //public Movies movies;
    //public Navis navis;
    //public CutIn cutIn;
    public GameObject LogoLampObject;
    private Logo logo;

    // Start is called before the first frame update
    void Start()
    {
        logo = LogoLampObject.GetComponent<Logo>();
        Sound.LoadSe("1BET", "00_BET音");
        Sound.LoadSe("3BET", "01_高速3BET音");
        Sound.LoadSe("WAIT", "02_ウエイト音");
        Sound.LoadSe("REELSTART", "03_レバオン");
        Sound.LoadSe("YOKOKU_A", "07_予告音1");
        Sound.LoadSe("YOKOKU_B", "08_予告音2");
        Sound.LoadSe("YOKOKU_C", "09_予告音3");
        Sound.LoadSe("STOP", "04_停止音1");
        Sound.LoadSe("REPLAY", "10_リプレイ");
        Sound.LoadSe("BELL", "11_ベル払い出し");
        Sound.LoadSe("BNS_SUIKA_DAITAI", "18_技術介入払い出し");
        Sound.LoadSe("SUIKA", "12_スイカ払い出し");
        Sound.LoadSe("CHERRY", "13_チェリー払い出し");
        Sound.LoadSe("JACBELL", "19_JAC払い出し");
        Sound.LoadSe("REACH_A", "14_テンパイ音1");
        Sound.LoadSe("REACH_B", "15_テンパイ音2");
        Sound.LoadSe("REACH_C", "16_SPテンパイ音");
        Sound.LoadSe("EV_STOP_1", "20_演出音1");
        Sound.LoadSe("EV_STOP_2", "21_演出音2");
        Sound.LoadSe("EV_STOP_3", "22_演出音3");
        Sound.LoadSe("ウーハー", "25_ウーハー風単体");
        Sound.LoadSe("遅れ", "遅れ_レバオン");
    }

    /// <summary>
    /// ロゴフラッシュ
    /// </summary>
    public void LogoFlash(Color color) { logo.flashStart(color); }

    /// <summary>
    /// ロゴ配色変更
    /// </summary>
    /// <param name="color"></param>
    public void LogoColor(Color color) { logo.setColor(color); }
}
