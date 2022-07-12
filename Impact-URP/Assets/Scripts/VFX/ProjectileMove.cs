using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    public float speed;
    public float destroyTime;
    [Header("Effects")]
    public Object destroyEffect;
    public Object groundEffect;
    public Object rockEffect;
    public Object steelEffect;
    public Object woodEffect;
    public Object waterEffect;
    public Object bloodEffect;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Destroy", destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (speed != 0)
        {
            transform.position += transform.forward * (speed * Time.deltaTime);
        }
        else
        {
            Debug.Log("No Speed");
        }
    }

    void Destroy()
    {
        Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Untagged"))
        {
            Instantiate(groundEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Rock"))
        {
            Instantiate(rockEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Metal"))
        {
            Instantiate(steelEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Wood"))
        {
            Instantiate(woodEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Water"))
        {
            Instantiate(waterEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            Instantiate(bloodEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            Instantiate(bloodEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
