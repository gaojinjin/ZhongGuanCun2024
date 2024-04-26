using UnityEngine;

public class ScreenOrientationDetector : MonoBehaviour
{
    private ScreenOrientation lastOrientation;
    private RectTransform m_RectT;
    private float initialAspectRatio; // 记录初始的宽高比例

    void Start()
    {
        m_RectT = GetComponent<RectTransform>();
        lastOrientation = Screen.orientation;

        // 记录初始的宽高比例
        initialAspectRatio = (float)Screen.width / Screen.height;
    }

    void Update()
    {
        if (Screen.orientation != lastOrientation)
        {
            lastOrientation = Screen.orientation;
            OnOrientationChanged();
        }
    }

    private void OnOrientationChanged()
    {
        Debug.Log("屏幕方向已更改为: " + Screen.orientation);

        // 根据新的屏幕方向调整 Image 的尺寸
        if (Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight)
        {
            // 横屏时调整尺寸,保持宽高比例不变
            m_RectT.sizeDelta = new Vector2(Screen.width, Screen.width / initialAspectRatio);
        }
        else if (Screen.orientation == ScreenOrientation.Portrait || Screen.orientation == ScreenOrientation.PortraitUpsideDown)
        {
            // 竖屏时调整尺寸,保持宽高比例不变
            m_RectT.sizeDelta = new Vector2(Screen.height * initialAspectRatio, Screen.height);
        }
    }
}
