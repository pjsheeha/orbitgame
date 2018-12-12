
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;
using System.Collections.Generic;

/// Displays dialogue lines to the player, and sends
/// user choices back to the dialogue system.

/** Note that this is just one way of presenting the
 * dialogue to the user. The only hard requirement
 * is that you provide the RunLine, RunOptions, RunCommand
 * and DialogueComplete coroutines; what they do is up to you.
 */
public class KeyboardDialogueUI : Yarn.Unity.DialogueUIBehaviour
{

    /// The objects that contain the dialogue line and the options including graphics.
    public GameObject lineObject;
    //public GameObject instantiatechatlog;
    public GameObject optionObject;
    public bool done = false;
    /// The UI element that displays lines
    public Text lineText;
    public bool createchatlog;
    /// The UI element that displays the current option
    public Text optionText;
    public bool sound = false;
    //the variable keeping track of the current option index
    public int currentOptionIndex = 0;

    /// A delegate (ie a function-stored-in-a-variable) that
    /// we call to tell the dialogue system about what option
    /// the user selected
    private Yarn.OptionChooser SetSelectedOption;

    //the options sent from yarn
    private IList<string> currentOptions;

    /// A UI element that appears after lines have finished appearing
    public GameObject continuePrompt;
    public GameObject optionPrompt;

    /// How quickly to show the text, in seconds per character
    [Tooltip("How quickly to show the text, in seconds per character")]
    public float textSpeed = 0.025f;


    void Awake()
    {
        // Start by hiding the container, line and option buttons
        //instantiatechatlog.SetActive(false);

        lineObject.SetActive(false);
        optionObject.SetActive(false);


        // Hide the continue prompt if it exists
        if (continuePrompt != null)
            continuePrompt.SetActive(false);
    }

    /// Show a line of dialogue, gradually
    public override IEnumerator RunLine(Yarn.Line line)
    {
        // Show the text
        lineObject.SetActive(true);
        optionObject.SetActive(false);
        //if (createchatlog == true){
        // instantiatechatlog.GetComponent<instantiateText>().MakeNewText();


        //    createchatlog = false;
        //}
        if (textSpeed > 0.0f)
        {
            // Display the line one character at a time
            var stringBuilder = new StringBuilder();

            foreach (char c in line.text)
            {
                stringBuilder.Append(c);
                lineText.text = stringBuilder.ToString();
                yield return new WaitForSeconds(textSpeed);
            }
        }
        else
        {
            // Display the line immediately if textSpeed == 0
            lineText.text = line.text;
            //instantiatechatlog.SetActive(true);
            //instantiatechatlog.GetComponent<instantiateText>().reassignText(line.text);

        }

        // Show the 'press any key' prompt when done, if we have one
        if (continuePrompt != null)
            continuePrompt.SetActive(true);

        // Wait for any user input

        while (sound == true)
        {
            //                print("bbbbbb");
            yield return null;
            //done = false;

        }





        if (continuePrompt != null)
            continuePrompt.SetActive(false);
        done = false;
    }

    /// Show a list of options, and wait for the player to make a selection.
    public override IEnumerator RunOptions(Yarn.Options optionsCollection,
                                            Yarn.OptionChooser optionChooser)
    {
        currentOptionIndex = 0;

        currentOptions = optionsCollection.options;

        ShowOption(currentOptionIndex);

        // Record that we're using it
        SetSelectedOption = optionChooser;

        if (optionPrompt != null)
        {
            if (currentOptions.Count == 1)
                optionPrompt.SetActive(false);
            else
                optionPrompt.SetActive(true);
        }

        // Wait until the chooser has been used and then removed (see SetOption below)
        while (SetSelectedOption != null)
        {
            yield return null;
        }

    }

    //cycle through options and select
    public void Update()
    {

        if (optionObject.activeSelf)
        {
            if (Input.GetButtonDown("Horizontal") && Input.GetAxisRaw("Horizontal") > 0)
            {
                currentOptionIndex++;

                if (currentOptionIndex >= currentOptions.Count)
                {
                    currentOptionIndex = 0;
                }

                ShowOption(currentOptionIndex);
            }

            if (Input.GetButtonDown("Horizontal") && Input.GetAxisRaw("Horizontal") < 0)
            {
                currentOptionIndex--;

                if (currentOptionIndex < 0)
                {
                    currentOptionIndex = currentOptions.Count - 1;
                }

                ShowOption(currentOptionIndex);
            }

            if (Input.GetButtonDown("Jump"))
            {
                SetOption(currentOptionIndex);
            }
        }
    }


    public void ShowOption(int i)
    {

        optionText.text = currentOptions[i];
        optionObject.SetActive(true);
    }

    /// Called by buttons to make a selection.
    public void SetOption(int selectedOption)
    {
        // Call the delegate to tell the dialogue system that we've
        // selected an option.
        SetSelectedOption(selectedOption);

        // Now remove the delegate so that the loop in RunOptions will exit
        SetSelectedOption = null;
    }


    /// Run an internal command.
    public override IEnumerator RunCommand(Yarn.Command command)
    {
        // "Perform" the command
        Debug.Log("Command: " + command.text);

        yield break;
    }

    /// Called when the dialogue system has started running.
    public override IEnumerator DialogueStarted()
    {
        Debug.Log("Dialogue starting!");

        //this is where you disable all the other controls

        yield break;
    }

    /// Called when the dialogue system has finished running.
    public override IEnumerator DialogueComplete()
    {
        Debug.Log("Complete!");

        //this is where you enable all the other controls
        //instantiatechatlog.SetActive(false);
        //createchatlog = true;



        // Hide the dialogue interface.
        lineObject.SetActive(false);
        optionObject.SetActive(false);

        yield break;
    }

}


