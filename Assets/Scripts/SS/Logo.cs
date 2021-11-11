using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logo : MonoBehaviour
{
    public enum Status
    {
        ON,
        OFF,
        FLASH,   // フラッシュ制御中
    }
    public Status status = Status.ON;
    public UnityEngine.UI.Image image;

    // フラッシュ制御用
    public int flashCount = 4;          // 点滅回数
    public int flashChangeFream = 10;   // 指定のフレーム経過でON/OFF切り替え
    public int flashFream = 0;          // 制御中のトータルフレーム回数

    // フラッシュカラー
    private Color flasfColor = Color.white;

    // 点灯カラー
    private Color nmlColor = Color.white;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        switch (status)
        {
            case Status.ON:
                image.color = nmlColor;
                break;
            case Status.OFF:
                image.color = Color.gray;
                break;
            case Status.FLASH:
                flashMain();
                break;
            default:
                break;
        }
    }

    public void lampOn()
    {
        status = Status.ON;
    }

    public void lampOff()
    {
        status = Status.OFF;
    }

    public void flashStart(Color color)
    {
        // フラッシュ状態移行時の初期設定
        flasfColor = color;
        status = Status.FLASH;
        flashFream = 0;
    }

    public void setColor(Color color)
    {
        nmlColor = color;
    }

    /// <summary>
    /// フラッシュ制御メイン
    /// </summary>
    private void flashMain()
    {
        flashFream++;
        if(flashFream % flashChangeFream == 0)
        {
            if(image.color == flasfColor)
            {
                // 消灯
                image.color = Color.gray;
            }
            else
            {
                // 点灯
                image.color = flasfColor;
            }
        }

        // 点灯制御に戻す
        if (flashChangeFream * flashCount == flashFream) status = Status.ON;
    }
}
