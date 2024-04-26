using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class TestEffect : MonoBehaviour
{
    // Start is called before the first frame update
    public Volume volume; // 引用Volume组件
    public SplitToning splitToning; // 引用SplitToning组件
    private  Slider slider; // 引用Slider组件
    void Start()
    {
        slider = GetComponent<Slider>();
        volume.profile.TryGet(out splitToning);
        slider.onValueChanged.AddListener((arg)=>{
               splitToning.balance  = new ClampedFloatParameter(arg,-100,100,true); 

        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
