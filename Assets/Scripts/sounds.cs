using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sounds : MonoBehaviour
{
    [SerializeField] private AudioClip[] lasers = new AudioClip[2];
    [SerializeField] private AudioClip destroyed;
    [SerializeField] private AudioClip destroyedPlayer;
    private Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
       
    }
    public void playLaserSound(int index)
    {
        camera.GetComponent<AudioSource>().PlayOneShot(lasers[index]);
    }
    public void playDestroyed()
    {
        camera.GetComponent<AudioSource>().volume = 0;
        camera.GetComponent<AudioSource>().PlayOneShot(destroyed);
        camera.GetComponent<AudioSource>().volume = 1f;
    }
    public void playPlayerDestroyed()
    {
        camera.GetComponent<AudioSource>().PlayOneShot(destroyedPlayer);
    }
}
