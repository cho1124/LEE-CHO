using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class RopeAcion : MonoBehaviour
{
    [Header("SpringJoint")]
    public float springForce;
    public float springDamper;
    public float springMass;
    public bool isPressed = false;
    public bool isStarted = false;
    public LayerMask myLayer;
    
    LineRenderer lr;
    SpringJoint sj;

    [SerializeField] private Image ToggleImage;
    [SerializeField] private bool isAvailable = false;

    [SerializeField] private Sprite baseSprite;
    [SerializeField] private Sprite aimedSprite;

    Transform mainTr;

    RaycastHit hit;
    private float dis;
    
    private Transform targetTransform = null;

    private void OnEnable()
    {
        ToggleImage.sprite = baseSprite;
        mainTr = Camera.main.transform;

        if (lr == null)
        {
            lr = gameObject.AddComponent<LineRenderer>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        FindObstacle();
        checkSwingAction();

        if (isStarted && lr.positionCount == 2)
        {
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, targetTransform.position);
        }

    }

    void checkSwingAction()
    {
        if (isAvailable && Input.GetMouseButton(0) && !isStarted)
        {
            isPressed = true;
            isStarted = true;
            StartSwing();
        }

        if (isStarted && !Input.GetMouseButton(0))
        {
            isPressed = false;
            isStarted = false;
            EndSwing();
        }
    }

    void StartSwing()
    {
        if (hit.point == Vector3.zero) return;

        lr.positionCount = 2;

        Vector3 newVec = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);

        lr.SetPosition(0, newVec);
        lr.SetPosition(1, targetTransform.position);

        sj = gameObject.AddComponent<SpringJoint>();

        sj.autoConfigureConnectedAnchor = false;        
        sj.connectedAnchor = targetTransform.GetComponent<Collider>().bounds.center;

        sj.spring = springForce;    
        sj.damper = springDamper;   
        sj.massScale = springMass;  

        dis = Vector3.Distance(newVec, targetTransform.position); 

        sj.maxDistance = dis * 0.8f;   
        sj.minDistance = dis * 0.2f;   

    }

    void EndSwing()
    {
        lr.positionCount = 0;

        if (sj != null)
        {
            Destroy(sj);
            sj = null; // 참조 초기화
        }

        targetTransform = null; // 타겟 초기화
    }

    private void FindObstacle()
    {
        // Raycast를 수행하여 충돌 검사
        bool isHit = Physics.Raycast(mainTr.position, mainTr.forward, out RaycastHit newHit, 10f, myLayer);

        
        hit = newHit; // 새 충돌 정보를 갱신
        GetHitAction(isHit, hit.transform);
    }

    private void GetHitAction(bool isHit, Transform hitTransform)
    {
        isAvailable = isHit;

        if (isHit)
        {
            targetTransform = hitTransform;
            ToggleImage.sprite = aimedSprite;
        }
        else
        {
            ToggleImage.sprite = baseSprite;
        }
    }
    
}
