using System.IO;
using UnityEditor;
using UnityEngine;

public static class CustomAssetUtility {

    public static void CreateAsset<T>() where T : ScriptableObject
    {
        
        //경로를 반환한다. Selection.activeObject가 null이면 ""를 반환한다.
        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        Debug.Log("에셋 생성을 위해 우클릭한 경로" + path);
        //경로에 파일이 없으면
        if (path == "")
        {
            path = "Assets";
        }
        //경로에 있는 파일의 확장명을 가져온다. 그것이 ""가 아니면
        else if (Path.GetExtension(path) != "")
        {   //경로에 있는 파일의 이름을 가져와 ""로 대체한다.(없앤다)
            path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
            Debug.Log("확장명이 이미 있어서 이름이 삭제된 경로" + path);
        }
        CreateAsset<T>(path);
    }

    public static void CreateAsset<T>(string path) where T : ScriptableObject
    {
        T asset = ScriptableObject.CreateInstance<T>();
        
        string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/New " + typeof(T).ToString() + ".asset");

        AssetDatabase.CreateAsset(asset, assetPathAndName);

        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow(); // 뭔지 모르겠음.
        Selection.activeObject = asset; // 우클릭 메뉴창에서 선택된 메뉴에 에셋의 레퍼런스를 넣는다. Selection 목록은 static 처럼 에디터상에서 유지되며 관리되는건가?
    }
}
