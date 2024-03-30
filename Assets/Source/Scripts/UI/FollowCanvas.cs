using UnityEngine;

public class FollowCanvas : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private GameObject _targetObject;

    private void LateUpdate()
    {
        _canvas.transform.localScale = _targetObject.transform.localScale / 10;
    }
}