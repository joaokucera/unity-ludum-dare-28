using UnityEngine;
using System.Collections;

public class DoorBehavior : MonoBehaviour
{
    [HideInInspector]
    public bool isOpen = false;

    public AudioClip openDoorClip;
    public AudioClip closeDoorClip;

    public string animationOpenDoor;
    public string animationCloseDoor;

    public void OpenDoor()
    {
        audio.PlayOneShot(openDoorClip);
        animation.Play(animationOpenDoor);
        isOpen = true;

        Invoke("CloseDoor", 5f);
    }

    private void CloseDoor()
    {
        audio.PlayOneShot(closeDoorClip);
        animation.Play(animationCloseDoor);
        isOpen = false;

        CancelInvoke("CloseDoor");
    }
}