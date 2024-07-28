using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCodeBloc : MonoBehaviour
{
    public ParticleSystem particleSystemPrefab;
    public AudioClip destroySound;
    private AudioSource audioSource;

    void Start()
    {
        // Add an AudioSource component to the game object if not already present
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered by: " + other.gameObject.name);
        
        // Check if the collided object has the tag "codebloc"
        if (other.CompareTag("codebloc"))
        {
            Debug.Log("Destroying object with tag 'codebloc': " + other.gameObject.name);
            
            // Ensure the particle system prefab is not null
            if (particleSystemPrefab != null)
            {
                // Instantiate the particle system at the codebloc's position
                ParticleSystem instantiatedParticleSystem = Instantiate(particleSystemPrefab, other.transform.position, Quaternion.identity);
                
                // Enable and play the instantiated particle system
                var emission = instantiatedParticleSystem.emission;
                emission.enabled = true;
                instantiatedParticleSystem.Play();

                // Start coroutine to disable emission after playing
                StartCoroutine(DisableEmissionAfterDelay(instantiatedParticleSystem, instantiatedParticleSystem.main.duration));
            }
            else
            {
                Debug.LogWarning("Particle system prefab is not assigned!");
            }
            
            if (audioSource != null && destroySound != null)
            {
                // Play the destroy sound
                audioSource.PlayOneShot(destroySound);
            }
            else
            {
                Debug.LogWarning("Audio source or destroy sound is not assigned!");
            }
            
            // Destroy the collided object
            Destroy(other.gameObject);
        }
    }

    private IEnumerator DisableEmissionAfterDelay(ParticleSystem particleSystem, float delay)
    {
        yield return new WaitForSeconds(delay);
        
        // Disable the particle system emission
        var emission = particleSystem.emission;
        emission.enabled = false;

        // Destroy the particle system game object after it finishes playing
        Destroy(particleSystem.gameObject);
    }
}