using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeField]
    private Transform targeting;


    [SerializeField]
    private float basicHeight;
    [SerializeField]
    private float basicDistance;
    [SerializeField]
    float followSpeed;

    Vector3 setPos;

    public void SetTarget(Transform target)
    {
        targeting = target;
    }
    

	// Update is called once per frame
	void Update () {

        if (targeting == null)
            return;
        setPos = new Vector3(targeting.position.x, targeting.position.y + basicHeight, targeting.position.z + basicDistance);
        transform.position = Vector3.Lerp(transform.position, setPos, followSpeed * Time.deltaTime);
	}
}
