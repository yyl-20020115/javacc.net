namespace JavaCC.Parser;

public class SingleCharacter
{
    public int Line { get; protected internal set; } = 1;
    public int Column { get; protected internal set; } = 1;
    public char CH { get; protected internal set; } = '\0';

    public SingleCharacter() { }

    public SingleCharacter(char ch)
    {
        this.CH = ch;
    }
}
