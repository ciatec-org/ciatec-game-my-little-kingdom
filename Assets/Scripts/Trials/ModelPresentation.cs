using UnityEngine;
using UnityEngine.UI;

/// <summary>Displays baked mesh assets through an isolated orthographic camera.</summary>
public sealed class ModelPresentation : MonoBehaviour
{
    public RawImage surface;
    public Transform rig;
    public Camera modelCamera;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;
    RenderTexture target;
    static int nextStage;
    static readonly Quaternion View = Quaternion.Euler(22, -34, 0);
    void Awake()
    {
        rig.SetParent(null, false);
        rig.position = new Vector3(10000 + nextStage++ * 32, 10000, 0);
        target = new RenderTexture(512, 512, 24, RenderTextureFormat.ARGB32)
        {
            name = "Model viewport",
            antiAliasing = 1
        };
        modelCamera.targetTexture = target;
        surface.texture = target;
    }

    public void Present(TrialModel model, bool mirrored, Quaternion rotation)
    {
        meshFilter.sharedMesh = mirrored ? model.mirroredMesh : model.referenceMesh;
        modelCamera.orthographicSize = model.framingRadius / .91f;
        SetPose(rotation);
    }

    public void SetPose(Quaternion rotation)
    {
        meshFilter.transform.localRotation = View * rotation;
    }

    void OnEnable()
    {
        if (rig != null)
            rig.gameObject.SetActive(true);
    }

    void OnDisable()
    {
        if (rig != null)
            rig.gameObject.SetActive(false);
    }

    void OnDestroy()
    {
        if (!Application.isPlaying)
            return;
        if (modelCamera != null)
            modelCamera.targetTexture = null;
        if (target != null)
        {
            target.Release();
            Destroy(target);
        }

        if (rig != null)
            Destroy(rig.gameObject);
    }
}
