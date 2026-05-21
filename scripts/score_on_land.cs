using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class score_on_land : MonoBehaviour
{
    [SerializeField] private lander lander;
    [SerializeField] private TMPro.TextMeshProUGUI scoretext;
    [SerializeField] private TMPro.TextMeshProUGUI landing_type;
    [SerializeField] private on_completion_controller controller;
    [SerializeField] private statsui statsui;
    [SerializeField] private TMPro.TextMeshProUGUI nextbutton_text;
    [SerializeField] private gamemanagement gamemanagement;
    public Button nextbutton;
    private bool hadupdated = false;
    private Action nextbuttonClickAction;
    private void Awake()
    {
        controller.hide();
        nextbutton.gameObject.SetActive(false);
        nextbutton.onClick.AddListener(() =>
        {
            nextbuttonClickAction();
        });
    }

    private void Update()
    {
        if (lander.getgameover() && !hadupdated)
        {
            controller.show();
            UpdateLandingType();
            Updatescoretext();
            statsui.hide();
            nextbutton.gameObject.SetActive(true);
            nextbuttontext();
        }
    }
    private void nextbuttontext()
    {
        if(lander.landed())
        {
            nextbutton_text.text = "Next Level";
            nextbuttonClickAction =gamemanagement.loadnextlevel;
            
        }
        else
        {
            nextbutton_text.text = "Retry";
            nextbuttonClickAction = gamemanagement.reloadlevel;
        }
    }

    private void Updatescoretext()
    {
        scoretext.text =  lander.getscore().ToString() + "\n"
                        + lander.gettime().ToString("F2") + "\n"
                        + lander.coin().ToString() + "\n"
                        + Mathf.Sqrt(lander.xvelocity() * lander.xvelocity() + lander.yvelocity() * lander.yvelocity()).ToString("F2");
        hadupdated = true;
    }
    private void UpdateLandingType()
    {
            if(lander.getgameover() && lander.getLandedAt() == "terrain")
             {
                landing_type.color = Color.red;
                landing_type.text = "Landed on terrain!";
             }
            else if(lander.getgameover() && lander.getLandedAt() == "High velocity")
            {
                landing_type.color = Color.red;
                landing_type.text = "Landed at high velocity!";
            }
            else if(lander.getgameover() && lander.getLandedAt() == "Steep Angle")
            {
                landing_type.color = Color.red;
                landing_type.text = "Landed at steep angle!";
            }
            else if (lander.landed())
            {
                landing_type.color = Color.green;
                landing_type.text = "Landed Successfully!";
            }
            
            
    }


}
