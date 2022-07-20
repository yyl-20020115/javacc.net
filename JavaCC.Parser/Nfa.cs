namespace JavaCC.Parser;

public class Nfa
{
    public NfaState Start;
    public NfaState End;

    public Nfa() { }

    public Nfa(NfaState start, NfaState end)
    {
        Start = start;
        End = end;
    }
}
