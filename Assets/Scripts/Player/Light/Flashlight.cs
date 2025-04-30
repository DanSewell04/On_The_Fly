using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    Light m_light;
    public bool drainOverTime;
    public float maxBrightness;
    public float minBrightness;
    public float drainSpeed;


    void Start()
    {
        m_light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (drainOverTime == true && m_light.enabled == true)
        {
            m_light.intensity = Mathf.Clamp(m_light.intensity, minBrightness, maxBrightness);
            if (m_light.intensity > minBrightness )
            {
                m_light.intensity -= Time.deltaTime * (drainSpeed / 1000);
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            m_light.enabled = !m_light.enabled;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Replace(.3f);
        }
    }

    private void Replace(float amount)
    {
        m_light.intensity += amount;
    }
}
