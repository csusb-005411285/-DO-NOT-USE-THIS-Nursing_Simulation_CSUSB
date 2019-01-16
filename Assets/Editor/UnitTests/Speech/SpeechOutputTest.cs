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
        SpeechOutput testSOSize2 = ScriptableObject.CreateInstance<SpeechOutput>();
        SpeechOutput emptySO = ScriptableObject.CreateInstance<SpeechOutput>();

        OutputClip oc1 = ScriptableObject.CreateInstance<OutputClip>();
        OutputClip oc2 = ScriptableObject.CreateInstance<OutputClip>();

        testSO.outputAudioClipList = new OutputClip[] { oc1, oc2 };
        testSOSize2.outputAudioClipList = new OutputClip[] { oc1, oc2 };

        //Assert
        Assert.AreEqual(null, emptySO.GetOutputClip());

        Assert.AreEqual(oc1, testSO.GetOutputClip(0));
        Assert.AreNotEqual(oc1, testSO.GetOutputClip(1));
        Assert.AreEqual(oc2, testSO.GetOutputClip(1));
        Assert.AreEqual(oc2, testSO.GetOutputClip(10));
        Assert.IsInstanceOf(typeof(OutputClip), testSO.GetOutputClip(0));

        //test for random non repeating
        Assert.AreEqual(oc1, testSOSize2.GetOutputClip(0));
        for (int i = 0; i < 20; i++)
        {
            Assert.AreEqual(oc2, testSOSize2.GetOutputClip());
            Assert.AreEqual(oc1, testSOSize2.GetOutputClip());
        }
    }

}
