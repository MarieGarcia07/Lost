using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(MovementController))]
public class Player : MonoBehaviour
{
    [SerializeField] CameraRig mCameraRigPrefab;

    private PlayerInputActions mPlayerInputActions;
    private MovementController mMovementController;

    CameraRig mCameraRig;

    void Awake()
    {
        mCameraRig = Instantiate(mCameraRigPrefab);
        mCameraRig.SetFollowTransform(transform);

        mMovementController = GetComponent<MovementController>();

        mPlayerInputActions = new PlayerInputActions();


        mPlayerInputActions.GamePlay.Jump.performed += mMovementController.PerformJump;

        mPlayerInputActions.GamePlay.Move.performed += mMovementController.HandleMoveInput;
        mPlayerInputActions.GamePlay.Move.canceled += mMovementController.HandleMoveInput;

        mPlayerInputActions.GamePlay.Look.performed += (context) => mCameraRig.SetLookInput(context.ReadValue<Vector2>());
        mPlayerInputActions.GamePlay.Look.canceled += (context) => mCameraRig.SetLookInput(context.ReadValue<Vector2>());
    }

    void OnEnable()
    {
        mPlayerInputActions.Enable();
    }

    void OnDisable()
    {
        mPlayerInputActions.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
