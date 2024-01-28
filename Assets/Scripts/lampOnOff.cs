using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lampOnOff : MonoBehaviour
{
    [SerializeField] Light light1, light2 = null;
    [SerializeField] DayNight refDayNight;
    
    void Start()
    {
        refDayNight = FindAnyObjectByType<DayNight>();
        refDayNight.OnNightStarted += LampOn;
        refDayNight.OnDayStarted += LampOff;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void LampOn()
    {
       light1.intensity = 300;
       light2.intensity = 300;    
    }
    public void LampOff()
    {
        light1.intensity = 0;
        light2.intensity = 0;
    }

}
