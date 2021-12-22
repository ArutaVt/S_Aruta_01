using System.Threading.Tasks;
using RpgAtsumaruApiForUnity;
using UnityEngine;

public class AtsumaruScoreboard : MonoBehaviour
{
    private void Awake()
    {
        // �����v���O�C���̏��������I����Ă��Ȃ��Ȃ�
        if (!RpgAtsumaruApi.Initialized)
        {
            // �v���O�C���̏�����
            RpgAtsumaruApi.Initialize();
        }
    }

    // �w�肳�ꂽ�{�[�hID�ɃX�R�A�f�[�^�𑗐M���܂�
    // ���M�ł���X�R�A�{�[�h�̐��́ARPG�A�c�}�[����API�Ǘ���ʂɂĒ������鎖���o���܂��B
    // ����̐���10�܂łƂȂ��Ă��܂��B
    public async void SendScore(int boardId, long score, bool dispFlg = false)
    {
        // RPG�A�c�}�[���ɃX�R�A�𑗐M����
        await RpgAtsumaruApi.ScoreboardApi.SendScoreAsync(boardId, score);

        if(dispFlg == true)
        {
            // �񓯊��̕\���Ăяo��������i�\�����ꂽ���ǂ����̑ҋ@�ł͂Ȃ��A�����̌��ʑҋ@�ł��邱�Ƃɒ��ӂ��ĉ������j
            await RpgAtsumaruApi.ScoreboardApi.ShowScoreboardAsync(boardId);
        }
    }

    // �w�肳�ꂽ�X�R�A�{�[�hID�̃X�R�A�{�[�h��RPG�A�c�}�[����ɕ\�����܂�
    public async void ShowScoreboard(int boardId)
    {
        // �񓯊��̕\���Ăяo��������i�\�����ꂽ���ǂ����̑ҋ@�ł͂Ȃ��A�����̌��ʑҋ@�ł��邱�Ƃɒ��ӂ��ĉ������j
        await RpgAtsumaruApi.ScoreboardApi.ShowScoreboardAsync(boardId);
    }


    // RPG�A�c�}�[���̃X�R�A�T�[�o�[����X�R�A�{�[�h�̃f�[�^���擾���܂�
    public async Task<RpgAtsumaruScoreboardData> GetScoreboardData(int boardId)
    {
        // �񓯊��̎擾�Ăяo��������i�^�v���^�ŕԂ���邽��3�ڂ̌��ʂ������󂯎��ꍇ�͈ȉ��̒ʂ�Ɏ�������Ɨǂ��ł��傤�j
        var (_, _, scoreboardData) = await RpgAtsumaruApi.ScoreboardApi.GetScoreboardAsync(boardId);
        return scoreboardData;
    }
}