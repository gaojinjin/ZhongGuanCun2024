using RenderHeads.Media.AVProMovieCapture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.IO;

public class RotateCube : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool hasBeenLongPressed = false; // �����жϳ����Ƿ��Ѿ���ʶ���ִ��
    public float longPressThreshold = 0.5f; // ������ʱ����ֵ�����Ը�����Ҫ����
    [SerializeField] CaptureBase _movieCapture = null;
    public void OnPointerDown(PointerEventData eventData)
    {
        hasBeenLongPressed = false; // ��ʼ����Ϊfalse
        Invoke("CheckIfLongPress", longPressThreshold); // ����ֵʱ������Ƿ�Ϊ����
        //zhi
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        CancelInvoke("CheckIfLongPress"); // ȡ���������

        // ������ɿ���ťʱ������δ��ʶ����ִ�е������
        if (!hasBeenLongPressed)
        {
            ClickAction();
        }
        _movieCapture.StopCapture();
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
        /* _movieCapture.OutputTarget = OutputTarget.ImageSequence;
         _movieCapture.StartCapture();
         _movieCapture.StopCapture();*/


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