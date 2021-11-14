using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Mn : MonoBehaviour
{

#region enum

    /// <summary>
    /// 状態
    /// </summary>
    public enum GameState
    {
        BetWait,
        LeverWait,
        GameWait,
        ReelStartWait,
        StopWait,
        AllStop,
        Payout,
        Payout_Now,
        BnsEndWait,
    }

    /// <summary>
    /// 押し順
    /// </summary>
    public enum StopSeq
    {
        _1xx,
        _12x,
        _123,
        _1x2,
        _132,

        _x1x,
        _21x,
        _213,
        _x12,
        _312,

        _xx1,
        _2x1,
        _231,
        _x21,
        _321,
    }


    /// <summary>
    /// トリガー
    /// </summary>
    public enum Trigger
    {
        None,
        Bet,
        Lever,
        ReelStart,
        S1_On,
        S1_Off,
        S2_On,
        S2_Off,
        S3_On,
        S3_Off,
        Payout,
        Payout_Now,
        GameEnd
    }

    /// <summary>
    /// 図柄定義
    /// </summary>
    public enum zugara
    {
        Replay,
        Bell_A,
        Bell_B,
        Suika,
        Cherry,
        Brank,
        Seven_A,
        Seven_B,
        Bar,
        Homura,
    };

    public enum AutoPlayType
    {
        None,
        Infinity,
        BnsHit,
        RareHit,
    }

    enum ReelHeight
    {
        H,
        M,
        L
    };

    enum BetType
    { 
        _1Bet,
        _3Bet,
    };

    public enum DebugType
    {
        Normal,         // 通常抽せん
        DebugLot,       // デバッグ用抽せん
        SelectLot,      // 選択抽せん
    };

#endregion


    // 有効ライン
    List<ReelHeight[]>[] list = 
    {
        new List<ReelHeight[]>()
        {
            new ReelHeight[]{ ReelHeight.M, ReelHeight.M, ReelHeight.M },
            new ReelHeight[]{ ReelHeight.H, ReelHeight.M, ReelHeight.L },
            new ReelHeight[]{ ReelHeight.L, ReelHeight.M, ReelHeight.H },
        },

        new List<ReelHeight[]>()
        {
            new ReelHeight[]{ ReelHeight.H, ReelHeight.H, ReelHeight.H },
            new ReelHeight[]{ ReelHeight.M, ReelHeight.M, ReelHeight.M },
            new ReelHeight[]{ ReelHeight.L, ReelHeight.L, ReelHeight.L },
            new ReelHeight[]{ ReelHeight.H, ReelHeight.M, ReelHeight.L },
            new ReelHeight[]{ ReelHeight.L, ReelHeight.M, ReelHeight.H },
        }
    };

    public class CmbRecode
    {
        public string name;
        public int[] zugara;
        public bool replay;
        public int bnsMedal;
        public int[] pay;
    }


    // 図柄組み合わせ
    List<CmbRecode> cmbList = new List<CmbRecode>()
    {
        new CmbRecode()
        {
            name = "7BIG",
            zugara = new int[]{ 0,0,0 },
            bnsMedal = 284,
            pay = new int[]{ 0, 0 },
            replay = false,
        },

        new CmbRecode()
        {
            name = "ABIG_A",
            zugara = new int[]{ 1,1,1 },
            bnsMedal = 284,
            pay = new int[]{ 0, 0 },
            replay = false,
        },

        new CmbRecode()
        {
            name = "ABIG_B",
            zugara = new int[]{ 8,1,1 },
            bnsMedal = 284,
            pay = new int[]{ 0, 0 },
            replay = false,
        },

        new CmbRecode()
        {
            name = "ABIG_C",
            zugara = new int[]{ 1,1,8 },
            bnsMedal = 284,
            pay = new int[]{ 0, 0 },
            replay = false,
        },

        new CmbRecode()
        {
            name = "ABIG_D",
            zugara = new int[]{ 8,1,8 },
            bnsMedal = 284,
            pay = new int[]{ 0, 0 },
            replay = false,
        },

        new CmbRecode()
        {
            name = "REG_1",
            zugara = new int[]{ 0,0,2 },
            bnsMedal = 90,
            pay = new int[]{ 0, 0 },
            replay = false,
        },

        new CmbRecode()
        {
            name = "REG_2",
            zugara = new int[]{ 2,2,2 },
            bnsMedal = 90,
            pay = new int[]{ 0, 0 },
            replay = false,
        },

        //new CmbRecode()
        //{
        //    name = "チェリー_1",
        //    zugara = new int[]{ 5,3,6 },
        //    bnsMedal = 0,
        //    pay = new int[]{ 15, 1 },
        //    replay = false,
        //},

        new CmbRecode()
        {
            name = "チェリー_1",
            zugara = new int[]{ 5,6,6 },
            bnsMedal = 0,
            pay = new int[]{ 15, 1 },
            replay = false,
        },

        new CmbRecode()
        {
            name = "チェリー代替",
            zugara = new int[]{ 8,6,6 },
            bnsMedal = 0,
            pay = new int[]{ 15, 1},
            replay = false,
        },

        new CmbRecode()
        {
            name = "スイカ",
            zugara = new int[]{ 3,3,3 },
            bnsMedal = 0,
            pay = new int[]{ 15, 15 },
            replay = false,
        },

        new CmbRecode()
        {
            name = "スイカ代替",
            zugara = new int[]{ 1,3,3 },
            bnsMedal = 0,
            pay = new int[]{ 7, 0 },
            replay = false,
        },

        new CmbRecode()
        {
            name = "ベル",
            zugara = new int[]{ 4,4,4 },
            bnsMedal = 0,
            pay = new int[]{ 15, 8 },
            replay = false,
        },

        new CmbRecode()
        {
            name = "ベル代替",
            zugara = new int[]{ 2,4,4 },
            bnsMedal = 0,
            pay = new int[]{ 15, 8 },
            replay = false,
        },

        new CmbRecode()
        {
            name = "リプレイ",
            zugara = new int[]{ 6,6,6 },
            bnsMedal = 0,
            pay = new int[]{ 0, 0 },
            replay = true,
        },

        new CmbRecode()
        {
            name = "1枚役A",
            zugara = new int[]{ 0,1,7 },
            bnsMedal = 0,
            pay = new int[]{ 1, 1 },
            replay = false,
        },

        new CmbRecode()
        {
            name = "1枚役B",
            zugara = new int[]{ 3,7,0 },
            bnsMedal = 0,
            pay = new int[]{ 1, 1 },
            replay = false,
        },

        new CmbRecode()
        {
            name = "1枚役C",
            zugara = new int[]{ 7,9,9 },
            bnsMedal = 0,
            pay = new int[]{ 1, 1 },
            replay = false,
        },
    };

    // 停止位置
    int[,] stopPos =
    {
        { 0,0,0 },
        { 0,0,0 },
        { 0,0,0 },
    };

    public GameObject leftReelObject;
    public GameObject centerReelObject;
    public GameObject rightReelObject;
    public static ReelController leftReel;
    public static ReelController centerReel;
    public static ReelController rightReel;
    public static GameState gameState;
    public static int stopCnt = 0;
    public static StopSeq stopSeq;
    public AutoPlayType autoPlayType = AutoPlayType.None;
    private BetType betType;
    private SubMain subMain;
    private System.Random rand = new System.Random();
    public int bonusStartCount = 180;
    private int _bonusStartCount = 0;

    // 制御データ格納用
    public List<StopRecode> stopRecodes;

    // リールデータ
    public int[,] reel;

    // 今回の制御データ
    private StopRecode stopRecode;

    // Mnの抽せんデータ
    public BnsFrtLot bnsFrtLot;

    // 設定
    public AutoMakeCode.Enum.Settei settei;

    // Mn状態
    static public AutoMakeCode.Enum.Status mnStatus;

    // RT状態
    public AutoMakeCode.Enum.RT RT;

    // 特賞
    public static AutoMakeCode.Enum.BnsCode bnsCode;
    public AutoMakeCode.Enum.BnsCode _bnsCode;  // デバッグ用

    // 小役
    public static AutoMakeCode.Enum.FrtCode frtCode;
    public AutoMakeCode.Enum.FrtCode _frtCode;  // デバッグ用

    // 抽せん結果
    public BnsFrtLot.Recode lotResult;

    // 通常消化G数
    public static int nmlPlayGame = 0;

    // 当せん当該遊技フラグ
    public static bool hitBnsGameFlg;

    // 1st停止
    private StopRecode.Reel firstPushReel;

    // 押し順
    private StopRecode.PushSeq pushSeq;

    // 押下位置
    private int[] pushComa = { 0, 0, 0 };

    // 払い出し枚数
    private int pay;

    // 残りボーナス枚数
    private int bnsMedal;

    // デバッグ種類
    public DebugType debugType;

    // Wait時間
    public int WaitCount = 180;
    private int _WaitCount = 0;

    // デバッグ用抽せんリスト
    public AutoMakeCode.Enum.BnsCode[] debugBnsCode;
    public AutoMakeCode.Enum.FrtCode[] debugFrtCode;

    // セグリスト
    public GameObject[] segObjects;
    // セグ制御用
    private SegControle creditSeg;
    private SegControle bonusSeg;
    private SegControle payoutSeg;

    // Start is called before the first frame update
    void Start()
    {
        Sim.DdmVariable.HaveCoin = 50;
        leftReel = leftReelObject.GetComponent<ReelController>();
        centerReel = centerReelObject.GetComponent<ReelController>();
        rightReel = rightReelObject.GetComponent<ReelController>();
        subMain = new SubMain();
        // Sim.DdmVariable.Settei = Sim.SETTEI.Settei1;
        settei = AutoMakeCode.Enum.Settei._6;

        // 制御データ読み込み
        {
            stopRecodes = new List<StopRecode>();

            var inputString = Resources.LoadAll<TextAsset>("Json/StopRecodes");

            foreach (var item in inputString)
            {
                StopRecode inputJson = JsonConvert.DeserializeObject<StopRecode>(item.text);
                stopRecodes.Add(inputJson);
            }
        }

        // リールデータ読み込み
        {
            var inputString = Resources.Load<TextAsset>("Json/reel");
            reel = JsonConvert.DeserializeObject<int[,]>(inputString.text);
            Sim.DdmVariable.reel = JsonConvert.DeserializeObject<int[,]>(inputString.text);
        }

        // jsonからMn抽せんデータを読み取る
        {
            var inputString = Resources.Load<TextAsset>("Json/bnsFrtLot");
            bnsFrtLot = JsonConvert.DeserializeObject<BnsFrtLot>(inputString.text);
        }

        {
            creditSeg = segObjects[0].GetComponent<SegControle>();
            bonusSeg = segObjects[1].GetComponent<SegControle>();
            payoutSeg = segObjects[2].GetComponent<SegControle>();
        }

        // ボーナス用のセグは3桁表示
        bonusSeg.setNum(0, "D3");

        // 初期クレジットを設定
        creditSeg.setNum(50, "D2", false);
        creditSeg.setwaitCnt(3);
        bonusSeg.setwaitCnt(3);
        payoutSeg.setwaitCnt(3);
    }

    /// <summary>
    /// ウェイト計測用
    /// </summary>
    private void FixedUpdate()
    {
        if (_WaitCount > 0) _WaitCount--;
        if (_bonusStartCount > 0) _bonusStartCount--;
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameState)
        {
            case GameState.BetWait:
                if (Input.GetKeyDown(KeyCode.UpArrow) == true || autoPlayType != AutoPlayType.None)
                {
                    // Bet処理
                    Debug.ClearDeveloperConsole();
                    Debug.Log("Bet");
                    gameState = GameState.LeverWait;

                    payoutSeg.setNum(0, "D2", false);

                    switch (mnStatus)
                    {
                        case AutoMakeCode.Enum.Status.ABIGStandby:
                        case AutoMakeCode.Enum.Status.SBIGStandby:
                        case AutoMakeCode.Enum.Status.RBStandby:
                        case AutoMakeCode.Enum.Status.Nml:
                            bonusSeg.setNum(0, "D3", false);    // カウンター初期化
                            betType = BetType._3Bet;
                            if (creditSeg.getNum() < 3) creditSeg.setNum(0);
                            else creditSeg.setNum(creditSeg.getNum() - 3);
                            Sim.DdmVariable.In += 3;
                            break;
                        case AutoMakeCode.Enum.Status.ABIG:
                        case AutoMakeCode.Enum.Status.SBIG:
                        case AutoMakeCode.Enum.Status.RB:
                            betType = BetType._1Bet;
                            if (creditSeg.getNum() < 1) creditSeg.setNum(0);
                            else creditSeg.setNum(creditSeg.getNum() - 1);
                            Sim.DdmVariable.In += 1;
                            Sim.DdmVariable.BnsGet -= 1;
                            break;
                        default:
                            break;
                    }

                    subMain.Bet((int)betType);
                }
                break;
            case GameState.LeverWait:
                if (Input.GetKeyDown(KeyCode.UpArrow) == true || autoPlayType != AutoPlayType.None)
                {
                    // Lever処理
                    Debug.Log("Lever");

                    Sim.DdmVariable.startMnSts = mnStatus;
                    Sim.DdmVariable.TotalGames++;

                    switch (mnStatus)
                    {
                        case AutoMakeCode.Enum.Status.ABIGStandby:
                        case AutoMakeCode.Enum.Status.SBIGStandby:
                        case AutoMakeCode.Enum.Status.RBStandby:
                        case AutoMakeCode.Enum.Status.Nml:
                            Sim.DdmVariable.NmlGame++;
                            Sim.DdmVariable.TotalNmlGames++;
                            break;
                        default:
                            break;
                    }

                    switch (debugType)
                    {
                        case DebugType.Normal:
                            // 通常抽せん
                            lotResult = bnsFrtLot.Lot(settei, mnStatus, RT);
                            if (bnsCode == AutoMakeCode.Enum.BnsCode.Hazure) bnsCode = lotResult.bnsCode;
                            frtCode = lotResult.frtCode;

                            // デバッグ用
                            _frtCode = frtCode;
                            _bnsCode = bnsCode;

                            // 強制フラグ
                            if (Input.GetKey(KeyCode.Alpha1)) bnsCode = AutoMakeCode.Enum.BnsCode.RB;
                            if (Input.GetKey(KeyCode.Alpha2)) bnsCode = AutoMakeCode.Enum.BnsCode.SBIG;
                            if (Input.GetKey(KeyCode.Alpha3)) bnsCode = AutoMakeCode.Enum.BnsCode.ABIG;

                            // 一枚役
                            if(bnsCode != AutoMakeCode.Enum.BnsCode.Hazure)
                            {
                                if (Input.GetKey(KeyCode.A)) frtCode = AutoMakeCode.Enum.FrtCode.ItimaiA;
                                if (Input.GetKey(KeyCode.B)) frtCode = AutoMakeCode.Enum.FrtCode.ItimaiB;
                                if (Input.GetKey(KeyCode.C)) frtCode = AutoMakeCode.Enum.FrtCode.ItimaiC;
                            }

                            break;
                        case DebugType.DebugLot:
                            // 指定の内容で均等抽せん
                            System.Random r = new System.Random();
                            bnsCode = debugBnsCode[r.Next(debugBnsCode.Length)];
                            frtCode = debugFrtCode[r.Next(debugFrtCode.Length)];
                            break;
                        case DebugType.SelectLot:
                            // デバッグ用条件装置固定（Unity上で選択
                            bnsCode = _bnsCode;
                            frtCode = _frtCode; 
                            break;
                        default:
                            break;
                    }

                    // 抽せん結果デバッグ表示
                    Debug.Log(bnsCode + ":" + frtCode);

                    switch (bnsCode)
                    {
                        case AutoMakeCode.Enum.BnsCode.ABIG:
                            mnStatus = AutoMakeCode.Enum.Status.ABIGStandby;
                            break;
                        case AutoMakeCode.Enum.BnsCode.SBIG:
                            mnStatus = AutoMakeCode.Enum.Status.SBIGStandby;
                            break;
                        case AutoMakeCode.Enum.BnsCode.RB:
                            mnStatus = AutoMakeCode.Enum.Status.RBStandby;
                            break;
                        default:
                            break;
                    }

                    // 今回の制御データ
                    stopRecode = stopRecodes.Find(x => x.bnsCode == (int)bnsCode && x.frtCode == (int)frtCode);

                    // Mnの演出処理はここに記載
                    {
                        //Sim.AllLever.Flow();           // 出玉処理
                        // 当せん当該判定
                        if (Sim.DdmVariable.startMnSts == AutoMakeCode.Enum.Status.Nml && mnStatus != AutoMakeCode.Enum.Status.Nml)
                        {
                            hitBnsGameFlg = true;
                        }
                        else
                        {
                            hitBnsGameFlg = false;
                        }

                        // 通常消化G数加算
                        if (Sim.DdmVariable.startMnSts == AutoMakeCode.Enum.Status.Nml) nmlPlayGame++;

                        // // 1G目に単独ABIGでロングフリーズ
                        // if (nmlPlayGame == 1 && hitBnsGameFlg == true && (bnsCode == AutoMakeCode.Enum.BnsCode.ABIG || bnsCode == AutoMakeCode.Enum.BnsCode.SBIG) && frtCode == AutoMakeCode.Enum.FrtCode.Hazure) Sim.DdmVariable.FreezeType = Sim.FREEZE_TYPE.LongFreeze;
                        // else Sim.DdmVariable.FreezeType = Sim.FREEZE_TYPE.None;

                        FreezeFunc();                    // フリーズ条件に合っている場合はリール始動を遅らせる
                    }
                    subMain.Lever(mnStatus);

                    // ウェイトある場合はサブにウェイト告知
                    if(_WaitCount > 0)
                    {
                        Debug.Log("WaitStart");
                        subMain.WaitStart();
                    }

                    gameState = GameState.GameWait;
                    stopCnt = 0;

                    // 押し順を作成する
                    if(autoPlayType != AutoPlayType.None)
                    {
                        AutoStopMake();
                    }

                    // 初期化
                    Sim.DdmVariable.deme = new int[,]
                    {
                        { -1,-1,-1},
                        { -1,-1,-1},
                        { -1,-1,-1},
                    };
                }

                break;
            case GameState.GameWait:

                if (leftReel.reelState != ReelController.ReelState.Event)
                {
                    if (centerReel.reelState != ReelController.ReelState.Event)
                    {
                        if (rightReel.reelState != ReelController.ReelState.Event)
                        {
                            if (_WaitCount == 0)
                            {
                                Debug.Log("ReelStart");
                                _WaitCount = WaitCount;     // Wait再セット
                                leftReel.ReelStart();
                                centerReel.ReelStart();
                                rightReel.ReelStart();
                                gameState = GameState.ReelStartWait;
                            }
                        }
                    }
                }

                break;

            case GameState.ReelStartWait:
                if (leftReel.reelState == ReelController.ReelState.StopWait)
                {
                    if (centerReel.reelState == ReelController.ReelState.StopWait)
                    {
                        if (rightReel.reelState == ReelController.ReelState.StopWait)
                        {
                            //　すべてのリールが正常駆動開始
                            subMain.ReelStart();
                            gameState = GameState.StopWait;
                        }
                    }
                }
                break;

            case GameState.StopWait:

                // リールが停止受付中のときに左を入力
                if ((Input.GetKeyDown(KeyCode.LeftArrow) == true || IsAutoPlayStop(ReelController.Reel_ID.Left)) && (leftReel.reelState == ReelController.ReelState.StopWait))
                {
                    // 左入力
                    Debug.Log("LeftStop");
                    pushComa[stopCnt] = leftReel.getComa();     // stopCntをindexとして使うのでインクメント前に代入
                    stopCnt++;
                    StopSeqChange(ReelController.Reel_ID.Left);
                    //leftReel.ReelStop(getSuberiComa(ReelController.Reel_ID.Left, leftReel.getComa()));
                    switch (stopCnt)
                    {
                        case 1:
                            leftReel.ReelStop(stopRecode.getSuberiComa(firstPushReel, pushComa[0]));
                            break;
                        case 2:
                            leftReel.ReelStop(stopRecode.getSuberiComa(pushSeq, pushComa[0], pushComa[1]));
                            break;
                        case 3:
                            leftReel.ReelStop(stopRecode.getSuberiComa(pushSeq, pushComa[0], pushComa[1], pushComa[2]));
                            break;
                        default:
                            break;
                    }


                    // 停止コマ保存
                    {
                        int[] stopComa = leftReel.getStopComa();
                        for (int i = 0; i < stopComa.Length; i++)
                        {
                            Sim.DdmVariable.deme[i, 0] = stopComa[i];
                            //Debug.Log(stopComa[i]);
                        }
                    }

                    subMain.StopBtn(stopCnt, ReelController.Reel_ID.Left);
                }

                // リールが停止受付中のときに下を入力
                if ((Input.GetKeyDown(KeyCode.DownArrow) == true || IsAutoPlayStop(ReelController.Reel_ID.Center)) && (centerReel.reelState == ReelController.ReelState.StopWait))
                {
                    // 下入力
                    Debug.Log("CenterStop");
                    pushComa[stopCnt] = centerReel.getComa();     // stopCntをindexとして使うのでインクメント前に代入
                    stopCnt++;
                    StopSeqChange(ReelController.Reel_ID.Center);
                    //centerReel.ReelStop(getSuberiComa(ReelController.Reel_ID.Center, centerReel.getComa()));
                    switch (stopCnt)
                    {
                        case 1:
                            centerReel.ReelStop(stopRecode.getSuberiComa(firstPushReel, pushComa[0]));
                            break;
                        case 2:
                            centerReel.ReelStop(stopRecode.getSuberiComa(pushSeq, pushComa[0], pushComa[1]));
                            break;
                        case 3:
                            centerReel.ReelStop(stopRecode.getSuberiComa(pushSeq, pushComa[0], pushComa[1], pushComa[2]));
                            break;
                        default:
                            break;
                    }

                    // 停止コマ保存
                    {
                        int[] stopComa = centerReel.getStopComa();
                        for (int i = 0; i < stopComa.Length; i++)
                        {
                            Sim.DdmVariable.deme[i, 1] = stopComa[i];
                            //Debug.Log(stopComa[i]);
                        }
                    }

                    subMain.StopBtn(stopCnt, ReelController.Reel_ID.Center);
                }

                // リールが停止受付中のときに右を入力
                if ((Input.GetKeyDown(KeyCode.RightArrow) == true || IsAutoPlayStop(ReelController.Reel_ID.Right)) && (rightReel.reelState == ReelController.ReelState.StopWait))
                {
                    // 右入力
                    Debug.Log("RightStop");
                    pushComa[stopCnt] = rightReel.getComa();     // stopCntをindexとして使うのでインクメント前に代入
                    stopCnt++;
                    StopSeqChange(ReelController.Reel_ID.Right);
                    //rightReel.ReelStop(getSuberiComa(ReelController.Reel_ID.Right, rightReel.getComa()));
                    switch (stopCnt)
                    {
                        case 1:
                            rightReel.ReelStop(stopRecode.getSuberiComa(firstPushReel, pushComa[0]));
                            break;
                        case 2:
                            rightReel.ReelStop(stopRecode.getSuberiComa(pushSeq, pushComa[0], pushComa[1]));
                            break;
                        case 3:
                            rightReel.ReelStop(stopRecode.getSuberiComa(pushSeq, pushComa[0], pushComa[1], pushComa[2]));
                            break;
                        default:
                            break;
                    }

                    // 停止コマ保存
                    {
                        int[] stopComa = rightReel.getStopComa();
                        for (int i = 0; i < stopComa.Length; i++)
                        {
                            Sim.DdmVariable.deme[i, 2] = stopComa[i];
                            //Debug.Log(stopComa[i]);
                        }
                    }

                    subMain.StopBtn(stopCnt, ReelController.Reel_ID.Right);
                }

                // すべてのリールが停止状態？
                if ((leftReel.reelState == ReelController.ReelState.Stop) &&
                    (centerReel.reelState == ReelController.ReelState.Stop) &&
                    (rightReel.reelState == ReelController.ReelState.Stop))
                {
                    Debug.Log(pushSeq.ToString());
                    Debug.Log("AllStop");
                    gameState = GameState.AllStop;
                }

                break;
            case GameState.AllStop:
                if((Input.GetKey(KeyCode.LeftArrow) == false) &&
                    (Input.GetKey(KeyCode.DownArrow) == false) &&
                    (Input.GetKey(KeyCode.RightArrow) == false))
                {
                    gameState = GameState.Payout;
                }
                break;
            case GameState.Payout:

                // PayOut処理
                Debug.Log("PayOut");

                // 中段の停止位置代入
                stopPos[1, 0] = leftReel.getComa();
                stopPos[1, 1] = centerReel.getComa();
                stopPos[1, 2] = rightReel.getComa();

                // 上下に拡張
                for (int i = 0; i < stopPos.GetUpperBound(0) + 1; i++)
                {
                    stopPos[0, i] = stopPos[1, i] - 1 < 0 ? stopPos[1, i] + reel.GetUpperBound(1) : stopPos[1, i] - 1;
                    stopPos[2, i] = stopPos[1, i] + 1 > reel.GetUpperBound(1) ? stopPos[1, i] - reel.GetUpperBound(1) : stopPos[1, i] + 1;
                }

                // 有効ライン上の図柄
                List<int[]> judgeList = new List<int[]>();
                foreach (var item in list[(int)betType])
                {
                    int[] ary = new int[3];
                    for (int x = 0; x < 3; x++)
                    {
                        ary[x] = reel[x, stopPos[(int)item[x], x]];
                    }
                    judgeList.Add(ary);
                }

                // 停止した図柄組合せ判定
                List<CmbRecode> stopCmb = new List<CmbRecode>();
                foreach (CmbRecode item in cmbList)
                {
                    foreach (var judgeRecode in judgeList)
                    {
                        if (item.zugara.SequenceEqual(judgeRecode) == true)
                        {
                            // 停止した図柄組み合わせに追加
                            stopCmb.Add(item);
                        }
                    }
                }

                pay = 0;
                gameState = GameState.Payout_Now;
                foreach (CmbRecode item in stopCmb)
                {
                    Debug.Log(item.name);
                    // リプレイが作動した場合はBetスキップ
                    if (item.replay == true)
                    {
                        gameState = GameState.LeverWait;
                        subMain.PayEnd(mnStatus);
                        subMain.GameEnd();
                    }

                    pay += item.pay[(int)betType];
                    if (pay > 15) pay = 15;     // 15枚以上は超えないよう補正


                    switch (item.name)
                    {
                        case "7BIG":
                            Sim.DdmVariable.BnsGet = 0;
                            Sim.DdmVariable.SBIG++;
                            _bonusStartCount = bonusStartCount;
                            nmlPlayGame = 0;
                            subMain.BonusStart(bnsCode);
                            mnStatus = AutoMakeCode.Enum.Status.SBIG;
                            bnsMedal = item.bnsMedal;
                            bnsCode = AutoMakeCode.Enum.BnsCode.Hazure;
                            bonusSeg.setNum(bnsMedal + 1, "D3" ,false);
                            debugType = DebugType.Normal;
                            break;
                        case "ABIG_A":
                        case "ABIG_B":
                        case "ABIG_C":
                        case "ABIG_D":
                            if(mnStatus != AutoMakeCode.Enum.Status.ABIG)
                            {
                                Sim.DdmVariable.BnsGet = 0;
                                Sim.DdmVariable.ABIG++;
                                _bonusStartCount = bonusStartCount;
                                nmlPlayGame = 0;
                                subMain.BonusStart(bnsCode);
                                mnStatus = AutoMakeCode.Enum.Status.ABIG;
                                bnsMedal = item.bnsMedal;
                                bnsCode = AutoMakeCode.Enum.BnsCode.Hazure;
                                bonusSeg.setNum(bnsMedal + 1, "D3", false);
                                debugType = DebugType.Normal;
                            }
                            break;
                        case "REG_1":
                        case "REG_2":
                            Sim.DdmVariable.BnsGet = 0;
                            Sim.DdmVariable.RB++;
                            nmlPlayGame = 0;
                            subMain.BonusStart(bnsCode);
                            mnStatus = AutoMakeCode.Enum.Status.RB;
                            bnsMedal = item.bnsMedal;
                            bnsCode = AutoMakeCode.Enum.BnsCode.Hazure;
                            bonusSeg.setNum(bnsMedal + 1, "D3" ,false);
                            debugType = DebugType.Normal;
                            break;
                        case "チェリー_1":
                        case "チェリー_2":
                        case "チェリー代替":
                            break;
                        case "スイカ":
                        case "スイカ代替":
                            break;
                        case "ベル":
                        case "ベル代替":
                            break;
                        case "リプレイ":
                            break;
                        case "1枚役A":
                        case "1枚役B":
                        case "1枚役C":
                            break;
                        default:
                            break;
                    }
                }

                if(pay > 0)
                {
                    Debug.Log($"payout:{pay}");
                    payoutSeg.setNum(pay);
                    if (creditSeg.getNum() + pay > 50) creditSeg.setNum(50);
                    else creditSeg.setNum(creditSeg.getNum() + pay);
                    Sim.DdmVariable.Out += (ulong)pay;
                }

                subMain.PayOut(stopCmb, (int)betType, mnStatus);

                // ボーナス中の払い出し枚数
                switch (mnStatus)
                {
                    case AutoMakeCode.Enum.Status.ABIG:
                    case AutoMakeCode.Enum.Status.SBIG:
                    case AutoMakeCode.Enum.Status.RB:
                        Sim.DdmVariable.BnsOut += (ulong)pay;
                        Sim.DdmVariable.BnsGet += pay;
                        bnsMedal -= pay;
                        if(bnsMedal < 0)
                        {
                            Sim.DdmVariable.NmlGame = 0;
                            subMain.BonudEnd();
                            mnStatus = AutoMakeCode.Enum.Status.Nml;
                            bonusSeg.setNum(0, "D3", false);
                        }
                        else
                        {
                            bonusSeg.setNum(bonusSeg.getNum() - pay, "D3");
                        }
                        break;
                    default:
                        break;
                }

                Debug.Log($"Mn状態:{mnStatus}");
                

                break;

            case GameState.Payout_Now:
                if ((payoutSeg.getAnime() == false) && (_bonusStartCount == 0))
                {
                    // ボーナス終了の払い出し完了で獲得枚数表示
                    if (Sim.DdmVariable.startMnSts != AutoMakeCode.Enum.Status.Nml && mnStatus == AutoMakeCode.Enum.Status.Nml)
                    {
                        gameState = GameState.BnsEndWait;

                        switch (Sim.DdmVariable.startMnSts)
                        {
                            case AutoMakeCode.Enum.Status.ABIG:
                            case AutoMakeCode.Enum.Status.SBIG:
                                _bonusStartCount = bonusStartCount;     // BIG終了時は指定フレームwait
                                break;
                            case AutoMakeCode.Enum.Status.RB:
                                // RBは即終了
                                break;
                            default:
                                break;
                        }

                        subMain.PayEnd(mnStatus);
                        subMain.GameEnd();
                        bonusSeg.setNum(Sim.DdmVariable.BnsGet, "D3", false);
                    }
                    else
                    {
                        gameState = GameState.BetWait;
                        subMain.PayEnd(mnStatus);
                        subMain.GameEnd();
                    }
                }
                break;

            case GameState.BnsEndWait:
                if(_bonusStartCount == 0)
                {
                    gameState = GameState.BetWait;
                }
                break;

            default:
                break;
        }

        DebugFunc();

        AutoPlayFunc();

        subMain.UpData();
    }

    private void DebugFunc()
    {
        // デバッグ操作はDボタンを押しながら別のボタンで作動
        if (Input.GetKey(KeyCode.D) == true)
        {
            // Wはウェイトカット切り替え
            if (Input.GetKeyDown(KeyCode.W) == true)
            {
                if (WaitCount == 0)
                {
                    Debug.Log("ウェイトON");
                    WaitCount = 180;
                    bonusStartCount = 240;
                }
                else
                {
                    _WaitCount = 0;
                    WaitCount = 0;
                    bonusStartCount = 0;
                    _bonusStartCount = 0;
                    Debug.Log("ウェイトOFF");
                }
            }
        }

        if (gameState == GameState.GameWait)
        {
            // ボーナスショートカット
            if((mnStatus == AutoMakeCode.Enum.Status.ABIG) || (mnStatus == AutoMakeCode.Enum.Status.SBIG) || (mnStatus == AutoMakeCode.Enum.Status.RB))
            {
                if ((Input.GetKey(KeyCode.B) == true) && (Input.GetKey(KeyCode.C) == true))
                {
                    if(bonusSeg.getNum() > 1)
                    {
                        Debug.Log("ボーナスショートカット");
                        bnsMedal = 1;
                        bonusSeg.setNum(1, "D3", false);
                    }
                }
            }
        }
    }

    private void StopSeqChange(ReelController.Reel_ID reel)
    {
        switch (stopCnt)
        {
            case 1:
                switch (reel)
                {
                    case ReelController.Reel_ID.Left:
                        firstPushReel = StopRecode.Reel.L;
                        stopSeq = StopSeq._1xx;
                        break;
                    case ReelController.Reel_ID.Center:
                        firstPushReel = StopRecode.Reel.C;
                        stopSeq = StopSeq._x1x;
                        break;
                    case ReelController.Reel_ID.Right:
                        firstPushReel = StopRecode.Reel.R;
                        stopSeq = StopSeq._xx1;
                        break;
                    default:
                        break;
                }
                break;
            case 2:
                switch (reel)
                {
                    case ReelController.Reel_ID.Left:
                        switch (stopSeq)
                        {
                            case StopSeq._x1x:
                                pushSeq = StopRecode.PushSeq.CLR;
                                stopSeq = StopSeq._21x;
                                break;
                            case StopSeq._xx1:
                                pushSeq = StopRecode.PushSeq.RLC;
                                stopSeq = StopSeq._2x1;
                                break;
                            default:
                                break;
                        }
                        break;
                    case ReelController.Reel_ID.Center:
                        switch (stopSeq)
                        {
                            case StopSeq._1xx:
                                pushSeq = StopRecode.PushSeq.LCR;
                                stopSeq = StopSeq._12x;
                                break;
                            case StopSeq._xx1:
                                pushSeq = StopRecode.PushSeq.RCL;
                                stopSeq = StopSeq._x21;
                                break;
                            default:
                                break;
                        }
                        break;
                    case ReelController.Reel_ID.Right:
                        switch (stopSeq)
                        {
                            case StopSeq._x1x:
                                pushSeq = StopRecode.PushSeq.CRL;
                                stopSeq = StopSeq._x12;
                                break;
                            case StopSeq._1xx:
                                pushSeq = StopRecode.PushSeq.LRC;
                                stopSeq = StopSeq._1x2;
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
                break;
            case 3:
                switch (reel)
                {
                    case ReelController.Reel_ID.Left:
                        switch (stopSeq)
                        {
                            case StopSeq._x12:
                                stopSeq = StopSeq._312;
                                break;
                            case StopSeq._x21:
                                stopSeq = StopSeq._321;
                                break;
                            default:
                                break;
                        }
                        break;
                    case ReelController.Reel_ID.Center:
                        switch (stopSeq)
                        {
                            case StopSeq._1x2:
                                stopSeq = StopSeq._132;
                                break;
                            case StopSeq._2x1:
                                stopSeq = StopSeq._231;
                                break;
                            default:
                                break;
                        }
                        break;
                    case ReelController.Reel_ID.Right:
                        switch (stopSeq)
                        {
                            case StopSeq._12x:
                                stopSeq = StopSeq._123;
                                break;
                            case StopSeq._21x:
                                stopSeq = StopSeq._213;
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// リール制御
    /// </summary>
    public int getSuberiComa(ReelController.Reel_ID reel, int stopComa)
    {
        return 0;

        //switch ((Sim.FRT_CODE)Sim.FrtData.ResultFrtItem.lowResult)
        //{
        //    case Sim.FRT_CODE.Chance:
        //        Data.ChanceControl chanceControl = new Data.ChanceControl();
        //        return chanceControl.getSuberiComa(reel, stopComa);
        //    case Sim.FRT_CODE.KyoChe:
        //        Data.KyoChe kyoChe = new Data.KyoChe();
        //        return kyoChe.getSuberiComa(reel, stopComa);
        //    case Sim.FRT_CODE.JakuChe:
        //        Data.JakuChe jakuChe = new Data.JakuChe();
        //        return jakuChe.getSuberiComa(reel, stopComa);
        //    case Sim.FRT_CODE.Suika:
        //        Data.Suika suika = new Data.Suika();
        //        return suika.getSuberiComa(reel, stopComa);
        //    case Sim.FRT_CODE.HomuraReplay:
        //        Data.HomuraReplay homuraReplay = new Data.HomuraReplay();
        //        return homuraReplay.getSuberiComa(reel, stopComa);
        //    case Sim.FRT_CODE.RegReplay:
        //        Data.RegReplay regReplay = new Data.RegReplay();
        //        return regReplay.getSuberiComa(reel, stopComa);
        //    case Sim.FRT_CODE.BigReplay:
        //        Data.BigReplay bigReplay = new Data.BigReplay();
        //        return bigReplay.getSuberiComa(reel, stopComa);
        //    case Sim.FRT_CODE.SBigReplay:
        //        Data.SBigReplay sBigReplay = new Data.SBigReplay();
        //        return sBigReplay.getSuberiComa(reel, stopComa);
        //    case Sim.FRT_CODE.Bell_123:
        //        Data.Bell_123 bell_123 = new Data.Bell_123();
        //        return bell_123.getSuberiComa(reel, stopComa);
        //    case Sim.FRT_CODE.Bell_132:
        //        Data.Bell_132 bell_132 = new Data.Bell_132();
        //        return bell_132.getSuberiComa(reel, stopComa);
        //    case Sim.FRT_CODE.Bell_213:
        //        Data.Bell_213 bell_213 = new Data.Bell_213();
        //        return bell_213.getSuberiComa(reel, stopComa);
        //    case Sim.FRT_CODE.Bell_231:
        //        Data.Bell_231 bell_231 = new Data.Bell_231();
        //        return bell_231.getSuberiComa(reel, stopComa);
        //    case Sim.FRT_CODE.Bell_321:
        //        Data.Bell_321 bell_321 = new Data.Bell_321();
        //        return bell_321.getSuberiComa(reel, stopComa);
        //    case Sim.FRT_CODE.Bell_312:
        //        Data.Bell_312 bell_312 = new Data.Bell_312();
        //        return bell_312.getSuberiComa(reel, stopComa);
        //    case Sim.FRT_CODE.Hazure:
        //        Data.Hazure hazure = new Data.Hazure();
        //        return hazure.getSuberiComa(reel, stopComa);
        //    default:
        //        return 0;
        //}
    }

    private void FreezeFunc()
    {
        switch (Sim.DdmVariable.FreezeType)
        {
            case Sim.FREEZE_TYPE.None:
                // leftReel.SetWait(1);
                // centerReel.SetWait(1);
                // rightReel.SetWait(1);
                break;
            case Sim.FREEZE_TYPE.LongFreeze:
                Debug.Log("ロングフリーズ発生");
                leftReel.SetEvent();
                centerReel.SetEvent();
                rightReel.SetEvent();
                break;
            case Sim.FREEZE_TYPE.RushStart:
                if (Sim.DdmVariable.AtMode == Sim.RUSH_MODE._90)
                {
                    if(Sim.DdmVariable.FreezeFlg == false)
                    {
                        Sound.PlaySe("UltimateHit");
                        Sim.DdmVariable.FreezeFlg = true;
                    }
                }
                if(Sim.DdmVariable.FreezeFlg == true)
                {
                    Debug.Log("アルティメットバトル");
                    leftReel.SetWait(50 * 21);
                    centerReel.SetWait(50 * 21);
                    rightReel.SetWait(50 * 21);
                }
                else
                {
                    Debug.Log("ワルプルギスの夜突入");
                    leftReel.SetWait(50 * 20);
                    centerReel.SetWait(50 * 20);
                    rightReel.SetWait(50 * 20);

                }
                break;
            default:
                break;
        }
    }

    private void AutoPlayFunc()
    {
        //switch (autoPlayType)
        //{
        //    case AutoPlayType.None:
        //        if (Input.GetKeyDown(KeyCode.Escape)) autoPlayType = AutoPlayType.Infinity;
        //        break;
        //    case AutoPlayType.Infinity:
        //        if (Input.GetKeyDown(KeyCode.Escape)) autoPlayType = AutoPlayType.None;
        //        break;
        //    case AutoPlayType.BnsHit:
        //        if (Input.GetKeyDown(KeyCode.Escape)) autoPlayType = AutoPlayType.None;
        //        if (Sim.DdmVariable.StartDdmMode == Sim.DDMMODE.BnsWait) autoPlayType = AutoPlayType.None;
        //        break;
        //    case AutoPlayType.RareHit:
        //        if (Input.GetKeyDown(KeyCode.Escape)) autoPlayType = AutoPlayType.None;
        //        if (gameState == GameState.StopWait)
        //        {
        //            switch (Sim.DdmVariable.FrtCode)
        //            {
        //                case Sim.FRT_CODE.Chance:
        //                case Sim.FRT_CODE.KyoChe:
        //                case Sim.FRT_CODE.JakuChe:
        //                case Sim.FRT_CODE.Suika:
        //                    autoPlayType = AutoPlayType.None;
        //                    break;
        //                default:
        //                    break;
        //            }
        //        }
        //        break;
        //    default:
        //        break;
        //}

        //if(Input.GetKey(KeyCode.A) == true)
        //{
        //    if (Input.GetKeyDown(KeyCode.R) == true) autoPlayType = AutoPlayType.RareHit;
        //    if (Input.GetKeyDown(KeyCode.B) == true) autoPlayType = AutoPlayType.BnsHit;
        //}
    }

    List<ReelController.Reel_ID> stopList = new List<ReelController.Reel_ID>();
    /// <summary>
    /// 自動停止時の押し順を作成する
    /// </summary>
    private void AutoStopMake()
    {
        stopList.Clear();
        switch (Sim.DdmVariable.StartDdmMode)
        {
            case Sim.DDMMODE.Bns:
            case Sim.DDMMODE.Rush:
            case Sim.DDMMODE.AtBns:
                switch (Sim.DdmVariable.FrtCode)
                {
                    case Sim.FRT_CODE.Bell_123:
                        stopList.Add(ReelController.Reel_ID.Left);
                        stopList.Add(ReelController.Reel_ID.Center);
                        stopList.Add(ReelController.Reel_ID.Right);
                        break;
                    case Sim.FRT_CODE.Bell_132:
                        stopList.Add(ReelController.Reel_ID.Left);
                        stopList.Add(ReelController.Reel_ID.Right);
                        stopList.Add(ReelController.Reel_ID.Center);
                        break;
                    case Sim.FRT_CODE.Bell_213:
                        stopList.Add(ReelController.Reel_ID.Center);
                        stopList.Add(ReelController.Reel_ID.Left);
                        stopList.Add(ReelController.Reel_ID.Right);
                        break;
                    case Sim.FRT_CODE.Bell_231:
                        stopList.Add(ReelController.Reel_ID.Right);
                        stopList.Add(ReelController.Reel_ID.Left);
                        stopList.Add(ReelController.Reel_ID.Center);
                        break;
                    case Sim.FRT_CODE.Bell_321:
                        stopList.Add(ReelController.Reel_ID.Right);
                        stopList.Add(ReelController.Reel_ID.Center);
                        stopList.Add(ReelController.Reel_ID.Left);
                        break;
                    case Sim.FRT_CODE.Bell_312:
                        stopList.Add(ReelController.Reel_ID.Center);
                        stopList.Add(ReelController.Reel_ID.Right);
                        stopList.Add(ReelController.Reel_ID.Left);
                        break;
                    default:
                        stopList.Add(ReelController.Reel_ID.Left);
                        stopList.Add(ReelController.Reel_ID.Center);
                        stopList.Add(ReelController.Reel_ID.Right);
                        break;
                }
                break;
            default:
                stopList.Add(ReelController.Reel_ID.Left);
                stopList.Add(ReelController.Reel_ID.Center);
                stopList.Add(ReelController.Reel_ID.Right);
                break;
        }
    }

    /// <summary>
    /// オート中の押し順を管理する
    /// </summary>
    /// <returns></returns>
    private bool IsAutoPlayStop(ReelController.Reel_ID reel)
    {
        if (autoPlayType == AutoPlayType.None) return false;

        if (stopList.Count == 0) return false;

        if(stopList[0] == reel)
        {
            stopList.Remove(reel);
            return true;
        }
        else
        {
            return false;
        }
    }
}
