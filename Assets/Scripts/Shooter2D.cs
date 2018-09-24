using UnityEngine;
using System.Collections;

public class Shooter2D : MonoBehaviour
{
    public RunnerController runnerController;
    public Transform ShootPoint;
    public GameObject ProjectilePrefab;
    public float ShootSpeed = 5f;

    public void ShootGun()
    {
        Debug.Log("Shoot Gun!");
        GameObject projectile = GameObject.Instantiate(ProjectilePrefab);
        projectile.transform.position = ShootPoint.position;

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (runnerController.IsFacingRight)
        {
            projectile.transform.localScale = new Vector3(1f, 1f);
            rb.velocity = ShootPoint.right * ShootSpeed;
        }
        else
        {
            projectile.transform.localScale = new Vector3(-1f, 1f);
            rb.velocity = ShootPoint.right * ShootSpeed;
        }

    }
}
