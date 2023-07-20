using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    private CinemachineVirtualCamera vCam;
    private GameObject player;
    private void Awake()
    {
        vCam = GetComponent<CinemachineVirtualCamera>();
        player = GameObject.FindWithTag("Player");
        vCam.Follow = player.transform;
    }
}
