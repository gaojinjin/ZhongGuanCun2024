using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public class FileUploader : MonoBehaviour
{
    public string filePathT = "C:/Users/ASUS/Desktop/b.mp4";
    private string uploadURL = "https://kiwistudio.top/uploads/upload.php"; // ����Ϊ��ķ�����URL
    IEnumerator UploadFile(string filePath, string uploadURL)
    {
        yield return new WaitForSeconds(1);
        //filePath = filePath.Replace("\\", "/");
        //filePath = filePath.Replace("//", "/");
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
        
        StartCoroutine(UploadFile(filePathT, uploadURL));
    }
    public void UpdateLoad(string mFilePath)
    {
        StartCoroutine(UploadFile(mFilePath, uploadURL));
    }
}
