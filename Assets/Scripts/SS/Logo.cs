using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logo : MonoBehaviour
{
    public enum Status
    {
        ON,
        OFF,
        FLASH,   // �t���b�V�����䒆
    }
    public Status status = Status.ON;
    public UnityEngine.UI.Image image;

    // �t���b�V������p
    public int flashCount = 4;          // �_�ŉ�
    public int flashChangeFream = 10;   // �w��̃t���[���o�߂�ON/OFF�؂�ւ�
    public int flashFream = 0;          // ���䒆�̃g�[�^���t���[����

    // �t���b�V���J���[
    private Color flasfColor = Color.white;

    // �_���J���[
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
        // �t���b�V����Ԉڍs���̏����ݒ�
        flasfColor = color;
        status = Status.FLASH;
        flashFream = 0;
    }

    public void setColor(Color color)
    {
        nmlColor = color;
    }

    /// <summary>
    /// �t���b�V�����䃁�C��
    /// </summary>
    private void flashMain()
    {
        flashFream++;
        if(flashFream % flashChangeFream == 0)
        {
            if(image.color == flasfColor)
            {
                // ����
                image.color = Color.gray;
            }
            else
            {
                // �_��
                image.color = flasfColor;
            }
        }

        // �_������ɖ߂�
        if (flashChangeFream * flashCount == flashFream) status = Status.ON;
    }
}
