using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    [Header("Vision Settings")]
    public float viewRadius =5f;
    [Range(0, 360)]
    public float viewAngle = 60f;
    public LayerMask obstacleMask;
    public LayerMask playerMask;
    public MeshFilter viewMeshFilter;
    public Material viewMaterial;

    private Mesh viewMesh;
    private List<Transform> visibleTargets = new List<Transform>();

    private EnemyAI enemyAI;
    private EnemyLevelController enemyLevelController;
    [SerializeField] private GameController gameController;

    private void Awake()
    {
        enemyAI = GetComponent<EnemyAI>();
        enemyLevelController = GetComponent<EnemyLevelController>();
    }

    void Start()
    {
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        viewMeshFilter.mesh = viewMesh;
        viewMeshFilter.GetComponent<MeshRenderer>().material = viewMaterial;
    }

    void LateUpdate()
    {
        DrawFieldOfView();
        FindVisibleTargets();
    }

    private Collider[] targetsInViewRadius;
    void FindVisibleTargets()
    {

        targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, playerMask);
        foreach (Collider target in targetsInViewRadius)
        {
            Transform targetTransform = target.transform;
            Vector3 dirToTarget = (targetTransform.position - transform.position).normalized / 2;

            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle /2)
            {
                float distToTarget = Vector3.Distance(transform.position, targetTransform.position);

                if (!Physics.Raycast(transform.position, dirToTarget, distToTarget, obstacleMask))
                {
                    PlayerLevelController playerLevel = targetTransform.GetComponent<PlayerLevelController>();

                    if (playerLevel != null && enemyLevelController.enemyLevel > playerLevel.GetCurrentLevel())
                    {
                      
                        enemyAI.AttackPlayer();
                        if (target != null)
                        {
                            playerLevel.GetDeathImage();
                            playerLevel.playerLevelText.enabled = false;
                            StartCoroutine(destroyplayerCoroutune(target));
                        }
                        else
                        {
                            gameController.GameOver();
                            //oyuncu öldü yeniden başlatt
                            Debug.Log("oyuncu bulunamadı");
                        }


                    }
                }
            }
        }
    }

    IEnumerator destroyplayerCoroutune(Collider target)
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("bekliyor");
        if (target != null)
        {
            Destroy(target.gameObject);

        }
        else
        {
            gameController.GameOver();
            // oyuncu öldü yeniden başlat
            Debug.Log("oyuncu bulunamadı");
        }

    }

 
    void DrawFieldOfView()
    {
        int stepCount = Mathf.RoundToInt(viewAngle * 2);
        float stepAngleSize = viewAngle / stepCount;

        List<Vector3> viewPoints = new List<Vector3>();
        for (int i = 0; i <= stepCount; i++)
        {
            float angle = transform.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;
            ViewCastInfo newViewCast = ViewCast(angle);
            viewPoints.Add(newViewCast.point);
        }

        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];

        vertices[0] = Vector3.zero;
        for (int i = 0; i < vertexCount - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);

            if (i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }

        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }

    ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 dir = DirFromAngle(globalAngle, true);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, dir, out hit, viewRadius, obstacleMask))
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }
        else
        {
            return new ViewCastInfo(false, transform.position + dir * viewRadius, viewRadius, globalAngle);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, viewRadius);

        Vector3 viewAngleA = DirFromAngle(-viewAngle / 2, false);
        Vector3 viewAngleB = DirFromAngle(viewAngle / 2, false);

        Gizmos.DrawLine(transform.position, transform.position + viewAngleA * viewRadius);
        Gizmos.DrawLine(transform.position, transform.position + viewAngleB * viewRadius);
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    public List<Transform> GetVisibleTargets()
    {
        return visibleTargets;
    }

    public struct ViewCastInfo // Eklendi
    {
        public bool hit;
        public Vector3 point;
        public float dst;
        public float angle;

        public ViewCastInfo(bool _hit, Vector3 _point, float _dst, float _angle)
        {
            hit = _hit;
            point = _point;
            dst = _dst;
            angle = _angle;
        }
    }
}
