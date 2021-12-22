using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RpgAtsumaruApiForUnity;

public class VolumeSetting : MonoBehaviour
{
    private void Awake()
    {
        // �����v���O�C���̏��������I����Ă��Ȃ��Ȃ�
        if (!RpgAtsumaruApi.Initialized)
        {
            // �v���O�C���̏�����
            RpgAtsumaruApi.Initialize();
            // ����API���擾���ĉ��ʃo�[�̊Ď����J�n����
            RpgAtsumaruApi.VolumeApi.StartVolumeChangeListen();
            // RPG�A�c�}�[���̉��ʒ����o�[���������ꂽ�Ƃ��Ɏ�����Unity�̃}�X�^�[���ʂ���������
            RpgAtsumaruApi.VolumeApi.EnableAutoVolumeSync = true;
        }
    }
}
