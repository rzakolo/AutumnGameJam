using System;
using System.Text.RegularExpressions;
using UnityEngine;
internal abstract class NonPlayerCharacter : MonoBehaviour
{
    protected GameManager gameManager;
    public string text = "<b>Укажите</b> <color=#ffea00>имя</color> объекта";
    public int textSize = 14;
    public Font textFont;
    public Color textColor = Color.white;
    public float textHeight = 1.15f;
    public bool showShadow = true;
    public bool isPumpkin = false;
    public float pumpkinHealth;
    public float waterHydration;
    public float growthSpeed;
    public Color shadowColor = new Color(0, 0, 0, 0.5f);
    public Vector2 shadowOffset = new Vector2(1, 1);
    private string textShadow;
    private bool incorrectAction;
    [Range(0, 1)]
    private float time;
    public Gradient incorrectColorGradient;

    public abstract void Action();
    protected void IncorrectAction()
    {
        incorrectAction = true;
    }
    private void Update()
    {
        pumpkinHealth = gameManager.pumpkin.health;
        waterHydration = gameManager.pumpkin.waterHydration;
        growthSpeed = gameManager.pumpkin.growthSpeed;
    }
    void Awake()
    {
        enabled = false;
        TextShadowReady();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void TextShadowReady()
    {
        textShadow = Regex.Replace(text, "<color[^>]+>|</color>", string.Empty);
    }

    void OnGUI()
    {
        GUI.depth = 9999;
        GUIStyle style = new GUIStyle();
        style.fontSize = textSize;
        style.richText = true;
        if (textFont) style.font = textFont;
        style.normal.textColor = textColor;
        style.alignment = TextAnchor.MiddleCenter;
        if (incorrectAction)
        {
            style.normal.textColor = incorrectColorGradient.Evaluate(time);
            time += Time.deltaTime;
            if (time >= 1)
            {
                incorrectAction = false;
                time = 0;
            }
        }

        GUIStyle shadow = new GUIStyle();
        shadow.fontSize = textSize;
        shadow.richText = true;
        if (textFont) shadow.font = textFont;
        shadow.normal.textColor = shadowColor;
        shadow.alignment = TextAnchor.MiddleCenter;

        Vector3 worldPosition = new Vector3(transform.position.x, transform.position.y + textHeight, transform.position.z);
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);
        screenPosition.y = Screen.height - screenPosition.y;
        TextShadowReady();
        if (showShadow) GUI.Label(new Rect(screenPosition.x + shadowOffset.x, screenPosition.y + shadowOffset.y, 0, 0), textShadow, shadow);
        GUI.Label(new Rect(screenPosition.x, screenPosition.y, 0, 0), text, style);
        //if (isPumpkin)
        //{
        //    pumpkinHealth = GUI.HorizontalSlider(new Rect(screenPosition.x, screenPosition.y, 0, 0), pumpkinHealth, 0, 100);
        //}

    }

    void OnBecameVisible()
    {
        enabled = true;
    }

    void OnBecameInvisible()
    {
        enabled = false;
    }
}