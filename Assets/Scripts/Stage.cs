using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public GameObject stage;
    public SlimeMover slimeMover;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            slimeMover.enabled = false;
            stage.SetActive(true);

        }
    }
}
