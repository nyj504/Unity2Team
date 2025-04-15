using UnityEngine;

public class Player : MonoBehaviour
{
    private static Player _instance;
    public static Player Instance
    {
        get { return _instance; }
    }

    public enum State
    {
        Idle,
        Walk,
        Run,
        BaseAttack,
        Skill,
        Dead
    }

    private Animator _animator;

    private State _curState;

    private float _moveSpeed = 5.0f;
    private float _rotateSpeed = 1.0f;

    private Vector3 _velocity;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        Control();
        UpdateAnimator();
    }

    private void Control()
    {
        bool isMoving = Input.GetKey(KeyCode.W);
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        Vector3 playerScreenPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 mouseScreenPos = Input.mousePosition;
        Vector3 dir = mouseScreenPos - playerScreenPos;
        float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        if(Input.GetMouseButtonDown(0))
        {
            _curState = State.BaseAttack;
        }

        switch (_curState)
        {
            case State.Idle:
                {
                    if (isMoving)
                    {
                        _curState = isRunning ? State.Run : State.Walk;
                    }
                    _velocity = Vector3.zero;
                }   
                break;

            case State.Walk:
                {
                    if (!isMoving)
                    {
                        _curState = State.Idle;
                        _velocity = Vector3.zero;
                        break;
                    }
                    if (isRunning)
                    {
                        _curState = State.Run;
                        break;
                    }
                    _velocity = transform.forward * _moveSpeed;
                    transform.position += _velocity * Time.deltaTime;
                }
                break;

            case State.Run:
                {
                    if (!isMoving)
                    {
                        _curState = State.Idle;
                        _velocity = Vector3.zero;
                        break;
                    }
                    if (!isRunning)
                    {
                        _curState = State.Walk;
                        break;
                    }
                    _velocity = transform.forward * _moveSpeed * 1.8f;
                    transform.position += _velocity * Time.deltaTime;
                } 
                break;            
        }
    }
    private void UpdateAnimator()
    {
        _animator.SetInteger("CurState", (int)_curState);
    }
}
