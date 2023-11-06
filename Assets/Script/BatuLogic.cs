using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;


public class BatuLogic : MonoBehaviour
{
    public float life = 3;      

    void Awake()
    {
        Destroy(gameObject, life);        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag.Equals("Enemy"))
        {
            SlendermanLogic target = collision.transform.GetComponent<SlendermanLogic>();
            target.TakeDamage(50);
        }
    }
}

