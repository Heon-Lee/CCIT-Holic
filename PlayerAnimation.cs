using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AnimationEnum;

namespace AnimationEnum
{
    public enum AnimationIndex
    {
        Attack = 34,
        Buff = 33,
        Idle = 21,
        Die = 23,
        Hit = 22,
        Run = 19,
        Skill1 = 18,
        Skill2 = 17,
        Walk = 0
    }

    public enum PlayerMoveState
    {
        Move = 0,
        Idle = 1,
        Attack = 2,
        Die = 4,
        Hit = 8,
        Run = 16,
        Skill1 = 32,
        SKill2 = 64,
        Walk = 128,
        Calm = 110
        //Calm 01101110
    }
}


public class PlayerAnimation : MonoBehaviour {

    Animation playerAnimation;

    AnimationIndex oldIndex;
    AnimationIndex nextIndex;

    PlayerMoveState moveState;

    bool skillUse;

    private void Awake()
    {
        playerAnimation = GetComponent<Animation>();
    }

    void Start () {
        //Test();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha0))
            PlayAnimation(AnimationIndex.Attack);
	}


    public bool GetAnimMoveState()
    {
        if ((moveState & PlayerMoveState.Calm) == PlayerMoveState.Move)
            return true;
        else
            return false;
    }

    public bool GetAnimSkillState()
    {
        return skillUse;
    }

    public void PlayAnimation(AnimationIndex animIndex)
    {
        if (skillUse)
            return;

        if (animIndex != AnimationIndex.Run)
            if (oldIndex == AnimationIndex.Run)
                nextIndex = oldIndex;
        oldIndex = animIndex;

        AnimationIndex testIndex = animIndex;
        int index = 0;

        foreach(AnimationState state in playerAnimation)
        {
            if (index == (int)testIndex)
                playerAnimation.clip = state.clip;
            index++;
        }

        switch(animIndex)
        {
            case AnimationIndex.Attack:
                moveState = PlayerMoveState.Attack;
                break;
            case AnimationIndex.Buff:
                skillUse = true;
                moveState = PlayerMoveState.Attack;
                break;
            case AnimationIndex.Die:
                moveState = PlayerMoveState.Die;
                break;
            case AnimationIndex.Hit:
                moveState = PlayerMoveState.Hit;
                break;
            case AnimationIndex.Idle:
                moveState = PlayerMoveState.Idle;
                break;
            case AnimationIndex.Run:
                moveState = PlayerMoveState.Run;
                break;
            case AnimationIndex.Skill1:
                skillUse = true;
                moveState = PlayerMoveState.Skill1;
                break;
            case AnimationIndex.Skill2:
                skillUse = true;
                moveState = PlayerMoveState.SKill2;
                break;
            case AnimationIndex.Walk:
                moveState = PlayerMoveState.Walk;
                break;
        }


        playerAnimation.Play();
        StartCoroutine(CheckAnimationEnd());
    } 

    public void SetIdle()
    {
        if (nextIndex == AnimationIndex.Run && InputTouch.instance.GetControllerTouch())
        {
            nextIndex = AnimationIndex.Idle;
            PlayAnimation(AnimationIndex.Run);
            return;
        }

        AnimationIndex testIndex = AnimationIndex.Idle;
        int index = 0;

        foreach (AnimationState state in playerAnimation)
        {
            
            if (index == (int)testIndex)
                playerAnimation.clip = state.clip;
            index++;
        }

        
        moveState = PlayerMoveState.Idle;
        playerAnimation.Play();
    }

    IEnumerator CheckAnimationEnd()
    {
        while(true)
        {
            if (!playerAnimation.isPlaying)
            {
                if (oldIndex == AnimationIndex.Buff || oldIndex == AnimationIndex.Skill1 || oldIndex == AnimationIndex.Skill2)
                    skillUse = false;

                SetIdle();
                break;
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
