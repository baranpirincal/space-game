using UnityEngine;

public class Enemy : MonoBehaviour
{

	public int movementPattern;
	
	WaveController waveController;
	public float mobility;
	public float hostileness;
	private float mobilityR = 0.0f;
	private float hostilenessR = 0.0f;
	
	private int currentScheme=0;
	Transform target;
	int mod(int x, int m) 
	{
		return (x%m + m)%m;
	}
	
	void Start()
	{
		
		waveController = GameObject.Find("WaveController").GetComponent<WaveController>();
		mobilityR=Random.Range(0,1/mobility);
		hostilenessR=Random.Range(0,1/hostileness);
		if(movementPattern==2) //SPY
		{
			if(GameObject.FindGameObjectWithTag("Player"))
				target=GameObject.FindGameObjectWithTag("Player").transform;
		}
	}

    void FixedUpdate()
    {
		if(mobilityR>1/mobility)
		{
			mobilityR=0;
			currentScheme++;
		}
		
		if(hostilenessR>1/hostileness)
		{
			hostilenessR=Random.Range(0,1/hostileness);
			gameObject.SendMessage("Shoot",0,SendMessageOptions.RequireReceiver);
			gameObject.SendMessage("Shoot",1,SendMessageOptions.RequireReceiver);
			gameObject.SendMessage("Shoot",2,SendMessageOptions.RequireReceiver);
		}
			
			//1 up
			//2 down
			//3 left
			//4 right
			
		// -> <-
		if(movementPattern==0)
		{
			if(mod(currentScheme,2)==0)
				gameObject.SendMessage("GetInput", 3);
			else
				gameObject.SendMessage("GetInput", 4);
		}
		// <>
		else if(movementPattern==1)
		{
			if(mod(currentScheme,4)==0)
			{
				gameObject.SendMessage("GetInput", 4);
				gameObject.SendMessage("GetInput", 2);
			}
			else if(mod(currentScheme,4)==3)
			{
				gameObject.SendMessage("GetInput", 4);
				gameObject.SendMessage("GetInput", 1);
			}			
			else if(mod(currentScheme,4)==2)
			{
				gameObject.SendMessage("GetInput", 3);
				gameObject.SendMessage("GetInput", 1);
			}
			else
			{
				gameObject.SendMessage("GetInput", 3);
				gameObject.SendMessage("GetInput", 2);
			}
		}
		// SPY
		else if(movementPattern==2)
		{
			if(transform.position.x>target.position.x)
				gameObject.SendMessage("GetInput", 4);
			else
				gameObject.SendMessage("GetInput", 3);
		}
		
    }
	
	void OnCollisionEnter2D(Collision2D coll) 
	{
		currentScheme++;
	}
	
	void LateUpdate()
	{
		mobilityR+=Time.deltaTime;
		hostilenessR+=Time.deltaTime;
	}
	
	public void OnDestroy() 
	{
		waveController.Reduce();
	}
	
}