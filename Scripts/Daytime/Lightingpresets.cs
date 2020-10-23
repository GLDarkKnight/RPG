/*Dustin Huff Lighting Presets
 * AmbientColor designed for Sun Color 
 * DirectionalColor designed for the Directional color of the light (Sun Light)
 * FogColor designed for Ambient Fog
 *       
 *  Copyright 2020 Dustin Huff AKA Grupy Grampa Paints Studios (Name Pending)
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Lighting Presets", menuName ="Scriptables/Lighting Presets", order =1)]
//Lighting Preset as Scriptable Object
public class Lightingpresets : ScriptableObject
{
    public Gradient AmbientColor;
    public Gradient DirectionalColor;
    public Gradient FogColor;
    public AnimationCurve SunIntensity;
    public AnimationCurve FogIntensity;
}
