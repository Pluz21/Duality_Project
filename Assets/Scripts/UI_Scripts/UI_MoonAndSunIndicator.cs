using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_MoonAndSunIndicator : MonoBehaviour
{
    [SerializeField] DayNight dayNight = null;
    [SerializeField] GameObject moonIcon = null;
    [SerializeField] GameObject sunIcon = null;
    void Start()
    {
        Init();
    }

    void Init()
    {
        if(!dayNight)
        dayNight = FindAnyObjectByType<DayNight>();
        CheckDayState();
    }

    void CheckDayState()
    {
        dayNight.OnDayStarted += SunIconLogic;
        dayNight.OnNightStarted += MoonIconLogic;
        if (dayNight.DayStateRef == DayState.NIGHT)
        {
            Debug.Log($"REF TO DAYSTATE {dayNight.DayStateRef}");
            MoonIconLogic();

        }
        if (dayNight.DayStateRef == DayState.DAY)
        {
            SunIconLogic();
        }
     
    }

    private void MoonIconLogic()
    {
        moonIcon.SetActive(true);
        sunIcon.SetActive(false);
    }

    private void SunIconLogic()
    {
        moonIcon.SetActive(false);
        sunIcon.SetActive(true);
    }

    void Update()
    {
        CheckDayState();

    }
}
