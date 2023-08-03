using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startpos;
    [SerializeField] private GameObject cam;
    [SerializeField] private float parallaxFX;

    private Vector3 initialPosition; 

    private void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        initialPosition = transform.position; 
    }

    private void Update()
    {
        float temp = (cam.transform.position.x * (1 - parallaxFX));
        float dist = (cam.transform.position.x * parallaxFX);

        transform.position = new Vector3(startpos + dist, initialPosition.y, initialPosition.z); 

        if (temp > startpos + length) startpos += length;
        else if (temp < startpos - length) startpos -= length;
    }
}
