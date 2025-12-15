using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetKey : MonoBehaviour
{
    string keyName;
    public GameObject key;
    public bool haveKey1 = false;
    public bool haveKey2 = false;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            string keyName = other.gameObject.name;

            if (keyName == "Key1")
            {
                haveKey1 = true;
            }
            else if (keyName == "Key2")
            {
                haveKey2 = true;
            }

            other.gameObject.SetActive(false);
        }
    }
}
