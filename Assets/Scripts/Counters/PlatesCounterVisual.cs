using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] PlatesCounter platesCounter;
    [SerializeField] Transform visualPlates;
    [SerializeField] Transform platesLocation;
                
    private List<GameObject> plateVisualGameObjectList;
    private void Awake()
    {
        plateVisualGameObjectList = new List<GameObject>();
    }
    
    private void Start()
    {
        platesCounter.OnSpawnPlate += PlatesCounter_OnSpawnPlate;
        platesCounter.OnRemovePlate += PlatesCounter_OnRemovePlate;
    }
    
    private void PlatesCounter_OnSpawnPlate(object sender, System.EventArgs e)
    {
        Transform plateVisualTransform = Instantiate(visualPlates, platesLocation);


        float plateOffsetY = .1f;
        if (plateVisualTransform == null) Debug.LogError("Null");
        plateVisualTransform.localPosition = new Vector3(0, plateOffsetY * plateVisualGameObjectList.Count, 0);
        plateVisualGameObjectList.Add(plateVisualTransform.gameObject);
    }
    
    private void PlatesCounter_OnRemovePlate(object sender, System.EventArgs e)
    {
        GameObject plateRemove = plateVisualGameObjectList[plateVisualGameObjectList.Count - 1];
        plateVisualGameObjectList.Remove(plateRemove);
        Destroy(plateRemove);
    }
}
