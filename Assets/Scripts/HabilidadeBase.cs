using UnityEngine;
using UnityEngine.InputSystem;

public abstract class HabilidadeBase : MonoBehaviour
{
    [SerializeField] protected float cooldown = 7f;
    [SerializeField] protected InputAction teclaHabilidade;

    protected Animator animator;
    protected float timerCooldown = 0f;
    protected bool emCooldown = false;
    private float cooldownFinal;

    public float CooldownAtual => timerCooldown;
    public float CooldownTotal => cooldownFinal; 
    public bool EmCooldown => emCooldown;

    protected virtual void OnEnable() => teclaHabilidade.Enable();
    protected virtual void OnDisable() => teclaHabilidade.Disable();

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        int reducao = PlayerPrefs.GetInt("ReducaoCooldown", 0);
        cooldownFinal = Mathf.Max(1f, cooldown - reducao);
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
            timerCooldown = cooldownFinal;
        }
    }

    protected abstract void UsarHabilidade();
}