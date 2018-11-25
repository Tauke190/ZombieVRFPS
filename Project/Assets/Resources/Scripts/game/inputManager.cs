using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class inputManager : MonoBehaviour 
{
    public enum viewPrefs { Monitor, Headset };
    public enum controlPrefs { KeyboardAndMouse, Gamepad, HandControllers };

    public viewPrefs viewingPreferance = viewPrefs.Headset;
    public controlPrefs controllerPreferance = controlPrefs.HandControllers;


    public static int viewMode;
    public static int controlMode;

    public static Transform[] handControllers = new Transform[2];
    public static Transform headMountedDisplay;


    private bool gamepadDetected = false;
    private bool handControllersDetected = false;

    private bool hmdDetected = false;

    void Awake()
    {
        string[] joystickNameList = Input.GetJoystickNames();
        bool[] controllerDetected = new bool[2];

        for (int i = 0; i < joystickNameList.Length; i++)
        {
            switch (joystickNameList[i])
            {
                case "Controller (XBOX 360 For Windows)":
                    gamepadDetected = true;
                    break;

                case "OpenVR Controller - Left":
                    controllerDetected[0] = true;
                    break;

                case "OpenVR Controller - Right":
                    controllerDetected[1] = true;
                    break;
            }
        }


        if (GameObject.FindGameObjectWithTag("Player"))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Valve.VR.InteractionSystem.Player vrPlayerSystem = player.GetComponent<Valve.VR.InteractionSystem.Player>();

            handControllers[0] = vrPlayerSystem.hands[0].transform;
            handControllers[1] = vrPlayerSystem.hands[1].transform;
            headMountedDisplay = vrPlayerSystem.hmdTransforms[0].transform;


            hmdDetected = XRDevice.isPresent;
            handControllersDetected = (controllerDetected[0] && controllerDetected[1]);
        }


        SetViewMode();
        SetControlMode();


        StereoTargetEyeMask targetEye;

        switch (viewMode)
        {
            case 0:
                targetEye = StereoTargetEyeMask.None;
                break;

            case 1:
                targetEye = StereoTargetEyeMask.Both;
                break;

            default:
                targetEye = StereoTargetEyeMask.None;
                break;
        }

        Camera.main.stereoTargetEye = targetEye;
    }

    void SetViewMode()
    {
        if (viewingPreferance == viewPrefs.Headset && !hmdDetected)
        {
            viewingPreferance = viewPrefs.Monitor;
            Debug.Log("no headset detected, using monitor instead");
        }

        viewMode = (int)viewingPreferance;
    }

    void SetControlMode()
    {
        if (controllerPreferance == controlPrefs.HandControllers && !handControllersDetected)
        {
            if (gamepadDetected)
            {
                controllerPreferance = controlPrefs.Gamepad;
                Debug.Log("no hand controller detected, using detected gamepad instead");
            }
            else
            {
                controllerPreferance = controlPrefs.KeyboardAndMouse;
                Debug.Log("no hand controller or gamepad detected, using keyboard and mouse instead");
            }
        }

        if (controllerPreferance == controlPrefs.Gamepad && !gamepadDetected)
        {
            controllerPreferance = controlPrefs.KeyboardAndMouse;
            Debug.Log("no gamepad detected, using keyboard and mouse instead");
        }

        controlMode = (int)controllerPreferance;
    }
}
