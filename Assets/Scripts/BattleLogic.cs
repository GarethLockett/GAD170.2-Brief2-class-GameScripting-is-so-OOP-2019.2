using System.Collections.Generic; // Required for using Lists
using UnityEngine;

public class BattleLogic : MonoBehaviour
{
    // Properties

    // Characters are first instantiated and added to the following lists. As characters are defeated, they are removed from these lists.
    // Once one of the lists is empty, the other list wins the battle.
    // *NOTE: during set up you may wish to make these lists public so you can check characters are been added and removed throughout the battle. Remember to make private at the end.
    private List<CharacterStats> activeHeroes = new List<CharacterStats>();
    private List<CharacterStats> activeMonsters = new List<CharacterStats>();

    // TODO: Create more prefabs in the Unity editor and add to the following arrays so they can be instantiated into the battle.
    public CharacterStats[] heroLibrary;
    public CharacterStats[] monsterLibrary;

    // TODO: In the Unity editor, populate the following arrays with spawn points where you want the heroes and monsters to be instantiated.
    public Transform[] heroSpawnPoints;
    public Transform[] monsterSpawnPoints;

    // A reference to the Writetext script which handles writing text to the screen. Usage: ouputLog.OutputText( "text to display here" );
    public WriteText ouputLog;

    private void Start()
    {
        // Create the active heroes list.
        this.InstantiateRandomCharactersOntoSpawnPositions( this.heroLibrary, this.heroSpawnPoints, this.activeHeroes );

        // Create the active monsters list.
        this.InstantiateRandomCharactersOntoSpawnPositions( this.monsterLibrary, this.monsterSpawnPoints, this.activeMonsters );

        // Check some heroes and monsters have been created to battle. If not, end the game.
        if( this.activeHeroes.Count == 0 && this.activeMonsters.Count == 0 )
        {
            this.EndBattle( "There will be no battle today." );
            return;
        }

        // Call the Fight() method every four seconds until CancelInvoke() called  (* See Unity Scripting Reference "InvokeRepeating" for more)
        InvokeRepeating( "Fight", 4, 4 );

        // An example of how to write a string to the screen.
        ouputLog.OutputText( "Let the battle begin..." );
    }

    private void InstantiateRandomCharactersOntoSpawnPositions( CharacterStats[] characterLibrary, Transform[] spawnPositions, List<CharacterStats> activeList )
    {
        /*
            Method: private void InstantiateRandomCharactersOntoSpawnPositions( CharacterStats[] characterLibrary, Transform[] spawnPositions, List<CharacterStats> activeList )
            Description: Instantiates a random character from the character library at each of the spawnPositions. Instantiated characters are added to the active list.
            Parameters:
                characterLibrary: An array of characters to randomly instantiate from.
                spawnPositions: An array of spawn positions to instantiate at.
                activeList: The list an instantiated character will get added to.
        */

        // TODO: Complete the code in the following loop to instantiate a prefab from the characterLibrary at each of the spawnPositions. 
        //       Instantiated characters need to be added to the activeList.

        // Loop through each of the spawn position and instantiate a random character at that position.
        foreach( Transform sp in spawnPositions )
        {
            // Check to see if position is null (eg it may not have been assigned in the Unity editor) If it is null, go on with other spawn positions.
            if( sp == null ) { continue; }

            // Instantiate a random character from the characterLibrary (*See 'Instantiate' and 'Random.Range' in the Unity Scripting Reference for more)
            // HINT: GameObject newCharacter = ...

            // Fix the new character GameObject name (eg remove the "(Clone)" Unity puts at the end)  Uncomment the next line...
            //newCharacter.name = newCharacter.name.Replace( "(Clone)", "" );

            // Position the new character at the current loop spawn position.
            // HINT: newCharacter.transform.position = ...

            // Add the new character to the passed in active List.
            //activeList.Add( newCharacter.GetComponent<CharacterStats>() );
        }
    }

    private void Fight()
    {
        /*
            Method: private void Fight()
            Description: Called every 4 seconds by the InvokeRepeating() code started in the Start() method.
        */

        // Remove any destroyed (null) character slots from the active lists.
        this.activeHeroes.RemoveAll( item => item == null );
        this.activeMonsters.RemoveAll( item => item == null );

        // Check to see if there are no heroes nor monsters left. If so, end the battle and announce a draw.
        if( this.activeHeroes.Count == 0 && this.activeMonsters.Count == 0 )
        {
            this.EndBattle( "All heroes and monsters have been defeated." );
            return;
        }

        // Check to see if there are any active heroes left. If not, end the battle and announce the heroes have been defeated.
        if( this.activeHeroes.Count == 0 )
        {
            this.EndBattle( "Defeat! The heroes have been defeated!" );
            return;
        }

        // Check to see if there are any active monsters left. If not, end the battle and announce the monsters have been defeated.
        if( this.activeMonsters.Count == 0 )
        {
            this.EndBattle( "Victory! The monsters have been defeated!" );
            return;
        }

        // TODO: Randomly select a hero and monster from the active heroes ( eg choosing one hero and one monster per round )
        CharacterStats hero = this.activeHeroes[ Random.Range( 0, this.activeHeroes.Count ) ];
        // HINT: CharacterStats monster = ...

        // Dull the color of all active characters.
        this.SetAllActiveCharacterColors( Color.gray );

        // Set the randomly selected, fighting hero and monster characters back to white ( eg Makes it easier to see which characters are fighting )
        hero.GetComponent<SpriteRenderer>().color = Color.white;
        //monster.GetComponent<SpriteRenderer>().color = Color.white;

        // Some text that will be
        string log = "";


        /* TODO: Write code below to decide the outcome of the fighting hero and monster characters in an interesting way.
            Some examples to test:
                - Randomly select either the hero or monster to hit the other character.
                - Have the character with the higher health hit the other character ( Makes for longer battles )
                - Have both the hero and monster do damage to each other ( Makes for shorter battles )
                - Use the number of characters left in either the heroes or monsters lists to influence the outcome ( Closer battles )
                - Randomly scale the amount of damage a character inflicts.

            NOTES:
                - Make sure to call the TakeDamage( damageAmount ) method on whichever character(s) is receiving damage ( *See CharacterStats.cs )
                - Remember after doing damage to check if a character's health is equal to or less than zero, and if so, Destroy() the character's gameObject.
                - Make sure to assign some text describing the outcome to the log variable
                    eg "The " + characterDoingDamage.name + " hits the " + characterTakingDamage.name + " for " + characterDoingDamage.damage + " damage! It has " + characterTakingDamage.health + " HP remaining"
                        OR characterTakingDamage.name + " was defeated by " + characterDoingDamage.name
        */


        // EXAMPLE: Randomly choose either the the hero or the monster to hit the other.
        if( Random.value > 0.5f )
        {
            // Monster hits hero (HINT: See TakeDamage( amount ) method in CharacterStats script)

            // Check hero health.
            if( hero.health <= 0f )
            {
                // If the heros' health is less than or equal to zero, then destroy the hero and output 'the monster has defeated the hero'.
                // HINT: Destroy( ... );
                // log = monster.name + " defeated " + hero.name + ".";
            }
            else
            {
                // If the heros' health is greater than zero, then output 'the monster hit the hero for X points of damage'.
                // log = monster.name + " hit " + hero.name + " for " + monster.damage + " points of damage.";
            }
        }
        else
        {
            // Hero hits monster (HINT: See TakeDamage( amount ) method in CharacterStats script)
           
            // Check if monster health is less than or equal to zero.
                // If so, destroy the monster gameobject and output '{hero name} defeated {monster name}'
                // Else, output '{hero name} hit {monster name} for {hero damage} points of damage.'
            
        }



        //Writes the outcome of the fight to the screen.
        ouputLog.OutputText( log );
    }

    private void SetAllActiveCharacterColors( Color color )
    {
        foreach( CharacterStats ch in this.activeHeroes ) { ch.GetComponent<SpriteRenderer>().color = color; }
        foreach( CharacterStats ch in this.activeMonsters ) { ch.GetComponent<SpriteRenderer>().color = color; }
    }

    private void EndBattle( string finalMessage )
    {
        // Reset any remaining character colors back to white.
        this.SetAllActiveCharacterColors( Color.white );

        // Stop the Attack() method from been called.
        this.CancelInvoke();

        // Send the final string to the output log and tell it leave it onscreen.
        ouputLog.OutputText( finalMessage );
        ouputLog.leaveOnScreen = true;
    }
}
