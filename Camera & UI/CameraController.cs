using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    private CinemachineVirtualCamera vCam;
    private GameObject player;
    private void OnEnable()
    {
        vCam = GetComponent<CinemachineVirtualCamera>();
        player = GameObject.FindWithTag("Player");
        vCam.Follow = player.transform;
    }
}
