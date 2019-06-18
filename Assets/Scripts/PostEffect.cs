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
    private Shader _postOutline;
    [SerializeField]
    private Shader _drawSimple;

    private Camera _attachedCamera;
    private Camera _tempCam;
    private Material _postMat;
    // public RenderTexture TempRT;
    private int _cullingMask;

    void Start () 
    {
        _attachedCamera = GetComponent<Camera>();
        _tempCam = new GameObject().AddComponent<Camera>();
        _tempCam.enabled = false;
        _postMat = new Material(_postOutline);
        _cullingMask = 1 << LayerMask.NameToLayer("Selection");
    }
 
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        //set up a temporary camera
        _tempCam.CopyFrom(_attachedCamera);
        _tempCam.clearFlags = CameraClearFlags.SolidColor;
        _tempCam.backgroundColor = new Color(0,0,0,0);
 
        //cull any layer that isn't the outline
        _tempCam.cullingMask = _cullingMask;
 
        //make the temporary rendertexture
        RenderTexture TempRT = new RenderTexture(source.width, source.height, 0, RenderTextureFormat.R8);
 
        //put it to video memory
        TempRT.Create();
 
        //set the camera's target texture when rendering
        _tempCam.targetTexture = TempRT;
 
        //render all objects this camera can render, but with our custom shader.
        //TempCam.Render();
        _tempCam.RenderWithShader(_drawSimple,"");
        _postMat.SetTexture(_scenePropertyId, source);
        //copy the temporary RT to the final image    
        Graphics.Blit(TempRT, destination, _postMat);
 
        //release the temporary RT
        TempRT.Release();
    }

}
