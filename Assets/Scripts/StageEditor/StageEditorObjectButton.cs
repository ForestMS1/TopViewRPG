using UnityEngine;
using UnityEngine.UI;

public class StageEditorObjectButton : MonoBehaviour
{
    public GameObject selected;
    private Button button;
    public GameObject targetObject;

    public StageObjectData data;
    
    void Start()
    {
        selected = transform.GetChild( 0 ).gameObject;
        button = GetComponent<Button>( );
        button.onClick.AddListener( () =>
            StageEditor.instance.SelectObject( this.gameObject )
         );
    }

    public void Init( GameObject obj )
    {
        targetObject = obj;
        data = new StageObjectData
        {
            objectID = obj.name,
            position = Vector3Int.zero,
            rotation = Quaternion.identity
        };
    }

    public void SetPosition( Vector3Int pos )
    {
        data.position = pos;
    }

    public void SetRotation( Quaternion rot )
    {
        data.rotation = rot;
    }
}