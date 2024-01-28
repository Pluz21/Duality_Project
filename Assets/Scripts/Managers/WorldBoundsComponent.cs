
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class WorldBoundsComponent : MonoBehaviour
{
    [SerializeField] Player playerRef = null;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


 
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            Rigidbody _rb = other.GetComponent<Rigidbody>();
            if (!_rb) return;
            _rb.useGravity = true;
            Debug.Log($"exiting collision with {other}");
        }

    }

    private void OnTriggerStay(Collider other)
    {
        //if (other.gameObject.GetComponent<Player>())
        //    Debug.Log($"collided with {other}");

    }
   
}
