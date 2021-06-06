using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// �T�E���h�Ǘ�
public class Sound
{

    /// SE�`�����l����
    const int SE_CHANNEL = 4;

    /// �T�E���h���
    enum eType
    {
        Bgm, // BGM
        Se,  // SE
    }

    // �V���O���g��
    static Sound _singleton = null;
    // �C���X�^���X�擾
    public static Sound GetInstance()
    {
        return _singleton ?? (_singleton = new Sound());
    }

    // �T�E���h�Đ��̂��߂̃Q�[���I�u�W�F�N�g
    GameObject _object = null;
    // �T�E���h���\�[�X
    AudioSource _sourceBgm = null; // BGM
    AudioSource _sourceSeDefault = null; // SE (�f�t�H���g)
    AudioSource[] _sourceSeArray; // SE (�`�����l��)
                                  // BGM�ɃA�N�Z�X���邽�߂̃e�[�u��
    Dictionary<string, _Data> _poolBgm = new Dictionary<string, _Data>();
    // SE�ɃA�N�Z�X���邽�߂̃e�[�u�� 
    Dictionary<string, _Data> _poolSe = new Dictionary<string, _Data>();

    /// �ێ�����f�[�^
    class _Data
    {
        /// �A�N�Z�X�p�̃L�[
        public string Key;
        /// ���\�[�X��
        public string ResName;
        /// AudioClip
        public AudioClip Clip;

        /// �R���X�g���N�^
        public _Data(string key, string res)
        {
            Key = key;
            ResName = "Sounds/" + res;
            // AudioClip�̎擾
            Clip = Resources.Load(ResName) as AudioClip;
        }
    }

    /// �R���X�g���N�^
    public Sound()
    {
        // �`�����l���m��
        _sourceSeArray = new AudioSource[SE_CHANNEL];
    }

    /// AudioSource���擾����
    AudioSource _GetAudioSource(eType type, int channel = -1)
    {
        if (_object == null)
        {
            // GameObject���Ȃ���΍��
            _object = new GameObject("Sound");
            // �j�����Ȃ��悤�ɂ���
            GameObject.DontDestroyOnLoad(_object);
            // AudioSource���쐬
            _sourceBgm = _object.AddComponent<AudioSource>();
            _sourceSeDefault = _object.AddComponent<AudioSource>();
            for (int i = 0; i < SE_CHANNEL; i++)
            {
                _sourceSeArray[i] = _object.AddComponent<AudioSource>();
            }
        }

        if (type == eType.Bgm)
        {
            // BGM
            return _sourceBgm;
        }
        else
        {
            // SE
            if (0 <= channel && channel < SE_CHANNEL)
            {
                // �`�����l���w��
                return _sourceSeArray[channel];
            }
            else
            {
                // �f�t�H���g
                return _sourceSeDefault;
            }
        }
    }

    // �T�E���h�̃��[�h
    // ��Resources/Sounds�t�H���_�ɔz�u���邱��
    public static void LoadBgm(string key, string resName)
    {
        GetInstance()._LoadBgm(key, resName);
    }
    public static void LoadSe(string key, string resName)
    {
        GetInstance()._LoadSe(key, resName);
    }
    void _LoadBgm(string key, string resName)
    {
        if (_poolBgm.ContainsKey(key))
        {
            // ���łɓo�^�ς݂Ȃ̂ł����������
            _poolBgm.Remove(key);
        }
        _poolBgm.Add(key, new _Data(key, resName));
    }
    void _LoadSe(string key, string resName)
    {
        if (_poolSe.ContainsKey(key))
        {
            // ���łɓo�^�ς݂Ȃ̂ł����������
            _poolSe.Remove(key);
        }
        _poolSe.Add(key, new _Data(key, resName));
    }

    /// BGM�̍Đ�
    /// �����O��LoadBgm�Ń��[�h���Ă�������
    public static bool PlayBgm(string key, float volume)
    {
        return GetInstance()._PlayBgm(key,  volume);
    }
    bool _PlayBgm(string key, float volume)
    {
        if (_poolBgm.ContainsKey(key) == false)
        {
            // �Ή�����L�[���Ȃ�
            return false;
        }

        // ��������~�߂�
        _StopBgm();

        // ���\�[�X�̎擾
        var _data = _poolBgm[key];

        // �Đ�
        var source = _GetAudioSource(eType.Bgm);
        source.loop = true;
        source.clip = _data.Clip;
        source.volume = volume;
        source.Play();

        return true;
    }
    /// BGM�̒�~
    public static bool StopBgm()
    {
        return GetInstance()._StopBgm();
    }
    bool _StopBgm()
    {
        _GetAudioSource(eType.Bgm).Stop();

        return true;
    }

    /// SE�̍Đ�
    /// �����O��LoadSe�Ń��[�h���Ă�������
    public static bool PlaySe(string key, float volume, int channel = -1)
    {
        return GetInstance()._PlaySe(key, volume, channel);
    }
    bool _PlaySe(string key, float volume, int channel = -1)
    {
        if (_poolSe.ContainsKey(key) == false)
        {
            // �Ή�����L�[���Ȃ�
            return false;
        }

        // ���\�[�X�̎擾
        var _data = _poolSe[key];

        if (0 <= channel && channel < SE_CHANNEL)
        {
            // �`�����l���w��
            var source = _GetAudioSource(eType.Se, channel);
            source.clip = _data.Clip;
            source.volume = volume;
            source.Play();
        }
        else
        {
            // �f�t�H���g�ōĐ�
            var source = _GetAudioSource(eType.Se);
            source.PlayOneShot(_data.Clip, volume);
        }

        return true;
    }
}