using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderBinder : MonoBehaviour
{
    public Renderer target;
    public int materialID = 0;
    public string propertyName = "_property";

    public bool applyMatToSliderOnStart = true;

    Material mat;
    int propertyID;

    private void Start()
    {
        mat = target.materials[materialID];
        propertyID = Shader.PropertyToID(propertyName);

        if (applyMatToSliderOnStart)
            GetComponent<Slider>().value = mat.GetFloat(propertyID);
        else
            OnValueChanged(GetComponent<Slider>().value);
    }

    public void OnValueChanged( float value )
    {
        mat.SetFloat(propertyID, value);
    }
}
