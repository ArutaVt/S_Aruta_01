using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using E7.Introloop;

public class IntroloopManager
{
    // �Đ�����
    public void Play(IntroloopAudio introloopAudio)
    {
        // Introloop�Ȃ�C���g���t�����[�v�ŁA
        // Loop�Ȃ烋�[�v�ŁA
        // Non looping�Ȃ烋�[�v�Ȃ��ōĐ�����܂��B
        IntroloopPlayer.Instance.Play(introloopAudio);
        // �t�F�[�h�C�����Ȃ���Đ����ł���
        // IntroloopPlayer.Instance.Play(introloopAudio, fadetime);
    }

    // �|�[�Y����
    public void Pause()
    {
        IntroloopPlayer.Instance.Pause();

        // �t�F�[�h�A�E�g���Ȃ���|�[�Y���ł���
        // IntroloopPlayer.Instance.Pause(fadetime);
    }

    // �ĊJ����
    public void Resume()
    {
        IntroloopPlayer.Instance.Resume();

        // �t�F�[�h�C�����Ȃ���ĊJ���ł���
        // IntroloopPlayer.Instance.Resume(fadetime);
    }

    // �w�肵�����ԂɃV�[�N����
    public void Seek(float elapsedTime)
    {
        IntroloopPlayer.Instance.Seek(elapsedTime);
    }

    // ��~����
    public void Stop()
    {
        IntroloopPlayer.Instance.Stop();

        // �t�F�[�h�A�E�g���Ȃ����~���ł���
        // IntroloopPlayer.Instance.Stop(fadetime);
    }
}
