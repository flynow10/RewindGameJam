using Assets.Scripts;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor
{
    [CustomEditor(typeof(FlagPole))]
    public class FlagPoleEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            FlagPole flagPole = (FlagPole) target;
            base.OnInspectorGUI();

            if (GUILayout.Button("Look At Camera"))
            {
                flagPole.LookAtCamera();
            }
        }
    }
}
