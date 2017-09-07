using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookQuat : MonoBehaviour {

    float mouseSensitivity = 2.0f;
    float rotLeftRight, rotTopBottom;

    Quaternion Quat = Quaternion.identity;

    void Start () {
		Input.gyro.enabled = true;
		Quat = Input.gyro.attitude;
	}

    private void FixedUpdate()
	{
        #if UNITY_EDITOR //유니티 에디터에서는 마우스로 카메라 회전
            FPRotate();
        #elif UNITY_ANDROID //폰에서는 자이로센서로 카메라 회전
		    gyroupdate();
        #endif
    }

    //마우스 사용해서 카메라 회전
    void FPRotate()
    {
        rotLeftRight = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * mouseSensitivity;
        rotTopBottom += Input.GetAxis("Mouse Y") * mouseSensitivity;
        rotTopBottom = Mathf.Clamp(rotTopBottom, -20.0f, 60.0f);

        transform.localEulerAngles = new Vector3(-rotTopBottom, rotLeftRight, 0);
    }

    //쿼터니온과 자이로센서
    void gyroupdate()
    {
        transform.localRotation = Quaternion.Inverse(Quaternion.Inverse(Quat) * Input.gyro.attitude);
    }

    //자이로 카메라 리셋
    public void ResetBtn(){
		Quat = Input.gyro.attitude;	
	}
}
