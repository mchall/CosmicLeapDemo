using UnityEngine;
using System.Collections;

public class Advert : MonoBehaviour
{
    public int Index;

    void Start()
    {
        StartCoroutine(Change());
    }

    IEnumerator Change()
    {
        if (enabled)
        {
            yield return new WaitForSeconds(Random.Range(5f, 5.3f));

            var newIndex = Index + 1;
            if (newIndex == 11)
                newIndex = 1;

            string path = "Adverts/advert" + newIndex;
            var resource = Resources.Load(path) as GameObject;

            GameObject newAdvert = Instantiate(resource);
            newAdvert.transform.position = transform.position;
            newAdvert.transform.localRotation = transform.localRotation;
            newAdvert.transform.localScale = transform.localScale;

            Destroy(gameObject);
        }
    }
}