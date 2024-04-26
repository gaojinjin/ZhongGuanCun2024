using UnityEngine;

public class ScreenOrientationDetector : MonoBehaviour
{
    private ScreenOrientation lastOrientation;
    private RectTransform m_RectT;
    void Start()
    {
        m_RectT = GetComponent<RectTransform>();
        // ��ʼ����Ļ����
        lastOrientation = Screen.orientation;
    }

    void Update()
    {
        // �����Ļ�����Ƿ����仯
        if (Screen.orientation != lastOrientation)
        {
            lastOrientation = Screen.orientation;
            // ������ķ���仯��������
            OnOrientationChanged();
        }
    }

    private void OnOrientationChanged()
    {
        Debug.Log("屏幕方向已更改为: " + Screen.orientation);
    // 根据新的屏幕方向调整 Image 的尺寸
    if (Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight)
    {
        // 横屏时调整尺寸
        m_RectT.sizeDelta = new Vector2(Screen.height, Screen.width);
    }
    else if (Screen.orientation == ScreenOrientation.Portrait || Screen.orientation == ScreenOrientation.PortraitUpsideDown)
    {
        // 竖屏时调整尺寸
        m_RectT.sizeDelta = new Vector2(Screen.width, Screen.height);
    }
    }
}
