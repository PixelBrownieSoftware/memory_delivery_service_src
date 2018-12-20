using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class e_post : MonoBehaviour
{
    public bool is_delivered;
    public s_houseorders house;
    public string wrong_message;

    public bool SendToPost(string object_name)
    {
        if (house.desired_item == object_name)
        {
            s_camera.cam.desc_text = "Item Delivered";
            is_delivered = true;
            return true;
        }
        else
        {
            s_camera.cam.lives--;
            s_camera.cam.desc_text = wrong_message;
            return false;
        }
    }
}

