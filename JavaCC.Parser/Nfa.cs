namespace JavaCC.Parser;

public class Nfa
{
    internal NfaState Start = new();
    internal NfaState End = new();

    public Nfa() { }

    public Nfa(NfaState ns1, NfaState ns2)
    {
        Start = ns1;
        End = ns2;
    }
}
