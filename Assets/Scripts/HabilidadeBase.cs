using UnityEngine;
using UnityEngine.InputSystem;

public abstract class HabilidadeBase : MonoBehaviour
{
    [SerializeField] protected float cooldown = 5f;
    [SerializeField] protected InputAction teclaHabilidade;

    protected Animator animator;
    protected float timerCooldown = 0f;
    protected bool emCooldown = false;
    public float CooldownAtual => timerCooldown;
    public float CooldownTotal => cooldown;
    public bool EmCooldown => emCooldown;

    protected virtual void OnEnable() => teclaHabilidade.Enable();
    protected virtual void OnDisable() => teclaHabilidade.Disable();

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        if (emCooldown)
        {
            timerCooldown -= Time.deltaTime;
            if (timerCooldown <= 0f)
                emCooldown = false;
        }

        if (teclaHabilidade.WasPressedThisFrame() && !emCooldown)
        {
            UsarHabilidade();
            animator.SetTrigger("poder");
            emCooldown = true;
            timerCooldown = cooldown;
        }
    }

    protected abstract void UsarHabilidade();
}