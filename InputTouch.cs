using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using AnimationEnum;

public class InputTouch : MonoBehaviour , IPointerDownHandler, IPointerUpHandler, IDragHandler{

    

    public static InputTouch instance;

    [SerializeField]
    RectTransform joyStickBackRect;
    [SerializeField]
    RectTransform joyStickRect;
    [SerializeField]
    Animator normalAnim;
    [SerializeField]
    PlayerAnimation playerAnim;


    private bool isClick = false;

    Vector2 origin, buttonDis;

    private float degree;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            origin = joyStickRect.position;
        }
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //if (Define.timeScale == 0)
            //    Define.timeScale = 1;
            //else if (Define.timeScale == 1)
            //    Define.timeScale = 0;
            if (Time.timeScale == 1)
                Time.timeScale = 0;
            else if (Time.timeScale == 0)
                Time.timeScale = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SoundManager.instance.PlayBGM("field01");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
            SoundManager.instance.PlaySound("Paper");

        if (Input.GetKeyDown(KeyCode.Alpha3))
            UIPlay.instance.SetBGMToggle(false);


    }

    public float GetControllerDegree()
    {
        return degree;
    }

    public bool GetControllerTouch()
    {
        return isClick;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Time.timeScale == 0)
            return;

        if (isClick)
            return;

            buttonDis = eventData.position - origin;
            isClick = true;
            joyStickRect.position = eventData.position;


        if(playerAnim.GetAnimMoveState())
            playerAnim.PlayAnimation(AnimationIndex.Run);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Time.timeScale == 0)
            return;

        joyStickRect.position = eventData.position;

        Vector2 dir = (Vector2)joyStickRect.position - origin;
        degree = (Mathf.Atan2(dir.x,dir.y) * Mathf.Rad2Deg + 360f) % 360f;

        float distance = Vector3.Distance(joyStickRect.position, joyStickBackRect.position);
        if (distance > joyStickBackRect.rect.width * 0.5f)
            joyStickRect.position = joyStickBackRect.position + (joyStickRect.position - joyStickBackRect.position).normalized * joyStickBackRect.rect.width * 0.5f;

    }



    public void OnPointerUp(PointerEventData eventData)
    {
        if (Time.timeScale == 0)
            return;

        joyStickRect.position = origin;
        isClick = false;

        if(playerAnim.GetAnimMoveState())
            playerAnim.SetIdle();
    }

    public void NormAtkBtnAnim(string anim)
    {
        if (Time.timeScale == 0)
            return;

        normalAnim.SetTrigger(anim);
        SoundManager.instance.PlaySound("Paper");

        if (anim == "Up")
        playerAnim.PlayAnimation(AnimationIndex.Attack);
    }

    public void UseSkill(string type)
    {
        if (Time.timeScale == 0)
            return;

        if (DelayCheck.instance.GetDelayState(type))
        {

            if (type == "SingleSkill")
            {
                if (playerAnim.GetAnimSkillState())
                    return;   
                playerAnim.PlayAnimation(AnimationIndex.Skill2);
            }
            else if (type == "RangeSkill")
            {
                if (playerAnim.GetAnimSkillState())
                    return;
                playerAnim.PlayAnimation(AnimationIndex.Skill1);
            }
            else if (type == "BuffSkill")
            {
                if (playerAnim.GetAnimSkillState())
                    return;
                playerAnim.PlayAnimation(AnimationIndex.Buff);
            }
            else if (type == "Potions")
            {

            }

            DelayCheck.instance.SetDelayState(type, false);
        }

        
    }

    public void OptionBtn()
    {
        UIPlay.instance.SetPanelActive();
    }

}
