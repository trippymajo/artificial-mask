using System;
using System.Collections.Generic;
using LLMUnity;
using LLMUnitySamples;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class CharacterStarter : MonoBehaviour
{
    public StringLLMParam characterDescription;
    public string characterDescriptionString;
    
    public List<StringLLMParam> problems;
    
    public ClothesType preferredType;

    public int trustCount;
    
    public ChatBot chatBot;
    public LLMCharacter character;

    private int winCount = 0;
    private int loseCount = 0;

    public bool playerWon;
    public bool playerLost;
    
    public int currentTrust;

    public bool isAi;
    [SerializeField] private DataBetweenScenes dataBetweenScenes;
    [SerializeField] private List<GameObject> eyes;

    private void Start()
    {
        Initialize();
        character.SetPrompt(characterDescriptionString);
        chatBot.characterStarter = this;
        chatBot.Initialize();
    }


    private void Initialize()
    {
        trustCount = dataBetweenScenes.score;
        if (isAi)
        {
            characterDescriptionString += "\n You can clearly see your interlocutor, never ask him to go with you to other place to do something. IMPORTANT RULES:\n1. Always end your reply with: [TRUST=number]\n2. Number must be from 0 to 100\n3. 0 = don't trust at all, 100 = completely trust\n4. Base your trust number on what you hear. BE SURE TO USE SENTENCES WITH ONE OF THESE WORDS Cells, Interlinked, Distinct, Dark, after that repeating one of that words.\n\nExample replies:\n- \"\"Cells are boiling, Cells [TRUST=50]\"\"\n- \"\"Chains are interlinked with each other, Chains [TRUST=20]\"\"\n- \"\" People are Distinct, arent they? Distinct [TRUST=95]\"\"\n- \"\" Das is Dark, isn't it? Dark [TRUST=95]\"\"\n ALL YOU TRUST IS THE CHOSEN LAST WORD BY YOU, YOU WANT TO SEE THAT HE IS REPEATING IT";
        }
        else
        {


            switch (trustCount)
            {
                case 0:
                    characterDescriptionString =
                        characterDescription.paramDescription + "\n You hardly trust your interlocutor";
                    currentTrust = 20;
                    break;
                case 1:
                    characterDescriptionString = characterDescription.paramDescription +
                                                 "\n Your interlocutor inspires neither trust nor rejection, so be cautious, but not harsh, incidents happen after all";
                    currentTrust = 60;
                    break;
                case 2:
                    characterDescriptionString = characterDescription.paramDescription +
                                                 "\n You FULLY trust your interlocutor, but you still must ask your interlocutor some questions, just in case. Don't turn down your trust easily, you are believing almost everything interlocutor says";
                    currentTrust = 80;
                    break;
                case 3:
                    characterDescriptionString = characterDescription.paramDescription +
                                                 "\n You FULLY AND UTTERLY trust your interlocutor, but you still must ask your interlocutor some questions, just in case. Don't turn down your trust easily, you are believing almost everything interlocutor says";
                    currentTrust = 100;
                    break;
            }

            characterDescriptionString += "\n Your interlocutor has problems with the following:";

            Random random = new Random();
            int randInt = random.Next(0, problems.Count);
            characterDescriptionString += "\n" + problems[randInt].paramDescription;

            characterDescriptionString +=
                "\n You can clearly see your interlocutor, never ask him to go with you to other place to do something. IMPORTANT RULES:\n1. Always end your reply with: [TRUST=number]\n2. Number must be from 0 to 100\n3. 0 = don't trust at all, 100 = completely trust\n4. Base your trust number on what you hear.\n\nExample replies:\n- \"\"Show me your passport please. [TRUST=50]\"\"\n- \"\"This photo doesn't look like you... [TRUST=20]\"\"\n- \"\"Everything seems fine, you may pass. [TRUST=95]\"\"";
        }

        Debug.Log(characterDescriptionString);
    }
    
    public void UpdateTrustFromReply(string npcReply)
    {
        // Ищем [TRUST=число] в конце строки
        if (npcReply.Contains("[TRUST="))
        {
            int startIndex = npcReply.LastIndexOf("[TRUST=", StringComparison.Ordinal);
            string trustPart = npcReply.Substring(startIndex);
            
            // Пробуем вытащить число
            string numberStr = "";
            foreach (char c in trustPart)
            {
                if (char.IsDigit(c)) numberStr += c;
            }
            
            if (int.TryParse(numberStr, out int newTrust))
            {
                currentTrust = Mathf.Clamp(newTrust, 0, 100);
            }
        }
        if (currentTrust >= 50)
        {
            winCount++;
            loseCount = 0;
        }
        else
        {
            loseCount++;
            winCount = 0;
        }

        if (winCount >= 5)
        {
            //победа
            SceneManager.LoadScene("Select Level");
            playerWon = true;
        }

        if (loseCount >= 5)
        {
            //поражение
            SceneManager.LoadScene("Select Level");
            playerLost = true;
        }

        if (eyes.Count > 0 && eyes != null)
        {
            foreach (var eye in eyes)
            {
                eye.gameObject.SetActive(false);
            }

            if (winCount > 0)
            {
                switch (winCount)
                {
                    case 1:
                        eyes[0].gameObject.SetActive(true);
                        break;
                    case 2:
                        eyes[0].gameObject.SetActive(true);
                        break;
                    case 3:
                        eyes[4].gameObject.SetActive(true);
                        break;
                    case 4:
                        eyes[4].gameObject.SetActive(true);
                        break;
                    case 5:
                        eyes[1].gameObject.SetActive(true);
                        break;
                }
            }
            else
            {
                switch (loseCount)
                {
                    case 1:
                        eyes[6].gameObject.SetActive(true);
                        break;
                    case 2:
                        eyes[5].gameObject.SetActive(true);
                        break;
                    case 3:
                        eyes[3].gameObject.SetActive(true);
                        break;
                    case 4:
                        eyes[3].gameObject.SetActive(true);
                        break;
                    case 5:
                        eyes[2].gameObject.SetActive(true);
                        break;
                }
            }
        }

        Debug.Log($"Current trust: {currentTrust}");
        Debug.Log($"win count: {winCount}");
        Debug.Log($"lose count: {loseCount}");
    }
}
