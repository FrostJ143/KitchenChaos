using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffects";
    public static SoundManager Instance {get; private set;}
    [SerializeField] private SoundEffectsSO soundEffectsSOList;
    
    private float volume = .5f;
    
    private void Awake()
    {
        Instance = this;
        
        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, 1f);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnSucceedDelivery += DeliveryManager_OnSucceedDelivery;
        DeliveryManager.Instance.OnFailedDelivery += DeliveryManager_OnFailedDelivery;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPickedSomething += Player_OnPickedSomething;
        BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectPlacedHere;
        TrashCounter.OnObjectTrashed += TrashCounter_OnObjectTrashed;
    }
    
    private void TrashCounter_OnObjectTrashed(object sender, System.EventArgs e)
    {
        PlaySound(soundEffectsSOList.trash, (sender as TrashCounter).transform.position);
    }

    private void BaseCounter_OnAnyObjectPlacedHere(object sender, System.EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(soundEffectsSOList.objectDrop, baseCounter.transform.position);
    }
    private void Player_OnPickedSomething(object sender, System.EventArgs e)
    {
        PlaySound(soundEffectsSOList.objectPickup, Player.Instance.transform.position);
    }
    
    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(soundEffectsSOList.chop, cuttingCounter.transform.position);
    }

    private void DeliveryManager_OnSucceedDelivery(object sender, System.EventArgs e)
    {
        PlaySound(soundEffectsSOList.deliverySucceed, DeliveryCounter.Instance.transform.position);
    }

    private void DeliveryManager_OnFailedDelivery(object sender, System.EventArgs e)
    {
        Debug.Log("Hello");
        PlaySound(soundEffectsSOList.deliveryFailed, DeliveryCounter.Instance.transform.position);
    }

    private void PlaySound(List<AudioClip> audioClipList, Vector3 position, float volumeMultiplier = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClipList[Random.Range(0, audioClipList.Count)], position, volume * volumeMultiplier);
    }
    
    public void PlayFootstepSound(Vector3 position, float volume = 1f)
    {
        PlaySound(soundEffectsSOList.footstop, position, volume);
    }
    
    public void PlayCountDownSound()
    {
        PlaySound(soundEffectsSOList.warning, Vector3.zero);
    }
    
    public void PlayWarningSound(Vector3 position)
    {
        PlaySound(soundEffectsSOList.warning, position);
    }
    
    public void ChangeVolume()
    {
        volume += .1f;
        if (volume > 1.1f)
        {
            volume = 0f;
        }
        
        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return volume;
    }
}
   
