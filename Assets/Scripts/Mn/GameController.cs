using System.Reflection;
using UnityEditor;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public enum MnState
    {
        BetWait = 0,
        LeverWait,
        StopWait,
        Payout,
    }

    // 図柄定義
    public enum zugara
    {
        eReplay_A,
        eReplay_B,
        eBell_A,
        eSuika_A,
        eCherry_A,
        eBrank_A,
        eBrank_B,
        eSeven_A,
        eSeven_C,
        eBar_A,
        ANY
    };

    public int AutoPlaySpeed = 10;   // 待機フレーム 
    public GameObject[] reels;
    public GameObject _monitor;
    public bool AutoPlay;
    private bool AutoPlayControle;
    private ReelController[] reelControllers = new ReelController[3];
    private MnState mnState = MnState.BetWait;
    private int FreamCnt = 10;

    // リール配列定義
    zugara[,] ReelArray = {
        {zugara.eBrank_A, zugara.eSuika_A, zugara.eSeven_C, zugara.eReplay_A, zugara.eBell_A, zugara.eCherry_A, zugara.eSuika_A, zugara.eBar_A, zugara.eReplay_A, zugara.eBell_A, zugara.eCherry_A, zugara.eSuika_A, zugara.eSeven_A, zugara.eReplay_A, zugara.eBell_A, zugara.eBrank_A, zugara.eSuika_A, zugara.eBrank_B, zugara.eReplay_A, zugara.eBell_A},
        {zugara.eReplay_A, zugara.eSuika_A, zugara.eSeven_C, zugara.eBrank_B, zugara.eBell_A, zugara.eReplay_A, zugara.eSuika_A, zugara.eBar_A, zugara.eBrank_B, zugara.eBell_A, zugara.eReplay_A, zugara.eSuika_A, zugara.eBrank_A, zugara.eCherry_A, zugara.eBell_A, zugara.eReplay_A, zugara.eSuika_A, zugara.eSeven_A, zugara.eCherry_A, zugara.eBell_A},
        {zugara.eBell_A, zugara.eReplay_A, zugara.eSeven_C, zugara.eSuika_A, zugara.eBrank_A, zugara.eBell_A, zugara.eReplay_A, zugara.eBar_A, zugara.eSuika_A, zugara.eBrank_A, zugara.eBell_A, zugara.eReplay_A, zugara.eSeven_A, zugara.eCherry_A, zugara.eBrank_A, zugara.eBell_A, zugara.eReplay_A, zugara.eBrank_B, zugara.eCherry_A, zugara.eBrank_A},
    };

    //// 制御定義
    //private Data.ReelControleData[] stopData = new Data.ReelControleData[]{
    //    new Data.ReelControleData{
    //        ControleName = "チャンス目",
    //        Left = new int[]{3,4,0,1,2,3,4,0,1,2,3,4,0,1,2,3,4,0,1,2,},
    //        Center = new int[]{4,0,1,2,3,4,0,1,2,3,4,0,1,2,3,4,0,1,2,3,},
    //        Right = new int[]{3,4,0,0,1,2,3,4,0,1,2,3,4,1,2,3,4,0,1,2,},
    //    },
    //    new Data.ReelControleData{
    //        ControleName = "強チェリー",
    //        Left = new int[]{2,3,4,0,0,1,2,1,2,0,1,2,1,2,3,4,3,4,0,1,},
    //        Center = new int[]{2,3,4,0,1,2,3,4,0,1,2,3,4,0,1,2,3,4,0,1,},
    //        Right = new int[]{3,4,0,1,2,3,4,0,1,2,3,4,0,1,2,1,2,0,1,2,},
    //    },
    //    new Data.ReelControleData{
    //        ControleName = "弱チェリー",
    //        Left = new int[]{2,3,4,0,0,1,2,1,2,0,1,2,1,2,3,4,3,4,0,1,},
    //        Center = new int[]{2,3,4,0,1,2,3,4,0,1,2,3,4,0,1,2,3,4,0,1,},
    //        Right = new int[]{0,1,2,3,4,0,1,2,3,4,0,1,2,3,4,0,1,2,3,4,},
    //    },
    //    new Data.ReelControleData{
    //        ControleName = "スイカ",
    //        Left = new int[]{3,4,0,1,2,3,4,0,1,2,3,4,0,1,2,3,4,0,1,2,},
    //        Center = new int[]{4,0,1,2,3,4,0,1,2,3,4,0,1,2,3,4,0,1,2,3,},
    //        Right = new int[]{3,4,0,1,2,3,4,0,1,2,3,4,0,1,2,3,4,0,1,2,},
    //    },
    //    new Data.ReelControleData{
    //        ControleName = "中段リプレイ",
    //        Left = new int[]{2,3,4,0,1,2,3,4,0,1,2,3,4,0,1,2,3,4,0,1,},
    //        Center = new int[]{0,1,2,3,4,0,1,2,3,4,0,1,2,3,4,0,1,2,3,4,},
    //        Right = new int[]{4,0,1,2,3,4,0,1,2,3,4,0,1,2,3,4,0,1,2,3,},
    //    },
    //    new Data.ReelControleData{
    //        ControleName = "赤/白7揃い",
    //        Left = new int[]{2,3,0,1,2,3,4,0,0,1,2,3,0,1,2,3,4,0,0,1,},
    //        Center = new int[]{3,4,0,1,2,3,4,2,3,4,0,1,2,3,4,0,1,0,1,2,},
    //        Right = new int[]{4,0,0,1,2,3,4,1,2,3,4,1,0,1,2,3,4,1,2,3,},
    //    },
    //    new Data.ReelControleData{
    //        ControleName = "リプレイC",
    //        Left = new int[]{3,4,0,1,2,3,4,0,1,2,3,4,0,1,2,3,4,0,1,2,},
    //        Center = new int[]{0,1,2,3,4,0,1,0,1,2,3,4,2,3,4,0,1,2,3,4,},
    //        Right = new int[]{3,4,0,1,2,3,4,0,1,2,3,4,0,1,2,3,4,0,1,2,},
    //    },
    //    new Data.ReelControleData{
    //        ControleName = "中段ベル揃い",
    //        Left = new int[]{1,2,3,4,0,1,2,3,4,0,1,2,3,4,0,1,2,3,4,0,},
    //        Center = new int[]{1,2,3,4,0,1,2,3,4,0,1,2,3,4,0,1,2,3,4,0,},
    //        Right = new int[]{0,1,2,3,4,0,1,2,3,4,0,1,2,3,4,0,1,2,3,4,},
    //    },
    //    new Data.ReelControleData{
    //        ControleName = "ベルこぼし",
    //        Left = new int[]{2,3,4,0,1,2,3,4,0,1,2,3,4,0,1,2,3,4,0,1,},
    //        Center = new int[]{3,4,0,1,2,3,4,0,1,2,3,4,0,1,2,3,4,0,1,2,},
    //        Right = new int[]{0,1,2,3,4,0,1,2,3,4,0,1,2,3,4,0,1,2,3,4,},
    //    },
    //    new Data.ReelControleData{
    //        ControleName = "ベルこぼし1枚",
    //        Left = new int[]{2,3,4,0,1,2,3,4,0,1,2,3,4,0,1,2,3,4,0,1,},
    //        Center = new int[]{3,4,0,1,2,3,4,0,1,2,3,4,0,1,2,3,4,0,1,2,},
    //        Right = new int[]{3,4,0,1,2,3,4,0,1,2,3,4,0,1,2,3,4,0,1,2,},
    //    },
    //};

    private int LeftSuberi;
    private int CenterSuberi;
    private int RightSuberi;
    // private SubMain subMain;
    private const float ReelStopVolume = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            reelControllers[i] = reels[i].GetComponent<ReelController>();
        }

        //Sound.LoadSe("Bet", "okidoki_se_3");
        //Sound.LoadSe("ReelStop", "00002a02");
        //Sound.LoadSe("ReelStart", "000029a2");
        //Sound.LoadBgm("StartBgm", "Undertale_Bgm_Home");
        //Sound.PlayBgm("StartBgm");
    }

    // Update is called once per frame
    void Update()
    {


        switch (mnState)
        {
            case MnState.BetWait:
                if (Input.GetKeyDown(KeyCode.UpArrow) == true || AutoPlayControle == true)
                {
                    // Bet処理
                    // ConsoleClearer.Clear();
                    AutoPlayControle = false;
                    Sound.PlaySe("Bet");
                    mnState = MnState.LeverWait;
                    Debug.Log("Bet");
                }
                break;
            case MnState.LeverWait:
                if (Input.GetKeyDown(KeyCode.UpArrow) == true || AutoPlayControle == true)
                {
                    // Lever処理
                    AutoPlayControle = false;
                    Sim.AllLever.Flow();            // 出玉処理
                    Sound.PlaySe("ReelStart", 0.5f);
                    reelControllers[0].ReelStart();
                    reelControllers[1].ReelStart();
                    reelControllers[2].ReelStart();
                    mnState = MnState.StopWait;
                    Debug.Log("Lever");
                }

                break;
            case MnState.StopWait:

                // リールが停止受付中のときに左を入力
                if ((Input.GetKeyDown(KeyCode.LeftArrow) == true || AutoPlayControle == true) && (reelControllers[0].reelState == ReelController.ReelState.StopWait))
                {
                    // 左入力
                    AutoPlayControle = false;
                    Sound.PlaySe("ReelStop", ReelStopVolume);
                    Debug.Log("LeftStop");
                    ReelControleFunc();
                    reelControllers[0].ReelStop(LeftSuberi);
                }

                // リールが停止受付中のときに下を入力
                if ((Input.GetKeyDown(KeyCode.DownArrow) == true || AutoPlayControle == true) && (reelControllers[1].reelState == ReelController.ReelState.StopWait))
                {
                    // 下入力
                    AutoPlayControle = false;
                    Sound.PlaySe("ReelStop", ReelStopVolume);
                    Debug.Log("CenterStop");
                    ReelControleFunc();
                    reelControllers[1].ReelStop(CenterSuberi);
                }

                // リールが停止受付中のときに下を入力
                if ((Input.GetKeyDown(KeyCode.RightArrow) == true || AutoPlayControle == true) && (reelControllers[2].reelState == ReelController.ReelState.StopWait))
                {
                    // 右入力
                    AutoPlayControle = false;
                    Sound.PlaySe("ReelStop", ReelStopVolume);
                    Debug.Log("RightStop");
                    ReelControleFunc();
                    reelControllers[2].ReelStop(RightSuberi);
                }

                // すべてのリールが停止状態？
                if ((reelControllers[0].reelState == ReelController.ReelState.Stop) &&
                    (reelControllers[1].reelState == ReelController.ReelState.Stop) &&
                    (reelControllers[2].reelState == ReelController.ReelState.Stop))
                {
                    mnState = MnState.Payout;
                    Debug.Log("AllStop");
                }

                break;
            case MnState.Payout:

                // PayOut処理
                if (Sim.FrtData.ResultFrtItem.hitType == Sim.HitType.replay) mnState = MnState.LeverWait;
                else mnState = MnState.BetWait;
                Debug.Log("PayOut");

                break;
            default:
                break;
        }

        if (AutoPlay == true && AutoPlayControle == false && FreamCnt == 0)
        {
            AutoPlayControle = true;
            FreamCnt = AutoPlaySpeed;
        }
        else
        {
            FreamCnt--;
            if (FreamCnt < 0) FreamCnt = 0;
        }

    }

    enum stopName
    {
        Chance = 0, // チャンス目   
        KyoChe,     // 強チェリー
        JakuChe,    // 弱チェリー
        Suika,      // スイカ
        Replay,     // 中段リプレイ
        Bonus,      // 赤/白7揃い
        Bar,        // リプレイC
        Bell,       // 中段ベル揃い
        BellKobo,   // ベルこぼし
        BellItimai, // ベルこぼし1枚
    }

    static System.Random random = new System.Random();
    public void ReelControleFunc()
    {

        switch ((Sim.FRT_CODE)Sim.FrtData.ResultFrtItem.lowResult)
        {
            case Sim.FRT_CODE.Bell_123:
                SetControle(stopName.Bell);
                break;
            case Sim.FRT_CODE.Bell_132:
                // 指定の出玉状態ではベルをこぼす（1/2で1枚）
                switch (Sim.DdmVariable.StartDdmMode)
                {
                    case Sim.DDMMODE.Nml:
                    case Sim.DDMMODE.BnsWait:
                        if (random.Next(2) == 0)
                        {
                            SetControle(stopName.BellItimai);
                        }
                        else
                        {
                            SetControle(stopName.BellKobo);
                        }
                        break;
                    default:
                        SetControle(stopName.Bell);
                        break;
                }
                break;
            case Sim.FRT_CODE.Bell_213:
            case Sim.FRT_CODE.Bell_231:
            case Sim.FRT_CODE.Bell_321:
            case Sim.FRT_CODE.Bell_312:
                // 指定の出玉状態ではベルをこぼす（1/4で1枚）
                switch (Sim.DdmVariable.StartDdmMode)
                {
                    case Sim.DDMMODE.Nml:
                    case Sim.DDMMODE.BnsWait:
                        if (random.Next(4) == 0)
                        {
                            SetControle(stopName.BellItimai);
                        }
                        else
                        {
                            SetControle(stopName.BellKobo);
                        }
                        break;
                    default:
                        SetControle(stopName.Bell);
                        break;
                }
                break;

            case Sim.FRT_CODE.Chance:
                SetControle(stopName.Chance);
                break;

            case Sim.FRT_CODE.JakuChe:
                SetControle(stopName.JakuChe);
                break;

            case Sim.FRT_CODE.KyoChe:
                SetControle(stopName.KyoChe);
                break;

            case Sim.FRT_CODE.RegReplay:
            case Sim.FRT_CODE.SBigReplay:
                switch (Sim.DdmVariable.StartDdmMode)
                {
                    case Sim.DDMMODE.BnsWait:
                        SetControle(stopName.Bonus);
                        break;
                    default:
                        SetControle(stopName.Replay);
                        break;
                }
                break;

            case Sim.FRT_CODE.Suika:
                SetControle(stopName.Suika);
                break;

            default:
                SetControle(stopName.BellKobo);
                break;
        }


    }

    // 制御セット
    private void SetControle(stopName ControleNum)
    {
#if false
        Debug.Log(stopData[(int)ControleNum].ControleName);
        int LeftStopComa = reelControllers[0].getComa();
        int CenterStopComa = reelControllers[1].getComa();
        int RightStopComa = reelControllers[2].getComa();
        LeftSuberi = stopData[(int)ControleNum].Left[LeftStopComa];
        CenterSuberi = stopData[(int)ControleNum].Center[CenterStopComa];
        RightSuberi = stopData[(int)ControleNum].Right[RightStopComa];
#endif

        LeftSuberi = 0;
        CenterSuberi = 0;
        RightSuberi = 0;
    }

    //    // コンソールクリアクラス
    //    public static class ConsoleClearer
    //    {
    //        [MenuItem("Tools/Clear Console &#c")]
    //        public static void Clear()
    //        {
    //            var type = Assembly
    //                .GetAssembly(typeof(SceneView))
    //#if UNITY_2017_1_OR_NEWER
    //            .GetType("UnityEditor.LogEntries")
    //#else
    //            .GetType( "UnityEditorInternal.LogEntries" )
    //#endif
    //        ;
    //            var method = type.GetMethod("Clear", BindingFlags.Static | BindingFlags.Public);
    //            method.Invoke(null, null);
    //        }
    //    }

}
