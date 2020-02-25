using UnityEngine;
using UnityEngine.UI;

// DANGER ZONE: You should not have to modify anything on this script

public class WriteText : MonoBehaviour
{
    [ Range( 0, 0.1f ) ]
    [ Header( "Controls how fast the text is written to the screen" ) ]
    public float scrollSpeed;

    [ Range( 0, 2 ) ]
    [ Header( "Controls how long the text box waits before dissapearing" ) ]
    public float fadeSpeed;

    public Image textBackground;
    public Text textBox;
    int characters, currentChar;
    string fullOutputText, outputText;
    bool isWriting = false;

    [ HideInInspector ] public bool leaveOnScreen; // Leave final message showing winner onscreen at the end of the battle.

    private void Awake()
    {
        this.textBackground = this.GetComponentInChildren<Image>();
        this.textBox = this.GetComponentInChildren<Text>();
        this.textBox.text = string.Empty;
    }

    // Accepts a string, and writes it to the output log on screen.
    public void OutputText( string text )
    {
        if( this.isWriting == true ){ return; }
        if( string.IsNullOrEmpty( text ) == true ){ return; } // gFix: Stops error later on passing empty strings.

        this.ResetValues();

        this.textBackground.enabled = true;

        this.characters = text.Length;
        this.fullOutputText = text;
        
        InvokeRepeating( "ScrollingText", 0, this.scrollSpeed );

        // Create battle log in the Console for debugging.
        Debug.Log( text );
    }

    // Writes each individual character to the screen, decreasing/increasing the scrollSpeed float will make this function write faster/slower respectively.
    private void ScrollingText()
    {
        this.outputText += this.fullOutputText[ this.currentChar ];
        this.textBox.text = this.outputText;
        this.currentChar++;

        if( this.currentChar >= this.fullOutputText.Length )
        {
            if( this.leaveOnScreen == false ){ Invoke( "Delay", this.fadeSpeed ); }
            CancelInvoke( "ScrollingText" );
        }
    }

    /// <summary>
    /// Turns off the text box & resets critical parameters
    /// </summary>
    void Delay()
    {
        ResetValues();
        isWriting = false;
        textBackground.enabled = false;
    }

    void ResetValues()
    {
        currentChar = 0;
        outputText = null;
    textBox.text = string.Empty;
        //textBox.text = null;
    }
}
