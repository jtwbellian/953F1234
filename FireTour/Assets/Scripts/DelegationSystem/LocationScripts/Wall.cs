using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A side of the building
/// </summary>
public class Wall : DelegationLocation
{
    public FireProbe[] myProbes;
    public float health = 100f;    
}