namespace org.javacc.parser;

public class Nfa
{
	internal NfaState start;
	internal NfaState end;
	
	public Nfa()
	{
		start = new NfaState();
		end = new NfaState();
	}

	
	public Nfa(NfaState ns1, NfaState ns2)
	{
		start = ns1;
		end = ns2;
	}
}
