using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
   
    public GameObject battleCamera;
    

    public List<BasePokemon> allPokemon = new List<BasePokemon>();
    public List<PokemonMoves> allMoves = new List<PokemonMoves>();

    public Transform defencePodium;
    public Transform attackPodium;
    public GameObject emptyPoke;
    public bool isBattle;
    static Rarity rareItE;

    static string currentScene;
   

    // Start is called before the first frame update
    void Start()
    {
        if (isBattle)
        {
            
            BasePokemon battlePokemon = GetRandomPokemonFromList(GetPokemonByRarity(rareItE));
            GameObject dPoke = Instantiate(emptyPoke, defencePodium.transform);
            dPoke.GetComponent<SpriteRenderer>().sprite = battlePokemon.image;

            GameObject.Find("Background").GetComponent<Image>().sprite = Resources.Load("BattleScenes/" + currentScene + "/battle", typeof(Sprite)) as Sprite;
            GameObject.Find("EnemyBase").GetComponent<Image>().sprite = Resources.Load("BattleScenes/" + currentScene + "/enemy", typeof(Sprite)) as Sprite;
            GameObject.Find("PlayerBase").GetComponent<Image>().sprite = Resources.Load("BattleScenes/" + currentScene + "/player", typeof(Sprite)) as Sprite;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
            
    }

    public void EnterBattle(string scene, Rarity rarity)
    {
        rareItE = rarity;
        SceneManager.LoadScene("BattleScene");
        
        currentScene = scene;

        BasePokemon battlePokemon = GetRandomPokemonFromList(GetPokemonByRarity(rarity));
        //Debug.Log(battlePokemon.name);

        GameObject dPoke = Instantiate(emptyPoke, defencePodium.transform);

        BasePokemon tempPoke = dPoke.AddComponent<BasePokemon>() as BasePokemon;
       
        dPoke.GetComponent<SpriteRenderer>().sprite = battlePokemon.image;

        isBattle = true;
    }


    public List<BasePokemon> GetPokemonByRarity(Rarity rarity) 
    {
        List<BasePokemon> returnPokemon = new List<BasePokemon>();
        foreach (BasePokemon Pokemon in allPokemon)
        {
            if (Pokemon.rarity == rarity)
            {
                returnPokemon.Add(Pokemon);
            }
        }
        return returnPokemon;
    }

    public BasePokemon GetRandomPokemonFromList(List<BasePokemon> pokeList)
    {
        
        int pokeIndex = Random.Range(0, pokeList.Count);
        BasePokemon poke = pokeList[pokeIndex];
        return poke;
    }

}

[System.Serializable]
public class PokemonMoves
{
    string name;
    public MoveType category;
    public Stat moveStat;
    public PokemonType moveType;
    public int PP;
    public float power;
    public float accuracy;
}

[System.Serializable]
public class Stat
{
    public float min;
    public float max;

}

public enum MoveType
{
    Physical,
    Special,
    Status
}