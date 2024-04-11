using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public class FileUploader : MonoBehaviour
{
    public string filePath = "C:/Users/ASUS/Desktop/b.mp4";
    private string uploadURL = "https://kiwistudio.top/uploads/upload.php"; // 更改为你的服务器URL
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
            Debug.Log("文件上传成功！");
        }
    }

    // 调用示例
    void Start()
    {
        
        
        StartCoroutine(UploadFile(filePath, uploadURL));
    }
    public void UpdateLoad(string filePath)
    {
        StartCoroutine(UploadFile(filePath, uploadURL));
    }
}
