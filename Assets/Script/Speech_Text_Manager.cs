using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TextSpeech;
using UnityEngine.Android;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class Speech_Text_Manager : MonoBehaviour
{
    [SerializeField] private string language = "es-ES";
    [SerializeField] private Text uIText;

    [Serializable]

    public struct VoiceCommand
    {
        public string Keyword;
        public UnityEvent Response;
    }

    public VoiceCommand[] voiceCommands;

    private Dictionary<string, UnityEvent> commands = new Dictionary<string, UnityEvent>();

    private void Awake()
    {
        #if UNITY_ANDROID
            if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
            {
                Permission.RequestUserPermission(Permission.Microphone);
            }
        #endif
            foreach (var command in voiceCommands)
            {
                commands.Add(command.Keyword.ToLower(), command.Response);
            }
    }

    void Start()
    {
        TextToSpeech.Instance.Setting(language, 1, 1);
        SpeechToText.Instance.Setting(language);

        SpeechToText.Instance.onResultCallback = OnFinalSpeechResult;
        TextToSpeech.Instance.onStartCallBack = OnSpeakStart;
        TextToSpeech.Instance.onDoneCallback = OnSpeakStop;
        #if UNITY_ANDROID
            SpeechToText.Instance.onPartialResultsCallback = OnPartialSpeechResult;
        #endif
    }

    // Speech to Text

    public void StartListening()
    {
        SpeechToText.Instance.StartRecording();
    }

    public void StopListening()
    {
        SpeechToText.Instance.StopRecording();
    }

    public void OnFinalSpeechResult(string result)
    {
        uIText.text = result;
        if (result != null)
        {
            var response = commands[result.ToLower()];
            if (response != null)
            {
                response?.Invoke();
            }
        }
    }

    public void OnPartialSpeechResult(string result)
    {
        uIText.text = result;
    }

    public void StartSpeaking(string message)
    {
        TextToSpeech.Instance.StartSpeak(message);
    }

    public void StopSpeaking()
    {
        TextToSpeech.Instance.StopSpeak();
    }

    public void OnSpeakStart()
    {
        Debug.Log("Talking start...");
    }

    public void OnSpeakStop()
    {
        Debug.Log("Talking stop...");
    }
}
