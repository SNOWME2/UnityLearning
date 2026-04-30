using System;
using UnityEngine;

public interface IHasProgress 
{

    public event EventHandler<OnProgressEventChangedArgs> OnProgressChanged;
    public class OnProgressEventChangedArgs : EventArgs {
        public float progressNormalized;
    }
}
