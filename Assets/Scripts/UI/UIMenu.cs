using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMenu : MonoBehaviour 
{
    public static UIMenu Instance;

    public Button buttonNew;
    public Button buttonLoad;
    public Button buttonSettings;
    public Button buttonCredits;
    public Button buttonQuit;

    public void Init()
    {
        Instance = this;
    }
}
