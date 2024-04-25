using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class EasyQRCode : MonoBehaviour {

    public string textToEncode = "Hello World!";
    public Color darkColor = Color.black;
    public Color lightColor = Color.white;
    public RawImage qrCodeImage;
    public Texture2D whiteQR;
    public TextMeshProUGUI waitQECode;
    public void UpdateQRCode(string url)
    {
        waitQECode.gameObject.SetActive(true);
        // Debug.Log(" url  info  "+ url);
        // Example usage of QR Generator
        // The text can be any string, link or other QR Code supported string
        textToEncode = "https://kiwistudio.top/uploads/GetInfo.php?file=uploads/" + url;
        Texture2D qrTexture = QRGenerator.EncodeString(textToEncode, darkColor, lightColor);

        // Set the generated texture as the mainTexture on the quad
        qrCodeImage.texture = qrTexture;
    }

    public void ClearRawImage() {
        //Debug.Log("提示二维码生成 ，tip make qr code");
        qrCodeImage.texture =  whiteQR;
        waitQECode.gameObject.SetActive(true);
    }
}
