using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class CamController : MonoBehaviour
{
    public float lerpTime;
    public float xOffset;//khoang cach tieu chuan
    bool _canLerp;
    float _lerpXDist;

	private void Update()
	{
		if (_canLerp)
		{
			MoveLerp();
		}
	}
	void MoveLerp()
	{
		float xPos = transform.position.x;
		float yPos = transform.position.y;

		xPos = Mathf.Lerp(xPos, _lerpXDist, lerpTime * Time.deltaTime);
		transform.position = new Vector3(xPos,transform.position.y,transform.position.z);
		if (transform.position.x >= (_lerpXDist- xOffset)){
			_canLerp = false;
		}
	}
	public void LerpTrigger(float dist)
	{
		_canLerp = true;
		_lerpXDist = dist;
	}
}
