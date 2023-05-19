using UnityEngine;

public static class RaycastUtils
{
    public static T GetSelectedObject<T>() where T : MonoBehaviour
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hitInfo))
        {
            var hitInfoComponent = hitInfo.collider.gameObject.GetComponent<T>();
            return hitInfoComponent;
        }
        return null;
    }
}