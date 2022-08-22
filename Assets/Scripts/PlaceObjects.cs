using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(GenerateMesh))]
public class PlaceObjects : MonoBehaviour {

    public TerrainController TerrainController { get; set; }

    public RuntimeAnimatorController anim2;

    public void Place() {
        int numObjects = Random.Range(TerrainController.MinObjectsPerTile, TerrainController.MaxObjectsPerTile);
        for (int i = 0; i < numObjects; i++) {
            int prefabType = Random.Range(0, TerrainController.PlaceableObjects.Length);
            Vector3 startPoint = new Vector3(
                            Random.Range(transform.position.x - TerrainController.TerrainSize.x / 2, transform.position.x + TerrainController.TerrainSize.x / 2),
                            0,
                            Random.Range(transform.position.z - TerrainController.TerrainSize.z / 2, transform.position.z + TerrainController.TerrainSize.z / 2)
                            );

            Quaternion orientation = Quaternion.Euler(Vector3.up * Random.Range(0f, 360f));

            GameObject newGameObject = Instantiate(TerrainController.PlaceableObjects[prefabType], new Vector3(startPoint.x, 0, startPoint.z), orientation, transform);
            if (!TerrainController.PlaceableObjects[prefabType].name.Contains("pointy")) {

                // add a random person on the non pointy iceberg 
                int CharactersIndex = Random.Range(0, TerrainController.Characters.Length);
                int DanceMoveIndex = Random.Range(0, TerrainController.DanceMoves.Length);

                RaycastHit standingSpot;
                startPoint.y += 100; // make sure the startPoint is above all the icebergs otherwise the raycast will be inside the mesh
                Physics.Raycast(startPoint, Vector3.down, out standingSpot); 

                GameObject dancerLocation =  new GameObject("dancer_loc");
                dancerLocation.name = "dancer_loc";
                dancerLocation.transform.position = new Vector3(startPoint.x, standingSpot.point.y+50, startPoint.z);
                dancerLocation.transform.rotation = orientation;
                dancerLocation.transform.parent = transform;
                dancerLocation.transform.Rotate(Vector3.up, 180.0f);
                dancerLocation.AddComponent<BoxCollider>();
                dancerLocation.GetComponent<BoxCollider>().size = new Vector3(50,50,50);

                GameObject newDancer = Instantiate(TerrainController.Characters[CharactersIndex], new Vector3(startPoint.x, standingSpot.point.y+1, startPoint.z), orientation, transform);
                newDancer.GetComponent<Animator>().runtimeAnimatorController = TerrainController.DanceMoves[DanceMoveIndex];

            }
            else{
                float randomScale = Random.Range(0.3f, 2);
                Vector3 localScale = newGameObject.transform.localScale;
                newGameObject.transform.localScale = new Vector3(localScale.x*randomScale, localScale.x*randomScale, localScale.x*randomScale);
            }

        }

    }
}