namespace JavaCC.Parser;

public class SingleCharacter
{
    internal int line = 1;
    internal int column = 1;
    public char ch = '\0';

    internal SingleCharacter() { }

    internal SingleCharacter(char ch)
    {
        this.ch = ch;
    }
}
