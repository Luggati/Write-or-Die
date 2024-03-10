using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Random = UnityEngine.Random;


public class UtilsScript : MonoBehaviour
{
    List<List<string>> gerWords = new List<List<string>>();
    List<List<string>> engWords = new List<List<string>>();

    List<string> gerWords1 = new List<string>();
    List<string> gerWords2 = new List<string>();
    List<string> gerWords3 = new List<string>();
    List<string> gerWords4 = new List<string>();
    List<string> gerWords5 = new List<string>();

    List<string> engWords1 = new List<string>();
    List<string> engWords2 = new List<string>();
    List<string> engWords3 = new List<string>();
    List<string> engWords4 = new List<string>();
    List<string> engWords5 = new List<string>();

    public GameObject logicScript;

    // Start is called before the first frame update
    void Start()
    {
        // Lese die Daten aus der txt Datei ein
        List<string> gerWordsUnsorted = new List<string>(File.ReadAllLines("Assets/gerWords.txt"));
        List<string> engWordsUnsorted = new List<string>(File.ReadAllLines("Assets/engWords.txt"));

        AddGerWordToList(gerWordsUnsorted);
        AddEngWordToList(engWordsUnsorted);

        gerWords.Add(gerWords1);
        gerWords.Add(gerWords2);
        gerWords.Add(gerWords3);
        gerWords.Add(gerWords4);
        gerWords.Add(gerWords5);

        engWords.Add(engWords1);
        engWords.Add(engWords2);
        engWords.Add(engWords3);
        engWords.Add(engWords4);
        engWords.Add(engWords5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetRandomGerWordWithLenght(int wordLenght)
    {
        int index = Random.Range(0, gerWords[wordLenght - 1].Count);
        return gerWords[wordLenght - 1][index];
    }

    public string GetRandomEngWordWithLenght(int wordLenght)
    {
        int index = Random.Range(0, engWords[wordLenght - 1].Count);
        return engWords[wordLenght - 1][index];
    }

    void AddGerWordToList(List<string> wordList)
    {
        foreach (string word in wordList)
        {
            foreach (string prohibitedWord in logicScript.GetComponent<LogicScript>().GetProhibitedWords())
            {
                if (word.ToLower().Equals(prohibitedWord))
                {
                    continue;
                }
            }

            if (word.Length == 1)
            {
                gerWords1.Add(word);
            }
            else if (word.Length == 2)
            {
                gerWords2.Add(word);
            }
            else if (word.Length == 3)
            {
                gerWords3.Add(word);
            }
            else if (word.Length == 4)
            {
                gerWords4.Add(word);
            }
            else if (word.Length == 5)
            {
                gerWords5.Add(word);
            }
        }
    }

    void AddEngWordToList(List<string> wordList)
    {
        foreach (string word in wordList)
        {
            foreach (string prohibitedWord in logicScript.GetComponent<LogicScript>().GetProhibitedWords())
            {
                if (word.ToLower().Equals(prohibitedWord))
                {
                    continue;
                }
            }

            if (word.Length == 1)
            {
                engWords1.Add(word);
            }
            else if (word.Length == 2)
            {
                engWords2.Add(word);
            }
            else if (word.Length == 3)
            {
                engWords3.Add(word);
            }
            else if (word.Length == 4)
            {
                engWords4.Add(word);
            }
            else if (word.Length == 5)
            {
                engWords5.Add(word);
            }
        }
    }

}
