using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EasyQRCode : MonoBehaviour {

    public string textToEncode = "Hello World!";
    public Color darkColor = Color.black;
    public Color lightColor = Color.white;
    public RawImage qrCodeImage;
    public void UpdateQRCode(string url)
    {
        // Debug.Log(" url  info  "+ url);
        // Example usage of QR Generator
        // The text can be any string, link or other QR Code supported string
        textToEncode = "https://kiwistudio.top/uploads/uploads/"+ url;
        Texture2D qrTexture = QRGenerator.EncodeString(textToEncode, darkColor, lightColor);

        // Set the generated texture as the mainTexture on the quad
        qrCodeImage.texture = qrTexture;
    }
}
