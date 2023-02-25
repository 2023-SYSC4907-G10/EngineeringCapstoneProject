using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FWD_Packet : MonoBehaviour
{
    // Inspector based fields
    public PacketSpriteSequence packetSpriteSequence;
    public GameObject MaliciousSprites;
    public GameObject DeathFlamePrefab;

    private bool _isMalicious;
    private Queue<GameObject> _waypoints;
    private Rigidbody2D _rb;
    private float _movementSpeed;

    private readonly int FIREWALL_MALICIOUS_FILTER_ACCURACY_PERCENT = 75;
    private readonly int BETTER_FIREWALL_MALICIOUS_FILTER_ACCURACY_PERCENT = 90;
    private readonly int MALICIOUS_PACKET_PERCENT = 40;



    // // Start is called before the first frame update
    void Start()
    {
        this._isMalicious = Random.Range(0, 100) < MALICIOUS_PACKET_PERCENT;
        if (this._isMalicious)
        {
            MaliciousSprites.SetActive(true);
        }
        this._waypoints = FWD_Manager.GetInstance().WaypointManager.GetWaypointPath();
        this._rb = GetComponent<Rigidbody2D>();
        this._movementSpeed = 5;

        GoTowardsNextWaypoint();

    }

    private void GoTowardsNextWaypoint()
    {
        GameObject nextWaypoint;

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
        else if (theCollision.gameObject.layer == LayerMask.NameToLayer("FirewallDefense"))
        {
            if (this._isMalicious && Random.Range(0, 100) < FIREWALL_MALICIOUS_FILTER_ACCURACY_PERCENT)
            {
                Destroy(gameObject);
                Instantiate(DeathFlamePrefab, theCollision.gameObject.transform);
            }
        }
        else if (theCollision.gameObject.layer == LayerMask.NameToLayer("BetterFirewallDefense"))
        {
            if (this._isMalicious && Random.Range(0, 100) < BETTER_FIREWALL_MALICIOUS_FILTER_ACCURACY_PERCENT)
            {
                Destroy(gameObject);
                Instantiate(DeathFlamePrefab, theCollision.gameObject.transform);
            }
        }
    }
}
