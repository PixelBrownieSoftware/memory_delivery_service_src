using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_items : MonoBehaviour {

    public List<Items> items = new List<Items>();

    public void Sav()
    {
        o_item[] objects = GetComponentsInChildren<o_item>();

        foreach (o_item obj in objects) {
            Items ite = new Items();
            ite.item = obj.gameObject;
            ite.position = obj.transform.position;

            items.Add(ite);
        }
    }
}
