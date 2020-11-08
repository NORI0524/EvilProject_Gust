using UnityEngine;
using System.Collections;

/// <summary>
/// ダメージのコンポーネント
/// （攻撃中の時だけこのコンポーネントを有効にすることを推奨）
/// ※攻撃していないときにコライダーが接触すると勝手にダメージ処理されるため
/// </summary>
public class DamageComponent : BaseStatusComponent
{
    [SerializeField] private int damageValue = 10;
    //[SerializeField, Range(0.0f, 1.0f)] private float criticalRate = 0.0f;
    [SerializeField] private bool isHitStop = false;

    // Use this for initialization
    void Start()
    {
        currentValue = damageValue;
    }

    public void ChangeDamageHalf()
    {
        MultiplyValue(0.5f);
    }
    public void ChangeDamageDouble()
    {
        MultiplyValue(2.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        //ダメージ増減
        var targetHp = other.GetComponent<HpComponent>();
        if (targetHp == null) return;
        targetHp.AddDamage(currentValue);

        if (isHitStop == false) return;

        //ヒットストップ
        var hitStop = other.GetComponent<HitStopSlowAnim>();
        if (hitStop == null) return;
        if (hitStop.IsSlowDown() == false)
            hitStop.SlowDown();
    }
}