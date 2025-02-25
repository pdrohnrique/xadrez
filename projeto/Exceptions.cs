public class MovimentoInvalidoException : Exception
{
    public MovimentoInvalidoException(string mensagem) : base(mensagem) {}
}

public class JogadaInvalidaException : Exception
{
    public JogadaInvalidaException(string mensagem) : base(mensagem) {}
}