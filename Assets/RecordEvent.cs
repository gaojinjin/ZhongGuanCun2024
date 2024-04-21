using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RecordEvent : MonoBehaviour
{
    public MainManager mainManager;
    public Button m_button;
    private bool isRecording = false;
    public GameObject defultImage, recodingImage;
    public int countDownTIme = 60, tempTime;
    public TextMeshProUGUI countDownText;
    private void Start()
    {
        m_button = GetComponent<Button>();
        
    }
    


}
