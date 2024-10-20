using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Col : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("B");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("A");
    }
}
