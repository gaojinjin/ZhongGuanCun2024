using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System.Net;
using UnityEngine.Networking;

public class EasyQRCode : MonoBehaviour {

    public string textToEncode = "Hello World!";
    public Color darkColor = Color.black;
    public Color lightColor = Color.white;
    public RawImage qrCodeImage;
    public Texture2D whiteQR;
    public TextMeshProUGUI waitQECode;
    public string ipAddress;
    private void Start()
    {
        StartCoroutine(LoadText(@"https://kiwistudio.top/uploads/serverAddress.txt"));
    }
    public void UpdateQRCode(string url)
    {
        waitQECode.gameObject.SetActive(true);
        // Debug.Log(" url  info  "+ url);
        // Example usage of QR Generator
        // The text can be any string, link or other QR Code supported string
        textToEncode = $"https://{ipAddress}/uploads/GetInfo.php?file=uploads/" + url;
        Texture2D qrTexture = QRGenerator.EncodeString(textToEncode, darkColor, lightColor);

        // Set the generated texture as the mainTexture on the quad
        qrCodeImage.texture = qrTexture;
    }

    public void ClearRawImage() {
        //Debug.Log("提示二维码生成 ，tip make qr code");
        qrCodeImage.texture =  whiteQR;
        waitQECode.gameObject.SetActive(true);
    }
    IEnumerator LoadText(string ipServer)
    {
        // Create a UnityWebRequest to get the file
        UnityWebRequest request = UnityWebRequest.Get(ipServer);

        // Send the request and wait for a response
        yield return request.SendWebRequest();

        // Check for errors
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + request.error);
        }
        else
        {
            // Get the text content from the download handler
            string text = request.downloadHandler.text;
            ipAddress = text;
            // Print the text to the console (or handle it as needed)
            Debug.Log("File Content: " + text);

            // You can also handle the text content here, for example, by storing it in a variable
        }
    }
}
