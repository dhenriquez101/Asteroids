using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{

    public float speed = 10f;
    public float maxLifeTime = 1f;
    public Vector3 targetVector;
    public GameObject meteorPrefab;

    void Start()
    {
        Destroy(gameObject, maxLifeTime);
    }
    void Update()
    {
        transform.Translate(speed * targetVector * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
            SplitMeteor(collision);
        }
        else if (collision.gameObject.tag == "Enemy2")
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
            IncreaseScore();
        }
    }

    private void SplitMeteor(Collision collision)
    {
        GameObject miniMeteor1 = Instantiate(meteorPrefab, collision.transform.position, Quaternion.identity);
        GameObject miniMeteor2 = Instantiate(meteorPrefab, collision.transform.position, Quaternion.identity);

        miniMeteor1.transform.localScale = Vector3.one / 5;
        miniMeteor2.transform.localScale = Vector3.one / 5;

        miniMeteor1.GetComponent<Rigidbody>().useGravity = false;
        miniMeteor2.GetComponent<Rigidbody>().useGravity = false;

        miniMeteor1.tag = "Enemy2";
        miniMeteor2.tag = "Enemy2";

        Vector3 bulletDirection = targetVector;
        bulletDirection.z = 0;
        bulletDirection.Normalize();

        float angle = 145f;

        Vector3 trajectory1 = Quaternion.AngleAxis(angle, Vector3.forward) * bulletDirection;
        Vector3 trajectory2 = Quaternion.AngleAxis(-angle, Vector3.forward) * bulletDirection;

        float fragmentSpeed = speed / 2f;

        miniMeteor1.GetComponent<Rigidbody>().velocity = trajectory1 * fragmentSpeed;
        miniMeteor2.GetComponent<Rigidbody>().velocity = trajectory2 * fragmentSpeed;
    }

    private void IncreaseScore()
    {
        Player.SCORE++;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        GameObject go = GameObject.FindGameObjectWithTag("UI");
        go.GetComponent<Text>().text = "Score: " + Player.SCORE;
    }
}
