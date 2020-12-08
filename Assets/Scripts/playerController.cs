using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float h;
    public float v;

    public CharacterController character;
    public float speed;
    public Vector3 playerInput;
    public Vector3 movPlayer;
    public float gravity = 9.8f;
    public float fallVelocity;
    public float jumpForce;
    public Animator animator;

    public Camera camera;
    public Vector3 camForward;
    public Vector3 camRight;

    public string controlNameHorizontal = "Horizontal";
    public string controlNameVertical = "Vertical";
    public string controlNameJump = "Jump";

    public ControladorDeUI cdUI;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ControladorJuego.iniciarjuego)
        {
            h = Input.GetAxis(controlNameHorizontal);
            v = Input.GetAxis(controlNameVertical);
        }
        playerInput = new Vector3(h, 0, v);
        playerInput = playerInput.normalized;

        CamDirection();

        movPlayer = playerInput.x * camRight + playerInput.z * camForward;

        movPlayer = movPlayer * speed;

        character.transform.LookAt(character.transform.position + movPlayer);

        SetGravity();

        if (ControladorJuego.iniciarjuego)
        {
            PlayerSkill();
        }

        character.Move(movPlayer * Time.deltaTime);

        animator.SetFloat("speed", character.velocity.normalized.magnitude);

        //Debug.Log(character.velocity.magnitude);
        
    }

    private void PlayerSkill()
    {
        if (character.isGrounded && Input.GetButtonDown(controlNameJump))
        {
            fallVelocity = jumpForce;
        }

        movPlayer.y = fallVelocity;
    }

    private void SetGravity()
    {
        if (character.isGrounded)
        {
            fallVelocity = -gravity * Time.deltaTime;
        }
        else
        {
            fallVelocity -= gravity * Time.deltaTime;
        }
        movPlayer.y = fallVelocity;
    }

    private void CamDirection()
    {
        camForward = camera.transform.forward;
        camRight = camera.transform.right;

        Debug.Log("Righ: " + camera.transform.right);
        Debug.Log("forward: " + camera.transform.forward);

        camForward.y = 0;
        camRight.y = 0;

        camForward = camForward.normalized;
        camRight = camRight.normalized;
    }

    public void Eliminado() {
        cdUI.setDelete();
        camera.cullingMask = 0;
        if (name.Equals("Player"))
        {
            cdUI.showEliminadoJ1();
        }
        else
        {
            cdUI.showEliminadoJ2();
        }
        gameObject.SetActive(false);
    }
}
