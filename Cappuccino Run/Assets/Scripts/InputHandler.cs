using UnityEngine;

public class InputHandler : Singleton<InputHandler>
{
    #region Veriables
    public Transform target;
    public float _sensitivity = 0.005f;
    [SerializeField] private bool _isSwerving;
    public bool isGaming;
    public bool isSwerving
    {
        get { return _isSwerving; }
        set { _isSwerving = value; }
    }
    private Vector3 _mouseReference;
    private Vector3 _mouseOffset;
    public Vector3 mouseOffset
    {
        get { return _mouseOffset; }
        set { mouseOffset = value; }
    }
    private Vector3 _transfom = Vector3.zero;

    public bool left, right;


    #endregion

    void FixedUpdate()
    {
        Swerving();
    }

    private void Swerving()
    {
        if (_isSwerving && isGaming)
        {

            // offset
            _mouseOffset = (Input.mousePosition - _mouseReference);

            // apply move x left & right
            _transfom.x = (_mouseOffset.x + _mouseOffset.x) * _sensitivity;

            // move
            target.transform.Translate(_transfom);

            // store new mouse positionn
            _mouseReference = Input.mousePosition;
        }
    }

    void OnMouseDown()
    {
        // moving flag
        _isSwerving = true;

        // store mouse position
        _mouseReference = Input.mousePosition;
    }

    void OnMouseUp()
    {
        // moving flag
        _isSwerving = false;
        right = false;
        left = false;
    }

}
