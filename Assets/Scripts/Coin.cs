using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] AudioClip coinSFX;
    [SerializeField] int coinValue;
    
    bool wasPickedup = false;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !wasPickedup)
        {
            wasPickedup = true;
            AudioSource.PlayClipAtPoint(coinSFX, Camera.main.transform.position);
            FindObjectOfType<GameSession>().AddScore(coinValue);
            Destroy(gameObject);
        }
    }
}
