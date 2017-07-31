using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class playGif : MonoBehaviour {

    private RawImage image;
    public VideoClip videoToPlay;
    private VideoPlayer videoPlayer;

	void Start () {
		image = this.GetComponent<RawImage>();
		StartCoroutine(Prepare());
	}

	IEnumerator Prepare()
	{
		videoPlayer = this.gameObject.AddComponent<VideoPlayer>();
		videoPlayer.source = VideoSource.VideoClip;
		videoPlayer.playOnAwake = true;
		videoPlayer.isLooping = true;
        videoPlayer.clip = videoToPlay;
        videoPlayer.Prepare();
        while (!videoPlayer.isPrepared)
        {
            yield return null;
        }
		image.texture = videoPlayer.texture;
        videoPlayer.Play();
	}
}