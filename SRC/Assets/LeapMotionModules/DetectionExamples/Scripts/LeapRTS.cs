using UnityEngine;

namespace Leap.Unity {

  /// <summary>
  /// Use this component on a Game Object to allow it to be manipulated by a pinch gesture.  The component
  /// allows rotation, translation, and scale of the object (RTS).
  /// </summary>
  public class LeapRTS : MonoBehaviour {

    public float showX;

    float minDist = 0.05f;
    public bool isPickedUp;
    public Rigidbody rbd;
    public int whichHand = -1; // 0 == left, 1 == right, else -1

    float xLeftBound = -2.2f;
    float xRightBound = 0;
    float yLowBound = 3.14f;
    float zFrontBound = -2.4f;
    float otherY = -1;

    private GameStateScript gameStateScript;

    public enum RotationMethod {
      None,
      Single,
      Full
    }

    [SerializeField]
    private PinchDetector _pinchDetectorA;
    public PinchDetector PinchDetectorA {
      get {
        return _pinchDetectorA;
      }
      set {
        _pinchDetectorA = value;
      }
    }

    [SerializeField]
    private PinchDetector _pinchDetectorB;
    public PinchDetector PinchDetectorB {
      get {
        return _pinchDetectorB;
      }
      set {
        _pinchDetectorB = value;
      }
    }

    [SerializeField]
    private RotationMethod _oneHandedRotationMethod;

    [SerializeField]
    private RotationMethod _twoHandedRotationMethod;

    [SerializeField]
    private bool _allowScale = true;

    [Header("GUI Options")]
    [SerializeField]
    private KeyCode _toggleGuiState = KeyCode.None;

    [SerializeField]
    private bool _showGUI = true;

    private Transform _anchor;

    private float _defaultNearClip;

    void Start() {
//      if (_pinchDetectorA == null || _pinchDetectorB == null) {
//        Debug.LogWarning("Both Pinch Detectors of the LeapRTS component must be assigned. This component has been disabled.");
//        enabled = false;
//      }

      GameObject pinchControl = new GameObject("RTS Anchor");
      _anchor = pinchControl.transform;
      _anchor.transform.parent = transform.parent;
      //transform.parent = _anchor;

      isPickedUp = false;
      rbd = this.GetComponent<Rigidbody>();
      gameStateScript = GameObject.Find("GameState").GetComponent<GameStateScript>();
    }

    void Update() {

      if (Input.GetKeyDown(_toggleGuiState)) {
        _showGUI = !_showGUI;
      }

      bool didUpdate = false;
      if(_pinchDetectorA != null)
        didUpdate |= _pinchDetectorA.DidChangeFromLastFrame;
      if(_pinchDetectorB != null)
        didUpdate |= _pinchDetectorB.DidChangeFromLastFrame;

      if (didUpdate) {
         //transform.SetParent(null, true);
        transform.parent = _anchor.parent;
      }

        /*if (_pinchDetectorA != null && _pinchDetectorA.IsActive && 
            _pinchDetectorB != null &&_pinchDetectorB.IsActive) {
            transformDoubleAnchor();
        } else if (_pinchDetectorA != null && _pinchDetectorA.IsActive) {
            transformSingleAnchor(_pinchDetectorA);
        } else if (_pinchDetectorB != null && _pinchDetectorB.IsActive) {
            transformSingleAnchor(_pinchDetectorB);
        }*/

        //float distA = Vector3.Distance (transform.position, _pinchDetectorA.transform.position);
        //float distB = Vector3.Distance (transform.position, _pinchDetectorB.transform.position);
        
        if (_pinchDetectorA != null && _pinchDetectorA.IsActive)
        {
            transformSingleAnchor(_pinchDetectorA);
            whichHand = 0;
        }
        else if (_pinchDetectorB != null && _pinchDetectorB.IsActive)
        {
            transformSingleAnchor(_pinchDetectorB);
            whichHand = 1;
        }
        //else whichHand = -1;


            
     //restrict _anchor movement
      //if(_anchor.position.x > xRightBound)
            //transform.position.Set(xRightBound, transform.position.y, transform.position.z);
        //if(_anchor.position.x < xLeftBound)
            //transform.position.Set(xLeftBound, transform.position.y, transform.position.z);
        /*if(_anchor.position.y < yLowBound && isPickedUp) {
            transform.SetParent(null, true);
            rbd.isKinematic = false;
            whichHand = -1;
            isPickedUp = false;
            transform.position.Set(_anchor.position.x, 3.18f, _anchor.position.z);
        }*/
        //if(_anchor.position.z < zFrontBound)
            //transform.position.Set(transform.position.x, transform.position.y, zFrontBound);
        //
        
        /*if(transform.position.y < -0.5f) {
            transform.parent = _anchor.parent;
            transform.position = new Vector3(transform.position.x, 3.4f, transform.position.z);
            return;
        }*/
        if(transform.position.y < 3.14f) {
            transform.position = new Vector3(transform.position.x, 3.14f, transform.position.z);
        }
        if(transform.position.x < -2.1f) {
            transform.position = new Vector3(-2.1f, transform.position.y, transform.position.z);
        }
        if(transform.position.x > -1.48f) {
            transform.position = new Vector3(-1.48f, transform.position.y, transform.position.z);
        }
        if(transform.position.z < -2.2f) {
            transform.position = new Vector3(transform.position.x, transform.position.y, -2.2f);
        }
        if(transform.position.z > -1.95f) {
            transform.position = new Vector3(transform.position.x, transform.position.y, -1.95f);
        }
        showX = transform.position.z;
        

      float distance = Vector3.Distance (transform.position, _anchor.position);

      if (didUpdate && distance <= minDist && whichHand != -1) {
        rbd.isKinematic = true;
        transform.SetParent(_anchor, true);
        isPickedUp = true;
        gameStateScript.anyObjectPicked = true;
      }

    if (whichHand == 0 && !PinchDetectorA.IsHolding) {
        rbd.isKinematic = false;
        whichHand = -1;
        isPickedUp = false;
        gameStateScript.anyObjectPicked = false;
    }
    else if (whichHand == 1 && !PinchDetectorB.IsHolding) {
        rbd.isKinematic = false;
        whichHand = -1;
        isPickedUp = false;
        gameStateScript.anyObjectPicked = false;
    }
        
        

    }

    void OnGUI() {
      if (_showGUI) {
        GUILayout.Label("One Handed Settings");
        doRotationMethodGUI(ref _oneHandedRotationMethod);
        GUILayout.Label("Two Handed Settings");
        doRotationMethodGUI(ref _twoHandedRotationMethod);
        _allowScale = GUILayout.Toggle(_allowScale, "Allow Two Handed Scale");
      }
    }

    private void doRotationMethodGUI(ref RotationMethod rotationMethod) {
      GUILayout.BeginHorizontal();

      GUI.color = rotationMethod == RotationMethod.None ? Color.green : Color.white;
      if (GUILayout.Button("No Rotation")) {
        rotationMethod = RotationMethod.None;
      }

      GUI.color = rotationMethod == RotationMethod.Single ? Color.green : Color.white;
      if (GUILayout.Button("Single Axis")) {
        rotationMethod = RotationMethod.Single;
      }

      GUI.color = rotationMethod == RotationMethod.Full ? Color.green : Color.white;
      if (GUILayout.Button("Full Rotation")) {
        rotationMethod = RotationMethod.Full;
      }

      GUI.color = Color.white;

      GUILayout.EndHorizontal();
    }

    private void transformDoubleAnchor() {
      _anchor.position = (_pinchDetectorA.Position + _pinchDetectorB.Position) / 2.0f;

      switch (_twoHandedRotationMethod) {
        case RotationMethod.None:
          break;
        case RotationMethod.Single:
          Vector3 p = _pinchDetectorA.Position;
          p.y = _anchor.position.y;
          _anchor.LookAt(p);
          break;
        case RotationMethod.Full:
          Quaternion pp = Quaternion.Lerp(_pinchDetectorA.Rotation, _pinchDetectorB.Rotation, 0.5f);
          Vector3 u = pp * Vector3.up;
          _anchor.LookAt(_pinchDetectorA.Position, u);
          break;
      }

      if (_allowScale) {
        _anchor.localScale = Vector3.one * Vector3.Distance(_pinchDetectorA.Position, _pinchDetectorB.Position);
      }
    }

    private void transformSingleAnchor(PinchDetector singlePinch) {
      _anchor.position = singlePinch.Position;

      switch (_oneHandedRotationMethod) {
        case RotationMethod.None:
          break;
        case RotationMethod.Single:
          Vector3 p = singlePinch.Rotation * Vector3.right;
          p.y = _anchor.position.y;
          _anchor.LookAt(p);
          break;
        case RotationMethod.Full:
          _anchor.rotation = singlePinch.Rotation;
          break;
      }

      _anchor.localScale = Vector3.one;
    }
  }
}
