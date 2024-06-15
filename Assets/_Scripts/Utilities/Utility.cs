using UnityEngine;

namespace _Scripts.Utilities
{
    public static class Utility
    {
        private static Camera _mainCamera;
        
        public static Vector3 GetMouseWorldPosition()
        {
            if(_mainCamera == null) _mainCamera = Camera.main;
            var mouseWorldPosition = _mainCamera!.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPosition.z = 0;
            return mouseWorldPosition;
        }
    }
}
