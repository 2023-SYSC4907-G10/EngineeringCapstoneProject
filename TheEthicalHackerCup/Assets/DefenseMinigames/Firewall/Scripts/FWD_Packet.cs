using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FWD_Packet : MonoBehaviour
{
    // Inspector based fields
    public PacketSpriteSequence packetSpriteSequence;

    private bool _isMalicious;
    private Queue<FWD_Waypoint> _waypoints;
    private Rigidbody2D _rb;
    private float _movementSpeed;




    // // Start is called before the first frame update
    void Start()
    {
        this._isMalicious = true; // TODO: Get from manager
        this._waypoints = FWD_Manager.GetInstance().WaypointManager.GetWaypointPath();
        this._rb = GetComponent<Rigidbody2D>();
        this._movementSpeed = 4; //TODO: Get from FWD manager

        GoTowardsNextWaypoint();

    }

    private void GoTowardsNextWaypoint()
    {
        FWD_Waypoint nextWaypoint;// = _waypoints.Dequeue();

        if (_waypoints.TryDequeue(out nextWaypoint))
        {
            Vector2 direction = (nextWaypoint.transform.position - this.transform.position).normalized;
            _rb.velocity = direction * _movementSpeed;
        }
        else
        {
            // Realistically shouln't get here as the packet will get destroyed before getting to 3rd waypoint
            _rb.velocity = new Vector2(0, 0);
        }
    }

    public bool IsMalicous() { return this._isMalicious; }

    void OnTriggerEnter2D(Collider2D theCollision)
    {
        if (theCollision.gameObject.layer == LayerMask.NameToLayer("Waypoint"))
        {
            // When reaching a waypoint
            GoTowardsNextWaypoint();
        }
        else if (theCollision.gameObject.layer == LayerMask.NameToLayer("Target"))
        {
            // When reaching the target computer
            if (this._isMalicious) { FWD_Manager.GetInstance().ReceivedBadPacket(); }
            
            // Self destruction
            Destroy(gameObject);
        }
        else if (theCollision.gameObject.layer == LayerMask.NameToLayer("PlayerLayer"))
        {
            // When caught by a flame
            if (!this._isMalicious) { FWD_Manager.GetInstance().BurnGoodPacket(); }
            
            // Self destruction
            Destroy(gameObject);
        }
        else if (theCollision.gameObject.layer == LayerMask.NameToLayer("PacketLayerRemover"))
        {
            packetSpriteSequence.SwitchToNextPacketLayer();
        }

    }


}
