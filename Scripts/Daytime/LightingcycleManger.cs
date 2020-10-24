/*LightingcycleManger.cs
 * 10-24-2020
 * Light cycle manger - Not implacted yet - the control for Day and Night cycles!
 * RPG.DayNight - Not yet added
 */
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Presets;
using UnityEngine;

[ExecuteInEditMode]
public class LightingcycleManger : MonoBehaviour
{
    //Referances
    [SerializeField] private Light directionalLight;
    [SerializeField] private Lightingpresets lPreset;
    //Vars
    [SerializeField, Range(0,24)] private float timeOfDay;
    [SerializeField] private bool FreezeTime = false;
    [SerializeField] private float timeDelay = 360f;
    [SerializeField] private float Lightcomplete = 0;
    [SerializeField] private float currentTimeofDay;
    [SerializeField] private float secondsinFullDay = 120f;
    [SerializeField] private float timeMulitiplier=1f;
    //Update function
    private void Update()
    {
        Lightcomplete = timeOfDay / 24f;
        if (lPreset == null)
            return;
        if (Application.isPlaying)
        {
            if (FreezeTime)
            {
                UpdateLighting(timeOfDay);
                TimeZone();
            }
            if (!FreezeTime)
            {
                timeOfDay += Time.deltaTime;
                timeOfDay %= 24;
                UpdateLighting(timeOfDay / 24f);
                TimeZone();
            }
        }
        if (Application.isEditor)
        {
            UpdateLighting(timeOfDay / 24f);
            TimeZone();
        }
        
    }
    //Not Working!
    private void TimeZone()
    {
        currentTimeofDay += (Time.deltaTime / secondsinFullDay) * timeMulitiplier;
        if (currentTimeofDay >= 1) currentTimeofDay = 0;
    }
    //Notworking! will be fixed!
    private void UpdateLighting(float timePercent)
    {
        //RenderSettings.ambientLight = lPreset.AmbientColor.Evaluate(lightcomp);
        //RenderSettings.fogColor = lPreset.FogColor.Evaluate(lightcomp);
        if (directionalLight != null)
        {
            //directionalLight.color = lPreset.DirectionalColor.Evaluate(lightcomp);
            //directionalLight.intensity = lPreset.SunIntensity.Evaluate(lightcomp);
            //redo this for current time of day!
            directionalLight.transform.localRotation = Quaternion.Euler(new Vector3((currentTimeofDay * 360f) - 90f, 170f, 0));
        }
    }
    //On Validate will check each time its loaded up - this will make sure theres a light is set.
    private void OnValidate()
    {
        if (directionalLight != null)
            return;
        if (RenderSettings.sun != null)
        {
            directionalLight = RenderSettings.sun;
        }
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    directionalLight = light;
                    return;
                }
            }
        }
    }
}
