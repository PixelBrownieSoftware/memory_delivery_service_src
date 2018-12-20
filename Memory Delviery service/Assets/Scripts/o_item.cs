using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class o_item : MonoBehaviour {

    public void DisableItem() {
        this.gameObject.SetActive(false);
    }

    public void SwapObject(Vector3 Position) {

        this.gameObject.SetActive(true);
        this.transform.position = Position;
    }

}
