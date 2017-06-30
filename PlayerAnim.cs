using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerAnim : MonoBehaviour {

    [SerializeField]
    Animator playerAnim;

    public static PlayerAnim instance;

    bool attackAnimation = true;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool GetAttackState()
    {
        return attackAnimation;
    }

    public void SetAttackFalse()
    {
        attackAnimation = false;
    }

    public void SetAttackTrue()
    {
        attackAnimation = true;
    }

    public void SetPlayerAttackSpeed(float value)
    {
        playerAnim.SetFloat("PlayerSpeed", value);
    }

    public void AnimTrigger(string anim)
    {
        if (anim == "Attack")
        {
            if (!attackAnimation)
                return;
            SetAttackFalse();
        }
        playerAnim.SetTrigger(anim);
    }
}
