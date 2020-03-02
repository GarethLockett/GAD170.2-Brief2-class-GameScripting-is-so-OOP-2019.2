using UnityEngine;

// DANGER ZONE: You should not have to modify anything on this script

public class CharacterStats : MonoBehaviour
{
    public int health;
    public int damage;

    public void TakeDamage( int damageAmount )
    {
        this.health -= damageAmount;

        // HACK to fix playing audio incase about to destroy character (Otherwise should use this.GetComponent<AudioSource>().Play())
        AudioSource.PlayClipAtPoint( this.GetComponent<AudioSource>().clip, Camera.main.transform.position );

        this.GetComponent<SpriteRenderer>().color = Color.red;
        Invoke( "StopDamage", 0.2f );
    }

    void StopDamage()
    {
        this.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
