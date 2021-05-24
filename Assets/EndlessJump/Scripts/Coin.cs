using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1;

    public GameObject coin = null;
    public AudioClip audioClip = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player has picked up a coin.");

            Manager.instance.updateCoinCount(coinValue);

            coin.SetActive(false);
            this.GetComponent<AudioSource>().PlayOneShot(audioClip);  

            Destroy(this.gameObject, audioClip.length);
        }
    }
}
