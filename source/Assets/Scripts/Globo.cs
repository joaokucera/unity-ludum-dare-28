using UnityEngine;
using System.Collections;

public class Globo : MonoBehaviour
{

    private const int StartCounter = 5;
    private int control;

    void Update()
    {
        if (control <= 0)
        {
            control = StartCounter;
            renderer.material.color = new Color(Random.Range(0, 5), Random.Range(0, 5), Random.Range(0, 5));
        }

        control--;

        if (name == "Globo")
        {
            transform.Rotate(Vector3.forward);
        }
    }
}
