using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXManager : MonoBehaviour
{

    public enum PType
    {
        Flame1, Smoke1, Flame2, Smoke2, 
    }

    static FXManager instance = null;
    public ParticleSystem[] part_systems;

    public int index = 2;

    void Awake()
    {
        if (FXManager.GetInstance() == null)
        {
            instance = this;
        }
        else if (instance != this)
            Destroy(this);

        if (part_systems == null)
        {
            part_systems = GetComponentsInChildren<ParticleSystem>();
        }

        foreach (ParticleSystem ps in part_systems)
        {
            var main = ps.main;
            main.simulationSpace = ParticleSystemSimulationSpace.World;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    public static FXManager GetInstance()
    {
        return instance;
    }

    // burst from position (wind direction)
    public void Burst(PType type, Vector3 pos, float force, int amt)
    {
        var emitter = new ParticleSystem.EmitParams(); 

        emitter.position = pos;
        emitter.applyShapeToPosition = true;
        
        ParticleSystem ps = part_systems[(int)type]; 
        
        // Set velocity curve to wind horn windSpeed, ending with 1/10th speed
        var vel = ps.velocityOverLifetime;
        vel.enabled = true;
        vel.space = ParticleSystemSimulationSpace.Local;

        AnimationCurve curve = new AnimationCurve();
        curve.AddKey(0.0f, force);
        curve.AddKey(1.0f, force / 10);
        vel.x = new ParticleSystem.MinMaxCurve(10.0f, curve);
        vel.y = new ParticleSystem.MinMaxCurve(0f, curve);
        vel.z = new ParticleSystem.MinMaxCurve(0f, curve);

        ps.Emit(emitter, amt);
    }
}
