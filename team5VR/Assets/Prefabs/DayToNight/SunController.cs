using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunController : MonoBehaviour
{
    [SerializeField] private Transform sunTransform;
    [SerializeField] private Light sun;
    [SerializeField] private float angleAtNoon;
    [SerializeField] private Vector3 hourMinuteSecond = new Vector3(6, 0, 0), hmsSunSet  = new Vector3(18,0,0);
    [SerializeField] public float speed = 100;
    [SerializeField] private float intensityAtNoon = 1f, intensityAtSunSet = 0.5f;
    [SerializeField] private Color fogColorDay = Color.gray, fogColorNight = Color.black;
    [NonSerialized] public float time;
    [SerializeField] private Transform starsTransform;
    [SerializeField] private Vector3 hmsStarsLight = new Vector3(19f, 30f, 0), hmsStarsExtinguish = new Vector3(03, 30, 0);
    [SerializeField] private float starsFadeInTime = 7200f, starsFadeOutTime = 7200f;

    private Material monMAt;
    private float intensity, rotation, prev_rotation = -1f, sunSet, sunRise, sunDayRatio, fade, timeLight, timeExitinguish;
    private Color tintColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
    private Vector3 dir;
    private Renderer rend;
    private AudioSource audio;
    public GameObject moon;
    public AudioClip[] clips;

    // Start is called before the first frame update
    void Start()
    {
        rend = starsTransform.GetComponent<ParticleSystem>().GetComponent<Renderer>();
        time = HMS_to_TIme(hourMinuteSecond.x, hourMinuteSecond.y, hourMinuteSecond.z);
        sunSet = HMS_to_TIme(hmsSunSet.x, hmsSunSet.y, hmsSunSet.z);
        sunRise = 86400f - sunSet;
        sunDayRatio = (sunSet - sunRise) / 43200;
        dir = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angleAtNoon), Mathf.Sin(Mathf.Deg2Rad * angleAtNoon), 0);
        starsFadeInTime /= speed;
        starsFadeOutTime /= speed;
        fade = 0;
        timeLight = HMS_to_TIme(hmsStarsLight.x, hmsStarsLight.y, hmsStarsLight.z);
        timeExitinguish = HMS_to_TIme(hmsStarsExtinguish.x, hmsStarsExtinguish.y, hmsStarsExtinguish.z);
        monMAt = moon.GetComponent<MeshRenderer>().material;
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime*speed;
        if (time > 86400f) time -= 86400;

        if (prev_rotation == -1f)
        {
            sunTransform.eulerAngles = Vector3.zero;
            prev_rotation = 0f;
        }
        else prev_rotation = rotation;

        //Rotate
        rotation = (time - 21600f) / 86400f * 360;
        sunTransform.Rotate(dir, rotation - prev_rotation);
        moon.transform.RotateAround(this.transform.position, dir, rotation - prev_rotation);

        
        if (time < sunRise) intensity = intensityAtSunSet * time / sunRise; //night-sunrise
        else if (time < 43200f) intensity = intensityAtSunSet + (intensityAtNoon - intensityAtSunSet) * (time - sunRise) / (43200 - sunRise); //sunrise-noon
        else if (time < sunSet) intensity = intensityAtNoon - (intensityAtNoon - intensityAtSunSet) * (time - 43200) / (sunSet - 43200); //noon-sunset
        else intensity = intensityAtSunSet - (1f - intensityAtSunSet) * (time - sunSet) / (86400 - sunSet); // sunset =night
        
        RenderSettings.fogColor = Color.Lerp(fogColorNight, fogColorDay, intensity * intensity);
        if (sun != null) sun.intensity = intensity;

        //at sunrise time fade out stars & moon
        if (Time_Falls_Between(time, timeLight, timeExitinguish)) 
        {
            fade += Time.deltaTime / starsFadeInTime;
            if (fade > 1) fade = 1f;
        }
        else
        {
            fade -= Time.deltaTime / starsFadeOutTime;
            if (fade < 0f) fade = 0f; 
        }
        
        monMAt.color = new Color(monMAt.color.r, monMAt.color.g, monMAt.color.b, fade);
        
        tintColor.a = fade;
        rend.material.SetColor("_TintColor", tintColor);

        //Change Clip
        if((sunRise < time &&  time < sunSet) && audio.isPlaying == false)
        {
            audio.volume = 1;
            audio.clip = clips[0];
            audio.Play();
        }
        else if((sunSet < time && time < timeLight) && audio.isPlaying == true)
        {
            audio.volume -= Time.deltaTime;
            if (audio.volume < Mathf.Epsilon) audio.Stop();
        }
        else if(((timeLight < time && time <86400) || (0< time && time < (sunRise - 3600f))) && audio.isPlaying == false)
        {
            audio.volume = 1;
            audio.clip = clips[1];
            audio.Play();
        }
        else if(((sunRise - 3600f) < time && time < sunRise) && audio.isPlaying == true)
        {
            audio.volume -= Time.deltaTime;
            if (audio.volume < Mathf.Epsilon) audio.Stop();
        }
    }

    private float HMS_to_TIme(float hour, float minute, float second)
    {
        return 3600 * hour + 60 * minute + second;
    }
    private bool Time_Falls_Between(float currentTime, float startTime, float endTime)
    {
        if(startTime < endTime)
        {
            if (currentTime >= startTime && currentTime <= endTime) return true;
            else return false;
        }
        else
        {
            if (currentTime < startTime && currentTime > endTime) return false;
            else return true;
        }
    }
}
