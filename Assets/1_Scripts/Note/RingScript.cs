using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingScript : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, NoteRecord.Instance.heart.transform.position, speed * Time.deltaTime);
        if (transform.position.x >= NoteRecord.Instance.heart.transform.position.x)
        {
            gameObject.SetActive(false);
        }
    }
}
