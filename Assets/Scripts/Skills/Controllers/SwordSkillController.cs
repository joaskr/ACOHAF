using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSkillController : MonoBehaviour
{
    [SerializeField] private float returnSpeed = 12;
    private Animator anim;
    private Rigidbody2D rb;
    private CircleCollider2D cd;
    private Player player;
    private bool canRotate = true;
    private bool isReturning;
    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<CircleCollider2D>();
    }

    public void SetupSword(Vector2 _dir, float _gravityScale, Player _player)
    {
        player = _player;
        rb.velocity = _dir;
        rb.gravityScale = _gravityScale;
        anim.SetBool("Rotation", true);
    }

    public void ReturnSword()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        //rb.isKinematic = false;
        transform.parent = null;
        isReturning = true;
    }

    private void Update()
    {
        if(canRotate)
        {
            transform.right = rb.velocity;
        }
        if(isReturning)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, returnSpeed * Time.deltaTime);
            if(Vector2.Distance(transform.position, player.transform.position) < 1)
            {
                player.ClearSword();
            }
        }
    }

    /* private void OnTriggerEnter2D(Collider2D collision)
     {
         if (isReturning)
             return;

         anim.SetBool("Rotation", false);
         canRotate = false;
         cd.enabled = false;
         rb.isKinematic = true;
         rb.constraints = RigidbodyConstraints2D.FreezeAll;
         transform.parent = collision.transform;
     }*/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isReturning)
            return;


        if (collision.GetComponent<Enemy>() != null)
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            SwordSkillDamage(enemy);
            StuckInto(collision);

        }

    }

    private void SwordSkillDamage(Enemy enemy)
    {
        EnemyStats enemyStats = enemy.GetComponent<EnemyStats>();

        player.stats.DoDamage(enemyStats);
    }

    private void StuckInto(Collider2D collision)
    {

        canRotate = false;
        cd.enabled = false;

        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;



        anim.SetBool("Rotation", false);
        transform.parent = collision.transform;
    }
}
