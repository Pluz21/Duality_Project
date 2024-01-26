using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dashTime = null;
    [SerializeField] TextMeshProUGUI dashNumber = null;
    [SerializeField] TextMeshProUGUI inviTimer = null;
    [SerializeField] Slider sliderLife = null;
    public event Action<float> UINumbreDash = null;
    public event Action<float> UITime = null;
    public event Action<float> UITimeInvi = null;
    public event Action<float, float> UILifeSlider = null;
    [SerializeField] MovementComponent refplayer = null;
    [SerializeField] Player refLifeplayer = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UILifeSlider += UILife;
        UILifeSlider?.Invoke(refLifeplayer.Life, refLifeplayer.MaxLife);

        UITimeInvi += InviUI;
        UITimeInvi?.Invoke(refplayer.TimerInvi);

        UINumbreDash += NumbrerDash;
        UINumbreDash?.Invoke(refplayer.NumberDash);

        UITime += TimerDash;
        UITime?.Invoke(refplayer.RegenDash);
    }
    public void TimerDash(float _value)
    {
        dashTime.text = $"{((int)_value)}";    
    }

    public void NumbrerDash(float _value)
    {
        dashNumber.text = $"{((int)_value)}";
    }

    public void InviUI(float _value)
    {
        inviTimer.text = $"{((int)_value)}";
    }

    public void UILife(float _value, float _maxvalue)
    {
        sliderLife.value = _value / _maxvalue;
    }
}
