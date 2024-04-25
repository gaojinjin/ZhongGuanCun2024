using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public class FileUploader : MonoBehaviour
{
    public string filePathT = "C:/Users/ASUS/Desktop/b.mp4";
    private string uploadURL = "https://kiwistudio.top/uploads/upload1.php"; // ����Ϊ��ķ�����URL
    public EasyQRCode easyQRCode;
    IEnumerator UploadFile(string filePath, string uploadURL)
    {
        easyQRCode.ClearRawImage();
        easyQRCode.UpdateQRCode(Path.GetFileName(filePath));
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
            Debug.Log("have been upload to server!");
            //upload file and create QR Code

            easyQRCode.waitQECode.gameObject.SetActive(false);
        }
        
    }

    // ����ʾ��
    //void Start()
    //{
        
    //    StartCoroutine(UploadFile(filePathT, uploadURL));
    //}
    public void UpdateLoad(string mFilePath)
    {
        StartCoroutine(UploadFile(mFilePath, uploadURL));
    }
}
