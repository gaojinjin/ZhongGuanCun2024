using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownMove : MonoBehaviour
{
    // Start is called before the first frame update
    private Renderer render;
    public float speed = 0.1f;
    void Start()
    {
        render =GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(render){
            render.material.mainTextureOffset = new Vector2(0,Time.time*speed);
        
        }      
    }
}
