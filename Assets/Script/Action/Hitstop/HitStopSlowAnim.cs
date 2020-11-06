using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStopSlowAnim : MonoBehaviour
{
	//　通常時のアニメーションスピード
	private float normalAnimSpeed;

	//　遅くした時のアニメーションスピード
	[SerializeField]
	private float slowAnimSpeed = 0.1f;

	//　時間を遅くしている時間
	[SerializeField]
	private float slowTime = 1f;

	//　経過時間
	private float elapsedTime = 0f;

	//　時間を遅くしているかどうか
	private bool isSlowDown = false;

	private Animator animator;

	void Start()
	{
		animator = GetComponent<Animator>();
		normalAnimSpeed = animator.speed;
	}

	void Update()
	{
		//　スローの時だけ実行
		if (isSlowDown)
		{
			elapsedTime += Time.unscaledDeltaTime;
			if (elapsedTime >= slowTime)
			{
				SetNormalTime();
			}
		}
	}
	//　時間を遅くする処理
	public void SlowDown()
	{
		elapsedTime = 0f;
		animator.speed = slowAnimSpeed;
		isSlowDown = true;
	}
	//　時間を元に戻す処理
	public void SetNormalTime()
	{
		animator.speed = normalAnimSpeed;
		isSlowDown = false;
	}
	//　時間を遅くしているかどうかを返す
	public bool IsSlowDown()
	{
		return isSlowDown;
	}
}
