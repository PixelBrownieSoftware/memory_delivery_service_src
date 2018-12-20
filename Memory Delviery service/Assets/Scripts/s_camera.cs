using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class s_camera : MonoBehaviour {

    public int lives;
    public static s_camera cam;
    float thing2;
    public m_character player;
    public Text txt;

    public string desc_text;

    private void Awake()
    {
        lives = 2;
        cam = this;
    }

    void OnGUI () {
        string displives = "Lives: " + lives;
        string line1 = player.item != null ?  "Item: " + player.item.name : "Item: Nothing";
        txt.text = displives + "\n" + line1 + "\n" + desc_text;
    }
}
