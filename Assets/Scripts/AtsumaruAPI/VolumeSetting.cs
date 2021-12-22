using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RpgAtsumaruApiForUnity;

public class VolumeSetting : MonoBehaviour
{
    private void Awake()
    {
        // もしプラグインの初期化が終わっていないなら
        if (!RpgAtsumaruApi.Initialized)
        {
            // プラグインの初期化
            RpgAtsumaruApi.Initialize();
            // 音量APIを取得して音量バーの監視を開始する
            RpgAtsumaruApi.VolumeApi.StartVolumeChangeListen();
            // RPGアツマールの音量調整バーが調整されたときに自動でUnityのマスター音量も調整する
            RpgAtsumaruApi.VolumeApi.EnableAutoVolumeSync = true;
        }
    }
}
