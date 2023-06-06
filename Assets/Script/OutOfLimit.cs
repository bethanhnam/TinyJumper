using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfLimit : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag(TagConsts.PLATFORM))
		{
			Destroy(collision.gameObject);
		}
	}
}
