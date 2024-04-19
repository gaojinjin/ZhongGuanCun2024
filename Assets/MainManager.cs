using RenderHeads.Media.AVProMovieCapture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.IO;
using TMPro;
using UnityEngine.Video;
using UnityEngine.Networking;

public class MainManager : MonoBehaviour
{
    public Button countDownBut, backShareBut, tenCountDownBut, fifteenCountDownBut, reGetImageBut, shareQRCodeBut, photoBut;
    public FileUploader fileUpload;
    private bool hasBeenLongPressed = false; 
    public bool HasBeenLongPressed { 
        set { hasBeenLongPressed = value;
            if (hasBeenLongPressed)
            {
                StartCapture();
            }
            else
            {
                StopCapture();
            }
        }
    }
    public float longPressThreshold = 0.5f; 
    [SerializeField] CaptureBase _movieCapture = null;
    public GameObject shareGroupGo, countDownTimeGo, backAndShareGo;
    public TextMeshProUGUI countDownTime10, countDownTime15;
    public VideoPlayer videoPlayer;
    public RawImage rawImage;
    private string photoFilePath, videoFilePath;
    private void Start()
    {
        //click count down time button ,start count down time ,and then short down screen
        countDownBut.onClick.AddListener(() =>
        {
            countDownTimeGo.SetActive(true);
            backAndShareGo.SetActive(false);
        });
        backShareBut.onClick.AddListener(() =>
        {
            ShowShareTip(false);
        });

        tenCountDownBut.onClick.AddListener(() =>
        {
            StopAllCoroutines();
            ShowShareTip(false);
            StartCoroutine(CountDownTime(10, countDownTime10));
            //countDownTimeGo.SetActive(false);
        });
        fifteenCountDownBut.onClick.AddListener(() =>
        {
            StopAllCoroutines();
            ShowShareTip(false);
            StartCoroutine(CountDownTime(15, countDownTime15));
            //countDownTimeGo.SetActive(false);
        });
        reGetImageBut.onClick.AddListener(() =>
        {
            backAndShareGo.SetActive(false);


        });
        //show share panel
        shareQRCodeBut.onClick.AddListener(() =>
        {
            ShowShareTip(true);
            backAndShareGo.SetActive(false);
        });
        //take photo button event
        photoBut.onClick.AddListener(() =>
        {
            backAndShareGo.SetActive(false);
            StopAllCoroutines();

            ShowShareTip(false);
            ClickAction();
        });
    }

    IEnumerator CountDownTime(int sconed,TextMeshProUGUI coutDownText)
    {

        for (int i = sconed; i > 0; i--)
        {
            coutDownText.text = i.ToString();
            yield return new WaitForSeconds(1);

        }
        ClickAction();
    }

    /// <summary>
    /// stop capture 
    /// </summary>
    private void StopCapture()
    {

        _movieCapture.StopCapture();
        Debug.Log("StopCapture");
        //ShowShareTip(true);
        Debug.Log("FilePath   " + _movieCapture.LastFilePath);
        //upload file to server
        fileUpload.UpdateLoad(_movieCapture.LastFilePath);
        backAndShareGo.SetActive(true);
        countDownTimeGo.SetActive(false);
        StartCoroutine(PlayVideo(_movieCapture.LastFilePath));
    }

    void ClickAction()
    {
        // 
        Debug.Log("Start take photo!");
        _movieCapture.OutputTarget = OutputTarget.ImageSequence;
        _movieCapture.ResolutionDownScale = CaptureBase.DownScale.Half;
        _movieCapture.StartCapture();
        StartCoroutine(WaitOneFrame());

    }

    IEnumerator WaitOneFrame()
    {
        if (_movieCapture.IsCapturing())
        {
            while (_movieCapture.CaptureStats.NumEncodedFrames < 1)
            {
                yield return null;
            }
            Debug.Log("Take photo end");
            _movieCapture.StopCapture();
            backAndShareGo.SetActive(true);
            countDownTimeGo.SetActive(true);
            fileUpload.UpdateLoad(_movieCapture.LastFilePath);
            StartCoroutine(LoadImageFromWeb(_movieCapture.LastFilePath));
            yield break; 
        }
        else
        {
            Debug.LogWarning("_movieCapture has not started capturing.");
        }

    }
    /// <summary>
    /// start Capture
    /// </summary>
    private void StartCapture()
    {
        Debug.Log("Start capture");
        backAndShareGo.SetActive(false);
        _movieCapture.OutputTarget = OutputTarget.VideoFile;
        _movieCapture.ResolutionDownScale = CaptureBase.DownScale.Original;
        _movieCapture.StartCapture();
    }
    void ShowShareTip(bool mValue)
    {
        shareGroupGo.SetActive(mValue);
        countDownTime10.text = 10.ToString();
        countDownTime15.text = 15.ToString();
    }
    /// <summary>
    /// show photo image on rawimage
    /// </summary>
    /// <param name="url">image url</param>
    /// <returns>nothing</returns>
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
        yield return new WaitForSeconds(1);
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
public enum RecType
{
    None,
    Image,
    Video
}