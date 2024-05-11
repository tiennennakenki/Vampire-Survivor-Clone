using System.Collections;
using TMPro;
using UnityEngine;

public class BlinkingText : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI text;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        yield return new WaitForSeconds(1f);

        while (true)
        {
            
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a > 0 ? 0 : 0.2f);

            
            yield return new WaitForSeconds(0.5f);
        }
    }
}
