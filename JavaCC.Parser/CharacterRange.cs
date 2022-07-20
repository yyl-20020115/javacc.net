namespace JavaCC.Parser;

public class CharacterRange :Expansion
{
    public char Left = '\0';
    public char Right = '\0';

    public CharacterRange() { }

    public CharacterRange(char c1, char c2)
    {
        if (c1 > c2)
        {
            JavaCCErrors.Semantic_Error(this, ("Invalid range : \"") + ((int)c1) + ("\" - \"")
                + ((int)c2)
                + ("\". First character shoud be less than or equal to the second one in a range.")
                );
        }
        Left = c1;
        Right = c2;
    }
}
