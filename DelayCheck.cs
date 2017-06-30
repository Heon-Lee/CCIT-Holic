using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayCheck : MonoBehaviour {


    public static DelayCheck instance;

    enum DelayType
    {
        SingleSkill = 0,
        RangeSkill = 1,
        BuffSkill = 2,
        Potions = 3
    }


    bool singleSKill;
    bool rangeSkill;
    bool buffSkill;
    bool potions;

    struct DelayCheckSet
    {
        public float time;
        public float maxTime;
        public DelayType type;
        public string typeStr;
    }

    List<DelayCheckSet> delayCheckList = new List<DelayCheckSet>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            singleSKill = true;
            rangeSkill = true;
            buffSkill = true;
            potions = true;

        }
    }

    // Use this for initialization
    void Start () {
        StartCoroutine(DelayCheckCo());
	}
	
    public bool GetDelayState(string type)
    {
        if (type == "SingleSkill")
            return singleSKill;
        if (type == "RangeSkill")
            return rangeSkill;
        if (type == "BuffSkill")
            return buffSkill;
        if (type == "Potions")
            return potions;

        return false;
    }


    public void SetDelayState(string type, bool state)
    {
        if (type == "SingleSkill")
        {

            if (!state)
            {
                singleSKill = false;
                AddDelay(type);
            }
            else
            {
                singleSKill = true;
            }
        }
        if(type == "RangeSkill")
        {
            if (!state)
            {

                rangeSkill = false;
                AddDelay(type);

            }
            else
                rangeSkill = true;
        }
        if (type == "BuffSkill")
        {
            if (!state)
            {
                buffSkill = false;
                AddDelay(type);
            }

            else
                buffSkill = true;
        }
        if (type == "Potions")
        {

            if (!state)
            {
                potions = false;
                AddDelay(type);
            }
            else
                potions = true;
        }
    }

    public void AddDelay(string type)
    {
        DelayCheckSet newDelay = new DelayCheckSet();
        if (type == "SingleSkill")
        {
            newDelay.time = 3f;
            newDelay.type = DelayType.SingleSkill;
        }
        else if (type == "RangeSkill")
        {
            newDelay.time = 10f;
            newDelay.type = DelayType.RangeSkill;
        }
        else if (type == "BuffSkill")
        {
            newDelay.time = 20f;
            newDelay.type = DelayType.BuffSkill;
        }
        else if (type == "Potions")
        {
            newDelay.time = 7f;
            newDelay.type = DelayType.Potions;
        }

        newDelay.maxTime = newDelay.time;
        newDelay.typeStr = type;

        delayCheckList.Add(newDelay);
    }



    IEnumerator DelayCheckCo()
    {
        while (true)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            for(int i =0; i<delayCheckList.Count;i++)
            {
                DelayCheckSet delay = delayCheckList[i];
                delay.time -= Time.deltaTime;
                delayCheckList[i] = delay;
                UIPlay.instance.SetDelayFilled(delayCheckList[i].time, delayCheckList[i].maxTime, delayCheckList[i].typeStr);
            }

            for (int i = 0; i < delayCheckList.Count; i++)
                if (delayCheckList[i].time < 0)
                {
                    SetDelayState(delayCheckList[i].typeStr, true);
                    delayCheckList.Remove(delayCheckList[i]);
                }
        }
    }
}
