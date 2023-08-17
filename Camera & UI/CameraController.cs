using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    private CinemachineVirtualCamera vCam;
    private GameObject player;
    [SerializeField] private bool isStatic = false;
    private void Awake()
    {
        vCam = GetComponent<CinemachineVirtualCamera>();
        player = GameObject.FindWithTag("Player");
        if (!isStatic) { vCam.Follow = player.transform; }
        vCam.Priority = 0;
    }
    private void OnEnable()
    {
        vCam.Priority = 10;
    }
    private void OnDisable()
    {
        vCam.Priority = 0;
    }
}
