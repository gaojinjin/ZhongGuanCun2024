using RenderHeads.Media.AVProMovieCapture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.IO;

public class RotateCube : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool hasBeenLongPressed = false; // 用于判断长按是否已经被识别和执行
    public float longPressThreshold = 0.5f; // 长按的时间阈值，可以根据需要调整
    [SerializeField] CaptureBase _movieCapture = null;
    public void OnPointerDown(PointerEventData eventData)
    {
        hasBeenLongPressed = false; // 初始设置为false
        Invoke("CheckIfLongPress", longPressThreshold); // 在阈值时间后检查是否为长按
        //zhi
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        CancelInvoke("CheckIfLongPress"); // 取消长按检查

        // 如果在松开按钮时长按还未被识别，则执行点击方法
        if (!hasBeenLongPressed)
        {
            ClickAction();
        }
        _movieCapture.StopCapture();
    }

    void CheckIfLongPress()
    {
        // 仅当长按还未被识别时才执行长按方法
        if (!hasBeenLongPressed)
        {
            LongPressAction();
            hasBeenLongPressed = true; // 标记长按已经被识别和执行
        }
    }

    void ClickAction()
    {
        // 点击时执行的方法
        Debug.Log("执行截屏!");
        /* _movieCapture.OutputTarget = OutputTarget.ImageSequence;
         _movieCapture.StartCapture();
         _movieCapture.StopCapture();*/


    }
    
    void LongPressAction()
    {
        // 长按时执行的方法（只执行一次）
        Debug.Log("开始录屏!");
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