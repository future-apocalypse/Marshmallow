using UnityEngine;

public class SinkingPlatform : MonoBehaviour
{
    [SerializeField] private float _springStrength = 40f;
    [SerializeField] private float _damping = 6f;
    [SerializeField] private float _playerWeightForce = 10f;
    [SerializeField] private float _maxSinkDepth = 0.4f;

    [SerializeField] private float _sinkDelay = 3f;
    [SerializeField] private float _finalSinkSpeed = 1f;
    [SerializeField] private float _destroyY = -10f;
    
    
    private Rigidbody _rb;
    private float _restY;
    private bool _playerOnPlatform;

    private float _timer;
    private bool _sinkingForever;
    private bool _timerActive;
    
    
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _restY = transform.position.y;
    }

    void FixedUpdate()
    {
        if (_sinkingForever)
        {
            _rb.linearDamping = 1f;
            _rb.AddForce(Vector3.down * _finalSinkSpeed, ForceMode.VelocityChange);
            return;
            
        }

        float displacement = _restY - _rb.position.y;
        float springForce = displacement * _springStrength;
        float dampingForce = -_rb.linearVelocity.y * _damping;

        _rb.AddForce(Vector3.up * (springForce + dampingForce), ForceMode.Force);

        _rb.linearDamping = _playerOnPlatform ? 4f : 2f;

        if (_playerOnPlatform)
        {
            _rb.AddForce(Vector3.down * _playerWeightForce, ForceMode.Force);
            
        }

        float minY = _restY - _maxSinkDepth;
        if (_rb.position.y < minY)
        {
            _rb.position = new Vector3(_rb.position.x, minY, _rb.position.z);
            _rb.linearVelocity = new Vector3(_rb.linearVelocity.x, 0f, _rb.linearVelocity.z);
        }
    }

    void Update()
    {
        if (!_sinkingForever && _timerActive)
        {
            _timer += Time.deltaTime;

            if (_timer >= _sinkDelay)
            {
                _sinkingForever = true;
                _rb.linearVelocity = Vector3.zero;
            }
        }

        if (transform.position.y < _destroyY)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerOnPlatform = true;
            _timerActive = true;
            GameManager.Instance.RegisterJump();
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")){
            _playerOnPlatform = false;
        }
    }
}
