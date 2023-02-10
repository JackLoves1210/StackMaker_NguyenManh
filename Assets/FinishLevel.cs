using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLevel : MonoBehaviour
{
    public GameController controller;

    private void Start()
    {
        controller = GameObject.FindObjectOfType<GameController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("HI");
        if (collision.gameObject.tag == "Player")
        {
            controller.ResetGame();
        }
    }
}
