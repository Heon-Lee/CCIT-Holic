using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlay : MonoBehaviour {

    public static UIPlay instance;


    [SerializeField]
    Image singleSkillDelayImgae;
    [SerializeField]
    Image rangeSkillDelayImage;
    [SerializeField]
    Image buffSkillDelayImage;
    [SerializeField]
    Image potionsDelayImage;


    [SerializeField]
    GameObject optionPanel;

    [SerializeField]
    GameObject soundOn;
    [SerializeField]
    GameObject soundOff;
    [SerializeField]
    GameObject bgmOn;
    [SerializeField]
    GameObject bgmOff;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void SetDelayFilled(float now , float max, string type)
    {
        if (now / max < 0)
            now = 0;
        if (now > max)
            now = max;

        if (type == "SingleSkill")
        {
            singleSkillDelayImgae.fillAmount = now / max;
        }
        if (type == "RangeSkill")
        {
            rangeSkillDelayImage.fillAmount = now / max;
        }
        if (type == "BuffSkill")
        {
            buffSkillDelayImage.fillAmount = now / max;
        }
        if (type == "Potions")
        {
            potionsDelayImage.fillAmount = now / max;
        }
    }

    public void SetPanelActive()
    {
        if (optionPanel.activeSelf)
            Time.timeScale = 1;
        else
            Time.timeScale = 0;
        optionPanel.SetActive(!optionPanel.activeSelf);

    }

    public void SetBGMToggle(bool set)
    {
        if(set)
        {
            bgmOn.SetActive(true);
            bgmOff.SetActive(false);
        }
        else
        {
            bgmOn.SetActive(false);
            bgmOff.SetActive(true);
        }
    }

    public void SetSoundToggle(bool set)
    {
        if(set)
        {
            soundOn.SetActive(true);
            soundOff.SetActive(false);
        }
        else
        {
            soundOn.SetActive(false);
            soundOff.SetActive(true);
        }
    }
}
