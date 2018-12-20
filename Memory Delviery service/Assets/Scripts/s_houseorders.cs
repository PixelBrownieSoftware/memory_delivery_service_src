using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct o_hints {
    public GameObject obj;
    public string description;
    
}

[System.Serializable]
public class Level
{
    public GameObject levelObj;
    public List<Items> items = new List<Items>();
    public s_items itemsthing;
    public Vector3 spawn;

    public void ResetItems() {
        foreach (Items item in items) {
            item.item.transform.position = item.position;
            item.item.gameObject.SetActive(true);
        }

        s_houseorders[] houses = levelObj.transform.Find("Houses").GetComponentsInChildren<s_houseorders>();
        foreach (s_houseorders house in houses) {
            house.post.GetComponent<e_post>().is_delivered = false;
        }
    }
}


public class s_houseorders : MonoBehaviour {

    public List<o_hints> hints = new List<o_hints>();
    public string desired_item;
    public GameObject post;

    private void Start()
    {
        foreach (o_hints hint in hints) {
            hint.obj.AddComponent<e_hintObj>();
            hint.obj.GetComponent<e_hintObj>().description = hint.description;
        }
        post.GetComponent<e_post>().house = this;
    }

    private void Update()
    {
        is_satisfied();
    }

    public bool is_satisfied()
    { if (post.GetComponent<e_post>().is_delivered)
        { return true; } else { return false; } }

}

public class e_hintObj : MonoBehaviour {
    public string description;

    public void Interact()
    {
        s_camera.cam.desc_text = description;
    }
}