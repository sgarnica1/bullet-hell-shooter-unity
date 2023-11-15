using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MotherShip : MonoBehaviour
{
  public GameObject BulletPrefab;
  public TextMeshProUGUI counterText;
  public GameObject Player;
  private float step = 2.0f;
  private int lines = 40;
  private float angle = 0.0f;
  private int currentBullets = 0;
  private float shootingDuration = 10f;


  /// <summary>
  /// Este método se ejecuta al inicio
  /// </summary>
  void Start()
  {
    StartCoroutine(Shoot());
  }

  /// <summary>
  /// Este método se ejecuta en cada frame
  /// </summary>
  void Update()
  {
    updateCounter();
  }


  /// <summary>
  /// Dispara las balas en diferentes patrones
  /// </summary>
  /// <returns></returns>
  private IEnumerator Shoot()
  {
    yield return StartCoroutine(ShootAlternate());
    yield return StartCoroutine(ShootSpiral(12));
    yield return StartCoroutine(ShootTan());
    yield return StartCoroutine(ShootAll(10));
  }

  /// <summary>
  /// Dispara las balas utilizando la función tangente
  /// </summary>
  /// <returns></returns>
  private IEnumerator ShootTan()
  {
    float startTime = Time.time;
    angle = 360 / lines;

    while (Time.time - startTime < shootingDuration)
    {
      for (int i = 0; i < lines; i++)
      {
        float extraSpeed = (float)(Mathf.Abs(Mathf.Tan(5 * (angle * i) * Mathf.PI / 360)) / 0.1);
        Instantiate(
          BulletPrefab,
          Player.transform.position,
          Quaternion.identity)
          .GetComponent<Bullet>().Init((i * angle), extraSpeed);
        currentBullets++;
      }
      yield return new WaitForSeconds(step / 5);
    }
  }

  /// <summary>
  /// Dispara las balas en espiral
  /// </summary>
  /// <param name="bullets"></param>
  /// <returns></returns>
  private IEnumerator ShootSpiral(int bullets = 10)
  {
    float startTime = Time.time;
    angle = 360 / bullets;

    while (Time.time - startTime < shootingDuration)
    {
      for (int i = 0; i < 360; i += bullets)
      {
        for (int j = 0; j < 360; j += (int)(angle))
        {
          Instantiate(BulletPrefab, Player.transform.position, Quaternion.identity)
              .GetComponent<Bullet>().Init(i + j);
        }
        currentBullets += bullets;
        yield return new WaitForSeconds(step / 20);
      }

      new WaitForSeconds(step / 10);

      for (int i = 360; i > 0; i -= bullets)
      {
        for (int j = 360; j > 0; j -= (int)(angle))
        {
          Instantiate(BulletPrefab, Player.transform.position, Quaternion.identity)
              .GetComponent<Bullet>().Init(i + j);
        }
        currentBullets += bullets;
        yield return new WaitForSeconds(step / 20);
      }
    }
  }

  /// <summary>
  /// Dispara las balas en espiral y múltiples direcciones al mismo tiempo
  /// </summary>
  /// <param name="bullets"></param>
  /// <returns></returns>
  private IEnumerator ShootAll(int bullets = 10)
  {
    float startTime = Time.time;
    angle = 360 / bullets;

    while (Time.time - startTime < shootingDuration)
    {
      for (int i = 0; i < 360; i += bullets)
      {
        for (int j = 0; j < 360; j += (int)(angle))
        {
          float nextAngle = (float)(Mathf.Abs(Mathf.Sin(Mathf.Tan(i))) / 0.1);
          Instantiate(BulletPrefab, Player.transform.position, Quaternion.identity)
              .GetComponent<Bullet>().Init(i + j, nextAngle);
        }
        currentBullets += bullets;
        yield return new WaitForSeconds(step / 20);
      }

      new WaitForSeconds(step / 10);

      for (int i = 360; i > 0; i -= bullets)
      {
        for (int j = 360; j > 0; j -= (int)(angle))
        {
          float nextAngle = (float)(Mathf.Abs(Mathf.Sin(Mathf.Tan(i))) / 0.1);
          Instantiate(BulletPrefab, Player.transform.position, Quaternion.identity)
              .GetComponent<Bullet>().Init(i + j, nextAngle);
        }
        currentBullets += bullets;
        yield return new WaitForSeconds(step / 20);
      }
    }
  }

  /// <summary>
  /// Dispara las balas en dos líneas alternas
  /// </summary>
  /// <returns></returns>
  private IEnumerator ShootAlternate()
  {
    float startTime = Time.time;
    bool even = true;
    angle = 360 / lines;

    while (Time.time - startTime < shootingDuration)
    {
      for (int i = 0; i < lines; i++)
      {
        if (i % 2 == 0 && even)
        {
          float extraSpeed = (float)(Mathf.Abs(Mathf.Sin(10 * (angle * i) * Mathf.PI / 360)) / 0.1);
          Instantiate(BulletPrefab,
            Player.transform.position,
            Quaternion.identity)
          .GetComponent<Bullet>().Init((i * angle), extraSpeed);
          currentBullets++;
        }

        if (i % 2 != 0 && !even)
        {
          float extraSpeed = (float)(Mathf.Abs(Mathf.Sin(10 * (angle * i) * Mathf.PI / 360)) / 0.1);
          Instantiate(BulletPrefab,
            Player.transform.position,
            Quaternion.identity)
          .GetComponent<Bullet>().Init((i * angle), extraSpeed);
          currentBullets++;
        }
      }
      if (even) even = false;
      else even = true;
      yield return new WaitForSeconds(step / 5);
    }
  }


  /// <summary>
  /// Update the counter text
  /// </summary>
  private void updateCounter()
  {
    counterText.text = "Bullets: " + currentBullets.ToString();
  }


  /// <summary>
  /// Get the current number of bullets
  /// </summary>
  /// <returns>currentBullets</returns>
  public int getCurrentBullets()
  {
    return currentBullets;
  }

  /// <summary>
  /// Set the current number of bullets
  /// </summary>
  /// <param name="newBullets"></param>
  public void setCurrentBullets(int newBullets)
  {
    currentBullets = newBullets;
  }
}
