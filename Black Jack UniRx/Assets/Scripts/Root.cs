using UnityEngine;

public class Root : MonoBehaviour
{
    
    [SerializeField]
    private O4koView m_View;

    private void Start()
    {
        var model = new O4koModel(100);
        var presenter = new O4koPresenter(m_View,model);
        presenter.Init();
    }

    
}