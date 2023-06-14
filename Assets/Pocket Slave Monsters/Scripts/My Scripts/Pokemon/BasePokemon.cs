using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasePokemon : MonoBehaviour
{
    public string Name;
    public int pokedexNum;
    public Sprite pokemonFront;
    public Sprite pokemonBack;
    public Sprite image;
    public BiomeList regionFound;
    public PokemonType type1;
    public PokemonType type2;
    public Rarity rarity;
    public int HP;
    public Stat AttackStat;
    public Stat DefenseStat;    
    public PokemonStats pokemonStats;
    public bool canEvolve;
    public PokemonEvolution evolveTo;

    private int level;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public enum Rarity
{
    Abundant = 1,
    Common = 2,
    Uncommon = 3,
    Occasional = 4,
    Meager = 5,
    Scarce = 6,
    Sparse = 7,
    Rare = 8,
    ExtremelyRare = 9,
    Unhuntable = 10,
    Shiny = 11
}

public enum PokemonType
{
    NONE,
    FIRE,
    WATER,
    GRASS,
    ROCK,
    GROUND,
    FLYING,
    ICE,
    STEEL,
    ELECTRIC,
    PSYCHIC,
    DARK,
    DRAGON,
    GHOST,
    FIGHTING,
    NORMAL,
    POISON,
    BUG,
    FAIRY

}

[System.Serializable]
public class PokemonEvolution
{
    public BasePokemon nextEvolution;
    public string levelUp;
}

[System.Serializable]
public class PokemonStats
{
    public int Total;
    public int HP;  
    public int Atk;
    public int Def;
    public int SpA;
    public int SpD;
    public int Spe;
}