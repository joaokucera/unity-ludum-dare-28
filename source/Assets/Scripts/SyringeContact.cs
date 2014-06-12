using UnityEngine;
using System.Collections;

public class SyringeContact : MonoBehaviour
{

    float fRange = 0.7f;

    // Ok, the code below is a hideous hack.
    public LevelManager levelManagerScript;

    FPSInputController fpsScript;
    CharacterMotor movementScript;
    MouseLook mouseScript;

    void Awake()
    {

        // Find the level manager object and script
        GameObject goLevelManager = GameObject.Find("Level Manager");

        if (goLevelManager)
        {

            levelManagerScript = goLevelManager.GetComponent<LevelManager>();
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        movementScript = player.GetComponent<CharacterMotor>();
        mouseScript = player.GetComponent<MouseLook>();
        fpsScript = player.GetComponent<FPSInputController>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CheckSyringeContact()
    {

        CheckForCollidersInsideRadius(8);
    }

    /* ==========================================================================================================
     * CLASS METHODS
     * ==========================================================================================================
     */

    /// <summary>
    /// Check for colliders inside the radius.
    /// </summary>
    public void CheckForCollidersInsideRadius(int nLayer)
    {

        Vector3 v3Center = this.transform.position;

        // 1 - Check for colliders inside some radius
        Collider[] colHitColliders = Physics.OverlapSphere(v3Center, fRange, 1 << nLayer);

        if (colHitColliders.Length <= 0)
        {
            // Wasted shot, do not hit anybody!
            levelManagerScript.LevelLostWastedShot();

            StopPlayerToWalkAndLook();

            return;
        }

        // 2 - Check for the angle between these actors and me
        foreach (Collider colOther in colHitColliders)
        {

            // DEBUG
            Debug.Log("Hit " + colOther.transform);

            // 1 - Get the person script from the person hit
            PersonHit personScript = colOther.transform.gameObject.GetComponent<PersonHit>();

            if (personScript)
            {

                // 2 - tell him that he was hit
                bool isTarget = personScript.Hit();

                // Yes, we've hitted the target
                if (isTarget)
                {
                    levelManagerScript.LevelWon();
                }
                else
                {
                    levelManagerScript.LevelLostWrongTarget();
                }

                StopPlayerToWalkAndLook();
            }
        }
    }

    void StopPlayerToWalkAndLook()
    {
        Camera.main.GetComponent<MouseLook>().enabled = false;
        mouseScript.enabled = false;

        movementScript.enabled = false;
        fpsScript.enabled = false;
    }
}
