using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPlaceSamples : MonoBehaviour
{
    public float rotationSpeed = 5;

    void Update()
    {
        transform.eulerAngles = Vector3.up * Mathf.Deg2Rad * rotationSpeed * Time.time;
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
