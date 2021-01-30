using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class DescriptionPopup : PanelBehaviour
{
    public TMP_Text Text;

    public void SetDescription(ItemInfo info)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine(info.Name);
        sb.AppendFormat("Found: {0} {1}", info.Room, info.Location);
        sb.AppendLine();
        bool firstProp = true;
        foreach (var item in info.Property)
        {
            if (firstProp)
            {
                sb.Append(",");
                firstProp = false;
            }
            sb.AppendFormat("{0}, {1}", item.Key, item.Value);
        }
    }
}
