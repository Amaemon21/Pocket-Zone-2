using System.Collections;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    [SerializeField] private float _detectionRadius;
    [SerializeField] private float _detectionTime;

    private Transform _target;
    private bool _isAware;

    public bool PlayerDetected { get; private set; }

    public void Init(PlayerController target)
    {
        _target = target.transform;
    }

    private void Update()
    {
        Detection();
    }

    public void Detection()
    {
        if (Vector3.Distance(transform.position, _target.position) <= _detectionRadius)
        {
            _isAware = true;
            PlayerDetected = true;
        }
        else
        {
            if (_isAware)
            {
                StartCoroutine(DetectionPlayer());
            }
        }
            
    }

    private IEnumerator DetectionPlayer()
    {
        Debug.Log("+");
        yield return new WaitForSeconds(_detectionTime);
        PlayerDetected = false;
        _isAware = false;
    }
}