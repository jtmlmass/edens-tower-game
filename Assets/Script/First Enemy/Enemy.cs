using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    Transform target;
    Transform enemyTransform;
    public float speed = 3f;
    private bool facingRight;
    public float followingDistance = 1f;
    public float attackingDistance = 0.2f;
    public bool shouldAttack = false;
    public float aimingTime = 0.2f;
    public float attackTime = 1.3f;

    private Rigidbody2D _rigidbody;
    Animator _animator;


    private void Awake()
    {
        enemyTransform = this.GetComponent<Transform>();
        _animator = this.GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    void Start()
    {   
        _animator.SetBool("Idle", true);
        if (transform.localScale.x < 0f)
        {
            facingRight = false;
        }
        else if (transform.localScale.x > 0f)
        {
            facingRight = true;
        }
    }

    void Update()
    {

        target = GameObject.FindWithTag("Player").transform;
        float distance = target.transform.position.x - transform.position.x;

        if(shouldAttack == false)
        {
            moving(distance);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
            if (shouldAttack == false && collision.CompareTag("Player")) //If the enemy is not attacking and the player is in the trigger
            {
                StartCoroutine(Attack()); //Attack
            }
 
    }
    private void LateUpdate() 
    {
        _animator.SetBool("Idle", _rigidbody.velocity == Vector2.zero);
    }

    private void moving(float distance)
    {

        if (distance < 0) //If the distance is negative
        {
            distance = distance * -1f; //We make it postive
        }

        if (distance < followingDistance)
        {
            Vector3 targetHeading = target.position - transform.position;
            Vector3 targetDirection = targetHeading.normalized;

            //Rotate to look at the player

            if (facingRight == false) //If the enemy is facing left
            {
                if (target.transform.position.x > transform.position.x) //And the player is on the right side
                {
                    flip();
                }
            }
            if (facingRight == true) //If the enemy is facing right 
            {
                if (target.transform.position.x < transform.position.x) //And the player is on the left side
                {
                    flip();
                }
            }

            float horizontalVelocity = speed; 

            if (facingRight == false) //If the enemy is facing left we have to change the speed to face left
            {
                horizontalVelocity = horizontalVelocity * -1f;
            }

            _rigidbody.velocity = new Vector2(horizontalVelocity, _rigidbody.velocity.y); //The enemy moves.
        }
    }
    public void flip() //Makes the enemy face the other way 
    {
        facingRight = !facingRight;
        float localScaleX = transform.localScale.x;
        localScaleX = localScaleX * -1f;
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private IEnumerator Attack()
    {
        float speedBackup = speed;
        speed = 0f;
        shouldAttack = true;

        yield return new WaitForSeconds(aimingTime);
        _animator.SetTrigger("IsAttacking");
        yield return new WaitForSeconds(attackTime);

        shouldAttack = false;
        speed = speedBackup;
    }
}