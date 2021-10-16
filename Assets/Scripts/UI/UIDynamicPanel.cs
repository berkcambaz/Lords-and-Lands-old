using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDynamicPanel : MonoBehaviour
{
    public static UIDynamicPanel Instance;

    public GameObject panelDynamic;
    public Text textProvinceName;
    public Button buttonBuildCapital;
    public Button buttonBuildForest;
    public Button buttonBuildHouse;
    public Button buttonBuildTower;
    public Button buttonBuildChurch;

    public void Init()
    {
        Instance = this;
    }

    public static void ShowProvince(Province _province)
    {
        Instance.textProvinceName.text = Utility.GetProvinceName(_province);
        /// TODO: Show or hide buttons according to the situation of the province

        Instance.panelDynamic.SetActive(true);
    }

    public static void HideProvince()
    {
        Instance.panelDynamic.SetActive(false);
    }
}
