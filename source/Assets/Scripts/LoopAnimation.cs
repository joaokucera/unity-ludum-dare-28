using UnityEngine;
using System.Collections;

public class LoopAnimation : MonoBehaviour
{
    public Animation animationBody;

    void Start()
    {
        animationBody = GetComponentInChildren<Animation>();
        animationBody.wrapMode = WrapMode.Loop;
    }
}
