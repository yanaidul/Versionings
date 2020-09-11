using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Bird : MonoBehaviour
{
    [SerializeField] private float upForce = 100;
    [SerializeField] private bool isDead;
    [SerializeField] private UnityEvent OnJump, OnDead;
    private Animator animator;
    [SerializeField] private int score;
    [SerializeField] private UnityEvent OnAddPoint;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileSpeed;
    [SerializeField] GameObject gun;
    [SerializeField] AudioClip laserSFX;
    [SerializeField] [Range(0, 1)] float laserSFXVolume = 0.7f;

    private Rigidbody2D rigidBody2d;

    //init variable
    void Start()
    {
        //Mendapatkan komponent ketika game baru berjalan
        rigidBody2d = GetComponent<Rigidbody2D>();

        //Mendapatkan komponen animator pada game object   
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        //Melakukan pengecekan jika belum mati dan klik kiri pada mouse
        if (!isDead && Input.GetMouseButtonDown(0))
        {
            //Burung meloncat
            Jump();
        }

        fire();
    }

    //Fungsi untuk mengecek sudah mati apa belum
    public bool IsDead()
    {
        return isDead;
    }

    //Membuat Burung Mati
    public void Dead()
    {
        //Pengecekan jika belum mati dan value OnDead tidak sama dengan Null
        if (!isDead && OnDead != null)
        {
            //Memanggil semua event pada OnDead
            OnDead.Invoke();
        }

        //Mengeset variable Dead menjadi True
        isDead = true;
        //Destroy(gameObject);

    }

    void Jump()
    {
        //Mengecek rigidbody null atau tidak
        if (rigidBody2d)
        {
            //menghentikan kecepatan burung ketika jatuh
            rigidBody2d.velocity = Vector2.zero;

            //Menambahkan gaya ke arah sumbu y agar burung meloncat
            rigidBody2d.AddForce(new Vector2(0, upForce));
        }

        //Pengecekan Null variable
        if (OnJump != null)
        {
            //Menjalankan semua event OnJump event
            OnJump.Invoke();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //menghentikan Animasi Burung ketika bersentukan dengan object lain
        animator.enabled = false;
    }

    public void AddScore(int value)
    {
        //Menambahkan Score value
        score += value;

        //Pengecekan Null Value
        if (OnAddPoint != null)
        {
            //Memanggil semua event pada OnAddPoint
            OnAddPoint.Invoke();
        }

        //Mengubah nilai text pada score text
        scoreText.text = score.ToString();
    }

    private void fire()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Instantiate(projectile, gun.transform.position, transform.rotation);
            AudioSource.PlayClipAtPoint(laserSFX, Camera.main.transform.position, laserSFXVolume);
        }

    }

}
