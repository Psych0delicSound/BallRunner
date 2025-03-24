using System.Collections;
using UnityEngine;

public class GenerateLocation : MonoBehaviour
{
    float columbDistance = 4.23f, rowsDistance = 7.85f;
    int rows, columns, rand;

    void Start()
    {
        rows = 18;
        columns = 9;
        Generation();
    }
    void Generation()
    {
        for (int i = 1; i <= rows; i++)
        {           
            for (int j = 1; j <= columns; j++)
            {
                Vector3 positionTile = new Vector3(transform.position.x -43 + columbDistance * (i%2 + 2 * j), transform.position.y + 22, transform.position.z + i * rowsDistance);
                Vector3 positionPickup = new Vector3(positionTile.x, positionTile.y + 2.5f, positionTile.z);
                rand = Random.Range(1, 40);
                if ((i < 6 && j == 5) || rand > 4 && rand < 27)
                {
                    Instantiate(Resources.Load<GameObject>("Prefabs/Tile"), positionTile, Quaternion.identity, transform);
                }
                else {
                    switch (rand)
                    {
                        case 1:
                            Instantiate(Resources.Load<GameObject>("Prefabs/Stone"), positionTile, Quaternion.identity, transform);
                            break;
                        case 2:
                            Instantiate(Resources.Load<GameObject>("Prefabs/Tree"), positionTile, Quaternion.identity, transform);
                            break;
                        case 3:
                            Instantiate(Resources.Load<GameObject>("Prefabs/Tower"), positionTile, Quaternion.identity, transform);
                            break;
                        case 4:
                            Instantiate(Resources.Load<GameObject>("Prefabs/Walltower"), positionTile, Quaternion.identity, transform);
                            break;                       
                    }  
                }   
                switch(rand)
                {
                    case 5:    
                        Instantiate(Resources.Load<GameObject>("Prefabs/ModifierDestroyer"), positionPickup, Quaternion.identity, transform);
                        break;
                    case 6:    
                        Instantiate(Resources.Load<GameObject>("Prefabs/ModifierSpeedUp"), positionPickup, Quaternion.identity, transform);
                        break;
                    case 7:    
                        Instantiate(Resources.Load<GameObject>("Prefabs/ModifierJump"), positionPickup, Quaternion.identity, transform);
                        break;
                    case 8:    
                        Instantiate(Resources.Load<GameObject>("Prefabs/ModifierUncontrolable"), positionPickup, Quaternion.identity, transform);
                        break;
                    case 9:    
                        Instantiate(Resources.Load<GameObject>("Prefabs/ModifierRotate"), positionPickup, Quaternion.identity, transform);
                        break;
                    case <= 11:    
                        if (rand > 9) Instantiate(Resources.Load<GameObject>("Prefabs/Star"), positionPickup, Quaternion.identity, transform);
                        break;
                }            
            }
        }
        gameObject.transform.rotation = Quaternion.Euler(40, 0, 0);
    }

    public void OptimizationLoop(GameObject sphere)
    {                          
        GameObject nextLocation = GameObject.Find("Location Next"); 
        sphere.transform.SetParent(nextLocation.transform);
        nextLocation.transform.position = new Vector3(0, -22, -17);
        nextLocation.name = "Location";
        sphere.transform.SetParent(null);

        Destroy(gameObject);
        GameController ob = GameObject.Find("Game Controller").GetComponent<GameController>();
        ob.GenerateNextLocation(true);
    }
}
