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

    // �z�M�J�n����
    public static DateTime startTime = new DateTime(2022, 6, 11, 12, 0, 0);

    // �z�M�I������
    public static DateTime endTime = new DateTime(2022, 6, 11, 12, 50, 0);

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // �z�M�J�n���Ԃ���z�M�I�����Ԃ܂Ńh���b�v�_�E���ɔz�M�����L���O�{�^�����o��
        if (dropdown.options.Find(x => x.text == "�z�M�����L���O") == null)
        {
            // �z�M�J�n���Ԃ���z�M�I�����Ԃ܂Ńh���b�v�_�E���ɔz�M�����L���O�{�^�����o��
            if (System.DateTime.Now.CompareTo(startTime) == 1)
            {
                if (System.DateTime.Now.CompareTo(endTime) == -1)
                {
                    dropdown.options.Add(new UnityEngine.UI.Dropdown.OptionData { text = "�z�M�����L���O" });
                    dropdown.RefreshShownValue();
                }
            }
        }
        else
        {
            if (System.DateTime.Now.CompareTo(endTime) == 1)
            {
                dropdown.options.Remove(dropdown.options.Find(x => x.text == "�z�M�����L���O"));
                dropdown.RefreshShownValue();
            }
        }
    }

    /// <summary>
    /// �����L���O�\���{�^���N���b�N������
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
    /// ���[�h�ύX
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
