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
    public GameObject defultImage,recodingImage;
    public int countDownTIme=60,tempTime;
    public TextMeshProUGUI countDownText;
    private void Start()
    {
        
        StopAllCoroutines();
        countDownText.text = countDownTIme.ToString();
        m_button = GetComponent<Button>();
        m_button.onClick.AddListener(() =>
        {
            tempTime = countDownTIme;
            isRecording = !isRecording;
            defultImage.SetActive(!isRecording);
            recodingImage.SetActive(isRecording);
            countDownText.gameObject.SetActive(isRecording);
            mainManager.HasBeenLongPressed = isRecording;
            StartCoroutine(CountDownTimeMethend(countDownTIme));
        });
    }
IEnumerator CountDownTimeMethend(int countDownTime){

    while(tempTime > 0){
        yield return new WaitForSeconds(1);
        tempTime--;
        countDownText.text = tempTime.ToString();
    }
    mainManager.HasBeenLongPressed =false;
}


}
