using UnityEngine;

public class IntroScroller : MonoBehaviour
{
    private Transform mainTF;
    private Transform villageTF;
    [SerializeField] private GameObject village;
    [SerializeField] private float scrollSpeed = 0.005f;
    private void OnEnable()
    {
        mainTF = GetComponent<Transform>();
        villageTF = village.GetComponent<Transform>();
        mainTF.position = new Vector2(0f, 2.25f);

    }
    private void FixedUpdate()
    {
        if (mainTF.position.y > -3.62f) {
            mainTF.position = new Vector2(0f, 
            mainTF.position.y - scrollSpeed);
        }
        else {
            villageTF.position = new Vector2(villageTF.position.x, 
            villageTF.position.y - scrollSpeed);
        }
    }
}
