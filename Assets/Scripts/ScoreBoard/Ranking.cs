using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranking : MonoBehaviour
{
    public UnityEngine.UI.Button dispbutton;
    public UnityEngine.UI.Button dispsetteibutton;
    public UnityEngine.UI.Dropdown dropdown;
    private AtsumaruScoreboard scoreboard = new AtsumaruScoreboard();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// ランキング表示ボタンクリック時処理
    /// </summary>
    public void DispButtonClick()
    {
        switch (Mn.mnMode)
        {
            case Mn.MnMode.Normal:
                scoreboard.ShowScoreboard(1);
                break;
            case Mn.MnMode.Score:
                scoreboard.ShowScoreboard(2);
                break;
            case Mn.MnMode.Tricks:
                scoreboard.ShowScoreboard(3);
                break;
            default:
                break;
        }
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
