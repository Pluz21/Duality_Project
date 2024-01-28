using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UI_DeathMenu : MonoBehaviour
{
    [SerializeField] GameObject deathMenu = null;
    [SerializeField] ScrollRect kanjiScrollViewRect = null;
    [SerializeField] public float scrollSpeed = 1f;
    void Start()
    {
        Init();
    }

    void Init()
    {
        deathMenu.SetActive(false);
  
        

    }

    // Update is called once per frame
    void Update()
    {
        if (deathMenu.activeSelf)
        {
            // Get the current vertical position
            float currentVerticalPosition = kanjiScrollViewRect.verticalNormalizedPosition;

            // Calculate the new vertical position for scrolling
            float newVerticalPosition = currentVerticalPosition - Time.deltaTime * scrollSpeed;

            // Ensure the value is between 0 and 1
            newVerticalPosition = Mathf.Clamp01(newVerticalPosition);

            // Set the new vertical position
            kanjiScrollViewRect.verticalNormalizedPosition = newVerticalPosition;
        }

    }
}
