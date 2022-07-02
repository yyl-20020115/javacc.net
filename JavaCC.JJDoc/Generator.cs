using JavaCC.Parser;
namespace JavaCC.JJDoc;

public interface Generator
{
    void Text(string str);
    void Write(string str);
    void DocumentStart();
    void DocumentEnd();
    void SpecialTokens(string str);
    void TokenStart(TokenProduction tp);
    void TokenEnd(TokenProduction tp);
    void NonterminalsStart();
    void NonterminalsEnd();
    void TokensStart();
    void TokensEnd();
    void Javacode(JavaCodeProduction jcp);
    void ProductionStart(NormalProduction np);
    void ProductionEnd(NormalProduction np);
    void ExpansionStart(Expansion e, bool b);
    void ExpansionEnd(Expansion e, bool b);
    void NonTerminalStart(NonTerminal nt);
    void NonTerminalEnd(NonTerminal nt);
    void ReStart(RegularExpression re);
    void ReEnd(RegularExpression re);
    void Debug(string str);
    void Info(string str);
    void Warn(string str);
    void Error(string str);
}
