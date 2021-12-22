using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetLampControler : MonoBehaviour
{
    UnityEngine.UI.Image[] betImages = new UnityEngine.UI.Image[3];
    bool[] lightFlg = new bool[3];

    public int frameCnt = 20;
    private int _frameCnt = 20;

    public int betCnt = 0;
    private int _betCnt = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        betImages[0] = GameObject.Find("BetLamp1").GetComponent<UnityEngine.UI.Image>();
        betImages[1] = GameObject.Find("BetLamp2").GetComponent<UnityEngine.UI.Image>();
        betImages[2] = GameObject.Find("BetLamp3").GetComponent<UnityEngine.UI.Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if((_frameCnt == 0) && (_betCnt < betCnt))
        {
            _betCnt++;
            betImages[_betCnt].color = Color.white;
            _frameCnt = frameCnt;
        }
    }

    private void FixedUpdate()
    {
        if (_frameCnt > 0) _frameCnt--;
    }

    public enum BetNum
    {
        _1Bet = 0,
        _2Bet = 1,
        _3Bet = 2,
    }

    /// <summary>
    /// ëSïîè¡ìî
    /// </summary>
    void LightOff()
    {
        for (int i = 0; i < betImages.Length; i++)
        {
            betImages[i].color = Color.gray;
        }
    }

    /// <summary>
    /// éwíËÇÃêîÇ‹Ç≈BetÇ∑ÇÈ
    /// </summary>
    /// <param name="betNum"></param>
    /// <param name="frame"></param>
    public void SetBet(BetNum betNum, int frame = 9)
    {
        LightOff();
        betImages[0].color = Color.white;
        if(betNum > BetNum._1Bet)
        {
            _betCnt = 0;
            _frameCnt = frame;
            betCnt = (int)betNum;
            frameCnt = frame;
        }
    }
}
