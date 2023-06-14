using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static PC;



public class PlayerInteract : MonoBehaviour
{
    private GameObject pokeball;
    private OwnedPokemon starter = new OwnedPokemon(); //party pokemon

    public GameObject starterCanvas; //starter pokemon canvas
    public GameObject sideGUI; //sideGUI canvas
    

    //Arrows for canvas'
    private GameObject YesArrow; //starterCanvas YesArrow
    private GameObject NoArrow; //starterCanvas NoArrow
    private GameObject PokedexArrow; //sideGUI arrow start
    private GameObject PokemonArrow; //
    private GameObject BagArrow; // 
    private GameObject PlayerArrow; //
    private GameObject SaveArrow; // 
    private GameObject OptionArrow; //
    private GameObject ExitArrow; //sideGUI arrow end

    private bool pickUpAllowed = false;
    static private bool hasPokemon = false;
    private bool pokemonAcquired;
   

    private PC storePokemon;
 
    // Start is called before the first frame update
    void Start()
    {
        starterCanvas.SetActive(false);
        sideGUI.SetActive(false);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Interactable"))
        {
            pickUpAllowed = true;
            pokeball = other.gameObject;              
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        pickUpAllowed = false;
    }



    void Update()
    {
        if (Input.GetButton("Cancel"))
        {
            sideGUI.SetActive(true);
           
            //Assign arrows
            {
                if (GameObject.Find("PokedexArrow"))
                    PokedexArrow = GameObject.Find("PokedexArrow");
                if (GameObject.Find("PokemonArrow"))
                    PokemonArrow = GameObject.Find("PokemonArrow");
                if (GameObject.Find("BagArrow"))
                    BagArrow = GameObject.Find("BagArrow");
                if (GameObject.Find("PlayerArrow"))
                    PlayerArrow = GameObject.Find("PlayerArrow");
                if (GameObject.Find("SaveArrow"))
                    SaveArrow = GameObject.Find("SaveArrow");
                if (GameObject.Find("OptionArrow"))
                    OptionArrow = GameObject.Find("OptionArrow");
                if (GameObject.Find("ExitArrow"))
                    ExitArrow = GameObject.Find("ExitArrow");

                PokedexArrow.SetActive(true);
                PokemonArrow.SetActive(false);
                BagArrow.SetActive(false);
                PlayerArrow.SetActive(false);
                SaveArrow.SetActive(false);
                OptionArrow.SetActive(false);
                ExitArrow.SetActive(false);
            }

            StartCoroutine(WaitForMenuChoice());
        }



        if (!hasPokemon && pickUpAllowed && Input.GetButton("Submit"))
        {
            if (transform.position.y < pokeball.GetComponent<Transform>().position.y && GetComponent<PlayerController>().dir == Direction.North ||
                transform.position.y > pokeball.GetComponent<Transform>().position.y && GetComponent<PlayerController>().dir == Direction.South ||
                transform.position.x > pokeball.GetComponent<Transform>().position.x && GetComponent<PlayerController>().dir == Direction.West ||
                transform.position.x < pokeball.GetComponent<Transform>().position.x && GetComponent<PlayerController>().dir == Direction.East)
            {
               
                starterCanvas.SetActive(true); //turns canvas on to show pokemon

                if (GameObject.Find("YesArrow"))
                    YesArrow = GameObject.Find("YesArrow");
                if (GameObject.Find("NoArrow"))
                    NoArrow = GameObject.Find("NoArrow");

                
                NoArrow.SetActive(false);
                YesArrow.SetActive(true);
                
                starter.pokemon = (Resources.Load("Pokemon/Prefabs/" + pokeball.name.Substring(11), typeof(GameObject)) as GameObject).GetComponent<BasePokemon>();
                
                

                GameObject.Find("InfoText").GetComponent<Text>().text = "So,  PLAYER,  you  want  to  go  with  the  " + starter.pokemon.type1 + 
                                                                         "  POKEMON  " + (starter.pokemon.Name).ToUpper();
                GameObject.Find("Pokemon").GetComponent<Image>().sprite = starter.pokemon.pokemonFront;


                StartCoroutine(WaitForSelection());

                
            }
        }
    }


    IEnumerator WaitForSelection()
    {
        while (true)
        {
            pickUpAllowed = false;
            yield return new WaitForSeconds(.2f);
            GetComponent<PlayerController>().isAllowedToMove = false; //stops player from moving

            do
            {
                yield return new WaitForEndOfFrame();
            }
            while (!Input.GetButton("Cancel") && !Input.GetButton("Vertical") && !Input.GetButton("Submit"));
                                        
            if (Input.GetButton("Submit") && YesArrow.activeSelf == true)
            {
                Player.partyPokemon[0] = starter;

                
                Destroy(pokeball);
                hasPokemon = true;
                break;                
            }
            if (Input.GetButton("Submit") && NoArrow.activeSelf == true)
            {
                break;
            }


            if (Input.GetButton("Cancel"))
            {                
                break;
            }

            if (Input.GetAxis("Vertical") < 0)
            {
                YesArrow.SetActive(false);
                NoArrow.SetActive(true);
            }
            if (Input.GetAxis("Vertical") > 0)
            {
                YesArrow.SetActive(true);
                NoArrow.SetActive(false);
            }
           // yield return new WaitForSeconds(.1f);
        }

        starterCanvas.SetActive(false);
        GetComponent<PlayerController>().isAllowedToMove = true;
        yield return new WaitForSeconds(.2f);
        pickUpAllowed = true;
    }

    IEnumerator WaitForMenuChoice()
    {
        while (true)
        {

            GetComponent<PlayerController>().isAllowedToMove = false; //stops player from moving
            yield return new WaitForSeconds(.2f);

            do
            {
                yield return new WaitForEndOfFrame();
            }
            while (!Input.GetButton("Cancel") && !Input.GetButton("Vertical") && !Input.GetButton("Submit"));

            if (Input.GetButton("Submit") && PokedexArrow.activeSelf == true)
            {
                 
            }
            if (Input.GetButton("Submit") && PokemonArrow.activeSelf == true)
            {

            }
            if (Input.GetButton("Submit") && BagArrow.activeSelf == true)
            {

            }
            if (Input.GetButton("Submit") && PlayerArrow.activeSelf == true)
            {

            }
            if (Input.GetButton("Submit") && SaveArrow.activeSelf == true)
            {

            }
            if (Input.GetButton("Submit") && OptionArrow.activeSelf == true)
            {

            }
            if (Input.GetButton("Submit") && ExitArrow.activeSelf == true)
            {
                break;
            }

            if (Input.GetButton("Cancel") )
            {
                break;
            }                                   
        }
        GetComponent<PlayerController>().isAllowedToMove = true;
        sideGUI.SetActive(false);
        
        yield return new WaitForSeconds(.2f);

    }

    enum PauseMenuArrows
    {
        PokedexArrow = 0,
        PokemonArrow = 1,
        BagArrow = 2,
        PlayerArrow = 3,
        SaveArrow = 4,
        OptionArrow = 5,
        ExitArrow = 6
    }
    
    
}

 

