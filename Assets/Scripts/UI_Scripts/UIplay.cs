using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIplay : MonoBehaviour
{
    [SerializeField] MovementComponent refplayer = null;
   
    //Texts Values
    [SerializeField] TextMeshProUGUI dashTime = null;
    [SerializeField] TextMeshProUGUI dashNumber = null;
    [SerializeField] TextMeshProUGUI inviTimer = null;

    //Dash Variables
    [SerializeField] Image dashIcon = null;
    [SerializeField] Color dashUsedIconColor = new Color();
    [SerializeField] Color noMoreDashesAvailableColor = new Color();
    Color dashInitialIconColor = new Color();

    //Invisibility Variables
    [SerializeField] Image invisibilityIcon = null;
    [SerializeField] Color invisibilityIconColor = new Color();
    Color invisibilityIconInitialColor = new Color();


    //Life Variables 
    [SerializeField] Slider sliderLife = null;
    [SerializeField] Player refLifeplayer = null;

    //Events 
    public event Action<float> UINumbreDash = null;
    public event Action<float> UITime = null;
    public event Action<float> UITimeInvi = null;
    public event Action<float, float> UILifeSlider = null;

    void Start()
    {
        Init();
       
    }

    void Init()
    {
        dashInitialIconColor = dashIcon.canvasRenderer.GetColor();
        invisibilityIconInitialColor = invisibilityIcon.canvasRenderer.GetColor();

        refplayer.OnDash += UpdateDashUsedIconColor;
        refplayer.OnInvisibilityStarted += UpdateInvisibilityIconColor;
        refplayer.OnDashFinished += ResetDashIconColor;
        
    }
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
        //dash checker
        if (CheckPlayerDashesQuantity())
        {
            UpdateAllDashesUsedIconColor();
        }
        else
        {
            ResetDashIconColor();
        }



    }

    private bool CheckPlayerDashesQuantity()
    {
        if (refplayer.NumberDash < 1)
        {
            return true;
            //            UpdateAllDashesUsedIconColor();

        }
        else
        {
            return false;
            //ResetDashIconColor();
        }
    }
    private void UpdateInvisibilityIconColor()
    {
        invisibilityIcon.canvasRenderer.SetColor(invisibilityIconColor);
        Invoke(nameof(ResetInvisibilityIconColor), refplayer.TimerInvi);

    }
    private void ResetIconColor(Image _iconToChange)  // couldn't turn this one into lambda for an Invoke();
    {
        _iconToChange.canvasRenderer.SetColor(invisibilityIconInitialColor);

    }
    private void ResetInvisibilityIconColor()
    {
        invisibilityIcon.canvasRenderer.SetColor(invisibilityIconInitialColor);

    }
    private void ResetDashIconColor()
    {
        dashIcon.canvasRenderer.SetColor(dashInitialIconColor);

    }

    // Update is called once per frame

    public void UpdateDashUsedIconColor()
    {
        dashIcon.canvasRenderer.SetColor(dashUsedIconColor);
    }
    public void UpdateAllDashesUsedIconColor()
    {
        dashIcon.canvasRenderer.SetColor(noMoreDashesAvailableColor);
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
