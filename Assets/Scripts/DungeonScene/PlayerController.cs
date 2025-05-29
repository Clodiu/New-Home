using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements.Experimental;

//Script realizat de Craciun Claudiu-Viorel
//Script realizat de Oprea Vlad Ovidiu
public enum PlayerState
{
    WALK,
    ATTACK,
    DASHING,
    INTERACT
}

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private PlayerState currentState;
    [SerializeField]
    private float moveSpeed;


    public float health = 10f;
    public float maxHealth = 10f;
    private float regenAmount = 1;
    private float regenerationSpeed = 10f;
    private float lastRegenTime;


    public static int coins = 0;

    private float stamina = 10f;
    private float maxStamina = 10f;
    private float lastStaminaDischarge = 0f;

    [SerializeField]
    private KeyCode attackKey;
    [SerializeField]
    private KeyCode dashKey;

    [SerializeField]
    private AnimationClip attackAnimationClip;
    [SerializeField]
    private float attackAnimationModifier = 1.0f;

    // Update <---> FixedUpdate
    private Vector2 input = Vector2.zero;
    private bool performAttack = false;
    private bool performDash = false;


    // Componentele astea trebuie gasite in Start()
    private Animator animator;
    private Rigidbody2D rb;
    private Collider2D col;
    private TrailRenderer tr;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        col = gameObject.GetComponent<Collider2D>();
        tr = gameObject.GetComponent<TrailRenderer>();
    }

    // Input(keyboard, mouse etc.) in update, logica in FixedUpdate(foloseste fizica!)
    void Update()
    {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if(!performAttack) performAttack = Input.GetKeyDown(attackKey);
        if(!performDash) performDash = Input.GetKeyDown(dashKey);

        if((Time.time - lastRegenTime) >= regenerationSpeed)
        {
            regenHealth(regenAmount);
            lastRegenTime = Time.time;
        }
    }

    void FixedUpdate()
    {
        CreateMove();
    }

    private void CreateMove()
    {
        // Codul pentru miscare si atac.
        if (performAttack && currentState != PlayerState.ATTACK && currentState != PlayerState.DASHING && stamina > 1) {
            DischargeStamina();
            StartCoroutine(AttackCo());
        } else {
            // Cod pentru animatii
            if(input != Vector2.zero) {
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);
                animator.SetBool("isMoving", true);
            } else {
                animator.SetBool("isMoving", false);
            }
        }

        // Reincarcare stamina
        if(Time.realtimeSinceStartup - lastStaminaDischarge > 0.75 && stamina < maxStamina)
            stamina = Mathf.Min(stamina + (5 * Time.deltaTime), maxStamina);

        if (performDash && stamina > 1)
        {
            DischargeStamina();
            StartCoroutine(DashCo());
        }

        float movementModifier = 1.0f;
        if (currentState == PlayerState.DASHING) movementModifier = 5.0f;

        rb.MovePosition(rb.position + (input.normalized * moveSpeed * Time.fixedDeltaTime * movementModifier));

        performAttack = false;
        performDash = false;
    }

    private void DischargeStamina(float price = 1.0f)
    {
        stamina -= price;
        lastStaminaDischarge = Time.realtimeSinceStartup;
    }

    private IEnumerator AttackCo()
    {
        animator.SetBool("isAttacking", true);
        currentState = PlayerState.ATTACK;
        yield return new WaitForSeconds(attackAnimationClip.length * (1 / attackAnimationModifier));
        animator.SetBool("isAttacking", false);
        currentState = PlayerState.WALK;
    }

    private IEnumerator DashCo()
    {
        currentState = PlayerState.DASHING;
        tr.emitting = true;
        yield return new WaitForSeconds(0.2f);
        tr.emitting = false;
        currentState = PlayerState.WALK;
    }

    private IEnumerator PlayStepCo()
    {
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.1f);
    }


    public void takeDamage(float amount)
    {
        this.health = this.health - amount;
        //Debug.Log(health);
        if (this.health <= 0)
        {
            this.health = 0;
            //Debug.Log("Viata ta este 0");
            SceneManager.LoadScene(sceneName: "PlanetMenu");
        }
    }

    public void regenHealth(float amount)
    {
        if(health+amount >= maxHealth)
        {
            health = maxHealth;
        }
        else
        {
            health += amount;
        }
    }

    public void addCoin(int value)
    {
        coins += value;
        //Debug.Log("Balance:" + coins);
    }

}
