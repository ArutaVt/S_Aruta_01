using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NaviController : MonoBehaviour
{
    public enum Type
    {
        Yellow,
        Blue,
        Rare,
    };
    
    private int number;
    private Type type;

    private GameObject backgroundA;
    private Vector3 backgroundA_Rotation;
    private GameObject backgroundB;
    private GameObject numberImage;
    private Sprite[] bellNumberSprite;
    private Sprite[] replayNumberSprite;
    private Sprite rareSprite;
    private float rotation_z = 0;

    // Start is called before the first frame update
    void Start()
    {
        backgroundA = transform.Find("Background_A").gameObject;
        backgroundB = transform.Find("Background_B").gameObject;
        numberImage = transform.Find("Number").gameObject;

        bellNumberSprite = new Sprite[]
        {
            Resources.Load<Sprite>("Navi/bell_order_1"),
            Resources.Load<Sprite>("Navi/bell_order_2"),
            Resources.Load<Sprite>("Navi/bell_order_3"),
        };

        replayNumberSprite = new Sprite[]
        {
            Resources.Load<Sprite>("Navi/replay_order_1"),
        };

        rareSprite = Resources.Load<Sprite>("Navi/Middle_1");
    }

    // Update is called once per frame
    void Update()
    {
        rotation_z = 0.2f;

        backgroundA.transform.Rotate(0, 0, rotation_z);
        backgroundB.transform.Rotate(0, 0, -rotation_z);
    }

    // ナビの画像を切替える処理
    public void DispNavi(Type disptype, int dispnumber, float size)
    {
        Sprite sprite;

        switch (disptype)
        {
            case Type.Yellow:
                backgroundA.SetActive(true);
                backgroundB.SetActive(true);
                sprite = bellNumberSprite[dispnumber - 1];
                break;
            case Type.Blue:
                backgroundA.SetActive(true);
                backgroundB.SetActive(true);
                sprite = replayNumberSprite[0];
                break;
            case Type.Rare:
                backgroundA.SetActive(false);
                backgroundB.SetActive(false);
                sprite = rareSprite;
                break;
            default:
                sprite = rareSprite;
                break;
        }

        numberImage.GetComponent<SpriteRenderer>().sprite = sprite;

        ChangeNaviSize(size);   // サイズ変更
    }

    // ナビサイズ変更
    public void ChangeNaviSize(float size)
    {
        backgroundA.GetComponent<Transform>().localScale = new Vector2(size, size);
        backgroundB.GetComponent<Transform>().localScale = new Vector2(size, size);
        numberImage.GetComponent<Transform>().localScale = new Vector2(size, size);
    }
}
