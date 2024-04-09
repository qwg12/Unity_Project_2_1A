using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;          //에디터
using System.Text;          //텍스트 사용
using UnityEngine.UI;
using TMPro;

namespace STORYGAME //이름충돌 방지
{
#if UNITY_EDITOR
    [CustomEditor(typeof(GameSystem))]

    public class GameSystemEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GameSystem gameSystem = (GameSystem)target;

            if(GUILayout.Button("Reset Story Models"))
            {
                gameSystem.ResetStoryModels();
            }

            if (GUILayout.Button("Assing Text Component by Name"))
            {
                GameObject textObject = GameObject.Find("StroyTextUI");
                if (textObject != null)
                {
                    Text textComponent = textObject.GetComponent<Text>();
                    if (textComponent != null)
                    {
                        gameSystem.textComponent = textComponent;
                        Debug.Log("Text Componet assinged Successfully");
                    }
                }
            }
        }
    }
#endif

    public class GameSystem : MonoBehaviour
    {
        public static GameSystem instance;              //간단한 싱글톤 화
        public Text textComponent = null;

        public float delay = 0.1f;
        public string fullText;
        public string currentText = "";

        public enum GAMESTATE
        {
            STORYSHOW,
            WAITSELECT,
            STROTYEND,
            ENDMODE
        }

        public GAMESTATE currentState;
        public StoryTableObject[] storyModels;
        public StoryTableObject currentModels;
        public int currentStoryIndex;
        public bool showStroy = false;

        private void Awake()
        {
            instance = this;
        }        

        public void Start()
        {
            StartCoroutine(ShowText());
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q)) StoryShow(1);
            if (Input.GetKeyDown(KeyCode.W)) StoryShow(2);
            if (Input.GetKeyDown(KeyCode.E)) StoryShow(3);

            if(Input.GetKeyDown(KeyCode.Space))
            {
                delay = 0.0f;
            }
        }

        public void StoryShow(int number)
        {
            if(!showStroy)
            {
                currentModels = FindStoryModel(number);
                delay = 0.1f;
                StartCoroutine(ShowText());
            }            
        }

        StoryTableObject FindStoryModel(int number)
        {
            StoryTableObject tempStoryModels = null;
            for(int i = 0; i < storyModels.Length; i++)
            {
                if(storyModels[i].storyNumeber == number)
                {
                    tempStoryModels = storyModels[i];
                    break;
                }
            }

            return tempStoryModels;

        }

        IEnumerator ShowText()
        {
            showStroy = true;
            for(int i = 0; i <= currentModels.storyText.Length; i++)
            {
                currentText = currentModels.storyText.Substring(0, i);
                textComponent.text = currentText;
                yield return new WaitForSeconds(delay);
            }

            yield return new WaitForSeconds(delay);
            showStroy = false;
        }

#if UNITY_EDITOR
        [ContextMenu("Reset Stroy Models")]

        public void ResetStoryModels()
        {
            storyModels = Resources.LoadAll<StoryTableObject>("");
        }
#endif
    }
}



