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

    [SerializeField]
    private Animator doorAnimController;

    void SetBoolean()
    {
        isOpen = !isOpen;

    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            print("I'm a player");

            if (!isOpen)
            {
                OnPressurePlateDown?.Invoke();
                SetBoolean();
                door.SetActive(false);

            }
            else

            {
                OnPressurePlateUp?.Invoke();
                SetBoolean();
                door.SetActive(true);
            }
            doorAnimController.SetBool("isOpen", isOpen);
        }
    }
}
