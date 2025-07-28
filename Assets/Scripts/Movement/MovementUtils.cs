using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class MovementUtils
{
    private GridManager _gridManager;
    private MovementSO _movementSO;
    
    private List<Vector3> _positions = new List<Vector3>();
    private float _movementSpeed;

    public MovementUtils(GridManager gridManager, SpotsManager spotsManager,BusEvents busEvents)
    {
        _gridManager = gridManager;
    }
    
    public void SetMovement(MovementSO movementSO)
    {
        _movementSO = movementSO;
        _movementSpeed = _movementSO.MovementSpeed;
    }

    public async Task MoveToSingleTargetAsync(Transform transform, Transform target, Action onComplete = null)
    {
        Vector3 currentTargetPos = target.position;
            
        while ((currentTargetPos - transform.position).magnitude >= 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentTargetPos, _movementSpeed * Time.deltaTime);
            await Task.Yield();
        }
            
        transform.position = currentTargetPos;
        onComplete?.Invoke();
    }
    
    public IEnumerator MoveToSingleTarget(Transform transform, Transform target,Action onComplete = null)
    {
        Vector3 currentTargetPos = target.position;
            
        while ((currentTargetPos - transform.position).magnitude >= 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentTargetPos, _movementSpeed * Time.deltaTime);
            yield return null;
        }
            
        transform.position = currentTargetPos;
        onComplete?.Invoke();
    }
    
    public IEnumerator PathMovement(Transform transform, Vector3 target,Action<Vector3> onStepComplete = null,Action onComplete = null)
    {
        Vector3 currentTargetPos = target;
        onStepComplete?.Invoke(currentTargetPos);
            
        while ((currentTargetPos - transform.position).magnitude >= 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentTargetPos, _movementSpeed * Time.deltaTime);
                
            yield return null;
        }
            
        transform.position = currentTargetPos;
        onComplete?.Invoke();
    }
    
    public IEnumerator PathMovement(Transform transform, List<Grid> targets,Action<Vector3> onStepComplete = null,Action onComplete = null)
    {
        _positions.Clear();
        
        //Set grids to position
        foreach (var target in targets)
        {
            _positions.Add(_gridManager.GetWorldPosition(target.X + 0.5f,target.Z + 0.5f));
        }
        
        for (int i = 0; i < _positions.Count(); i++)
        {
            Vector3 currentTargetPos = _positions[i];
            onStepComplete?.Invoke(currentTargetPos);
            
            while ((currentTargetPos - transform.position).magnitude >= 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, currentTargetPos, _movementSpeed * Time.deltaTime);
                
                yield return null;
            }
            
            transform.position = currentTargetPos;
        }
        onComplete?.Invoke();
    }
}