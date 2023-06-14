using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{

    public BiomeList grassType;
    private GameManager gm;

    string scene;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
       
        scene = "" + grassType;
        
    }

    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<PlayerController>())
        {
            //P = x / 187.5
            //VC = 10, C = 8.5, SR = 6.75, R = 3.33, VR = 1.25
            /*
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
            */
            float abundant = 20 / 187.5f;
            float common = 15f / 187.5f;
            float uncommon = 10f / 187.5f;
            float occasional = 7.5f / 187.5f;
            float meager = 5.75f / 187.5f;
            float scarce = 3.25f / 187.5f;
            float sparse = 2.5f / 187.5f;
            float rare = 1.25f / 187.5f;
            float erare = .5f / 187.5f;


            float p = Random.Range(0.0f, 100.0f);

            if (gm != null)
            {
                if (p < abundant * 100)
                {
                    gm.EnterBattle(scene, Rarity.Abundant);
                }
                else if (p < common * 100)
                {
                    gm.EnterBattle(scene, Rarity.Common);
                }
                else if (p < uncommon* 100)
                {
                    gm.EnterBattle(scene, Rarity.Uncommon);
                }
                else if (p < occasional * 100)
                {
                    gm.EnterBattle(scene, Rarity.Occasional);
                }
                else if (p < meager * 100)
                {
                    gm.EnterBattle(scene, Rarity.Meager);
                }
                else if (p < scarce * 100)
                {
                    gm.EnterBattle(scene, Rarity.Scarce);
                }
                else if (p < sparse * 100)
                {
                    gm.EnterBattle(scene, Rarity.Sparse);
                }
                else if (p < rare * 100)
                {
                    gm.EnterBattle(scene, Rarity.Rare);
                }
                else if (p < erare * 100)
                {
                    gm.EnterBattle(scene, Rarity.ExtremelyRare);
                }
            }
        }
    }

}
