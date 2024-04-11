using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public class FileUploader : MonoBehaviour
{
    public string filePath = "C:/Users/ASUS/Desktop/b.mp4";
    private string uploadURL = "https://kiwistudio.top/uploads/upload.php"; // ����Ϊ��ķ�����URL
    IEnumerator UploadFile(string filePath, string uploadURL)
    {
        byte[] fileBytes = File.ReadAllBytes(filePath);
        WWWForm form = new WWWForm();
        form.AddBinaryData("file", fileBytes, Path.GetFileName(filePath));

        UnityWebRequest uwr = UnityWebRequest.Post(uploadURL, form);
        yield return uwr.SendWebRequest();

        if (uwr.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(uwr.error);
        }
        else
        {
            Debug.Log("�ļ��ϴ��ɹ���");
        }
    }

    // ����ʾ��
    void Start()
    {
        
        
        StartCoroutine(UploadFile(filePath, uploadURL));
    }
    public void UpdateLoad(string filePath)
    {
        StartCoroutine(UploadFile(filePath, uploadURL));
    }
}
