namespace JavaCC.Parser;

public class Phase3Data
{
    public Expansion Expansion;

    public int Count = 0;

    internal Phase3Data(Expansion exp, int count)
    {
        this.Expansion = exp;
        this.Count = count;
    }
}
