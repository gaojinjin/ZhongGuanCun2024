using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TestSaveFile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string screenshotsDirectory = Path.Combine(Application.persistentDataPath, "Screenshots");
        if (!Directory.Exists(screenshotsDirectory))
        {
            Directory.CreateDirectory(screenshotsDirectory);
        }

        string fileName = "Screenshot_" + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png";
        string filePath = Path.Combine(screenshotsDirectory, fileName);

        ScreenCapture.CaptureScreenshot(fileName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
