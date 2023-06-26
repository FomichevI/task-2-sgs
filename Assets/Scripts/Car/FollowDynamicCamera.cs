using System.Collections;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class FollowDynamicCamera : MonoBehaviour
{
    [SerializeField] private bool _isFallow = true;
    [SerializeField] private Transform _playerTrans;
    [SerializeField] private float _followSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private bool _raicingMod = false;
    [Header("������ ������")]
    [SerializeField] private bool _shakeCamera = false;
    #region Editor
#if UNITY_EDITOR
    [CustomEditor(typeof(FollowDynamicCamera))]
    public class FoolowCameraEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            FollowDynamicCamera followCamera = (FollowDynamicCamera)target;
            if(followCamera._shakeCamera)
            {

                EditorGUILayout.BeginVertical();
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Duration", GUILayout.MaxWidth(70));
                followCamera._duration = EditorGUILayout.FloatField(followCamera._duration);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Magnitude", GUILayout.MaxWidth(70));
                followCamera._magnitude = EditorGUILayout.FloatField(followCamera._magnitude);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Noize", GUILayout.MaxWidth(70));
                followCamera._noize = EditorGUILayout.FloatField(followCamera._noize);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndVertical();
            }
        }
    }
#endif
    #endregion
    private float _duration = 1;
    private float _magnitude = 2;
    private float _noize = 2;
    private Transform _pointTrans;
    private Vector3 _noizeOffset;
    private Vector3 _offset;
    private Quaternion _rotationOffset;

    private void Start()
    {
        _offset = transform.position - _playerTrans.position;
        _rotationOffset = transform.rotation * _playerTrans.rotation;
        _noizeOffset = Vector3.zero;
        if (_shakeCamera)
            StartCoroutine(ShakeCameraCor(_duration, _magnitude, _noize));
    }

    private void FixedUpdate()
    {
        if (_isFallow)
        {
            transform.position = Vector3.Lerp(transform.position, _playerTrans.position + transform.rotation * _offset + _noizeOffset,
               _followSpeed * Time.fixedDeltaTime);
            if (_raicingMod)
                transform.rotation = Quaternion.Lerp(transform.rotation, _playerTrans.rotation, _rotationSpeed * Time.fixedDeltaTime);
            else
                transform.rotation = Quaternion.Lerp(transform.rotation, _playerTrans.rotation * _rotationOffset, _rotationSpeed * Time.fixedDeltaTime);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, _pointTrans.position + _noizeOffset, _followSpeed * Time.fixedDeltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, _pointTrans.rotation, _rotationSpeed * Time.fixedDeltaTime);
        }
    }

    public void StartFallow()
    {
        _isFallow = true;
    }
    public void StopFallow(Transform newTrans)
    {
        _isFallow = false;
        _pointTrans = newTrans;
    }

    private IEnumerator ShakeCameraCor(float duration, float magnitude, float noize)
    {
        while (true)
        {
            //�������������� ��������� ���������� �������
            float elapsed = 0f;
            //��������� ��������� ��������� �������
            Vector3 startPosition = transform.localPosition;
            //���������� ��� ����� �� "��������" ���� �������
            Vector2 noizeStartPoint0 = Random.insideUnitCircle * noize;
            Vector2 noizeStartPoint1 = Random.insideUnitCircle * noize;

            //��������� ��� �� ��� ��� ���� �� �������� �����
            while (elapsed < duration)
            {
                //���������� ��� ��������� ���������� �� �������� ������� � ����������� �� ���������� �������
                Vector2 currentNoizePoint0 = Vector2.Lerp(noizeStartPoint0, Vector2.zero, elapsed / duration);
                Vector2 currentNoizePoint1 = Vector2.Lerp(noizeStartPoint1, Vector2.zero, elapsed / duration);
                //������ ����� ������ ��� ������ � �������� � �� ����� ���� ������ �������� �������
                Vector2 cameraPostionDelta = new Vector2(Mathf.PerlinNoise(currentNoizePoint0.x, currentNoizePoint0.y), Mathf.PerlinNoise(currentNoizePoint1.x, currentNoizePoint1.y));
                cameraPostionDelta *= magnitude;

                //���������� ������ � ����� ����������
                _noizeOffset = (Vector3)cameraPostionDelta;

                //����������� ������� ���������� �������
                elapsed += Time.deltaTime;
                //���������������� ���������� ��������, � ��������� ����� ��� ��������� ���������� � ������ �����
                yield return null;
            }
        }
    }
}
