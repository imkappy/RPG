using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;


public class CreateBattleScenes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach (var obj in Resources.LoadAll("Battlebacks/Sprites", typeof(Sprite)))
        {
            if (obj.name.Substring(0, 6) == "battle" && !Directory.Exists("Assets/Resources/Battlebacks/Sprites/" + obj.name.Substring(8)))
                Directory.CreateDirectory("Assets/Resources/Battlebacks/Sprites/" + obj.name.Substring(8));
            if (obj.name.Substring(0, 5) == "enemy" && !Directory.Exists("Assets/Resources/Battlebacks/Sprites/" + obj.name.Substring(9)))
                Directory.CreateDirectory("Assets/Resources/Battlebacks/Sprites/" + obj.name.Substring(9));
            if (obj.name.Substring(0, 6) == "player" && !Directory.Exists("Assets/Resources/Battlebacks/Sprites/" + obj.name.Substring(10)))
                Directory.CreateDirectory("Assets/Resources/Battlebacks/Sprites/" + obj.name.Substring(10));
            //break;
        }
        AssetDatabase.Refresh();
    }
    private void OnApplicationQuit()
    {
        foreach (var obj in Resources.LoadAll("Battlebacks/Sprites", typeof(Sprite)))
        {
            //AssetDatabase.MoveAsset("Assets/Resources/Battlebacks/Sprites/battlebgCave.png", "Assets/Resources/Battlebacks/Sprites/Cave/battlebgCave.png");
            for (int i = 7; i < (obj.name.Length); i++)
            {
                if (Directory.Exists("Assets/Resources/Battlebacks/Sprites/" + obj.name.Substring(i)))
                {
                    Debug.Log(AssetDatabase.ValidateMoveAsset("Assets/Resources/Battlebacks/Sprites/battlebgCave.png", "Assets/Resources/Battlebacks/Sprites/Cave/battlebgCave.png"));
                    switch (obj.name[0])
                    {
                        case 'b':
                        case 'B':
                            AssetDatabase.MoveAsset("Assets/Resources/Battlebacks/Sprites/" + obj.name + ".png", "Assets/Resources/Battlebacks/Sprites/" + obj.name.Substring(i) + "/battle.png");
                            break;
                        case 'e':
                        case 'E':
                            AssetDatabase.MoveAsset("Assets/Resources/Battlebacks/Sprites/" + obj.name + ".png", "Assets/Resources/Battlebacks/Sprites/" + obj.name.Substring(i) + "/enemy.png");
                            break;
                        case 'p':
                        case 'P':
                            AssetDatabase.MoveAsset("Assets/Resources/Battlebacks/Sprites/" + obj.name + ".png", "Assets/Resources/Battlebacks/Sprites/" + obj.name.Substring(i) + "/player.png");
                            break;
                    }

                }
            }
        }
        AssetDatabase.Refresh();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
