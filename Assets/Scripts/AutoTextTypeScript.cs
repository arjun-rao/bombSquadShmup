using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoTextTypeScript : MonoBehaviour
{
    public float letterPause = 0.2f;
    public AudioClip typeSound1;

    string message;
    Text textComp;
    // Start is called before the first frame update
    void Start()
    {
        textComp = GetComponent<Text>();
        message = textComp.text;
        textComp.text = "";
        StartCoroutine(TypeText ());
    }

    IEnumerator TypeText () {
        SoundManagerScript.instance.PlaySingle(typeSound1);
        foreach (char letter in message.ToCharArray()) {
            textComp.text += letter;
            yield return 0;
            yield return new WaitForSeconds (letterPause);
        }
        SoundManagerScript.instance.efxSource.Stop();
    }
}
