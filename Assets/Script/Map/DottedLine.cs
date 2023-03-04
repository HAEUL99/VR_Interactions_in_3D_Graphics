using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// source code and dot sprite from https://medium.com/@kunaltandon.kt/creating-a-dotted-line-in-unity-ca044d02c3e2
namespace DottedLine
{
    public class DottedLine : MonoBehaviour
    {
        // Inspector fields
        public Sprite Dot;
        [Range(0.01f, 1f)]
        public float Size;
        [Range(0.1f, 2f)]
        public float Delta;

        //Static Property with backing field
        private static DottedLine instance;
        public static DottedLine Instance
        {
            get
            {
                if (instance == null)
                    instance = FindObjectOfType<DottedLine>();
                return instance;
            }
        }

        //Utility fields
        List<Vector3> positions = new List<Vector3>();
        List<GameObject> dots = new List<GameObject>();


        public void DestroyAllDots()
        {



            foreach (var dot in dots)
            {
                Destroy(dot.gameObject);
            }


            dots.Clear();
            positions.Clear();

        }

        GameObject GetOneDot()
        {
            var gameObject = new GameObject();
            gameObject.transform.localScale = Vector3.one * Size;
            gameObject.transform.parent = transform;

            var sr = gameObject.AddComponent<SpriteRenderer>();
            sr.sprite = Dot;
            return gameObject;
        }

        public float DrawDottedLineFromPlayer(Vector3 start, Vector3 end)
        {
            DestroyAllDots();

            Vector3 point = start;
            Vector3 direction = (end - start).normalized;

            while ((end - start).magnitude > (point - start).magnitude)
            {
                positions.Add(point);
                point += (direction * Delta);
            }

            return Vector3.Distance(start, end);


        }

        public float DrawDottedLineFromObj(Vector3 start, Vector3 end)
        {
            Vector3 point = start;
            Vector3 direction = (end - start).normalized;

            while ((end - start).magnitude > (point - start).magnitude)
            {
                positions.Add(point);
                point += (direction * Delta);
            }

            return Vector3.Distance(start, end);

        }



        public void Render()
        {
            foreach (var position in positions)
            {
                var g = GetOneDot();
                g.transform.position = position;
                g.transform.rotation = Quaternion.Euler(90, 0, 0);
                dots.Add(g);
            }
        }
    }
}