using System.Collections;
using System.Collections.Generic;
using System.IO;
using Assets.Scripts;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToolsWindow : EditorWindow
{
    [MenuItem("Tools/Tools 'n Things")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<ToolsWindow>("Tools 'n Things", true);
    }
    void OnGUI()
    {
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Lock To Camera"))
        {
            Transform position = Camera.main.transform;
            SceneView.lastActiveSceneView.AlignViewToObject(position);
            SceneView.lastActiveSceneView.Repaint();
        }

        if (GUILayout.Button("New Level"))
        {
            for (int i = 0; i > EditorBuildSettings.scenes.Length-1; i++)
            {
                EditorSceneManager.UnloadSceneAsync(i);
            }
            EditorSceneManager.OpenScene("Assets/Scenes/Main.unity");
            Scene Test = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Additive);
            Test.name = "Level " + (EditorBuildSettings.scenes.Length - 1);
            PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/OuterWalls.prefab"));
            GameObject spawnpoint1 = (GameObject)PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Player Spawn Point.prefab"));
            spawnpoint1.transform.position = new Vector3(2,0.5f,0);
            spawnpoint1.GetComponent<SpawnPoint>().Type = playerType.Chaser;
            GameObject spawnpoint2 = (GameObject)PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Player Spawn Point.prefab"));
            spawnpoint2.transform.position = new Vector3(-2, 0.5f, 0);
            spawnpoint2.GetComponent<SpawnPoint>().Type = playerType.Runner;
            EditorSceneManager.SaveScene(Test, "Assets/Scenes/" + Test.name + ".unity", true);
            var original = EditorBuildSettings.scenes;
            var newSettings = new EditorBuildSettingsScene[original.Length + 1];
            System.Array.Copy(original, newSettings, original.Length);
            var sceneToAdd = new EditorBuildSettingsScene("Assets/Scenes/" + Test.name + ".unity", true);
            newSettings[newSettings.Length - 1] = sceneToAdd;
            EditorBuildSettings.scenes = newSettings;
        }

        if (GUILayout.Button("Add Wall"))
        {
            Scene activeScene = SceneManager.GetActiveScene();
            SceneManager.SetActiveScene(SceneManager.GetSceneAt(1));
            GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
            wall.transform.position = new Vector3(0, 0.25f, 0);
            wall.GetComponent<MeshRenderer>().material =
                AssetDatabase.LoadAssetAtPath<Material>("Assets/Materials/Default.mat");
            SceneManager.SetActiveScene(activeScene);
        }
        GUILayout.EndHorizontal();
        GUIStyle style = new GUIStyle();
        style.fontSize = 13;
        style.alignment = TextAnchor.MiddleCenter;
        GUILayout.BeginHorizontal();
        GUILayout.Label("Playing and Building", style);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Start Game"))
        {
            if (SceneManager.sceneCount > 1)
            {
                PlayerPrefs.SetString("LevelSelectType", "select");
                PlayerPrefs.SetInt("buildIndex", SceneManager.GetSceneAt(1).buildIndex);
            }
            EditorSceneManager.OpenScene("Assets/Scenes/Main.unity", OpenSceneMode.Single);
            EditorApplication.EnterPlaymode();
        }

        if (GUILayout.Button("Take Screenshot"))
        {
            ScreenCapture.CaptureScreenshot("Assets/Images/Titlescreen2.png");
        }
        GUILayout.EndHorizontal();
    }
}
