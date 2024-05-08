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
    public Button countDownBut, backShareBut, tenCountDownBut, fifteenCountDownBut, reGetImageBut, shareQRCodeBut, photoBut,recordButton;
    public FileUploader fileUpload;
    
    public float longPressThreshold = 0.5f;
    [SerializeField] CaptureBase _movieCapture = null;
    public GameObject shareGroupGo, countDownTimeGo, backAndShareGo;
    public TextMeshProUGUI countDownTime10, countDownTime15;
    public VideoPlayer videoPlayer;
    public RawImage videoImage, photoImage;
    private string photoFilePath, videoFilePath;
    private bool isRecording = false;
    public GameObject defultImage, recodingImage;
    public int countDownTIme = 60, tempTime;
    public TextMeshProUGUI countDownText;
    private void Start()
    {
        //click count down time button ,start count down time ,and then short down screen
        countDownBut.onClick.AddListener(() =>
        {
            ResetRercodingEffect();
            ShowShareTip(false);
            backAndShareGo.SetActive(false);
            StopAllCoroutines();
            countDownTimeGo.SetActive(true);
            backAndShareGo.SetActive(false);
            countDownTime10.text = "10秒拍照";
            countDownTime15.text = "15秒拍照";
            tenCountDownBut.gameObject.SetActive(true);
            fifteenCountDownBut.gameObject.SetActive(true);
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
            fifteenCountDownBut.gameObject.SetActive(false);
        });
        fifteenCountDownBut.onClick.AddListener(() =>
        {
            StopAllCoroutines();
            ShowShareTip(false);
            StartCoroutine(CountDownTime(15, countDownTime15));
            tenCountDownBut.gameObject.SetActive(false);
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

        recordButton.onClick.AddListener(() =>
        {
            countDownText.text = countDownTIme.ToString();
            countDownTimeGo.SetActive(false);
            tempTime = countDownTIme;
            isRecording = !isRecording;
            defultImage.SetActive(!isRecording);
            recodingImage.SetActive(isRecording);
            countDownText.gameObject.SetActive(isRecording);
            StartCoroutine(CountDownTimeMethend(countDownTIme));
            if (!isRecording)
            {
                StopAllCoroutines();
                StopCapture();
            }
            else{

                StartCapture();
            }

        });
    }

    void ResetRercodingEffect(){

        defultImage.SetActive(true);
        recodingImage.SetActive(false);
        countDownText.gameObject.SetActive(false);
        isRecording = false;
    }
IEnumerator CountDownTimeMethend(int countDownTime)
    {

        while (tempTime > 1)
        {
            tempTime--;
            countDownText.text = tempTime.ToString();
            yield return new WaitForSeconds(1);

        }
        StopCapture();
        ResetRercodingEffect();
    }
    IEnumerator CountDownTime(int sconed, TextMeshProUGUI coutDownText)
    {

        for (int i = sconed; i > 0; i--)
        {
            coutDownText.text = i+"秒拍照";
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
        //Debug.Log("show image");
        StartCoroutine(PlayVideo(_movieCapture.LastFilePath));
    }

    void ClickAction()
    {
        ResetRercodingEffect();
        _movieCapture.StopCapture();
        photoImage.gameObject.SetActive(false);
        Debug.Log("Start take photo!");
        _movieCapture.OutputTarget = OutputTarget.ImageSequence;
        _movieCapture.ResolutionDownScale = CaptureBase.DownScale.Original;
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
            countDownTimeGo.SetActive(false);
            
            StartCoroutine(LoadImageFromLocalFile(_movieCapture.LastFilePath));
            fileUpload.UpdateLoad(_movieCapture.LastFilePath);

            //StartCoroutine(LoadImageFromWeb(_movieCapture.LastFilePath));
            yield break;
        }
        else
        {
            Debug.LogWarning("_movieCapture has not started capturing.");
        }

    }
    /// <summary>
    /// start video Capture
    /// </summary>
    private void StartCapture()
    {
        Debug.Log("Start capture");
        videoImage.gameObject.SetActive(false);
        backAndShareGo.SetActive(false);
        _movieCapture.OutputTarget = OutputTarget.VideoFile;
        _movieCapture.ResolutionDownScale = CaptureBase.DownScale.Half;
        _movieCapture.StartCapture();
    }
    void ShowShareTip(bool mValue)
    {
        shareGroupGo.SetActive(mValue);
        countDownTime10.text = 10.ToString();
        countDownTime15.text = 15.ToString();
    }
    /// <summary>
    /// load local image 
    /// </summary>
    /// <param name="filePath">image path</param>
    /// <returns>unity image</returns> 
    IEnumerator LoadImageFromLocalFile(string filePath)
    {
        videoImage.gameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        if (File.Exists(filePath))
        {
            byte[] fileData = File.ReadAllBytes(filePath);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(fileData);  // 创建纹理
            photoImage.texture = texture;  // 将纹理设置到RawImage组件上
            //photoImage.SetNativeSize();    // 可选: 调整图片大小以匹配其原始尺寸
            photoImage.gameObject.SetActive(true);
            videoImage.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("File not found at: " + filePath);
        }
    }
    IEnumerator PlayVideo(string path)
    {
        photoImage.gameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        
        videoImage.gameObject.SetActive(true);
        videoPlayer.url = path;
        videoPlayer.prepareCompleted += Prepared;
        videoPlayer.Prepare();
        //Debug.Log("Video playing  isPrepared");
        while (!videoPlayer.isPrepared)
        {
            yield return null;
        }
        videoImage.texture = videoPlayer.texture;
        videoPlayer.isLooping = true;
        videoPlayer.Play();
        //Debug.Log("Video playing");
    }

    void Prepared(VideoPlayer vp)
    {
        videoImage.texture = vp.texture;
    }
}
public enum RecType
{
    None,
    Image,
    Video
}