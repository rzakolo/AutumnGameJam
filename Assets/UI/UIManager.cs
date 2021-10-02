using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject Menu;
    public GameObject igrok;
    public GameObject StartWindow;
    public GameObject Tutorial;
    public GameObject credits;
    public GameObject lostWindow;
    public GameObject winWindow;
    private AudioSource music;
    public Slider volumeSlider;
    public GameObject[] pages;
    public Text moneyText;
    public Image hpBar;
    public Text winText;
    public Text dayInformer;
    [Range(0, 1)]
    float timer = 0;
    [SerializeField] Gradient dayInformerColor;

    public bool isMenu;

    public int i;

    void Start()
    {
        music = Camera.main.GetComponent<AudioSource>();
        music.volume = 0.5f;
        volumeSlider.value = music.volume;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) isMenu = !isMenu;
        if (isMenu)
        {
            Menu.SetActive(true);
            Time.timeScale = 0;
            igrok.SetActive(false);
            if (!Tutorial.activeInHierarchy && !credits.activeInHierarchy)
            {
                volumeSlider.gameObject.SetActive(true);
            }
            else
                volumeSlider.gameObject.SetActive(false);
        }
        else
        {
            Menu.SetActive(false);
            Time.timeScale = 1;
            igrok.SetActive(true);
        }
        if (lostWindow.activeInHierarchy || winWindow.activeInHierarchy)
            Time.timeScale = 0;
        if (timer <= 1)
        {
            dayInformer.color = dayInformerColor.Evaluate(timer);
            timer += Time.deltaTime;
        }
    }
    public void SetDayText(string text)
    {
        timer = 0;
        dayInformer.text = text;
    }
    public void VolumeChanged()
    {
        music.volume = volumeSlider.value;
    }
    public void LoseGame()
    {
        lostWindow.SetActive(true);
    }
    public void WinGame()
    {
        winWindow.SetActive(true);
    }

    public void tutorial()
    {
        StartWindow.SetActive(false);
        Tutorial.SetActive(true);
    }

    public void menuO()
    {
        StartWindow.SetActive(true);
        Tutorial.SetActive(false);
        credits.SetActive(false);
    }

    public void creditsO()
    {
        StartWindow.SetActive(false);
        credits.SetActive(true);
    }

    public void next()
    {
        pages[i].SetActive(false);
        i += 1;
        if (i < pages.Length)
        {
            pages[i].SetActive(true);
        }
        else
        {
            i = 0;
            pages[i].SetActive(true);
        }

    }

    public void back()
    {
        pages[i].SetActive(false);
        i -= 1;
        if (i >= 0)
        {
            pages[i].SetActive(true);
        }
        else
        {
            i = pages.Length - 1;
            pages[i].SetActive(true);
        }

    }

    public void Restart()
    {
        lostWindow.SetActive(false);
        winWindow.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        Application.Quit();
    }
    public void play()
    {
        isMenu = false;
    }
}
