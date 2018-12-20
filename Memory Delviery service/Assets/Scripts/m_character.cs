using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class m_character : MonoBehaviour {
    
    public Transform ground_sensor;

    public LayerMask ground;
    public LayerMask obj;
    public LayerMask interactable;

    public float angles;

    GameObject objecttolookat;
    public GameObject item;
    GameObject collision;

    float mouse_x;
    float mouse_y;

    Rigidbody rbody;
    Vector3 movedir;

    public enum pickingObj { isPicking, interact , nothing }
    public pickingObj pick;

    void Start () {
        collision = this.transform.parent.gameObject;
        pick = pickingObj.nothing;
        Cursor.lockState = CursorLockMode.Locked;
        rbody = collision.GetComponent<Rigidbody>();
        ground_sensor = transform.GetChild(0);
    }
	
	void Update () {
        UpdateInput();
        UpdatePhysics();
        CameraUpdate();

        switch (pick)
        {
            case pickingObj.isPicking:

                if (Input.GetKeyDown(KeyCode.E))
                {
                    pick = pickingObj.nothing;
                }
                break;

            case pickingObj.interact:

                pick = pickingObj.nothing;
                objecttolookat.GetComponent<e_hintObj>().Interact();
                if (Input.GetKeyDown(KeyCode.E)) {
                    // close dialogue
                }
                    break;

            case pickingObj.nothing:
                if (Input.GetKeyDown(KeyCode.E))
                {
                    switch (thing) {
                        case pointingtoObj.interactable:
                                pick = pickingObj.interact;
                            break;

                        case pointingtoObj.item:

                            //pick = pickingObj.isPicking;
                            break;
                    }



                    /*
                    if (is_pointing_to_object) {
                        if (objecttolookat.GetComponent<e_hintObj>())
                        {
                            objecttolookat.GetComponent<e_hintObj>().Interact();
                            pick = pickingObj.interact;
                        }
                    }
                    else {

                    }*/
                }


                break;
        }
        
	}

    void CameraUpdate() {
        Camera.main.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        
        mouse_x += Input.GetAxis("Mouse X");
        mouse_y -= Input.GetAxis("Mouse Y");

        mouse_y = Mathf.Clamp(mouse_y, -80, 80);
        if (mouse_x > 360) {
            mouse_x = 0;
        }

        angles = Mathf.Sin(this.transform.rotation.x * Mathf.Deg2Rad);
        Camera.main.transform.eulerAngles = new Vector3(mouse_y, mouse_x, 0);
    }

    void UpdatePhysics()
    {
        Vector3 yvel = new Vector3(0, rbody.velocity.y,0);
        rbody.velocity = movedir * 145f * Time.deltaTime;
        rbody.velocity += yvel;
        collision.transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X"));
    }



    enum pointingtoObj { item, interactable, none }
    pointingtoObj thing {
        get {
            Ray r = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit ray;
            if (Physics.Raycast(r, out ray))
            {
                if (ray.collider.GetComponent<o_item>()) {
                    if (item == null) {
                        objecttolookat = ray.transform.gameObject;
                        item = objecttolookat;
                        objecttolookat.GetComponent<o_item>().DisableItem();
                        objecttolookat = null;
                    } else {
                        objecttolookat = ray.transform.gameObject;
                        item.GetComponent<o_item>().SwapObject(objecttolookat.transform.position);
                        item = objecttolookat;
                        objecttolookat.GetComponent<o_item>().DisableItem();
                        objecttolookat = null;
                    }

                    return pointingtoObj.item;
                }

                if (ray.collider.GetComponent<e_post>()) {
                    e_post thing = ray.collider.GetComponent<e_post>();

                    if (item != null) {
                        if (thing.SendToPost(item.name)) {
                            item = null;
                        }
                    }
                }

                if (ray.collider.GetComponent<e_hintObj>())
                {
                    objecttolookat = ray.transform.gameObject;
                    return pointingtoObj.interactable;
                }

            }
            /*
            else if (ray.collider.gameObject.layer == interactable.value) {
                    objecttolookat = ray.transform.gameObject;
                    return pointingtoObj.interactable;
                }
                else { return pointingtoObj.none; }
            */
            return pointingtoObj.none;
        }
    }

    void UpdateInput() {

        float vert = Input.GetAxisRaw("Vertical");
        float horiz = Input.GetAxisRaw("Horizontal");

        movedir = (horiz * transform.right + vert * transform.forward).normalized;// (horiz * transform.right + 0 +vert * transform.forward).normalized;

        if (Input.GetKeyDown(KeyCode.Escape)) {
            Cursor.lockState = CursorLockMode.None;
        }

        if (IsGrounded)
        {
            print("k");
        }

        if (IsGrounded && Input.GetKeyDown(KeyCode.Space)) {
            //print(IsGrounded);
            rbody.AddForce(new Vector3(0,4,0), ForceMode.Impulse);
        }
    }
    

     bool IsGrounded {
         get
         {
            return Physics.CheckSphere(ground_sensor.position, 0.35f, ground, QueryTriggerInteraction.Ignore);
         }
     }

}
