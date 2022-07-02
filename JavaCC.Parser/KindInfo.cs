namespace JavaCC.Parser;

internal class KindInfo
{
	internal long[] validKinds;
	internal long[] finalKinds;
	internal int validKindCnt;
	internal int finalKindCnt;
	
	internal KindInfo(int P_0)
	{
		validKindCnt = 0;
		finalKindCnt = 0;
		validKinds = new long[P_0 / 64 + 1];
		finalKinds = new long[P_0 / 64 + 1];
	}

	public virtual void InsertValidKind(int P_0)
	{
		long[] array = validKinds;
		int num = P_0 / 64;
		long[] array2 = array;
		array2[num] |= 1L << ((64 != -1) ? (P_0 % 64) : 0);
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
