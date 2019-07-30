using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireManager : MonoBehaviour
{
    public List<FireProbe> probes;
    public WindHorn windHorn;
    private FXManager fx;
    public bool simulateFire = true;

    [SerializeField, Range(0.01f, 50f)]
    private float timeToDouble = 30f;

    #region singleton

    public static FireManager instance = null;

    delegate void ProbeDelegate();
    ProbeDelegate probeUpdate;

    void Awake() 
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(instance);
                instance = this;
            }
        }
    }
    
    public static FireManager GetInstance()
    {
        return instance;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        fx = FXManager.GetInstance();
        StartCoroutine("SpreadFire");
    }

    public void AddProbe(FireProbe p)
    {
        probeUpdate += p.Grow;
        probes.Add(p);
    }

    public void RemoveProbe(FireProbe p)
    {
        probeUpdate -= p.Grow;
        probes.Remove(p);
    }
     [ContextMenu("Put Out")]
    public void PutOut()
    {
        foreach( FireProbe f in probes)
        {
            f.TurnOff();
        }
    }

    IEnumerator SpreadFire()
    {
        while(simulateFire)
        {
            if (probeUpdate != null)
                probeUpdate.Invoke();
                
            yield return new WaitForSeconds(timeToDouble);
        }

        yield return null;
    }
}
