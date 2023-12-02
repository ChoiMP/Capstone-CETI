using System.Collections;
using UnityEngine;

public class zippermouth : Monster
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public int numberOfBullets = 8;
    float fireRate = 1f;
    float rotationSpeed = 20f;
    float bulletSpeed = 5f;

    [SerializeField] GameObject[] Randpos;
    public override void Start()
    {
        base.Start();
        GetComponent<Animation>().Play("idle");

         StartCoroutine(RandomAttackPattern());
        InvokeRepeating("MoveRandpos", 0f,6);

    }

    private void OnDestroy()
    {
        CancelInvoke("MoveRandpos");
    }

    IEnumerator RandomAttackPattern()
    {
        while (true)
        {
            int randomPattern = Random.Range(0, 3);
           
            switch (randomPattern)
            {
                case 0:
                    FireBulletPattern();
                    break;
                case 1:
                    FireBulletRotationPattern();
                    break;
                case 2:
                    FireUP_DOWN();
                    break;
                default:
                    break;
            }

            float patternInterval = Random.Range(2f,4f);
            yield return new WaitForSeconds(patternInterval);
        }
    }

    void FireBulletPattern()
    {
        numberOfBullets = 8;
        for (int i = 0; i < numberOfBullets; i++)
        {
            float angle = i * (360f / numberOfBullets);
            Quaternion rotation = Quaternion.Euler(0f, angle, 0f);

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, rotation * firePoint.rotation);
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

            bulletRb.velocity = bullet.transform.forward * bulletSpeed;
            Destroy(bulletRb, 8);
        }

        firePoint.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    void FireBulletRotationPattern()
    {
        numberOfBullets = 5;
        fireRate = 0.2f;
        rotationSpeed = 100;

        for (int i = 0; i < numberOfBullets; i++)
        {
            float angleIncrement = 360f / numberOfBullets;
            float angle = i * angleIncrement + Time.time * rotationSpeed;
            Quaternion rotation = Quaternion.Euler(0f, angle, 0f);

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, rotation * firePoint.rotation);
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            bulletRb.velocity = bullet.transform.forward * bulletSpeed;

            Destroy(bulletRb, 8);
        }
    }

    void FireUP_DOWN()
    {
        numberOfBullets = 3;
        fireRate = 1f;
        rotationSpeed = 100;

        for (int i = 0; i < numberOfBullets; i++)
        {
            Vector3 randomDirection = Quaternion.Euler(Random.Range(-45, 45), Random.Range(-45, 45), 0f) * Vector3.up;
            float randomForce = Random.Range(10, 15);

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            bulletRb.velocity = randomDirection * randomForce;
            bulletRb.useGravity = true;

            Destroy(bulletRb, 8);
        }
    }

    //------------·£´ý ÀÌµ¿
    void  MoveRandpos()
    {
        int x = (int)Random.Range(Randpos[0].transform.localPosition.x, Randpos[1].transform.localPosition.x);
        int z = (int)Random.Range(Randpos[0].transform.localPosition.z, Randpos[1].transform.localPosition.z);

        transform.position = new Vector3(x, transform.position.y, z);

    }
}
