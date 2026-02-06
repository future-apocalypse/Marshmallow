using UnityEngine;

public class PlayerShadow : MonoBehaviour
{
    [SerializeField] private Transform _shadow;
    [SerializeField] private LayerMask _groundMask;

    [SerializeField] private float _maxRayDistance = 20f;

    [SerializeField] private float _minScale = 0.3f;
    [SerializeField] private float _maxScale = 0.8f;
    
    
    void Update()
    {
        UpdateShadow();
    }

    void UpdateShadow()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, _maxRayDistance, _groundMask))
        {
            _shadow.position = hit.point + Vector3.up * 0.02f;
            
            float height = transform.position.y - hit.point.y;
            float t = Mathf.InverseLerp(0f, 5f, height);
            
            float scale = Mathf.Lerp(_minScale, _maxScale, t);
            _shadow.localScale = Vector3.one * scale;
            
        }
        
    }
}
