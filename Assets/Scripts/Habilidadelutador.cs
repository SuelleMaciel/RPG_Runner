using UnityEngine;

public class Habilidadelutador : HabilidadeBase
{
    [Header("Escudo")]
    [SerializeField] private float duracaoEscudo = 3f;

    public bool EscudoAtivo { get; private set; } = false;

    private float timerEscudo = 0f;

    protected override void Update()
    {
        base.Update();
        if (EscudoAtivo)
        {
            timerEscudo -= Time.deltaTime;
            if (timerEscudo <= 0f)
                EscudoAtivo = false;
        }
    }

    protected override void UsarHabilidade()
    {
        EscudoAtivo = true;
        timerEscudo = duracaoEscudo;
    }
}