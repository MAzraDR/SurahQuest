using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Groundcheck : MonoBehaviour
{
    MovementLogic logicmovement;
    private void Start()
    {
        logicmovement = this.GetComponentInParent<MovementLogic>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        logicmovement.groundedchanger();        
    }


}
