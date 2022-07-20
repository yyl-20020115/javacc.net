namespace JavaCC.Parser;

public class KindInfo
{
    public long[] validKinds;
    public long[] finalKinds;
    public int validKindCnt;
    public int finalKindCnt;

    public KindInfo(int length)
    {
        validKindCnt = 0;
        finalKindCnt = 0;
        validKinds = new long[length / 64 + 1];
        finalKinds = new long[length / 64 + 1];
    }

    public virtual void InsertValidKind(int kid)
    {
        long[] array = validKinds;
        int num = kid / 64;
        long[] array2 = array;
        array2[num] |= 1L << ((64 != -1) ? (kid % 64) : 0);
        validKindCnt++;
    }

    public virtual void InsertFinalKind(int P_0)
    {
        long[] array = finalKinds;
        int num = P_0 / 64;
        long[] array2 = array;
        array2[num] |= 1L << ((64 != -1) ? (P_0 % 64) : 0);
        finalKindCnt++;
    }
}
