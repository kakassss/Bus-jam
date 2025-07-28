using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class StickMan : MonoBehaviour, IStickMan
{
    public ColorIndex ColorIndex => _colorIndex;

    [Header("References")]
    [SerializeField] private Animator _animator;
    [SerializeField] private MovementSO _movementSO;
    [SerializeField] private RotationSO _rotationSO;
    [SerializeField] private ColorIndex _colorIndex;
    
    private MovementUtils _movementUtils;
    private QuaternionUtils _quaternionUtils;
    private PathFinding _pathFinding;
    private StickManAnimator _stickManAnimator;
    private StickManProvider _stickManProvider;
    private SpotsMovementManager _spotsMovementManager;
    private GridManager _gridManager;
    
    private List<Grid> _path = new List<Grid>();
    private Coroutine _movementCoroutine;

    private bool _onBus;
    private BusSeat _markedSeat;
    private PassengerData _spotData;
    
    [Inject]
    private void Construct(StickManAnimator stickManAnimator, MovementUtils movementUtils, PathFinding pathFinding,
        GridManager gridManager,QuaternionUtils quaternionUtils,SpotsMovementManager spotsMovementManager)
    {
        _stickManAnimator = stickManAnimator;
        _stickManAnimator.SetAnimator(_animator);
        
        _movementUtils = movementUtils;
        _movementUtils.SetMovement(_movementSO);
        
        _quaternionUtils = quaternionUtils;
        quaternionUtils.SetRotation(_rotationSO);
        
        _pathFinding = pathFinding;
        _spotsMovementManager = spotsMovementManager;
        _gridManager = gridManager;
    }
    
    public void FindPath()
    {
        _path = _pathFinding.FindPathForward(transform.position);
    }
    
    public void FirstAction()
    {
        if(_path == null) return;
        
        FirstMovement();
        SetStickManGrid();
        
        if(_spotsMovementManager.CanColorEqual(this) == false) return;
        if(_spotsMovementManager.CanSeatMarked() == false) return;
        _markedSeat = _spotsMovementManager.SetMarkSeat();
    }
    
    private void FirstMovement()
    {
        if (_movementCoroutine == null)
        {
            _movementCoroutine = StartCoroutine(_movementUtils.PathMovement(transform, _path,Rotate,OnFirstActionCompleted));
            MovementAnimation();
        }
    }
    private void OnFirstActionCompleted()
    {
        StopCoroutine(_movementCoroutine);
        _movementCoroutine = null;
        
        FinalMovement();
    }
    
    private void FinalMovement()
    {
        var finalPos = _spotsMovementManager.SetTarget(this, transform,_markedSeat);
        _movementCoroutine = StartCoroutine(_movementUtils.PathMovement(transform, finalPos,Rotate,OnSecondActionCompleted));
    }

    private void OnSecondActionCompleted()
    {
        StopCoroutine(_movementCoroutine);
        _movementCoroutine = null;
        
        if (_markedSeat == null)
        {
            IdleAnimation();
            _quaternionUtils.ResetRotation(transform);
        }
        else
        {
            var data = _spotsMovementManager.GetPassengerData();
            Seat(data.BusSeatTransform,data.BusTransform);
            IdleAnimation();
            _quaternionUtils.SetRotation(transform,new Vector3(0f,90f,0f));
            
            _spotsMovementManager.SetSelectedSeatFull();
            _spotsMovementManager.FireBus();
        }
    }
    
    private void Seat(Transform seatTransform,Transform busTransform)
    {
        transform.position = seatTransform.position;
        transform.SetParent(busTransform);
        _onBus = true;
    }
    
    public async Task MoveToBus(BusSeat seat,Bus bus)
    {
        if(_onBus == true) return;
        if(_movementCoroutine != null) return;

        _spotData.Bus = bus;
        _spotData.Seat = seat;
        _spotData.BusTransform = bus.transform;
        _spotData.BusSeatTransform = seat.SeatTransform;
        
        _stickManAnimator.SetMovementAnimation();
        Rotate(_spotData.BusTransform.position);
        await _movementUtils.MoveToSingleTargetAsync(transform, bus.transform,OnMoveToBus);
    }
    
    private void OnMoveToBus()
    {
        _stickManAnimator.SetIdleAnimation();
        _quaternionUtils.SetRotation(transform,new Vector3(0f,90f,0f));
        Seat(_spotData.BusSeatTransform.transform,_spotData.BusTransform.transform);
    }
    
    private void Rotate(Vector3 position)
    {
        _quaternionUtils.LookAtPosition(transform,position);
    }

    private void SetStickManGrid()
    {
        _gridManager.GetXZ(transform.position, out var x, out var z);
        var currentGrid = _gridManager.GetGridAtIndex(x,z);
        currentGrid.StickMan = null;
    }
    
    private void MovementAnimation()
    {
        _stickManAnimator.SetMovementAnimation();
    }

    private void IdleAnimation()
    {
        _stickManAnimator.SetIdleAnimation();
    }
}