using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("This shows that the Start function has gone.");
        OurOwnMethod();
    }

    // Update is called once per every frame
    void Update()
    {
        Debug.Log("This shows that the Update function is going.");
    }

    // A method we made up
    void OurOwnMethod()
    {
        Debug.Log("This is our own method");
    }
}
