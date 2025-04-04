using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VidPlayerWebGl : MonoBehaviour
{
    [SerializeField] string videoFileName;
    // Start is called before the first frame update
    void Start()
    {
        PlayVideo();
    }


    public void PlayVideo()
    {
        VideoPlayer vp = GetComponent<VideoPlayer>();

        if (vp)
        {
            string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, videoFileName);
            Debug.Log(videoPath);
            vp.url = videoPath;
            vp.Play();
        }
    }
}
