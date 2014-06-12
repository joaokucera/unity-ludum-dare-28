using UnityEngine;
using System.Collections;

public enum StatusIntroControl
{
    SHOW_POPCORN,
    SHOW_PRESENTS,
    HIDE_POPCORN_AND_PRESENTS,
    SHOW_SPJAM,
    HIDE_SPJAM,
    SHOW_TITLE,
    WAITING,
    SHOW_MENU
}

public class Intro : MonoBehaviour
{
    #region Fields

    public GUISkin textSkin;
    public GUISkin titleSkin;

    public GameObject soundObject;
    public Texture2D popcornTexture;
    public Texture2D gameJamTexture;
    public Texture bgTexture;

    private StatusIntroControl control = StatusIntroControl.SHOW_POPCORN;
    private Color progressiveColor = Color.white;

    private int speedToChangeColor = 2;
    private int speedToPopcorn = 100;
    private float popcornTextureWidth = 0f;
    private float popcornTextureHeight = 0f;
    private float magicNumber = 1.319587628865979f;

    private string textPresents = "PRESENTS";
    private Vector2 textPresentsSize;
    private float textPresentsHeight = 0f;
    private int speedToTextPresents = 500;

    private string textInAssociationWith = "IN ASSOCIATION WITH";
    private Vector2 textInAssociationWithSize;
    private float textInAssociationWithHeight = 0f;
    private int speedToTextInAssociationWith = 250;

    //public GUIText titleGUIText;
    private string titleText = "ONE SHOT";
    private Vector2 titleTextSize;
    //private float titleTextHeight = 0f;
    //private int speedToTitleText = 125;

    //private Color titleColor = Color.black;

    #endregion

    #region Methods

    void Start()
    {
        Camera.main.backgroundColor = progressiveColor;

        textPresentsHeight = Screen.height;
        textInAssociationWithHeight = 0;

        //titleGUIText.color = titleColor;
        //titleGUIText.enabled = false;
    }

    void Update()
    {
        if (control == StatusIntroControl.SHOW_POPCORN)
        {
            StartCoroutine(ColorShifter());

            if (popcornTexture.width > popcornTextureWidth && popcornTexture.height > popcornTextureHeight)
            {
                popcornTextureWidth += Time.deltaTime * speedToPopcorn;
                popcornTextureHeight += Time.deltaTime * speedToPopcorn * magicNumber;
            }
            else
            {
                control = StatusIntroControl.SHOW_PRESENTS;
            }
        }
        else if (control == StatusIntroControl.SHOW_PRESENTS)
        {
            if (textPresentsHeight > (Screen.height / 1.3f - textPresentsSize.y / 2f))
            {
                textPresentsHeight -= Time.deltaTime * speedToTextPresents;
            }
            else
            {

                Invoke("HidePopcornAndPresents", 2.5f);
            }
        }
        else if (control == StatusIntroControl.HIDE_POPCORN_AND_PRESENTS)
        {
            if (popcornTextureHeight > -popcornTexture.height)
            {
                popcornTextureHeight -= Time.deltaTime * speedToTextPresents;
                textPresentsHeight += Time.deltaTime * speedToTextPresents / 2;
            }
            else
            {
                Invoke("ShowSPJAM", 0.5f);
            }
        }
        else if (control == StatusIntroControl.SHOW_SPJAM)
        {
            if (textInAssociationWithHeight < Screen.height / 2 - gameJamTexture.height / 2f)
            {
                textInAssociationWithHeight += Time.deltaTime * speedToTextInAssociationWith;
            }
            else
            {
                Invoke("HideSPJAM", 2.5f);
            }
        }
        else if (control == StatusIntroControl.HIDE_SPJAM)
        {
            if (textInAssociationWithHeight > -textInAssociationWithSize.y)
            {
                textInAssociationWithHeight -= Time.deltaTime * speedToTextInAssociationWith;
            }
            else
            {
                Invoke("ShowTitle", 1.5f);
            }
        }
        else if (control == StatusIntroControl.SHOW_TITLE)
        {
            //if (titleTextHeight > -titleTextSize.y)
            //{
            //    titleTextHeight -= Time.deltaTime * speedToTitleText;
            //}
            //else
            //{
                Invoke("ToWaiting", 1f);
            //}
            //StartCoroutine(ColorTitle());
        }
        else if (control == StatusIntroControl.WAITING)
        {
            soundObject.GetComponent<SoundManager>().control = SoundControl.OUT;
            Invoke("ShowMenu", 3.5f);
        }
        else if (control == StatusIntroControl.SHOW_MENU)
        {
            AutoFade.LoadLevel("Menu", 1.5f, 1.5f, Color.black);
        }
    }

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), bgTexture);
        //GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 

        if (control == StatusIntroControl.SHOW_POPCORN || control == StatusIntroControl.SHOW_PRESENTS || control == StatusIntroControl.HIDE_POPCORN_AND_PRESENTS)
        {
            if (control == StatusIntroControl.HIDE_POPCORN_AND_PRESENTS)
            {
                GUI.DrawTexture(new Rect(Screen.width / 2f - popcornTextureWidth / 2f, popcornTextureHeight, popcornTexture.width, popcornTexture.height), popcornTexture);
            }
            else
            {
                //Debug.Log(popcornTextureWidth + " - " + popcornTextureHeight);
                GUI.DrawTexture(new Rect(Screen.width / 2f - popcornTextureWidth / 2f, Screen.height / 2f - popcornTextureHeight / 1.5f, popcornTextureWidth, popcornTextureHeight), popcornTexture);
            }

            if (control != StatusIntroControl.SHOW_POPCORN)
            {
                GUI.skin = textSkin;

                textPresentsSize = GUI.skin.GetStyle("Label").CalcSize(new GUIContent(textPresents));
                GUI.Label(new Rect(Screen.width / 2f - textPresentsSize.x / 2f, textPresentsHeight, textPresentsSize.x, textPresentsSize.y), textPresents);
            }
        }
        else if (control == StatusIntroControl.SHOW_SPJAM || control == StatusIntroControl.HIDE_SPJAM)
        {
            GUI.DrawTexture(new Rect(Screen.width / 2f - gameJamTexture.width / 2f + Random.Range(-1f, 1f),
                                     Screen.height / 2f - gameJamTexture.height / 2.5f + Random.Range(-1f, 1f),
                                     gameJamTexture.width, gameJamTexture.height), gameJamTexture);

            GUI.skin = textSkin;

            textInAssociationWithSize = GUI.skin.GetStyle("Label").CalcSize(new GUIContent(textInAssociationWith));
            GUI.Label(new Rect(Screen.width / 2f - textInAssociationWithSize.x / 2f, textInAssociationWithHeight, textInAssociationWithSize.x, textInAssociationWithSize.y), textInAssociationWith);
        }
        else if (control == StatusIntroControl.SHOW_TITLE || control == StatusIntroControl.WAITING)
        {
            GUI.skin = titleSkin;

            titleTextSize = GUI.skin.GetStyle("Label").CalcSize(new GUIContent(titleText));
            GUI.Label(new Rect(Screen.width / 2f - titleTextSize.x / 2f, Screen.height / 2f - titleTextSize.y / 2f, titleTextSize.x, titleTextSize.y), titleText);
        }

        //Debug.Log(control);
    }

    void HidePopcornAndPresents()
    {
        popcornTextureHeight = Screen.height / 2f - popcornTextureHeight / 1.5f;

        control = StatusIntroControl.HIDE_POPCORN_AND_PRESENTS;
        CancelInvoke("HidePopcornAndPresents");
    }

    void ShowSPJAM()
    {
        control = StatusIntroControl.SHOW_SPJAM;
        CancelInvoke("ShowSPJAM");
    }

    void HideSPJAM()
    {
        control = StatusIntroControl.HIDE_SPJAM;
        CancelInvoke("HideSPJAM");
    }

    void ShowMenu()
    {
        control = StatusIntroControl.SHOW_MENU;
        CancelInvoke("ShowMenu");
    }

    void ShowTitle()
    {
        //titleGUIText.enabled = true;
        control = StatusIntroControl.SHOW_TITLE;
        CancelInvoke("ShowTitle");
    }

    void ToWaiting()
    {
        control = StatusIntroControl.WAITING;
        CancelInvoke("ToWaiting");
    }

    //IEnumerator ColorTitle()
    //{
    //    while (true)
    //    {
    //        titleColor.r += Time.deltaTime / speedToChangeColor;
    //        titleColor.r = Mathf.Clamp(titleColor.r, 0, 255);
    //        titleColor.g += Time.deltaTime / speedToChangeColor;
    //        titleColor.g = Mathf.Clamp(titleColor.g, 0, 255);
    //        titleColor.b += Time.deltaTime / speedToChangeColor;
    //        titleColor.b = Mathf.Clamp(titleColor.b, 0, 255);
    //        titleColor.a = 1f;

    //        float t = 0f;
    //        var currentColor = titleGUIText.color;
    //        while (t < 1.0)
    //        {
    //            titleGUIText.color = Color.Lerp(currentColor, titleColor, t);
    //            yield return null;
    //            t += Time.deltaTime;
    //        }

    //        if (titleGUIText.color == Color.white)
    //        {
    //            control = StatusIntroControl.WAITING;
    //            break;
    //        }
    //    }
    //}

    IEnumerator ColorShifter()
    {
        while (true)
        {
            progressiveColor.r -= Time.deltaTime / speedToChangeColor;
            progressiveColor.r = Mathf.Clamp(progressiveColor.r, 0, 255);
            progressiveColor.g -= Time.deltaTime / speedToChangeColor;
            progressiveColor.g = Mathf.Clamp(progressiveColor.g, 0, 255);
            progressiveColor.b -= Time.deltaTime / speedToChangeColor;
            progressiveColor.b = Mathf.Clamp(progressiveColor.b, 0, 255);
            progressiveColor.a = 1f;

            float t = 0f;
            var currentColor = Camera.main.backgroundColor;
            while (t < 1.0)
            {
                Camera.main.backgroundColor = Color.Lerp(currentColor, progressiveColor, t);
                yield return null;
                t += Time.deltaTime;
            }

            if (Camera.main.backgroundColor == Color.black)
            {
                break;
            }
        }
    }

    #endregion
}