using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float time = 0;
    public bool timing = true;
    void Update()
    {
        if (timing) {
            time += Time.deltaTime;
            this.GetComponent<TextMeshProUGUI>().text = time.ToString();
        }
    }
}
