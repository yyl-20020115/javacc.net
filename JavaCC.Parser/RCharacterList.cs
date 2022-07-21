namespace JavaCC.Parser;

using System;
using System.Collections.Generic;

public class RCharacterList : RegularExpression
{
    public bool NegatedList = false;

    public readonly List<Expansion> Descriptors = new();

    public bool Transformed = false;

    public RCharacterList()
    {
    }

    public virtual void ToCaseNeutral()
    {
        int nc = Descriptors.Count;
        for (int i = 0; i < nc; i++)
        {
            int ch;
            if (Descriptors[i] is SingleCharacter character)
            {
                ch = character.CH;
                if (ch != char.ToLower((char)ch))
                {
                    Descriptors.Add(new SingleCharacter(char.ToLower((char)ch)));
                }
                if (ch != char.ToUpper((char)ch))
                {
                    Descriptors.Add(new SingleCharacter(char.ToUpper((char)ch)));
                }
                continue;
            }
            ch = ((CharacterRange)Descriptors[i]).Left;
            int right = ((CharacterRange)Descriptors[i]).Right;
            int j;
            for (j = 0; ch > DiffLowerCaseRanges[j]; j += 2)
            {
            }
            if (ch < DiffLowerCaseRanges[j])
            {
                if (right >= DiffLowerCaseRanges[j])
                {
                    if (right > DiffLowerCaseRanges[j + 1])
                    {
                        Descriptors.Add(new CharacterRange(char.ToLower(DiffLowerCaseRanges[j]), char.ToLower(DiffLowerCaseRanges[j + 1])));
                        goto IL_01e9;
                    }
                    Descriptors.Add(new CharacterRange(char.ToLower(DiffLowerCaseRanges[j]), (char)(char.ToLower(DiffLowerCaseRanges[j]) + right - DiffLowerCaseRanges[j])));
                }
            }
            else
            {
                if (right > DiffLowerCaseRanges[j + 1])
                {
                    Descriptors.Add(new CharacterRange((char)(char.ToLower(DiffLowerCaseRanges[j]) + ch - DiffLowerCaseRanges[j]), char.ToLower(DiffLowerCaseRanges[j + 1])));
                    goto IL_01e9;
                }
                Descriptors.Add(new CharacterRange((char)(char.ToLower(DiffLowerCaseRanges[j]) + ch - DiffLowerCaseRanges[j]), (char)(char.ToLower(DiffLowerCaseRanges[j]) + right - DiffLowerCaseRanges[j])));
            }
            goto IL_0279;
        IL_01e9:
            for (j += 2; right > DiffLowerCaseRanges[j]; j += 2)
            {
                if (right <= DiffLowerCaseRanges[j + 1])
                {
                    Descriptors.Add(new CharacterRange(char.ToLower(DiffLowerCaseRanges[j]), (char)(char.ToLower(DiffLowerCaseRanges[j]) + right - DiffLowerCaseRanges[j])));
                    break;
                }
                Descriptors.Add(new CharacterRange(char.ToLower(DiffLowerCaseRanges[j]), char.ToLower(DiffLowerCaseRanges[j + 1])));
            }
            goto IL_0279;
        IL_0279:
            for (j = 0; ch > DiffUpperCaseRanges[j]; j += 2)
            {
            }
            if (ch < DiffUpperCaseRanges[j])
            {
                if (right < DiffUpperCaseRanges[j])
                {
                    continue;
                }
                if (right <= DiffUpperCaseRanges[j + 1])
                {
                    Descriptors.Add(new CharacterRange(char.ToUpper(DiffUpperCaseRanges[j]), (char)(char.ToUpper(DiffUpperCaseRanges[j]) + right - DiffUpperCaseRanges[j])));
                    continue;
                }
                Descriptors.Add(new CharacterRange(char.ToUpper(DiffUpperCaseRanges[j]), char.ToUpper(DiffUpperCaseRanges[j + 1])));
            }
            else
            {
                if (right <= DiffUpperCaseRanges[j + 1])
                {
                    Descriptors.Add(new CharacterRange((char)(char.ToUpper(DiffUpperCaseRanges[j]) + ch - DiffUpperCaseRanges[j]), (char)(char.ToUpper(DiffUpperCaseRanges[j]) + right - DiffUpperCaseRanges[j])));
                    continue;
                }
                Descriptors.Add(new CharacterRange((char)(char.ToUpper(DiffUpperCaseRanges[j]) + ch - DiffUpperCaseRanges[j]), char.ToUpper(DiffUpperCaseRanges[j + 1])));
            }
            for (j += 2; right > DiffUpperCaseRanges[j]; j += 2)
            {
                if (right <= DiffUpperCaseRanges[j + 1])
                {
                    Descriptors.Add(new CharacterRange(char.ToUpper(DiffUpperCaseRanges[j]), (char)(char.ToUpper(DiffUpperCaseRanges[j]) + right - DiffUpperCaseRanges[j])));
                    break;
                }
                Descriptors.Add(new CharacterRange(char.ToUpper(DiffUpperCaseRanges[j]), char.ToUpper(DiffUpperCaseRanges[j + 1])));
            }
        }
    }


    public virtual void SortDescriptors()
    {
        List<Expansion> vector = new(Descriptors.Count);
        int num = 0;
        for (int i = 0; i < Descriptors.Count; i++)
        {
            int n2;
            if (Descriptors[i] is SingleCharacter singleCharacter)
            {
                n2 = 0;
                while (true)
                {
                    if (n2 < num)
                    {
                        if (vector[n2] is SingleCharacter character2)
                        {
                            if (character2.CH <= singleCharacter.CH)
                            {
                                if (character2.CH == singleCharacter.CH)
                                {
                                    break;
                                }
                                goto IL_00e5;
                            }
                        }
                        else
                        {
                            int left = ((CharacterRange)vector[n2]).Left;
                            if (InRange(singleCharacter.CH, (CharacterRange)vector[n2]))
                            {
                                break;
                            }
                            if (left <= singleCharacter.CH)
                            {
                                goto IL_00e5;
                            }
                        }
                    }
                    vector.Insert(n2, singleCharacter);//.insertElementAt(singleCharacter, num2);
                    num++;
                    break;
                IL_00e5:
                    n2++;
                }
                continue;
            }
            CharacterRange characterRange = (CharacterRange)Descriptors[i];
            n2 = 0;
            while (true)
            {
                if (n2 < num)
                {
                    if (vector[n2] is SingleCharacter character2)
                    {
                        if (InRange(character2.CH, characterRange))
                        {
                            vector.RemoveAt(n2--);
                            num += -1;
                            goto IL_0268;
                        }
                        if (character2.CH <= characterRange.Right)
                        {
                            goto IL_0268;
                        }
                    }
                    else
                    {
                        if (SubRange(characterRange, (CharacterRange)vector[n2]))
                        {
                            break;
                        }
                        if (SubRange((CharacterRange)vector[n2], characterRange))
                        {
                            vector[n2] = (characterRange);
                            break;
                        }
                        if (Overlaps(characterRange, (CharacterRange)vector[n2]))
                        {
                            characterRange.Left = (char)(((CharacterRange)vector[n2]).Right + 1);
                            goto IL_0268;
                        }
                        if (Overlaps((CharacterRange)vector[n2], characterRange))
                        {
                            CharacterRange obj = characterRange;
                            ((CharacterRange)vector[n2]).Right = (char)(characterRange.Left + 1);
                            characterRange = (CharacterRange)vector[n2];
                            vector[n2] = obj;//.setElementAt(obj, num2);
                            goto IL_0268;
                        }
                        if (((CharacterRange)vector[n2]).Left <= characterRange.Right)
                        {
                            goto IL_0268;
                        }
                    }
                }
                vector.Insert(num, characterRange);//.insertElementAt(characterRange, num2);
                num++;
                break;
            IL_0268:
                n2++;
            }
        }
        vector.TrimExcess();
        Descriptors.Clear();
        Descriptors.AddRange(vector);
    }


    internal virtual void RemoveNegation()
    {
        SortDescriptors();
        List<Expansion> vector = new();
        int n = -1;
        for (int i = 0; i < Descriptors.Count; i++)
        {
            int ch;
            if (Descriptors[i] is SingleCharacter character)
            {
                ch = character.CH;
                if (ch >= 0 && ch <= n + 1)
                {
                    n = ch;
                }
                else
                {
                    vector.Add(new CharacterRange((char)(n + 1), (char)((n = ch) - 1)));
                }
                continue;
            }
            ch = ((CharacterRange)Descriptors[i]).Left;
            int right = ((CharacterRange)Descriptors[i]).Right;
            if (ch >= 0 && ch <= n + 1)
            {
                n = right;
                continue;
            }
            vector.Add(new CharacterRange((char)(n + 1), (char)(ch - 1)));
            n = right;
        }
        if (NfaState.unicodeWarningGiven || Options.JavaUnicodeEscape)
        {
            if (n < 65535)
            {
                vector.Add(new CharacterRange((char)(n + 1), '\uffff'));
            }
        }
        else if (n < 255)
        {
            vector.Add(new CharacterRange((char)(n + 1), 'ÿ'));
        }
        Descriptors.Clear();
        Descriptors.AddRange(vector);
        NegatedList = false;
    }

    public static bool InRange(char c, CharacterRange r) => (c >= r.Left && c <= r.Right);

    public static bool SubRange(CharacterRange r1, CharacterRange r2) => (r1.Left >= r2.Left && r1.Right <= r2.Right);

    public static bool Overlaps(CharacterRange cr1, CharacterRange cr2) => (cr1.Left <= cr2.Right && cr1.Right > cr2.Right);

    public override Nfa GenerateNfa(bool b)
    {
        if (!Transformed)
        {
            if (Options.IgnoreCase || b)
            {
                ToCaseNeutral();
                SortDescriptors();
            }
            if (NegatedList)
            {
                RemoveNegation();
            }
            else
            {
                SortDescriptors();
            }
        }
        Transformed = true;
        var nfa = new Nfa();
        var start = nfa.Start;
        var end = nfa.End;
        for (int i = 0; i < Descriptors.Count; i++)
        {
            if (Descriptors[i] is SingleCharacter character)
            {
                start.AddChar(character.CH);
                continue;
            }
            var characterRange = (CharacterRange)Descriptors[i];
            if (characterRange.Left == characterRange.Right)
            {
                start.AddChar(characterRange.Left);
            }
            else
            {
                start.AddRange(characterRange.Left, characterRange.Right);
            }
        }
        start.next = end;
        return nfa;
    }

    public RCharacterList(char ch)
    {
        Descriptors = new()
        {
            new SingleCharacter(ch)
        };
        NegatedList = false;
        Ordinal = int.MaxValue;
    }


    public override bool CanMatchAnyChar => (NegatedList && (Descriptors == null || Descriptors.Count == 0));
    public static char[] DiffLowerCaseRanges = new char[]
        {
            'A', 'Z', 'À', 'Ö', 'Ø', 'Þ', 'Ā', 'Ā', 'Ă', 'Ă',
            'Ą', 'Ą', 'Ć', 'Ć', 'Ĉ', 'Ĉ', 'Ċ', 'Ċ', 'Č', 'Č',
            'Ď', 'Ď', 'Đ', 'Đ', 'Ē', 'Ē', 'Ĕ', 'Ĕ', 'Ė', 'Ė',
            'Ę', 'Ę', 'Ě', 'Ě', 'Ĝ', 'Ĝ', 'Ğ', 'Ğ', 'Ġ', 'Ġ',
            'Ģ', 'Ģ', 'Ĥ', 'Ĥ', 'Ħ', 'Ħ', 'Ĩ', 'Ĩ', 'Ī', 'Ī',
            'Ĭ', 'Ĭ', 'Į', 'Į', 'İ', 'İ', 'Ĳ', 'Ĳ', 'Ĵ', 'Ĵ',
            'Ķ', 'Ķ', 'Ĺ', 'Ĺ', 'Ļ', 'Ļ', 'Ľ', 'Ľ', 'Ŀ', 'Ŀ',
            'Ł', 'Ł', 'Ń', 'Ń', 'Ņ', 'Ņ', 'Ň', 'Ň', 'Ŋ', 'Ŋ',
            'Ō', 'Ō', 'Ŏ', 'Ŏ', 'Ő', 'Ő', 'Œ', 'Œ', 'Ŕ', 'Ŕ',
            'Ŗ', 'Ŗ', 'Ř', 'Ř', 'Ś', 'Ś', 'Ŝ', 'Ŝ', 'Ş', 'Ş',
            'Š', 'Š', 'Ţ', 'Ţ', 'Ť', 'Ť', 'Ŧ', 'Ŧ', 'Ũ', 'Ũ',
            'Ū', 'Ū', 'Ŭ', 'Ŭ', 'Ů', 'Ů', 'Ű', 'Ű', 'Ų', 'Ų',
            'Ŵ', 'Ŵ', 'Ŷ', 'Ŷ', 'Ÿ', 'Ÿ', 'Ź', 'Ź', 'Ż', 'Ż',
            'Ž', 'Ž', 'Ɓ', 'Ɓ', 'Ƃ', 'Ƃ', 'Ƅ', 'Ƅ', 'Ɔ', 'Ɔ',
            'Ƈ', 'Ƈ', 'Ɖ', 'Ɖ', 'Ɗ', 'Ɗ', 'Ƌ', 'Ƌ', 'Ə', 'Ə',
            'Ɛ', 'Ɛ', 'Ƒ', 'Ƒ', 'Ɠ', 'Ɠ', 'Ɣ', 'Ɣ', 'Ɩ', 'Ɩ',
            'Ɨ', 'Ɨ', 'Ƙ', 'Ƙ', 'Ɯ', 'Ɯ', 'Ɲ', 'Ɲ', 'Ơ', 'Ơ',
            'Ƣ', 'Ƣ', 'Ƥ', 'Ƥ', 'Ƨ', 'Ƨ', 'Ʃ', 'Ʃ', 'Ƭ', 'Ƭ',
            'Ʈ', 'Ʈ', 'Ư', 'Ư', 'Ʊ', 'Ʋ', 'Ƴ', 'Ƴ', 'Ƶ', 'Ƶ',
            'Ʒ', 'Ʒ', 'Ƹ', 'Ƹ', 'Ƽ', 'Ƽ', 'Ǆ', 'Ǆ', 'ǅ', 'ǅ',
            'Ǉ', 'Ǉ', 'ǈ', 'ǈ', 'Ǌ', 'Ǌ', 'ǋ', 'ǋ', 'Ǎ', 'Ǎ',
            'Ǐ', 'Ǐ', 'Ǒ', 'Ǒ', 'Ǔ', 'Ǔ', 'Ǖ', 'Ǖ', 'Ǘ', 'Ǘ',
            'Ǚ', 'Ǚ', 'Ǜ', 'Ǜ', 'Ǟ', 'Ǟ', 'Ǡ', 'Ǡ', 'Ǣ', 'Ǣ',
            'Ǥ', 'Ǥ', 'Ǧ', 'Ǧ', 'Ǩ', 'Ǩ', 'Ǫ', 'Ǫ', 'Ǭ', 'Ǭ',
            'Ǯ', 'Ǯ', 'Ǳ', 'Ǳ', 'ǲ', 'ǲ', 'Ǵ', 'Ǵ', 'Ǻ', 'Ǻ',
            'Ǽ', 'Ǽ', 'Ǿ', 'Ǿ', 'Ȁ', 'Ȁ', 'Ȃ', 'Ȃ', 'Ȅ', 'Ȅ',
            'Ȇ', 'Ȇ', 'Ȉ', 'Ȉ', 'Ȋ', 'Ȋ', 'Ȍ', 'Ȍ', 'Ȏ', 'Ȏ',
            'Ȑ', 'Ȑ', 'Ȓ', 'Ȓ', 'Ȕ', 'Ȕ', 'Ȗ', 'Ȗ', 'Ά', 'Ά',
            'Έ', 'Ί', 'Ό', 'Ό', 'Ύ', 'Ώ', 'Α', 'Ρ', 'Σ', 'Ϋ',
            'Ϣ', 'Ϣ', 'Ϥ', 'Ϥ', 'Ϧ', 'Ϧ', 'Ϩ', 'Ϩ', 'Ϫ', 'Ϫ',
            'Ϭ', 'Ϭ', 'Ϯ', 'Ϯ', 'Ё', 'Ќ', 'Ў', 'Џ', 'А', 'А',
            'Б', 'Б', 'В', 'Я', 'Ѡ', 'Ѡ', 'Ѣ', 'Ѣ', 'Ѥ', 'Ѥ',
            'Ѧ', 'Ѧ', 'Ѩ', 'Ѩ', 'Ѫ', 'Ѫ', 'Ѭ', 'Ѭ', 'Ѯ', 'Ѯ',
            'Ѱ', 'Ѱ', 'Ѳ', 'Ѳ', 'Ѵ', 'Ѵ', 'Ѷ', 'Ѷ', 'Ѹ', 'Ѹ',
            'Ѻ', 'Ѻ', 'Ѽ', 'Ѽ', 'Ѿ', 'Ѿ', 'Ҁ', 'Ҁ', 'Ґ', 'Ґ',
            'Ғ', 'Ғ', 'Ҕ', 'Ҕ', 'Җ', 'Җ', 'Ҙ', 'Ҙ', 'Қ', 'Қ',
            'Ҝ', 'Ҝ', 'Ҟ', 'Ҟ', 'Ҡ', 'Ҡ', 'Ң', 'Ң', 'Ҥ', 'Ҥ',
            'Ҧ', 'Ҧ', 'Ҩ', 'Ҩ', 'Ҫ', 'Ҫ', 'Ҭ', 'Ҭ', 'Ү', 'Ү',
            'Ұ', 'Ұ', 'Ҳ', 'Ҳ', 'Ҵ', 'Ҵ', 'Ҷ', 'Ҷ', 'Ҹ', 'Ҹ',
            'Һ', 'Һ', 'Ҽ', 'Ҽ', 'Ҿ', 'Ҿ', 'Ӂ', 'Ӂ', 'Ӄ', 'Ӄ',
            'Ӈ', 'Ӈ', 'Ӌ', 'Ӌ', 'Ӑ', 'Ӑ', 'Ӓ', 'Ӓ', 'Ӕ', 'Ӕ',
            'Ӗ', 'Ӗ', 'Ә', 'Ә', 'Ӛ', 'Ӛ', 'Ӝ', 'Ӝ', 'Ӟ', 'Ӟ',
            'Ӡ', 'Ӡ', 'Ӣ', 'Ӣ', 'Ӥ', 'Ӥ', 'Ӧ', 'Ӧ', 'Ө', 'Ө',
            'Ӫ', 'Ӫ', 'Ӯ', 'Ӯ', 'Ӱ', 'Ӱ', 'Ӳ', 'Ӳ', 'Ӵ', 'Ӵ',
            'Ӹ', 'Ӹ', 'Ա', 'Ֆ', 'Ⴀ', 'Ⴥ', 'Ḁ', 'Ḁ', 'Ḃ', 'Ḃ',
            'Ḅ', 'Ḅ', 'Ḇ', 'Ḇ', 'Ḉ', 'Ḉ', 'Ḋ', 'Ḋ', 'Ḍ', 'Ḍ',
            'Ḏ', 'Ḏ', 'Ḑ', 'Ḑ', 'Ḓ', 'Ḓ', 'Ḕ', 'Ḕ', 'Ḗ', 'Ḗ',
            'Ḙ', 'Ḙ', 'Ḛ', 'Ḛ', 'Ḝ', 'Ḝ', 'Ḟ', 'Ḟ', 'Ḡ', 'Ḡ',
            'Ḣ', 'Ḣ', 'Ḥ', 'Ḥ', 'Ḧ', 'Ḧ', 'Ḩ', 'Ḩ', 'Ḫ', 'Ḫ',
            'Ḭ', 'Ḭ', 'Ḯ', 'Ḯ', 'Ḱ', 'Ḱ', 'Ḳ', 'Ḳ', 'Ḵ', 'Ḵ',
            'Ḷ', 'Ḷ', 'Ḹ', 'Ḹ', 'Ḻ', 'Ḻ', 'Ḽ', 'Ḽ', 'Ḿ', 'Ḿ',
            'Ṁ', 'Ṁ', 'Ṃ', 'Ṃ', 'Ṅ', 'Ṅ', 'Ṇ', 'Ṇ', 'Ṉ', 'Ṉ',
            'Ṋ', 'Ṋ', 'Ṍ', 'Ṍ', 'Ṏ', 'Ṏ', 'Ṑ', 'Ṑ', 'Ṓ', 'Ṓ',
            'Ṕ', 'Ṕ', 'Ṗ', 'Ṗ', 'Ṙ', 'Ṙ', 'Ṛ', 'Ṛ', 'Ṝ', 'Ṝ',
            'Ṟ', 'Ṟ', 'Ṡ', 'Ṡ', 'Ṣ', 'Ṣ', 'Ṥ', 'Ṥ', 'Ṧ', 'Ṧ',
            'Ṩ', 'Ṩ', 'Ṫ', 'Ṫ', 'Ṭ', 'Ṭ', 'Ṯ', 'Ṯ', 'Ṱ', 'Ṱ',
            'Ṳ', 'Ṳ', 'Ṵ', 'Ṵ', 'Ṷ', 'Ṷ', 'Ṹ', 'Ṹ', 'Ṻ', 'Ṻ',
            'Ṽ', 'Ṽ', 'Ṿ', 'Ṿ', 'Ẁ', 'Ẁ', 'Ẃ', 'Ẃ', 'Ẅ', 'Ẅ',
            'Ẇ', 'Ẇ', 'Ẉ', 'Ẉ', 'Ẋ', 'Ẋ', 'Ẍ', 'Ẍ', 'Ẏ', 'Ẏ',
            'Ẑ', 'Ẑ', 'Ẓ', 'Ẓ', 'Ẕ', 'Ẕ', 'Ạ', 'Ạ', 'Ả', 'Ả',
            'Ấ', 'Ấ', 'Ầ', 'Ầ', 'Ẩ', 'Ẩ', 'Ẫ', 'Ẫ', 'Ậ', 'Ậ',
            'Ắ', 'Ắ', 'Ằ', 'Ằ', 'Ẳ', 'Ẳ', 'Ẵ', 'Ẵ', 'Ặ', 'Ặ',
            'Ẹ', 'Ẹ', 'Ẻ', 'Ẻ', 'Ẽ', 'Ẽ', 'Ế', 'Ế', 'Ề', 'Ề',
            'Ể', 'Ể', 'Ễ', 'Ễ', 'Ệ', 'Ệ', 'Ỉ', 'Ỉ', 'Ị', 'Ị',
            'Ọ', 'Ọ', 'Ỏ', 'Ỏ', 'Ố', 'Ố', 'Ồ', 'Ồ', 'Ổ', 'Ổ',
            'Ỗ', 'Ỗ', 'Ộ', 'Ộ', 'Ớ', 'Ớ', 'Ờ', 'Ờ', 'Ở', 'Ở',
            'Ỡ', 'Ỡ', 'Ợ', 'Ợ', 'Ụ', 'Ụ', 'Ủ', 'Ủ', 'Ứ', 'Ứ',
            'Ừ', 'Ừ', 'Ử', 'Ử', 'Ữ', 'Ữ', 'Ự', 'Ự', 'Ỳ', 'Ỳ',
            'Ỵ', 'Ỵ', 'Ỷ', 'Ỷ', 'Ỹ', 'Ỹ', 'Ἀ', 'Ἇ', 'Ἐ', 'Ἕ',
            'Ἠ', 'Ἧ', 'Ἰ', 'Ἷ', 'Ὀ', 'Ὅ', 'Ὑ', 'Ὑ', 'Ὓ', 'Ὓ',
            'Ὕ', 'Ὕ', 'Ὗ', 'Ὗ', 'Ὠ', 'Ὧ', 'ᾈ', 'ᾏ', 'ᾘ', 'ᾟ',
            'ᾨ', 'ᾯ', 'Ᾰ', 'Ᾱ', 'Ὰ', 'Ά', 'ᾼ', 'ᾼ', 'Ὲ', 'Ή',
            'ῌ', 'ῌ', 'Ῐ', 'Ῑ', 'Ὶ', 'Ί', 'Ῠ', 'Ῡ', 'Ὺ', 'Ύ',
            'Ῥ', 'Ῥ', 'Ὸ', 'Ό', 'Ὼ', 'Ώ', 'ῼ', 'ῼ', 'Ⅰ', 'Ⅿ',
            'Ⓐ', 'Ⓩ', 'Ａ', 'Ｚ', '［', '\ufffe', '\uffff', '\uffff'
        };
    public static char[] DiffUpperCaseRanges = new char[]
        {
            'a', 'z', 'à', 'ö', 'ø', 'þ', 'ÿ', 'ÿ', 'ā', 'ā',
            'ă', 'ă', 'ą', 'ą', 'ć', 'ć', 'ĉ', 'ĉ', 'ċ', 'ċ',
            'č', 'č', 'ď', 'ď', 'đ', 'đ', 'ē', 'ē', 'ĕ', 'ĕ',
            'ė', 'ė', 'ę', 'ę', 'ě', 'ě', 'ĝ', 'ĝ', 'ğ', 'ğ',
            'ġ', 'ġ', 'ģ', 'ģ', 'ĥ', 'ĥ', 'ħ', 'ħ', 'ĩ', 'ĩ',
            'ī', 'ī', 'ĭ', 'ĭ', 'į', 'į', 'ı', 'ı', 'ĳ', 'ĳ',
            'ĵ', 'ĵ', 'ķ', 'ķ', 'ĺ', 'ĺ', 'ļ', 'ļ', 'ľ', 'ľ',
            'ŀ', 'ŀ', 'ł', 'ł', 'ń', 'ń', 'ņ', 'ņ', 'ň', 'ň',
            'ŋ', 'ŋ', 'ō', 'ō', 'ŏ', 'ŏ', 'ő', 'ő', 'œ', 'œ',
            'ŕ', 'ŕ', 'ŗ', 'ŗ', 'ř', 'ř', 'ś', 'ś', 'ŝ', 'ŝ',
            'ş', 'ş', 'š', 'š', 'ţ', 'ţ', 'ť', 'ť', 'ŧ', 'ŧ',
            'ũ', 'ũ', 'ū', 'ū', 'ŭ', 'ŭ', 'ů', 'ů', 'ű', 'ű',
            'ų', 'ų', 'ŵ', 'ŵ', 'ŷ', 'ŷ', 'ź', 'ź', 'ż', 'ż',
            'ž', 'ž', 'ſ', 'ſ', 'ƃ', 'ƃ', 'ƅ', 'ƅ', 'ƈ', 'ƈ',
            'ƌ', 'ƌ', 'ƒ', 'ƒ', 'ƙ', 'ƙ', 'ơ', 'ơ', 'ƣ', 'ƣ',
            'ƥ', 'ƥ', 'ƨ', 'ƨ', 'ƭ', 'ƭ', 'ư', 'ư', 'ƴ', 'ƴ',
            'ƶ', 'ƶ', 'ƹ', 'ƹ', 'ƽ', 'ƽ', 'ǅ', 'ǅ', 'ǆ', 'ǆ',
            'ǈ', 'ǈ', 'ǉ', 'ǉ', 'ǋ', 'ǋ', 'ǌ', 'ǌ', 'ǎ', 'ǎ',
            'ǐ', 'ǐ', 'ǒ', 'ǒ', 'ǔ', 'ǔ', 'ǖ', 'ǖ', 'ǘ', 'ǘ',
            'ǚ', 'ǚ', 'ǜ', 'ǜ', 'ǟ', 'ǟ', 'ǡ', 'ǡ', 'ǣ', 'ǣ',
            'ǥ', 'ǥ', 'ǧ', 'ǧ', 'ǩ', 'ǩ', 'ǫ', 'ǫ', 'ǭ', 'ǭ',
            'ǯ', 'ǯ', 'ǲ', 'ǲ', 'ǳ', 'ǳ', 'ǵ', 'ǵ', 'ǻ', 'ǻ',
            'ǽ', 'ǽ', 'ǿ', 'ǿ', 'ȁ', 'ȁ', 'ȃ', 'ȃ', 'ȅ', 'ȅ',
            'ȇ', 'ȇ', 'ȉ', 'ȉ', 'ȋ', 'ȋ', 'ȍ', 'ȍ', 'ȏ', 'ȏ',
            'ȑ', 'ȑ', 'ȓ', 'ȓ', 'ȕ', 'ȕ', 'ȗ', 'ȗ', 'ɓ', 'ɓ',
            'ɔ', 'ɔ', 'ɖ', 'ɖ', 'ɗ', 'ɗ', 'ə', 'ə', 'ɛ', 'ɛ',
            'ɠ', 'ɠ', 'ɣ', 'ɣ', 'ɨ', 'ɨ', 'ɩ', 'ɩ', 'ɯ', 'ɯ',
            'ɲ', 'ɲ', 'ʃ', 'ʃ', 'ʈ', 'ʈ', 'ʊ', 'ʋ', 'ʒ', 'ʒ',
            'ά', 'ά', 'έ', 'ί', 'α', 'ρ', 'ς', 'ς', 'σ', 'ϋ',
            'ό', 'ό', 'ύ', 'ώ', 'ϐ', 'ϐ', 'ϑ', 'ϑ', 'ϕ', 'ϕ',
            'ϖ', 'ϖ', 'ϣ', 'ϣ', 'ϥ', 'ϥ', 'ϧ', 'ϧ', 'ϩ', 'ϩ',
            'ϫ', 'ϫ', 'ϭ', 'ϭ', 'ϯ', 'ϯ', 'ϰ', 'ϰ', 'ϱ', 'ϱ',
            'а', 'я', 'ё', 'ќ', 'ў', 'џ', 'ѡ', 'ѡ', 'ѣ', 'ѣ',
            'ѥ', 'ѥ', 'ѧ', 'ѧ', 'ѩ', 'ѩ', 'ѫ', 'ѫ', 'ѭ', 'ѭ',
            'ѯ', 'ѯ', 'ѱ', 'ѱ', 'ѳ', 'ѳ', 'ѵ', 'ѵ', 'ѷ', 'ѷ',
            'ѹ', 'ѹ', 'ѻ', 'ѻ', 'ѽ', 'ѽ', 'ѿ', 'ѿ', 'ҁ', 'ҁ',
            'ґ', 'ґ', 'ғ', 'ғ', 'ҕ', 'ҕ', 'җ', 'җ', 'ҙ', 'ҙ',
            'қ', 'қ', 'ҝ', 'ҝ', 'ҟ', 'ҟ', 'ҡ', 'ҡ', 'ң', 'ң',
            'ҥ', 'ҥ', 'ҧ', 'ҧ', 'ҩ', 'ҩ', 'ҫ', 'ҫ', 'ҭ', 'ҭ',
            'ү', 'ү', 'ұ', 'ұ', 'ҳ', 'ҳ', 'ҵ', 'ҵ', 'ҷ', 'ҷ',
            'ҹ', 'ҹ', 'һ', 'һ', 'ҽ', 'ҽ', 'ҿ', 'ҿ', 'ӂ', 'ӂ',
            'ӄ', 'ӄ', 'ӈ', 'ӈ', 'ӌ', 'ӌ', 'ӑ', 'ӑ', 'ӓ', 'ӓ',
            'ӕ', 'ӕ', 'ӗ', 'ӗ', 'ә', 'ә', 'ӛ', 'ӛ', 'ӝ', 'ӝ',
            'ӟ', 'ӟ', 'ӡ', 'ӡ', 'ӣ', 'ӣ', 'ӥ', 'ӥ', 'ӧ', 'ӧ',
            'ө', 'ө', 'ӫ', 'ӫ', 'ӯ', 'ӯ', 'ӱ', 'ӱ', 'ӳ', 'ӳ',
            'ӵ', 'ӵ', 'ӹ', 'ӹ', 'ա', 'ֆ', 'ḁ', 'ḁ', 'ḃ', 'ḃ',
            'ḅ', 'ḅ', 'ḇ', 'ḇ', 'ḉ', 'ḉ', 'ḋ', 'ḋ', 'ḍ', 'ḍ',
            'ḏ', 'ḏ', 'ḑ', 'ḑ', 'ḓ', 'ḓ', 'ḕ', 'ḕ', 'ḗ', 'ḗ',
            'ḙ', 'ḙ', 'ḛ', 'ḛ', 'ḝ', 'ḝ', 'ḟ', 'ḟ', 'ḡ', 'ḡ',
            'ḣ', 'ḣ', 'ḥ', 'ḥ', 'ḧ', 'ḧ', 'ḩ', 'ḩ', 'ḫ', 'ḫ',
            'ḭ', 'ḭ', 'ḯ', 'ḯ', 'ḱ', 'ḱ', 'ḳ', 'ḳ', 'ḵ', 'ḵ',
            'ḷ', 'ḷ', 'ḹ', 'ḹ', 'ḻ', 'ḻ', 'ḽ', 'ḽ', 'ḿ', 'ḿ',
            'ṁ', 'ṁ', 'ṃ', 'ṃ', 'ṅ', 'ṅ', 'ṇ', 'ṇ', 'ṉ', 'ṉ',
            'ṋ', 'ṋ', 'ṍ', 'ṍ', 'ṏ', 'ṏ', 'ṑ', 'ṑ', 'ṓ', 'ṓ',
            'ṕ', 'ṕ', 'ṗ', 'ṗ', 'ṙ', 'ṙ', 'ṛ', 'ṛ', 'ṝ', 'ṝ',
            'ṟ', 'ṟ', 'ṡ', 'ṡ', 'ṣ', 'ṣ', 'ṥ', 'ṥ', 'ṧ', 'ṧ',
            'ṩ', 'ṩ', 'ṫ', 'ṫ', 'ṭ', 'ṭ', 'ṯ', 'ṯ', 'ṱ', 'ṱ',
            'ṳ', 'ṳ', 'ṵ', 'ṵ', 'ṷ', 'ṷ', 'ṹ', 'ṹ', 'ṻ', 'ṻ',
            'ṽ', 'ṽ', 'ṿ', 'ṿ', 'ẁ', 'ẁ', 'ẃ', 'ẃ', 'ẅ', 'ẅ',
            'ẇ', 'ẇ', 'ẉ', 'ẉ', 'ẋ', 'ẋ', 'ẍ', 'ẍ', 'ẏ', 'ẏ',
            'ẑ', 'ẑ', 'ẓ', 'ẓ', 'ẕ', 'ẕ', 'ạ', 'ạ', 'ả', 'ả',
            'ấ', 'ấ', 'ầ', 'ầ', 'ẩ', 'ẩ', 'ẫ', 'ẫ', 'ậ', 'ậ',
            'ắ', 'ắ', 'ằ', 'ằ', 'ẳ', 'ẳ', 'ẵ', 'ẵ', 'ặ', 'ặ',
            'ẹ', 'ẹ', 'ẻ', 'ẻ', 'ẽ', 'ẽ', 'ế', 'ế', 'ề', 'ề',
            'ể', 'ể', 'ễ', 'ễ', 'ệ', 'ệ', 'ỉ', 'ỉ', 'ị', 'ị',
            'ọ', 'ọ', 'ỏ', 'ỏ', 'ố', 'ố', 'ồ', 'ồ', 'ổ', 'ổ',
            'ỗ', 'ỗ', 'ộ', 'ộ', 'ớ', 'ớ', 'ờ', 'ờ', 'ở', 'ở',
            'ỡ', 'ỡ', 'ợ', 'ợ', 'ụ', 'ụ', 'ủ', 'ủ', 'ứ', 'ứ',
            'ừ', 'ừ', 'ử', 'ử', 'ữ', 'ữ', 'ự', 'ự', 'ỳ', 'ỳ',
            'ỵ', 'ỵ', 'ỷ', 'ỷ', 'ỹ', 'ỹ', 'ἀ', 'ἇ', 'ἐ', 'ἕ',
            'ἠ', 'ἧ', 'ἰ', 'ἷ', 'ὀ', 'ὅ', 'ὑ', 'ὑ', 'ὓ', 'ὓ',
            'ὕ', 'ὕ', 'ὗ', 'ὗ', 'ὠ', 'ὧ', 'ὰ', 'ά', 'ὲ', 'ή',
            'ὶ', 'ί', 'ὸ', 'ό', 'ὺ', 'ύ', 'ὼ', 'ώ', 'ᾀ', 'ᾇ',
            'ᾐ', 'ᾗ', 'ᾠ', 'ᾧ', 'ᾰ', 'ᾱ', 'ᾳ', 'ᾳ', 'ῃ', 'ῃ',
            'ῐ', 'ῑ', 'ῠ', 'ῡ', 'ῥ', 'ῥ', 'ῳ', 'ῳ', 'ⅰ', 'ⅿ',
            'ⓐ', 'ⓩ', 'ａ', 'ｚ', '｛', '\ufffe', '\uffff', '\uffff'
        };
}
