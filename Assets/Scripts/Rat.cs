using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Rat : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float idleDelay = 1f;
    public float stealingDelay = 2f;
    public float lookingDelay = 0.25f;

    public enum RatState
    {
        Idle,
        Turning,
        Moving,
        Looking,
        Stealing
    }

    public RatWaypoint currentWaypoint;

    private Rigidbody2D _body;
    private RatState _state;
    private float _delayTime;
    private SurgeryTable _surgeryTable;

    private void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        SetState(RatState.Idle);
        _delayTime = 0.0f;
    }

    void Update()
    {
        if (_state == RatState.Idle)
        {
            _delayTime += Time.deltaTime;

            if (_delayTime > idleDelay)
            {
                SetState(RatState.Turning);
                _delayTime = 0.0f;
            }

            return;
        }

        if (_state == RatState.Turning)
        {
            var toTarget = currentWaypoint.transform.position - transform.position;
            if (Math.Abs(toTarget.x) > Math.Abs(toTarget.y))
            {
                var rotation = transform.localRotation;
                rotation.z = -90;
                transform.localRotation = rotation;
            }
            else
            {
                var rotation = transform.localRotation;
                rotation.z = 0;
                transform.localRotation = rotation;
            }

            SetState(RatState.Moving);

            return;
        }

        if (_state == RatState.Moving)
        {
            Vector2 toTarget = (currentWaypoint.transform.position - transform.position);
            if (toTarget.magnitude > 0.1f)
            {
                var position = _body.position;
                _body.position = position + toTarget.normalized * (moveSpeed * Time.deltaTime);
            }
            else
            {
                _body.position = currentWaypoint.transform.position;
                
                if (currentWaypoint.nextPoint)
                {
                    currentWaypoint = currentWaypoint.nextPoint;
                    SetState(RatState.Looking);
                }
                else
                {
                    Destroy(gameObject);
                }
            }

            return;
        }

        if (_state == RatState.Looking)
        {
            if (_delayTime < lookingDelay)
            {
                _delayTime += Time.deltaTime;
                return;
            }

            _delayTime = 0.0f;

            if (_surgeryTable)
            {
                SetState(RatState.Stealing);
            }
            else
            {
                SetState(RatState.Idle);
            }

            return;
        }

        if (_state == RatState.Stealing)
        {
            _delayTime += Time.deltaTime;

            if (_delayTime > stealingDelay && _surgeryTable)
            {
                _surgeryTable.Steal(gameObject.transform);
                
                _delayTime = 0.0f;
                SetState(RatState.Idle);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Rat: I see table!");
        _surgeryTable = other.GetComponent<SurgeryTable>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<SurgeryTable>())
        {
            Debug.Log("Rat: There is no table!");
            _surgeryTable = null;
        }
    }

    public void SetState(RatState state)
    {
        _state = state;
        Debug.Log("Rat state: " + _state);
    }
}