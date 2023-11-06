using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLogic : MonoBehaviour
{   
    public Transform batuSpawnPoint;
    public GameObject batuPrefab;
    public float batuSpeed = 10f;

    private CameraLogic cameraLogic;

    // Start is called before the first frame update
    void Start()
    {
        cameraLogic = GetComponent<CameraLogic>();
    }

    // Update is called once per frame
    void Update()        
    {
        if (cameraLogic.AIMMode && Input.GetKeyDown(KeyCode.Mouse0)) 
        {
            lempar();
        }
    }

    private void lempar()
    {
        var batu = Instantiate(batuPrefab, batuSpawnPoint.position, batuSpawnPoint.rotation);
        Rigidbody batuRigidbody = batu.GetComponent<Rigidbody>();
        if (batuRigidbody != null)
        {
            if (Camera.main != null)
            {
                batuRigidbody.velocity = Camera.main.transform.forward * batuSpeed;
            }
            else
            {
                Debug.LogError("Main camera not found in the scene.");
            }
        }
        Destroy(batu, 2.0f);
    } 
}
