using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GradientEvaluate : MonoBehaviour
{
    public Gradient myGradient = new Gradient();
    public float strobeDuration = 0.5f;
    public Color StandardColor;
    public Material MyMat;

    private void Awake()
    {
        GradientColorKey[] colorKey = new GradientColorKey[8];
        colorKey[0].color = Color.red;
        colorKey[0].time = 0.0f;
        colorKey[1].color = new Color(1f, 0.5f, 0);
        colorKey[1].time = 0.083f;
        colorKey[2].color = Color.yellow;
        colorKey[2].time = 0.250f;
        colorKey[3].color = Color.green;
        colorKey[3].time = 0.416f;
        colorKey[4].color = Color.cyan;
        colorKey[4].time = 0.583f;
        colorKey[5].color = Color.blue;
        colorKey[5].time = 0.750f;
        colorKey[6].color = new Color(0.5f, 0, 1f);
        colorKey[6].time = 0.916f;
        colorKey[7].color = Color.red;
        colorKey[7].time = 1.0f;

        GradientAlphaKey[] alphaKey = new GradientAlphaKey[2];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 1.0f;
        alphaKey[1].time = 1.0f;

        myGradient.SetKeys(colorKey, alphaKey);

        if (TryGetComponent<Renderer>(out Renderer Renderer))
            MyMat = Renderer.material;
        else if (TryGetComponent<MeshRenderer>(out MeshRenderer MeshRenderer))
            MyMat = MeshRenderer.material;
        else if (TryGetComponent<SkinnedMeshRenderer>(out SkinnedMeshRenderer SkinnedMeshRenderer))
            MyMat = SkinnedMeshRenderer.material;

        StandardColor = MyMat.color;
    }

    public void Update()
    {
        float t = Mathf.PingPong(Time.time / (strobeDuration*Time.timeScale), 1f);
        MyMat.SetColor("_Color", myGradient.Evaluate(t));
    }
}
