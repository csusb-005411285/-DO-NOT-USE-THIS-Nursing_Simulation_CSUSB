using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using Speech;

public class SpeechOutputTest {

    [Test]
    public void SpeechOutputStoreTest()
    {
        //Arrange
        SpeechOutput testSO = ScriptableObject.CreateInstance<SpeechOutput>();
        SpeechOutput emptySO = ScriptableObject.CreateInstance<SpeechOutput>();

        OutputClip oc1 = ScriptableObject.CreateInstance<OutputClip>();
        OutputClip oc2 = ScriptableObject.CreateInstance<OutputClip>();

        testSO.outputAudioClipList = new OutputClip[] { oc1, oc2 };

        //Assert
        Assert.AreEqual(null, emptySO.GetOutputClip());

        Assert.AreEqual(oc1, testSO.GetOutputClip(0));
        Assert.AreEqual(oc2, testSO.GetOutputClip(1));
        Assert.AreEqual(oc2, testSO.GetOutputClip(10));
        Assert.IsInstanceOf(typeof(OutputClip), testSO.GetOutputClip());
    }

}
