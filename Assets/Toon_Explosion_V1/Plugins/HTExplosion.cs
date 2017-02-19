using UnityEngine;
using System.Collections;

/// <summary>
/// HThis allows the creation of a particle and play an animated sprite from spritesheet.
/// </summary>
public class HTExplosion : MonoBehaviour {
	
	#region public properties
	
	/// <summary>
	/// The sprite sheet material.
	/// </summary>
	public Material spriteSheetMaterial;	
	/// <summary>
	/// The number of sprtie on the spritesheet.
	/// </summary>
	public int spriteCount;
	/// <summary>
	/// The uv animation tile x.
	/// </summary>
	public int uvAnimationTileX;
	/// <summary>
	/// The uv animation tile y.
	/// </summary>
	public int uvAnimationTileY;
	/// <summary>
	/// The number of images per second to play animation
	/// </summary>
	public int framesPerSecond;
	/// <summary>
	/// The initial size of the explosion
	/// </summary>
	public Vector3 size = new Vector3(1,1,1);
	/// <summary>
	/// The speed growing.
	/// </summary>
	public float speedGrowing;
	/// <summary>
	/// Applied a rondom rotation on z-Axis.
	/// </summary>
	public bool randomRotation;
	/// <summary>
	/// The is one shot animation.
	/// </summary>
	public bool isOneShot=true;
	
	#endregion
	
	#region private properties
	/// <summary>
	/// The material with the sprite speed.
	/// </summary>
	private Material mat;
	/// <summary>
	/// The mesh.
	/// </summary>
	private Mesh mesh;
	/// <summary>
	/// The mesh render.
	/// </summary>
	private MeshRenderer meshRender;
	/// <summary>
	/// The start time of the explosion
	/// </summary>
	private float startTime;
	/// <summary>
	/// The effect end.
	/// </summary>
	private bool effectEnd=false;
	/// <summary>
	/// The random Z angle.
	/// </summary>
	private float randomZAngle;
	
	#endregion
	
	#region MonoBehaviour methods
	
	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake(){
		
		// Creation of the particle
		CreateParticle();

		GetComponent<Renderer>().enabled = false;
	}
	
	// Use this for initialization
	void Start () {
	
		startTime = Time.time;
		transform.localScale = size;
		
		if (randomRotation){
			randomZAngle = Random.Range(-180.0f,180.0f);
		}
		else{
			randomZAngle = 0;
		}
	}
	
	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update () {
		
		bool end=false;
		
		Camera_BillboardingMode();
		
		// Calculate index
    	float index = (Time.time-startTime) * framesPerSecond;
		
		if ((index<=spriteCount || !isOneShot) && !effectEnd ){
		     // repeat when exhausting all frames
		    index = index % (uvAnimationTileX * uvAnimationTileY);
		   		
			if (index== spriteCount){
				startTime = Time.time;	
				index=0;
			}
			
		    // Size of every tile
		    Vector2 size = new Vector2 (1.0f / uvAnimationTileX, 1.0f / uvAnimationTileY);
		   
		    // split into horizontal and vertical index
		    float uIndex = Mathf.Floor(index % uvAnimationTileX);
		    float vIndex = Mathf.Floor(index / uvAnimationTileX);
		
		    // build offset
		    Vector2 offset = new Vector2 (uIndex * size.x , 1.0f - size.y - vIndex * size.y);
			
		   	GetComponent<Renderer>().material.SetTextureOffset ("_MainTex", offset);
		   	GetComponent<Renderer>().material.SetTextureScale ("_MainTex", size);
		    
		    // growing
		    transform.localScale += new Vector3(speedGrowing,speedGrowing,speedGrowing) * Time.deltaTime ;
		    GetComponent<Renderer>().enabled = true;
		}			
		else{
	 		effectEnd = true;
			GetComponent<Renderer>().enabled = false;
			end = true;		
			
			if (end){
				Destroy(gameObject);	
 			}
		}
	}
	
	#endregion
	
	#region private methods
	
	/// <summary>
	/// Creates the particle.
	/// </summary>
	void CreateParticle(){
		
		mesh = gameObject.AddComponent<MeshFilter>().mesh; 
		meshRender = gameObject.AddComponent<MeshRenderer>(); 		
		
		mesh.vertices = new Vector3[] { new Vector3(-0.5f,-0.5f,0f),new Vector3(-0.5f,0.5f,0f), new Vector3(0.5f,0.5f,0f), new Vector3(0.5f,-0.5f,0f) };
		mesh.triangles = new int[] {0,1,2, 2,3,0 };
		mesh.uv = new Vector2[] { new Vector2 (0f, 0f), new Vector2 (0f, 1f), new Vector2 (1f, 1f), new Vector2(1f,0f)};

		meshRender.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
		meshRender.receiveShadows = false;
		mesh.RecalculateNormals();		
		
		GetComponent<Renderer>().material= spriteSheetMaterial;
	}
	
	/// <summary>
	/// Camera_s the billboarding mode.
	/// </summary>
	void Camera_BillboardingMode(){
		
		transform.eulerAngles = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y,randomZAngle);
	}
	
	#endregion
	
}
