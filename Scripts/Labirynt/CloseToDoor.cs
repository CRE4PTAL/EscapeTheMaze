using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CloseToDoor : MonoBehaviour
{
    public TextMeshProUGUI tmp;
    public TextMeshProUGUI tmp1;
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "TextShower")
        {
            tmp.gameObject.SetActive(true);
        }

        if(other.name == "TextShower1")
        {
            tmp1.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        tmp.gameObject.SetActive(false);
        tmp1.gameObject.SetActive(false);
    }

    void Start()
    {
        tmp.gameObject.SetActive(false);
        tmp1.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
