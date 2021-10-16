using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform camera;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float speed = 5f;

    private Vector3 direction;
    private float rotationTime = 0.1f;
    private float rotationSpeed;

    //On a très rarement une gravité réelle dans un jeu de plateforme, celle-ci donne un effet de "flottement"
    private float gravity = 60f;//9.81f;
    private float jumpSpeed = 40f;//6f;
    private float vecticalMovement = 0f;

    // Update is called once per frame
    void Update()
    {
        //On a droit à un seul move par framerate.  Bien que le déplacement et le saut n'ont rien à voir
        //chaque partie doit "construire" le vector de direction à sa façon.  Avoir un move pour le saut et un autre pour le mouvement
        //ferait en sorte que le joueur ne pourrait pas se déplacer et sauter en même temps.
        BuildSurfaceMovement();
        BuildVerticalMovement();

        characterController.Move(direction);
    }

    private void BuildSurfaceMovement()
    {
        //Get axis donne au déplacement un effet d'ajout progressif au déplacement, comme si on utilisait un joystick analogique
        //GetAxisRaw enlève cet effet.  Par contre il est présent naturellement pour un joystick
        //Avec un Joystick le raw n'est pas recommandé.
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        //.normalized fait en sorte que nous aurons un vecteur de 1 de longeur, peu importe la direction
        //Empêche donc le playerController d'aller plus vite en diagonale.
        //Par contre avec un gamepad cela fait en sorte qu'on a plus l'effet de vitesse progressive qui on enfonce le gamepad à moitié
        //La vitesse est totale ou elle ne l'est pas.
        direction = new Vector3(horizontal, 0f, vertical).normalized;

        //Pour éliminer les effets de shack du joystick
        //Avec le clavier + normalized, la magnitude sera toujours de 1.
        if (direction.magnitude >= 0.1f)
        {
            //l'angle que l'on vise avec nos contrôles.  Je pense que ça doit vous dire quelque chose.
            //float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camera.eulerAngles.y;

            //SmoothDampAngle permet de faire un déplacement progressif entre l'angle actuel et l'angle visé.
            //Sans cette ligne de code, le pivot du personnage sera brutal.
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSpeed, rotationTime);

            //Quaternion.Euler permet de gérer correctement les rotaions en degrés malgré que l'on ai affaire à un quaternion.
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            //Si vous voulez que le personnage diminue de vitesse en saut.
            //Si on voudrait que le saut ne change pas de direction: garder le même vecteur en x et z qaund le joueur n'est pas grounded
            //Si on veut que la vitesse reste optimal tant qu'on en change pas de direction: enregistrer la direction au saut et ralentir seulement si
            //Cette direction change
            float tempSpeed = speed;
            if (!characterController.isGrounded) tempSpeed /= 2;

            //direction.x = direction.x * tempSpeed * Time.deltaTime;
            //direction.z = direction.z * tempSpeed * Time.deltaTime;
            Vector3 moveDirection = (Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward).normalized;
            direction.x = moveDirection.x * tempSpeed * Time.deltaTime;
            direction.z = moveDirection.z * tempSpeed * Time.deltaTime;
        }
        else
        {
            direction = Vector3.zero;
        }
    }

    private void BuildVerticalMovement()
    {

        if (Input.GetButtonDown("Jump"))
        {
            if (characterController.isGrounded)
            {
                vecticalMovement = jumpSpeed;
            }
        }

        vecticalMovement -= gravity * Time.deltaTime;
        direction.y = vecticalMovement * Time.deltaTime;
    }
}
