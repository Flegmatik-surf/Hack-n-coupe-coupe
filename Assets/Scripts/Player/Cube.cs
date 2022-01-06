using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    
    float duration = 3f;



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("a")){
            StartCoroutine(Jump());
        }
    }


    
    IEnumerator Jump()
    {
        Vector3 start = transform.position;
        Vector3 finish = start + transform.forward * 1f;
        float animation = 0f;
        while (animation < duration)
        {
            animation += Time.deltaTime;
            transform.position = MathParabola.Parabola(start, finish, duration, animation / duration);


            yield return null;
        }
        yield return null;
    }
}
