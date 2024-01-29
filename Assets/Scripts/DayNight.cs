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
    public Color skyColor; public Color equatorColor; public float ambientIntensity;
    public void SetAmbientColors(Color _skyColor, Color _equatorColor)
    {
        skyColor = _skyColor; equatorColor = _equatorColor;
    }
    public void UpdateAmbientColors(Color _skyColor, Color _equatorColor)
    {
        RenderSettings.ambientGroundColor = _skyColor;
        RenderSettings.ambientSkyColor = _skyColor;
        RenderSettings.ambientEquatorColor = _equatorColor;
    }

    public void UpdateAmbientIntensity(float _intensity)
    {
        RenderSettings.reflectionIntensity = _intensity;
        Debug.Log("Called Test function in struct");
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
    //struct settings
    AmbientColors dayColors = new AmbientColors();
    AmbientColors nightColors = new AmbientColors();

    //
    [SerializeField] Color nightAmbientSkyColor = new Color();
    [SerializeField] Color nightAmbientEquatorColor = new Color();
    [SerializeField,Range(0.1f,0.8f)] float customNightIntensity = 0.4f;
    [SerializeField] float customDayIntensity = 1f;
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
        dayColors.SetAmbientColors(RenderSettings.ambientSkyColor, RenderSettings.ambientEquatorColor);
        nightColors.SetAmbientColors(nightAmbientSkyColor, nightAmbientEquatorColor);

    }

    
    void InitEvents()
    {
        OnTimeElapsed += SetDayNightState;
        OnNightStarted += SetNightAmbientColors;
        OnDayStarted += SetDayAmbientColors;

    }

    private void SetNightAmbientColors()
    {
        nightColors.UpdateAmbientColors(nightColors.skyColor, nightColors.equatorColor);
        nightColors.UpdateAmbientIntensity(customNightIntensity);
    }
    private void SetDayAmbientColors()
    {
        dayColors.UpdateAmbientColors(dayColors.skyColor, dayColors.equatorColor);
        nightColors.UpdateAmbientIntensity(customDayIntensity);       
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
    }
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
