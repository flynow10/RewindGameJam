using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int firstLevelBuildIndex = 2;
        int lastLevelBuildIndex = 6;
        int buildIndex;
        if (!PlayerPrefs.HasKey("LevelSelectType"))
        {
            buildIndex = Random.Range(4, 6);
        }
        else
        {
            switch (PlayerPrefs.GetString("LevelSelectType"))
            {
                case "shuffle":
                {
                    buildIndex = Random.Range(firstLevelBuildIndex, lastLevelBuildIndex+1);
                    break;
                }
                case "loop":
                {
                    if (Global.BUILDINDEX < firstLevelBuildIndex)
                    {
                        Global.BUILDINDEX = firstLevelBuildIndex;
                    }

                    buildIndex = Global.BUILDINDEX;
                    Global.BUILDINDEX++;
                    if (Global.BUILDINDEX > lastLevelBuildIndex)
                    {
                        Global.BUILDINDEX = firstLevelBuildIndex;
                    }
                    break;
                }
                case "select":
                {
                    buildIndex = PlayerPrefs.GetInt("buildIndex");
                    break;
                }
                default:
                {
                    buildIndex = Random.Range(firstLevelBuildIndex, lastLevelBuildIndex + 1);
                    break;
                }
            }
        }
        SceneManager.LoadScene(buildIndex, LoadSceneMode.Additive);
        Global.WINNER = false;
    }
}
