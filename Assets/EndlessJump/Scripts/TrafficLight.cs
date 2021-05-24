using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    public GameObject trafficLlight = null;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "train")
        {
            trafficLlight.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "train")
        {
            trafficLlight.SetActive(false);
        }
    }
}
