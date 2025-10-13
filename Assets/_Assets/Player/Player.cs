using System;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(MovementController))]
public class Player : MonoBehaviour, IViewClient
{
    [SerializeField] CameraRig mCameraRigPrefab;

    private PlayerInputActions mPlayerInputActions;
    private MovementController mMovementController;

    private BattleState mBattleState;
    private BattlePartyComponent mBattlePartyComponent;

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

        mBattlePartyComponent = GetComponent<BattlePartyComponent>();
    }

    void OnEnable()
    {
        mPlayerInputActions.Enable();
    }

    void OnDisable()
    {
        mPlayerInputActions.Disable();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == gameObject)
        {
            return;
        }

        BattlePartyComponent otherBattlePartyComponent = other.GetComponent<BattlePartyComponent>();
        if (otherBattlePartyComponent && !IsInBattle())
        {
            GameMode.MainGameMode.BattleManager.StartBattle(mBattlePartyComponent, otherBattlePartyComponent);
            SwitchToBattleMode(BattleState.InBattle);
        }
    }

    private void SwitchToBattleMode(BattleState battleState)
    {
        if (battleState == BattleState.InBattle)
        {
            mPlayerInputActions.GamePlay.Disable();
        }

        if (battleState == BattleState.Roaming)
        {
            mPlayerInputActions.GamePlay.Enable();
        }
        
       
    }

    private bool IsInBattle()
    {
        return mBattleState == BattleState.InBattle;
    }

    public void SetViewTarget(Transform viewTarget)
    {
        mCameraRig.SetFollowTransform(viewTarget);
        mCameraRig.transform.rotation = viewTarget.transform.rotation;
    }

    public void ResetViewAngle()
    {
        mCameraRig.ResetViewAngle();
    }
}
