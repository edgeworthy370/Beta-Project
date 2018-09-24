using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider2 : MonoBehaviour
{
    public string TargetTag = "Player";
    public float DamageValue = 25f;
    public float KnockbackForce = 10f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(TargetTag));
        {
            Debug.Log(collision.gameObject);
            RunnerController rc = collision.gameObject.GetComponent <RunnerController>();
            if (rc !=null)
            {
                rc.TakeDamage(DamageValue);

                Vector2 direction;

                if(transform.position.x < rc.transform.position.x)
                {
                    direction = Vector2.right + Vector2.up;
                }
                else
                {
                    direction = Vector2.left + Vector2.up;
                }
                
                Vector2 forceVector = direction * KnockbackForce;

                rc.IsKnockBack(forceVector);
            }
        }
        
    }



}
