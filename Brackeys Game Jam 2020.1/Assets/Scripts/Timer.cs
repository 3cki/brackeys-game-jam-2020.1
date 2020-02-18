using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float time = 0;

    void Update()
    {
        time += Time.deltaTime;
        this.GetComponent<TextMeshProUGUI>().text = time.ToString();
    }
}
