using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* the 'using' statements above list the code libraries (APIs) we're using.
 * this is what makes MonoBehaviour, .Rotate etc. have meaning
 */

public class ExampleScript : MonoBehaviour
{
    //create an editable field for a 'speed' with default value of 1
    [SerializeField]
    float speed = 1f; //'f' makes it a float (1.00000...), rather than integer 1

     // Update is called once per frame
    void Update()
    {
        /*
         * Each frame, call the static method Rotate from UnityEngine.Transform
         * on the transform of the object this script is attached to.
         * Pass a Vector3 that has zero x and y rotation,
         * and z rotation equal to the time since the previous frame,
         * multiplied by our float 'speed'.
        */

        transform.Rotate(new Vector3(0f, 0f, Time.deltaTime * speed));

        // observe how by using the API we accomplish all this in 1 line of code!

    }
}
