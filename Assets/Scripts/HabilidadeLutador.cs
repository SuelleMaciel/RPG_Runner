using UnityEngine;

public class HabilidadeLutador : HabilidadeBase
{
    [SerializeField] private float duracaoEscudo = 3f;

    public bool EscudoAtivo { get; private set; } = false;
    [SerializeField] private ParticleSystem particulas;


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
        if (particulas != null){particulas.Play();}
        EscudoAtivo = true;
        timerEscudo = duracaoEscudo;
    }
}