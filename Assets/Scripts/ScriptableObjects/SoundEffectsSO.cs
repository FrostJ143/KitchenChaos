using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class SoundEffectsSO : ScriptableObject
{
    public List<AudioClip> chop;
    public List<AudioClip> deliverySucceed;
    public List<AudioClip> deliveryFailed;
    public List<AudioClip> footstop;
    public List<AudioClip> objectDrop;
    public List<AudioClip> objectPickup;
    public AudioClip stoveSizzle;
    public List<AudioClip> trash;
    public List<AudioClip> warning;
}
