using UnityEngine;

public class TutorialArrowComponent : MonoBehaviour
{
    [Header("Tutorial target")]
    public Transform tutorialPoint;
    public Transform tutorailTarget;
    public float tutorialHeight = 2;
    public float closeDistance = 3;
    [Header("Primary target")]
    public Transform PrimaryPoint;
    public Transform PrimaryTarget;
    public float PrimaryHeight = 2;
    [Header("OBJECTS")]
    [SerializeField] Transform arrowWay;
    [SerializeField] Transform arrowObj;
    [SerializeField] float lerpCoeff = 25;
    bool closeDist;
    Transform currentTarget;
    Transform currentPoint;
    Transform cam;
    private void Start()
    {
        arrowWay.gameObject.SetActive(false);
        arrowObj.gameObject.SetActive(false);
        cam = Camera.main.transform;
    }
    public void Set(Transform Point = null, Transform Target = null, float Height = 2)
    {
        tutorialPoint = Point;
        tutorailTarget = Target;
        tutorialHeight = Height;
    }
    public void SetPrimary(Transform Point = null, Transform Target = null, float Height = 2)
    {
        PrimaryPoint = Point;
        PrimaryTarget = Target;
        PrimaryHeight = Height;
    }
    public void Upd()
    {
        PrimaryHeight = PrimaryTarget ? PrimaryHeight : tutorialHeight;
        currentTarget = PrimaryTarget ? PrimaryTarget : tutorailTarget;
        currentPoint = PrimaryPoint ? PrimaryPoint : tutorialPoint;

        if (currentPoint && currentTarget)
        {
            closeDist = DistanceXZ(currentPoint.position, currentTarget.position) > closeDistance;

            transform.position = closeDist ?
                Vector3.Lerp(transform.position, currentPoint.position, Time.deltaTime * lerpCoeff)
                : transform.position = currentTarget.position + Vector3.up * tutorialHeight;

            transform.LookAt(closeDist ? currentTarget : cam);

            arrowObj.gameObject.SetActive(!closeDist);
            arrowWay.gameObject.SetActive(closeDist);

            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }
        else
        {
            arrowWay.gameObject.SetActive(false);
            arrowObj.gameObject.SetActive(false);
        }
    }

    public float DistanceXZ(Vector3 pos, Vector3 point)
    {
        return Mathf.Sqrt((pos.x - point.x) * (pos.x - point.x) + (pos.z - point.z) * (pos.z - point.z));
    }
}
