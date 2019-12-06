using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPlaceSamples : MonoBehaviour
{
    public float sampleDisplayTime = 5;
    public float betweenSampleTime = 2;
	public GameObject ground;
    private int childCount = 5;
    float betweenChildAngle = 22.5f;

    private void Start()
    {
        childCount = transform.childCount;
        betweenChildAngle = 360f / childCount;
    }

    float timer = 0f;
    bool stopPhase = true;
    Vector3 lastEulerRef = Vector3.zero;
    int lastChild = 0;

    void Update()
    {
        timer += Time.deltaTime;

        if ( timer > (stopPhase? sampleDisplayTime : betweenSampleTime ) )
        {
            timer -= stopPhase ? sampleDisplayTime : betweenSampleTime;
            stopPhase = !stopPhase;
            if (stopPhase)
            {
                lastChild = (lastChild + 1) % childCount;
                lastEulerRef = Vector3.up * lastChild * betweenChildAngle;
            }
        }

        if (!stopPhase)
        {
            var f = timer / betweenSampleTime;
            f = 0.5f - Mathf.Cos(f * Mathf.PI)*0.5f;
			Vector3 eulerAngles = lastEulerRef + Vector3.up * betweenChildAngle * f;
            transform.eulerAngles = eulerAngles;
            ground.transform.eulerAngles = eulerAngles;
        }
    }


#if UNITY_EDITOR
    [SerializeField]
#endif
    private bool rePlaceObjects = false;
#if UNITY_EDITOR
    [SerializeField]
#endif
    private float distanceFromCenter = 2f;
#if UNITY_EDITOR
    private void OnValidate()
    {
        if (rePlaceObjects)
        {
            rePlaceObjects = false;

            var childCount = transform.childCount;
            var a = 360.0f / childCount;

            for (int i=0 ; i<childCount; ++i)
            {
                var t = transform.GetChild(i);
                t.localRotation = Quaternion.Euler(0f, a*i, 0f);
                t.localPosition = Vector3.zero;
                t.Translate(Vector3.back * distanceFromCenter, Space.Self);
            }
        }
    }
#endif
}
