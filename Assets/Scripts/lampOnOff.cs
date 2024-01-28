using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class lampOnOff : MonoBehaviour
{
    [SerializeField] Light light1, light2 = null;
    [SerializeField] List<Light> allSpotLights = new List<Light>();
   
    [SerializeField] DayNight refDayNight;
    
    void Start()
    {
        Init();
    }

    void Init()
    {
        refDayNight = FindAnyObjectByType<DayNight>();
        refDayNight.OnNightStarted += LampOn;
        refDayNight.OnDayStarted += LampOff;
        InitLights(LightType.Spot);
    }

    void InitLights(LightType _typeToInit)
    {

        List<Light> _allLights = GetComponents<Light>().ToList();
        int _size = _allLights.Count;
        for (int i = 0; i < _size; i++)
        {
            if (_allLights[i].type != _typeToInit) return;
            allSpotLights.Add(_allLights[i]);
        }
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
