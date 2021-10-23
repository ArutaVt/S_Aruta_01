using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navis : MonoBehaviour
{
    public enum OrderType
    {
        Order_123,
        Order_132,
        Order_213,
        Order_231,
        Order_321,
        Order_312,
        Rare,
    };

    public enum NaviPos
    {
        Left = 0,
        Center,
        Right,
        All
    };

    public GameObject[] gameObjects;
    private NaviController[] naviControllers;
    private OrderType orderType;
    private NaviController.Type colorType;
    private int stopcnt = 0;
    const float FristSize = 100.0f;
    const float SecondSize = 75.0f;
    const float ThredSize = 50.0f;



    // Start is called before the first frame update
    void Start()
    {
        naviControllers = new NaviController[gameObjects.Length];
        naviControllers[0] = gameObjects[0].GetComponent<NaviController>();
        naviControllers[1] = gameObjects[1].GetComponent<NaviController>();
        naviControllers[2] = gameObjects[2].GetComponent<NaviController>();
    }


    /// <summary>
    /// ナビ表示
    /// </summary>
    /// <param name="naviType"></param>
    /// <param name="_colorType"></param>
    public void DispNavi(OrderType naviType, NaviController.Type _colorType)
    {
        stopcnt = 0;

        foreach (var item in naviControllers)
        {
            item.gameObject.SetActive(true);
        }

        switch (naviType)
        {
            case OrderType.Order_123:
                Sound.PlaySe("DispNavi");
                naviControllers[0].DispNavi(_colorType, 1, FristSize);
                naviControllers[1].DispNavi(_colorType, 2, SecondSize);
                naviControllers[2].DispNavi(_colorType, 3, ThredSize);
                break;
            case OrderType.Order_132:
                Sound.PlaySe("DispNavi");
                naviControllers[0].DispNavi(_colorType, 1, FristSize);
                naviControllers[1].DispNavi(_colorType, 3, ThredSize);
                naviControllers[2].DispNavi(_colorType, 2, SecondSize);
                break;
            case OrderType.Order_213:
                Sound.PlaySe("DispNavi");
                naviControllers[0].DispNavi(_colorType, 2, SecondSize);
                naviControllers[1].DispNavi(_colorType, 1, FristSize);
                naviControllers[2].DispNavi(_colorType, 3, ThredSize);
                break;
            case OrderType.Order_231:
                Sound.PlaySe("DispNavi");
                naviControllers[0].DispNavi(_colorType, 2, SecondSize);
                naviControllers[1].DispNavi(_colorType, 3, ThredSize);
                naviControllers[2].DispNavi(_colorType, 1, FristSize);
                break;
            case OrderType.Order_321:
                Sound.PlaySe("DispNavi");
                naviControllers[0].DispNavi(_colorType, 3, ThredSize);
                naviControllers[1].DispNavi(_colorType, 2, SecondSize);
                naviControllers[2].DispNavi(_colorType, 1, FristSize);
                break;
            case OrderType.Order_312:
                Sound.PlaySe("DispNavi");
                naviControllers[0].DispNavi(_colorType, 3, ThredSize);
                naviControllers[1].DispNavi(_colorType, 1, FristSize);
                naviControllers[2].DispNavi(_colorType, 2, SecondSize);
                break;
            case OrderType.Rare:
                Sound.PlaySe("Rare");
                naviControllers[0].DispNavi(_colorType, 3, 100.0f);
                naviControllers[1].DispNavi(_colorType, 1, 100.0f);
                naviControllers[2].DispNavi(_colorType, 2, 100.0f);
                break;
        }

        orderType = naviType;
        colorType = _colorType;
    }

    /// <summary>
    /// ナビの非表示
    /// </summary>
    /// <param name="pos"></param>
    public void NoDispNavi(NaviPos pos)
    {
        if (pos == NaviPos.All)
        {
            foreach (var item in naviControllers)
            {
                item.gameObject.SetActive(false);
            }
            return;
        }

        if(CheckDispNavi(pos) == true)
        {
            naviControllers[(int)pos].gameObject.SetActive(false);
        }
        UpDateNaviSize();
    }

    // ナビが表示されているか確認する
    public bool CheckDispNavi(NaviPos pos)
    {
        return naviControllers[(int)pos].gameObject.activeSelf;
    }
    
    // ナビの大きさを更新する
    public void UpDateNaviSize()
    {
        stopcnt++;
        switch (stopcnt)
        {
            case 1:
                switch (orderType)
                {
                    case OrderType.Order_123:
                        naviControllers[1].DispNavi(colorType, 2, FristSize);
                        naviControllers[2].DispNavi(colorType, 3, SecondSize);
                        break;
                    case OrderType.Order_132:
                        naviControllers[1].DispNavi(colorType, 3, SecondSize);
                        naviControllers[2].DispNavi(colorType, 2, FristSize);
                        break;
                    case OrderType.Order_213:
                        naviControllers[0].DispNavi(colorType, 2, FristSize);
                        naviControllers[2].DispNavi(colorType, 3, SecondSize);
                        break;
                    case OrderType.Order_231:
                        naviControllers[0].DispNavi(colorType, 2, FristSize);
                        naviControllers[1].DispNavi(colorType, 3, SecondSize);
                        break;
                    case OrderType.Order_321:
                        naviControllers[0].DispNavi(colorType, 3, SecondSize);
                        naviControllers[1].DispNavi(colorType, 2, FristSize);
                        break;
                    case OrderType.Order_312:
                        naviControllers[0].DispNavi(colorType, 3, SecondSize);
                        naviControllers[2].DispNavi(colorType, 2, FristSize);
                        break;
                }
                break;
            case 2:
                switch (orderType)
                {
                    case OrderType.Order_123:
                        naviControllers[2].DispNavi(colorType, 3, FristSize);
                        break;
                    case OrderType.Order_132:
                        naviControllers[1].DispNavi(colorType, 3, FristSize);
                        break;
                    case OrderType.Order_213:
                        naviControllers[2].DispNavi(colorType, 3, FristSize);
                        break;
                    case OrderType.Order_231:
                        naviControllers[1].DispNavi(colorType, 3, FristSize);
                        break;
                    case OrderType.Order_321:
                        naviControllers[0].DispNavi(colorType, 3, FristSize);
                        break;
                    case OrderType.Order_312:
                        naviControllers[0].DispNavi(colorType, 3, FristSize);
                        break;
                }
                break;
        }
    }
}
