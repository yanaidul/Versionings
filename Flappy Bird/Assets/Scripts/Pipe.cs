using UnityEngine;

public class Pipe : MonoBehaviour
{
    //Global variable
    [SerializeField] private Bird bird;
    [SerializeField] private float speed = 1;
    [SerializeField] GameObject explosionVFX;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] [Range(0, 1)] float deathSFXVolume = 0.7f;

    //Dipanggil setiap frame
    private void Update()
    {
        //Melakukan pengecekan jika burung belum mati
        if (!bird.IsDead())
        {
            //Membuat pipa bergerak kesebelah kiri dengan kecepatan tertentu
            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
        }
    }

    //Membuat Bird mati ketika bersentuhan dan menjatuhkannya ke ground jika mengenai di atas collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bird bird = collision.gameObject.GetComponent<Bird>();
        Projectile laser = collision.gameObject.GetComponent<Projectile>();

        //Pengecekan Null value
        if (bird)
        {
            //Mendapatkan komponent Collider pada game object
            Collider2D collider = GetComponent<Collider2D>();

            //Melakukan pengecekan Null varibale atau tidak
            if (collider)
            {
                //Menonaktifkan collider
                collider.enabled = false;
            }

            //Burung Mati
            bird.Dead();
        }

        if(laser)
        {
            Destroy(gameObject);
            GameObject explosion = Instantiate(
                    explosionVFX,
                    transform.position,
                    transform.rotation);
            Destroy(explosion, 1f);
            AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);
        }
    }

}