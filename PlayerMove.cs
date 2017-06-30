using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    Rigidbody myRigi;
    [SerializeField]
    float playerMoveSpeed;

    [SerializeField]
    PlayerAnimation playerAnimation;

    private void Awake()
    {
        myRigi = GetComponent<Rigidbody>();
    }

    // Use this for initialization
    void Start () {

        StartCoroutine(PlayerMoveCo());
	}



    IEnumerator PlayerMoveCo()
    {
        while(true)
        {

            //if(InputTouch.instance.GetControllerTouch() && PlayerAnim.instance.GetAttackState())
            if (InputTouch.instance.GetControllerTouch() && playerAnimation.GetAnimMoveState())
            {
                if(Define.timeScale == 1)
                    transform.eulerAngles = new Vector3(0, InputTouch.instance.GetControllerDegree(), 0);

                myRigi.MovePosition(transform.position + transform.forward.normalized * playerMoveSpeed * Time.deltaTime * Define.timeScale);
            }

            yield return new WaitForFixedUpdate();
        }
    }
}
