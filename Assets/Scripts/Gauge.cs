using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gauge : MonoBehaviour
{
    public Image gauge;
    public bool coolingDown = false;
    public float specialTime = 10.0f;

    public void Update()
    {
        if (coolingDown == true)
        {
            gauge.fillAmount -= 1.0f / specialTime * Time.deltaTime;
        }
    }
}
