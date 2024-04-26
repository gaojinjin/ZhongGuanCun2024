using UnityEngine;

public class ScreenOrientationDetector : MonoBehaviour
{
    private ScreenOrientation lastOrientation;
    private RectTransform m_RectT;
    void Start()
    {
        m_RectT = GetComponent<RectTransform>();
        // 初始化屏幕方向
        lastOrientation = Screen.orientation;
    }

    void Update()
    {
        // 检测屏幕方向是否发生变化
        if (Screen.orientation != lastOrientation)
        {
            lastOrientation = Screen.orientation;
            // 调用你的方向变化处理函数
            OnOrientationChanged();
        }
    }

    private void OnOrientationChanged()
    {
        Debug.Log("屏幕方向已更改为: " + Screen.orientation);
        // 在这里添加您的逻辑，处理屏幕方向变化
        float widthRect = m_RectT.rect.width;
        float hightRect = m_RectT.rect.height;
        m_RectT.sizeDelta = new Vector2(hightRect, widthRect );
    }
}
