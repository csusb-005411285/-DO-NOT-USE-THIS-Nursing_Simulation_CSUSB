
/// static class for globally sharing information about the current "patient" character
public static class CharacterManager
{
    /// character 0: "bob" (arbitrary name), Male
    public static CharacterSettings bob = new CharacterSettings(0, "bob");
    /// character 1: "sally" (arbitrary name), Female
    public static CharacterSettings sally = new CharacterSettings(1, "sally");

    /// global setting for getting current active character
    public static CharacterSettings currentCharacter = bob; //default to bob
}

/// class for holding all setings for a character instance
public class CharacterSettings
{
    /// character name
    public string name { get; private set; }
    /// character number
    public int characterNumber { get; private set; }

    //TODO set aws polly settings for character here

    public CharacterSettings(int characterNumber, string name)
    {
        this.name = name;
        this.characterNumber = characterNumber;
    }
}
