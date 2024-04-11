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
    private bool hasBeenLongPressed = false; // 用于判断长按是否已经被识别和执行
    public float longPressThreshold = 0.5f; // 长按的时间阈值，可以根据需要调整
    [SerializeField] CaptureBase _movieCapture = null;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        hasBeenLongPressed = false; // 初始设置为false
        Invoke("CheckIfLongPress", longPressThreshold); // 在阈值时间后检查是否为长按
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        CancelInvoke("CheckIfLongPress"); // 取消长按检查

        // 如果在松开按钮时长按还未被识别，则执行点击方法
        if (!hasBeenLongPressed)
        {
            ClickAction();
        }
        else
        {
            _movieCapture.StopCapture();
            Debug.Log("退出视频录制");
        }
        //输出最终文件路径
        Debug.Log("最终生成的文件路径：   "+ _movieCapture.LastFilePath);

        //生成文件之后上传文件到服务器
        fileUpload.UpdateLoad(_movieCapture.LastFilePath);
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
        _movieCapture.OutputTarget = OutputTarget.ImageSequence;
        _movieCapture.StartCapture();
        StartCoroutine(WaitOneFrame());
    }
    
    IEnumerator WaitOneFrame()
    {
        // 确保_movieCapture已经开始捕获
        if (_movieCapture.IsCapturing())
        {
            // 循环检查NumEncodedFrames直到其值达到1
            while (_movieCapture.CaptureStats.NumEncodedFrames < 1)
            {
                // 等待一帧
                yield return null;
            }

            // 当NumEncodedFrames达到1时执行的操作
            Debug.Log("NumEncodedFrames has reached 1, stopping the coroutine.");
            // 这里可以添加其他操作，例如停止捕获等
             _movieCapture.StopCapture();

            // 停止协程
            yield break; // 或直接使用 return;
        }
        else
        {
            Debug.LogWarning("_movieCapture has not started capturing.");
        }
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