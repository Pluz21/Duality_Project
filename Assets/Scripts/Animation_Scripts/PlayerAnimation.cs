using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] Animator playerAnimator = null;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateForwardAnimatorParam(float _value)
    {
        if (playerAnimator == null) return;
        playerAnimator.SetFloat(AnimationParameter.ForwardAxisParam, _value);

    }
    public void UpdateRightAnimatorParam(float _value)
    {
        if (playerAnimator == null) return;
        playerAnimator.SetFloat(AnimationParameter.rightAxisParam, _value);
    }
    public void UpdateRotateAnimatorParam(float _value)
    {
        if (playerAnimator == null) return;
        playerAnimator.SetFloat(AnimationParameter.RotateAxisParam, _value);
    }
    public void UpdateIsDashingAnimatorParam(bool _value)
    { 
        if (playerAnimator == null) return;
        playerAnimator.SetBool(AnimationParameter.dash, _value);
    
    }
    public void UpdateIsInvisibleAnimatorParam(bool _value)
    { 
        if (playerAnimator == null) return;
        playerAnimator.SetBool(AnimationParameter.invi, _value);
    
    }
   
    
}
