using RenderHeads.Media.AVProMovieCapture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.IO;

public class RotateCube : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public FileUploader fileUpload;
    private bool hasBeenLongPressed = false; // �����жϳ����Ƿ��Ѿ���ʶ���ִ��
    public float longPressThreshold = 0.5f; // ������ʱ����ֵ�����Ը�����Ҫ����
    [SerializeField] CaptureBase _movieCapture = null;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        hasBeenLongPressed = false; // ��ʼ����Ϊfalse
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
        }
        //��������ļ�·��
        Debug.Log("�������ɵ��ļ�·����   "+ _movieCapture.LastFilePath);

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
        _movieCapture.StartCapture();
    }

}
public enum RecType
{
    None,
    Image,
    Video
}