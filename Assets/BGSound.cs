using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BGSound : MonoBehaviour
{
    private AudioSource audioSource;
    private Object[] tracks;
    private int numTracks;
    private Dictionary<AudioClip, float> weights;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // tracks = Resources.LoadAll("Soundtrack", typeof(AudioClip)).Cast<AudioClip>().ToArray();
        tracks = Resources.LoadAll("Soundtrack", typeof(AudioClip));
        numTracks = tracks.Length;
        Debug.Log(numTracks);
        foreach (AudioClip t in tracks)
        {
            Debug.Log(t.name);
        }
        weights = new Dictionary<AudioClip, float>();
        foreach (AudioClip t in tracks)
        {
            weights[t] = 1f / numTracks;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = ChooseNew();
            audioSource.Play();
        }
    }

    private AudioClip ChooseNew()
    {
        int n = Random.Range(0, 100);
        float sum = 0f;
        AudioClip choice = audioSource.clip;
        float currWeight;
        float foundWeight = 0f;
        bool found = false;
        ArrayList cache = new ArrayList();
        shuffle(tracks);
        foreach (AudioClip t in tracks)
        {
            currWeight = weights[t];
            if (n >= sum && n < (sum + 100 * currWeight)) {
                choice = t;
                foundWeight = weights[t];
                found = true;
                weights[t] = 0f;
            } else if (!found){
                cache.Add(t);
            } else {
                weights[t] += foundWeight / (numTracks - 1);
            }
            sum += 100 * currWeight;
        }
        foreach (AudioClip t in cache)
        {
            weights[t] += foundWeight / (numTracks - 1);
        }
        foreach(KeyValuePair<AudioClip, float> entry in weights){
            Debug.Log(entry.Key.name);
            Debug.Log(entry.Value);
        }
        return choice;
    }

    private void shuffle(Object[] arr)
    {
        for (int i = 0; i < arr.Length; i += 1)
        {
            Object temp = arr[i];
            int rand = Random.Range(i, arr.Length);
            arr[i] = arr[rand];
            arr[rand] = temp;
        }
    }
}
