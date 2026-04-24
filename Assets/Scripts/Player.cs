using UnityEngine;
using System;

public class Player : MonoBehaviour {
    // ===================== FIELDS =====================
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInputs gameInput;
    [SerializeField] private LayerMask counterLayerMask;

    public static Player Instance { get; private set; }

    private bool isWalking;
    private Vector3 lastInteractDiraction;
    private ClearCounter selectedCounter;


    // ===================== EVENTS =====================
    public event EventHandler<OnSelectedCounterChangedEvetArgs> OnSelectedCounterChanged;

    public class OnSelectedCounterChangedEvetArgs : EventArgs {
        public ClearCounter selectedClearCounter;
    }


    // ===================== UNITY METHODS =====================
    private void Awake() {
        Instance = this;
    }

    private void Start() {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void Update() {
        HandleMovement();
        HandleInteractions();
    }


    // ===================== PUBLIC METHODS =====================
    public bool IsWalking() {
        return isWalking;
    }


    // ===================== INPUT CALLBACKS =====================
    private void GameInput_OnInteractAction(object sender, EventArgs e) {
        if (selectedCounter != null) {
            selectedCounter.Interact();
        }
    }


    // ===================== MOVEMENT =====================
    private void HandleMovement() {
        Vector2 inputVector = gameInput.GetMoventVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = 0.7f;
        float playerHeight = 2f;

        bool canMove = !Physics.CapsuleCast(
            transform.position,
            transform.position + Vector3.up * playerHeight,
            playerRadius,
            moveDir,
            moveDistance
        );

        // Diagonal collision
        if (!canMove) {
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(
                transform.position,
                transform.position + Vector3.up * playerHeight,
                playerRadius,
                moveDirX,
                moveDistance
            );

            if (canMove) {
                moveDir = moveDirX;
            }
            else {
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(
                    transform.position,
                    transform.position + Vector3.up * playerHeight,
                    playerRadius,
                    moveDirZ,
                    moveDistance
                );

                if (canMove) {
                    moveDir = moveDirZ;
                }
            }
        }

        // Move
        if (canMove) {
            transform.position += moveDir * moveSpeed * Time.deltaTime;
        }

        isWalking = moveDir != Vector3.zero;

        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(
            transform.forward,
            moveDir,
            Time.deltaTime * rotateSpeed
        );
    }


    // ===================== INTERACTIONS =====================
    private void HandleInteractions() {
        Vector2 inputVector = gameInput.GetMoventVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero) {
            lastInteractDiraction = moveDir.normalized;
        }

        float interactDistance = 2f;

        if (Physics.Raycast(
            transform.position,
            lastInteractDiraction,
            out RaycastHit raycastHit,
            interactDistance,
            counterLayerMask)) {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter)) {
                if (clearCounter != selectedCounter) {
                    SelectedKitchenTable(clearCounter);

                    Debug.Log(clearCounter);
                }
            }
            else {
                SelectedKitchenTable(null);
            }
        }
        else {
            SelectedKitchenTable(null);
        }
    }


    // ===================== SELECTION =====================
    private void SelectedKitchenTable(ClearCounter selectedClearCounter) {
        this.selectedCounter = selectedClearCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEvetArgs {
            selectedClearCounter = selectedClearCounter
        });

        Debug.Log(selectedClearCounter);
    }
}