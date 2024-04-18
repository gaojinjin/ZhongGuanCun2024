using RenderHeads.Media.AVProMovieCapture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.IO;
using TMPro;

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
    public TextMeshProUGUI countDownTime;

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
            StartCoroutine(CountDownTime(10));
            countDownTimeGo.SetActive(false);
        });
        fifteenCountDownBut.onClick.AddListener(() =>
        {
            StopAllCoroutines();
            ShowShareTip(false);
            StartCoroutine(CountDownTime(15));
            countDownTimeGo.SetActive(false);
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

    IEnumerator CountDownTime(int sconed)
    {

        for (int i = sconed; i > 0; i--)
        {
            countDownTime.text = i.ToString();
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
            fileUpload.UpdateLoad(_movieCapture.LastFilePath);
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
        countDownTime.text = 10.ToString();
    }
}
public enum RecType
{
    None,
    Image,
    Video
}