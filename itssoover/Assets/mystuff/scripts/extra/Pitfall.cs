using UnityEngine;
using System.Collections;

public class Pitfall : MonoBehaviour 
{    //aanroepen van alle gebruikte functies
    private bool Fell; 
    private GameObject trappedchild; 
    private float Shakehowlong = 0.2f; 
    private float Shakey = 0.1f; 
    private float traptimer = 3f; 

    void OnTriggerEnter2D(Collider2D other)  //checkt of een gameobject met de peasant tag 
    {                                       //de collider aanraakt van de val
        if (other.CompareTag("peasant")) 
        {
            trappedchild = other.gameObject;
            Fell = true;                      //zo ja, dan is de enemy gevallen in de val


            Rigidbody2D rb = trappedchild.GetComponent<Rigidbody2D>(); 
            if (rb != null)
            {
                rb.velocity = Vector2.zero;        // roept functie aan die de enemy laat stilstaan
            }

            Shakechild();                          //roept functie aan die de enemy laat trillen, 
                                                   //alsof hij probeert te ontsnappen
            Destroy(gameObject, traptimer);       //vernietigt de val
        }
    }

    void Shakechild()               
    {
        if (trappedchild != null)       //als de enemy in de val zit, wordt hier het trillen aangeroepen
        {
            StartCoroutine(Shaking());
        }
    }

    IEnumerator Shaking()
    {
        Vector3 originalPosition = trappedchild.transform.position;  //checkt waar de enemy staat momenteel
        float elapsedTime = 0f;    //begin van checken hoelang de enemy in de val zit
        while (elapsedTime < Shakehowlong)   //laat enemy trillen voor bepaalde tijd met bepaalde sterkte
        {                                     // op de plek waar hij vastzit
            float x = Random.Range(-1f, 1f) * Shakey;
            float y = Random.Range(-1f, 1f) * Shakey;
            trappedchild.transform.position = originalPosition + new Vector3(x, y, 0f);
            elapsedTime += Time.deltaTime;                    //houdt bij hoelang speler vastzit
            yield return null;                               //de enemy volgt weer zijn normale beweegpatronen, 
        }                                                   // zoals in enemyscript staat

        trappedchild.transform.position = originalPosition; //enemy staat weer op zijn positie van voordat hij
    }                                                       //in de val was gevallen, oftewel voor het trillen

    void Update()
    {          
 
            if (Time.timeSinceLevelLoad >= traptimer)   //na 3 seconden in de val gezeten te hebben 
            {                                           //roept het de functie aan die de enemy had van voor de val
                ChildisFree();
            }
    }

    void ChildisFree()
    {
        if (trappedchild != null)  //als de enemy niet meer vastzit, dan 
        {
       
            Rigidbody2D rb = trappedchild.GetComponent<Rigidbody2D>(); //not sure wat dit doet, want ik heb ook state enums in een apart script die de beweging controllen
            if (rb != null)
            {
              
                rb.velocity = new Vector2(2f, 0f); 
            }

       
            trappedchild = null;
            Fell = false;

            Destroy(gameObject); //val wordt vernietigd
        }
    }
}
