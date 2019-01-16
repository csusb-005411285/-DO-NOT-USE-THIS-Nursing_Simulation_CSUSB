using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using Speech;
using UnityEditor;


public class OutputClipTest {

    [Test]
    public void OutputClipAudioClipStoreTest()
    {

        //Arrange
        OutputClip testOC = ScriptableObject.CreateInstance<OutputClip>();

        testOC.outputPhrase = "test phrase";

        AudioClip ac1 = (AudioClip)AssetDatabase.LoadAssetAtPath("Assets/Audio/Buzz.mp3", typeof(AudioClip));
        AudioClip ac2 = (AudioClip)AssetDatabase.LoadAssetAtPath("Assets/Audio/Buzz.mp3", typeof(AudioClip));
        testOC.outputAudioClips = new AudioClip[]{ ac1, ac2};

        //Assert
        Assert.AreEqual(ac1, testOC.outputAudioClips[0]);
        CharacterConfig.currentCharacter = CharacterConfig.characterList[0];
        Assert.AreEqual(ac1, testOC.GetAudioClip());

        Assert.AreEqual(ac2, testOC.outputAudioClips[1]);
        CharacterConfig.currentCharacter = CharacterConfig.characterList[1];
        Assert.AreEqual(ac2, testOC.GetAudioClip()); 
    }

    [TearDown]
    public void AfterEachTest()
    {
        CharacterConfig.currentCharacter = CharacterConfig.characterList[0];
    }
}
