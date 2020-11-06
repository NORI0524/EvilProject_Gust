using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStopSlowParticle : MonoBehaviour
{
	//　通常時のパーティクルスピード
	private float normalParticleSpeed;
	//　遅くした時のパーティクルスピード
	[SerializeField]
	private float slowParticleSpeed = 0.1f;
	//　時間を遅くしている時間
	[SerializeField]
	private float slowTime = 1f;
	//　経過時間
	private float elapsedTime = 0f;
	//　時間を遅くしているかどうか
	private bool isSlowDown = false;
	private ParticleSystem[] particles;

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
		particles = GetComponentsInChildren<ParticleSystem>();
		//　とりあえず最初のパーティクルシステムのノーマルスピードを取得
		normalParticleSpeed = particles[0].playbackSpeed;
		//　パーティクルプレハブに使用しているパーティクルシステムの全部のスピードを変更
		foreach (var particle in particles)
		{
			particle.playbackSpeed = slowParticleSpeed;
		}
		isSlowDown = true;
	}
	//　時間を元に戻す処理
	public void SetNormalTime()
	{
		foreach (var particle in particles)
		{
			particle.playbackSpeed = normalParticleSpeed;
		}
		isSlowDown = false;
	}
	//　時間を遅くしているかどうかを返す
	public bool IsSlowDown()
	{
		return isSlowDown;
	}
}
