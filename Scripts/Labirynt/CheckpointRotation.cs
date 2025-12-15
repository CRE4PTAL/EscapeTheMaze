using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointRotation : MonoBehaviour
{
    public float speed = 60f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rotation = Vector3.zero;
        rotation += Vector3.up;
        transform.Rotate(rotation * speed * Time.deltaTime, Space.World);
    }
}
