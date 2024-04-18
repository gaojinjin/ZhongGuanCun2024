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
    private bool hasBeenLongPressed = false; // �����жϳ����Ƿ��Ѿ���ʶ���ִ��
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
    public float longPressThreshold = 0.5f; // ������ʱ����ֵ�����Ը�����Ҫ����
    [SerializeField] CaptureBase _movieCapture = null;
    public GameObject shareGroupGo, countDownTimeGo, backAndShareGo;
    public TextMeshProUGUI countDownTime;

    private void Start()
    {
        //�������ʱ��ť����ʼ����ʱ������ʱ��ɺ�ִ�н�������ͼƬ����
        //click count down time button ,start count down time ,and then short down screen
        countDownBut.onClick.AddListener(() =>
        {
            countDownTimeGo.SetActive(true);
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
        });
        fifteenCountDownBut.onClick.AddListener(() =>
        {
            StopAllCoroutines();
            ShowShareTip(false);
            StartCoroutine(CountDownTime(15));
        });
        reGetImageBut.onClick.AddListener(() =>
        {
            backAndShareGo.SetActive(false);


        });
        //��ʾ������棬show share panel
        shareQRCodeBut.onClick.AddListener(() =>
        {
            ShowShareTip(true);

        });
        //���չ���  take photo button event
        photoBut.onClick.AddListener(() =>
        {

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
    /// ֹͣ��Ƶ¼�ơ�stop capture 
    /// </summary>
    private void StopCapture()
    {

        _movieCapture.StopCapture();
        Debug.Log("�˳���Ƶ¼��");
        //ShowShareTip(true);
        //��������ļ�·��
        Debug.Log("�������ɵ��ļ�·����   " + _movieCapture.LastFilePath);

        //�����ļ�֮���ϴ��ļ���������
        fileUpload.UpdateLoad(_movieCapture.LastFilePath);
    }

    void ClickAction()
    {
        // ���ʱִ�еķ���
        Debug.Log("ִ�н���!");
        _movieCapture.OutputTarget = OutputTarget.ImageSequence;
        _movieCapture.ResolutionDownScale = CaptureBase.DownScale.Half;
        _movieCapture.StartCapture();
        StartCoroutine(WaitOneFrame());

    }

    IEnumerator WaitOneFrame()
    {
        // ȷ��_movieCapture�Ѿ���ʼ����
        if (_movieCapture.IsCapturing())
        {
            // ѭ�����NumEncodedFramesֱ����ֵ�ﵽ1
            while (_movieCapture.CaptureStats.NumEncodedFrames < 1)
            {
                // �ȴ�һ֡
                yield return null;
            }

            // ��NumEncodedFrames�ﵽ1ʱִ�еĲ���
            Debug.Log("NumEncodedFrames has reached 1, stopping the coroutine.");
            // ������������������������ֹͣ�����
            _movieCapture.StopCapture();
            //ShowShareTip(true);
            // ֹͣЭ��
            yield break; // ��ֱ��ʹ�� return;
        }
        else
        {
            Debug.LogWarning("_movieCapture has not started capturing.");
        }

    }
    /// <summary>
    /// ��ʼ¼����Ƶ��start Capture
    /// </summary>
    private void StartCapture()
    {
        // ����ʱִ�еķ�����ִֻ��һ�Σ�
        Debug.Log("��ʼ¼��!");

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