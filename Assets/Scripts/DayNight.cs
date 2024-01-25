using System.Collections;
using System.Collections.Generic;
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
        sun.transform.Rotate(Vector3.right * speedSun * Time.deltaTime);
    }
}
