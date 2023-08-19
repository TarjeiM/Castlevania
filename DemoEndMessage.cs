using UnityEngine;
using TMPro;

public class DemoEndMessage : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            text.text = "END OF DEMO";
            text.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            text.gameObject.SetActive(false);
            text.text = "LEVEL UP";
        }
    }   
}
