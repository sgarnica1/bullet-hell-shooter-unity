using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is attached to the bullet prefab and is responsible for moving the bullet
/// </summary>
public class Bullet : MonoBehaviour
{
  private float angle;
  private float extraSpeed = 0.0f;

  /// <summary>
  /// Define el ángulo y la velocidad extra de la bala
  /// </summary>
  /// <param name="newAngle">newAngle</param>
  public void Init(float angle, float extraSpeed = 0.0f)
  {
    this.angle = angle;
    this.extraSpeed = extraSpeed;
  }


  void Update()
  {
    float speed = 10.0f;
    Matrix4x4 rotationMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 0, angle));
    Vector3 direction = rotationMatrix.MultiplyVector(Vector3.down);

    transform.Translate(direction * Time.deltaTime * (speed + extraSpeed));
  }


  /// <summary>
  /// Esta función se ejecuta cuando la bala sale de la pantalla para destruirla
  /// </summary>
  void OnBecameInvisible()
  {
    GameObject enemy = GameObject.Find("MotherShip");

    // Decrementar el contador de balas
    int currentBullets = enemy.GetComponent<MotherShip>().getCurrentBullets();
    enemy.GetComponent<MotherShip>().setCurrentBullets(currentBullets - 1);

    // Destruir la bala
    Destroy(gameObject);
  }
}
