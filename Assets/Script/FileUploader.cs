using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public class FileUploader : MonoBehaviour
{
    public string filePathT = "C:/Users/ASUS/Desktop/b.mp4";
    private string uploadURL = "https://kiwistudio.top/uploads/upload1.php"; // 更改为你的服务器URL
    public EasyQRCode easyQRCode;
    IEnumerator UploadFile(string filePath, string uploadURL)
    {
        easyQRCode.ClearRawImage();
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
            Debug.Log("文件上传成功！");
            //upload file and create QR Code
           
            easyQRCode.UpdateQRCode(Path.GetFileName(filePath));
        }
        
    }

    // 调用示例
    //void Start()
    //{
        
    //    StartCoroutine(UploadFile(filePathT, uploadURL));
    //}
    public void UpdateLoad(string mFilePath)
    {
        StartCoroutine(UploadFile(mFilePath, uploadURL));
    }
}
