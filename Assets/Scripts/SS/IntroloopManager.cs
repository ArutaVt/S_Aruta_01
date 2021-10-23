using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using E7.Introloop;

public class IntroloopManager
{
    // 再生する
    public void Play(IntroloopAudio introloopAudio)
    {
        // Introloopならイントロ付きループで、
        // Loopならループで、
        // Non loopingならループなしで再生されます。
        IntroloopPlayer.Instance.Play(introloopAudio);
        // フェードインしながら再生もできる
        // IntroloopPlayer.Instance.Play(introloopAudio, fadetime);
    }

    // ポーズする
    public void Pause()
    {
        IntroloopPlayer.Instance.Pause();

        // フェードアウトしながらポーズもできる
        // IntroloopPlayer.Instance.Pause(fadetime);
    }

    // 再開する
    public void Resume()
    {
        IntroloopPlayer.Instance.Resume();

        // フェードインしながら再開もできる
        // IntroloopPlayer.Instance.Resume(fadetime);
    }

    // 指定した時間にシークする
    public void Seek(float elapsedTime)
    {
        IntroloopPlayer.Instance.Seek(elapsedTime);
    }

    // 停止する
    public void Stop()
    {
        IntroloopPlayer.Instance.Stop();

        // フェードアウトしながら停止もできる
        // IntroloopPlayer.Instance.Stop(fadetime);
    }
}
