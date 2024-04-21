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
        m_button.onClick.AddListener(() =>
        {
            countDownText.text = countDownTIme.ToString();
            StopAllCoroutines();
            tempTime = countDownTIme;
            isRecording = !isRecording;
            defultImage.SetActive(!isRecording);
            recodingImage.SetActive(isRecording);
            countDownText.gameObject.SetActive(isRecording);
            mainManager.HasBeenLongPressed = isRecording;
            StartCoroutine(CountDownTimeMethend(countDownTIme));
        });
    }
    IEnumerator CountDownTimeMethend(int countDownTime)
    {

        while (tempTime > 1)
        {
            tempTime--;
            countDownText.text = tempTime.ToString();
            yield return new WaitForSeconds(1);

        }
        mainManager.HasBeenLongPressed = false;
        defultImage.SetActive(true);
        recodingImage.SetActive(false);
        countDownText.gameObject.SetActive(false);
        isRecording = false;
    }


}
