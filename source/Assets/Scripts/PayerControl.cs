using UnityEngine;
using System.Collections;

public class PayerControl : MonoBehaviour {

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //Debug.Log(hit.gameObject.tag);
        if (hit.gameObject.tag == "Door")
        {
            DoorBehavior doorBehavior = hit.gameObject.GetComponent<DoorBehavior>();

            if (!doorBehavior.isOpen)
            {
                doorBehavior.OpenDoor();
            }
        }
    }
}
