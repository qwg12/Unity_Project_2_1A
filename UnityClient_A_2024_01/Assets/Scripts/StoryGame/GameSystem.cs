using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;

public class GameSystemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GameSystem gameSystem = (GameSystem)target;

        if(GUILayout.Button("Resrt Story Models"))
        {
            gameSystem.ResetStoryModel();
        }
    }
}

    public class GameSystem : MonoBehaviour
{
    public StoryModel[] storyModels;


#if UNITY_EDITOR
    [ContextMenu("Rest Story Models")]
    public void ResetStoryModel()
    {
        storyModels = Resources.LoadAll<StoryModel>("");
    }
#endif
}
