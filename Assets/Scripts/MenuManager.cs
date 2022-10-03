using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Toggle MuteToggle, PlayTutorialToggle;

    public bool MainMenuOpen = true, SettingsOpen, CreditsOpen, LevelSelectTypeOpen, LevelSelectOpen, Tutorial1Open, Tutorial2Open, Tutorial3Open, Tutorial4Open;
    public Transform MainMenuTransform, SettingsTransform, CreditsTransform, LevelSelectTypeTransform, LevelSelectTransform, Tutorial1Transform, Tutorial2Transform, Tutorial3Transform, Tutorial4Transform;
    private Vector3 MainMenuLocation, SettingsLocation, CreditsLocation, LevelSelectTypeLocation, LevelSelectLocation, Tutorial1Location, Tutorial2Location, Tutorial3Location, Tutorial4Location;

    public float speed = 50;
    // Start is called before the first frame update
    void Start()
    {
        MainMenuLocation = MainMenuTransform.position;
        SettingsLocation = SettingsTransform.position;
        CreditsLocation = CreditsTransform.position;
        LevelSelectTypeLocation = LevelSelectTypeTransform.position;
        LevelSelectLocation = LevelSelectTransform.position;
        Tutorial1Location = Tutorial1Transform.position;
        Tutorial2Location = Tutorial2Transform.position;
        Tutorial3Location = Tutorial3Transform.position;
        Tutorial4Location = Tutorial4Transform.position;
        if (PlayerPrefs.HasKey("PlayTutorial") && PlayerPrefs.GetInt("PlayTutorial") == 0)
        {
            PlayTutorialToggle.isOn = false;
        }
        else
        {
            PlayerPrefs.SetInt("PlayTutorial", 1);
        }
        if (PlayerPrefs.HasKey("Mute") && PlayerPrefs.GetInt("Mute") == 1)
        {
            MuteToggle.isOn = true;
        }
        else
        {
            PlayerPrefs.SetInt("Mute", 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (MainMenuOpen)
        {
            MainMenuTransform.position =
                Vector3.MoveTowards(MainMenuTransform.position, MainMenuLocation, speed * Time.deltaTime);
                MainMenuTransform.gameObject.SetActive(true);
        }
        else
        {
            MainMenuTransform.position =
                Vector3.MoveTowards(MainMenuTransform.position,
                    new Vector3(MainMenuLocation.x * -1, MainMenuLocation.y, MainMenuLocation.z),
                    speed * Time.deltaTime);
            if (Vector3.Distance(new Vector3(MainMenuLocation.x * -1, MainMenuLocation.y, MainMenuLocation.z),
                MainMenuTransform.position) < 0.1f)
            {
                MainMenuTransform.gameObject.SetActive(false);
            }
        }
        if (SettingsOpen)
        {
            SettingsTransform.position =
                Vector3.MoveTowards(SettingsTransform.position, SettingsLocation, speed * Time.deltaTime);
            SettingsTransform.gameObject.SetActive(true);
        }
        else
        {
            SettingsTransform.position =
                Vector3.MoveTowards(SettingsTransform.position,
                    new Vector3(-1361, SettingsLocation.y, SettingsLocation.z),
                    speed * Time.deltaTime);
            if (Vector3.Distance(new Vector3(-1361, SettingsLocation.y, SettingsLocation.z),
                SettingsTransform.position) < 0.1f)
            {
                SettingsTransform.gameObject.SetActive(false);
            }
        }
        if (CreditsOpen)
        {
            CreditsTransform.position =
                Vector3.MoveTowards(CreditsTransform.position, CreditsLocation, speed * Time.deltaTime);
            CreditsTransform.gameObject.SetActive(true);
        }
        else
        {
            CreditsTransform.position =
                Vector3.MoveTowards(CreditsTransform.position,
                    new Vector3(-1361, CreditsLocation.y, CreditsLocation.z),
                    speed * Time.deltaTime);
            if (Vector3.Distance(new Vector3(-1361, CreditsLocation.y, CreditsLocation.z),
                CreditsTransform.position) < 0.1f)
            {
                CreditsTransform.gameObject.SetActive(false);
            }
        }
        if (LevelSelectTypeOpen)
        {
            LevelSelectTypeTransform.position =
                Vector3.MoveTowards(LevelSelectTypeTransform.position, LevelSelectTypeLocation, speed * Time.deltaTime);
            LevelSelectTypeTransform.gameObject.SetActive(true);
        }
        else
        {
            LevelSelectTypeTransform.position =
                Vector3.MoveTowards(LevelSelectTypeTransform.position,
                    new Vector3(-1361, LevelSelectTypeLocation.y, LevelSelectTypeLocation.z),
                    speed * Time.deltaTime);
            if (Vector3.Distance(new Vector3(-1361, LevelSelectTypeLocation.y, LevelSelectTypeLocation.z),
                LevelSelectTypeTransform.position) < 0.1f)
            {
                LevelSelectTypeTransform.gameObject.SetActive(false);
            }
        }
        if (LevelSelectOpen)
        {
            LevelSelectTransform.position =
                Vector3.MoveTowards(LevelSelectTransform.position, LevelSelectLocation, speed * Time.deltaTime);
            LevelSelectTransform.gameObject.SetActive(true);
        }
        else
        {
            LevelSelectTransform.position =
                Vector3.MoveTowards(LevelSelectTransform.position,
                    new Vector3(-1361, LevelSelectLocation.y, LevelSelectLocation.z),
                    speed * Time.deltaTime);
            if (Vector3.Distance(new Vector3(-1361, LevelSelectLocation.y, LevelSelectLocation.z),
                LevelSelectTransform.position) < 0.1f)
            {
                LevelSelectTransform.gameObject.SetActive(false);
            }
        }
        if (Tutorial1Open)
        {
            Tutorial1Transform.position =
                Vector3.MoveTowards(Tutorial1Transform.position, Tutorial1Location, speed * Time.deltaTime);
            Tutorial1Transform.gameObject.SetActive(true);
        }
        else
        {
            Tutorial1Transform.position =
                Vector3.MoveTowards(Tutorial1Transform.position,
                    new Vector3(Tutorial1Location.x, -1080, Tutorial1Location.z),
                    speed * Time.deltaTime);
            if (Vector3.Distance(new Vector3(Tutorial1Location.x, -1080, Tutorial1Location.z),
                Tutorial1Transform.position) < 0.1f)
            {
                Tutorial1Transform.gameObject.SetActive(false);
            }
        }
        if (Tutorial2Open)
        {
            Tutorial2Transform.position =
                Vector3.MoveTowards(Tutorial2Transform.position, Tutorial2Location, speed * Time.deltaTime);
            Tutorial2Transform.gameObject.SetActive(true);
        }
        else
        {
            Tutorial2Transform.position =
                Vector3.MoveTowards(Tutorial2Transform.position,
                    new Vector3(Tutorial2Location.x, -1080, Tutorial2Location.z),
                    speed * Time.deltaTime);
            if (Vector3.Distance(new Vector3(Tutorial2Location.x, -1080, Tutorial2Location.z),
                Tutorial2Transform.position) < 0.1f)
            {
                Tutorial2Transform.gameObject.SetActive(false);
            }
        }
        if (Tutorial3Open)
        {
            Tutorial3Transform.position =
                Vector3.MoveTowards(Tutorial3Transform.position, Tutorial3Location, speed * Time.deltaTime);
            Tutorial3Transform.gameObject.SetActive(true);
        }
        else
        {
            Tutorial3Transform.position =
                Vector3.MoveTowards(Tutorial3Transform.position,
                    new Vector3(Tutorial3Location.x, -1080, Tutorial3Location.z),
                    speed * Time.deltaTime);
            if (Vector3.Distance(new Vector3(Tutorial3Location.x, -1080, Tutorial3Location.z),
                Tutorial3Transform.position) < 0.1f)
            {
                Tutorial3Transform.gameObject.SetActive(false);
            }
        }
        if (Tutorial4Open)
        {
            Tutorial4Transform.position =
                Vector3.MoveTowards(Tutorial4Transform.position, Tutorial4Location, speed * Time.deltaTime);
            Tutorial4Transform.gameObject.SetActive(true);
        }
        else
        {
            Tutorial4Transform.position =
                Vector3.MoveTowards(Tutorial4Transform.position,
                    new Vector3(Tutorial4Location.x, -1080, Tutorial4Location.z),
                    speed * Time.deltaTime);
            if (Vector3.Distance(new Vector3(Tutorial4Location.x, -1080, Tutorial4Location.z),
                Tutorial4Transform.position) < 0.1f)
            {
                Tutorial4Transform.gameObject.SetActive(false);
            }
        }
    }

    public void ChangedMute()
    {
        PlayerPrefs.SetInt("Mute", MuteToggle.isOn ? 1 : 0);
    }

    public void ChangedTutorial()
    {
        PlayerPrefs.SetInt("PlayTutorial", PlayTutorialToggle.isOn ? 1 : 0);
    }

    void DisableAll()
    {
        MainMenuOpen = false;
        SettingsOpen = false;
        CreditsOpen = false;
        LevelSelectTypeOpen = false;
        LevelSelectOpen = false;
        Tutorial1Open = false;
        Tutorial2Open = false;
        Tutorial3Open = false;
        Tutorial4Open = false;
    }
    public void OpenSettings()
    {
        DisableAll();
        SettingsOpen = true;
    }

    public void OpenMainMenu()
    {
        DisableAll();
        MainMenuOpen = true;
    }

    public void OpenLevelSelectType()
    {
        DisableAll();
        LevelSelectTypeOpen = true;
    }

    public void Tutorial1()
    {
        DisableAll();
        Tutorial1Open = true;
    }
    public void Tutorial2()
    {
        DisableAll();
        Tutorial2Open = true;
    }
    public void Tutorial3()
    {
        DisableAll();
        Tutorial3Open = true;
    }

    public void Tutorial4()
    {
        DisableAll();
        Tutorial4Open = true;
    }
    void OpenLevelSelect()
    {
        DisableAll();
        LevelSelectOpen = true;
    }

    public void ExitTutorial()
    {
        PlayerPrefs.SetInt("PlayTutorial", 1);
        PlayTutorialToggle.isOn = true;
        OpenLevelSelectType();
    }
    public void SelectLevelType(string type)
    {
        PlayerPrefs.SetString("LevelSelectType", type);
        if (type.Equals("select"))
        {
            OpenLevelSelect();
            return;
        }
        DisableAll();
        Play();
    }

    public void SelectLevel(int buildindex)
    {
        PlayerPrefs.SetInt("buildIndex", buildindex);
        Play();
    }
    public void Play()
    {
        if (PlayTutorialToggle.isOn)
        {
            PlayerPrefs.SetInt("PlayTutorial", 0);
            PlayTutorialToggle.isOn = false;
            Tutorial1();
            return;
        }
        Global.PLAYER1 = 0;
        Global.PLAYER2 = 0;
        Global.BUILDINDEX = 0;
        SceneManager.LoadScene(1);
    }

    public void OpenCredits()
    {
        DisableAll();
        CreditsOpen = true;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
