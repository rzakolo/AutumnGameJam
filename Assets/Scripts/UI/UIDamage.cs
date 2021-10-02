using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDamage : MonoBehaviour
{
    TextMeshPro textMesh;
    static GameObject prefab;
    float timer = 1f;
    Color textColor;
    private void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
    }
    private void Update()
    {
        transform.position += new Vector3(0, 2f, 0) * Time.deltaTime;
        if (timer > timer * 0.5f)
        {
            transform.localScale += Vector3.one * 0.5f * Time.deltaTime;
        }
        else
        {
            transform.localScale -= Vector3.one * Time.deltaTime;
        }
        if (timer < 0)
        {
            textColor.a -= 3f * Time.deltaTime;
            textMesh.color = textColor;
            if (textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
        timer -= Time.deltaTime;
    }
    public static UIDamage Create(Vector3 position, string text)
    {
        prefab = Resources.Load<GameObject>("textInformer");
        Transform uiTransform = Instantiate(prefab, position, Quaternion.identity).transform;
        UIDamage damagePopup = uiTransform.GetComponent<UIDamage>();
        damagePopup.SetText(text);
        return damagePopup;
    }
    public void SetText(string s)
    {
        textMesh.SetText(s);
        textColor = textMesh.color;
    }
}
