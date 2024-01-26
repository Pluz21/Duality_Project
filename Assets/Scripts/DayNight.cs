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

    public DayState DayStateRef => dayState;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void Init()
    {
        dayState = DayState.DAY;
        //maxTime = speedSun/2 - 0.8f;
        speedSun = 180 /maxTime;
        OnTimeElapsed += SetDayNightState;
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
        TimeSun();
        sun.transform.Rotate(Vector3.right * speedSun * Time.deltaTime);
        currentTime = IncreaseTime(currentTime,maxTime);
    }
    public void TimeSun()
    {
       // Debug.Log(sun.transform.localRotation.x);
        //Debug.Log(sun.transform.localRotation.x);
        //if (sun.transform.rotation.x >- 0.1 || sun.transform.rotation.x > 0.96)
        //if (sun.transform.localRotation.x > -0.1 || sun.transform.localRotation.x > 0.96)
           //Debug.Log("night");
      //  sun.transform.rotation.x
        
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
