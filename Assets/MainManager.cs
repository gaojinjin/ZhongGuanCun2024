using RenderHeads.Media.AVProMovieCapture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class MainManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Button countDownBut,backShareBut,tenCountDownBut,fiveCountDownBut,reGetImageBut,shareQRCodeBut;
    public FileUploader fileUpload;
    private bool hasBeenLongPressed = false; // �����жϳ����Ƿ��Ѿ���ʶ���ִ��
    public float longPressThreshold = 0.5f; // ������ʱ����ֵ�����Ը�����Ҫ����
    [SerializeField] CaptureBase _movieCapture = null;
    public GameObject shareGroupGo,countDownTimeGo,backAndShareGo;
    public TextMeshProUGUI countDownTime;

    private void Start()
    {
        //�������ʱ��ť����ʼ����ʱ������ʱ��ɺ�ִ�н�������ͼƬ����
        //click count down time button ,start count down time ,and then short down screen
        countDownBut.onClick.AddListener(() =>
        {
            countDownTimeGo.SetActive(true);
        });
        backShareBut.onClick.AddListener(() => {
            ShowShareTip(false);
        });

        tenCountDownBut.onClick.AddListener(() => {
            StopAllCoroutines();
            ShowShareTip(false);
            StartCoroutine(CountDownTime(10));
        });
        fiveCountDownBut.onClick.AddListener(() => {
            StopAllCoroutines();
            ShowShareTip(false);
            StartCoroutine(CountDownTime(5));
        });
        reGetImageBut.onClick.AddListener(() => {
            backAndShareGo.SetActive(false);


        });
        //��ʾ������棬show share panel
        shareQRCodeBut.onClick.AddListener(() => {
            ShowShareTip(true);

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
    public void OnPointerDown(PointerEventData eventData)
    {
        StopAllCoroutines();
        hasBeenLongPressed = false; // ��ʼ����Ϊfalse
        ShowShareTip(false);
        Invoke("CheckIfLongPress", longPressThreshold); // ����ֵʱ������Ƿ�Ϊ����
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        CancelInvoke("CheckIfLongPress"); // ȡ���������

        // ������ɿ���ťʱ������δ��ʶ����ִ�е������
        if (!hasBeenLongPressed)
        {
            ClickAction();
        }
        else
        {
            _movieCapture.StopCapture();
            Debug.Log("�˳���Ƶ¼��");
            //ShowShareTip(true);
        }
        //��������ļ�·��
        Debug.Log("�������ɵ��ļ�·����   " + _movieCapture.LastFilePath);

        //�����ļ�֮���ϴ��ļ���������
        fileUpload.UpdateLoad(_movieCapture.LastFilePath);
    }

    void CheckIfLongPress()
    {
        // ����������δ��ʶ��ʱ��ִ�г�������
        if (!hasBeenLongPressed)
        {
            LongPressAction();
            hasBeenLongPressed = true; // ��ǳ����Ѿ���ʶ���ִ��
        }
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

    void LongPressAction()
    {
        // ����ʱִ�еķ�����ִֻ��һ�Σ�
        Debug.Log("��ʼ¼��!");

        _movieCapture.OutputTarget = OutputTarget.VideoFile;
        _movieCapture.ResolutionDownScale = CaptureBase.DownScale.Original;
        _movieCapture.StartCapture();
    }
    void ShowShareTip(bool mValue) {
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