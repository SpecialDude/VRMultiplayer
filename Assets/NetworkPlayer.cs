using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Photon.Pun;
using Unity.XR.CoreUtils;
using UnityEngine.XR.Interaction.Toolkit;

public class NetworkPlayer : MonoBehaviour
{
    public Transform head;
    public Transform lefthand;
    public Transform righthand;

    public Animator leftHandAnimator;
    public Animator rightHandAnimator;

    private PhotonView photonView;
    private Transform headRig;
    private Transform lefthandRig;
    private Transform righthandRig;

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        //XROrigin rig = FindObjectOfType<XROrigin>();
        // XRRig rig = FindObjectOfType<XRRig>();

        // headRig = rig.transform.Find("Camera Offset/Main Camera");
        // lefthandRig = rig.transform.Find("Camera Offset/LeftHand Controller");
        // lefthandRig = rig.transform.Find("Camera Offset/RightHand Controller");

        if (photonView.IsMine)
        {
            foreach (var item in GetComponentsInChildren<Renderer>())
            {
                item.enabled = false;
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            // righthand.gameObject.SetActive(false);
            // lefthand.gameObject.SetActive(false);
            // head.gameObject.SetActive(false);


            // NewMapPosition(head, headRig);
            // NewMapPosition(lefthand, lefthandRig);
            // NewMapPosition(righthand, righthandRig);

            UpdateHandAnimation(InputDevices.GetDeviceAtXRNode(XRNode.LeftHand), leftHandAnimator);
            UpdateHandAnimation(InputDevices.GetDeviceAtXRNode(XRNode.RightHand), rightHandAnimator);

            MapPosition(head, XRNode.Head);
            MapPosition(lefthand, XRNode.LeftHand);
            MapPosition(righthand, XRNode.RightHand);
        }

    }

    void UpdateHandAnimation(InputDevice targetDevice, Animator handAnimator)
    {
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            handAnimator.SetFloat("Trigger", triggerValue);
        }
        else
        {
            handAnimator.SetFloat("Trigger", 0);
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            handAnimator.SetFloat("Grip", gripValue);
        }
        else
        {
            handAnimator.SetFloat("Grip", 0);
        }
    }

    void MapPosition(Transform target, XRNode node)
    {
        InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 position);
        InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rotation);

        target.position = position;
        target.rotation = rotation;
    }

    void NewMapPosition(Transform target, Transform rigTransform)
    {
        target.position = rigTransform.position;
        target.rotation = rigTransform.rotation;
    }
}
