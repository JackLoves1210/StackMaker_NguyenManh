using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float speed = 20f;
    Vector3 target_1 = new Vector3();

    private void Start()
    {
        target = FindObjectOfType<Player>().transform;
       
    }

    private void Update()
    {
        target_1 = new Vector3(target.position.x, 0.25f, target.position.z);
        transform.position = Vector3.Lerp(transform.position, target_1 + offset, Time.deltaTime * speed);
    }
}
