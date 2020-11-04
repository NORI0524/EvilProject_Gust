using UnityEngine;

public class Hit : MonoBehaviour
{
    [Header("エフェクト")]
    [SerializeField] private GameObject damagePrefab = null;
    HitStopSlowAnim slowAnim = null;
    void Start()
    {

    }

    void Update()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") == false) return;

        if (damagePrefab != null)
        {
            Instantiate(damagePrefab, other.transform.position, Quaternion.identity);
        }

        slowAnim = other.GetComponent<HitStopSlowAnim>();
        if (slowAnim == null) return;

        if (slowAnim.IsSlowDown() == false)
        {
            slowAnim.SlowDown();
        }
    }
}
