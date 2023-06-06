using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
	private void Update()
	{
		if (GameManager.Ins.cam)
		{
			transform.position = new Vector3(
				GameManager.Ins.cam.transform.position.x,
				GameManager.Ins.cam.transform.position.y, 0f);
		}
	}
}
