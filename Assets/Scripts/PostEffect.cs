using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This Component applies an outline effect to objects that are on the "Selection" layer. 
/// It creates a custom camera for rendering the current camera viewport with the DrawSimple Shader
/// </summary>
public class PostEffect : MonoBehaviour
{
    private static int _scenePropertyId = Shader.PropertyToID("_SceneTex");

    [SerializeField]
    private Shader PostOutline;
    [SerializeField]
    private Shader DrawSimple;

    private Camera AttachedCamera;
    private Camera TempCam;
    private Material Post_Mat;
    // public RenderTexture TempRT;
    private int _cullingMask;

    void Start () 
    {
        AttachedCamera = GetComponent<Camera>();
        TempCam = new GameObject().AddComponent<Camera>();
        TempCam.enabled = false;
        Post_Mat = new Material(PostOutline);
        _cullingMask = 1 << LayerMask.NameToLayer("Selection");
    }
 
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        //set up a temporary camera
        TempCam.CopyFrom(AttachedCamera);
        TempCam.clearFlags = CameraClearFlags.SolidColor;
        TempCam.backgroundColor = new Color(0,0,0,0);
 
        //cull any layer that isn't the outline
        TempCam.cullingMask = _cullingMask;
 
        //make the temporary rendertexture
        RenderTexture TempRT = new RenderTexture(source.width, source.height, 0, RenderTextureFormat.R8);
 
        //put it to video memory
        TempRT.Create();
 
        //set the camera's target texture when rendering
        TempCam.targetTexture = TempRT;
 
        //render all objects this camera can render, but with our custom shader.
        //TempCam.Render();
        TempCam.RenderWithShader(DrawSimple,"");
        Post_Mat.SetTexture(_scenePropertyId, source);
        //copy the temporary RT to the final image    
        Graphics.Blit(TempRT, destination, Post_Mat);
 
        //release the temporary RT
        TempRT.Release();
    }

}
