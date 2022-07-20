namespace JavaCC.Parser;
using System.Collections.Generic;

public class RCharacterList : RegularExpression
{
    public bool negated_list = false;

    public List<object> descriptors = new();


    internal static char[] diffLowerCaseRanges;


    internal static char[] diffUpperCaseRanges;

    internal bool transformed = false;


    internal RCharacterList()
    {
    }


    internal virtual void ToCaseNeutral()
    {
        int num = descriptors.Count;
        for (int i = 0; i < num; i++)
        {
            int ch;
            if (descriptors[i] is SingleCharacter character)
            {
                ch = character.CH;
                if (ch != char.ToLower((char)ch))
                {
                    descriptors.Add(new SingleCharacter(char.ToLower((char)ch)));
                }
                if (ch != char.ToUpper((char)ch))
                {
                    descriptors.Add(new SingleCharacter(char.ToUpper((char)ch)));
                }
                continue;
            }
            ch = ((CharacterRange)descriptors[i]).Left;
            int right = ((CharacterRange)descriptors[i]).Right;
            int j;
            for (j = 0; ch > diffLowerCaseRanges[j]; j += 2)
            {
            }
            if (ch < diffLowerCaseRanges[j])
            {
                if (right >= diffLowerCaseRanges[j])
                {
                    if (right > diffLowerCaseRanges[j + 1])
                    {
                        descriptors.Add(new CharacterRange(char.ToLower(diffLowerCaseRanges[j]), char.ToLower(diffLowerCaseRanges[j + 1])));
                        goto IL_01e9;
                    }
                    descriptors.Add(new CharacterRange(char.ToLower(diffLowerCaseRanges[j]), (char)(char.ToLower(diffLowerCaseRanges[j]) + right - diffLowerCaseRanges[j])));
                }
            }
            else
            {
                if (right > diffLowerCaseRanges[j + 1])
                {
                    descriptors.Add(new CharacterRange((char)(char.ToLower(diffLowerCaseRanges[j]) + ch - diffLowerCaseRanges[j]), char.ToLower(diffLowerCaseRanges[j + 1])));
                    goto IL_01e9;
                }
                descriptors.Add(new CharacterRange((char)(char.ToLower(diffLowerCaseRanges[j]) + ch - diffLowerCaseRanges[j]), (char)(char.ToLower(diffLowerCaseRanges[j]) + right - diffLowerCaseRanges[j])));
            }
            goto IL_0279;
        IL_01e9:
            for (j += 2; right > diffLowerCaseRanges[j]; j += 2)
            {
                if (right <= diffLowerCaseRanges[j + 1])
                {
                    descriptors.Add(new CharacterRange(char.ToLower(diffLowerCaseRanges[j]), (char)(char.ToLower(diffLowerCaseRanges[j]) + right - diffLowerCaseRanges[j])));
                    break;
                }
                descriptors.Add(new CharacterRange(char.ToLower(diffLowerCaseRanges[j]), char.ToLower(diffLowerCaseRanges[j + 1])));
            }
            goto IL_0279;
        IL_0279:
            for (j = 0; ch > diffUpperCaseRanges[j]; j += 2)
            {
            }
            if (ch < diffUpperCaseRanges[j])
            {
                if (right < diffUpperCaseRanges[j])
                {
                    continue;
                }
                if (right <= diffUpperCaseRanges[j + 1])
                {
                    descriptors.Add(new CharacterRange(char.ToUpper(diffUpperCaseRanges[j]), (char)(char.ToUpper(diffUpperCaseRanges[j]) + right - diffUpperCaseRanges[j])));
                    continue;
                }
                descriptors.Add(new CharacterRange(char.ToUpper(diffUpperCaseRanges[j]), char.ToUpper(diffUpperCaseRanges[j + 1])));
            }
            else
            {
                if (right <= diffUpperCaseRanges[j + 1])
                {
                    descriptors.Add(new CharacterRange((char)(char.ToUpper(diffUpperCaseRanges[j]) + ch - diffUpperCaseRanges[j]), (char)(char.ToUpper(diffUpperCaseRanges[j]) + right - diffUpperCaseRanges[j])));
                    continue;
                }
                descriptors.Add(new CharacterRange((char)(char.ToUpper(diffUpperCaseRanges[j]) + ch - diffUpperCaseRanges[j]), char.ToUpper(diffUpperCaseRanges[j + 1])));
            }
            for (j += 2; right > diffUpperCaseRanges[j]; j += 2)
            {
                if (right <= diffUpperCaseRanges[j + 1])
                {
                    descriptors.Add(new CharacterRange(char.ToUpper(diffUpperCaseRanges[j]), (char)(char.ToUpper(diffUpperCaseRanges[j]) + right - diffUpperCaseRanges[j])));
                    break;
                }
                descriptors.Add(new CharacterRange(char.ToUpper(diffUpperCaseRanges[j]), char.ToUpper(diffUpperCaseRanges[j + 1])));
            }
        }
    }


    internal virtual void SortDescriptors()
    {
        List<object> vector = new(descriptors.Count);
        int num = 0;
        for (int i = 0; i < descriptors.Count; i++)
        {
            int num2;
            if (descriptors[i] is SingleCharacter)
            {
                SingleCharacter singleCharacter = (SingleCharacter)descriptors[i];
                num2 = 0;
                while (true)
                {
                    if (num2 < num)
                    {
                        if (vector[num2] is SingleCharacter)
                        {
                            if (((SingleCharacter)vector[num2]).CH <= singleCharacter.CH)
                            {
                                if (((SingleCharacter)vector[num2]).CH == singleCharacter.CH)
                                {
                                    break;
                                }
                                goto IL_00e5;
                            }
                        }
                        else
                        {
                            int left = ((CharacterRange)vector[num2]).Left;
                            if (InRange(singleCharacter.CH, (CharacterRange)vector[num2]))
                            {
                                break;
                            }
                            if (left <= singleCharacter.CH)
                            {
                                goto IL_00e5;
                            }
                        }
                    }
                    vector.Insert(num2, singleCharacter);//.insertElementAt(singleCharacter, num2);
                    num++;
                    break;
                IL_00e5:
                    num2++;
                }
                continue;
            }
            CharacterRange characterRange = (CharacterRange)descriptors[i];
            num2 = 0;
            while (true)
            {
                if (num2 < num)
                {
                    if (vector[num2] is SingleCharacter)
                    {
                        if (InRange(((SingleCharacter)vector[num2]).CH, characterRange))
                        {
                            vector.RemoveAt(num2--);
                            num += -1;
                            goto IL_0268;
                        }
                        if (((SingleCharacter)vector[num2]).CH <= characterRange.Right)
                        {
                            goto IL_0268;
                        }
                    }
                    else
                    {
                        if (SubRange(characterRange, (CharacterRange)vector[num2]))
                        {
                            break;
                        }
                        if (SubRange((CharacterRange)vector[num2], characterRange))
                        {
                            vector[num2] = (characterRange);
                            break;
                        }
                        if (Overlaps(characterRange, (CharacterRange)vector[num2]))
                        {
                            characterRange.Left = (char)(((CharacterRange)vector[num2]).Right + 1);
                            goto IL_0268;
                        }
                        if (Overlaps((CharacterRange)vector[num2], characterRange))
                        {
                            CharacterRange obj = characterRange;
                            ((CharacterRange)vector[num2]).Right = (char)(characterRange.Left + 1);
                            characterRange = (CharacterRange)vector[num2];
                            vector[num2] = obj;//.setElementAt(obj, num2);
                            goto IL_0268;
                        }
                        if (((CharacterRange)vector[num2]).Left <= characterRange.Right)
                        {
                            goto IL_0268;
                        }
                    }
                }
                vector.Insert(num, characterRange);//.insertElementAt(characterRange, num2);
                num++;
                break;
            IL_0268:
                num2++;
            }
        }
        vector.TrimExcess();
        descriptors = vector;
    }


    internal virtual void RemoveNegation()
    {
        SortDescriptors();
        List<object> vector = new();
        int num = -1;
        for (int i = 0; i < descriptors.Count; i++)
        {
            int ch;
            if (descriptors[i] is SingleCharacter)
            {
                ch = ((SingleCharacter)descriptors[i]).CH;
                if (ch >= 0 && ch <= num + 1)
                {
                    num = ch;
                }
                else
                {
                    vector.Add(new CharacterRange((char)(num + 1), (char)((num = ch) - 1)));
                }
                continue;
            }
            ch = ((CharacterRange)descriptors[i]).Left;
            int right = ((CharacterRange)descriptors[i]).Right;
            if (ch >= 0 && ch <= num + 1)
            {
                num = right;
                continue;
            }
            vector.Add(new CharacterRange((char)(num + 1), (char)(ch - 1)));
            num = right;
        }
        if (NfaState.unicodeWarningGiven || Options.JavaUnicodeEscape)
        {
            if (num < 65535)
            {
                vector.Add(new CharacterRange((char)(num + 1), '\uffff'));
            }
        }
        else if (num < 255)
        {
            vector.Add(new CharacterRange((char)(num + 1), 'ÿ'));
        }
        descriptors = vector;
        negated_list = false;
    }

    internal static bool InRange(char c, CharacterRange r)
    {
        return (c >= r.Left && c <= r.Right) ? true : false;
    }

    internal static bool SubRange(CharacterRange r1, CharacterRange r2)
    {
        return (r1.Left >= r2.Left && r1.Right <= r2.Right) ? true : false;
    }

    internal static bool Overlaps(CharacterRange cr1, CharacterRange cr2)
    {
        return (cr1.Left <= cr2.Right && cr1.Right > cr2.Right) ? true : false;
    }


    public override Nfa GenerateNfa(bool b)
    {
        if (!transformed)
        {
            if (Options.IgnoreCase || b)
            {
                ToCaseNeutral();
                SortDescriptors();
            }
            if (negated_list)
            {
                RemoveNegation();
            }
            else
            {
                SortDescriptors();
            }
        }
        transformed = true;
        Nfa nfa = new Nfa();
        NfaState start = nfa.Start;
        NfaState end = nfa.End;
        for (int i = 0; i < descriptors.Count; i++)
        {
            if (descriptors[i] is SingleCharacter)
            {
                start.AddChar(((SingleCharacter)descriptors[i]).CH);
                continue;
            }
            CharacterRange characterRange = (CharacterRange)descriptors[i];
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


    internal RCharacterList(char ch)
    {
        negated_list = false;
        descriptors = new();
        transformed = false;
        descriptors = new();
        descriptors.Add(new SingleCharacter(ch));
        negated_list = false;
        ordinal = int.MaxValue;
    }


    public override bool CanMatchAnyChar()
    {
        return (negated_list && (descriptors == null || descriptors.Count == 0)) ? true : false;
    }

    static RCharacterList()
    {
        diffLowerCaseRanges = new char[748]
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
        diffUpperCaseRanges = new char[758]
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
}
