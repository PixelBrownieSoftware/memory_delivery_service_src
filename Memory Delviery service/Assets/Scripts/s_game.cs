using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class s_game : MonoBehaviour
{

    public List<Level> levels = new List<Level>();
    public int level;
    public m_character player;
    Vector3 player_spawn;

    void Start() {
        level = 0;
        for (int i = 0; i < levels.Count; i++) {
            if (level == i)
            {
                GameObject thing = levels[i].levelObj;
                thing.SetActive(true);
                player.transform.parent.transform.position = levels[level].spawn;
                continue;
            } else
            {
                GameObject thing = levels[i].levelObj;
                thing.SetActive(false);
            }
        }
    }

    public void LoadNextlevel() {
        if (level == 4) {
            SceneManager.LoadScene("Ending");
        }
        GameObject.Find("Fade").GetComponent<Animator>().Play("FadeIn");

        for (int i = 0; i < levels.Count; i++) {
            if (level == i)
            {
                GameObject thing = levels[i].levelObj;
                thing.SetActive(true);
                player.transform.parent.transform.position = levels[level].spawn;
                continue;
            } else
            {
                GameObject thing = levels[i].levelObj;
                thing.SetActive(false);
            }
        }
    }

    void Update()
    {
        if (s_camera.cam.lives == 0) {
            RestartLevel();
        }
        if (is_level_complete()) {

            level++;
            LoadNextlevel();
        }

    }

    bool is_level_complete() {
        s_houseorders[] houses = levels[level].levelObj.transform.Find("Houses").GetComponentsInChildren<s_houseorders>();
        int num = 0;
        foreach (s_houseorders house in houses)
        {
            if (house.is_satisfied())
            {
                num++;
            }
        }
        if (num == levels[level].items.Count)
        {
            return true;
        }
        return false;
    }


    public void SaveLevel()
    {
        GameObject thing = levels[level].levelObj;
        levels[level].spawn = thing.transform.Find("Spawn").transform.position;
        GameObject itemsti = thing.transform.Find("Items").gameObject;
        s_items ite = itemsti.GetComponent<s_items>();
        levels[level].items.Clear();
        ite.items.Clear();
        ite.Sav();
        for (int i = 0; i < ite.items.Count; i++)
        {
            Items itemlistind = new Items();
            itemlistind.item = ite.items[i].item;
            itemlistind.position = ite.items[i].position;
            levels[level].items.Add(itemlistind);
            print("added");
        }
    }

    void RestartLevel()
    {
        levels[level].ResetItems();
        player.transform.parent.transform.position = levels[level].spawn;
        player.item = null;
        s_camera.cam.lives = 3;
    }
}


[System.Serializable]
public class House
{
    public string object_location;
    public Vector3 position;
    public List<o_hints> hints = new List<o_hints>();
    public string desired_item;
    public GameObject post;
    public List<GameObject> objects = new List<GameObject>();
}

[System.Serializable]
public struct Items
{
    public GameObject item;
    public Vector3 position;
}



