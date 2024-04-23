using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TestInstanceFile : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject InstanceGo;
    public Button createBut;
    void Start()
    {
        createBut.onClick.AddListener(() => {
            Instantiate(InstanceGo);

        });

        Addressables.LoadAssetAsync<GameObject>("Show").Completed += OnAssetLoaded;
    }

    private void OnAssetLoaded(AsyncOperationHandle<GameObject> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            GameObject loadedAsset = obj.Result;
            Instantiate(loadedAsset);
        }
    }
}