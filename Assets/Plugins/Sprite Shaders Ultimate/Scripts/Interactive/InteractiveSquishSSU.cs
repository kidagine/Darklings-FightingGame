using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveSquishSSU : MonoBehaviour
{
    [Header("Settings:")]
    public float squishSpeed = 5f;
    public bool staySquished = true;
    public float squishDuration = 0.1f;

    //References:
    Material mat;

    //Internal:
    float currentSquish;
    float lastTriggerStayTime;

    void Start()
    {
        mat = GetComponent<SpriteRenderer>().material;
        currentSquish = 0f;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (staySquished)
        {
            lastTriggerStayTime = Time.time;
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        lastTriggerStayTime = Time.time;
    }

    void Update()
    {
        float newSquish = currentSquish;

        if(Time.time > lastTriggerStayTime + squishDuration)
        {
            newSquish = Mathf.Lerp(newSquish, -0.1f, Time.deltaTime * squishSpeed);
        }
        else
        {
            newSquish = Mathf.Lerp(newSquish, 1.1f, Time.deltaTime * squishSpeed);
        }

        newSquish = Mathf.Clamp01(newSquish);
        if(newSquish != currentSquish)
        {
            currentSquish = newSquish;
            UpdateSquish();
        }
    }

    void UpdateSquish()
    {
        mat.SetFloat("_SquishFade", currentSquish);
    }
}
