using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranking : MonoBehaviour
{
    public UnityEngine.UI.Button dispbutton;
    public UnityEngine.UI.Button dispsetteibutton;
    public UnityEngine.UI.Dropdown dropdown;
    // private AtsumaruScoreboard scoreboard = new AtsumaruScoreboard();

    // 配信開始時間
    public static DateTime startTime = new DateTime(2022, 6, 11, 12, 0, 0);

    // 配信終了時間
    public static DateTime endTime = new DateTime(2022, 6, 11, 12, 50, 0);

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 配信開始時間から配信終了時間までドロップダウンに配信ランキングボタンが出現
        if (dropdown.options.Find(x => x.text == "配信ランキング") == null)
        {
            // 配信開始時間から配信終了時間までドロップダウンに配信ランキングボタンが出現
            if (System.DateTime.Now.CompareTo(startTime) == 1)
            {
                if (System.DateTime.Now.CompareTo(endTime) == -1)
                {
                    dropdown.options.Add(new UnityEngine.UI.Dropdown.OptionData { text = "配信ランキング" });
                    dropdown.RefreshShownValue();
                }
            }
        }
        else
        {
            if (System.DateTime.Now.CompareTo(endTime) == 1)
            {
                dropdown.options.Remove(dropdown.options.Find(x => x.text == "配信ランキング"));
                dropdown.RefreshShownValue();
            }
        }
    }

    /// <summary>
    /// ランキング表示ボタンクリック時処理
    /// </summary>
    public void DispButtonClick()
    {
        //switch (Mn.mnMode)
        //{
        //    case Mn.MnMode.Normal:
        //        scoreboard.ShowScoreboard(1);
        //        break;
        //    case Mn.MnMode.Score:
        //        scoreboard.ShowScoreboard(2);
        //        break;
        //    case Mn.MnMode.Tricks:
        //        scoreboard.ShowScoreboard(3);
        //        break;
        //    default:
        //        break;
        //}
    }

    public void DispSetteiButtonClick()
    {
        dispsetteibutton.interactable = false;
        SubMain.dispSetteiFlg = true;
    }

    /// <summary>
    /// モード変更
    /// </summary>
    public void MoveMode()
    {
        Mn.mnMode = (Mn.MnMode)dropdown.value;
    }

    public void ModeButtonOff()
    {
        dropdown.interactable = false;
        if(Mn.mnMode == Mn.MnMode.Score)
        {
            dispsetteibutton.interactable = false;
        }
    }
}
