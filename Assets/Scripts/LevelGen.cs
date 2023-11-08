using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGen: MonoBehaviour{

    void OnTriggerEnter(Collider other) {
        StartCoroutine("Transfer");
    }

    IEnumerator Transfer() {
        //Debug.Log("Hit the trigger zone z = " + gameObject.transform.position.z);
        yield return new WaitUntil(() => Camera.main.transform.position.z >= gameObject.transform.position.z);
        //yield return new WaitForSeconds(1);
        gameObject.transform.parent.position = new Vector3(0, 0, gameObject.transform.position.z + 200);
        // note that the 100 in the above is the forward (z) leength of the Ground object in the Planet_a
        // Basically there's two pieces of ground and they're walking forward to provide a level surface
        // like hwen you move your hands to let a spider walk on them.
        // BUT
        // what if it didn't move the level until you were [camera offset] away...
    }
}
