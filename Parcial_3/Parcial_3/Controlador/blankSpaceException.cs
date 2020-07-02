using System;

namespace Parcial_3.Controlador
{
    public class blankSpaceException : Exception
    {
        public blankSpaceException(string Message) : base(Message) { }
    }
}