using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Sound.LoadSe("BET", "00_BET音");
        Sound.LoadSe("WAIT", "02_ウエイト音");
        Sound.LoadSe("LEVER", "03_レバオン");
        Sound.LoadSe("STOP", "04_停止音1");
        Sound.LoadBgm("BIG", "02_BIG1小役ゲーム");
        //Sound.PlayBgm("BIG", 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("up"))
        {
            Sound.PlaySe("BET", 0.3f);
        }

        if (Input.GetKeyDown("left"))
        {
            Sound.PlaySe("WAIT", 0.3f);
        }

        if (Input.GetKeyDown("down"))
        {
            Sound.PlaySe("LEVER", 0.3f);
        }

        if (Input.GetKeyDown("right"))
        {
            Sound.PlaySe("STOP", 0.3f);
        }
    }
}
