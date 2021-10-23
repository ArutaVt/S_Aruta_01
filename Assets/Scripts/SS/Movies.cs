using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Movies : MonoBehaviour
{
    public static VideoPlayer videoPlayer;
    public VideoClip[] videoClips;
    private Movie movie = Movie.Max;

    public enum Movie
    {
        LongFreeze,
        Nml,
        GoldShutter,
        BonusWait,
        Bonus_1,
        RushStart_1,
        RushBackground,
        BonusEnd,
        RushStart_2,
        Max,
    }


    private void Start()
    {
        GameObject gameObject1 = GameObject.Find("Monitor");
        videoPlayer = gameObject1.GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += VideoPlayer_loopPointReached;
    }

    private void VideoPlayer_loopPointReached(VideoPlayer source)
    {
        switch (movie)
        {
            case Movie.LongFreeze:
                break;
            case Movie.Nml:
                break;
            case Movie.GoldShutter:
                break;
            case Movie.BonusWait:
                videoPlayer.time = 11.18f;
                break;
            case Movie.Bonus_1:
                break;
            case Movie.RushStart_1:
                break;
            case Movie.RushBackground:
                break;
            case Movie.Max:
                break;
            default:
                break;
        }
    }

    public void SetMovie(Movie _movie, bool loopflg)
    {
        movie = _movie;
        videoPlayer.clip = videoClips[(int)_movie];
        videoPlayer.isLooping = loopflg;
    }
}
