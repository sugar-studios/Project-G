using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectG.UI
{
    public class miniMap : MonoBehaviour
    {
        public Transform player;
        public Transform mapCamera;
        public Transform playerModel;
        public Transform playerCursor;

        private void Update()
        {
            Vector3 camPosition = player.position / 100;
            camPosition.y = 0.567f;
            mapCamera.localPosition = camPosition;

            Vector3 cameraRotation = playerModel.rotation.eulerAngles;

            cameraRotation.x = 90;
            cameraRotation.z = 0;

            Vector3 cursorRotation = new Vector3(-90, 180, playerModel.rotation.eulerAngles.y);

            camPosition.y = 0.02399826f;
            playerCursor.localPosition = camPosition;
            

            mapCamera.rotation = Quaternion.Slerp(mapCamera.rotation, Quaternion.Euler(cameraRotation), 1* Time.deltaTime);
            playerCursor.localRotation = Quaternion.Euler( cursorRotation);

        }
    }
}
