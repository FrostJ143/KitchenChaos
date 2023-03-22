using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    private const string CUT = "Cut";
    private Animator animator;
    [SerializeField] private CuttingCounter cuttingCounter;
    
    public void Awake()
    {
        animator = GetComponent<Animator>();
    }
    
    public void Start()
    {
        cuttingCounter.OnCut += CuttingCounter_OnCut;
    }
    
    private void CuttingCounter_OnCut(object sender, System.EventArgs e)
    {
        animator.SetTrigger(CUT);
    }
}
