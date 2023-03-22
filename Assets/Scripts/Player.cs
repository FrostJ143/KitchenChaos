using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    public static Player Instance {get; private set;}
    

    [SerializeField] private float movingSpeed = 5f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask counterLayerMask;
    [SerializeField] private Transform kitchenObjectLocation;

    private KitchenObject kitchenObject;
    
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public event EventHandler OnPickedSomething;

    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }
    
    private BaseCounter selectedCounter;

    private bool isWalking = false;
    
    private Vector3 lastMovingDir;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There are more than 1 player!");
        }
        Instance = this;
    }
    
    private void Start()
    {
        gameInput.OnInteracte += GameInput_OnInteracte;
        gameInput.OnAlternateInteracte += GameInput_OnAlternateInteracte;
    }
    
    private void GameInput_OnInteracte(object sender, System.EventArgs e)
    {
        if (selectedCounter != null)
        {
            if (!GameManager.Instance.IsPlaying()) return;
            selectedCounter.Interacte(this);
        }
    }

    private void GameInput_OnAlternateInteracte(object sender, System.EventArgs e)
    {
        if (selectedCounter != null)
        {
            if (!GameManager.Instance.IsPlaying()) return;
            selectedCounter.AlternateInteracte(this);
        }
    }
    
    

    private void Update()
    {
        HandleMovement();
        HandleInteraction();
    }
    
    public bool IsWalking()
    {
        return isWalking;
    }
    
    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        
        float playerHeight = 2f;
        float playerRadius = .6f;
        float moveDistance= Time.deltaTime * movingSpeed;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);
        
        float RotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * RotateSpeed);

        if (!canMove)
        {
            // Check if can move X direction
            Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);
            if (canMove)
            {
                moveDir = moveDirX;
            }
            else // Cannot move in X direction, check if can in Z direction
            {
                Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);
                if (canMove)
                {
                    moveDir = moveDirZ;
                }
            }
        }

        if (canMove)
        {
            transform.position += moveDir * Time.deltaTime * movingSpeed;
        }
        
        isWalking = moveDir != Vector3.zero;
        
    }
    
    private void HandleInteraction()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        
        if (moveDir != Vector3.zero)
        {
            lastMovingDir = moveDir;
        }

        float interacteDistancce = 1f;

        if (Physics.Raycast(transform.position, lastMovingDir, out RaycastHit raycastHit, interacteDistancce, counterLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter clearCounter))
            {
                if (selectedCounter != clearCounter)
                {
                    SetSelectedCounter(clearCounter);
                }
            }
            else 
            {
                SetSelectedCounter(null);
            }
        }
        else 
        {
            SetSelectedCounter(null);
        }
    }
    
    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;
        
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs {
                selectedCounter = selectedCounter
        });
    }

    public Transform GetKitchenObjectLocation()
    {
        return kitchenObjectLocation;
    }
    
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
    
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        OnPickedSomething?.Invoke(this, EventArgs.Empty);
    }
    
    public void ClearKitchenObject()
    {
        this.kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
