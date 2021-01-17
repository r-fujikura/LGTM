using Academy.HoloToolkit.Unity;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Video;

/// <summary>
/// This keeps track of the various parts of the recording and text display process.
/// </summary>

[RequireComponent(typeof(AudioSource), typeof(MicrophoneManager), typeof(KeywordManager))]
public class Communicator : MonoBehaviour
{
    [Tooltip("The button to be selected when the user wants to record audio and dictation.")]
    public Button RecordButton;
    [Tooltip("The button to be selected when the user wants to stop recording.")]
    public Button RecordStopButton;
    [Tooltip("The button to be selected when the user wants to play audio.")]
    public Button PlayButton;
    [Tooltip("The button to be selected when the user wants to stop playing.")]
    public Button PlayStopButton;

    [Tooltip("The sound to be played when the recording session starts.")]
    public AudioClip StartListeningSound;
    [Tooltip("The sound to be played when the recording session ends.")]
    public AudioClip StopListeningSound;

    [Tooltip("The icon to be displayed while recording is happening.")]
    public GameObject MicIcon;

    [Tooltip("A message to help the user understand what to do next.")]
    public Renderer MessageUIRenderer;

    [Tooltip("The waveform animation to be played while the microphone is recording.")]
    public Transform Waveform;
    [Tooltip("The meter animation to be played while the microphone is recording.")]
    public VideoPlayer SoundMeter;

    public static AudioSource dictationAudio;
    private AudioSource startAudio;
    private AudioSource stopAudio;

    private float origLocalScale;
    private bool animateWaveform;
    public static GameObject Camera;
    
    private int i,rec,rec2;
    private bool isRunning;
    private bool isRunning2;
    private bool isRunning3;
    public static float vol;
    private  AudioSource audiooo;
    public static int kazu;//debug用チェックと発話の数
    public static float checking;
    public GameObject target;
    public static AudioSource[] hatsuwa = new AudioSource[10];
    private static string deviceName = string.Empty;
    private int samplingRate;
    public static int length;
    public static int unused;
    public static int restart;
    public static int machi;
    public enum Message
    {
        PressMic,
        PressStop,
        SendMessage
    };

    private MicrophoneManager microphoneManager;
    
    void Start()
    {
        isRunning = false;
        isRunning2 = false;
        length = 30;//本当は秒数はかって動的にしたいけど最初に宣言しなきゃだめっぽいから
        kazu = 0;
        checking = 0;
        i = 0;
        rec = 0;
        rec2 = 0;
        vol = 0;
        machi = 0;
        dictationAudio = gameObject.GetComponent<AudioSource>();

        startAudio = gameObject.AddComponent<AudioSource>();
        stopAudio = gameObject.AddComponent<AudioSource>();

        startAudio.playOnAwake = false;
        startAudio.clip = StartListeningSound;
        stopAudio.playOnAwake = false;
        stopAudio.clip = StopListeningSound;

        microphoneManager = GetComponent<MicrophoneManager>();

        Camera = GameObject.FindGameObjectWithTag("MainCamera");

        
        origLocalScale = Waveform.localScale.y;
        animateWaveform = false;

        //audiooo = GetComponent<AudioSource>();
        //dictationAudio.clip = Microphone.Start(null, true, 999, 44100);  // マイクからのAudio-InをAudioSourceに流す
        //dictationAudio.loop = true;                                      // ループ再生にしておく
        //dictationAudio.mute = true;                                      // マイクからの入力音なので音を流す必要がない
        //while (!(Microphone.GetPosition("") > 0)) { }             // マイクが取れるまで待つ。空文字でデフォルトのマイクを探してくれる
        //dictationAudio.Play();
       
        
        //detect the no conversations.
        
        
        // TODO: 2.a Delete the following two lines:
        //RecordButton.SetActive(false);
        //MessageUIRenderer.gameObject.SetActive(false);
    }

    void Update()
    {
      
        if (i == 0)
        {
            StartCoroutine(DelaystartMethod());
           
        }
        else if (i == 1)
        {
            StartCoroutine(RecordDelayMethod());
        }

        //vol = GetAveragedVolume();
        //Debug.Log(vol);
        if (i==1)
        {
            StartCoroutine(Delaytalkcheck());
        }
        if (animateWaveform)
        {
            Vector3 newScale = Waveform.localScale;
            newScale.y = Mathf.Sin(Time.time * 2.0f) * origLocalScale;
            Waveform.localScale = newScale;
        }

        // If the audio has stopped playing and the PlayStop button is still active,  reset the UI.
        if (!dictationAudio.isPlaying && PlayStopButton.enabled)
        {
            //PlayStop();
        }
        
    }

    public void Record()
    {
        MicTest.talkon = 1;
        //MicTest.audio.Stop();
        //Microphone.End(deviceName);
        hatsuwa[0] = GetComponent<AudioSource>();
        //Microphone.GetDeviceCaps(deviceName, out unused, out samplingRate);
        //hatsuwa[0].clip = Microphone.Start(deviceName, false,10, AudioSettings.outputSampleRate);

        //
        //Microphone.End(deviceName);

        //hatsuwa[kazu].clip = MicTest.audio.clip;//20:28
        //hatsuwa[kazu].clip = Resources.Load("okaeri", typeof(AudioClip)) as AudioClip;
        //hatsuwa[kazu].loop = true;
        //hatsuwa[kazu].mute = false;
        //hatsuwa[kazu].mute = true;//getoutputdataでやったほうがいいかな？でも値いらない。
        //hatsuwa[0].Play();//aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa



        //if (RecordButton.IsOn())
        //{
        // Turn the microphone on, which returns the recorded audio.

        //dictationAudio.clip = microphoneManager.StartRecording();

        //hatsuwa[0].clip= Microphone.Start(Microphone.devices[0], false, 999, AudioSettings.outputSampleRate);
        //hatsuwa[0].loop = true;
        //while (!(Microphone.GetPosition(Microphone.devices[0]) > 0)) { }
        //hatsuwa[0].Play();
        //Microphone.End(deviceName);

        //MicTest mic = Camera.GetComponent<MicTest>();
        //mic.talkcheck();


        // Set proper UI state and play a sound.
        SetUI(true, Message.PressStop, startAudio);

            RecordButton.gameObject.SetActive(false);
            RecordStopButton.gameObject.SetActive(true);
        //}
    }

    public void RecordStop()
    {


        //if (RecordStopButton.IsOn())
        //{
        // Turn off the microphone.
        //checking = checking + 1;
        //MicTest.audio.loop = false;
        //MicTest.audio.Stop();rrr
        //MicTest.audio.Play();
        //MicTest.talkon = 0;
       
        //MicTest.audio.Play();
      
        //hatsuwa[0].Play();
        target.AddComponent<AudioSource>();
        target.AddComponent<Objectconnect>();
        MicTest.audio[MicTest.n].Stop();
        Instantiate(target, new Vector3(Camera.transform.position.x, Camera.transform.position.y, Camera.transform.position.z), Quaternion.Euler(90, 0, 0));
        //hatsuwa[kazu].Stop();//場所移動

        //MicTest.audio.clip = null;
        //hatsuwa[0] = target.GetComponent<AudioSource>();
        //hatsuwa[0].clip = microphoneManager.StartRecording();




        //MicTest.talkon = 0;//もう一度判定のマイクを拾う  20:02
        // MicTest mic = Camera.GetComponent<MicTest>();
        //mic.talkcheck();

        //MicTest.audio.Play();
        // Restart the PhraseRecognitionSystem and KeywordRecognizer
       
            
            microphoneManager.StopRecording();
            
            restart = 1;
            kazu = kazu + 1;
            microphoneManager.StartCoroutine("RestartSpeechSystem", GetComponent<KeywordManager>());

            // Set proper UI state and play a sound.
            SetUI(false, Message.SendMessage, stopAudio);

            PlayButton.SetActive(true);
            RecordStopButton.SetActive(false);
            
        
        //}
    }

    public void Play()
    {
        if (PlayButton.IsOn())
        {
            PlayButton.gameObject.SetActive(false);
            PlayStopButton.gameObject.SetActive(true);

            //dictationAudio.Play();
            
        }
    }

    public void PlayStop()
    {
        if (PlayStopButton.IsOn())
        {
            PlayStopButton.gameObject.SetActive(false);
            PlayButton.gameObject.SetActive(true);

            //dictationAudio.Stop();
        }
    }

    public void SendCommunicatorMessage()
    {
        AstronautWatch.Instance.CloseCommunicator();
    }

    void ResetAfterTimeout()
    {
        // Set proper UI state and play a sound.
        SetUI(false, Message.PressMic, stopAudio);

        RecordStopButton.gameObject.SetActive(false);
        RecordButton.gameObject.SetActive(true);
    }

    private void SetUI(bool enabled, Message newMessage, AudioSource soundToPlay)
    {
        animateWaveform = enabled;
        if (enabled)
        {
            SoundMeter.Play();
        }
        else
        {
            SoundMeter.Stop();
        }
        MicIcon.SetActive(enabled);

        StartCoroutine(ChangeLabel(newMessage));

        soundToPlay.Play();
    }

    private IEnumerator ChangeLabel(Message newMessage)
    {
        switch (newMessage)
        {
            case Message.PressMic:
                for (float i = 0.0f; i < 1.0f; i += 0.1f)
                {
                    MessageUIRenderer.material.SetFloat("_BlendTex01", Mathf.Lerp(1.0f, 0.0f, i));
                    yield return null;
                }
                break;
            case Message.PressStop:
                for (float i = 0.0f; i < 1.0f; i += 0.1f)
                {
                    MessageUIRenderer.material.SetFloat("_BlendTex01", Mathf.Lerp(0.0f, 1.0f, i));
                    yield return null;
                }
                break;
            case Message.SendMessage:
                for (float i = 0.0f; i < 1.0f; i += 0.1f)
                {
                    MessageUIRenderer.material.SetFloat("_BlendTex02", Mathf.Lerp(0.0f, 1.0f, i));
                    yield return null;
                }
                break;
        }
    }
    private IEnumerator DelaystartMethod()
    {
        if (isRunning) { yield break; }
        isRunning = true;

        yield return new WaitForSeconds(7f);
        Debug.Log("Delay finish");
        i = 1;
        isRunning = false;
    }
    private IEnumerator RecordDelayMethod()
    {

        if (isRunning2) { yield break; }
        if (rec == 0)
        {
            isRunning2 = true;
            float a;
            Vector3 A = Camera.transform.position;


            a = Camera.transform.position.x;
            yield return new WaitForSeconds(3f);
            //if (Camera.transform.position.x >0 )
            //if (a - Camera.transform.position.x > -0.1 && a - Camera.transform.position.x < 0.1)
            if (Vector3.Distance(A, Camera.transform.position) < 0.1)
            {
                //if (rec==0) {
                //Debug.Log(Vector3.Distance(A, Camera.transform.position));
                Record();
                rec = 1;

                //}
            }
        }
    
        
            isRunning2 = false;
        
    }

   /* private IEnumerator StopDelayMethod()
    {
        if (isRunning2) { yield break; }
        isRunning2 = true;
        float a;
        Vector3 A = Camera.transform.position;

        a = Camera.transform.position.x;
        yield return new WaitForSeconds(3f);
        //if (Camera.transform.position.x >0 )
        //if (a - Camera.transform.position.x > -0.1 && a - Camera.transform.position.x < 0.1)
        if (Vector3.Distance(A, Camera.transform.position) < 0.1)
        {
            if (rec == 0)
            {
                Debug.Log(Vector3.Distance(A, Camera.transform.position));
                Record();
                rec = 1;

            }
        }
        isRunning2 = false;
    }
    */

    
    private IEnumerator Delaytalkcheck()
    {
        if (isRunning3) { yield break; }
        if (rec == 1)
        {
            isRunning3 = true;
        float a;
        a = MicTest.vol;
        
            yield return new WaitForSeconds(6f);
            //if (Camera.transform.position.x >0 )
            //if (a - Camera.transform.position.x > -0.1 && a - Camera.transform.position.x < 0.1)
            if (a < 0.03)
            {

                if (MicTest.vol < 0.03)
                {
                    //if (rec == 1)
                    //{ 

                    RecordStop();
                    

                    rec = 0;
                    //}
                }


            }
        }
        isRunning3 = false;
    }
    
    
}
