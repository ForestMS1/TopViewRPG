using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.UI;
using TMPro;

public class StageEditorCamera : MonoBehaviour
{
    [Header("Cameras")]
    public CinemachineCamera cineCam;
    public Camera mainCam;
    
    [Header("Variables")]
    public bool isOrthographic = true;

    [Header( "UI Objects" )]
    public TMP_Text camButtonText;

    public static StageEditorCamera instance;
    
    void Awake( )
    {
        if( instance == null )
            instance = this;
        else
            Destroy( this );
    }

    public void ChangeCamMode( )
    {
        isOrthographic = !isOrthographic;
        mainCam.orthographic = isOrthographic;
        camButtonText.text = isOrthographic ? "Isometric" : "Perspective";
    }
}