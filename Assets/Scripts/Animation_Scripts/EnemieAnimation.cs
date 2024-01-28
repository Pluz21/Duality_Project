using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieAnimation : MonoBehaviour
{
    [SerializeField] Animator enemiAnimator = null;
    [SerializeField] Enemy refEnemy = null;
    // Start is called before the first frame update
    void Start()
    {
        enemiAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
      
      refEnemy.attack += updateAttackAnimator;
    }
    public void updateAttackAnimator(bool _value)
    {
        if (enemiAnimator == null) return;
        enemiAnimator.SetBool(AnimationEnemy.Attack, _value);

    }
}
   
