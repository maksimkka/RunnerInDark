using UnityEditor;
using UnityEngine;

namespace Code.Tools
{
    public class PlayerPrefsCleaner : EditorWindow
    {
        [MenuItem("Tools/Clean PlayerPrefs")]
        public static void ShowWindow()
        {
            var window = (PlayerPrefsCleaner)EditorWindow.GetWindow(typeof(PlayerPrefsCleaner));
            window.titleContent = new GUIContent("PlayerPrefs Cleaner");
            window.Show();
        }

        private void OnGUI()
        {
            GUILayout.Label("This tool will clear all PlayerPrefs data. Are you sure you want to proceed?", EditorStyles.wordWrappedLabel);

            if (GUILayout.Button("Clear PlayerPrefs"))
            {
                PlayerPrefs.DeleteAll();
                PlayerPrefs.Save();
            }
        }
    }
}