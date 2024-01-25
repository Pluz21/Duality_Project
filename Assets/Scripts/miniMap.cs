using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniMap : MonoBehaviour
{
    [SerializeField] Transform player = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newposition = player.position;
        newposition.y =transform.position.y;
        transform.position = newposition;
    }
}
