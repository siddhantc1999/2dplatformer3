using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playermove : MonoBehaviour
{
    public Charactermovement mycharactermovment;
    float movement;
     public Rigidbody2D myrigibody;
    public float movespeed;
    Animator myanimator;
    bool here;
    public float upspeed;
    public bool iscollided;
    /*AnimatorClipInfo[] myanimatorclipinfo;*/
    // Start is called before the first frame update
    void Start()
    {
    

        mycharactermovment = new Charactermovement();
        mycharactermovment.playermovment.Enable();
        myanimator = GetComponent<Animator>();
     /*   myanimatorclipinfo = myanimator.GetCurrentAnimatorClipInfo(0);*/
        mycharactermovment.playermovment.jump.started += playerjump;
       
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movement = mycharactermovment.playermovment.movement.ReadValue<Vector2>().x;

      /*  Debug.Log("the current animation "+ myanimatorclipinfo[0].clip.name);*/

            if (movement != 0 )
            {
            myanimator.SetBool("run",true);
            if (movement>0f)
            {
                gameObject.transform.localScale = new Vector3(1f,transform.localScale.y,transform.localScale.z);
            }
            else
            if(movement<0f)
            {
                gameObject.transform.localScale = new Vector3(-1f, transform.localScale.y, transform.localScale.z);
            }
           

           
            transform.position =new Vector2(transform.position.x, transform.position.y) + new Vector2(movement * movespeed * Time.deltaTime,0F); new Vector2(movement * movespeed * Time.deltaTime,0F);
          

        }
        else
        {
            myanimator.SetBool("run", false);
        }

        


    
     
    }
    public void playerjump(InputAction.CallbackContext move)
    {
        if (iscollided)
        {
            
            myanimator.SetTrigger("jump");
          /*  transform.GetComponent<Rigidbody2D>().velocity = new Vector2();*/
        
            transform.position = new Vector2(transform.position.x, transform.position.y)+new Vector2(0f, upspeed);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
       
        if (collision.gameObject.layer == LayerMask.NameToLayer("landarea"))
        {
            
            iscollided = true;
        }
        //Debug.Log("the layer name "+collision.gameObject.layer);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        iscollided = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       

        myanimator.SetTrigger("jumpexit");

    }


}
