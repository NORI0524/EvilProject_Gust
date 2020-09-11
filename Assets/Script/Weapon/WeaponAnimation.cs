using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimation : MonoBehaviour
{
    [SerializeField] Animator animator;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (!animator.GetBool("Attack"))
            {
                animator.SetBool("Attack", true);
                Debug.Log("攻撃！");
            }
        }

        // アニメーション終了時にAttackをfalseに設定
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            animator.SetBool("Attack", false);
        }

    }
}
