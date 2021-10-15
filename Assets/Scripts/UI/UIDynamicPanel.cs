using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDynamicPanel {
    public static void ShowProvince(Province _province)
    {
        UIManager.Instance.textProvinceName.text = Utility.GetProvinceName(_province);
        /// TODO: Show or hide buttons according to the situation of the province

        UIManager.Instance.panelDynamic.SetActive(true);
    }

    public static void HideProvince()
    {
        UIManager.Instance.panelDynamic.SetActive(false);
    }
}
