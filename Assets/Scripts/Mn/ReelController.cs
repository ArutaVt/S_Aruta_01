using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReelController : MonoBehaviour
{
    // 左中右リールの定義用
    public enum Reel_ID
    {
        Left = 0,
        Center,
        Right
    }

    // 最大コマ数
    public int MaxComa = 21;

    // リールサイズ
    public int ReelSize = 1050;

    // リールの回転速度
    public float reelSpeed = 25;

    // リールの初期位置
    public int startComa = 0;

    // リールのID
    public Reel_ID reelId;

    // Waitフレーム
    public int waitFream = 0;

    // 押下コマ(中段
    private int _pushComa = 0;

    // 停止コマ(中段
    private int _stopComa = 0;

    // リール画像
    private UnityEngine.UI.Image image;

    // リールステータス
    public enum ReelState
    {
        Stop,           // 停止中
        Wait,           // Wait
        Event,          // 回胴演出
        StopWait,       // 停止待ち
        Controle,       // 制御中
    }
    public ReelState reelState = ReelState.Stop;    // 初期状態は停止中

    // リールの回転方向
    public enum RotationDirection
    {
        Regular,    // 正回転
        Inverse,    // 逆回転
    }
    public RotationDirection rotationDirection = RotationDirection.Regular;     // 初期回転方向は正回転

    // リールの表示位置
    RectTransform pos;

    // 停止目標座標
    private Vector3 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        pos = GetComponent<RectTransform>();         // 現在座標取得
        pos.localPosition = new Vector3(pos.localPosition.x, (startComa) * (ReelSize / MaxComa), pos.localPosition.z);    // 初期座標
        image = GetComponent<UnityEngine.UI.Image>();   // 画像データ取得
    }

    // Update is called once per frame
    void Update()
    {
        switch(reelState)
        {
            case ReelState.Stop:
                break;
            case ReelState.Wait:
                break;
            case ReelState.StopWait:
                // RotationUpDate();
                break;
            case ReelState.Controle:
                // StopControle();
                break;
            default:
                // Debug.Log("ReelController");
                break;
        }   
    }

    private void FixedUpdate()
    {
        switch (reelState)
        {
            case ReelState.Stop:
                break;
            case ReelState.Wait:
                if (waitFream == 0)
                {
                    reelState = ReelState.StopWait;
                }
                else
                {
                    waitFream--;
                }
                break;
            case ReelState.Event:
                if (waitFream == 0)
                {
                    if(eventList.Count == 0)
                    {
                        reelState = ReelState.StopWait;     // 演出終了
                    }
                    else
                    {
                        ActEvent(); // 次のリール演出へ
                    }
                }
                else
                {
                    waitFream--;
                }
                RotationUpDate();
                break;
            case ReelState.StopWait:
                RotationUpDate();
                break;
            case ReelState.Controle:
                StopControle();
                break;
            default:
                Debug.Log("ReelController");
                break;
        }
    }

    public void LightOn(){ image.color = Color.white; }
    public void LightOff() { image.color = Color.gray; }

    // 回転開始
    public void ReelStart()
    {
        reelState = ReelState.Wait;
    }

    // リールの停止要求(現在はビタ停止）
    public void ReelStop(int suberiComa)
    {
        int stopComa = (int)pos.localPosition.y;    // 現在座標を取得
        stopComa /= (ReelSize/MaxComa);                             // 停止コマを算出（0～
        _pushComa = stopComa;   // 押下位置を保持しておく
        Debug.Log(reelId +  " : 押下位置 : " + stopComa + " : 滑りコマ : " + suberiComa);
        stopComa -= suberiComa;                     // 滑りコマを加算
        if (stopComa < 0) stopComa += MaxComa;    // もし切れ目を超えた場合はコマ数を補正

        //リバース時はプラス1コマ(↑)を参照するため補正する。
        if ((stopComa + (int)rotationDirection) > MaxComa)
        {
            stopComa = 0; 
        }
        else
        {
            stopComa = (stopComa + (int)rotationDirection);
        }

        targetPosition = new Vector3(pos.localPosition.x, (stopComa * (ReelSize / MaxComa)), pos.localPosition.z);
        reelState = ReelState.Controle;
        _stopComa = stopComa;   // 停止位置を保持しておく
        Debug.Log(reelId + " : 停止位置 : " + stopComa + " : 座標 : " + targetPosition.y);
    }

    // 回転処理
    void RotationUpDate()
    {
        if (rotationDirection == RotationDirection.Regular)
        {
            // 正回転
            if ((pos.localPosition.y - reelSpeed) < 0)
            {
                pos.localPosition = new Vector3(pos.localPosition.x, pos.localPosition.y + ReelSize - reelSpeed, pos.localPosition.z);
            }
            else
            {
                pos.localPosition = new Vector3(pos.localPosition.x, pos.localPosition.y - reelSpeed, pos.localPosition.z);
            }
        }
        else
        {
            // 逆回転
            if ((pos.localPosition.y - reelSpeed) >= ReelSize)
            {
                pos.localPosition = new Vector3(pos.localPosition.x, pos.localPosition.y - ReelSize + reelSpeed, pos.localPosition.z);
            }
            else
            {
                pos.localPosition = new Vector3(pos.localPosition.x, pos.localPosition.y + reelSpeed, pos.localPosition.z);
            }
        }
    }

    // 目標座標まで制御
    void StopControle()
    {
        if(Mathf.Abs(pos.localPosition.y - targetPosition.y) <= reelSpeed)
        {
            // 今回のリール移動で目標座標を越える場合は停止先を目標座標にする
            pos.localPosition = targetPosition;
            reelState = ReelState.Stop;
        }
        else
        {
            if (rotationDirection == RotationDirection.Regular)
            {
                // 正回転
                if ((pos.localPosition.y - reelSpeed) < 0)
                {
                    pos.localPosition = new Vector3(pos.localPosition.x, pos.localPosition.y + ReelSize, pos.localPosition.z);
                }
                else
                {
                    pos.localPosition = new Vector3(pos.localPosition.x, pos.localPosition.y - reelSpeed, pos.localPosition.z);
                }
            }
            else
            {
                // 逆回転
                if ((pos.localPosition.y - reelSpeed) >= ReelSize)
                {
                    pos.localPosition = new Vector3(pos.localPosition.x, pos.localPosition.y - ReelSize, pos.localPosition.z);
                }
                else
                {
                    pos.localPosition = new Vector3(pos.localPosition.x, pos.localPosition.y + reelSpeed, pos.localPosition.z);
                }
            }
        }
    }

    // 現在のコマ数(中段を返す)
    public int getComa()
    {
        int nowComa = (int)pos.localPosition.y;    // 現在座標を取得
        nowComa /= (ReelSize / MaxComa);                             // 停止コマを算出（0～
        //nowComa++; // 中段基準に変換
        if (nowComa >= MaxComa) nowComa -= MaxComa;
        return nowComa;
    }

    /// <summary>
    /// 停止コマを返す（中段
    /// </summary>
    /// <returns></returns>
    public int[] getStopComa()
    {
        int[] ret = { -1, -1, -1 };

        // 上段
        ret[0] = _stopComa - 1 < 0 ? (_stopComa - 1) + MaxComa : _stopComa - 1;

        // 中段
        ret[1] = _stopComa;

        // 下段
        ret[2] = _stopComa + 1 >= MaxComa ? (_stopComa + 1) - MaxComa : _stopComa + 1;

        return ret;
    }

    public void SetWait(int value)
    {
        waitFream = value;
        reelState = ReelState.Wait;
    }

    public class EventData
    {
        public float speed = 0;
        public int time = 0;
        public ReelController.RotationDirection rotation;
    }

    public List<ReelController.EventData> eventList = new List<EventData>();

    public void SetEvent()
    {
        eventList.Add(new ReelController.EventData()
        {
            speed = 5,
            time = 300,
            rotation = ReelController.RotationDirection.Inverse,
        });
        eventList.Add(new ReelController.EventData()
        {
            speed = 37,
            time = 1,
            rotation = ReelController.RotationDirection.Regular,
        });

        reelState = ReelState.Event;
        ActEvent();
    }

    private void ActEvent()
    {
        if(eventList.Count > 0)
        {
            reelSpeed = eventList[0].speed;
            waitFream = eventList[0].time;
            rotationDirection = eventList[0].rotation;
            eventList.RemoveAt(0);
        }
    }
}
