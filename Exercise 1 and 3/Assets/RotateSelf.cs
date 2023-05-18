using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSelf : MonoBehaviour
{
    private bool rotate;
    MeshRenderer rend;
    Color original;
    public static float waitTime;
    public static bool wait;

    // Start is called before the first frame update
    void Start()
    {
        rotate = true;
        rend = GetComponentByChildren<MeshRenderer>();
        original = rend.material.color;
        waitTime = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (rotate)
        {
            transform.Rotate(Vector3.up * Time.deltaTime * 45f, Space.Self);
        }
    }

    public void InterruptRotation()
    {
        if (rotate)
        {
            rend.material.color = Color.green;
            StopAllCoroutines();
        }
        else
        {
            if (wait)
            {
                StartCoroutine(waitASecond(waitTime));
            }
            else
            {
                rend.material.color = original;
            }
        }
        rotate = !rotate;
    }

    IEnumerator waitASecond(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        rend.material.color = original;
    }
}
