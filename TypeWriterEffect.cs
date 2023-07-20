using System.Collections;
using UnityEngine;
using TMPro;

public class TypeWriterEffect : MonoBehaviour
{
    [SerializeField] private float delay = 0.1f;
    [SerializeField] private string fullText;
    private string currentText = "";
    [SerializeField] private TextMeshProUGUI textPro;
    void OnEnable()
    {
        StartCoroutine(ShowText());       
    }

    private IEnumerator ShowText() {
        for (int i = 0; i < fullText.Length + 1; i++) {
            currentText = fullText.Substring(0, i);
            textPro.text = currentText;
            yield return new WaitForSeconds(delay);
        }
    }
}
