
/// static class for globally sharing information about the current "patient" character
public static class CharacterConfig
{
    /// character 0: "bob" (arbitrary name), Male
    public static CharacterSettings bob = new CharacterSettings(0, "bob", Amazon.Polly.VoiceId.Matthew);
    /// character 1: "sally" (arbitrary name), Female
    public static CharacterSettings sally = new CharacterSettings(1, "sally", Amazon.Polly.VoiceId.Amy);

    /// global setting for getting current active character
    public static CharacterSettings currentCharacter = bob; //default to bob
    /// list of configured characters, EDIT THIS LIST WHEN CHARACTERS ARE ADDED OR REMOVED!!!! ==========================================================
    public static CharacterSettings[] characterList = {bob, sally};
}

/// class for holding all setings for a character instance
public class CharacterSettings
{
    /// character number, should mirror character number in characterList[]
    public int characterNumber { get; private set; }
    /// character name
    public string name { get; private set; }

    /// character polly voiceID
    public Amazon.Polly.VoiceId pollyVoiceId { get; private set; }

    public CharacterSettings(int characterNumber, string name, Amazon.Polly.VoiceId pollyVoiceId)
    {
        this.name = name;
        this.characterNumber = characterNumber;
        this.pollyVoiceId = pollyVoiceId;
    }
}
