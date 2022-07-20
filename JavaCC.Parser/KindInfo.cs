using System;

namespace JavaCC.Parser;

public class KindInfo
{
    public long[] ValidKinds = Array.Empty<long>();
    public long[] FinalKinds = Array.Empty<long>();
    public int ValidKindCnt = 0;
    public int VinalKindCnt = 0;

    public KindInfo(int length)
    {
        ValidKindCnt = 0;
        VinalKindCnt = 0;
        ValidKinds = new long[length / 64 + 1];
        FinalKinds = new long[length / 64 + 1];
    }

    public virtual void InsertValidKind(int kid)
    {
        long[] array = ValidKinds;
        int num = kid / 64;
        long[] array2 = array;
        array2[num] |= 1L << ((64 != -1) ? (kid % 64) : 0);
        ValidKindCnt++;
    }

    public virtual void InsertFinalKind(int count)
    {
        long[] array = FinalKinds;
        int num = count / 64;
        long[] array2 = array;
        array2[num] |= 1L << ((64 != -1) ? (count % 64) : 0);
        VinalKindCnt++;
    }
}
