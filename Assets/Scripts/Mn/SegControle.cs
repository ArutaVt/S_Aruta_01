using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SegControle : MonoBehaviour
{
    private Text _text;
    private int _reqnum;
    private int _num;
    private int _waitCnt;
    private string fmt;
    public int waitCnt;
    private bool _animation;

    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<Text>();
        _text.text = "00";
        _waitCnt = 1;
        waitCnt = 1;
        _animation = false;
    }

    void FixedUpdate()
    {
        if(_reqnum != _num)
        {
            _waitCnt--;
            if(_waitCnt == 0)
            {
                _waitCnt = waitCnt;
                if (_reqnum > _num)
                {
                    _num++;
                    _text.text = _num.ToString(fmt);
                }
                else if (_reqnum < _num)
                {
                    _num--;
                    _text.text = _num.ToString(fmt);
                }
            }
        }
        else
        {
            _animation = false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <param name="num"></param>
    public void setNum(int value, string str = "D2", bool animeFlg = true)
    {
        _animation = true;
        _reqnum = value;
        fmt = str;
        _text.text = _num.ToString(fmt);

        if(animeFlg == false)
        {
            _num = _reqnum;
            fmt = str;
            _animation = false;
            _text.text = value.ToString(fmt);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public int getNum()
    {
        return _num;
    }

    public void setwaitCnt(int value)
    {
        waitCnt = value;
    }

    /// <summary>
    /// êßå‰íÜÇ©ï‘Ç∑
    /// </summary>
    /// <returns></returns>
    public bool getAnime()
    {
        return _animation;
    }
}
