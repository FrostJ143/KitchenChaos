using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public interface IHasProgressBar 
{
    public event EventHandler<OnCuttingProgressEventArgs> OnProgressChanged;
    public class OnCuttingProgressEventArgs {
        public float progressNormalized;
    }
}
