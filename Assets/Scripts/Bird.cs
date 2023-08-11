using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private bool isClick = false;
    
    public float maxDis = 1;

    public LineRenderer right;
    public LineRenderer left;
    public Transform leftPos;
    public Transform rightPos;

    public Boom boom;

    private TestMyTrail trail;

    [HideInInspector]
    public SpringJoint2D sp;
    private Rigidbody2D rg;

    public float smooth = 3;

    bool canMove = true;

    public AudioClip select;
    public AudioClip fly;

    private void Awake()
    {
        sp = GetComponent<SpringJoint2D>();
        rg = GetComponent<Rigidbody2D>();
        trail = GetComponent<TestMyTrail>();
    }

    void Update()
    {
        if (isClick) {
            // 小鸟跟着鼠标走
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position += new Vector3(0, 0, -Camera.main.transform.position.z);

            // 小鸟最大长度限制
            if (Vector3.Distance(transform.position, rightPos.position) > maxDis) {
                Vector3 pos = (transform.position - rightPos.position).normalized; // 
                pos *= maxDis; // 最大长度向量
                transform.position = pos + rightPos.position;
            }
            Line();
        }

        float positionX = gameObject.transform.position.x;
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(Mathf.Clamp(positionX, 0, 15), Camera.main.transform.position.y, Camera.main.transform.position.z), Time.deltaTime * smooth);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        trail.CleanTrails();
    }


    public void AudioPlay(AudioClip clip) {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }
    private void OnMouseDown()
    {
        if (canMove)
        {
            AudioPlay(select);
            isClick = true;
            rg.isKinematic = true;
        }
    }

    private void OnMouseUp()
    {
        if (canMove)
        {
            isClick = false;
            rg.isKinematic = false;

            Invoke("Fly", 0.1f);

            left.enabled = false;
            right.enabled = false;

            canMove = false;
        }
    }

    private void Fly() {
        AudioPlay(fly);
        trail.ShowTrails();
        sp.enabled = false;
        Invoke("Next", 5.0f);
        
    }

    void Line() {
        left.enabled = true;
        right.enabled = true;

        right.SetPosition(0, rightPos.position);
        right.SetPosition(1, transform.position);

        left.SetPosition(0, leftPos.position);
        left.SetPosition(1, transform.position);
    }

    void Next() {
        GameManager._instance.birds.Remove(this);
        Destroy(gameObject);
        Instantiate(boom, transform.position, Quaternion.identity);
        GameManager._instance.NextBird();
    }
}
