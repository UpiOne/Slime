using UnityEngine;

public class SlimeMover : MonoBehaviour
{
  
    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.z += 2.0f * Time.deltaTime; // Move slime by 2 units per second
        transform.position = pos;
       
    }
}
