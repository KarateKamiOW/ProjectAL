using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float minHeight, maxHeight;


    private void Update()
    {
        //transform.position = new Vector3(target.position.x, transform.position.y, transform.position.z);

        transform.position = new Vector3(target.position.x, Mathf.Clamp(target.position.y, minHeight, maxHeight), transform.position.z);
    }
}
