using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebGLManager : MonoBehaviour
{
    public Vector3 speed;
    // Start is called before the first frame update
    void Update()
    {
        transform.Rotate(speed * Time.deltaTime);
    }
    private void Start()
    {
        
    }

}
