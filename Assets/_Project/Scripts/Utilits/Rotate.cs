using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Project.Scripts.Utilits
{
    internal class Rotate : MonoBehaviour
    {
        [SerializeField] private Vector3 rotationSpeed;

        private void Update()
        {
            transform.Rotate(rotationSpeed * Time.deltaTime);
        }
    }
}
