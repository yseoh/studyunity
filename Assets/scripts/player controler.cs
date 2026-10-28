using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        
    }

    private float _speed = 5.0f;
    [SerializeField] private float _jumpForce = 500.0f;
    private Rigidbody2D _rb;
    private bool _isJumping = false;
   

    // Update is called once per frame ㅁㄹㄵ ㅈㅎ ㅁㄺㄷ ㅇㅇ ㅈㄹ ㅈㄱㄳㄷ 
    void Update()
    {
        transform.Translate(_speed * Time.deltaTime * Input.GetAxis("Horizontal") * Vector3.right);

        if (Input.GetKeyDown(KeyCode.Space) && !_isJumping)
        {
            _isJumping = true;
            _rb.AddForce(Vector2.up * _jumpForce);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        _isJumping = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
       
        
        if (other.CompareTag("Spike"))
        {
            GameManager.Instance.ResetMap();
        }

        if (other.CompareTag("Portal"))
        {
            GameManager.Instance.ToNextMap();
        }
    }
}