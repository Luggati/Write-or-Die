using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;


public class UtilsScript : MonoBehaviour
{
    Dictionary<int, List<string>> germanWords;
    Dictionary<int, List<string>> englishWords;

    public GameObject logicScript;

    // Start is called before the first frame update
    void Start()
    {
        // Lese die Daten aus der txt Datei ein
        string prepath = Application.streamingAssetsPath;
        List<string> gerWordsUnsorted = new List<string>(File.ReadAllLines(prepath + "/gerWords.txt"));
        List<string> engWordsUnsorted = new List<string>(File.ReadAllLines(prepath + "/engWords.txt"));

        germanWords = gerWordsUnsorted.GroupBy(w => w.Length).ToDictionary(g => g.Key, g => g.ToList());
        englishWords = engWordsUnsorted.GroupBy(w => w.Length).ToDictionary(g => g.Key, g => g.ToList());

        RemoveProhibitedWords();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetRandomGerWordWithLenght(int wordLenght)
    {
        int index = Random.Range(0, germanWords[wordLenght].Count);
        if (germanWords.ContainsKey(wordLenght) == false)
        {
            return "Error";
        }
        return germanWords[wordLenght][index];
    }

    public string GetRandomEngWordWithLenght(int wordLenght)
    {
        int index = Random.Range(0, englishWords[wordLenght].Count);
        if (englishWords.ContainsKey(wordLenght) == false)
        {
            return "Error";
        }
        return englishWords[wordLenght][index];
    }

    void RemoveProhibitedWords()
    {
        foreach (string prohibitedWord in logicScript.GetComponent<LogicScript>().GetProhibitedWords())
        {
            if (germanWords.ContainsKey(prohibitedWord.Length))
            {
                germanWords[prohibitedWord.Length].Remove(prohibitedWord);
            }
            if (englishWords.ContainsKey(prohibitedWord.Length))
            {
                englishWords[prohibitedWord.Length].Remove(prohibitedWord);
            }
        }
    }

}
