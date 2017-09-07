using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookEuler : MonoBehaviour {

    float mouseSensitivity = 2.0f;
    float rotLeftRight, rotTopBottom;

    Vector3 m_PlayerRot;

	RaycastHit temp;

	void Start()
	{
		Input.gyro.enabled = true;
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

    //오일러와 자이로 센서
    void gyroupdate()
	{
		m_PlayerRot.x -= Input.gyro.rotationRate.x;
		m_PlayerRot.y += Input.gyro.rotationRate.y * -1f;

		if (m_PlayerRot.x >= 360f)
			m_PlayerRot.x = 0f;

		if (m_PlayerRot.y >= 360f)
			m_PlayerRot.y = 0f;

		transform.eulerAngles = m_PlayerRot;
	}

	//자이로 카메라 리셋
	public void AngleReset()
	{
		m_PlayerRot = Vector3.zero;
    }
}
