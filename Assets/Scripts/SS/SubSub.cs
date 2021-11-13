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

    // もういっそpublicにするからここから呼ぶことにする
    public GameObject SevenLampObject;
    private Logo sevenLamp;
    public GameObject ALampObject;
    private Logo aLamp;
    public GameObject BarLampObject;
    private Logo barLamp;
    public GameObject WinLampObject;
    private Logo winLamp;

    // Start is called before the first frame update
    void Start()
    {
        logo = LogoLampObject.GetComponent<Logo>();
        sevenLamp = SevenLampObject.GetComponent<Logo>();
        aLamp = ALampObject.GetComponent<Logo>();
        barLamp = BarLampObject.GetComponent<Logo>();
        winLamp = WinLampObject.GetComponent<Logo>();
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
        Sound.LoadSe("BONUS_LAMP", "17_告知音");
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

    /// <summary>
    /// 全てのボーナスランプ
    /// </summary>
    public void BonusLampAllOff()
    {
        sevenLamp.lampOff();
        aLamp.lampOff();
        barLamp.lampOff();
        winLamp.lampOff();
    }

    /// <summary>
    /// ボーナスランプ
    /// </summary>
    public enum BonusLamp
    {
        Seven,A,Bar
    }

    /// <summary>
    /// 指定のボーナスランプを点灯させる
    /// </summary>
    /// <param name="lamp"></param>
    public void BonusLampOn(BonusLamp lamp)
    {
        winLamp.lampOn();

        switch (lamp)
        {
            case BonusLamp.Seven:
                sevenLamp.lampOn();
                sevenLamp.flashStart(Color.white);
                break;
            case BonusLamp.A:
                aLamp.lampOn();
                aLamp.flashStart(Color.white);
                break;
            case BonusLamp.Bar:
                barLamp.lampOn();
                barLamp.flashStart(Color.white);
                break;
            default:
                break;
        }
    }

    public void ReelLightOff(ReelController.Reel_ID reel)
    {
        switch (reel)
        {
            case ReelController.Reel_ID.Left:
                Mn.leftReel.LightOff();
                break;
            case ReelController.Reel_ID.Center:
                Mn.centerReel.LightOff();
                break;
            case ReelController.Reel_ID.Right:
                Mn.rightReel.LightOff();
                break;
            default:
                break;
        }
    }

    public void ReelLightOn()
    {
        Mn.leftReel.LightOn();
        Mn.centerReel.LightOn();
        Mn.rightReel.LightOn();
    }
}
