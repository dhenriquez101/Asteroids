using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float thrustForce = 100f;
    public float rotationSpeed = 120f;

    public GameObject gun, bulletPrefab;

    public float xBorderLimit;
    public float yBorderLimit;

    private Rigidbody _rigid;

    public static int SCORE = 0;
    void Start()
    {
        _rigid = GetComponent<Rigidbody>();

        xBorderLimit = (Camera.main.orthographicSize + 1) * Screen.width / Screen.height;
        yBorderLimit = Camera.main.orthographicSize + 1;
    }

    private void FixedUpdate()
    {
        float rotation = Input.GetAxis("Horizontal") * Time.deltaTime;
        float thrust = Input.GetAxis("Vertical") * Time.deltaTime;

        Vector3 thrustDirection = transform.right;

        _rigid.AddForce(thrustDirection * thrust * thrustForce);

        transform.Rotate(Vector3.forward, -rotation * rotationSpeed);
    }
    void Update()
    {
        var newPos = transform.position;
        if (newPos.x > xBorderLimit)
            newPos.x = -xBorderLimit + 1;
        else if (newPos.x < -xBorderLimit)
            newPos.x = xBorderLimit - 1;
        else if (newPos.y > yBorderLimit)
            newPos.y = -yBorderLimit + 1;
        else if (newPos.y < -yBorderLimit)
            newPos.y = yBorderLimit - 1;
        transform.position = newPos;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet = Instantiate(bulletPrefab, gun.transform.position, Quaternion.identity);

            Bullet balaScript = bullet.GetComponent<Bullet>();
            
            balaScript.targetVector = transform.right;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Enemy2")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            SCORE = 0;
        }
        else
        {
            Debug.Log("He colisionado con otra cosa...");
        }
    }
}
