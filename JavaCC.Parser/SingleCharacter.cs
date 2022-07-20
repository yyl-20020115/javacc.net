namespace JavaCC.Parser;

public class SingleCharacter : Expansion
{
    public char CH { get; protected internal set; } = '\0';

    public SingleCharacter() { }

    public SingleCharacter(char ch)
    {
        this.CH = ch;
    }
}
