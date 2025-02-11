using UnityEditor;
using UnityEngine;
using System.IO;

public class FolderStructureCreator : EditorWindow {
    [MenuItem("Tools/Create Folder Structure")]
    public static void CreateFolders() {
        string basePath = Application.dataPath + "/LoupzyDev";

        string[] folders = new string[]
        {
            "Art/Materials",
            "Art/Models",
            "Art/Textures",
            "Audio/Music",
            "Audio/Sound",
            "Code/Scripts",
            "Code/Shaders",
            "Level/Prefabs",
            "Level/Scenes",
            "Level/UI"
        };

        foreach (string folder in folders) {
            string fullPath = Path.Combine(basePath, folder);
            if (!Directory.Exists(fullPath)) {
                Directory.CreateDirectory(fullPath);
                Debug.Log("Carpeta creada: " + fullPath);
            }
        }

        AssetDatabase.Refresh();
    }
}
