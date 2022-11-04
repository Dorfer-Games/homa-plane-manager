using UnityEngine;

public class TutorialArrow2DComponent : MonoBehaviour
{
    public Transform target;
    GameObject uiArrow;

    private void Start()
    {
        uiArrow = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        if (target)
        {
            uiArrow.SetActive(true);
            transform.position = target.position;
        }
        else
            uiArrow.SetActive(false);
    }
}
