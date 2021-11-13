using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

class SubMain
{
    string[] payoutSoundList =
    {
        "SUIKA",
        "BNS_SUIKA_DAITAI",
        "SUIKA",
        "JACBELL",
        "BELL",
    };

#region Variable

    private EvData data = new EvData();
    private SubSub subSub;
    //private Text nmlGame;
    //private GameObject LastSetText;
    //private GameObject GetMedal;
    private System.Random rand = new System.Random();
    private bool bnsEndflg = false;
    private int bonusMarginGame = 0;
    private bool bonusSoundChangeFlg = false;
    private BonusSound bonusSound;
    private int naibuCnt = 0;
    private GameObject scoreBoard;

    public BonusType bonustype = BonusType.BB1;
    public enum BonusType
    { 
        BB1,
        BB2,
        BB3,
        BB4,
        RB1,
        RB2
    };

    // 演出抽せんクラス
    SMLot smLot = new SMLot();

#endregion

    public SubMain()
    {
        GameObject gameObject = GameObject.Find("Main");
        subSub = gameObject.GetComponent<SubSub>();
        bonusSound = gameObject.GetComponent<BonusSound>();
        scoreBoard = GameObject.Find("ScoreBoard");
        //nmlGame = GameObject.Find("NmlGame").GetComponent<Text>();
        //nmlGame.text = string.Format("{0:D3}", Sim.DdmVariable.NmlGame) + "G";
        //LastSetText = GameObject.Find("LastGame_SetNum");
        //LastSetText.SetActive(false);
        //GetMedal = GameObject.Find("GetMedal");
        //GetMedal.SetActive(false);
    }

    public void Bet(int betnum)
    {
        if(Mn.mnStatus == AutoMakeCode.Enum.Status.Nml)
        {
            bonusSound.StopBgm();
        }

        Sound.StopSe(0);

        switch (betnum)
        {
            case 0:
                Sound.PlaySe("1BET", 0.2f);
                break;
            case 1:
                Sound.PlaySe("3BET", 0.2f);
                break;
            default:
                break;
        }

    }
    public void Lever(AutoMakeCode.Enum.Status state)
    {
        data.MnDataUpData();    // 内容整理

        if (state == AutoMakeCode.Enum.Status.Nml)
        {
            bonusMarginGame++;
        }

        // Leverで白点灯に戻す
        subSub.LogoColor(Color.white);
    }

    public void WaitStart()
    {
        if (smLot.lotA4() == SMLot.A4_Result.Hit)
        {
            Sound.PlaySe("ウーハー", 0.4f, 0);
        }
        else
        {
            Sound.PlaySe("WAIT", 0.1f, 0);
        }
    }

    public void ReelStart()
    {
        Sound.StopSe(0);

        // 予告音抽せん        
        switch (smLot.lotA1())
        {
            case SMLot.A1_Result.None:
                break;
            case SMLot.A1_Result.Normal:

                // ロゴ演出抽せん
                switch (smLot.lotA6())
                {
                    case SMLot.A6_Result.nml:
                        subSub.LogoColor(Color.white);
                        break;
                    case SMLot.A6_Result.blackout:
                        subSub.LogoColor(Color.gray);
                        break;
                    case SMLot.A6_Result.flash:
                        subSub.LogoFlash(Color.white);
                        break;
                    case SMLot.A6_Result.yellow:
                        subSub.LogoColor(Color.yellow);
                        break;
                    case SMLot.A6_Result.blue:
                        subSub.LogoColor(new Color32(158, 178, 255, 255));
                        break;
                    case SMLot.A6_Result.red:
                        subSub.LogoColor(new Color32(255, 170, 170, 255));
                        break;
                    case SMLot.A6_Result.green:
                        subSub.LogoColor(Color.green);
                        break;
                    case SMLot.A6_Result.yellow_flash:
                        subSub.LogoFlash(Color.yellow);
                        break;
                    case SMLot.A6_Result.blue_flash:
                        subSub.LogoFlash(new Color32(158, 178, 255, 255));
                        break;
                    case SMLot.A6_Result.red_flash:
                        subSub.LogoFlash(new Color32(255, 170, 170, 255));
                        break;
                    case SMLot.A6_Result.green_flash:
                        subSub.LogoFlash(Color.green);
                        break;
                    default:
                        break;
                }

                Sound.PlaySe("REELSTART", 0.2f);
                break;
            case SMLot.A1_Result.Yokoku_A:
                subSub.LogoFlash(Color.white);
                Sound.PlaySe("YOKOKU_A", 0.2f);
                break;
            case SMLot.A1_Result.Yokoku_B:
                subSub.LogoFlash(Color.white);
                Sound.PlaySe("YOKOKU_B", 0.2f);
                break;
            case SMLot.A1_Result.Yokoku_C:
                subSub.LogoFlash(Color.white);
                Sound.PlaySe("YOKOKU_C", 0.2f);
                break;
            case SMLot.A1_Result.Late:
                Sound.PlaySe("遅れ", 0.2f);
                break;
            default:
                Sound.PlaySe("REELSTART", 0.2f);
                break;
        }

        // 停止音演出有無
        smLot.a5_Result = SMLot.A5_Result.None;
        if (smLot.lotA7() == SMLot.A7_Result.Hit)
        {
            // 停止音演出抽せん
            smLot.lotA5();
        }

        switch (Sim.DdmVariable.StartDdmMode)
        {
            case Sim.DDMMODE.Nml:
                if (Sim.DdmVariable.FreezeType == Sim.FREEZE_TYPE.LongFreeze)
                {
                    // subSub.cutIn.DispCutIn(CutIn.Type.White);
                }
                break;
            case Sim.DDMMODE.BnsWait:
                break;
            case Sim.DDMMODE.Bns:
                break;
            case Sim.DDMMODE.Rush:
                if (Sim.DdmVariable.FreezeType == Sim.FREEZE_TYPE.RushStart)
                {
                    DispText(true);
                    if(Sim.DdmVariable.FreezeFlg == true)
                    {
                        Sound.PlayBgm("UltimateBattle");
                    }
                    else if((Sim.DdmVariable.AtMode == Sim.RUSH_MODE._80) && (rand.Next(2) == 0))
                    {
                        Sound.PlayBgm("Magia");
                    }
                    else
                    {
                        Sound.PlayBgm("WalpurgisNight");
                    }
                    DispNaviFunction();
                }
                break;
            case Sim.DDMMODE.AtBns:
                break;
            default:
                break;
        }
    }

    private int bonusGameCnt = 0;
    public void BonusStart(AutoMakeCode.Enum.BnsCode bns)
    {
        bonusGameCnt = 0;

        if(naibuCnt < 3)
        {
            // 告知ランプ点灯
            switch (Mn.bnsCode)
            {
                case AutoMakeCode.Enum.BnsCode.ABIG:
                    subSub.BonusLampOn(SubSub.BonusLamp.A);
                    break;
                case AutoMakeCode.Enum.BnsCode.SBIG:
                    subSub.BonusLampOn(SubSub.BonusLamp.Seven);
                    break;
                case AutoMakeCode.Enum.BnsCode.RB:
                    subSub.BonusLampOn(SubSub.BonusLamp.Bar);
                    break;
            }
        }
    }

    /// <summary>
    /// JacGame中はtrue
    /// </summary>
    private bool jacGameFlg = false;
    public void PayOut(List<Mn.CmbRecode> list, int betnum, AutoMakeCode.Enum.Status state)
    {
        subSub.ReelLightOn();
        if (smLot.a5_Result == SMLot.A5_Result.S123_Off) subSub.LogoColor(Color.gray);

        List<string> paysound = new List<string>();
        foreach (Mn.CmbRecode item in list)
        {
            switch (item.name)
            {
                case "7BIG":
                    if (paysound.Exists(x => x == "7BIG") == false) paysound.Add("7BIG");
                    break;
                case "ABIG_A":
                case "ABIG_B":
                case "ABIG_C":
                case "ABIG_D":
                    if (paysound.Exists(x => x == "ABIG") == false) paysound.Add("ABIG");
                    break;
                case "REG_1":
                    if (paysound.Exists(x => x == "REG_1") == false) paysound.Add("REG_1");
                    break;
                case "REG_2":
                    if (paysound.Exists(x => x == "REG_2") == false) paysound.Add("REG_2");
                    break;
                case "チェリー_1":
                case "チェリー_2":
                case "チェリー代替":
                    if (paysound.Exists(x => x == "CHERRY") == false) paysound.Add("CHERRY");
                    break;
                case "スイカ":
                    if (paysound.Exists(x => x == "SUIKA") == false) paysound.Add("SUIKA");
                    break;
                case "スイカ代替":
                    if(betnum == 0)
                    {
                        if (paysound.Exists(x => x == "BNS_SUIKA_DAITAI") == false) paysound.Add("BNS_SUIKA_DAITAI");
                    }
                    break;
                case "ベル":
                case "ベル代替":

                    switch (state)
                    {
                        case AutoMakeCode.Enum.Status.ABIG:
                        case AutoMakeCode.Enum.Status.SBIG:
                            if (jacGameFlg == true)
                            {
                                if (paysound.Exists(x => x == "JACBELL") == false) paysound.Add("JACBELL");
                            }
                            else
                            {
                                if (paysound.Exists(x => x == "BELL") == false) paysound.Add("BELL");
                            }
                            break;
                        case AutoMakeCode.Enum.Status.RB:
                            if (paysound.Exists(x => x == "JACBELL") == false) paysound.Add("JACBELL");
                            break;
                        default:
                            if (paysound.Exists(x => x == "BELL") == false) paysound.Add("BELL");
                            break;
                    }

                    break;
                case "リプレイ":
                    if (paysound.Exists(x => x == "REPLAY") == false) paysound.Add("REPLAY");
                    break;
                case "1枚役A":
                case "1枚役B":
                case "1枚役C":
                    break;
                default:
                    break;
            }
        }

        // サウンド再生
        foreach (var item in paysound)
        {
            switch (item)
            {
                case "7BIG":
                    bonusGameCnt = 0;
                    if (bonusSoundChangeFlg == true)
                    {
                        if(bonusMarginGame == 0)
                        {
                            bonustype = BonusType.BB4;
                            bonusSound.PlayBgm(BonusSound.BonusSoundType.BB4_StartFrtJacGame);
                        }
                        else
                        {
                            bonustype = BonusType.BB3;
                            bonusSound.PlayBgm(BonusSound.BonusSoundType.BB3_StartFrtJacGame);
                        }
                    }
                    else
                    {
                        bonustype = BonusType.BB1;
                        bonusSound.PlayBgm(BonusSound.BonusSoundType.BB1_Start);
                    }
                    break;
                case "ABIG":
                    bonusGameCnt = 0;
                    if (bonusSoundChangeFlg == true)
                    {
                        if (bonusMarginGame == 0)
                        {
                            bonustype = BonusType.BB4;
                            bonusSound.PlayBgm(BonusSound.BonusSoundType.BB4_StartFrtJacGame);
                        }
                        else
                        {
                            bonustype = BonusType.BB3;
                            bonusSound.PlayBgm(BonusSound.BonusSoundType.BB3_StartFrtJacGame);
                        }
                    }
                    else
                    {
                        bonustype = BonusType.BB2;
                        bonusSound.PlayBgm(BonusSound.BonusSoundType.BB2_Start);
                    }
                    break;
                case "REG_1":
                    bonusGameCnt = 0;
                    bonustype = BonusType.RB1;
                    bonusSound.PlayBgm(BonusSound.BonusSoundType.BB1_JacGame);
                    break;
                case "REG_2":
                    bonusGameCnt = 0;
                    bonustype = BonusType.RB2;
                    bonusSound.PlayBgm(BonusSound.BonusSoundType.BB2_JacGame);
                    break;
                default: 
                    Sound.PlaySe(item, 0.2f, 0);
                    break;
            }
        }

        //if (Sim.FrtData.ResultFrtItem.hitType != Sim.HitType.replay) data.TotalIn += 3;                   // 投入カウント
        //data.TotalOut += Sim.FrtData.ResultFrtItem.pay;                                                   // 払出カウント
    }

    public void PayEnd(AutoMakeCode.Enum.Status state)
    {

        // ボーナス中のゲーム数をカウントしてBGMを切り替えます
        switch (state)
        {
            case AutoMakeCode.Enum.Status.ABIG:
            case AutoMakeCode.Enum.Status.SBIG:

                if ((bonustype == BonusType.BB3) || (bonustype == BonusType.BB4)) return;

                bonusGameCnt++;
                if ((bonusGameCnt % 5 == 0) && (bonusGameCnt <= 15))
                {
                    float phase = (float)bonusGameCnt;
                    phase /= 5;

                    if (phase % 2 == 0)
                    {
                        // 偶数フェーズ
                        Debug.Log("小役ゲーム開始");
                        jacGameFlg = false;
                        switch (bonustype)
                        {
                            case BonusType.BB1:
                                bonusSound.PlayBgm(BonusSound.BonusSoundType.BB1_FrtGame);
                                break;
                            case BonusType.BB2:
                                bonusSound.PlayBgm(BonusSound.BonusSoundType.BB2_FrtGame);
                                break;
                            case BonusType.BB3:
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        // 奇数フェーズ
                        Debug.Log("JACゲーム開始");
                        jacGameFlg = true;
                        switch (bonustype)
                        {
                            case BonusType.BB1:
                                bonusSound.PlayBgm(BonusSound.BonusSoundType.BB1_JacGame);
                                break;
                            case BonusType.BB2:
                                bonusSound.PlayBgm(BonusSound.BonusSoundType.BB2_JacGame);
                                break;
                            case BonusType.BB3:
                                break;
                            default:
                                break;
                        }
                    }

                }
                break;
            case AutoMakeCode.Enum.Status.RB:
                break;
            default:
                break;
        }
    }

    public void StopBtn(int stopcnt, ReelController.Reel_ID reel)
    {
        switch (reel)
        {
            case ReelController.Reel_ID.Left:
                //if (subSub.navis.CheckDispNavi(Navis.NaviPos.Left) == true)
                //{
                //    Sound.PlaySe("BellNaviStop");
                //    subSub.navis.NoDispNavi(Navis.NaviPos.Left);
                //}
                //else
                {
                    // Sound.PlaySe("STOP", 0.2f);
                }
                break;
            case ReelController.Reel_ID.Center:
                //if (subSub.navis.CheckDispNavi(Navis.NaviPos.Center) == true)
                //{
                //    Sound.PlaySe("BellNaviStop");
                //    subSub.navis.NoDispNavi(Navis.NaviPos.Center);
                //}
                //else
                {
                    // Sound.PlaySe("STOP", 0.2f);
                }
                break;
            case ReelController.Reel_ID.Right:
                //if (subSub.navis.CheckDispNavi(Navis.NaviPos.Right) == true)
                //{
                //    Sound.PlaySe("BellNaviStop");
                //    subSub.navis.NoDispNavi(Navis.NaviPos.Right);
                //}
                //else
                {
                    // Sound.PlaySe("STOP", 0.2f);
                }
                break;
            default:
                break;
        }

        switch (stopcnt)
        {
            case 1:
                switch (smLot.a5_Result)
                {
                    case SMLot.A5_Result.S1:
                    case SMLot.A5_Result.S12:
                    case SMLot.A5_Result.S123:
                    case SMLot.A5_Result.S123_On:
                    case SMLot.A5_Result.S123_Off:
                        Sound.PlaySe("EV_STOP_1", 0.2f, 0);
                        subSub.ReelLightOff(reel);
                        break;
                    default:
                        Sound.PlaySe("STOP", 0.2f, 0);
                        break;
                }
                break;
            case 2:
                switch (smLot.a5_Result)
                {
                    case SMLot.A5_Result.S12:
                    case SMLot.A5_Result.S123:
                    case SMLot.A5_Result.S123_On:
                    case SMLot.A5_Result.S123_Off:
                        Sound.PlaySe("EV_STOP_2", 0.2f, 0);
                        subSub.ReelLightOff(reel);
                        break;
                    default:
                        Sound.PlaySe("STOP", 0.2f, 0);
                        break;
                }

                switch (Mn.mnStatus)
                {
                    case AutoMakeCode.Enum.Status.ABIGStandby:
                        if (data.IsReach(new int[] { 1, 8 }))
                        {
                            // 特殊聴牌音抽せん
                            if(Mn.frtCode == AutoMakeCode.Enum.FrtCode.Hazure)
                            {
                                switch (smLot.lotA2())
                                {
                                    case SMLot.A2_Result.None:
                                        break;
                                    case SMLot.A2_Result.A:
                                        Sound.PlaySe("REACH_A", 0.2f, 1);
                                        break;
                                    case SMLot.A2_Result.B:
                                        Sound.PlaySe("REACH_B", 0.2f, 1);
                                        break;
                                    case SMLot.A2_Result.C:
                                        Sound.PlaySe("REACH_C", 0.2f, 1);
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                        else if (data.IsReach(new int[] { 0 }) || data.IsReach(new int[] { 2 }))
                        {
                            Sound.PlaySe("REACH_A", 0.2f, 1);
                        }
                        break;
                    case AutoMakeCode.Enum.Status.SBIGStandby:
                        if (data.IsReach(new int[] { 0 }))
                        {
                            // 特殊聴牌音抽せん
                            if (Mn.frtCode == AutoMakeCode.Enum.FrtCode.Hazure)
                            {
                                switch (smLot.lotA2())
                                {
                                    case SMLot.A2_Result.None:
                                        break;
                                    case SMLot.A2_Result.A:
                                        Sound.PlaySe("REACH_A", 0.2f, 1);
                                        break;
                                    case SMLot.A2_Result.B:
                                        Sound.PlaySe("REACH_B", 0.2f, 1);
                                        break;
                                    case SMLot.A2_Result.C:
                                        Sound.PlaySe("REACH_C", 0.2f, 1);
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                        else if (data.IsReach(new int[] { 1, 8 }) || data.IsReach(new int[] { 2 }))
                        {
                            Sound.PlaySe("REACH_A", 0.2f, 1);
                        }
                        break;
                    case AutoMakeCode.Enum.Status.RBStandby:
                        if (data.IsReach(new int[] { 2 }))
                        {
                            // 特殊聴牌音抽せん
                            if (Mn.frtCode == AutoMakeCode.Enum.FrtCode.Hazure)
                            {
                                switch (smLot.lotA2())
                                {
                                    case SMLot.A2_Result.None:
                                        break;
                                    case SMLot.A2_Result.A:
                                        Sound.PlaySe("REACH_A", 0.2f, 1);
                                        break;
                                    case SMLot.A2_Result.B:
                                        Sound.PlaySe("REACH_B", 0.2f, 1);
                                        break;
                                    case SMLot.A2_Result.C:
                                        Sound.PlaySe("REACH_C", 0.2f, 1);
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                        else if (data.IsReach(new int[] { 1, 8 }) || data.IsReach(new int[] { 0 }))
                        {
                            Sound.PlaySe("REACH_A", 0.2f, 1);
                        }
                        break;
                    case AutoMakeCode.Enum.Status.Nml:
                        if (data.IsReach(new int[] { 1, 8 }) || data.IsReach(new int[] { 0 }) || data.IsReach(new int[] { 2 })) Sound.PlaySe("REACH_A", 0.2f);
                        break;
                    default:
                        break;
                }
                break;

            case 3:
                Sound.StopSe(0);
                switch (smLot.a5_Result)
                {
                    case SMLot.A5_Result.S123_On:
                        subSub.LogoColor(Color.gray);
                        Sound.PlaySe("EV_STOP_3", 0.2f, 0);
                        subSub.ReelLightOff(reel);
                        break;
                    case SMLot.A5_Result.S123:
                    case SMLot.A5_Result.S123_Off:
                        Sound.PlaySe("EV_STOP_3", 0.2f, 0);
                        subSub.ReelLightOff(reel);
                        break;
                    default:
                        Sound.PlaySe("STOP", 0.2f, 0);
                        break;
                }
                break;
            default:
                break;
        }
    }

    public void BonudEnd()
    {
        // ボーナス終了フラグをON（ボーナス終了サウンド処理後にOFF）
        bnsEndflg = true;

        // ボーナス終了後にBGM変更用のフラグをONにする（100G経過でOFF）
        bonusSoundChangeFlg = true;

        // ボーナス間
        bonusMarginGame = 0;

        // 内部中のハズレカウント用
        naibuCnt = 0;
    }
    
    public void GameEnd()
    {

        if (bnsEndflg == true)
        {
            bnsEndflg = false;
            switch (bonustype)
            {
                case BonusType.BB1:
                    Sound.StopSe(0);
                    bonusSound.PlayBgm(BonusSound.BonusSoundType.BB1_Ending);
                    break;
                case BonusType.BB2:
                    Sound.StopSe(0);
                    bonusSound.PlayBgm(BonusSound.BonusSoundType.BB2_Ending);
                    break;
                case BonusType.BB3:
                    Sound.StopSe(0);
                    bonusSound.PlayBgm(BonusSound.BonusSoundType.BB3_Ending);
                    break;
                case BonusType.BB4:
                    Sound.StopSe(0);
                    bonusSound.PlayBgm(BonusSound.BonusSoundType.BB4_Ending);
                    break;
                default:
                    bonusSound.StopBgm();
                    break;
            }

            // 告知ランプ消灯
            subSub.BonusLampAllOff();
        }

        // (5G)経過した時にBGM変更フラグがONの場合はOFFにする
        if((bonusSoundChangeFlg == true) && (bonusMarginGame > 5))
        {
            bonusSoundChangeFlg = false;
        }

        // 内部中のハズレをカウントしてボーナス告知をする
        if((Mn.bnsCode != AutoMakeCode.Enum.BnsCode.Hazure) && (Mn.frtCode == AutoMakeCode.Enum.FrtCode.Hazure))
        {
            naibuCnt++;
            if(naibuCnt == 3)
            {
                Sound.PlaySe("BONUS_LAMP", 0.2f);

                // 告知ランプ点灯
                switch (Mn.bnsCode)
                {
                    case AutoMakeCode.Enum.BnsCode.ABIG:
                        subSub.BonusLampOn(SubSub.BonusLamp.A);
                        break;
                    case AutoMakeCode.Enum.BnsCode.SBIG:
                        subSub.BonusLampOn(SubSub.BonusLamp.Seven);
                        break;
                    case AutoMakeCode.Enum.BnsCode.RB:
                        subSub.BonusLampOn(SubSub.BonusLamp.Bar);
                        break;
                }
            }
        }

    }

    /// <summary>
    // ナビの表示処理
    /// </summary>
    private void DispNaviFunction()
    {
        switch (Sim.DdmVariable.StartDdmMode)
        {
            case Sim.DDMMODE.Bns:
            case Sim.DDMMODE.AtBns:
            case Sim.DDMMODE.Rush:
                switch (Sim.DdmVariable.FrtCode)
                {
                    case Sim.FRT_CODE.Chance:
                    case Sim.FRT_CODE.KyoChe:
                    case Sim.FRT_CODE.JakuChe:
                    case Sim.FRT_CODE.Suika:
                        // subSub.navis.DispNavi(Navis.OrderType.Rare, NaviController.Type.Rare);
                        break;
                    case Sim.FRT_CODE.HomuraReplay:
                        if((Sim.DdmVariable.StartDdmMode == Sim.DDMMODE.Bns) || (Sim.DdmVariable.StartDdmMode == Sim.DDMMODE.AtBns))
                        {
                            // subSub.cutIn.DispCutIn(CutIn.Type.Homura);
                        }
                        break;
                    case Sim.FRT_CODE.RegReplay:
                    case Sim.FRT_CODE.BigReplay:
                    case Sim.FRT_CODE.SBigReplay:
                        break;
                    case Sim.FRT_CODE.Bell_123:
                        // subSub.navis.DispNavi(Navis.OrderType.Order_123, NaviController.Type.Yellow);
                        break;
                    case Sim.FRT_CODE.Bell_132:
                        // subSub.navis.DispNavi(Navis.OrderType.Order_132, NaviController.Type.Yellow);
                        break;
                    case Sim.FRT_CODE.Bell_213:
                        // subSub.navis.DispNavi(Navis.OrderType.Order_213, NaviController.Type.Yellow);
                        break;
                    case Sim.FRT_CODE.Bell_231:
                        // subSub.navis.DispNavi(Navis.OrderType.Order_231, NaviController.Type.Yellow);
                        break;
                    case Sim.FRT_CODE.Bell_321:
                        // subSub.navis.DispNavi(Navis.OrderType.Order_321, NaviController.Type.Yellow);
                        break;
                    case Sim.FRT_CODE.Bell_312:
                        // subSub.navis.DispNavi(Navis.OrderType.Order_312, NaviController.Type.Yellow);
                        break;
                    case Sim.FRT_CODE.Hazure:
                        break;
                    default:
                        break;
                }
                break;
            case Sim.DDMMODE.BnsWait:
                switch (Sim.DdmVariable.FrtCode)
                {
                    case Sim.FRT_CODE.RegReplay:
                        // subSub.cutIn.DispCutIn(CutIn.Type.Bar);
                        break;
                    case Sim.FRT_CODE.BigReplay:
                        // subSub.cutIn.DispCutIn(CutIn.Type.Red);
                        break;
                    case Sim.FRT_CODE.SBigReplay:
                        // subSub.cutIn.DispCutIn(CutIn.Type.White);
                        break;
                    default:
                        if(data.IsRare() == true)
                        {
                            // subSub.navis.DispNavi(Navis.OrderType.Rare, NaviController.Type.Rare);
                        }
                        break;
                }
                break;
        }
    }

    /// <summary>
    /// 獲得枚数、残りG数の表示を開始する
    /// </summary>
    private void DispText(bool flg)
    {
        //LastSetText.GetComponent<Text>().color = Color.red;
        //GetMedal.GetComponent<Text>().color = Color.red;
        //LastSetText.SetActive(flg);
        //GetMedal.SetActive(flg);
    }

    /// <summary>
    /// MnLoopに置いとく
    /// </summary>
    public void UpData()
    {
        //if( LastSetText.activeSelf == true)
        // {
        //     switch (Sim.DdmVariable.StartDdmMode)
        //     {
        //         case Sim.DDMMODE.Bns:
        //         case Sim.DDMMODE.AtBns:
        //             LastSetText.GetComponent<Text>().text = "Last " + Sim.DdmVariable.BnsGame;
        //             break;
        //         case Sim.DDMMODE.Rush:
        //             LastSetText.GetComponent<Text>().text = "Last " + data.rushGame + "\n" + "Set " + data.setNum;
        //             break;
        //     }
        // }
        //if(GetMedal.activeSelf == true)
        // {
        //     GetMedal.GetComponent<Text>().text = "Total " + data.getmedal;
        // }

        if (Input.GetKey(KeyCode.D))
        {
            scoreBoard.SetActive(true);
        }
        else
        {
            scoreBoard.SetActive(false);
        }

        if (Input.GetKey(KeyCode.D))
        {
            DispData();
        }
        else
        {
            switch (Sim.DdmVariable.StartDdmMode)
            {
                case Sim.DDMMODE.Bns:
                case Sim.DDMMODE.AtBns:
                case Sim.DDMMODE.Rush:
                    DispText(true);
                    break;
                default:
                    DispText(false);
                    break;
            }
        }
    }

    /// <summary>
    /// 差枚数表示等
    /// </summary>
    public void DispData()
    {
        //if(LastSetText.activeSelf == false) LastSetText.SetActive(true);
        //string msg;
        //if(data.TotalIn > 0)
        //{
        //    double ret = ((double)data.TotalOut / (double)data.TotalIn) * 100;
        //    ret = Math.Round(ret, 2);
        //    msg = ret.ToString();
        //}
        //else
        //{
        //    msg = "0.00";
        //}
        //LastSetText.GetComponent<Text>().text = "総回転数:" + data.TotalGame + "G" + "\n";
        //LastSetText.GetComponent<Text>().text += "In:" + data.TotalIn + "\n";
        //LastSetText.GetComponent<Text>().text += "Out:" + data.TotalOut + "\n";
        //LastSetText.GetComponent<Text>().text += "機械割:" + msg + "%" + "\n";
        //LastSetText.GetComponent<Text>().text += "差枚数:" + (data.TotalOut - data.TotalIn) + "枚" + "\n";
        //LastSetText.GetComponent<Text>().text += Sim.DdmVariable.Settei + "\n";
        //LastSetText.GetComponent<Text>().color = Color.red;

        //if(Input.GetKeyDown(KeyCode.S) == true)
        //{
        //    switch (Sim.DdmVariable.Settei)
        //    {
        //        case Sim.SETTEI.Settei1:
        //            Sim.DdmVariable.Settei = Sim.SETTEI.Settei2;
        //            break;
        //        case Sim.SETTEI.Settei2:
        //            Sim.DdmVariable.Settei = Sim.SETTEI.Settei3;
        //            break;
        //        case Sim.SETTEI.Settei3:
        //            Sim.DdmVariable.Settei = Sim.SETTEI.Settei4;
        //            break;
        //        case Sim.SETTEI.Settei4:
        //            Sim.DdmVariable.Settei = Sim.SETTEI.Settei5;
        //            break;
        //        case Sim.SETTEI.Settei5:
        //            Sim.DdmVariable.Settei = Sim.SETTEI.Settei6;
        //            break;
        //        case Sim.SETTEI.Settei6:
        //            Sim.DdmVariable.Settei = Sim.SETTEI.Settei1;
        //            break;
        //        default:
        //            break;
        //    }
        //}
    }
}

class EvData
{
    public int getmedal = 0;
    public bool replayFlg = false;
    public bool preDdmModeChangeFlg = false;
    public bool DdmModeChangeFlg = false;
    public bool rushHitFlg = false;
    public bool stockHitFlg = false;
    public int[] setStock = new int[] { 0, 0 };
    public int rushGame = 0;
    public int setNum = 0;
    public int TotalIn = 0;
    public int TotalOut = 0;
    public int TotalGame = 0;

    public void Init()
    {
        getmedal = 0;
        replayFlg = false;
        preDdmModeChangeFlg = false;
        DdmModeChangeFlg = false;
    }

    /// <summary>
    /// 指定の出玉状態が終了した当該
    /// </summary>
    /// <param name="mode"></param>
    /// <returns></returns>
    public bool IsDdmEnd(Sim.DDMMODE mode)
    {
        if(Sim.DdmVariable.StartDdmMode == mode)
        {
            if(Sim.DdmVariable.DdmMode == mode)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 指定図柄の聴牌チェック
    /// </summary>
    /// <param name="zugaraNum"></param>
    /// <returns></returns>
    public bool IsReach(int[] zugaraNum)
    {
        int[,] deme = new int[3, 3];

        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                if(Sim.DdmVariable.deme[y, x] != -1)
                {
                    deme[y, x] = Sim.DdmVariable.reel[x, Sim.DdmVariable.deme[y, x]];
                }
                else
                {
                    deme[y, x] = -1;
                }
            }
        }

        List<bool> list = new List<bool>();

        foreach (var item in deme)
        {
            if(item != -1)
            {
                bool flg = false;
                foreach (var num in zugaraNum)
                {
                    if (item == num) flg = true;
                }
                list.Add(flg);
            }
        }

        if(deme[0, 0] == -1)
        {
            // 左リール回転中
            if ((list[0] == list[1]) && list[0] == true) return true;
            if ((list[2] == list[3]) && list[2] == true) return true;
            if ((list[4] == list[5]) && list[4] == true) return true;
            if ((list[1] == list[2]) && list[1] == true) return true;
            if ((list[2] == list[5]) && list[2] == true) return true;
        }
        else if(deme[0, 1] == -1)
        {
            // 中リール回転中
            if ((list[0] == list[1]) && list[0] == true) return true;
            if ((list[2] == list[3]) && list[2] == true) return true;
            if ((list[4] == list[5]) && list[4] == true) return true;
            if ((list[0] == list[5]) && list[0] == true) return true;
            if ((list[4] == list[1]) && list[4] == true) return true;
        }
        else
        {
            // 右リール回転中
            if ((list[0] == list[1]) && list[0] == true) return true;
            if ((list[2] == list[3]) && list[2] == true) return true;
            if ((list[4] == list[5]) && list[4] == true) return true;
            if ((list[0] == list[3]) && list[0] == true) return true;
            if ((list[3] == list[4]) && list[3] == true) return true;
        }

        return false;
    }

    /// <summary>
    /// Mnでの状態を整理する
    /// </summary>
    public void MnDataUpData()
    {
        const int Old = 1;
        const int Now = 0;

        // 状態移行判定
        DdmModeChangeFlg = false;
        if(preDdmModeChangeFlg == false)
        {
            if(Sim.DdmVariable.StartDdmMode != Sim.DdmVariable.DdmMode)
            {
                preDdmModeChangeFlg = true;
            }
        }
        else
        {
            preDdmModeChangeFlg = false;
            DdmModeChangeFlg = true;
        }

        // ボーナス中にAT当せん？
        rushHitFlg = false;
        if(Sim.DdmVariable.StartDdmMode == Sim.DDMMODE.Bns)
        {
            if(Sim.DdmVariable.DdmMode == Sim.DDMMODE.AtBns)
            {
                rushHitFlg = true;
            }
        }

        // ストック当せん確認
        stockHitFlg = false;
        setStock[Old] = setStock[Now];
        setStock[Now] = Sim.DdmVariable.Stock;
        if(setStock[Now] > setStock[Old])
        {
            stockHitFlg = true;
        }
    }

    /// <summary>
    /// ラッシュ継続当該？
    /// </summary>
    /// <returns></returns>
    public bool IsNextSet()
    {
        if (Sim.DdmVariable.StartDdmMode == Sim.DDMMODE.Rush)
        {
            if (Sim.DdmVariable.FreezeType == Sim.FREEZE_TYPE.None)
            {
                if (Sim.DdmVariable.RushGame == 7)
                {
                    return true;
                }
            }
        }
        return false;
    }

    /// <summary>
    /// ラッシュが終了した遊技？
    /// </summary>
    /// <returns></returns>
    public bool IsRushEnd()
    {
        if (Sim.DdmVariable.StartDdmMode == Sim.DDMMODE.Rush)
        {
            if (Sim.DdmVariable.DdmMode == Sim.DDMMODE.Nml)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// ほむら揃いOK？
    /// </summary>
    /// <returns></returns>
    public bool IsHomuraSoroi()
    {
        switch (Sim.DdmVariable.StartDdmMode)
        {
            case Sim.DDMMODE.Bns:
            case Sim.DDMMODE.AtBns:
                if(Sim.DdmVariable.BnsGame > 0)
                {
                    if(Sim.DdmVariable.FrtCode == Sim.FRT_CODE.HomuraReplay)
                    {
                        return true;
                    }
                }
                break;
            default:
                break;
        }
        return false;
    }

    /// <summary>
    /// レア役か確認する
    /// </summary>
    /// <returns></returns>
    public bool IsRare()
    {
        switch (Sim.DdmVariable.FrtCode)
        {
            case Sim.FRT_CODE.Chance:
            case Sim.FRT_CODE.KyoChe:
            case Sim.FRT_CODE.JakuChe:
            case Sim.FRT_CODE.Suika:
                return true;
            default:
                return false;
        }
    }
}

