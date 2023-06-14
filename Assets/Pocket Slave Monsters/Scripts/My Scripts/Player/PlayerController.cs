using UnityEngine;
using UnityEngine.Tilemaps;

// require SpriteRenderer for changing the player's sprite
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    // walkspeed in tiles per second
    public float walkSpeed;
    public Animator myAnim;
    // the tilemap which has the tiles we want to collide with
    public Tilemap tilemap;
    // the amount of time we can press an input without moving in seconds
    public float moveDelay = 0.2f;
    // our player's direction
    static Direction currentDir = Direction.South;
    public Direction dir;
    // a vector storing the input of our input-axis
    Vector2 input;
    // states if the player is moving or waiting for movement input
    bool isMoving = false;
    // position before a move is executed
    Vector3 startPos;
    // target-position after the move is executed
    Vector3 endPos;
    // stores the progress of the current move in a range from 0f to 1f
    float progress;
    // stores the time remaining before the player can move again
    float remainingMoveDelay = 0f;

    public VectorValue startingPosition;

    public bool isAllowedToMove = true;

    

    private void setDirection(string moveType)
    {
        switch (currentDir)
        {
            case Direction.North:
                myAnim.SetFloat(moveType + "X", 0f);
                myAnim.SetFloat(moveType + "Y", 1f);
                break;
            case Direction.East:
                myAnim.SetFloat(moveType + "X", 1f);
                myAnim.SetFloat(moveType + "Y", 0f);
                break;
            case Direction.South:
                myAnim.SetFloat(moveType + "X", 0f);
                myAnim.SetFloat(moveType + "Y", -1f);
                break;
            case Direction.West:
                myAnim.SetFloat(moveType + "X", -1f);
                myAnim.SetFloat(moveType + "Y", 0f);
                break;
        }
    }

    void OnApplicationQuit()
    {
        startingPosition.isStart = true;
    }

    void Start()
    {
        isAllowedToMove = true;

        if (startingPosition.isStart)
            transform.position = startingPosition.initialValue;
        else
            transform.position = startingPosition.storedPosition;
        setDirection("lastMove");
    }

    public void Update()
    {
        // check if the player is moving
        if (!isMoving && isAllowedToMove) 
        {
            // The player is currently not moving so check if there is keyinput
            input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            // if there is input in x direction disable input in y direction to
            // disable diagonal movement
            if (input.x != 0f)
                input.y = 0;

            // check if there is infact movement or if the input axis are in idle
            // position
            if (input != Vector2.zero)
            {
                // save the old direction for later use
                Direction oldDirection = currentDir;

                // update the players direction according to the input
                #region update Direction
                if (input.x == -1f)
                    currentDir = Direction.West;
                if (input.x == 1f)
                    currentDir = Direction.East;
                if (input.y == 1f)
                    currentDir = Direction.North;
                if (input.y == -1f)
                    currentDir = Direction.South;
                #endregion

                dir = currentDir;

                if (myAnim.GetFloat("moveX") == 0 && myAnim.GetFloat("moveY") == 0)
                    myAnim.SetBool("Alt", !(myAnim.GetBool("Alt")));

                // since there is currently no further animation components we
                // just set the sprite according to the direction

                setDirection("move");

                // if the currentDirection is different from the old direction
                // we want to add a delay so the player can just change direction
                // without having to move
                if (currentDir != oldDirection)
                {
                    remainingMoveDelay = moveDelay;
                }

                // if the direction of the input does not change then the move-
                // delay ticks down
                if (remainingMoveDelay > 0f)
                {
                    remainingMoveDelay -= Time.deltaTime;
                    return;
                }

                // for the collision detection and movement we need the current
                // position as well as the target position where our player
                // is going to move to
                startPos = transform.position;
                endPos = new Vector3(startPos.x + input.x, startPos.y + input.y, startPos.z);

                // we subtract 0.5 both in x and y direction to get the coordinates
                // of the upper left corner of our player sprite and convert
                // the floating point vector into an int vector for tile search
                Vector3Int tilePosition = new Vector3Int((int)(endPos.x - 0.5f),
                                                         (int)(endPos.y - 0.5f), 0);

                // with our freshly calculated tile position of the tile where our
                // player want to move to we can now check if there is in fact
                // a tile at that position which we would collide with
                // if there is no tile so the GetTile-function return null then
                // we can go ahead and move towards our target
                if (tilemap.GetTile(tilePosition) == null)
                {
                    // we set our moving variable to true and our progress
                    // towards the target position to 0
                    isMoving = true;
                    progress = 0f;
                }
            }
            else 
            {
                if (myAnim.GetFloat("moveX") != 0f || myAnim.GetFloat("moveY") != 0f)
                {
                    myAnim.SetFloat("lastMoveX", myAnim.GetFloat("moveX"));
                    myAnim.SetFloat("lastMoveY", myAnim.GetFloat("moveY"));
                }

                myAnim.SetFloat("moveX", 0f);
                myAnim.SetFloat("moveY", 0f);
            }
        }

        // check if the player is currently in the moving state
        if (isMoving)
        {
            // check if the progress is still below 1f so the movement is still
            // going on
            if (progress < 1f)
            {
                // increase our movement progress by our deltaTime times our
                // above specified walkspeed
                progress += Time.deltaTime * walkSpeed;

                // linearly interpolate between our start- and end-positions
                // with the value of our progress which is in range of [0, 1]
                transform.position = Vector3.Lerp(startPos, endPos, progress);
            }
            else
            {
                // if we are moving and our progress is above one that means we
                // either landed exactly on our desired position or we overshot
                // by some tiny amount so in ordered to not accumulate errors
                // we clamp our final position to our desired end-position
                isMoving = false;
                transform.position = endPos;
            }
        }
    }
}

// small Enumeration to help us keep track of the player's direction more easyly
public enum Direction
{
    North, East, South, West
}