namespace JavaCC.JJDoc;
using JavaCC.Parser;

public interface Generator
{
    void Text(string text);
    void Write(string text);
    void DocumentStart();
    void DocumentEnd();
    void SpecialTokens(string text);
    void TokenStart(TokenProduction production);
    void TokenEnd(TokenProduction production);
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
    void Debug(string text);
    void Info(string text);
    void Warn(string text);
    void Error(string text);
}
