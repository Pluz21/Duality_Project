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
    [SerializeField] Image dashIcon = null;
    [SerializeField] Color dashUsedIconColor = new Color();
    Color dashInitialIconColor = new Color();
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
        Init();
       
    }

    void Init()
    {
        dashInitialIconColor = dashIcon.canvasRenderer.GetColor();

        refplayer.OnDash += UpdateDashUsedIconColor;
        refplayer.OnDashFinished += ResetDashIconColor;
    }

    private void ResetDashIconColor()
    {
        dashIcon.canvasRenderer.SetColor(dashInitialIconColor);

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

    public void UpdateDashUsedIconColor()
    {
        dashIcon.canvasRenderer.SetColor(dashUsedIconColor);
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
