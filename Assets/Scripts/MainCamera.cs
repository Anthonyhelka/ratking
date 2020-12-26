using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
  [SerializeField] private GameObject _player;

  void Awake()
  {
    _player = GameObject.Find("Player");
  }

  void Update()
  {
    transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y, transform.position.z);
  }
}
