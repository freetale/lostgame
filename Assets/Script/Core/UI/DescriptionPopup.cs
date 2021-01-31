using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class DescriptionPopup : PanelBehaviour
{
    public TMP_Text Text;

    public void SetItem(ItemInfo info)
    {
        Text.text = info.GetInfoString();
    }
}
