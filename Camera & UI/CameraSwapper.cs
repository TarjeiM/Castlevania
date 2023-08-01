using UnityEngine;
using Cinemachine;

public class CameraSwapper : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera vCam;

    private void Awake()
    {
        vCam.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Player")
        {
            vCam.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            vCam.gameObject.SetActive(false);
        }
    }
}
