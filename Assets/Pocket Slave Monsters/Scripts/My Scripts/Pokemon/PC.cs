using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PC
{
    public BasePokemon[][] boxes = new BasePokemon[][]
    {
        new BasePokemon[6], //Party
        new BasePokemon[30] //Box 1 of PC inventory
    };


    public bool hasSpace(int box) //checks if there is space in boxes
    {
        for (int i = 0; i < boxes[box].Length; i++)
        {
            if(boxes[box][i] == null)
            {
                return true;
            }
        }
        return false;
    }

    public void packParty()
    {
        BasePokemon[] packedArray = new BasePokemon[6];
        int i2 = 0;  // counter for packed array
        for (int i = 0; i < 6; i++)
        {
            if(boxes[0][i] != null)
            {
                packedArray[i2] = boxes[0][i];
                i2 += 1;
            }
            boxes[0] = packedArray;
        }
    }

    public bool AddPokemon(BasePokemon acquiredPokemon)
    {
        
        if (hasSpace(0))
        {
            packParty();
            boxes[0][boxes[0].Length - 1] = acquiredPokemon;
            return true;
        }
        else
        {
            for (int i=1; i<boxes.Length; i++)
            {
                if (hasSpace(i))
                {
                    for (int i2 = 0; i2 < boxes[i].Length; i2++)
                    {
                        if (boxes[i][i2] == null)
                        {
                            boxes[i][i2] = acquiredPokemon;
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }

}
