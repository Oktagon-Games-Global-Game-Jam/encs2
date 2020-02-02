using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemController : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    
    private ParticleSystem.EmitParams FX_Particle_Param;
    // Start is called before the first frame update

    public void SpawnParticle(C_Particle cParticle)
    {
        FX_Particle_Param.position = cParticle.ParticlePos;
        _particleSystem.Emit(FX_Particle_Param, cParticle.Count);
    }
}
