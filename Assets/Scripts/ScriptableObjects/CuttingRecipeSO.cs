using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CuttingRecipeSO : ScriptableObject
{
    public KichenObjectsSO input;
    public KichenObjectsSO output;
    
    public int maxRecipeCut;
}
