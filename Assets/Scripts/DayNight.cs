using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DayNight : MonoBehaviour
{
    [SerializeField] float speedSun = 10f;
    [SerializeField] Light sun = null;
    // Start is called before the first frame update
    void Start()
    {
        sun = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        TimeSun();
        sun.transform.Rotate(Vector3.right * speedSun * Time.deltaTime);
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

}
