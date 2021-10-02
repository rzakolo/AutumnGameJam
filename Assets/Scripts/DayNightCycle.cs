using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Range(0, 1)]
    public float TimeOfDay;
    public float DayDuration = 30f;
    public bool setDay = false;
    public bool setNight = false;
    public bool skipDay = false;
    public bool levelClean = false;
    public float dayStartTime;
    public float DayLenght;

    public AnimationCurve SunCurve;
    public AnimationCurve MoonCurve;
    public AnimationCurve SkyboxCurve;

    public Material DaySkybox;
    public Material NightSkybox;

    public ParticleSystem Stars;

    public Light Sun;
    public Light Moon;

    private float sunIntensity;
    private float moonIntensity;

    private void Start()
    {
        sunIntensity = Sun.intensity;
        moonIntensity = Moon.intensity;
        TimeOfDay = dayStartTime;
        InvokeRepeating(nameof(UpdateGI), 1, 1);
    }

    private void Update()
    {
        //пропуск дня и переход в ночь
        if (skipDay)
        {
            DayDuration = 15;
            DayNight();
        }
        //установка параметров дня
        if (setDay && TimeOfDay >= dayStartTime && TimeOfDay < 0.45)
        {
            DayDuration = DayLenght;
            setDay = false;
        }
        //обычный день
        if (!setDay && DayDuration == DayLenght)
        {
            DayNight();
        }
        //установка параметров ночи
        if (!setDay && !levelClean && TimeOfDay >= 0.98f)
        {
            setNight = true;
            DayDuration = 120;
            levelClean = true;
        }
        //переход между ночью и днем
        if (setDay && !levelClean)
        {
            DayDuration = 30;
            DayNight();
        }
    }
    public void SetDay()
    {
        setNight = false;
        setDay = true;
    }

    private void DayNight()
    {
        if (setNight == false || setDay == false)
        {
            TimeOfDay += Time.deltaTime / DayDuration;
            if (TimeOfDay >= 1) TimeOfDay -= 1;
        }

        // Настройки освещения (skybox и основное солнце)
        RenderSettings.skybox.Lerp(NightSkybox, DaySkybox, SkyboxCurve.Evaluate(TimeOfDay));
        RenderSettings.sun = SkyboxCurve.Evaluate(TimeOfDay) > 0.1f ? Sun : Moon;
        //DynamicGI.UpdateEnvironment();

        // Прозрачность звёзд
        //var mainModule = Stars.main;
        //mainModule.startColor = new Color(1, 1, 1, 1 - SkyboxCurve.Evaluate(TimeOfDay));

        // Поворот луны и солнца
        Sun.transform.localRotation = Quaternion.Euler(TimeOfDay * 360f, 180, 0);
        Moon.transform.localRotation = Quaternion.Euler(TimeOfDay * 360f + 180f, 180, 0);

        // Интенсивность свечения луны и солнца
        Sun.intensity = sunIntensity * SunCurve.Evaluate(TimeOfDay);
        Moon.intensity = moonIntensity * MoonCurve.Evaluate(TimeOfDay);

        if (skipDay && TimeOfDay >= 0.98f)
        {
            skipDay = false;
        }

    }
    private void UpdateGI()
    {
        DynamicGI.UpdateEnvironment();
    }
}