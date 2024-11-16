namespace Game3D;

public class Utils
{
    public static string ToScoreFormat(int score, int maxCharacters)
    {
        char[] scoreFormattedChar = new char[maxCharacters];
        string scoreString = score.ToString();
        int scoreCharactersCount = scoreString.Length;
        
        for(int characterIndex = 0; characterIndex < maxCharacters; characterIndex++)
            scoreFormattedChar[characterIndex] = '0';


        int characterStartIndex = maxCharacters - scoreCharactersCount;
        for(int characterIndex = 0; characterIndex < maxCharacters; characterIndex++)
        {
            if(characterIndex >= characterStartIndex)
            {
                int scoreIndex = characterIndex - characterStartIndex;
                scoreFormattedChar[characterIndex] = scoreString[scoreIndex];
            }
        }
        
        string scoreFormatted = "";
        foreach(char character in scoreFormattedChar)
            scoreFormatted += character.ToString();
        return scoreFormatted;
    }
}
