using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInteraction : MonoBehaviour {

    private Transform[] playerHand = new Transform[2];




    void Update()
    {
        ControlEquipping();
    }

    void ControlEquipping()
    {
        switch (inputManager.controlMode)
        {
            case 0:
                ManageKeyboard();
                break;

            case 1:
                ManageGamepad();
                break;

            case 2:
                ManageHands();
                break;
        }
    }

    void ManageKeyboard()
    {

    }


    void ManageGamepad()
    {

    }


    void ManageHands()
    {
        for (int i = 0; i < 2; i++)
        {
            Transform selectedHand = playerHand[i];
            Transform availableItem = selectedHand.GetComponent<handProperties>().availableItem;

            if (availableItem)
            {
                itemProperties itemHandler = availableItem.GetComponent<itemProperties>();

                Transform handController = inputManager.handControllers[i];
                //SteamVR_TrackedController controllerProperties = handController.GetComponent<SteamVR_TrackedController>();

                //if (controllerProperties.gripped)
                //{
                    if (!itemHandler.itemEquipped)
                    {
                        //GripItem(selectedHand, availableItem);
                    }
                //}
                else
                {
                    if (itemHandler.itemEquipped)
                    {
                        DropItem(selectedHand, availableItem);
                    }
                }
            }
        }
    }

    void GripItem(Transform hand, Transform item)
    {
        item.parent = hand;
        item.localPosition = Vector3.zero;
        item.localRotation = Quaternion.identity;


        itemProperties itemHandler = item.GetComponent<itemProperties>();
        handProperties handHandler = hand.GetComponent<handProperties>();

        Vector3 handOffset = hand.position - handHandler.gripMount.bounds.center;
        Vector3 itemOffset = item.position - itemHandler.gripMount.bounds.center;

        item.localPosition = itemOffset - handOffset;
        item.RotateAround(itemHandler.gripMount.bounds.center, Vector3.right, handHandler.gripTilt);


        itemHandler.itemEquipped = true;
    }

    void DropItem(Transform hand, Transform item)
    {
        itemProperties itemHandler = item.GetComponent<itemProperties>();

        itemHandler.itemEquipped = false;
    }
}
