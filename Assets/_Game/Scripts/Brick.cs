using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private Vector3 sizeBrick;

    Vector3 ySizeBrick;
    private void Awake()
    {
        sizeBrick = transform.localScale;
        ySizeBrick = new Vector3(0,0.25f,0);
    }

    private void Update()
    {
        
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        Debug.Log("N");
    //        other.gameObject.transform.position += ySizeBrick;
    //        Destroy(gameObject);
    //    }
    //}

    private void BrickUp()
    {
        
    }
}
