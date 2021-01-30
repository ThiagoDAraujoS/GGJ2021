using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField]
    GameObject door;
    SpriteRenderer doorSprite;

    [SerializeField]
    private UnityEvent OnPressurePlateDown;

    [SerializeField]
    private UnityEvent OnPressurePlateUp;


    private bool isOpen = false;
    private bool isUp = true;

    [SerializeField]
    private Animator doorAnimController;

    [SerializeField]
    private Animator plateAnimController;

    void SetBoolean()
    {
        isOpen = !isOpen;
        isUp = !isUp;

    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            print("I'm a player");

            if (!isOpen)
            {
                OnPressurePlateDown?.Invoke();
                AudioManager.instance.PlayDoorOpenClip();
                SetBoolean();
                //door.SetActive(false);
                door.GetComponent<Collider2D>().enabled = false;

            }
            else

            {
                OnPressurePlateUp?.Invoke();
                AudioManager.instance.PlayDoorClosedClip();
                SetBoolean();
                //door.SetActive(true);
                door.GetComponent<Collider2D>().enabled = true;
            }
            print("this is executed"+isOpen);
            doorAnimController.SetBool("isOpen", isOpen);
            plateAnimController.SetBool("isUp", isUp);
        }
    }
}
