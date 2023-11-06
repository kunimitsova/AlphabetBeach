using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnotherExample : MonoBehaviour
{

    public bool isDaveAGoodBloke = false;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Is Dave a good bloke? " + isDaveAGoodBloke);
        isDaveAGoodBloke = true;
        Debug.Log("Is Dave a good bloke? " + isDaveAGoodBloke);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
