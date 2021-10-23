using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutIn : MonoBehaviour
{
    public Sprite[] sprites;
    private SpriteRenderer spriteRenderer;
    private GameObject cutinObject;

    public enum Type
    {
        Bar,
        Red,
        White,
        Homura,
    }

    // Start is called before the first frame update
    void Start()
    {
        cutinObject = GameObject.Find("CutIn");
        spriteRenderer = cutinObject.GetComponent<SpriteRenderer>();
    }


    public void DispCutIn(Type type)
    {
        spriteRenderer.sprite = sprites[(int)type];
        cutinObject.SetActive(true);

        switch (type)
        {
            case Type.Bar:
            case Type.Red:
            case Type.White:
                Sound.PlaySe("DispBonus");
                break;
            case Type.Homura:
                Sound.PlaySe("AimSign");
                break;
            default:
                break;
        }
    }

    public void NoDispCutIn()
    {
        cutinObject.SetActive(false);
    }
}
