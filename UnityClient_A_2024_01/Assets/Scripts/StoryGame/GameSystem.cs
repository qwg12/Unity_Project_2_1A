using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using STORYGAME;
using System.Text;

#if UNITY_EDITOR
[CustomEditor(typeof(GameSystem))]
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
#endif

public class GameSystem : MonoBehaviour
{
    public StoryModel[] storyModels;

    public static GameSystem instance;

    public void Awake()
    {
        instance = this;
    }

    public enum GAMESTATE
    {
        STROYSHOW,
        WAITSELECT,
        STORTEND,
        BATTLEMODE,
        BATTLEDONE,
        SHOPMODE,
        ENDMODE
    }
    public Stats stats;
    public GAMESTATE currentState;

    public int currentStoryIndex = 1;

    public void ApplyChoice(StoryModel.Result result)
    {
        switch(result.resultType)
        {
            case StoryModel.Result.ResultType.ChangeHp:
                //stats.currentHpPoint += result.value;
                ChangeState(result);
                break;

            case StoryModel.Result.ResultType.AddExperience:
                ChangeState(result);
                break;

            case StoryModel.Result.ResultType.GoToNextStory:
                currentStoryIndex = result.value;
                StoryShow(currentStoryIndex);
                break;
            case StoryModel.Result.ResultType.GoToRandomStory:
                StoryModel temp = RandomStory();
                StoryShow(temp.storyNumber);
                break;
        }
    }

    public void StoryShow(int number)
    {
        StoryModel tempStoryModel = FindStoryModel(number);

        StorySystem.instance.currentStoryModel = tempStoryModel;
        StorySystem.instance.CoShowText();
    }

    public void ChangeState(StoryModel.Result result)
    {
        if (result.stats.hpPoint > 0) stats.hpPoint += result.stats.hpPoint;
        if (result.stats.hpPoint > 0) stats.spPoint += result.stats.spPoint;
        if (result.stats.currentHpPoint > 0) stats.currentHpPoint += result.stats.currentHpPoint;
        if (result.stats.currentSpPoint > 0) stats.currentSpPoint += result.stats.currentSpPoint;
        if (result.stats.currentXpPoint > 0) stats.currentXpPoint += result.stats.currentXpPoint;
        if (result.stats.strength > 0) stats.strength += result.stats.strength;
        if (result.stats.dexterity > 0) stats.dexterity += result.stats.dexterity;
        if (result.stats.consitiution > 0) stats.consitiution += result.stats.consitiution;
        if (result.stats.wisdom > 0) stats.wisdom += result.stats.wisdom;
        if (result.stats.Intelligence > 0) stats.Intelligence += result.stats.Intelligence;
        if (result.stats.charisma > 0) stats.charisma += result.stats.charisma;
    }

    StoryModel RandomStory()
    {

        StoryModel tempStoryModels = null;
        List<StoryModel> StoryModelList = new List<StoryModel>();

        for(int i = 0; i < storyModels.Length; i++)
        {
            if(storyModels[i].storytype == StoryModel.STORYTYPE.MAIN)
            {
                StoryModelList.Add(storyModels[i]);
            }
        }

        tempStoryModels = StoryModelList[Random.Range(0, StoryModelList.Count)];
        currentStoryIndex = tempStoryModels.storyNumber;
        Debug.Log("currentStoryIndex" + currentStoryIndex);

        return tempStoryModels;
    }

    StoryModel FindStoryModel(int number)
    {
        StoryModel tempStoryModels = null;

        for (int i = 0; i < storyModels.Length; i++)
        {
            if (storyModels[i].storyNumber == number)
            {
                tempStoryModels = storyModels[i];
                break;
            }
        }
        return tempStoryModels;
    }

#if UNITY_EDITOR
    [ContextMenu("Rest Story Models")]
    public void ResetStoryModel()
    {
        storyModels = Resources.LoadAll<StoryModel>("");
    }
#endif
}
