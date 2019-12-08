using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupContoller : MonoBehaviour
{
    private PlayerController playerController;

    void Start()
    {
        GameObject playerControllerObject = GameObject.FindWithTag("Player");
        if (playerControllerObject != null)
        {
            playerController = playerControllerObject.GetComponent<PlayerController>();
        }
        if (playerController == null)
        {
            Debug.Log("Cannot find 'PlayerController' script");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerController.PickUp();
            
            Destroy(gameObject);
        }
    }
}
     