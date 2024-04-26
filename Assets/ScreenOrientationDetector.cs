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
            // ������ķ���仯������
            OnOrientationChanged();
        }
    }

    private void OnOrientationChanged()
    {
        Debug.Log("��Ļ�����Ѹ���Ϊ: " + Screen.orientation);
        // ��������������߼���������Ļ����仯
        float widthRect = m_RectT.rect.width;
        float hightRect = m_RectT.rect.height;
        m_RectT.sizeDelta = new Vector2(hightRect, widthRect );
    }
}
