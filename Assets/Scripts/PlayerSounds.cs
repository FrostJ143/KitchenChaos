using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    private Player player;

    private float footstepTimer;
    private float footstepTimerMax = .2f;
    
    private void Awake()
    {
        player = GetComponent<Player>();
    }
    
    private void Update()
    {
        footstepTimer += Time.deltaTime;

        if (footstepTimer >= footstepTimerMax)
        {
            if (player.IsWalking())
            {
                SoundManager.Instance.PlayFootstepSound(player.transform.position);
            }
        }
    }
}