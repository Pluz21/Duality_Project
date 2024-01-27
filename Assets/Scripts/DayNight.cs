using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
using UnityEngine.Playables;

public enum DayState 
{
    DAY,
    NIGHT
}

[Serializable]
public struct AmbientColors
{
    public Color skyColor; public Color equatorColor;
    public void UpdateAmbientColors(Color _skyColor, Color _equatorColor)
    {
        skyColor = _skyColor; equatorColor = _equatorColor;
    }
    
}
public class DayNight : MonoBehaviour
{
    public event Action OnTimeElapsed;
    public Action OnDayStarted;
    public Action OnNightStarted;
    [SerializeField] float speedSun = 10f;
    [SerializeField] Light sun = null;
    [SerializeField] float currentTime = 0;
    [SerializeField] float maxTime = 9.2f;
    [SerializeField] DayState dayState;

    //Colors for day and night shifting
    [SerializeField] AmbientColors dayColors = new AmbientColors();
    [SerializeField] AmbientColors nightColors = new AmbientColors();
    [SerializeField] Color customAmbientSkyColor = new Color();
    [SerializeField] Color customAmbientEquatorColor = new Color();
    public DayState DayStateRef => dayState;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void Init()
    {
        InitEvents();

        InitLightEnvironmentColorSettings();
        dayState = DayState.DAY;
        speedSun = 180 /maxTime;       // Scaling the sunSpeed to the maxTime (one day or one night is 180 degrees)
 
    }

    void InitLightEnvironmentColorSettings()
    {
        dayColors.UpdateAmbientColors(RenderSettings.ambientSkyColor, RenderSettings.ambientEquatorColor);
        nightColors.UpdateAmbientColors(customAmbientSkyColor, customAmbientEquatorColor);
        
    }

    
    void InitEvents()
    {
        OnTimeElapsed += SetDayNightState;
        OnNightStarted += SetNightAmbientColors;
        OnDayStarted += SetDayAmbientColors;

    }

    private void SetNightAmbientColors()
    {
        RenderSettings.ambientSkyColor = nightColors.skyColor;
        RenderSettings.ambientEquatorColor = nightColors.equatorColor;
    }
    private void SetDayAmbientColors()
    {
        RenderSettings.ambientSkyColor = dayColors.skyColor;
        RenderSettings.ambientEquatorColor = dayColors.equatorColor;
    }

    private void SetDayNightState()
    {
        if (dayState == DayState.DAY)
        {
            OnNightStarted?.Invoke();
            dayState = DayState.NIGHT;
            Debug.Log($"It is now {dayState}");
        }
        else
        {
            OnDayStarted?.Invoke();
            dayState = DayState.DAY;
        }

        //if (dayState == DayState.NIGHT)
        //{ 
        //    dayState = DayState.DAY;
        //    Debug.Log($"It is now {dayState}");
            
        //}
    }

    // Update is called once per frame
    void Update()
    {
       
        sun.transform.Rotate(Vector3.right * speedSun * Time.deltaTime);
        currentTime = IncreaseTime(currentTime,maxTime);
    }

    float IncreaseTime(float _current, float _max) 
    { 
        _current += Time.deltaTime;
        if (_current > _max)
        { 
            _current = 0;
            OnTimeElapsed?.Invoke();
            return _current;

        }
        return _current;

    }

}
