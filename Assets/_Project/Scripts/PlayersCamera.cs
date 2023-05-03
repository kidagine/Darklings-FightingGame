using UnityEngine;

public class PlayersCamera : MonoBehaviour
{
    [SerializeField] private Camera _stageCamera = default;
    [SerializeField] private Camera _playersCamera = default;


    void LateUpdate() => _playersCamera.fieldOfView = _stageCamera.fieldOfView;
}
