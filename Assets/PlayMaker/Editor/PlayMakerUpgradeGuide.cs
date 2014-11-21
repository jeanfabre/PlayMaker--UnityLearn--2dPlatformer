using UnityEngine;
using UnityEditor;

namespace HutongGames.PlayMakerEditor
{
    /// <summary>
    /// Shows critical upgrade info for each version
    /// </summary>
    public class PlayMakerUpgradeGuide : EditorWindow
    {
        private const string urlReleaseNotes = "https://hutonggames.fogbugz.com/default.asp?W311";

        private bool showOnLoad;
        private Vector2 scrollPosition;

        public void OnEnable()
        {
            title = "PlayMaker";
            position = new Rect(100,100,350,400);
            minSize = new Vector2(350,200);

            showOnLoad = EditorPrefs.GetBool(EditorPrefStrings.ShowUpgradeGuide, true);
        }

        public void OnGUI()
        {
            FsmEditorStyles.Init();

            FsmEditorGUILayout.ToolWindowLargeTitle(this, "Upgrade Guide");

            scrollPosition = GUILayout.BeginScrollView(scrollPosition);

            EditorGUILayout.HelpBox("Always BACKUP projects before updating!", MessageType.Error);

            GUILayout.Label("Version 1.8.0", EditorStyles.boldLabel);
            EditorGUILayout.HelpBox("FSMs saved with 1.8.0 cannot be opened in earlier versions of PlayMaker without data loss! Please BACKUP your projects!", MessageType.Warning);
            GUILayout.Label("Upgrading to Unity 5", EditorStyles.boldLabel);
            EditorGUILayout.HelpBox("Third party actions and addons that use GameObject properties removed from Unity 5 will need updating! Please contact the original author for help.", MessageType.Warning);

            GUILayout.EndScrollView();

            GUILayout.FlexibleSpace();

            EditorGUI.BeginChangeCheck();
            var dontShowAgain = GUILayout.Toggle(!showOnLoad, "Don't Show Again Until Next Update");
            if (EditorGUI.EndChangeCheck())
            {
                showOnLoad = !dontShowAgain;
                EditorPrefs.SetBool(EditorPrefStrings.ShowUpgradeGuide, showOnLoad);
            }

            if (GUILayout.Button("Online Release Notes"))
            {
                Application.OpenURL(urlReleaseNotes);
            }
        }
    }
}