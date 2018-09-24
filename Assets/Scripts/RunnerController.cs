using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RunnerController : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public Animator myAnimator;
    public float speed = 2f;
    public float jumpForce = 4f;
    public bool IsFacingRight = true;
    public bool IsGrounded = false;
    public bool ArialControl = true;

    [Header("Shooting")]
    public Transform ShootPoint;
    public GameObject ProjectilePrefab;
    public float ShootSpeed = 5f;

    [Header("Health")]
    public float CurrentHealth = 100f;
    public float MaxHealth = 100f;
    public bool IsDead = false;

    [Header("Misc")]
    public bool IsKnockedBack = false;

    // Update is called once per frame
    void Update()
    {
        if (IsDead) return;
        if (IsKnockedBack) return;

        // STEP 1: MOVING
        var h = Input.GetAxis("Horizontal");
        if (IsGrounded || ArialControl)
        {
            myRigidbody.velocity = new Vector2(speed * h, myRigidbody.velocity.y);
        }

        // STEP 2: ANIMATING
        myAnimator.SetFloat("Speed", Mathf.Abs(h));

        // STEP 3: FLIPPING
        if (IsFacingRight)
        {
            if (h < 0)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
                IsFacingRight = false;
            }
        }
        else
        {
            if (h > 0)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
                IsFacingRight = true;
            }
        }

        // Step 4: JUMPING
        if (IsGrounded)
        { 
            if (Input.GetButtonDown("Jump"))
            {
                myRigidbody.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
                //timesJumped = timesJumped + 1;
            }
        }
        //Step 5: SHOOTING
        if (Input.GetButtonDown("Fire1"))
        {
            myAnimator.SetTrigger("Shoot");
        }

        // DAMAGE TESTING
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TakeDamage(10f);
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Ground")
        {
            IsGrounded = true;
            myAnimator.SetBool("IsGrounded", true);
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.tag == "Ground")
        {
            IsGrounded = false;
            myAnimator.SetBool("IsGrounded", false);
        }
    }

    public void ShootGun()
    {
        Debug.Log("Shoot Gun!");
        GameObject projectile = GameObject.Instantiate(ProjectilePrefab);
        projectile.transform.position = ShootPoint.position;

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (IsFacingRight)
        {
            projectile.transform.localScale = new Vector3(1f, 1f);
            rb.velocity = ShootPoint.right * ShootSpeed;
        }
        else
        {
            projectile.transform.localScale = new Vector3(-1f, 1f);
            rb.velocity = -ShootPoint.right * ShootSpeed;
        }
        Destroy(projectile, 2f);
    }

    //HEALTH SECTION
    public void TakeDamage(float damage)
    {
        if (IsDead) return; //return means "stop"

        Debug.Log("Taking Damage: " + damage);
        CurrentHealth -= damage;
        myAnimator.Play("TakeDamage");
        if (CurrentHealth <=0)
        {
            CurrentHealth = 0f;
            Die();
        }
    }
    public void Die()
    {
        Debug.Log("Dead!");
        CurrentHealth = 0f;
        IsDead = true;
        myAnimator.Play("Death");

        Invoke("ResetScene", 1f);
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void IsKnockBack(Vector2 force)
    {
        myRigidbody.AddForce(force, ForceMode2D.Impulse);
        IsKnockedBack = true;
        myAnimator.SetFloat("Speed", 0f);
        Invoke("FinishKnockBack", 0.5f);
    }

    public void FinishKnockBack()
    {
        IsKnockedBack = false;
    }
}
