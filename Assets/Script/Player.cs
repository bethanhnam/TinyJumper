using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
	public Vector2 jumpForce;
	public Vector2 jumpForeceUp;
	public float minForceX;
	public float maxForceX;
	public float minForceY;
	public float maxForceY;

	[HideInInspector]
	public int lastPlatformId;

	bool _DidJump;
	bool _PowerSetted;

	Rigidbody2D rb;
	Animator animator;

	float _curPowerBarVal = 0;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
	}
	private void Update()
	{
		if (GameManager.Ins.IsGameStarted)
		{
			setPower();
			if (Input.GetMouseButtonDown(0))
			{
				setPower(true);

			}
			if (Input.GetMouseButtonUp(0))
			{
				setPower(false);
			}
		}
	}
	private void setPower()
	{
		if (_PowerSetted && !_DidJump)
		{
			jumpForce.x += jumpForeceUp.x * Time.deltaTime;
			jumpForce.y += jumpForeceUp.y * Time.deltaTime;

			jumpForce.x = Mathf.Clamp(jumpForce.x, minForceX, maxForceX);
			jumpForce.y = Mathf.Clamp(jumpForce.y, minForceY, maxForceY);

			_curPowerBarVal += GameManager.Ins.powerBarUp * Time.deltaTime;
			GameGUIManager.Ins.UpdatePowerBar(_curPowerBarVal, 1);
		}
	}
	public void setPower(bool isHoldingMouse)
	{
		_PowerSetted = isHoldingMouse;
		if (!_PowerSetted && !_DidJump)
		{
			jump();
		}
	}
	void jump()
	{
		if (!rb || jumpForce.x <= 0 || jumpForce.y <= 0)
			return;
		rb.velocity = jumpForce;
		_DidJump = true;
		if (animator)
		{
			animator.SetBool("DidJump", true);
		}
		AudioController.Ins.PlaySound(AudioController.Ins.jump);
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag(TagConsts.GROUND))
		{
			Platform p = collision.transform.root.GetComponent<Platform>();

			if (_DidJump)
			{
				_DidJump = false;
				if (animator != null)
				{
					animator.SetBool("DidJump", false);

				}
				if (rb != null)
				{
					rb.velocity = Vector2.zero;
				}
				jumpForce = Vector2.zero;

				_curPowerBarVal = 0;
				GameGUIManager.Ins.UpdatePowerBar(_curPowerBarVal, 1);
			}
			if (p && p.id != lastPlatformId)
			{
				GameManager.Ins.CreatePlatformAndLerp(transform.position.x);
				lastPlatformId = p.id;
				GameManager.Ins.AddScore();
			}
		}
		if (collision.CompareTag(TagConsts.DEADZONE))
		{
			GameGUIManager.Ins.ShowOverDialog();
			AudioController.Ins.PlaySound(AudioController.Ins.gameover);
		Destroy(gameObject);
		}
	}
}
