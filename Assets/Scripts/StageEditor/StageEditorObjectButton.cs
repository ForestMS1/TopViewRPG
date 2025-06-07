using UnityEngine;
using UnityEngine.UI;

public class StageEditorObjectButton : MonoBehaviour
{
    public GameObject selected;
    private Button button;
    
    void Start()
    {
        selected = transform.GetChild( 0 ).gameObject;
        button = GetComponent<Button>( );
        button.onClick.AddListener( () =>
            StageEditor.instance.SelectObject( this.gameObject )
         );
    }
}