using System;

namespace Parcial_3.Controlador
{
    public class AccessException : Exception
    {
        public AccessException(string Message) : base(Message) { }
    }
}