using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class Player : MonoBehaviour {

    public static OwnedPokemon[] partyPokemon = new OwnedPokemon[6];
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public class OwnedPokemon
{
    public string nickname;
    public BasePokemon pokemon;
    public int level;
    public PokemonMoves[] moves = new PokemonMoves[4];
}
