using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndDayUI : MonoBehaviour
{
    public TMP_Text TotalCustomerText;
    public TMP_Text ImposterCaptureText;
    public TMP_Text ImposterGetAways;
    public TMP_Text InnocentCapture;
    public TMP_Text CustomerStilMissing;
    public TMP_Text Satisfaction;

    public Button NextDayButton;


    public void SetInfomation(DailyScore score)
    {
        TotalCustomerText.text = score.TotalCustomer.ToString();
        ImposterCaptureText.text = score.ImposterCapture.ToString();
        ImposterGetAways.text = score.ImposterGetAway.ToString();
        InnocentCapture.text = score.InnocentGetAways.ToString();
        CustomerStilMissing.text = score.CustomerStilMissing.ToString();
        var satisfaction = (int)((float)score.Satisfaction / score.MaxSatisfaction * 100);
        Satisfaction.text = satisfaction.ToString() + "%";
    }

}
