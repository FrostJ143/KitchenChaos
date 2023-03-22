using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    public event EventHandler OnSpawnPlate;
    public event EventHandler OnRemovePlate;
    
    [SerializeField] private KichenObjectsSO kichenObjectsSO;

    private float spawnPlatesTimer;
    private float spawnPlatesTimerMax = 4f;
    private int platesSpawnAmount;
    private int platesSpawnAmountMax = 4;
    
    private void Update()
    {
        spawnPlatesTimer += Time.deltaTime;
        if ( spawnPlatesTimer >= spawnPlatesTimerMax)
        {
            if (GameManager.Instance.IsPlaying() && platesSpawnAmount < platesSpawnAmountMax)
            {
                OnSpawnPlate?.Invoke(this, EventArgs.Empty);
                ++platesSpawnAmount;
            }
            spawnPlatesTimer = 0f;
        }
    }
    
    public override void Interacte(Player player)
    {
        if (!player.HasKitchenObject())
        {
            if (platesSpawnAmount > 0)
            {
                --platesSpawnAmount;
                Transform kitchenObjectTransform = Instantiate(kichenObjectsSO.prefab);
                KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
                kitchenObject.SetKitchenObjectParent(player);

                OnRemovePlate?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
