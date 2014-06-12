using UnityEngine;
using System.Collections;

/// <summary>
/// Controls all things related to the level flow
/// </summary>
public class LevelManager : MonoBehaviour
{

    public SoundManager soundManager;

    public string stMenuLevelName;
    public string stThisLevelName;

    public Transform trObjectivesCard;
    public ObjectivesCard objectivesCardScript;
    public GUIObjectivesCard guiObjectivesCardScript;
    public GUILevelEnd guiLevelEnd;

    /* ==========================================================================================================
     * UNITY METHODS
     * ==========================================================================================================
     */

    void Awake()
    {

        guiLevelEnd = gameObject.GetComponent<GUILevelEnd>();
    }

    void Start()
    {

        stThisLevelName = Application.loadedLevelName;
    }


    /* ==========================================================================================================
     * CLASS METHODS
     * ==========================================================================================================
     */
    void OnChangeScreen(string levelName)
    {
        soundManager.control = SoundControl.OUT;
        AutoFade.LoadLevel(levelName, 1.5f, 1.5f, Color.black);
    }

    /// <summary>
    /// What to do when the player won the level, i.e., kills the correct target
    /// </summary>
    public void LevelWon()
    {

        objectivesCardScript.ForceCardToBeShow();
        guiObjectivesCardScript.IconShowRightTarget();
        guiLevelEnd.SetGUILevelWon();
    }

    /// <summary>
    /// What to do when the player looses the level by killing the wrong target
    /// </summary>
    public void LevelLostWrongTarget()
    {

        objectivesCardScript.ForceCardToBeShow();
        guiObjectivesCardScript.IconShowMissedTarget();
        guiLevelEnd.SetGUILevelLostWrongTarget();
    }

    /// <summary>
    /// What to do when the player looses the level by being caught by the stalker
    /// </summary>
    public void LevelLostCaughtByStalker()
    {

        objectivesCardScript.ForceCardToBeShow();
        guiObjectivesCardScript.IconShowCaughtByStalker();
        guiLevelEnd.SetGUILevelLostCaughtByStalker();
    }

    /// <summary>
    /// What to do when the player loose the level by using the shot on anything but a person
    /// </summary>
    public void LevelLostWastedShot()
    {

        objectivesCardScript.ForceCardToBeShow();
        guiObjectivesCardScript.IconShowMissedTarget();
        guiLevelEnd.SetGUILevelLostWastedShot();
    }

    /// <summary>
    /// The objective card will register itself with us. That will be useful to when the level ends and we
    /// must force the card to be shown
    /// </summary>
    public void RegisterObjectiveCard(Transform tr, ObjectivesCard cardScript)
    {

        trObjectivesCard = tr;
        objectivesCardScript = cardScript;
    }

    /// <summary>
    /// The objective card will register itself with us. That will be useful to when the level ends and we
    /// must force the card to be shown
    /// </summary>
    public void RegisterGUIObjectivesCard(GUIObjectivesCard cardScript)
    {

        guiObjectivesCardScript = cardScript;
    }
}
