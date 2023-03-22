using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class BurningRecipeSO : ScriptableObject
{
    public KichenObjectsSO input;
    public KichenObjectsSO output;
    
    public float burningTimerMax;
}
