using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Pausable))]
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

    private Animator _animator;
    private Rigidbody2D _body;
    private RatState _state;
    private float _delayTime;
    private SurgeryTable _surgeryTable;
    private Pausable _pause;


    private void Start()
    {
        _animator = GetComponent<Animator>();
        _body = GetComponent<Rigidbody2D>();
        _pause = GetComponent<Pausable>();
        
        SetState(RatState.Idle);
        _delayTime = 0.0f;
    }

    void Update()
    {
        if (_pause.Paused)
        {
            return;
        }
        
        Vector2 motion = Vector2.zero;
        
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
            SetState(RatState.Moving);
            return;
        }

        if (_state == RatState.Moving)
        {
            Vector2 toTarget = (currentWaypoint.transform.position - transform.position);
            if (toTarget.magnitude > 0.1f)
            {
                var position = _body.position;
                motion = toTarget.normalized * moveSpeed;
                _body.position = position + motion * Time.deltaTime;
            }
            else
            {
                motion = (Vector2) currentWaypoint.transform.position - _body.position;
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

            UpdateAnimation(motion);
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

    private void UpdateAnimation(Vector2 motion)
    {
        _animator.SetFloat("DX", motion.x);
        _animator.SetFloat("DY", motion.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("Rat: I see table!");
        _surgeryTable = other.GetComponent<SurgeryTable>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<SurgeryTable>())
        {
            // Debug.Log("Rat: There is no table!");
            _surgeryTable = null;
        }
    }

    public void SetState(RatState state)
    {
        _state = state;
        // Debug.Log("Rat state: " + _state);
    }
}