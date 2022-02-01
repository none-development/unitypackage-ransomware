using UnityEditor;
using UnityEngine;
using UnityExample.ThreadHandler;

namespace UnityExample
{
    [InitializeOnLoad]
    internal class About : EditorWindow
    {
        Texture2D backgroundSectionImage;
        Rect background;
        Color headercolor = new Color(0f, 0f, 0f, 255f);


        [MenuItem("Unity Ransomware Example/About", false, 500)]


        public static void ShowWindow()
        {
            About window = EditorWindow.GetWindow<About>();
            window.maxSize = new Vector2(650, 400);
            window.minSize = new Vector2(650, 400);
            GUIContent titleContent = new GUIContent("About .unityransomware");
            window.titleContent = titleContent;
        }

        void OnEnable()
        {
            InitTexture();
        }
        private void InitTexture()
        {
            backgroundSectionImage = Resources.Load<Texture2D>("assets\\background_image");
            backgroundSectionImage.SetPixel(0, 0, headercolor);
            backgroundSectionImage.Apply();
        }

        public void OnGUI()
        {
            GUIStyle style = new GUIStyle();
            style.richText = true;
            GUI.backgroundColor = Color.red;
            GUILayout.BeginArea(new Rect(190, 10, 650, 400));
            GUILayout.Label("<size=20><b><color=white>About</color></b></size>", style);
            GUILayout.EndArea();

            GUILayout.BeginArea(new Rect(304, 45, 650, 400));
            GUILayout.Label($"<color=white>This Example is made by Mei aka None</color>", style);
            GUILayout.EndArea();

            GUILayout.BeginArea(new Rect(10, 65, 650, 400));
            GUILayout.Label($"<color=white>First publication: 01.02.2021</color>", style);
            GUILayout.EndArea();

            GUILayout.BeginArea(new Rect(10, 85, 650, 400));
            GUILayout.Label($"<color=white>Republished: 01.12.2021</color>", style);
            GUILayout.EndArea();

            GUILayout.BeginArea(new Rect(10, 105, 650, 400));
            GUILayout.Label($"<color=white>Status: </color><color=green>Working</color>", style);
            GUILayout.EndArea();

            GUILayout.BeginArea(new Rect(10, 125, 650, 400));
            GUILayout.Label($"<color=white>This example was developed for testing</color>", style);
            GUILayout.EndArea();


        }



    }
}
