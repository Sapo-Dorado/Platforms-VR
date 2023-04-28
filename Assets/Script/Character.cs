using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

public class Character : MonoBehaviour
{
    // Start is called before the first frame update
    public TeleportationProvider provider = null;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -25) {
            TeleportRequest request = CreateTeleportationRequest(new Vector3(0,0,0));
            provider.QueueTeleportRequest(request);
        }
    }

    private TeleportRequest CreateTeleportationRequest(Vector3 pos) {
        return new TeleportRequest()
        {
            destinationPosition = pos
        };

    }
}
