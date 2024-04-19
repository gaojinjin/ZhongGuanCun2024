using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Video;

public class TestmageAndVideo : MonoBehaviour
{
    public RawImage rawImage; // 在Inspector中设置这个变量
    public VideoPlayer videoPlayer;
    public Button videoBut, imageBut;
    public string videoPath, imagePath;
    void Start()
    {
        videoBut.onClick.AddListener(() => {
            StartCoroutine(PlayVideo(videoPath));
        });
        imageBut.onClick.AddListener(() => {
            StartCoroutine(LoadImageFromWeb(imagePath));
        });
    }

    IEnumerator LoadImageFromWeb(string url)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
            Debug.LogError(request.error);
        else
            rawImage.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
    }

    IEnumerator PlayVideo(string path)
    {
        videoPlayer.url = path;
        videoPlayer.prepareCompleted += Prepared;
        videoPlayer.Prepare();

        while (!videoPlayer.isPrepared)
        {
            yield return null;
        }
        rawImage.texture = videoPlayer.texture;
        videoPlayer.Play();
    }

    void Prepared(VideoPlayer vp)
    {
        rawImage.texture = vp.texture;
    }
}
